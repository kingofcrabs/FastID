using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FastID
{
    class TestViewer : FrameworkElement
    {
        List<LAB> labs = new List<LAB>();
       
        Dictionary<PositionInt, LAB> pos_lab;

        public Dictionary<PositionInt, LAB> Position_LAB
        {
            get
            {
                return pos_lab;
            }
            set
            {
                pos_lab = value;
                InvalidateVisual();
            }
        }

        Size sz;
        public TestViewer(double w, double h)
        {
            sz = new Size(w, h);
        }

        internal void Resize(Size newSize)
        {
            sz = newSize;
            InvalidateVisual();
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            
            var layout = GlobalVars.Instance.Recipe.layoutInfo;
            var plateInfo = layout.plateInfo;
            int xx = plateInfo.xLEDCount;
            int yy = plateInfo.yLEDCount;
            int szUint = GetUint(xx, yy);
            for (int x = 0; x < xx; x++)
            {
                for (int y = 0; y < yy; y++)
                {
                    DrawWell(x, y,szUint, drawingContext);
                }
            }
        }

        private void DrawWell(int x, int y, int szUint, DrawingContext drawingContext)
        {
            bool drawDetail = szUint > 100;
            int startX = x * szUint;
            int startY = y * szUint;
            PositionInt position = new PositionInt(x,y);
            if (Position_LAB != null && Position_LAB.ContainsKey(position))
            {
                var lab = Position_LAB[position];
                double delta = CalculateDelta(lab);
                bool bOk = delta < GlobalVars.Instance.Recipe.labDelta.delta;
                Brush brush = bOk ? Brushes.LightGreen : Brushes.Red;
                drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 1), new Rect(new Point(startX, startY), new Size(szUint, szUint)));
                if( drawDetail)
                {
                    int fontSize = CalculateFontSize(szUint);
                    int yInc = szUint / 4;
                    DrawText(string.Format("L:{0:F2}", lab.l), new Point(startX, startY), drawingContext, fontSize);
                    DrawText(string.Format("A:{0:F2}", lab.a), new Point(startX, startY + yInc), drawingContext, fontSize);
                    DrawText(string.Format("B:{0:F2}", lab.b), new Point(startX, startY + yInc * 2), drawingContext, fontSize);
                    DrawText(string.Format("δ:{0:F2}", delta), new Point(startX, startY + yInc * 3), drawingContext, fontSize);
                }
            }
            else
                drawingContext.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Black, 1), new Rect(new Point(startX, startY), new Size(szUint, szUint)));
        }

        private int CalculateFontSize(int szUint)
        {
            return szUint / 10;
        }


        private void DrawText(string str, Point point, DrawingContext drawingContext, int fontSize = 16)
        {
            if (str == null)
                return;

            var txt = new FormattedText(
               str,
               System.Globalization.CultureInfo.CurrentCulture,
               FlowDirection.LeftToRight,
               new Typeface("Courier new"),
               fontSize,
               Brushes.Black);

            drawingContext.DrawText(txt, point);
        }

        private double CalculateDelta(LAB lab)
        {
            var labDelta = GlobalVars.Instance.Recipe.labDelta;
            double l = labDelta.l - lab.l;
            double a = labDelta.a - lab.a;
            double b = labDelta.b - lab.b;
            return Math.Sqrt(l * l + a * a + b * b);
        }

        
        private int GetUint(int xx, int yy)
        {
            int wUnit = (int)(sz.Width / xx);
            int hUnit = (int)(sz.Height / yy);
            return Math.Min(wUnit, hUnit);
        }
    }
}
