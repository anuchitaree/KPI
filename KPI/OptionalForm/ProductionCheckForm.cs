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
using KPI.KeepClass;
using KPI.Parameter;
using KPI.Class;
using KPI.Models;

namespace KPI.OptionalForm
{
    public partial class ProductionCheckForm : Form
    {

      //  string SectionDivPlant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);


        public ProductionCheckForm()
        {
            InitializeComponent();
        }

        private void ProductionCheckForm_Load(object sender, EventArgs e)
        {
            chartMC.ChartAreas[0].BackColor = Color.White;
            chartLossId.ChartAreas[0].BackColor = Color.White;
            chartDetailId.ChartAreas[0].BackColor = Color.White;
            //TimeClock t = new TimeClock();
            //t.StartupDatetTimePicker(dateTimePicker1);
            InitialComponent.DateTimePicker(dateTimePicker1);
            MT_NamaListSQL();
        }

        private void buttonAnalysis_Click(object sender, EventArgs e)
        {
            MT_MachineTimeRead();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            MT_ProductNameListRead();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            MT_ProductNameListRead();
        }

        // /////////////////////////////// OPERATION LOOP //////////////////////////////////////////

        private void MT_NamaListSQL()
        {
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ProductVolume_MachineNameLoadSQL();
            if (sqlstatus)
            {
                DataTable dt = sql.Datatable;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string str = $"{dr.ItemArray[0]} : {dr.ItemArray[1]}";
                        lsbMC.Items.Add(str);
                    }

                }
            }
        }

        private void MT_ProductNameListRead()
        {
            string start = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string stop = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ProductVolume_ProductNameLoadSQL(dateTimePicker1.Value, dateTimePicker2.Value);
            if (sqlstatus)
            {
                DataTable dt = sql.Datatable;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lsbPN.Items.Add(dr.ItemArray[0]);
                    }
                }
            }
        }

        private void MT_MachineTimeRead()
        {
            string start = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string stop = dateTimePicker2.Value.ToString("yyyy-MM-dd");
        
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ProductVolume_BYMachineLoadSQL(dateTimePicker1.Value, dateTimePicker2.Value);
            if (sqlstatus)
            {
                DataTable dt = sql.Datatable;


                chartMC.Series.Clear();
                chartLossId.Series.Clear();
                chartDetailId.Series.Clear();

                Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen,
                Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};
                // ===== Loss Name =====
                if (dt.Rows.Count > 0)
                {
                    Series chartSeries = chartMC.Series.Add("NUMBER");
                    chartMC.Legends[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                    chartSeries.Color = Color.Transparent;
                    chartMC.ChartAreas[0].AxisX.Interval = 1;
                    chartMC.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chartMC.ChartAreas[0].BorderWidth = 1;
                    chartMC.ChartAreas[0].BorderColor = Color.White;  //Color.Black;
                    chartMC.ChartAreas[0].BackColor = Color.White;
                    chartMC.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                    chartMC.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                    chartMC.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

                    chartMC.Series[0].Points.Clear();
                    //lossDic.Clear();
                    for (int count = 0; count < dt.Rows.Count; count++)
                    {
                        string Yaxis = dt.Rows[count].ItemArray[0].ToString();
                        string Xaxis = dt.Rows[count].ItemArray[2].ToString();
                        chartSeries.Points.AddXY(Yaxis, Xaxis);
                        chartSeries.Points[count].Color = Color.SkyBlue; //color[count];
                        chartSeries.Points[count].BorderWidth = 1;
                        chartSeries.Points[count].BorderColor = Color.Black;
                        chartSeries.Points[count].BorderDashStyle = ChartDashStyle.Solid;
                    }
                    //chartLegendConstruct_1(tpanelChartLoss, 1, "tpanelChartLoss2", "lbchartLoss", color, mcLossTable);
                    chartSeries.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
                    chartSeries.IsValueShownAsLabel = true;


                }
                //====== Machine name ======///


                // ===== Loss NAME ===== ///

                //string tolatloss = string.Empty;
                //if (dtTotalloss.Rows.Count > 0)
                //{
                //    tolatloss = dtTotalloss.Rows[0].ItemArray[0].ToString();
                //}
                //lbMachine.Text = string.Format("Loss By Machine : {0} Hr of {1} to {2}", tolatloss, dateTimePicker1.Value.ToString("dd-MMM-yy"), dateTimePicker2.Value.ToString("dd-MMM-yy"));

            }
        }


        #region initial dataGridView
        private void InitialDataGridView1()
        {
            this.dataGridView1.ColumnCount = 2;
            this.dataGridView1.Columns[0].Name = "Code";
            this.dataGridView1.Columns[0].Width = 100;
            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].Name = "Decription";
            this.dataGridView1.Columns[1].Width = 600;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.RowHeadersWidth = 15;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowTemplate.Height = 15;

        }

        private void InitialDataGridView2()
        {
            this.dataGridView2.ColumnCount = 2;
            this.dataGridView2.Columns[0].Name = "Code";
            this.dataGridView2.Columns[0].Width = 50;
            this.dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[1].Name = "Decription";
            this.dataGridView2.Columns[1].Width = 600;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView2.RowHeadersWidth = 15;
            this.dataGridView2.DefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridView2.RowHeadersWidth = 4;
            this.dataGridView2.RowTemplate.Height = 15;

        }



        #endregion

    }
}
