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
    
    public partial class RD_PackingSlipTable
    {
        public int run { get; set; }
        public string sectionCode { get; set; }
        public System.DateTime registDate { get; set; }
        public System.DateTime currentDateTime { get; set; }
        public string dayNight { get; set; }
        public string machineId { get; set; }
        public string partNumber { get; set; }
        public double maxLeftTop { get; set; }
        public double minLeftTop { get; set; }
        public double maxRightTop { get; set; }
        public double minRightTop { get; set; }
        public double maxLeftBottom { get; set; }
        public double minLeftBottom { get; set; }
        public double maxRightBottom { get; set; }
        public double minRightBottom { get; set; }
        public string judgeResult { get; set; }
        public Nullable<short> modeResult { get; set; }
        public int numberDataTop { get; set; }
        public int numberDataBottom { get; set; }
    }
}
