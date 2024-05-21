using KPI.Class;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KPI.ProdForm
{
    public partial class ManPowerReportForm : Form
    {
        public event EventHandler ManPowerReportFormClosed;

        private List<EmpMPsummaryReport> empMPovertime;
        private List<EmpNameList> empNameList;
        private List<SummaryMH> summaryManHour;
        private DateTime startDate, stopDate,selectedDate;
        public ManPowerReportForm(List<EmpMPsummaryReport> empMPovertime, List<EmpNameList> empNameList,
            List<SummaryMH> summaryManHour, DateTime startDate, DateTime stopDate, DateTime selectedDate)
        {
            InitializeComponent();
            this.empMPovertime = empMPovertime;
            this.empNameList = empNameList;
            this.summaryManHour = summaryManHour;
            this.startDate = startDate;
            this.stopDate = stopDate;
            this.selectedDate = selectedDate;
        }
        public ManPowerReportForm()
        {

        }

        private void ManPowerReportForm_Load(object sender, EventArgs e)
        {
            Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen,
                Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            Charts.ChartManPowerOTSummary(empMPovertime.OrderBy(x => x.UserId).ToList(), ChatOT, color, startDate, stopDate);

            var empNameList1 = empNameList.OrderBy(i => i.UserId).ToList();
            ChartLegent.Legend_NewListMultiColor2(tpanelChartPartNumber, 1, "tpanelChartPartNumber", "lbcharSixtLoss", color, empNameList1);
            DateSelect1.Value = startDate;
            DateSelect2.Value = stopDate;

            DgvOT.Rows.Clear();
            InitialDgv();
            foreach (var item in summaryManHour)
            {
                DgvOT.Rows.Add(item.Content, item.WorkingHr, item.OvertimeHr, item.TotalHr);
            }

            DgvOTList.Rows.Clear();
            InitialDgv1();
            var summ = new EmpMPsummaryReport();
            foreach (EmpMPsummaryReport item in empMPovertime.OrderBy(x => x.UserId).ToList())
            {
                DgvOTList.Rows.Add(item.UserId, item.Fullname, item.MWorkingTime, item.MOverTime, item.MTotalTime);
                summ.MWorkingTime += item.MWorkingTime;
                summ.MOverTime += item.MOverTime;
                summ.MTotalTime += item.MTotalTime;
            }
            DgvOTList.Rows.Add("", "Total", summ.MWorkingTime, summ.MOverTime, summ.MTotalTime);
            label3.Text = String.Format("Summary between  {0}  to  {1} ", startDate.ToString("d MMMM yyyy"), stopDate.ToString("d MMMM yyyy"));
            label1.Text = String.Format("Summary the {0} ", selectedDate.ToString("d MMMM yyyy"));
        }


        private void InitialDgv()
        {
            string[] header = new string[] { "Shift", "  Working (Hr)", "  Over Time (Hr)", "Total ManHour (Hr)" };
            int[] width = new int[] { 150, 100, 100, 100 };
            DataGridViewSetup.Norm4(DgvOT, header, width, 9);
        }

        private void InitialDgv1()
        {
            string[] header = new string[] { "Emp id", "Full name", "  Working (Hr)", "  Over Time (Hr)", "Total (Hr)" };
            int[] width = new int[] { 100, 170, 100, 100, 100 };
            DataGridViewSetup.Norm5(DgvOTList, header, width, 9);
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManPowerReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ManPowerReportFormClosed?.Invoke(this, EventArgs.Empty);
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            ExportExcelReport();
        }

        private void BtnCal_Click(object sender, EventArgs e)
        {
            Calculate();

        }
        private void Calculate()
        {
            Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen,
                Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            DateTime date1 = DateSelect1.Value, date2 = DateSelect2.Value;
            DateTime startdate = new DateTime(date1.Year, date1.Month, date1.Day);
            DateTime stopdate = new DateTime(date2.Year, date2.Month, date2.Day);
            using (var db = new ProductionEntities11())
            {

                var datalist = db.Emp_ManPowerRegistedTable
                                   .Where(r => r.registDate >= startdate && r.registDate <= stopdate)
                                   .Where(s => s.sectionCode == User.SectionCode).ToList();

                var summary = ManPowerRegister.ManPowerSummary(datalist, startdate, stopdate);

                var empMPovertime = (from o in summary
                                 join e in db.Emp_ManPowersTable
                              on o.UserId equals e.userID
                                 select new EmpMPsummaryReport
                                 {
                                     UserId = o.UserId,
                                     Fullname = e.fullName,
                                     MWorkingTime = o.MWorkingTime,
                                     MOverTime = o.MOverTime,
                                     MTotalTime = o.MTotalTime
                                 }).ToList();

               var nameList = (from o in empMPovertime
                            join e in db.Emp_ManPowersTable
                            on o.UserId equals e.userID
                            select new EmpNameList
                            {
                                UserId = o.UserId,
                                Fullname = e.fullName
                            }).ToList();

                Charts.ChartManPowerOTSummary(empMPovertime.OrderBy(x => x.UserId).ToList(), ChatOT, color, startdate, stopdate);

                var empNameList1 = empNameList.OrderBy(i => i.UserId).ToList();
                ChartLegent.Legend_NewListMultiColor2(tpanelChartPartNumber, 1, "tpanelChartPartNumber", "lbcharSixtLoss", color, nameList);

               
                DgvOTList.Rows.Clear();
                InitialDgv1();
                var summ = new EmpMPsummaryReport();
                foreach (EmpMPsummaryReport item in empMPovertime.OrderBy(x => x.UserId).ToList())
                {
                    DgvOTList.Rows.Add(item.UserId, item.Fullname, item.MWorkingTime, item.MOverTime, item.MTotalTime);
                    summ.MWorkingTime += item.MWorkingTime;
                    summ.MOverTime += item.MOverTime;
                    summ.MTotalTime += item.MTotalTime;
                }
                DgvOTList.Rows.Add("", "Total", summ.MWorkingTime, summ.MOverTime, summ.MTotalTime);
                label3.Text = String.Format("Summary between  {0}  to  {1} ", startdate.ToString("d MMMM yyyy"), stopdate.ToString("d MMMM yyyy"));



            }



        }

        private void ExportExcelReport()
        {
            string dateStr = string.Format($"{DateSelect1.Value:d_MMMM_yyyy}_TO_{DateSelect2.Value:d_MMMM_yyyy}");
            ExportExcel exp = new ExportExcel();
            DataGridViewToExcelResult result = exp.FileName3("ManHour", User.SectionCode, dateStr);
            if (result.Status)
            {
                exp.ManHourExcel(DgvOTList, result.FileName);
            }

        }
    }
}
