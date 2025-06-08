using _06_坦克大战_正式.activeobjectclass;
using _06_坦克大战_正式.Properties;
using _06_坦克大战_正式.staticbojectclass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.logic
{
    internal class ClassCreateLogic : ClassBaseLogic//静态生成逻辑
    {
        private static List<ClassWall> listwalls = new List<ClassWall>();
        private static List<ClassWall> listwallsBoss = new List<ClassWall>();
        private static List<ClassWall> liststeels = new List<ClassWall>();
        //private static List<ClassProp> listprops = new List<ClassProp>();
        private static ClassProp BOSS;
        private static ClassMy myTank;
        public static void MCreateBoss()//现在只有一关，若是有多关每关都需要生成boss,没准一关地图比一关大呢233
        {//将boss和周边的墙一起生成，固定在地图下半的底部中间
            double xPasition = (Form1.Bttp.Width - 30) / 2;
            double yPasition = (Form1.Bttp.Height - 30);
            double x1 = (xPasition - 15) / 30, x2 = (xPasition + 30) / 30;
            double y1 = yPasition / 30, y2 = (yPasition -15) / 30;
            BOSS = new ClassProp((int)xPasition, (int)yPasition , Resources.Boss);
            //生成基地防御砖墙
            MCreateWall(x1, y1, 0.5, 1, Resources.wall, listwallsBoss);
            MCreateWall(x1, y2, 2, 0.5, Resources.wall, listwallsBoss);
            MCreateWall(x2, y1, 0.5, 1, Resources.wall, listwallsBoss);
        }
        public static void MDrawBoss()
        {
            BOSS.MDrawSelf();
            foreach (ClassWall wl in listwallsBoss)
                wl.MDrawSelf();
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

        public static void MDrawMap()//实际画图方法，下面的是计算图形位置方法
        {
            foreach (ClassWall wall in listwalls)//将墙列表中的每个墙块依次绘制在画布上
                wall.MDrawSelf();
            foreach(ClassWall steel in liststeels)
                steel.MDrawSelf();
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

        internal static void MCreateMyTank()
        {//固定生成在老巢的左边
            int xPosition = (Form1.Bttp.Width - 30) / 2 - 45;
            int yPosition = Form1.Bttp.Height - 30 ;
            myTank = new ClassMy(xPosition, yPosition, 1);
        }
        internal static void MDrawMyTank()
        {
            myTank.MDrawSelf();
        }

    }
}
