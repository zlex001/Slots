using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot
{
    public class Grade
    {
        public int id;
        public int level;
        public long exp;
        public long maxBet;
        public float orignalPurchasePower;
        public Basic.List objs = new Basic.List();
        public double LevelCount
        {
            get
            {
                Grade next = Program.slot.objs.Get<Grade>(l => l.level > level);
                return next != null ? next.level - level : 0;
            }
        }
        public long LevelExp => (long)(exp / LevelCount);
        public List<Bet> RegularBets => objs.Gets<Bet>(b => b.type == 0).Distinct().OrderBy(b => b.coin).ToList();
        public List<Bet> HighBet => objs.Gets<Bet>(b => b.type == 1).Distinct().OrderBy(b => b.coin).ToList();
        public Grade Last =>Program.slot.objs.Get<Grade>(l => l.id==id-1);
        public Grade Next
        {
            get
            {
                List<Grade> lasts = Program.slot.objs.Gets<Grade>(l => l.level > level);
                if (lasts.Count > 0)
                {
                    int min = lasts.Select(l => l.level).Min();
                    return lasts.Find(l => l.level == min);
                }
                else
                {
                    return null;
                }

            }
        }
        public Bet MaxBet 
        {
            get
            {
                List<Bet> bets = objs.Gets<Bet>(b => b.type == 0);
                long max = bets.Select(b => b.coin).Max();
                return bets.Find(b => b.coin == max);
            }
        }
        public Bet MaxHighBet
        {
            get
            {
                List<Bet> bets = objs.Gets<Bet>(b => b.type == 1);
                long max = bets.Select(b => b.coin).Max();
                return bets.Find(b => b.coin == max);
            }
        }
        public Bet UnlockBet => RegularBets.Count>8? RegularBets[8]: RegularBets[RegularBets.Count/2];
        public Player Player => Program.slot.objs.Get<Player>(p => p.grade == this);
        public bool Protect => level < 30;
        public List<Bet> Jackpots(int count)
        {
            List<Bet> bets = new List<Bet>();

            if (RegularBets.Count < count)
            {
                for(int i = 0; i < count; i++)
                {
                    bets.Add(RegularBets[0]);
                }
            }
            else if (RegularBets.Count < 21)
            {
                for (int i = 0; i < count; i++)
                {
                    bets.Add(RegularBets[i]);
                }
            }
            else
            {
                int distance = 2;
                while (bets.Count < count)
                {
                    bets.AddRange(objs.Gets<Bet>(b => b.type == 0 && Math.Abs(UnlockBet.Id(this) - b.Id(this)) == distance));
                    distance += 2;
                }
            }
            List<Bet> finals = new List<Bet>();
            for (int i = 0; i < count; i++)
            {
                finals.Add(bets.OrderBy(b => b.coin).ToList()[i]);
            }
            return finals;

        }


        public long SpinCount(Bet bet)
        {
            switch (level)
            {
                case 1:
                    return (1 + 2);
                case 3:
                    return 5;
                case 4:
                    return 10;
            }
            return (long)((exp / bet.Exp(this)));
        }
        public double SpinPerLevel(Bet bet)
        {
            long a = SpinCount(bet);
            double final= 1f / a* LevelCount;
            return final;
        }


        public void CompleteHighBets()
        {
            int id = objs.Gets<Bet>(b => b.type == 0).Count > 17 ? 17 : objs.Gets<Bet>(b => b.type == 0).Count - 1;
            Bet start = new Bet { type = 1, coin = objs.Get<Bet>(b => b.type == 0 && b.Id(this) == id).coin };
            Bet maxHighBet = Program.slot.objs.Get<Bet>(b => b.type == 1 && b.id == this.id);
            objs.Add(maxHighBet);
            double fix = Math.Pow(maxHighBet.coin / start.coin, 1f / 16f);
            objs.Add(start);
            long coin = start.coin;
            for (int i = 0; i < 15; i++)
            {
                coin =Basic.Numerical.instance.Rasure( (long)(coin * fix), 3);
                objs.Add(new Bet { type = 1, coin = coin });
            }
        }
    }
}
