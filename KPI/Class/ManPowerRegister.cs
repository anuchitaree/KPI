using KPI.Models;
using KPI.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KPI.Class
{
    public static class ManPowerRegister
    {

        public static List<Emp_ManPowerRegistedTable> MPRegistedNormal(DataGridView Dgv, string section, DateTime registDate, string shift,
            string dayNight, string workType, CheckBox chkWork, List<Emp_ManPowerRegistedTable> registedMP)
        {
            if (Dgv.RowCount == 0 || chkWork.Checked == false)
                return registedMP;

            for (int i = 0; i < Dgv.RowCount; i++)
            {
                DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)Dgv["Responsibility", i];
                int functionID = dcc.Items.IndexOf(dcc.Value);
                DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)Dgv["ResponsibilityForOT", i];
                int functionOtID = dcc1.Items.IndexOf(dcc1.Value);

                var mp = new Emp_ManPowerRegistedTable()
                {
                    registDate = registDate,
                    userID = Convert.ToInt32(Dgv.Rows[i].Cells[2].Value),
                    sectionCode = section,
                    workType = workType,
                    DayNight = dayNight,
                    shiftAB = shift,
                    functionID = functionID,
                    rate = Converting.IsDouble(Dgv.Rows[i].Cells[4].Value),
                    period = Converting.IsDouble(Dgv.Rows[i].Cells[5].Value),
                    functionOtID = functionOtID,
                    rateOT = Converting.IsDouble(Dgv.Rows[i].Cells[6].Value),
                    periodOT = Converting.IsDouble(Dgv.Rows[i].Cells[7].Value),
                    DecInc = "0",
                    sectionCodeFrom = section,
                };
                registedMP.Add(mp);
            }

            return registedMP;

        }


        public static List<Emp_ManPowerRegistedTable> MPRegistedDecInc(DataGridView Dgv, string section, DateTime registDate, string workType, List<Emp_ManPowerRegistedTable> registedMP, bool save)
        {
            if (Dgv.RowCount == 0)
                return registedMP;

            for (int i = 0; i < Dgv.RowCount; i++)
            {
                DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)Dgv["Responsibility", i];
                int functionID = dcc.Items.IndexOf(dcc.Value);

                DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)Dgv["ResponsibilityForOT", i];
                int functionOtID = dcc1.Items.IndexOf(dcc1.Value);

                DataGridViewComboBoxCell dcc2 = (DataGridViewComboBoxCell)Dgv["ToSection", i];
                int IncreaseSection = dcc2.Items.IndexOf(dcc2.Value);
                string sectionCodeFrom = Dict.SectionCodeName.ElementAt(IncreaseSection).Key;

                var mpD = new Emp_ManPowerRegistedTable()
                {
                    registDate = registDate,
                    userID = Convert.ToInt32(Dgv.Rows[i].Cells[2].Value),
                    sectionCode = section,
                    workType = workType,
                    DayNight = Convert.ToString(Dgv.Rows[i].Cells[8].Value),
                    shiftAB = Convert.ToString(Dgv.Rows[i].Cells[0].Value),
                    functionID = functionID,
                    rate = Converting.IsDouble(Dgv.Rows[i].Cells[4].Value),
                    period = Converting.IsDouble(Dgv.Rows[i].Cells[5].Value),
                    functionOtID = functionOtID,
                    rateOT = Converting.IsDouble(Dgv.Rows[i].Cells[6].Value),
                    periodOT = Converting.IsDouble(Dgv.Rows[i].Cells[7].Value),
                    DecInc = "D",
                    sectionCodeFrom = section,
                };
                registedMP.Add(mpD);
                if (save)
                {
                    var mpI = new Emp_ManPowerRegistedTable()
                    {
                        registDate = registDate,
                        userID = Convert.ToInt32(Dgv.Rows[i].Cells[2].Value),
                        sectionCode = sectionCodeFrom,
                        workType = workType,
                        DayNight = Convert.ToString(Dgv.Rows[i].Cells[8].Value),
                        shiftAB = Convert.ToString(Dgv.Rows[i].Cells[0].Value),
                        functionID = functionID,
                        rate = Converting.IsDouble(Dgv.Rows[i].Cells[4].Value),
                        period = Converting.IsDouble(Dgv.Rows[i].Cells[5].Value),
                        functionOtID = functionOtID,
                        rateOT = Converting.IsDouble(Dgv.Rows[i].Cells[6].Value),
                        periodOT = Converting.IsDouble(Dgv.Rows[i].Cells[7].Value),
                        DecInc = "I",
                        sectionCodeFrom = section,
                    };
                    registedMP.Add(mpI);
                }
            }

            return registedMP;

        }


        public static List<Emp_ManPowerRegistedTable> MPRegistedInc(DataGridView Dgv, string section, DateTime registDate, string workType, List<Emp_ManPowerRegistedTable> registedMP)
        {
            if (Dgv.RowCount == 0)
                return registedMP;

            for (int i = 0; i < Dgv.RowCount; i++)
            {
                DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)Dgv["Responsibility", i];
                int functionID = dcc.Items.IndexOf(dcc.Value);

                DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)Dgv["ResponsibilityForOT", i];
                int functionOtID = dcc1.Items.IndexOf(dcc1.Value);

                //string fromsection = Dgv.Rows[i].Cells["ToSection"].Value.ToString();

                DataGridViewComboBoxCell dcc2 = (DataGridViewComboBoxCell)Dgv["DecreasefromSection", i];
                int IncreaseSection = dcc2.Items.IndexOf(dcc2.Value);
                string sectionCodeFrom = Dict.SectionCodeName.ElementAt(IncreaseSection).Key;

                var mpI = new Emp_ManPowerRegistedTable()
                {
                    registDate = registDate,
                    userID = Convert.ToInt32(Dgv.Rows[i].Cells[2].Value),
                    sectionCode = section,
                    workType = workType,
                    DayNight = Convert.ToString(Dgv.Rows[i].Cells[8].Value),
                    shiftAB = Convert.ToString(Dgv.Rows[i].Cells[0].Value),
                    functionID = functionID,
                    rate = Converting.IsDouble(Dgv.Rows[i].Cells[4].Value),
                    period = Converting.IsDouble(Dgv.Rows[i].Cells[5].Value),
                    functionOtID = functionOtID,
                    rateOT = Converting.IsDouble(Dgv.Rows[i].Cells[6].Value),
                    periodOT = Converting.IsDouble(Dgv.Rows[i].Cells[7].Value),
                    DecInc = "I",
                    sectionCodeFrom = sectionCodeFrom,
                };
                registedMP.Add(mpI);

            }

            return registedMP;

        }

        public static List<Emp_ManPowerRegistedTable> MPRegistedAbsent(DataGridView Dgv, string section, DateTime registDate, string workType, List<Emp_ManPowerRegistedTable> registedMP)
        {
            if (Dgv.RowCount == 0)
                return registedMP;

            for (int i = 0; i < Dgv.RowCount; i++)
            {
                DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)Dgv["Responsibility", i];
                int functionID = dcc.Items.IndexOf(dcc.Value);
                DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)Dgv["ResponsibilityForOT", i];
                int functionOtID = dcc1.Items.IndexOf(dcc1.Value);

                var mp = new Emp_ManPowerRegistedTable()
                {
                    registDate = registDate,
                    userID = Convert.ToInt32(Dgv.Rows[i].Cells[2].Value),
                    sectionCode = section,
                    workType = "A",
                    DayNight = Convert.ToString(Dgv.Rows[i].Cells[9].Value),
                    shiftAB = Convert.ToString(Dgv.Rows[i].Cells[0].Value),
                    functionID = functionID,
                    rate = Converting.IsDouble(Dgv.Rows[i].Cells[4].Value),
                    period = Converting.IsDouble(Dgv.Rows[i].Cells[5].Value),
                    functionOtID = functionOtID,
                    rateOT = Converting.IsDouble(Dgv.Rows[i].Cells[6].Value),
                    periodOT = Converting.IsDouble(Dgv.Rows[i].Cells[7].Value),
                    DecInc = "-",
                    sectionCodeFrom = section,
                };
                registedMP.Add(mp);
            }

            return registedMP;
        }


        public static List<Exclusion_RecordTable> ExclusionTime(string section, DateTime registDate, string workType, CheckBox chkA, CheckBox chkB, List<Emp_ManPowerRegistedTable> registedMP)
        {
            int fA = registedMP.Where(d => d.DecInc == "0").Where(s => s.shiftAB == "A").Where(p => p.period > 0).Count();
            int fB = registedMP.Where(d => d.DecInc == "0").Where(s => s.shiftAB == "B").Where(p => p.period > 0).Count();
            int mA = registedMP.Where(d => d.DecInc == "0").Where(s => s.shiftAB == "A").Where(p => p.period > 0)
                .Where(f => f.functionID == 4 || f.functionID == 10).Count();
            int mB = registedMP.Where(d => d.DecInc == "0").Where(s => s.shiftAB == "B").Where(p => p.period > 0)
                .Where(f => f.functionID == 4 || f.functionID == 10).Count();

            string[] shift = new string[] { "A", "B" };

            int[] min = new int[] { 5, 20, 5 };

            string[] code = new string[] { "F1", "F2", "F3" };


            var excList = new List<Exclusion_RecordTable>();
            var workAside = registedMP.Where(s => s.shiftAB == "A").Where(p => p.period > 0).Where(f => f.functionID == 0).Any();
            if (chkA.Checked && workAside)
            {
                for (int i = 0; i < 3; i++)
                {
                    var exc = new Exclusion_RecordTable()
                    {
                        sectionCode = section,
                        registDate = registDate,
                        shiftAB = "A",
                        workStyle = workType,
                        exclusionID = code[i],
                        MP = fA,
                        minute = min[i],
                        totalMH = fA * min[i],
                        recordTime = DateTime.Now,
                        decription = "automatic",
                    };
                    excList.Add(exc);
                }

                var excM = new Exclusion_RecordTable()
                {
                    sectionCode = section,
                    registDate = registDate,
                    shiftAB = "A",
                    workStyle = workType,
                    exclusionID = "M1",
                    MP = mA,
                    minute = 223,
                    totalMH = mA * 223,
                    recordTime = DateTime.Now,
                    decription = "automatic",
                };
                excList.Add(excM);
            }
            var workBside = registedMP.Where(s => s.shiftAB == "B").Where(p => p.period > 0).Where(f => f.functionID == 0).Any();
            if (chkB.Checked && workBside)
            {
                for (int i = 0; i < 3; i++)
                {
                    var exc = new Exclusion_RecordTable()
                    {
                        sectionCode = section,
                        registDate = registDate,
                        shiftAB = "B",
                        workStyle = workType,
                        exclusionID = code[i],
                        MP = fB,
                        minute = min[i],
                        totalMH = fB * min[i],
                        recordTime = DateTime.Now,
                        decription = "automatic",
                    };
                    excList.Add(exc);
                }
                var excM = new Exclusion_RecordTable()
                {
                    sectionCode = section,
                    registDate = registDate,
                    shiftAB = "B",
                    workStyle = workType,
                    exclusionID = "M1",
                    MP = mB,
                    minute = 223,
                    totalMH = mB * 223,
                    recordTime = DateTime.Now,
                    decription = "automatic",
                };
                excList.Add(excM);
            }



            return excList;

        }


        public static List<Prod_TodayWorkTable> CreateProdTodayWorkTable(string section, DateTime registDate, string Adn, string Bdn, CheckBox chkA, CheckBox chkB, List<Emp_ManPowerRegistedTable> registedMP)
        {
            var prod_TodayWorkTable = new List<Prod_TodayWorkTable>();
          

            var workAside = registedMP.Where(s => s.shiftAB == "A").Where(p => p.period > 0).Where(f => f.functionID == 0).Any();
            if (chkA.Checked == true && workAside == true)
            {
                var workhr = registedMP.Where(s => s.shiftAB == "A").Where(p => p.periodOT > 0).Where(f => f.functionOtID == 0).Any();
                var todayWorkA = new Prod_TodayWorkTable()
                {
                    sectionCode = section,
                    registDate = registDate,
                    dayNight = Adn,
                    workHour = workhr == true ? 10 : 8,
                    workShift = "A"
                };
                prod_TodayWorkTable.Add(todayWorkA);
            }
            else
            {
                var todayWorkA = new Prod_TodayWorkTable()
                {
                    sectionCode = section,
                    registDate = registDate,
                    dayNight = Adn,
                    workHour = 0,
                    workShift = "A"
                };
                prod_TodayWorkTable.Add(todayWorkA);
            }

            var workBside = registedMP.Where(s => s.shiftAB == "B").Where(p => p.period > 0).Where(f => f.functionID == 0).Any();
            if (chkB.Checked == true && workBside == true)
            {
                var workhr = registedMP.Where(s => s.shiftAB == "B").Where(p => p.periodOT > 0).Where(f => f.functionOtID == 0).Any();
                var todayWorkB = new Prod_TodayWorkTable()
                {
                    sectionCode = section,
                    registDate = registDate,
                    dayNight = Bdn,
                    workHour = workhr == true ? 10 : 8,
                    workShift = "B"
                };
                prod_TodayWorkTable.Add(todayWorkB);
            }
            else
            {
                var todayWorkB = new Prod_TodayWorkTable()
                {
                    sectionCode = section,
                    registDate = registDate,
                    dayNight = Bdn,
                    workHour = 0,
                    workShift = "B"
                };
                prod_TodayWorkTable.Add(todayWorkB);
            }
            return prod_TodayWorkTable;

        }


        public static List<ShiftABMH> SummaryManHour(List<Emp_ManPowerRegistedTable> manpowerList)
        {
            var summaryMH = new List<ShiftABMH>();

            var summaryNomarlMHexist = manpowerList.Where(w => w.workType == "W").Any();
            if (summaryNomarlMHexist)
            {
                var summaryMHNor = manpowerList.Where(w => w.workType == "W").Where(d => d.DecInc == "0")
                    .GroupBy(s => s.shiftAB)
                    .Select(s => new ShiftABMH
                    {
                        ShiftAB = s.Key,
                        Type = "0",
                        ManNormalHr = s.Sum(a => a.rate * a.period),
                        ManOTHr = s.Sum(a => a.rateOT * a.periodOT),
                    }).ToList();

                var summaryMHdec = manpowerList.Where(w => w.workType == "W").Where(d => d.DecInc == "D")
                                    .GroupBy(s => s.shiftAB)
                                    .Select(s => new ShiftABMH
                                    {
                                        ShiftAB = s.Key,
                                        Type = "D",
                                        ManNormalHr = s.Sum(a => a.rate * a.period * -1),
                                        ManOTHr = s.Sum(a => a.rateOT * a.periodOT * -1),

                                    }).ToList();
                var summaryMHinc = manpowerList.Where(w => w.workType == "W").Where(d => d.DecInc == "I")
                                   .GroupBy(s => s.shiftAB)
                                   .Select(s => new ShiftABMH
                                   {
                                       ShiftAB = s.Key,
                                       Type = "I",
                                       ManNormalHr = s.Sum(a => a.rate * a.period),
                                       ManOTHr = s.Sum(a => a.rateOT * a.periodOT),
                                   }).ToList();





                summaryMH.AddRange(summaryMHNor);
                summaryMH.AddRange(summaryMHdec);
                summaryMH.AddRange(summaryMHinc);
            }

            var summaryHolidayMHexist = manpowerList.Where(w => w.workType == "H").Any();
            if (summaryHolidayMHexist)
            {
                var summaryMHnor = manpowerList
                    .Where(w => w.workType == "H").Where(d => d.DecInc == "0")
                    .GroupBy(s => s.shiftAB)
                    .Select(s => new ShiftABMH
                    {
                        ShiftAB = s.Key,
                        Type = "0",
                        ManNormalHr = 0,
                        ManOTHr = s.Sum(a => a.rate * a.period + a.rateOT * a.periodOT),
                    }).ToList();

                var summaryMHdec = manpowerList
                    .Where(w => w.workType == "H").Where(d => d.DecInc == "D")
                                 .GroupBy(s => s.shiftAB)
                                 .Select(s => new ShiftABMH
                                 {
                                     ShiftAB = s.Key,
                                     Type = "D",
                                     ManNormalHr = 0,
                                     ManOTHr = s.Sum(a => (a.rate * a.period + a.rateOT * a.periodOT) * -1),

                                 }).ToList();
                var summaryMHinc = manpowerList
                    .Where(w => w.workType == "H").Where(d => d.DecInc == "I")
                                  .GroupBy(s => s.shiftAB)
                                  .Select(s => new ShiftABMH
                                  {
                                      ShiftAB = s.Key,
                                      Type = "I",
                                      ManNormalHr = 0,
                                      ManOTHr = s.Sum(a => a.rate * a.period + a.rateOT * a.periodOT),
                                  }).ToList();

                summaryMH.AddRange(summaryMHnor);
                summaryMH.AddRange(summaryMHdec);
                summaryMH.AddRange(summaryMHinc);
            }

          
            return summaryMH;

        }


        public static PeriodDateOT CheckOvertimeSummary(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            DateTime startdate, stopdate;
            if (day < 16)
            {
                startdate = new DateTime(year, month, 16, 0, 0, 0);
                stopdate = new DateTime(year, month, 15, 0, 0, 0);
                stopdate = stopdate.AddMonths(-1);
            }
            else
            {
                startdate = new DateTime(year, month, 16, 0, 0, 0);
                stopdate = new DateTime(year, month, 15, 0, 0, 0);
                stopdate = stopdate.AddMonths(1);
            }
            var period = new PeriodDateOT()
            {
                Start = stopdate,
                Stop = startdate,
            };
            return period;
        }


        public static void LoadingIncrease(DataGridView DgvIncrease, List<EmpManPowerRegistedTable> IncreaseList)
        {
            while (DgvIncrease.Rows.Count > 1)
            {
                DgvIncrease.Rows.RemoveAt(0);
            }
            DgvIncrease.Rows.Clear();
            int i = 1;
            foreach (var item in IncreaseList)
            {
                DgvIncrease.Rows.Add(item.ShiftAB, i, item.UserId, item.Fullname, item.Rate, item.Period, item.RateOT, item.PeriodOT, item.DayNight);

                string funcName = Dict.EmpFunction.FirstOrDefault(x => x.Key == item.FunctionId).Value;
                DgvIncrease.Rows[i - 1].Cells["Responsibility"].Value = funcName;

                string funcOTName = Dict.EmpFunction.FirstOrDefault(x => x.Key == item.FunctionOTId).Value;
                DgvIncrease.Rows[i - 1].Cells["ResponsibilityForOT"].Value = funcOTName;

                string scetionFrom = Dict.SectionCodeName.FirstOrDefault(x => x.Key == item.SectionCodeFrom).Value;
                DgvIncrease.Rows[i - 1].Cells["DecreasefromSection"].Value = scetionFrom;
                i++;
            }


        }


        public static List<SummaryMH> SummaryOverAllManHour(List<ShiftABMH> shiftABMH , double exlusiontime)
        {
            var list = new List<SummaryMH>();

            var aNor = shiftABMH.Where(s => s.ShiftAB == "A").Where(n => n.Type == "0").Any();
            if (aNor)
            {
                ShiftABMH r = shiftABMH.Where(s => s.ShiftAB == "A").Where(n => n.Type == "0").FirstOrDefault();
                var newlist = new SummaryMH()
                {
                    Shift = "A",
                    Content = "A Normal Working",
                    WorkingHr = r.ManNormalHr,
                    OvertimeHr = r.ManOTHr,
                    TotalHr = r.ManNormalHr + r.ManOTHr
                };
                list.Add(newlist);
       
            }

            var aDec = shiftABMH.Where(s => s.ShiftAB == "A").Where(n => n.Type == "D").Any();
            if (aDec)
            {
                ShiftABMH r = shiftABMH.Where(s => s.ShiftAB == "A").Where(n => n.Type == "D").FirstOrDefault();
                var newlist = new SummaryMH()
                {
                    Shift = "A",
                    Content = "A Decrease Working",
                    WorkingHr = r.ManNormalHr,
                    OvertimeHr = r.ManOTHr,
                    TotalHr = r.ManNormalHr + r.ManOTHr
                };
                list.Add(newlist);
    
            }

            var aIec = shiftABMH.Where(s => s.ShiftAB == "A").Where(n => n.Type == "I").Any();
            if (aIec)
            {
                ShiftABMH r = shiftABMH.Where(s => s.ShiftAB == "A").Where(n => n.Type == "I").FirstOrDefault();
                var newlist = new SummaryMH()
                {
                    Shift = "A",
                    Content = "A Increase Working",
                    WorkingHr = r.ManNormalHr,
                    OvertimeHr = r.ManOTHr,
                    TotalHr = r.ManNormalHr + r.ManOTHr
                };
                list.Add(newlist);

            }

            var bNor = shiftABMH.Where(s => s.ShiftAB == "B").Where(n => n.Type == "0").Any();
            if (bNor)
            {
                ShiftABMH r = shiftABMH.Where(s => s.ShiftAB == "B").Where(n => n.Type == "0").FirstOrDefault();
                var newlist = new SummaryMH()
                {
                    Shift = "B",
                    Content = "B Normal Working",
                    WorkingHr = r.ManNormalHr,
                    OvertimeHr = r.ManOTHr,
                    TotalHr = r.ManNormalHr + r.ManOTHr
                };
                list.Add(newlist);

           
            }
            var bDec = shiftABMH.Where(s => s.ShiftAB == "B").Where(n => n.Type == "D").Any();
            if (bDec)
            {
                ShiftABMH r = shiftABMH.Where(s => s.ShiftAB == "B").Where(n => n.Type == "D").FirstOrDefault();
                var newlist = new SummaryMH()
                {
                    Shift = "B",
                    Content = "B Decrease Working",
                    WorkingHr = r.ManNormalHr,
                    OvertimeHr = r.ManOTHr,
                    TotalHr = r.ManNormalHr + r.ManOTHr
                };
                list.Add(newlist);
           
            }
            var bIec = shiftABMH.Where(s => s.ShiftAB == "B").Where(n => n.Type == "I").Any();
            if (bIec)
            {
                ShiftABMH r = shiftABMH.Where(s => s.ShiftAB == "B").Where(n => n.Type == "I").FirstOrDefault();
                var newlist = new SummaryMH()
                {
                    Shift = "B",
                    Content = "B Increase Working",
                    WorkingHr = r.ManNormalHr,
                    OvertimeHr = r.ManOTHr,
                    TotalHr = r.ManNormalHr + r.ManOTHr
                };
                list.Add(newlist);
            }


            double[] sum = new double[3];
            foreach (var item in list)
            {
                sum[0] += item.WorkingHr;
                sum[1] += item.OvertimeHr;
                sum[2] += item.TotalHr;
            }
            var newlist1 = new SummaryMH()
            {
                Shift = "Total",
                Content = "Total Man-Hour",
                WorkingHr = sum[0],
                OvertimeHr = sum[1],
                TotalHr = sum[2]
            };
            list.Add(newlist1);

            var newlist2 = new SummaryMH()
            {
                Shift = "Exclusion",
                Content = "ExclusionTime",
                WorkingHr = 0,
                OvertimeHr =0 ,
                TotalHr = exlusiontime
            };
            list.Add(newlist2);

            var newlist3 = new SummaryMH()
            {
                Shift = "GrossMH",
                Content = "TotalMH-Exclusion",
                WorkingHr = 0,
                OvertimeHr = 0,
                TotalHr = sum[2]-exlusiontime
            };
            list.Add(newlist3);



            return list;
        }


        public static string ChkTodayIsDay(DateTime dt)
        {
            var result = Obj.DensoWorking.Where(x => x.Registdate == dt); //.FirstOrDefault(x => x.Key == dt);
            if (result == null)
            {
                MessageBox.Show("Date in not Over 1 year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "0";
            }
            else
            {
                ProdDensoWorkingDay data = result.FirstOrDefault();
                if (data == null)
                    data.WorkHoliday = 0;
                return data.WorkHoliday == 1 ? "W" : "H";
            }
        }



        public static List<Prod_TodayWorkTable> InitialProd_TodayWorkTable(DateTime date, string section)
        {
            int y = date.Year;
            int m = date.Month;
            int d = date.Day;
            int daYInMonth = DateTime.DaysInMonth(y, m);
            DateTime startdate = new DateTime(y, m, 1, 0, 0, 0);
            var init = new List<Prod_TodayWorkTable>();
            for (int i = 0; i < daYInMonth; i++)
            {
                DateTime newdate = startdate.AddDays(i);
                var blank1 = new Prod_TodayWorkTable()
                {
                    sectionCode = section,
                    registDate = newdate,
                    dayNight = "D",
                    workHour = 0,
                    workShift = "A",
                      };
                init.Add(blank1);
                var blank2 = new Prod_TodayWorkTable()
                {
                    sectionCode = section,
                    registDate = newdate,
                    dayNight = "N",
                    workHour = 0,
                    workShift = "B",
                };
                init.Add(blank2);
            }
            return init;

        }



        public static List<Pg_MH>  InitialProgressMH(DateTime date,string section)
        {
            int y = date.Year;
            int m = date.Month;
            int d = date.Day;
            int daYInMonth = DateTime.DaysInMonth(y, m);
            DateTime startdate = new DateTime(y, m, 1, 0, 0, 0);
            var init = new List<Pg_MH>();
            for (int i = 0; i < daYInMonth; i++)
            {
                DateTime newdate = startdate.AddDays(i);
                var blank = new Pg_MH()
                {
                    sectionCode = section,
                    registDate = newdate,
                    MHNormal = 0,
                    MHOT = 0,
                    TotalMH = 0,
                    exclusionHr = 0,
                    GMH=0,
                };
                init.Add(blank);
            }
            return init;

        }



        public static List<EmpMPsummaryReport> ManPowerSummary(List<Emp_ManPowerRegistedTable> emp_ManPowerRegistedTable,DateTime startdate, DateTime stopdate)
        {
            var ot_normal = emp_ManPowerRegistedTable
                .Where(r => r.registDate >= startdate && r.registDate <= stopdate).Where(w => w.workType == "W")
                    .Where(d => d.DecInc == "0" || d.DecInc == "I")
                    .GroupBy(i => i.userID)
                    .Select(s => new EmpMPsummary
                    {
                        UserId = s.Key,
                        MWorkingTime = s.Sum(a => a.rate * a.period),
                        MOverTime = s.Sum(a => a.periodOT * a.rateOT)
                    }).ToList();


            var ot_holiday = emp_ManPowerRegistedTable
               .Where(r => r.registDate >= startdate && r.registDate <= stopdate).Where(w => w.workType == "H")
               .Where(d => d.DecInc == "0" || d.DecInc == "I")
               .GroupBy(i => i.userID)
               .Select(s => new EmpMPsummary
               {
                   UserId = s.Key,
                   MWorkingTime = 0,
                   MOverTime = s.Sum(a => a.periodOT * a.rateOT + a.period * a.rate)
               }).ToList();

            var dot_normal = emp_ManPowerRegistedTable
               .Where(r => r.registDate >= startdate && r.registDate <= stopdate).Where(w => w.workType == "W")
               .Where(d => d.DecInc == "D")
               .Where(s => s.sectionCode == User.SectionCode).GroupBy(i => i.userID)
               .Select(s => new EmpMPsummary
               {
                   UserId = s.Key,
                   MWorkingTime = s.Sum(a => a.rate * a.period),
                   MOverTime = s.Sum(a => a.periodOT * a.rateOT)
               }).ToList();


            var dot_holiday = emp_ManPowerRegistedTable
               .Where(r => r.registDate >= startdate && r.registDate <= stopdate).Where(w => w.workType == "H")
               .Where(d => d.DecInc == "D")
               .Where(s => s.sectionCode == User.SectionCode).GroupBy(i => i.userID)
               .Select(s => new EmpMPsummary
               {
                   UserId = s.Key,
                   MWorkingTime = 0,
                   MOverTime = s.Sum(a => a.periodOT * a.rateOT + a.period * a.rate)
               }).ToList();


            var users = emp_ManPowerRegistedTable
               .Where(r => r.registDate >= startdate && r.registDate <= stopdate)
               .Where(s => s.sectionCode == User.SectionCode).GroupBy(i => i.userID)
               .Select(s => new Emp
               {
                   UserId = s.Key,
               }).ToList();

            var summary = new List<EmpMPsummaryReport>();
            foreach (Emp u in users)
            {
                var workingtime = 0.0;
                var overtime = 0.0;
                var exits1 = ot_normal.Where(s => s.UserId == u.UserId);
                if (exits1.Any())
                {
                    var data = exits1.First();
                    workingtime += data.MWorkingTime;
                    overtime += data.MOverTime;
                }

                var exits2 = ot_holiday.Where(s => s.UserId == u.UserId);
                if (exits2.Any())
                {
                    var data = exits2.First();
                    workingtime += data.MWorkingTime;
                    overtime += data.MOverTime;
                }

                var exits3 = dot_normal.Where(s => s.UserId == u.UserId);
                if (exits3.Any())
                {
                    var data = exits3.First();
                    workingtime += data.MWorkingTime;
                    overtime += data.MOverTime;
                }

                var exits4 = dot_holiday.Where(s => s.UserId == u.UserId);
                if (exits4.Any())
                {
                    var data = exits4.First();
                    workingtime += data.MWorkingTime;
                    overtime += data.MOverTime;
                }

                var summ = new EmpMPsummaryReport()
                {
                    UserId = u.UserId,
                    Fullname = "",
                    MWorkingTime = workingtime,
                    MOverTime = overtime,
                    MTotalTime = workingtime + overtime
                };
                summary.Add(summ);
            }

            return summary;
            

        }



    }
}
