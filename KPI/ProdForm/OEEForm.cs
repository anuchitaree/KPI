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
    public partial class OEEForm : Form
    {
        private string selectedRegistDate = string.Empty;
        private string selectedMachineId = string.Empty;
        private string selectedMachineName = string.Empty;
        internal string selectedId = string.Empty;

    
        private readonly Color[] color = { Color.FromArgb(255, 255, 10), Color.FromArgb(255, 127, 127), Color.FromArgb(141, 93, 199), Color.FromArgb(0, 148, 118) };


        internal Dictionary<string, string> MCName = new Dictionary<string, string>();

        List<ProdMachineName> machineName = new List<ProdMachineName>();
        List<OEEOeeMCMonitor> oeeMachine = new List<OEEOeeMCMonitor>();
        public OEEForm()
        {
            InitializeComponent();
        }

        private void OEEForm_Load(object sender, EventArgs e)
        {
            InitialComponent.DateTimePickerPrevious(dateTimePickerStart);
        }


        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            OeeDisplay();
        }

        private void OeeSubIDFormClosed_Close(object sender, EventArgs e)
        {

        }

        /////========= Loop  Operation  =========///////////






        #region OEE OVER ALL DISPLAY

        private void OeeDisplay()
        {
            DateTime registDateDtStart = dateTimePickerStart.Value;
            string registDateStart = registDateDtStart.ToString("yyyy-MM-dd");
            DateTime registDateDtStop = dateTimePickerStop.Value; ;
            string registDateStop = registDateDtStop.ToString("yyyy-MM-dd");

            if (true)
            {
                using (var db = new ProductionEntities11())
                {
                    List<OeeLineMonitor> oeeLine = db.oeeProductionLines.Where(s => s.section_id == User.SectionCode)
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
                                      A1 = m.a1,
                                      A2 = m.a2,
                                      A3 = m.a3,
                                      P1 = m.p1,
                                      P2 = m.p2,
                                      P3 = m.p3,
                                      P4 = m.p4,
                                      OK = m.ok,
                                      NG = m.no_good,
                                      RE = m.re_work,
                                      A = m.availability,
                                      P = m.performance,
                                      Q = m.quality,
                                      Oee = m.oee_summary
                                  }).ToList();



                    Charts.LineOEEList(oeeLine, ChartOEELine, color);

                    ChartLegent.Legend_DTSingleColor(tableLayoutPanel34, 1, "tableLayoutPanel34", "sixChartloss", color[1], LegendPlate.GetAviName());
                    ChartLegent.Legend_DTSingleColor(tableLayoutPanel32, 1, "tableLayoutPanel32", "sixChartloss", color[2], LegendPlate.GetPerfName());
                    ChartLegent.Legend_DTSingleColor(tableLayoutPanel30, 1, "tableLayoutPanel30", "sixChartloss", color[3], LegendPlate.GetQualName());

                    Color[] color1 = { color[0], color[3], color[2], color[1] };

                    ChartLegent.Legend_DTMultiColor(tableLayoutPanel26, 1, "tableLayoutPanel26", "sixChartloss", color1, LegendPlate.GetOEEName());
                    ChartLegent.Legend_DTMultiColor(tableLayoutPanel36, 1, "tableLayoutPanel36", "sixChartloss", color1, LegendPlate.GetOEEName());


                }

            }

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



        private void chartOEELine_MouseClick(object sender, MouseEventArgs e)
        {
            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);
            if (pointIndex >= 0)
            {
                DataPoint dp = pie.Series[0].Points[pointIndex];
                selectedRegistDate = dp.AxisLabel;
                string[] dat = selectedRegistDate.Split('-');
                string registdate = string.Format("20{0}-{1}-{2}", dat[2], dat[1], dat[0]);
                DateTime registDate = Convert.ToDateTime(registdate);
                lbDate.Text = string.Format("Date : {0}-{1}-20{2}", dat[0], dat[1], dat[2]);

                List<OEEOeeMCMonitor> oeeMc = oeeMachine.Where(r => r.registDate == registDate).OrderBy(s => s.machineSort).ToList();

                Charts.OeemachineList(oeeMc, ChartMC, color);


            }
            else
            {
                lbMCA.Text = lbMCP.Text = lbMCQ.Text = string.Empty;
            }

            ChartMCday.Series.Clear();
            ChartMCA.Series.Clear();
            ChartMCP.Series.Clear();
            ChartMCQ.Series.Clear();
            TBLoading.Text = TBAPQ.Text = TBAsum.Text = TBPsum.Text = TBQsum.Text = string.Empty;

        }


        private void chartMC_MouseClick(object sender, MouseEventArgs e)
        {

            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);


            if (pointIndex >= 0)
            {
                DataPoint dp = pie.Series[0].Points[pointIndex];
                selectedMachineName = dp.AxisLabel;
                string[] dat = selectedRegistDate.Split('-');
                string registdate = string.Format("20{0}-{1}-{2}", dat[2], dat[1], dat[0]);
                DateTime registDate = Convert.ToDateTime(registdate);

                lbName.Text = selectedMachineName;
                ProdMachineName machineId = machineName.Where(m => m.machineName == selectedMachineName).FirstOrDefault();
                tbMCId.Text = machineId.machineId;

                List<OEEDate> oeeMCByDate = oeeMachine
                    .Where(n => n.machineID == machineId.machineId)
                    .OrderBy(d => d.registDate).Select(o => new OEEDate
                    {
                        Registdate = o.registDate,
                        A = o.A,
                        P = o.P,
                        Q = o.Q,
                        OEE = o.Oee
                    }).ToList();


                Charts.MCOEEListByDay(oeeMCByDate, ChartMCday, color);

                OEEOeeMCMonitor result = oeeMachine
                    .Where(m => m.registDate == registDate)
                    .Where(m => m.machineID == machineId.machineId)
                    .FirstOrDefault();

                Charts.OEEMCA(result, DataContain.Name.AvilabilityName, ChartMCA, color[1]);
                Charts.OEEMCP(result, DataContain.Name.PerformanceName, ChartMCP, color[2]);
                Charts.OEEMCQ(result, DataContain.Name.QualityName, ChartMCQ, color[3]);

                lbMCA.Text = $"{result.A} %";
                lbMCP.Text = $"{result.P} %";
                lbMCQ.Text = $"{result.Q} %";
                TBLoading.Text = $"{result.Loadingtime}";
                double a = Math.Round(result.A1 + result.A2 + result.A3, 2, MidpointRounding.AwayFromZero);
                double p = Math.Round(result.P1 + result.P2 + result.P3 + result.P4, 2, MidpointRounding.AwayFromZero);
                double q = Math.Round(result.NG + result.RE, 2, MidpointRounding.AwayFromZero);

                TBAsum.Text = $"{a}";
                TBPsum.Text = $"{p}";
                TBQsum.Text = $"{q}";
                TBAPQ.Text = $"{a + p + q}";


            }


        }





        private void chartMCA_MouseClick(object sender, MouseEventArgs e)
        {
            //Chart pie = (Chart)sender;
            //int pointIndex = PieHitPointIndex(pie, e);
            //if (pointIndex >= 0)
            //{
            //    DataPoint dp = pie.Series[0].Points[pointIndex];
            //    selectedId = dp.AxisLabel;

            //    if (Memory.OEEMCSub.SubA1 == false)
            //    {
            //        OEESubIDForm frm = new OEESubIDForm( selectedMachineId, selectedId, OEEMC);
            //        frm.OeeSubIDFormClosed += new EventHandler(OeeSubIDFormClosed_Close);
            //     //   Memory.OEEMCSub.SubA1 = true;
            //        frm.TopLevel = true;
            //        frm.TopMost = true;
            //        frm.Focus();
            //        frm.Text = string.Format("OEE sub {0} : Date {1} ,Machine {2} : {3}", selectedId, selectedRegistDate, selectedMachineId, selectedMachineName);
            //        frm.Show();

            //    }
            //}

        }

        private void chartMCP_MouseClick(object sender, MouseEventArgs e)
        {
            //Chart pie = (Chart)sender;
            //int pointIndex = PieHitPointIndex(pie, e);
            //if (pointIndex >= 0)
            //{
            //    DataPoint dp = pie.Series[0].Points[pointIndex];
            //    selectedId = dp.AxisLabel;

            //    if (selectedId == "P1" && Memory.OEEMCSub.SubP1 == false)
            //    {
            //        OEESubIDForm frm = new OEESubIDForm( selectedMachineId, selectedId, OEEMC);
            //        frm.OeeSubIDFormClosed += new EventHandler(OeeSubIDFormClosed_Close);
            //        Memory.OEEMCSub.SubP1 = true;
            //        frm.TopLevel = true;
            //        frm.TopMost = true;
            //        frm.Focus();
            //        frm.Text = string.Format("OEE sub {0} : Date {1} ,Machine {2} : {3}", selectedId, selectedRegistDate, selectedMachineId, selectedMachineName);
            //        frm.Show();

            //    }
            //}

        }

        private void chartMCQ_MouseClick(object sender, MouseEventArgs e)
        {

        }



        private void ChartMC_DoubleClick(object sender, EventArgs e)
        {

        }

        private void ChartMC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var mcName = MCName.FirstOrDefault(m => m.Key == selectedMachineId);
            if (mcName.Value != null)
                selectedMachineName = mcName.Value;


            selectedMachineId = tbMCId.Text;

            OEESubIDForm frm = new OEESubIDForm(selectedMachineId, oeeMachine);
            frm.OeeSubIDFormClosed += new EventHandler(OeeSubIDFormClosed_Close);
            //  Memory.OEEMCSub.SubP1 = true;
            frm.TopLevel = true;
            frm.TopMost = true;
            frm.Focus();
            frm.Text = string.Format("OEE sub : Date {0} ,Machine {1} : {2}", selectedRegistDate, selectedMachineId, selectedMachineName);
            frm.Show();
        }
    }
}
