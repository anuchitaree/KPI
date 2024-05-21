using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace KPI.Class
{
    public static class ProductionVolume
    {






        //public FrameByHourMember FrameByHour(string section)
        //{
        //    FrameByHourMember result = new FrameByHourMember();
        //    result.HourList.Add("");
        //    result.Quqry = "select * where SectionCode = '0000-000'";
        //    result.Status = false;
        //    DateTime dt = DateTime.Now;
        //    int year = dt.Year;
        //    int month = dt.Month;
        //    int day = dt.Day;
        //    DateTime dr = new DateTime(1900, 1, 1);
        //    DateTime dc = new DateTime(year, month, day);
        //    double plus = (dc - dr).TotalDays;

        //    SqlClass sql = new SqlClass();
        //    bool sqlstatus = sql.SSQL_SSS("LoadProd_TimeBreakTable", "@DateToday", dt.ToString("yyyy-MM-dd"),
        //        "@DayNightDN", "B", "@SectionCode", section);
        //    var query = new StringBuilder();
        //    if (sqlstatus)
        //    {
        //        DataSet ds = sql.Dataset;
        //        DataTable dt1 = ds.Tables[1];
        //        if (dt1.Rows.Count > 0)
        //        {
        //            result.HourList.Clear();
        //            for (int i = 0; i < dt1.Rows.Count; i++)
        //            {
        //                string hd = dt1.Rows[i].ItemArray[0].ToString();
        //                result.HourList.Add(hd);
        //                DateTime start = Convert.ToDateTime(dt1.Rows[i].ItemArray[1]);
        //                start = start.AddDays(plus);
        //                DateTime stop = Convert.ToDateTime(dt1.Rows[i].ItemArray[2]);
        //                stop = stop.AddDays(plus);
        //                query.Append("SELECT PartNumber, count(PartNumber) FROM [dbo].[Prod_RecordTable] ");
        //                query.AppendFormat(" where registDateTime between '{0}' AND '{1}' and SectionCode = '{2}' group by PartNumber \n",
        //                    start, stop, section);

        //            }
        //            result.Status = true;
        //            result.Quqry = query.ToString();
        //        }
        //    }
        //    return result;
        //}

        //public void TitleAndVolume(string query)
        //{
        //    TitleAndVolumeMember result = new TitleAndVolumeMember();
        //    //result.Amount = { { "",""} };
        //        SqlClass sql = new SqlClass();
        //    bool sqlstatus = sql.Prod_VolumReadByPartNumberSQL(query);
        //    if (sqlstatus)
        //    {
        //        DataSet ds2 = sql.Dataset;
        //        List<string> pn = new List<string>();
        //        string[,] amount = new string[21, 500];
        //        if (ds2.Tables.Count > 0)
        //        {
        //            for (int i = 0; i < ds2.Tables.Count; i++)
        //            {
        //                if (ds2.Tables[i].Rows.Count > 0)
        //                {
        //                    for (int j = 0; j < ds2.Tables[i].Rows.Count; j++)
        //                    {
        //                        if (ds2.Tables[i].Rows[j].ItemArray[0] != null)
        //                        {
        //                            string a = ds2.Tables[i].Rows[j].ItemArray[0].ToString();
        //                            int index = pn.IndexOf(a);
        //                            if (index == -1)
        //                            {
        //                                pn.Add(a);
        //                            }
        //                            index = pn.IndexOf(a);
        //                            string qty = ds2.Tables[i].Rows[j].ItemArray[1].ToString();
        //                            amount[i, index] = qty;
        //                        }
        //                    }
        //                }
        //            }

        //        }
        //    }

        //}




        //public class FrameByHourMember
        //{
        //    public List<string> HourList { get; set; }
        //    public string Quqry { get; set; }
        //    public bool Status { get; set; }
        //}

        //public class TitleAndVolumeMember
        //{
        //    public List<string> Title { get; set; }
        //    public string[,] Amount { get; set; }
        //}
    }

}