using _06_坦克大战_正式.baseclass;
using _06_坦克大战_正式.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _06_坦克大战_正式.staticbojectclass
{
    internal class ClassAct : ClassStaticObject
    {
        //用来存放如爆炸这样的不动的特效
        public int playspeed = 2;//动画播放速度，2帧播放一张
        public int playcount = 0;//计数器，不是用if区间（0-2，3-5）判断，而是除法索引，秒啊又学到了！
        //（具体操作：count/speed，得出的数就是应该输出的数组的索引。例如0/2=0，1/2=0，2/2=1.每个索引输出2次，这样就可以便捷的控制输出哪张图片了）
        public int playtime = 0;//在构造方法里初始化，数值是speed*图片个数。这样就能控制何时结束了（count小于time时候输出，到了就不输出清零方便下次调用了）
        public int arrayIndex = 0;//数组的索引,需要实时更新，得在draw里更新//在构造方法里初始化，不在drawself方法里每次构造感觉那样太麻烦cpu了233

        public Bitmap[] arrayBP { set; get; }/*= new Bitmap[]{}*/

        public ClassAct(int x, int y, int actIndex,int playspeed) : base()//不太懂base有无的具体差异
        {//虽然目前只有一个爆炸特效，不过也要预留出冗余。通过特效索引，控制生成哪种特效//通过biemapnum属性，控制不同特效
            switch (actIndex)//0号特效就是爆炸特效//int bitmapNum = 0;//用不着了，给每个特效单独做一个方法就行了
            {
                case 0:
                    //bitmapNum = 5;
                    //arrayBP = new Bitmap[5];//这也是一个法子，不过还是用下面的吧，简洁好看
                    //MBoomBitmap();
                    MGetBoomBitmap(/*arrayBP*/);
                    break;
            }//arrayBP = new Bitmap[bitmapNum];//根据所需的特效类型指定数组长度（即特效图片数量）

            this.X = x - arrayBP[0].Width / 2;//特效图片是有大小的，默认发生特效的点是图片中心，所以要修正xy将xy移动到左上角
            this.Y = y - arrayBP[0].Height / 2;
            this.playspeed = playspeed;//播放帧数可以指定
            playtime = arrayBP.Length * playspeed;

            foreach (Bitmap b in arrayBP)//将每张图片的黑色背景色弄透明
            {
                b.MakeTransparent(Color.Black);
            }
        }

        //public  override void MDrawSelf()//挠头，用new吧，之前都没用虚方法，现在改太麻烦了233
        protected override Bitmap MGetBitmap()//不用new了，老师说得对，直接根据需求返还图片就好了，没必要改绘画方法
        {//老师的代码和我的不同，他最后又没改这个，而是放在了update里修改参数，这里只保留return。我和他的代码逻辑不一样，我得注意点别照搬要用自己的办法
            //if (playcount == playtime)
            //{
            //    playcount = 0;
            //    return null;//如果达到帧率播放上限就退出，之前陷入误区了以为是唯一一个static全局爆炸效果，不过跟老师学，每次需要的时候new一个新的，然后再销毁就好了
            //}
            arrayIndex = playcount / playspeed;
            playcount++;//计数器得实时自增
            return arrayBP[arrayIndex];
        }

        private void MGetBoomBitmap(/*Bitmap[] array*/)//不麻烦，我搞错了，get方法不该是静态的233//整成静态的话，给数组赋值会很麻烦，还是算了吧2333
        {
            this.arrayBP = new Bitmap[5];
            this.arrayBP[0] = Resources.EXP1;
            this.arrayBP[1] = Resources.EXP2;
            this.arrayBP[2] = Resources.EXP3;
            this.arrayBP[3] = Resources.EXP4;
            this.arrayBP[4] = Resources.EXP5;
        }
    }
}
