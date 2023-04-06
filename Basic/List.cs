using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic
{ 
    public class List
    {
        public Random random = new Random();
        public List<object> content = new List<object>();
        public void Add(object o)
        {
            if (!content.Contains(o))
            {
                content.Add(o);
            }
        }
        public void Remove(object o)
        {
            content.Remove(o);
        }
        public List<T> Gets<T>() where T : class
        {
            return content.Where(c => c is T).Select(c => c as T).ToList();
        }
        public T Get<T>() where T : class
        {
            return Gets<T>().Count > 0 ? Gets<T>()[0] : null;
        }
        public T RandomGet<T>() where T : class
        {
            List<T> objs = Gets<T>();
            return objs[random.Next(0, objs.Count)];
        }
        public T RandomGet<T>(Func<T, bool> predicate) where T : class
        {
            Random random = new Random();
            List<T> objs = Gets<T>(predicate);
            return objs.Count > 0 ? objs[random.Next(0, objs.Count)] : null;
        }
        public T Get<T>(Func<T, bool> predicate) where T : class
        {
            T final = null;
            List<T> finals = Gets<T>(predicate);
            if (finals.Count > 0) final = finals[0];
            return final;
        }
        public List<T> Gets<T>(Func<T, bool> predicate) where T : class
        {
            return Gets<T>().Where(predicate).ToList();
        }
        public bool Has<T>() where T : class
        {
            return Get<T>() != null;
        }
        public bool Has<T>(Func<T, bool> predicate) where T : class
        {
            return Get<T>(predicate) != null;
        }
        public int Count<T>() where T : class
        {
            return Gets<T>().Count();
        }
        public int Count<T>(Func<T, bool> predicate) where T : class
        {
            return Gets<T>().Where(predicate).Count();
        }
    }
}
