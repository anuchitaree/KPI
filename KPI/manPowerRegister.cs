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
    
    public partial class manPowerRegister
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string section_id { get; set; }
        public string work_shift { get; set; }
        public int noramal_function_id { get; set; }
        public double normal_rate { get; set; }
        public int ot_function_id { get; set; }
        public double ot_rate { get; set; }
        public Nullable<System.DateTime> update_at { get; set; }
    }
}
