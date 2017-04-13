using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FastID
{
    class SimulationViewer : FrameworkElement
    {
        public int CurrentLEDID { get; set; }
        public int CurrentPlateID { get; set; }
        
        
        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            var refPoint = GlobalVars.Instance.Recipe.refPoint;
            var layoutInfo = GlobalVars.Instance.Recipe.layoutInfo;
            var w = layoutInfo.bottomRight.X - layoutInfo.topLeft.X;
            var h = layoutInfo.bottomRight.Y - layoutInfo.topLeft.Y;

            var mapX = MapX(refPoint.X); 
            var mapY = MapY(refPoint.Y);
            Pen redPen = new Pen(Brushes.Red, 1);
            int r = 10;
            //draw ref point
            drawingContext.DrawEllipse(null, redPen, new Point(mapX, mapY), r, r);


            int colIndex, rowIndex;
            colIndex = rowIndex = 0;
            int plateCnt = layoutInfo.xPlateCount * layoutInfo.yPlateCount;
            
            var plateInfo = layoutInfo.plateInfo;
            int ledCnt = plateInfo.xLEDCount * plateInfo.yLEDCount;
            var ledSize = Tester.GetLEDSize();
            for (int plateIndex = 0; plateIndex < plateCnt; plateIndex++)
            {
                int plateID = plateIndex + 1;
                Tester.GetPlateColRow(plateID, ref colIndex, ref rowIndex);
                Point platePos = Tester.GetPosition(
                layoutInfo.topLeft,
                layoutInfo.bottomRight,
                colIndex,
                rowIndex,
                layoutInfo.xPlateCount,
                layoutInfo.yPlateCount);

                for (int ledIndex = 0; ledIndex < ledCnt; ledIndex++)
                {
                    int ledID = ledIndex + 1;
                    Tester.GetLEDColRow(ledID, ref colIndex, ref rowIndex);
                    Size sz = Tester.GetLEDSize();
                    Point ledPosInPlate = Tester.GetPosition(
                        plateInfo.firstLEDPos,
                        plateInfo.lastLEDPos,
                        colIndex,
                        rowIndex,
                        plateInfo.xLEDCount,
                        plateInfo.yLEDCount);
                    
                    double x = platePos.X + ledPosInPlate.X;
                    double y = platePos.Y + ledPosInPlate.Y;
                    bool isCurrent = plateID == CurrentPlateID && ledID == CurrentLEDID;
                    DrawLEDs(x, y, sz, isCurrent,drawingContext);
                }
            }
        }

        private void DrawLEDs(double x, double y, System.Windows.Size sz, bool isCurrent ,DrawingContext drawingContext)
        {
            double mapX = MapX(x);
            double mapY = MapY(y);
            double mapW = MapX(sz.Width);
            double mapH = MapY(sz.Height);
            double startX = mapX - mapW/2;
            double startY = mapY - mapH/2;
            Brush brush = isCurrent ? Brushes.Red : Brushes.Black;
            drawingContext.DrawRectangle(brush, new Pen(brush,1),new Rect(new Point(startX,startY),new Size(mapW,mapH)));
        }

        private double MapX(double v)
        {
            var refPoint = GlobalVars.Instance.Recipe.refPoint;
            var layoutInfo = GlobalVars.Instance.Recipe.layoutInfo;
            var w = layoutInfo.bottomRight.X - layoutInfo.topLeft.X;
            var h = layoutInfo.bottomRight.Y - layoutInfo.topLeft.Y;
            return v/ w * this.ActualWidth;
        }

        private double MapY(double v)
        {
            var refPoint = GlobalVars.Instance.Recipe.refPoint;
            var layoutInfo = GlobalVars.Instance.Recipe.layoutInfo;
            var w = layoutInfo.bottomRight.X - layoutInfo.topLeft.X;
            var h = layoutInfo.bottomRight.Y - layoutInfo.topLeft.Y;
            return v / h * this.ActualHeight;
        }

        private void DrawRefrencePoint(System.Windows.Media.DrawingContext drawingContext)
        {
            var refPoint = GlobalVars.Instance.Recipe.refPoint;
            var layoutInfo = GlobalVars.Instance.Recipe.layoutInfo;
            var w = layoutInfo.bottomRight.X - layoutInfo.topLeft.X;
            var h = layoutInfo.bottomRight.Y - layoutInfo.topLeft.Y;

            var mapX = refPoint.X / w * this.ActualWidth;
            var mapY = refPoint.Y / h * this.ActualHeight;
            Pen redPen = new Pen(Brushes.Red,1);
            int r = 10;
            drawingContext.DrawEllipse(null, redPen, new Point(mapX, mapY), r,r);
        }

        internal void SetCurrentInfo(int plateID, int ledID)
        {
            CurrentPlateID = plateID;
            CurrentLEDID = ledID;
            InvalidateVisual();
        }
    }
}
