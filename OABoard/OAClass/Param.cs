using OABoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OABoard.OAClass
{
    public static class Param
    {
        public static List<TimeBreak> TimeBreakTable = new List<TimeBreak>();

        public static PeriodTime PeriodOfTime = new PeriodTime();


        public static List<CTime> CTimeTable = new List<CTime>();

        public static int[] MasterTime = new int[43200];

        public static double OATarget=100;

        public static double CTAverage = 1;

        public static DateTime Registdate=DateTime.Now;
    }
}
