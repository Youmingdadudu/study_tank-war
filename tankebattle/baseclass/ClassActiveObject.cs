using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.baseclass
{
    //enum EM_Direction
    //{
    //    Up, Down, Left, Right
    //}
    internal class ClassActiveObject : ClassBase
    {
        //速度，朝向判定，四个方向位图
        public EM_Direction dir {get ;set; }

        public bool isMoving = false;
        public int speed { get; set; }

        public Bitmap bitmapUp { get; set; }
        public Bitmap bitmapDown { get; set; }
        public Bitmap bitmapLeft { get; set; }
        public Bitmap bitmapRight { get; set; }

        
              

        public enum EM_Direction
        {
            Up, Down, Left, Right
        }

        public ClassActiveObject(int x,int y, int spead,Bitmap bitUp,Bitmap bitDown, Bitmap bitLeft, Bitmap bitRight) : base(x,y)
        {//方向默认上，所以构造函数不用传递了
            this.X = x;
            this.Y = y;
            this.speed = spead;
            this.bitmapUp = bitUp;
            this.bitmapDown = bitDown;
            this.bitmapLeft = bitLeft;
            this.bitmapRight = bitRight;
            dir = EM_Direction.Up;
            this.Width = bitUp.Width;//不能直接在这里，因为有些坦克的高宽是不一样的，不能用一个图的高宽设定
            this.Height = bitUp.Height;//这里先有一个吧，初始化一下避免以后哪里因为没有高宽出了错
        }
            

        protected override Bitmap MGetBitmap()
        {//根据朝向返回位图，应为子弹等图片不是png有黑色背景，所以不能直接返回，需要先设置透明度
            Bitmap bitmap = null;
            switch (dir)
            {
                case EM_Direction.Up:
                    bitmap = bitmapUp;
                    //this.Width = bitmapUp.Width;//只返回一个就行了，不过先留着没准以后用上了
                    this.Height = bitmapUp.Height;
                    break;
                case EM_Direction.Down: 
                    bitmap = bitmapDown;
                    //this.Width = bitmapDown.Width;
                    this.Height = bitmapDown.Height;
                    break;
                case EM_Direction.Left:
                    bitmap = bitmapLeft;
                    this.Width = bitmapLeft.Width;
                    //this.Height = bitmapLeft.Height;
                    break;
                case EM_Direction.Right:
                    bitmap = bitmapRight;
                    this.Width = bitmapRight.Width;
                    //this.Height = bitmapRight.Height;
                    break;
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }


        
    }
}
