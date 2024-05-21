using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
    public class ProgressMP
    {
        public DateTime RegistDate { get; set; }
        public double ManHour { get; set; }

    }

    public class QTY1
    {
        public int qty { get; set; }
    }


    public class QTY
    {
        public int qty { get; set; }
    }

    public class DateWorking
    {
        public DateTime RegistDate { get; set; }
        public int WorkShift { get;  set; }
    }

}
