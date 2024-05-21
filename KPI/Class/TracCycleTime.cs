using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Class
{
   public class TracCycleTime
    {
        public string station { get; set; }
    }
    public class TracStation
    {
        public string station { get; set; }
    }
    public class TracNagaraTime: TracStation
    {
        public string partnumber { get; set; }
        public double nagaratime { get; set; }
        public DateTime dateTime { get; set; }
    }

    public class Tracdeviation : TracStation
    {
        public double deviation { get; set; }
    }
    public class TracNagaraTimes: TracStation
    {
        public int nagaraTimes { get; set; }
    }
    public class TracParetoNagara : TracNagaraTimes
    {
        public double axis { get; set; }
    }
}
