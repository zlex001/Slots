using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot.Data
{
    public class Grade : Basic.Data.Obj
    {
        public int level;
        public long maxBet;
        public long maxHighBet;
        public float orignalPurchasePower;
    }
    public class Bet : Basic.Data.Obj
    {
        public long coin;
    }
    public class Tounament : Basic.Data.Obj
    {
        public int bet;
        public int level;
    }
    public class TounamentBet : Basic.Data.Obj
    {
        public int id; 
        public int num;
    }
    public class TounamentLevel : Basic.Data.Obj
    {
        public int id;
        public int num;
    }
}
