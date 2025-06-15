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
    internal class ClassMy : ClassActiveObject
    {
        public ClassMy(int x, int y, int spead) : base(x, y, spead, Resources.MyTankUp, Resources.MyTankDown, Resources.MyTankLeft, Resources.MyTankRight)
        {
            this.X = x;
            this.Y = y;
            this.speed = spead;
            bitmapUp = Resources.MyTankUp;
            bitmapDown = Resources.MyTankDown;
            bitmapLeft = Resources.MyTankLeft;
            bitmapRight = Resources.MyTankRight;
            this.tag = EM_Tag.myTank;
            this.isHave = true;
        }
    }
}
