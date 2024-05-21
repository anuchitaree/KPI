using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
   public class TracMachine
    {
        public string machinename { get; set; }
    }

    public class TracMachineId
    {
        public string machineId { get; set; }
    }

    public class TracMachineIdName: TracMachineId
    {
        public string machineName { get; set; }
    }

    public class TracMachineTime :TracMachine
    {
        public string machinetime { get; set; }
        public string machineSD { get; set; }
    }

    public class TracMachineTimes : TracMachine
    {
        public string machineId { get; set; }
        public string partnumber { get; set; }

        public double machinetime { get; set; }
        public DateTime dateTime { get; set; }
    }


    public class TracParetoMachine
    {
        public string machineId { get; set; }
        public Double axis { get; set; }

        public double machineTime { get; set; }
 
    }

}
