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
        //老师在这里给set写了方法，作用是会根据设置的方向改变成对应图片和宽高。我忘了我为啥没用了，不过反正我的代码用不着费这二遍事情，也暂时没遇到老师遇到的资源冲突的问题
        //来来回回看了好几遍老师代码，终于明白了，老师讲的也不对他自个也不知道真的原因就开始一本正经的胡说八道2333
        //回顾了一下，因为坦克有四种方向的图片，绘制时需要根据当前方向决定对应图片
        //老师的代码在改变坦克的方向dir属性的时候，在keydown/up事件的单独线程里，会通过属性的set方法修改坦克当前的图片（跟老师说的宽高没关系）。
        //而与此同时，主线程中的drawself方法会调用getimage方法，getimage里也有根据当前方向重新设定坦克当前图片的语句
        //等于改一个方向，可能同时设置了两次一样的图片，所以可能会冲突
        //我只在drawself中根据方向重绘图片，坦克元素不会改变图片
        //（因为没意义，坦克没有当前图片的数据成员，根本就没有bitmap这个所谓的坦克图片元素，而是根据方向，用自定义的get方法从四个方向图片中选，当前需要绘制的图片）
        //（那么搞纯属多余了，无效操作233）
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
        public ClassActiveObject() : base()
        {

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
                    //this.Height = bitmapUp.Height;//真用上了，正确返回宽高，免得有的不同方位长宽不同的敌人在2/3面墙的地方鬼畜旋转穿模233
                    break;//好吧，未能完全解决，当前转头频率太高了。不过也有用，起码概率大大下降不会每次都鬼畜旋转了
                case EM_Direction.Down: 
                    bitmap = bitmapDown;
                    //this.Width = bitmapDown.Width;
                    //this.Height = bitmapDown.Height;
                    break;
                case EM_Direction.Left:
                    bitmap = bitmapLeft;
                    //this.Width = bitmapLeft.Width;
                    //this.Height = bitmapLeft.Height;
                    break;
                case EM_Direction.Right:
                    bitmap = bitmapRight;
                    //this.Width = bitmapRight.Width;
                    //this.Height = bitmapRight.Height;
                    break;
            }
            this.Width = bitmap.Width;
            this.Height = bitmap.Height;
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }


        
    }
}
