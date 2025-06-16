using _06_坦克大战_正式.activeobjectclass;
using _06_坦克大战_正式.baseclass;
using _06_坦克大战_正式.staticbojectclass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
        public static void MPlayerControl/*MovePosition*/(object type, KeyEventArgs args)//方向和移动判断，具体移动不在这了
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
                        myTanktemp.isMoving = true;
                        break;
                    case Keys.S:
                        if (myTanktemp.dir != EM_Direction.Down)
                            myTanktemp.dir = EM_Direction.Down;
                        myTanktemp.isMoving = true;
                        break;
                    case Keys.A:
                        if (myTanktemp.dir != EM_Direction.Left)
                            myTanktemp.dir = EM_Direction.Left;
                        myTanktemp.isMoving = true;
                        break;
                    case Keys.D:
                        if (myTanktemp.dir != EM_Direction.Right)
                            /*ClassCreateLogic.*/
                            myTanktemp.dir = /*ClassMy.*/EM_Direction.Right;
                        myTanktemp.isMoving = true;
                        break;
                    case Keys.Space://升级为控制方法，不只是移动，还有开火控制
                        lock (_lock)
                        {
                            if(myTank != null)
                            MCreateBullet(myTank.X, myTank.Y, 5, myTank.Width, myTank.Height, myTank.dir, EM_Tag.myTank);
                        }
                        break;
                }
                /*myTanktemp.isMoving = true;*///不能直接在这儿了，成为控制方法后会导致摁下其他键包括开火也会移动233。也不写条件wasd才移动的判定了那太麻烦还没必要额外检测直接挪就行了
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
        private static ClassWall MIsCollidedWalllist(Rectangle rta, List<ClassWall> listao)//墙列表碰撞检测，哎列表不支持父类范型改用子类泛型//静态列表对象检测逻辑
        {
            foreach (ClassWall o in listao)
            {
                if(o.rectangle.IntersectsWith(rta))
                    return o;
            }
            return null;
        }
        //private static ClassProp MIsCollidedSO(Rectangle rta, ClassProp ao)//道具碰撞检测//静态单个对象检测逻辑（目前就一个boss，没有其他道具）
        //{
        //    if (ao.rectangle.IntersectsWith(rta))//单个判断，以后可以改为道具列表循环判断，boss恒定0号元素即可
        //    {
        //        return ao;
        //    }
        //        return null;
        //}//先隐藏，boss不加入道具列表了，直接单独判定，所以暂时用不上道具判断了
        private static ClassEnemy MIsCollidedBullet/*Tank*/(ClassActiveObject bullet,Rectangle rta/*, List<ClassEnemy> listtank*/)//发现用不着这个参数，直接传递敌坦克列表就行
        {//灵感，把所有的碰撞检测合并，然后通过传递的参数列表来灵活检测，例如传递墙列表就检测墙，道具列表就检测道具。如果是子弹..等会，不行，这些都不是一个类不能用一个参数列表类型，美梦破灭了233
            if (bullet.tag == EM_Tag.myTank)
            {
                foreach (ClassEnemy ao in listenemyTank)
                {
                    if (ao.rectangle.IntersectsWith(rta))
                    {
                        //enemyBornNow--;//如果想要消灭后一直生成坦克就需要让集合计数器-1
                        return ao;
                    } 
                }
            }
            else if (myTank != null && bullet.tag == EM_Tag.enemyTank)
            {
                if (myTank.rectangle.IntersectsWith(rta))
                {
                    myTank.isHave = false;
                    ClassEnemy classEnemy = new ClassEnemy();//用于触发爆炸特效的临时坦克。因为代码原因不能直接返回mytank。幸好我有先见之明早早做了无参构造方法备着2333
                    return classEnemy;
                }
                    
            }
            return null;
        }
        private static ClassActiveObject MIsCollidedTank(ClassActiveObject AObject, Rectangle rta)//发现将子弹用的坦克碰撞改一改就行不用单开233.加个判定只有子弹碰撞才会弄坏mytank就好了
        {//算了还是再改一个吧不想再改类了，还是写方法糊墙方便233
            if (myTank == null) return null;//如果我的坦克不存在就不用判定了
            if (AObject.tag == EM_Tag.myTank)
            {
                foreach (ClassEnemy ao in listenemyTank)//判定mytanke有没有和任意敌坦克碰撞
                {
                    if (ao.rectangle.IntersectsWith(rta))
                        return ao;//返回谁都行，只用来判定，只要不是空就行
                }
            }
            else if (AObject.tag == EM_Tag.enemyTank)
            {
                if (myTank.rectangle.IntersectsWith(rta))//判定敌坦克和mytank有没有碰撞
                    return AObject;
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
            if (MIsCollidedWalllist(rtaTemp, listwalls) != null)
            {
                AObject.isMoving = false;
                return;//碰到一个墙后续就不需要检测了
            }
            if (MIsCollidedWalllist(rtaTemp, liststeels) != null)
            {
                AObject.isMoving = false;
                return;
            }
            if (BOSS != null && BOSS.rectangle.IntersectsWith(rtaTemp) == true)
            {
                AObject.isMoving = false;
                return;
            }
            //if (MIsCollidedSO(rtaTemp, BOSS) != null)
            //{
            //    AObject.isMoving = false;
            //    return;
            //}
            //敌我车体的碰撞
            if (MIsCollidedTank(AObject,rtaTemp) != null)
            {
                AObject.isMoving = false;
                return;
            }
        }
        public static void MBulletCheck(ClassActiveObject AObject)//子弹的碰撞(本来想用移动的哪个凑合的，结果发现子弹图片太大，导致子弹和碰撞物离得太远就停下了233)
        {
            int x = AObject.X /*- AObject.Width / 2*/;//不对，蒙了，原因应该是不需要在这里改，而且虽然构造改了，但生成方法又让子弹出现在坦克中间了//不用了，我想起了我在classshow里已经处理了，现在相当于子弹的图形在左上角了
            int y = AObject.Y /*- AObject.Height / 2*/;
            int xPosition = AObject.X + AObject.Width / 2;//用于爆炸特效的坐标，让爆炸中心点在子弹图片的中心点也就是子弹的位置上
            int yPosition = AObject.Y + AObject.Height / 2;
            Rectangle rtaTemp = new Rectangle();
            rtaTemp = AObject.rectangle;
            //矩形处理，让矩形只有中间子弹那么大//因为矩形是只读属性，不能直接在子弹类里改
            rtaTemp.X = x + rtaTemp.Width / 2 - 2;//调整xy坐标，子弹大小差不多5*5像素，就照4*4改吧！
            rtaTemp.Y = y + rtaTemp.Height / 2 - 2;
            rtaTemp.Width = 5;
            rtaTemp.Height = 5;
            //rtaTemp.Width = rtaTemp.Width - 18;//看老师的操作恍然大悟，我干嘛算啊直接给数值不就得了这又不是不能直接修改2333
            //rtaTemp.Height = rtaTemp.Height - 18;


            //因为子弹的独特性质（子弹在图片中心xy在左上角），检测是否超出窗体和碰撞，都需要每个方向单独修改,判定用的xy的值（但不能直接修改，这让就让子弹乱飞了）
            switch (AObject.dir)
            {//子弹在图片中心，坐标却是左上角，所以需要将xy挪到子弹的位置//加减弄反了，就这么简单的事情蒙了一小时，手忙脚乱调试把图片坐标和矩形边缘碰撞搞混了以为构造方法改变了xy啥的。
             //挠头，我是太困了吗怎么竟钻牛角尖呢，而且是完全不对的死胡同，那个词怎么说来着？蒙了，魔怔了，当局者迷了，鬼迷心窍？233
                case EM_Direction.Up:
                    y += AObject.Height / 2;
                    //rtaTemp.Y += (AObject.Height / 2);//子弹得调整对应的一半矩形，才是实际效果子弹撞上了（旧效果，跟老师学直接改矩形大小就用不上了）
                    break;
                case EM_Direction.Down:
                    y += (AObject.Height / 2);
                    //rtaTemp.Y -= (AObject.Height / 2);//我说怎么修改完矩形大小，子弹差点穿墙才撞上呢，原来这个忘记注释了233
                    break;
                case EM_Direction.Left:
                    x += AObject.Width / 2;
                    //rtaTemp.X += (AObject.Height / 2);
                    break;
                case EM_Direction.Right:
                    x += AObject.Width / 2;
                    //rtaTemp.X -= (AObject.Height / 2);
                    break;
            }
            //检测是否超出窗体
            if ((AObject.dir == EM_Direction.Right && x > 385) //因为速度是5，所以基本上停下来就到边界了
                || (AObject.dir == EM_Direction.Left && x <5)
                || (AObject.dir == EM_Direction.Down && y > 385)
                || (AObject.dir == EM_Direction.Up && y <5))
            {
                AObject.isMoving = false; return;//如果碰到窗体就不需要再检测是否碰到墙了
            }
            //检测是否碰撞墙体
            ClassWall tempWL = MIsCollidedWalllist(rtaTemp, listwalls);
            if (tempWL != null)
            {
                listdestroyWall.Add(tempWL);
                MCreateExplsion(xPosition, yPosition);//如果撞到墙了，就创建一个爆炸效果。创建和绘制是分开的，这里只管创建
                //listwalls.Remove(tempWL);
                AObject.isMoving = false; return;//碰到一个墙后续就不需要检测了
            }
            if (MIsCollidedWalllist(rtaTemp, liststeels) != null)
            {
                MCreateExplsion(xPosition, yPosition);
                AObject.isMoving = false; return;
            }
            if (BOSS != null && BOSS.rectangle.IntersectsWith(rtaTemp) == true)
            {
                BOSS.isHave = false;//想想还是不行，道具和boss完全不一样，算了还是把boss单拎出来特殊对待吧！//还是现在就改吧顺便把道具列表也整出来233//暂时就一个物品boss，先这样以后再改
                AObject.isMoving = false;
                MCreateExplsion(xPosition, yPosition);
                return;
            }
            //if (MIsCollidedSO(rtaTemp, BOSS) != null)
            //{
            //    AObject.isMoving = false; return;
            //}
            //检测是否碰到坦克
            ClassEnemy tempEY = MIsCollidedBullet(AObject, rtaTemp);
            if(MIsCollidedBullet(AObject,rtaTemp) != null)
            {
                AObject.isMoving = false;
                MCreateExplsion(xPosition, yPosition);
                listdestroyTank.Add(tempEY);
            }
        }


        #endregion

        #region 敌人的ai
        //先实现移动
        public static EM_Direction MEnemyMoveCheck(EM_Direction dirTemp)//相当于随即转向，不改了直接用就行//根据撞墙时的当前方向，转向另外三个方向，免得无效转向
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
                MEneryAutoChangeDir(ey);//随即转向方法，让ai更灵活多变
                if (ey.isMoving == false)//如果撞墙了，就换个方向
                {
                    //EM_Direction nowDir = ey.dir;//当前方向
                    ey.dir = MEnemyMoveCheck(ey.dir);//话说可不可以用转向计数器呢，一次循环最多转向4次啥的？嗯，感觉有可行性
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
                if (ey.isMoving == true)
                    MMove(ey);
                //MCreateBullet(ey.X, ey.Y, enemyBulletSpeed, ey.Width, ey.Height, ey.dir, EM_Tag.enemyTank);//能触发啊，为啥方法就不行呢
                MEnemyAttack(ey);//移动之后攻击//麻了，为什么不发子弹啊，明明里面的创造子弹没错啊，条件也没错啊//解决了，确实错了，不过是逻辑错了，难崩，乐
            }
            //Thread thread2 = new Thread(MEnemyAI);//想让转方向少判定点减少鬼畜大旋转，不过线程这不明白，先别乱搞了
            //thread2.Start();
            //Thread.Sleep(100);
        }

        private static void MEnemyAttack(ClassEnemy ey)//敌人攻击方法
        {
            if (ey.attackCounter < enemyAttackMax)
            {
                ey.attackCounter++; return;
            }
            //else if (ey.attackCounter > enemyAttackMax)//绷不住了，问了ds原来问题这么简单（我这个逻辑没有判定二者相等的时候我给忘了完全没注意没想起来2333），这叫啥，知见障灯下黑转不过弯来？挠头啥词来着2333
            //{(ey.attackCounter == enemyAttackMax)应该这么写逻辑，不过实际上不用写，因为就两种情况要么不满足要么开火，既然上面不满足就return那这里就不用判定了233
            lock (_lock)
            {
                MCreateBullet(ey.X, ey.Y, enemyBulletSpeed, ey.Width, ey.Height, ey.dir, EM_Tag.enemyTank);
            }
            ey.attackCounter = 0;//发射子弹后计数器归零
            //}
        }
        
        private static void MEneryAutoChangeDir(ClassEnemy ey)
        {
            ey.ChangeDirCount++;
            if (ey.ChangeDirCount < ey.ChangeDirSpeed) return;
            ey.dir = MEnemyMoveCheck(ey.dir);
            ey.ChangeDirCount = 0;
        }

        #endregion

        #region 通用子弹生成，玩家子弹控制（空格），ai子弹逻辑
        //乐，老师生成子弹的时候忘了子弹是个图片不是一个点了2333
        //通用子弹生成
        public static void MCreateBullet(/*int num,*/int x, int y, int speed, int width,int height,EM_Direction dir, EM_Tag tag)//num可以用来控制一次生成子弹数量，暂时没用
        {//让子弹在对应方向的炮口生成
            switch (dir)
            {
                case EM_Direction.Up:
                    x += width / 2;
                    break;
                case EM_Direction.Down:
                    x += width / 2;
                    y += height;
                    break;
                case EM_Direction.Left:
                    y += height / 2;
                    break;
                case EM_Direction.Right:
                    x += width;
                    y += height / 2;
                    break;
                    #region //旧生成位置
                    //case EM_Direction.Up:
                    //    y -= height / 2;
                    //    x += 1;
                    //    break;
                    //case EM_Direction.Down:
                    //    y += height / 2;
                    //    x += 2;
                    //    break;
                    //case EM_Direction.Left:
                    //    x -= width / 2;
                    //    y += 2;
                    //    break;
                    //case EM_Direction.Right:
                    //    x += width / 2;
                    //    y += 2;
                    //    break;
                    #endregion
            }
            ClassShow newbullet = new ClassShow(x, y, speed, dir, tag);
            listbullet.Add(newbullet);
        }
        public static void MBulletControl()//子弹的绘制，碰撞和销毁（将绘画和控制放在一起，直接调用更方便）
        {
            if (listbullet.Count == 0) return;//如果没子弹就不用执行这个方法
            #region 旧碰撞，移动和销毁
            //foreach (ClassShow bt in listbullet)
            //{
            //    //碰撞检测和销毁
            //    MBulletCheck(bt);//我个脑瘫，奇怪半天为啥怎么改都没用差点破防，一检查这里还是mmovecheck忘了换了2333
            //    //if (bt.isMoving == false)
            //        //    bt = null;//不行foreach里这个不是列表实际的值不能改。记下序号，加入序号列表，然后再来个循环将对应序号的子弹移除怎么样（不用了，用for循环就搞定了233）（老师的给子弹加一个是否存在的属性的办法也挺棒的，碰到了就表示无，然后当前循环结束后再销毁）
            //        //listbullet.Remove(bt);//不能直接移除，会报错，我得曲线移除233
            //    //子弹的移动和绘制
            //    if (bt.isMoving == true)
            //        MMove(bt);
            //    if(bt.isMoving == true)
            //        bt.MDrawSelf();//活动对象基类的返回图片方法已经将黑色背景去除了，我就不用再搞一次了
            //}
            //public static void MBulletDispose()//单独的销毁方法，
            //{
            //用不着了
            //}
        #endregion
            int count = listbullet.Count;//移动，碰撞检测，销毁一条龙
            for (int i = 0; i < /*listbullet.Count*/count; i++)
            {
                MBulletCheck(listbullet[i]);
                if (listbullet[i].isMoving == true)
                {
                    MMove(listbullet[i]); listbullet[i].MDrawSelf();
                }
                else//不能直接去除，这会导致集合的元素数量直接发生变化，后面的会直接前进1位。去除一个后，集合数量-1，而i会访问超出索引元素导致出错
                {
                    listbullet.Remove(listbullet[i]);//偷懒的办法是直接用listbullet.Count实时调整不用int count。但这样的问题是，去除0号子弹后，i=1，而原本的1号子弹前移成0号，导致这一帧会跳过该子弹的绘制直接绘制原本的2号子弹
                    i--;//解决了，灵光一闪灵机一动，既然是i比元素大一号，只要将i-1不就好了！不过这样也有个问题，那就是每次循环倒要返回列表的元素个数，这会不会太浪费计算效率呢？
                    count--;//解决了，既然i能减，那count也可以减啊！原本是例如3个元素少一个，只剩2个元素，但i会按照原来的count=3访问2号也就是第三个元素导致访问不到报错索引溢出，那让count也实时变动即可！不知道--运算和上边那个哪个更浪费时间233
                }
            }
        }

        //玩家子弹控制//不用了，直接将移动控制升级了，在那里就行
        //ai子弹逻辑

        #endregion

        #region //物品销毁逻辑
        public static void MDestroy()//销毁方法//和老师学碰撞和销毁合并？先不搞，如果大修了再说
        {
            if(BOSS != null && BOSS.isHave == false)//my坦克和boss应该执行游戏结束方法，不过目前还没有先拿这个顶一顶，先让图片没了再说
            {
                BOSS = null;
            }
            if(myTank != null && myTank.isHave == false)//发射子弹，绘图，碰撞检测都额外加了条件。其实感觉没必要，因为游戏已经结束了
            {
                myTank = null;
            }

            foreach (ClassWall wl in listdestroyWall)//测试后，发现子弹碰撞检测从动态绘图独立后，直接在MBulletCheck里移除也可以。不过不打算改，集中起来好管理和修改
            {
                listwalls.Remove(wl);
            }
            //foreach(ClassProp pp in listdestroyProp)
            //{
            //BOSS单独对待不用这个，暂时这个循环还没用呢
            //}
            foreach (ClassEnemy ey in listdestroyTank)
            {
                listenemyTank.Remove(ey);
            }
            foreach (ClassAct at in listdestroyExplsion)
            {
                if (at.playcount == at.playtime)//如果这个爆炸效果结束了（计数器满了），就销毁了
                {
                    listexplsion.Remove(at);
                }
            }
        }
        #endregion

        #region //爆炸特效逻辑
        public static void MCreateExplsion(int x, int y)
        {
            ClassAct newExplsion = new ClassAct(x,y,0,3);
            listexplsion.Add(newExplsion);
        }
        public static void MExplsionControl()
        {
            foreach (ClassAct at in listexplsion)
            {
                if (at.playcount < at.playtime)//如果绘制计数器没满，就根据设定绘图
                    at.MDrawSelf();
                else//如果满了，就加到移除名单里等待销毁
                {
                    listdestroyExplsion.Remove(at);
                }
            }
        }
        #endregion
    }
}
