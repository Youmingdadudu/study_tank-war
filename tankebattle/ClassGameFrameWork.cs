using _06_坦克大战_正式.logic;
using _06_坦克大战_正式.staticbojectclass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式
{
    //灵感
    //难度不同：敌人的生成速度，双方生命值，速度啥的//弹药数量，发射速度，子弹伤害和飞行速度
    //如果我把碰撞检测时用到的临时xy，和临时矩形，都直接放在对应类里，在生成时直接计算（子弹类直接不用改了，坦克只要每次触发转弯事件时候变一下即可），这样就不用每次遍历都重新计算了，会不会减少计算量呢？
    
    enum EM_GameState
    {
        start,running,gameOver
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

        public static void MStart()
        {//frameGraphics.Clear(Color.Black);不行，只渲染一次，这个效果不是持久的将背景设置呈黑色，仅限于当前，重新渲染就没了
            gameState = EM_GameState.running;
            ClassCreateLogic.MCreateBoss();//开始时传递一次boss位置数据
            ClassCreateLogic.MCreateMap();//开始方法传递一次地图绘制数据
            ClassCreateLogic.MCreateMyTank();

            ClassCreateLogic.MCreatStart();//创建敌人和道具等元素的信息
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

        }
    }
}
