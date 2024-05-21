using KPI.Class;
using KPI.DataContain;
using KPI.Models;
using KPI.Parameter;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KPI.ProdForm
{
    public partial class ProductionProgressForm : Form
    {
        public ProductionProgressForm()
        {
            InitializeComponent();
            ComboboxInitial();
        }

        private void ProductionProgressForm_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach (KeyValuePair<string, string> item in Dict.SectionCodeName)
            {
                if (item.Key == User.SectionCode)
                {
                    //comboBox5.SelectedIndex = i;
                    break;
                }
                i += 1;
            }
            Roles();
            ProgressResult();
        }


        #region Event : Tab 1

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }



        private void BtnExcelForm_Click(object sender, EventArgs e)
        {
            ExportExcelForm();
        }



        private void BtnCAL_Click(object sender, EventArgs e)
        {
            ProductionProgressCaluculate();
            ProgressResult();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ProgressResult();
        }

        #endregion


        #region initial 

        private void Roles()
        {
            BtnExportExcel.Enabled = false;
            BtnExcelForm.Enabled = false;
            if (User.Role == Models.Roles.Invalid) // invalid
            {

            }
            else if (User.Role == Models.Roles.General) // General
            {

            }
            else if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager)  // production
            {
                BtnExportExcel.Enabled = true;
                BtnExcelForm.Enabled = true;
            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {
                BtnExportExcel.Enabled = true;
                BtnExcelForm.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Min) // Admin-mini
            {
                BtnExportExcel.Enabled = true;
                BtnExcelForm.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnExportExcel.Enabled = true;
                BtnExcelForm.Enabled = true;
            }
        }


        private void ComboboxInitial()
        {
            int m = DateTime.Now.Month;
            CmbYear.Text = DateTime.Now.ToString("yyyy");
            CmbMonth.SelectedIndex = m - 1;
        }

        private void MakingHeader()
        {
            DgvProgress.Rows.Clear();
            var header = LegendPlate.ProgressHeader();
            for (int h = 0; h < 18 - 1; h++)
            {
                string[] row = new string[30];
                DgvProgress.Rows.Add((h + 1).ToString(), row);
            }
            for (int i = 0; i < header.Count; i++)
            {
                DgvProgress.Rows[i].Cells[1].Value = header[i];

            }

        }


        private void DgvProgressInit(int dayInMonth)
        {

            int column = dayInMonth + 4;
            string[] header = new string[column];
            int[] width = new int[column];
            DataGridViewContentAlignment[] alignment = new DataGridViewContentAlignment[column];
            header[0] = "No";
            header[1] = "ITEM NAME";
            width[0] = 30;
            width[1] = 230;
            alignment[0] = DataGridViewContentAlignment.MiddleCenter;
            alignment[1] = DataGridViewContentAlignment.MiddleLeft;
            for (int i = 0; i < dayInMonth; i++)
            {
                header[i + 2] = $"{i + 1}";
                width[i + 2] = 60;
                alignment[i + 2] = DataGridViewContentAlignment.MiddleCenter;
            }
            header[column - 2] = "";
            header[column - 1] = "Month";
            alignment[column - 1] = DataGridViewContentAlignment.MiddleCenter;
            width[column - 2] = 5;
            width[column - 1] = 50;
            DataGridViewSetup.Norm3(DgvProgress, header, width, alignment);


        }

        private void DataGridViewInitial1()
        {
            int year = Convert.ToInt32(CmbYear.Text);
            int month = CmbMonth.SelectedIndex + 1;
            int dayInmonth = DateTime.DaysInMonth(year, month);
            int column = dayInmonth + 4;
            string[] header = new string[column];
            int[] width = new int[column];
            DataGridViewContentAlignment[] alignment = new DataGridViewContentAlignment[column];
            header[0] = "No";
            header[1] = "ITEM NAME";
            width[0] = 30;
            width[1] = 230;
            alignment[0] = DataGridViewContentAlignment.MiddleCenter;
            alignment[1] = DataGridViewContentAlignment.MiddleLeft;
            for (int i = 0; i < dayInmonth; i++)
            {
                header[i + 2] = $"{i + 1}";
                width[i + 2] = 60;
                alignment[i + 2] = DataGridViewContentAlignment.MiddleCenter;
            }
            header[column - 2] = "";
            header[column - 1] = "Month";
            alignment[column - 1] = DataGridViewContentAlignment.MiddleCenter;
            width[column - 2] = 5;
            width[column - 1] = 50;
            DataGridViewSetup.Norm3(DgvProgress, header, width, alignment);


        }




        #endregion

        #region Operation loop

        private void ExportExcel()
        {
            string yearmonth = $"{CmbYear.Text}{CmbMonth.SelectedIndex + 1:00}";
            ExportExcel exp = new ExportExcel();
            DataGridViewToExcelResult result = exp.FileName1("Progress", User.SectionCode, yearmonth);
            if (result.Status)
            {
                exp.ProgressExcel(DgvProgress, result.FileName);
            }

        }

        private void ExportExcelForm()
        {
            if (DgvProgress.Rows.Count > 0)
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
                    InsertExcel(fileName, DgvProgress);
                }

            }
        }

        private void ProgressResult()
        {
            Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(62, 168, 247), Color.Red, Color.Blue };


            DataGridViewInitial1();
            DgvProgress.Rows.Clear();
            MakingHeader();
            DataGridViewCellStyle style = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 197, 197),
                ForeColor = Color.Black
            };
            //string[,] ChartSource = new string[32, 2];
            Int32.TryParse(CmbYear.Text, out int year);
            year = year == 0 ? DateTime.Now.Year : year;


            int month = CmbMonth.SelectedIndex + 1;
            string startingDate = (new DateTime(year, month, 1)).ToString("yyyy-MM-dd");
            int dayInmonth = (int)DateTime.DaysInMonth(year, month);
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SSS("ProdProgressDisplay", "@StringDate", startingDate, "@LoopRun", dayInmonth.ToString(), "@SectionCode", User.SectionCode);
            int rows = 17;
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];
                DataTable dt3 = ds.Tables[2];
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt1.Rows[i].ItemArray[0]) == "0")  // do not Working day
                            for (int k = 0; k < rows; k++)
                                DgvProgress.Rows[k].Cells[2 + i].Style = style;
                    }

                }
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        for (int r = 0; r < 17; r++)
                        {
                            if (r < 4)
                            {
                                DgvProgress.Rows[r].Cells[2 + i].Value = DataFormat.Comma(dt2.Rows[i].ItemArray[r].ToString());
                            }
                            else
                            {
                                DgvProgress.Rows[r].Cells[2 + i].Value = dt2.Rows[i].ItemArray[r];
                            }

                        }
                    }
                }

                if (dt3.Rows.Count > 0)
                {
                    for (int r = 0; r < 17; r++)
                    {
                        if (r < 4)
                        {
                            DgvProgress.Rows[r].Cells[dayInmonth + 3].Value = DataFormat.Comma(dt3.Rows[0].ItemArray[r].ToString());
                        }
                        else
                        {
                            DgvProgress.Rows[r].Cells[dayInmonth + 3].Value = dt3.Rows[0].ItemArray[r];
                        }
                    }
                }

                Charts.LineProgress(dt2, chartProg, color);
                ChartLegent.Legend_DTMultiColor(tableLayoutPanel4, 1, "tableLayoutPanel4", "lbchartLoss", color, LegendPlate.GetProgressName());

            }

        }

        private void ProductionProgressCaluculate()
        {
            int year = Convert.ToInt32(CmbYear.Text);
            int month = CmbMonth.SelectedIndex + 1;
            DateTime dt = new DateTime(year, month, 1);
            int dayInmonth = (int)DateTime.DaysInMonth(year, month);


            LossAnalysis loss = new LossAnalysis
            {
                Startdate = dt,
                Amount = dayInmonth,
                SectionCode = User.SectionCode,
                DivPlant = string.Format("{0}{1}", User.Division, User.Plant)
            };
            loss.LossAnalysisExc();


            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SIS("ProdProgressResult", "@StringDate", dt.ToString("yyyy-MM-dd"), "@LoopRun", dayInmonth, "@SectionCode", User.SectionCode);
            if (sqlstatus == false)
            {
                MessageBox.Show("Error");

            }

        }

        private void InsertExcel(string fileName, DataGridView dataGridView1)
        {
            FileInfo excelFile = new FileInfo(fileName);
            ExcelPackage excel = new ExcelPackage(excelFile);
            //int lastrow = dataGridView1.ColumnCount-1;
            string[] section = User.SectionCode.Split('-');
            int month = CmbMonth.SelectedIndex + 1;
            int year = Convert.ToInt32(CmbYear.Text);
            try
            {
                int row = 0;
                var sheet1 = excel.Workbook.Worksheets["1"];
                for (int i = 6; i < 200; i++)
                {
                    if (section[0] == Convert.ToString(sheet1.Cells[i, 1].Value))
                    {
                        row = i;
                        break;
                    }
                    if (i == 199)
                    {
                        string msg2 = string.Format("Not Found the Section Name {0} in Column A.\n Please confirm destination your file again.", section);
                        MessageBox.Show(msg2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        goto finish;
                    }
                }
                int dayInMonth = DateTime.DaysInMonth(year, month);
                for (int i = 0; i < dayInMonth; i++)
                {
                    string sheetName = (i + 1).ToString();
                    var worksheet = excel.Workbook.Worksheets[sheetName];
                    worksheet.Cells[row, 8].Value = Convert.ToInt32(dataGridView1.Rows[1].Cells[2 + i].Value);
                    worksheet.Cells[row, 9].Value = Convert.ToDouble(dataGridView1.Rows[10].Cells[2 + i].Value);
                    worksheet.Cells[row, 12].Value = Convert.ToDouble(dataGridView1.Rows[6].Cells[2 + i].Value);
                    worksheet.Cells[row, 14].Value = Convert.ToDouble(dataGridView1.Rows[7].Cells[2 + i].Value);
                }


                excel.SaveAs(excelFile);
                string msg = string.Format("Already insert to Excel file as \n {0} ", fileName);
                MessageBox.Show(msg, "Insert data to Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //******************************************************************************
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
                return;

            }
        finish: { }



        }



        #endregion




        private void BtnNewCal_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(CmbYear.Text);
            int month = CmbMonth.SelectedIndex + 1;
            DateTime dt = new DateTime(year, month, 1);
            CalculateProductionProgress(dt);
        }

        private void BtnRef_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(CmbYear.Text);
            int month = CmbMonth.SelectedIndex + 1;
            DateTime startDate = new DateTime(year, month, 1);

            var newProgress = new List<PG_Production>();
            Update(newProgress, startDate);
        }

        private void CalculateProductionProgress(DateTime dt)
        {
            int yy = dt.Year;
            int mm = dt.Month;
            int dayInMonth = DateTime.DaysInMonth(yy, mm);
            DateTime startDate = new DateTime(yy, mm, 1, 0, 0, 0);
            DateTime stopDate = startDate.AddDays(dayInMonth - 1);
            string section = User.SectionCode;

            try
            {


                using (var db = new ProductionEntities11())
                {
                    var postPlan = db.Pg_PostPlan.Where(s => s.sectionCode == section)
                        .Where(r => r.registDate >= startDate && r.registDate <= stopDate);
                    var manHour = db.Pg_MH.Where(s => s.sectionCode == section)
                       .Where(r => r.registDate >= startDate && r.registDate <= stopDate);
                    var stdManHour = db.Pg_STDMH.Where(s => s.sectionCode == section)
                       .Where(r => r.registDate >= startDate && r.registDate <= stopDate);

                    var stdRatio = db.Prod_StdYearlyTable.Where(s => s.sectionCode == section)
                       .Where(r => r.registYear == yy.ToString("0000"))
                       .Where(r => r.registMonth == mm.ToString("00"));

                    if (postPlan == null || manHour == null || stdManHour == null || stdRatio == null)
                    {
                        MessageBox.Show("Production plan, Manpower registration,stdRatio ,Production today Approve is not finished", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var postPlan1 = postPlan.ToList();
                    var manHour1 = manHour.ToList();
                    var stdGroup = stdManHour
                        .GroupBy(s => s.registDate).Select(x => new PgSTDMH
                        {
                            registDate = x.Key,
                            STD_MH = x.Sum(a => a.STD_MH),
                            actucalQty = x.Sum(a => a.actucalQty)
                        }).ToList();



                    var workToday = new List<ProdTodayWork>();
                    var prodworkTodayTable = db.Prod_TodayWorkTable.Where(s => s.sectionCode == section)
                        .Where(r => r.registDate >= startDate && r.registDate <= stopDate)
                        .GroupBy(r => r.registDate);
                    if (prodworkTodayTable != null)
                    {
                        workToday = prodworkTodayTable
                           .Select(x => new ProdTodayWork
                           {
                               Registdate = x.Key,
                               WorkHr = x.Sum(a => a.workHour) == 0 ? 0 : 1,
                           }).ToList();
                    }

                    string year = yy.ToString("0000");
                    string month = mm.ToString("00");

                    double stdratio = db.Prod_StdYearlyTable.Where(s => s.sectionCode == section)
                        .Where(r => r.registYear == year)
                        .SingleOrDefault(r => r.registMonth == month).standardRatio;


                    var pgLoss = new List<Pg_Loss>();
                    var progressLosssExist = db.Pg_Loss.Where(s => s.sectionCode == section)
                        .Where(r => r.registDate >= startDate && r.registDate <= stopDate).Any();
                    if (progressLosssExist == false)
                    {
                        pgLoss = ProductionProgress.InitPg_Loss(startDate, section);
                    }
                    else
                    {
                        pgLoss = db.Pg_Loss.Where(s => s.sectionCode == section)
                        .Where(r => r.registDate >= startDate && r.registDate <= stopDate).ToList();
                    }
                   
                    var resultJoin = (from p in postPlan1
                                      join m in manHour1 on new { p.sectionCode, p.registDate } equals new { m.sectionCode, m.registDate }
                                      join s in stdGroup on p.registDate equals s.registDate
                                      join w in workToday on p.registDate equals w.Registdate
                                      join l in pgLoss on p.registDate equals l.registDate
                                      select new PG_Production
                                      {
                                          sectionCode = p.sectionCode,
                                          registDate = p.registDate,
                                          postPlancAcc = p.postPlanAcc,
                                          actualProduction = s.actucalQty,
                                    
                                          MHNomal = m.MHNormal,
                                          MHOT = m.MHOT,
                                          totalMH = m.TotalMH,
                                          exclusionTime = m.exclusionHr,
                                          grossMH = m.GMH,
                                        
                                          lossTime = l.lossHr,
                                          STD_MH = s.STD_MH * stdratio,
                                         
                                          MH_R_Acc = 80,
                                         
                                          workingDay = w.WorkHr,

                                      }).ToList();

                    var arrageData = resultJoin.OrderBy(r => r.registDate).ToList();

                    double actualProductionAcc = 0;
                    double grossMHAcc = 0;
                    double STD_MHAcc = 0;


                    var newProgress = new List<PG_Production>();
                    foreach (PG_Production i in resultJoin)
                    {
                        actualProductionAcc += i.actualProduction;
                        grossMHAcc += i.grossMH;
                        STD_MHAcc += i.STD_MH;
                        var calculate = new PG_Production()
                        {
                            sectionCode = i.sectionCode,
                            registDate = i.registDate,
                            workingDay = i.workingDay,
                            postPlancAcc = i.postPlancAcc,
                            actualProduction = i.actualProduction,
                            actualProductionAcc = actualProductionAcc,
                            ProductionBalance = actualProductionAcc - i.postPlancAcc  ,

                            MHNomal = Math.Round(i.MHNomal, 2, MidpointRounding.AwayFromZero),
                            MHOT = Math.Round(i.MHOT, 2, MidpointRounding.AwayFromZero),
                            totalMH = Math.Round(i.totalMH, 2, MidpointRounding.AwayFromZero),
                            exclusionTime = Math.Round(i.exclusionTime, 2, MidpointRounding.AwayFromZero),
                            grossMH = Math.Round(i.grossMH, 2, MidpointRounding.AwayFromZero),
                            grossMHAcc = Math.Round(grossMHAcc, 2, MidpointRounding.AwayFromZero),

                            lossTime = Math.Round(i.lossTime, 2, MidpointRounding.AwayFromZero),
                         
                            STD_MH = Math.Round(i.STD_MH, 2, MidpointRounding.AwayFromZero),
                            STD_MHAcc = Math.Round(STD_MHAcc, 2, MidpointRounding.AwayFromZero),
                            MH_R_MGR = i.STD_MH == 0 ? 0 : Math.Round(i.totalMH / i.STD_MH * 100, 2, MidpointRounding.AwayFromZero),
                            MH_R_TL = i.STD_MH == 0 ? 0 : Math.Round(i.grossMH / i.STD_MH * 100, 2, MidpointRounding.AwayFromZero),
                            MH_R_TL_Acc = STD_MHAcc == 0 ? 0 : Math.Round(grossMHAcc / STD_MHAcc * 100, 2, MidpointRounding.AwayFromZero),
                            MH_R_Acc = i.MH_R_Acc,
                            judge = grossMHAcc / STD_MHAcc * 100 < i.MH_R_Acc ? "O" : "X",


                        };
                        newProgress.Add(calculate);
                    }

                    var deletedb = db.PG_Production
                        .Where(s => s.sectionCode == section)
                        .Where(r => r.registDate >= startDate && r.registDate <= stopDate);
                    if (deletedb != null)
                    {
                        db.PG_Production.RemoveRange(deletedb);
                        db.SaveChanges();
                    }
                    db.PG_Production.AddRange(newProgress);
                    db.SaveChanges();

                    if (newProgress.Count == 0)
                    {
                        MessageBox.Show("NO data, You shall register production today, manpower registration before calculation", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }

                    Update(newProgress, startDate);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("DataBase connection failuer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Update(List<PG_Production> progress, DateTime startdate)
        {
            Color[] color = { Color.FromArgb(255, 127, 127), Color.FromArgb(62, 168, 247), Color.Red, Color.Blue };

            int year = startdate.Year;
            int month = startdate.Month;
            string startingDate = (new DateTime(year, month, 1)).ToString("yyyy-MM-dd");
            int dayInmonth = (int)DateTime.DaysInMonth(year, month);
            DateTime stopdate = startdate.AddDays(dayInmonth - 1);

            DgvProgressInit(dayInmonth);

            DgvProgress.Rows.Clear();
          


            MakingHeader();


            DataGridViewCellStyle style0 = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 235, 235),
                ForeColor = Color.Black
            };
            DataGridViewCellStyle style1 = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(239, 253, 239),
                ForeColor = Color.Black
            };

            try
            {
                if (progress.Count == 0)
                {
                    using (var db = new ProductionEntities11())
                    {
                        progress = db.PG_Production.Where(s => s.sectionCode == User.SectionCode)
                            .Where(r => r.registDate >= startdate && r.registDate <= stopdate).ToList();

                        if (progress.Count == 0)
                        {
                            MessageBox.Show("NO Calculated data, You shall calculate progress before show on screen", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }

                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("DataBase connection failuer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (PG_Production item in progress.OrderBy(r => r.registDate))
            {
                int day = item.registDate.Day + 1;

                DgvProgress.Rows[0].Cells[day].Value = String.Format("{0:#,##0}", item.postPlancAcc);
                DgvProgress.Rows[1].Cells[day].Value = String.Format("{0:#,##0}", item.actualProduction);
                DgvProgress.Rows[2].Cells[day].Value = String.Format("{0:#,##0}", item.actualProductionAcc);
                DgvProgress.Rows[3].Cells[day].Value = String.Format("{0:#,##0}", item.ProductionBalance);


                DgvProgress.Rows[4].Cells[day].Value =item.MHNomal;
                DgvProgress.Rows[5].Cells[day].Value = item.MHOT;
                DgvProgress.Rows[6].Cells[day].Value = item.totalMH;

                DgvProgress.Rows[7].Cells[day].Value =item.exclusionTime;
                DgvProgress.Rows[8].Cells[day].Value = item.grossMH;
                DgvProgress.Rows[9].Cells[day].Value =item.grossMHAcc;

                DgvProgress.Rows[10].Cells[day].Value = item.lossTime;

                DgvProgress.Rows[11].Cells[day].Value = item.STD_MH;
                DgvProgress.Rows[12].Cells[day].Value = item.STD_MHAcc;

                DgvProgress.Rows[13].Cells[day].Value = item.MH_R_MGR;
                DgvProgress.Rows[14].Cells[day].Value =item.MH_R_TL;
                DgvProgress.Rows[15].Cells[day].Value = item.MH_R_TL_Acc;



                DgvProgress.Rows[16].Cells[day].Value = item.judge;

                for (int i = 0; i < 17; i++)
                {
                    DgvProgress.Rows[i].Cells[day].Style = item.workingDay == 0 ? style0 : style1;
                }
            }


            PG_Production itemF = progress.OrderByDescending(r => r.registDate).FirstOrDefault();
            if (itemF != null)
            {
                int dayF = dayInmonth + 3;
                DgvProgress.Rows[0].Cells[dayF].Value = String.Format("{0:#,##0}", itemF.postPlancAcc);
                DgvProgress.Rows[1].Cells[dayF].Value = String.Format("{0:#,##0}", itemF.actualProduction);
                DgvProgress.Rows[2].Cells[dayF].Value = String.Format("{0:#,##0}", itemF.actualProductionAcc);
                DgvProgress.Rows[3].Cells[dayF].Value = String.Format("{0:#,##0}", itemF.ProductionBalance);

                DgvProgress.Rows[4].Cells[dayF].Value = itemF.MHNomal;
                DgvProgress.Rows[5].Cells[dayF].Value = itemF.MHOT;
                DgvProgress.Rows[6].Cells[dayF].Value = itemF.totalMH;

                DgvProgress.Rows[7].Cells[dayF].Value = itemF.exclusionTime;
                DgvProgress.Rows[8].Cells[dayF].Value = itemF.grossMH;
                DgvProgress.Rows[9].Cells[dayF].Value = itemF.grossMHAcc;

                DgvProgress.Rows[10].Cells[dayF].Value = itemF.lossTime;


                DgvProgress.Rows[11].Cells[dayF].Value = itemF.STD_MH;
                DgvProgress.Rows[12].Cells[dayF].Value = itemF.STD_MHAcc;

                DgvProgress.Rows[13].Cells[dayF].Value = itemF.MH_R_MGR;
                DgvProgress.Rows[14].Cells[dayF].Value = itemF.MH_R_TL;
                DgvProgress.Rows[15].Cells[dayF].Value = itemF.MH_R_TL_Acc;

            }
                Charts.ProductionProgress(progress, chartProg, color);
                ChartLegent.Legend_DTMultiColor(tableLayoutPanel4, 1, "tableLayoutPanel4", "lbchartLoss", color, LegendPlate.GetProgressName());
            
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            string mess = "Step 1: Planing in Monthly production plan \n\n Step 2: Registration Manpower \n\n Step 3: Registration Production Today \n\n Step 4: Calculate Progress \n\n  if If in doubt, contact k.anuchit 080-916-5195";
            MessageBox.Show(mess, "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
