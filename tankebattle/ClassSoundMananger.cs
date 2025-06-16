using _06_坦克大战_正式.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式
{
    internal class ClassSoundMananger
    {
        private static SoundPlayer SPstart = new SoundPlayer();//基础音乐流类型
        //目前只能同时播放一个音效，老师的五个变量也没能解决。那我也先不改，等直到怎么解决了再修正
        //邪门了，写完上面那段话，游戏突然没有除了strat之外的音效了。按照老师的办法重写，大多数时候依旧没有，偶尔有很延迟的几声。真的邪门了，刚刚还完全正常呢！
        //行吧，不是代码的问题，是电脑的问题。运行老师的代码也一样高延迟。邪门了，我电脑坏了？？？
        //看来我电脑真有问题，绝了
        private static SoundPlayer SPadd = new SoundPlayer();
        private static SoundPlayer SPblast = new SoundPlayer();
        private static SoundPlayer SPhit = new SoundPlayer();
        private static SoundPlayer SPfire = new SoundPlayer();
        /*private static void initSound(int index)//通过音乐索引初始化音乐
        {
            switch (index)
            {
                case 0:
                    SPstart.Stream = Resources.start;//声音的源头文件//开始游戏
                    break;
                case 1:
                    SPstart.Stream = Resources.add;//生成敌人
                    break;
                case 2:
                    SPstart.Stream = Resources.blast;//爆炸
                    break;
                case 3:
                    SPstart.Stream = Resources.hit;//被命中
                    break;
                case 4:
                    SPstart.Stream = Resources.fire;//开火
                    break;
            }
        }*/
        public static void initSound()
        {
            SPstart.Stream = Resources.start;
            SPadd.Stream = Resources.add;
            SPblast.Stream = Resources.blast;
            SPhit.Stream = Resources.hit;
            SPfire.Stream = Resources.fire;

            //SPblast.SoundLocation = @"D:\biancheng\csharp\xuexibiji\第三季-坦克大战\06-坦克大战-正式\Resources\blast.wav";

            SPadd.Load();
            SPblast.Load();
            SPfire.Load();
            SPadd.Load();
            SPfire.Load();//又好了，而且这段加不加都一样，一会延迟高的吓死人一会好了，真奇了怪了！该怎么解决延迟呢
        }
        public static void MMusicStart()
        {
            //initSound(0);
            SPstart.Play();//播放声音
        }
        public static void MMusicAdd()
        {
            //initSound(1);
            SPadd.Play();
        }
        public static void MMusicBlast()
        {
            //initSound(2);
            SPblast.Play();
        }
        public static void MMusicHit()
        {
            //initSound(3);
            SPhit.Play();
        }
        public static void MMusicFire()
        {
            //initSound(4);
            SPfire.Play();
        }


        

    }
}
