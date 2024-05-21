using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
   public class EmpManPower
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Grade { get; set; }
    }

    public class EmpManPowerRegistedTable
    {
        public System.DateTime registDate { get; set; }
        public int UserId { get; set; }

        public string Fullname { get; set; }
        public string SectionCode { get; set; }
        public string WorkType { get; set; }
        public string DayNight { get; set; }
        public string ShiftAB { get; set; }
        public int FunctionId { get; set; }
        public double Rate { get; set; }
        public double Period { get; set; }
        public int FunctionOTId { get; set; }
        public double RateOT { get; set; }
        public double PeriodOT { get; set; }
        public string DecInc { get; set; }
        public string SectionCodeFrom { get; set; }

    }

}
