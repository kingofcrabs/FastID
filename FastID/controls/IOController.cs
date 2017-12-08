using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace FastID.controls
{
    class IOController
    {
        static IOController instance = null;
        public delegate void DelChannelError(int channelNum);
        public event DelChannelError onChannelError;
        private System.Timers.Timer timer = new System.Timers.Timer();
        //out
        
        
        readonly int airCylinderVaccumOn = 4;
        readonly int airCylinderVaccumDestroy = 5;
        readonly int airCylinderSource = 6;
        readonly int plateformValve = 7;
        readonly int plateformAirSource = 9;
        readonly int airCylinderSwitchValve = 10;
        readonly int mainAirSource = 11;
        //in
        readonly int airCylinderDownLimit = 5;
        readonly int airCylinderUpLimit = 6;
        readonly int vaccumReady = 4;

        public void StartCheck()
        {
            
            timer.Start();
        }

        public void CloseAll()
        {
            MPC08EDLL.outport_bit(1, airCylinderVaccumOn, 1);
            MPC08EDLL.outport_bit(1, airCylinderVaccumDestroy, 1);
            MPC08EDLL.outport_bit(1, airCylinderSource, 1);
            MPC08EDLL.outport_bit(1, plateformValve, 1);
            MPC08EDLL.outport_bit(1, plateformAirSource, 1);
            MPC08EDLL.outport_bit(1, airCylinderSwitchValve, 1); //air cylinder up
            MPC08EDLL.outport_bit(1, mainAirSource, 1);
            
        }

        internal void StopCheck()
        {
            timer.Stop();
        }



        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int channel1 = MPC08EDLL.checkin_bit(1, 1);
            int channel2 = MPC08EDLL.checkin_bit(1, 2);
            if(channel1 == 0 || channel2 == 0)
            {
                if(onChannelError != null)
                {
                    if (channel1 == 0)
                        onChannelError(1);
                    else
                        onChannelError(2);
                }
                timer.Stop();    
            }
        }

        public IOController()
        {
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed;
        }
        static public IOController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IOController();
                    
                }
                return instance;
            }
        }

        public void YellowLightOn()
        {
            MPC08EDLL.outport_bit(1, 1, 1);
            MPC08EDLL.outport_bit(1, 2, 1);
            MPC08EDLL.outport_bit(1, 3, 0);
        }

        public void RedLightOn()
        {
            MPC08EDLL.outport_bit(1, 1, 0);
            MPC08EDLL.outport_bit(1, 2, 1);
            MPC08EDLL.outport_bit(1, 3, 1);
        }

        public void GreenLightOn()
        {
            MPC08EDLL.outport_bit(1, 1, 1);
            MPC08EDLL.outport_bit(1, 2, 0);
            MPC08EDLL.outport_bit(1, 3, 1);
        }


        public void ReleaseSample()
        {
            // air cylinder source
            int res = 0;
            res = MPC08EDLL.outport_bit(1, airCylinderSource, 0);
            res = MPC08EDLL.outport_bit(1, airCylinderSwitchValve, 0); //air cylinder down
            bool bok = false;
            for (int i = 0; i < 10; i++)
            {
                if (MPC08EDLL.checkin_bit(1, airCylinderDownLimit) == 0)
                {
                    bok = true;
                    break;
                }
                System.Threading.Thread.Sleep(150);
            }
            if (!bok)
            {
                throw new Exception("气缸无法下降！");
            }
            //vaccum off
            res = MPC08EDLL.outport_bit(1, airCylinderVaccumOn, 1);
            res = MPC08EDLL.outport_bit(1, airCylinderVaccumDestroy, 0);
            System.Threading.Thread.Sleep(200);

            res = MPC08EDLL.outport_bit(1, airCylinderSwitchValve, 1); //air cylinder up
            bok = false;
            for (int i = 0; i < 10; i++)
            {
                if (MPC08EDLL.checkin_bit(1, airCylinderUpLimit) == 0)
                {
                    bok = true;
                    break;
                }
                System.Threading.Thread.Sleep(150);
            }
            res = MPC08EDLL.outport_bit(1, airCylinderVaccumDestroy, 1);
            if (!bok)
            {
                throw new Exception("气缸抬起失败！");
            }
            
            
        }

        public void GetSample()
        {
            // air cylinder source
            int res = 0;

            res = MPC08EDLL.outport_bit(1, airCylinderSource, 0);
            res = MPC08EDLL.outport_bit(1, airCylinderSwitchValve, 0); //air cylinder down
            bool bok = false;
            for (int i = 0; i < 10; i++ )
            {
                if( MPC08EDLL.checkin_bit(1,airCylinderDownLimit) == 0)
                {
                    bok = true;
                    break;
                }
                System.Threading.Thread.Sleep(150);
            }
            if(!bok)
            {
                throw new Exception("气缸无法下降！");
            }
            //vaccum on
            res = MPC08EDLL.outport_bit(1, airCylinderVaccumDestroy, 1);
            res = MPC08EDLL.outport_bit(1, airCylinderVaccumOn, 0);
            bok = false;
            System.Threading.Thread.Sleep(200);
            //for (int i = 0; i < 10; i++)
            //{
            //    if (MPC08EDLL.checkin_bit(1, vaccumReady) == 1)
            //    {
            //        bok = true;
            //        break;
            //    }
            //    System.Threading.Thread.Sleep(150);
            //}
            //if (!bok)
            //{
            //    throw new Exception("抽气失败！");
            //}
            res = MPC08EDLL.outport_bit(1, airCylinderSwitchValve, 1); //air cylinder up
            bok = false;
            for (int i = 0; i < 10; i++)
            {
                if (MPC08EDLL.checkin_bit(1, airCylinderUpLimit) == 0)
                {
                    bok = true;
                    break;
                }
                System.Threading.Thread.Sleep(150);
            }
            if (!bok)
            {
                throw new Exception("气缸抬起失败！");
            }
        }

        internal void Init()
        {
            //0 is open, 1 is close, tricky
            MPC08EDLL.outport_bit(1, airCylinderSource, 0);     // open air cylinder source
            Thread.Sleep(100);
            MPC08EDLL.outport_bit(1, airCylinderSwitchValve, 1); //air cylinder up
            MPC08EDLL.outport_bit(1, plateformAirSource, 0);
            MPC08EDLL.outport_bit(1, plateformValve, 0);
            MPC08EDLL.outport_bit(1, mainAirSource, 0);
            

        }
    }
}
