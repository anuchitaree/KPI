using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Class
{
    public static class Converting
    {
        public static bool IsNumeric(string value)
        {
            try
            {
                double price = Convert.ToDouble(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        public static double IsDouble(object obj)
        {
            try
            {

                return Convert.ToDouble(obj);

            }
            catch (Exception)
            {

                return 0;
            }
        }

    }
}
