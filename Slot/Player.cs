using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot
{
    public class Player
    {
        public enum PurchasePower
        {
            非R,
            小R,
            中R,
            大R,
            超R,
        }
        public Player(int level)
        {
            this.level = level;
            List<Grade> grades = Program.slot.objs.Gets<Grade>(l => l.level <= level);
            int max = grades.Select(l => l.level).Max();
            grade = grades.Find(l => l.level == max);
            vip = new Vip(level);

        }
        public int level;
        public Grade grade;
        public Vip vip ;
        public float ShopCount(Bet bet)
        {
            return (float)ShopCoin  / bet.coin;
        }
        public long ShopCoin => (long)(grade.MaxBet.coin * grade.orignalPurchasePower * vip.CoinFix);
        public int ShopFix => (int)(grade.MaxBet.coin * grade.orignalPurchasePower/ Program.slot.startShopCoin);
    }
}
