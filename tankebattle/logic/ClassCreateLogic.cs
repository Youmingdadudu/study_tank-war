using _06_坦克大战_正式.activeobjectclass;
using _06_坦克大战_正式.baseclass;
using _06_坦克大战_正式.Properties;
using _06_坦克大战_正式.staticbojectclass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _06_坦克大战_正式.baseclass.ClassActiveObject;

namespace _06_坦克大战_正式.logic
{
    internal class ClassCreateLogic : ClassBaseLogic//静态生成逻辑
    {
        //包括地图和角色的生成，道具的生成，各种绘图逻辑等等
        #region create方法
        internal static void MCreateMyTank()
        {//固定生成在老巢的左边
            int xPosition = (Form1.Bttp.Width - 30) / 2 - 45;
            int yPosition = Form1.Bttp.Height - 30;
            myTank = new ClassMy(xPosition, yPosition, 2);
            myTanktemp = myTank;
        }
        public static void MCreateBoss()//现在只有一关，若是有多关每关都需要生成boss,没准一关地图比一关大呢233
        {//将boss和周边的墙一起生成，固定在地图下半的底部中间
            double xPasition = (Form1.Bttp.Width - 30) / 2;
            double yPasition = (Form1.Bttp.Height - 30);
            double x1 = (xPasition - 15) / 30, x2 = (xPasition + 30) / 30;
            double y1 = yPasition / 30, y2 = (yPasition -15) / 30;
            BOSS = new ClassProp((int)xPasition, (int)yPasition , Resources.Boss);
            //生成基地防御砖墙
            MCreateWall(x1, y1, 0.5, 1, Resources.wall, listwalls);
            MCreateWall(x1, y2, 2, 0.5, Resources.wall, listwalls);
            MCreateWall(x2, y1, 0.5, 1, Resources.wall, listwalls);
        }
        
        public static void MCreateMap()
        {
            #region 生成地图
            //生成上半部分主砖墙
            MCreateWall(1, 1,1, 4, Resources.wall, listwalls);
            MCreateWall(3, 1, 1, 4, Resources.wall, listwalls);
            MCreateWall(5, 1, 1, 3, Resources.wall, listwalls);
            MCreateWall(7, 1, 1, 3, Resources.wall, listwalls);
            MCreateWall(9, 1, 1, 4, Resources.wall, listwalls);
            MCreateWall(11, 1, 1, 4, Resources.wall, listwalls);
            //生成下半部分主砖墙
            MCreateWall(1, 8, 1, 4, Resources.wall, listwalls);
            MCreateWall(3, 8, 1, 4, Resources.wall, listwalls);
            MCreateWall(5, 7, 1, 3, Resources.wall, listwalls);
            MCreateWall(7, 7, 1, 3, Resources.wall, listwalls);
            MCreateWall(9, 8, 1, 4, Resources.wall, listwalls);
            MCreateWall(11, 8, 1, 4, Resources.wall, listwalls);
            //生成零散砖墙
            MCreateWall(5, 5, 1, 1, Resources.wall, listwalls);
            MCreateWall(7, 5, 1, 1, Resources.wall, listwalls);
            MCreateWall(2, 6, 1, 1, Resources.wall, listwalls);
            MCreateWall(3, 6, 1, 1, Resources.wall, listwalls);
            MCreateWall(9, 6, 1, 1, Resources.wall, listwalls);
            MCreateWall(10, 6, 1, 1, Resources.wall, listwalls);
            MCreateWall(6, 7.5, 1, 1, Resources.wall, listwalls);
            //生成铁墙
            MCreateWall(6, 2.5, 1, 1, Resources.steel, liststeels);
            MCreateWall(0, 6.25, 1, 0.5, Resources.steel, liststeels);
            MCreateWall(12, 6.25, 1, 0.5, Resources.steel, liststeels);
            #endregion
        }
        

        #region//旧构造方法，只能创建4个或1个墙块，这不行，我还需要横2或竖2.我不想单独写一个方法，所以新方法引入xcount和ycount的新参数，就方法废弃
        //private static void MCreateWall(double x, double y, double count,Bitmap bt,List<ClassWall> listwall)//在指定位置竖排生成对应个数，对应类型的墙体，每个墙体由4个砖块图像组成
        //{//我只需要在1/2/3或者1.5/2.5这样的地方生成砖块即可
        //    int xPosition = (int)(x * 30);//不用将基类坐标改为double类型，因为地图生成时不是整数就是.5，砖块不可能生成在1或1.5之外，.5*30最后肯定是整数。
        //    int yPosition = (int)(y * 30);
        //    int max = (int)(yPosition + count * 30);
        //    for(int i = yPosition; i < max;i += 15)
        //    {
        //        ClassWall wall1 = new ClassWall(xPosition,i, bt);//修改，不传递指定资源，而是根据随机应变的资源
        //        listwall.Add(wall1);
        //        if (!(count < 1))
        //        {
        //            ClassWall wall2 = new ClassWall(xPosition + 15, i, bt);
        //            listwall.Add(wall2);

        //        }
        //    }
        //}
        #endregion
        
        private static void MCreateWall(double x, double y, double xCount , double yCount, Bitmap bt, List<ClassWall> listwall)
        {
            #region 为什么xy个数要用double？
            /*因为一个墙体是横2竖2四个墙图片拼成的！所以默认1*1是生成一个大砖块而不是一个墙图片
             *而0.5则代表在x或y轴方向，只需要一个墙图片即可！
             *如果是0.5*0.5，才是生成1个墙图片！*/
            #endregion
            int xPosition = (int)(x * 30);
            int yPosition = (int)(y * 30);
            int xMax = (int)(xCount *30);//原来是固定的个数*30像素，这里的y位置和循环的i=y位置取消了。
            int yMax = (int)(yCount *30);//现在则是循环执行次数和y个数有关，循环内横向生成次数和x个数有关
            for (int i = 0; i < yMax; i += 15)//改成双循环，外层控制竖向y轴输出
                for(int j = 0; j <xMax;j += 15)//内层控制横向x轴输出//因为这是用来生成一列而不是一排的方法，所以主循环控制y个数，内层控制x个数
                {
                    ClassWall wall = new ClassWall(xPosition + j, yPosition + i, bt);
                    listwall.Add(wall);
                }
        }

        public static void MCreatStart()//创建游戏开始时应用的数据
        {
            //敌坦克生成位置初始化
            arraypoint1[0].X = 0;//第一个生成在0.0
            arraypoint1[0].Y = 0;
            arraypoint1[1].X = Form1.Bttp.Width / 2 - 13;//第二个生成在中间
            arraypoint1[1].Y = 0;
            arraypoint1[2].X = Form1.Bttp.Width - 30;//第三个生成在最右边//减去30避免生成在墙里
            arraypoint1[2].Y = 0;

            //敌坦克实例初始化//很烦，这会导致我只有这四个敌人重复生成，我都enemyTankTemp = new ClassEnemy();了，结果一赋值还是成引用了，哎
            //ClassEnemy grayTank = new ClassEnemy(0, 0, 2, Resources.GrayUp, Resources.GrayDown, Resources.GrayLeft, Resources.GrayRight);
            //ClassEnemy greenTank = new ClassEnemy(0, 0, 2, Resources.GreenUp, Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);
            //ClassEnemy quickTank = new ClassEnemy(0, 0, 3, Resources.QuickUp, Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);
            //ClassEnemy slowTank = new ClassEnemy(0, 0, 1, Resources.SlowRight, Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);
            //listenemyType.Add(grayTank);
            //listenemyType.Add(greenTank);
            //listenemyType.Add(quickTank);
            //listenemyType.Add(slowTank);
            //把列表当数组用了难怪报错说超出索引，还没add呢根本就没有元素23333
            //listenemyTank[0] = new ClassEnemy(0, 0, 2, Resources.GrayUp, Resources.GrayDown, Resources.GrayLeft, Resources.GrayRight);//灰色坦克
            //listenemyTank[1] = new ClassEnemy(0, 0, 2, Resources.GreenUp, Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);//绿色坦克
            //listenemyTank[2] = new ClassEnemy(0, 0, 3, Resources.QuickUp, Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);//快速坦克
            //listenemyTank[3] = new ClassEnemy(0, 0, 1, Resources.SlowRight, Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);//慢速坦克
        }
        #endregion

        #region 把绘图方法合并到一起
        /*public static void MDrawMap()//实际画图方法，上面的是计算图形位置方法
        {
            foreach (ClassWall wall in listwalls)//将墙列表中的每个墙块依次绘制在画布上
                wall.MDrawSelf();
            foreach (ClassWall steel in liststeels)
                steel.MDrawSelf();
        }

        public static void MDrawBoss()
        {
            BOSS.MDrawSelf();
            foreach (ClassWall wl in listwallsBoss)
                wl.MDrawSelf();
        }

        internal static void MDrawMyTank()
        {
            myTank = myTanktemp;
            if(ClassCreateLogic.myTanktemp.isMoving == true)//我要做的就是把移动移到主循环，跟帧率绑定
            {
                switch (ClassCreateLogic.myTanktemp.dir)
                {
                    case EM_Direction.Up:
                        myTanktemp.Y -= myTanktemp.speed;
                        break;
                    case EM_Direction.Down:
                        myTanktemp.Y += myTanktemp.speed;
                        break;
                    case EM_Direction.Left:
                        myTanktemp.X -= myTanktemp.speed;
                        break;
                    case EM_Direction.Right:
                        myTanktemp.X += myTanktemp.speed;
                        break;
                }
            }
            myTank.MDrawSelf();
        }*/
        #endregion

        #region draw方法
        public static void MDrawStaticObject()//绘制静态对象方法
        {
            BOSS.MDrawSelf();
            //foreach (ClassWall wl in listwallsBoss)
            //    wl.MDrawSelf();
            foreach (ClassWall wall in listwalls)
                wall.MDrawSelf();
            foreach (ClassWall steel in liststeels)
                steel.MDrawSelf();
        }

        
        public static void MDrawActiveObject()//绘制动态对象方法
        {
            //myTank = myTanktemp;//不需要这句，因为是引用类型，二者指向了同一个对象
            ClassShowLogic.MMoveCheck(myTanktemp);//先调用检测，如果撞墙了就将ismoving改为flase
            if (myTanktemp.isMoving == true)
               ClassShowLogic.MMove(myTanktemp);
            myTank.MDrawSelf();

            //绘制敌人坦克
            //bool temp = MEnemyTankBorn();//先调用，随机生成敌方坦克，决定好位置和种类
            //ClassEnemy enemyTankTemp =  MEnemyTankBorn();
            //if (enemyTankTemp != null)
            //    MEnemyBorn(enemyTankTemp);
            MEnemyTankBorn();
            ClassShowLogic.MEnemyAI();//调用敌人ai改变敌人位置和移动状态
            foreach (ClassEnemy ey in listenemyTank)
                ey.MDrawSelf();
        }
        #endregion

        public static void MEnemyBorn(ClassEnemy enemyTemp)//生成敌人坦克
        {
            //Random rt1 = new Random();
            int index = ra.Next(0, 3);//随机生成0-3（不包括3）的整数，即敌人出生地点坐标的索引
            Point bornPoint = arraypoint1[index];//当前随机到的出生地点
            enemyTemp.X = bornPoint.X; 
            enemyTemp.Y = bornPoint.Y;
            listenemyTank.Add(enemyTemp);
        }
        public static void/*ClassEnemy*/ MEnemyTankBorn()
        {
            if (enemyBornCount < 181)
                enemyBornCount++;
            if (enemyBornCount < enemyBornSpeed || enemyBornNow > enemyBornMax) return /*null*/;//如果生成计数器小于生成速度，则以后代码不执行
            //Random rt2 = new Random();
            int enemytype = ra.Next(0,4);//随机生成敌人的种类
            ClassEnemy enemyTankTemp = new ClassEnemy();//根据种类生成一个敌人
            //enemyTankTemp = listenemyType[enemytype];//根据种类生成一个敌人
            switch (enemytype)
            {
                case 0:
                    enemyTankTemp = new ClassEnemy(0, 0, 2, Resources.GrayUp, Resources.GrayDown, Resources.GrayLeft, Resources.GrayRight);
                    break;
                case 1:
                    enemyTankTemp = new ClassEnemy(0, 0, 2, Resources.GreenUp, Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);
                    break;
                case 2:
                    enemyTankTemp = new ClassEnemy(0, 0, 3, Resources.QuickUp, Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);
                    break;
                case 3:
                    enemyTankTemp = new ClassEnemy(0, 0, 1, Resources.SlowUp, Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);
                    break;
            }
            enemyBornCount = 0;//生成一个敌人后，计数器归零
            enemyBornNow++;//敌人计数器+1
            MEnemyBorn(enemyTankTemp);
            return;
            //return enemyTankTemp;
        }

    }
}
