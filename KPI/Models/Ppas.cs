using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{

    public  class Ppas
    {
      
     
        public int hour { get; set; }
        public string monitor { get; set; }
        public int period { get; set; }
        public int plan100 { get; set; }
        public int  accPlan { get; set; }
        public int  volume { get; set; }
        public int  accVol { get; set; }
        public double percentOA { get; set; }
        public double stdOA { get; set; }
        public string redAlarm { get; set; }
        public double percentOAavg { get; set; }
        public string workStatus { get; set; }
        public int volumePerHr { get; set; }
        public string alarm { get; set; }
    }
}
