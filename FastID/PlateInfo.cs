using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FastID
{
    [Serializable]
    public class PlateInfo
    {
        public Point firstLEDPos;
        public Point lastLEDPos;
        public int xLEDCount;
        public int yLEDCount;
        public PlateInfo(Point first, Point last, int x, int y)
        {
            firstLEDPos = first;
            lastLEDPos = last;
            xLEDCount = x;
            yLEDCount = y;
        }

        public int GetLEDID(int colIndex, int rowIndex)
        {
            return xLEDCount * rowIndex + colIndex + 1;
        }

        public PlateInfo()
        {

        }
    }

    [Serializable]
    public class Recipe
    {
        public LayoutInfo layoutInfo;
        public Point refPoint;
        public LABDelta labDelta;
        private string p;
        public Recipe(LayoutInfo layoutInfo, Point refPoint, LABDelta labDelta)
        {
            this.layoutInfo = layoutInfo;
            this.refPoint = refPoint;
            this.labDelta = labDelta;
        }
        public Recipe()
        {

        }

       
    }

    [Serializable]
    public class LayoutInfo
    {

        public PlateInfo plateInfo;
        public Point topLeft;
        public Point bottomRight;
        public int xPlateCount;
        public int yPlateCount;
        public LayoutInfo()
        {

        }
        public LayoutInfo(PlateInfo pInfo, Point tl, Point br, int x, int y)
        {
            plateInfo = pInfo;
            topLeft = tl;
            bottomRight = br;
            xPlateCount = x;
            yPlateCount = y;
        }
    }

    public struct PositionInt
    {
        public int x;
        public int y;
        public PositionInt(int xIndex, int yIndex)
        {
            this.x = xIndex;
            this.y = yIndex;
        }
    }




    [Serializable]
    public struct LABDelta
    {
        public double l;
        public double a;
        public double b;
        public double delta;
        public LABDelta(double l, double a, double b, double delta)
        {
            this.l = l;
            this.a = a;
            this.b = b;
            this.delta = delta;
        }
    }


    public struct XYZ
    {
        public double x;
        public double y;
        public double z;
        public XYZ(double xx, double yy, double zz)
        {
            this.x = xx;
            this.y = yy;
            this.z = zz;

        }
    }

    public struct LAB
    {
        public double l;
        public double a;
        public double b;
        public LAB(double l, double a, double b)
        {
            this.l = l;
            this.a = a;
            this.b = b;
            
        }

        internal double CalculateDelta()
        {
            var labDelta = GlobalVars.Instance.Recipe.labDelta;
            double ll = labDelta.l - l;
            double aa = labDelta.a - a;
            double bb = labDelta.b - b;
            return Math.Sqrt(ll * ll + aa * aa + bb * bb);
        }
    }

}
