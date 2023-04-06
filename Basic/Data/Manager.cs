using MySql.Data.MySqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Basic.Data
{
    public enum Type
    {
        MySql,
        Excel,
    }
    public class Manager :Basic.Manager
    {
        public Basic.Text.Manager text = new Text.Manager();
        public Excel.Manager excel = new Excel.Manager();
        public void Load<TData>(Type type) where TData : Obj
        {
            switch (type)
            {
                case Type.MySql:
                    //Load<TData>(MySql.Manager.instance.Load<TData>());
                    break;
                case Type.Excel:
                    Load<TData>(excel.Load( typeof(TData).Name.ToString()));
                    break;
            }
        }
        private void Load<TData>(List<List<string>> datass) where TData : Obj
        {
            List<string> titles = datass[0];
            List<string> types = new List<string>();
            List<string> feilds = new List<string>();
            foreach (string title in titles)
            {
                string type = text.Extract(title, "[]");
                string feild = title.Replace("[" + type + "]", "");
                types.Add(type);
                feilds.Add(feild);
            }
            for (int i = 1; i < datass.Count; i++)
            {
                CreateInstance<TData>(types, feilds, datass[i]);
            }
        }

    }
}


