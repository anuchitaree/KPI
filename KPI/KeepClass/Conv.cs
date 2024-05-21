//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI
{
    class Conv
    {
      //  public static double x { get; set; }

        public static string DataString(Object data)
        {
            string result = "0";
            if (data==null)
            {
                return result;
            }
            result = Convert.ToString(data);
            return result;

        }

        //public static DateTime DataDateTime1(Object data)
        //{
        //    DateTime result = new DateTime(1900, 1, 1,0,0,0);
        //    if (data == null)
        //    {
        //        return result;
        //    }
        //    result = Convert.ToDateTime(data);
        //    return result;

        //}

        //public static int DataInt321(Object data)
        //{
        //    int result = 0;
        //    if (data == null)
        //    {
        //        return result;
        //    }
        //    result = Convert.ToInt32(data);
        //    return result;

        //}

    }
}
