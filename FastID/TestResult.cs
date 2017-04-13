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
            L = lab.l;
            a = lab.a;
            b = lab.b;
            E = lab.CalculateDelta();
        }
    }
}
