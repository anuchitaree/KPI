using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
    public class EmpMPovertime
    {
        public int UserId { get; set; }
        public double OT { get; set; }
    }

    public class EmpNameList
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
    }

    public class ShiftABMH
    {
        public string ShiftAB { get; set; }

        public string Type { get; set; }

        public double ManNormalHr { get; set; }

  

        public double ManOTHr { get; set; }

  
    }

    public class PeriodDateOT
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
    }

    public class SummaryMH
    {
        public string Shift { get; set; }

        public string Content { get; set; }

        public double WorkingHr { get; set; }

        public double OvertimeHr { get; set; }



        public double TotalHr { get; set; }


    }

    public class Exclusion
    {
        public double Exclusiontime { get; set; }
    }

    public class EmpMPsummary
    {
        public int UserId { get; set; }

        public double MWorkingTime { get; set; }
        public double MOverTime { get; set; }
    }

    public class EmpMPsummaryReport
    {
        public int UserId { get; set; }

        public string Fullname { get; set; }
        public double MWorkingTime { get; set; }
        public double MOverTime { get; set; }

        public double MTotalTime { get; set; }
    }


    public class Emp
    {
        public int UserId { get; set; }

    }
}
