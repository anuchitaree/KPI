using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
    public class Progress
    {

        public DateTime Registdate { get; set; }
      
        public double  PostPlan { get; set; }

        public double PostPlanAcc { get; set; }

        public double AcutalDay { get; set; }

        public double AcutalDayAccumate { get; set; }

        public double ProductionBalance { get; set; }



        public double MHNormal { get; set; }

        public double MHOT { get; set; }

        public double MHTotal { get; set; }

        public double Exclusiontime { get; set; }

        public double GrossMH { get; set; }
        public double GrossMHAcc { get; set; }





        public double LossTime { get; set; }

        public double STD_MH { get; set; }

        public double STD_MHacc { get; set; }

        public double MH_R_MGR { get; set; }

        public double MH_R_TL { get; set; }

        public double MH_R_TLacc { get; set; }

        public string Judge_MH_R_TLacc { get; set; } 
            
    }

    public class ProductionProgressSummay
    {
        public DateTime RegistDate { get; set; }
        public string Partnumber { get; set; }
        public double Qty { get; set; }
    }


    public class ProductionProgressSummay1
    {
        public DateTime RegistDate { get; set; }
        public string Partnumber { get; set; }
        public int Qty { get; set; }
    }



    public class DateProgressQty
    {
        public DateTime RegistDate { get; set; }
        public double Qty { get; set; }
    }
}
