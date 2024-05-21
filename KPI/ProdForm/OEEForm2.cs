using KPI.Class;
using KPI.DataContain;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPI.ProdForm
{
    public partial class OEEForm2 : Form
    {
        public OEEForm2()
        {
            InitializeComponent();
        }

        private readonly Color[] colorAPQ =
           {
            Color.FromArgb(124, 131, 253), Color.FromArgb(13, 236, 221), Color.FromArgb(247, 253, 5),

            Color.FromArgb(255, 73, 73), Color.FromArgb(41, 121, 255), Color.FromArgb(1, 88, 146), Color.FromArgb(214, 42, 208),

             Color.FromArgb(5, 1, 154) , Color.FromArgb(242, 18, 113), Color.FromArgb(170, 255, 170),

            Color.FromArgb(198, 170, 198)};


        private void monthCalendar1_MouseDown(object sender, MouseEventArgs e)
        {
            List<OEEOeeMCMonitor> oeeMachine = new List<OEEOeeMCMonitor>();

            DateTime selectdate = monthCalendar1.SelectionStart;

            using (var db = new ProductionEntities11())
            {

                oeeMachine = (from m in db.oeeMachines
                                .Where(r => r.regist_date == selectdate)
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

                Charts.MCOEEdetailSummary2(oeeMachine, ChartAP, colorAPQ);

            

            }
        }

       

        private void OEEForm2_Load(object sender, EventArgs e)
        {
            ChartLegent.Legend_DTMultiColor(tableLayoutPanel8, 1, "tableLayoutPanel34", "sixChartloss", colorAPQ, LegendPlate.GetAPQName2());

        }
    }
}
