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
    
    public partial class productionPpa
    {
        public string section_id { get; set; }
        public System.DateTime regist_date { get; set; }
        public int hour_no { get; set; }
        public string day_night { get; set; }
        public string monitor { get; set; }
        public double period { get; set; }
        public double plan { get; set; }
        public double plan_acc { get; set; }
        public double actual { get; set; }
        public double actual_acc { get; set; }
        public double volume_per_hour { get; set; }
        public double oa_actual { get; set; }
        public double oa_target { get; set; }
        public double oa_averge { get; set; }
        public string red_alarm { get; set; }
        public string work_status { get; set; }
        public string alarm { get; set; }
    }
}
