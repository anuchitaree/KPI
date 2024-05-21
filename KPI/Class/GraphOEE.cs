using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Class
{
    
    public static class GraphOEE
    {

        #region NEW Version OEE   ============================================================

        static List<MachineTime> MachineTimelist = new List<MachineTime>();

        public static List<OEEAll> ProcessingOEE(DateTime date, string section)
        {
            var resultOverAll = new List<OEEAll>();
            var resultMC = new List<LossItem>();
            var resultLine = new OeeAPQ() { A = 1, P = 1, Q = 1, OEE = 1 };

            List<Record> RecordList = new List<Record>();

            List<MasterTime> Mastertimelist = new List<MasterTime>();



            DateTime registDateDt = new DateTime(date.Year, date.Month, date.Day);

            DateTime initDate = new DateTime(1900, 1, 1);
            int dateDiff = Convert.ToInt32((registDateDt - initDate).TotalDays);

            string registDate = registDateDt.ToString("yyyy-MM-dd");
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SS("Oee_Processing", "@pStartTime", registDate, "@section", section);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable worktingTb = ds.Tables[0];  // working  starttime,stoptime
                                                      //DataTable exclusionTb = ds.Tables[1];  // exclusion time
                DataTable exclusionTimeTb = ds.Tables[1]; //[recordTime], [minute]

                DataTable MachineId = ds.Tables[2]; // [machineId] 

                DataTable a1Tb = ds.Tables[3];  //A1: setup time table 
                DataTable a1MCTb = ds.Tables[4];  //setup time mc name 

                DataTable a2Tb = ds.Tables[5];  //A2: tool change table 
                DataTable a2MCTb = ds.Tables[6];  //tool change mc name 

                DataTable recordTb = ds.Tables[7]; //A3,P3,P4 : [mcNumber],[registDateTime],[partNumber],[mcTimeSec],[OKNG]
                DataTable a3MCTb = ds.Tables[8];  //start up time


                DataTable P1Tb = ds.Tables[9];  //P1,P2 : machine breakdown table 
                DataTable P1MCTb = ds.Tables[10];  //machine breakdown table 


                DataTable mcTime = ds.Tables[11]; //[machineID],[partNumber],[MTminSec],[HTminSec] 

                if (worktingTb.Rows.Count > 0)
                {
                    RecordList = RecordTableToList(recordTb);

                    MachineTimelist = MachineTimeToList(mcTime);

                    List<WorkingTime> WorkingTimelist = WorkingTimeToList(worktingTb, dateDiff);


                    Mastertimelist = MasterTimeProcess(registDateDt, WorkingTimelist, MachineId, exclusionTimeTb);


                    double loadingtime = LoadingTimeCalculate(Mastertimelist);






                    //====>  Loss ====================================================================================

                    Console.WriteLine($"---------------  {registDateDt:yyyy-MM-dd}   ---------------------");

                    Console.WriteLine("P1 and P2");
                    List<LossItem> P1P2 = P1P2MCBreakDown(registDateDt, P1Tb, P1MCTb, ref Mastertimelist);

                    Console.WriteLine("A1");
                    List<LossItem> A1 = A1MCSetupTime(registDateDt, a1Tb, a1MCTb, ref Mastertimelist);


                    Console.WriteLine("A2");
                    List<LossItem> A2 = A2MCDandori(registDateDt, a2Tb, a2MCTb, ref Mastertimelist);


                    Console.WriteLine("A3");
                    List<LossItem> A3 = A3MCStartup(registDateDt, RecordList, a3MCTb, WorkingTimelist, ref Mastertimelist);

                    //=================================================================================================




                    List<MLRecord> recordAddHT = RecordToListAddHandTime(registDateDt, a3MCTb, RecordList, ref Mastertimelist, WorkingTimelist, MachineTimelist);

                    //  WriteToCSV(registDateDt, recordAddHT);

                    List<LossItem> P3P4 = P3P4SpeedIdlingLoss(a3MCTb, recordAddHT);





                    List<QualityProduced> OK = QualityProcess(recordAddHT, "OK");
                    List<QualityProduced> NG = QualityProcess(recordAddHT, "NG");
                    List<QualityProduced> RE = QualityProcess(recordAddHT, "RE");


                    foreach (DataRow dr in MachineId.Rows)
                    {
                        string mc = dr.ItemArray[0].ToString();
                        var lossItem = new LossItem();
                        var a1 = A1.FirstOrDefault(m => m.MachineID == mc);
                        var a2 = A2.FirstOrDefault(m => m.MachineID == mc);
                        var a3 = A3.FirstOrDefault(m => m.MachineID == mc);
                        var p1p2 = P1P2.FirstOrDefault(m => m.MachineID == mc);
                        var p3p4 = P3P4.FirstOrDefault(m => m.MachineID == mc);

                        lossItem.A1 = a1 != null ? a1.A1 : 0;
                        lossItem.A2 = a2 != null ? a2.A2 : 0;
                        lossItem.A3 = a3 != null ? a3.A3 : 0;
                        double avilabilityTime = lossItem.A1 + lossItem.A2 + lossItem.A3;

                        double operatingTime = avilabilityTime < loadingtime ? loadingtime - avilabilityTime : 0;
                        double avilabilityRatio = Math.Round(operatingTime / loadingtime * 100, 2, MidpointRounding.AwayFromZero);
                        avilabilityRatio = double.IsNaN(avilabilityRatio) ? 0 : avilabilityRatio;


                        lossItem.P1 = p1p2 != null ? Math.Round(p1p2.P1, 2, MidpointRounding.AwayFromZero) : 0;
                        lossItem.P2 = p1p2 != null ? Math.Round(p1p2.P2, 2, MidpointRounding.AwayFromZero) : 0;
                        lossItem.P3 = p3p4 != null ? Math.Round(p3p4.P3, 2, MidpointRounding.AwayFromZero) : 0;
                        lossItem.P4 = p3p4 != null ? Math.Round(p3p4.P4, 2, MidpointRounding.AwayFromZero) : 0;

                        double performanceTime = lossItem.P1 + lossItem.P2 + lossItem.P3 + lossItem.P4;
                        double netTime = operatingTime > performanceTime ? operatingTime - performanceTime : 0;
                        double performanceRatio = Math.Round(netTime / operatingTime * 100, 2, MidpointRounding.AwayFromZero);
                        performanceRatio = double.IsNaN(performanceRatio) ? 0 : performanceRatio;



                        var ok = OK.FirstOrDefault(m => m.MachineID == mc);
                        var ng = NG.FirstOrDefault(m => m.MachineID == mc);
                        var re = RE.FirstOrDefault(m => m.MachineID == mc);
                        lossItem.OK = ok != null ? Math.Round(ok.Qty, 1, MidpointRounding.AwayFromZero) : 0;
                        lossItem.NG = ng != null ? Math.Round(ng.Qty, 1, MidpointRounding.AwayFromZero) : 0;
                        lossItem.RE = re != null ? Math.Round(re.Qty, 1, MidpointRounding.AwayFromZero) : 0;


                        double totalWork = lossItem.OK + lossItem.NG + lossItem.RE;
                        double qualityRatio = Math.Round(lossItem.OK / totalWork * 100, 2, MidpointRounding.AwayFromZero);
                        qualityRatio = double.IsNaN(qualityRatio) ? 0 : qualityRatio;


                        double MCOee = Math.Round(avilabilityRatio * performanceRatio * qualityRatio / 10000, 2, MidpointRounding.AwayFromZero);
                        MCOee = double.IsNaN(MCOee) ? 0 : MCOee;

                        resultMC.Add(new LossItem
                        {
                            MachineID = mc,

                            LoadingTime = loadingtime,

                            A1 = lossItem.A1,
                            A2 = lossItem.A2,
                            A3 = lossItem.A3,
                            AvailabilityPercent = avilabilityRatio,

                            P1 = lossItem.P1,
                            P2 = lossItem.P2,
                            P3 = lossItem.P3,
                            P4 = lossItem.P4,
                            PerformancePercent = performanceRatio,

                            OK = lossItem.OK,
                            NG = lossItem.NG,
                            RE = lossItem.RE,
                            QaulityPercent = qualityRatio,

                            OEE = MCOee,
                        });

                    }

                    OeeAPQ oeeline = (from n in resultMC
                                      orderby n.OEE ascending
                                      select new OeeAPQ
                                      {
                                          A = n.AvailabilityPercent,
                                          P = n.PerformancePercent,
                                          Q = n.QaulityPercent,
                                          OEE = n.OEE,
                                      }).First();


                    resultOverAll.Add(new OEEAll { OEEMC = resultMC, OEELINE = oeeline });

                    var sql10 = new SqlClass();
                   // sql10.OEEMCandLine(section, registDate, resultMC, oeeline);

                    Console.WriteLine("{0} is normal", registDate);

                }
                else
                {
                    var sql12 = new SqlClass();
                    sql12.OEEMCandLineDel(section, registDate);

                    Console.WriteLine("{0} is NOT MP NOT registed", registDate);
                }


            }
            return resultOverAll;

        }


        //=================   Loading Time   ===========


        private static double LoadingTimeCalculate(List<MasterTime> masterTimeList)
        {
            int[] master = masterTimeList[0].Mastertime;
            int result = master.Sum();
            return result / 60;
        }


        #region  A1.Setup time model change  

        //=================   A1.Setup time model change   ===========

        private static List<LossItem> A1MCSetupTime(DateTime calculateday, DataTable lossTB, DataTable MC, ref List<MasterTime> masterT)
        {


            var lossItemlist = new List<LossItem>();
            if (lossTB.Rows.Count > 1)
            {
                int r = MC.Rows.Count;
                for (int i = 0; i < r; i++)
                {
                    string mcName = MC.Rows[i].ItemArray[0].ToString();
                    string _sqlWhere = string.Format("mcid = '{0}'", mcName);
                    DataTable _newDataTable = lossTB.Select(_sqlWhere).CopyToDataTable();
                    if (_newDataTable.Rows.Count > 0)
                    {
                        var lossItem = new LossItem();
                        double mcSetup = MCSetupTimeDandori(calculateday, _newDataTable, mcName, ref masterT);
                        lossItem.MachineID = mcName;
                        lossItem.A1 = mcSetup;
                        lossItemlist.Add(lossItem);
                    }
                }
            }
            return lossItemlist;
        }


        #endregion 


        #region A2.Cutting tool change 

        //=================   A2.Cutting tool change     ===========//

        private static List<LossItem> A2MCDandori(DateTime calculateday, DataTable lossTB, DataTable MC, ref List<MasterTime> masterT)
        {
            var lossItemlist = new List<LossItem>();
            if (lossTB.Rows.Count > 1)
            {
                int r = MC.Rows.Count;
                for (int i = 0; i < r; i++)
                {
                    string mcName = MC.Rows[i].ItemArray[0].ToString();
                    string _sqlWhere = string.Format("mcid = '{0}'", mcName);
                    DataTable _newDataTable = lossTB.Select(_sqlWhere).CopyToDataTable();
                    if (_newDataTable.Rows.Count > 0)
                    {
                        var lossItem = new LossItem();
                        double mcDandori = MCSetupTimeDandori(calculateday, _newDataTable, mcName, ref masterT);
                        lossItem.MachineID = mcName;
                        lossItem.A2 = mcDandori;
                        lossItemlist.Add(lossItem);
                    }
                }
            }
            return lossItemlist;
        }



        private static double MCSetupTimeDandori(DateTime calculateday, DataTable TB, string mcname, ref List<MasterTime> masterT)
        {
            int yy = calculateday.Year; int mm = calculateday.Month; int dd = calculateday.Day;
            DateTime dateTimeStart = new DateTime(yy, mm, dd, 07, 30, 00);

            int row = TB.Rows.Count;

            double HT = 0;

            var dbMasterTime = masterT.Where(m => m.MachineID == mcname).FirstOrDefault();
            int[] masterTime = dbMasterTime.Mastertime;
            int[] updateMaster = masterTime;






            for (int i = 0; i < row; i++)
            {
                DateTime dt1 = Convert.ToDateTime(TB.Rows[i].ItemArray[1]);
                DateTime dt2 = Convert.ToDateTime(TB.Rows[i].ItemArray[2]);
                int d11 = Convert.ToInt32((dt1 - dateTimeStart).TotalSeconds);
                int d12 = Convert.ToInt32((dt2 - dateTimeStart).TotalSeconds);
                if (d11 > 86400) d12 = 86400;
                if (d12 > 86400) d12 = 86400;

                for (int n = d11; n < d12; n++)
                {
                    updateMaster[n] = 0;
                }

                if (TB.Rows[i].ItemArray[3] != null)
                {
                    string partnumber = TB.Rows[i].ItemArray[3].ToString();

                    MachineTime db = MachineTimelist.Where(m => m.MachineID == mcname).Where(p => p.PartNumber == partnumber).FirstOrDefault();
                    if (db != null)
                    {
                        HT += db.HTminSec;
                    }

                }

            }



            int beforetime = masterTime.Sum();
            int aftertime = updateMaster.Sum();

            foreach (var master in masterT.Where(m => m.MachineID == mcname))
            {
                master.Mastertime = updateMaster;
            }


            return (beforetime - aftertime - HT) / 60;
        }



        #endregion 


        #region A3.Start up time 

        //=================   A3.Start up time    ===========//
        private static List<LossItem> A3MCStartup(DateTime calDay, List<Record> MlRecord, DataTable mcTable, List<WorkingTime> workingTimeList, ref List<MasterTime> masterT)
        {
            var lossItemlist = new List<LossItem>();
            int row = mcTable.Rows.Count;
            List<MasterTime> master = masterT;
            if (row > 1)
            {
                for (int i = 0; i < row; i++)
                {
                    string mcName = mcTable.Rows[i].ItemArray[0].ToString();

                    var db = MlRecord.Where(m => m.MachineId == mcName).OrderBy(m => m.CurrentDateTime);

                    if (db != null)
                    {
                        List<Record> mlRecord = db.ToList();

                        var lossitem = new LossItem
                        {
                            MachineID = mcName,
                            A3 = StartUpTime(calDay, mlRecord, workingTimeList, mcName, ref master)
                        };
                        lossItemlist.Add(lossitem);
                    }
                }
            }
            masterT = master;
            return lossItemlist;
        }



        private static double StartUpTime(DateTime calculateday, List<Record> mlRecord, List<WorkingTime> workingTimeList, string mcname, ref List<MasterTime> masterT)
        {
            int loop = 0;
            int row = mlRecord.Count;
            double a3 = 0;


            var dbMasterTime = masterT.Where(m => m.MachineID == mcname).FirstOrDefault();
            int[] masterTime = dbMasterTime.Mastertime;



            if (mcname == "T6SMM0017200")
            {
                Console.ReadLine();
                // bb("T6SMM0017200", masterTime, calculateday);
            }

            if (mcname == "T6PCC0000700")
            {
                Console.ReadLine();
                //bb("T6PCC0000700", masterTime, calculateday);
            }
            //string str = "MachineID || hour No : Period  \n";/* str1 = "MachineID || run number : Period  \n"; //**/

            int yy = calculateday.Year; int mm = calculateday.Month; int dd = calculateday.Day;
            DateTime dateTimeStart = new DateTime(yy, mm, dd, 07, 30, 00);

            for (int i = 1; i < 20; i += 2)
            {
                List<Startup> starttime = (from w in workingTimeList
                                           where w.HourNo == i
                                           select new Startup { StartupTime = w.StartTime }).ToList();
                if (starttime.Count > 0)
                {

                    while (loop < row)
                    {
                        DateTime dtr = Convert.ToDateTime(mlRecord[loop].CurrentDateTime).AddSeconds(mlRecord[loop].MTTime);

                        double period = PeriodWithOutBreakForword(dateTimeStart, starttime[0].StartupTime, dtr, masterTime);


                        if (period > 0)
                        {
                            i = NextChecKPointOfStartupTime(workingTimeList, i, dtr);

                            if (loop == 0)
                            {
                                a3 += Convert.ToDouble(period);
                                UpdateMasterTime(dateTimeStart, starttime[0].StartupTime, dtr, ref masterTime);

                                //str += $"{mcname} | {starttime[0].StartupTime} : {period} \n";

                                //if (mcname == "T6TMM0004000")
                                //{
                                //    Console.ReadLine();
                                //}
                            }

                            if (loop > 0)
                            {
                                dtr = Convert.ToDateTime(mlRecord[loop - 1].CurrentDateTime).AddSeconds(mlRecord[loop - 1].MTTime);

                                double period2 = PeriodWithOutBreakReverse(dateTimeStart, starttime[0].StartupTime, dtr, masterTime);

                                if (period2 > 300 || period2 == 0)
                                {
                                    a3 += Convert.ToDouble(period);
                                    UpdateMasterTime(dateTimeStart, starttime[0].StartupTime, dtr, ref masterTime);

                                    // str += $"{mcname} | {starttime[0].StartupTime} : {period} \n";

                                    //if (mcname == "T6TMM0004000")
                                    //{
                                    //    Console.ReadLine();
                                    //}
                                }

                            }
                            loop++;
                            break;
                        }
                        loop++;

                    }

                }


            }

            //** For test
            //if (mcname == "T6TMM0004000")
            //{
            //    Console.WriteLine(str);
            //    Console.ReadLine();
            //}


            foreach (var mstime in masterT.Where(m => m.MachineID == mcname))
            {
                mstime.Mastertime = masterTime;
            }

            double result = Math.Round(a3 / 60, 2, MidpointRounding.AwayFromZero);

            return result;

        }



        private static double PeriodWithOutBreakForword(DateTime dateTimeStart, DateTime checkTime, DateTime dtr, int[] masterTime)
        {
            if (dtr.Year == 1900)
                return 0;
            int beforetime = masterTime.Sum();

            int d11 = Convert.ToInt32((checkTime - dateTimeStart).TotalSeconds);
            int d12 = Convert.ToInt32((dtr - dateTimeStart).TotalSeconds);
            d11 = d11 > 86400 ? 86400 : d11;
            d12 = d12 > 86400 ? 86400 : d12;
            if (d11 < d12)
            {
                for (int n = d11; n < d12; n++)
                {
                    masterTime[n] = 0;
                }

            }

            int aftertime = masterTime.Sum();
            return (beforetime - aftertime);
        }



        private static double PeriodWithOutBreakReverse(DateTime dateTimeStart, DateTime checkTime, DateTime dtr, int[] masterTime)
        {
            if (dtr.Year == 1900)
                return 0;
            int beforetime = masterTime.Sum();

            int d11 = Convert.ToInt32((checkTime - dateTimeStart).TotalSeconds);
            int d12 = Convert.ToInt32((dtr - dateTimeStart).TotalSeconds);
            d11 = d11 > 86400 ? 86400 : d11;
            d12 = d12 > 86400 ? 86400 : d12;
            if (d11 > d12 && d11 > 0 && d12 > 0)
            {
                for (int n = d12; n < d11; n++)
                {
                    masterTime[n] = 0;
                }
            }
            int aftertime = masterTime.Sum();
            return (beforetime - aftertime);
        }




        private static int NextChecKPointOfStartupTime(List<WorkingTime> workingTimeList, int HourNO, DateTime dtr)
        {

            int nextpoint = HourNO;
            for (int i = HourNO; i < 20; i += 2)
            {
                List<Startup> starttime = (from w in workingTimeList
                                           where w.HourNo == i
                                           select new Startup { StartupTime = w.StartTime }).ToList();
                if (starttime.Count > 0)
                {
                    int period = Convert.ToInt32((starttime[0].StartupTime - dtr).TotalSeconds);
                    if (period >= 0)
                    {
                        nextpoint = i > 1 ? i - 2 : 1;
                        return nextpoint;
                        //    break;
                    }

                }

            }

            return 20;
        }




        private static void UpdateMasterTime(DateTime dateTimeStart, DateTime checkTime, DateTime dtr, ref int[] masterTime)
        {
            if (dtr.Year == 1900)
                return;

            int d11 = Convert.ToInt32((checkTime - dateTimeStart).TotalSeconds);
            int d12 = Convert.ToInt32((dtr - dateTimeStart).TotalSeconds);
            d11 = d11 > 86400 ? 86400 : d11;
            d12 = d12 > 86400 ? 86400 : d12;
            if (d11 < d12)
            {
                for (int n = d11; n < d12; n++)
                {
                    masterTime[n] = 0;
                }

            }

        }


        #endregion 


        #region  P1.Equipment failure: more than 5 min  , P2.Minor stops : less or equal 5 min

        //================  Performance Efficiency ================//
        //================  P1.Equipment failure: more than 5 min ==// 
        //================  P2.Minor stops : less or equal 5 min ===//

        private static void LossTimeWithOutBreakTime(DateTime caldate, DataTable dtlossTB, List<MasterTime> masterT)
        {
            // McId,dateTimeStart,dateTimeEnd,partnumberEnd,run

            int[] masterTime = masterT[0].Mastertime;

            var loss = new List<P1P2Loss>();

            int row = dtlossTB.Rows.Count;
            if (row > 0)
            {
                for (int i = 0; i < row; i++)
                {
                    var lossitem = new P1P2Loss();
                    DateTime dt1 = Convert.ToDateTime(dtlossTB.Rows[i].ItemArray[1]);
                    DateTime dt2 = Convert.ToDateTime(dtlossTB.Rows[i].ItemArray[2]);
                    lossitem.run = Convert.ToString(dtlossTB.Rows[i].ItemArray[4]);
                    lossitem.Second = CalculateUpdateLossTable(caldate, dt1, dt2, masterTime);
                    loss.Add(lossitem);
                }

                var sql = new SqlClass();
             //   sql.UpdateLossRecordTabelDB(loss);
            }
        }



        private static double CalculateUpdateLossTable(DateTime calculateday, DateTime dt1, DateTime dt2, int[] masterTime)
        {
            int yy = calculateday.Year; int mm = calculateday.Month; int dd = calculateday.Day;
            DateTime dateTimeStart = new DateTime(yy, mm, dd, 07, 30, 00);
            int d11 = Convert.ToInt32((dt1 - dateTimeStart).TotalSeconds);
            int d12 = Convert.ToInt32((dt2 - dateTimeStart).TotalSeconds);
            if (d11 > 86400) d11 = 86400;
            if (d12 > 86400) d12 = 86400;

            int before = masterTime.Sum();
            if (d12 > d11)
            {
                for (int n = d11; n < d12; n++)
                {
                    masterTime[n] = 0;
                }
            }

            int after = masterTime.Sum();

            return (before - after);
        }




        private static List<LossItem> P1P2MCBreakDown(DateTime calculateday, DataTable lossTB, DataTable MC, ref List<MasterTime> masterT)
        {
            var lossItemlist = new List<LossItem>();

            if (lossTB.Rows.Count > 1)
            {
                var update = new List<P1P2Loss>();
                int r = MC.Rows.Count;
                for (int i = 0; i < r; i++)
                {
                    string mcName = MC.Rows[i].ItemArray[0].ToString();
                    string _sqlWhere = string.Format("McId = '{0}'", mcName);
                    DataTable _newDataTable = lossTB.Select(_sqlWhere).CopyToDataTable();
                    if (_newDataTable.Rows.Count > 0)
                    {
                        LossP1P2 P1P2 = MCBreakDown(calculateday, _newDataTable, mcName, ref masterT, ref update);
                        var lossItem = new LossItem
                        {
                            MachineID = mcName,
                            P1 = P1P2.P1,
                            P2 = P1P2.P2
                        };
                        lossItemlist.Add(lossItem);
                    }
                }

                var sql = new SqlClass();
              //  sql.UpdateLossRecordTabelDB(update);
            }
            return lossItemlist;
        }

        private static LossP1P2 MCBreakDown(DateTime calculateday, DataTable TB, string mcname, ref List<MasterTime> masterT, ref List<P1P2Loss> update)
        {
            var losst = new LossP1P2();

            var dbMasterTime = masterT.Where(m => m.MachineID == mcname).FirstOrDefault();
            int[] masterTime = dbMasterTime.Mastertime;

            int[] newMasterTime = masterTime; //new int[87000];

            int yy = calculateday.Year; int mm = calculateday.Month; int dd = calculateday.Day;
            DateTime dateTimeStart = new DateTime(yy, mm, dd, 07, 30, 00);

            int row = TB.Rows.Count;

            //bool[] lossP1 = new bool[86400];
            //bool[] lossP2 = new bool[86400];

            double HTP1 = 0, HTP2 = 0; ;


            if (mcname == "T6SMM0017200")
            {
                //bb("master", masterTime);
                //bb("New", newMasterTime);

                Console.WriteLine(newMasterTime.Sum());
            }

            var str = new StringBuilder();
            str.Append("Machine,P1 loss ,P2 loss \n");

            for (int i = 0; i < row; i++)
            {
                bool[] loss = new bool[86400];
                DateTime dt1 = Convert.ToDateTime(TB.Rows[i].ItemArray[1]);
                DateTime dt2 = Convert.ToDateTime(TB.Rows[i].ItemArray[2]);
                int d11 = Convert.ToInt32((dt1 - dateTimeStart).TotalSeconds);
                int d12 = Convert.ToInt32((dt2 - dateTimeStart).TotalSeconds);
                if (d11 > 86400) d11 = 86400;
                if (d12 > 86400) d12 = 86400;



                int whatLoss = 0;

                int before = newMasterTime.Sum();

                for (int n = d11; n < d12; n++)
                {
                    newMasterTime[n] = 0;
                }
                int after = newMasterTime.Sum();

                whatLoss = before - after;

                Console.WriteLine($"{calculateday}, T6SMM0017200 =>> before ={dt1:yyyy-MM-dd HH:mm:ss} : {before} sec, after= {dt2:yyyy-MM-dd HH:mm:ss} : {after } sec");

                if (mcname == "T6SMM0017200")
                {
                    //bb((i + 20).ToString(), newMasterTime);

                }


                double HT = 0;
                if (TB.Rows[i].ItemArray[3] != null && whatLoss > 0)
                {
                    string partnumber = TB.Rows[i].ItemArray[3].ToString();

                    MachineTime db = MachineTimelist
                        .Where(m => m.MachineID == mcname)
                        .Where(p => p.PartNumber == partnumber)
                        .FirstOrDefault();
                    if (db != null)
                    {
                        HT += db.HTminSec;
                    }

                }

                if (whatLoss > 5 * 60) // P1
                {
                    HTP1 += whatLoss - HT;
                }
                else
                {
                    HTP2 += whatLoss - HT;
                }

                str.AppendFormat($"{mcname},{HTP1},{HTP2} \n");

                if (mcname == "T6SMM0017200")
                {
                    Console.WriteLine($"{calculateday}, T6SMM0017200 =>> P1={HTP1 / 60} min, P2= {HTP2 / 60} min");
                }

                var item = new P1P2Loss();
                item.Second = whatLoss;
                item.run = Convert.ToString(TB.Rows[i].ItemArray[4]);
                update.Add(item);
            }


            // aa(mcname, str.ToString());

            losst.P1 = HTP1 / 60;
            losst.P2 = HTP2 / 60;

            foreach (var msTime in masterT.Where(m => m.MachineID == mcname))
            {
                msTime.Mastertime = newMasterTime;
            }




            if (mcname == "T6SMM0017200")
            {
                Console.WriteLine($"{calculateday}, T6SMM0017200 =>> losst.P1={ losst.P1} min, losst.P2= {losst.P2} min , summary = { (losst.P1 + losst.P2)}");

                bb("master", masterTime, calculateday);
                bb("modify", newMasterTime, calculateday);


            }



            return losst;
        }








        private static List<MasterTime> MasterTimeProcess(DateTime dt, List<WorkingTime> workingTimelist, DataTable mcname, DataTable exclusionTb)
        {
            var mastertime = new List<MasterTime>();
            //int test = 0;
            int yy = dt.Year; int mm = dt.Month; int dd = dt.Day;
            DateTime startTime = new DateTime(yy, mm, dd, 7, 30, 00);

            int[] masterTime = new int[86400];
            int i = 0;
            foreach (var work in workingTimelist)
            {
                DateTime ts = Convert.ToDateTime(work.StartTime);
                DateTime te = Convert.ToDateTime(work.StopTime);
                int d11 = Convert.ToInt32((ts - startTime).TotalSeconds);
                int d12 = Convert.ToInt32((te - startTime).TotalSeconds);
                d11 = (d11 > 86400) ? 86400 : d11;
                d12 = (d12 > 86400) ? 86400 : d12;
                for (int n = d11; n < d12; n++)
                {
                    masterTime[n] = 1;
                }
                i++;
            }

            foreach (DataRow exc in exclusionTb.Rows)
            {
                DateTime ts = Convert.ToDateTime(exc.ItemArray[0]);
                int d11 = Convert.ToInt32((ts - startTime).TotalSeconds);
                int d12 = d11 + Convert.ToInt32(exc.ItemArray[1]) * 60;
                d11 = (d11 > 86400) ? 86400 : d11;
                d12 = (d12 > 86400) ? 86400 : d12;
                for (int n = d11; n <= d12; n++)
                {
                    masterTime[n] = 0;
                }
            }

            foreach (DataRow dr in mcname.Rows)
            {
                var master = new MasterTime()
                {
                    MachineID = dr.ItemArray[0].ToString(),
                    Mastertime = masterTime,
                };
                mastertime.Add(master);
            }

            return mastertime;
        }



        #endregion  P1.Equipment failure: more than 5 min  , P2.Minor stops : less or equal 5 min



        #region   P3, P4  RecordToListAddHandTime

        //===  P3P4.Speed & Idling loss (MC time)     ==//  

        private static List<MLRecord> RecordToListAddHandTime(DateTime registDate, DataTable mcTable, List<Record> recordTolist
          , ref List<MasterTime> masterT, List<WorkingTime> workingTimelist, List<MachineTime> machineTimelist)
        {
            var mlRecord = new List<MLRecord>();

            int row = mcTable.Rows.Count;
            if (row > 0)
            {

                int yy = registDate.Year; int mm = registDate.Month; int dd = registDate.Day;
                DateTime dateTimeStart = new DateTime(yy, mm, dd, 07, 30, 00);

                for (int i = 0; i < row; i++)   // each machine 
                {
                    string mcName = mcTable.Rows[i].ItemArray[0].ToString();

                    List<Record> db = recordTolist
                        .Where(m => m.MachineId == mcName)
                        .Where(c => c.CurrentDateTime >= dateTimeStart)
                        .OrderBy(r => r.CurrentDateTime)
                        .ToList();

                    var dbMasterTime = masterT.Where(m => m.MachineID == mcName).FirstOrDefault();
                    int[] masterTime = dbMasterTime.Mastertime;


                    int rows = db.Count;

                    for (int k = 0; k < rows; k++)
                    {

                        if (k < rows - 1)  // row 0 to end-1
                        {

                            DateTime dts = db[k].CurrentDateTime;
                            DateTime dte = db[k + 1].CurrentDateTime;

                            int d11 = Convert.ToInt32((dts - dateTimeStart).TotalSeconds);
                            int d12 = Convert.ToInt32((dte - dateTimeStart).TotalSeconds);
                            if (d11 > 86400) d11 = 86400;
                            if (d12 > 86400) d12 = 86400;


                            double ctactual = 0;



                            for (int n = d11; n < d12; n++)
                            {
                                if (masterTime[n] == 1)
                                {
                                    ctactual++;
                                }
                            }

                            var record = new MLRecord()
                            {
                                ID = db[k].ID,
                                MachineId = db[k].MachineId,
                                CurrentDateTime = db[k].CurrentDateTime,
                                MTTime = db[k].MTTime,
                                Unknow = ctactual - db[k].MTTime,
                                PartNumber = db[k].PartNumber,
                                OKNG = db[k].OKNG,
                            };

                            mlRecord.Add(record);
                        }
                        else
                        {

                            WorkingTime dbw = workingTimelist.OrderByDescending(h => h.HourNo).FirstOrDefault();
                            DateTime stopTime = dbw.StopTime;
                            DateTime running = db[rows - 1].CurrentDateTime;

                            int gap = Convert.ToInt32((stopTime - running).TotalSeconds);
                            if (gap <= 0)
                            {
                                var record = new MLRecord()
                                {
                                    ID = db[k].ID,
                                    MachineId = db[k].MachineId,
                                    CurrentDateTime = db[k].CurrentDateTime,
                                    MTTime = db[k].MTTime,
                                    Unknow = 0,
                                    PartNumber = db[k].PartNumber,
                                    OKNG = db[k].OKNG,
                                };
                                mlRecord.Add(record);
                            }
                            else if (gap > 0)  // หยุก ก่อน หมดเวลา
                            {

                                int d11 = Convert.ToInt32((running - dateTimeStart).TotalSeconds);
                                int d12 = Convert.ToInt32((stopTime - dateTimeStart).TotalSeconds);
                                if (d11 > 86400) d11 = 86400;
                                if (d12 > 86400) d12 = 86400;

                                double ctactual = 0;
                                for (int n = d11; n < d12; n++)
                                {
                                    if (masterTime[n] == 1)
                                    {
                                        ctactual += 1;
                                    }
                                }

                                var record = new MLRecord()
                                {
                                    ID = db[k].ID,
                                    MachineId = db[k].MachineId,
                                    CurrentDateTime = db[k].CurrentDateTime,
                                    MTTime = db[k].MTTime,
                                    Unknow = ctactual,
                                    PartNumber = db[k].PartNumber,
                                    OKNG = db[k].OKNG,
                                };
                                mlRecord.Add(record);

                            }
                        }

                    }

                }
            }


            List<MLRecord> recordMHTmin = (from r in mlRecord
                                           join m in machineTimelist on new { x1 = r.MachineId, x2 = r.PartNumber } equals new { x1 = m.MachineID, x2 = m.PartNumber }
                                           select new MLRecord
                                           {
                                               ID = r.ID,
                                               MachineId = r.MachineId,
                                               CurrentDateTime = r.CurrentDateTime,
                                               PartNumber = r.PartNumber,
                                               MTTime = r.MTTime,
                                               Unknow = r.Unknow,
                                               UnknowNoHT = r.Unknow - m.HTminSec,
                                               MTminTime = m.MTminSec,
                                               HTminTime = m.HTminSec,
                                               OKNG = r.OKNG,
                                           }).ToList();

            return recordMHTmin;
        }



        private static List<LossItem> P3P4SpeedIdlingLoss(DataTable mcTable, List<MLRecord> MlRecord)
        {
            var lossItemlist = new List<LossItem>();
            int row = mcTable.Rows.Count;
            if (row > 1)
            {
                for (int i = 0; i < row; i++)
                {
                    string mcName = mcTable.Rows[i].ItemArray[0].ToString();

                    //if (mcName == "T6TMM0004000")
                    //    Console.WriteLine();

                    // Idling loss
                    List<SpeedAndIdlingloss> idling = MlRecord
                        .Where(m => m.MachineId == mcName)
                        .Where(m => m.UnknowNoHT >= 2 * (m.MTminTime + m.HTminTime))
                        .GroupBy(m => m.MachineId)
                        .Select(m => new SpeedAndIdlingloss
                        {
                            Loss = m.Sum(u => u.UnknowNoHT)
                        }).ToList();



                    // Hand speed loss
                    List<SpeedAndIdlingloss> Hspeed = MlRecord
                        .Where(m => m.MachineId == mcName)
                        .Where(m => m.UnknowNoHT > 0)
                         .Where(m => m.UnknowNoHT < 2 * (m.MTminTime + m.HTminTime))
                        .GroupBy(m => m.MachineId)
                        .Select(m => new SpeedAndIdlingloss
                        {
                            Loss = m.Sum(u => u.UnknowNoHT)
                        }).ToList();



                    // Speed loss : MT
                    List<SpeedAndIdlingloss> Mspeed = MlRecord
                       .Where(m => m.MachineId == mcName)
                       .Where(m => (m.MTTime - m.MTminTime) > 0)
                       .GroupBy(m => m.MachineId)
                       .Select(ml => new SpeedAndIdlingloss
                       {
                           Loss = ml.Sum(m => (m.MTTime - m.MTminTime)),
                       }).ToList();

                    double speedloss = 0;
                    double idlingloss = 0;
                    if (Mspeed.Count > 0)
                    {
                        speedloss = Mspeed[0].Loss / 60;
                    }
                    if (Hspeed.Count > 0)
                    {
                        speedloss += Hspeed[0].Loss / 60;
                    }
                    if (idling.Count > 0)
                    {
                        idlingloss = idling[0].Loss / 60;
                    }



                    var lossitem = new LossItem
                    {
                        MachineID = mcName,
                        P3 = speedloss,
                        P4 = idlingloss,
                    };
                    lossItemlist.Add(lossitem);

                }
            }
            return lossItemlist;


        }



        //===  Preparation the List from DataTable     ==// 

        private static List<Record> RecordTableToList(DataTable RecordTable)
        {
            DateTime defaulttime = new DateTime(1900, 1, 1, 0, 0, 0);
            List<Record> record = new List<Record>();
            record.Clear();

            foreach (DataRow dr in RecordTable.Rows)
            {
                var ml = new Record
                {
                    ID = dr.ItemArray[0].ToString(),
                    MachineId = dr.ItemArray[1] == null ? "" : dr.ItemArray[1].ToString(),
                    CurrentDateTime = String.IsNullOrEmpty(dr.ItemArray[2].ToString()) == true ? defaulttime : Convert.ToDateTime(dr.ItemArray[2]),
                    PartNumber = dr.ItemArray[3] == null ? "" : dr.ItemArray[3].ToString(),
                    MTTime = dr.ItemArray[4] == null ? 0 : Convert.ToDouble(dr.ItemArray[4]),
                    OKNG = dr.ItemArray[5] == null ? "" : dr.ItemArray[5].ToString(),
                };
                record.Add(ml);

            }
            return record;
        }


        private static List<MachineTime> MachineTimeToList(DataTable mcTime)
        {
            List<MachineTime> record = new List<MachineTime>();
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


        private static List<WorkingTime> WorkingTimeToList(DataTable workingTime, int dateDiff)
        {
            var WorkingTimeTable = new List<WorkingTime>();
            WorkingTimeTable.Clear();
            foreach (DataRow dr in workingTime.Rows)
            {
                var wt = new WorkingTime
                {
                    HourNo = Convert.ToInt32(dr.ItemArray[2]),
                    StartTime = Convert.ToDateTime(dr.ItemArray[0]).AddDays(dateDiff),
                    StopTime = Convert.ToDateTime(dr.ItemArray[1]).AddDays(dateDiff),
                };
                WorkingTimeTable.Add(wt);
            }
            return WorkingTimeTable;
        }

        #endregion



        #region Q : Defectives  and Rework

        //===========  Q Quality ==========//

        private static List<QualityProduced> QualityProcess(List<MLRecord> mlrecord, string oKnGrE)
        {
            List<QualityProduced> ok = mlrecord.Where(o => o.OKNG == oKnGrE).GroupBy(m => m.MachineId)
                .Select(n => new QualityProduced
                {
                    MachineID = n.Key,
                    Qty = n.Sum(w => (w.MTminTime + w.HTminTime) / 60),
                }).ToList();

            return ok;
        }


        #endregion  Defectives  and Rework


        #region Write CSV

        private static void WriteToCSV(DateTime dt, List<MLRecord> mlrecordlist)
        {
            var id = mlrecordlist.Where(m => m.MachineId == "T6TMM0004000")
                  .Where(m => m.UnknowNoHT >= 2 * (m.MTminTime + m.HTminTime))
                  .Select(m => new MLRecord
                  {
                      ID = m.ID,
                      MachineId = m.MachineId,
                      CurrentDateTime = m.CurrentDateTime,
                      PartNumber = m.PartNumber,
                      MTTime = m.MTTime,
                      Unknow = m.Unknow,
                      UnknowNoHT = m.UnknowNoHT,
                      Speed = 0,
                      SpeedM = 0,
                      Idling = m.UnknowNoHT,
                      MTminTime = m.MTminTime,
                      HTminTime = m.HTminTime,

                  }).ToList();

            var spht = mlrecordlist.Where(m => m.MachineId == "T6TMM0004000")
                .Where(m => m.UnknowNoHT > 0)
                .Where(m => m.UnknowNoHT < 2 * (m.MTminTime + m.HTminTime))
                .Select(m => new MLRecord
                {
                    ID = m.ID,
                    MachineId = m.MachineId,
                    CurrentDateTime = m.CurrentDateTime,
                    PartNumber = m.PartNumber,
                    MTTime = m.MTTime,
                    Unknow = m.Unknow,
                    UnknowNoHT = m.UnknowNoHT,
                    Speed = m.UnknowNoHT,
                    SpeedM = 0,
                    Idling = 0,
                    MTminTime = m.MTminTime,
                    HTminTime = m.HTminTime,

                });

            var spmt = mlrecordlist.Where(m => m.MachineId == "T6TMM0004000")
               .Where(m => m.MTTime > m.MTminTime)
               .Select(m => new MLRecord
               {
                   ID = m.ID,
                   MachineId = m.MachineId,
                   CurrentDateTime = m.CurrentDateTime,
                   PartNumber = m.PartNumber,
                   MTTime = m.MTTime,
                   Unknow = m.Unknow,
                   UnknowNoHT = m.UnknowNoHT,
                   Speed = 0,
                   SpeedM = m.MTTime - m.MTminTime,
                   Idling = 0,
                   MTminTime = m.MTminTime,
                   HTminTime = m.HTminTime,

               });


            var database = new List<MLRecord>();

            foreach (var i in mlrecordlist.Where(m => m.MachineId == "T6TMM0004000"))
            {
                var m = new MLRecord();
                var id1 = id.FirstOrDefault(x => x.ID == i.ID);
                var ht1 = spht.FirstOrDefault(x => x.ID == i.ID);
                var mt1 = spmt.FirstOrDefault(x => x.ID == i.ID);

                m.ID = i.ID;
                m.MachineId = i.MachineId;
                m.PartNumber = i.PartNumber;
                m.CurrentDateTime = i.CurrentDateTime;
                m.MTTime = i.MTTime;
                m.Unknow = i.Unknow;
                m.UnknowNoHT = i.UnknowNoHT;
                m.Speed = ht1 != null ? ht1.Speed : 0;
                m.SpeedM = mt1 != null ? mt1.SpeedM : 0;
                m.Idling = id1 != null ? id1.Idling : 0;
                m.MTminTime = i.MTminTime;
                m.HTminTime = i.HTminTime;
                database.Add(m);
            }

            var str = new StringBuilder();
            string path = $"C:\\mlRecord{dt:yyyy_MM_dd}-{DateTime.Now:HHmmss}.csv";
            str.Append("run,MachineId,CurrentDateTime,PartNumber,OKNG,MTTime,Unknow,UnknowNoHT,Speed,SpeedM,Idling,,MTminTime,HTminTime \r\n");
            foreach (var i in database)
            {
                str.AppendFormat($"{i.ID},{i.MachineId},{i.CurrentDateTime},{i.PartNumber},{i.OKNG},{i.MTTime},{i.Unknow},{i.UnknowNoHT},{i.Speed},{i.SpeedM},{i.Idling},,{i.MTminTime},{i.HTminTime} \r\n");
            }
            File.WriteAllText(path, str.ToString());

        }


        private static void aa(string id, string database)
        {
            DateTime dt = DateTime.Now;
            var str = new StringBuilder();
            string path = $"C:\\zDebug\\{id}_{dt:yyyy_MM_dd}-{DateTime.Now:HHmmss}.csv";
            str.Append("No,boolean \r\n");

            File.WriteAllText(path, database);


        }

        private static void bb(string id, int[] booleanarray, DateTime calculateday)
        {
            DateTime dt = DateTime.Now;
            var str = new StringBuilder();
            string path = $"C:\\zDebug\\{id}_{dt:yyyy_MM_dd}-{DateTime.Now:HHmmss}.csv";
            str.Append("No,datetime,work 1,sum \r\n");

            DateTime dts = new DateTime(calculateday.Year, calculateday.Month, calculateday.Day, 7, 30, 00);
            var test = new List<string>();
            int sum = 0;

            for (int h = 0; h < booleanarray.Length; h++)
            {
                sum += booleanarray[h];
                str.AppendFormat($"{h},{dts.AddSeconds(h):yyyy-MM-dd HH:mm:ss}, {booleanarray[h]}, { sum} \r\n");
            }
            File.WriteAllText(path, str.ToString());
        }



        #endregion



        #endregion  ============================================================











        public class LossItem
        {
            public string MachineID { get; set; }

            public double LoadingTimeBYQTY { get; set; }
            public double LoadingTime { get; set; }

            public double A1 { get; set; }
            public double A2 { get; set; }
            public double A3 { get; set; }


            public double P1 { get; set; }
            public double P2 { get; set; }
            public double P3 { get; set; }
            public double P4 { get; set; }


            public double OK { get; set; }
            public double NG { get; set; }
            public double RE { get; set; }


            public double AvailabilityPercent { get; set; }
            public double PerformancePercent { get; set; }
            public double QaulityPercent { get; set; }
            public double OEE { get; set; }
        }


        private class LossP1P2
        {
            public double P1 { get; set; }
            public double P2 { get; set; }
        }


        public class OeeAPQ
        {
            public double A { get; set; }
            public double P { get; set; }
            public double Q { get; set; }

            public double OEE { get; set; }
        }



        public class Record
        {
            public string ID { get; set; }
            public string MachineId { get; set; }
            public DateTime CurrentDateTime { get; set; }
            public string PartNumber { get; set; }
            public double MTTime { get; set; }
            public string OKNG { get; set; }
        }

        public class MLRecord : Record
        {
            public double MTminTime { get; set; }
            public double Unknow { get; set; }
            public double HTminTime { get; set; }

            public double UnknowNoHT { get; set; }

            public double Speed { get; set; }

            public double SpeedM { get; set; }
            public double Idling { get; set; }
        }





        public class WorkingTime
        {
            public int HourNo { get; set; }
            public DateTime StopTime { get; set; }
            public DateTime StartTime { get; set; }

        }

        public class SpeedAndIdlingloss
        {
            public string MachineID { get; set; }
            public double Loss { get; set; }
        }

        public class OEEAll
        {
            public List<LossItem> OEEMC { get; set; }
            public OeeAPQ OEELINE { get; set; }

        }


        public class OEEMCList
        {
            public DateTime RegistDate { get; set; }
            public string MachineID { get; set; }
            public double LoadingTime { get; set; }

            public double A1 { get; set; }
            public double A2 { get; set; }
            public double A3 { get; set; }


            public double P1 { get; set; }
            public double P2 { get; set; }
            public double P3 { get; set; }
            public double P4 { get; set; }


            public double OK { get; set; }
            public double NG { get; set; }
            public double RE { get; set; }


            public double A { get; set; }
            public double P { get; set; }
            public double Q { get; set; }
            public double OEE { get; set; }
        }

        public class OEEMC
        {
            public string MachineID { get; set; }
            public double A { get; set; }
            public double P { get; set; }
            public double Q { get; set; }

            public double OEE { get; set; }
        }


        public class OEEDate
        {
            public DateTime Registdate { get; set; }
            public double A { get; set; }
            public double P { get; set; }
            public double Q { get; set; }

            public double OEE { get; set; }
        }


        public class OEEDateNew
        {
            public DateTime Registdate { get; set; }
            public double A1 { get; set; }
            public double A2 { get; set; }
            public double A3 { get; set; }

            public double A4 { get; set; }
            public double A5 { get; set; }
            public double A6 { get; set; }

            public double A7 { get; set; }
            public double A8 { get; set; }
          

            public double OEE { get; set; }
        }



        public class Startup
        {
            public DateTime StartupTime { get; set; }
        }

        public class MasterTime0
        {
            public string MachineID { get; set; }
            public bool[] Mastertime { get; set; }
        }

        public class MasterTime
        {
            public string MachineID { get; set; }
            public int[] Mastertime { get; set; }
        }

        public class MachineTime
        {
            public String MachineID { get; set; }
            public String PartNumber { get; set; }
            public double MTminSec { get; set; }
            public double HTminSec { get; set; }
        }

        public class QualityProduced
        {
            public string MachineID { get; set; }
            public double Qty { get; set; }
        }


        public class LoadingTimeByQTY
        {
            public string MachineID { get; set; }

            public double LoadingTime { get; set; }
        }

        public class P1P2Loss
        {
            public string run { get; set; }

            public double Second { get; set; }
        }


       
    }

}
