using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
   
    public partial class OEEOeeMCMonitor
    {
        public System.DateTime registDate { get; set; }
        public string machineID { get; set; }
        public string machineName { get; set; }
        public int machineSort { get; set; }
        public double Loadingtime { get; set; }
        public double A1 { get; set; }
        public double A2 { get; set; }
        public double A3 { get; set; }
        public double A4 { get; set; }
        public double A5 { get; set; }
        public double A6 { get; set; }
        public double A7 { get; set; }
        public double A8 { get; set; }
       


        public double P1 { get; set; }
        public double P2 { get; set; }
        public double P3 { get; set; }
        public double P4 { get; set; }
        public double OK { get; set; }
        public double NG { get; set; }
        public double RE { get; set; }
        public double A { get; set; }
        public double P { get; set; }
        public double Q { get; set; }
        public double Oee { get; set; }
    }
}
