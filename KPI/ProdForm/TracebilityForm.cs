using KPI.Class;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KPI.ProdForm
{
    public partial class TracebilityForm : Form
    {
        private static SerialPort mySerialPort;

        private static string productionId;
        DateTime productedTime = DateTime.Now;

        private DataTable dtDT = new DataTable();
        private DataTable dtSD = new DataTable();
        readonly Dictionary<string, string> DicMCname = new Dictionary<string, string>();
        readonly Chart[] ChartList = new Chart[6];
        readonly Chart[] ChartHisList = new Chart[6];
        readonly Label[] LbList = new Label[6];
        readonly Label[] SDList = new Label[6];
        readonly Label[] MaxList = new Label[6];
        readonly Label[] MinList = new Label[6];


        public TracebilityForm()
        {
            InitializeComponent();
            DataGridView1aInitial();
            InitialChart();
        }

        private void TracebilityForm_Load(object sender, EventArgs e)
        {
            ClearDataInTableAndChart();
            TbPn1.Text = "TG422136";
            TbPn2.Text = "96400T";
            TbDay.Text = "12";
            TbMonth.Text = "02";
            TbYear.Text = "21";
            TbHour.Text = "07";
            TbMinute.Text = "38";
            TbSecond.Text = "24";
        }

      

        private void BtnCloseall_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "mainForm" && Application.OpenForms[i].Name != "TrackForm")
                    Application.OpenForms[i].Close();
            }
        }

      

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            //ScannerSettingForm frm = new ScannerSettingForm();
            //frm.ScannerSettingFormClosed += new EventHandler(ScannerSettingFormClosed_Close);
            //frm.Show(new Form() { TopMost = true });
            TracebilityScannerSettingForm frm = new TracebilityScannerSettingForm();
            frm.TracebilityScannerSettingFormClosed += new EventHandler(TracebilityScannerSettingFormClosed_Close);
            frm.Show(new Form() { TopMost = true });
        }


        private void TracebilityScannerSettingFormClosed_Close(object sender, EventArgs e)
        {

            int maxRetries = 5;
            const int sleepTimeInMs = 500;
            string loggingMessage = string.Empty;

            string comport = TracebilityScannerSettingForm.com.comPort;

            string stopbit = TracebilityScannerSettingForm.com.stopBits;
            string parity = TracebilityScannerSettingForm.com.parity;
            mySerialPort = new SerialPort(comport)
            {
                BaudRate = Convert.ToInt32(TracebilityScannerSettingForm.com.baudRate),
                Parity = (Parity)Enum.Parse(typeof(Parity), parity),
                StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopbit),
                DataBits = Convert.ToInt16(TracebilityScannerSettingForm.com.dataBits)
            };

            string str = string.Format("Serial Port : {0}, {1}, {2}, {3}, {4}", comport, mySerialPort.BaudRate, mySerialPort.DataBits, parity, stopbit);
            lbSerial.Text = str;

            mySerialPort.Handshake = Handshake.None;

            while (maxRetries > 0)
            {
                try
                {
                    mySerialPort.Open();
                    if (mySerialPort.IsOpen)
                    {
                        mySerialPort.DiscardInBuffer();
                        mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                        break;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    maxRetries--;
                    Thread.Sleep(sleepTimeInMs);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            if (maxRetries != 5)
            {
                Console.WriteLine("maxRetries:{0}", maxRetries);
            }
        }


        

        private void BtnScan_Click(object sender, EventArgs e)
        {
            ScanReader();
        }


        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            productionId = sp.ReadExisting();
            mySerialPort.DiscardInBuffer();
            Console.WriteLine("Data Received:");
        }

      

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (productionId != null)
                SerialReceiveEvent(productionId);
            productionId = null;
        }


        private void TracebilityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mySerialPort != null)
            {
                if (mySerialPort.IsOpen)
                {
                    mySerialPort.DiscardOutBuffer();
                    mySerialPort.DiscardInBuffer();
                    mySerialPort.Close();
                    mySerialPort = null;
                }
            }

        }

        private void ScanReader()
        {
            productionId = null;

            int[] dat = new int[6];
            DateTime datetime;
            try
            {
                dat[0] = int.Parse(TbDay.Text);
                dat[1] = int.Parse(TbMonth.Text);
                dat[2] = int.Parse(($"20{TbYear.Text}"));
                dat[3] = int.Parse(TbHour.Text);
                dat[4] = int.Parse(TbMinute.Text);
                dat[5] = int.Parse(TbSecond.Text);
                datetime = new DateTime(dat[2], dat[1], dat[0], dat[3], dat[4], dat[5]);

            }
            catch (Exception)
            {
                MessageBox.Show("Please reconfrim filling data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string dn = OABorad.FindDayOrNight(datetime);
            LoadHistoryData(($"{TbPn1.Text}-{TbPn2.Text}"), datetime, dn);

        }

        

        private void ChartMC_MouseClick(object sender, MouseEventArgs e)
        {
            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);
            string axis = string.Empty;
            if (pointIndex >= 0)
            {
                DataPoint dp = pie.Series[0].Points[pointIndex];
                axis = dp.AxisLabel;

                string _sqlWhere = string.Format("MCNumber = '{0}'", axis);
                string _sqlOrder = "registDate ASC";
                DataTable _newDataTable = dtDT.Select(_sqlWhere, _sqlOrder).CopyToDataTable();

                string[] selectedColumns = new[] { "registDate", "LossMinute" };
                DataTable dt2 = new DataView(_newDataTable).ToTable(false, selectedColumns);


                string machineName = DicMCname.FirstOrDefault(x => x.Key == axis).Value;
                string header = string.Format("{0} : {1}", axis, machineName);

            

            }
        }

        private void ChartSD_MouseClick(object sender, MouseEventArgs e)
        {
            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);
            string axis = string.Empty;
            if (pointIndex >= 0)
            {
                DataPoint dp = pie.Series[0].Points[pointIndex];
                axis = dp.AxisLabel;
                string _sqlWhere = string.Format("mcNumber = '{0}'", axis);
                string _sqlOrder = "registDate ASC";
                DataTable _newDataTable = dtSD.Select(_sqlWhere, _sqlOrder).CopyToDataTable();

                string[] selectedColumns = new[] { "registDate", "SD" };
                DataTable dt2 = new DataView(_newDataTable).ToTable(false, selectedColumns);

                string machineName = DicMCname.FirstOrDefault(x => x.Key == axis).Value;
                string header = string.Format("{0} : {1}", axis, machineName);

            }
        }




        private void TrackSubFormClosed_Close(object sender, EventArgs e)
        {
           
        }

        //----------// OPERATION LOOP //----------////////////////////////////////////////////////////////

        #region Initial Component

        private void DataGridView1aInitial()
        {
            this.dataGridView1a.ColumnCount = 13;
            this.dataGridView1a.Columns[0].Name = "No";
            this.dataGridView1a.Columns[0].Width = 30;
            this.dataGridView1a.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1a.Columns[1].Name = "Shift";
            this.dataGridView1a.Columns[1].Width = 30;
            this.dataGridView1a.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1a.Columns[2].Name = "WorkType";
            this.dataGridView1a.Columns[2].Width = 100;
            this.dataGridView1a.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1a.Columns[3].Name = "ID";
            this.dataGridView1a.Columns[3].Width = 70;
            this.dataGridView1a.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1a.Columns[4].Name = "Full Name";
            this.dataGridView1a.Columns[4].Width = 150;
            this.dataGridView1a.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[5].Name = "Function";
            this.dataGridView1a.Columns[5].Width = 50;
            this.dataGridView1a.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1a.Columns[6].Name = "Rate";
            this.dataGridView1a.Columns[6].Width = 40;
            this.dataGridView1a.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1a.Columns[7].Name = "Period";
            this.dataGridView1a.Columns[7].Width = 50;
            this.dataGridView1a.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1a.Columns[8].Name = "FunctionOT";
            this.dataGridView1a.Columns[8].Width = 70;
            this.dataGridView1a.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView1a.Columns[9].Name = "OtRate";
            this.dataGridView1a.Columns[9].Width = 50;
            this.dataGridView1a.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1a.Columns[10].Name = "Period";
            this.dataGridView1a.Columns[10].Width = 50;
            this.dataGridView1a.Columns[10].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView1a.Columns[11].Name = "MP.Control";
            this.dataGridView1a.Columns[11].Width = 80;
            this.dataGridView1a.Columns[11].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView1a.Columns[12].Name = "SectionFrom";
            this.dataGridView1a.Columns[12].Width = 250;
            this.dataGridView1a.Columns[12].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1a.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView1a.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView1a.RowHeadersWidth = 4;
            this.dataGridView1a.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView1a.RowTemplate.Height = 25;
            dataGridView1a.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1a.AllowUserToResizeRows = false;
            dataGridView1a.AllowUserToResizeColumns = false;

        }

        private void InitialChart()
        {
            ChartList[0] = chartNag1;
            ChartList[1] = chartNag2;
            ChartList[2] = chartNag3;
            ChartList[3] = chartNag4;
            ChartList[4] = chartNag5;
            ChartList[5] = chartNag6;

            ChartHisList[0] = chartHis1;
            ChartHisList[1] = chartHis2;
            ChartHisList[2] = chartHis3;
            ChartHisList[3] = chartHis4;
            ChartHisList[4] = chartHis5;
            ChartHisList[5] = chartHis6;

            LbList[0] = label13;
            LbList[1] = label34;
            LbList[2] = label38;
            LbList[3] = label42;
            LbList[4] = label50;
            LbList[5] = label46;

            SDList[0] = label21;
            SDList[1] = label28;
            SDList[2] = label60;
            SDList[3] = label68;
            SDList[4] = label76;
            SDList[5] = label84;

            MinList[0] = label20;
            MinList[1] = label54;
            MinList[2] = label62;
            MinList[3] = label70;
            MinList[4] = label78;
            MinList[5] = label86;

            MaxList[0] = label23;
            MaxList[1] = label56;
            MaxList[2] = label64;
            MaxList[3] = label72;
            MaxList[4] = label80;
            MaxList[5] = label88;

        }

        #endregion

        private void SerialReceiveEvent(string productionlabel)
        {
            ClearDataInTableAndChart();
            if (productionlabel != null)
            {
                string lb = productionlabel;
                lbReading.Text = lb;

                if (lb.Length > 30)
                {
                    string partnumber = String.Format("{0}-{1}", lb.Substring(0, 8), lb.Substring(8, 6));
                    int yy = int.Parse(lb.Substring(14, 2));
                    int mm = int.Parse(lb.Substring(16, 2));
                    int dayinmonth = 1;
                    if (mm > 0)
                    {
                        dayinmonth = DateTime.DaysInMonth(yy, mm);
                    }
                    else if (mm == 0)
                    {
                        productionId = "";
                        //   productionlabel = "";
                        MessageBox.Show("QR TAG is not match pattern", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int dd = int.Parse(lb.Substring(18, 2));
                    int yynow = DateTime.Now.Year;
                    int hh = int.Parse(lb.Substring(24, 2));
                    int mmm = int.Parse(lb.Substring(26, 2));
                    int ss = int.Parse(lb.Substring(28, 2));
                    if (dd <= dayinmonth && mm <= 12 && yy <= yynow && hh < 24 && mmm < 60 && ss < 60)
                    {

                        //string datetime = string.Format("{0}-{1}-20{2} {3}:{4}:{5}", lb.Substring(18, 2), lb.Substring(16, 2),
                        //    lb.Substring(14, 2), lb.Substring(24, 2), lb.Substring(26, 2), lb.Substring(28, 2));
                        string qno = lb.Substring(20, 4);
                        label6.Text = qno;

                        TbPn1.Text = lb.Substring(0, 8);
                        TbPn2.Text = lb.Substring(8, 6);
                        TbYear.Text = lb.Substring(14, 2);
                        TbMonth.Text = lb.Substring(16, 2);
                        TbDay.Text = lb.Substring(18, 2);
                        TbHour.Text = lb.Substring(24, 2);
                        TbMinute.Text = lb.Substring(26, 2);
                        TbSecond.Text = lb.Substring(28, 2);




                        productedTime = new DateTime(int.Parse("20" + lb.Substring(14, 2)), int.Parse(lb.Substring(16, 2)), int.Parse(lb.Substring(18, 2)),
                            int.Parse(lb.Substring(24, 2)), int.Parse(lb.Substring(26, 2)), int.Parse(lb.Substring(28, 2)));

                        //TimeClock t = new TimeClock();
                        //string dn = t.FindDayOrNight(productedTime);
                        string dn = OABorad.FindDayOrNight(productedTime);
                        LoadHistoryData(partnumber, productedTime, dn);

                    }
                    else
                    {
                      
                        MessageBox.Show("Error");
                        return;
                    }

                }
            }
        }

        private void ClearDataInTableAndChart()
        {
            dataGridView1a.Rows.Clear();
            chart1.Series.Clear();
            chart3.Series.Clear();
            ChartMC.Series.Clear();
            ChartMethod.Series.Clear();
            ChartSD.Series.Clear();
            for (int i = 0; i < 6; i++)
            {
                ChartList[i].Series.Clear();
                ChartHisList[i].Series.Clear();
            }
        }
        private void LoadHistoryData(string partnumber, DateTime datetime, string dayNight)
        {
            string section = "4464-00";//User.SectionCode;
            dayNight = "D";
            string division = "ESD";
            //DateTime registdate = new DateTime(datetime.Year, datetime.Month, datetime.Day);
            DateTime registdate = new DateTime(2022, 01, 13);
            DateTime startdate = registdate.AddDays(-7);
            DateTime enddate = registdate.AddDays(7);

            using (var db = new ProductionEntities11())
            {
                /// Man power register /////////////////////////////////
                var empManpower = db.Emp_ManPowerRegistedTable
                    .Where(a => a.registDate == registdate)
                    .Where(s => s.sectionCode == section)
                    .Where(d => d.DayNight == dayNight);
                var emp = (from e in empManpower
                           join f in db.Emp_MPFunctionTable
                           on e.functionID equals f.functionID
                           join s in db.Emp_SectionTable
                           on e.sectionCode equals s.sectionCode
                           join ss in db.Emp_SectionTable
                           on e.sectionCodeFrom equals ss.sectionCode
                           join m in db.Emp_ManPowersTable
                           on e.userID equals m.userID
                           join di in db.Emp_DecIncNameTable
                           on e.DecInc equals di.decInc
                           select new TracEmp
                           {
                               shift = e.shiftAB,
                               workType = e.workType,
                               userId = e.userID,
                               fullname = m.fullName,
                               rate = e.rate,
                               period = e.period,
                               function = f.functionName,
                               rateOT = e.rateOT,
                               functionOT = f.functionName,
                               periodOT = e.periodOT,
                               mpControl = di.decIncName,
                               fromSection = ss.sectionName
                           }).ToList();
                //  production  good work volume  /////////////////////////////////////////////
                var goodWork = db.Prod_RecordTable.Where(s => s.sectionCode == section)
                    .Where(r => r.registDate >= startdate && r.registDate <= enddate)
                    .GroupBy(r => new { r.registDate, r.partNumber }).Select(r => new TracProductionVolume
                    {
                        registdate = r.Key.registDate ?? startdate,
                        partnumber = r.Key.partNumber,
                        volume = r.Count()
                    });

                var allVolume = goodWork.GroupBy(r => r.registdate)
                    .Select(r => new TracProductionVolume
                    {
                        registdate = r.Key,
                        volume = r.Sum(s => s.volume)
                    }).ToList();

                var specVolume = goodWork.Where(p => p.partnumber == partnumber).GroupBy(r => r.registdate)
                    .Select(r => new TracProductionVolume
                    {
                        registdate = r.Key,
                        volume = r.Sum(s => s.volume)
                    }).ToList();

                var volume = (from a in allVolume
                              join s in specVolume
                              on a.registdate equals s.registdate
                              select new TracProductionVolume
                              {
                                  registdate = a.registdate,
                                  partnumber = a.partnumber,
                                  volume = s.volume,
                                  volumeOther = a.volume - s.volume,

                              }).ToList();

                //  machine downtime  //////////////////////////////////////////////////

                var loss = db.oeeRelatedLosses.Where(a => a.division_id == division).ToList();
                var machineloss = db.Loss_RecordTable.Where(a => a.registDate == registdate)
                    .Where(s => s.sectionCode == section).ToList();

                var mcloss = (from m in machineloss
                              join l in loss
                              on m.lossID equals l.loss_id
                              select new TracMachineLoss
                              {
                                  machineId = m.MCNumber,
                                  oeeId = l.oee_id,
                                  losstime = m.Second ?? 0
                              }).ToList();
                var machinelosstime1 = mcloss.Where(p => p.oeeId == "P1").GroupBy(m => m.machineId)
                    .Select(a => new TracMachineLoss
                    {
                        machineId = a.Key,
                        losstime = a.Sum(x => x.losstime)
                    }).ToList();
                var machinelosstime = (from l in machinelosstime1
                                       join m in db.Prod_MachineNameTable
                                       on l.machineId equals m.machineId
                                       select new TracMachineLoss
                                       {
                                           machineId = l.machineId,
                                           machineName = m.machineName,
                                           losstime = l.losstime,
                                       }).ToList();
                //  Machine time analysis  ////////////////////////////////////////
                var machineTime = db.ML_RecordTable.Where(s => s.sectionCode == section)
                     .Where(r => r.registDate >= startdate && r.registDate <= enddate).ToList();
                var machineIn = machineTime.GroupBy(m => m.mcNumber).Select(m => new TracMachine
                {
                    machinename = m.Key,
                }).ToList();
                foreach (TracMachine m in machineIn)
                {

                }

                // Cycle time analysis ////////////////////////////////////////////
                List<TracNagaraTime> nagaratime = db.ML_NagaraStartTimeTable.Where(s => s.SectionCode == section)
                     .Where(r => r.RegistDate >= startdate && r.RegistDate <= enddate)
                     .Where(d => d.DayNight == dayNight).Where(p => p.PartNumber == partnumber)
                     .Select(n => new TracNagaraTime
                     {
                         station = n.StationNo,
                         partnumber = n.PartNumber,
                         nagaratime = n.NagaraSWTime ?? 0
                     }).ToList();
                var station = nagaratime.GroupBy(s => s.station)
                    .Select(s => new TracCycleTime
                    {
                        station = s.Key
                    }).ToList();

                var nagaraDeviation = new List<Tracdeviation>();
                var paretoNagara = new List<TracParetoNagara>();
                foreach (TracCycleTime s in station)
                {
                    var nagara = nagaratime.Where(a => a.station == s.station).ToList();
                    double mean = 0.0;
                    double sum = 0.0;
                    double stdDev = 0.0;
                    int n = 0;
                    foreach (TracNagaraTime val in nagara)
                    {
                        n++;
                        double delta = val.nagaratime - mean;
                        mean += delta / n;
                        sum += delta * (val.nagaratime - mean);
                    }
                    if (1 < n)
                        stdDev = Math.Sqrt(sum / (n - 1));
                    nagaraDeviation.Add(new Tracdeviation { station = s.station, deviation = stdDev });

                    var maxValue = nagara.Max();
                    double max = maxValue.nagaratime < 50 ? maxValue.nagaratime : 50;
                                       
                    for (int i = 0; i < max; i += 2)
                    {
                        double st1 = i;
                        double st2 = i+2;
                        var pareto = nagara.Where(nn => nn.nagaratime >= st1 && nn.nagaratime < st2)
                            .GroupBy(ss => ss.station).Select(nn => new TracNagaraTimes
                            {
                                station = nn.Key,
                                nagaraTimes = nn.Count()
                            }).ToList();
                        paretoNagara.Add(new TracParetoNagara
                        {
                            station = s.station,
                            nagaraTimes = pareto.FirstOrDefault().nagaraTimes,
                            axis = st2
                        });
                    }
                }


                // Machine parameter monitor at date //////////////////////////////


            }

        }


        private void LoadHistoryData1(string partnumber, DateTime datetime, string dayNight)
        {
            dataGridView1a.Rows.Clear();
            string datetimestr = datetime.ToString("yyyy-MM-dd HH:mm:ss");
            //string sectiondivplant;// = string.Empty;
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SSS("TrackQualityExc", "@partNumber", partnumber, "@datetime", datetimestr, "@DN", dayNight);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];  // manpower register
                DataTable dt2 = ds.Tables[1];  //   part number problem  | part number other
                DataTable dt3 = ds.Tables[2];   // machine breakdaow  loss
                DataTable dt4 = ds.Tables[3]; // machine time    -7 day to + 7 day
                DataTable dt5 = ds.Tables[4]; // NagaraSwtich Time
                DataTable dt6 = ds.Tables[5];  //NagaraSwtich Time Station

                dtDT = ds.Tables[6]; //  Track_LoadMachineDownTimeSQL  by Click at Chart
                dtSD = ds.Tables[7]; //  Track_LoadMachineTime SD SQL  by Click at Chart
                DataTable dtMCname = ds.Tables[8]; // Machine name list up //

                //DataTable dtDefect = ds.Tables[9];

                //DataTable dt7 = ds.Tables[6]; // Histogram
                int row = dt1.Rows.Count;
                if (row > 0)
                {
                    for (int i = 0; i < row; i++)
                    {
                        object[] data = new object[dt1.Rows[i].ItemArray.Length + 1];
                        data[0] = 1 + i;
                        for (int j = 0; j < dt1.Rows[i].ItemArray.Length; j++)
                        {
                            data[j + 1] = dt1.Rows[i].ItemArray[j];
                        }
                        dataGridView1a.Rows.Add(data);   // P 1- 1
                    }
                }
                if (dt2.Rows.Count > 0)
                {
                    Color[] color = {Color.FromArgb(191, 255, 191),Color.FromArgb(255,127,127),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};
                    Charts.TwoStackOnePoint(dt2, chart1, color);
                }
                if (dt3.Rows.Count > 0)
                {
                    Color[] color = { Color.FromArgb(191, 255, 191),Color.FromArgb(255,127,127), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

                    //Chart_MachineDownTime(dt3, chartMC);  // P 1- 4
                    Charts.MachineDownTime(dt3, ChartMC, color);
                }
                if (dt4.Rows.Count > 0)
                {
                    Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                        Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};
                    Charts.MachineTime(dt4, ChartMethod, color);
                    Charts.ColumnSingleColorOneAxis(dt4, ChartSD, Color.FromArgb(255, 127, 127));
                }
                if (dt5.Rows.Count > 0)   // station , ct , datetime  P 2
                {
                    Chart_MachineTimeStation(dt5, dt6);
                }
                DicMCname.Clear();
                if (dtMCname.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtMCname.Rows)
                    {
                        DicMCname.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                    }
                }
                //if (dtDefect.Rows.Count > 0)
                //{
                //    Color[] color = {Color.FromArgb(191, 255, 191),Color.FromArgb(255,127,127),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                //        Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

                //    //ChartProductDefectHistory(dtDefect, chart3);
                //    Charts.DefectHistory(dtDefect, chart3, color);
                //}

            }



        }

        private void Chart_MachineTimeStation(DataTable datadt, DataTable station)
        {
            int sta = station.Rows.Count;
            for (int i = 0; i < sta; i++) //station.Rows.Count    
            {
                string stationName = station.Rows[i].ItemArray[0].ToString();
                string _sqlWhere = string.Format("StationNo='{0}'", stationName);
                string _sqlOrder = "RegistDateTime ASC";
                DataTable _newDataTable = datadt.Select(_sqlWhere, _sqlOrder).CopyToDataTable();
                string[] selectColume = new[] { "NagaraSWTime", "RegistDateTime" };
                DataTable dt2 = new DataView(_newDataTable).ToTable(false, selectColume);

                LbList[i].Text = stationName;
                int pos = ChartChangeColor(dt2);
                //Chart_NagaraSwitchTimeByStation(dt2, ChartList[i], stationName, pos);
                Charts.NagaraSwitchTimeByStation(dt2, ChartList[i], stationName, pos);

                List<double> nagara = new List<double>();
                foreach (DataRow dr in dt2.Rows)
                {
                    nagara.Add(Convert.ToDouble(dr.ItemArray[0]));
                }
                Histogram(nagara, ChartHisList[i]);
                //STDEV stdev = new STDEV();
                STDEV stdev = CalculateStandardDeviation(nagara);
                SDList[i].Text = stdev.Stdev.ToString();
                MaxList[i].Text = stdev.Max.ToString();
                MinList[i].Text = stdev.Min.ToString();

            }



        }


        private void Histogram(List<double> dt, Chart chart1)
        {
            Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170),
                Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta };

            double interval = 5;
            int n = dt.Count;
            double max = dt.Max();
            double min = dt.Min();
            double R = max - min;
            double classInterval = Math.Round((R / interval), 1, MidpointRounding.AwayFromZero);
            Dictionary<string, string> histogram = new Dictionary<string, string>();
            histogram.Clear();

            for (int i = 0; i < interval; i++)
            {
                double minspec = min + i * classInterval;
                double maxspec = min + (i + 1) * classInterval;
                int cnt = 0;
                for (int j = 0; j < n; j++)
                {
                    double sample = dt[j];
                    if ((minspec < sample) && (sample <= maxspec))
                    {
                        cnt += 1;
                    }
                }
                try
                {
                    histogram.Add(maxspec.ToString(), cnt.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Charts.NagaraSwitchTimeHistogram(histogram, chart1, color);


        }

        #region TAB PAGE


        #region TAB PAGE_2 : NagaraSwitchTimeHistogram DISPLAY


        private int ChartChangeColor(DataTable dt2)
        {
            int result = -1;
            //productedTime = new DateTime(2021, 1, 8, 9, 20, 0);

            int row = dt2.Rows.Count;
            for (int i = 0; i < row; i++)
            {
                DateTime date1 = Convert.ToDateTime(dt2.Rows[i].ItemArray[1]);
                if (i < row - 1)
                {
                    DateTime date2 = Convert.ToDateTime(dt2.Rows[i + 1].ItemArray[1]);
                    if (productedTime >= date1 && productedTime <= date2)
                    {
                        result = i;
                        break;
                    }
                }
            }
            return result;

        }


        private STDEV CalculateStandardDeviation(List<double> values)
        {
            STDEV result = new STDEV();
            double avg = values.Average();
            double sum = values.Sum(d => Math.Pow(d - avg, 2));
            double stdev = Math.Sqrt((sum) / values.Count - 1);
            result.Max = Math.Round(values.Max(), 1, MidpointRounding.AwayFromZero);
            result.Min = Math.Round(values.Min(), 1, MidpointRounding.AwayFromZero);
            result.Stdev = Math.Round(stdev, 2, MidpointRounding.AwayFromZero);
            return result;
        }


        #endregion



        #endregion


        #region chart config

        private int PieHitPointIndex(Chart pie, MouseEventArgs e)
        {
            HitTestResult hitPiece = pie.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            HitTestResult hitLegend = pie.HitTest(e.X, e.Y, ChartElementType.LegendItem);
            int pointIndex = -1;
            if (hitPiece.Series != null) pointIndex = hitPiece.PointIndex;
            if (hitLegend.Series != null) pointIndex = hitLegend.PointIndex;
            return pointIndex;
        }

        #endregion



        #region Service

      


        private void TbPn1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                TbPn2.Focus();
        }

        private void TbPn2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                TbDay.Focus();
        }

        private void TbDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                TbMonth.Focus();
        }

        private void TbMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                TbYear.Focus();
        }

        private void TbYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                TbHour.Focus();
        }

        private void TbHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                TbMinute.Focus();
        }

        private void TbMinute_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                TbSecond.Focus();
        }

        private void TbSecond_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                BtnScan.Focus();
        }





        #endregion
    }

    public class STDEV
    {
        public double Stdev = 0.0;
        public double Max = 0.0;
        public double Min = 0.0;

    }
}