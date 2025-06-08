using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.baseclass
{
    enum EM_Direction
    {
        Up,Down,Left,Right
    }
    internal class ClassActiveClass : ClassBase
    {
        //速度，朝向判定，四个方向位图
        public int speed {  get; set; }
        public EM_Direction dir { get; set; }
        public Bitmap bitmapUp { get; set; }
        public Bitmap bitmapDown { get; set; }
        public Bitmap bitmapLeft { get; set; }
        public Bitmap bitmapRight { get; set; }

        public ClassActiveClass(int x,int y, int spead,Bitmap bitUp,Bitmap bitDown, Bitmap bitLeft, Bitmap bitRight) : base(x,y)
        {//方向默认上，所以构造函数不用传递了
            this.X = x;
            this.Y = y;
            this.speed = spead;
            this.bitmapUp = bitUp;
            this.bitmapDown = bitDown;
            this.bitmapLeft = bitLeft;
            this.bitmapRight = bitRight;
        }


        protected override Bitmap MGetBitmap()
        {//根据朝向返回位图，应为子弹等图片不是png有黑色背景，所以不能直接返回，需要先设置透明度
            Bitmap bitmap = null;
            switch (dir)
            {
                case EM_Direction.Up:
                    bitmap = bitmapUp;
                    break;
                case EM_Direction.Down: 
                    bitmap = bitmapDown;
                    break;
                case EM_Direction.Left:
                    bitmap = bitmapLeft;
                    break;
                case EM_Direction.Right:
                    bitmap = bitmapRight;
                    break;
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }
        
    }
}
