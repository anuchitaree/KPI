using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
   public class TracEmp
    {
        public string shift { get; set; }
        public string workType { get; set; }
        public int userId { get; set; }
        public string  fullname { get; set; }
        public string function { get; set; }
        public double rate { get; set; }
        public double period { get; set; }
        public string functionOT { get; set; }
        public double rateOT { get; set; }
        public double periodOT { get; set; }
        public string mpControl { get; set; }
        public string  fromSection   { get; set; }

    }

    public class TracUserInput
    {
        public string partnumber1 { get; set; }
        public string partnumber2 { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
        public string unique { get; set; }

        public DateTime dateTime { get; set; }
    }
}
