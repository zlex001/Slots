using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic
{
    public class Numerical
    {
        public static Numerical instance = new Numerical();
        public Dictionary<double, double> Exponential(int x, int y, int A)
        {
            Dictionary<double, double> final = new Dictionary<double, double>();
            return final;
        }
        public static Dictionary<int, double> Power(int x, long y, int k, int a)
        {
            Dictionary<int, double> final = new Dictionary<int, double>();
            double n = Math.Log(y - a, x * k);
            for (int i = 0; i <= x; i++)
            {
                final[i] = Math.Pow(i * k, n) + a;
            }
            return final;
        }
        public long  Rasure(long origal,int count)
        {
            long fix = 1;
            for(int i = 0; i < origal.ToString().Length- count; i++)
            {
                fix *= 10;
            }
            return (int)(origal/fix)*fix;
        }
        public static string Round(string orignal, int index,int num)
        {
            for(int i = index; i < orignal.Length; i++)
            {
                if(orignal.Substring(i, 1) != "0")
                {
                    string final = orignal.Substring(0, index);
                    for (int j = 0; j < orignal.Length - 3; j++)
                    {
                        final += "0";
                    }
                    return final;
                }
            }
            return orignal;
             

        }
        public static int Zone(List<int> zones,int goal)
        {
            List<int> ds = zones.Select(z => Math.Abs(goal - z)).ToList();
            int min = ds.Min();
            int id= ds.IndexOf(min);
            return goal - zones[id] >= 0 ? id : id - 1;

        }
        public List<float> FromWeight(List<float> weights)
        {
            List<float> finals = new List<float>();
            for(int i = 0; i < weights.Count; i++)
            {
                finals.Add(weights[i] / weights.Sum());
            }
            return finals;
        }
        public List<double> ArithmeticProgression(double start,double end,int count)
        {
            List<double> finals = new List<double>();
            double distance = end - start;
            double per = distance /( count-1);
            for(int i = 0; i < count; i++)
            {
                double increment = per * i;
                finals.Add(start + increment);
            }
            return finals;
        }
        public List<double> GeometricProgression(double start, double end, int count)
        {
            List<double> finals = new List<double>();
            double distance = end / start;
            double per = Math.Pow( distance ,(double)1/(count-1));
            for (int i = 0; i < count; i++)
            {
                double increment = Math.Pow(per,i);
                finals.Add(start * increment);
            }
            return finals;
        }
        public int Approximation(int num, List<int> nums)
        {
            List<int> distances = new List<int>();
            for(int i =0;i<nums.Count;i++)
            {
                distances.Add(Math.Abs(nums[i] - num));
            }
            return nums[distances.IndexOf( distances.Min())];
        }

    }
}
