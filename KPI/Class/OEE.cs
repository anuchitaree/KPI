using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace KPI.Class
{
    public class OEE
    {

        //public void ProcessingOEE(DateTime date, string section)
        //{
        //    var resultMC = new List<OeeItems>();

        //    var ghOeeRaw = new List<GHOeeRaw>();
        //    var updateLossDb = new List<UpdateLossDb>();

        //    DateTime registdate = new DateTime(date.Year, date.Month, date.Day, 7, 30, 0);
        //    DateTime initDate = new DateTime(1900, 1, 1, 7, 30, 0);
        //    int dateDiff = Convert.ToInt32((registdate - initDate).TotalDays);

        //    string registDate = registdate.ToString("yyyy-MM-dd");
        //    SqlClass sql = new SqlClass();
        //    bool sqlstatus = sql.SSQL_SS("Oee_Processing1", "@pStartTime", registDate, "@section", section);
        //    if (sqlstatus == false)
        //        return;

        //    DataSet ds = sql.Dataset;
        //    DataTable standardtime = ds.Tables[0];  // starttime,stoptime,hourNo                                           [Production].[dbo].[Prod_TimeBreakTable]
        //    DataTable exclusionTime = ds.Tables[1]; //[recordTime], [minute]                                               [Production].[dbo].[Exclusion_RecordTable]
        //    DataTable machineID = ds.Tables[2]; // [machineId]                                                             [Production].[dbo].[Prod_MachineNameTable]
        //    DataTable machineTime = ds.Tables[3]; //[machineID],[partNumber],[MTminSec],[HTminSec]                         [Production].[dbo].[Oee_MachineTime]
        //    DataTable losstime = ds.Tables[4]; //A1,A2,P1,P2 : McId,dateTimeStart,dateTimeEnd,partnumberEnd,OeeID,run      [Production].[dbo].[Loss_RecordTable]
        //    DataTable mlRecord = ds.Tables[5]; //A3,P3,P4    : [mcNumber],[registDateTime],[partNumber],[mcTimeSec],[OKNG] [Production].[dbo].[ML_RecordTable]

            

        //    if (mlRecord.Rows.Count == 0)
        //        return;
        //    if (standardtime.Rows.Count == 0 || machineID.Rows.Count == 0)
        //        return;


        //    List<LoadingTime> loadingTimeList = LoadingTimeToList(standardtime, dateDiff);
        //    List<ExclusionTime> exclusionTimeList = ExclusionTimeToList(exclusionTime);

        //    StardardTime standardTime = StandardTimeProcess(registdate, loadingTimeList, exclusionTimeList, ghOeeRaw);

        //    double loadingtime = standardTime.Stardardtime.Sum() / 60;
        //    ghOeeRaw = standardTime.GraphOeeRaw;

        //    List<Machine> machineIDList = MachineIDToList(machineID);
        //    List<MachineTime> machineTimeList = MachineTimeToList(machineTime);
        //    List<LossTime> losstimeList = LosstimeToList(losstime);
        //    List<Record> recordList = RecordTableToList(loadingTimeList, mlRecord);


        //    Machine machinedefault = machineIDList.FirstOrDefault();
        //    string buttomNeckMC = machinedefault.MachineID;
        //    using (var db = new ProductionEntities1())
        //    {
        //        Oee_ButtomNecks buttomNeckExist = db.Oee_ButtomNecks.FirstOrDefault(s => s.sectionCode == section);
        //        if (buttomNeckExist != null)
        //        {
        //            buttomNeckMC = buttomNeckExist.machineId;
        //        }
                
        //    }


        //    Console.WriteLine($"---------------  {registDate}   ---------------------");

        //    //Console.WriteLine($"data table {mlRecord.Rows.Count} list { recordList.Count }");

        //    //     bool hasOrNot = recordList.Where(m => m.MachineID == "T6SMM0018100").Any();


        //    int[] PstdTime = new int[86400];
        //    for (int i = 0; i < 86400; i++)
        //    {
        //        PstdTime[i] = standardTime.Stardardtime[i];
        //    }
        //    ProgressLoss(registdate, section, losstimeList, PstdTime);

        //    //-------------------------------------------

        //    foreach (Machine m in machineIDList)
        //    {
        //        var oeemc = new OeeItems();
        //        int[] stdtime = new int[86400];

        //        for (int i = 0; i < 86400; i++)
        //        {
        //            stdtime[i] = standardTime.Stardardtime[i];
        //        }

        //        //========= Exclusion Cycle time : Case :: When some model ,machine is not used ===========//

        //        var hasRecord = recordList.Where(c => c.MachineID == m.MachineID).Where(p => p.PartNumber == "TG000000-00000T").Any();
        //        if (hasRecord)
        //        {
        //            var rawMlRecord = recordList.Where(c => c.MachineID == m.MachineID).ToList();
        //            var exc = ExclusionMachineCycle(registdate, stdtime, rawMlRecord, ghOeeRaw);
        //            ghOeeRaw = exc.GraphOeeRaw;
        //            stdtime = exc.Stardardtime;
        //            //WriteRecordToCSV(registdate, exc.RecodeData, m.MachineID);
        //        }
        //        else
        //        {
        //            //  WriteRecordToCSV(registdate, recordList,m.MachineID);
        //        }

        //        oeemc.LoadingTime = loadingtime;

        //        //=========== Progress Loss Time =========//






        //        //========= P1,P2 =============//

        //        Console.WriteLine($"===========================  \n Machine name : {m.MachineID}   \n Stardard Time   = {stdtime.Sum()}");

        //        var hasLossTimebyMC = losstimeList.Where(c => c.MachineID == m.MachineID).Where(o => o.OeeID == "P1");
        //        if (!hasLossTimebyMC.Any())
        //        {
        //            oeemc.P1 = oeemc.P2 = 0;
        //        }
        //        else
        //        {
        //            var lossTimeByMC = hasLossTimebyMC.ToList();
        //            ResultP12 p12 = P12MCBreakDown(registdate, stdtime, lossTimeByMC, updateLossDb, ghOeeRaw);
        //            oeemc.P1 = Math.Round(p12.P1, 2, MidpointRounding.AwayFromZero);
        //            oeemc.P2 = Math.Round(p12.P2, 2, MidpointRounding.AwayFromZero);
        //            stdtime = p12.Stardardtime;
        //            ghOeeRaw = p12.GraphOeeRaw;
        //            updateLossDb = p12.UpdateLoss;
        //        }

        //        //=========  A1 ========//
        //        Console.WriteLine($"Stardard Time after P1,P2 = {stdtime.Sum()}");

        //        var hasSetupTimebyMC = losstimeList.Where(c => c.MachineID == m.MachineID).Where(o => o.OeeID == "A1");
        //        if (!hasSetupTimebyMC.Any())
        //        {
        //            oeemc.A1 = 0;
        //        }
        //        else
        //        {
        //            var setupTimeByMC = hasSetupTimebyMC.ToList();
        //            ResultA1 a1 = A1SetupTime(registdate, stdtime, setupTimeByMC, updateLossDb, ghOeeRaw);
        //            oeemc.A1 = Math.Round(a1.A1, 2, MidpointRounding.AwayFromZero);
        //            stdtime = a1.Stardardtime;
        //            ghOeeRaw = a1.GraphOeeRaw;
        //            updateLossDb = a1.UpdateLoss;
        //        }

        //        //=========  A2 ========//
        //        Console.WriteLine($"Stardard Time after A1 = {stdtime.Sum()}");

        //        var hasToolChangeTimebyMC = losstimeList.Where(c => c.MachineID == m.MachineID).Where(o => o.OeeID == "A2");
        //        if (!hasToolChangeTimebyMC.Any())
        //        {
        //            oeemc.A2 = 0;
        //        }
        //        else
        //        {
        //            var toolChangeByMC = hasToolChangeTimebyMC.ToList();
        //            ResultA2 a2 = A2ToolChangeTime(registdate, stdtime, toolChangeByMC, updateLossDb, ghOeeRaw);
        //            oeemc.A2 = Math.Round(a2.A2, 2, MidpointRounding.AwayFromZero);
        //            stdtime = a2.Stardardtime;
        //            ghOeeRaw = a2.GraphOeeRaw;
        //            updateLossDb = a2.UpdateLoss;
        //        }


        //        //=========  A3  , P3 ,P4 , Q ========//
        //        var hasData = recordList.Where(c => c.MachineID == m.MachineID);
        //        if (!hasData.Any())
        //        {
        //            oeemc.A3 = oeemc.P3 = oeemc.P4 = oeemc.OK = oeemc.NG = oeemc.RE = 0;
        //        }
        //        else
        //        {
        //            var mlrecord = hasData.OrderBy(r => r.CurrentDateTime).ToList();

        //            //=========  A3 ===========//
        //            Console.WriteLine($"Stardard Time after A2 = {stdtime.Sum()}");
        //            ResultA3 a3 = A3StartUpTime(registdate, stdtime, mlrecord, loadingTimeList, ghOeeRaw);
        //            oeemc.A3 = Math.Round(a3.A3, 2, MidpointRounding.AwayFromZero);
        //            stdtime = a3.Stardardtime;
        //            ghOeeRaw = a3.GraphOeeRaw;


        //            //=========  P3 ,P4  ========//
        //            Console.WriteLine($"Stardard Time after A3  = {stdtime.Sum()}");
        //            ResultP34 p34 = P3SpeeP4IdlingAndQ(registdate, stdtime, mlrecord, loadingTimeList, machineTimeList, ghOeeRaw);
        //            oeemc.P3 = Math.Round(p34.P3, 2, MidpointRounding.AwayFromZero);
        //            oeemc.P4 = Math.Round(p34.P4, 2, MidpointRounding.AwayFromZero);

        //            ghOeeRaw = p34.GraphOeeRaw;
        //            Console.WriteLine($"Stardard Time after P3,P4 = {stdtime.Sum()}");

        //            //=========  Q => OK ,NG ,RE  ========//
        //            oeemc.OK = Math.Round(p34.OKCycleTime, 2, MidpointRounding.AwayFromZero);
        //            oeemc.NG = Math.Round(p34.NGCycleTime, 2, MidpointRounding.AwayFromZero);
        //            oeemc.RE = Math.Round(p34.RECycleTime, 2, MidpointRounding.AwayFromZero);


        //            //if (m.MachineID == "T7ZZT0440400")
        //            //{
        //            ghOeeRaw = WriteGHOeeRaw(registdate, mlrecord, ghOeeRaw);
        //            //}

        //        }


        //        Console.WriteLine($"Stardard Time End = {stdtime.Sum()}");

        //        double avilabilityTime = oeemc.A1 + oeemc.A2 + oeemc.A3;
        //        double operatingTime = avilabilityTime < loadingtime ? loadingtime - avilabilityTime : 0;
        //        double avilabilityRatio = Math.Round(operatingTime / loadingtime * 100, 2, MidpointRounding.AwayFromZero);
        //        avilabilityRatio = double.IsNaN(avilabilityRatio) ? 0 : avilabilityRatio;


        //        double performanceTime = oeemc.P1 + oeemc.P2 + oeemc.P3 + oeemc.P4;
        //        double netTime = operatingTime > performanceTime ? operatingTime - performanceTime : 0;
        //        double performanceRatio = Math.Round(netTime / operatingTime * 100, 2, MidpointRounding.AwayFromZero);
        //        performanceRatio = double.IsNaN(performanceRatio) ? 0 : performanceRatio;

        //        double totalWork = oeemc.OK + oeemc.NG + oeemc.RE;
        //        double qualityRatio = Math.Round(oeemc.OK / totalWork * 100, 2, MidpointRounding.AwayFromZero);
        //        qualityRatio = double.IsNaN(qualityRatio) ? 0 : qualityRatio;

        //        double MCOee = Math.Round(avilabilityRatio * performanceRatio * qualityRatio / 10000, 2, MidpointRounding.AwayFromZero);
        //        oeemc.OEEPercent = double.IsNaN(MCOee) ? 0 : MCOee;
        //        oeemc.AvailabilityPercent = avilabilityRatio;
        //        oeemc.PerformancePercent = performanceRatio;
        //        oeemc.QaulityPercent = qualityRatio;
        //        oeemc.MachineID = m.MachineID;
        //        resultMC.Add(oeemc);

        //    }


        //    var sql1 = new SqlClass();
        //    sql1.GHOeeRawInsert(registdate, section, ghOeeRaw);
        //    sql1.UpdateLossRecordDB(registDate, section, updateLossDb);


        //    //LineOEE oeeline = (from n in resultMC
        //    //                   orderby n.OEEPercent ascending
        //    //                   select new LineOEE
        //    //                   {
        //    //                       A = n.AvailabilityPercent,
        //    //                       P = n.PerformancePercent,
        //    //                       Q = n.QaulityPercent,
        //    //                       OEE = n.OEEPercent,
        //    //                   }).First();

        //    LineOEE oeeline = resultMC.Where(m=>m.MachineID== buttomNeckMC)
        //                      .Select(n=> new LineOEE
        //                       {
        //                           A = n.AvailabilityPercent,
        //                           P = n.PerformancePercent,
        //                           Q = n.QaulityPercent,
        //                           OEE = n.OEEPercent,
        //                       }).First();

        //    if (loadingtime != 0)
        //    {
        //        var sql10 = new SqlClass();
        //        sql10.OEEMCandLine(section, registDate, resultMC, oeeline);
        //        Console.WriteLine("{0} is normal", registDate);
        //    }
        //    else
        //    {
        //        var sql12 = new SqlClass();
        //        sql12.OEEMCandLineDel(section, registDate);
        //        Console.WriteLine("{0} is NOT MP NOT registed", registDate);
        //    }

        //}

        #region  =============== Data preparation =============
        private List<LoadingTime> LoadingTimeToList(DataTable loadingTime, int dateDiff)
        {
            var LoadingTimeTable = new List<LoadingTime>();
            foreach (DataRow dr in loadingTime.Rows)
            {
                var wt = new LoadingTime
                {
                    HourNo = Convert.ToInt32(dr.ItemArray[2]),
                    StartTime = Convert.ToDateTime(dr.ItemArray[0]).AddDays(dateDiff),
                    StopTime = Convert.ToDateTime(dr.ItemArray[1]).AddDays(dateDiff),
                };
                LoadingTimeTable.Add(wt);
            }
            return LoadingTimeTable;
        }
        private List<ExclusionTime> ExclusionTimeToList(DataTable exclsionTime)
        {
            var ExclusionTime = new List<ExclusionTime>();
            foreach (DataRow dr in exclsionTime.Rows)
            {
                DateTime recordtime = Convert.ToDateTime(dr.ItemArray[0]);
                double period = Convert.ToDouble(dr.ItemArray[1]);
                var wt = new ExclusionTime
                {
                    StartTime = recordtime,
                    EndTime = recordtime.AddMinutes(period),
                };
                ExclusionTime.Add(wt);
            }
            return ExclusionTime;
        }
        private StardardTime StandardTimeProcess(DateTime regitsDate, List<LoadingTime> LoadingTimeList, List<ExclusionTime> exclusion, List<GHOeeRaw> ghOeeRaw)
        {
            int[] masterTime = new int[86400];
            int i = 0;
            foreach (var l in LoadingTimeList)
            {
                int d11 = Convert.ToInt32((l.StartTime - regitsDate).TotalSeconds);
                int d12 = Convert.ToInt32((l.StopTime - regitsDate).TotalSeconds);
                d11 = (d11 > 86400) ? 86400 : d11;
                d12 = (d12 > 86400) ? 86400 : d12;
                for (int n = d11; n < d12; n++)
                {
                    masterTime[n] = 1;
                }
                i++;


                // For GHOee_RAW
                var working = new GHOeeRaw()
                {
                    MachineID = l.StartTime.ToString("HH:mm"),
                    Type = 2,
                    StartMinuteTime = d11 / 60,
                    OccurePeriodMinute = (d12 - d11) / 60
                };
                ghOeeRaw.Add(working);
            }




            foreach (ExclusionTime e in exclusion)
            {
                int d11 = Convert.ToInt32((e.StartTime - regitsDate).TotalSeconds);
                int d12 = Convert.ToInt32((e.EndTime - regitsDate).TotalSeconds);

                d11 = (d11 > 86400) ? 86400 : d11;
                d12 = (d12 > 86400) ? 86400 : d12;
                int bef = masterTime.Sum();
                for (int n = d11; n <= d12; n++)
                {
                    masterTime[n] = 0;
                }
                int aft = masterTime.Sum();

                // For GHOee_RAW
                var exc = new GHOeeRaw()
                {
                    MachineID = e.StartTime.ToString("HH:mm"),
                    Type = 3,
                    StartMinuteTime = d11 / 60,
                    OccurePeriodMinute = (aft - bef) / 60
                };
                ghOeeRaw.Add(exc);

            }

            var result = new StardardTime()
            {
                Stardardtime = masterTime,
                GraphOeeRaw = ghOeeRaw,
            };


            return result;
        }
        private List<Machine> MachineIDToList(DataTable dt)
        {
            var record = new List<Machine>();
            record.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                var ml = new Machine
                {
                    MachineID = dr.ItemArray[0].ToString(),
                };
                record.Add(ml);
            }
            return record;
        }
        private List<MachineTime> MachineTimeToList(DataTable mcTime)
        {
            var record = new List<MachineTime>();
            record.Clear();
            foreach (DataRow dr in mcTime.Rows)
            {
                var ml = new MachineTime
                {
                    MachineID = dr.ItemArray[0].ToString(),
                    PartNumber = dr.ItemArray[1].ToString(),
                    MTminSec = Convert.ToDouble(dr.ItemArray[2]),
                    HTminSec = Convert.ToDouble(dr.ItemArray[3]),
                };

                record.Add(ml);
            }
            return record;
        }

        private List<LossTime> LosstimeToList(DataTable dt)
        {
            var record = new List<LossTime>();
            record.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                var ml = new LossTime
                {
                    MachineID = dr.ItemArray[0].ToString(),
                    DateTimeStart = Convert.ToDateTime(dr.ItemArray[1]),
                    DateTimeEnd = Convert.ToDateTime(dr.ItemArray[2]),
                    PartnumberEnd = dr.ItemArray[3].ToString(),
                    OeeID = dr.ItemArray[4].ToString(),
                    Run = dr.ItemArray[5].ToString(),
                    Registdate = Convert.ToDateTime(dr.ItemArray[6]),
                };

                record.Add(ml);
            }
            return record;
        }
        private List<Record> RecordTableToList(List<LoadingTime> LoadingTimeList, DataTable RecordTable)
        {
            DateTime defaulttime = new DateTime(1900, 1, 1, 0, 0, 0);
            List<Record> record = new List<Record>();
            record.Clear();

            foreach (DataRow dr in RecordTable.Rows)
            {
                try
                {
                    var ml = new Record
                    {
                        ID = dr.ItemArray[0].ToString(),
                        MachineID = dr.ItemArray[1] == null ? "" : dr.ItemArray[1].ToString(),
                        CurrentDateTime = String.IsNullOrEmpty(dr.ItemArray[2].ToString()) == true ? defaulttime : Convert.ToDateTime(dr.ItemArray[2]),
                        PartNumber = dr.ItemArray[3] == null ? "" : dr.ItemArray[3].ToString(),
                        MTTime = dr.ItemArray[4] == null ? 0 : Convert.ToDouble(dr.ItemArray[4]),
                        OKNG = dr.ItemArray[5] == null ? "" : dr.ItemArray[5].ToString(),

                    };
                    record.Add(ml);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.ToString());
                }

            }


            //bool hasOrNot = record.Where(m => m.MachineID == "T6SMM0018100").Any();


            var h1 = LoadingTimeList.Where(h => h.HourNo == 1).Any();
            if (h1)
            {
                LoadingTime h01 = LoadingTimeList.Where(h => h.HourNo == 1).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime < h01.StartTime);
            }

            var h3 = LoadingTimeList.Where(h => h.HourNo == 3).Any();
            if (h3)
            {
                LoadingTime h21 = LoadingTimeList.Where(h => h.HourNo == 2).FirstOrDefault();
                LoadingTime h31 = LoadingTimeList.Where(h => h.HourNo == 3).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h21.StopTime && t.CurrentDateTime < h31.StartTime);
            }
            var h5 = LoadingTimeList.Where(h => h.HourNo == 5).Any();
            if (h5)
            {
                LoadingTime h41 = LoadingTimeList.Where(h => h.HourNo == 4).FirstOrDefault();
                LoadingTime h51 = LoadingTimeList.Where(h => h.HourNo == 5).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h41.StopTime && t.CurrentDateTime < h51.StartTime);
            }

            var h7 = LoadingTimeList.Where(h => h.HourNo == 7).Any();
            if (h7)
            {
                LoadingTime h61 = LoadingTimeList.Where(h => h.HourNo == 6).FirstOrDefault();
                LoadingTime h71 = LoadingTimeList.Where(h => h.HourNo == 7).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h61.StopTime && t.CurrentDateTime < h71.StartTime);
            }
            var h8 = LoadingTimeList.Where(h => h.HourNo == 8).Any();
            var h9 = LoadingTimeList.Where(h => h.HourNo == 9).Any();
            var h11 = LoadingTimeList.Where(h => h.HourNo == 11).Any();
            if (h9 && h8)
            {
                LoadingTime h81 = LoadingTimeList.Where(h => h.HourNo == 8).FirstOrDefault();
                LoadingTime h91 = LoadingTimeList.Where(h => h.HourNo == 9).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h81.StopTime && t.CurrentDateTime < h91.StartTime);
            }
            else if (!h9 && h8 && h11)
            {
                LoadingTime h811 = LoadingTimeList.Where(h => h.HourNo == 8).FirstOrDefault();
                LoadingTime h1111 = LoadingTimeList.Where(h => h.HourNo == 11).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h811.StopTime && t.CurrentDateTime < h1111.StartTime);
            }


            var h10 = LoadingTimeList.Where(h => h.HourNo == 10).Any();
            if (h11 && h10)
            {
                LoadingTime h101 = LoadingTimeList.Where(h => h.HourNo == 10).FirstOrDefault();
                LoadingTime h111 = LoadingTimeList.Where(h => h.HourNo == 11).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h101.StopTime && t.CurrentDateTime < h111.StartTime);
            }
            else if (h11 && h8)
            {
                LoadingTime h81 = LoadingTimeList.Where(h => h.HourNo == 8).FirstOrDefault();
                LoadingTime h111 = LoadingTimeList.Where(h => h.HourNo == 11).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h81.StopTime && t.CurrentDateTime < h111.StartTime);
            }

            var h13 = LoadingTimeList.Where(h => h.HourNo == 13).Any();
            if (h13)
            {
                LoadingTime h131 = LoadingTimeList.Where(h => h.HourNo == 13).FirstOrDefault();
                LoadingTime h121 = LoadingTimeList.Where(h => h.HourNo == 12).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h121.StopTime && t.CurrentDateTime < h131.StartTime);
            }

            var h15 = LoadingTimeList.Where(h => h.HourNo == 15).Any();
            if (h15)
            {
                LoadingTime h151 = LoadingTimeList.Where(h => h.HourNo == 15).FirstOrDefault();
                LoadingTime h141 = LoadingTimeList.Where(h => h.HourNo == 14).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h141.StopTime && t.CurrentDateTime < h151.StartTime);
            }

            var h17 = LoadingTimeList.Where(h => h.HourNo == 17).Any();
            if (h17)
            {
                LoadingTime h171 = LoadingTimeList.Where(h => h.HourNo == 17).FirstOrDefault();
                LoadingTime h161 = LoadingTimeList.Where(h => h.HourNo == 16).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h161.StopTime && t.CurrentDateTime < h171.StartTime);
            }

            var h18 = LoadingTimeList.Where(h => h.HourNo == 18).Any();
            var h19 = LoadingTimeList.Where(h => h.HourNo == 19).Any();
            if (h19)
            {
                LoadingTime h191 = LoadingTimeList.Where(h => h.HourNo == 19).FirstOrDefault();
                LoadingTime h181 = LoadingTimeList.Where(h => h.HourNo == 18).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h181.StopTime && t.CurrentDateTime < h191.StartTime);
            }
            else if (h18 && !h19)
            {
                LoadingTime h1811 = LoadingTimeList.Where(h => h.HourNo == 18).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h1811.StopTime);
            }

            var h20 = LoadingTimeList.Where(h => h.HourNo == 20).Any();
            if (h20)
            {
                LoadingTime h201 = LoadingTimeList.Where(h => h.HourNo == 20).FirstOrDefault();
                record.RemoveAll(t => t.CurrentDateTime > h201.StopTime);
            }
            // bool hasOrNot = record.Where(m => m.MachineID == "T6SMM0018100").Any();

            return record;
        }

        private ExclusionCycleTime ExclusionMachineCycle(DateTime registDate, int[] standardTime, List<Record> record, List<GHOeeRaw> ghOeeRaw)
        {
            var exclusionCycleTime = new ExclusionCycleTime() { RecodeData = record, Stardardtime = standardTime, GraphOeeRaw = ghOeeRaw };
            int row = record.Count - 1;
            var newRecord = new List<Record>();
            //var hasData = record.Where(p => p.PartNumber == "TG000000-00000T").Any();
            //if (!hasData)
            //    return exclusionCycleTime;

            //  record = record.Where(p => p.PartNumber == "TG000000-00000T").OrderBy(c => c.CurrentDateTime).ToList();

            for (int i = 0; i < row - 1; i++)
            {
                double d11 = Convert.ToDouble((record[i + 1].CurrentDateTime - record[i].CurrentDateTime).TotalSeconds);
                var rec = new Record()
                {
                    ID = record[i].ID,
                    MachineID = record[i].MachineID,
                    CurrentDateTime = record[i].CurrentDateTime,
                    PartNumber = record[i].PartNumber,
                    MTTime = record[i].MTTime,
                    OKNG = record[i].OKNG,
                    CTime = d11
                };
                newRecord.Add(rec);
            }

            double d12 = Convert.ToDouble((registDate.AddDays(1) - record[row].CurrentDateTime).TotalSeconds);
            var rec1 = new Record()
            {
                ID = record[row].ID,
                MachineID = record[row].MachineID,
                CurrentDateTime = record[row].CurrentDateTime,
                PartNumber = record[row].PartNumber,
                MTTime = record[row].MTTime,
                OKNG = record[row].OKNG,
                CTime = d12
            };
            newRecord.Add(rec1);

            List<Record> filter = newRecord.Where(p => p.PartNumber == "TG000000-00000T").OrderBy(c => c.CurrentDateTime).ToList();
            foreach (Record r in filter)
            {
                int ds = Convert.ToInt32((r.CurrentDateTime - registDate).TotalSeconds);
                int de = ds + Convert.ToInt32(r.CTime);
                if (ds > 86400) ds = 86400;
                if (de > 86400) de = 86400;

                double before = standardTime.Sum();
                for (int n = ds; n < de; n++)
                {
                    standardTime[n] = 0;
                }
                double after = standardTime.Sum();


                // For GHOee_RAW
                double statt = Convert.ToDouble(ds);
                var a1a2 = new GHOeeRaw()
                {
                    MachineID = r.MachineID,
                    Type = 12,
                    StartMinuteTime = Convert.ToInt32(Math.Floor(statt / 60)),
                    OccurePeriodMinute = Convert.ToInt32(Math.Ceiling(before - after) / 60)
                };
                ghOeeRaw.Add(a1a2);

            }
            exclusionCycleTime.RecodeData = newRecord;
            exclusionCycleTime.Stardardtime = standardTime;
            exclusionCycleTime.GraphOeeRaw = ghOeeRaw;

            return exclusionCycleTime;
        }

        #endregion


        private static void ProgressLoss(DateTime date, string section, List<LossTime> losslist, int[] PstdTime)
        {
            using (var db = new ProductionEntities11())
            {
                DateTime newDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                var existtable = db.Pg_Loss.Where(s => s.sectionCode == section).Where(r => r.registDate == newDate).Any();
                if (existtable == false)
                {
                    DateTime newDate0 = new DateTime(date.Year, date.Month, 1, 0, 0, 0);
                    var initPgLoss = new List<Pg_Loss>();
                    int dayInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                    for (int i = 0; i < dayInMonth; i++)
                    {
                        var init = new Pg_Loss()
                        {
                            sectionCode = section,
                            registDate = newDate0.AddDays(i),
                            lossHr = 0,
                        };
                        initPgLoss.Add(init);
                    }
                    db.Pg_Loss.AddRange(initPgLoss);
                    db.SaveChanges();
                }

                double losstime;

                if (losslist.Count == 0)
                {
                    losstime = 0;
                }
                else
                {
                    int before = PstdTime.Sum();
                    foreach (LossTime l in losslist)
                    {
                        int d11 = Convert.ToInt32((l.DateTimeStart - date).TotalSeconds);
                        int d12 = Convert.ToInt32((l.DateTimeEnd - date).TotalSeconds);
                        if (d11 > 86400) d11 = 86400;
                        if (d12 > 86400) d12 = 86400;

                        for (int n = d11; n < d12; n++)
                        {
                            PstdTime[n] = 0;
                        }
                    }
                    losstime = Convert.ToDouble(before - PstdTime.Sum())/3600;
                }


                var exist = db.Pg_Loss.Where(s => s.sectionCode == section).SingleOrDefault(r => r.registDate == newDate);
                if (exist != null)
                {
                    exist.lossHr = losstime;
                    db.SaveChanges();
                }
            }



        }


        #region ====== A1 , A2 , A3 , P1 , P2 , P3 , P4  ======

        private ResultP12 P12MCBreakDown(DateTime registDate, int[] standardTime, List<LossTime> lossItmes, List<UpdateLossDb> updateDb, List<GHOeeRaw> ghOeeRaw)
        {
            double HTP1 = 0, HTP2 = 0; ;
            foreach (LossTime l in lossItmes)
            {
                int d11 = Convert.ToInt32((l.DateTimeStart - registDate).TotalSeconds);
                int d12 = Convert.ToInt32((l.DateTimeEnd - registDate).TotalSeconds);
                if (d11 > 86400) d11 = 86400;
                if (d12 > 86400) d12 = 86400;

                double whatLoss = 0;
                int before = standardTime.Sum();
                for (int n = d11; n < d12; n++)
                {
                    standardTime[n] = 0;
                }
                int after = standardTime.Sum();
                whatLoss = before - after;


                double HT = 0;

                //if (TB.Rows[i].ItemArray[3] != null && whatLoss > 0)
                //{
                //    string partnumber = TB.Rows[i].ItemArray[3].ToString();

                //    MachineTime db = MachineTimelist
                //        .Where(m => m.MachineID == mcname)
                //        .Where(p => p.PartNumber == partnumber)
                //        .FirstOrDefault();
                //    if (db != null)
                //    {
                //        HT += db.HTminSec;
                //    }

                //}

                int type = 0; // 4 : P1, 5: P2
                if (whatLoss > 5 * 60) // P1
                {
                    HTP1 += whatLoss - HT;
                    type = 4;
                }
                else if (whatLoss > 0)  // P2
                {
                    HTP2 += whatLoss - HT;
                    type = 5;
                }


                var update = new UpdateLossDb()
                {
                    Run = l.Run,
                    PureLossTime = whatLoss,
                    MixLossTime = Convert.ToDouble((l.DateTimeEnd - l.DateTimeStart).TotalSeconds)
                };
                updateDb.Add(update);



                if (type > 0)
                {
                    double startt = Convert.ToDouble(d11);
                    var p1p2 = new GHOeeRaw()
                    {
                        MachineID = l.MachineID,
                        Type = type,
                        StartMinuteTime = Convert.ToInt32(Math.Floor(startt / 60)),
                        OccurePeriodMinute = Convert.ToInt32(Math.Ceiling(whatLoss / 60)),
                    };
                    ghOeeRaw.Add(p1p2);
                }


            }


            var result = new ResultP12()
            {
                P1 = HTP1 / 60,
                P2 = HTP2 / 60,
                Stardardtime = standardTime,
                GraphOeeRaw = ghOeeRaw,
                UpdateLoss = updateDb
            };

            return result;


        }

        private ResultA1 A1SetupTime(DateTime registDate, int[] standardTime, List<LossTime> lossItmes, List<UpdateLossDb> updateDb, List<GHOeeRaw> ghOeeRaw)
        {
            double a1 = 0;
            foreach (LossTime l in lossItmes)
            {
                int d11 = Convert.ToInt32((l.DateTimeStart - registDate).TotalSeconds);
                int d12 = Convert.ToInt32((l.DateTimeEnd - registDate).TotalSeconds);
                if (d11 > 86400) d12 = 86400;
                if (d12 > 86400) d12 = 86400;

                double bef = standardTime.Sum();
                for (int n = d11; n < d12; n++)
                {
                    standardTime[n] = 0;
                }
                double aft = standardTime.Sum();
                a1 += (bef - aft) / 60;

                //if (TB.Rows[i].ItemArray[3] != null)
                //{
                //    string partnumber = TB.Rows[i].ItemArray[3].ToString();

                //    MachineTime db = MachineTimelist.Where(m => m.MachineID == mcname).Where(p => p.PartNumber == partnumber).FirstOrDefault();
                //    if (db != null)
                //    {
                //        HT += db.HTminSec;
                //    }

                //}


                var update = new UpdateLossDb()
                {
                    Run = l.Run,
                    PureLossTime = bef - aft,
                    MixLossTime = Convert.ToDouble((l.DateTimeEnd - l.DateTimeStart).TotalSeconds)
                };
                updateDb.Add(update);


                // For GHOee_RAW
                double statt = Convert.ToDouble(d11);
                var a1a2 = new GHOeeRaw()
                {
                    MachineID = l.MachineID,
                    Type = 6,
                    StartMinuteTime = Convert.ToInt32(Math.Floor(statt / 60)),
                    OccurePeriodMinute = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(d12 - d11) / 60))
                };
                ghOeeRaw.Add(a1a2);

            }

            var result = new ResultA1()
            {
                A1 = a1,
                Stardardtime = standardTime,
                UpdateLoss = updateDb,
                GraphOeeRaw = ghOeeRaw,
            };

            return result;
        }

        private ResultA2 A2ToolChangeTime(DateTime registDate, int[] standardTime, List<LossTime> lossItmes, List<UpdateLossDb> updateDb, List<GHOeeRaw> ghOeeRaw)
        {
            double a2 = 0;
            foreach (LossTime l in lossItmes)
            {
                int d11 = Convert.ToInt32((l.DateTimeStart - registDate).TotalSeconds);
                int d12 = Convert.ToInt32((l.DateTimeEnd - registDate).TotalSeconds);
                if (d11 > 86400) d12 = 86400;
                if (d12 > 86400) d12 = 86400;

                double bef = standardTime.Sum();
                for (int n = d11; n < d12; n++)
                {
                    standardTime[n] = 0;
                }
                double aft = standardTime.Sum();
                a2 += (bef - aft) / 60;

                //if (TB.Rows[i].ItemArray[3] != null)
                //{
                //    string partnumber = TB.Rows[i].ItemArray[3].ToString();

                //    MachineTime db = MachineTimelist.Where(m => m.MachineID == mcname).Where(p => p.PartNumber == partnumber).FirstOrDefault();
                //    if (db != null)
                //    {
                //        HT += db.HTminSec;
                //    }

                //}


                var update = new UpdateLossDb()
                {
                    Run = l.Run,
                    PureLossTime = bef - aft,
                    MixLossTime = Convert.ToDouble((l.DateTimeEnd - l.DateTimeStart).TotalSeconds)
                };
                updateDb.Add(update);


                // For GHOee_RAW
                double statt = Convert.ToDouble(d11);
                var a1a2 = new GHOeeRaw()
                {
                    MachineID = l.MachineID,
                    Type = 7,
                    StartMinuteTime = Convert.ToInt32(Math.Floor(statt / 60)),
                    OccurePeriodMinute = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(d12 - d11) / 60))
                };
                ghOeeRaw.Add(a1a2);

            }

            var result = new ResultA2()
            {
                A2 = a2,
                Stardardtime = standardTime,
                UpdateLoss = updateDb,
                GraphOeeRaw = ghOeeRaw,
            };

            return result;
        }



        private ResultA3 A3StartUpTime(DateTime registDate, int[] standardTime, List<Record> record, List<LoadingTime> LoadingTimeList, List<GHOeeRaw> ghOeeRaw)
        {
            var result = new ResultA3() { A3 = 0, Stardardtime = standardTime, GraphOeeRaw = ghOeeRaw };
            if (record.Count == 0)
                return result;

            Record buff = record.FirstOrDefault();
            string machineID = buff.MachineID;
            double summaryMinute = 0;
            for (int i = 1; i < 20; i += 2)
            {
                var has = LoadingTimeList.Where(h => h.HourNo == i).Any();
                if (has)
                {
                    LoadingTime hs = LoadingTimeList.Where(h => h.HourNo == i).FirstOrDefault();
                    LoadingTime he = LoadingTimeList.Where(h => h.HourNo == i + 1).FirstOrDefault();

                    bool hasRec = record.Where(t => t.CurrentDateTime >= hs.StartTime && t.CurrentDateTime <= he.StopTime).Any();

                    ResultSubA3 a3;
                    if (hasRec)
                    {
                        Record resultrecord = record.Where(t => t.CurrentDateTime >= hs.StartTime && t.CurrentDateTime <= he.StopTime).OrderBy(c => c.CurrentDateTime).FirstOrDefault();

                        a3 = A3StartUpTrue(registDate, standardTime, resultrecord, hs.StartTime);
                    }
                    else
                    {
                        a3 = A3StartUpFalse(registDate, standardTime, hs.StartTime, he.StopTime);
                    }

                    summaryMinute += a3.A3;
                    standardTime = a3.Stardardtime;
                    var a3log = new GHOeeRaw()
                    {
                        MachineID = machineID,
                        Type = 8,
                        StartMinuteTime = Convert.ToInt32(Math.Floor(a3.StartTime)),
                        OccurePeriodMinute = Convert.ToInt32(Math.Ceiling(a3.A3))
                    };
                    ghOeeRaw.Add(a3log);

                }

            }

            var result1 = new ResultA3()
            {
                A3 = summaryMinute,
                Stardardtime = standardTime,
                GraphOeeRaw = ghOeeRaw,
            };
            return result1;
        }

        private ResultSubA3 A3StartUpTrue(DateTime registDate, int[] standardTime, Record record, DateTime startTime)
        {
            var result = new ResultSubA3();
            if (registDate.Year == 1900)
                return result;

            double bef = standardTime.Sum();

            int d11 = Convert.ToInt32((startTime - registDate).TotalSeconds);
            int d12 = Convert.ToInt32((record.CurrentDateTime - registDate).TotalSeconds);
            d11 = d11 > 86400 ? 86400 : d11;
            d12 = d12 > 86400 ? 86400 : d12;
            if (d11 < d12)
            {
                for (int n = d11; n < d12; n++)
                {
                    standardTime[n] = 0;
                }

            }
            double aft = standardTime.Sum();
            result.A3 = (bef - aft) / 60;
            result.StartTime = d11 / 60;
            result.Stardardtime = standardTime;
            return result;
        }

        private ResultSubA3 A3StartUpFalse(DateTime registDate, int[] standardTime, DateTime startTime, DateTime stopTime)
        {
            int diff = Convert.ToInt32((stopTime - startTime).TotalSeconds);
            int d11 = Convert.ToInt32((startTime - registDate).TotalSeconds);
            int d12 = d11 + diff;

            d11 = d11 > 86400 ? 86400 : d11;
            d12 = d12 > 86400 ? 86400 : d12;

            for (int n = d11; n < d12; n++)
            {
                standardTime[n] = 0;
            }

            var result = new ResultSubA3()
            {
                A3 = diff / 60,
                StartTime = d11 / 60,
                Stardardtime = standardTime
            };
            return result;
        }







        private ResultP34 P3SpeeP4IdlingAndQ(DateTime registDate, int[] standardTime, List<Record> record, List<LoadingTime> LoadingTimeList, List<MachineTime> machineTimelist, List<GHOeeRaw> ghOeeRaw)
        {

            var result = new ResultP34() { P3 = 0, P4 = 0, GraphOeeRaw = ghOeeRaw, OKCycleTime = 0, NGCycleTime = 0, RECycleTime = 0 };
            if (record.Count == 0)
                return result;

            var csv = new List<MLRecord>();

            Record buff = record.FirstOrDefault();
            string machineID = buff.MachineID;
            double summaryP3 = 0, summaryP4 = 0, summaryPmachine = 0, summaryPhand = 0; ;
            double ok = 0, ng = 0, re = 0;
            for (int i = 1; i < 20; i += 2)
            {
                var has = LoadingTimeList.Where(h => h.HourNo == i).Any();
                if (has)
                {
                    LoadingTime hs = LoadingTimeList.Where(h => h.HourNo == i).FirstOrDefault();
                    LoadingTime he = LoadingTimeList.Where(h => h.HourNo == i + 1).FirstOrDefault();

                    bool hasRec = record.Where(t => t.CurrentDateTime >= hs.StartTime && t.CurrentDateTime <= he.StopTime).Any();

                    ResultP34 p34;
                    ResultQ qual;
                    if (hasRec)
                    {
                        List<Record> resultrecord = record.Where(t => t.CurrentDateTime >= hs.StartTime && t.CurrentDateTime <= he.StopTime).OrderBy(c => c.CurrentDateTime).ToList();

                        List<MLRecord> mlRecord = P34ConvertToPureMhtByStandardTime(registDate, standardTime, resultrecord, he.StopTime, machineTimelist);

                        // test
                        csv.AddRange(mlRecord);
                        //test

                        p34 = P34SpeedIdlingTimeTrue(registDate, mlRecord, ghOeeRaw);

                        qual = QualityCount(mlRecord);

                        

                    }
                    else
                    {
                        p34 = P34SpeedIdlingTimeFalse(registDate, standardTime, hs.StartTime, he.StopTime, ghOeeRaw, machineID);
                        qual = new ResultQ { OKCycleTime = 0, NGCycleTime = 0, RECycleTime = 0 };

                    }

                    summaryP3 += p34.P3;
                    summaryP4 += p34.P4;
                    summaryPhand += p34.PHand;
                    summaryPmachine += p34.PMachine;
                    ghOeeRaw = p34.GraphOeeRaw;

                    ok += qual.OKCycleTime;
                    ng += qual.NGCycleTime;
                    re += qual.RECycleTime;

                }
            }
            var result1 = new ResultP34()
            {
                P3 = summaryP3,
                P4 = summaryP4,
                PHand =summaryPhand,
                PMachine = summaryPmachine,
                GraphOeeRaw = ghOeeRaw,
                OKCycleTime = ok,
                NGCycleTime = ng,
                RECycleTime = re
            };

            // test
            WriteToCSV(registDate, csv);
            // test

            return result1;
        }


        private List<MLRecord> P34ConvertToPureMhtByStandardTime(DateTime registDate, int[] standardTime, List<Record> recordList, DateTime stopTime, List<MachineTime> machineTimeList)
        {
            var mlRecord = new List<MLRecord>();
            int rows = recordList.Count;
            if (rows == 0)
                return mlRecord;

            for (int k = 0; k < rows; k++)
            {

                if (k < rows - 1)  // row 0 to end-1
                {
                    DateTime dts = recordList[k].CurrentDateTime;
                    DateTime dte = recordList[k + 1].CurrentDateTime;
                    int d11 = Convert.ToInt32((dts - registDate).TotalSeconds);
                    int d12 = Convert.ToInt32((dte - registDate).TotalSeconds);
                    if (d11 > 86400) d11 = 86400;
                    if (d12 > 86400) d12 = 86400;

                    double ctactual = 0;

                    for (int n = d11; n < d12; n++)
                    {
                        if (standardTime[n] == 1)
                        {
                            ctactual++;
                        }
                    }

                    var record = new MLRecord()
                    {
                        ID = recordList[k].ID,
                        MachineID = recordList[k].MachineID,
                        CurrentDateTime = recordList[k].CurrentDateTime,
                        MTTime = recordList[k].MTTime,
                        CTime = ctactual,
                        Unknow = ctactual - recordList[k].MTTime,
                        PartNumber = recordList[k].PartNumber,
                        OKNG = recordList[k].OKNG,
                    };

                    mlRecord.Add(record);
                }
                else
                {

                    DateTime running = recordList[rows - 1].CurrentDateTime;

                    int gap = Convert.ToInt32((stopTime - running).TotalSeconds);

                    if (gap >= 0) // STOP before Time out
                    {
                        int d11 = Convert.ToInt32((running - registDate).TotalSeconds);
                        int d12 = Convert.ToInt32((stopTime - registDate).TotalSeconds);
                        if (d11 > 86400) d11 = 86400;
                        if (d12 > 86400) d12 = 86400;

                        double ctactual = 0;
                        for (int n = d11; n < d12; n++)
                        {
                            if (standardTime[n] == 1)
                            {
                                ctactual += 1;
                            }
                        }

                        var record = new MLRecord()
                        {
                            ID = recordList[k].ID,
                            MachineID = recordList[k].MachineID,
                            CurrentDateTime = recordList[k].CurrentDateTime,
                            CTime= ctactual,
                            MTTime = recordList[k].MTTime,
                            Unknow = ctactual,
                            PartNumber = recordList[k].PartNumber,
                            OKNG = recordList[k].OKNG,
                        };
                        mlRecord.Add(record);
                    }
                    else // STOP After Time out
                    {
                        // Cut 
                    }
                }

            }



            List<MLRecord> recordMHTmin = (from r in mlRecord
                                           join m in machineTimeList on new
                                           { x1 = r.MachineID, x2 = r.PartNumber }
                                           equals new
                                           { x1 = m.MachineID, x2 = m.PartNumber }
                                           select new MLRecord
                                           {
                                               ID = r.ID,
                                               MachineID = r.MachineID,
                                               CurrentDateTime = r.CurrentDateTime,
                                               PartNumber = r.PartNumber,
                                               CTime=r.CTime,
                                               MTTime = r.MTTime,
                                               Unknow = r.Unknow,
                                               UnknowNoHT = r.Unknow - m.HTminSec,
                                               MTminTime = m.MTminSec,
                                               HTminTime = m.HTminSec,
                                               OKNG = r.OKNG,
                                           }).ToList();

            return recordMHTmin;

        }


        private ResultP34 P34SpeedIdlingTimeTrue(DateTime registDate, List<MLRecord> MlRecord, List<GHOeeRaw> ghOeeRaw)
        {

            var temp1 = MlRecord;

            //========= Idling Time =========//
            List<MLRecord> idlingSet = MlRecord
               .Where(m => m.UnknowNoHT >= 2 * (m.MTminTime + m.HTminTime)).ToList();

            

            List<Loss> idling = idlingSet.GroupBy(m => m.MachineID).Select(m => new Loss
            {
                Losstime = m.Sum(u => u.UnknowNoHT)
            }).ToList();


            foreach (MLRecord m in idlingSet)
            {
                var a3log = new GHOeeRaw()
                {
                    MachineID = m.MachineID,
                    Type = 10,
                    StartMinuteTime = Convert.ToInt32(Math.Floor((m.CurrentDateTime - registDate).TotalMinutes)),
                    OccurePeriodMinute = Convert.ToInt32(Math.Ceiling(m.UnknowNoHT / 60))
                };
                ghOeeRaw.Add(a3log);
            }
            //========= Idling Time =========//


            //========= Hand speed loss  ======//

            List<MLRecord> HspeedSet = MlRecord
                .Where(m => m.UnknowNoHT > 0)
                 .Where(m => m.UnknowNoHT < 2 * (m.MTminTime + m.HTminTime)).ToList();


            List<Loss> Hspeed = HspeedSet.GroupBy(m => m.MachineID).Select(m => new Loss
            {
                Losstime = m.Sum(u => u.UnknowNoHT)
            }).ToList();


            foreach (MLRecord m in HspeedSet)
            {
                var a3log = new GHOeeRaw()
                {
                    MachineID = m.MachineID,
                    Type = 9,
                    StartMinuteTime = Convert.ToInt32(Math.Floor((m.CurrentDateTime - registDate).TotalMinutes)),
                    OccurePeriodMinute = Convert.ToInt32(Math.Ceiling(m.UnknowNoHT / 60))
                };
                ghOeeRaw.Add(a3log);
            }
            //========= Hand speed loss  ======//


            //======== Speed loss : MT ============//

            List<MLRecord> MspeedSet = MlRecord
               .Where(m => (m.MTTime - m.MTminTime) > 0).ToList();

            List<Loss> Mspeed = MspeedSet.GroupBy(m => m.MachineID).Select(ml => new Loss
            {
                Losstime = ml.Sum(m => (m.MTTime - m.MTminTime)),
            }).ToList();

            foreach (MLRecord m in MspeedSet)
            {
                var a3log = new GHOeeRaw()
                {
                    MachineID = m.MachineID,
                    Type = 11,
                    StartMinuteTime = Convert.ToInt32(Math.Floor((m.CurrentDateTime - registDate).TotalMinutes)),
                    OccurePeriodMinute = Convert.ToInt32(Math.Ceiling((m.MTTime - m.MTminTime) / 60))
                };
                ghOeeRaw.Add(a3log);
            }

            //======== Speed loss : MT ============//

            double speedloss = 0;
            double idlingloss = 0;
            double speedHandloss = 0;
            double speedMachineloss = 0;

            if (Mspeed.Count > 0)
            {
                speedloss = Mspeed[0].Losstime / 60;
                speedMachineloss += Mspeed[0].Losstime / 60;
            }
            if (Hspeed.Count > 0)
            {
                speedloss += Hspeed[0].Losstime / 60;
                speedHandloss += Hspeed[0].Losstime / 60;
            }
            if (idling.Count > 0)
            {
                idlingloss = idling[0].Losstime / 60;
            }

            var result = new ResultP34
            {
                P3 = speedloss,
                P4 = idlingloss,
                PHand= speedHandloss,
                PMachine =speedMachineloss,
                GraphOeeRaw = ghOeeRaw,
            };

            return result;
        }


        private ResultP34 P34SpeedIdlingTimeFalse(DateTime registDate, int[] standardTime, DateTime startTime, DateTime stopTime, List<GHOeeRaw> ghOeeRaw, string MachineID)
        {
            //========= Idling Time =========//

            int diff = Convert.ToInt32((stopTime - startTime).TotalSeconds);
            int d11 = Convert.ToInt32((startTime - registDate).TotalSeconds);
            int d12 = d11 + diff;

            d11 = d11 > 86400 ? 86400 : d11;
            d12 = d12 > 86400 ? 86400 : d12;
            int bfe = standardTime.Sum();
            for (int n = d11; n < d12; n++)
            {
                standardTime[n] = 0;
            }
            int aft = standardTime.Sum();
            double idl = (bfe - aft) / 60;

            var log = new GHOeeRaw()
            {
                MachineID = MachineID,
                Type = 10,      //  10: P4 Idling
                StartMinuteTime = Convert.ToInt32(d11),
                OccurePeriodMinute = Convert.ToInt32(Math.Ceiling(idl))
            };
            ghOeeRaw.Add(log);

            var result = new ResultP34
            {
                P3 = 0,
                P4 = idl,
                GraphOeeRaw = ghOeeRaw,
            };
            return result;
        }






        private ResultQ QualityCount(List<MLRecord> mlrecord)
        {
            List<CycleTime> ok = mlrecord.Where(o => o.OKNG == "OK").GroupBy(m => m.MachineID)
                .Select(n => new CycleTime
                {
                    Cycletime = n.Sum(w => (w.MTminTime + w.HTminTime) / 60)
                }).ToList();
            List<CycleTime> ng = mlrecord.Where(o => o.OKNG == "NG").GroupBy(m => m.MachineID)
                .Select(n => new CycleTime
                {
                    Cycletime = n.Sum(w => (w.MTminTime + w.HTminTime) / 60)
                }).ToList();
            List<CycleTime> re = mlrecord.Where(o => o.OKNG == "RE").GroupBy(m => m.MachineID)
                .Select(n => new CycleTime
                {
                    Cycletime = n.Sum(w => (w.MTminTime + w.HTminTime) / 60)
                }).ToList();

            var result = new ResultQ()
            {
                OKCycleTime = ok.Count > 0 ? ok[0].Cycletime : 0,
                NGCycleTime = ng.Count > 0 ? ng[0].Cycletime : 0,
                RECycleTime = re.Count > 0 ? re[0].Cycletime : 0,
            };
            return result;
        }




        #endregion

        private List<GHOeeRaw> WriteGHOeeRaw(DateTime registdate, List<Record> mlrecord, List<GHOeeRaw> ghOeeRaw)
        {

            foreach (Record m in mlrecord)
            {
                double sec = (m.CurrentDateTime - registdate).TotalSeconds;
                var g = new GHOeeRaw()
                {
                    MachineID = m.MachineID,
                    Type = 13,
                    StartMinuteTime = Convert.ToInt32(Math.Floor(sec / 60)),
                    OccurePeriodMinute = Convert.ToInt32(Math.Ceiling(m.MTTime / 60))
                };
                ghOeeRaw.Add(g);
            }
            return ghOeeRaw;
        }





        private static void WriteToCSV(DateTime dt, List<MLRecord> mlrecordlist)
        {
            var mc = mlrecordlist.FirstOrDefault().MachineID;
            var str = new StringBuilder();
            string path = $"C:\\zdebug\\{mc}_{dt:yyyy_MM_dd}-{DateTime.Now:HHmmss}.csv";
            str.Append("run,MachineId,CurrentDateTime,PartNumber,OKNG,CT = Cycle_time,MT= MT_actual,Unknow= CT - MT,UnknowNoHT = unknow - HTmin,speedHandloss = UnknowNoHT < 2*CTmin,speedMachineloss = MT_MTmin > 0,Idling = UnknowNoHT >= 2*CTmin,,MT_min,HT_min \r\n");
            foreach (var i in mlrecordlist)
            {
                double speedMachine = i.MTTime > i.MTminTime ? i.MTTime - i.MTminTime : 0;
                double idling = i.UnknowNoHT >= 2 * (i.MTminTime + i.HTminTime) ? i.UnknowNoHT : 0;
                double speedHand = i.UnknowNoHT < 2 * (i.MTminTime + i.HTminTime) ? i.UnknowNoHT : 0;
                if (speedHand < 0)
                    speedHand = 0;
                if (speedMachine < 0)
                    speedMachine = 0;


                str.AppendFormat($"{i.ID},{i.MachineID},{i.CurrentDateTime},{i.PartNumber},{i.OKNG},{i.CTime},{i.MTTime},{i.Unknow},{i.UnknowNoHT},{speedHand},{speedMachine},{idling},,{i.MTminTime},{i.HTminTime} \r\n");
            }
            File.WriteAllText(path, str.ToString());

        }

        private static void WriteRecordToCSV(DateTime dt, List<Record> mlrecordlist, string machineId)
        {
            var str = new StringBuilder();
            string path = $"C:\\zdebug\\{machineId}_{dt:yyyy_MM_dd}-{DateTime.Now:HHmmss}.csv";
            str.Append("run,MachineId,CurrentDateTime,PartNumber,OKNG,MTTime,CTime \r\n");
            foreach (Record i in mlrecordlist)
            {
                str.AppendFormat($"{i.ID},{i.MachineID},{i.CurrentDateTime},{i.PartNumber},{i.OKNG},{i.MTTime},{i.CTime} \r\n");
            }
            File.WriteAllText(path, str.ToString());

        }




    }

}
