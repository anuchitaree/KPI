//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prod_TimeBreakTable
    {
        public string divisionID { get; set; }
        public string plantID { get; set; }
        public int breakQueue { get; set; }
        public int hourNo { get; set; }
        public System.DateTime startTime { get; set; }
        public System.DateTime stopTime { get; set; }
        public string monitor { get; set; }
        public int period { get; set; }
        public string dayNight { get; set; }
        public System.DateTime startTimeMonitor { get; set; }
        public System.DateTime stopTimeMonitor { get; set; }
    }
}