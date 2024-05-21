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
using KPI.Parameter;
using System.Windows.Forms.DataVisualization.Charting;

namespace KPI.ProdForm
{
    public partial class ExculsionLossMonitorForm : Form
    {
        public DataTable copyDtSixLoss = new DataTable();
        public Color[] color = new Color[60];
        internal Dictionary<string, double> lossDic = new Dictionary<string, double>();
        internal Dictionary<string, double> excDic = new Dictionary<string, double>();
        public ExculsionLossMonitorForm()
        {
            InitializeComponent();
        }


        private void ButtonAnalysis_Click_1(object sender, EventArgs e)
        {
            LossAndExclusionTimeAnalysis();
        }

        private void ExculsionLossMonitorForm_Load(object sender, EventArgs e)
        {
            InitialComponent.DateTimePicker(dateTimePicker1);
            LossAndExclusionTimeAnalysis();
        }



        // /////////////////////////////// OPERATION LOOP //////////////////////////////////////////

        #region Operation Loop 


        private void LossAndExclusionTimeAnalysis()
        {
            string start = dateTimePicker1.Value.ToString("yyyy-MM-dd"); 
            string stop = dateTimePicker2.Value.ToString("yyyy-MM-dd"); 
            string divplant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SSS("LossExclusionMonitorExc", "@Startdate", start, "@Stopdate", stop, "@sectiondivplant", divplant);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dtLoss = ds.Tables[0];
                DataTable dtLossName = ds.Tables[1];
                DataTable dtExculsion = ds.Tables[2];
                DataTable dtExculsionName = ds.Tables[3];
                DataTable dtSixLoss = ds.Tables[4];

                Charts.ColumnSingleColor(dtLoss, chartLoss, Color.FromArgb(149, 223, 255));
                ChartLegent.Legend_DTSingleColor(tableLayoutPanel1, 1, "tableLayoutPanel1", "sixChartloss", Color.FromArgb(149, 223, 255), dtLossName);

                Charts.ColumnSingleColor(dtExculsion, chartExecu, Color.FromArgb(255, 197, 198));
                ChartLegent.Legend_DTSingleColor(tableLayoutPanel5, 1, "tableLayoutPanel5", "sixChartloss", Color.FromArgb(255, 197, 198), dtExculsionName);

                Charts.ColumnSingleColor(dtSixLoss, chartSix, Color.FromArgb(170, 170, 255));
            }
        }

        #endregion

       
    }
}
