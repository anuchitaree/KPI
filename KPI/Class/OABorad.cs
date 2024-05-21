using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace KPI.Class
{
    public static class OABorad
    {
        public static OA CalculateOA(DateTime today, string sectionDivPlant)
        {
            OA oa = new OA();
            double qtytotal = 0;
            double avgct = 0;
            double orTagrget = 0;
            int workHr = 0;
            int chkOT = 0;
            int hourno=0;
            List<string> WorkingTime = new List<string>();
            List<DateTime> startTimeMonitor = new List<DateTime>();
            List<DateTime> stopTimeMonitor = new List<DateTime>();
            DateTime registDate = FindRegistDateFromCurrentTime(today);
            string DayNight = FindDayOrNight(today);
            string partnumber = string.Empty;
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SSS("LoadProd_TimeBreakTable", "@DateToday", registDate.ToString("yyyy-MM-dd"), "@DayNightDN", DayNight, "@sectiondivplant", sectionDivPlant);

            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                if (sqlstatus)
                {
                    DataTable dt1 = ds.Tables[0]; // Total, CT, WorkHour, , OA
                    DataTable dt2 = ds.Tables[1];
                    if (dt1.Rows.Count > 0)
                    {
                        try
                        {
                            qtytotal = dt1.Rows[0].ItemArray[0] == null ? 0 : Convert.ToDouble(dt1.Rows[0].ItemArray[0]);
                            avgct = dt1.Rows[0].ItemArray[1] == null ? 0 : Convert.ToDouble(dt1.Rows[0].ItemArray[1]);
                            workHr = dt1.Rows[0].ItemArray[2] == null ? 0 : Convert.ToInt32(dt1.Rows[0].ItemArray[2]);
                            orTagrget = dt1.Rows[0].ItemArray[4] == null ? 0 : Convert.ToDouble(dt1.Rows[0].ItemArray[4]);
                            partnumber = dt1.Rows[0].ItemArray[5].ToString();
                        }


                        catch { }
                    }

                    if (workHr == 10) { chkOT = 0; } // Hour = 10
                    else if (workHr == 8) { chkOT = 2; } // Hour = 8
                    else if (workHr == 0) { chkOT = 10; }  // Hour = 0

                    if (dt2.Rows.Count > 0)
                    {
                        int compensate = (DayNight == "D") ? 0 : 10;
                        for (int i = 0; i < 10 - chkOT; i++)
                        {
                            WorkingTime.Add(Convert.ToString(dt2.Rows[i + compensate].ItemArray[0]));
                            startTimeMonitor.Add(Convert.ToDateTime(dt2.Rows[i + compensate].ItemArray[1].ToString()));
                            stopTimeMonitor.Add(Convert.ToDateTime(dt2.Rows[i + compensate].ItemArray[2].ToString()));
                        }
                    }

                  
                    if ((10 - chkOT) != 0)
                    {
                        oa.TimeActual = LoadingTimeCalculation(registDate, startTimeMonitor, stopTimeMonitor, chkOT, DayNight);
                        hourno = FindWorkingHourNumber(startTimeMonitor,  chkOT);
                    }
                    else
                    {
                        oa.TimeActual = 0;
                    }
                    double qPlan = (workHr == 0) ? 0 : oa.TimeActual / avgct;
                    oa.QtyPlan = qPlan.ToString("0");
                    oa.QtyTotal = qtytotal.ToString("0");
                    oa.Diff = (qtytotal - qPlan).ToString("0");
                    oa.AvgCT = avgct.ToString("0.0"); ;
                    oa.OR = (qtytotal / qPlan * 100);
                    oa.ORTarget = orTagrget;
                    oa.status = true;
                    oa.PartNumber = partnumber;
                    oa.WorkingInHour = hourno;

                    //SqlClass sql1 = new SqlClass();
                    //sql1.PPAS_UpdateOABoradSQL(oa.QtyPlan, oa.QtyTotal, oa.Diff, oa.OR.ToString("0.0"), partnumber);

                }
            }
            return oa;
        }


        private static int FindWorkingHourNumber(List<DateTime> startTimeMonitor, int chkOT)
        {
            int hourNo = 0;
            for (int i = 0; i < 10 - chkOT; i++)
            {
                double dtest = (DateTime.Now - startTimeMonitor[i]).TotalSeconds;
                if (dtest > 0)
                    hourNo = i + 1;
            }
            return hourNo;
        }


    


        public async static void ServiceOABoard(List<string> SectionCode)
        {
            foreach (var section in SectionCode)
            {
                await Task.Run(() => PPASMethod(section));
            }

        }

        static void PPASMethod(string section)
        {
            DateTime dt = DateTime.Now;
            dt = KPI.Class.RegistDateTime.FindRegistDateFromCurrentTime(dt);
            SqlClass sql = new SqlClass();
            sql.SSQL_SS("PPAS1", "@RegistDateTime", dt.ToString("yyyy-MM-dd"), "@section", section);
        }


        private static int FindWorkingHourNumber(List<DateTime> startTimeMonitor, List<DateTime> stopTimeMonitor, int chkOT)
        {
            int hourNo = 0;
            for (int i = 0; i < 10 - chkOT; i++)
            {
                double dtest = (DateTime.Now - startTimeMonitor[i]).TotalSeconds;
                if (dtest > 0)
                    hourNo = i + 1;
            }
            return hourNo;
        }



        private static double LoadingTimeCalculation(DateTime registdate, List<DateTime> startTimeMonitor, List<DateTime> stopTimeMonitor, int ChkOT, string DayNight)
        {
            bool[] MasterPattern = new bool[86400];
            bool[] TimeUsed = new bool[86400];
            int yy = registdate.Year; int mm = registdate.Month; int dd = registdate.Day;
            DateTime StartTime = (DayNight == "D") ? new DateTime(yy, mm, dd, 07, 30, 00) : new DateTime(yy, mm, dd, 19, 30, 00);

            for (int i = 0; i < 10 - ChkOT; i++)
            {
                int d11 = Convert.ToInt32((startTimeMonitor[i] - StartTime).TotalSeconds);// 0------.-------------------86400
                int d12 = Convert.ToInt32((stopTimeMonitor[i] - StartTime).TotalSeconds);// 0--------------------.-----86400
                d11 = (d11 > 86400) ? 86400 : d11;
                d12 = (d12 > 86400) ? 86400 : d12;
                if (d11 < d12)
                {
                    for (int n = d11; n < d12; n++)
                    {
                        MasterPattern[n] = true;
                    }
                }

            }
            int difftime = Convert.ToInt32((DateTime.Now - StartTime).TotalSeconds);
            for (int n = 0; n < difftime; n++)
            {
                TimeUsed[n] = true;
            }

            int usingtime = 0;
            for (int ki = 0; ki < 86400; ki++)
            {
                if (TimeUsed[ki] == true && MasterPattern[ki] == true)   //TimeUsed[ki] == true
                {
                    usingtime += 1;
                }
            }
            return Convert.ToDouble(usingtime);
        }



        public static LoadingTimeResult WorkTakeTime(DateTime registdate, List<DateTime> startTimeMonitor, List<DateTime> stopTimeMonitor, int ChkOT, string DayNight)
        {
            var result = new LoadingTimeResult();
            DateTime timenow = DateTime.Now;
            bool[] MasterPattern = new bool[86400];
            bool[] TimeUsed = new bool[86400];
            int yy = registdate.Year; int mm = registdate.Month; int dd = registdate.Day;
            DateTime StartTime = (DayNight == "D") ? new DateTime(yy, mm, dd, 07, 30, 00) : new DateTime(yy, mm, dd, 19, 30, 00);

            for (int i = 0; i < 10 - ChkOT; i++)
            {
                int d11 = Convert.ToInt32((startTimeMonitor[i] - StartTime).TotalSeconds);// 0------.-------------------86400
                int d12 = Convert.ToInt32((stopTimeMonitor[i] - StartTime).TotalSeconds);// 0--------------------.-----86400
                d11 = (d11 > 86400) ? 86400 : d11;
                d12 = (d12 > 86400) ? 86400 : d12;
                if (d11 < d12)
                {
                    for (int n = d11; n < d12; n++)
                    {
                        MasterPattern[n] = true;
                    }
                }
                if (startTimeMonitor[i] < timenow && stopTimeMonitor[i] > timenow)
                    result.WorkStatus = true;

            }
            int difftime = Convert.ToInt32((DateTime.Now - StartTime).TotalSeconds);
            for (int n = 0; n < difftime; n++)
            {
                TimeUsed[n] = true;
            }

            int usingtime = 0;
            for (int ki = 0; ki < 86400; ki++)
            {
                if (TimeUsed[ki] == true && MasterPattern[ki] == true)   //TimeUsed[ki] == true
                {
                    usingtime += 1;
                }
            }
            result.TakeTime = Convert.ToDouble(usingtime);
            return result;
        }




        public static DateTime FindRegistDateFromCurrentTime(DateTime startnow)
        {
            int yy = startnow.Year;
            int mm = startnow.Month;
            int dd = startnow.Day;

            DateTime LimitTime = new DateTime(yy, mm, dd, 07, 30, 00);
            int timecompare = Convert.ToInt32((LimitTime - startnow).TotalSeconds);
            return (timecompare > 0) ? startnow.AddDays(-1) : startnow;
        }

        public static string FindDayOrNight(DateTime startnow)
        {
            // เวลา ณ ขณะนั้น เป็น เดย์ หรือ ไนท์
            int yy = startnow.Year;
            int mm = startnow.Month;
            int dd = startnow.Day;
            DateTime daystart = new DateTime(yy, mm, dd, 0, 0, 0);
            DateTime dayshift = new DateTime(yy, mm, dd, 07, 30, 00);
            DateTime nightshift = new DateTime(yy, mm, dd, 19, 30, 00);
            int t1 = Convert.ToInt32((dayshift - daystart).TotalSeconds);
            int t2 = Convert.ToInt32((nightshift - daystart).TotalSeconds);
            int tnow = Convert.ToInt32((startnow - daystart).TotalSeconds);
            return (tnow >= t1 && tnow <= t2) ? "D" : "N";
        }

    }


   

    public class OA
    {
        public bool status = false;
        public double TimeActual = 0;

        public string QtyPlan = string.Empty;
        public string QtyTotal = string.Empty;
        public string Diff = string.Empty;
        public string AvgCT = string.Empty;
        public double OR = 0;
        public double ORTarget = 0;
        public string PartNumber { get; set; }
        public int WorkingInHour { get; set; } = 0;
    }

    public class LoadingTimeResult
    {
        public double TakeTime { get; set; }
        public bool WorkStatus { get; set; }
    }
}
