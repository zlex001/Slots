using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot
{
    public class Manager
    {
        public long startShopCoin;
        public float MachineRtp = 0.95f;
        public float SystemRtp = 0.1f;
        public Basic.File.Manager file = new Basic.File.Manager();
        public Basic.Excel.Manager excel = new Basic.Excel.Manager();
        public Basic.Data.Manager data = new Basic.Data.Manager();
        //public Core.Config.Manager config = new Core.Config.Manager();
        public Basic.List objs = new Basic.List();
        public List<int> betPool = new List<int> { 1, 2, 3, 4, 5, 6, 9, 10, 12, 15, 18, 20, 21, 24, 25, 27, 30, 33, 35, 36, 39, 40, 42, 45, 48, 50, 51, 54, 55, 57, 60, 63, 65, 66, 69, 70, 72, 75, 78, 80, 81, 84, 85, 87, 90, 93, 95, 96, 99, 100, 102, 105, 108, 111, 114, 117, 120, 123, 126, 129, 132, 135, 138, 141, 144, 147, 150, 153, 156, 159, 162, 165, 168, 171, 174, 177, 180, 183, 186, 189, 192, 195, 198, 200, 201, 204, 207, 210, 213, 216, 219, 222, 225, 228, 231, 234, 237, 240, 243, 246, 249, 250, 252, 255, 258, 261, 264, 267, 270, 273, 276, 279, 282, 285, 288, 291, 294, 297, 300, 303, 306, 309, 312, 315, 318, 321, 324, 327, 330, 333, 336, 339, 342, 345, 348, 350, 351, 354, 357, 360, 363, 366, 369, 372, 375, 378, 381, 384, 387, 390, 393, 396, 399, 400, 402, 405, 408, 411, 414, 417, 420, 423, 426, 429, 432, 435, 438, 441, 444, 447, 450, 453, 456, 459, 462, 465, 468, 471, 474, 477, 480, 483, 486, 489, 492, 495, 498, 500, 501, 504, 507, 510, 513, 516, 519, 522, 525, 528, 531, 534, 537, 540, 543, 546, 549, 550, 552, 555, 558, 561, 564, 567, 570, 573, 576, 579, 582, 585, 588, 591, 594, 597, 600, 603, 606, 609, 612, 615, 618, 621, 624, 627, 630, 633, 636, 639, 642, 645, 648, 650, 651, 654, 657, 660, 663, 666, 669, 672, 675, 678, 681, 684, 687, 690, 693, 696, 699, 700, 702, 705, 708, 711, 714, 717, 720, 723, 726, 729, 732, 735, 738, 741, 744, 747, 750, 753, 756, 759, 762, 765, 768, 771, 774, 777, 780, 783, 786, 789, 792, 795, 798, 800, 801, 804, 807, 810, 813, 816, 819, 822, 825, 828, 831, 834, 837, 840, 843, 846, 849, 850, 852, 855, 858, 861, 864, 867, 870, 873, 876, 879, 882, 885, 888, 891, 894, 897, 900, 903, 906, 909, 912, 915, 918, 921, 924, 927, 930, 933, 936, 939, 942, 945, 948, 950, 951, 954, 957, 960, 963, 966, 969, 972, 975, 978, 981, 984, 987, 990, 993, 996, 999 };
        public void Init()
        {
            data.Load<Data.Bet>(Basic.Data.Type.Excel);
            data.Load<Data.Grade>(Basic.Data.Type.Excel);


            Console.WriteLine("BetAdd:");
            foreach (Data.Bet betConfig in data.list.Gets<Data.Bet>())
            {
                objs.Add(new Bet { id = Convert.ToInt32(betConfig.id), type = Convert.ToInt32(betConfig.type), coin = betConfig.coin });
            }
            Console.WriteLine("GradeAdd:");
            foreach (Data.Grade gradeConfig in data.list.Gets<Data.Grade>())
            {
                objs.Add(new Grade { id = Convert.ToInt32(gradeConfig.id), level = gradeConfig.level, orignalPurchasePower = gradeConfig.orignalPurchasePower });
            }
            List<List<int>> betIdss = new List<List<int>>();
            List<int> endIds = new List<int>();
            List<double> points = Basic.Numerical.instance.ArithmeticProgression(1, objs.Gets<Grade>().Count, 5);
            endIds.AddRange(Basic.Numerical.instance.ArithmeticProgression(points[0], points[1], 5).Select(p => (int)p));
            endIds.AddRange(Basic.Numerical.instance.ArithmeticProgression(points[1], points[2], 6).Select(p => (int)p));
            endIds.AddRange(Basic.Numerical.instance.ArithmeticProgression(points[2], points[3], 6).Select(p => (int)p));
            endIds.AddRange(Basic.Numerical.instance.ArithmeticProgression(points[3], points[4], 7).Select(p => (int)p));
            endIds = endIds.Distinct().ToList();
            for (int i = 1; i < Basic.Numerical.instance.ArithmeticProgression(1, objs.Gets<Grade>().Count, 5).Count; i++)
            {
                double start = Basic.Numerical.instance.ArithmeticProgression(1, objs.Gets<Grade>().Count, 5)[i - 1];
                double end = Basic.Numerical.instance.ArithmeticProgression(1, objs.Gets<Grade>().Count, 5)[i];
            }
            List<int> startIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            for (int y = 0; y < objs.Gets<Grade>().Count; y++)
            {
                List<int> ids = new List<int>();
                if (y < 21)
                {
                    for (int i = 1; i <= y + 1; i++)
                    {
                        ids.Add(i);
                    }
                }
                betIdss.Add(ids);
            }

            for (int i = 0; i < 21; i++)
            {
                List<double> ids = Basic.Numerical.instance.ArithmeticProgression(startIds[i], endIds[i], betIdss.Count - 20);
                for (int j = 0; j < betIdss.Count; j++)
                {
                    if (j > 20)
                    {
                        betIdss[j].Add((int)ids[j - 20]);
                    }
                }
            }
            Console.WriteLine("CreatRegularBets:");
            for (int i = 0; i < betIdss.Count; i++)
            {
                List<int> regularBetIds = betIdss[i];
                Grade grade = objs.Get<Grade>(g => g.id == i + 1);
                for (int j = 0; j < regularBetIds.Count; j++)
                {
                    grade.objs.Add(objs.Get<Bet>(b => b.type == 0 && b.id == regularBetIds[j]));
                }
            }
            Console.WriteLine("CompleteHighBets:");
            foreach (Grade grade in objs.Gets<Grade>(g => g.id >= 30))
            {
                grade.CompleteHighBets();
            }
            Console.WriteLine("CaculateExp:");
            List<double> levelUpPers = Basic.Numerical.instance.ArithmeticProgression(0.05, 0.1, objs.Gets<Grade>(g => g.Protect).Count);
            for (int i = 0; i < levelUpPers.Count; i++)
            {
                int id = i + 1;
                Grade grade = objs.Gets<Grade>(g => g.Protect)[i];
                double count = 1 / levelUpPers[i];
                grade.exp = (long)(count * grade.MaxBet.Exp(grade) * grade.LevelCount);
            }
            levelUpPers = Basic.Numerical.instance.ArithmeticProgression(0.1, 0.01, objs.Gets<Grade>(g => !g.Protect).Count);
            for (int i = 0; i < levelUpPers.Count; i++)
            {
                int id = i + 1;
                Grade grade = objs.Gets<Grade>(g => !g.Protect)[i];
                double count = 1 / levelUpPers[i];
                grade.exp = (long)(count * grade.MaxBet.Exp(grade) * grade.LevelCount);
            }
            Console.WriteLine("CreatPlayer");
            for (int i = 1; i <= objs.Gets<Grade>().Select(g => g.level).Max(); i++)
            {
                Console.WriteLine("CreatPlayer" + i);
                objs.Add(new Player(i));
            }
            Console.WriteLine("ShopCoinFix");
            startShopCoin = objs.Get<Grade>(g => g.level == 1).Player.ShopCoin;

        }
        public void Start()
        {
            Init();
            OutPut();
            //instance.Test();
        }
        public long Approximation(long num)
        {
            num *= 10000;
            string str = num.ToString();
            if (str.Length <= 3)
            {
                return Basic.Numerical.instance.Approximation((int)num, betPool);
            }
            else
            {
                long fix = (long)Math.Pow(10, str.Length - 3);
                int head = (int)(num / fix);
                head = Basic.Numerical.instance.Approximation(head, betPool);
                str = (head * fix).ToString();
                if (str.Length < 10)
                {
                    head = (int)Basic.Numerical.instance.Rasure(head, 2);
                }
                return head * fix/10000;
            }
        }
        public void BetOutPut( string sheet)
        {
            List<string> titles = new List<string> { "等级" };
            for (int i = 1; i < 22; i++)
            {
                titles.Add("Bet" + i);
            }
            titles.Add("");
            for (int i = 1; i < 18; i++)
            {
                titles.Add("Bet" + i);
            }
            List<List<string>> datass = new List<List<string>> { titles };
            foreach (Grade grade in objs.Gets<Grade>())
            {
                List<string> data = new List<string> { grade.level.ToString() };
                for (int i = 0; i <= 20; i++)
                {
                    Bet bet = grade.objs.Get<Bet>(b => b.type == 0 && b.Id(grade) == i);
                    data.Add(bet == null ? "" : bet.Coin.ToString());
                }
                data.Add("");
                for (int i = 0; i <= 16; i++)
                {
                    Bet bet = grade.objs.Get<Bet>(b => b.type == 1 && b.Id(grade) == i);
                    data.Add(bet == null ? "" : bet.Coin.ToString());
                }
                datass.Add(data);
            }
            excel.Save(datass, sheet);
        }
        public void SpinCountOutPut(string sheet)
        {
            List<string> titles = new List<string> { "等级" };
            for (int i = 1; i < 22; i++)
            {
                titles.Add("Bet" + i);
            }
            titles.Add("");
            for (int i = 1; i < 18; i++)
            {
                titles.Add("Bet" + i);
            }
            List<List<string>> datass = new List<List<string>> { titles };
            foreach (Grade grade in objs.Gets<Grade>())
            {
                List<string> data = new List<string> { grade.level.ToString() };
                for (int i = 0; i <= 20; i++)
                {
                    Bet bet = grade.objs.Get<Bet>(b => b.type == 0 && b.Id(grade) == i);
                    data.Add(bet == null ? "" : grade.SpinCount(bet).ToString());
                }
                data.Add("");
                for (int i = 0; i <= 16; i++)
                {
                    Bet bet = grade.objs.Get<Bet>(b => b.type == 1 && b.Id(grade) == i);
                    data.Add(bet == null ? "" : grade.SpinCount(bet).ToString());
                }
                datass.Add(data);
            }
            excel.Save(datass,  sheet);
        }
        public void GradeConfigOutPut(string sheet)
        {
            List<string> titles = new List<string> {"level", "unlockBet", "regularBet", "highBet", "3jp", "4jp", "5jp", "6jp", };
            List<List<string>> datass = new List<List<string>> { titles };
            foreach (Grade grade in objs.Gets<Grade>())
            {
                datass.Add(new List<string> { grade.level.ToString(),
                   Basic.Numerical.instance.Rasure (grade.UnlockBet.coin,3).ToString(),
                    string.Join("#", grade.objs.Gets<Bet>(b=>b.type==0).OrderBy(b=>b.coin).Select(b=> b.Coin).Distinct()),
                    string.Join("#", grade.objs.Gets<Bet>(b=>b.type==1).OrderBy(b=>b.coin).Select(b=>b.Coin).Distinct()),
                    string.Join("#", grade.Jackpots(3).Select(j=>Basic.Numerical.instance.Rasure ( j.Coin,3))),
                    string.Join("#", grade.Jackpots(4).Select(j=>Basic.Numerical.instance.Rasure ( j.Coin,4))),
                    string.Join("#", grade.Jackpots(5).Select(j=>Basic.Numerical.instance.Rasure ( j.Coin,5))),
                    string.Join("#", grade.Jackpots(6).Select(j=>Basic.Numerical.instance.Rasure ( j.Coin,6))),

                });
            }
            excel.Save(datass, sheet);
        }
        public void LevelConfigOutput(string sheet)
        {
            int max = objs.Gets<Player>().Select(p => p.level).Max();
            List<string> titles = new List<string> { "level", "exp" };
            List<List<string>> datass = new List<List<string>> { titles };
            for(int i = 1; i <= max; i++)
            {
                Player player = objs.Get<Player>(p => p.level == i);
                datass.Add(new List<string> { player.level.ToString(), player.grade.LevelExp.ToString() });
            }
            excel.Save(datass, sheet);
        }
        public void OutPut()
        {
            BetOutPut("Bet");
            SpinCountOutPut("SpinCount");
            GradeConfigOutPut("GradeConfig");
            LevelConfigOutput("LevelConfig");
            ShopConfigOutput("ShopConfig");
            PlayerOutput("Player");
            List<Grade> grades = objs.Gets<Grade>(g => g.Protect);
            Console.WriteLine("度过新手期一直用MaxBet的话需要{0}次", grades.Select(g => g.SpinCount(g.MaxBet)).Sum());
        }
        public void ShopConfigOutput(string sheet)
        {

            List<string> titles = new List<string> { "level", "商店金币" };
            List<List<string>> datass = new List<List<string>> { titles };
            List<int> shopFixs = objs.Gets<Player>().Select(p => p.ShopFix).Distinct().ToList();
            foreach(int shopFix in shopFixs.OrderBy(s=>s))
            {
                List<Player> players = objs.Gets<Player>(p => p.ShopFix == shopFix);
                int min = players.Select(g => g.level).Min();
                int max = players.Select(g => g.level).Max();
                datass.Add(new List<string> { min+"#"+max, shopFix.ToString() });
            }
            excel.Save(datass, sheet);
        }
        public void PlayerOutput(string sheet)
        {
            List<string> titles = new List<string> { "level", "Vip", "VipFix", "ShopCoin", "非R", "小R", "中R", "大R","超R" };
            List<List<string>> datass = new List<List<string>> { titles };
            foreach(Grade grade in objs.Gets<Grade>())
            {
                List<string> datas = new List<string> { grade.Player.level.ToString(), grade.Player.vip.Level.ToString(), grade.Player.vip.CoinFix.ToString(), grade.Player.ShopCoin.ToString(), grade.Player.ShopCount(grade.MaxBet).ToString()};
                for(int i = 5; i <= 8; i++)
                {
                    grade.Player.vip.purchase += i- grade.Player.vip.Level;
                    datas.Add(grade.Player.ShopCount(grade.MaxBet).ToString());
                }
                datass.Add(datas);
            }
            excel.Save(datass, sheet);
        }
    }
}
