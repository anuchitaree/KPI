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
    
    public partial class manPowerDailyRegister
    {
        public int id { get; set; }
        public System.DateTime regist_date { get; set; }
        public string user_id { get; set; }
        public string section_id { get; set; }
        public string work_holiday { get; set; }
        public string day_night { get; set; }
        public string work_shift { get; set; }
        public int noramal_function_id { get; set; }
        public double normal_rate { get; set; }
        public double normal_period { get; set; }
        public int ot_function_id { get; set; }
        public double ot_rate { get; set; }
        public double ot_period { get; set; }
        public string option_di { get; set; }
        public string section_id_from { get; set; }
    }
}