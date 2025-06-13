using _06_坦克大战_正式.activeobjectclass;
using _06_坦克大战_正式.baseclass;
using _06_坦克大战_正式.staticbojectclass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
                        if(myTanktemp.dir != EM_Direction.Up)
                            myTanktemp.dir = EM_Direction.Up;
                        break;
                    case Keys.S:
                        if (myTanktemp.dir != EM_Direction.Down)
                            myTanktemp.dir = EM_Direction.Down;
                        break;
                    case Keys.A:
                        if (myTanktemp.dir != EM_Direction.Left)
                            myTanktemp.dir = EM_Direction.Left;
                        break;
                    case Keys.D:
                        if (myTanktemp.dir != EM_Direction.Right)
                            /*ClassCreateLogic.*/
                            myTanktemp.dir = /*ClassMy.*/EM_Direction.Right;
                        break;
                }
                myTanktemp.isMoving = true;
            }
            else
            {
                myTanktemp.isMoving = false;
            }
        }

        //移动方法
        public static void MMove(ClassActiveObject AObject)
        {//根据朝向移动方法，这样敌我单位都调用这个就可以了
            switch (AObject.dir)
            {
                case EM_Direction.Up:
                    AObject.Y -= AObject.speed;
                    break;
                case EM_Direction.Down:
                    AObject.Y += AObject.speed;
                    break;
                case EM_Direction.Left:
                    AObject.X -= AObject.speed;
                    break;
                case EM_Direction.Right:
                    AObject.X += AObject.speed;
                    break;
            }
        }
        #endregion

        #region 移动检测和碰撞检测

        //检测物体碰撞方法
        private static ClassWall MIsCollidedAOlist(Rectangle rta, List<ClassWall> listao)//墙列表碰撞检测，哎列表不支持父类范型改用子类泛型//静态列表对象检测逻辑
        {
            foreach (ClassWall o in listao)
            {
                if(o.rectangle.IntersectsWith(rta))
                    return o;
            }
            return null;
        }
        private static ClassProp MIsCollidedAO(Rectangle rta, ClassProp ao)//道具碰撞检测//静态单个对象检测逻辑（目前就一个boss，没有其他道具）
        {
            if (ao.rectangle.IntersectsWith(rta))//单个判断，以后可以改为道具列表循环判断，boss恒定0号元素即可
            {
                return ao;
            }
                return null;
        }

        public static void MMoveCheck(ClassActiveObject AObject)//移动碰撞检测，子弹的碰撞不在这里
        {
            //检测是否超出窗体
            if((AObject.dir==EM_Direction.Right && AObject.X > (390- AObject.Width)) //不能直接390，得减去坦克的高宽，因为坦克的坐标也在图片左上角
                || (AObject.dir == EM_Direction.Left && AObject.X < 0 )//需要每个方向单独判定，否贼只判定坐标，那么一旦碰到边界，就会将ismoving一直等于flase
                || (AObject.dir == EM_Direction.Down && AObject.Y > 390- AObject.Height) 
                || (AObject.dir == EM_Direction.Up && AObject.Y < 0))
            {
                AObject.isMoving = false;
                return;//如果碰到窗体就不需要再检测是否碰到墙了
            }

            //检测是否碰撞墙体
            //Rectangle rtaTemp = AObject.rectangle;//临时矩形，这属性我设置为只读了懒得改了233（因为如果用自身矩形判断，会导致撞上了就没法动的问题（老师说的，我觉得不会））
            Rectangle rtaTemp = new Rectangle();
            rtaTemp = AObject.rectangle;
            switch (AObject.dir)
            {
                case EM_Direction.Up:
                    rtaTemp.Y -= AObject.speed;//将临时矩形的坐标设定为当前方向下一帧速度后的图片
                    break;
                case EM_Direction.Down:
                    rtaTemp.Y += AObject.speed;
                    break;
                case EM_Direction.Left:
                    rtaTemp.X -= AObject.speed;
                    break;
                case EM_Direction.Right:
                    rtaTemp.X += AObject.speed;//不小心写成-=了难怪不动呢。如果这么写，会导致向右走矩形碰到一起，因为矩形已经嵌在一起了，等于向右嵌进去1像素，其他方向-1像素也相交了，这样无论哪个方向都不行了
                    break;
            }
            if (MIsCollidedAOlist(rtaTemp, listwalls) != null)
            {
                AObject.isMoving = false;
                return;//碰到一个墙后续就不需要检测了
            }
            if (MIsCollidedAOlist(rtaTemp, liststeels) != null)
            {
                AObject.isMoving = false;
                return;
            }
            if (MIsCollidedAO(rtaTemp, BOSS) != null)
            {
                AObject.isMoving = false;
                return;
            }
        }
        #endregion

        #region 敌人的ai
        //先实现移动
        public static EM_Direction MenemyMoveCheck(EM_Direction dirTemp)//根据撞墙时的当前方向，转向另外三个方向，免得无效转向
        {
            List<EM_Direction> listdir = new List<EM_Direction> (4) { EM_Direction.Up, EM_Direction.Down, EM_Direction.Left, EM_Direction.Right };
            listdir.Remove(dirTemp);
            //Random ra = new Random();
            int index = ra.Next(0, 3);
            EM_Direction newdir = listdir[index];
            return newdir;
        }

        public static void MEnemyAI()//遍历敌人坦克列表，改变每个敌人的位置和移动状态
        {
            foreach(ClassEnemy ey in listenemyTank)//移动遍历
            {
                MMoveCheck(ey);//先调用检测，如果撞墙了就将ismoving改为flase
                if(ey.isMoving == false)//如果撞墙了，就换个方向
                {
                    //EM_Direction nowDir = ey.dir;//当前方向
                    ey.dir = MenemyMoveCheck(ey.dir);
                    ey.isMoving = true;
                    #region //旧随机方向
                    //Random ra = new Random();
                    //int index = ra.Next(1, 5);
                    //switch (index)
                    //{
                    //    case 1:
                    //        ey.dir = EM_Direction.Up;
                    //        break;
                    //    case 2:
                    //        ey.dir = EM_Direction.Down;
                    //        break;
                    //    case 3:
                    //        ey.dir = EM_Direction.Left;
                    //        break;
                    //    case 4:
                    //        ey.dir = EM_Direction.Right;
                    //        break;
                    //}
                    //ey.isMoving = true;
                    #endregion
                }
                //if (ey.isMoving == true)
                    MMove(ey);
            }
            //Thread thread2 = new Thread(MEnemyAI);//想让转方向少判定点减少鬼畜大旋转，不过线程这不明白，先别乱搞了
            //thread2.Start();
            //Thread.Sleep(100);
        }
        #endregion
    }
}
