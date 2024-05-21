using KPI.Class;
using KPI.DataContain;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static KPI.Class.GraphOEE;

namespace KPI.ProdForm
{
    public partial class OEEnewForm : Form
    {
        private string selectedRegistDate = string.Empty;
        private string selectedMachineId = string.Empty;
        private string selectedMachineName = string.Empty;
        internal string selectedId = string.Empty;


        internal Dictionary<string, string> MCName = new Dictionary<string, string>();

        List<ProdMachineName> machineName = new List<ProdMachineName>();

        List<OeeLineMonitor> oeeLine = new List<OeeLineMonitor>();
        List<OEEOeeMCMonitor> oeeMachine = new List<OEEOeeMCMonitor>();

        List<SevenLoss> legendSevenLoss = LegendPlate.SevenLoss();
        List<SevenLoss> legendOeeoutput = LegendPlate.OEEoutput();
        string  _machineId, _machineName = null;

        DateTime _date = DateTime.Now;

        public OEEnewForm()
        {
            InitializeComponent();
        }

        private void OEEnewForm_Load(object sender, EventArgs e)
        {
            InitialComponent.DateTimePickerPrevious(dateTimePickerStart);

            ChartLegent.LegendSevenLoss(tableLayoutPanel30, 1, "tableLayoutPanel34", "sixChartloss", legendSevenLoss);
            ChartLegent.LegendOEEmenu(tableLayoutPanel26, 1, "tableLayoutPanel34", "sixChartloss", legendOeeoutput);

            StartupPage();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            OeeDisplay();
        }

        private void ChartOEELine_MouseClick(object sender, MouseEventArgs e)
        {
            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);
            if (pointIndex >= 0)
            {
                DataPoint dp = pie.Series[0].Points[pointIndex];
                selectedRegistDate = dp.AxisLabel;

                string[] dat = selectedRegistDate.Split('-');
                int dd = Convert.ToInt32(dat[0]);
                int mm = Convert.ToInt32(dat[1]);
                int yy = Convert.ToInt32(dat[2]);
                DateTime registDate = new DateTime(2000+yy, mm, dd);
                _date = registDate;
                selectedRegistDate = registDate.ToString("yyyy-MM-dd");
                ChartFromOEELine(registDate);

            }

        }

        private void ChartMC_MouseClick(object sender, MouseEventArgs e)
        {
            DateTime registDateDtStart = dateTimePickerStart.Value;
        
            DateTime registDateDtStop = dateTimePickerStop.Value; 
        

            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);

            if (pointIndex < 0)
            {
                return;
            }
            DataPoint dp = pie.Series[9].Points[pointIndex];

            selectedMachineName = dp.AxisLabel;

            ChartFromOEEMachine(registDateDtStart, registDateDtStop);

        }

        private void ChartMCQ_MouseClick(object sender, MouseEventArgs e)
        {
            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);

            if (pointIndex >= 0)
            {
                DataPoint dp = pie.Series[0].Points[pointIndex];

                string lossLable = dp.AxisLabel;
                string message = legendSevenLoss.FirstOrDefault(x => x.lossCode == lossLable).description;
                string header = string.Format($"{lossLable} : Meaning ");
                MessageBox.Show(message, header, MessageBoxButtons.OK);

            }
        }





        #region OEE OVER ALL DISPLAY

        private void OeeDisplay()
        {
            DateTime registDateDtStart = dateTimePickerStart.Value;
            string registDateStart = registDateDtStart.ToString("yyyy-MM-dd");
            DateTime registDateDtStop = dateTimePickerStop.Value; ;
            string registDateStop = registDateDtStop.ToString("yyyy-MM-dd");
            ChartMC.Series.Clear();
            ChartMCday.Series.Clear();
            ChartMCA.Series.Clear();
            ChartMCQ.Series.Clear();
            if (true)
            {
                using (var db = new ProductionEntities11())
                {
                    oeeLine = db.oeeProductionLines.Where(s => s.section_id == User.SectionCode)
                        .Where(r => r.regist_date >= registDateDtStart && r.regist_date <= registDateDtStop)
                        .OrderBy(o => o.regist_date)
                        .Select(n => new OeeLineMonitor
                        {
                            registDate = n.regist_date,
                            A = n.availability,
                            P = n.performance,
                            Q = n.quality,
                            OEE = n.oee_summary
                        }).ToList();

                    machineName = db.Prod_MachineNameTable.Where(s => s.sectionCode == User.SectionCode)
                                    .Where(f => f.OeeFunc == true).Select(s => new ProdMachineName
                                    {
                                        machineId = s.machineId,
                                        machineName = s.machineName,
                                        sort = s.sort
                                    }).ToList();

                    oeeMachine = (from m in db.oeeMachines
                                .Where(r => r.regist_date >= registDateDtStart && r.regist_date <= registDateDtStop)
                                .Where(s => s.section_id == User.SectionCode)
                                  join p in db.Prod_MachineNameTable
                                  .Where(s => s.sectionCode == User.SectionCode)
                                    .Where(f => f.OeeFunc == true)
                                  on m.machine_id equals p.machineId
                                  select new OEEOeeMCMonitor
                                  {
                                      registDate = m.regist_date,
                                      machineID = m.machine_id,
                                      machineName = p.machineName,
                                      machineSort = p.sort,
                                      Loadingtime = m.loading_time,
                                      A1 = m.a3,
                                      A2 = m.p1+m.p2,
                                      A3 =0,

                                      A4 = m.a1 + m.a2,
                                      A5 = m.p4,
                                      A6 = m.no_good+m.re_work,
                                      A7 = m.p3,
                                      A8 = 0,

                                      OK = m.ok,
                                     
                                      Oee = m.ok / m.loading_time * 100
                                  }).ToList();


                    string datedisplay = $" {registDateDtStart:d-MM-yyyy} to {registDateDtStop:d-MM-yyyy} ";
                    Charts.LineNewOEEList(oeeLine, ChartOEELine, legendOeeoutput, datedisplay);



                }

            }

        }

        private void ChartFromOEELine(DateTime registDate)
        {
           

            List<OEEOeeMCMonitor> oeeMc = oeeMachine.Where(r => r.registDate == registDate).OrderBy(s => s.machineSort).ToList();

            Charts.OeeNewMachineList(oeeMc, ChartMC, legendOeeoutput, legendSevenLoss, _date);
            ChartMCday.Series.Clear();
            ChartMCA.Series.Clear();

            ChartMCQ.Series.Clear();
        }

        private void ChartFromOEEMachine(DateTime registDateDtStart, DateTime registDateDtStop)
        {
           
            DateTime registDate = _date; 

            _machineName = selectedMachineName;
            ProdMachineName machineId = machineName.Where(m => m.machineName == selectedMachineName).FirstOrDefault();
            if (machineId == null)
                return;

            _machineId = machineId.machineId;

            List<OEEDateNew> oeeMCByDate = oeeMachine
                .Where(n => n.machineID == machineId.machineId)
                .OrderBy(d => d.registDate).Select(o => new OEEDateNew
                {
                    Registdate = o.registDate,
                    A1 = o.A1,
                    A2 = o.A2,
                    A3 = o.A3,
                    A4 = o.A4,
                    A5 = o.A5,
                    A6 = o.A6,
                    A7 = o.A7,
                    A8 = o.A8,
                    OEE = o.OK / o.Loadingtime * 100
                }).ToList();


            Charts.MCOEEnewListByDay(oeeMCByDate, ChartMCday, legendOeeoutput, legendSevenLoss, machineId.machineId, selectedMachineName);

            OEEDateNew result = oeeMCByDate
                .Where(m => m.Registdate == registDate)
                .FirstOrDefault();

            List<SortedLoss> sortedloss = new List<SortedLoss>()
                {
                    new SortedLoss { LossCode="L1",Losstime=result.A1},
                    new SortedLoss { LossCode="L2",Losstime=result.A2},
                    new SortedLoss { LossCode="L3",Losstime=result.A3},
                    new SortedLoss { LossCode="L4",Losstime=result.A4},
                    new SortedLoss { LossCode="L5",Losstime=result.A5},
                    new SortedLoss { LossCode="L6",Losstime=result.A6},
                    new SortedLoss { LossCode="L7",Losstime=result.A7},
                    new SortedLoss { LossCode="L8",Losstime=result.A8},
                };

            var sl = sortedloss.OrderByDescending(x => x.Losstime);


            List<SevenLoss> legend = LegendPlate.SevenLoss();

            var losses = (from s in sortedloss
                          join n in legend
                          on s.LossCode equals n.lossCode
                          select new SevenLoss
                          {
                              lossCode = s.LossCode,
                              lossName = n.lossName,
                              lossTime = s.Losstime,
                              r = n.r,
                              g = n.g,
                              b = n.b
                          });

            double total = losses.Sum(x => x.lossTime);
            double timeaccu = 0;

            List<SevenLoss> losscode2 = new List<SevenLoss>();

            foreach (SevenLoss a in losses.OrderByDescending(x => x.lossTime))
            {
                timeaccu += a.lossTime;

                var result1 = new SevenLoss()
                {
                    lossCode = a.lossCode,
                    lossName = a.lossName,
                    lossTime = a.lossTime,
                    percent = timeaccu * 100 / total,
                    r=a.r,
                    g=a.g,
                    b=a.b,
                };
                losscode2.Add(result1);
            }

            var lossesResult = losscode2.OrderByDescending(x => x.lossTime).ToList();

            string datedisplay = string.Format($"{registDateDtStart:d-MM-yyyy} to {registDateDtStop:d-MM-yyyy}");
            Charts.OEEMCofLoss(lossesResult, ChartMCQ, _machineName,datedisplay);

            MachineDownTime(registDateDtStart, registDateDtStop, _machineId, User.SectionCode);
        }

        private void MachineDownTime(DateTime ds, DateTime de, string machineId, string section)
        {

            using (var db = new ProductionEntities11())
            {
                var sumloss = db.Loss_RecordTable.Where(x => x.registDate >= ds && x.registDate <= de)
                    .Where(x => x.mcStop == "S").Where(x => x.sectionCode == section).Where(x => x.MCNumber == machineId)
                    .GroupBy(a => a.errorCode).Select(n => new AlarmCode
                    {
                        alarmecode = n.Key,
                        alarmtime = n.Sum(x => x.Second ?? 0)
                    }).ToList();


                var alarm = db.Prod_MachineFaultCodeTable.Where(x => x.machineId == machineId).Where(s => s.sectionCode == section)
                    .Select(x => new AlarmCode
                    {
                        alarmecode = x.faultCode.ToString(),
                        alarmename = x.messageAlarm,
                    }).ToList();


                var alarmcode1 = (from s in sumloss
                                  join a in alarm
                                  on s.alarmecode equals a.alarmecode
                                  select new AlarmCode
                                  {
                                      alarmecode = s.alarmecode,
                                      alarmename = a.alarmename,
                                      alarmtime = s.alarmtime / 60,

                                  }).ToList();


                double total = alarmcode1.Sum(x => x.alarmtime);
                double timeaccu = 0;
                List<AlarmCode> alarmcode2 = new List<AlarmCode>();
                foreach (AlarmCode a in alarmcode1.OrderByDescending(x => x.alarmtime))
                {
                    timeaccu += a.alarmtime;

                    var result = new AlarmCode()
                    {
                        alarmecode = a.alarmecode,
                        alarmename = a.alarmename,
                        alarmtime = a.alarmtime,
                        percent = timeaccu * 100 / total,
                    };
                    alarmcode2.Add(result);
                }


                List<AlarmCode> alarmcode = alarmcode2.OrderByDescending(x => x.alarmtime).ToList();


                string datedisplay = string.Format($"{ds:d-MM-yyyy} to {de:d-MM-yyyy}");
                Charts.OEEMCofAlarmCode(alarmcode, ChartMCA, Color.FromArgb(199, 0, 20), _machineName, datedisplay);

                ChartLegent.LegendAlarmCode(tableLayoutPanel34, 1, "tableLayoutPanel34", "sixChartloss", Color.FromArgb(199, 0, 20), alarmcode);

            }

        }

        private void StartupPage()
        {
            DateTime registDateDtStart = dateTimePickerStart.Value;
            DateTime registDateDtStop = dateTimePickerStop.Value;
            OeeDisplay();

            var lastday = oeeLine.OrderByDescending(x => x.registDate);
            if (lastday.Any() == false)
                return;
            DateTime focusDate = lastday.FirstOrDefault().registDate;
            _date = focusDate;
            ChartFromOEELine(focusDate);

            var machineName = oeeMachine.Where(x => x.registDate == focusDate).FirstOrDefault();
            if (machineName == null)
                return;
            selectedMachineName = machineName.machineName;

            ChartFromOEEMachine(registDateDtStart, registDateDtStop );
        }

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






    }

}
