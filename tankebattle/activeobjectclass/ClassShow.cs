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
    internal class ClassShow : ClassActiveObject
    {
        public ClassShow(int x, int y, int spead, EM_Direction dir, EM_Tag tag) : base(x, y, spead, Resources.BulletUp, Resources.BulletDown, Resources.BulletLeft, Resources.BulletRight)
        {//子弹构造方法
            this.X = x;
            this.Y = y;
            this.speed = spead;
            this.dir = dir;
            this.tag = tag;
            isMoving = true;//子弹默认是移动的

            X -= Width / 2;//确实这么直接减，让子弹自己在左上角更方便，不然后续碰撞检测啥的太麻烦
            Y -= Height / 2;
        }
    }
}
