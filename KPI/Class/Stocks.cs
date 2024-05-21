using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Class
{
    public static class Stocks
    {
        public static List<stockMonitor> StockStatus(DataTable dt)
        {
            List<stockMonitor> stocks = new List<stockMonitor>() { };
            int row = dt.Rows.Count;
            if ( row> 0)
            {
                for (int i = 0; i < row; i++)
                {
                    string pn = dt.Rows[i][0].ToString();
                    int qty = Convert.ToInt32(dt.Rows[i][1]);
                    int lower = Convert.ToInt32(dt.Rows[i][2]);
                    int upper = Convert.ToInt32(dt.Rows[i][3]);
                    Color color=Color.White;
                    if(qty < lower)
                    {
                        color = Color.YellowGreen;
                    }
                    else if (lower <qty && qty < upper)
                    {
                        color = Color.Blue;
                    }
                    else if(upper > qty)
                    {
                        color = Color.Red;
                    }
                    stocks.Add(new stockMonitor() { 
                        PartNumber = pn, Qty = qty.ToString(), LowerLimit = lower.ToString(), UpperLimit = upper.ToString(),Colors=color });
                }
            }
            return stocks;

        } 
    }


    public class stockMonitor
    {
        public string PartNumber { get; set; }
        public string Qty { get; set; }
        public string LowerLimit { get; set; }
        public string UpperLimit { get; set; }
        public Color Colors { get; set; }

    }
}
