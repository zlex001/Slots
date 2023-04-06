using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot
{
    public class Bet
    {
        public int id;
        public long coin;
        public int type;
        public long Coin => Program.slot.Approximation(coin);
        public int Id(Grade grade)
        {
            return type == 0 ? grade.RegularBets.IndexOf(this) : grade.HighBet.IndexOf(this);
        }
        public float ExpFix(Grade grade)
        {
            List<float> regulars = new List<float> { 1f, 0.95f, 0.95f, 0.9f, 0.9f, 0.85f, 0.85f, 0.8f, 0.8f, 0.75f, 0.75f, 0.7f, 0.7f, 0.65f, 0.65f, 0.6f, 0.6f, 0.55f, 0.55f, 0.5f, 0.5f, 0.45f, };
            List<float> highs = new List<float> { 0.533333333333333f, 0.466666666666667f, 0.4f, 0.333333333333333f, 0.3f, 0.266666666666667f, 0.233333333333333f, 0.2f, 0.183333333333333f, 0.166666666666667f, 0.15f, 0.133333333333333f, 0.125f, 0.116666666666667f, 0.108333333333333f, 0.104166666666667f, 0.1f, };
            List<float> final = type == 0 ? regulars : highs;
            int id=Id(grade);
            return final[Id(grade)] ;
        }
        public long Exp (Grade grade)
        {
                long final= (long)(coin * ExpFix(grade));
                return final;
        }


    }
}
