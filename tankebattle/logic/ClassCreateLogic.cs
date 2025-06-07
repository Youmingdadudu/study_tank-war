using _06_坦克大战_正式.Properties;
using _06_坦克大战_正式.staticbojectclass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_坦克大战_正式.logic
{
    internal class ClassCreateLogic : ClassBaseLogic//静态生成逻辑
    {
        private static List<ClassWall> listwalls = new List<ClassWall>();
        public static void MCreateMap()
        {
            MCreateWall(1, 1, 5, listwalls);
            MCreateWall(1, 9, 5, listwalls);
        }

        public static void MDrwaMap()
        {
            foreach(ClassWall wall in listwalls)//将墙列表种的每个墙块依次绘制在画布上
            {
                wall.MDrawSelf();
            }
        }
        private static void MCreateWall(int x, int y, int count,List<ClassWall> listwall)//在指定位置竖排生成对应个数的墙体，每个墙体由4个砖块图像组成
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            int max = yPosition + count * 30;
            for(int i = yPosition; i < max;i += 15)
            {
                ClassWall wall1 = new ClassWall(xPosition,i,Resources.wall);
                ClassWall wall2 = new ClassWall(xPosition + 15, i, Resources.wall);
                listwall.Add(wall1);
                listwall.Add(wall2);
            }
        }

    }
}
