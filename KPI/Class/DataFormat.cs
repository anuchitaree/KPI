using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Class
{
    public static class DataFormat
    {
        public static string Comma(string str)
        {
            int l = str.Length;
            for (int k = 3; k <= str.Length - 1; k += 3)
            {
                if (l - k > 0)
                    str = str.Insert(l - k, ",");
            }
           return str;
        }
    }
}
