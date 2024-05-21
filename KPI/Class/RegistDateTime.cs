using System;
using System.Collections.Generic;

namespace KPI.Class
{
    public static class RegistDateTime
    {
        public static string OutDate(DateTime lossOccureddate)
        {
            int yy = lossOccureddate.Year;
            int mm = lossOccureddate.Month;
            int dd = lossOccureddate.Day;

            DateTime dta = new DateTime(yy, mm, dd, 07, 30, 00);
            DateTime dtb = dta.AddDays(1);

            double diffa = (lossOccureddate - dta).TotalSeconds;
            double diffb = (dtb - lossOccureddate).TotalSeconds;
            DateTime dtr;
            if (diffa < 0)
            {
                dtr = lossOccureddate.AddDays(-1);
            }
            else if (diffb < 0)
            {
                dtr = lossOccureddate.AddDays(-1);
            }
            else
            {
                dtr = lossOccureddate;
            }
            return dtr.ToString("yyyy-MM-dd");
        }

        public static DateTime FindRegistDateFromCurrentTime(DateTime startnow)
        { // เวลา ณ ขณะนั้น เป็น วันการทำงานของวันนี้ หรือเมื่อวาน
            int yy = startnow.Year;
            int mm = startnow.Month;
            int dd = startnow.Day;

            DateTime nowData = new DateTime(yy, mm, dd);

            DateTime LimitTime = new DateTime(yy, mm, dd, 07, 30, 00);
            int timecompare = Convert.ToInt32((LimitTime - startnow).TotalSeconds);
            return (timecompare > 0) ? nowData.AddDays(-1) : nowData;
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

        public static bool DataCollectionPromis (DateTime date,string shift)
        {
            DateTime registdateAt = FindRegistDateFromCurrentTime(date);
            string shiftAt = FindDayOrNight(date);
            DateTime registdateNow = FindRegistDateFromCurrentTime(DateTime.Now);
            string shiftNow = FindDayOrNight(DateTime.Now);
            registdateAt = new DateTime(registdateAt.Year, registdateAt.Month, registdateAt.Day);
            registdateNow = new DateTime(registdateNow.Year, registdateNow.Month, registdateNow.Day);
            if (registdateAt== registdateNow && shiftAt!= shiftNow && shiftAt=="D")
            {
                return true;
            }
            else if (registdateAt < registdateNow)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int FindWorkingHourIsInNumber(List<DateTime> startTimeMonitor,  int chkOT)
        {
            int hourNo = 0;
            //int compensate = (DayNight == "D") ? 0 : 10;
            for (int i = 0; i < 10 - chkOT; i++)
            {
                double dtest = (DateTime.Now - startTimeMonitor[i]).TotalSeconds;
                if (dtest > 0)
                    hourNo = i + 1;
            }
            return hourNo;
        }





    }
}
