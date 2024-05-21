using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using KPI.Parameter;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml;
using System.IO;
using KPI.Models;

namespace KPI.KeepClass
{
    public class ExcelClass
    {
        public string FileName { get; set; }
      //  private string Errorlogfile = Paths.Errorlogxlsx;
        public List<string> ErrorXlsxline = new List<string>();

        public ExcelClass()
        {
        }

        public static void AppendErrorLogXlsx(string errorcode, string functionname, string decription)  //AppendErrorLogFile
        {
            return;

            //FileInfo excelFile = new FileInfo(Paths.Errorlogxlsx);
            //if (File.Exists(Paths.Errorlogxlsx) == false)
            //{
            //    using (ExcelPackage excel = new ExcelPackage())
            //    {
            //        excel.Workbook.Worksheets.Add("sheet1");
            //        var workSheet = excel.Workbook.Worksheets["sheet1"];
            //        workSheet.Cells[1, 1].Value = "occuredDateTime";
            //        workSheet.Cells[1 ,2].Value = "userId";
            //        workSheet.Cells[1 , 3].Value = "errorCode";
            //        workSheet.Cells[1, 4].Value = "functionName";
            //        workSheet.Cells[1, 5].Value = "decription";
            //        workSheet.Cells[1, 6].Value = 2;
            //        excel.SaveAs(excelFile);
            //    }
               
            //}
            //if (User.ID != "0")
            //{
            //    using (ExcelPackage excel = new ExcelPackage(excelFile))
            //    {
            //        try
            //        {
            //            string userId = User.ID;
            //            var workSheet = excel.Workbook.Worksheets["sheet1"];
            //            string dtstr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //            int nextrow = Convert.ToInt32(workSheet.Cells[1, 6].Value);
            //            workSheet.Cells[nextrow, 1].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //            workSheet.Cells[nextrow, 2].Value = userId;
            //            workSheet.Cells[nextrow, 3].Value = errorcode;
            //            workSheet.Cells[nextrow, 4].Value = functionname;
            //            workSheet.Cells[nextrow, 5].Value = decription;
            //            workSheet.Cells[1, 6].Value = nextrow + 1;
            //            excel.SaveAs(excelFile);
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.ToString());
            //        }
            //    }
            //}
            
        }

        public void ReadErrorLogXlsx()
        {
            FileInfo excelFile = new FileInfo(Paths.Errorlogxlsx);
            if (File.Exists(Paths.Errorlogxlsx) == true)
            {
                using (ExcelPackage excel = new ExcelPackage(excelFile))
                {
                    var workSheet = excel.Workbook.Worksheets["sheet1"];
                    workSheet.Cells[1, 1].Value = "occuredDateTime";
                    workSheet.Cells[1, 2].Value = "userId";
                    workSheet.Cells[1, 3].Value = "errorCode";
                    workSheet.Cells[1, 4].Value = "functionName";
                    workSheet.Cells[1, 5].Value = "decription";
                    workSheet.Cells[1, 6].Value = 2;
                  
                }

            }





        }



    }
}
