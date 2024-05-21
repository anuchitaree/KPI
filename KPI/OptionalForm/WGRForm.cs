using KPI.Class;
using KPI.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KPI.OptionalForm
{
    public partial class WGRForm : Form
    {
        DataSet ds = new DataSet();
        readonly string SectionDivPlant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
        public WGRForm()
        {
            InitializeComponent();
            InitialDataGridView(DgvWarning);
        }

        private void WGRForm_Load(object sender, EventArgs e)
        {
            InitialComponent.DateTimePicker(dateTimePicker1);
            InitialComponent.DateTimePicker(dateTimePicker3);

            hScrollBarCh1.Minimum = 0;
            hScrollBarCh1.Maximum = 1000;
            hScrollBarCh1.Value = 0;
            hScrollBarCh2.Minimum = 0;
            hScrollBarCh2.Maximum = 1000;
            hScrollBarCh2.Value = 0;


            DateTime da = DateTime.Now;
            int mon = da.Month;
            CmbYear.Text = DateTime.Now.ToString("yyyy");
            CmbMonth.SelectedIndex = mon - 1;
        }


        //================ OPERATION LOOP ====================///
        #region Helium LeakTest Machine
        private DataTable tbleak1 = new DataTable();
        private DataTable tbleak2 = new DataTable();

        private void BtnStart_Click(object sender, EventArgs e)
        {
            HeliumeLeakTest();
        }


        private void HScrollBarCh1_Scroll(object sender, ScrollEventArgs e)
        {
            Color[] color2 = { Color.Gray, Color.Red, Color.ForestGreen };
            ChartOneAxis(tbleak1, chart20, color2, hScrollBarCh1.Value);

        }
        private void HScrollBarCh2_Scroll(object sender, ScrollEventArgs e)
        {
            Color[] color2 = { Color.Gray, Color.Red, Color.ForestGreen };
            ChartOneAxis(tbleak2, chart21, color2, hScrollBarCh2.Value);
        }



        private void HeliumeLeakTest()
        {
            string betweendate = string.Format("{0}:{1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            //   string sectiondivplant = string.Format("{0}{1}{2}", CommonDefine.SectionCode, CommonDefine.Emp_Division, CommonDefine.Emp_Plant);
            SqlClassWGR sql = new SqlClassWGR();
            bool sqlstatus = sql.SSQL_SS("HeliumLeakTestExc", "@betweendate", betweendate, "@sectiondivplant", SectionDivPlant);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable tbCal1 = ds.Tables[0];
                DataTable tbCal2 = ds.Tables[1];
                tbleak1 = ds.Tables[2];
                tbleak2 = ds.Tables[3];
                DataTable tbNG1 = ds.Tables[4];
                DataTable tbNG2 = ds.Tables[5];

                Color[] color1 = { Color.Black, Color.Blue, Color.DarkGreen, Color.Violet };
                ChartTwoAxis(tbCal1, chart10, color1);
                ChartTwoAxis(tbCal2, chart11, color1);
                List<string> calibrateLegent = new List<string>() { "SGa", "BGa", "Setpoint", "SN" };
                //ChartLegentConstruction.Legend_1(tableLayoutPanel9, 1, "tableLayoutPanel9", "detailChartLoss", color1, calibrateLegent);
                ChartLegent.Legend_ListMultiColor(tableLayoutPanel9, 1, "tableLayoutPanel9", "detailChartLoss", color1, calibrateLegent);

                Color[] color2 = { Color.Gray, Color.Red, Color.ForestGreen };
                ChartOneAxis(tbleak1, chart20, color2, 0);
                ChartOneAxis(tbleak2, chart21, color2, 0);
                List<string> leakrateCh1 = new List<string>() { "LeakRate:OK", "LeakRate:NG", "Setpoint" };
                //ChartLegentConstruction.Legend_1(tableLayoutPanel10, 1, "tableLayoutPanel10", "detailChartLoss", color2, leakrateCh1);
                //ChartLegentConstruction.Legend_1(tableLayoutPanel11, 1, "tableLayoutPanel11", "detailChartLoss", color2, leakrateCh1);
                ChartLegent.Legend_ListMultiColor(tableLayoutPanel10, 1, "tableLayoutPanel10", "detailChartLoss", color2, leakrateCh1);
                ChartLegent.Legend_ListMultiColor(tableLayoutPanel11, 1, "tableLayoutPanel11", "detailChartLoss", color2, leakrateCh1);

                ChartNG(tbNG1, chart5, color1);
                ChartNG(tbNG2, chart6, color1);


            }
        }


        private void ChartTwoAxis(DataTable dt2, Chart chart1, Color[] color)
        {
            if (dt2.Rows.Count > 0)
            {
                //Color[] color = {Color.FromArgb(255,127,127),Color.FromArgb(191, 255, 191),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                //    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "SGa",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "BGa",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "NGsetpoint",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries3);

                var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "SN",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries4);

                chartSeries1.YAxisType = AxisType.Primary;
                chartSeries2.YAxisType = AxisType.Primary;
                chartSeries3.YAxisType = AxisType.Primary;
                chartSeries4.YAxisType = AxisType.Secondary;


                chart1.ChartAreas[0].AxisY.Title = "Calibrate";
                chart1.ChartAreas[0].AxisY2.Title = "SN";
                chart1.ChartAreas[0].AxisX.Title = "Date";
                chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 11, FontStyle.Bold);
                chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Violet;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Regular);

                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

                chartSeries1.Points.Clear();
                for (int count = 0; count < totalRow; count++)
                {
                    DateTime dt = Convert.ToDateTime(dt2.Rows[count].ItemArray[0]);
                    string station = dt.ToString("dd-MM-yy");
                    string SGa = dt2.Rows[count].ItemArray[1].ToString();
                    string BGa = dt2.Rows[count].ItemArray[2].ToString();
                    string setpoint = dt2.Rows[count].ItemArray[3].ToString();
                    string SN = dt2.Rows[count].ItemArray[4].ToString();


                    chartSeries1.Points.AddXY(station, SGa);
                    chartSeries1.Points[count].Color = color[0];
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries1.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = color[0];
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(station, BGa);
                    chartSeries2.Points[count].Color = color[1];
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = color[1];
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(station, setpoint);
                    chartSeries3.Points[count].Color = color[2];
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries3.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries3.Points[count].BorderWidth = 1;
                    chartSeries3.Points[count].BorderColor = color[2];
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries4.Points.AddXY(station, SN);
                    chartSeries4.Points[count].Color = color[3];
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries4.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries4.Points[count].BorderWidth = 1;
                    chartSeries4.Points[count].BorderColor = color[3];
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries2.Points.AddXY(station, max);
                    //chartSeries2.Points[count].Color = Color.DarkGreen;
                    //chartSeries2.Points[count].BorderWidth = 2;
                    //chartSeries2.Points[count].BorderColor = Color.Black;
                    //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    //chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                    //chartSeries2.Points[count].MarkerSize = 10;
                    //chartSeries2.Points[count].MarkerColor = Color.Red;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = true;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                chartSeries3.IsValueShownAsLabel = true;
                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = true;
            }
            else
            {
                chart1.Series.Clear();
            }
        }

        private void ChartOneAxis(DataTable dt2, Chart chart1, Color[] color, int startGap)
        {
            if (dt2.Rows.Count > 0)
            {

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "leakrate",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Setpoint",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries2);


                chartSeries1.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Primary;
                //chartSeries3.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Secondary;

                chart1.ChartAreas[0].AxisY.Title = "LeakRate";
                chart1.ChartAreas[0].AxisX.Title = "Times";
                //chart1.ChartAreas[0].AxisY2.Title = "%";
                //chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 11, FontStyle.Bold);
                //chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 6, FontStyle.Regular);

                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

                //chart1.ChartAreas[0].AxisY.Minimum = 0.001;
                chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;

                chartSeries1.Points.Clear();


                int pitch = 500;
                int lap = pitch / 3;

                int startRow = startGap * lap;
                int endRow = startRow + pitch;
                startRow = startRow < totalRow ? startRow : totalRow;
                endRow = endRow < totalRow ? endRow : totalRow;
                pitch = endRow - startRow;



                for (int count = 0; count < pitch; count++)
                {
                    //DateTime dt = Convert.ToDateTime(dt2.Rows[count].ItemArray[0]);
                    //string station = dt.ToString("dd-MM-yy");
                    string station = count.ToString();
                    string leakrate = dt2.Rows[count + startRow].ItemArray[1].ToString();
                    string setpoint = dt2.Rows[count + startRow].ItemArray[2].ToString();
                    string OKNG = dt2.Rows[count + startRow].ItemArray[3].ToString();
                    //string SN = dt2.Rows[count].ItemArray[4].ToString();

                    chartSeries1.Points.AddXY(station, leakrate);
                    if (OKNG == "OK")
                    {
                        chartSeries1.Points[count].Color = color[0];
                        chartSeries1.Points[count].BorderColor = color[0];
                    }
                    else
                    {
                        chartSeries1.Points[count].Color = color[1];
                        chartSeries1.Points[count].BorderColor = color[1];
                    }
                    //chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    //chartSeries1.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries1.Points[count].BorderWidth = 1;

                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(station, setpoint);
                    chartSeries2.Points[count].Color = color[2];
                    //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    //chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries2.Points[count].BorderWidth = 2;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;





                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6f);
                chartSeries1.IsValueShownAsLabel = false;
                //chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries2.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }

        private void ChartNG(DataTable dt2, Chart chart1, Color[] color)
        {
            if (dt2.Rows.Count > 0)
            {

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "leakrate",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Bar,
                };
                chart1.Series.Add(chartSeries1);
                //var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                //{
                //    Name = "Setpoint",
                //    Color = System.Drawing.Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.Line,
                //};
                //chart1.Series.Add(chartSeries2);


                chartSeries1.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Primary;
                //chartSeries3.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Secondary;

                chart1.ChartAreas[0].AxisY.Title = "NG Count (pcs)";
                chart1.ChartAreas[0].AxisX.Title = "NG Mode";
                //chart1.ChartAreas[0].AxisY2.Title = "%";
                //chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 11, FontStyle.Bold);
                //chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#";
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10, FontStyle.Regular);

                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

                //chart1.ChartAreas[0].AxisY.Minimum = 0.001;
                chart1.ChartAreas[0].AxisY.IsStartedFromZero = true;

                chartSeries1.Points.Clear();

                for (int count = 0; count < totalRow; count++)
                {

                    //string station = count.ToString();
                    string ngMode = dt2.Rows[count].ItemArray[0].ToString();
                    string ngCount = dt2.Rows[count].ItemArray[1].ToString();
                    //string OKNG = dt2.Rows[count].ItemArray[3].ToString();

                    chartSeries1.Points.AddXY(ngMode, ngCount);
                    chartSeries1.Points[count].Color = Color.FromArgb(255, 127, 127); //Color.BlueViolet;
                    chartSeries1.Points[count].BorderColor = color[0];
                    //chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    //chartSeries1.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries2.Points.AddXY(station, setpoint);
                    //chartSeries2.Points[count].Color = color[2];
                    ////chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    ////chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                    //chartSeries2.Points[count].BorderWidth = 2;
                    //chartSeries2.Points[count].BorderColor = Color.Black;
                    //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;





                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f);
                chartSeries1.IsValueShownAsLabel = true;
                //chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries2.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }



        #endregion

        #region Packing Slip

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    PackingSlipMachine();
        //}

        private void BtnPacking_Click(object sender, EventArgs e)
        {
            PackingSlipMachine();
        }



        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

            Color[] color1 = { Color.Green, Color.Red, Color.Blue, Color.DarkGreen };
            int scroll = hScrollBar1.Value;
            DataTable tbTL = ds.Tables[0];
            DataTable tbTR = ds.Tables[1];
            DataTable tbBL = ds.Tables[2];
            DataTable tbBR = ds.Tables[3];
            ChartOneAxisPacking(tbTL, chart1, color1, scroll);
            ChartOneAxisPacking(tbTR, chart2, color1, scroll);
            ChartOneAxisPacking(tbBL, chart3, color1, scroll);
            ChartOneAxisPacking(tbBR, chart4, color1, scroll);


        }

        private void PackingSlipMachine()
        {

            string betweendate = string.Format("{0}:{1}", dateTimePicker3.Value.ToString("yyyy-MM-dd"), dateTimePicker4.Value.ToString("yyyy-MM-dd"));

            //  string sectiondivplant = string.Format("{0}{1}{2}", CommonDefine.SectionCode, CommonDefine.Emp_Division, CommonDefine.Emp_Plant);
            SqlClassWGR sql = new SqlClassWGR();
            bool sqlstatus = sql.SSQL_SS("PackingSlipExc", "@betweendate", betweendate, "@sectiondivplant", SectionDivPlant);
            if (sqlstatus)
            {
                ds = sql.Dataset;
                DataTable tbTL = ds.Tables[0];
                DataTable tbTR = ds.Tables[1];
                DataTable tbBL = ds.Tables[2];
                DataTable tbBR = ds.Tables[3];
                DataTable tbNGmode = ds.Tables[4];
                DataTable tbNGmodeCount = ds.Tables[5];

                Color[] color1 = { Color.Green, Color.Red, Color.Blue, Color.DarkGreen };
                int scroll = hScrollBar1.Value;
                ChartOneAxisPacking(tbTL, chart1, color1, scroll);
                ChartOneAxisPacking(tbTR, chart2, color1, scroll);
                ChartOneAxisPacking(tbBL, chart3, color1, scroll);
                ChartOneAxisPacking(tbBR, chart4, color1, scroll);

                List<string> calibrateLegent = new List<string>() { "Diff:OK", "Diff:NG", "Tagget" };
                ChartLegent.Legend_ListMultiColor(tableLayoutPanel14, 1, "tableLayoutPanel14", "detailChartLoss", color1, calibrateLegent);
                ChartLegent.Legend_ListMultiColor(tableLayoutPanel21, 1, "tableLayoutPanel21", "detailChartLoss", color1, calibrateLegent);
                //ChartLegentConstruction.Legend_1(tableLayoutPanel14, 1, "tableLayoutPanel14", "detailChartLoss", color1, calibrateLegent);
                //ChartLegentConstruction.Legend_1(tableLayoutPanel21, 1, "tableLayoutPanel21", "detailChartLoss", color1, calibrateLegent);

                ChartNG(tbNGmode, chart7, color1);
                label29.Text = $"Total NG ={tbNGmodeCount.Rows[0].ItemArray[0]}";
            }

        }


        private void ChartOneAxisPacking(DataTable dt2, Chart chart1, Color[] color, int startGap)
        {
            if (dt2.Rows.Count > 0)
            {
                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "leakrate",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Setpoint",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries2);


                chartSeries1.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Primary;
                //chartSeries3.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Secondary;

                chart1.ChartAreas[0].AxisY.Title = "OffSetDiff.(mm)";
                chart1.ChartAreas[0].AxisX.Title = "Times";
                //chart1.ChartAreas[0].AxisY2.Title = "%";
                //chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 11, FontStyle.Bold);
                //chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 6, FontStyle.Regular);

                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

                chart1.ChartAreas[0].AxisY.Maximum = 0.5;
                chart1.ChartAreas[0].AxisY.Minimum = 0;

                //chart1.ChartAreas[0].AxisY.Minimum = 0.001;
                chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;

                chartSeries1.Points.Clear();

                bool status = int.TryParse(textBox1.Text, out int pitch);
                if (status == false)
                    return;

                int lap = pitch / 3;

                int startRow = startGap * lap;
                int endRow = startRow + pitch;
                startRow = startRow < totalRow ? startRow : totalRow;
                endRow = endRow < totalRow ? endRow : totalRow;
                pitch = endRow - startRow;

                label22.Text = startRow.ToString();
                label25.Text = $"{startRow} to {endRow}";
                label26.Text = totalRow.ToString();

                for (int count = 0; count < pitch; count++)
                {
                    string station = count.ToString();
                    string leakrate = dt2.Rows[count + startRow].ItemArray[0].ToString();
                    string OKNG = dt2.Rows[count + startRow].ItemArray[1].ToString();

                    chartSeries1.Points.AddXY(station, leakrate);
                    if (OKNG == "OK")
                    {
                        chartSeries1.Points[count].Color = color[0];
                        chartSeries1.Points[count].BorderColor = color[0];
                    }
                    else
                    {
                        chartSeries1.Points[count].Color = color[1];
                        chartSeries1.Points[count].BorderColor = color[1];
                    }
                    //chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    //chartSeries1.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries1.Points[count].BorderWidth = 1;

                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(station, "0.2");
                    chartSeries2.Points[count].Color = color[2];
                    //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    //chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries2.Points[count].BorderWidth = 2;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;



                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6f);
                chartSeries1.IsValueShownAsLabel = false;
                //chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries2.IsValueShownAsLabel = true;
                //chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                //chartSeries3.IsValueShownAsLabel = true;
                //chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries4.IsValueShownAsLabel = true;
            }
            else
            {
                chart1.Series.Clear();
            }
        }





        #endregion


        #region Record Sheet 
      

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            ExportExcelForm();

        }


        private void BtnRead_Click(object sender, EventArgs e)
        {
            string year = CmbYear.Text;
            string month = (CmbMonth.SelectedIndex + 1).ToString("00");
            string dat = string.Format("{0}-{1}%", year, month);

            SqlClassWGR sql = new SqlClassWGR();
            bool sqlstatus = sql.SSQL_SS("HL_RecordSheetExc", "@betweendate", dat, "@sectiondivplant", SectionDivPlant);
            if (sqlstatus)
            {
                DataSet ds1 = sql.Dataset;
                DataTable chamber1DN = ds1.Tables[0];
                DataTable chamber2DN = ds1.Tables[1];
                DgvCH1.DataSource = chamber1DN;
                DgvCH2.DataSource = chamber2DN;
                DataTable chamber1cnt = ds1.Tables[2];
                DataTable chamber2cnt = ds1.Tables[3];
                if (chamber1cnt.Rows.Count != 0 || chamber2cnt.Rows.Count != 0)
                {
                    MessageBox.Show("In 1 workShift has data more than 1 set \n You shall manage data before Save it", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
            }
        }


        private void ExportExcelForm()
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                RestoreDirectory = true,
                Title = "Browse Excel Files",
                DefaultExt = "xlsx",
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FilterIndex = 2
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                openFileDialog1.Multiselect = false;
                string year = CmbYear.Text;
                string month = (CmbMonth.SelectedIndex + 1).ToString("00");
                string dat = string.Format("{0}-{1}%", year, month);

                SqlClassWGR sql = new SqlClassWGR();
                bool sqlstatus = sql.SSQL_SS("HL_RecordSheetExc", "@betweendate", dat, "@sectiondivplant", SectionDivPlant);
                if (sqlstatus)
                {
                    DataSet ds1 = sql.Dataset;
                    DataTable chamber1DN = ds1.Tables[0];
                    DataTable chamber2DN = ds1.Tables[1];
                    DgvCH1.DataSource = chamber1DN;
                    DgvCH2.DataSource = chamber2DN;
                    DataTable chamber1cnt = ds1.Tables[2];
                    DataTable chamber2cnt = ds1.Tables[3];
                    if (chamber1cnt.Rows.Count != 0 || chamber2cnt.Rows.Count != 0)
                    {
                        MessageBox.Show("In 1 workShift has data more than 1 set \n You shall manage data before Save it", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (fileName != null)
                    {
                        bool status = InsertExcel("STD check sheet2", fileName, year, CmbMonth.Text, chamber1DN, chamber2DN);
                        if (status)
                        {
                            string msg = string.Format("Already insert to Excel file as \n {0} ", fileName);
                            MessageBox.Show(msg, "Insert data to Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string msg = string.Format("Please check the Sheet's name again and you shall closed file before copy data \n ");
                            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }


        }





        private bool InsertExcel(string sheetName, string fileName, string year, string month, DataTable Chamber1, DataTable Chamber2)
        {
            FileInfo excelFile = new FileInfo(fileName);
            ExcelPackage excel = new ExcelPackage(excelFile);
            ExcelWorksheet worksheet = excel.Workbook.Worksheets[sheetName];
            ExcelWorksheet anotherWorksheet = excel.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName);
            if (anotherWorksheet == null)
            {
                return false;
            }

            try
            {
                DataTable dt1 = Chamber1;
                int rowdt1 = dt1.Rows.Count;
                int startColumn = 5 - 1; ;
                worksheet.Cells[1, 11].Value = int.Parse(year);
                worksheet.Cells[1, 16].Value = month;

                for (int i = 0; i < 31; i++)
                {
                    worksheet.Cells[2, startColumn + i + 1].Value = i + 1;
                    worksheet.Cells[33, startColumn + i + 1].Value = i + 1;
                    for (int j = 3; j < 25; j++)
                    {
                        worksheet.Cells[j, startColumn + i + 1].Value = string.Empty;
                    }
                    for (int j = 34; j < 53; j++)
                    {
                        worksheet.Cells[j, startColumn + i + 1].Value = string.Empty;
                    }
                }


                for (int i = 0; i < rowdt1; i += 2)
                {
                    int date = Convert.ToInt32(dt1.Rows[i].ItemArray[0]);
                    int column = startColumn + date;
                    int day = i; // Day  
                    int startrow = 4;

                    worksheet.Cells[startrow - 1, column].Value = dt1.Rows[day].ItemArray[2];
                    worksheet.Cells[startrow + 0, column].Value = dt1.Rows[day].ItemArray[3];
                    worksheet.Cells[startrow + 1, column].Value = dt1.Rows[day].ItemArray[4];
                    worksheet.Cells[startrow + 2, column].Value = dt1.Rows[day].ItemArray[5];
                    worksheet.Cells[startrow + 3, column].Value = dt1.Rows[day].ItemArray[6];
                    worksheet.Cells[startrow + 4, column].Value = dt1.Rows[day].ItemArray[7];
                    worksheet.Cells[startrow + 5, column].Value = dt1.Rows[day].ItemArray[8];

                    day = i + 1; // Night
                    startrow = 35;
                    worksheet.Cells[startrow - 1, column].Value = dt1.Rows[day].ItemArray[2];
                    worksheet.Cells[startrow + 0, column].Value = dt1.Rows[day].ItemArray[3];
                    worksheet.Cells[startrow + 1, column].Value = dt1.Rows[day].ItemArray[4];
                    worksheet.Cells[startrow + 2, column].Value = dt1.Rows[day].ItemArray[5];
                    worksheet.Cells[startrow + 3, column].Value = dt1.Rows[day].ItemArray[6];
                    worksheet.Cells[startrow + 4, column].Value = dt1.Rows[day].ItemArray[7];
                    worksheet.Cells[startrow + 5, column].Value = dt1.Rows[day].ItemArray[8];
                }


                dt1 = Chamber2;
                rowdt1 = dt1.Rows.Count;
                for (int i = 0; i < rowdt1; i += 2)
                {
                    int date = Convert.ToInt32(dt1.Rows[i].ItemArray[0]);
                    int column = startColumn + date;
                    int day = i; // Day  
                    int startrow = 19;

                    worksheet.Cells[startrow + 0, column].Value = dt1.Rows[day].ItemArray[3];
                    worksheet.Cells[startrow + 1, column].Value = dt1.Rows[day].ItemArray[4];
                    worksheet.Cells[startrow + 2, column].Value = dt1.Rows[day].ItemArray[5];
                    worksheet.Cells[startrow + 3, column].Value = dt1.Rows[day].ItemArray[6];
                    worksheet.Cells[startrow + 4, column].Value = dt1.Rows[day].ItemArray[7];
                    worksheet.Cells[startrow + 5, column].Value = dt1.Rows[day].ItemArray[8];

                    day = i + 1; // Night
                    startrow = 47;
                    worksheet.Cells[startrow + 0, column].Value = dt1.Rows[day].ItemArray[3];
                    worksheet.Cells[startrow + 1, column].Value = dt1.Rows[day].ItemArray[4];
                    worksheet.Cells[startrow + 2, column].Value = dt1.Rows[day].ItemArray[5];
                    worksheet.Cells[startrow + 3, column].Value = dt1.Rows[day].ItemArray[6];
                    worksheet.Cells[startrow + 4, column].Value = dt1.Rows[day].ItemArray[7];
                    worksheet.Cells[startrow + 5, column].Value = dt1.Rows[day].ItemArray[8];
                }
                excel.SaveAs(excelFile);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }

        private void DgvCH1_DoubleClick(object sender, EventArgs e)
        {
            string run;
            if (DgvCH1.Rows.Count > 0)
            {
                run = CellSelection(DgvCH1, Color.FromArgb(255, 197, 197));
                string msg = $"Do you want to delete at RED MARK ";
                DialogResult r = MessageBox.Show(msg, "Chamber 1 : Confrim", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r && run != "0")
                {
                    SqlClassWGR sql = new SqlClassWGR();
                    bool sqlstatus = sql.DeleteRecord_RD_HL_CalibrationTableSQL(run);
                    if (sqlstatus)
                    {
                        int row = DgvCH1.CurrentRow.Index;
                        DgvCH1.Rows.RemoveAt(row);
                        MessageBox.Show("Delete completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            
        }
        private void DgvCH2_DoubleClick(object sender, EventArgs e)
        {
            string run;
            if (DgvCH2.Rows.Count > 0)
            {
                run = CellSelection(DgvCH2, Color.FromArgb(255, 197, 197));
                string msg = $"Do you want to delete at RED MARK ";
                DialogResult r = MessageBox.Show(msg, "Chamber 2 : Confrim", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r && run !="0")
                {
                    SqlClassWGR sql = new SqlClassWGR();
                    bool sqlstatus = sql.DeleteRecord_RD_HL_CalibrationTableSQL(run);
                    if (sqlstatus)
                    {
                        int row = DgvCH2.CurrentRow.Index;
                        DgvCH2.Rows.RemoveAt(row);
                        MessageBox.Show("Delete completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private string CellSelection(DataGridView dgv, Color color)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle
            {
                BackColor = color,
                ForeColor = Color.Black
            };
            DataGridViewCellStyle styleNormal = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 255, 255),
                ForeColor = Color.Black
            };
            int row = dgv.Rows.Count;
            int col = dgv.Columns.Count;
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    dgv.Rows[i].Cells[j].Style = styleNormal;
                }
            }
            int rowc = dgv.CurrentRow.Index;
            for (int j = 0; j < col; j++)
            {
                dgv.Rows[rowc].Cells[j].Style = style;
            }

            if (dgv.Rows[rowc].Cells[9].Value.ToString() == "")
            {
                return "0";
            }
            else
            {
                //string a = dgv.Rows[rowc].Cells[9].Value.ToString();
                return dgv.Rows[rowc].Cells[9].Value.ToString();
            }

            
        }

      
        private void DgvCH1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvCH1.Rows.Count > 0) CellSelection(DgvCH1, Color.FromArgb(127, 191, 212));
        }

        private void DgvCH2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvCH2.Rows.Count > 0) CellSelection(DgvCH2, Color.FromArgb(127, 191, 212));
        }

        #endregion

        #region HL_Warning record

        private void RD_HLWaring()
        {
            SqlClassWGR sql = new SqlClassWGR();
            bool status = sql.WarningRecord_RD_HL_NormalTableSQL(User.SectionCode);
            if (status)
            {
                DataTable dt = sql.Datatable;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DateTime date = Convert.ToDateTime(dr.ItemArray[1]);
                        DgvWarning.Rows.Add(dr.ItemArray[0],date.ToString("dd/MM/yy HH:mm:ss") , dr.ItemArray[2], dr.ItemArray[3],
                            dr.ItemArray[4], dr.ItemArray[5], dr.ItemArray[6], dr.ItemArray[7], dr.ItemArray[8],
                            dr.ItemArray[9], dr.ItemArray[10] );
                    }
                }

            }
        }

        private void InitialDataGridView(DataGridView Dgv)
        {
            string[] header = new string[] { "run","RecordDateTime","DayNight","Machine","Serial PartNumber",
                "Chamber","LeakRate","NGmode","Judge","Warning","Ack"};
            int[] width = new int[] { 80,150,40,100,150,
                80,80,40,60,60,40};
            DataGridViewSetup.Norm1(DgvWarning, header, width);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RD_HLWaring();
        }

        private void DgvWarning_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewSetup.MarkRowColor(DgvWarning);
        }


        #endregion

       
    }
}
