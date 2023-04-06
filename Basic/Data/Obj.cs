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
    public class Obj : Basic.Obj
    {
        public string id;
        public string name;
        public string type;
        public override void Init(params object[] args)
        {
            List<string> types = (List<string>)args[0];
            List<string> feilds = (List<string>)args[1];
            List<string> datas = (List<string>)args[2];
            for (int i = 0; i < feilds.Count; i++)
            {
                if (GetType().GetField(feilds[i]) != null)
                {
                    if (datas[i] == null)
                    {
                        GetType().GetField(feilds[i]).SetValue(this, default);

                    }
                    else
                    {
                        if (types[i].Contains("list"))
                        {
                            string objType = types[i].Split('_')[1];
                            switch (objType)
                            {
                                case "act":
                                    //GetType().GetField(feilds[i]).SetValue(this, Basic.Json.Manager.instance.javaScriptSerializer.Deserialize<List<Act>>(datas[i]));
                                    break;
                            }
                        }
                        else
                        {
                            switch (types[i])
                            {
                                case "string":
                                    GetType().GetField(feilds[i]).SetValue(this, datas[i]);
                                    break;
                                case "int":
                                    GetType().GetField(feilds[i]).SetValue(this, datas[i] == null ? 0 : Convert.ToInt32(datas[i]));
                                    break;
                                case "strings":
                                    GetType().GetField(feilds[i]).SetValue(this, datas[i] == null ? new List<string>() : datas[i].Split('|').ToList());
                                    break;
                                case "longs":
                                    GetType().GetField(feilds[i]).SetValue(this, datas[i] == null ? new List<long>() : datas[i].Split('|').Select(d => Convert.ToInt64(d)).ToList());
                                    break;
                                case "long":
                                    if (datas[i].Contains("E"))
                                    {
                                        GetType().GetField(feilds[i]).SetValue(this, Decimal.Parse(datas[i], System.Globalization.NumberStyles.Float));
                                    }
                                    else
                                    {
                                        GetType().GetField(feilds[i]).SetValue(this, datas[i] == null ? 0 : Convert.ToInt64(datas[i]));
                                    }
                                    break;
                                case "double":
                                    GetType().GetField(feilds[i]).SetValue(this, datas[i] == null ? 0 : Convert.ToDouble(datas[i]));
                                    break;
                                case "float":
                                    GetType().GetField(feilds[i]).SetValue(this, datas[i] == null ? 0 : Convert.ToSingle(datas[i]));
                                    break;
                                case "floats":
                                    GetType().GetField(feilds[i]).SetValue(this, datas[i] == null ? new List<float>() : datas[i].Split('|').Select(d => Convert.ToSingle(d)).ToList());
                                    break;
                                case "ints":
                                    GetType().GetField(feilds[i]).SetValue(this, datas[i] == null ? new int[] { } : datas[i].Split('|').Select(d => Convert.ToInt32(d)).ToArray());
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

}


