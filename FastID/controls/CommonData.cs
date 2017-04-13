using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastID.controls
{
    class CommonData
    {
        public const int BIT_1 = 0x00000001;
        public const int BIT_2 = 0x00000002;
        public const int BIT_3 = 0x00000004;
        public const int BIT_4 = 0x00000008;
        public const int BIT_5 = 0x00000010;
        public const int BIT_6 = 0x00000020;
        public const int BIT_7 = 0x00000040;
        public const int BIT_8 = 0x00000080;
        public const int BIT_9 = 0x00000100;
        public const int BIT_10 = 0x00000200;
        public const int BIT_11 = 0x00000400;
        public const int BIT_12 = 0x00000800;
        public const int BIT_13 = 0x00001000;
        public const int BIT_14 = 0x00002000;
        public const int BIT_15 = 0x00004000;
        public const int BIT_16 = 0x00008000;
        public const int BIT_17 = 0x00010000;
        public const int BIT_18 = 0x00020000;
        public const int BIT_19 = 0x00040000;
        public const int BIT_20 = 0x00080000;
        public const int BIT_21 = 0x00100000;
        public const int BIT_22 = 0x00200000;
        public const int BIT_23 = 0x00400000;
        public const int BIT_24 = 0x00800000;
        public const int BIT_25 = 0x01000000;
        public const int BIT_26 = 0x02000000;
        public const int BIT_27 = 0x04000000;
        public const int BIT_28 = 0x08000000;
        public const int BIT_29 = 0x10000000;
        public const int BIT_30 = 0x20000000;
        public const int BIT_31 = 0x40000000;

        public static int[] status_SFR = new int[MAXCARDNUM]{0x000fffff,0x000fffff,0x000fffff,0x000fffff,0x000fffff,0x000fffff};
        public static int[] status_GI = new int[MAXCARDNUM]{0x00000000,0x00000000,0x00000000,0x00000000,0x00000000,0x00000000};
        public static int[] status_GO = new int[MAXCARDNUM] { 0x0fffffff, 0x0fffffff, 0x0fffffff, 0x0fffffff, 0x0fffffff, 0x0fffffff };

        public static string strInitMsg = "Initialization succeeded!";
        public static List<string> errorMsgList_autoset = new List<string> { "auto_set error(0): 检测不到板卡！\r\n", "auto_set error(-1): 与底层通信失败！\r\n", "auto_set error(-2): 与底层通信失败！\r\n", "auto_set error(-3): 卡上无轴！\r\n", "", "", "", "", "", "", "auto_set error(-10): 卡号设置错误!\r\n" };
        public static List<string> errorMsgList_initboard = new List<string> { "init_board error(0): 检测不到卡！\r\n", "init_board error(-1): 未调用auto_set或与底层通信失败！\r\n", "init_board error(-2): 卡上无轴！\r\n", "init_board error(-3): 与auto_set检测到的轴或板卡数目不符！\r\n", "", "init_board error(-5): 驱动程序或动态库版本错误！\r\n", "init_board error(-6): 板卡版本错误!\r\n", "", "", "", "init_board error(-10): 卡号设置错误!\r\n" };
        public const int MAXCARDNUM = 6;
        public const int MAXAXENUM = 24;
        public static int currentCardNum = 0;
        public static int cardNum = 0;
        public static int g_iTotalCardNum = 0;
        public static int g_iTotalAxeNum = 0;
        public static int[] BIT_ALL_31 = new int[31]{0x00000001,
            0x00000002, 0x00000004, 0x00000008, 0x00000010,
            0x00000020, 0x00000040, 0x00000080, 0x00000100,
            0x00000200, 0x00000400, 0x00000800, 0x00001000,
            0x00002000, 0x00004000, 0x00008000, 0x00010000,
            0x00020000, 0x00040000, 0x00080000, 0x00100000,
            0x00200000, 0x00400000, 0x00800000, 0x01000000,
            0x02000000, 0x04000000, 0x08000000, 0x10000000,
            0x20000000, 0x40000000};


        public static bool BoardCheck()
        {
            return (CommonData.g_iTotalCardNum > 0 && CommonData.g_iTotalCardNum <= CommonData.MAXCARDNUM);
        }
    }
}
