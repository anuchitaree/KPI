using System;
using System.Collections.Generic;
using System.Data;


namespace KPI.Class
{
    public class OADisplay
    {

        public OaDisplay OABoardDisplay(string section)
        {
            OaDisplay o = new OaDisplay();
            List<string> WorkingTime = new List<string>();
            List<DateTime> startTimeMonitor = new List<DateTime>();
            List<DateTime> stopTimeMonitor = new List<DateTime>();
            DateTime registDate = OABorad.FindRegistDateFromCurrentTime(DateTime.Now);
            string DayNight = OABorad.FindDayOrNight(DateTime.Now);
            double qtytotal = 0;
            double avgct = 0;
            int workHr = 0;
            string lastPartNumber = "";
            double oATarget = 100;
            int chkOT = 0;

            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SSS("OABoardExc1", "@DateToday", registDate.ToString("yyyy-MM-dd"), "@DayNightDN", DayNight, "@SectionCode", section);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                if (sqlstatus)
                {
                    DataTable dt1 = ds.Tables[0]; // Total, CT, WorkHour, , OA
                    DataTable dt2 = ds.Tables[1];
                    if (dt1.Rows.Count > 0)
                    {
                        qtytotal = dt1.Rows[0].ItemArray[0] == null ? 0 : Convert.ToDouble(dt1.Rows[0].ItemArray[0]);
                        avgct = dt1.Rows[0].ItemArray[1] == null ? 0 : Convert.ToDouble(dt1.Rows[0].ItemArray[1]);
                        workHr = dt1.Rows[0].ItemArray[2] == null ? 0 : Convert.ToInt32(dt1.Rows[0].ItemArray[2]);
                        lastPartNumber = dt1.Rows[0].ItemArray[3] == null ? "" : Convert.ToString(dt1.Rows[0].ItemArray[3]);
                        oATarget = dt1.Rows[0].ItemArray[4] == null ? 0 : Convert.ToDouble(dt1.Rows[0].ItemArray[4]);
                    }

                    if (workHr == 10) { chkOT = 0; } // Hour = 10
                    else if (workHr == 8) { chkOT = 2; } // Hour = 8
                    else if (workHr == 0) { chkOT = 10; }  // Hour = 0


                    LoadingTimeResult takwork = new LoadingTimeResult();
                    if (dt2.Rows.Count > 0)
                    {
                        int compensate = (DayNight == "D") ? 0 : 10;
                        for (int i = 0; i < 10 - chkOT; i++)
                        {
                            WorkingTime.Add(Convert.ToString(dt2.Rows[i + compensate].ItemArray[0]));
                            startTimeMonitor.Add(Convert.ToDateTime(dt2.Rows[i + compensate].ItemArray[1].ToString()));
                            stopTimeMonitor.Add(Convert.ToDateTime(dt2.Rows[i + compensate].ItemArray[2].ToString()));
                        }

                        if ((10 - chkOT) != 0)
                        {
                            takwork = OABorad.WorkTakeTime(registDate, startTimeMonitor, stopTimeMonitor, chkOT, DayNight);
                        }
                        else
                        {
                            takwork.TakeTime = 0;
                        }
                    }
                    double qPlan = (workHr == 0) ? 0 : takwork.TakeTime / avgct;
                    string QtyPlan = qPlan.ToString("0");
                    string Diff = (qtytotal - qPlan).ToString("0");
                    double or = qtytotal / qPlan * 100;
                    string OR = qPlan == 0 ? "0" : or.ToString("0.0");
                    double OrWarning = oATarget * 0.9;
                    if (or >= oATarget)
                        o.OaStatus = Oastatus.Normal;
                    else if (or >= OrWarning && or < oATarget)
                        o.OaStatus = Oastatus.Warning;
                    else if (or < OrWarning)
                        o.OaStatus = Oastatus.Abnormal;
                    o.Status = true;
                    o.QtyPlan = QtyPlan;
                    o.QtyTotal = qtytotal.ToString();
                    o.Diff = Diff;
                    o.CTavg = avgct.ToString("0.0");
                    o.Or = OR;
                    o.OrTarget = oATarget.ToString("0.0");
                    o.PartNumber = lastPartNumber;
                    o.LineStatus = takwork.WorkStatus == true ? Lstatus.Operate : Lstatus.Break;

                }
            }
            return o;

        }


    }

    public class OaDisplay
    {
        public bool Status = false;
        public string QtyPlan { get; set; }
        public string QtyTotal { get; set; }
        public string Diff { get; set; }
        public string CTavg { get; set; }
        public string Or { get; set; }
        public string OrTarget { get; set; }
        public string PartNumber { get; set; }
        public Lstatus LineStatus { get; set; }
        public Oastatus OaStatus { get; set; }
    }

    public enum Lstatus
    {
        Operate,
        Break,
        Stop
    }

    public enum Oastatus
    {
        Normal,
        Warning,
        Abnormal
    }
}
