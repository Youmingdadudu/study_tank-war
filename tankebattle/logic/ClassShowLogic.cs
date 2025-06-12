using _06_坦克大战_正式.activeobjectclass;
using _06_坦克大战_正式.baseclass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _06_坦克大战_正式.baseclass.ClassActiveObject;
//using _06_坦克大战_正式.baseclass;

namespace _06_坦克大战_正式.logic
{
    internal class ClassShowLogic : ClassBaseLogic//动态演出逻辑
    {
        #region 保存的逻辑类型：需要时刻检测的逻辑
        /*
         * 玩家的移动
         * 子弹的移动
         * 碰撞检测
        */
        #endregion
        #region 内部数据成员
        //public static List<int> listIsMove = new List<int>() { 1};//用于判定是否持续移动的集合
        //public static List<Keys> listIsMove = new List<Keys>();//用数字不行，copilot说键盘连发会添加一大堆1。要想只在按下时触发，需要改逻辑，用int会很麻烦，要先判定键入再添加1234，不如直接判断键入。

        #endregion

        #region 旧移动方法
        //enum EM_Direction//早知道和老师学也放在mytank类里了，真麻烦啊23333
        //{
        //    Up, Down, Left, Right
        //}
        /*public static void MMovePosition(object type,KeyEventArgs args)//siki老师为up和down各提供了一个方法，我想试试写成一个（虽然完全没必要只是好看，多了个判定还拖慢效率）2333
        {//判定移动方向
            //EM_Direction dirTemp;
            //int xRun = 0, yRun = 0;
            switch (args.KeyCode)
            {
                case Keys.W:
                    //ClassCreateLogic.myTanktemp.dir = EM_Direction.Up;//该枚举是_06_坦克大战_正式.baseclass命名空间下的不是类下的，所以不能访问
                    ClassCreateLogic.myTanktemp.dir = ClassMy.EM_Direction.Up;//忽然想起来枚举也能定义在方法里，那就不用引入了2333
                    //yRun = -2;
                    ClassCreateLogic.myTanktemp.Y -= ClassCreateLogic.myTanktemp.speed;
                    break;
                case Keys.S:
                    ClassCreateLogic.myTanktemp.dir = ClassMy.EM_Direction.Down;
                    ClassCreateLogic.myTanktemp.Y += ClassCreateLogic.myTanktemp.speed;
                    //yRun = 2;
                    break;
                case Keys.A:
                    ClassCreateLogic.myTanktemp.dir = ClassMy.EM_Direction.Left;
                    ClassCreateLogic.myTanktemp.X -= ClassCreateLogic.myTanktemp.speed;
                    //xRun = -2;
                    break;
                case Keys.D:
                    ClassCreateLogic.myTanktemp.dir = ClassMy.EM_Direction.Right;
                    ClassCreateLogic.myTanktemp.X += ClassCreateLogic.myTanktemp.speed;
                    //xRun = 2;
                    break;
            }

            //if(type == "Down") //判定按下按键时方法
            //{//我怎么没想到直接在上面控制位置呢！就不用在这儿加减了！虽然老师最终移动方法不是这么做的，但却让我受益良多！
            //    ClassCreateLogic.myTanktemp.X += xRun;
            //    ClassCreateLogic.myTanktemp.Y += yRun;
            //}
            //else//判定松开按键时方法
            //{//暂停方法不需要判定方向，直接当前方向停车就完了，所以判断方向就不单独做成方法了。移动同理，停车也不需要移动
            //    //这里什么都不用写，只需要在松开时打断移动就好了
            //}//试了试，实际上根本用不着松开按键时候执行啥，只要保证只有摁下按键时触发位置变化就好了233
            
            //只看到控制方向，后面老师怎么做的没看，以上时我自己写的方法。挠头，有延迟，而且摁住一个键再按另一个键转向时不顺畅会蜜汁回头，而且没用到速度属性233.看看siki老师怎么做的吧
        }


        //public static void MMoveDraw()//用不着，我直接在上面控制就好了
        //{//应该根据帧率自适应维持相同移动速度，不过目前换不了刷新率就不用管了
        //    //ClassCreateLogic.myTanktemp.X = 0;
        //    //ClassCreateLogic.myTanktemp.Y = 0;
        //}
    }*/
        #endregion
        #region 新移动方法：听到老师的点拨（加一个判断是否移动的属性，然后在update里调用控制），新的灵感，依旧没看老师的控制方法
        public static void MMovePosition(object type, KeyEventArgs args)//方向和移动判断，具体移动不在这了
        {//keydown/up如果在事件中触发调用，频率为固定的按键触发（低频），具体数值操作系统的“键盘重复延迟”和“重复速率”设置决定。
            //解决了一个了（将位置改变合并到MDrawMyTank方法在主循环里调用），之后就是转向失灵问题。我问copilot说是单一依赖问题，用列表实时记录目前按下的方向然后依据最后一个决定方向和状态即可。原来如此，不过我先看看老师怎么解决的再决定如何修正
            //灵光一闪发现鬼畜回头问题在于，我把换方向的switch语句放在方法里了，导致无论按下还是松开都会改变方向，放在down里就好了，松开就不会换方向了
            //不过按住一个方向时摁另一个方向，再松开前一个方向，还是会有延迟（调用了up导致的）。不知道怎么解决了
            //设计个列表，按下时加入临时数据，松开时删除。如果有临时数据，则无视松开事件，或许可以取消停顿
            //完全没用，问题在于我松开时必定触发KeyUp事件，而这个触发由系统控制，我不能修改。挠头，我投降了2333
            if (type == "Down")
            {
                switch (args.KeyCode)//我要做的就是把移动移到主循环，跟帧率绑定
                {
                    case Keys.W:
                        ClassCreateLogic.myTanktemp.dir = ClassMy.EM_Direction.Up;
                        break;
                    case Keys.S:
                        ClassCreateLogic.myTanktemp.dir = ClassMy.EM_Direction.Down;
                        break;
                    case Keys.A:
                        ClassCreateLogic.myTanktemp.dir = ClassMy.EM_Direction.Left;
                        break;
                    case Keys.D:
                        ClassCreateLogic.myTanktemp.dir = ClassMy.EM_Direction.Right;
                        break;
                }
                ClassCreateLogic.myTanktemp.isMoving = true;
            }
            else
            {
                    ClassCreateLogic.myTanktemp.isMoving = false;
            }
        }

        //移动方法
        public static void MMove(ClassActiveObject Dir)
        {//根据朝向移动方法，这样敌我单位都调用这个就可以了
            switch (Dir.dir)
            {
                case EM_Direction.Up:
                    Dir.Y -= Dir.speed;
                    break;
                case EM_Direction.Down:
                    Dir.Y += Dir.speed;
                    break;
                case EM_Direction.Left:
                    Dir.X -= Dir.speed;
                    break;
                case EM_Direction.Right:
                    Dir.X += Dir.speed;
                    break;
            }
        }
        #endregion

        #region 移动检测和碰撞检测
        public static void MMoveCheck(ClassActiveObject Dir)//子弹的碰撞不在这里
        {
            //检测是否超出窗体
            if((Dir.dir==EM_Direction.Right && Dir.X > (390-Dir.Width)) //不能直接390，得减去坦克的高宽，因为坦克的坐标也在图片左上角
                || (Dir.dir == EM_Direction.Left && Dir.X < 0 )//需要每个方向单独判定，否贼只判定坐标，那么一旦碰到边界，就会将ismoving一直等于flase
                || (Dir.dir == EM_Direction.Down && Dir.Y > 390- Dir.Height) 
                || (Dir.dir == EM_Direction.Up && Dir.Y < 0))
                Dir.isMoving = false;
            //检测是否碰撞墙体
        }


        #endregion

    }
}
