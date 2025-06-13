using _06_坦克大战_正式.activeobjectclass;
using _06_坦克大战_正式.staticbojectclass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.logic
{
    internal class ClassBaseLogic
    {
        //把数据都挪到逻辑基类，方便两个子类访问
        protected static List<ClassWall> listwalls = new List<ClassWall>();
        //protected static List<ClassWall> listwallsBoss = new List<ClassWall>();//不单独放了，都是红墙没必要
        protected static List<ClassWall> liststeels = new List<ClassWall>();
        //protected static List<ClassProp> listprops = new List<ClassProp>();
        protected static ClassProp BOSS;
        protected static ClassMy myTank;//本来想再整个public的引用来间接修改数据（朝向啥的），但是这个小游戏没必要考虑安全性，就偷懒了直接改成public2333
        public static ClassMy myTanktemp;//算了不偷懒了挑战自己233

        //敌人生成相关
        protected static Random ra = new Random();//都用同一个随机种子
        protected static int enemyBornSpeed = 180;//30秒生成1个，一秒60帧，所以速度是60
        protected static int enemyBornCount = 0;//帧率计数器，每帧+1，满60生成敌人。实际帧率会低于60，但对于窗体应用这也是没办法的事情（不知道有没有其他计时手段）
        protected static int enemyBornMax = 10;//最多生成10个，省的满屏都是2333
        protected static int enemyBornNow = 0;//最多生成10个，省的满屏都是2333
        protected static Point[] arraypoint1 = new Point[3];//敌人生成位置的列表
        protected static List<ClassEnemy> listenemyType = new List<ClassEnemy>(4);//将四种敌人坦克保存为一个列表
        //protected static Point bornPoint;//敌人当前生成位置
        //protected static ClassEnemy enemyTankTemp;//当前生成的敌人
        protected static List<ClassEnemy> listenemyTank = new List<ClassEnemy>();//将生成的坦克保存入这里，持续绘图，才能用来控制和检测碰撞什么的
    }
}
