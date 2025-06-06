﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _06_坦克大战_正式
{
    public partial class Form1 : Form
    {
        private Thread thread1;//将线程设置为类的成员变量，不然在构造函数中的临时变量无法被其他方法调用
        private static Graphics graphicsMain;
        private Graphics graphicsTemp;
        private static Bitmap bitmaptemp = new Bitmap(450, 450);
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;

            graphicsMain = this.CreateGraphics();
            graphicsTemp = Graphics.FromImage(bitmaptemp);

            ClassGameFrameWork.frameGraphics = graphicsTemp;//通过全局静态变量访问同一个画布，不然会很麻烦

            thread1 = new Thread(MGameMainThread);//游戏逻辑要放在单独的线程，否则会阻塞之后的代码
            thread1.Start();
            //this.ClientSize = new Size(450, 450);
        }
        private static void MGameMainThread()
        {
            /*游戏的主逻辑方法，持续运行游戏
             * 使用自定义的游戏框架类，类在独立文件中定义，不要把所有代码都放在一个文件里，最好一个类一个文件夹，这是好习惯
             * 首先要调用一次开始方法
             * 其次要持续调用更新方法，这是游戏运行的主方法
             *  更新方法也是游戏的刷新率，前期根据老师的设计固定60FPS即可，后期或可以选择刷新率
             *  控制刷新率的方法：每次更新一次数据，让线程等待1/60秒
             * 最后要调用结束方法
            */
            ClassGameFrameWork.MStart();

            int threadSleepTime = 1000 / 60;//sleep方法只支持int类型,所以不能用浮点类型
            while (true)
            {
                ClassGameFrameWork.frameGraphics.Clear(Color.Black);//每一帧都清除然后刷新成黑色
                ClassGameFrameWork.MUpdate();
                graphicsMain.DrawImage(bitmaptemp, 0, 0);

                Thread.Sleep(threadSleepTime);
            }

            ClassGameFrameWork.MEnd();
        }
        //private void Form1_Paint(object sender, PaintEventArgs e)
        //{
            //目前用不上
        //}

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread1.Abort();
        }
    }
}
