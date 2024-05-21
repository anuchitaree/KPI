using KPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPI.Class
{
    public static class ProductedCollection
    {

        public static List<ProductionByDay> ChartProductionByDay(List<App_ProductionToday> raw,DateTime selectedtime)
        {
            var result = new List<ProductionByDay>() { };
            if (raw.Count == 0)
                return result;

            int month = selectedtime.Month;
            int year = selectedtime.Year;
            int dayInMonth = DateTime.DaysInMonth(year, month);
            DateTime registStart = new DateTime(year, month, 1);

            var pnList = raw.GroupBy(g => g.partNumber)
                .Select(s => new PartNumber
                {
                    Partnumber = s.Key,
                }).ToList();
            var producted = raw.GroupBy(x => new { x.partNumber, x.registDate })
                                .Select(p => new ProductionSummay
                                {
                                    RegistDate = p.Key.registDate,
                                    Partnumber = p.Key.partNumber,
                                    Qty = p.Sum(x => x.Qty)
                                }).ToList();

            DateTime runDate = registStart;

            var produtionByday = new List<ProductionByDay>();
            for (int i = 0; i < dayInMonth; i++)
            {
                runDate = runDate.AddDays(i);

                var partnumerQty = new List<PartnumerQty>();
                foreach (PartNumber p in pnList)
                {
                    var qty = producted.Where(r => r.RegistDate == runDate)
                        .Where(n => n.Partnumber == p.Partnumber);
                    int amount = qty.Any() ? qty.FirstOrDefault().Qty : 0;
                    var pq = new PartnumerQty()
                    {
                        Partnumber = p.Partnumber,
                        Qty = amount
                    };
                    partnumerQty.Add(pq);
                }

                var prodution = new ProductionByDay()
                {
                    RegistDate = runDate,
                    PiecePerModel = partnumerQty
                };
                produtionByday.Add(prodution);

            }
            return produtionByday;
        }

        public static void DataGridViewShow(DataGridView Dvg)
        {

        }







    }














    }
}
