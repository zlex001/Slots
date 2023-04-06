using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Excel
{
    public enum PathType
    {
        Input,
        Output,
    }
    public class Manager
    {
        public Manager()
        {
            file.root += "Excel";
        }
        private readonly File.Manager file = new File.Manager();
        public delegate void Output();
        public Output output;
        private string Path(PathType type, string path)
        {
            return string.Format(@"{0}/{1}/{2}.xlsx", file.root,type.ToString(), path);
        }
        public List<List<string>>Load(ISheet sheet)
        {
            List<List<string>> finals = new List<List<string>>();
            List<IRow> rows = new List<IRow>();
            for (int r = 0; r < sheet.LastRowNum + 1; r++)
            {
                rows.Add(sheet.GetRow(r));
            }
            foreach (IRow row in rows)
            {
                List<string> final = new List<string>();
                if (row != null)
                {
                    for (int c = 0; c < rows.Select(r=>r.LastCellNum).Max(); c++)
                    {
                        string cell = Convert.ToString(row.GetCell(c));
                        final.Add(cell == "" ? null : cell);
                    }
                }
                finals.Add(final);
            }
            return finals;
        }
        public List<List<string>> Load(string xlsName,int sheetId)
        {
             FileStream fileStream = System.IO.File.OpenRead(Path(PathType.Input,xlsName));
            IWorkbook sheets = new XSSFWorkbook(fileStream);
            ISheet sheet = sheets.GetSheetAt(sheetId);
            return Load(sheet);
        }
        public List<List<string>> Load(string xlsName)
        {
            return Load(xlsName,0);
        }
        public void Save(List<List<string>> datas, string excelName,int sheetId  )
        {
            string path = Path(PathType.Output, excelName);
            IWorkbook book = WorkbookFactory.Create(path);
            ISheet sheet = book.GetSheetAt(sheetId);
            for (int r = 0; r < datas.Count; r++)
            {
                IRow row = sheet.CreateRow(r);
                for (int c = 0; c < datas[r].Count; c++)
                {
                    row.CreateCell(c).SetCellValue(datas[r][c]);
                }
            }
            FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            book.Write(stream);
            stream.Close();
        }
        public void Save(List<List<string>> datas, string excelName )
        {
            Save(datas, excelName, 0);
        }
    }
}
