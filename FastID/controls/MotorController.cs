using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FastID.controls
{
    class MotorController
    {
        static private MotorController instance;

        public SortedDictionary<int, int> Axis_Dir;
        public int[] Axis;
        public int[] Dir;
        int garbageX = int.Parse(ConfigurationManager.AppSettings["garbageX"]);
        int garbageY = int.Parse(ConfigurationManager.AppSettings["garbageY"]);
        const int  stepsPerMM = 100;

        public double m_dbSpeedLow = stepsPerMM * 10; //10mm
        public double m_dbSpeedHigh = stepsPerMM * 1000; // 100mm
        public double m_dbSpeedAccel = stepsPerMM * 1000; // 5s accelerate
        private Point lastPt = new Point(0,0);
        bool fastMove = true;
        bool initialed = false;
        bool moveHome = false;
        static public MotorController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MotorController();
                }
                return instance;
            }
        }

        private MotorController()
        {
            Init();
        }

        public void Init()
        {
            if (initialed)
                return;
            
            int speed = int.Parse(ConfigurationManager.AppSettings["speed"]);
            m_dbSpeedHigh = speed * stepsPerMM;
            m_dbSpeedAccel = speed * stepsPerMM;
            CommonData.g_iTotalAxeNum = MPC08EDLL.auto_set();
            if (CommonData.g_iTotalAxeNum <= 0)
            {
                string errMsgs = "";
                CommonData.errorMsgList_autoset.ForEach(x=>errMsgs = errMsgs + x);
                throw new Exception(string.Format("auto_set() ERROR! Reason can be one of the following: {0}", errMsgs));
            }
            CommonData.g_iTotalCardNum = MPC08EDLL.init_board();
            if (CommonData.g_iTotalCardNum <= 0)
            {
                string errMsgs = "";
                CommonData.errorMsgList_initboard.ForEach(x=>errMsgs = errMsgs + x);
                throw new Exception(string.Format("init_board() ERROR! Reason can be one of the following: {0}",errMsgs));
            }

            for (int i = 0; i < CommonData.g_iTotalAxeNum; i++)
            {
                if (fastMove)
                {
                    MPC08EDLL.set_profile(i + 1, m_dbSpeedLow, m_dbSpeedHigh, m_dbSpeedAccel);
                    MPC08EDLL.set_maxspeed(i + 1, m_dbSpeedHigh);
                }
                else
                {
                    MPC08EDLL.set_maxspeed(i + 1, m_dbSpeedLow);
                    MPC08EDLL.set_conspeed(i + 1, m_dbSpeedLow);
                }
            }
            initialed = true;
        }

        public void Move2Garbage()
        {
            MoveAbsolute(garbageX, garbageY);
        }

        public void MoveHome()
        {
            if (moveHome)
                return;
            
            MPC08EDLL.con_hmove2(1, -1, 2, -1);
            int i = 300;  //30 seconds
            int ch1, ch2;
            ch1 = ch2 = 0;
            while(i > 0)
            {
                ch1 = MPC08EDLL.check_limit(1);
                ch2 = MPC08EDLL.check_limit(2);
                if (ch1 == -1 && ch2 == -1)
                    break;
                Thread.Sleep(100);
                i--;
            }
            if (ch1 != -1 || ch2 != -1)
            {
                throw new Exception("无法回零！");
            }
            
            lastPt = new Point(0, 0);
            moveHome = true;
        }


        public void MoveAbsolute(double x, double y)
        {
            if (!CommonData.BoardCheck())
                throw new Exception("No card found!");

            double disX = x - lastPt.X;
            double disY = y - lastPt.Y;
           
            int xSteps = (int)(disX * stepsPerMM);
            int ySteps = (int)(disY * stepsPerMM);
            Debug.WriteLine("x step: {0} y step: {1}", xSteps, ySteps);
            lastPt = new Point(x, y);
            if(fastMove)
            {
                MPC08EDLL.fast_pmove2(1, xSteps, 2, ySteps);
                WaitForMoveFinish(30);
            }
            else
                MPC08EDLL.con_pmove2(1, xSteps, 2, ySteps);


        }


        private void WaitForMoveFinish(int seconds)
        {
            int ch1,ch2;
            ch1 = ch2 = 1;
            while(seconds > 0)
            {
                ch1 = MPC08EDLL.check_done(1);
                ch2 = MPC08EDLL.check_done(2);
                if (ch1 == 0 && ch2 == 0)
                    break;
                Thread.Sleep(1000);
                seconds--;
            }
            if( ch1 != 0 || ch2 != 0)
            {
                throw new Exception("无法运动到位！");
            }

        }
    }
}
