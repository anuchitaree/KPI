using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using KPI.Parameter;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPI.KeepClass
{
    public class OeeLossTimeClass
    {
        public List<string> worktingtime = new List<string>();
        public List<OeelossResult> McLossInDay = new List<OeelossResult>();
        internal bool[] masterTime = new bool[87000];
        internal bool[] workingTime = new bool[900000];
        private DateTime _StartTime;
        public OeeLossTimeClass()
        {

        }

        public bool OEE_EquipmentStop(DataTable dt,DateTime dat,DataTable machineId)
        {
            MakingMasterTime(dat);
            
            if (dt.Rows.Count > 1)
            {
                for (int i = 0; i < machineId.Rows.Count; i++)
                {
                    string mcId = machineId.Rows[i].ItemArray[0].ToString();
                    string _sqlWhere = string.Format("mcid = '{0}'", mcId);
                    string _sqlOrder = "mcid DESC";
                    DataTable _newDataTable = dt.Select(_sqlWhere, _sqlOrder).CopyToDataTable();

                    string[] selectedColumns = new[] { "dateTimeStart", "dateTimeEnd" };
                    DataTable dt1 = new DataView(_newDataTable).ToTable(false, selectedColumns);
                    if (dt1.Rows.Count > 0)
                    {
                        (string, string) mcloss = CalculateLossTime(dt1);
                        McLossInDay.Add(new OeelossResult { machineId = mcId, minorloss = mcloss.Item1, majorloss = mcloss.Item2 });
                    }


                }
                
            }
            return true;

        }

        public void OEE_StartUpTimeCalculate(DataTable dt, DateTime dat, DataTable machineId)
        {

        }

        public void OEE_SetupTimeCalculate()
        {

        }

        public void OEECuttingToolChangeTimeCalculate()
        {

        }

       
        public void OEE_OhterDownTimeCalculate()
        {

        }

        public void OEE_Idling(DataTable CTTb, DateTime dat,DataTable breaktable)
        {
            bool[] Oee_operating = new bool[900000];
            bool[] operatineTime = new bool[900000];
            DateTime dateTimeStart = new DateTime(2020, 11, 25, 07, 30, 00);
            FunctionWorkingTime(dateTimeStart,breaktable);
            int idlingTime = 0;
            for (int i = 0; i < CTTb.Rows.Count; i++)
            {
                DateTime t1 = Convert.ToDateTime(CTTb.Rows[i].ItemArray[0]);
                int d1 = Convert.ToInt32((t1 - dateTimeStart).TotalSeconds*10);
                int ts = d1 - Convert.ToInt32(CTTb.Rows[i].ItemArray[1])*10;
                int te = d1 + Convert.ToInt32(CTTb.Rows[i].ItemArray[2])*10;
                for (int n = ts; n < te; n++)
                {
                    operatineTime[n] = true;
                    Oee_operating[n] = true;
                }
            }

            int mctime = 0;
            for (int i = 0; i < 900000; i++)
            {
                if (operatineTime[i] == true)
                    mctime += 1;
                if (workingTime[i] == true && operatineTime[i] == false)
                    idlingTime += 1;
            }
            int idlingtimeloss = idlingTime / 10/3600;
           
        }


        private void MakingMasterTime(DateTime dt)
        {
            int yy = dt.Year;int mm = dt.Month;int dd = dt.Day;
            _StartTime = new DateTime(yy, mm, dd, 7, 30, 00);
            for (int i = 0; i < worktingtime.Count; i++)
            {
                string mon = worktingtime[i];
                int h1 = Convert.ToInt32(mon.Substring(0, 2));
                int m1 = Convert.ToInt32(mon.Substring(3, 2));
                int h2 = Convert.ToInt32(mon.Substring(8, 2));
                int m2 = Convert.ToInt32(mon.Substring(11, 2));
                DateTime dtcompare = new DateTime(yy, mm, dd, 07, 30, 00);
                DateTime dt1 = new DateTime(yy, mm, dd, h1, m1, 00);
                DateTime dt2 = new DateTime(yy, mm, dd, h2, m2, 00);
                DateTime datetime1 = (dtcompare < dt1) ? dt1 : dt1.AddDays(1);// 00:00-07:30 ? => +1 day
                DateTime datetime2 = (dtcompare < dt2) ? dt2 : dt2.AddDays(1);// 00:00-07:30 ? => +1 day
                int d11 = Convert.ToInt32((datetime1 - _StartTime).TotalSeconds);// 0------.-------------------86400
                int d12 = Convert.ToInt32((datetime2 - _StartTime).TotalSeconds);// 0--------------------.-----86400
                d11 = (d11 > 86400) ? 86400 : d11;
                d12 = (d12 > 86400) ? 86400 : d12;
                for (int n = d11; n < d12; n++)
                {
                    masterTime[n] = true;
                }

               
            }

        }



        private  (string,string)CalculateLossTime(DataTable dt)
        {
            var result = (minor: string.Empty, major: string.Empty);
            int mi = 0;
            int ma = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime dt1 = Convert.ToDateTime(dt.Rows[i].ItemArray[0]);
                DateTime dt2 = Convert.ToDateTime(dt.Rows[i].ItemArray[1]);
                int d11 = Convert.ToInt32((dt1 - _StartTime).TotalSeconds);
                int d12 = Convert.ToInt32((dt2 - _StartTime).TotalSeconds);
                if (d12 > 86400) d12 = 86400;
                bool[] itemloss = new bool[87000];
                for (int n = d11; n < d12; n++)
                   itemloss[n] = true;
             
                int calLoss = 0;
                for (int ki = 0; ki < 86400; ki++)
                {
                    if (itemloss[ki] == true && masterTime[ki] == true)
                    {
                        calLoss = calLoss + 1;
                    }
                }
                if (calLoss < 60 * 5)
                {
                    mi = mi + calLoss;
                }
                else
                {
                    ma  = ma + calLoss;
                }
                


            }
            result.minor = mi.ToString();
            result.major = ma.ToString();
            return result;

        }


        private void FunctionWorkingTime(DateTime dt,DataTable working)
        {
            int yy = dt.Year; int mm = dt.Month; int dd = dt.Day;
            bool[] Oee_working = new bool[900000];

            _StartTime = new DateTime(yy, mm, dd, 7, 30, 00);
            for (int i = 0; i < working.Rows.Count; i++)
            {
                //string mon = worktingtime[i];
                string mon = Convert.ToString(working.Rows[i].ItemArray[0]);

                int h1 = Convert.ToInt32(mon.Substring(0, 2));
                int m1 = Convert.ToInt32(mon.Substring(3, 2));
                int h2 = Convert.ToInt32(mon.Substring(8, 2));
                int m2 = Convert.ToInt32(mon.Substring(11, 2));
                DateTime dtcompare = new DateTime(yy, mm, dd, 07, 30, 00);
                DateTime dt1 = new DateTime(yy, mm, dd, h1, m1, 00);
                DateTime dt2 = new DateTime(yy, mm, dd, h2, m2, 00);
                DateTime datetime1 = (dtcompare < dt1) ? dt1 : dt1.AddDays(1);// 00:00-07:30 ? => +1 day
                DateTime datetime2 = (dtcompare < dt2) ? dt2 : dt2.AddDays(1);// 00:00-07:30 ? => +1 day
                int d11 = Convert.ToInt32((datetime1 - _StartTime).TotalSeconds)*10;// 0------.-------------------86400
                int d12 = Convert.ToInt32((datetime2 - _StartTime).TotalSeconds)*10;// 0--------------------.-----86400
                d11 = (d11 > 864000) ? 864000 : d11;
                d12 = (d12 > 864000) ? 864000 : d12;
                for (int n = d11; n < d12; n++)
                {
                    workingTime[n] = true;
                    Oee_working[n] = true;
                }


            }

            int count = 0;
            for (int i = 0; i < 900000; i++)
            {
                if (workingTime[i] == true)
                    count += 1;
            }
            ConvertToGraph();
            //MessageBox.Show("working time =" + (count/36000).ToString() + "Hour");
        }

        private void ConvertToGraph()
        {
            Dictionary<int, int> gh = new Dictionary<int, int>();
            bool start = false;
            int st = 0;
            Dict.Graph.Clear();
            for (int i = 1; i < 900000; i++)
            {
                

                if ( start ==false && workingTime[i - 1] == false && workingTime[i] == true)
                {
                    st = i;
                    start = true;
                }
                else if (start == true && workingTime[i - 1] == true && workingTime[i] == false)
                {
                    Dict.Graph.Add(st, i-st);
                    st = 0;start = false;
                }
            }


        }


    }

    public class OeelossResult
    {
        public string machineId;
        public string majorloss;
        public string minorloss;
    }
    public class MclossTime
    {
        public string donwTime;
        public string minorStopTime;
        public string speedLossTime;
        public string defectTime;
        public string reworkTime;
    }



}
