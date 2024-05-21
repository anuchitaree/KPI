using KPI.Class;
using KPI.DataContain;
using KPI.Models;
using KPI.Parameter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static KPI.Class.GraphOEE;
using static KPI.Class.OEE;

namespace KPI.ProdForm
{
    public partial class OEESubIDForm : Form
    {
        public event EventHandler OeeSubIDFormClosed;
       
        private readonly string machineId = string.Empty;
      
      

        private readonly Color[] colorAPQ =
            {
            Color.FromArgb(124, 131, 253), Color.FromArgb(13, 236, 221), Color.FromArgb(247, 253, 5),

            Color.FromArgb(255, 73, 73), Color.FromArgb(41, 121, 255), Color.FromArgb(1, 88, 146), Color.FromArgb(214, 42, 208),

            Color.FromArgb(170, 255, 170), Color.FromArgb(242, 18, 113), Color.FromArgb(5, 1, 154),

            Color.FromArgb(198, 170, 198)};


        private readonly Color[] colorAP_ = { Color.FromArgb(198, 122, 206), Color.FromArgb(216, 248, 183), Color.FromArgb(255, 154, 140), Color.FromArgb(206, 31, 106), Color.FromArgb(14, 73, 181),  Color.Blue };


        private readonly List<OEEOeeMCMonitor> oEEMC;
        public OEESubIDForm(string mcId,  List<OEEOeeMCMonitor> OEEMC)
        {
            InitializeComponent();
            machineId = mcId;
            oEEMC = OEEMC;
         
        }

        public OEESubIDForm()
        {

        }

        private void OEESubIDForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            List<OEEOeeMCMonitor> db = oEEMC.Where(m => m.machineID == machineId).ToList();

        
            Charts.MCOEEdetailSummary1(db, ChartA, colorAPQ);
            ChartLegent.Legend_DTMultiColor1(tableLayoutPanel21, 1, "tableLayoutPanel34", "sixChartloss", colorAPQ, LegendPlate.GetAPQLName());


            Charts.MCOEEdetailSummary(db, ChartAP, colorAPQ);

            ChartLegent.Legend_DTMultiColor(tableLayoutPanel8, 1, "tableLayoutPanel34", "sixChartloss", colorAPQ, LegendPlate.GetAPQName());


        }

        private void OEESubIDForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OeeSubIDFormClosed?.Invoke(this, EventArgs.Empty);
        }


    }
       

}