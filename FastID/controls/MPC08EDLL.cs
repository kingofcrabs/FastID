using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FastID.controls
{
    class MPC08EDLL
    {
        //////////////////////////////////////////////////////////////////////////
        // 初始化函数

        /// <summary>
        /// 使用板卡时调用的第一个函数
        /// 功能： 板卡初始化
        /// 返回值： 总轴数（正确）-----  值小于零（错误）
        /// </summary>
        /// <returns> 总轴数（正确）-----  值小于零（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 auto_set();

        /// <summary>
        /// 使用板卡时调用的第二个函数
        /// 功能： 板卡初始化
        /// 返回值： 总卡数（正确）-----  值小于零（错误） 
        /// </summary>
        /// <returns> 总卡数（正确）-----  值小于零（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 init_board();

        ////////////////////////////////////////////////////////////////////////
        // 轴属性设置函数
        /// <summary>
        /// 功能： 设置脉冲输出模式
        /// 参数：
        ///     ch ----- 轴号
        ///     mode ---- 模式： 0 （CW/CCW) -- 1 (Pul/Dir)
        ///     logic ---- 暂不使用，设置为 0
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="mode">模式： 0 （CW/CCW) -- 1 (Pul/Dir)</param>
        /// <param name="logic"> 暂不使用，设置为 0</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_outmode(Int32 ch, Int32 mode, Int32 logic);

        /// <summary>
        /// 功能： 设置回零模式
        /// 参数：
        ///     ch ----- 轴号
        ///     mode ---- 模式
        ///                 0 ---- ORG 信号有效立即停止
        ///                 1 ---- 编码器 Z 信号有效时立即停止
        ///                 2 ---- 快速模式下，遇到 ORG 信号时开始减速，直到停止
        ///                 3 ---- 快速模式下，遇到 ORG 信号时开始减速，直到编码器 Z 信号有效时立即停止
        /// 返回值： 0（正确）-----  -1（错误） 
        ///                 0 ---- ORG 信号有效立即停止
        ///                 1 ---- 编码器 Z 信号有效时立即停止
        ///                 2 ---- 快速模式下，遇到 ORG 信号时开始减速，直到停止
        ///                 3 ---- 快速模式下，遇到 ORG 信号时开始减速，直到编码器 Z 信号有效时立即停止</summary>
        /// <param name="ch">轴号</param>
        /// <param name="origin_mode">模式
        /// </param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_home_mode(Int32 ch, Int32 origin_mode);

        /// <summary>
        /// 功能： 设置方向信号的电平
        /// 参数：
        ///     ch ----- 轴号
        ///     dir ---- 方向： 1 （正向) -- -1 (负向)
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="dir">方向： 1 （正向) -- -1 (负向)</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_dir(Int32 ch, Int32 dir);

        /// <summary>
        /// 功能： 使能轴报警信号
        /// 参数：
        ///     ch ----- 轴号
        ///     flag ---- 使能标志： 1 （使能) -- 0 (不使能)
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="flag">使能标志： 1 （使能) -- 0 (不使能)</param>
        /// <returns>0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 enable_alm(Int32 ch, Int32 flag);

        /// <summary>
        /// 功能： 使能轴限位信号
        /// 参数：
        ///     ch ----- 轴号
        ///     flag ---- 使能标志： 1 （使能) -- 0 (不使能)
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="flag">使能标志： 1 （使能) -- 0 (不使能)</param>
        /// <returns>0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 enable_el(Int32 ch, Int32 flag);

        /// <summary>
        /// 功能： 使能轴原点信号
        /// 参数：
        ///     ch ----- 轴号
        ///     flag ---- 使能标志： 1 （使能) -- 0 (不使能)
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="flag">使能标志： 1 （使能) -- 0 (不使能)</param>
        /// <returns>0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 enable_org(Int32 ch, Int32 flag);

        /// <summary>
        /// 功能：设置轴报警信号有效电平
        /// 参数：
        ///     ch ----- 轴号
        ///     flag ---- 有效电平： 1 （高电平) -- 0 (低电平)
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="flag">有效电平： 1 （高电平) -- 0 (低电平)</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_alm_logic(Int32 ch, Int32 flag);

        /// <summary>
        /// 功能：设置轴限位信号有效电平
        /// 参数：
        ///     ch ----- 轴号
        ///     flag ---- 有效电平： 1 （高电平) -- 0 (低电平)
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="flag">有效电平： 1 （高电平) -- 0 (低电平)</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_el_logic(Int32 ch, Int32 flag);

        /// <summary>
        /// 功能：设置轴原点信号有效电平
        /// 参数：
        ///     ch ----- 轴号
        ///     flag ---- 有效电平： 1 （高电平) -- 0 (低电平)
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="flag">有效电平： 1 （高电平) -- 0 (低电平)</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_org_logic(Int32 ch, Int32 flag);

        /// <summary>
        /// 功能：使能板卡报警信号
        /// 参数：
        ///     cardno ----- 卡号
        ///     flag ----  使能标志： 1 （使能) -- 0 (不使能)
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="flag"> 使能标志： 1 （使能) -- 0 (不使能)</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 enable_card_alm(Int32 cardno, Int32 flag);

        /// <summary>
        /// 功能：设置卡报警信号有效电平
        /// 参数：
        ///     cardno ----- 轴号
        ///     flag ---- 有效电平： 1 （高电平) -- 0 (低电平)
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="cardno">轴号</param>
        /// <param name="mode">有效电平： 1 （高电平) -- 0 (低电平)</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_card_alm_logic(Int32 cardno, Int32 mode);
 
        /// <summary>
        /// 功能：检测板卡报警信号有效状态
        /// 参数：
        ///     ch ----- 轴号
        /// 返回值： 
        ///     1 --- 有效
        ///     0 --- 无效
        ///    -3 --- 出错
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns>
        ///     1 --- 有效
        ///     0 --- 无效
        ///    -3 --- 出错</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 check_card_alarm(Int32 ch);

        /// <summary>
        /// 功能：设置轴最大运行速度
        /// 参数：
        ///     ch ----- 轴号
        ///     speed ---- 最大速度（正值）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="speed">最大速度（正值）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_maxspeed(Int32 ch, double speed);

        /// <summary>
        /// 功能：设置轴常速运行速度
        /// 参数：
        ///     ch ----- 轴号
        ///     speed ---- 常速速度（正值）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="conspeed">常速速度（正值）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_conspeed(Int32 ch, double conspeed);

        /// <summary>
        /// 功能：设置常速插补运动运行速度
        /// 参数：
        ///     speed ---- 常速速度（正值）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="speed">常速速度（正值）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_vector_conspeed(double speed);
 
        /// <summary>
        /// 功能：设置轴快速运动参数
        /// 参数：
        ///     ch ----- 轴号
        ///     vl ---- 快速运动低速（正值）
        ///     vh ---- 快速运动高速（正值）
        ///     vec_ad ---- 上升加速度（正值）
        ///     vec_dc ---- 下降加速度（正值）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="vl">快速运动低速（正值）</param>
        /// <param name="vh">快速运动高速（正值）</param>
        /// <param name="vec_ad">上升加速度（正值）</param>
        /// <param name="vec_dc">下降加速度（正值）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_profile(Int32 ch, double vl, double vh, double vec_ad);

        /// <summary>
        /// 功能：设置快速插补运动参数
        /// 参数：
        ///     vl ---- 快速运动低速（正值）
        ///     vh ---- 快速运动高速（正值）
        ///     vec_ad ---- 上升加速度（正值）
        ///     vec_ac ---- 下降加速度（正值）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="vl">快速运动低速（正值）</param>
        /// <param name="vh">快速运动高速（正值）</param>
        /// <param name="vec_ad">上升加速度（正值）</param>
        /// <param name="vec_ac">下降加速度（正值）</param>
        /// <returns>0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_vector_profile(double vl, double vh, double vec_ad, double vec_ac);
 
        /// <summary>
        /// 功能：设置轴 S 型曲线运动升降速范围
        /// 参数：
        ///     ch ----- 轴号
        ///     accel_sec ---- 从 低速 到 （低速+accel_sec）为 S 型曲线加加速段
        ///     decel_sec ---- 从 （高速-decel_sec）到 高速 为 S 型曲线减加速段
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="accel_sec">从 低速 到 （低速+accel_sec）为 S 型曲线加加速段</param>
        /// <param name="decel_sec">从 （高速-decel_sec）到 高速 为 S 型曲线减加速段</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_s_section(Int32 ch, double accel_sec, double decel_sec);

        /// <summary>
        /// 功能：设置快速运动曲线模式
        /// 参数：
        ///     ch ---- 轴号
        ///     mode ---- 0：T型加减速    1：S型加减速
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="mode">0：T型加减速    1：S型加减速</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_s_curve(Int32 ch, Int32 mode);

        /// <summary>
        /// 功能：设置轴当前绝对位置
        /// 参数：
        ///     ch ---- 轴号
        ///     pos ---- 绝对位置值（正值）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="pos">绝对位置值（正值）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_abs_pos(Int32 ch, Int32 pos);
 
        /// <summary>
        /// 功能：将轴的绝对位置和相对位置清零
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 reset_pos(Int32 ch);

        ///////////////////////////////////////////////////////////////////////
        //运动指令函数
        /// <summary>
        /// 功能：单轴常速点位运动指令
        /// 参数：
        ///     ch ---- 轴号
        ///     step ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="step">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_pmove(Int32 ch, Int32 step);

        /// <summary>
        /// 功能：单轴快速点位运动指令
        /// 参数：
        ///     ch ---- 轴号
        ///     step ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="step">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns>0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_pmove(Int32 ch, Int32 step);

        /// <summary>
        /// 功能：两轴常速点位运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_pmove2(Int32 ch1, Int32 step1, Int32 ch2, Int32 step2);

        /// <summary>
        /// 功能：两轴快速点位运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns></returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_pmove2(Int32 ch1, Int32 step1, Int32 ch2, Int32 step2);

        /// <summary>
        /// 功能：三轴常速点位运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch3 ---- 轴号
        ///     step3 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="step3">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_pmove3(Int32 ch1, Int32 step1, Int32 ch2, Int32 step2, Int32 ch3, Int32 step3);

        /// <summary>
        /// 功能：三轴快速点位运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch3 ---- 轴号
        ///     step3 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="step3">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_pmove3(Int32 ch1, Int32 step1, Int32 ch2, Int32 step2, Int32 ch3, Int32 step3);

        /// <summary>
        /// 功能：四轴常速点位运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch3 ---- 轴号
        ///     step3 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch4 ---- 轴号
        ///     step4 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="step3">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch4">轴号</param>
        /// <param name="step4">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_pmove4(Int32 ch1, Int32 step1, Int32 ch2, Int32 step2, Int32 ch3, Int32 step3, Int32 ch4, Int32 step4);

        /// <summary>
        /// 功能：四轴快速点位运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch3 ---- 轴号
        ///     step3 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch4 ---- 轴号
        ///     step4 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="step3">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch4">轴号</param>
        /// <param name="step4">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_pmove4(Int32 ch1, Int32 step1, Int32 ch2, Int32 step2, Int32 ch3, Int32 step3, Int32 ch4, Int32 step4);

        /// <summary>
        /// 功能：单轴常速点位绝对位置运动指令
        /// 参数：
        ///     ch ---- 轴号
        ///     step ---- 绝对位置值
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="step">绝对位置值</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_pmove_to(Int32 ch, Int32 step);

        /// <summary>
        /// 功能：单轴快速点位绝对位置运动指令
        /// 参数：
        ///     ch ---- 轴号
        ///     step ---- 绝对位置值
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="step">绝对位置值</param>
        /// <returns> 0（正确）-----  -1（错误） </returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_pmove_to(Int32 ch, Int32 step);

        /// <summary>
        /// 功能：单轴常速连续运动指令
        /// 参数：
        ///     ch ---- 轴号
        ///     dir ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="dir"> 方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误） </returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_vmove(Int32 ch,Int32 dir);

        /// <summary>
        /// 功能：单轴快速连续运动指令
        /// 参数：
        ///     ch ---- 轴号
        ///     dir ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="dir">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_vmove(Int32 ch,Int32 dir);

        /// <summary>
        /// 功能：两轴常速连续运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_vmove2(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2);

        /// <summary>
        /// 功能：两轴快速连续运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_vmove2(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2);

        /// <summary>
        /// 功能：三轴常速连续运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        ///     ch3 ---- 轴号
        ///     dir3 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="dir3">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_vmove3(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2,Int32 ch3,Int32 dir3);

        /// <summary>
        /// 功能：三轴快速连续运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        ///     ch3 ---- 轴号
        ///     dir3 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="dir3">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_vmove3(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2,Int32 ch3,Int32 dir3);

        /// <summary>
        /// 功能：四轴常速连续运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        ///     ch3 ---- 轴号
        ///     dir3 ---- 方向（ 1：正向   -1：负向）
        ///     ch4 ---- 轴号
        ///     dir4 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="dir3">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch4">轴号</param>
        /// <param name="dir4">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_vmove4(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2,Int32 ch3,Int32 dir3,Int32 ch4,Int32 dir4);

        /// <summary>
        /// 功能：四轴快速连续运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        ///     ch3 ---- 轴号
        ///     dir3 ---- 方向（ 1：正向   -1：负向）
        ///     ch4 ---- 轴号
        ///     dir4 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="dir3">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch4">轴号</param>
        /// <param name="dir4">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_vmove4(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2,Int32 ch3,Int32 dir3,Int32 ch4,Int32 dir4);

        /// <summary>
        /// 功能：单轴常速回零运动指令
        /// 参数：
        ///     ch ---- 轴号
        ///     dir ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="dir">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_hmove(Int32 ch,Int32 dir);

        /// <summary>
        /// 功能：单轴快速回零运动指令
        /// 参数：
        ///     ch ---- 轴号
        ///     dir ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="dir">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_hmove(Int32 ch,Int32 dir);

        /// <summary>
        /// 功能：两轴常速回零运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_hmove2(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2);

        /// <summary>
        /// 功能：两轴快速回零运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_hmove2(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2);
 
        /// <summary>
        /// 功能：三轴常速回零运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        ///     ch3 ---- 轴号
        ///     dir3 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="dir3">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_hmove3(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2,Int32 ch3,Int32 dir3);
 
        /// <summary>
        /// 功能：三轴快速回零运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        ///     ch3 ---- 轴号
        ///     dir3 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="dir3">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_hmove3(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2,Int32 ch3,Int32 dir3);

        /// <summary>
        /// 功能：四轴常速回零运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        ///     ch3 ---- 轴号
        ///     dir3 ---- 方向（ 1：正向   -1：负向）
        ///     ch4 ---- 轴号
        ///     dir4 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="dir3">方向（ 1：正向   -1：负向）</param>
        /// <param name="ch4">轴号</param>
        /// <param name="dir4">方向（ 1：正向   -1：负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_hmove4(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2,Int32 ch3,Int32 dir3,Int32 ch4,Int32 dir4);

        /// <summary>
        /// 功能：四轴快速回零运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     dir1 ---- 方向（ 1：正向   -1：负向）
        ///     ch2 ---- 轴号
        ///     dir2 ---- 方向（ 1：正向   -1：负向）
        ///     ch3 ---- 轴号
        ///     dir3 ---- 方向（ 1：正向   -1：负向）
        ///     ch4 ---- 轴号
        ///     dir4 ---- 方向（ 1：正向   -1：负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="dir1">方向（ 1：正向   -1：负向） </param>
        /// <param name="ch2">轴号</param>
        /// <param name="dir2">方向（ 1：正向   -1：负向） </param>
        /// <param name="ch3">轴号</param>
        /// <param name="dir3">方向（ 1：正向   -1：负向） </param>
        /// <param name="ch4">轴号</param>
        /// <param name="dir4">方向（ 1：正向   -1：负向） </param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_hmove4(Int32 ch1,Int32 dir1,Int32 ch2,Int32 dir2,Int32 ch3,Int32 dir3,Int32 ch4,Int32 dir4);

        /// <summary>
        /// 功能：两轴常速直线插补运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1"> 相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2"> 相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_line2(Int32 ch1,double step1,Int32 ch2, double step2);

        /// <summary>
        /// 功能：三轴常速直线插补运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch3 ---- 轴号
        ///     step3 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="step3">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_line3(Int32 ch1, double step1, Int32 ch2, double step2, Int32 ch3, double step3);

        /// <summary>
        /// 功能：四轴常速直线插补运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch3 ---- 轴号
        ///     step3 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch4 ---- 轴号
        ///     step4 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="step3">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch4">轴号</param>
        /// <param name="step4">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 con_line4(Int32 ch1, double step1, Int32 ch2, double step2, Int32 ch3, double step3, Int32 ch4, double step4);

        /// <summary>
        /// 功能：两轴快速直线插补运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_line2(Int32 ch1, double step1, Int32 ch2, double step2);

        /// <summary>
        /// 功能：三轴快速直线插补运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch3 ---- 轴号
        ///     step3 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <param name="ch3">轴号</param>
        /// <param name="step3">相对当前位置的位移（正值为正向，负值为负向）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_line3(Int32 ch1, double step1, Int32 ch2, double step2, Int32 ch3, double step3);
  
        /// <summary>
        /// 功能：四轴快速直线插补运动指令
        /// 参数：
        ///     ch1 ---- 轴号
        ///     step1 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch2 ---- 轴号
        ///     step2 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch3 ---- 轴号
        ///     step3 ---- 相对当前位置的位移（正值为正向，负值为负向）
        ///     ch4 ---- 轴号
        ///     step4 ---- 相对当前位置的位移（正值为正向，负值为负向）
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="step1">相对当前位置的位移（正值为正向，负值为负向</param>
        /// <param name="ch2">轴号</param>
        /// <param name="step2">相对当前位置的位移（正值为正向，负值为负向</param>
        /// <param name="ch3">轴号</param>
        /// <param name="step3">相对当前位置的位移（正值为正向，负值为负向</param>
        /// <param name="ch4">轴号</param>
        /// <param name="step4">相对当前位置的位移（正值为正向，负值为负向</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 fast_line4(Int32 ch1, double step1, Int32 ch2, double step2, Int32 ch3, double step3, Int32 ch4, double step4);
  
        ////////////////////////////////////////////////////////////////////////////
        //制动函数
        /// <summary>
        /// 功能：单轴急停
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 sudden_stop(Int32 ch);

        /// <summary>
        /// 功能：两轴急停
        /// 参数：
        ///     ch1 ---- 轴号
        ///     ch2 ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="ch2">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 sudden_stop2(Int32 ch1,Int32 ch2);

        /// <summary>
        /// 功能：三轴急停
        /// 参数：
        ///     ch1 ---- 轴号
        ///     ch2 ---- 轴号
        ///     ch3 ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="ch2">轴号</param>
        /// <param name="ch3">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 sudden_stop3(Int32 ch1,Int32 ch2,Int32 ch3);

        /// <summary>
        /// 功能：四轴急停
        /// 参数：
        ///     ch1 ---- 轴号
        ///     ch2 ---- 轴号
        ///     ch3 ---- 轴号
        ///     ch4 ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="ch2">轴号</param>
        /// <param name="ch3">轴号</param>
        /// <param name="ch4">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 sudden_stop4(Int32 ch1,Int32 ch2,Int32 ch3,Int32 ch4);

        /// <summary>
        /// 功能：快速运动模式下单轴缓停
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 decel_stop(Int32 ch);

        /// <summary>
        /// 功能：快速运动模式下二轴缓停
        /// 参数：
        ///     ch1 ---- 轴号
        ///     ch2 ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="ch2">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 decel_stop2(Int32 ch1,Int32 ch2);

        /// <summary>
        /// 功能：快速运动模式下三轴缓停
        /// 参数：
        ///     ch1 ---- 轴号
        ///     ch2 ---- 轴号
        ///     ch3 ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="ch2">轴号</param>
        /// <param name="ch3">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 decel_stop3(Int32 ch1,Int32 ch2,Int32 ch3);

        /// <summary>
        /// 功能：快速运动模式下四轴缓停
        /// 参数：
        ///     ch1 ---- 轴号
        ///     ch2 ---- 轴号
        ///     ch3 ---- 轴号
        ///     ch4 ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch1">轴号</param>
        /// <param name="ch2">轴号</param>
        /// <param name="ch3">轴号</param>
        /// <param name="ch4">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 decel_stop4(Int32 ch1,Int32 ch2,Int32 ch3,Int32 ch4);

        /// <summary>
        /// 功能：运动暂停
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 move_pause(Int32 ch);

        /// <summary>
        /// 功能：暂停后恢复运动
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 move_resume(Int32 ch);

        ///////////////////////////////////////////////////////////////
        // I/O操作函数

        /// <summary>
        /// 功能：读取所有通用输入口状态
        /// 参数：
        ///     cardno ---- 卡号
        /// 返回值： 非负（通用输入口状态）-----  -1（错误） 
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <returns> 非负（通用输入口状态）-----  -1（错误） </returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 checkin_byte(Int32 cardno);

        /// <summary>
        /////////////////////////////////////////////
        /// 功能：读取某位通用输入口状态
        /// 参数：
        ///     cardno ---- 卡号
        ///     bitno ---- 通用输入口位标记
        /// 返回值： 1（ON）---- 0（OFF）-----  -1（错误）
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="bitno">通用输入口位标记</param>
        /// <returns> 1（高电平）---- 0（低电平）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 checkin_bit(Int32 cardno, Int32 bitno);

        /// <summary>
        /// 功能：设置所有通用输出口状态
        /// 参数：
        ///     cardno ---- 卡号
        ///     data ---- 通用输出口状态
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="data">通用输出口状态</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 outport_byte(Int32 cardno, Int32 data);

        /// <summary>
        /// 功能：设置某位通用输出口状态
        /// 参数：
        ///     cardno ---- 卡号
        ///     bitno ---- 通用输出口位标记
        ///     status ---- 输出电平：1（高电平）--- 0（低电平）
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="bitno">通用输出口位标记</param>
        /// <param name="status">输出电平：1（高电平）--- 0（低电平）</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 outport_bit(Int32 cardno, Int32 bitno, Int32 status);

        /// <summary>
        /// 功能：读取某位专用输入口状态（ORG、EL、轴ALM、Z信号和板卡ALARM信号）
        /// 参数：
        ///     cardno ---- 卡号
        ///     bitno ---- 专用输入口位标记
        /// 返回值： 1（高电平）---- 0（低电平）-----  -1（错误） 
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="bitno">专用输入口位标记</param>
        /// <returns> 1（高电平）---- 0（低电平）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 check_sfr_bit(Int32 cardno, Int32 bitno);

        /// <summary>
        /// 功能：读取专用输入口状态（ORG、EL、轴ALM、Z信号和板卡ALARM信号）
        /// 参数：
        ///     cardno ---- 卡号
        /// 返回值： 非负（专用输入口状态）-----  -1（错误）
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <returns> 非负（专用输入口状态）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 check_sfr(Int32 cardno);

        ///////////////////////////////////////////////////////////////
        // 特殊功能函数

        /// <summary>
        /// 功能：设置反向间隙补偿值
        /// 参数：
        ///     ch ---- 轴号
        ///     blash ---- 补偿值（正），默认 20 个脉冲
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="blash">补偿值（正），默认 20 个脉冲</param>
        /// <returns>0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_backlash(Int32 ch, double blash);

        /// <summary>
        /// 功能：开启反向间隙补偿功能
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（正确）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 start_backlash(Int32 ch);

        /// <summary>
        /// 功能：关闭反向间隙补偿功能
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（成功）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（成功）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 end_backlash(Int32 ch);
 
        /// <summary>
        /// 功能：动态改变目标位置
        /// 参数：
        ///     ch ---- 轴号
        ///     pos ---- 目标位（相对于上条运动指令起始位置）
        /// 返回值： 0（成功）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="pos">目标位（相对于上条运动指令起始位置）</param>
        /// <returns> 0（成功）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 change_pos(Int32 ch, double pos);

        ////////////////////////////////////////////////////////////////
        // 位置和状态查询函数

        /// <summary>
        /// 功能：获取总轴数
        /// 返回值： 轴数（成功）-----  0（错误） 
        /// </summary>
        /// <returns> 轴数（成功）-----  0（错误） </returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_max_axe();

        /// <summary>
        /// 功能：获取总卡数
        /// 返回值： 轴数（成功）-----  0（错误）
        /// </summary>
        /// <returns> 轴数（成功）-----  0（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_board_num();
 
        /// <summary>
        /// 功能：获取指定板卡上轴数
        /// 参数：
        ///     cardno ---- 板卡
        /// 返回值： 轴数（成功）-----  0（错误） 
        /// </summary>
        /// <param name="cardno">板卡</param>
        /// <returns> 轴数（成功）-----  0（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_axe(Int32 cardno);

        /// <summary>
        /// 功能：获取指定板卡本地ID号
        /// 参数：
        ///     cardno ---- 板卡
        /// 返回值： 1~总卡数（成功）-----  其他（错误） 
        /// </summary>
        /// <param name="cardno">板卡</param>
        /// <returns> 1~总卡数（成功）-----  其他（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 check_IC(Int32 cardno);

        /// <summary>
        /// 功能：设置轴绝对位置值
        /// 参数：
        ///     ch ---- 轴号
        ///     pos ---- 绝对位置
        /// 返回值： 0（成功）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="pos">绝对位置</param>
        /// <returns> 0（成功）-----  -1（错误） </returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_abs_pos(Int32 ch, ref double pos);

        /// <summary>
        /// 功能：获取轴当前方向
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（停止状态） -1（负向） 1（正向） -2（出错） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（停止状态） -1（负向） 1（正向） -2（出错） </returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_cur_dir(Int32 ch);

        /// <summary>
        /// 功能：获取轴常速点位运动速度
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 正值速度（成功）-----  -1（错误）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns>正值速度（成功）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double get_conspeed(Int32 ch);

        /// <summary>
        /// 功能：获取轴常速插补运动速度
        /// 返回值： 正值速度（成功）-----  -1（错误） 
        /// </summary>
        /// <returns> 正值速度（成功）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double get_vector_conspeed();

        /// <summary>
        /// 功能：获取轴当前快速运动速度
        /// 参数：
        ///     ch ---- 轴号
        ///     vl ---- 低速
        ///     vh ---- 高速
        ///     ad ---- 上升加速度
        ///     dc ---- 下降加速度
        /// 返回值： 0（成功）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <param name="vl">低速</param>
        /// <param name="vh">高速</param>
        /// <param name="ad">上升加速度</param>
        /// <param name="dc">下降加速度</param>
        /// <returns> 0（成功）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_profile(Int32 ch, ref double vl, ref double vh, ref double ad, ref double dc);

        /// <summary>
        /// 功能：获取快速插补运动速度
        /// 参数：
        ///     vec_vl ---- 低速
        ///     vec_vh ---- 高速
        ///     vec_ad ---- 上升加速度
        ///     vec_dc ---- 下降加速度
        /// 返回值： 0（成功）-----  -1（错误） 
        /// </summary>
        /// <param name="vec_vl">低速</param>
        /// <param name="vec_vh">高速</param>
        /// <param name="vec_ad">上升加速度</param>
        /// <param name="vec_dc">下降加速度</param>
        /// <returns> 0（成功）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_vector_profile(ref double vec_vl, ref double vec_vh, ref double vec_ad, ref double vec_dc);

        /// <summary>
        /// 功能：获取轴运动速度
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 非负（成功）-----  负值（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 非负（成功）-----  负值（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double get_rate(Int32 ch);

        /// <summary>
        /// 功能：获取轴当前状态（ORG、EL、ALM等有效状态）
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 正值（成功）-----  -1（错误） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns>正值（成功）-----  -1（错误） </returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 check_status(Int32 ch);

        /// <summary>
        /// 功能：获取轴当前运动状态
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（停止）-----  1（运动）---- -1（出错） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（停止）-----  1（运动）---- -1（出错）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 check_done(Int32 ch);

        /// <summary>
        /// 功能：获取轴当前限位信号状态
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（无效）-----  1（正限位有效）---- -1（负限位有效）---- （正负限位有效）---- -3（出错） 
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（无效）-----  1（正限位有效）---- -1（负限位有效）---- （正负限位有效）---- -3（出错）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 check_limit(Int32 ch);

        /// <summary>
        /// 功能：获取轴当前原点信号状态
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（无效）-----  1（有效）---- -3（出错）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（无效）-----  1（有效）---- -3（出错）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 check_home(Int32 ch);

        /// <summary>
        /// 功能：获取轴当前报警信号状态
        /// 参数：
        ///     ch ---- 轴号
        /// 返回值： 0（无效）-----  1（有效）---- -3（出错）
        /// </summary>
        /// <param name="ch">轴号</param>
        /// <returns> 0（无效）-----  1（有效）---- -3（出错）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 check_alarm(Int32 ch);

        ///////////////////////////////////////////////////////////////
        // SPI存储芯片操作函数

        /// <summary>
        /// 功能：写密码区
        /// 参数：
        ///     cardno ---- 卡号
        ///     no ---- 地址编号：0~16383
        ///     data ---- 写入的数据
        ///     password ---- 校验密码
        /// 返回值： 0（无效）-----  -1（写入参数错误）---- -2（校验密码出错） 
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="no">地址编号：0~16383</param>
        /// <param name="data">写入的数据</param>
        /// <param name="password">校验密码</param>
        /// <returns> 0（无效）-----  -1（写入参数错误）---- -2（校验密码出错）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 write_password_flash(Int32 cardno, Int32 no, Int32 data, Int32 password);

        /// <summary>
        /// 功能：读密码区
        /// 参数：
        ///     cardno ---- 卡号
        ///     no ---- 地址编号：0~16382
        ///     data ---- 指定地址内的数据
        ///     password ---- 校验密码（16383地址内的数据）
        /// 返回值： 0（无效）-----  -1（参数错误）---- -2（校验密码出错）
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="no">地址编号：0~16382</param>
        /// <param name="data">指定地址内的数据</param>
        /// <param name="password">校验密码（16383地址内的数据）</param>
        /// <returns> 0（无效）-----  -1（参数错误）---- -2（校验密码出错）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 read_password_flash(Int32 cardno, Int32 no, ref Int32 data, Int32 password);

        /// <summary>
        /// 功能：擦写密码区
        /// 参数：
        ///     cardno ---- 卡号
        ///     password ---- 校验密码
        /// 返回值： 0（无效）-----  -1（参数错误）---- -2（校验密码出错）
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="password">校验密码</param>
        /// <returns> 0（无效）-----  -1（参数错误）---- -2（校验密码出错）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 clear_password_flash(Int32 cardno, Int32 password);

        /// <summary>
        /// 功能：擦写密码区
        /// 参数：
        ///     cardno ---- 卡号
        ///     piece ---- 片区号
        ///     no ---- 地址： 0~16383
        ///     data --- 写入数据
        /// 返回值： 0（无效）-----  -1（参数错误） 
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="piece">片区号</param>
        /// <param name="no">地址： 0~16383</param>
        /// <param name="data">写入数据</param>
        /// <returns> 0（无效）-----  -1（参数错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 write_flash(Int32 cardno, Int32 piece, Int32 no, Int32 data);
 
        /// <summary>
        /// 功能：读写数据区
        /// 参数：
        ///     cardno ---- 卡号
        ///     piece ---- 片区号
        ///     no ---- 地址： 0~16383
        ///     data --- 存储读出的数据
        /// 返回值： 0（无效）-----  -1（参数错误） 
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="piece">片区号</param>
        /// <param name="no">地址： 0~16383</param>
        /// <param name="data">存储读出的数据</param>
        /// <returns> 0（无效）-----  -1（参数错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 read_flash(Int32 cardno, Int32 piece, Int32 no, ref Int32 data);

        /// <summary>
        /// 功能：擦写数据区，将指定片区数据各bit位置 1
        /// 参数：
        ///     cardno ---- 卡号
        ///     piece ---- 片区号：0~15
        /// 返回值： 0（无效）-----  -1（参数错误）
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="piece">片区号：0~15</param>
        /// <returns> 0（无效）-----  -1（参数错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 clear_flash(Int32 cardno, Int32 piece);

        ///////////////////////////////////////////////////////////////
        // 错误代码操作函数

        /// <summary>
        /// 功能：获取最近一次错误代码
        /// 返回值： 0（没有错误）-----  正数（正确） 
        /// </summary>
        /// <returns> 0（没有错误）-----  正数（正确） </returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_last_err();
 
        /// <summary>
        /// 功能：获取错误代码
        /// 参数：
        ///     index ---- 错误索引：1~10
        ///     data ---- 存储错误代码
        /// 返回值： 0（正确）-----  -1（index错误或系统没有错误） 
        /// </summary>
        /// <param name="index">错误索引：1~10</param>
        /// <param name="data">存储错误代码</param>
        /// <returns> 0（正确）-----  -1（index错误或系统没有错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_err(Int32 index, ref Int32 data);

        /// <summary>
        /// 功能：清楚最近10次错误代码
        /// 返回值： 0（正确）-----  -1（错误）
        /// </summary>
        /// <returns> 0（正确）-----  -1（错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 reset_err();

        ///////////////////////////////////////////////////////////////
        // 版本读取函数

        /// <summary>
        /// 功能：获取函数库版本
        /// 参数：
        ///     major ---- 主版本号
        ///     minor1 ---- 一级版本号
        ///     minor2 ---- 二级版本号
        /// 返回值： 0（无效）-----  -1（参数错误）
        /// </summary>
        /// <param name="major">主版本号</param>
        /// <param name="minor1">一级版本号</param>
        /// <param name="minor2">二级版本号</param>
        /// <returns> 0（无效）-----  -1（参数错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_lib_ver(ref Int32 major, ref Int32 minor1, ref Int32 minor2);

        /// <summary>
        /// 功能：获取驱动程序版本
        /// 参数：
        ///     major ---- 主版本号
        ///     minor1 ---- 一级版本号
        ///     minor2 ---- 二级版本号
        /// 返回值： 0（无效）-----  -1（参数错误） 
        /// </summary>
        /// <param name="major">主版本号</param>
        /// <param name="minor1">一级版本号</param>
        /// <param name="minor2">二级版本号</param>
        /// <returns> 0（无效）-----  -1（参数错误）</returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_sys_ver(ref Int32 major, ref Int32 minor1, ref Int32 minor2);

        /// <summary>
        /// 功能：获取板卡版本
        /// 参数：
        ///     cardno ---- 板卡
        ///     type ---- 板卡类型
        ///     major ---- 主版本号
        ///     minor1 ---- 一级版本号
        ///     minor2 ---- 二级版本号
        /// 返回值： 0（无效）-----  -1（参数错误）
        /// </summary>
        /// <param name="carno">板卡</param>
        /// <param name="type">板卡类型</param>
        /// <param name="major">主版本号</param>
        /// <param name="minor1">一级版本号</param>
        /// <param name="minor2">二级版本号</param>
        /// <returns></returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 get_card_ver(Int32 carno, ref Int32 type, ref Int32 major, ref Int32 minor1, ref Int32 minor2);

        /// <summary>
        /// 系统调试使用，暂不开放
        /// </summary>
        /// <param name="InfoNum"></param>
        /// <param name="rtn"></param>
        /// <returns></returns>
        [DllImport(@"MPC08.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 GetDllInfo(Int32 InfoNum, ref Int32 rtn);
        ////////////////////////////////////////////////////////////////////////
    }
}
