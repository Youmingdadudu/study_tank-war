using _06_坦克大战_正式.baseclass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.staticbojectclass
{
    internal class ClassProp : ClassStaticObject
    {//道具类
        //private string propName;//道具名字
        //private string propHp;//道具生命值//这些属性暂时没意义233
        public ClassProp(int x, int y, Bitmap bt) : base(x, y, bt)
        {
            this.isHave = true;
        }
    }
}
