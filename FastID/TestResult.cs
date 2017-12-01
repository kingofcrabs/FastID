using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastID
{
    class TestResult
    {
        public int PlateNo { get; set; }
        public int LEDNo { get; set; }
        public double L { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public double E { get; set; }
        public TestResult(int pNo, int lNo, LAB lab)
        {
            PlateNo = pNo;
            LEDNo = lNo;
            L = Math.Round(lab.l,2);
            a = Math.Round(lab.a, 2);
            b = Math.Round(lab.b, 2);
            E = Math.Round(lab.CalculateDelta(), 2); 
        }
    }
}
