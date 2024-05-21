using System;
using System.Collections.Generic;

namespace KPI.Models
{
    public class OeeObj
    {

    }


    public class StardardTime
    {
        public int[] Stardardtime { get; set; }

        public List<GHOeeRaw> GraphOeeRaw { get; set; }
    }
    public class GHOeeRaw
    {
        public string MachineID { get; set; }
        public int Type { get; set; }
        public int StartMinuteTime { get; set; }
        public int OccurePeriodMinute { get; set; }
    }

    public class UpdateLossDb
    {
        public string Run { get; set; }
        public double PureLossTime { get; set; }
        public double MixLossTime { get; set; }
    }

    public class ResultCommon
    {
        public int[] Stardardtime { get; set; }

        public List<GHOeeRaw> GraphOeeRaw { get; set; }
       
    }

    public class ResultP12 : ResultCommon
    {
        public double P1 { get; set; }
        public double P2 { get; set; }
        public List<UpdateLossDb> UpdateLoss { get; set; }

    }

    public class ResultP34 : ResultQ
    {
        public double P3 { get; set; }
        public double P4 { get; set; }

        public double PMachine { get; set; }

        public double PHand { get; set; }

        public List<GHOeeRaw> GraphOeeRaw { get; set; }
    }


    public class ResultA1 : ResultCommon
    {
        public double A1 { get; set; }
        public List<UpdateLossDb> UpdateLoss { get; set; }
    }

    public class ResultA2 : ResultCommon
    {
        public double A2 { get; set; }
        public List<UpdateLossDb> UpdateLoss { get; set; }
    }
    public class ResultA3 : ResultCommon
    {
        public double A3 { get; set; }
       
    }

    public class ResultSubA3
    {
        public double A3 { get; set; }
        public double  StartTime { get; set; }
        public int[] Stardardtime { get; set; }

    }


    public class ResultQ 
    {
        public double OKCycleTime { get; set; }
        public double NGCycleTime { get; set; }
        public double RECycleTime { get; set; }

    }
    public class CycleTime
    {
        public double Cycletime { get; set; }
  
    }


    public class ExclusionCycleTime 
    {
        public List<Record> RecodeData { get; set; }
        public int[] Stardardtime { get; set; }

        public List<GHOeeRaw> GraphOeeRaw { get; set; }
    }




    public class Machine
    {
        public string MachineID { get; set; }
    }


    public class LossTime : Machine
    {
        public DateTime Registdate { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string PartnumberEnd { get; set; }
        public string OeeID { get; set; }
        public string Run { get; set; }
    }





    public class OeeItems
    {
        public string MachineID { get; set; }
        public double LoadingTime { get; set; }

        public double A1 { get; set; }
        public double A2 { get; set; }
        public double A3 { get; set; }


        public double P1 { get; set; }
        public double P2 { get; set; }
        public double P3 { get; set; }
        public double P4 { get; set; }


        public double OK { get; set; }
        public double NG { get; set; }
        public double RE { get; set; }


        public double AvailabilityPercent { get; set; }
        public double PerformancePercent { get; set; }
        public double QaulityPercent { get; set; }
        public double OEEPercent { get; set; }
    }

    public class LoadingTime
    {
        public int HourNo { get; set; }
        public DateTime StopTime { get; set; }
        public DateTime StartTime { get; set; }

    }

    public class ExclusionTime
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }



    public class Record
    {
        public string ID { get; set; }
        public string MachineID { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public string PartNumber { get; set; }
        public double MTTime { get; set; }
        public string OKNG { get; set; }

        public double CTime { get; set; }
    }

    public class MLRecord : Record
    {

        public double MTminTime { get; set; }
        public double Unknow { get; set; }
        public double HTminTime { get; set; }

        public double UnknowNoHT { get; set; }

        public double Speed { get; set; }

        public double SpeedM { get; set; }
        public double Idling { get; set; }
    }


    public class LineOEE
    {
        public double A { get; set; }
        public double P { get; set; }
        public double Q { get; set; }

        public double OEE { get; set; }
    }

    public class MachineTime
    {
        public String MachineID { get; set; }
        public String PartNumber { get; set; }
        public double MTminSec { get; set; }
        public double HTminSec { get; set; }
    }

    public class Startup
    {
        public DateTime StartupTime { get; set; }
    }

    public class Loss
    {
        public double Losstime { get; set; }
    }




}
