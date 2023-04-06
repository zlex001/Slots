using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Basic.Text
{
    public class Manager
    {

        public Dictionary<string, List<string>> Information(List<string> informations)
        {
            Dictionary<string, List<string>> final = new Dictionary<string, List<string>>();
            foreach (string information in informations)
            {
                int index = information.IndexOf("|");
                string key = information.Substring(0, index);
                string value = information.Substring(index + 1, information.Length - (index + 1));
                if (final.ContainsKey(key))
                {
                    final[key].Add(value);
                }
                else
                {
                    final[key] = new List<string> { value };
                }
            }
            return final;
        }
        public bool Safe(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\【|\】|\}|\{|%|@|\*|!|\']");
        }
        public string SignNum(int num)
        {
            if (num >= 0)
            {
                return "+" + num;
            }
            else
            {
                return num.ToString();
            }
        }
        public string Extract(string text, string sign)
        {
            string final = "";
            switch (sign)
            {
                case "()":
                    final = Regex.Replace(text, @"(.*\()(.*)(\).*)", "$2");
                    break;
                case "[]":
                    Regex regex = new Regex(@"(?i)(?<=\[)(.*)(?=\])");
                    final = regex.Match(text).Value;
                    break;
                case "{}":
                    final = Regex.Match(text, @"\{(.*)\}", RegexOptions.Singleline).Groups[1].Value;
                    break;
            }
            return final;

        }
        public List<List<string>> Vertical(List<string> texts)
        {
            List<List<string>> finalss = new List<List<string>>();
            foreach (string text in texts)
            {
                finalss.Add(new List<string> { text });
            }
            return finalss;
        }
    }

}
