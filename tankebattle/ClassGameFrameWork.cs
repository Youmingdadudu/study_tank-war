using _06_坦克大战_正式.logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式
{
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

        public static void MStart()
        {//frameGraphics.Clear(Color.Black);不行，只渲染一次，这个效果不是持久的将背景设置呈黑色，仅限于当前，重新渲染就没了
            ClassCreateLogic.MCreateMap();//开始方法传递一次地图绘制数据
        }
        public static void MUpdate()
        {
            ClassCreateLogic.MDrwaMap();//根据地图绘制数据，每帧生成地图
        }
        public static void MEnd()
        {

        }
    }
}
