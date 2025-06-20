﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.baseclass
{
    internal abstract class ClassBase
    {//x和y坐标轴,获得图像方法（不同类），绘图方法
        public int  X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }//高和宽
        public int Height { get; set; }
        public Rectangle rectangle//自动返回当前矩形的属性
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }

        public bool isHave {  get; set; }//判断物体是否还存在的属性，用于判断销毁。还是老师的办法简单方便233
        protected abstract Bitmap MGetBitmap();
        protected ClassBase()
        {

        }
        protected ClassBase(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void MDrawSelf()
        {
            Graphics myGra = ClassGameFrameWork.frameGraphics;//在游戏画面上绘画自身
            myGra.DrawImage(MGetBitmap(), X, Y);
        }


    }
}
