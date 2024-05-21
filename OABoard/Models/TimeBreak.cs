using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OABoard.Models
{
   public class TimeBreak
    {

      
        public int HourNo { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime StopTime { get; set; }
        public string DayNight { get; set; }
        public System.DateTime StartTimeMonitor { get; set; }
        public System.DateTime StopTimeMonitor { get; set; }
    }
}
