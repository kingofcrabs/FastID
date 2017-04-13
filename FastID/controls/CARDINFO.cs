using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastID.controls
{
    public class CARDINFO
    {
        public static int GlobalCardNum { get; set; }
        public static int GlobalAxeNum { get; set; }
        public const int MAXCARDNUM = 6;
        public const int MAXAXENUM = 24;

        public static bool[] EnableOrg1 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set;}
        public static bool[] EnableELN1 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableELP1 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableSD1 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }

        public static bool[] EnableOrg2 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableELN2 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableELP2 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableSD2 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }

        public static bool[] EnableOrg3 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableELN3 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableELP3 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableSD3 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }

        public static bool[] EnableOrg4 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableELN4 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableELP4 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }
        public static bool[] EnableSD4 = new bool[MAXCARDNUM] { true, true, true, true, true, true };//{ get; set; }

        public static int[] CmbOrg1 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbELN1 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbELP1 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbSD1 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }

        public static int[] CmbOrg2 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbELN2 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbELP2 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbSD2 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }

        public static int[] CmbOrg3 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbELN3 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbELP3 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbSD3 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }

        public static int[] CmbOrg4 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbELN4 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbELP4 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbSD4 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }

        public static int[] CmbAlarm = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }

        public static int[] CmbPulseMode = new int[MAXCARDNUM] { 1, 1, 1, 1, 1, 1 };//{ get; set; }
        public static int[] CmbPosMode = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbHomeMode = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbEncoderMode = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }
        public static int[] CmbFrequency = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };//{ get; set; }


//        public CARDINFO()
//        {
//            CARDINFO.GlobalAxeNum = 0;
//            CARDINFO.GlobalCardNum = 0;

//            CARDINFO.EnableELN1 = new bool[MAXCARDNUM]{true, true, true, true, true, true};//
//            CARDINFO.EnableELN2 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableELN3 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableELN4 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableELP1 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableELP2 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableELP3 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableELP4 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableOrg1 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableOrg2 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableOrg3 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableOrg4 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableSD1 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableSD2 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableSD3 = new bool[MAXCARDNUM]{true, true, true, true, true, true};
//            CARDINFO.EnableSD4 = new bool[MAXCARDNUM]{true, true, true, true, true, true};

//            CARDINFO.CmbOrg1 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };//
//            CARDINFO.CmbELN1 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELP1 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbSD1 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };

//            CARDINFO.CmbOrg2 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELN2 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELP2 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbSD2 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };

//            CARDINFO.CmbOrg3 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELN3 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELP3 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbSD3 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };

//            CARDINFO.CmbOrg4 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELN4 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELP4 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbSD4 = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };

//            CARDINFO.CmbAlarm = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//////////////////////////////////////////////////////////////////
//            // board settings parameters
//////////////////////////////////////////////////////////////////
//            CARDINFO.CmbPosMode = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbHomeMode = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbPulseMode = new int[MAXCARDNUM] { 1, 1, 1, 1, 1, 1 }; ;
//            CARDINFO.CmbEncoderMode = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbFrequency = new int[MAXCARDNUM]{ 0, 0, 0, 0, 0, 0 };
//        }

//        public CARDINFO(int cardNum, int axeNum)
//        {
//            CARDINFO.GlobalCardNum = cardNum;
//            CARDINFO.GlobalAxeNum = axeNum;

//            CARDINFO.EnableELN1 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableELN2 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableELN3 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableELN4 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableELP1 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableELP2 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableELP3 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableELP4 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableOrg1 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableOrg2 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableOrg3 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableOrg4 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableSD1 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableSD2 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableSD3 = new bool[MAXCARDNUM] { true, true, true, true, true, true };
//            CARDINFO.EnableSD4 = new bool[MAXCARDNUM] { true, true, true, true, true, true };

//            CARDINFO.CmbOrg1 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELN1 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELP1 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbSD1 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };

//            CARDINFO.CmbOrg2 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELN2 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELP2 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbSD2 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };

//            CARDINFO.CmbOrg3 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELN3 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELP3 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbSD3 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };

//            CARDINFO.CmbOrg4 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELN4 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbELP4 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbSD4 = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };

//            CARDINFO.CmbAlarm = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };

//    ////////////////////////////////////////////////////////////////
//    // board settings parameters
//    ////////////////////////////////////////////////////////////////
//            CARDINFO.CmbPosMode = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbHomeMode = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbPulseMode = new int[MAXCARDNUM] { 1, 1, 1, 1, 1, 1 };
//            CARDINFO.CmbEncoderMode = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//            CARDINFO.CmbFrequency = new int[MAXCARDNUM] { 0, 0, 0, 0, 0, 0 };
//        }

        public static bool BoardCheck()
        {
            if (CARDINFO.GlobalCardNum <= 0 || CARDINFO.GlobalCardNum > CARDINFO.MAXCARDNUM)
                return false;
            else if (CARDINFO.GlobalAxeNum <= 0 || CARDINFO.GlobalAxeNum > CARDINFO.MAXAXENUM)
                return false;
            return true;
        }
    }

}
