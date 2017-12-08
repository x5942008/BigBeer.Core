using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BigBeer.Core.Excel
{
    /// <summary>
    /// 模拟使用
    /// </summary>
    //public class EXSample
    //{
    //    IList<object> data = new List<object>();
    //    public void Sample()
    //    {
    //        var result = from x in linqdata.ToList()
    //                     select new
    //                     {
    //                         命名 = "数据"
    //                     };
    //        var temp = result.ToList();
    //        for (int i = 0; i < temp.count(); i++)
    //        {
    //            data.Add(temp[i]);
    //        }
    //        Excel.SaveExcel(temp,path);
    //    }
    //}

    public class Excel
    {
        /// <summary>
        /// 只针对2003以上版本
        /// </summary>
        /// <param name="data">需要存储的数据</param>
        /// <param name="path">存储地址</param>
        /// <param name="filename">文件名即可，后缀.xlsx</param>
        /// <param name="worksheetname">EXCEL文档下方的命名</param>
        public static void SaveExcel(IList<object> data, string path = null, string filename = null, string worksheetname = null)
        {
            int count = data[0].ToString().Split(',').Count();
            string sWebRootFolder = "D://Excel/";
            if (!string.IsNullOrEmpty(path)) sWebRootFolder = path;
            if (!Directory.Exists(sWebRootFolder))
            {
                Directory.CreateDirectory(sWebRootFolder);
            }
            string sFileName = $"{Guid.NewGuid()}.xlsx";
            if (!string.IsNullOrEmpty(filename)) sFileName = $"{filename}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            string worksheetName = "sheet";
            if (!string.IsNullOrEmpty(worksheetname)) worksheetName = worksheetname;

            using (ExcelPackage package = new ExcelPackage(file))
            {
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetName);
                //添加头
                for (int i = 0; i < count; i++)
                {
                    var key = data.ToList()[i].ToString().Split(',')[i].Split('=')[0].Replace("{", "").Replace("}", "");
                    worksheet.Cells[1, i + 1].Value = key;
                }
                //添加值
                for (int i = 0; i < data.Count; i++)
                {
                    var list = data[i];
                    var shu = i + 2;
                    for (int t = 0; t < count; t++)
                    {
                        var c = Convert.ToChar('A' + t);
                        var value = list.ToString().Split(',')[t].Split('=')[1].Replace("{", "").Replace("}", "");
                        worksheet.Cells[$"{c}{shu}"].Value = value;
                    }
                }
                package.Save();
                GC.Collect();
            }
        }
    }
}
