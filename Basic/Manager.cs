using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic
{
    public class Manager
    {
        public List list = new List();
        public virtual T CreateInstance<T>(params object[] args) where T : Obj
        {
            T obj = Activator.CreateInstance<T>();
            obj.Init(args);
            list.Add(obj);
            return obj;
        }
        public virtual void Add(object o)
        {
            list.Add(o);
        }
        public virtual void Remove(object o)
        {
            list.Remove(o);
        }
    }
}
