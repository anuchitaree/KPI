using System;
using System.Collections.Generic;
using System.Linq;
using KPI.Models;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using KPI.Parameter;
using System.Windows.Forms;
using System.Data;


namespace KPI.Class
{
    public class LossAnalysis
    {
        public DateTime Startdate { get; set; } // working day
        public int Amount { get; set; }   // time at now
        public string SectionCode { get; set; }
        public string DivPlant { get; set; }

        private readonly bool[] masterLoss = new bool[87000];

        public LossAnalysis()
        {

        }

        public void LossAnalysisExc()
        {

            string dat = Startdate.ToString("yyyy-MM-dd");
            string sectionDivPlant = string.Format("{0}{1}{2}", User.SectionCode,User.Division, User.Plant);
            SqlClass sql = new SqlClass();
            // bool sqlstatus = sql.LoadLossRecordSSQL("loss_tabeldownload", "@StringDate", dat, "@LoopRun", Amount, "@sectiondivplant", sectionDivPlant);
            bool sqlstatus = sql.SSQL_SIS("loss_tabeldownload", "@StringDate", dat, "@LoopRun", Amount, "@sectiondivplant", sectionDivPlant);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dtm = ds.Tables[0];
                int test = 0;
                if (dtm.Rows.Count > 0) //=== Master Table ===//
                {
                    DateTime StartTime = new DateTime(1900, 1, 1, 7, 30, 00);
                    for (int i = 0; i < dtm.Rows.Count; i++)
                    {

                        DateTime startTimeMonitor = Convert.ToDateTime(dtm.Rows[i].ItemArray[2]);
                        DateTime stopTimeMonitor = Convert.ToDateTime(dtm.Rows[i].ItemArray[3]);
                        int d11 = Convert.ToInt32((startTimeMonitor - StartTime).TotalSeconds);
                        int d12 = Convert.ToInt32((stopTimeMonitor - StartTime).TotalSeconds);
                        if (d11 <= d12)
                        {
                            for (int n = d11; n < d12; n++)
                            {
                                masterLoss[n] = true;
                                test += 1;
                            }
                        }
                    }
                    //TimeLineMasterLossAnalysisGraph(masterLoss, fixYaxis);
                }
                Console.WriteLine(test);

                ///  Main loss Table ///
                if (ds.Tables.Count > 1)
                {
                    Dictionary<string, string> memoEachLoss = new Dictionary<string, string>();
                    Dictionary<string, string> memoDayLoss = new Dictionary<string, string>();
                    for (int i = 1; i < ds.Tables.Count; i++) // Loss Table by day
                    {
                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            bool[] LossInOneDay = new bool[87000];
                            string run;
                            DateTime datet = DateTime.Now;
                            for (int j = 0; j < ds.Tables[i].Rows.Count; j++)  // data in 1 day
                            {
                                run = Conv.DataString(ds.Tables[i].Rows[j].ItemArray[0]);
                                datet = Convert.ToDateTime(ds.Tables[i].Rows[j].ItemArray[1]);

                                int yy = datet.Year;
                                int mm = datet.Month;
                                int dd = datet.Day;
                                DateTime StartTime = new DateTime(yy, mm, dd, 7, 30, 00);
                                DateTime dt1 = Convert.ToDateTime(ds.Tables[i].Rows[j].ItemArray[2]);
                                DateTime dt2 = Convert.ToDateTime(ds.Tables[i].Rows[j].ItemArray[3]);
                                int d11 = Convert.ToInt32((dt1 - StartTime).TotalSeconds);
                                int d12 = Convert.ToInt32((dt2 - StartTime).TotalSeconds);
                                if (d12 > 86400) d12 = 86400;
                                bool[] temploss = new bool[87000];
                                for (int n = d11; n < d12; n++)
                                {
                                    LossInOneDay[n] = true;
                                    temploss[n] = true;
                                }
                                //TimeLineLossInOneDayAnalysisGraph(LossInOneDay, fixYaxis+20);
                                //TimeLineSubLossAnalysisGraph(temploss, fixYaxis+20 + j * 20);

                                int calculateEachLoss = 0;
                                for (int ki = 0; ki < 87000; ki++)
                                {
                                    if (temploss[ki] == true && masterLoss[ki] == true)
                                    {
                                        calculateEachLoss += 1;
                                    }

                                }
                                memoEachLoss.Add(run, calculateEachLoss.ToString());


                            }
                            int calculateLossPerDay = 0;
                            for (int ki = 0; ki < 87000; ki++)
                            {
                                if (LossInOneDay[ki] == true && masterLoss[ki] == true)
                                {
                                    calculateLossPerDay += 1;
                                }
                            }

                            string datestr = datet.ToString("yyyy-MM-dd");
                            double netLoss = Convert.ToDouble(calculateLossPerDay);
                            double netlossHour = netLoss / 3600;
                            double netHour = Math.Round(netlossHour, 2, MidpointRounding.AwayFromZero);
                            memoDayLoss.Add(datestr, netHour.ToString());  // unit second

                        }

                        // after analysis

                    }


                    int numberOfData = memoDayLoss.Count;
                    if (numberOfData > 0)
                    {
                        DateTime dateend = Convert.ToDateTime(dat);
                        dateend = dateend.AddDays(Amount - 1);
                        string dateendstr = dateend.ToString("yyyy-MM-dd");
                        var q = new StringBuilder();
                        q.AppendFormat("Delete [Production].[dbo].[Loss_SummaryTable] Where SectionCode ='{0}' and RegistDate Between '{1}' and '{2}' \n", SectionCode, dat, dateendstr);
                        q.AppendFormat(" Insert into [Production].[dbo].[Loss_SummaryTable] ([sectionCode],[RegistDate],[summaryHour]) values \n");
                        //string cmd11 = "";
                        for (int i = 0; i < memoDayLoss.Count; i++)
                        {
                            q.AppendFormat("('{0}','{1}',{2}) \n", SectionCode, memoDayLoss.Keys.ElementAt(i), memoDayLoss[memoDayLoss.Keys.ElementAt(i)]);
                            if (i < memoDayLoss.Count - 1) q.Append(",");

                        }
                        //var a = q.ToString();
                        SqlClass ss = new SqlClass();
                        ss.SqlWrite(q.ToString());
                    }
                }

            }

        }

        //private void TimeLineMasterLossAnalysisGraph(bool[] loss, int YAxis)
        //{
        //    //System.Drawing.Graphics graphicsObj;
        //    //graphicsObj = this.CreateGraphics();
        //    //Pen myPen = new Pen(System.Drawing.Color.Blue, 10);
        //    //int y = YAxis; // 500;
        //    //int next = fixXaxis;
        //    //for (int i = 0; i < 86400; i = i + 60)
        //    //{
        //    //    if (loss[i] == true)
        //    //    {
        //    //        graphicsObj.DrawLine(myPen, next, y, i / 60, y);
        //    //        next = i / 60;
        //    //    }
        //    //    else
        //    //    {
        //    //        next = i / 60;
        //    //    }

        //    //}
        //}

        //private void TimeLineLossInOneDayAnalysisGraph(bool[] loss, int YAxis)
        //{
        //    //System.Drawing.Graphics graphicsObj;
        //    //graphicsObj = this.CreateGraphics();
        //    //Pen myPen = new Pen(System.Drawing.Color.Red, 10);
        //    //int y = YAxis; // 500;
        //    //int next = fixXaxis;
        //    //for (int i = 0; i < 86400; i = i + 60)
        //    //{
        //    //    if (loss[i] == true)
        //    //    {
        //    //        graphicsObj.DrawLine(myPen, next, y, i / 60, y);
        //    //        next = i / 60;
        //    //    }
        //    //    else
        //    //    {
        //    //        next = i / 60;
        //    //    }

        //    //}
        //}

        //private void TimeLineSubLossAnalysisGraph(bool[] loss, int YAxis)
        //{
        //    //System.Drawing.Graphics graphicsObj;
        //    //graphicsObj = this.CreateGraphics();
        //    //Pen myPen = new Pen(System.Drawing.Color.FromArgb(85, 0, 0), 5);
        //    //int y = YAxis; // 500;
        //    //int next = fixXaxis;
        //    //for (int i = 0; i < 86400; i = i + 60)
        //    //{
        //    //    if (loss[i] == true)
        //    //    {
        //    //        graphicsObj.DrawLine(myPen, next, y, i / 60, y);
        //    //        next = i / 60;
        //    //    }
        //    //    else
        //    //    {
        //    //        next = i / 60;
        //    //    }

        //    //}
        //}







    }
}
