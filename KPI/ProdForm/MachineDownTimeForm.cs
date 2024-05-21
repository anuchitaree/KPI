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

namespace KPI.ProdForm
{
    public partial class MachineDownTimeForm : Form
    {
        readonly Dictionary<string, string> SixDic = new Dictionary<string, string>();
        readonly Dictionary<string, string> LossItemDic = new Dictionary<string, string>();
        readonly Dictionary<string, string> MCDic = new Dictionary<string, string>();


        public MachineDownTimeForm()
        {
            InitializeComponent();
        }

        private void MachineDownTimeForm_Load(object sender, EventArgs e)
        {
            InitialComponent.DateTimePicker(dateTimePicker1);
            InitialComponent.DateTimePicker(dateTimePicker3);
            SixGroupLoss();
        }



        private void SixGroupLoss()
        {
            Color[] color = {Color.FromArgb(255,127,127),Color.FromArgb(191, 255, 191),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            string betweendate = string.Format("{0}:{1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            string sectiondivplant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SSS("SixLossAnalysisExc", "@betweendate", betweendate, "@sectiondivplant", sectiondivplant, "@machineId", "0");
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable tbSixloss = ds.Tables[0];
                DataTable tbSixlossName = ds.Tables[1];

                Charts.ColumnMultiColor(tbSixloss, ChartSix, color);
                ChartLegent.Legend_DTMultiColor(tableLayoutPanel3, 1, "tableLayoutPanel3", "sixChartloss", color, tbSixlossName);

                SixDic.Clear();
                foreach (DataRow dr in tbSixlossName.Rows)
                {
                    SixDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                }
            }






            ChartLegent.Clear(tableLayoutPanel2, "tableLayoutPanel2");
            ChartLegent.Clear(tableLayoutPanel6, "tableLayoutPanel6");

            ChartLoss.Series.Clear();
            ChartMC.Series.Clear();
        }




        private void ChartSix_MouseClick(object sender, MouseEventArgs e)
        {

            Color[] color = {Color.FromArgb(255,127,127),Color.FromArgb(191, 255, 191),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);
            if (pointIndex >= 0)
            {
                DataPoint dp = pie.Series[0].Points[pointIndex];
                string betweendate = string.Format("{0}:{1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                string sectiondivplant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.SSQL_SSS("SixLossDetailAnalysisExc", "@betweendate", betweendate, "@sectiondivplant", sectiondivplant, "@Sixloss", dp.AxisLabel);
                if (sqlstatus)
                {
                    DataSet ds = sql.Dataset;
                    DataTable dtLossItem = ds.Tables[0];
                    DataTable dtLossItemName = ds.Tables[1];

                    Charts.ColumnMultiColor(dtLossItem, ChartLoss, color);
                    ChartLegent.Legend_DTMultiColor(tableLayoutPanel2, 1, "tableLayoutPanel2", "ItemsChartLoss", color, dtLossItemName);
                    LossItemDic.Clear();
                    foreach (DataRow dr in dtLossItemName.Rows)
                    {
                        LossItemDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                    }
                    lbSix.Text = dp.AxisLabel;
                    lbSixName.Text = SixDic.FirstOrDefault(x => x.Key == dp.AxisLabel).Value;
                }

            }


        }

        private void ChartLoss_MouseClick(object sender, MouseEventArgs e)
        {
            Color[] color = {Color.FromArgb(255,127,127),Color.FromArgb(191, 255, 191),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);
            if (pointIndex >= 0)
            {
                DataPoint dp = pie.Series[0].Points[pointIndex];
                string betweendate = string.Format("{0}:{1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                string sectiondivplant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.SSQL_SSS("SixLossItemsAnalysisExc", "@betweendate", betweendate, "@sectiondivplant", sectiondivplant, "@LossId", dp.AxisLabel);
                if (sqlstatus)
                {
                    DataSet ds = sql.Dataset;
                    DataTable dtLossMC = ds.Tables[0];
                    DataTable dtLossMCName = ds.Tables[1];

                    Charts.ColumnMultiColor(dtLossMC, ChartMC, color);
                    ChartLegent.Legend_DTMultiColor(tableLayoutPanel6, 1, "tableLayoutPanel6", "detailChartLoss", color, dtLossMCName);
                    MCDic.Clear();
                    foreach (DataRow dr in dtLossMCName.Rows)
                    {
                        MCDic.Add(dr.ItemArray[1].ToString(), dr.ItemArray[0].ToString());
                    }
                    lbdetail.Text = dp.AxisLabel;
                    lbdetailName.Text = LossItemDic.FirstOrDefault(x => x.Key == dp.AxisLabel).Value;
                }
            }
        }






        private void ChartMC_MouseClick(object sender, MouseEventArgs e)
        {
            Color[] color = {Color.FromArgb(255,127,127),Color.FromArgb(191, 255, 191),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            Chart pie = (Chart)sender;
            int pointIndex = PieHitPointIndex(pie, e);
            if (pointIndex >= 0)
            {
                DataPoint dp = pie.Series[0].Points[pointIndex];
                string betweendate = string.Format("{0}:{1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                string sectiondivplant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
                string dpAxisLabel = dp.AxisLabel;
                if (lbSix.Text == "1")
                {
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.SSQL_SSSS("SixLossDetailListExc", "@betweendate", betweendate, "@sectiondivplant", sectiondivplant, "@LossId", lbdetail.Text, "@MachineId", dpAxisLabel);
                    if (sqlstatus)
                    {
                        DataSet ds = sql.Dataset;
                        DataTable dtErrCode = ds.Tables[0];
                        DataTable dtErrCodeName = ds.Tables[1];

                        Charts.ColumnMultiColor(dtErrCode, ChartErr, color);
                        ChartLegent.Legend_DTMultiColor(tableLayoutPanel21, 1, "tableLayoutPanel21", "detailChartLoss", color, dtErrCodeName);
                        lberr.Text = "Machine";
                        lberr1.Text = dpAxisLabel;
                        lberr2.Text = MCDic.FirstOrDefault(x => x.Key == dpAxisLabel).Value;

                        DataTable dtErrCodeDetail = ds.Tables[2];
                        DataTable dtErrCodeDetailName = ds.Tables[3];
                        Charts.ColumnMultiColor(dtErrCodeDetail, ChartDetail, color);
                        ChartLegent.Legend_DTMultiColor(tpanelChartDetail, 1, "tpanelChartDetail", "detailChartLoss", color, dtErrCodeDetailName);
                    }
                }

            }
        }

        private void ButtonAnalysis_Click(object sender, EventArgs e)
        {
            SixGroupLoss();
        }



        private int PieHitPointIndex(Chart pie, MouseEventArgs e)
        {
            HitTestResult hitPiece = pie.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            HitTestResult hitLegend = pie.HitTest(e.X, e.Y, ChartElementType.LegendItem);
            int pointIndex = -1;
            if (hitPiece.Series != null) pointIndex = hitPiece.PointIndex;
            if (hitLegend.Series != null) pointIndex = hitLegend.PointIndex;
            return pointIndex;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            History();
        }


        private void History()
        {
            Color[] color = {Color.FromArgb(191, 255, 191),Color.FromArgb(255,127,127),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            SqlClass sql = new SqlClass();

            bool status = sql.LossOEE(User.SectionCode, dateTimePicker3.Value.ToString("yyyy-MM-dd"), dateTimePicker4.Value.ToString("yyyy-MM-dd"));
            var mcLoss = new List<MCLoss>();
            var mcName = new List<MCIdname>();
            if (!status)
                return;
            DataSet ds = sql.Dataset;
            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var loss = new MCLoss()
                    {
                        Run = dr.ItemArray[0].ToString(),
                        MachineId = dr.ItemArray[1].ToString(),
                        ErrorCode = dr.ItemArray[2].ToString(),
                        LossTime = Convert.ToDouble(dr.ItemArray[3])
                    };
                    mcLoss.Add(loss);
                }
            }
            if (dt1.Rows.Count > 0)
            {
                foreach(DataRow dr in dt1.Rows)
                {
                    var name = new MCIdname()
                    {
                        MachineId = dr.ItemArray[0].ToString(),
                        MachineName = dr.ItemArray[1].ToString()
                    };
                    mcName.Add(name);
                }
            }

            List<MCname> groupmc = mcLoss.GroupBy(m => m.MachineId).Select(m => new MCname
            {
                MachineId = m.Key
            }).ToList();



            List<MCLoss> more5 = mcLoss.Where(s => s.LossTime > 300).ToList();
            List<MCGroupLoss> groupmore5 = more5.GroupBy(m => m.MachineId).Select(m => new MCGroupLoss
            {
                MachineId = m.Key,
                LossTime = m.Sum(x => x.LossTime).ToString()
            }).ToList();

            List<MCLoss> less5 = mcLoss.Where(s => s.LossTime <= 300).ToList();
            List<MCGroupLoss> groupless5 = less5.GroupBy(m => m.MachineId).Select(m => new MCGroupLoss
            {
                MachineId = m.Key,
                LossTime = m.Sum(x => x.LossTime).ToString()
            }).ToList();


            var mcLossOEE = new List<MCLossOEE>();
            foreach (var m in groupmc)
            {
                MCGroupLoss greater = groupmore5.Where(x => x.MachineId == m.MachineId).FirstOrDefault();
                MCGroupLoss lessequal = groupless5.Where(x => x.MachineId == m.MachineId).FirstOrDefault();
                double g5 = 0, le5 = 0;
                if (greater != null)
                    g5 = greater.LossTime==null ? 0: Convert.ToDouble(greater.LossTime);
               
                if (lessequal != null)
                    le5 = lessequal.LossTime==null ? 0: Convert.ToDouble(lessequal.LossTime);

                var loss = new MCLossOEE()
                {
                    MachineId=m.MachineId,
                    G5 = g5,
                    LE5 = le5,
                };
                mcLossOEE.Add(loss);
            }

            List<MCIdname> mcname = (from l in mcLossOEE
                                     join m in mcName
                                     on l.MachineId equals m.MachineId
                                     select new MCIdname
                                     {
                                         MachineId = l.MachineId,
                                         MachineName = m.MachineName
                                     }).ToList();

            Console.WriteLine();

            Charts.LossByMC(mcLossOEE, chart2, color);

            ChartLegent.Legend_DTMultiColor(tableLayoutPanel25, 1, "tableLayoutPanel23", "sixChartloss", color, LegendPlate.LossMC());

            ChartLegent.Legend_ListSingleNOTColor(tableLayoutPanel23, 1, "tableLayoutPanel23", "sixChartloss", Color.White, mcname);





        }

        public class MCname
        {
            public string MachineId { get; set; }
        }

        public class MCGroupLoss : MCname
        {
            public string LossTime { get; set; }
        }

        public class MCLoss : MCname
        {
            public string Run { get; set; }
            public string ErrorCode { get; set; }
            public double LossTime { get; set; }
        }

        public class MCLossOEE : MCname
        {
            public double G5 { get; set; }
            public double LE5 { get; set; }
        }

        public class MCIdname : MCname
        {
            public string MachineName { get; set; }
        }
    }
}
