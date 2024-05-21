using KPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KPI.Class
{
    public static class ProductionToday
    {


        public static List<ProductionByDay> ChartProductionByDay(List<Prod_ProductionToday> raw, DateTime registDate)
        {
            var result = new List<ProductionByDay>();
            if (raw.Count == 0)
                return result;
            int month = registDate.Month;
            int year = registDate.Year;
            int dayInMonth = DateTime.DaysInMonth(year, month);

            DateTime registStart = new DateTime(year, month, 1);


            var partlist = raw.GroupBy(p => p.partNumber).Select(x => new PartNumber { Partnumber = x.Key }).ToList();

            var produtionSummary = raw.GroupBy(x => new { x.registDate, x.partNumber })
                .Select(x => new ProductionSummay
                {
                    RegistDate = x.Key.registDate,
                    Partnumber = x.Key.partNumber,
                    Qty = x.Sum(b => b.Qty)
                }).ToList();



            var productionByDay = new List<ProductionByDay>();
            for (int i = 0; i < dayInMonth; i++)
            {
                DateTime dateRun = registStart.AddDays(i);

                var piecePerModel = new List<PartnumerQty>();
                foreach (PartNumber p in partlist)
                {
                    var data = produtionSummary.Where(r => r.RegistDate == dateRun)
                        .Where(x => x.Partnumber == p.Partnumber);
                    int qty = data.Any() ? data.FirstOrDefault().Qty : 0;

                    var partnumerQty = new PartnumerQty()
                    {
                        Partnumber = p.Partnumber,
                        Qty = qty
                    };
                    piecePerModel.Add(partnumerQty);
                }
                var pbd = new ProductionByDay()
                {
                    RegistDate = dateRun,
                    PiecePerModel = piecePerModel
                };
                productionByDay.Add(pbd);
            }
            return productionByDay;

        }


        public static void ProductionByShift(DataGridView Dvg, List<Prod_ProductionToday> raw, List<ProdWorkToday> work, DateTime registDate, string shift)
        {
            int month = registDate.Month;
            int year = registDate.Year;
            int dayInMonth = DateTime.DaysInMonth(year, month);

            DateTime registStart = new DateTime(year, month, 1);

            var partlist = raw.Where(s => s.workShift == shift).GroupBy(p => p.partNumber)
                .Select(x => new PartNumber { Partnumber = x.Key }).ToList();

            var produtionSummary = raw.Where(s => s.workShift == shift).ToList();

            var productionByPartNumber = new List<ProductionByPartNumber>();
            foreach (PartNumber p in partlist)
            {
                var piecePerDate = produtionSummary
               .Where(n => n.partNumber == p.Partnumber)
               .Select(d => new DateQty
               {
                   RegistDate = d.registDate,
                   Qty = d.Qty

               }).ToList();
                var prodvol = new ProductionByPartNumber()
                {
                    Partnumber = p.Partnumber,
                    PiecePerDate = piecePerDate
                };
                productionByPartNumber.Add(prodvol);
            }

            //====Accumulate  ==
            var sumday = produtionSummary.GroupBy(r => r.registDate)
                .Select(x => new DateQty
                {
                    RegistDate = x.Key,
                    Qty = x.Sum(a => a.Qty)
                }).ToList();

            var dateQtyAcc = new List<DateQtyAcc>();
            int accumulate = 0;
            for (int i = 0; i < dayInMonth; i++)
            {
                DateTime dateRun = registStart.AddDays(i);
                var qty = sumday.Where(x => x.RegistDate == dateRun);
                int amount = qty.Any() ? qty.FirstOrDefault().Qty : 0;
                accumulate += amount;
                var acc = new DateQtyAcc()
                {
                    RegistDate = dateRun,
                    Qty = amount,
                    Accumulate = accumulate,
                };
                dateQtyAcc.Add(acc);
            }




            int count = 1;
            Dvg.Rows.Clear();
            var summary = new List<DateQty>();
            foreach (ProductionByPartNumber p in productionByPartNumber)
            {
                var o = new Object[34];
                o[0] = count;
                o[1] = p.Partnumber;
                foreach (DateQty q in p.PiecePerDate)
                {
                    int day = q.RegistDate.Day + 1;
                    o[day] = q.Qty;
                }
                Dvg.Rows.Add(o);
                count++;
            }


            var f1 = new Object[35];
            var f2 = new Object[35];
            f1[0] = "";
            f1[1] = "Total";
            f2[1] = "Accumulate";
            foreach (DateQtyAcc q in dateQtyAcc)
            {
                int day = q.RegistDate.Day + 1;
                f1[day] = String.Format("{0:#,##0.##}", q.Qty);
                f2[day] = String.Format("{0:#,##0.##}", q.Accumulate);
            }
            Dvg.Rows.Add(f1);
            Dvg.Rows.Add(f2);






            // HighLight only holiday//
            DataGridViewCellStyle style = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(207, 249, 207),
                ForeColor = Color.Black
            };
            var workToDay = work.Where(d => d.WorkShift == shift).ToList();

            object[] ll = new object[34];
            ll[1] = "LL Approved";

            object[] tl = new object[34];
            tl[1] = "TL Approved";


            object[] am = new object[34];
            am[1] = "AM Approved";


            foreach (ProdWorkToday w in workToDay)
            {
                int day = w.Registdate.Day + 1;
                ll[day] = w.LL != null ? "O" : "X";
                tl[day] = w.TL != null ? "O" : "X";
                am[day] = w.AM != null ? "O" : "X";
            }
            Dvg.Rows.Add(ll);
            Dvg.Rows.Add(tl);
            Dvg.Rows.Add(am);



            int totalrow = Dvg.Rows.Count;
            foreach (ProdWorkToday w in workToDay)
            {
                int day = w.Registdate.Day + 1;
                for (int i = 0; i < totalrow; i++)
                {
                    Dvg.Rows[i].Cells[day].Style = style;
                }
            }
        }

        public static void TableHiglight(IQueryable<ProdWorkToday> appProd, Label ll, Button bll, Label tl, Button btl, Label am, Button bam)
        {
            int countData = appProd.Count();
            int countLL = appProd.Where(a => a.LL != null).Count();
            int countTL = appProd.Where(a => a.TL != null).Count();
            int countAM = appProd.Where(a => a.AM != null).Count();
            //ll.Text = tl.Text = tl.Text = string.Empty;

            if (countData == countLL)
            {
                ll.Text = "completed";
                ll.BackColor = Color.Green;
                bll.Enabled = false;
            }
            else
            {
                ll.Text = "need approve";
                ll.BackColor = Color.Yellow;
                bll.Enabled = true;
            }

            if (countData == countTL)
            {
                tl.Text = "completed";
                tl.BackColor = Color.Green;
                btl.Enabled = false;
            }
            else
            {
                tl.Text = "need approve";
                tl.BackColor = Color.Yellow;
                btl.Enabled = true;
            }

            if (countData == countAM)
            {
                am.Text = "completed";
                am.BackColor = Color.Green;
                bam.Enabled = false;

            }
            else
            {
                am.Text = "need approve";
                am.BackColor = Color.Yellow;
                bam.Enabled = true;
            }

        }


        public static void PartnumberHighlight(DataGridView Dgv, int row, int col)
        {

            DataGridViewCellStyle abnormalStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 99, 71),
                ForeColor = Color.Black
            };
            Dgv.Rows[row].Cells[col].Style = abnormalStyle;

        }


        public static List<Pg_STDMH> InitialProductionToday(DateTime date, string section)
        {
            int y = date.Year;
            int m = date.Month;
            int d = date.Day;
            int daYInMonth = DateTime.DaysInMonth(y, m);
            DateTime startdate = new DateTime(y, m, 1, 0, 0, 0);

            var init = new List<Pg_STDMH>();
            for (int i = 0; i < daYInMonth; i++)
            {
                DateTime newdate = startdate.AddDays(i);
                var blank1 = new Pg_STDMH()
                {
                    sectionCode = section,
                    registDate = newdate,
                    workShift = "A",
                    STD_MH = 0,
                    actucalQty = 0,
                };
                init.Add(blank1);

                var blank2 = new Pg_STDMH()
                {
                    sectionCode = section,
                    registDate = newdate,
                    workShift = "B",
                    STD_MH = 0,
                    actucalQty = 0,
                };
                init.Add(blank2);
            }
            return init;

        }




        //    public static void InitialProductionToday(DateTime date,string section)
        //    {

        //        public string sectionCode { get; set; }
        //    public System.DateTime registDate { get; set; }
        //    public string workShift { get; set; }
        //    public double STD_MH { get; set; }
        //    public double actucalQty { get; set; }

        //}


    }
}
