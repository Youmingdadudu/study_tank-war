using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.baseclass
{
    internal class ClassStaticObject : ClassBase
    {
        public Bitmap bitmap {  get; set; }

        protected override Bitmap MGetBitmap()
        {
            return bitmap;
        }

        public ClassStaticObject(int x, int y ,Bitmap bt) : base(x,y) //基础构造函数,包括静态游戏元素的图片，位置
        {
            this.X = x;
            this.Y = y;
            this.bitmap = bt;
            this.Width = bt.Width;//不能直接在这里，因为有些坦克的高宽是不一样的，不能用一个图的高宽设定
            this.Height = bt.Height;//这里先有一个吧，初始化一下避免以后哪里因为没有高宽出了错
        }
    }
}
