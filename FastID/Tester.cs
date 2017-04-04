using FastID.controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FastID
{
    class Tester
    {
        public delegate void ProgressChanged(string sMessage);
        public delegate void LABUpdate(int plateID, int xLED, int yLED, LAB val, bool isLast);
        public event ProgressChanged onProgressChanged;
        public event LABUpdate onLABUpdate;
        
        public bool Finished { get; set; }
        bool isAbort = false;
        void Move2RefPoint()
        {
            NotifyProgressChanged("Move to reference point.");
            
        }

        LAB ReadLAB()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            double l = 13 + rnd.Next(40000) / 10000.0;
            double a = 14 + rnd.Next(40000) / 10000.0;
            double b = 15 + rnd.Next(40000) / 10000.0;
            return new LAB(l, a, b);
        }

        public void Go()
        {
            Finished = false;
            isAbort = false;
            var recipe = GlobalVars.Instance.Recipe;
            var layoutInfo = recipe.layoutInfo;
            int plateCnt = layoutInfo.xPlateCount * layoutInfo.yPlateCount;
            
            var plateInfo = layoutInfo.plateInfo;
            int ledCnt = plateInfo.xLEDCount * plateInfo.yLEDCount;
            for(int plateIndex = 0; plateIndex < plateCnt; plateIndex++)
            {
                NotifyProgressChanged(string.Format("Plate:{0}", plateIndex + 1));
                for(int ledIndex = 0; ledIndex < ledCnt; ledIndex++)
                {
                    int plateID = plateIndex+1;
                    int ledID = ledIndex+1;
                    Move2Led(plateID,ledID);
                    var lab = ReadLAB();
                    NotifyLABUpdate(plateID, ledID, lab);
                    Thread.Sleep(100);
                    if(isAbort)
                    {
                        break;
                    }
                }
                if(isAbort)
                {
                    NotifyProgressChanged("Aborted！");
                    break;
                }
                
            }
            Finished = true;
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


        public void Move2Led(int plateID, int ledID)
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
            //MotorController.Instance.MoveAbsolute(x, y);
            
        }

        static public Point  GetPosition(Point ptStart, Point ptEnd, int colIndex, int rowIndex, int xCnt, int yCnt)
        {
            var topLeftPos = ptStart;
            var bottomRightPos = ptEnd;
            double w = ptEnd.X - ptStart.X;
            double h = ptEnd.Y - ptStart.Y;
            double x = ptStart.X + colIndex * w / xCnt;
            double y = ptStart.Y + rowIndex * h / yCnt;
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

      
    }
}
