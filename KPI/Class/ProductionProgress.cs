using KPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPI.Class
{
    public static class ProductionProgress
    {
        public static void Progress()
        {
            DateTime dt = new DateTime(2021, 9, 1);
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            int datInMonth = DateTime.DaysInMonth(year, month);
            DateTime startDate = new DateTime(year, month, day);
            DateTime stopDate = startDate.AddDays(datInMonth - 1);

            string section = "4464-00";

            using (var db = new ProductionEntities11())
            {
                string yearStr = year.ToString("0000");
                string monthStr = month.ToString("00");

                var productionPlan = db.Prod_ProdPlanTable
                    .Where(s => s.sectionCode == section)
                    .Where(y => y.registYear == yearStr)
                    .Where(m => m.registMonth == monthStr)
                    .GroupBy(m => m.registMonth)
                    .Select(a => new QTY1
                    {
                        //qty = (double)a.Sum(b => Convert.ToDouble(b.planQty))
                        qty = a.Sum(b => b.planQty)
                    }).FirstOrDefault();





                var planWorkingDay = db.Prod_CustWorkingDayTable
                     .Where(s => s.sectionCode == section)
                    .Where(y => y.registYear == year)
                    .Where(m => m.registMonth == month)
                    .OrderBy(r => r.registDate)
                    .Select(a => new DateWorking
                    {
                        RegistDate = a.registDate,
                        WorkShift = a.workHoliday
                    });

                var countWorkingDay = planWorkingDay
                    .GroupBy(i => 1)
                    .Select(a => new QTY
                    {
                        qty = a.Sum(x => x.WorkShift)
                    }).FirstOrDefault();

                DateTime lastDateWork = planWorkingDay
                            .Where(s => s.WorkShift != 0)
                            .OrderByDescending(r => r.RegistDate)
                            .FirstOrDefault().RegistDate;


                double QtyperDay = Math.Floor(Convert.ToDouble(productionPlan.qty) / countWorkingDay.qty);

                double Qtyacc = 0;

                Console.WriteLine(productionPlan.qty);

                var pre_progress = new List<Progress>();
                //var str = new StringBuilder();
                foreach (var item in planWorkingDay)
                {

                    double postPlan;
                    if (lastDateWork == item.RegistDate)
                    {
                        postPlan = productionPlan.qty - Qtyacc;
                        Qtyacc = productionPlan.qty;
                    }
                    else
                    {
                        postPlan = item.WorkShift * QtyperDay;
                        Qtyacc += item.WorkShift * QtyperDay;
                    }
                    var calendar = new Progress()
                    {
                        Registdate = item.RegistDate,
                        PostPlan = postPlan,
                        PostPlanAcc = Qtyacc,
                    };
                    pre_progress.Add(calendar);
                    //str.AppendFormat($"{item.RegistDate},{postPlan},{Qtyacc} \n");
                }
                // var aa = str.ToString();



                ///*****************************************************************


                var rawMPregister = db.Emp_ManPowerRegistedTable
                        .Where(r => r.registDate >= startDate && r.registDate <= stopDate)
                        .Where(s => s.sectionCode == section)
                        .Where(f => f.functionID != 4 && f.functionID != 5 && f.functionID != 10 && f.functionID != 11);

                //IQueryable<ProgressMP> totalMH = ManHourCalculate(rawMPregister);
                var normalWorking = rawMPregister
                  .Where(i => i.DecInc == "0")
                  .GroupBy(r => r.registDate)
                  .Select(x => new ProgressMP
                  {
                      RegistDate = x.Key,
                      ManHour = x.Sum(a => a.rate * a.period)
                  });
                var decreaseWorking = rawMPregister
                   .Where(i => i.DecInc == "D")
                   .GroupBy(r => r.registDate)
                   .Select(x => new ProgressMP
                   {
                       RegistDate = x.Key,
                       ManHour = x.Sum(a => a.rate * a.period)
                   });
                var increaseWorking = rawMPregister
                   .Where(i => i.DecInc == "I")
                   .GroupBy(r => r.registDate)
                   .Select(x => new ProgressMP
                   {
                       RegistDate = x.Key,
                       ManHour = x.Sum(a => a.rate * a.period)
                   });



                var OTnormalWorking = rawMPregister
                    .Where(i => i.DecInc == "0")
                    .GroupBy(r => r.registDate)
                    .Select(x => new ProgressMP
                    {
                        RegistDate = x.Key,
                        ManHour = x.Sum(a => a.rateOT * a.periodOT)
                    });
                var OTdecreaseWorking = rawMPregister
                   .Where(i => i.DecInc == "D")
                   .GroupBy(r => r.registDate)
                   .Select(x => new ProgressMP
                   {
                       RegistDate = x.Key,
                       ManHour = x.Sum(a => a.rateOT * a.periodOT)
                   });
                var OTincreaseWorking = rawMPregister
                   .Where(i => i.DecInc == "I")
                   .GroupBy(r => r.registDate)
                   .Select(x => new ProgressMP
                   {
                       RegistDate = x.Key,
                       ManHour = x.Sum(a => a.rateOT * a.periodOT)
                   });

                var MHNormal = from n in normalWorking
                               join d in decreaseWorking
                               on n.RegistDate equals d.RegistDate
                               join i in increaseWorking
                               on n.RegistDate equals i.RegistDate
                               select new ProgressMP
                               {
                                   RegistDate = n.RegistDate,
                                   ManHour = n.ManHour - d.ManHour + i.ManHour,
                               };

                var MHOT = from n in OTnormalWorking
                           join d in OTdecreaseWorking
                           on n.RegistDate equals d.RegistDate
                           join i in OTincreaseWorking
                           on n.RegistDate equals i.RegistDate
                           select new ProgressMP
                           {
                               RegistDate = n.RegistDate,
                               ManHour = n.ManHour - d.ManHour + i.ManHour,
                           };

                var MHTotal = from n in MHNormal
                              join h in MHOT
                              on n.RegistDate equals h.RegistDate
                              select new ProgressMP
                              {
                                  RegistDate = n.RegistDate,
                                  ManHour = n.ManHour + h.ManHour,
                              };

                var exclusionTime = db.Exclusion_RecordTable
                                  .Where(r => r.registDate >= startDate && r.registDate <= stopDate)
                                  .Where(s => s.sectionCode == section)
                                  .Where(x => x.exclusionID != "M1")
                                  .GroupBy(x => x.registDate)
                                  .Select(x => new ProgressMP
                                  {
                                      RegistDate = x.Key,
                                      ManHour = x.Sum(a => a.totalMH)
                                  });

                var aaaaa = (from n in MHNormal
                            join o in MHOT  on n.RegistDate equals o.RegistDate
                            join t in MHTotal on n.RegistDate equals t.RegistDate
                            join e in exclusionTime on n.RegistDate equals e.RegistDate
                            select new Progress
                            {
                                MHNormal = n.ManHour,
                                MHOT = o.ManHour,
                                MHTotal= t.ManHour,
                                Exclusiontime=e.ManHour,
                            }).ToList();


                var str1 = new StringBuilder();
                foreach (var i in aaaaa)
                {
                    str1.AppendFormat($"{i.MHNormal},{i.MHOT},{i.MHTotal},{i.Exclusiontime} \n");
                }

                var aaaaaa = str1.ToString();






                ///======================================================

                var productionTodayByPartnumber1 = db.Prod_ProductionToday
                                        .Where(r => r.registDate >= startDate && r.registDate <= stopDate)
                                        .Where(s => s.sectionCode == section)
                                        .GroupBy(r => new { r.registDate, r.partNumber })
                                        .Select(x => new ProductionProgressSummay1
                                        {
                                            RegistDate = x.Key.registDate,
                                            Partnumber = x.Key.partNumber,
                                            Qty = x.Sum(a => a.Qty),


                                        }).ToList();

                var productionTodayByPartnumber = new List<ProductionProgressSummay>();
                foreach (var item in productionTodayByPartnumber1)
                {
                    var newItem = new ProductionProgressSummay()
                    {
                        RegistDate = item.RegistDate,
                        Partnumber = item.Partnumber,
                        Qty = Convert.ToDouble(item.Qty),
                    };
                    productionTodayByPartnumber.Add(newItem);
                }




                var netTime = db.Prod_NetTimeTable.Where(n => n.registYear == year.ToString());




                var STDMH = (from p in productionTodayByPartnumber
                             join n in netTime
                             on p.Partnumber equals n.partNumber
                             select new ProgressMP
                             {
                                 RegistDate = p.RegistDate,
                                 ManHour = (Convert.ToDouble(p.Qty) * n.netTime)
                             }).ToList();



                var productionToday = productionTodayByPartnumber
                   .GroupBy(r => r.RegistDate).
                   Select(x => new DateProgressQty
                   {
                       RegistDate = x.Key,
                       Qty = x.Sum(a => a.Qty)
                   });




                var result = from q in pre_progress

                             join m in MHNormal

                             on q.Registdate equals m.RegistDate

                             join o in MHOT

                             on m.RegistDate equals o.RegistDate

                             join e in exclusionTime

                             on m.RegistDate equals e.RegistDate

                             join p in productionToday

                             on m.RegistDate equals p.RegistDate

                             join s in STDMH

                             on m.RegistDate equals s.RegistDate

                             select new Progress
                             {
                                 Registdate = q.Registdate,
                                 PostPlan = q.PostPlanAcc,
                                 AcutalDay = p.Qty,
                                 ProductionBalance = 0,
                                 MHNormal = m.ManHour,
                                 MHOT = o.ManHour,
                                 MHTotal = m.ManHour + o.ManHour,
                                 Exclusiontime = e.ManHour,
                                 GrossMH = m.ManHour + o.ManHour - e.ManHour,
                                 LossTime = 0,
                                 STD_MH = s.ManHour,
                             };

                var str = new StringBuilder();
                foreach (var i in result)
                {
                    str.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11} \n",
                        i.Registdate.ToString("dd-MM-YYYY"), i.PostPlan.ToString(), i.AcutalDay.ToString(), i.ProductionBalance.ToString()
                        , i.MHNormal.ToString(), i.MHOT.ToString(), i.MHTotal.ToString(), i.Exclusiontime.ToString(), i.GrossMH.ToString(), i.LossTime.ToString(), i.STD_MH.ToString());
                }
                var aaaa = str.ToString();



            }






        }


        private static IQueryable<ProgressMP> ManHourCalculate(IQueryable<Emp_ManPowerRegistedTable> rawMPregister)
        {
            var normalWorking = rawMPregister
                   .Where(i => i.DecInc == "0")
                   .GroupBy(r => r.registDate)
                   .Select(x => new ProgressMP
                   {
                       RegistDate = x.Key,
                       ManHour = x.Sum(a => a.rate * a.period)
                   });
            var decreaseWorking = rawMPregister
               .Where(i => i.DecInc == "D")
               .GroupBy(r => r.registDate)
               .Select(x => new ProgressMP
               {
                   RegistDate = x.Key,
                   ManHour = x.Sum(a => a.rate * a.period)
               });
            var increaseWorking = rawMPregister
               .Where(i => i.DecInc == "I")
               .GroupBy(r => r.registDate)
               .Select(x => new ProgressMP
               {
                   RegistDate = x.Key,
                   ManHour = x.Sum(a => a.rate * a.period)
               });



            var OTnormalWorking = rawMPregister
                .Where(i => i.DecInc == "0")
                .GroupBy(r => r.registDate)
                .Select(x => new ProgressMP
                {
                    RegistDate = x.Key,
                    ManHour = x.Sum(a => a.rateOT * a.periodOT)
                });
            var OTdecreaseWorking = rawMPregister
               .Where(i => i.DecInc == "D")
               .GroupBy(r => r.registDate)
               .Select(x => new ProgressMP
               {
                   RegistDate = x.Key,
                   ManHour = x.Sum(a => a.rateOT * a.periodOT)
               });
            var OTincreaseWorking = rawMPregister
               .Where(i => i.DecInc == "I")
               .GroupBy(r => r.registDate)
               .Select(x => new ProgressMP
               {
                   RegistDate = x.Key,
                   ManHour = x.Sum(a => a.rateOT * a.periodOT)
               });

            var norMH = from n in normalWorking
                        join d in decreaseWorking
                        on n.RegistDate equals d.RegistDate
                        join i in increaseWorking
                        on n.RegistDate equals i.RegistDate
                        select new ProgressMP
                        {
                            RegistDate = n.RegistDate,
                            ManHour = n.ManHour - d.ManHour + i.ManHour,
                        };

            var HolMH = from n in OTnormalWorking
                        join d in OTdecreaseWorking
                        on n.RegistDate equals d.RegistDate
                        join i in OTincreaseWorking
                        on n.RegistDate equals i.RegistDate
                        select new ProgressMP
                        {
                            RegistDate = n.RegistDate,
                            ManHour = n.ManHour - d.ManHour + i.ManHour,
                        };

            var MHTotal = from n in norMH
                          join h in HolMH
                          on n.RegistDate equals h.RegistDate
                          select new ProgressMP
                          {
                              RegistDate = n.RegistDate,
                              ManHour = n.ManHour + h.ManHour,
                          };

            return MHTotal;
        }

        public static List<PG_Production> InitProgressProduction(DateTime date, string section)
        {
            int dayInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var init = new List<PG_Production>();
            for (int i = 0; i < dayInMonth; i++)
            {
                DateTime dateStart = date.AddDays(i);
                var newinit = new PG_Production()
                {
                    sectionCode = section,
                    registDate = dateStart,
                    workingDay = 0,
                    postPlancAcc = 0,
                    actualProduction = 0,
                    actualProductionAcc = 0,
                    ProductionBalance = 0,
                    MHNomal = 0,
                    MHOT = 0,
                    totalMH = 0,
                    exclusionTime = 0,
                    grossMH = 0,
                    grossMHAcc = 0,
                    lossTime = 0,
                    STD_MH = 0,
                    STD_MHAcc = 0,
                    MH_R_MGR = 0,
                    MH_R_TL = 0,
                    MH_R_TL_Acc = 0,
                    MH_R_Acc =0,
                    judge = "-",
                };
                init.Add(newinit);
            }
            return init;
        }


        public static List<Pg_Loss> InitPg_Loss(DateTime date,string section)
        {
            int dayInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var initPgLoss = new List<Pg_Loss>();
            for (int i = 0; i < dayInMonth; i++)
            {
                var init = new Pg_Loss()
                {
                    sectionCode = section,
                    registDate = date.AddDays(i),
                    lossHr = 0,
                };
                initPgLoss.Add(init);
            }
            return initPgLoss;
        }
        
     


    }

}
