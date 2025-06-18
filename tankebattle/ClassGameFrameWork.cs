using _06_坦克大战_正式.logic;
using _06_坦克大战_正式.staticbojectclass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _06_坦克大战_正式
{
    //灵感
    //难度不同：敌人的生成速度，双方生命值，速度啥的//弹药数量，发射速度，子弹伤害和飞行速度//记录死亡次数，分数，运行时间，消灭敌人等等
    //如果我把碰撞检测时用到的临时xy，和临时矩形，都直接放在对应类里，在生成时直接计算（子弹类直接不用改了，坦克只要每次触发转弯事件时候变一下即可），这样就不用每次遍历都重新计算了，会不会减少计算量呢？
    
    enum EM_GameState
    {
        start,running,gameOver
    }
    public enum EM_EventMusic
    {
        Start, Add, Blast, Hit, Fire , gameOver // 为多线程添加一个游戏结束事件
    }
    internal class ClassGameFrameWork
    {
        #region 游戏框架类设计
        /*
         *开始方法，代表游戏开始
         *最主要的更新方法，代表游戏内持续运行的数据
         *结束方法，代表游戏结束
         */
        #endregion

        public static Graphics frameGraphics;//引用绘制元素的画布的全局静态变量
        private static object _lock = new object();
        public static EM_GameState gameState;

        //private static bool isMusicPlaying  = false; // 音乐播放状态，控制音乐线程

        //Thread ThreadMusic;

        public static event Action<EM_EventMusic> EventMusic;

        public ClassGameFrameWork()//终于成了！我把音乐事件放在了构造函数里，却忘了在窗口创建框架类实例，折腾了那么半天太蠢了2333
        {
            //ThreadMusic = new Thread(() => { MPlayMusic }//这啥啊自动出现的，我还没学这个语句呢
            //isMusicPlaying = true; // 初始化音乐播放状态为true
            EventMusic += MHandleMusicEvent;

            //ThreadMusic = new Thread(MPlayMusic)
            //{
            //    Name = "MusicThread", // 设置线程名称，便于调试和识别
            //};
            //ThreadMusic.IsBackground = true;// 设置为后台线程，确保应用程序可以在没有音乐线程的情况下正常退出
            //ThreadMusic.Start();
        }


        public static void MStart()
        {//frameGraphics.Clear(Color.Black);不行，只渲染一次，这个效果不是持久的将背景设置呈黑色，仅限于当前，重新渲染就没了
            gameState = EM_GameState.running;
            ClassSoundMananger.initSound();//初始化音乐
            //ClassSoundMananger.MMusicStart();
            ClassCreateLogic.MCreateBoss();//开始时传递一次boss位置数据
            ClassCreateLogic.MCreateMap();//开始方法传递一次地图绘制数据
            ClassCreateLogic.MCreateMyTank();

            ClassCreateLogic.MCreatStart();//创建敌人和道具等元素的信息

            EventMusic?.Invoke(EM_EventMusic.Start);//播放开始音乐


        }
        //if(gameState == )//这儿不能用if，挠头，那我在from里改吧，正好也方便
        public static void MUpdate()
        {
            //ClassProp.MDrawSelf();//需要创建一个道具生成方法，随机生成道具再里面调用MD方法，现在我只需要把BOSS（老家）整出来就行
            //ClassCreateLogic.MDrawBoss();
            //ClassCreateLogic.MDrawMap();//根据地图绘制数据，每帧生成地图
            //ClassCreateLogic.MDrawMyTank();

            ClassCreateLogic.MDrawStaticObject();
            ClassCreateLogic.MDrawActiveObject();

            //以下为动态方法
            //ClassShowLogic.MMoveDraw();
            lock (_lock)
            {
                ClassShowLogic.MBulletControl();//将子弹的控制从动态绘制里移出来不集中了。因为这不仅是绘制还有移动和碰撞啥的，是动态的就放在这了
            }
            ClassShowLogic.MDestroy();//销毁方法，因为改变了列表所以要放在所有用到列表循环的方法的最下面
        }
        public static void MEnd()
        {
            //isMusicPlaying = false; // 停止音乐播放
            Thread.Sleep(200); // 给予音乐线程一些时间完成当前操作
            // 清理音效相关资源
            EventMusic = null;
            ClassSoundMananger.MClean();

            ClassCreateLogic.MGameOver();//基地完蛋了就调用gameover方法
            gameState = EM_GameState.gameOver;
        }

        //public void MDispose()//音乐线程的清理方法
        //{
        //    isMusicPlaying = false;
        //    if (ThreadMusic != null && ThreadMusic.IsAlive)
        //    {
        //        ThreadMusic.Join(1000); // 等待音乐线程最多1秒钟
        //    }
        //    EventMusic = null;
        //    ClassSoundMananger.MClean();
        //}
        public void MDispose()//音乐资源的清理方法
        {
            EventMusic = null;
            ClassSoundMananger.MClean();
        }

        //public static void MPlayMusic()//没用了，单纯事件驱动，不需要线程了
        //{
        //    while (isMusicPlaying)//让线程持续运行，监听音乐事件，直到isMusicPlaying变为false
        //    {
        //        Thread.Sleep(100); // 避免CPU过度消耗
        //    }
        //}

        public static void MRaiseEventMusic(EM_EventMusic eventType)//触发音乐事件方法
        {
            EventMusic?.Invoke(eventType);
        }

        private static void MHandleMusicEvent(EM_EventMusic eventType)//音乐事件处理器
        {
            //if (!isMusicPlaying) return; // 检查是否应该继续处理音效
            switch (eventType)
            {
                case EM_EventMusic.Start:
                    Task.Run(() => ClassSoundMananger.MMusicStart());
                    break;
                case EM_EventMusic.Add:
                    Task.Run(() => ClassSoundMananger.MMusicAdd());
                    break;
                case EM_EventMusic.Blast:
                    Task.Run(() => ClassSoundMananger.MMusicBlast());
                    break;
                case EM_EventMusic.Hit:
                    Task.Run(() => ClassSoundMananger.MMusicHit());
                    break;
                case EM_EventMusic.Fire:
                    Task.Run(() => ClassSoundMananger.MMusicFire());
                    break;
            }
        }
    }
}
