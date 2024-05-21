using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
    public class ProdWorkToday
    {
        public DateTime Registdate { get; set; }

        public string WorkShift { get; set; }

        public int? LL { get; set; }
        public int? TL { get; set; }
        public int? AM { get; set; }
    }
}
