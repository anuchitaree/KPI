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
    
    public partial class PG_Production
    {
        public string sectionCode { get; set; }
        public System.DateTime registDate { get; set; }
        public double postPlancAcc { get; set; }
        public double actualProduction { get; set; }
        public double actualProductionAcc { get; set; }
        public double ProductionBalance { get; set; }
        public double MHNomal { get; set; }
        public double MHOT { get; set; }
        public double totalMH { get; set; }
        public double exclusionTime { get; set; }
        public double grossMH { get; set; }
        public double grossMHAcc { get; set; }
        public double lossTime { get; set; }
        public double STD_MH { get; set; }
        public double STD_MHAcc { get; set; }
        public double MH_R_MGR { get; set; }
        public double MH_R_TL { get; set; }
        public double MH_R_TL_Acc { get; set; }
        public double MH_R_Acc { get; set; }
        public string judge { get; set; }
        public int workingDay { get; set; }
    }
}
