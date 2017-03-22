using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastID
{
    [Serializable]
    public class PlateInfo
    {
        public Position firstLEDPos;
        public Position lastLEDPos;
        public int xLEDCount;
        public int yLEDCount;
        public PlateInfo(Position first, Position last, int x, int y)
        {
            firstLEDPos = first;
            lastLEDPos = last;
            xLEDCount = x;
            yLEDCount = y;
        }

        public PlateInfo()
        {

        }
    }

    [Serializable]
    public class Recipe
    {
        public LayoutInfo layoutInfo;
        public Position refPoint;
        public LABDelta labDelta;
        public Recipe(LayoutInfo layoutInfo, Position refPoint, LABDelta labDelta)
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
        public Position topLeft;
        public Position bottomRight;
        public int xPlateCount;
        public int yPlateCount;
        public LayoutInfo()
        {

        }
        public LayoutInfo(PlateInfo pInfo, Position tl, Position br, int x, int y)
        {
            plateInfo = pInfo;
            topLeft = tl;
            bottomRight = br;
            xPlateCount = x;
            yPlateCount = y;
        }
    }

    [Serializable]
    public class Position
    {
        public double x;
        public double y;
        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Position()
        {

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

}
