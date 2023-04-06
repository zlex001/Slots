using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot
{
    public class Vip
    {
        public Vip(int playerLevel)
        {
            for (int i = 1; i < playerLevel; i++)
            {
                exp += Level * 2;
            }
        }
        public int id;
        public int purchase;
        public float CoinFix
        {
            get 
            {
                List<float> finals = new List<float> { 0f, 1f,1.5f,2.5f,4f,7f,10f, 15f, 30f };
                return finals[Level];
            }
        }
        public  int gainFix;
        public int exp = 1;
        public int Level => Basic.Numerical.Zone(new List<int> { 0, 150, 4100, 31000, 260000, 2100000, 10000000 }, exp)+1+purchase;

    }
}
