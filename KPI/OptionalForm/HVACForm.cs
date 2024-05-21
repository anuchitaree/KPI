using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KPI.Parameter;
using KPI.Class;
using System.Windows.Forms.DataVisualization.Charting;
using KPI.Models;

namespace KPI.OptionalForm
{
    public partial class HVACForm : Form
    {
        readonly string SectionDivPlant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);

        public HVACForm()
        {
            InitializeComponent();
        }

        private void HVACForm_Load(object sender, EventArgs e)
        {
            CmbDayNight.SelectedIndex = 0;
            LoadInitial();
        }


        private void CmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCTStation();
        }

        private void CmbPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCTStation();
        }









        //------------------------------------//
        private void BtnT1_update1_Click(object sender, EventArgs e)
        {
            LoadCTStation();
        }

        private void DateTimePickerTabOperation_ValueChanged(object sender, EventArgs e)
        {
            LoadInitial();
        }



        // ----- OPERATION LOOP ------ // 


        private void LoadInitial()
        {
            CmbPN.SelectedIndexChanged -= CmbPN_SelectedIndexChanged;
            CmbPeriod.SelectedIndexChanged -= CmbPeriod_SelectedIndexChanged;
            string datestr = DateTimePickerTabOperation.Value.ToString("yyyy-MM-dd");
            string DN = CmbDayNight.SelectedIndex == 0 ? "D" : "N";
         //   string SectionCodeDivPlant = string.Format("{0}{1}{2}", CommonDefine.SectionCode, CommonDefine.Emp_Division, CommonDefine.Emp_Plant);
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SSS("CT_monitorInitialExc", "@datetime", datestr, "@DN", DN, "SectionCodeDivPlant", SectionDivPlant);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];
                CmbPN.DataSource = dt1;
                CmbPN.DisplayMember = "partNumber";

                List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        data.Add(new KeyValuePair<string, string>(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString()));
                    }
                    CmbPeriod.DataSource = null;
                    CmbPeriod.Items.Clear();
                    CmbPeriod.DataSource = new BindingSource(data, null);
                    CmbPeriod.DisplayMember = "Value";
                    CmbPeriod.ValueMember = "Key";
                }


            }
            CmbPN.SelectedIndexChanged += CmbPN_SelectedIndexChanged;
            CmbPeriod.SelectedIndexChanged += CmbPeriod_SelectedIndexChanged;
        }

        private void LoadCTStation()
        {
            string datestr = DateTimePickerTabOperation.Value.ToString("yyyy-MM-dd");
            string partNumber = CmbPN.Text;
            KeyValuePair<string, string> selectedPair = ((KeyValuePair<string, string>)CmbPeriod.SelectedItem);
            string hourNo = selectedPair.Key.PadLeft(2, '0');
            string LF = string.Empty;
            string HF = string.Empty;
            bool boolmin = int.TryParse(tbCutMin.Text, out int min);
            bool boolmax = int.TryParse(tbCutMax.Text, out int max);
            if (boolmin && boolmax)
            {
                string LFstr = min > 0 ? min.ToString() : "0000";
                string HFstr = max < 9999 ? max.ToString() : "9999";
                LF = LFstr.PadLeft(4, '0');
                HF = HFstr.PadLeft(4, '0');
            }
            string HourMinMax = string.Format("{0}{1}{2}", hourNo, LF, HF);
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SSSS("CT_monitorExc", "@RegistDateTime", datestr, "@partNumber", partNumber,
                 "@SectionCodeDivPlant", SectionDivPlant, "@HourMinMax", HourMinMax);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    ChartStationNumberAndCT(dt1, ChartCT);
                }

            }
        }


        #region TAB PAGE

        #region TAB PAGE_1 : Cycle Time

        private void ChartStationNumberAndCT(DataTable dt2, Chart chart1)
        {
            if (dt2.Rows.Count > 0)
            {
                //Color[] color = {Color.FromArgb(191, 255, 191),Color.FromArgb(255,127,127),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                //    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Min",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Max",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Avg",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries3);

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                chartSeries1.Points.Clear();
                for (int count = 0; count < totalRow; count++)
                {
                    string station = dt2.Rows[count].ItemArray[0].ToString();
                    string min = dt2.Rows[count].ItemArray[1].ToString();
                    string max = dt2.Rows[count].ItemArray[2].ToString();
                    string avg = dt2.Rows[count].ItemArray[3].ToString();

                    chartSeries1.Points.AddXY(station, min);
                    chartSeries1.Points[count].Color = Color.FromArgb(18, 116, 239);
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(station, max);
                    chartSeries2.Points[count].Color = Color.White;
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(station, avg);
                    chartSeries3.Points[count].Color = Color.Red;//Color.FromArgb(191, 255, 191);
                    chartSeries3.Points[count].BorderWidth = 1;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                }
                //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f);
                chartSeries3.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }

        //private void Chart_MachineDownTime(DataTable dt2, Chart chart1)
        //{
        //    if (dt2.Rows.Count > 0)
        //    {
        //        Color[] color = { Color.FromArgb(191, 255, 191),Color.FromArgb(255,127,127), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
        //            Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

        //        int totalRow = dt2.Rows.Count;
        //        chart1.Series.Clear();
        //        var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "DownTime",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            ChartType = SeriesChartType.Column,
        //        };
        //        chart1.Series.Add(chartSeries1);


        //        chart1.ChartAreas[0].AxisX.Interval = 1;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        chart1.ChartAreas[0].BorderWidth = 1;
        //        chart1.ChartAreas[0].BorderColor = Color.White;
        //        chart1.ChartAreas[0].BackColor = Color.White;
        //        chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
        //        chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

        //        chartSeries1.Points.Clear();
        //        for (int count = 0; count < totalRow; count++)
        //        {
        //            string mcId = dt2.Rows[count].ItemArray[0].ToString();
        //            string avai = dt2.Rows[count].ItemArray[1].ToString();

        //            chartSeries1.Points.AddXY(mcId, avai);
        //            chartSeries1.Points[count].Color = Color.FromArgb(191, 255, 191);
        //            chartSeries1.Points[count].BorderWidth = 1;
        //            chartSeries1.Points[count].BorderColor = Color.Black;
        //            chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;


        //        }
        //        //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
        //        chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries1.IsValueShownAsLabel = true;

        //    }
        //    else
        //    {
        //        chart1.Series.Clear();
        //    }
        //}

        //private void Chart_MachineTime(DataTable dt2, Chart chart1)
        //{
        //    if (dt2.Rows.Count > 0)
        //    {
        //        Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
        //            Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

        //        int totalRow = dt2.Rows.Count;
        //        chart1.Series.Clear();
        //        var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "MinMax",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            ChartType = SeriesChartType.RangeColumn,
        //        };
        //        chart1.Series.Add(chartSeries1);
        //        var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "Average",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            ChartType = SeriesChartType.Point,
        //        };
        //        chart1.Series.Add(chartSeries2);

        //        chart1.ChartAreas[0].AxisX.Interval = 1;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        chart1.ChartAreas[0].BorderWidth = 1;
        //        chart1.ChartAreas[0].BorderColor = Color.White;
        //        chart1.ChartAreas[0].BackColor = Color.White;
        //        chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
        //        chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

        //        chartSeries1.Points.Clear();
        //        for (int count = 0; count < totalRow; count++)
        //        {
        //            string mcId = dt2.Rows[count].ItemArray[0].ToString();
        //            string value1 = dt2.Rows[count].ItemArray[1].ToString();
        //            string value2 = dt2.Rows[count].ItemArray[2].ToString();
        //            string value3 = dt2.Rows[count].ItemArray[3].ToString();

        //            chartSeries1.Points.AddXY(mcId, value1, value2);
        //            chartSeries1.Points[count].Color = Color.FromArgb(255, 127, 127);
        //            chartSeries1.Points[count].BorderWidth = 1;
        //            chartSeries1.Points[count].BorderColor = Color.Black;
        //            chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //            chartSeries2.Points.AddXY(mcId, value3);
        //            chartSeries2.Points[count].Color = Color.Blue;
        //            chartSeries2.Points[count].BorderWidth = 1;
        //            chartSeries2.Points[count].BorderColor = Color.Black;
        //            chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;


        //        }
        //        //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
        //        chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries1.IsValueShownAsLabel = false;
        //        chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries2.IsValueShownAsLabel = true;

        //    }
        //    else
        //    {
        //        chart1.Series.Clear();
        //    }
        //}

        //private void Chart_MachineTimeSD(DataTable dt2, Chart chart1)
        //{
        //    if (dt2.Rows.Count > 0)
        //    {
        //        Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
        //            Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

        //        int totalRow = dt2.Rows.Count;
        //        chart1.Series.Clear();
        //        var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "MinMax",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            ChartType = SeriesChartType.Point,
        //        };
        //        chart1.Series.Add(chartSeries1);
        //        chart1.ChartAreas[0].AxisX.Interval = 1;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        chart1.ChartAreas[0].BorderWidth = 1;
        //        chart1.ChartAreas[0].BorderColor = Color.White;
        //        chart1.ChartAreas[0].BackColor = Color.White;
        //        chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
        //        chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

        //        chartSeries1.Points.Clear();
        //        for (int count = 0; count < totalRow; count++)
        //        {
        //            string mcId = dt2.Rows[count].ItemArray[0].ToString();
        //            string value1 = dt2.Rows[count].ItemArray[4].ToString();

        //            chartSeries1.Points.AddXY(mcId, value1);
        //            chartSeries1.Points[count].Color = Color.FromArgb(255, 127, 127);
        //            chartSeries1.Points[count].BorderWidth = 1;
        //            chartSeries1.Points[count].BorderColor = Color.Black;
        //            chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;



        //        }
        //        //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
        //        chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries1.IsValueShownAsLabel = true;

        //    }
        //    else
        //    {
        //        chart1.Series.Clear();
        //    }
        //}




        //private void Chart_MachineDownTimeByDay(string dpAxisLabel, Chart chart1)
        //{
        //    // registDate ,MCNumber, LossMintue
        //    //string _sqlWhere = string.Format("MCNumber = '{0}'", dpAxisLabel);
        //    //string _sqlOrder = "registDate ASC";
        //    //DataTable _newDataTable = dtDT.Select(_sqlWhere, _sqlOrder).CopyToDataTable();

        //    //string[] selectedColumns = new[] { "registDate", "LossMinute" };

        //    //DataTable dt2 = new DataView(_newDataTable).ToTable(false, selectedColumns);

        //    //if (dt2.Rows.Count > 0)
        //    //{

        //    //    chart1.Series.Clear();
        //    //    int totalRow = dt2.Rows.Count;
        //    //    var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
        //    //    {
        //    //        Name = "Loss",
        //    //        Color = System.Drawing.Color.AntiqueWhite,
        //    //        IsVisibleInLegend = false,
        //    //        IsXValueIndexed = true,
        //    //        ChartType = SeriesChartType.Column,
        //    //    };
        //    //    chart1.Series.Add(chartSeries1);

        //    //    chartSeries1.Color = Color.Transparent;
        //    //    chart1.ChartAreas[0].AxisX.Interval = 1;
        //    //    chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //    //    chart1.ChartAreas[0].BorderWidth = 1;
        //    //    chart1.ChartAreas[0].BorderColor = Color.White;
        //    //    chart1.ChartAreas[0].BackColor = Color.White;
        //    //    chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //    //    chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
        //    //    chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
        //    //    chart1.ChartAreas[0].AxisX.Title = "DATE";
        //    //    chart1.ChartAreas[0].AxisY.Title = "Down Time (minute)";

        //    //    chartSeries1.Points.Clear();
        //    //    for (int count = 0; count < totalRow; count++)
        //    //    {
        //    //        DateTime datetime = Convert.ToDateTime(dt2.Rows[count].ItemArray[0]);
        //    //        string ExecGroupID = datetime.ToString("dd-MM-yy");
        //    //        string date = datetime.ToString("dd-MM-yyyy");
        //    //        string ExecNumber = dt2.Rows[count].ItemArray[1].ToString();
        //    //        chartSeries1.Points.AddXY(ExecGroupID, ExecNumber);
        //    //        if (productedDate == date)
        //    //        {
        //    //            chartSeries1.Points[count].Color = Color.Red; //color[count];
        //    //        }
        //    //        else
        //    //        {
        //    //            chartSeries1.Points[count].Color = Color.FromArgb(216, 242, 255); //color[count];
        //    //        }
        //    //        chartSeries1.Points[count].BorderWidth = 1;
        //    //        chartSeries1.Points[count].BorderColor = Color.Black;
        //    //        chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //    //    }
        //    //    chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //    //    chartSeries1.IsValueShownAsLabel = true;
        //    //}
        //}


        //private void Chart_MachineTimeBySD(string dpAxisLabel, Chart chart1)
        //{
        //    // mcNumber,registDate ,SD
        //    //string _sqlWhere = string.Format("mcNumber = '{0}'", dpAxisLabel);
        //    //string _sqlOrder = "registDate ASC";
        //    //DataTable _newDataTable = dtSD.Select(_sqlWhere, _sqlOrder).CopyToDataTable();

        //    //string[] selectedColumns = new[] { "registDate", "SD" };

        //    //DataTable dt2 = new DataView(_newDataTable).ToTable(false, selectedColumns);

        //    //if (dt2.Rows.Count > 0)
        //    //{

        //    //    chart1.Series.Clear();
        //    //    int totalRow = dt2.Rows.Count;
        //    //    var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
        //    //    {
        //    //        Name = "Loss",
        //    //        Color = System.Drawing.Color.AntiqueWhite,
        //    //        IsVisibleInLegend = false,
        //    //        IsXValueIndexed = true,
        //    //        ChartType = SeriesChartType.Point,
        //    //    };
        //    //    chart1.Series.Add(chartSeries1);

        //    //    chartSeries1.Color = Color.Transparent;
        //    //    chart1.ChartAreas[0].AxisX.Interval = 1;
        //    //    chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //    //    chart1.ChartAreas[0].BorderWidth = 1;
        //    //    chart1.ChartAreas[0].BorderColor = Color.White;
        //    //    chart1.ChartAreas[0].BackColor = Color.White;
        //    //    chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //    //    chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
        //    //    chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

        //    //    chartSeries1.Points.Clear();
        //    //    for (int count = 0; count < totalRow; count++)
        //    //    {
        //    //        DateTime datetime = Convert.ToDateTime(dt2.Rows[count].ItemArray[0]);
        //    //        string ExecGroupID = datetime.ToString("dd-MM-yy");
        //    //        string date = datetime.ToString("dd-MM-yyyy");
        //    //        string ExecNumber = dt2.Rows[count].ItemArray[1].ToString();
        //    //        chartSeries1.Points.AddXY(ExecGroupID, ExecNumber);
        //    //        if (productedDate == date)
        //    //        {
        //    //            chartSeries1.Points[count].Color = Color.Red; //color[count];
        //    //        }
        //    //        else
        //    //        {
        //    //            chartSeries1.Points[count].Color = Color.FromArgb(216, 242, 255); //color[count];
        //    //        }
        //    //        chartSeries1.Points[count].BorderWidth = 1;
        //    //        chartSeries1.Points[count].BorderColor = Color.Black;
        //    //        chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //    //    }
        //    //    chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //    //    chartSeries1.IsValueShownAsLabel = true;
        //    //}
        //}


        #endregion

        #region TAB PAGE_2 : NagaraSwitchTimeHistogram DISPLAY


        //private void Chart_NagaraSwitchTimeHistogram(Dictionary<string, string> dt, Chart chart1)
        //{
        //    if (dt.Count > 0)
        //    {
        //        Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta };

        //        int totalRow = dt.Count;
        //        chart1.Series.Clear();
        //        var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "MinMax",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            ChartType = SeriesChartType.Column,
        //        };
        //        chart1.Series.Add(chartSeries1);


        //        chart1.ChartAreas[0].AxisX.Interval = 1;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        chart1.ChartAreas[0].BorderWidth = 1;
        //        chart1.ChartAreas[0].BorderColor = Color.White;
        //        chart1.ChartAreas[0].BackColor = Color.White;
        //        chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
        //        chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

        //        chartSeries1.Points.Clear();
        //        for (int count = 0; count < totalRow; count++)
        //        {
        //            string axis = dt.ElementAt(count).Key;   //dt.Rows[count].ItemArray[0].ToString();
        //            string value1 = dt.ElementAt(count).Value;  //dt2.Rows[count].ItemArray[1].ToString();

        //            chartSeries1.Points.AddXY(axis, value1);
        //            chartSeries1.Points[count].Color = Color.Blue;//Color.FromArgb(255, 127, 127);
        //            chartSeries1.Points[count].BorderWidth = 1;
        //            chartSeries1.Points[count].BorderColor = Color.Blue;
        //            chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;



        //        }
        //        //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
        //        chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries1.IsValueShownAsLabel = false;

        //    }
        //    else
        //    {
        //        chart1.Series.Clear();
        //    }
        //}

        //private void Chart_NagaraSwitchTimeByStation(DataTable dt2, Chart chart1, string station, int pos)
        //{

        //    if (dt2.Rows.Count > 0)
        //    {
        //        Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta };

        //        int totalRow = dt2.Rows.Count;
        //        chart1.Series.Clear();
        //        var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "MinMax",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            //IsXValueIndexed = false,
        //            ChartType = SeriesChartType.Column,
        //        };
        //        chart1.Series.Add(chartSeries1);
        //        var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "Average",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            //IsXValueIndexed = true,
        //            IsXValueIndexed = false,
        //            ChartType = SeriesChartType.Line,
        //        };
        //        chart1.Series.Add(chartSeries2);


        //        chart1.ChartAreas[0].AxisX.Interval = 1;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        chart1.ChartAreas[0].BorderWidth = 1;
        //        chart1.ChartAreas[0].BorderColor = Color.White;
        //        chart1.ChartAreas[0].BackColor = Color.White;
        //        chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
        //        chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

        //        chartSeries1.Points.Clear();



        //        for (int count = 0; count < totalRow; count++)
        //        {

        //            string value1 = dt2.Rows[count].ItemArray[0].ToString();


        //            chartSeries1.Points.AddXY(count, value1);
        //            if (count == pos || count == pos + 1)
        //            {
        //                chartSeries1.Points[count].Color = Color.FromArgb(255, 0, 118); //Color.FromArgb(255, 127, 127);
        //                chartSeries1.Points[count].BorderColor = Color.FromArgb(255, 0, 118); // Color.Black;
        //            }
        //            else
        //            {
        //                chartSeries1.Points[count].Color = Color.Blue;//Color.FromArgb(255, 127, 127);
        //                chartSeries1.Points[count].BorderColor = Color.Blue; // Color.Black;
        //            }
        //            chartSeries1.Points[count].BorderWidth = 1;

        //            chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //            chartSeries2.Points.AddXY(count, 15.8);
        //            chartSeries2.Points[count].Color = Color.Red;
        //            chartSeries2.Points[count].BorderWidth = 2;
        //            chartSeries2.Points[count].BorderColor = Color.Red;
        //            chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;



        //        }
        //        //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
        //        chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries1.IsValueShownAsLabel = false;


        //    }
        //    else
        //    {
        //        chart1.Series.Clear();
        //    }
        //}


        //private int ChartChangeColor(DataTable dt2)
        //{
        //    int result = -1;
        //    //productedTime = new DateTime(2021, 1, 8, 9, 20, 0);
        //    //int row = dt2.Rows.Count;
        //    //for (int i = 0; i < row; i++)
        //    //{
        //    //    DateTime date1 = Convert.ToDateTime(dt2.Rows[i].ItemArray[1]);
        //    //    if (i < row - 1)
        //    //    {
        //    //        DateTime date2 = Convert.ToDateTime(dt2.Rows[i + 1].ItemArray[1]);
        //    //        if (productedTime >= date1 && productedTime <= date2)
        //    //        {
        //    //            result = i;
        //    //            break;
        //    //        }
        //    //    }
        //    //}
        //    return result;

        //}


        //private STDEV CalculateStandardDeviation(List<double> values)
        //{
        //    STDEV result = new STDEV();
        //    double avg = values.Average();
        //    double sum = values.Sum(d => Math.Pow(d - avg, 2));
        //    double stdev = Math.Sqrt((sum) / values.Count - 1);
        //    result.Max = Math.Round(values.Max(), 1, MidpointRounding.AwayFromZero);
        //    result.Min = Math.Round(values.Min(), 1, MidpointRounding.AwayFromZero);
        //    result.Stdev = Math.Round(stdev, 2, MidpointRounding.AwayFromZero);
        //    return result;
        //}


        #endregion

        #region TAB PAGE 3 : ENVIROMENT  
        //private void chart(DataTable dt2, Chart chartOee)
        //{
        //    if (dt2.Rows.Count > 0)
        //    {
        //        Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
        //            Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

        //        int totalRow = dt2.Rows.Count;
        //        chartOee.Series.Clear();
        //        var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "Availability",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            ChartType = SeriesChartType.StackedColumn,
        //        };
        //        chartOee.Series.Add(chartSeries1);
        //        var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "Performance",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            ChartType = SeriesChartType.StackedColumn,
        //        };
        //        chartOee.Series.Add(chartSeries2);
        //        var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "Quality",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            ChartType = SeriesChartType.StackedColumn,
        //        };
        //        chartOee.Series.Add(chartSeries3);
        //        var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
        //        {
        //            Name = "OEE",
        //            Color = System.Drawing.Color.AntiqueWhite,
        //            IsVisibleInLegend = false,
        //            IsXValueIndexed = true,
        //            ChartType = SeriesChartType.Point,
        //        };
        //        chartOee.Series.Add(chartSeries4);

        //        chartOee.ChartAreas[0].AxisX.Interval = 1;
        //        chartOee.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        chartOee.ChartAreas[0].BorderWidth = 1;
        //        chartOee.ChartAreas[0].BorderColor = Color.White;
        //        chartOee.ChartAreas[0].BackColor = Color.White;
        //        chartOee.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //        chartOee.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
        //        chartOee.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;



        //        chartSeries1.Points.Clear();
        //        for (int count = 0; count < totalRow; count++)
        //        {
        //            DateTime date = Convert.ToDateTime(dt2.Rows[count].ItemArray[0]);
        //            string mcId = date.ToString("yyyy-MM-dd");
        //            string avai = dt2.Rows[count].ItemArray[1].ToString();
        //            string per = dt2.Rows[count].ItemArray[2].ToString();
        //            string qul = dt2.Rows[count].ItemArray[3].ToString();
        //            string oee = dt2.Rows[count].ItemArray[4].ToString();
        //            if (avai == "")
        //            {
        //                avai = per = qul = oee = "0";
        //            }
        //            chartSeries1.Points.AddXY(mcId, avai);
        //            chartSeries1.Points[count].Color = Color.FromArgb(191, 255, 191);
        //            chartSeries1.Points[count].BorderWidth = 1;
        //            chartSeries1.Points[count].BorderColor = Color.Black;
        //            chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //            chartSeries2.Points.AddXY(mcId, per);
        //            chartSeries2.Points[count].Color = Color.FromArgb(255, 127, 127);
        //            chartSeries2.Points[count].BorderWidth = 1;
        //            chartSeries2.Points[count].BorderColor = Color.Black;
        //            chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //            chartSeries3.Points.AddXY(mcId, qul);
        //            chartSeries3.Points[count].Color = Color.FromArgb(149, 223, 255);
        //            chartSeries3.Points[count].BorderWidth = 1;
        //            chartSeries3.Points[count].BorderColor = Color.Black;
        //            chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //            chartSeries4.Points.AddXY(mcId, oee);
        //            chartSeries4.Points[count].Color = Color.White;
        //            chartSeries4.Points[count].BorderWidth = 1;
        //            chartSeries4.Points[count].BorderColor = Color.Black;
        //            chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
        //        }
        //        //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
        //        chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries1.IsValueShownAsLabel = false;
        //        chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries2.IsValueShownAsLabel = false;
        //        chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries3.IsValueShownAsLabel = false;
        //        chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries4.IsValueShownAsLabel = true;
        //    }
        //    else
        //    {
        //        chartOee.Series.Clear();
        //    }
        //}

        #endregion

        #endregion




        #region chart config

        //private int PieHitPointIndex(Chart pie, MouseEventArgs e)
        //{
        //    HitTestResult hitPiece = pie.HitTest(e.X, e.Y, ChartElementType.DataPoint);
        //    HitTestResult hitLegend = pie.HitTest(e.X, e.Y, ChartElementType.LegendItem);
        //    int pointIndex = -1;
        //    if (hitPiece.Series != null) pointIndex = hitPiece.PointIndex;
        //    if (hitLegend.Series != null) pointIndex = hitLegend.PointIndex;
        //    return pointIndex;
        //}

        //public void chartLegendConstruct_1(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color[] _color, DataTable _dt)
        //{
        //    float rowHeight = 15F;
        //    int numberRow = _dt.Rows.Count;
        //    TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
        //    if (_tp != null)
        //    {
        //        _tp.Controls.Clear();
        //        _tp.Dispose();
        //        _tp = null;
        //    }

        //    TableLayoutPanel panel = new TableLayoutPanel();
        //    panel.AutoScroll = true;
        //    panel.ColumnCount = 2;
        //    panel.RowCount = 1;
        //    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
        //    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        //    panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        //    panel.Name = panelName;
        //    panel.Dock = DockStyle.Fill;
        //    panel.Margin = new Padding(1);
        //    momPanel.Controls.Add(panel, 0, startPanelRow);

        //    _tp = (TableLayoutPanel)momPanel.Controls[panelName];
        //    if (_tp != null)
        //    {
        //        _tp.RowStyles[0].SizeType = SizeType.Absolute;
        //        _tp.RowStyles[0].Height = rowHeight;

        //        for (int count = 0; count < numberRow; count++)
        //        {
        //            string lossGroupID = _dt.Rows[count].ItemArray[0].ToString();
        //            string lossNumber = _dt.Rows[count].ItemArray[1].ToString();

        //            Label lb1 = new Label();
        //            Label lb2 = new Label();
        //            lb1.Margin = new Padding(1);
        //            lb1.Dock = DockStyle.Fill;
        //            lb1.BorderStyle = BorderStyle.FixedSingle;
        //            lb1.TextAlign = ContentAlignment.MiddleCenter;
        //            lb1.ForeColor = Color.White;
        //            lb1.Text = "";
        //            lb1.BackColor = _color[count];
        //            lb1.Name = lbName + count + ToString();

        //            lb2.Name = lbName + "_" + count + ToString();
        //            lb2.Margin = new Padding(1);
        //            lb2.Dock = DockStyle.Fill;
        //            lb2.BorderStyle = BorderStyle.FixedSingle;
        //            lb2.TextAlign = ContentAlignment.MiddleLeft;
        //            lb2.ForeColor = Color.White;
        //            lb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
        //            lb2.Text = lossGroupID;


        //            _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
        //            _tp.Controls.Add(lb1, 0, count);
        //            _tp.Controls.Add(lb2, 0, count);
        //            if (count == numberRow - 1)
        //            {
        //                _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        //                _tp.Controls.Add(new Panel(), 0, count + 1);
        //            }
        //        }
        //    }
        //}

        //public void chartLegendConstruct_2(TableLayoutPanel momPanel, int startPanelRow, string panelName, string lbName, Color[] _color, DataTable _dt)
        //{
        //    float rowHeight = 15F;
        //    int numberRow = _dt.Rows.Count;
        //    TableLayoutPanel _tp = (TableLayoutPanel)momPanel.Controls[panelName];
        //    if (_tp != null)
        //    {
        //        _tp.Controls.Clear();
        //        _tp.Dispose();
        //        _tp = null;
        //    }

        //    TableLayoutPanel panel = new TableLayoutPanel();
        //    panel.AutoScroll = true;
        //    panel.ColumnCount = 2;
        //    panel.RowCount = 1;
        //    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        //    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        //    panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        //    panel.Name = panelName;
        //    panel.Dock = DockStyle.Fill;
        //    panel.Margin = new Padding(1);
        //    momPanel.Controls.Add(panel, 0, startPanelRow);

        //    _tp = (TableLayoutPanel)momPanel.Controls[panelName];
        //    if (_tp != null)
        //    {
        //        _tp.RowStyles[0].SizeType = SizeType.Absolute;
        //        _tp.RowStyles[0].Height = rowHeight;

        //        for (int count = 0; count < numberRow; count++)
        //        {
        //            string lossGroupID = _dt.Rows[count].ItemArray[0].ToString();
        //            string lossNumber = _dt.Rows[count].ItemArray[1].ToString();

        //            Label lb1 = new Label();
        //            Label lb2 = new Label();
        //            lb1.Margin = new Padding(1);
        //            lb1.Dock = DockStyle.Fill;
        //            lb1.BorderStyle = BorderStyle.FixedSingle;
        //            lb1.TextAlign = ContentAlignment.MiddleCenter;
        //            lb1.ForeColor = Color.White;
        //            lb1.Text = "";
        //            lb1.BackColor = _color[count];
        //            lb1.Name = lbName + count + ToString();

        //            lb2.Name = lbName + "_" + count + ToString();
        //            lb2.Margin = new Padding(1);
        //            lb2.Dock = DockStyle.Fill;
        //            lb2.BorderStyle = BorderStyle.FixedSingle;
        //            lb2.TextAlign = ContentAlignment.MiddleLeft;
        //            lb2.ForeColor = Color.White;
        //            lb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //            lb2.Text = lossGroupID;


        //            _tp.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
        //            _tp.Controls.Add(lb1, 0, count);
        //            _tp.Controls.Add(lb2, 0, count);
        //            if (count == numberRow - 1)
        //            {
        //                _tp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        //                _tp.Controls.Add(new Panel(), 0, count + 1);
        //            }
        //        }
        //    }
        //}

        //private void colorConstruct(ref Color[] _color)
        //{
        //    Random rnd = new Random();
        //    int size = _color.Length;

        //    for (int count = 0; count < size; count++)
        //    {
        //        Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        //        _color[count] = randomColor;
        //    }
        //}











        #endregion


    }
}
