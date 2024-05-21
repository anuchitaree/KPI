using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
    class TracParameter
    {
    }

    public class TracPackingSlip
    {
        public System.DateTime currentDateTime { get; set; }
        public double maxLeftTop { get; set; }
        public double minLeftTop { get; set; }
        public double maxRightTop { get; set; }
        public double minRightTop { get; set; }
        public double maxLeftBottom { get; set; }
        public double minLeftBottom { get; set; }
        public double maxRightBottom { get; set; }
        public double minRightBottom { get; set; }
        public string judgeResult { get; set; }
        public int numberDataTop { get; set; }
        public int numberDataBottom { get; set; }
    }

    public class TracPackingSlipSelect
    {
        public System.DateTime currentDateTime { get; set; }
        public double max { get; set; }
        public double min { get; set; }
       
    }
}
