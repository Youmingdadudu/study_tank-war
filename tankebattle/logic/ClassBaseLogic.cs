using _06_坦克大战_正式.activeobjectclass;
using _06_坦克大战_正式.staticbojectclass;
using System;
using System.Collections.Generic;
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
    }
}
