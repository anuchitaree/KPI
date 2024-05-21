using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
    
    public partial class OeeLineMonitor
    {
        public System.DateTime registDate { get; set; }
        public double A { get; set; }
        public double P { get; set; }
        public double Q { get; set; }
        public double OEE { get; set; }
    }
}
