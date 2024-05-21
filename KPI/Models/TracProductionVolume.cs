using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
    public class TracProductionVolume
    {
        public DateTime registdate { get; set; }
        public string partnumber { get; set; }
        public int volume { get; set; }
        public int volumeOther { get; set; }
    }
}
