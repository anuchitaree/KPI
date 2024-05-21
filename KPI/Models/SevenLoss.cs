using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
    public class SevenLoss
    {
        public string lossCode { get; set; }
        public string lossName { get; set; }
        public double lossTime { get; set; }

        public double percent { get; set; }
        public string description { get; set; }
        public byte r { get; set; }
        public byte g { get; set; }
        public byte b { get; set; }
    }
}
