using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static KPI.Class.GraphOEE;
using static KPI.ProdForm.MachineDownTimeForm;

namespace KPI.Class
{
    public static class Charts
    {
        public static void ColumnSingleColor(DataTable dt2, Chart chart1, Color color)
        {
            if (dt2.Rows.Count > 0)
            {
                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Min",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Max",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries2);
                chartSeries1.YAxisType = AxisType.Primary;
                chartSeries2.YAxisType = AxisType.Secondary;

                //chart1.ChartAreas[0].AxisX.Interval = 1;
                //chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                //chart1.ChartAreas[0].AxisX.Title = "Working hour";
                //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
                //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10, FontStyle.Regular);

                //chart1.ChartAreas[0].AxisY.Title = "Product volume (pcs)";
                //chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                //chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
                //chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

                chart1.ChartAreas[0].AxisY2.Title = "%";
                chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 8, FontStyle.Bold);
                chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

                //chart1.ChartAreas[0].AxisX.Interval = 1;
                //chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
                //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

                chart1.BackColor = Color.White;
                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.Transparent; //Color.White;
                chart1.ChartAreas[0].BackColor = Color.Transparent; // Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

                chartSeries1.Points.Clear();
                for (int count = 0; count < totalRow; count++)
                {
                    string station = dt2.Rows[count].ItemArray[0].ToString();
                    string min = dt2.Rows[count].ItemArray[1].ToString();
                    string max = dt2.Rows[count].ItemArray[2].ToString();
                    //string avg = dt2.Rows[count].ItemArray[3].ToString();

                    chartSeries1.Points.AddXY(station, min);
                    chartSeries1.Points[count].Color = color;
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(station, max);
                    chartSeries2.Points[count].Color = Color.DeepPink;
                    chartSeries2.Points[count].BorderWidth = 2;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries2.Points[count].MarkerSize = 10;
                    chartSeries2.Points[count].MarkerColor = Color.Red;
                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void OEEMCA(OEEOeeMCMonitor oeeA, string[] nameplate, Chart chart1, Color color)
        {

            chart1.Series.Clear();
            var chartSeries1 = new Series
            {
                Name = "Values",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Max",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);
            chartSeries1.YAxisType = AxisType.Primary;
            chartSeries2.YAxisType = AxisType.Secondary;

            chart1.ChartAreas[0].AxisY2.Title = "%";
            chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
            chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 8, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.BackColor = Color.White;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.Transparent; //Color.White;
            chart1.ChartAreas[0].BackColor = Color.Transparent; // Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

            chartSeries1.Points.Clear();


            chartSeries1.Points.AddXY(nameplate[0], oeeA.A1.ToString());
            chartSeries1.Points[0].Color = color;
            chartSeries1.Points[0].BorderWidth = 1;
            chartSeries1.Points[0].BorderColor = Color.Black;
            chartSeries1.Points[0].BorderDashStyle = ChartDashStyle.Solid;

            chartSeries1.Points.AddXY(nameplate[1], oeeA.A2.ToString());
            chartSeries1.Points[1].Color = color;
            chartSeries1.Points[1].BorderWidth = 1;
            chartSeries1.Points[1].BorderColor = Color.Black;
            chartSeries1.Points[1].BorderDashStyle = ChartDashStyle.Solid;

            chartSeries1.Points.AddXY(nameplate[2], oeeA.A3.ToString());
            chartSeries1.Points[2].Color = color;
            chartSeries1.Points[2].BorderWidth = 1;
            chartSeries1.Points[2].BorderColor = Color.Black;
            chartSeries1.Points[2].BorderDashStyle = ChartDashStyle.Solid;


            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries1.IsValueShownAsLabel = true;
        }

        public static void OEEMCP(OEEOeeMCMonitor oeeP, string[] nameplate, Chart chart1, Color color)
        {
            chart1.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Min",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Max",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);
            chartSeries1.YAxisType = AxisType.Primary;
            chartSeries2.YAxisType = AxisType.Secondary;


            chart1.ChartAreas[0].AxisY2.Title = "%";
            chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
            chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 8, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.BackColor = Color.White;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.Transparent; //Color.White;
            chart1.ChartAreas[0].BackColor = Color.Transparent; // Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

            chartSeries1.Points.Clear();

            chartSeries1.Points.AddXY(nameplate[0], oeeP.P1.ToString());
            chartSeries1.Points[0].Color = color;
            chartSeries1.Points[0].BorderWidth = 1;
            chartSeries1.Points[0].BorderColor = Color.Black;
            chartSeries1.Points[0].BorderDashStyle = ChartDashStyle.Solid;

            chartSeries1.Points.AddXY(nameplate[1], oeeP.P2.ToString());
            chartSeries1.Points[1].Color = color;
            chartSeries1.Points[1].BorderWidth = 1;
            chartSeries1.Points[1].BorderColor = Color.Black;
            chartSeries1.Points[1].BorderDashStyle = ChartDashStyle.Solid;

            chartSeries1.Points.AddXY(nameplate[2], oeeP.P3.ToString());
            chartSeries1.Points[2].Color = color;
            chartSeries1.Points[2].BorderWidth = 1;
            chartSeries1.Points[2].BorderColor = Color.Black;
            chartSeries1.Points[2].BorderDashStyle = ChartDashStyle.Solid;

            chartSeries1.Points.AddXY(nameplate[3], oeeP.P4.ToString());
            chartSeries1.Points[3].Color = color;
            chartSeries1.Points[3].BorderWidth = 1;
            chartSeries1.Points[3].BorderColor = Color.Black;
            chartSeries1.Points[3].BorderDashStyle = ChartDashStyle.Solid;

            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries1.IsValueShownAsLabel = true;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries2.IsValueShownAsLabel = true;


        }




        public static void OEEMCQ(OEEOeeMCMonitor oeeQ, string[] nameplate, Chart chart1, Color color)
        {

            chart1.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Min",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Max",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);
            chartSeries1.YAxisType = AxisType.Primary;
            chartSeries2.YAxisType = AxisType.Secondary;


            chart1.ChartAreas[0].AxisY2.Title = "%";
            chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
            chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 8, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.BackColor = Color.White;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.Transparent; //Color.White;
            chart1.ChartAreas[0].BackColor = Color.Transparent; // Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

            chartSeries1.Points.Clear();

            chartSeries1.Points.AddXY(nameplate[0], oeeQ.NG.ToString());
            chartSeries1.Points[0].Color = color;
            chartSeries1.Points[0].BorderWidth = 1;
            chartSeries1.Points[0].BorderColor = Color.Black;
            chartSeries1.Points[0].BorderDashStyle = ChartDashStyle.Solid;

            chartSeries1.Points.AddXY(nameplate[1], oeeQ.RE.ToString());
            chartSeries1.Points[1].Color = color;
            chartSeries1.Points[1].BorderWidth = 1;
            chartSeries1.Points[1].BorderColor = Color.Black;
            chartSeries1.Points[1].BorderDashStyle = ChartDashStyle.Solid;


            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries1.IsValueShownAsLabel = true;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries2.IsValueShownAsLabel = true;


        }



        public static void OEEMCofAlarmCode(List<AlarmCode> data, Chart chart1, Color color, string machine, string datedisplay)
        {
            chart1.Series.Clear();

            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "errcode",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "percent",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);

            chartSeries1.YAxisType = AxisType.Primary;
            chartSeries2.YAxisType = AxisType.Secondary;




            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = machine + " 'M/C of : " + datedisplay;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = "Alarm loss (Hour)";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;


            chart1.ChartAreas[0].AxisY2.Title = "%";
            chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Black;
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY2.Maximum = 100;



            chart1.BackColor = Color.White;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.Transparent; //Color.White;
            chart1.ChartAreas[0].BackColor = Color.Transparent; // Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
            //chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

            chartSeries1.Points.Clear();
            int len = data.Count;
            len = len > 7 ? 7 : len;
            int count = 0;
            foreach (AlarmCode d in data)
            {
                string loss = Math.Round(d.alarmtime / 60, 1, MidpointRounding.AwayFromZero).ToString();
                string percent = Math.Round(d.percent, 1, MidpointRounding.AwayFromZero).ToString();

                chartSeries1.Points.AddXY(d.alarmecode, loss);
                chartSeries1.Points[count].Color = color;
                chartSeries1.Points[count].BorderWidth = 1;
                chartSeries1.Points[count].BorderColor = Color.Black;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries2.Points.AddXY(d.alarmecode, percent);
                //chartSeries2.Points[count].Color = Color.Black;
                //chartSeries2.Points[count].BorderWidth = 2;
                //chartSeries2.Points[count].BorderColor = Color.Black;
                //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                //chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                //chartSeries2.Points[count].MarkerSize = 10;
                //chartSeries2.Points[count].MarkerColor = Color.Red;

                count++;
                if (count == len)
                {
                    break;
                }
            }

            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries1.IsValueShownAsLabel = true;
            //chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            //chartSeries2.IsValueShownAsLabel = true;

        }


        public static void OEEMCofLoss(List<SevenLoss> data, Chart chart1, string machine, string datedisplay)
        {
            chart1.Series.Clear();

            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "loss",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "percent",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);

            chartSeries1.YAxisType = AxisType.Primary;
            chartSeries2.YAxisType = AxisType.Secondary;



            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = machine + " M/C of : " + datedisplay;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = "Major Loss ( Hour )";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;



            chart1.ChartAreas[0].AxisY2.Title = "%";
            chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Black;
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY2.Maximum = 100;


            chart1.BackColor = Color.White;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.Transparent; //Color.White;
            chart1.ChartAreas[0].BackColor = Color.Transparent; // Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
            //chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

            chartSeries1.Points.Clear();
            int count = 0;
            foreach (SevenLoss d in data)
            {
                string loss = Math.Round(d.lossTime / 60, 1, MidpointRounding.AwayFromZero).ToString();
                string percent = Math.Round(d.percent, 1, MidpointRounding.AwayFromZero).ToString();

                chartSeries1.Points.AddXY(d.lossCode, loss);
                chartSeries1.Points[count].Color = Color.FromArgb(d.r, d.g, d.b);
                chartSeries1.Points[count].BorderWidth = 1;
                chartSeries1.Points[count].BorderColor = Color.Black;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries2.Points.AddXY(d.lossCode, percent);
                //chartSeries2.Points[count].Color = Color.Orange;
                //chartSeries2.Points[count].BorderWidth = 2;
                //chartSeries2.Points[count].BorderColor = Color.Black;
                //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                //chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                //chartSeries2.Points[count].MarkerSize = 10;
                //chartSeries2.Points[count].MarkerColor = Color.Orange;


                count++;
            }

            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries1.IsValueShownAsLabel = true;
            //chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            //chartSeries2.IsValueShownAsLabel = true;

        }





        public static void ColumnMultiColor(DataTable dt2, Chart chart1, Color[] _color)
        {
            if (dt2.Rows.Count > 0)
            {

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Min",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Max",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries2);

                chartSeries1.YAxisType = AxisType.Primary;
                chartSeries2.YAxisType = AxisType.Secondary;

                //chart1.ChartAreas[0].AxisY2.Title = "%";
                //chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 11, FontStyle.Bold);
                //chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

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
                    string station = dt2.Rows[count].ItemArray[0].ToString();
                    string min = dt2.Rows[count].ItemArray[1].ToString();
                    string max = dt2.Rows[count].ItemArray[2].ToString();

                    chartSeries1.Points.AddXY(station, min);
                    chartSeries1.Points[count].Color = _color[count];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(station, max);
                    chartSeries2.Points[count].Color = Color.DeepPink;
                    chartSeries2.Points[count].BorderWidth = 2;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries2.Points[count].MarkerSize = 10;
                    chartSeries2.Points[count].MarkerColor = Color.Gray;



                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = true;
            }
            else
            {
                chart1.Series.Clear();
            }
        }



        public static void ProductionProgress(List<PG_Production> progress, Chart chart, Color[] color)
        {

            chart.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "MRG Target",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "TL Target",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries2);
            var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "MGR Actual",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries3);
            var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "TL Actual",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries4);

            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].BorderWidth = 1;
            chart.ChartAreas[0].BorderColor = Color.White;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Black;
            chart.ChartAreas[0].AxisX2.MajorGrid.LineColor = Color.Blue;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
            chart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            chart.BackGradientStyle = GradientStyle.Center;

            chartSeries1.Points.Clear();
            int i = 0;
            foreach (PG_Production item in progress.OrderBy(r => r.registDate))
            {

                //}
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                string[] value = new string[2];
                string Yaxis = (i + 1).ToString();
                //double Xaxis1 = Convert.ToDouble(dt.Rows[i].ItemArray[13]);
                //double Xaxis2 = Convert.ToDouble(dt.Rows[i].ItemArray[14]);

                string Xaxis1str = item.MH_R_MGR > 150 ? "150" : item.MH_R_MGR.ToString();
                string Xaxis2str = item.MH_R_TL > 150 ? "150" : item.MH_R_TL.ToString();

                chartSeries1.Points.AddXY(Yaxis, "104");
                chartSeries1.Points[i].Color = color[0];
                chartSeries1.Points[i].BorderWidth = 2;
                chartSeries1.Points[i].BorderColor = Color.Black;
                chartSeries1.Points[i].BorderDashStyle = ChartDashStyle.Dash;

                chartSeries2.Points.AddXY(Yaxis, "80");
                chartSeries2.Points[i].Color = color[1];
                chartSeries2.Points[i].BorderWidth = 2;
                chartSeries2.Points[i].BorderColor = Color.Black;
                chartSeries2.Points[i].BorderDashStyle = ChartDashStyle.Dash;

                chartSeries3.Points.AddXY(Yaxis, Xaxis1str);
                chartSeries3.Points[i].Color = color[2];
                chartSeries3.Points[i].BorderWidth = 3;
                chartSeries3.Points[i].BorderColor = Color.Black;
                chartSeries3.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries4.Points.AddXY(Yaxis, Xaxis2str);
                chartSeries4.Points[i].Color = color[3];
                chartSeries4.Points[i].BorderWidth = 3;
                chartSeries4.Points[i].BorderColor = Color.Black;
                chartSeries4.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                i++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.LabelForeColor = color[0];
            chartSeries1.IsValueShownAsLabel = false;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries2.LabelForeColor = color[1];
            chartSeries2.IsValueShownAsLabel = false;
            chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries3.LabelForeColor = color[0];
            chartSeries3.IsValueShownAsLabel = false;
            chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries4.LabelForeColor = color[1];
            chartSeries4.IsValueShownAsLabel = false;


        }



        public static void LineProgress(DataTable dt, Chart chart, Color[] color)
        {

            chart.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "MRG Target",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "TL Target",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries2);
            var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "MGR Actual",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries3);
            var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "TL Actual",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries4);

            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].BorderWidth = 1;
            chart.ChartAreas[0].BorderColor = Color.White;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Black;
            chart.ChartAreas[0].AxisX2.MajorGrid.LineColor = Color.Blue;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
            chart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            chart.BackGradientStyle = GradientStyle.Center;

            chartSeries1.Points.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] value = new string[2];
                string Yaxis = (i + 1).ToString();
                double Xaxis1 = Convert.ToDouble(dt.Rows[i].ItemArray[13]);
                double Xaxis2 = Convert.ToDouble(dt.Rows[i].ItemArray[14]);

                string Xaxis1str = Xaxis1 > 150 ? "150" : Xaxis1.ToString();
                string Xaxis2str = Xaxis2 > 150 ? "150" : Xaxis2.ToString();

                chartSeries1.Points.AddXY(Yaxis, "104");
                chartSeries1.Points[i].Color = color[0];
                chartSeries1.Points[i].BorderWidth = 2;
                chartSeries1.Points[i].BorderColor = Color.Black;
                chartSeries1.Points[i].BorderDashStyle = ChartDashStyle.Dash;

                chartSeries2.Points.AddXY(Yaxis, "80");
                chartSeries2.Points[i].Color = color[1];
                chartSeries2.Points[i].BorderWidth = 2;
                chartSeries2.Points[i].BorderColor = Color.Black;
                chartSeries2.Points[i].BorderDashStyle = ChartDashStyle.Dash;

                chartSeries3.Points.AddXY(Yaxis, Xaxis1str);
                chartSeries3.Points[i].Color = color[2];
                chartSeries3.Points[i].BorderWidth = 3;
                chartSeries3.Points[i].BorderColor = Color.Black;
                chartSeries3.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries4.Points.AddXY(Yaxis, Xaxis2str);
                chartSeries4.Points[i].Color = color[3];
                chartSeries4.Points[i].BorderWidth = 3;
                chartSeries4.Points[i].BorderColor = Color.Black;
                chartSeries4.Points[i].BorderDashStyle = ChartDashStyle.Solid;


            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.LabelForeColor = color[0];
            chartSeries1.IsValueShownAsLabel = false;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries2.LabelForeColor = color[1];
            chartSeries2.IsValueShownAsLabel = false;
            chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries3.LabelForeColor = color[0];
            chartSeries3.IsValueShownAsLabel = false;
            chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries4.LabelForeColor = color[1];
            chartSeries4.IsValueShownAsLabel = false;


        }


        public static void RedRatio(DataGridView dataGridView2, Chart chart, Color[] color)
        {
            chart.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Tagget100",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Target85",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries2);
            var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Monday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries3);
            var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Tuesday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries4);
            var chartSeries5 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Wednesday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries5);
            var chartSeries6 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Thursday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries6);
            var chartSeries7 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Friday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries7);

            var chartSeries8 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Saturday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries8);
            var chartSeries9 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Sunday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries9);

            //var chartSeries10 = new System.Windows.Forms.DataVisualization.Charting.Series
            //{
            //    Name = "Target",
            //    Color = System.Drawing.Color.AntiqueWhite,
            //    IsVisibleInLegend = false,
            //    IsXValueIndexed = true,
            //    ChartType = SeriesChartType.Line,
            //};
            //chart.Series.Add(chartSeries10);
            //var chartSeries11 = new System.Windows.Forms.DataVisualization.Charting.Series
            //{
            //    Name = "Actual",
            //    Color = System.Drawing.Color.AntiqueWhite,
            //    IsVisibleInLegend = false,
            //    IsXValueIndexed = true,
            //    ChartType = SeriesChartType.Line,
            //};
            //chart.Series.Add(chartSeries11);


            //chartSeries1.YAxisType = AxisType.Primary;
            //chartSeries2.YAxisType = AxisType.Primary;
            //chartSeries3.YAxisType = AxisType.Primary;
            //chartSeries4.YAxisType = AxisType.Primary;
            //chartSeries5.YAxisType = AxisType.Primary;
            //chartSeries6.YAxisType = AxisType.Primary;
            //chartSeries7.YAxisType = AxisType.Primary;
            //chartSeries8.YAxisType = AxisType.Primary;
            //chartSeries9.YAxisType = AxisType.Primary;
            //chartSeries10.YAxisType = AxisType.Secondary;
            //chartSeries11.YAxisType = AxisType.Secondary;



            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].BorderWidth = 1;
            chart.ChartAreas[0].BorderColor = Color.White;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;



            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisX.Title = "Working hour";
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10, FontStyle.Regular);

            chart.ChartAreas[0].AxisY.Title = "Product volume (pcs)";
            chart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart.ChartAreas[0].AxisY.TitleForeColor = Color.Black;


            //chart1.ChartAreas[0].AxisX.Interval = 1;
            //chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisX.Title = machine + " M/C of : " + datedisplay;
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            //chart1.ChartAreas[0].AxisY.Title = "Major Loss ( Hour )";
            //chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            //chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            //chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            //chart.ChartAreas[0].AxisY2.Title = "Target";
            //chart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated270;
            //chart.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            //chart.ChartAreas[0].AxisY2.TitleForeColor = Color.Black;
            //chart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;
            //chart1.ChartAreas[0].AxisY2.Maximum = 100;


            chartSeries1.Points.Clear();
            for (int i = 0; i < 10; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value == null)
                {
                    return;
                }
                string Yaxis = dataGridView2.Rows[i].Cells[0].Value.ToString();
                string[] value = new string[10];
                for (int k = 1; k < 9; k++)
                {
                    if (dataGridView2.Rows[i].Cells[k].Value == null)
                    {
                        value[k - 1] = "0";
                    }
                    else
                    {
                        value[k - 1] = dataGridView2.Rows[i].Cells[k].Value.ToString();
                    }
                }

                chartSeries1.Points.AddXY(Yaxis, value[0]);
                chartSeries1.Points[i].Color = color[0];
                chartSeries1.Points[i].BorderWidth = 1;
                chartSeries1.Points[i].BorderColor = Color.Black;
                chartSeries1.Points[i].BorderDashStyle = ChartDashStyle.Dash;

                chartSeries2.Points.AddXY(Yaxis, value[1]);
                chartSeries2.Points[i].Color = color[1];
                chartSeries2.Points[i].BorderWidth = 1;
                chartSeries2.Points[i].BorderColor = Color.Black;
                chartSeries2.Points[i].BorderDashStyle = ChartDashStyle.DashDotDot;

                chartSeries3.Points.AddXY(Yaxis, value[2]);
                chartSeries3.Points[i].Color = color[2];
                chartSeries3.Points[i].BorderWidth = 2;
                chartSeries3.Points[i].BorderColor = Color.Black;
                chartSeries3.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries4.Points.AddXY(Yaxis, value[3]);
                chartSeries4.Points[i].Color = color[3];
                chartSeries4.Points[i].BorderWidth = 2;
                chartSeries4.Points[i].BorderColor = Color.Black;
                chartSeries4.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries5.Points.AddXY(Yaxis, value[4]);
                chartSeries5.Points[i].Color = color[4];
                chartSeries5.Points[i].BorderWidth = 2;
                chartSeries5.Points[i].BorderColor = Color.Black;
                chartSeries5.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries6.Points.AddXY(Yaxis, value[5]);
                chartSeries6.Points[i].Color = color[5];
                chartSeries6.Points[i].BorderWidth = 2;
                chartSeries6.Points[i].BorderColor = Color.Black;
                chartSeries6.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries7.Points.AddXY(Yaxis, value[6]);
                chartSeries7.Points[i].Color = color[6];
                chartSeries7.Points[i].BorderWidth = 2;
                chartSeries7.Points[i].BorderColor = Color.Black;
                chartSeries7.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries8.Points.AddXY(Yaxis, value[7]);
                chartSeries8.Points[i].Color = color[7];
                chartSeries8.Points[i].BorderWidth = 2;
                chartSeries8.Points[i].BorderColor = Color.Black;
                chartSeries8.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries9.Points.AddXY(Yaxis, value[8]);
                chartSeries9.Points[i].Color = color[8];
                chartSeries9.Points[i].BorderWidth = 2;
                chartSeries9.Points[i].BorderColor = Color.Black;
                chartSeries9.Points[i].BorderDashStyle = ChartDashStyle.Solid;



            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.LabelForeColor = color[0];
            chartSeries1.IsValueShownAsLabel = true;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries2.LabelForeColor = color[1];
            chartSeries2.IsValueShownAsLabel = true;
            chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries3.LabelForeColor = color[2];
            chartSeries3.IsValueShownAsLabel = true;
            chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries4.LabelForeColor = color[3];
            chartSeries4.IsValueShownAsLabel = true;
            chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries5.LabelForeColor = color[4];
            chartSeries5.IsValueShownAsLabel = true;
            chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries6.LabelForeColor = color[5];
            chartSeries6.IsValueShownAsLabel = true;
            chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries7.LabelForeColor = color[6];
            chartSeries7.IsValueShownAsLabel = true;
            chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries8.LabelForeColor = color[7];
            chartSeries8.IsValueShownAsLabel = true;
            chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries9.LabelForeColor = color[8];
            chartSeries9.IsValueShownAsLabel = true;


        }



        public static void RedRatio1(DataGridView dataGridView2, Chart chart, Color[] color, List<Ppas> volume)
        {
            chart.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Tagget100",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Target85",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries2);
            var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Monday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries3);
            var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Tuesday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries4);
            var chartSeries5 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Wednesday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries5);
            var chartSeries6 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Thursday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries6);
            var chartSeries7 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Friday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries7);

            var chartSeries8 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Saturday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries8);
            var chartSeries9 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Sunday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries9);

            var chartSeries10 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Target",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries10);
            var chartSeries11 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Actual",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries11);


            chartSeries1.YAxisType = AxisType.Primary;
            chartSeries2.YAxisType = AxisType.Primary;
            chartSeries3.YAxisType = AxisType.Primary;
            chartSeries4.YAxisType = AxisType.Primary;
            chartSeries5.YAxisType = AxisType.Primary;
            chartSeries6.YAxisType = AxisType.Primary;
            chartSeries7.YAxisType = AxisType.Primary;
            chartSeries8.YAxisType = AxisType.Primary;
            chartSeries9.YAxisType = AxisType.Primary;
            chartSeries10.YAxisType = AxisType.Secondary;
            chartSeries11.YAxisType = AxisType.Secondary;



            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].BorderWidth = 1;
            chart.ChartAreas[0].BorderColor = Color.White;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;


            //chart1.ChartAreas[0].AxisX.Interval = 1;
            //chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisX.Title = machine + " M/C of : " + datedisplay;
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            //chart1.ChartAreas[0].AxisY.Title = "Major Loss ( Hour )";
            //chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            //chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            //chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            chart.ChartAreas[0].AxisY2.Title = "Target (Pcs)";
            chart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated270;
            chart.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart.ChartAreas[0].AxisY2.TitleForeColor = Color.Black;
            chart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;
            //chart1.ChartAreas[0].AxisY2.Maximum = 100;


            chartSeries1.Points.Clear();
            for (int i = 0; i < 10; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value == null)
                {
                    return;
                }
                string Yaxis = dataGridView2.Rows[i].Cells[0].Value.ToString();
                string[] value = new string[10];
                for (int k = 1; k < 9; k++)
                {
                    if (dataGridView2.Rows[i].Cells[k].Value == null)
                    {
                        value[k - 1] = "0";
                    }
                    else
                    {
                        value[k - 1] = dataGridView2.Rows[i].Cells[k].Value.ToString();
                    }
                }

                string accPlan = volume[i].accPlan.ToString();
                string accActual = volume[i].accVol.ToString();

                chartSeries1.Points.AddXY(Yaxis, value[0]);
                chartSeries1.Points[i].Color = color[0];
                chartSeries1.Points[i].BorderWidth = 1;
                chartSeries1.Points[i].BorderColor = Color.Black;
                chartSeries1.Points[i].BorderDashStyle = ChartDashStyle.Dash;

                chartSeries2.Points.AddXY(Yaxis, value[1]);
                chartSeries2.Points[i].Color = color[1];
                chartSeries2.Points[i].BorderWidth = 1;
                chartSeries2.Points[i].BorderColor = Color.Black;
                chartSeries2.Points[i].BorderDashStyle = ChartDashStyle.DashDotDot;

                chartSeries3.Points.AddXY(Yaxis, value[2]);
                chartSeries3.Points[i].Color = color[2];
                chartSeries3.Points[i].BorderWidth = 2;
                chartSeries3.Points[i].BorderColor = Color.Black;
                chartSeries3.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries4.Points.AddXY(Yaxis, value[3]);
                chartSeries4.Points[i].Color = color[3];
                chartSeries4.Points[i].BorderWidth = 2;
                chartSeries4.Points[i].BorderColor = Color.Black;
                chartSeries4.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries5.Points.AddXY(Yaxis, value[4]);
                chartSeries5.Points[i].Color = color[4];
                chartSeries5.Points[i].BorderWidth = 2;
                chartSeries5.Points[i].BorderColor = Color.Black;
                chartSeries5.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries6.Points.AddXY(Yaxis, value[5]);
                chartSeries6.Points[i].Color = color[5];
                chartSeries6.Points[i].BorderWidth = 2;
                chartSeries6.Points[i].BorderColor = Color.Black;
                chartSeries6.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries7.Points.AddXY(Yaxis, value[6]);
                chartSeries7.Points[i].Color = color[6];
                chartSeries7.Points[i].BorderWidth = 2;
                chartSeries7.Points[i].BorderColor = Color.Black;
                chartSeries7.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries8.Points.AddXY(Yaxis, value[7]);
                chartSeries8.Points[i].Color = color[7];
                chartSeries8.Points[i].BorderWidth = 2;
                chartSeries8.Points[i].BorderColor = Color.Black;
                chartSeries8.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries9.Points.AddXY(Yaxis, value[8]);
                chartSeries9.Points[i].Color = color[8];
                chartSeries9.Points[i].BorderWidth = 2;
                chartSeries9.Points[i].BorderColor = Color.Black;
                chartSeries9.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries10.Points.AddXY(Yaxis, accPlan);
                chartSeries10.Points[i].Color = Color.Black;
                chartSeries10.Points[i].BorderWidth = 1;
                chartSeries10.Points[i].BorderColor = Color.Black;
                chartSeries10.Points[i].BorderDashStyle = ChartDashStyle.Dash;

                chartSeries11.Points.AddXY(Yaxis, accActual);
                chartSeries11.Points[i].Color = Color.FromArgb(24, 86, 0);
                chartSeries11.Points[i].BorderWidth = 2;
                chartSeries11.Points[i].BorderColor = Color.Black;
                chartSeries11.Points[i].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries11.Points[i].MarkerStyle = MarkerStyle.Square;
                chartSeries11.Points[i].MarkerSize = 10;
                chartSeries11.Points[i].MarkerColor = Color.Gray;



            }

            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.LabelForeColor = color[0];
            chartSeries1.IsValueShownAsLabel = true;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries2.LabelForeColor = color[1];
            chartSeries2.IsValueShownAsLabel = true;
            chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries3.LabelForeColor = color[2];
            chartSeries3.IsValueShownAsLabel = true;
            chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries4.LabelForeColor = color[3];
            chartSeries4.IsValueShownAsLabel = true;
            chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries5.LabelForeColor = color[4];
            chartSeries5.IsValueShownAsLabel = true;
            chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries6.LabelForeColor = color[5];
            chartSeries6.IsValueShownAsLabel = true;
            chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries7.LabelForeColor = color[6];
            chartSeries7.IsValueShownAsLabel = true;
            chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries8.LabelForeColor = color[7];
            chartSeries8.IsValueShownAsLabel = true;
            chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries9.LabelForeColor = color[8];
            chartSeries9.IsValueShownAsLabel = true;
            chartSeries11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries11.LabelForeColor = Color.Black;
            chartSeries11.IsValueShownAsLabel = true;


        }


        public static void RedRatio2(DataGridView dataGridView2, Chart chart, Color[] color, List<Prod_PPASTable> volume)
        {
            chart.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Tagget100",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Target85",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries2);
            var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Monday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries3);
            var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Tuesday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries4);
            var chartSeries5 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Wednesday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries5);
            var chartSeries6 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Thursday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries6);
            var chartSeries7 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Friday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries7);

            var chartSeries8 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Saturday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries8);
            var chartSeries9 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Sunday",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries9);

            var chartSeries10 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Target",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries10);
            var chartSeries11 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Actual",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart.Series.Add(chartSeries11);


            chartSeries1.YAxisType = AxisType.Primary;
            chartSeries2.YAxisType = AxisType.Primary;
            chartSeries3.YAxisType = AxisType.Primary;
            chartSeries4.YAxisType = AxisType.Primary;
            chartSeries5.YAxisType = AxisType.Primary;
            chartSeries6.YAxisType = AxisType.Primary;
            chartSeries7.YAxisType = AxisType.Primary;
            chartSeries8.YAxisType = AxisType.Primary;
            chartSeries9.YAxisType = AxisType.Primary;
            chartSeries10.YAxisType = AxisType.Secondary;
            chartSeries11.YAxisType = AxisType.Secondary;



            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].BorderWidth = 1;
            chart.ChartAreas[0].BorderColor = Color.White;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;


            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisX.Title = "Working hour";
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10, FontStyle.Regular);

            chart.ChartAreas[0].AxisY.Title = "Product volume (pcs)";
            chart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            chart.ChartAreas[0].AxisY2.Title = "Target (Pcs)";
            chart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated270;
            chart.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart.ChartAreas[0].AxisY2.TitleForeColor = Color.Black;
            chart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;
            //chart1.ChartAreas[0].AxisY2.Maximum = 100;


            chartSeries1.Points.Clear();
            for (int i = 0; i < 10; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value == null)
                {
                    return;
                }
                string Yaxis = dataGridView2.Rows[i].Cells[0].Value.ToString();
                string[] value = new string[10];
                for (int k = 1; k < 9; k++)
                {
                    if (dataGridView2.Rows[i].Cells[k].Value == null)
                    {
                        value[k - 1] = "0";
                    }
                    else
                    {
                        value[k - 1] = dataGridView2.Rows[i].Cells[k].Value.ToString();
                    }
                }


                string accPlan = volume.Count > 0 ? volume[i].accPlan.ToString() : "0";
                string accActual = volume.Count > 0 ? volume[i].accVol.ToString() : "0";

                chartSeries1.Points.AddXY(Yaxis, value[0]);
                chartSeries1.Points[i].Color = color[0];
                chartSeries1.Points[i].BorderWidth = 1;
                chartSeries1.Points[i].BorderColor = Color.Black;
                chartSeries1.Points[i].BorderDashStyle = ChartDashStyle.Dash;

                chartSeries2.Points.AddXY(Yaxis, value[1]);
                chartSeries2.Points[i].Color = color[1];
                chartSeries2.Points[i].BorderWidth = 1;
                chartSeries2.Points[i].BorderColor = Color.Black;
                chartSeries2.Points[i].BorderDashStyle = ChartDashStyle.DashDotDot;

                chartSeries3.Points.AddXY(Yaxis, value[2]);
                chartSeries3.Points[i].Color = color[2];
                chartSeries3.Points[i].BorderWidth = 2;
                chartSeries3.Points[i].BorderColor = Color.Black;
                chartSeries3.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries4.Points.AddXY(Yaxis, value[3]);
                chartSeries4.Points[i].Color = color[3];
                chartSeries4.Points[i].BorderWidth = 2;
                chartSeries4.Points[i].BorderColor = Color.Black;
                chartSeries4.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries5.Points.AddXY(Yaxis, value[4]);
                chartSeries5.Points[i].Color = color[4];
                chartSeries5.Points[i].BorderWidth = 2;
                chartSeries5.Points[i].BorderColor = Color.Black;
                chartSeries5.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries6.Points.AddXY(Yaxis, value[5]);
                chartSeries6.Points[i].Color = color[5];
                chartSeries6.Points[i].BorderWidth = 2;
                chartSeries6.Points[i].BorderColor = Color.Black;
                chartSeries6.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries7.Points.AddXY(Yaxis, value[6]);
                chartSeries7.Points[i].Color = color[6];
                chartSeries7.Points[i].BorderWidth = 2;
                chartSeries7.Points[i].BorderColor = Color.Black;
                chartSeries7.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries8.Points.AddXY(Yaxis, value[7]);
                chartSeries8.Points[i].Color = color[7];
                chartSeries8.Points[i].BorderWidth = 2;
                chartSeries8.Points[i].BorderColor = Color.Black;
                chartSeries8.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries9.Points.AddXY(Yaxis, value[8]);
                chartSeries9.Points[i].Color = color[8];
                chartSeries9.Points[i].BorderWidth = 2;
                chartSeries9.Points[i].BorderColor = Color.Black;
                chartSeries9.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries10.Points.AddXY(Yaxis, accPlan);
                chartSeries10.Points[i].Color = Color.Black;
                chartSeries10.Points[i].BorderWidth = 1;
                chartSeries10.Points[i].BorderColor = Color.Black;
                chartSeries10.Points[i].BorderDashStyle = ChartDashStyle.Dash;

                chartSeries11.Points.AddXY(Yaxis, accActual);
                chartSeries11.Points[i].Color = Color.FromArgb(24, 86, 0);
                chartSeries11.Points[i].BorderWidth = 2;
                chartSeries11.Points[i].BorderColor = Color.Black;
                chartSeries11.Points[i].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries11.Points[i].MarkerStyle = MarkerStyle.Square;
                chartSeries11.Points[i].MarkerSize = 10;
                chartSeries11.Points[i].MarkerColor = Color.Gray;

            }
            //int n = 0;
            //foreach (Prod_PPASTable p in volume)
            //{

            //    chartSeries10.Points.AddXY(Yaxis, accPlan);
            //    chartSeries10.Points[n].Color = Color.Black;
            //    chartSeries10.Points[n].BorderWidth = 1;
            //    chartSeries10.Points[n].BorderColor = Color.Black;
            //    chartSeries10.Points[n].BorderDashStyle = ChartDashStyle.Dash;

            //    chartSeries11.Points.AddXY(Yaxis, accActual);
            //    chartSeries11.Points[n].Color = Color.FromArgb(24, 86, 0);
            //    chartSeries11.Points[n].BorderWidth = 2;
            //    chartSeries11.Points[n].BorderColor = Color.Black;
            //    chartSeries11.Points[n].BorderDashStyle = ChartDashStyle.Solid;
            //    chartSeries11.Points[n].MarkerStyle = MarkerStyle.Square;
            //    chartSeries11.Points[n].MarkerSize = 10;
            //    chartSeries11.Points[n].MarkerColor = Color.Gray;

            //    n++;
            //}

            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.LabelForeColor = color[0];
            chartSeries1.IsValueShownAsLabel = true;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries2.LabelForeColor = color[1];
            chartSeries2.IsValueShownAsLabel = true;
            chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries3.LabelForeColor = color[2];
            chartSeries3.IsValueShownAsLabel = true;
            chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries4.LabelForeColor = color[3];
            chartSeries4.IsValueShownAsLabel = true;
            chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries5.LabelForeColor = color[4];
            chartSeries5.IsValueShownAsLabel = true;
            chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries6.LabelForeColor = color[5];
            chartSeries6.IsValueShownAsLabel = true;
            chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries7.LabelForeColor = color[6];
            chartSeries7.IsValueShownAsLabel = true;
            chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries8.LabelForeColor = color[7];
            chartSeries8.IsValueShownAsLabel = true;
            chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries9.LabelForeColor = color[8];
            chartSeries9.IsValueShownAsLabel = true;
            chartSeries11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries11.LabelForeColor = Color.Black;
            chartSeries11.IsValueShownAsLabel = true;


        }

        public static void RedFront1(List<ProdRedFront> prodRedFront, Chart chart, Color[] color)
        {
            if (prodRedFront.Count > 0)
            {
              
                chart.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Tagget",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "RedCount",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart.Series.Add(chartSeries2);

                chart.ChartAreas[0].AxisX.Interval = 1;
                chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart.ChartAreas[0].BorderWidth = 1;
                chart.ChartAreas[0].BorderColor = Color.White;
                chart.ChartAreas[0].BackColor = Color.White;
                chart.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;



                chart.ChartAreas[0].AxisX.Interval = 1;
                chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart.ChartAreas[0].AxisX.Title = "Date";
                chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
                chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10, FontStyle.Regular);

                chart.ChartAreas[0].AxisY.Title = "Count (point)";
                chart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                chart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
                chart.ChartAreas[0].AxisY.TitleForeColor = Color.Black;



                chartSeries1.Points.Clear();
                int i = 0;
                foreach (var item in prodRedFront)
                {

                   
                    string Yaxis = item.RegistDate.ToString("dd-MM-yy");
                    string redCount =  item.RedCount.ToString();

                    chartSeries1.Points.AddXY(Yaxis, "6");
                    chartSeries1.Points[i].Color = color[0];
                    chartSeries1.Points[i].BorderWidth = 3;
                    chartSeries1.Points[i].BorderColor = Color.Black;
                    chartSeries1.Points[i].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(Yaxis, redCount);
                    chartSeries2.Points[i].Color = color[1];
                    chartSeries2.Points[i].BorderWidth = 2;
                    chartSeries2.Points[i].BorderColor = Color.Black;
                    chartSeries2.Points[i].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries2.Points[i].MarkerStyle = MarkerStyle.Square;
                    chartSeries2.Points[i].MarkerSize = 10;
                    chartSeries2.Points[i].MarkerColor = Color.Yellow;

                    i++;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.LabelForeColor = color[0];
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.LabelForeColor = color[1];
                chartSeries2.IsValueShownAsLabel = true;

            }
            //ChartControlAppendix();
        }


        public static void RedFront(DataTable dt, Chart chart, Color[] color)
        {
            if (dt.Rows.Count > 0)
            {
                //Color[] color = { Color.Red, Color.Blue };
                chart.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Tagget",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "RedCount",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart.Series.Add(chartSeries2);

                chart.ChartAreas[0].AxisX.Interval = 1;
                chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart.ChartAreas[0].BorderWidth = 1;
                chart.ChartAreas[0].BorderColor = Color.White;
                chart.ChartAreas[0].BackColor = Color.White;
                chart.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                chartSeries1.Points.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] value = new string[2];
                    value[0] = "6";
                    string Yaxis = Convert.ToDateTime(dt.Rows[i].ItemArray[0]).ToString("dd-MM-yy");
                    value[1] = dt.Rows[i].ItemArray[1].ToString();

                    chartSeries1.Points.AddXY(Yaxis, value[0]);
                    chartSeries1.Points[i].Color = color[0];
                    chartSeries1.Points[i].BorderWidth = 1;
                    chartSeries1.Points[i].BorderColor = Color.Black;
                    chartSeries1.Points[i].BorderDashStyle = ChartDashStyle.DashDotDot;

                    chartSeries2.Points.AddXY(Yaxis, value[1]);
                    chartSeries2.Points[i].Color = color[1];
                    chartSeries2.Points[i].BorderWidth = 3;
                    chartSeries2.Points[i].BorderColor = Color.Black;
                    chartSeries2.Points[i].BorderDashStyle = ChartDashStyle.Solid;


                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.LabelForeColor = color[0];
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.LabelForeColor = color[1];
                chartSeries2.IsValueShownAsLabel = true;

            }
            //ChartControlAppendix();
        }


        public static void TwoStackOnePoint(DataTable dt2, Chart chart1, Color[] color)
        {
            if (dt2.Rows.Count > 0)
            {
                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Claim",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Other",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Total",
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
                    DateTime date = Convert.ToDateTime(dt2.Rows[count].ItemArray[0]);
                    string mcId = date.ToString("dd-MM-yyyy");
                    string avai = dt2.Rows[count].ItemArray[1].ToString();
                    string per = dt2.Rows[count].ItemArray[2].ToString();
                    string qul = dt2.Rows[count].ItemArray[3].ToString();

                    chartSeries1.Points.AddXY(mcId, avai);
                    chartSeries1.Points[count].Color = Color.FromArgb(255, 127, 127);
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(mcId, per);
                    chartSeries2.Points[count].Color = Color.FromArgb(191, 255, 191);
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(mcId, qul);
                    chartSeries3.Points[count].Color = Color.White;
                    chartSeries3.Points[count].BorderWidth = 1;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = true;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }

        public static void TracProductionHistory(List<TracProductionVolume> values, Chart chart1, Color[] color)
        {
            if (values.Count == 0)
            {
                chart1.Series.Clear();
                return;
            }
            TracProductionVolume datetime1 = values.OrderBy(r => r.registdate).FirstOrDefault();
            string ds = datetime1.registdate.ToString("d-M-yyyy");
            TracProductionVolume datetime2 = values.OrderByDescending(r => r.registdate).FirstOrDefault();
            string de = datetime2.registdate.ToString("d-M-yyyy");

            chart1.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Specify",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Other",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries2);


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "Date between " + ds + " to " + de;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 5, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = "Production (piece)";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;


            chartSeries1.Points.Clear();
            int n = 0;
            foreach (TracProductionVolume value in values)
            {
                string x = value.registdate.ToString("d-M-yy");
                string Specify = value.volume.ToString();
                string other = value.volumeOther.ToString();


                chartSeries1.Points.AddXY(x, Specify);
                chartSeries1.Points[n].Color = color[1];
                chartSeries1.Points[n].BorderWidth = 1;
                chartSeries1.Points[n].BorderColor = Color.Black;
                chartSeries1.Points[n].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries2.Points.AddXY(x, other);
                chartSeries2.Points[n].Color = color[0];
                chartSeries2.Points[n].BorderWidth = 1;
                chartSeries2.Points[n].BorderColor = Color.Black;
                chartSeries2.Points[n].BorderDashStyle = ChartDashStyle.Solid;


                n++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = true;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries2.IsValueShownAsLabel = true;





        }



        public static void TracMachineDownTime(List<TracMachineLoss> values, Chart chart1, Color[] color, string date)
        {
            if (values.Count == 0)
            {
                chart1.Series.Clear();
                return;
            }


            chart1.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "losstime",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries1);
            //var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            //{
            //    Name = "Other",
            //    Color = System.Drawing.Color.AntiqueWhite,
            //    IsVisibleInLegend = false,
            //    IsXValueIndexed = true,
            //    ChartType = SeriesChartType.StackedColumn,
            //};
            //chart1.Series.Add(chartSeries2);


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "Machine name in :" + date;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 7, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = "Down time (hour)";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;


            chartSeries1.Points.Clear();
            int n = 0;
            foreach (TracMachineLoss value in values)
            {
                string x = (n + 1).ToString();
                string Specify = Math.Round(value.losstime / 3600, 1, MidpointRounding.AwayFromZero).ToString();

                chartSeries1.Points.AddXY(x, Specify);
                chartSeries1.Points[n].Color = Color.FromArgb(255, 127, 127);
                chartSeries1.Points[n].BorderWidth = 1;
                chartSeries1.Points[n].BorderColor = Color.Black;
                chartSeries1.Points[n].BorderDashStyle = ChartDashStyle.Solid;

                n++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = true;

        }


        public static void MachineTime(DataTable dt2, Chart chart1, Color[] color)
        {
            if (dt2.Rows.Count > 0)
            {
                //Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
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
                    Name = "StandardCT",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries3);
                var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "MinCT",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries4);
                var chartSeries5 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "MaxCT",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries5);

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
                    string mcId = dt2.Rows[count].ItemArray[0].ToString();
                    string min = dt2.Rows[count].ItemArray[1].ToString();
                    string max = dt2.Rows[count].ItemArray[2].ToString();
                    string max_min = dt2.Rows[count].ItemArray[3].ToString();
                    string std = dt2.Rows[count].ItemArray[4].ToString();

                    chartSeries1.Points.AddXY(mcId, min);
                    chartSeries1.Points[count].Color = Color.FromArgb(255, 204, 255);
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(mcId, max_min);
                    chartSeries2.Points[count].Color = Color.White;//Color.Blue;
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Dash;

                    chartSeries3.Points.AddXY(mcId, std);
                    chartSeries3.Points[count].Color = Color.Blue;
                    chartSeries3.Points[count].BorderWidth = 1;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries4.Points.AddXY(mcId, min);
                    chartSeries4.Points[count].Color = Color.Blue;
                    chartSeries4.Points[count].BorderWidth = 1;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries5.Points.AddXY(mcId, max);
                    chartSeries5.Points[count].Color = Color.Blue;
                    chartSeries5.Points[count].BorderWidth = 1;
                    chartSeries5.Points[count].BorderColor = Color.Black;
                    chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                }
                //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = true;
                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = true;
                chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries5.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void ColumnSingleColorOneAxis(DataTable dt2, Chart chart1, Color color)
        {
            if (dt2.Rows.Count > 0)
            {
                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "MinMax",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
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
                    string mcId = dt2.Rows[count].ItemArray[0].ToString();
                    string value1 = dt2.Rows[count].ItemArray[5].ToString();

                    chartSeries1.Points.AddXY(mcId, value1);
                    chartSeries1.Points[count].Color = color;
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;



                }
                //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void ColumnSingleColorOneAxis1(DataTable dt2, Chart chart1, Color color)
        {
            if (dt2.Rows.Count > 0)
            {
                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "MinMax",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
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
                    string mcId = dt2.Rows[count].ItemArray[0].ToString();
                    string value1 = dt2.Rows[count].ItemArray[1].ToString();

                    chartSeries1.Points.AddXY(mcId, value1);
                    chartSeries1.Points[count].Color = color;
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;



                }
                //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void Column1Color1(DataTable dt2, Chart chart1, Color color)
        {
            if (dt2.Rows.Count > 0)
            {
                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "MinMax",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
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
                    string mcId = dt2.Rows[count].ItemArray[0].ToString();
                    string value1 = dt2.Rows[count].ItemArray[1].ToString();

                    chartSeries1.Points.AddXY(mcId, value1);
                    chartSeries1.Points[count].Color = color;
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;



                }
                //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void ChartNewProductionDay(DateTime date, int stackAmout, List<ProductionByDay> raw, Chart chart1, Color[] color)
        {
            chart1.Series.Clear();
            if (stackAmout == 0)
                return;

            for (int i = 0; i < stackAmout; i++)
            {
                string seriesname = string.Format("P{0}", i);
                var chartSeries1 = new Series
                {
                    Name = seriesname,
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
            }


            var chartSeries2 = new Series
            {
                Name = "Total",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Point,
            };
            chart1.Series.Add(chartSeries2);

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;


            chart1.ChartAreas[0].AxisY.Title = "Pieces";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;


            chart1.ChartAreas[0].AxisX.Title = date.ToString("MMMM , yyyy");
            chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            chart1.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;



            for (int i = 0; i < stackAmout; i++)
            {
                string seriesname = string.Format("P{0}", i);
                chart1.Series[seriesname].Points.Clear();
            }
            chartSeries2.Points.Clear();

            List<ProductionByDay> data = raw.OrderBy(x => x.RegistDate).ToList();
            int count = 0;
            foreach (ProductionByDay d in data)
            {
                string xAxis = d.RegistDate.Day.ToString();

                int stack = 0, total = 0;
                foreach (PartnumerQty q in d.PiecePerModel)
                {
                    string seriesname = string.Format("P{0}", stack);
                    string value1 = "0";
                    value1 = q.Qty.ToString();
                    total += q.Qty;
                    chart1.Series[seriesname].Points.AddXY(xAxis, value1);
                    chart1.Series[seriesname].Color = color[stack];
                    chart1.Series[seriesname].BorderWidth = 1;
                    chart1.Series[seriesname].BorderColor = Color.Black;
                    chart1.Series[seriesname].BorderDashStyle = ChartDashStyle.Solid;
                    stack++;
                }

                chartSeries2.Points.AddXY(xAxis, total.ToString());
                chartSeries2.Points[count].Color = Color.FromArgb(255, 127, 127);
                chartSeries2.Points[count].BorderWidth = 1;
                chartSeries2.Points[count].BorderColor = Color.Black;
                chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                count++;
            }
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries2.IsValueShownAsLabel = true;

        }


        public static void ChartManPowerOTSummary(List<EmpMPsummaryReport> raw, Chart chart1, Color[] color, DateTime startdate, DateTime stopdate)
        {
            chart1.Series.Clear();
            if (raw.Count == 0)
                return;


            var chartSeries1 = new Series
            {
                Name = "overTime",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);

            var chartSeries2 = new Series
            {
                Name = "limit",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;


            chart1.ChartAreas[0].AxisY.Title = "OT control Hour";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            string datestr = string.Format("OT Summary date between {0} to {1}", startdate.ToString("d MMMM yyyy"), stopdate.ToString("d MMMM yyyy"));
            chart1.ChartAreas[0].AxisX.Title = datestr; //date.ToString("MMMM , yyyy");
            chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            chart1.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;



            chartSeries2.Points.Clear();

            List<EmpMPsummaryReport> data = raw.OrderBy(x => x.UserId).ToList();
            int count = 0;
            foreach (EmpMPsummaryReport d in data)
            {
                string xAxis = d.UserId.ToString();
                chartSeries1.Points.AddXY(xAxis, d.MOverTime.ToString());
                chartSeries1.Points[count].Color = color[count];
                chartSeries1.Points[count].BorderWidth = 1;
                chartSeries1.Points[count].BorderColor = Color.Black;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries2.Points.AddXY(xAxis, (50).ToString());
                chartSeries2.Points[count].Color = Color.FromArgb(255, 127, 127);
                chartSeries2.Points[count].BorderWidth = 1;
                chartSeries2.Points[count].BorderColor = Color.Black;
                chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = true;

        }




        public static void ChartProductionDay(List<string> PN, Color[] color, Chart chart1, DataGridView dataGridViewDay)
        {
            string prodPlan = string.Empty;
            if (PN.Count > 0)
            {
                int totalRow = PN.Count - 2;
                chart1.Series.Clear();

                for (int i = 0; i < totalRow; i++)
                {
                    string seriesname = string.Format("P{0}", i);
                    var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                    {
                        Name = seriesname,
                        Color = System.Drawing.Color.AntiqueWhite,
                        IsVisibleInLegend = false,
                        IsXValueIndexed = true,
                        ChartType = SeriesChartType.StackedColumn,
                    };
                    chart1.Series.Add(chartSeries1);
                }


                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Performance",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "productionPlan",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
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

                int rowcount = dataGridViewDay.Rows.Count;
                int colcount = dataGridViewDay.Columns.Count;

                for (int i = 0; i < totalRow; i++)
                {
                    string seriesname = string.Format("P{0}", i);
                    chart1.Series[seriesname].Points.Clear();
                }
                chartSeries2.Points.Clear();
                for (int count = 0; count < colcount - 3; count++)
                {
                    string xaxis = (count + 1).ToString();
                    for (int i = 0; i < totalRow; i++)
                    {
                        string seriesname = string.Format("P{0}", i);
                        string value1 = "0";
                        if (dataGridViewDay.Rows[i].Cells[count + 2].Value != null)
                        {
                            value1 = dataGridViewDay.Rows[i].Cells[count + 2].Value.ToString();
                        }
                        chart1.Series[seriesname].Points.AddXY(xaxis, value1);
                        chart1.Series[seriesname].Color = color[i];
                        chart1.Series[seriesname].BorderWidth = 1;
                        chart1.Series[seriesname].BorderColor = Color.Black;
                        chart1.Series[seriesname].BorderDashStyle = ChartDashStyle.Solid;
                    }
                    string value2 = "0";
                    if (dataGridViewDay.Rows[totalRow + 1].Cells[count + 2].Value != null)
                    {
                        value2 = dataGridViewDay.Rows[totalRow + 1].Cells[count + 2].Value.ToString();
                    }
                    chartSeries2.Points.AddXY(xaxis, value2);
                    chartSeries2.Points[count].Color = Color.FromArgb(255, 127, 127);
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(xaxis, prodPlan);
                    chartSeries3.Points[count].Color = Color.Red;
                    chartSeries3.Points[count].BorderWidth = 1;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                }
                //for (int i = 0; i < totalRow; i++)
                //{
                //    string seriesname = string.Format("P{0}", i);
                //}


                //chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = true;
                //chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries3.IsValueShownAsLabel = false;
                //chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries4.IsValueShownAsLabel = true;
                //ChartLegent.Legend_ListMultiColor1(tpanelChartPartNumber, 1, "tpanelChartPartNumber", "lbcharSixtLoss", color, PN);
            }
            else
            {
                chart1.Series.Clear();
            }

        }


        public static void ChartProductionHour(List<string> PN, List<string> Hd, Color[] color, Chart chart1, DataGridView dataGridViewDay)
        {
            string prodPlan = string.Empty;
            if (PN.Count > 0)
            {
                int totalRow = PN.Count - 2;
                chart1.Series.Clear();

                for (int i = 0; i < totalRow; i++)
                {
                    string seriesname = string.Format("P{0}", i);
                    var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                    {
                        Name = seriesname,
                        Color = System.Drawing.Color.AntiqueWhite,
                        IsVisibleInLegend = false,
                        IsXValueIndexed = true,
                        ChartType = SeriesChartType.StackedColumn,
                    };
                    chart1.Series.Add(chartSeries1);
                }


                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Performance",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "productionPlan",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
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

                int rowcount = dataGridViewDay.Rows.Count;
                int colcount = dataGridViewDay.Columns.Count;

                for (int i = 0; i < totalRow; i++)
                {
                    string seriesname = string.Format("P{0}", i);
                    chart1.Series[seriesname].Points.Clear();
                }
                chartSeries2.Points.Clear();
                for (int count = 0; count < colcount - 3; count++)
                {
                    string xaxis = Hd[count]; //(count + 1).ToString();
                    for (int i = 0; i < totalRow; i++)
                    {
                        string seriesname = string.Format("P{0}", i);
                        string value1 = "0";
                        if (dataGridViewDay.Rows[i].Cells[count + 2].Value != null)
                        {
                            value1 = dataGridViewDay.Rows[i].Cells[count + 2].Value.ToString();
                        }
                        chart1.Series[seriesname].Points.AddXY(xaxis, value1);
                        chart1.Series[seriesname].Color = color[i];
                        chart1.Series[seriesname].BorderWidth = 1;
                        chart1.Series[seriesname].BorderColor = Color.Black;
                        chart1.Series[seriesname].BorderDashStyle = ChartDashStyle.Solid;
                    }
                    string value2 = "0";
                    if (dataGridViewDay.Rows[totalRow + 1].Cells[count + 2].Value != null)
                    {
                        value2 = dataGridViewDay.Rows[totalRow + 1].Cells[count + 2].Value.ToString();
                    }
                    chartSeries2.Points.AddXY(xaxis, value2);
                    chartSeries2.Points[count].Color = Color.FromArgb(255, 127, 127);
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(xaxis, prodPlan);
                    chartSeries3.Points[count].Color = Color.Red;
                    chartSeries3.Points[count].BorderWidth = 1;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                }
                //for (int i = 0; i < totalRow; i++)
                //{
                //    string seriesname = string.Format("P{0}", i);
                //}


                //chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = true;
                //chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries3.IsValueShownAsLabel = false;
                //chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries4.IsValueShownAsLabel = true;
                //ChartLegent.Legend_ListMultiColor1(tpanelChartPartNumber, 1, "tpanelChartPartNumber", "lbcharSixtLoss", color, PN);
            }
            else
            {
                chart1.Series.Clear();
            }

        }



        //public static void ColumnSingleColorOneAxis(DataTable dt2, Chart chart1, Color color)
        //{
        //    if (dt2.Rows.Count > 0)
        //    {
        //        int totalRow = dt2.Rows.Count;
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
        //            string mcId = dt2.Rows[count].ItemArray[0].ToString();
        //            string value1 = dt2.Rows[count].ItemArray[5].ToString();

        //            chartSeries1.Points.AddXY(mcId, value1);
        //            chartSeries1.Points[count].Color = color;
        //            chartSeries1.Points[count].BorderWidth = 1;
        //            chartSeries1.Points[count].BorderColor = Color.Black;
        //            chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
        //        }
        //        chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries1.IsValueShownAsLabel = true;

        //    }
        //    else
        //    {
        //        chart1.Series.Clear();
        //    }
        //}



        //public static void ColumnSingleColorOneAxis(DataTable dt2, Chart chart1, Color color)
        //{
        //    if (dt2.Rows.Count > 0)
        //    {
        //        int totalRow = dt2.Rows.Count;
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
        //            string mcId = dt2.Rows[count].ItemArray[0].ToString();
        //            string value1 = dt2.Rows[count].ItemArray[1].ToString();

        //            chartSeries1.Points.AddXY(mcId, value1);
        //            chartSeries1.Points[count].Color = color;
        //            chartSeries1.Points[count].BorderWidth = 1;
        //            chartSeries1.Points[count].BorderColor = Color.Black;
        //            chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
        //        }
        //        chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
        //        chartSeries1.IsValueShownAsLabel = true;

        //    }
        //    else
        //    {
        //        chart1.Series.Clear();
        //    }
        //}


        public static void Clear(Chart chart1)
        {
            chart1.Series.Clear();
        }


        public static void DefectHistory(DataTable dt2, Chart chart1, Color[] color)
        {
            if (dt2.Rows.Count > 0)
            {
                //Color[] color = {Color.FromArgb(191, 255, 191),Color.FromArgb(255,127,127),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                //    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Claim",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Other",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries2);
                //var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                //{
                //    Name = "Total",
                //    Color = System.Drawing.Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.Point,
                //};
                //chart1.Series.Add(chartSeries3);

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
                    DateTime date = Convert.ToDateTime(dt2.Rows[count].ItemArray[0]);
                    string mcId = date.ToString("dd-MM-yyyy");
                    string avai = dt2.Rows[count].ItemArray[1].ToString();
                    //string per = dt2.Rows[count].ItemArray[2].ToString();
                    //string qul = dt2.Rows[count].ItemArray[3].ToString();

                    chartSeries1.Points.AddXY(mcId, avai);
                    chartSeries1.Points[count].Color = Color.FromArgb(255, 127, 127);
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries2.Points.AddXY(mcId, per);
                    //chartSeries2.Points[count].Color = Color.FromArgb(191, 255, 191);
                    //chartSeries2.Points[count].BorderWidth = 1;
                    //chartSeries2.Points[count].BorderColor = Color.Black;
                    //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries3.Points.AddXY(mcId, qul);
                    //chartSeries3.Points[count].Color = Color.White;
                    //chartSeries3.Points[count].BorderWidth = 1;
                    //chartSeries3.Points[count].BorderColor = Color.Black;
                    //chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                }
                //chartLegendConstruct_1(tpanelChartOee, 1, "tpanelChartOee", "lbchartLoss", color, dt2);
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = true;
                //chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries2.IsValueShownAsLabel = true;
                //chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries3.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }

        public static void MachineDownTime(DataTable dt2, Chart chart1, Color[] color)
        {
            if (dt2.Rows.Count > 0)
            {

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "DownTime",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);


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
                    string mcId = dt2.Rows[count].ItemArray[0].ToString();
                    string avai = dt2.Rows[count].ItemArray[1].ToString();

                    chartSeries1.Points.AddXY(mcId, avai);
                    chartSeries1.Points[count].Color = Color.FromArgb(191, 255, 191);
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }




        public static void NagaraSwitchTimeByStation(DataTable dt2, Chart chart1, string station, int pos)
        {

            if (dt2.Rows.Count > 0)
            {
                Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta };

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "MinMax",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    //IsXValueIndexed = false,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Average",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    //IsXValueIndexed = true,
                    IsXValueIndexed = false,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries2);


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
                    string value1 = dt2.Rows[count].ItemArray[0].ToString();
                    chartSeries1.Points.AddXY(count, value1);
                    if (count == pos || count == pos + 1)
                    {
                        chartSeries1.Points[count].Color = Color.FromArgb(255, 0, 118);
                        chartSeries1.Points[count].BorderColor = Color.FromArgb(255, 0, 118);
                    }
                    else
                    {
                        chartSeries1.Points[count].Color = Color.Blue;
                        chartSeries1.Points[count].BorderColor = Color.Blue;
                    }
                    chartSeries1.Points[count].BorderWidth = 1;

                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(count, 15.8);
                    chartSeries2.Points[count].Color = Color.Red;
                    chartSeries2.Points[count].BorderWidth = 2;
                    chartSeries2.Points[count].BorderColor = Color.Red;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
            }
            else
            {
                chart1.Series.Clear();
            }
        }

        public static void NagaraSwitchTimeHistogram(Dictionary<string, string> dt, Chart chart1, Color[] color)
        {
            if (dt.Count > 0)
            {
                //Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta };

                int totalRow = dt.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "MinMax",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);


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
                    string axis = dt.ElementAt(count).Key;   //dt.Rows[count].ItemArray[0].ToString();
                    string value1 = dt.ElementAt(count).Value;  //dt2.Rows[count].ItemArray[1].ToString();

                    chartSeries1.Points.AddXY(axis, value1);
                    chartSeries1.Points[count].Color = Color.Blue;//Color.FromArgb(255, 127, 127);
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Blue;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;



                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;

            }
            else
            {
                chart1.Series.Clear();
            }
        }





        public static void NagaraSwitchTimeByStation1(List<TracNagaraTime> values, Chart chart1)
        {

            if (values.Count == 0)
            {
                chart1.Series.Clear();
                return;
            }

            Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta };


            chart1.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "MinMax1",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                //IsXValueIndexed = false,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);
            //var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            //{
            //    Name = "Average",
            //    Color = System.Drawing.Color.AntiqueWhite,
            //    IsVisibleInLegend = false,
            //    //IsXValueIndexed = true,
            //    IsXValueIndexed = false,
            //    ChartType = SeriesChartType.Line,
            //};
            //chart1.Series.Add(chartSeries2);


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "Nagara Time record time";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = "Nagara time (sec)";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;


            chartSeries1.Points.Clear();
            int count = 0;
            foreach (TracNagaraTime value in values)
            {
                string x = value.dateTime.ToString("hh:mm:ss");
                string y = value.nagaratime.ToString();
                chartSeries1.Points.AddXY(x, y);
                //if (count == pos || count == pos + 1)
                //{
                //    chartSeries1.Points[count].Color = Color.FromArgb(255, 0, 118);
                //    chartSeries1.Points[count].BorderColor = Color.FromArgb(255, 0, 118);
                //}
                //else
                //{
                chartSeries1.Points[count].Color = Color.Blue;
                chartSeries1.Points[count].BorderColor = Color.Blue;
                //}
                chartSeries1.Points[count].BorderWidth = 1;

                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries2.Points.AddXY(count, 15.8);
                //chartSeries2.Points[count].Color = Color.Red;
                //chartSeries2.Points[count].BorderWidth = 2;
                //chartSeries2.Points[count].BorderColor = Color.Red;
                //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = false;

        }

        public static void NagaraSwitchTimeHistogram1(List<TracParetoNagara> values, Chart chart1, Color[] color)
        {
            if (values.Count == 0)
            {
                chart1.Series.Clear();
                return;
            }
            var station = values.FirstOrDefault();

            //   chart1.Series.Clear();
            var time = DateTime.Now.ToString("hhmmss");
            var chartSeries1 = new Series
            {
                Name = "MinMax" + time,
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "Nagara Time(sec)";
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = "Frequency";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            chartSeries1.Points.Clear();
            int count = 0;
            foreach (TracParetoNagara value in values)
            {
                string axis = value.axis.ToString();   //dt.Rows[count].ItemArray[0].ToString();
                string value1 = value.nagaraTimes.ToString();  //dt2.Rows[count].ItemArray[1].ToString();

                chartSeries1.Points.AddXY(axis, value1);
                chartSeries1.Points[count].Color = Color.Blue;//Color.FromArgb(255, 127, 127);
                chartSeries1.Points[count].BorderWidth = 1;
                chartSeries1.Points[count].BorderColor = Color.Blue;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = false;


        }


        public static void NagaraSwitchTimeByStation2(List<TracNagaraTime> values, Chart chart1)
        {
            chart1.Series.Clear();
            if (values.Count == 0)
                return;
           

            Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta };


            chart1.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "MinMax1",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                //IsXValueIndexed = false,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries1);
            //var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            //{
            //    Name = "Average",
            //    Color = System.Drawing.Color.AntiqueWhite,
            //    IsVisibleInLegend = false,
            //    //IsXValueIndexed = true,
            //    IsXValueIndexed = false,
            //    ChartType = SeriesChartType.Line,
            //};
            //chart1.Series.Add(chartSeries2);


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "Nagara Time record time";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = "Nagara time (sec)";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;


            chartSeries1.Points.Clear();
            int count = 0;
            foreach (TracNagaraTime value in values)
            {
                string x = value.dateTime.ToString("hh:mm:ss");
                string y = value.nagaratime.ToString();
                chartSeries1.Points.AddXY(x, y);
                
                chartSeries1.Points[count].Color = Color.Blue;
                chartSeries1.Points[count].BorderColor = Color.Blue;
                //}
                chartSeries1.Points[count].BorderWidth = 1;

                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = false;

        }

        public static void NagaraSwitchTimeHistogram2(List<TracParetoNagara> values, Chart chart1, Color[] color,int minimumScaleX, int maximumScaleX)
        {
            chart1.Series.Clear();
            if (values.Count == 0)
                return;

            var chartSeries1 = new Series
            {
                Name = "MinMax",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "Nagara Time(sec)";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);
            //chart1.ChartAreas[0].AxisX.Minimum = maximumScaleX ;
            //chart1.ChartAreas[0].AxisX.Maximum = minimumScaleX;

            chart1.ChartAreas[0].AxisY.Title = "Frequency";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

           
            int count = 0;
            foreach (TracParetoNagara value in values)
            {
                string axis = value.axis.ToString();   //dt.Rows[count].ItemArray[0].ToString();
                string value1 = value.nagaraTimes.ToString();  //dt2.Rows[count].ItemArray[1].ToString();

                chartSeries1.Points.AddXY(axis, value1);
                chartSeries1.Points[count].Color = Color.Blue;//Color.FromArgb(255, 127, 127);
                chartSeries1.Points[count].BorderWidth = 1;
                chartSeries1.Points[count].BorderColor = Color.Blue;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = false;


        }




        public static void MachineTimeByMachineId2(List<TracMachineTimes> values, Chart chart1)
        {
            chart1.Series.Clear();
            if (values.Count == 0)
                return;


            Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta };


            chart1.Series.Clear();
            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "MinMax1",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                //IsXValueIndexed = false,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries1);
           

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "Machine Time record time";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = "Machine time (sec)";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;


            chartSeries1.Points.Clear();
            int count = 0;
            foreach (TracMachineTimes value in values)
            {
                string x = value.dateTime.ToString("hh:mm:ss");
                string y = value.machinetime.ToString();
                chartSeries1.Points.AddXY(x, y);

                chartSeries1.Points[count].Color = Color.Blue;
                chartSeries1.Points[count].BorderColor = Color.Blue;
                //}
                chartSeries1.Points[count].BorderWidth = 1;

                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = false;

        }

        public static void MachineTimeHistogram2(List<TracParetoMachine> values, Chart chart1, Color[] color)
        {
            chart1.Series.Clear();
            if (values.Count == 0)
                return;

            var chartSeries1 = new Series
            {
                Name = "MinMax",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "Machine Time(sec)";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = "Frequency";
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            chartSeries1.Points.Clear();
            int count = 0;
            foreach (TracParetoMachine value in values)
            {
                string axis = value.axis.ToString();   //dt.Rows[count].ItemArray[0].ToString();
                string value1 = value.machineTime.ToString();  //dt2.Rows[count].ItemArray[1].ToString();

                chartSeries1.Points.AddXY(axis, value1);
                chartSeries1.Points[count].Color = Color.Blue;//Color.FromArgb(255, 127, 127);
                chartSeries1.Points[count].BorderWidth = 1;
                chartSeries1.Points[count].BorderColor = Color.Blue;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = false;


        }





        public static void MachineParameter4314(List<TracPackingSlipSelect> values, Chart chart1,Color[] color,string titleX,string titleY)
        {
            chart1.Series.Clear();
            if (values.Count == 0)
                return;

            var chartSeries1 = new Series
            {
                Name = "MaxLefTop",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new Series
            {
                Name = "MinLeftTop",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);


            chartSeries1.YAxisType = AxisType.Primary;
            chartSeries2.YAxisType = AxisType.Primary;
          

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = titleX;
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);

            chart1.ChartAreas[0].AxisY.Title = titleY;
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            //chart1.ChartAreas[0].AxisY.Maximum = 1;
           
            chartSeries1.Points.Clear();
            int count = 0;
            foreach (TracPackingSlipSelect value in values)
            {
                string axis = value.currentDateTime.ToString();
                string value1 = value.max.ToString();  
                string value2 = value.min.ToString();


                chartSeries1.Points.AddXY(axis, value1);
                chartSeries1.Points[count].Color = color[0];
                chartSeries1.Points[count].BorderWidth = 2;
                chartSeries1.Points[count].BorderColor = Color.Blue;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries2.Points.AddXY(axis, value2);
                chartSeries2.Points[count].Color = color[1];
                chartSeries2.Points[count].BorderWidth = 2;
                chartSeries2.Points[count].BorderColor = Color.Blue;
                chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                               
                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
            chartSeries1.IsValueShownAsLabel = false;


        }








        public static void LineOEEList(List<OeeLineMonitor> oee, Chart chart1, Color[] _color)
        {
            chart1.Series.Clear();
            if (oee.Count == 0)
                return;

            var chartSeries1 = new Series
            {
                Name = "OEE",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new Series
            {
                Name = "AVI",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);
            var chartSeries3 = new Series
            {
                Name = "PER",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries3);
            var chartSeries4 = new Series
            {
                Name = "QUA",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries4);


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
            int count = 0;
            foreach (OeeLineMonitor o in oee)
            {
                string station = o.registDate.ToString("dd-MM-yy");

                string OEE = o.OEE > 0 ? o.OEE.ToString() : "0";

                string Avi = o.A > 0 ? o.A.ToString() : "0";

                string Per = o.P > 0 ? o.P.ToString() : "0";

                string Qua = o.Q > 0 ? o.Q.ToString() : "0";

                chartSeries1.Points.AddXY(station, OEE);
                chartSeries1.Points[count].Color = _color[0];
                chartSeries1.Points[count].BorderWidth = 1;
                chartSeries1.Points[count].BorderColor = Color.Black;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries2.Points.AddXY(station, Avi);
                chartSeries2.Points[count].Color = _color[1];
                chartSeries2.Points[count].BorderWidth = 2;
                chartSeries2.Points[count].BorderColor = Color.Black;
                chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                chartSeries2.Points[count].MarkerSize = 5;
                chartSeries2.Points[count].MarkerColor = Color.Gray;

                chartSeries3.Points.AddXY(station, Per);
                chartSeries3.Points[count].Color = _color[2];
                chartSeries3.Points[count].BorderWidth = 2;
                chartSeries3.Points[count].BorderColor = Color.Black;
                chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries3.Points[count].MarkerStyle = MarkerStyle.Diamond;
                chartSeries3.Points[count].MarkerSize = 5;
                chartSeries3.Points[count].MarkerColor = Color.DarkOrange;

                chartSeries4.Points.AddXY(station, Qua);
                chartSeries4.Points[count].Color = _color[3];
                chartSeries4.Points[count].BorderWidth = 2;
                chartSeries4.Points[count].BorderColor = Color.Black;
                chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries4.Points[count].MarkerStyle = MarkerStyle.Circle;
                chartSeries4.Points[count].MarkerSize = 5;
                chartSeries4.Points[count].MarkerColor = Color.DarkRed;
                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries1.IsValueShownAsLabel = true;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            chartSeries2.IsValueShownAsLabel = false;
            chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            chartSeries3.IsValueShownAsLabel = false;
            chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            chartSeries4.IsValueShownAsLabel = false;

        }



        public static void LineNewOEEList(List<OeeLineMonitor> oee, Chart chart1, List<SevenLoss> _color, string datedisplay)
        {
            chart1.Series.Clear();
            if (oee.Count == 0)
                return;

            var chartSeries1 = new Series
            {
                Name = "OEE",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new Series
            {
                Name = "Target",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Regular);

            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.Title = "Date  [ day - month - year ] =>" + datedisplay;
            chart1.ChartAreas[0].AxisY.Title = "%OEE Final process";
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            //chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

            chartSeries1.Points.Clear();
            int count = 0;

            SevenLoss target = _color.FirstOrDefault(x => x.lossCode == "target");
            SevenLoss actual = _color.FirstOrDefault(x => x.lossCode == "actual");

            foreach (OeeLineMonitor o in oee)
            {
                string station = o.registDate.ToString("dd-MM-yy");

                string OEE = o.OEE > 0 ? o.OEE.ToString() : "0";


                chartSeries1.Points.AddXY(station, OEE);
                chartSeries1.Points[count].Color = Color.FromArgb(actual.r, actual.g, actual.b);
                chartSeries1.Points[count].BorderWidth = 2;
                chartSeries1.Points[count].BorderColor = Color.Black;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries1.Points[count].MarkerStyle = MarkerStyle.Square;
                chartSeries1.Points[count].MarkerSize = 10;
                chartSeries1.Points[count].MarkerColor = Color.Gray;

                chartSeries2.Points.AddXY(station, 90);
                chartSeries2.Points[count].Color = Color.FromArgb(target.r, target.g, target.b);
                chartSeries2.Points[count].BorderWidth = 2;
                chartSeries2.Points[count].BorderColor = Color.Black;
                chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries1.IsValueShownAsLabel = true;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            chartSeries2.IsValueShownAsLabel = false;


        }



        public static void LineOEE(DataTable dt2, Chart chart1, Color[] _color)
        {
            if (dt2.Rows.Count > 0)
            {

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new Series
                {
                    Name = "OEE",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new Series
                {
                    Name = "AVI",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new Series
                {
                    Name = "PER",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries3);
                var chartSeries4 = new Series
                {
                    Name = "QUA",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries4);



                //chartSeries1.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Secondary;

                //chart1.ChartAreas[0].AxisY2.Title = "%";
                //chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 11, FontStyle.Bold);
                //chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

                //chart1.Series["OEE"]["PixelPointWidth"] = "30";



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
                    DateTime station1 = Convert.ToDateTime(dt2.Rows[count].ItemArray[0].ToString());
                    string station = station1.ToString("dd-MM-yy");
                    double oeedouble = Convert.ToDouble(dt2.Rows[count].ItemArray[4]);
                    string OEE = oeedouble > 0 ? oeedouble.ToString() : "0";

                    double Avidouble = Convert.ToDouble(dt2.Rows[count].ItemArray[1]);
                    string Avi = oeedouble > 0 ? Avidouble.ToString() : "0";

                    double Perdouble = Convert.ToDouble(dt2.Rows[count].ItemArray[2]);
                    string Per = oeedouble > 0 ? Perdouble.ToString() : "0";

                    double Quadouble = Convert.ToDouble(dt2.Rows[count].ItemArray[3]);
                    string Qua = oeedouble > 0 ? Quadouble.ToString() : "0";

                    chartSeries1.Points.AddXY(station, OEE);
                    chartSeries1.Points[count].Color = _color[0];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(station, Avi);
                    chartSeries2.Points[count].Color = _color[1];
                    chartSeries2.Points[count].BorderWidth = 2;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries2.Points[count].MarkerSize = 5;
                    chartSeries2.Points[count].MarkerColor = Color.Gray;

                    chartSeries3.Points.AddXY(station, Per);
                    chartSeries3.Points[count].Color = _color[2];
                    chartSeries3.Points[count].BorderWidth = 2;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries3.Points[count].MarkerStyle = MarkerStyle.Diamond;
                    chartSeries3.Points[count].MarkerSize = 5;
                    chartSeries3.Points[count].MarkerColor = Color.DarkOrange;

                    chartSeries4.Points.AddXY(station, Qua);
                    chartSeries4.Points[count].Color = _color[3];
                    chartSeries4.Points[count].BorderWidth = 2;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries4.Points[count].MarkerStyle = MarkerStyle.Circle;
                    chartSeries4.Points[count].MarkerSize = 5;
                    chartSeries4.Points[count].MarkerColor = Color.DarkRed;




                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
                chartSeries3.IsValueShownAsLabel = false;
                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
                chartSeries4.IsValueShownAsLabel = false;
            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void LineOEE1(DataTable dt2, Chart chartOee, Color[] color)
        {
            if (dt2.Rows.Count > 0)
            {
                int totalRow = dt2.Rows.Count;
                chartOee.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Availability",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chartOee.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Performance",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chartOee.Series.Add(chartSeries2);
                var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Quality",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chartOee.Series.Add(chartSeries3);

                var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "OEE",
                    Color = System.Drawing.Color.Transparent,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.FastLine,
                };
                chartOee.Series.Add(chartSeries4);

                chartSeries1.YAxisType = AxisType.Primary;
                chartSeries2.YAxisType = AxisType.Primary;
                chartSeries3.YAxisType = AxisType.Primary;
                chartSeries4.YAxisType = AxisType.Primary;


                //chartSeries5.YAxisType = AxisType.Secondary;
                //chartSeries6.YAxisType = AxisType.Secondary;
                //chartSeries7.YAxisType = AxisType.Secondary;

                chartOee.ChartAreas[0].AxisX.Interval = 1;
                chartOee.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartOee.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
                chartOee.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Regular);

                chartOee.BackColor = Color.White;
                chartOee.ChartAreas[0].BorderWidth = 1;
                chartOee.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
                chartOee.ChartAreas[0].BackColor = Color.Transparent;
                chartOee.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chartOee.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
                chartOee.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
                //chartOee.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;



                //chartOee.ChartAreas[0].AxisX.Interval = 1;
                //chartOee.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                //chartOee.ChartAreas[0].BorderWidth = 1;
                //chartOee.ChartAreas[0].BorderColor = Color.White;
                //chartOee.ChartAreas[0].BackColor = Color.White;
                //chartOee.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                //chartOee.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                //chartOee.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;



                chartSeries1.Points.Clear();
                for (int count = 0; count < totalRow; count++)
                {
                    DateTime date = Convert.ToDateTime(dt2.Rows[count].ItemArray[0]);
                    string mcId = date.ToString("dd-MM-yy");
                    string avai = dt2.Rows[count].ItemArray[1].ToString();
                    string per = dt2.Rows[count].ItemArray[2].ToString();
                    string qul = dt2.Rows[count].ItemArray[3].ToString();
                    string oee = dt2.Rows[count].ItemArray[4].ToString();

                    chartSeries1.Points.AddXY(mcId, avai);
                    chartSeries1.Points[count].Color = Color.Black; // color[0];
                    chartSeries1.Points[count].BorderWidth = 2;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(mcId, per);
                    chartSeries2.Points[count].Color = Color.Orange; //color[1];
                    chartSeries2.Points[count].BorderWidth = 2;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(mcId, qul);
                    chartSeries3.Points[count].Color = Color.Red; // color[2];
                    chartSeries3.Points[count].BorderWidth = 2;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries4.Points.AddXY(mcId, oee);
                    chartSeries4.Points[count].Color = Color.White;
                    chartSeries4.Points[count].BorderWidth = 1;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = true;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = true;
                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = true;
            }
            else
            {
                chartOee.Series.Clear();
            }
        }


        //public static void LineOEE(DataTable dt2, Chart chartOee, Color[] color)
        //{
        //    if (dt2.Rows.Count > 0)
        //    {
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

        //        chartSeries1.YAxisType = AxisType.Primary;
        //        chartSeries2.YAxisType = AxisType.Primary;
        //        chartSeries3.YAxisType = AxisType.Primary;
        //        chartSeries4.YAxisType = AxisType.Primary;


        //        //chartSeries5.YAxisType = AxisType.Secondary;
        //        //chartSeries6.YAxisType = AxisType.Secondary;
        //        //chartSeries7.YAxisType = AxisType.Secondary;

        //        chartOee.ChartAreas[0].AxisX.Interval = 1;
        //        chartOee.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        chartOee.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
        //        chartOee.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Regular);

        //        chartOee.BackColor = Color.White;
        //        chartOee.ChartAreas[0].BorderWidth = 1;
        //        chartOee.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
        //        chartOee.ChartAreas[0].BackColor = Color.Transparent;
        //        chartOee.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //        chartOee.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
        //        chartOee.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
        //        //chartOee.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;



        //        //chartOee.ChartAreas[0].AxisX.Interval = 1;
        //        //chartOee.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        //chartOee.ChartAreas[0].BorderWidth = 1;
        //        //chartOee.ChartAreas[0].BorderColor = Color.White;
        //        //chartOee.ChartAreas[0].BackColor = Color.White;
        //        //chartOee.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
        //        //chartOee.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
        //        //chartOee.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;



        //        chartSeries1.Points.Clear();
        //        for (int count = 0; count < totalRow; count++)
        //        {
        //            DateTime date = Convert.ToDateTime(dt2.Rows[count].ItemArray[0]);
        //            string mcId = date.ToString("dd-MM-yy");
        //            string avai = dt2.Rows[count].ItemArray[1].ToString();
        //            string per = dt2.Rows[count].ItemArray[2].ToString();
        //            string qul = dt2.Rows[count].ItemArray[3].ToString();
        //            string oee = dt2.Rows[count].ItemArray[4].ToString();

        //            chartSeries1.Points.AddXY(mcId, avai);
        //            chartSeries1.Points[count].Color = color[0];
        //            chartSeries1.Points[count].BorderWidth = 1;
        //            chartSeries1.Points[count].BorderColor = Color.Black;
        //            chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //            chartSeries2.Points.AddXY(mcId, per);
        //            chartSeries2.Points[count].Color = color[1];
        //            chartSeries2.Points[count].BorderWidth = 1;
        //            chartSeries2.Points[count].BorderColor = Color.Black;
        //            chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //            chartSeries3.Points.AddXY(mcId, qul);
        //            chartSeries3.Points[count].Color = color[2];
        //            chartSeries3.Points[count].BorderWidth = 1;
        //            chartSeries3.Points[count].BorderColor = Color.Black;
        //            chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

        //            chartSeries4.Points.AddXY(mcId, oee);
        //            chartSeries4.Points[count].Color = Color.White;
        //            chartSeries4.Points[count].BorderWidth = 1;
        //            chartSeries4.Points[count].BorderColor = Color.Black;
        //            chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
        //        }
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

        public static void MCOEE1(DataTable dt2, Chart chartOee, Color[] color)
        {
            if (dt2.Rows.Count > 0)
            {
                int totalRow = dt2.Rows.Count;
                chartOee.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "OEE",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chartOee.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Availability",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chartOee.Series.Add(chartSeries2);
                var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Performance",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chartOee.Series.Add(chartSeries3);

                var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Quality",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chartOee.Series.Add(chartSeries4);



                //chartSeries1.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Primary;
                //chartSeries3.YAxisType = AxisType.Primary;

                //chartSeries4.YAxisType = AxisType.Primary;


                //chartSeries5.YAxisType = AxisType.Secondary;
                //chartSeries6.YAxisType = AxisType.Secondary;
                //chartSeries7.YAxisType = AxisType.Secondary;

                chartOee.ChartAreas[0].AxisX.Interval = 1;
                chartOee.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartOee.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
                chartOee.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 7, FontStyle.Regular);

                chartOee.BackColor = Color.White;
                chartOee.ChartAreas[0].BorderWidth = 1;
                chartOee.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
                chartOee.ChartAreas[0].BackColor = Color.Transparent;
                chartOee.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chartOee.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
                chartOee.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
                chartOee.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;



                //chartOee.ChartAreas[0].AxisX.Interval = 1;
                //chartOee.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                //chartOee.ChartAreas[0].BorderWidth = 1;
                //chartOee.ChartAreas[0].BorderColor = Color.White;
                //chartOee.ChartAreas[0].BackColor = Color.White;
                //chartOee.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                //chartOee.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                //chartOee.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;



                chartSeries1.Points.Clear();
                for (int count = 0; count < totalRow; count++)
                {
                    string mcId = dt2.Rows[count].ItemArray[0].ToString();
                    string oee = dt2.Rows[count].ItemArray[1].ToString();
                    string avai = dt2.Rows[count].ItemArray[2].ToString();
                    string per = dt2.Rows[count].ItemArray[3].ToString();
                    string qul = dt2.Rows[count].ItemArray[4].ToString();

                    chartSeries1.Points.AddXY(mcId, avai);
                    chartSeries1.Points[count].Color = Color.FromArgb(127, 191, 212);
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(mcId, avai);
                    chartSeries2.Points[count].Color = color[0];
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(mcId, per);
                    chartSeries3.Points[count].Color = color[1];
                    chartSeries3.Points[count].BorderWidth = 1;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries4.Points.AddXY(mcId, qul);
                    chartSeries4.Points[count].Color = color[2];
                    chartSeries4.Points[count].BorderWidth = 1;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = false;
                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = true;
            }
            else
            {
                chartOee.Series.Clear();
            }
        }


        public static void MCOEE(DataTable dt2, Chart chart1, Color[] _color)
        {
            if (dt2.Rows.Count > 0)
            {

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "OEE",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "AVI",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "PER",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries3);
                var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "QUA",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries4);



                //chartSeries1.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Secondary;

                //chart1.ChartAreas[0].AxisY2.Title = "%";
                //chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 11, FontStyle.Bold);
                //chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

                chart1.Series["OEE"]["PixelPointWidth"] = "30";



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
                    //DateTime station1 = Convert.ToDateTime(dt2.Rows[count].ItemArray[0].ToString());
                    string station = dt2.Rows[count].ItemArray[0].ToString();
                    string OEE = dt2.Rows[count].ItemArray[1].ToString();
                    string Avi = dt2.Rows[count].ItemArray[2].ToString();
                    string Per = dt2.Rows[count].ItemArray[3].ToString();
                    string Qua = dt2.Rows[count].ItemArray[4].ToString();

                    chartSeries1.Points.AddXY(station, OEE);
                    chartSeries1.Points[count].Color = Color.SkyBlue;
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(station, Avi);
                    chartSeries2.Points[count].Color = _color[0];
                    chartSeries2.Points[count].BorderWidth = 2;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries2.Points[count].MarkerSize = 5;
                    chartSeries2.Points[count].MarkerColor = Color.Gray;

                    chartSeries3.Points.AddXY(station, Per);
                    chartSeries3.Points[count].Color = _color[1];
                    chartSeries3.Points[count].BorderWidth = 2;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries3.Points[count].MarkerStyle = MarkerStyle.Diamond;
                    chartSeries3.Points[count].MarkerSize = 5;
                    chartSeries3.Points[count].MarkerColor = Color.DarkOrange;

                    chartSeries4.Points.AddXY(station, Qua);
                    chartSeries4.Points[count].Color = _color[2];
                    chartSeries4.Points[count].BorderWidth = 2;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries4.Points[count].MarkerStyle = MarkerStyle.Circle;
                    chartSeries4.Points[count].MarkerSize = 5;
                    chartSeries4.Points[count].MarkerColor = Color.DarkRed;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
                chartSeries3.IsValueShownAsLabel = false;
                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
                chartSeries4.IsValueShownAsLabel = false;
            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void OeemachineList(List<OEEOeeMCMonitor> oee, Chart chart1, Color[] _color)
        {
            chart1.Series.Clear();
            if (oee.Count == 0)
                return;

            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "OEE",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "AVI",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries2);
            var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "PER",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries3);
            var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "QUA",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries4);

            chart1.Series["OEE"]["PixelPointWidth"] = "30";



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

            int count = 0;
            foreach (OEEOeeMCMonitor o in oee)
            {
                string station = o.machineName;
                string OEE = o.Oee < 0 ? "0" : o.Oee.ToString();
                string Avi = o.A < 0 ? "0" : o.A.ToString();
                string Per = o.P < 0 ? "0" : o.P.ToString();
                string Qua = o.Q < 0 ? "0" : o.Q.ToString();

                chartSeries1.Points.AddXY(station, OEE);
                chartSeries1.Points[count].Color = _color[0];
                chartSeries1.Points[count].BorderWidth = 1;
                chartSeries1.Points[count].BorderColor = Color.Black;
                chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries2.Points.AddXY(station, Avi);
                chartSeries2.Points[count].Color = _color[1];
                chartSeries2.Points[count].BorderWidth = 2;
                chartSeries2.Points[count].BorderColor = Color.Black;
                chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries2.Points[count].MarkerStyle = MarkerStyle.Square;
                chartSeries2.Points[count].MarkerSize = 5;
                chartSeries2.Points[count].MarkerColor = Color.Gray;

                chartSeries3.Points.AddXY(station, Per);
                chartSeries3.Points[count].Color = _color[2];
                chartSeries3.Points[count].BorderWidth = 2;
                chartSeries3.Points[count].BorderColor = Color.Black;
                chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries3.Points[count].MarkerStyle = MarkerStyle.Diamond;
                chartSeries3.Points[count].MarkerSize = 5;
                chartSeries3.Points[count].MarkerColor = Color.DarkOrange;

                chartSeries4.Points.AddXY(station, Qua);
                chartSeries4.Points[count].Color = _color[3];
                chartSeries4.Points[count].BorderWidth = 2;
                chartSeries4.Points[count].BorderColor = Color.Black;
                chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries4.Points[count].MarkerStyle = MarkerStyle.Circle;
                chartSeries4.Points[count].MarkerSize = 5;
                chartSeries4.Points[count].MarkerColor = Color.DarkRed;
                count++;
            }
            chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries1.IsValueShownAsLabel = true;
            chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            chartSeries2.IsValueShownAsLabel = false;
            chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            chartSeries3.IsValueShownAsLabel = false;
            chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            chartSeries4.IsValueShownAsLabel = false;

        }


        public static void OeeNewMachineList(List<OEEOeeMCMonitor> oee, Chart chart1, List<SevenLoss> _color, List<SevenLoss> sevenLosssColor, DateTime datename)
        {
            chart1.Series.Clear();
            if (oee.Count == 0)
                return;

            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l1",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l2",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries2);

            var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l3",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries3);
            var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l4",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries4);

            var chartSeries5 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l5",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries5);
            var chartSeries6 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l6",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries6);
            var chartSeries7 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l7",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries7);
            var chartSeries8 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "unclear",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries8);

            var chartSeries9 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "OEE",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries9);
            var chartSeries10 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Target",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries10);
            var chartSeries11 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Totalloss",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Point,
            };
            chart1.Series.Add(chartSeries11);

            chart1.Series["OEE"]["PixelPointWidth"] = "30";


            chartSeries1.YAxisType = AxisType.Secondary;
            chartSeries2.YAxisType = AxisType.Secondary;
            chartSeries3.YAxisType = AxisType.Secondary;
            chartSeries4.YAxisType = AxisType.Secondary;
            chartSeries5.YAxisType = AxisType.Secondary;
            chartSeries6.YAxisType = AxisType.Secondary;
            chartSeries7.YAxisType = AxisType.Secondary;
            chartSeries8.YAxisType = AxisType.Secondary;
            chartSeries9.YAxisType = AxisType.Primary;
            chartSeries10.YAxisType = AxisType.Primary;
            chartSeries11.YAxisType = AxisType.Secondary;


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Regular);

            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisX.Title = "Machine name list of date =>  " + datename.ToString("d-MM-yyyy");
            chart1.ChartAreas[0].AxisY.Title = "% M/C OEE";
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.Maximum = 100;

            chart1.ChartAreas[0].AxisY2.Title = "Loss [minute]";
            chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Black;
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

            chartSeries1.Points.Clear();
            chartSeries2.Points.Clear();
            chartSeries3.Points.Clear();
            chartSeries4.Points.Clear();
            chartSeries5.Points.Clear();
            chartSeries6.Points.Clear();
            chartSeries7.Points.Clear();
            chartSeries8.Points.Clear();
            chartSeries9.Points.Clear();
            chartSeries10.Points.Clear();
            chartSeries11.Points.Clear();


            int count = 0;

            //SevenLoss c1 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L1");
            //SevenLoss c2 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L2");
            //SevenLoss c3 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L3");
            //SevenLoss c4 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L4");
            //SevenLoss c5 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L5");
            //SevenLoss c6 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L6");
            //SevenLoss c7 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L7");
            //SevenLoss c8 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L8");

            SevenLoss target = _color.FirstOrDefault(x => x.lossCode == "target");
            SevenLoss actual = _color.FirstOrDefault(x => x.lossCode == "actual");

            foreach (OEEOeeMCMonitor o in oee)
            {
                string station = o.machineName;
                double oeecal = o.OK / o.Loadingtime * 100;

                //string OEE = oeecal  < 0 ? "0" : Math.Round(oeecal, 1, MidpointRounding.AwayFromZero).ToString();

                double oee1 = Math.Round(oeecal, 1, MidpointRounding.AwayFromZero);
                oee1 = oee1 > 100 ? 100 : oee1;
                string OEE = oee1 < 0 ? "0" : oee1.ToString();



                //string OEE = o.Oee < 0 ? "0" :  Math.Round(o.Oee,1,MidpointRounding.AwayFromZero).ToString();
                //string l1 = o.A1 < 0 ? "0" : o.A1.ToString();
                //string l2 = o.A2 < 0 ? "0" : o.A2.ToString();
                //string l3 = o.A3 < 0 ? "0" : o.A3.ToString();
                //string l4 = o.A4 < 0 ? "0" : o.A4.ToString();
                //string l5 = o.A5 < 0 ? "0" : o.A5.ToString();
                //string l6 = o.A6 < 0 ? "0" : o.A6.ToString();
                //string l7 = o.A7 < 0 ? "0" : o.A7.ToString();
                //string l8 = o.A8 < 0 ? "0" : o.A8.ToString();
                //double loss = Convert.ToDouble(l1) + Convert.ToDouble(l2) + Convert.ToDouble(l3) + Convert.ToDouble(l4) + Convert.ToDouble(l5) +
                //                Convert.ToDouble(l6) + Convert.ToDouble(l7) + Convert.ToDouble(l8);
                //string totalloss = Math.Round(loss,0,MidpointRounding.AwayFromZero).ToString();


                //chartSeries1.Points.AddXY(station, l1);
                //chartSeries1.Points[count].Color = Color.FromArgb(c1.r,c1.g,c1.b);
                //chartSeries1.Points[count].BorderWidth = 1;
                //chartSeries1.Points[count].BorderColor = Color.Black;
                //chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries2.Points.AddXY(station, l2);
                //chartSeries2.Points[count].Color = Color.FromArgb(c2.r, c2.g, c2.b);
                //chartSeries2.Points[count].BorderWidth = 1;
                //chartSeries2.Points[count].BorderColor = Color.Black;
                //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries3.Points.AddXY(station, l3);
                //chartSeries3.Points[count].Color = Color.FromArgb(c3.r, c3.g, c3.b);
                //chartSeries3.Points[count].BorderWidth = 1;
                //chartSeries3.Points[count].BorderColor = Color.Black;
                //chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries4.Points.AddXY(station, l4);
                //chartSeries4.Points[count].Color = Color.FromArgb(c4.r, c4.g, c4.b);
                //chartSeries4.Points[count].BorderWidth = 1;
                //chartSeries4.Points[count].BorderColor = Color.Black;
                //chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries5.Points.AddXY(station, l5);
                //chartSeries5.Points[count].Color = Color.FromArgb(c5.r, c5.g, c5.b);
                //chartSeries5.Points[count].BorderWidth = 1;
                //chartSeries5.Points[count].BorderColor = Color.Black;
                //chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries6.Points.AddXY(station, l6);
                //chartSeries6.Points[count].Color = Color.FromArgb(c6.r, c6.g, c6.b);
                //chartSeries6.Points[count].BorderWidth = 1;
                //chartSeries6.Points[count].BorderColor = Color.Black;
                //chartSeries6.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries7.Points.AddXY(station, l7);
                //chartSeries7.Points[count].Color = Color.FromArgb(c7.r, c7.g, c7.b);
                //chartSeries7.Points[count].BorderWidth = 1;
                //chartSeries7.Points[count].BorderColor = Color.Black;
                //chartSeries7.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries8.Points.AddXY(station, l8);
                //chartSeries8.Points[count].Color = Color.FromArgb(c8.r, c8.g, c8.b);
                //chartSeries8.Points[count].BorderWidth = 1;
                //chartSeries8.Points[count].BorderColor = Color.Black;
                //chartSeries8.Points[count].BorderDashStyle = ChartDashStyle.Solid;



                chartSeries9.Points.AddXY(station, 90);
                chartSeries9.Points[count].Color = Color.FromArgb(target.r, target.g, target.b);
                chartSeries9.Points[count].BorderWidth = 2;
                chartSeries9.Points[count].BorderColor = Color.Black;
                chartSeries9.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries10.Points.AddXY(station, OEE);
                chartSeries10.Points[count].Color = Color.FromArgb(actual.r, actual.g, actual.b);
                chartSeries10.Points[count].BorderWidth = 2;
                chartSeries10.Points[count].BorderColor = Color.Black;
                chartSeries10.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries10.Points[count].MarkerStyle = MarkerStyle.Square;
                chartSeries10.Points[count].MarkerSize = 10;
                chartSeries10.Points[count].MarkerColor = Color.Gray;

                //chartSeries11.Points.AddXY(station, totalloss);
                //chartSeries11.Points[count].Color = Color.Transparent ;
                //chartSeries11.Points[count].BorderWidth = 1;
                //chartSeries11.Points[count].BorderColor = Color.Black;
                //chartSeries11.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                count++;
            }

            //chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries1.IsValueShownAsLabel = false;
            //chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries2.IsValueShownAsLabel = false;
            //chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries3.IsValueShownAsLabel = false;
            //chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries4.IsValueShownAsLabel = false;
            //chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries5.IsValueShownAsLabel = false;
            //chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries6.IsValueShownAsLabel = false;
            //chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries7.IsValueShownAsLabel = false;
            //chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries8.IsValueShownAsLabel = false;
            chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            chartSeries9.IsValueShownAsLabel = false;
            chartSeries10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries10.IsValueShownAsLabel = true;
            //chartSeries11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries11.IsValueShownAsLabel = true;


        }



        public static void MCOEEnewListByDay(List<OEEDateNew> oee, Chart chart1, List<SevenLoss> _color, List<SevenLoss> sevenLosssColor, string machineId, string machine)
        {
            chart1.Series.Clear();
            if (oee.Count == 0)
                return;

            var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l1",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries1);
            var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l2",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries2);

            var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l3",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries3);
            var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l4",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries4);

            var chartSeries5 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l5",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries5);
            var chartSeries6 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l6",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries6);
            var chartSeries7 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "l7",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries7);
            var chartSeries8 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "unclear",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.StackedColumn,
            };
            chart1.Series.Add(chartSeries8);

            var chartSeries9 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "OEE",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries9);
            var chartSeries10 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Target",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            chart1.Series.Add(chartSeries10);
            var chartSeries11 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Totalloss",
                Color = System.Drawing.Color.AntiqueWhite,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Point,
            };
            chart1.Series.Add(chartSeries11);

            chart1.Series["OEE"]["PixelPointWidth"] = "30";


            chartSeries1.YAxisType = AxisType.Secondary;
            chartSeries2.YAxisType = AxisType.Secondary;
            chartSeries3.YAxisType = AxisType.Secondary;
            chartSeries4.YAxisType = AxisType.Secondary;
            chartSeries5.YAxisType = AxisType.Secondary;
            chartSeries6.YAxisType = AxisType.Secondary;
            chartSeries7.YAxisType = AxisType.Secondary;
            chartSeries8.YAxisType = AxisType.Secondary;
            chartSeries9.YAxisType = AxisType.Primary;
            chartSeries10.YAxisType = AxisType.Primary;
            chartSeries11.YAxisType = AxisType.Secondary;


            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}K";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Regular);

            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.Transparent; // Color.White;
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisX.Title = "Date [day-month-year],  Trend of Machine =>  " + machineId + "  : " + machine;
            chart1.ChartAreas[0].AxisY.Title = "% Trend M/C OEE";
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.Maximum = 100;

            chart1.ChartAreas[0].AxisY2.Title = "Loss [minute]";
            chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Black;
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

            chartSeries1.Points.Clear();
            chartSeries2.Points.Clear();
            chartSeries3.Points.Clear();
            chartSeries4.Points.Clear();
            chartSeries5.Points.Clear();
            chartSeries6.Points.Clear();
            chartSeries7.Points.Clear();
            chartSeries8.Points.Clear();
            chartSeries9.Points.Clear();
            chartSeries10.Points.Clear();
            chartSeries11.Points.Clear();



            //SevenLoss c1 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L1");
            //SevenLoss c2 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L2");
            //SevenLoss c3 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L3");
            //SevenLoss c4 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L4");
            //SevenLoss c5 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L5");
            //SevenLoss c6 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L6");
            //SevenLoss c7 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L7");
            //SevenLoss c8 = sevenLosssColor.FirstOrDefault(x => x.lossCode == "L8");

            SevenLoss target = _color.FirstOrDefault(x => x.lossCode == "target");
            SevenLoss actual = _color.FirstOrDefault(x => x.lossCode == "actual");

            int count = 0;
            foreach (OEEDateNew o in oee)
            {
                string station = o.Registdate.ToString("d-M-yy");

                double oee1 = Math.Round(o.OEE, 1, MidpointRounding.AwayFromZero);
                oee1 = oee1 > 100 ? 100 : oee1;
                string OEE = o.OEE < 0 ? "0" : oee1.ToString();

                //string l1 = o.A1 < 0 ? "0" : o.A1.ToString();
                //string l2 = o.A2 < 0 ? "0" : o.A2.ToString();
                //string l3 = o.A3 < 0 ? "0" : o.A3.ToString();
                //string l4 = o.A4 < 0 ? "0" : o.A4.ToString();
                //string l5 = o.A5 < 0 ? "0" : o.A5.ToString();
                //string l6 = o.A6 < 0 ? "0" : o.A6.ToString();
                //string l7 = o.A7 < 0 ? "0" : o.A7.ToString();
                //string l8 = o.A8 < 0 ? "0" : o.A8.ToString();
                //double loss = Convert.ToDouble(l1) + Convert.ToDouble(l2) + Convert.ToDouble(l3) + Convert.ToDouble(l4) + Convert.ToDouble(l5) +
                //                Convert.ToDouble(l6) + Convert.ToDouble(l7) + Convert.ToDouble(l8);
                //string totalloss = Math.Round(loss, 0, MidpointRounding.AwayFromZero).ToString();


                //chartSeries1.Points.AddXY(station, l1);
                //chartSeries1.Points[count].Color = Color.FromArgb(c1.r, c1.g, c1.b);
                //chartSeries1.Points[count].BorderWidth = 1;
                //chartSeries1.Points[count].BorderColor = Color.Black;
                //chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries2.Points.AddXY(station, l2);
                //chartSeries2.Points[count].Color = Color.FromArgb(c2.r, c2.g, c2.b);
                //chartSeries2.Points[count].BorderWidth = 1;
                //chartSeries2.Points[count].BorderColor = Color.Black;
                //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries3.Points.AddXY(station, l3);
                //chartSeries3.Points[count].Color = Color.FromArgb(c3.r, c3.g, c3.b);
                //chartSeries3.Points[count].BorderWidth = 1;
                //chartSeries3.Points[count].BorderColor = Color.Black;
                //chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries4.Points.AddXY(station, l4);
                //chartSeries4.Points[count].Color = Color.FromArgb(c4.r, c4.g, c4.b);
                //chartSeries4.Points[count].BorderWidth = 1;
                //chartSeries4.Points[count].BorderColor = Color.Black;
                //chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries5.Points.AddXY(station, l5);
                //chartSeries5.Points[count].Color = Color.FromArgb(c5.r, c5.g, c5.b);
                //chartSeries5.Points[count].BorderWidth = 1;
                //chartSeries5.Points[count].BorderColor = Color.Black;
                //chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries6.Points.AddXY(station, l6);
                //chartSeries6.Points[count].Color = Color.FromArgb(c6.r, c6.g, c6.b);
                //chartSeries6.Points[count].BorderWidth = 1;
                //chartSeries6.Points[count].BorderColor = Color.Black;
                //chartSeries6.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries7.Points.AddXY(station, l7);
                //chartSeries7.Points[count].Color = Color.FromArgb(c7.r, c7.g, c7.b);
                //chartSeries7.Points[count].BorderWidth = 1;
                //chartSeries7.Points[count].BorderColor = Color.Black;
                //chartSeries7.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                //chartSeries8.Points.AddXY(station, l8);
                //chartSeries8.Points[count].Color = Color.FromArgb(c8.r, c8.g, c8.b);
                //chartSeries8.Points[count].BorderWidth = 1;
                //chartSeries8.Points[count].BorderColor = Color.Black;
                //chartSeries8.Points[count].BorderDashStyle = ChartDashStyle.Solid;



                chartSeries9.Points.AddXY(station, 90);
                chartSeries9.Points[count].Color = Color.FromArgb(target.r, target.g, target.b);
                chartSeries9.Points[count].BorderWidth = 2;
                chartSeries9.Points[count].BorderColor = Color.Black;
                chartSeries9.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                chartSeries10.Points.AddXY(station, OEE);
                chartSeries10.Points[count].Color = Color.FromArgb(actual.r, actual.g, actual.b);
                chartSeries10.Points[count].BorderWidth = 2;
                chartSeries10.Points[count].BorderColor = Color.Black;
                chartSeries10.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                chartSeries10.Points[count].MarkerStyle = MarkerStyle.Square;
                chartSeries10.Points[count].MarkerSize = 10;
                chartSeries10.Points[count].MarkerColor = Color.Gray;

                //chartSeries11.Points.AddXY(station, totalloss);
                //chartSeries11.Points[count].Color = Color.Transparent;
                //chartSeries11.Points[count].BorderWidth = 1;
                //chartSeries11.Points[count].BorderColor = Color.Black;
                //chartSeries11.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                count++;
            }

            //chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries1.IsValueShownAsLabel = false;
            //chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries2.IsValueShownAsLabel = false;
            //chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries3.IsValueShownAsLabel = false;
            //chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries4.IsValueShownAsLabel = false;
            //chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries5.IsValueShownAsLabel = false;
            //chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries6.IsValueShownAsLabel = false;
            //chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries7.IsValueShownAsLabel = false;
            //chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries8.IsValueShownAsLabel = false;
            chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            chartSeries9.IsValueShownAsLabel = false;
            chartSeries10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
            chartSeries10.IsValueShownAsLabel = true;
            //chartSeries11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
            //chartSeries11.IsValueShownAsLabel = true;


        }




        public static void MCOEEListByDay(List<OEEDate> dt2, Chart chart1, Color[] _color)
        {
            if (dt2.Count > 0)
            {

                int totalRow = dt2.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "OEE",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "AVI",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries2);

                //var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                //{
                //    Name = "PER",
                //    Color = System.Drawing.Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.Line,
                //};
                //chart1.Series.Add(chartSeries3);
                //var chartSeries4 = new System.Windows.Forms.DataVisualization.Charting.Series
                //{
                //    Name = "QUA",
                //    Color = System.Drawing.Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.Line,
                //};
                //chart1.Series.Add(chartSeries4);



                //chartSeries1.YAxisType = AxisType.Primary;
                //chartSeries2.YAxisType = AxisType.Secondary;

                //chart1.ChartAreas[0].AxisY2.Title = "%";
                //chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 11, FontStyle.Bold);
                //chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

                chart1.Series["OEE"]["PixelPointWidth"] = "30";



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
                chart1.ChartAreas[0].AxisY.Maximum = 100;
                //chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

                chartSeries1.Points.Clear();
                for (int count = 0; count < totalRow; count++)
                {
                    string station = dt2[count].Registdate.ToString("d-M-yy");
                    string OEE = dt2[count].OEE < 0 ? "0" : dt2[count].OEE.ToString();
                    //string Avi = dt2[count].A < 0 ? "0" : dt2[count].A.ToString();
                    //string Per = dt2[count].P < 0 ? "0" : dt2[count].P.ToString();
                    //string Qua = dt2[count].Q < 0 ? "0" : dt2[count].Q.ToString();

                    chartSeries1.Points.AddXY(station, OEE);
                    chartSeries1.Points[count].Color = _color[0];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries1.Points[count].MarkerStyle = MarkerStyle.Square;
                    chartSeries1.Points[count].MarkerSize = 5;
                    chartSeries1.Points[count].MarkerColor = Color.Gray;

                    chartSeries2.Points.AddXY(station, 90);
                    chartSeries2.Points[count].Color = _color[1];
                    chartSeries2.Points[count].BorderWidth = 2;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    //chartSeries3.Points.AddXY(station, Per);
                    //chartSeries3.Points[count].Color = _color[2];
                    //chartSeries3.Points[count].BorderWidth = 2;
                    //chartSeries3.Points[count].BorderColor = Color.Black;
                    //chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    //chartSeries3.Points[count].MarkerStyle = MarkerStyle.Diamond;
                    //chartSeries3.Points[count].MarkerSize = 5;
                    //chartSeries3.Points[count].MarkerColor = Color.DarkOrange;

                    //chartSeries4.Points.AddXY(station, Qua);
                    //chartSeries4.Points[count].Color = _color[3];
                    //chartSeries4.Points[count].BorderWidth = 2;
                    //chartSeries4.Points[count].BorderColor = Color.Black;
                    //chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    //chartSeries4.Points[count].MarkerStyle = MarkerStyle.Circle;
                    //chartSeries4.Points[count].MarkerSize = 5;
                    //chartSeries4.Points[count].MarkerColor = Color.DarkRed;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
                chartSeries2.IsValueShownAsLabel = false;
                //chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
                //chartSeries3.IsValueShownAsLabel = false;
                //chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f);
                //chartSeries4.IsValueShownAsLabel = false;
            }
            else
            {
                chart1.Series.Clear();
            }
        }






        public static void MCOEEdetailAvi(List<OEEMCList> mcOee, Chart chart1, Color[] color)
        {
            int totalRow = mcOee.Count;
            if (totalRow > 0)
            {
                chart1.Series.Clear();
                var chartSeries1 = new Series
                {
                    Name = "A3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new Series
                {
                    Name = "A2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new Series
                {
                    Name = "A1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries3);
                var chartSeries4 = new Series
                {
                    Name = "Total",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries4);
                var chartSeries5 = new Series
                {
                    Name = "Loading",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries5);


                //   chart1.ChartAreas[0].AxisY.Maximum = 1000;
                chart1.ChartAreas[0].AxisY.Minimum = 0;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                chartSeries1.Points.Clear();
                int count = 0;
                foreach (var item in mcOee)
                {
                    string registdate = item.RegistDate.ToString("dd-MM-yy");
                    string a1 = Math.Round(item.A1, 1, MidpointRounding.AwayFromZero).ToString();
                    string a2 = Math.Round(item.A2, 1, MidpointRounding.AwayFromZero).ToString();
                    string a3 = Math.Round(item.A3, 1, MidpointRounding.AwayFromZero).ToString();
                    string total = Math.Round((item.A1 + item.A2 + item.A3), 1, MidpointRounding.AwayFromZero).ToString();
                    string loading = Math.Round(item.LoadingTime, 1, MidpointRounding.AwayFromZero).ToString();

                    chartSeries1.Points.AddXY(registdate, a3);
                    chartSeries1.Points[count].Color = color[2];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(registdate, a2);
                    chartSeries2.Points[count].Color = color[1];
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(registdate, a1);
                    chartSeries3.Points[count].Color = color[0];
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries4.Points.AddXY(registdate, total);
                    chartSeries4.Points[count].Color = Color.White;
                    chartSeries4.Points[count].BorderWidth = 1;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries5.Points.AddXY(registdate, loading);
                    chartSeries5.Points[count].Color = Color.Blue;
                    chartSeries5.Points[count].BorderWidth = 1;
                    chartSeries5.Points[count].BorderColor = Color.Black;
                    chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    count++;
                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = false;
                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = true;
                chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Italic);
                chartSeries5.IsValueShownAsLabel = true;


            }
            else
            {
                chart1.Series.Clear();
            }
        }

        public static void MCOEEdetailPerf(List<OEEMCList> mcOee, Chart chart1, Color[] color)
        {
            int totalRow = mcOee.Count;
            if (totalRow > 0)
            {
                chart1.Series.Clear();
                var chartSeries1 = new Series
                {
                    Name = "P4",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new Series
                {
                    Name = "P3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new Series
                {
                    Name = "P2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries3);

                var chartSeries4 = new Series
                {
                    Name = "P1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries4);

                var chartSeries5 = new Series
                {
                    Name = "Total",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries5);

                var chartSeries6 = new Series
                {
                    Name = "Loading",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries6);

                // chart1.ChartAreas[0].AxisY.Maximum = 1000;
                chart1.ChartAreas[0].AxisY.Minimum = 0;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                chartSeries1.Points.Clear();
                int count = 0;
                foreach (var item in mcOee)
                {
                    string registdate = item.RegistDate.ToString("dd-MM-yy");
                    string p1 = Math.Round(item.P1, 1, MidpointRounding.AwayFromZero).ToString();
                    string p2 = Math.Round(item.P2, 1, MidpointRounding.AwayFromZero).ToString();
                    string p3 = Math.Round(item.P3, 1, MidpointRounding.AwayFromZero).ToString();
                    string p4 = Math.Round(item.P4, 1, MidpointRounding.AwayFromZero).ToString();
                    string total = Math.Round((item.P1 + item.P2 + item.P3 + item.P4), 1, MidpointRounding.AwayFromZero).ToString();
                    double net = Math.Round(item.LoadingTime - (item.A1 + item.A2 + item.A3), 1, MidpointRounding.AwayFromZero);
                    string nettime = net.ToString();

                    chartSeries1.Points.AddXY(registdate, p4);
                    chartSeries1.Points[count].Color = color[3];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(registdate, p3);
                    chartSeries2.Points[count].Color = color[2];
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(registdate, p2);
                    chartSeries3.Points[count].Color = color[1];
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries4.Points.AddXY(registdate, p1);
                    chartSeries4.Points[count].Color = color[0];
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries5.Points.AddXY(registdate, total);
                    chartSeries5.Points[count].Color = Color.White;
                    chartSeries5.Points[count].BorderWidth = 0;
                    chartSeries5.Points[count].BorderColor = Color.Black;
                    chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries6.Points.AddXY(registdate, nettime);
                    chartSeries6.Points[count].Color = Color.Blue;
                    //  chartSeries6.Points[count].BorderWidth = 1;
                    chartSeries6.Points[count].BorderColor = Color.Black;
                    chartSeries6.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    count++;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = false;
                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = false;
                chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries5.IsValueShownAsLabel = true;
                chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Italic);
                chartSeries6.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }

        public static void MCOEEdetailQual(List<OEEMCList> mcOee, Chart chart1, Color[] color)
        {
            int totalRow = mcOee.Count;
            if (totalRow > 0)
            {
                chart1.Series.Clear();
                var chartSeries1 = new Series
                {
                    Name = "RE",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new Series
                {
                    Name = "NG",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries2);

                var chartSeries3 = new Series
                {
                    Name = "Total",
                    Color = Color.AntiqueWhite,
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
                int count = 0;
                foreach (var item in mcOee)
                {
                    string registdate = item.RegistDate.ToString("dd-MM-yy");
                    string ng = item.NG.ToString();
                    string re = item.RE.ToString();
                    string total = (item.NG + item.RE).ToString();

                    chartSeries1.Points.AddXY(registdate, re);
                    chartSeries1.Points[count].Color = color[1];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(registdate, ng);
                    chartSeries2.Points[count].Color = color[0];
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries3.Points.AddXY(registdate, total);
                    chartSeries3.Points[count].Color = Color.White;
                    chartSeries3.Points[count].BorderWidth = 1;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    count++;
                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }




        public static void MCOEEdetailSummary1(List<OEEOeeMCMonitor> mcOee, Chart chart1, Color[] color)
        {
            int totalRow = mcOee.Count;
            if (totalRow > 0)
            {
                chart1.Series.Clear();


                var chartSeries1 = new Series
                {
                    Name = "RE",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new Series
                {
                    Name = "NG",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new Series
                {
                    Name = "OK",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries3);


                var chartSeries4 = new Series
                {
                    Name = "P4",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries4);

                var chartSeries5 = new Series
                {
                    Name = "P3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries5);

                var chartSeries6 = new Series
                {
                    Name = "P2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries6);

                var chartSeries7 = new Series
                {
                    Name = "P1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries7);


                var chartSeries8 = new Series
                {
                    Name = "A3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries8);

                var chartSeries9 = new Series
                {
                    Name = "A2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries9);

                var chartSeries10 = new Series
                {
                    Name = "A1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries10);


                var chartSeries11 = new Series
                {
                    Name = "loading",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries11);



                //chart1.ChartAreas[0].AxisY.Maximum = 5000;
                //chart1.ChartAreas[0].AxisY.Minimum = 0;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                chartSeries1.Points.Clear();
                int count = 0;
                foreach (var item in mcOee)
                {
                    string registdate = item.registDate.ToString("dd-MM-yy");

                    string ok = Math.Round(item.OK, 1, MidpointRounding.AwayFromZero).ToString();
                    string ng = Math.Round(item.NG, 1, MidpointRounding.AwayFromZero).ToString();
                    string re = Math.Round(item.RE, 1, MidpointRounding.AwayFromZero).ToString();

                    string p4 = Math.Round(item.P4, 1, MidpointRounding.AwayFromZero).ToString();
                    string p3 = Math.Round(item.P3, 1, MidpointRounding.AwayFromZero).ToString();
                    string p2 = Math.Round(item.P2, 1, MidpointRounding.AwayFromZero).ToString();
                    string p1 = Math.Round(item.P1, 1, MidpointRounding.AwayFromZero).ToString();

                    string a3 = Math.Round(item.A3, 1, MidpointRounding.AwayFromZero).ToString();
                    string a2 = Math.Round(item.A2, 1, MidpointRounding.AwayFromZero).ToString();
                    string a1 = Math.Round(item.A1, 1, MidpointRounding.AwayFromZero).ToString();

                    string loading = Math.Round(item.Loadingtime, 0, MidpointRounding.AwayFromZero).ToString();


                    chartSeries1.Points.AddXY(registdate, re);
                    chartSeries1.Points[count].Color = color[9];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(registdate, ng);
                    chartSeries2.Points[count].Color = color[8];
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(registdate, ok);
                    chartSeries3.Points[count].Color = color[7];
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries4.Points.AddXY(registdate, p4);
                    chartSeries4.Points[count].Color = color[6];
                    chartSeries4.Points[count].BorderWidth = 1;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries5.Points.AddXY(registdate, p3);
                    chartSeries5.Points[count].Color = color[5];
                    chartSeries5.Points[count].BorderWidth = 1;
                    chartSeries5.Points[count].BorderColor = Color.Black;
                    chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries6.Points.AddXY(registdate, p2);
                    chartSeries6.Points[count].Color = color[4];
                    chartSeries6.Points[count].BorderColor = Color.Black;
                    chartSeries6.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries7.Points.AddXY(registdate, p1);
                    chartSeries7.Points[count].Color = color[3];
                    chartSeries7.Points[count].BorderWidth = 1;
                    chartSeries7.Points[count].BorderColor = Color.Black;
                    chartSeries7.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries8.Points.AddXY(registdate, a3);
                    chartSeries8.Points[count].Color = color[2];
                    chartSeries8.Points[count].BorderWidth = 1;
                    chartSeries8.Points[count].BorderColor = Color.Black;
                    chartSeries8.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries9.Points.AddXY(registdate, a2);
                    chartSeries9.Points[count].Color = color[1];
                    chartSeries9.Points[count].BorderColor = Color.Black;
                    chartSeries9.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries10.Points.AddXY(registdate, a1);
                    chartSeries10.Points[count].Color = color[0];
                    chartSeries10.Points[count].BorderColor = Color.Black;
                    chartSeries10.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries11.Points.AddXY(registdate, loading);
                    chartSeries11.Points[count].Color = color[10];
                    chartSeries11.Points[count].BorderWidth = 2;
                    chartSeries11.Points[count].BorderColor = Color.Black;
                    chartSeries11.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    count++;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = false;

                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = false;
                chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries5.IsValueShownAsLabel = false;
                chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries6.IsValueShownAsLabel = false;
                chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries7.IsValueShownAsLabel = false;
                chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries8.IsValueShownAsLabel = false;
                chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries9.IsValueShownAsLabel = false;
                chartSeries10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries10.IsValueShownAsLabel = false;
                chartSeries11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Italic);
                chartSeries11.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void MCOEEdetailSummary(List<OEEOeeMCMonitor> mcOee, Chart chart1, Color[] color)
        {
            int totalRow = mcOee.Count;
            if (totalRow > 0)
            {
                chart1.Series.Clear();


                var chartSeries1 = new Series
                {
                    Name = "RE",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new Series
                {
                    Name = "NG",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new Series
                {
                    Name = "OK",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries3);


                var chartSeries4 = new Series
                {
                    Name = "P4",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries4);

                var chartSeries5 = new Series
                {
                    Name = "P3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries5);

                var chartSeries6 = new Series
                {
                    Name = "P2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries6);

                var chartSeries7 = new Series
                {
                    Name = "P1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries7);


                var chartSeries8 = new Series
                {
                    Name = "A3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries8);

                var chartSeries9 = new Series
                {
                    Name = "A2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries9);

                var chartSeries10 = new Series
                {
                    Name = "A1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries10);


                var chartSeries11 = new Series
                {
                    Name = "OEE",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries11);



                //  chart1.ChartAreas[0].AxisY.Maximum = 1000;
                chart1.ChartAreas[0].AxisY.Minimum = 0;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                chartSeries1.Points.Clear();
                int count = 0;
                foreach (var item in mcOee)
                {
                    string registdate = item.registDate.ToString("dd-MM-yy");

                    string ok = Math.Round(item.OK, 1, MidpointRounding.AwayFromZero).ToString();
                    string ng = Math.Round(item.NG, 1, MidpointRounding.AwayFromZero).ToString();
                    string re = Math.Round(item.RE, 1, MidpointRounding.AwayFromZero).ToString();

                    string p4 = Math.Round(item.P4, 1, MidpointRounding.AwayFromZero).ToString();
                    string p3 = Math.Round(item.P3, 1, MidpointRounding.AwayFromZero).ToString();
                    string p2 = Math.Round(item.P2, 1, MidpointRounding.AwayFromZero).ToString();
                    string p1 = Math.Round(item.P1, 1, MidpointRounding.AwayFromZero).ToString();

                    string a3 = Math.Round(item.A3, 1, MidpointRounding.AwayFromZero).ToString();
                    string a2 = Math.Round(item.A2, 1, MidpointRounding.AwayFromZero).ToString();
                    string a1 = Math.Round(item.A1, 1, MidpointRounding.AwayFromZero).ToString();

                    string oee = Math.Round(item.Oee, 0, MidpointRounding.AwayFromZero).ToString();


                    chartSeries1.Points.AddXY(registdate, re);
                    chartSeries1.Points[count].Color = color[9];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(registdate, ng);
                    chartSeries2.Points[count].Color = color[8];
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(registdate, ok);
                    chartSeries3.Points[count].Color = color[7];
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries4.Points.AddXY(registdate, p4);
                    chartSeries4.Points[count].Color = color[6];
                    chartSeries4.Points[count].BorderWidth = 1;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries5.Points.AddXY(registdate, p3);
                    chartSeries5.Points[count].Color = color[5];
                    chartSeries5.Points[count].BorderWidth = 1;
                    chartSeries5.Points[count].BorderColor = Color.Black;
                    chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries6.Points.AddXY(registdate, p2);
                    chartSeries6.Points[count].Color = color[4];
                    chartSeries6.Points[count].BorderColor = Color.Black;
                    chartSeries6.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries7.Points.AddXY(registdate, p1);
                    chartSeries7.Points[count].Color = color[3];
                    chartSeries7.Points[count].BorderWidth = 1;
                    chartSeries7.Points[count].BorderColor = Color.Black;
                    chartSeries7.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries8.Points.AddXY(registdate, a3);
                    chartSeries8.Points[count].Color = color[2];
                    chartSeries8.Points[count].BorderWidth = 1;
                    chartSeries8.Points[count].BorderColor = Color.Black;
                    chartSeries8.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries9.Points.AddXY(registdate, a2);
                    chartSeries9.Points[count].Color = color[1];
                    chartSeries9.Points[count].BorderColor = Color.Black;
                    chartSeries9.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries10.Points.AddXY(registdate, a1);
                    chartSeries10.Points[count].Color = color[0];
                    chartSeries10.Points[count].BorderColor = Color.Black;
                    chartSeries10.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries11.Points.AddXY(registdate, oee);
                    chartSeries11.Points[count].Color = color[10];
                    chartSeries11.Points[count].BorderWidth = 2;
                    chartSeries11.Points[count].BorderColor = Color.Black;
                    chartSeries11.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    count++;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = false;

                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = false;
                chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries5.IsValueShownAsLabel = false;
                chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries6.IsValueShownAsLabel = false;
                chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries7.IsValueShownAsLabel = false;
                chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries8.IsValueShownAsLabel = false;
                chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries9.IsValueShownAsLabel = false;
                chartSeries10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries10.IsValueShownAsLabel = false;
                chartSeries11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Italic);
                chartSeries11.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void LossByMC(List<MCLossOEE> mcOee, Chart chart1, Color[] color)
        {
            int totalRow = mcOee.Count;
            if (totalRow > 0)
            {
                chart1.Series.Clear();

                var chartSeries1 = new Series
                {
                    Name = "G",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new Series
                {
                    Name = "LE",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries2);
                //var chartSeries3 = new Series
                //{
                //    Name = "P2",
                //    Color = Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.StackedColumn,
                //};
                //chart1.Series.Add(chartSeries3);

                //var chartSeries4 = new Series
                //{
                //    Name = "P1",
                //    Color = Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.StackedColumn,
                //};
                //chart1.Series.Add(chartSeries4);


                //var chartSeries5 = new Series
                //{
                //    Name = "A3",
                //    Color = Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.StackedColumn,
                //};
                //chart1.Series.Add(chartSeries5);
                //var chartSeries6 = new Series
                //{
                //    Name = "A2",
                //    Color = Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.StackedColumn,
                //};
                //chart1.Series.Add(chartSeries6);
                //var chartSeries7 = new Series
                //{
                //    Name = "A1",
                //    Color = Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.StackedColumn,
                //};
                //chart1.Series.Add(chartSeries7);


                //var chartSeries8 = new Series
                //{
                //    Name = "Total",
                //    Color = Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.Point,
                //};
                //chart1.Series.Add(chartSeries8);


                //var chartSeries9 = new Series
                //{
                //    Name = "Loading",
                //    Color = Color.AntiqueWhite,
                //    IsVisibleInLegend = false,
                //    IsXValueIndexed = true,
                //    ChartType = SeriesChartType.Line,
                //};
                //chart1.Series.Add(chartSeries9);



                //  chart1.ChartAreas[0].AxisY.Maximum = 1000;
                chart1.ChartAreas[0].AxisY.Minimum = 0;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                chartSeries1.Points.Clear();
                int count = 0;
                foreach (var item in mcOee)
                {
                    string registdate = item.MachineId;
                    string g5 = Math.Round(item.G5 / 60, 2, MidpointRounding.AwayFromZero).ToString();
                    string le5 = Math.Round(item.LE5 / 60, 2, MidpointRounding.AwayFromZero).ToString();
                    //string a3 = Math.Round(item.A3, 1, MidpointRounding.AwayFromZero).ToString();
                    //string p1 = Math.Round(item.P1, 1, MidpointRounding.AwayFromZero).ToString();
                    //string p2 = Math.Round(item.P2, 1, MidpointRounding.AwayFromZero).ToString();
                    //string p3 = Math.Round(item.P3, 1, MidpointRounding.AwayFromZero).ToString();
                    //string p4 = Math.Round(item.P4, 1, MidpointRounding.AwayFromZero).ToString();


                    chartSeries1.Points.AddXY(registdate, g5);
                    chartSeries1.Points[count].Color = color[1];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(registdate, le5);
                    chartSeries2.Points[count].Color = color[0];
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries3.Points.AddXY(registdate, p2);
                    //chartSeries3.Points[count].Color = color[4];
                    //chartSeries3.Points[count].BorderColor = Color.Black;
                    //chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries4.Points.AddXY(registdate, p1);
                    //chartSeries4.Points[count].Color = color[3];
                    //chartSeries4.Points[count].BorderWidth = 1;
                    //chartSeries4.Points[count].BorderColor = Color.Black;
                    //chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    //chartSeries5.Points.AddXY(registdate, a3);
                    //chartSeries5.Points[count].Color = color[2];
                    //chartSeries5.Points[count].BorderWidth = 1;
                    //chartSeries5.Points[count].BorderColor = Color.Black;
                    //chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries6.Points.AddXY(registdate, a2);
                    //chartSeries6.Points[count].Color = color[1];
                    //chartSeries6.Points[count].BorderColor = Color.Black;
                    //chartSeries6.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries7.Points.AddXY(registdate, a1);
                    //chartSeries7.Points[count].Color = color[0];
                    //chartSeries7.Points[count].BorderColor = Color.Black;
                    //chartSeries7.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries8.Points.AddXY(registdate, total);
                    //chartSeries8.Points[count].Color = color[3];
                    //chartSeries8.Points[count].BorderColor = Color.Black;
                    //chartSeries8.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries9.Points.AddXY(registdate, loading);
                    //chartSeries9.Points[count].Color = Color.Blue;
                    //chartSeries9.Points[count].BorderWidth = 2;
                    //chartSeries9.Points[count].BorderColor = Color.Black;
                    //chartSeries9.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    count++;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = true;
                //chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries3.IsValueShownAsLabel = false;

                //chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries4.IsValueShownAsLabel = false;
                //chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries5.IsValueShownAsLabel = false;
                //chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries6.IsValueShownAsLabel = false;
                //chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries7.IsValueShownAsLabel = false;
                //chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                //chartSeries8.IsValueShownAsLabel = true;
                //chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Italic);
                //chartSeries9.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }



        public static void MCOEEdetailSummary2(List<OEEOeeMCMonitor> mcOee, Chart chart1, Color[] color)
        {
            int totalRow = mcOee.Count;
            if (totalRow > 0)
            {
                chart1.Series.Clear();


                var chartSeries1 = new Series
                {
                    Name = "OK",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new Series
                {
                    Name = "NG",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new Series
                {
                    Name = "RE",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries3);


                var chartSeries4 = new Series
                {
                    Name = "P4",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries4);

                var chartSeries5 = new Series
                {
                    Name = "P3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries5);

                var chartSeries6 = new Series
                {
                    Name = "P2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries6);

                var chartSeries7 = new Series
                {
                    Name = "P1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries7);


                var chartSeries8 = new Series
                {
                    Name = "A3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries8);

                var chartSeries9 = new Series
                {
                    Name = "A2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries9);

                var chartSeries10 = new Series
                {
                    Name = "A1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn100,
                };
                chart1.Series.Add(chartSeries10);


                var chartSeries11 = new Series
                {
                    Name = "OEE",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries11);



                //  chart1.ChartAreas[0].AxisY.Maximum = 1000;
                chart1.ChartAreas[0].AxisY.Minimum = 0;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                chartSeries1.Points.Clear();
                int count = 0;
                foreach (var item in mcOee)
                {
                    string registdate = item.machineName;

                    string ok = Math.Round(item.OK, 1, MidpointRounding.AwayFromZero).ToString();
                    string ng = Math.Round(item.NG, 1, MidpointRounding.AwayFromZero).ToString();
                    string re = Math.Round(item.RE, 1, MidpointRounding.AwayFromZero).ToString();

                    string p4 = Math.Round(item.P4, 1, MidpointRounding.AwayFromZero).ToString();
                    string p3 = Math.Round(item.P3, 1, MidpointRounding.AwayFromZero).ToString();
                    string p2 = Math.Round(item.P2, 1, MidpointRounding.AwayFromZero).ToString();
                    string p1 = Math.Round(item.P1, 1, MidpointRounding.AwayFromZero).ToString();

                    string a3 = Math.Round(item.A3, 1, MidpointRounding.AwayFromZero).ToString();
                    string a2 = Math.Round(item.A2, 1, MidpointRounding.AwayFromZero).ToString();
                    string a1 = Math.Round(item.A1, 1, MidpointRounding.AwayFromZero).ToString();

                    string oee = Math.Round(item.Oee, 0, MidpointRounding.AwayFromZero).ToString();


                    chartSeries1.Points.AddXY(registdate, ok);
                    chartSeries1.Points[count].Color = color[9];
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries2.Points.AddXY(registdate, ng);
                    chartSeries2.Points[count].Color = color[8];
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(registdate, re);
                    chartSeries3.Points[count].Color = color[7];
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries4.Points.AddXY(registdate, p4);
                    chartSeries4.Points[count].Color = color[6];
                    chartSeries4.Points[count].BorderWidth = 1;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries5.Points.AddXY(registdate, p3);
                    chartSeries5.Points[count].Color = color[5];
                    chartSeries5.Points[count].BorderWidth = 1;
                    chartSeries5.Points[count].BorderColor = Color.Black;
                    chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries6.Points.AddXY(registdate, p2);
                    chartSeries6.Points[count].Color = color[4];
                    chartSeries6.Points[count].BorderColor = Color.Black;
                    chartSeries6.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries7.Points.AddXY(registdate, p1);
                    chartSeries7.Points[count].Color = color[3];
                    chartSeries7.Points[count].BorderWidth = 1;
                    chartSeries7.Points[count].BorderColor = Color.Black;
                    chartSeries7.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries8.Points.AddXY(registdate, a3);
                    chartSeries8.Points[count].Color = color[2];
                    chartSeries8.Points[count].BorderWidth = 1;
                    chartSeries8.Points[count].BorderColor = Color.Black;
                    chartSeries8.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries9.Points.AddXY(registdate, a2);
                    chartSeries9.Points[count].Color = color[1];
                    chartSeries9.Points[count].BorderColor = Color.Black;
                    chartSeries9.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries10.Points.AddXY(registdate, a1);
                    chartSeries10.Points[count].Color = color[0];
                    chartSeries10.Points[count].BorderColor = Color.Black;
                    chartSeries10.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries11.Points.AddXY(registdate, oee);
                    chartSeries11.Points[count].Color = color[10];
                    chartSeries11.Points[count].BorderWidth = 2;
                    chartSeries11.Points[count].BorderColor = Color.Black;
                    chartSeries11.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    count++;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = false;

                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = false;
                chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries5.IsValueShownAsLabel = false;
                chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries6.IsValueShownAsLabel = false;
                chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries7.IsValueShownAsLabel = false;
                chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries8.IsValueShownAsLabel = false;
                chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries9.IsValueShownAsLabel = false;
                chartSeries10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries10.IsValueShownAsLabel = false;
                chartSeries11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Italic);
                chartSeries11.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }


        public static void MCOEEdetailA123P12(List<OEEMCList> mcOee, Chart chart1, Color[] color)
        {
            int totalRow = mcOee.Count;
            if (totalRow > 0)
            {
                chart1.Series.Clear();

                var chartSeries1 = new Series
                {
                    Name = "P4",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new Series
                {
                    Name = "P3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new Series
                {
                    Name = "P2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries3);

                var chartSeries4 = new Series
                {
                    Name = "P1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries4);


                var chartSeries5 = new Series
                {
                    Name = "A3",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries5);
                var chartSeries6 = new Series
                {
                    Name = "A2",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries6);
                var chartSeries7 = new Series
                {
                    Name = "A1",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StackedColumn,
                };
                chart1.Series.Add(chartSeries7);


                var chartSeries8 = new Series
                {
                    Name = "Total",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Point,
                };
                chart1.Series.Add(chartSeries8);


                var chartSeries9 = new Series
                {
                    Name = "Loading",
                    Color = Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                };
                chart1.Series.Add(chartSeries9);



                //  chart1.ChartAreas[0].AxisY.Maximum = 1000;
                chart1.ChartAreas[0].AxisY.Minimum = 0;

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].BorderWidth = 1;
                chart1.ChartAreas[0].BorderColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.White;
                chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                chartSeries1.Points.Clear();
                int count = 0;
                foreach (var item in mcOee)
                {
                    string registdate = item.RegistDate.ToString("dd-MM-yy");
                    string a1 = Math.Round(item.A1, 1, MidpointRounding.AwayFromZero).ToString();
                    string a2 = Math.Round(item.A2, 1, MidpointRounding.AwayFromZero).ToString();
                    string a3 = Math.Round(item.A3, 1, MidpointRounding.AwayFromZero).ToString();
                    string p1 = Math.Round(item.P1, 1, MidpointRounding.AwayFromZero).ToString();
                    string p2 = Math.Round(item.P2, 1, MidpointRounding.AwayFromZero).ToString();
                    //string p3 = Math.Round(item.P3, 1, MidpointRounding.AwayFromZero).ToString();
                    //string p4 = Math.Round(item.P4, 1, MidpointRounding.AwayFromZero).ToString();

                    string total = Math.Round((item.A1 + item.A2 + item.A3 + item.P1 + item.P2), 1, MidpointRounding.AwayFromZero).ToString();
                    string loading = Math.Round(item.LoadingTime, 1, MidpointRounding.AwayFromZero).ToString();

                    //chartSeries1.Points.AddXY(registdate, p4);
                    //chartSeries1.Points[count].Color = color[6];
                    //chartSeries1.Points[count].BorderWidth = 1;
                    //chartSeries1.Points[count].BorderColor = Color.Black;
                    //chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    //chartSeries2.Points.AddXY(registdate, p3);
                    //chartSeries2.Points[count].Color = color[5];
                    //chartSeries2.Points[count].BorderWidth = 1;
                    //chartSeries2.Points[count].BorderColor = Color.Black;
                    //chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries3.Points.AddXY(registdate, p2);
                    chartSeries3.Points[count].Color = color[4];
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries4.Points.AddXY(registdate, p1);
                    chartSeries4.Points[count].Color = color[3];
                    chartSeries4.Points[count].BorderWidth = 1;
                    chartSeries4.Points[count].BorderColor = Color.Black;
                    chartSeries4.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries5.Points.AddXY(registdate, a3);
                    chartSeries5.Points[count].Color = color[2];
                    chartSeries5.Points[count].BorderWidth = 1;
                    chartSeries5.Points[count].BorderColor = Color.Black;
                    chartSeries5.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries6.Points.AddXY(registdate, a2);
                    chartSeries6.Points[count].Color = color[1];
                    chartSeries6.Points[count].BorderColor = Color.Black;
                    chartSeries6.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries7.Points.AddXY(registdate, a1);
                    chartSeries7.Points[count].Color = color[0];
                    chartSeries7.Points[count].BorderColor = Color.Black;
                    chartSeries7.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries8.Points.AddXY(registdate, total);
                    chartSeries8.Points[count].Color = color[3];
                    chartSeries8.Points[count].BorderColor = Color.Black;
                    chartSeries8.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    chartSeries9.Points.AddXY(registdate, loading);
                    chartSeries9.Points[count].Color = Color.Blue;
                    chartSeries9.Points[count].BorderWidth = 2;
                    chartSeries9.Points[count].BorderColor = Color.Black;
                    chartSeries9.Points[count].BorderDashStyle = ChartDashStyle.Solid;

                    count++;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries1.IsValueShownAsLabel = false;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = false;

                chartSeries4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries4.IsValueShownAsLabel = false;
                chartSeries5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries5.IsValueShownAsLabel = false;
                chartSeries6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries6.IsValueShownAsLabel = false;
                chartSeries7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries7.IsValueShownAsLabel = false;
                chartSeries8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries8.IsValueShownAsLabel = true;
                chartSeries9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, FontStyle.Italic);
                chartSeries9.IsValueShownAsLabel = true;

            }
            else
            {
                chart1.Series.Clear();
            }
        }









        public static void StockMonitoring(DataTable dt2, Chart chart1, Color[] _color)
        {
            if (dt2.Rows.Count > 0)
            {

                int totalRow = dt2.Rows.Count;
                chart1.Series.Clear();
                var chartSeries1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Qty",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Column,
                };
                chart1.Series.Add(chartSeries1);
                var chartSeries2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Lower",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StepLine,
                };
                chart1.Series.Add(chartSeries2);
                var chartSeries3 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Upper",
                    Color = System.Drawing.Color.AntiqueWhite,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.StepLine,
                };
                chart1.Series.Add(chartSeries3);




                chartSeries1.YAxisType = AxisType.Primary;

                //chartSeries2.YAxisType = AxisType.Secondary;

                //chart1.ChartAreas[0].AxisY2.Title = "%";
                //chart1.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //chart1.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 11, FontStyle.Bold);
                //chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.Red;

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

                chartSeries1.Points.AddXY(chartSeries1.Points.Count + 1, 100);

                chartSeries1["PixelPointWidth"] = "20";


                chartSeries1.Points.Clear();
                for (int count = 0; count < totalRow; count++)
                {
                    string station = dt2.Rows[count].ItemArray[0].ToString();
                    int len = station.Length;
                    station = station.Substring(9, len - 9);
                    string qty = dt2.Rows[count].ItemArray[1].ToString();
                    int qtyint = Convert.ToInt32(qty);
                    string lo = dt2.Rows[count].ItemArray[2].ToString();
                    int loint = Convert.ToInt32(lo);
                    string hi = dt2.Rows[count].ItemArray[3].ToString();
                    int hiint = Convert.ToInt32(hi);

                    chartSeries1.Points.AddXY(station, qty);
                    if (qtyint < loint)
                    {
                        chartSeries1.Points[count].Color = Color.Pink;
                    }
                    else if (loint <= qtyint && qtyint <= hiint)
                    {
                        chartSeries1.Points[count].Color = Color.LightGreen;
                    }
                    else if (qtyint > hiint)
                    {
                        chartSeries1.Points[count].Color = Color.FromArgb(255, 255, 0);
                    }
                    //  chartSeries1.Points[count].Color = Color.AliceBlue;
                    chartSeries1.Points[count].BorderWidth = 1;
                    chartSeries1.Points[count].BorderColor = Color.Black;
                    chartSeries1.Points[count].BorderDashStyle = ChartDashStyle.Solid;


                    chartSeries2.Points.AddXY(station, lo);
                    chartSeries2.Points[count].Color = Color.DeepPink;
                    chartSeries2.Points[count].BorderWidth = 1;
                    chartSeries2.Points[count].BorderColor = Color.Black;
                    chartSeries2.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries2.Points[count].MarkerStyle = MarkerStyle.Circle;
                    chartSeries2.Points[count].MarkerSize = 1;
                    chartSeries2.Points[count].MarkerColor = Color.Red;

                    chartSeries3.Points.AddXY(station, hi);
                    chartSeries3.Points[count].Color = Color.Blue;
                    chartSeries3.Points[count].BorderWidth = 1;
                    chartSeries3.Points[count].BorderColor = Color.Black;
                    chartSeries3.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    chartSeries3.Points[count].MarkerStyle = MarkerStyle.Circle;
                    chartSeries3.Points[count].MarkerSize = 1;
                    chartSeries3.Points[count].MarkerColor = Color.Red;

                }
                chartSeries1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                chartSeries1.IsValueShownAsLabel = true;
                chartSeries2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries2.IsValueShownAsLabel = false;
                chartSeries3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
                chartSeries3.IsValueShownAsLabel = false;
            }
            else
            {
                chart1.Series.Clear();
            }
        }

    }
}
