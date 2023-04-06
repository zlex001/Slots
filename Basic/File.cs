using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.File
{
    public enum Type
    {
        Config
    }
    public class Obj
    {
        public Obj(FileInfo fileInfo)
        {
            info = fileInfo;
            StreamReader streamReader = new StreamReader(fileInfo.FullName, Encoding.Default);
            string line = streamReader.ReadLine();
            string temLine = "";
            while (line != null)
            {
                temLine += line;
                if (line.Contains(@";")/*||line.Contains("\",") */ )
                {
                    texts.Add(temLine);
                    temLine = "";
                }
                line = streamReader.ReadLine();
            }
        }
        public FileInfo info;
        public List<string> texts = new List<string>();
    }
    public class Manager
    {
        public string root = Environment.CurrentDirectory.Replace(@"bin\Debug", "");
        public Dictionary<Type, string> paths = new Dictionary<Type, string>();
        public static Manager instance = new Manager();
        public List objs = new List();
        public void  Load(string path)
        {
            List<Obj> finals = new List<Obj>();
            DirectoryInfo theFolder = new DirectoryInfo(path);
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();//获取所在目录的文件夹
            FileInfo[] file = theFolder.GetFiles();//获取所在目录的文件
            //遍历文件夹
            foreach (DirectoryInfo directoryInfo in dirInfo)
            {
                Load(directoryInfo.FullName);
            }
            //遍历文件
            foreach (FileInfo fileInfo in file)
            {
                objs.Add(new Obj(fileInfo));
            }
        }
    }
}
