using FastID.controls;
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FastID
{
    class Tester : IDisposable
    {
        public delegate void ProgressChanged(string sMessage);
        public delegate void LABUpdate(int plateID, int xLED, int yLED, LAB val, bool isLast);
        public event ProgressChanged onProgressChanged;
        public event LABUpdate onLABUpdate;
        public delegate void MoveGarbageUpdate(int plateID, int ledID);
        public event MoveGarbageUpdate onMoveGarbageUpdate;
        UsbSession admesyusbSession = null;
        Dictionary<Point, bool> testResult = new Dictionary<Point, bool>();
        public bool Finished { get; set; }
        public Point cylinderOffSet;
        bool isAbort = false;
        void Move2RefPoint()
        {
            NotifyProgressChanged("Move to reference point.");
            
        }

        public Dictionary<Point, bool> TestResult
        {
            get
            {
                return testResult;
            }
        }
        static Tester tester = null;
        static public Tester Instance
        {
            get
            {
                if (tester == null)
                    tester = new Tester();
                return tester;
            }
        }
        private Tester()
        {
            InitLABReader();
            cylinderOffSet.X = double.Parse(ConfigurationManager.AppSettings["cylinderOffsetX"]);
            cylinderOffSet.Y = double.Parse(ConfigurationManager.AppSettings["cylinderOffsetY"]);
        }
        #region Lab related

        private XYZ ReadXYZ()
        {
            double xReal, yReal, zReal;
            xReal = yReal = zReal = 0;
            GetXYZ(admesyusbSession, ref xReal, ref yReal, ref zReal);
            return new XYZ(xReal, yReal, zReal);
        }

        LAB ReadLAB()
        {
            double x, y, z;
            x = y = z = 0;
            x = double.Parse(ConfigurationManager.AppSettings["x"]);
            y = double.Parse(ConfigurationManager.AppSettings["y"]);
            z = double.Parse(ConfigurationManager.AppSettings["z"]);

            //GetXYZ(admesyusbSession, ref x, ref y, ref z);
            double xReal, yReal, zReal;
            xReal = yReal = zReal = 0;
            GetXYZ(admesyusbSession, ref xReal, ref yReal, ref zReal);
            var Lab = XYZToLab(x, y, z, xReal, yReal, zReal);
            Debug.WriteLine("L: {0} a:{1} : b{2}", Lab.l, Lab.a, Lab.b);
            return Lab;
        }

        private LAB XYZToLab(double x, double y, double z, double xReal, double yReal, double zReal)
        {
            //D65 95.0182 100,108.7485
            double[] D65Vals = new double[] { 0.950182, 1, 1.087485 };
            double[] testVals = new double[] { 1, 1, 1 };
            double[] adjustRatios = testVals;
            double e = 216 / 24389.0;
            double k = 24389.0 / 27;
            double Xr = xReal / x * adjustRatios[0];
            double Yr = yReal / y * adjustRatios[1];
            double Zr = zReal / z * adjustRatios[2];
            double fx = GetFVal(Xr, e, k);
            double fy = GetFVal(Yr, e, k);
            double fz = GetFVal(Zr, e, k);
            double L = 116 * fx - 16;
            double a = 500 * (fx - fy);
            double b = 200 * (fy - fz);
            return new LAB(L, a, b);

        }

        void GetXYZ(UsbSession admesyusbSession, ref double x, ref double y, ref double z)
        {
            admesyusbSession.Write(":meas:xyz");
            string sValues = admesyusbSession.ReadString();
            string[] xyzValues = sValues.Split(new Char[] { ',' }, 5);
            x = double.Parse(xyzValues[0]);
            y = double.Parse(xyzValues[1]);
            z = double.Parse(xyzValues[2]);
        }

        private double GetFVal(double v, double e, double k)
        {
            double fVal = v > e ? Math.Pow(v, 1 / 3.0) : (k * v + 16) / 116;
            return fVal;
        }
        #endregion
        internal XYZ GetWhiteBoardXYZ(double x, double y)
        {
            MotorController.Instance.MoveHome();
            MotorController.Instance.MoveAbsolute(x, y);
            return ReadXYZ();
        }

      

        public LAB GetStandardLab(double x, double y)
        {
            MotorController.Instance.MoveHome();
            MotorController.Instance.MoveAbsolute(x, y);
            return ReadLAB();
        }
        public void Go()
        {
            try
            {
                GoImp();
            }
            catch(Exception ex)
            {
                NotifyProgressChanged("Error:" + ex.Message);
            }
        }
       
        private void GoImp()
        {
            IOController.Instance.StartCheck();
            MotorController.Instance.MoveHome();
            Finished = false;
            isAbort = false;
            var recipe = GlobalVars.Instance.Recipe;
            var layoutInfo = recipe.layoutInfo;
            int plateCnt = layoutInfo.xPlateCount * layoutInfo.yPlateCount;

            var plateInfo = layoutInfo.plateInfo;
            int ledCnt = plateInfo.xLEDCount * plateInfo.yLEDCount;
            testResult.Clear();
            for (int plateIndex = 0; plateIndex < plateCnt; plateIndex++)
            {
                NotifyProgressChanged(string.Format("Plate:{0}", plateIndex + 1));
                for (int ledIndex = 0; ledIndex < ledCnt; ledIndex++)
                {
                    int plateID = plateIndex + 1;
                    int ledID = ledIndex + 1;
                    Move2Led(plateID, ledID);
                    var lab = ReadLAB();
                    bool bOk = lab.CalculateDelta() < GlobalVars.Instance.Recipe.labDelta.delta;
                    testResult.Add(new Point(plateID, ledID), bOk);
                    NotifyLABUpdate(plateID, ledID, lab);
                    Thread.Sleep(100);
                    if (isAbort)
                    {
                        break;
                    }
                }
                if (isAbort)
                {
                    NotifyProgressChanged("Abort！");
                    break;
                }

            }
            Finished = true;
            IOController.Instance.StopCheck();
            NotifyProgressChanged("Finished!");
        }

        private void InitLABReader()
        {
            if (admesyusbSession != null)
                return;

            string[] resources;
            Boolean DeviceNotAvailable = true;

            /* Find all resources */
            resources = ResourceManager.GetLocalManager().FindResources("?*");
            /* find if an attached device is an Admesy device. first found Admesy device is used */
            foreach (string admesy_device in resources)
            {
                if (admesy_device.Contains("0x1781:") || admesy_device.Contains("0x23CF"))
                {
                    /* open usb session */
                    admesyusbSession = (UsbSession)ResourceManager.GetLocalManager().Open(admesy_device);
                    /* print to let user now wich device is used */
                    Console.WriteLine("Selected device:");
                    Console.WriteLine(admesyusbSession.ResourceName);
                    Console.WriteLine();
                    /* set DeviceNotAvailable because we have found one */
                    DeviceNotAvailable = false;
                    break;
                }
            }
            if (DeviceNotAvailable)
            {
                throw new Exception("No Admesy device detected.");
            }

            admesyusbSession.Write(":configure:white:use 1");
            int acquireTime = int.Parse(ConfigurationManager.AppSettings["acquireTime"]);
            admesyusbSession.Write(string.Format(":sens:aver {0}", acquireTime));
          
        }

        void NotifyProgressChanged(string sMessage)
        {
            if (onProgressChanged != null)
                onProgressChanged(sMessage);
        }
        void NotifyLABUpdate(int plateID, int ledID, LAB val)
        {
            if (onLABUpdate != null)
            {
                int colIndex = 0;
                int rowIndex = 0;
                GetLEDColRow(ledID, ref colIndex, ref rowIndex);
                bool isLast = IsLastLED(ledID);
                onLABUpdate(plateID,
                    colIndex,
                    rowIndex,
                    val, isLast);
            }
        }

        private bool IsLastLED(int ledID)
        {
            var recipe = GlobalVars.Instance.Recipe;
            var layoutInfo = recipe.layoutInfo;
            var plateInfo = layoutInfo.plateInfo;
            var xLedCnt = plateInfo.xLEDCount;
            var yLedCnt = plateInfo.yLEDCount;
            return ledID == xLedCnt * yLedCnt;
        }


        public void Move2Led(int plateID, int ledID, bool forCylinder = false)
        {
            //NotifyProgressChanged(string.Format("LED:{0}", ledID));
            var recipe = GlobalVars.Instance.Recipe;
            var layoutInfo = recipe.layoutInfo;
            int colIndex, rowIndex;
            colIndex = rowIndex = 0;
           
            GetPlateColRow(plateID, ref colIndex, ref rowIndex);

            Point platePos = GetPosition(
                layoutInfo.topLeft,
                layoutInfo.bottomRight, 
                colIndex, 
                rowIndex, 
                layoutInfo.xPlateCount,
                layoutInfo.yPlateCount);

            GetLEDColRow(ledID,ref colIndex, ref rowIndex);
            var plateInfo = layoutInfo.plateInfo;
            Point ledPosInPlate = GetPosition(
                plateInfo.firstLEDPos,
                plateInfo.lastLEDPos,
                colIndex,
                rowIndex,
                plateInfo.xLEDCount,
                plateInfo.yLEDCount);


            double x = platePos.X + ledPosInPlate.X;
            double y = platePos.Y + ledPosInPlate.Y;
            if (forCylinder)
            {
                x += cylinderOffSet.X;
                y += cylinderOffSet.Y;
            }
            MotorController.Instance.MoveAbsolute(x, y);
            
        }

        static public Point  GetPosition(Point ptStart, Point ptEnd, int colIndex, int rowIndex, int xCnt, int yCnt)
        {
            var topLeftPos = ptStart;
            var bottomRightPos = ptEnd;
            double w = ptEnd.X - ptStart.X;
            double h = ptEnd.Y - ptStart.Y;
            double x = xCnt == 1 ? ptStart.X : ptStart.X + colIndex * w / (xCnt-1);

            double y = yCnt == 1 ? ptStart.Y : ptStart.Y + rowIndex * h / (yCnt-1);
            return new Point(x, y);
        }

      

        static public void GetPlateColRow(int plateID, ref int colIndex, ref int rowIndex)
        {
            var recipe = GlobalVars.Instance.Recipe;
            var layoutInfo = recipe.layoutInfo;
            var xPlateCnt = layoutInfo.xPlateCount;
            var yPlateCnt = layoutInfo.yPlateCount;
            colIndex = (plateID - 1) / yPlateCnt;
            rowIndex = plateID - colIndex * yPlateCnt - 1;
        }
        static public void GetLEDColRow(int ledID, ref int colIndex, ref int rowIndex)
        {
 	        var recipe = GlobalVars.Instance.Recipe;
            var layoutInfo = recipe.layoutInfo;
            var plateInfo = layoutInfo.plateInfo;
            var xLedCnt = plateInfo.xLEDCount;
            var yLedCnt = plateInfo.yLEDCount;
            rowIndex = (ledID - 1) / xLedCnt;
            colIndex = ledID - rowIndex * xLedCnt - 1;
        }

        internal static Size GetLEDSize()
        {
            var recipe = GlobalVars.Instance.Recipe;
            var layoutInfo = recipe.layoutInfo;
            var plateInfo = layoutInfo.plateInfo;
            var xLedCnt = plateInfo.xLEDCount;
            var yLedCnt = plateInfo.yLEDCount;
            double w = plateInfo.lastLEDPos.X - plateInfo.firstLEDPos.X;
            double h = plateInfo.lastLEDPos.Y - plateInfo.firstLEDPos.Y;
            double ledW = w / (xLedCnt - 1) * 0.2;
            double ledH = h / (yLedCnt - 1) * 0.2;
            return new Size(ledW, ledH);
        }

        internal void Abort()
        {
            isAbort = true;
        }



        public void Dispose()
        {
            if(admesyusbSession != null)
                admesyusbSession.Dispose();
        }


        internal void MoveInvalidSamples2Garbage()
        {
            foreach (var pair in Tester.Instance.TestResult)
            {
                if (isAbort)
                {
                    NotifyProgressChanged("Abort");
                    break;
                }
                    
                if (!pair.Value)
                {
                    int plateID = (int)pair.Key.X;
                    int ledID = (int)pair.Key.Y;
                    Tester.Instance.Move2Led(plateID, ledID,true);
                    IOController.Instance.GetSample();
                    //simulationViewer.SetCurrentInfo(plateID, ledID);
                    
                    if (onMoveGarbageUpdate != null)
                        onMoveGarbageUpdate(plateID, ledID);
                    MotorController.Instance.Move2Garbage();
                    IOController.Instance.ReleaseSample();
                }
            }
        }

        internal int StatisticInvalidLEDs()
        {
            int cnt = 0;
            foreach(bool val in testResult.Values)
            {
                if (!val)
                    cnt++;
            }
            return cnt;
        }
    }
}
