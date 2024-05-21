using KPI.Class;
using KPI.DataContain;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace KPI.ProdForm
{
    public partial class TracebilityNewForm : Form
    {

        bool _loadManAndProdution = false;
        bool _loadNagaraAndMcTime = false;
        bool _loadMachineParameter = false;

        string _sectionCode;
        string _partnumber;
        DateTime _datetime = DateTime.Now;
        DateTime _registDate = DateTime.Now;
        string _dayNight;
        double _limitCT;

        private static SerialPort mySerialPort;
        private static string productionId;

        List<TracNagaraTime> _nagaratime = new List<TracNagaraTime>();
        List<TracMachineTimes> _machinetime = new List<TracMachineTimes>();
        List<TracMachineIdName> _machineIdName = new List<TracMachineIdName>();
        List<TracMachineIdName> _machineIdNameParameter = new List<TracMachineIdName>();
        List<oeeMachineTime> _limitMT = new List<oeeMachineTime>();
        List<TracPackingSlip> _dataPackingSlip = new List<TracPackingSlip>();

        public TracebilityNewForm()
        {
            InitializeComponent();
        }


        private void TracebilityNewForm_Load(object sender, EventArgs e)
        {
            InitialSetting();
        }

        private void InitialSetting()
        {
            CmbYear.SelectedIndexChanged -= CmbYear_SelectedIndexChanged;
            CmbMonth.SelectedIndexChanged -= CmbMonth_SelectedIndexChanged;
            string[] header = new string[] { "No", "Shift", "Worktype", "UserId", "Full name",
                "Function", "Rate", "Hr", "Function-OT", "Rate-OT", "Hr-OT", "MpControl", "From" };
            int[] width = new int[] { 30, 30, 50, 70, 150, 60, 40, 50, 80, 60, 50, 80, 250 };
            DataGridViewSetup.Norm1(DgvManpower, header, width);

            InitialComponent.BoxDateTimeInitial(CmbYear, CmbMonth, CmbDay, CmbHH, CmbMM, CmbSS);

            CmbYear.SelectedIndexChanged += CmbYear_SelectedIndexChanged;
            CmbMonth.SelectedIndexChanged += CmbMonth_SelectedIndexChanged;
            SerialPortSetting.Initial(CmbPort, CmbRate, CmbParity, CmbData, CmbStop);

            InitialComponent.TracUserInputQRmapping(ListBoxMapping);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])
            {
                if (_loadNagaraAndMcTime == false)
                    NagaraAndMachineTime();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                if (_loadManAndProdution == false)
                    ManAndProdcution();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
            {
                if (_loadMachineParameter == false)
                {
                    string section = _sectionCode;
                    if (section == null)
                        return;
                    string[] str = section.Split('-');
                    if (str.Length < 2)
                        return;

                    switch (str[0].Trim())
                    {
                        case "4464":

                            break;
                        case "4314":
                            MachineParameter4314();
                            break;
                        case "4318":

                            break;

                    }


                }

            }

        }


        #region 1. User Input
        private void BtnCheckpart_Click(object sender, EventArgs e)
        {
            MachineParameter();
        }

        private void Tb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                MachineParameter();
            }
        }
        private void MachineParameter()
        {
            string partnumber;
            string slot2 = Tb2.Text.Trim();
            partnumber = slot2 == "" ? String.Format($"{Tb1.Text.Trim()}") : String.Format($"{Tb1.Text.Trim()}-{slot2}");

            using (var db = new ProductionEntities11())
            {
                var findInNettime = db.Prod_NetTimeTable.Where(p => p.partNumber == partnumber);
                if (findInNettime.Any() == false)
                {
                    TextBoxPartNumber.Text = "no part number";
                    ListBoxPartnumber.Items.Clear();
                }
                else
                {
                    LbPartNumber.Text = partnumber;
                    TextBoxPartNumber.Text = partnumber;
                    _partnumber = partnumber;
                    ListBoxPartnumber.Items.Clear();
                    var findInNettime1 = findInNettime.GroupBy(x => x.sectionCode).Select(y => y.FirstOrDefault()).ToList();
                    var sectionIdName = (from id in findInNettime1
                                         join s in db.Emp_SectionTable
                                         on id.sectionCode equals s.sectionCode
                                         select new EmpSection
                                         {
                                             SectionCode = id.sectionCode,
                                             SectionName = s.sectionName,
                                         }).ToList();

                    foreach (EmpSection p in sectionIdName)
                    {
                        ListBoxPartnumber.Items.Add($"{p.SectionCode} = {p.SectionName}");
                    }
                    ListBoxPartnumber.SelectedIndex = 0;
                }



            }
        }

        private void CmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitialComponent.BoxDateTime(CmbYear, CmbMonth, CmbDay);
        }
        private void CmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitialComponent.BoxDateTime(CmbYear, CmbMonth, CmbDay);
        }
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            PreparaingData();
        }
        private void PreparaingData()
        {
            if (ListBoxPartnumber.Items.Count == 0)
                return;

            _loadManAndProdution = false;
            _loadNagaraAndMcTime = false;
            _loadMachineParameter = false;

            _nagaratime.Clear();
            _machinetime.Clear();
            _machineIdName.Clear();
            _machineIdNameParameter.Clear();
            _limitMT.Clear();
            _dataPackingSlip.Clear();

            ChartDefectWork.Series.Clear();
            ChartDown.Series.Clear();
            ChartUp.Series.Clear();
            ChartGoodWork.Series.Clear();
            ChartMCdown.Series.Clear();
            ChartMCTimeColumn.Series.Clear();
            ChartMCTimeLine.Series.Clear();
            ChartNagaraColumn.Series.Clear();
            ChartNagaraLine.Series.Clear();

            ListBoxDown.Items.Clear();
            ListBoxUp.Items.Clear();
            NagaralistBox.Items.Clear();
            MClistBox.Items.Clear();

            if (ListBoxPartnumber.SelectedIndex == -1)
            {
                ListBoxPartnumber.SelectedIndex = 0;
            }
            string[] section = ListBoxPartnumber.SelectedItem.ToString().Split('=');
            if (section.Length != 2)
                return;
            _sectionCode = section[0];



            _datetime = InitialComponent.BoxDateTimeConvert(CmbYear, CmbMonth, CmbDay, CmbHH, CmbMM, CmbSS);

            _dayNight = RegistDateTime.FindDayOrNight(_datetime);

            _registDate = RegistDateTime.FindRegistDateFromCurrentTime(_datetime);
            LbDateTime.Text = String.Format($"{_datetime: MMMM dd, yyyy} Time {_datetime:HH:mm:ss}");

            using (var db = new ProductionEntities11())
            {
                var ctExist = db.oeeMachineTimes.Where(s => s.section_id == _sectionCode).Where(p => p.part_number == _partnumber);
                if (ctExist.Any())
                {
                    _limitMT = ctExist.ToList();
                    var ct = ctExist.FirstOrDefault();
                    _limitCT = ct.ht_min_sec + ct.mt_min_sec;
                }
            }


        }



        private void BtnSave_Click(object sender, EventArgs e)
        {
            string lb = ListBoxMapping.SelectedItem.ToString();
            string[] str = lb.Split(':');
            if (str.Length < 2)
                return;

            var userInput = new TracUserInput();
            switch (str[0].Trim())
            {
                case "1": //1:Radiator mapping
                    userInput=  DataInputMappingRadiator(textBox1.Text);
                    break;
                case "2": //2:HVAC mappin
                    //DataInputMappingRadiator(textBox1.Text);
                    break;

                case "3": //3:AirBag mapping
                    //DataInputMappingRadiator(textBox1.Text);
                    break;

            }

            Tb1.Text = userInput.partnumber1;
            Tb2.Text = userInput.partnumber2;
            TextBoxPartNumber.Text = String.Format($"{Tb1.Text }-{Tb2.Text}");
            bool result = InitialComponent.BoxDateTimeForce(userInput,CmbYear, CmbMonth, CmbDay, CmbHH, CmbMM, CmbSS);
            if (!result)
                MessageBox.Show("DateTime over range");



        }
        private TracUserInput DataInputMappingRadiator(string lb)
        {
            var userInput = new TracUserInput();
          

            if (lb == null)
            {
                return userInput;
            }

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
                    MessageBox.Show("QR TAG is not match pattern", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return userInput;
                }
                int dd = int.Parse(lb.Substring(18, 2));
                int yynow = DateTime.Now.Year;
                int hh = int.Parse(lb.Substring(24, 2));
                int mmm = int.Parse(lb.Substring(26, 2));
                int ss = int.Parse(lb.Substring(28, 2));
                if (dd <= dayinmonth && mm <= 12 && yy <= yynow && hh < 24 && mmm < 60 && ss < 60)
                {
                    userInput.partnumber1 = string.Format($"{lb.Substring(0, 8)}");
                    userInput.partnumber2 = string.Format($"{ lb.Substring(8, 6)}");
                    userInput.year = Convert.ToInt32("20" + lb.Substring(14, 2));
                    userInput.month = Convert.ToInt32(lb.Substring(16, 2));
                    userInput.day = Convert.ToInt32(lb.Substring(18, 2));
                    userInput.unique = lb.Substring(20, 4);
                    userInput.hour = Convert.ToInt32(lb.Substring(24, 2));
                    userInput.minute = Convert.ToInt32(lb.Substring(26, 2));
                    userInput.second = Convert.ToInt32(lb.Substring(28, 2));
                    userInput.dateTime = new DateTime(userInput.year, userInput.month, userInput.day, userInput.hour, userInput.minute, userInput.second);
                }
                else
                {

                    MessageBox.Show("Error");
                   
                }
                
            }
            return userInput;
        }




        #endregion


        #region 2. Man_Production
        private void ManAndProdcution()
        {
            _loadManAndProdution = true;
            string section = _sectionCode;
            string dayNight = _dayNight;
            string partnumber = _partnumber;
            if (section == null)
                return;
            DateTime registdate = _registDate;
            DateTime startdate = registdate.AddDays(-7);
            DateTime enddate = registdate.AddDays(7);

            using (var db = new ProductionEntities11())
            {
                var bottleneck = db.oeeBottlenecks.Where(s => s.section_id == section).FirstOrDefault();
                if (bottleneck == null)
                    return;
                string machine = bottleneck.machine_id;
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
                ManpowerDisplay(DgvManpower, emp);
                //  production  good work volume  /////////////////////////////////////////////
                var goodWork = db.ML_RecordTable.Where(s => s.sectionCode == section).Where(m => m.mcNumber == machine)
                    .Where(r => r.registDate >= startdate && r.registDate <= enddate).Where(r => r.OKNG == "OK")
                    .GroupBy(r => new { r.registDate, r.partNumber }).Select(r => new TracProductionVolume
                    {
                        registdate = r.Key.registDate,
                        partnumber = r.Key.partNumber,
                        volume = r.Count()
                    }).ToList();

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
                var sortRegistdate = volume.OrderBy(x => x.registdate).ToList();
                Color[] color = { Color.FromArgb(191, 255, 191), Color.FromArgb(255, 127, 127) };
                Charts.TracProductionHistory(sortRegistdate, ChartGoodWork, color);
                ChartLegent.LegendTracGoodDefectProduction(tableLayoutPanel20, 1, "tableLayoutPanel34", "sixChartloss", color, LegendPlate.LegendGoodDefect());

                //  production  defect work volume  /////////////////////////////////////////////
                var defectAllWork = db.ML_RecordTable.Where(s => s.sectionCode == section).Where(m => m.mcNumber == machine)
                    .Where(r => r.registDate >= startdate && r.registDate <= enddate).Where(r => r.OKNG == "NG")
                    .GroupBy(r => new { r.registDate, r.partNumber }).Select(r => new TracProductionVolume
                    {
                        registdate = r.Key.registDate,
                        partnumber = r.Key.partNumber,
                        volume = r.Count()
                    }).ToList();

                var defectAllVolume = defectAllWork.GroupBy(r => r.registdate)
                    .Select(r => new TracProductionVolume
                    {
                        registdate = r.Key,
                        volume = r.Sum(s => s.volume)
                    }).ToList();

                var defectSpecVolume = defectAllWork.Where(p => p.partnumber == partnumber).GroupBy(r => r.registdate)
                    .Select(r => new TracProductionVolume
                    {
                        registdate = r.Key,
                        volume = r.Sum(s => s.volume)
                    }).ToList();

                var defectvolume = (from a in defectAllVolume
                                    join s in defectSpecVolume
                              on a.registdate equals s.registdate
                                    select new TracProductionVolume
                                    {
                                        registdate = a.registdate,
                                        partnumber = a.partnumber,
                                        volume = s.volume,
                                        volumeOther = a.volume - s.volume,

                                    }).ToList();
                var sortDefectRegistdate = defectvolume.OrderBy(x => x.registdate).ToList();

                Charts.TracProductionHistory(sortDefectRegistdate, ChartDefectWork, color);
                ChartLegent.LegendTracGoodDefectProduction(tableLayoutPanel22, 1, "tableLayoutPanel34", "sixChartloss", color, LegendPlate.LegendGoodDefect());


                //  machine downtime  //////////////////////////////////////////////////
                var div = db.Emp_SectionTable.FirstOrDefault(a => a.sectionCode == section);
                string division = div.sectionCode;

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
                var sortLossTime = machinelosstime.OrderByDescending(x => x.losstime).ToList();
                Color[] color1 = { Color.FromArgb(191, 255, 191), Color.FromArgb(255, 127, 127) };
                Charts.TracMachineDownTime(sortLossTime, ChartMCdown, color1, registdate.ToString("d-M-yyyy"));
                ChartLegent.LegendTracMachinedowntime(tableLayoutPanel34, 1, "tableLayoutPanel34", "sixChartloss", sortLossTime);


            }

        }
        private void ManpowerDisplay(DataGridView dgv, List<TracEmp> manpower)
        {
            int n = 0;
            foreach (TracEmp m in manpower)
            {
                dgv.Rows.Add(n + 1, m.shift, m.workType, m.userId, m.fullname, m.function,
                    m.rate, m.period, m.functionOT, m.rateOT, m.periodOT, m.mpControl, m.fromSection);
                n++;
            }
        }

        #endregion


        #region 3. Nagara & Machine Time
        private void NagaraAndMachineTime()
        {
            string section = _sectionCode;
            string dayNight = _dayNight;
            string partnumber = _partnumber;
            _loadNagaraAndMcTime = true;
            if (section == null)
                return;
            DateTime registdate = _registDate;
            DateTime startdate = registdate.AddDays(-7);
            DateTime enddate = registdate.AddDays(7);

            using (var db = new ProductionEntities11())
            {
                // Cycle time analysis ////////////////////////////////////////////
                _nagaratime = db.ML_NagaraStartTimeTable.Where(s => s.SectionCode == section)
                     .Where(r => r.RegistDate >= startdate && r.RegistDate <= enddate)
                     .Where(d => d.DayNight == dayNight).Where(p => p.PartNumber == partnumber)
                     .Select(n => new TracNagaraTime
                     {
                         station = n.StationNo,
                         partnumber = n.PartNumber,
                         nagaratime = n.NagaraSWTime ?? 0,
                         dateTime = n.RegistDateTime ?? DateTime.Now,
                     }).ToList();
                var station = _nagaratime.GroupBy(s => s.station)
                    .Select(s => new TracCycleTime
                    {
                        station = s.Key
                    }).ToList();

                NagaralistBox.Items.Clear();
                foreach (TracCycleTime s in station)
                {
                    string stationNo = String.Format($"station :{s.station}");
                    NagaralistBox.Items.Add(stationNo);
                }

                var bottleneck = db.oeeBottlenecks.Where(s => s.section_id == section).FirstOrDefault();
                if (bottleneck == null)
                    return;
                string machine = bottleneck.machine_id;
                // machine time analysis ////////////////////////////////////////////
                _machinetime = db.ML_RecordTable.Where(s => s.sectionCode == section)
                    .Where(r => r.registDate >= startdate && r.registDate <= enddate)
                    .Where(p => p.partNumber == partnumber)
                    .Select(n => new TracMachineTimes
                    {
                        machineId = n.mcNumber,
                        partnumber = n.partNumber,
                        machinetime = n.mcTimeSec,


                    }).ToList();

                var machinetotal = _machinetime.GroupBy(s => s.machineId)
                    .Select(s => new TracMachineId
                    {
                        machineId = s.Key
                    }).ToList();

                _machineIdName = (from m in machinetotal
                                  join stdmc in db.Prod_MachineNameTable
                                  on m.machineId equals stdmc.machineId
                                  select new TracMachineIdName
                                  {
                                      machineId = m.machineId,
                                      machineName = stdmc.machineName,
                                  }).ToList();

                MClistBox.Items.Clear();
                foreach (TracMachineIdName s in _machineIdName)
                {
                    MClistBox.Items.Add(s.machineName);
                }


            }
        }

        private void NagralistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = NagaralistBox.SelectedItem.ToString();
            if (selected == "")
                return;
            string[] str = selected.Split(':');
            if (str.Length < 2)
                return;

            string stationNo = str[1];

            var paretoNagara = new List<TracParetoNagara>();

            var nagara = _nagaratime.Where(a => a.station == stationNo).ToList();
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
                stdDev = Math.Round(Math.Sqrt(sum / (n - 1)), 2, MidpointRounding.AwayFromZero);

            var maxValue = nagara.Max(x => x.nagaratime);
            var minValue = nagara.Min(x => x.nagaratime);

            double max = maxValue < _limitCT * 2.0 ? maxValue : _limitCT * 2.0;
            double min = minValue > _limitCT * 0.2 ? minValue : _limitCT * 0.2;
            int maximumScaleX = (int)max;
            int minimumScaleX = (int)min;

            for (int i = minimumScaleX; i < maximumScaleX; i += 2)
            {
                double st1 = i;
                double st2 = i + 2;

                int pareto = nagara.Where(nn => nn.nagaratime >= st1 && nn.nagaratime < st2).ToList().Count();


                paretoNagara.Add(new TracParetoNagara
                {
                    station = stationNo,
                    nagaraTimes = pareto,
                    axis = st2
                });
            }

            var nagaraPareto = paretoNagara.OrderBy(x => x.axis).ToList();

            LbNagara.Text = String.Format($"Nagara Time of station No. {stationNo} has minimum = {minValue} sec, maximum = {maxValue} sec, standard diviation = {stdDev}");

            Color[] color = { Color.FromArgb(191, 255, 191), Color.FromArgb(255, 127, 127) };
            Charts.NagaraSwitchTimeHistogram2(nagaraPareto, ChartNagaraColumn, color, minimumScaleX, maximumScaleX + 1);
            var cyclemtime = nagara.OrderBy(x => x.dateTime).ToList();
            Charts.NagaraSwitchTimeByStation2(cyclemtime, ChartNagaraLine);



        }

        private void MClistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = MClistBox.SelectedItem.ToString();
            if (selected == "")
                return;
            var machineExist = _machineIdName.Where(s => s.machineName == selected);
            if (machineExist.Any() == false)
                return;
            string machineId = machineExist.FirstOrDefault().machineId;

            var paretoMachine = new List<TracParetoMachine>();

            var machineTime = _machinetime.Where(a => a.machineId == machineId).ToList();
            double mean = 0.0;
            double sum = 0.0;
            double stdDev = 0.0;
            int n = 0;
            foreach (TracMachineTimes val in machineTime)
            {
                n++;
                double delta = val.machinetime - mean;
                mean += delta / n;
                sum += delta * (val.machinetime - mean);
            }
            if (1 < n)
                stdDev = Math.Round(Math.Sqrt(sum / (n - 1)), 2, MidpointRounding.AwayFromZero);

            var maxValue = machineTime.Max(x => x.machinetime);
            var minValue = machineTime.Min(x => x.machinetime);

            var mtMinExist = _limitMT.Where(m => m.machine_id == machineId);
            double mtMin = mtMinExist.Any() == true ? mtMinExist.FirstOrDefault().mt_min_sec : 0;


            double max = maxValue < mtMin * 2.0 ? maxValue : mtMin * 2.0;
            double min = minValue > mtMin * 0.2 ? minValue : mtMin * 0.2;
            double diff = max - min;
            int step = diff < 5 ? 1 : 2;

            int maximumScaleX = (int)max + 1;
            int minimumScaleX = (int)min - 1;

            for (int i = minimumScaleX; i < maximumScaleX; i += step)
            {
                double st1 = i;
                double st2 = i + step;

                int pareto = machineTime.Where(nn => nn.machinetime >= st1 && nn.machinetime < st2).ToList().Count();

                paretoMachine.Add(new TracParetoMachine
                {
                    machineId = machineId,
                    machineTime = pareto,
                    axis = st2
                });
            }

            var mtPareto = paretoMachine.OrderBy(x => x.axis).ToList();

            LbMachineTime.Text = String.Format($"Machine Time of {selected} MC has minimum = {minValue} sec, maximum = {maxValue} sec, standard diviation = {stdDev}");

            Color[] color = { Color.FromArgb(191, 255, 191), Color.FromArgb(255, 127, 127) };
            Charts.MachineTimeHistogram2(mtPareto, ChartMCTimeColumn, color);

            var machineTime1 = machineTime.OrderBy(x => x.dateTime).ToList();
            Charts.MachineTimeByMachineId2(machineTime1, ChartMCTimeLine);

        }

        #endregion


        #region 4.Machine Parameter
        private void MachineParameter4314()
        {
            string section = _sectionCode;
            string partnumber = _partnumber;
            DateTime registdate = _registDate;
            string dayNight = _dayNight;

            if (section == null)
                return;

            Color[] color = {Color.Black,Color.Blue,Color.FromArgb(170,130,0), Color.FromArgb(255, 0, 255),
                    Color.FromArgb(0, 231,0), Color.FromArgb(255, 105, 54),
                    Color.FromArgb(78, 213, 251), Color.FromArgb(101, 85, 252),Color.FromArgb(255, 0, 0) };
            using (var db = new ProductionEntities11())
            {

                _machineIdNameParameter = db.Prod_MachineNameTable.Where(s => s.sectionCode == section)
                    .Select(x => new TracMachineIdName
                    {
                        machineId = x.machineId,
                        machineName = x.machineName,
                    }).ToList();

                ListBoxUp.Items.Clear();
                foreach (TracMachineIdName m in _machineIdNameParameter)
                {
                    ListBoxUp.Items.Add(m.machineName);
                }

            }



        }

        private void ListBoxUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string section = _sectionCode;
            string partnumber = _partnumber;
            DateTime registdate = _registDate;
            string dayNight = _dayNight;

            var machineIdExist = _machineIdNameParameter.Where(n => n.machineName == ListBoxUp.SelectedItem.ToString());
            if (machineIdExist.Any() == false)
                return;
            string machineId = machineIdExist.FirstOrDefault().machineId;
            switch (machineId)
            {
                case "6SM-0626":
                    PackingSlipMachine(machineId);
                    break;
                default:
                    ChartUp.Series.Clear();
                    ChartDown.Series.Clear();
                    ListBoxDown.Items.Clear();
                    ChartLegent.LegendTracInital(tableLayoutPanel8, 1, "tableLayoutPanel34", "sixChartloss");
                    ChartLegent.LegendTracInital(tableLayoutPanel9, 1, "tableLayoutPanel34", "sixChartloss");
                    break;
            }

        }


        private void PackingSlipMachine(string machineId)
        {
            string section = _sectionCode;
            string partnumber = _partnumber;
            DateTime registdate = _registDate;
            string dayNight = _dayNight;
            Color[] color = { Color.FromArgb(255, 0, 255),
                    Color.FromArgb(0, 231,0), };

            _dataPackingSlip.Clear();
            using (var db = new WGREntities())
            {

                _dataPackingSlip = db.RD_PackingSlipTable.Where(s => s.sectionCode == section).Where(d => d.dayNight == dayNight)
                   .Where(r => r.registDate == registdate).Where(p => p.partNumber == partnumber).Where(m => m.machineId == machineId)
                   .Select(a => new TracPackingSlip
                   {
                       currentDateTime = a.currentDateTime,
                       maxLeftTop = a.maxLeftTop,
                       minLeftTop = a.minLeftTop,
                       maxRightTop = a.maxRightTop,
                       minRightTop = a.minRightTop,
                       maxLeftBottom = a.maxLeftBottom,
                       minLeftBottom = a.minLeftBottom,
                       maxRightBottom = a.maxRightBottom,
                       minRightBottom = a.minRightBottom,
                       numberDataTop = a.numberDataTop,
                       numberDataBottom = a.numberDataBottom,
                       judgeResult = a.judgeResult,
                   }).ToList();

                ListBoxDown.Items.Clear();
                foreach (string l in LegendPlate.LegendPackingSlip())
                {
                    ListBoxDown.Items.Add(l);
                }
                ListBoxDown.SelectedIndex = 0;
                SelectPackingSlip();

                List<TracPackingSlipSelect> packingSlip = new List<TracPackingSlipSelect>();
                foreach (TracPackingSlip s in _dataPackingSlip)
                {
                    var value = new TracPackingSlipSelect()
                    {
                        currentDateTime = s.currentDateTime,
                        max = s.numberDataTop,
                        min = s.numberDataBottom,
                    };
                    packingSlip.Add(value);
                }
                var data = packingSlip.OrderBy(x => x.currentDateTime).ToList();
                Charts.MachineParameter4314(data, ChartUp, color, "packing slip check", "number of data");

                ChartLegent.LegendTracPackingSlip(tableLayoutPanel9, 1, "tableLayoutPanel34", "sixChartloss", color, LegendPlate.LegendPackingSlip1());

            }

        }

        private void ListBoxDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectPackingSlip();
        }

        private void SelectPackingSlip()
        {
            string selected = ListBoxDown.SelectedItem.ToString();
            Color[] color = { Color.FromArgb(255, 0, 255),
                    Color.FromArgb(0, 231,0), Color.FromArgb(255, 105, 54),
                    Color.FromArgb(78, 213, 251), Color.FromArgb(101, 85, 252),Color.FromArgb(255, 0, 0) };

            List<TracPackingSlipSelect> packingSlip = new List<TracPackingSlipSelect>();
            switch (selected)
            {
                case "LeftTop":
                    foreach (TracPackingSlip s in _dataPackingSlip)
                    {
                        var value = new TracPackingSlipSelect()
                        {
                            currentDateTime = s.currentDateTime,
                            max = s.maxLeftTop,
                            min = s.minLeftTop,
                        };
                        packingSlip.Add(value);
                    }
                    break;
                case "RightTop":
                    foreach (TracPackingSlip s in _dataPackingSlip)
                    {
                        var value = new TracPackingSlipSelect()
                        {
                            currentDateTime = s.currentDateTime,
                            max = s.maxRightTop,
                            min = s.minRightTop,
                        };
                        packingSlip.Add(value);
                    }
                    break;
                case "LeftBottom":
                    foreach (TracPackingSlip s in _dataPackingSlip)
                    {
                        var value = new TracPackingSlipSelect()
                        {
                            currentDateTime = s.currentDateTime,
                            max = s.maxLeftBottom,
                            min = s.minLeftBottom,
                        };
                        packingSlip.Add(value);
                    }
                    break;
                case "RightBottom":
                    foreach (TracPackingSlip s in _dataPackingSlip)
                    {
                        var value = new TracPackingSlipSelect()
                        {
                            currentDateTime = s.currentDateTime,
                            max = s.maxRightBottom,
                            min = s.minRightBottom,
                        };
                        packingSlip.Add(value);
                    }
                    break;
            }


            var data = packingSlip.OrderBy(x => x.currentDateTime).ToList();
            Charts.MachineParameter4314(data, ChartDown, color, "packing slip check", "Thickness (mm)");

            ChartLegent.LegendTracPackingSlip(tableLayoutPanel8, 1, "tableLayoutPanel34", "sixChartloss", color, LegendPlate.LegendPackingSlip2());

        }







        #endregion

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            InitialSerialPort();
        }
        private void InitialSerialPort()
        {
            int maxRetries = 5;
            const int sleepTimeInMs = 500;
            string loggingMessage = string.Empty;

            string comport = CmbPort.SelectedItem.ToString();

            string stopbit = CmbStop.SelectedItem.ToString();
            string parity = CmbParity.SelectedItem.ToString();
            mySerialPort = new SerialPort(comport)
            {
                BaudRate = Convert.ToInt32(CmbRate.SelectedItem.ToString()),
                Parity = (Parity)Enum.Parse(typeof(Parity), parity),
                StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopbit),
                DataBits = Convert.ToInt16(CmbData.SelectedItem.ToString())
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

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            productionId = sp.ReadExisting();
            mySerialPort.DiscardInBuffer();
            Console.WriteLine(productionId);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (productionId != null)
                textBox1.Text = productionId;
            productionId = null;
        }
    }
}
