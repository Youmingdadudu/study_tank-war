using _06_坦克大战_正式.baseclass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.activeobjectclass
{
    internal class ClassShow : ClassActiveClass
    {
        public ClassShow(int x, int y, Bitmap bt1, Bitmap bit2, Bitmap bit3, Bitmap bit4, int spead) : base(x, y, bt1, bit2, bit3, bit4, spead)
        {
        }
    }
}
