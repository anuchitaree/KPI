using KPI.Models;
using System.Collections.Generic;
using System.Data;

namespace KPI.DataContain
{
    public static class LegendPlate
    {

        public static DataTable GetDayName()
        {
            DataTable DayTb = new DataTable();
            DayTb.Columns.Add("Target", typeof(string));
            DayTb.Columns.Add("number", typeof(string));
            DayTb.Rows.Add("", "Plan100%");
            DayTb.Rows.Add("", "Target");
            DayTb.Rows.Add("", "Monday");
            DayTb.Rows.Add("", "Tuesday");
            DayTb.Rows.Add("", "Wednesday");
            DayTb.Rows.Add("", "Thursday");
            DayTb.Rows.Add("", "Friday");
            DayTb.Rows.Add("", "Saturday");
            DayTb.Rows.Add("", "Sunday");
            DayTb.Rows.Add("", "--Target acc");
            DayTb.Rows.Add("", "Actual acc");
            return DayTb;
        }

        public static DataTable GetDayHistoryName()
        {
            DataTable DayTb = new DataTable();
            DayTb.Columns.Add("Target", typeof(string));
            DayTb.Columns.Add("number", typeof(string));
            DayTb.Rows.Add("", "Plan100%");
            DayTb.Rows.Add("", "Target");
            DayTb.Rows.Add("", "Monday");
            DayTb.Rows.Add("", "Tuesday");
            DayTb.Rows.Add("", "Wednesday");
            DayTb.Rows.Add("", "Thursday");
            DayTb.Rows.Add("", "Friday");
            DayTb.Rows.Add("", "Saturday");
            DayTb.Rows.Add("", "Sunday");
           
            return DayTb;
        }

        public static DataTable GetRedCountName()
        {
            DataTable DayTb = new DataTable();
            DayTb.Columns.Add("Target", typeof(string));
            DayTb.Columns.Add("number", typeof(string));
            DayTb.Rows.Add("", "Target");
            DayTb.Rows.Add("", "Count");
            return DayTb;
        }

        public static DataTable GetProgressName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Actual", typeof(string));
            dt.Columns.Add("Target", typeof(string));
            dt.Rows.Add("MgrTarget");
            dt.Rows.Add("TLTarget");
            dt.Rows.Add("MgrActual");
            dt.Rows.Add("TLActual");
            return dt;
        }

        public static DataTable GetOEEName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Actual", typeof(string));
            dt.Columns.Add("Target", typeof(string));
            dt.Rows.Add("OEE");
            dt.Rows.Add("Quality");
            dt.Rows.Add("Performance");
            dt.Rows.Add("Availability");


            return dt;
        }

        public static DataTable GetAviName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("A1", "Model Change ");
            dt.Rows.Add("A2", "Tool Change ");
            dt.Rows.Add("A3", "Startup time ");
            dt.Rows.Add("--", "Loading time");


            return dt;
        }
        public static DataTable GetPerfName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("P1","Eq >5 min  Equipment failure: more than 5 min");
            dt.Rows.Add("P2","Eq <=5 min  Minor stops : less or equal 5 min");
            dt.Rows.Add("P3","Speed loss (CT) : CT avg to Ctmax");
            dt.Rows.Add("P4","Idling");
            dt.Rows.Add("--", "Net time");


            return dt;
        }

        public static DataTable GetQualName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("NG","WorkNG");
            dt.Rows.Add("RE","Rework");
          
            return dt;
        }



        public static DataTable GetAPQName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("A1", "Model Change ");
            dt.Rows.Add("A2", "Tool Change ");
            dt.Rows.Add("A3", "Startup time ");
            dt.Rows.Add("P1", "Eq >5 min  Equipment failure: more than 5 min");
            dt.Rows.Add("P2", "Eq <=5 min  Minor stops : less or equal 5 min");
            dt.Rows.Add("P3", "Speed loss (CT) : CT avg to Ctmax");
            dt.Rows.Add("P4", "Idling");
            dt.Rows.Add("OK", "Good work");
            dt.Rows.Add("NG", "Defect");
            dt.Rows.Add("RE", "Rework");
           dt.Rows.Add("--", "OEE");

            return dt;
        }

        public static DataTable GetAPQName2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("A1 ", "Model Change ");
            dt.Rows.Add("A2 ", "Tool Change ");
            dt.Rows.Add("A3 ", "Startup time ");
            dt.Rows.Add("P1 ", "Eq >5 min  Equipment failure: more than 5 min");
            dt.Rows.Add("P2 ", "Eq <=5 min  Minor stops : less or equal 5 min");
            dt.Rows.Add("P3 ", "Speed loss (CT) : CT avg to Ctmax");
            dt.Rows.Add("P4 ", "Idling");
            dt.Rows.Add("RE ", "Rework");
            dt.Rows.Add("NG ", "Defect");
           
            dt.Rows.Add("OK ", "Good work");
            dt.Rows.Add("% ", "OEE");

            return dt;
        }


        public static DataTable GetAPQLName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("A1", "Model Change ");
            dt.Rows.Add("A2", "Tool Change ");
            dt.Rows.Add("A3", "Startup time ");
            dt.Rows.Add("P1", "Eq >5 min  Equipment failure: more than 5 min");
            dt.Rows.Add("P2", "Eq <=5 min  Minor stops : less or equal 5 min");
            dt.Rows.Add("P3", "Speed loss (CT) : CT avg to Ctmax");
            dt.Rows.Add("P4", "Idling");
            dt.Rows.Add("OK", "Good work");
            dt.Rows.Add("NG", "Defect");
            dt.Rows.Add("RE", "Rework");
            dt.Rows.Add("--", "Time without exclusion");

            return dt;
        }

  

        public static DataTable GetAP_Name()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("A1", "Model Change ");
            dt.Rows.Add("A2", "Tool Change ");
            dt.Rows.Add("A3", "Startup time ");
            dt.Rows.Add("P1", "Eq >5 min  Equipment failure: more than 5 min");
            dt.Rows.Add("P2", "Eq <=5 min  Minor stops : less or equal 5 min");
            //dt.Rows.Add("None", "");
            //dt.Rows.Add("None", "");
           // dt.Rows.Add("--", "Loading time");

            return dt;
        }


        public static DataTable LossMC()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add("LE", "Less equal than 5 min");
            dt.Rows.Add("G", "Greater than 5 min");
           
           
            return dt;
        }

        public static List<string> ProgressHeader()
        {
            var header = new List<string>
            {
                "Post Plan Accumulate",
                "Actual Production/day",
                "Actual Production Accu",
                "Production Balance = No.3 - No.1",

                "MH.Normal=(Normal-Dec+Inc)",
                "MH.OT=(Normal-Dec+Inc)",
                "TotalMH = No.5 + No.6",

                "Exclusion Time  ",
                "GMH = No.7 - No.8 ",
                "GMHacc",

                "Loss Time",

                "STD.MH = Prod by PN* STD Ratio * Net time ",
                "STD.MHacc",

                "MH.R.MGR = TotalMH / STD.MH*100",
                "MH.R.TL = GMH / STD.MH *100",
                "MH.R.TLacc= GMHacc / STD.MHacc *100",
                "Monthly MH.R.TLacc < 80 ? O : X"
            };


            return header;
        }


        public static List<SevenLoss> SevenLoss()
        {

            List<SevenLoss> header = new List<SevenLoss>()
            {
                new SevenLoss {
                    lossCode = "L1",
                    lossName = "Operation Preparation Loss",
                    r = 142,
                    g = 219,
                    b = 66,
                    description=" -Check, lubrication, sweeping, adjustment work time \n" +
                                "-Start-up and preparation time before production, and shutdown time after production"
                },
                new SevenLoss {
                    lossCode = "L2",
                    lossName = "Breakdown Loss",
                    r = 199,
                    g = 0,
                    b = 20,
                    description ="-Choko-tei, machine breakdown, and the time required to restart from those states"
                },
                new SevenLoss {
                    lossCode = "L3",
                    lossName = "Quality / Master check loss",
                    r = 229,
                    g = 230,
                    b = 0 ,
                    description ="-TimeTime lost due to quality check and master check time"
                },

                new SevenLoss {
                    lossCode = "L4",
                    lossName = "Setup / Replenishment Loss",
                    r = 169,
                    g = 69,
                    b = 0,
                    description ="-TimeTime lost  due to changeover, material supply, and consumables replacement ",
                },
                new SevenLoss {
                    lossCode = "L5",
                    lossName = "Process interference loss",
                    r = 172,
                    g = 49,
                    b = 196,
                    description ="-Interference waiting time of previous or following process in a line/cell \n" +
                                    "-Time lost due to other factors in a line/cell",
                },

                new SevenLoss {
                    lossCode = "L6",
                    lossName = "Failure Loss",
                    r = 230,
                    g = 115,
                    b = 191,
                     description ="- Defect discharge time (number of defective products * actual takt time (target cycletime) \n " +
                                    "- Defect treatment time (Downtime due to screening and adjustment of defective products）",
                },
                new SevenLoss {
                    lossCode = "L7",
                    lossName = "speed loss",
                    r = 222,
                    g = 175,
                    b = 41,
                    description ="-TimeThe delay time of the measured actual cycle time against the target CT",
                },
                new SevenLoss {
                    lossCode = "L8",
                    lossName = "Any other Loss",
                    r = 192,
                    g = 192,
                    b = 192,
                    description ="-Time that Auto Operation is turned off though loss details are unclear \n" +
                                      "-Waiting for operator' time excluding breakdown",
                }
            };


            return header;
        }


        public static List<SevenLoss> OEEoutput()
        {
               List<SevenLoss> header = new List<SevenLoss>()
            {
                new SevenLoss { lossCode = "target", lossName = "% 90 target", r = 255, g = 197, b = 197 },
                new SevenLoss { lossCode = "actual", lossName = "% actual", r = 0, g = 0, b = 255 }
            };

            return header;
        }
        public static List<string> LegendGoodDefect()
        {
            List<string> header = new List<string>();
           
            header.Add("Other model");
            header.Add("Claim model");
            return header;
        }

        public static List<string> LegendPackingSlip()
        {
            List<string> header = new List<string>();

            header.Add("LeftTop");
            header.Add("RightTop");
            header.Add("LeftBottom");
            header.Add("RightBottom");
            return header;
        }

        public static List<string> LegendPackingSlip1()
        {
            List<string> header = new List<string>();

            header.Add("numberOfTop");
            header.Add("numberOfBottom");
            return header;
        }

        public static List<string> LegendPackingSlip2()
        {
            List<string> header = new List<string>();

            header.Add("MaxValue");
            header.Add("MinValue");
            return header;
        }


        public static List<string> LegendQRmapping()
        {
            List<string> header = new List<string>();

            header.Add("1:Radiator mapping ");
            header.Add("2:HVAC mapping");
            header.Add("3:AirBag mapping");
            return header;
        }

        public static Dictionary<string,string> LegendWRG4314()
        {
            Dictionary<string,string> header = new Dictionary<string,string>();

            //header.Add( "Other model");
            //header.Add("Claim model");
            return header;
        }
    }
}
