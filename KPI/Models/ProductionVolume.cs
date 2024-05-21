using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Models
{
    //public class ProductionVolume
    //{
    //    //public string PartNumber { get; set; }
    //    //public List<DateQty> DateVolume { get; set; }

    //}

    //public class DateQty
    //{
    //    public DateTime Day { get; set; }
    //    public int Qty { get; set; }
    //}

    public class PartNumber
    {
        public string  Partnumber { get; set; }
    }




   
    public class ProductionSummay
    {
        public DateTime RegistDate { get; set; }
        public string Partnumber { get; set; }
        public int Qty { get; set; }
    }

    //============ Chart ========================
    public class ProductionByDay
    {
        public DateTime RegistDate { get; set; }
        public List<PartnumerQty>  PiecePerModel { get; set; }
    }

    public class PartnumerQty
    {
        public string Partnumber { get; set; }
        public int Qty { get; set; }
    }

    //========================================

    public class ProductionByPartNumber
    {
        public string Partnumber  { get; set; }
        public List<DateQty> PiecePerDate { get; set; }
    }

    public class DateQty
    {
        public DateTime RegistDate  { get; set; }
        public int Qty { get; set; }
    }


    public class DateQtyAcc : DateQty
    {
        public int Accumulate { get; set; }
    }
    //========================================




}
