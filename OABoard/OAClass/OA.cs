using OABoard.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OABoard.OAClass
{
    public static class OA
    {

        public static bool InitOABoard(string section, DateTime date, string div, string plant)
        {
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            DateTime registDate = FindRegistDate(date);
            Param.Registdate = registDate;

            string dayNight = FindDayOrNight(date);
            string yearStr = registDate.ToString("yyyy");
            string monthStr = registDate.ToString("MM");
            DateTime baseDate = new DateTime(1900, 1, 1, 0, 0, 0);

            try
            {

                using (var db = new ProductionEntities())
                {

                    var breakNo = db.Prod_TimeBreakQueueTable.Where(s => s.sectionCode == section)
                        .Where(r => r.registYear == yearStr).Where(r => r.registMonth == monthStr).SingleOrDefault();
                    if (breakNo == null)
                    {
                        breakNo.breakQueue = 1;
                    }

                    // var todayWork = new Prod_TodayWorkTable() ;
                    int workHour = 10;
                    var todayWork = db.Prod_TodayWorkTable.Where(s => s.sectionCode == section)
                        .Where(r => r.registDate == registDate).Where(r => r.dayNight == dayNight).SingleOrDefault();
                    if (todayWork != null)
                    {
                        workHour = todayWork.workHour;
                    }

                    var stdYearly = db.Prod_StdYearlyTable.Where(s => s.sectionCode == section)
                       .Where(r => r.registYear == yearStr).Where(r => r.registMonth == monthStr).SingleOrDefault();
                    if (stdYearly == null)
                    {
                        stdYearly.oa = 100;
                        stdYearly.cycleTimeAverage = 1;
                    }
                    Param.OATarget = stdYearly.oa;
                    Param.CTAverage = stdYearly.cycleTimeAverage;

                    int start = dayNight == "D" ? 1 : 11;
                    int plusHr = workHour == 8 ? 7 : 9;
                    int stop = start + plusHr;

                    var breakTable = db.Prod_TimeBreakTable.Where(s => s.breakQueue == breakNo.breakQueue)
                       .Where(r => r.divisionID == div).Where(r => r.plantID == plant)
                       .Where(h => h.hourNo >= start && h.hourNo <= stop)
                       .Select(s => new TimeBreak
                       {
                           HourNo = s.hourNo,
                           StartTime = s.startTime,
                           StopTime = s.stopTime,
                           DayNight = s.dayNight,
                           StartTimeMonitor = s.startTimeMonitor,
                           StopTimeMonitor = s.stopTimeMonitor,
                       }).ToList();


                    int datediff = Convert.ToInt32((registDate - baseDate).TotalDays);

                    foreach (TimeBreak i in breakTable)
                    {
                        var update = new TimeBreak()
                        {
                            HourNo = i.HourNo,
                            StartTime = i.StartTime.AddDays(datediff),
                            StopTime = i.StopTime.AddDays(datediff),
                            DayNight = i.DayNight,
                            StartTimeMonitor = i.StartTimeMonitor.AddDays(datediff),
                            StopTimeMonitor = i.StopTimeMonitor.AddDays(datediff)
                        };
                        Param.TimeBreakTable.Add(update);
                    }

                    Param.PeriodOfTime = new PeriodTime()
                    {
                        Start = Param.TimeBreakTable.FirstOrDefault(h => h.HourNo == start).StartTime,
                        Stop = Param.TimeBreakTable.FirstOrDefault(h => h.HourNo == stop).StopTime,
                    };

                    Param.MasterTime = MakingWorkingTime(Param.PeriodOfTime.Start);

                    Param.CTimeTable = db.Prod_NetTimeTable.Where(s => s.sectionCode == section)
                        .Where(r => r.registYear == yearStr).Select(x => new CTime
                        {
                            Partnumber = x.partNumber,
                            Cycletime = x.CT,
                        }).ToList();

                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }




        public static OaBoard CalculateOA(string section, DateTime starttime)
        {
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            try
            {
                using (var db = new ProductionEntities())
                {
                    var raw = db.Prod_RecordTable.Where(s => s.sectionCode == section)
                        .Where(s => s.registDateTime >= Param.PeriodOfTime.Start && s.registDateTime <= Param.PeriodOfTime.Stop).ToList();

                    double Ctavg;
                    string model;
                    var actualAmount = Convert.ToDouble(raw.Count());
                    if (actualAmount != 0)
                    {

                        var modelPN = raw.ToList().OrderByDescending(r => r.registDateTime).FirstOrDefault();
                        model = modelPN.partNumber;

                        var ctavg = raw.GroupBy(p => p.partNumber)
                            .Select(x => new PartnumberQty
                            {
                                Partnumber = x.Key,
                                Qty = x.Count()
                            }).ToList();

                        var joinValue = (from r in ctavg
                                         join c in Param.CTimeTable on r.Partnumber equals c.Partnumber
                                         select new CTime
                                         {
                                             Cycletime = r.Qty * c.Cycletime
                                         }).ToList();
                        var result = joinValue.Sum(x => x.Cycletime);

                        Ctavg = result / actualAmount;
                    }
                    else
                    {
                        Ctavg = Param.CTimeTable.Average(c => c.Cycletime);
                        model = "";
                    }

                    double planAmount = Math.Floor(TakeTheTime() / Ctavg);

                    double or = Math.Round(actualAmount / planAmount * 100, 1, MidpointRounding.AwayFromZero);

                    int lintestatus = WorkStatus() == true ? 0 : 1;

                    double ctWeight = Math.Round(Ctavg, 1, MidpointRounding.AwayFromZero);

                    var oaboard = new OaBoard()
                    {
                        Partnumber = model,
                        Plan = planAmount,
                        Actual = actualAmount,
                        Diff = planAmount - actualAmount,
                        OR = or,
                        CT = ctWeight,
                        LineStatus = (Lintestatus)lintestatus,
                        OaStatus = or >= Param.OATarget ? OaStatus.Morethan : OaStatus.Lessthan,
                    };

                    //---->  for test
                    if (lintestatus == 0)
                    {
                        double planAmountTest = Math.Floor(TakeTheTime() / Param.CTAverage);
                        double orTest = Math.Round(actualAmount / planAmountTest * 100, 1, MidpointRounding.AwayFromZero);
                        var log = new OR_log()
                        {
                            sectionCode = section,
                            registDate = Param.Registdate,
                            registDateTime = starttime,
                            ctAvg = Param.CTAverage,
                            ctWeight = ctWeight,
                            orAvg = orTest,
                            orWeight = or,
                        };
                        db.OR_log.Add(log);
                        db.SaveChanges();

                    }
               


                    return oaboard;
                }

            }
            catch (Exception )
            {
                return new OaBoard();
            }


        }


        public static double TakeTheTime()
        {
            int endTime = Convert.ToInt32((DateTime.Now - Param.PeriodOfTime.Start).TotalSeconds);
            endTime = endTime > 43200 ? 43200 : endTime;
            double total = 0;
            for (int i = 0; i < endTime; i++)
            {
                total += Param.MasterTime[i];
            }
            return total;

        }

        public static bool WorkStatus()
        {
            DateTime timenow = DateTime.Now;
            foreach (TimeBreak item in Param.TimeBreakTable)
            {
                if (item.StartTimeMonitor <= timenow && timenow <= item.StopTimeMonitor)
                    return true;
            }
            return false;
        }




        public static int[] MakingWorkingTime(DateTime StartTime)
        {
            var MasterTime = new int[43200];
            foreach (var item in Param.TimeBreakTable)
            {
                int d11 = Convert.ToInt32((item.StartTimeMonitor - StartTime).TotalSeconds);
                int d12 = Convert.ToInt32((item.StopTimeMonitor - StartTime).TotalSeconds);
                d11 = (d11 > 43200) ? 43200 : d11;
                d12 = (d12 > 43200) ? 43200 : d12;
                if (d11 < d12)
                {
                    for (int n = d11; n < d12; n++)
                    {
                        MasterTime[n] = 1;
                    }
                }
            }

            return MasterTime;

        }

        public static DateTime FindRegistDate(DateTime startnow)
        {
            int yy = startnow.Year;
            int mm = startnow.Month;
            int dd = startnow.Day;

            DateTime LimitTime = new DateTime(yy, mm, dd, 07, 30, 00);
            int timecompare = Convert.ToInt32((LimitTime - startnow).TotalSeconds);

            DateTime newDate = new DateTime(yy, mm, dd, 0, 0, 0);
            if (timecompare > 0)
            {
                newDate = newDate.AddDays(-1);
            }
            return newDate;
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









}
