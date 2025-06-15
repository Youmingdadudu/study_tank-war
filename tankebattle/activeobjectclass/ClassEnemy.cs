using _06_坦克大战_正式.baseclass;
using _06_坦克大战_正式.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.activeobjectclass
{
    internal class ClassEnemy : ClassActiveObject
    {
        public int attackCounter = 0;//攻击计数器
        public ClassEnemy()
        {

        }
        public ClassEnemy(int x,int y, int spead,Bitmap bt1, Bitmap bit2, Bitmap bit3, Bitmap bit4) : base(x, y,spead, bt1, bit2, bit3, bit4)
        {
            this.X = x;
            this.Y = y;
            this.speed = spead;
            bitmapUp = bt1;
            bitmapDown = bit2;
            bitmapLeft = bit3;
            bitmapRight = bit4;
            this.dir = EM_Direction.Down;
            isMoving = true;//敌人得默认就行动，不然没法启动
            this.tag= EM_Tag.enemyTank;
        }
        //public ClassEnemy(Point a, int spead,Bitmap bt1, Bitmap bit2, Bitmap bit3, Bitmap bit4) : base(a.X, a.Y,spead, bt1, bit2, bit3, bit4)
        //{
        //    this.X = a.X;
        //    this.Y = a.Y;
        //    this.speed = spead;
        //    bitmapUp = bt1;
        //    bitmapDown = bit2;
        //    bitmapLeft = bit3;
        //    bitmapRight = bit4;
        //}
    }
}
