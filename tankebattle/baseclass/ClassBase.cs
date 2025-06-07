using System;
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
        protected abstract Bitmap MGetBitmap();
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
