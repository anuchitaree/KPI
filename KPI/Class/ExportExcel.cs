using KPI.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace KPI.Class
{
    public class ExportExcel
    {
        public DataGridViewToExcelResult FileName1(string sub, string section, string yearmonth)
        {
            var res = new DataGridViewToExcelResult
            {
                Status = false,
                FileName = ""
            };
            FolderBrowserDialog folderDlg = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\",
                ShowNewFolderButton = true
            };
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                string timenow = DateTime.Now.ToString("HHmmss");
                res.FileName = string.Format("{0}\\{1}{2}_{3}-{4}.xlsx", folderDlg.SelectedPath, sub, section, yearmonth, timenow);
                res.Status = true;
            }
            return res;

        }


        public DataGridViewToExcelResult FileName3(string sub, string section, string yearmonth)
        {
            var res = new DataGridViewToExcelResult
            {
                Status = false,
                FileName = ""
            };
            FolderBrowserDialog folderDlg = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\",
                ShowNewFolderButton = true
            };
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                res.FileName = string.Format("{0}\\{1}{2}_{3}.xlsx", folderDlg.SelectedPath, sub, section, yearmonth);
                res.Status = true;
            }
            return res;

        }

        public DataGridViewToExcelResult FileName2(string sub, string section, string yearmonthday)
        {
            var res = new DataGridViewToExcelResult
            {
                Status = false,
                FileName = ""
            };
            FolderBrowserDialog folderDlg = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\",
                ShowNewFolderButton = true
            };
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                string timenow = DateTime.Now.ToString("HHmmss");
                res.FileName = string.Format("{0}\\{1}{2}_{3}-{4}.xlsx", folderDlg.SelectedPath, sub, section, yearmonthday, timenow);
                res.Status = true;
            }
            return res;

        }

        public DataGridViewToExcelResult FileNamePpas(string sub, string section, string datebeteen)
        {
            var res = new DataGridViewToExcelResult
            {
                Status = false,
                FileName = ""
            };
            FolderBrowserDialog folderDlg = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\",
                ShowNewFolderButton = true
            };
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                //string timenow = DateTime.Now.ToString("HHmmss");
                res.FileName = string.Format("{0}\\{1}{2}_{3}.xlsx", folderDlg.SelectedPath, sub, section, datebeteen);
                res.Status = true;
            }
            return res;

        }

        public DataGridViewToExcelResult FileNamePpasCSV(string sub, string section, string datebeteen)
        {
            var res = new DataGridViewToExcelResult
            {
                Status = false,
                FileName = ""
            };
            FolderBrowserDialog folderDlg = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\",
                ShowNewFolderButton = true
            };
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                //string timenow = DateTime.Now.ToString("HHmmss");
                res.FileName = string.Format("{0}\\{1}{2}_{3}.csv", folderDlg.SelectedPath, sub, section, datebeteen);
                res.Status = true;
            }
            return res;

        }

        public void Excel(DataGridView dgvday, DataGridView dgvnight, string fileName, Label[] datename, TextBox[] day)
        {
            FileInfo excelFile = new FileInfo(fileName);
            ExcelPackage excel = new ExcelPackage(excelFile);
            string[] dateName = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            try
            {
                int row = dgvday.RowCount;
                int col = dgvday.ColumnCount;
                excel.Workbook.Worksheets.Add("Day");
                var worksheet1 = excel.Workbook.Worksheets["Day"];
                for (int r = 0; r < row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {
                        worksheet1.Cells[3 + r, c + 1].Value = dgvday.Rows[r].Cells[c].Value;
                    }
                }
                // header
                worksheet1.Cells[2, 1].Value = "Date";
                for (int i = 0; i < 7; i++)
                {
                    worksheet1.Cells[1, 6 + i * 4].Value = day[i].Text;
                    worksheet1.Cells[2, 6 + i * 4].Value = dateName[i];
                }


                row = dgvnight.RowCount;
                col = dgvnight.ColumnCount;
                excel.Workbook.Worksheets.Add("Night");


                var worksheet2 = excel.Workbook.Worksheets["Night"];
                for (int r = 0; r < row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {
                        worksheet2.Cells[3 + r, c + 1].Value = dgvnight.Rows[r].Cells[c].Value;
                    }
                }
                // header
                worksheet2.Cells[2, 1].Value = "Date";
                for (int i = 0; i < 7; i++)
                {
                    worksheet2.Cells[1, 6 + i * 4].Value = day[i].Text;
                    worksheet2.Cells[2, 6 + i * 4].Value = dateName[i];
                }

                excel.SaveAs(excelFile);
                MessageBox.Show("done", "Export data to Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //******************************************************************************
            }
            catch (Exception)
            {
                MessageBox.Show("Please close your file was open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }



        public void ProductionByDayExcel(DataGridView dataGridView1, string fileName, string daynight)
        {
            FileInfo excelFile = new FileInfo(fileName);
            ExcelPackage excel = new ExcelPackage(excelFile);
            int row = dataGridView1.RowCount;
            int col = dataGridView1.ColumnCount;
            try
            {
                excel.Workbook.Worksheets.Add(daynight);
                var worksheet = excel.Workbook.Worksheets[daynight];

                for (int r = 0; r < row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {
                        if (c == 0)
                        {
                            if (dataGridView1.Rows[r].Cells[c].Value == null)
                            {

                            }
                            else if (dataGridView1.Rows[r].Cells[c].Value.ToString() != "")
                            {
                                worksheet.Cells[r + 2, c + 1].Value = Convert.ToInt32(dataGridView1.Rows[r].Cells[c].Value);
                            }
                        }

                        else if (c == 1)
                        {
                            if (dataGridView1.Rows[r].Cells[c].Value == null)
                            {

                            }
                            else
                            {
                                worksheet.Cells[r + 2, c + 1].Value = dataGridView1.Rows[r].Cells[c].Value.ToString();
                            }
                        }
                        else if (c > 2)
                        {

                            if (dataGridView1.Rows[r].Cells[c].Value == null)
                            {

                            }
                            else if (dataGridView1.Rows[r].Cells[c].Value.ToString() != "")
                            {
                                worksheet.Cells[r + 2, c + 1].Value = Convert.ToInt32(dataGridView1.Rows[r].Cells[c].Value);
                            }
                        }
                    }

                }
                // header
                worksheet.Cells[1, 1].Value = "No";
                worksheet.Cells[1, 2].Value = "PART NUMBER";
                //int dayinmonth = DateTime.DaysInMonth(Convert.ToInt32(CmbYear.Text), CmbMonth.SelectedIndex + 1);
                int dayinmonth = col - 3;
                for (int i = 0; i < dayinmonth; i++)
                {
                    worksheet.Cells[1, 3 + i].Value = 1 + i;
                }
                worksheet.Cells[1, 3 + dayinmonth].Value = "Total";


                excel.SaveAs(excelFile);
                MessageBox.Show("Export data to Excel file","Info" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                //******************************************************************************
            }
            catch (Exception)
            {
                MessageBox.Show("Please close your file was open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }



        public void ProductionByHourExcel(DataGridView dataGridViewHour, string filename, List<string> _header)
        {
            FileInfo excelFile = new FileInfo(filename);
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("sheet1");
                var workSheet = excel.Workbook.Worksheets["sheet1"];

                for (int i = 0; i < _header.Count; i++)
                {
                    workSheet.Cells[2, 1 + i].Value = _header[i];
                }
                for (int i = 0; i < dataGridViewHour.RowCount - 1; i++)
                {
                    workSheet.Cells[3 + i, 1].Value = i + 1;
                    workSheet.Cells[3 + i, 2].Value = dataGridViewHour.Rows[i + 1].Cells[1].Value;
                    for (int j = 2; j < _header.Count; j++)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(dataGridViewHour.Rows[i + 1].Cells[j].Value)))
                        {
                            workSheet.Cells[3 + i, 1 + j].Value = "";
                        }
                        else
                        {
                            workSheet.Cells[3 + i, 1 + j].Value = Convert.ToInt32(dataGridViewHour.Rows[i + 1].Cells[j].Value);
                        }

                    }

                }

                excel.SaveAs(excelFile);
            }
        }



        public void ProgressExcel(DataGridView dataGridView1, string fileName)
        {
            FileInfo excelFile = new FileInfo(fileName);
            ExcelPackage excel = new ExcelPackage(excelFile);
            int row = dataGridView1.RowCount;
            int col = dataGridView1.ColumnCount;
            try
            {
                excel.Workbook.Worksheets.Add("sheet1");
                var worksheet = excel.Workbook.Worksheets["sheet1"];

                for (int r = 0; r < row; r++)
                {
                    worksheet.Cells[r + 2, 2].Value = dataGridView1.Rows[r].Cells[1].Value;
                    if ((row - 1) == r)
                    {
                        for (int c = 2; c < col; c++)
                        {
                            if ((col - 2) == c)
                            {
                                worksheet.Cells[r + 2, c + 1].Value = "";
                            }
                            else
                            {
                                worksheet.Cells[r + 2, c + 1].Value = Convert.ToString(dataGridView1.Rows[r].Cells[c].Value);
                            }
                        }
                    }
                    else
                    {
                        for (int c = 2; c < col; c++)
                        {
                            if ((col - 2) == c)
                            {
                                worksheet.Cells[r + 2, c + 1].Value = "";
                            }
                            else
                            {
                                worksheet.Cells[r + 2, c + 1].Value = Convert.ToDouble(dataGridView1.Rows[r].Cells[c].Value);
                            }
                        }
                    }

                }
                // header
                worksheet.Cells[1, 1].Value = "No";
                worksheet.Cells[1, 2].Value = "Items Name";
                int dayInMonth = col == 35 ? 31 : 30;
                for (int i = 0; i < dayInMonth; i++)
                {
                    worksheet.Cells[1, 3 + i].Value = (i + 1);
                }
                worksheet.Cells[1, col].Value = "Monthly";

                excel.SaveAs(excelFile);
                MessageBox.Show("done", "Export data to Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //******************************************************************************
            }
            catch (Exception)
            {
                MessageBox.Show("Please close your file was open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }



        public void ManHourExcel(DataGridView dataGridView1, string fileName)
        {
            FileInfo excelFile = new FileInfo(fileName);
            ExcelPackage excel = new ExcelPackage(excelFile);
            int row = dataGridView1.RowCount;
            int col = dataGridView1.ColumnCount;
            try
            {
                excel.Workbook.Worksheets.Add("sheet1");
                var worksheet = excel.Workbook.Worksheets["sheet1"];
                for (int r = 0; r < row; r++)
                {
                    worksheet.Cells[r + 2, 1].Value = dataGridView1.Rows[r].Cells[0].Value;
                    worksheet.Cells[r + 2, 2].Value = dataGridView1.Rows[r].Cells[1].Value;
                    worksheet.Cells[r + 2, 3].Value = dataGridView1.Rows[r].Cells[2].Value;
                    worksheet.Cells[r + 2, 4].Value = dataGridView1.Rows[r].Cells[3].Value;
                    worksheet.Cells[r + 2, 5].Value = dataGridView1.Rows[r].Cells[4].Value;
                }

                // header
                worksheet.Cells[1, 1].Value = "Emp Id";
                worksheet.Cells[1, 2].Value = "Full name";
                worksheet.Cells[1, 3].Value = "Working (Hr)";
                worksheet.Cells[1, 4].Value = "OverTime (Hr)";
                worksheet.Cells[1, 5].Value = "Total Time (Hr)";



                excel.SaveAs(excelFile);
                MessageBox.Show("done", "Export data to Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //******************************************************************************
            }
            catch (Exception)
            {
                MessageBox.Show("Please close your file was open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }



        public bool RadiatorCalibrationHelium(DataSet data, string filename, string sheetname, int day, int startrow, int startcol)
        {
            try
            {
                FileInfo excelFile = new FileInfo(filename);
                ExcelPackage excel = new ExcelPackage(excelFile);

                var workSheet = excel.Workbook.Worksheets[sheetname];
                DataTable dayshift = data.Tables[0];
                DataTable nightshift = data.Tables[1];
                int col = startcol + (day - 1) * 2;
                if (dayshift.Rows.Count > 0)
                {
                    int co = dayshift.Columns.Count;
                    for (int i = 0; i < co; i++)
                    {
                        workSheet.Cells[startrow + i, col].Value = Convert.ToDecimal(dayshift.Rows[0][i]);
                    }
                }
                col = startcol + 1 + (day - 1) * 2;
                if (nightshift.Rows.Count > 0)
                {
                    int co = nightshift.Columns.Count;
                    for (int i = 0; i < co; i++)
                    {
                        workSheet.Cells[startrow + i, col].Value = Convert.ToDecimal(nightshift.Rows[0][i]);
                    }
                }

                excel.SaveAs(excelFile);

                return true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        public bool ExportToCSV(string fileName, string[] date, DataGridView dgvDay, DataGridView dgvNight)
        {
            int row = 30+1;
            int col = 25+1;
            object[,] worksheet = new object[row, col];

            string[] dateName = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            string[] header = new string[] {"Time","Period","Plan100-PlanTarget","Accu100-AccuTarget",
                "Volume","Accumulate","%OA","Volume","Accumulate","%OA","Volume","Accumulate","%OA",
            "Volume","Accumulate","%OA","Volume","Accumulate","%OA","Volume","Accumulate","%OA",
            "Volume","Accumulate","%OA"};

            try
            {

                if (dgvDay.Rows.Count == 0 && dgvNight.Rows.Count == 0)
                    return false;

                worksheet[1, 1] = String.Format($"Line name : {User.SectionName}");
                int rowday = 5;
                for (int j = 0; j < 7; j++)
                {
                    worksheet[rowday - 3, j * 3 + 5] = date[j];
                    worksheet[rowday - 2, j * 3 + 5] = dateName[j];

                }
                int len = header.Length;
                for (int j = 0; j < len; j++)
                {
                    worksheet[rowday - 1, j + 1] = header[j];
                }
                if (dgvDay.Rows.Count > 0)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        worksheet[rowday + i, 1] = (dgvDay.Rows[i].Cells[0].Value);
                        worksheet[rowday + i, 2] = (dgvDay.Rows[i].Cells[1].Value);
                        worksheet[rowday + i, 3] = (dgvDay.Rows[i].Cells[2].Value);
                        worksheet[rowday + i, 4] = (dgvDay.Rows[i].Cells[3].Value);

                        worksheet[rowday + i, 5] = (dgvDay.Rows[i].Cells[5].Value);
                        worksheet[rowday + i, 6] = (dgvDay.Rows[i].Cells[6].Value);
                        worksheet[rowday + i, 7] = (dgvDay.Rows[i].Cells[7].Value);

                        worksheet[rowday + i, 8] = (dgvDay.Rows[i].Cells[9].Value);
                        worksheet[rowday + i, 9] = (dgvDay.Rows[i].Cells[10].Value);
                        worksheet[rowday + i, 10] = (dgvDay.Rows[i].Cells[11].Value);

                        worksheet[rowday + i, 11] = (dgvDay.Rows[i].Cells[13].Value);
                        worksheet[rowday + i, 12] = (dgvDay.Rows[i].Cells[14].Value);
                        worksheet[rowday + i, 13] = (dgvDay.Rows[i].Cells[15].Value);

                        worksheet[rowday + i, 14] = (dgvDay.Rows[i].Cells[17].Value);
                        worksheet[rowday + i, 15] = (dgvDay.Rows[i].Cells[18].Value);
                        worksheet[rowday + i, 16] = (dgvDay.Rows[i].Cells[19].Value);

                        worksheet[rowday + i, 17] = (dgvDay.Rows[i].Cells[21].Value);
                        worksheet[rowday + i, 18] = (dgvDay.Rows[i].Cells[22].Value);
                        worksheet[rowday + i, 19] = (dgvDay.Rows[i].Cells[23].Value);

                        worksheet[rowday + i, 20] = (dgvDay.Rows[i].Cells[25].Value);
                        worksheet[rowday + i, 21] = (dgvDay.Rows[i].Cells[26].Value);
                        worksheet[rowday + i, 22] = (dgvDay.Rows[i].Cells[27].Value);

                        worksheet[rowday + i, 23] = (dgvDay.Rows[i].Cells[29].Value);
                        worksheet[rowday + i, 24] = (dgvDay.Rows[i].Cells[30].Value);
                        worksheet[rowday + i, 25] = (dgvDay.Rows[i].Cells[31].Value);
                    }
                }
                rowday = 20;
                for (int j = 0; j < 7; j++)
                {
                    worksheet[rowday - 3, j * 3 + 5] = date[j];
                    worksheet[rowday - 2, j * 3 + 5] = dateName[j];
                }
                for (int j = 0; j < len; j++)
                {
                    worksheet[rowday - 1, j + 1] = header[j];
                }

                if (dgvNight.Rows.Count > 0)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        worksheet[rowday + i, 1] = (dgvNight.Rows[i].Cells[0].Value);
                        worksheet[rowday + i, 2] = (dgvNight.Rows[i].Cells[1].Value);
                        worksheet[rowday + i, 3] = (dgvNight.Rows[i].Cells[2].Value);
                        worksheet[rowday + i, 4] = (dgvNight.Rows[i].Cells[3].Value);

                        worksheet[rowday + i, 5] = (dgvNight.Rows[i].Cells[5].Value);
                        worksheet[rowday + i, 6] = (dgvNight.Rows[i].Cells[6].Value);
                        worksheet[rowday + i, 7] = (dgvNight.Rows[i].Cells[7].Value);

                        worksheet[rowday + i, 8] = (dgvNight.Rows[i].Cells[9].Value);
                        worksheet[rowday + i, 9] = (dgvNight.Rows[i].Cells[10].Value);
                        worksheet[rowday + i, 10] = (dgvNight.Rows[i].Cells[11].Value);

                        worksheet[rowday + i, 11] = (dgvNight.Rows[i].Cells[13].Value);
                        worksheet[rowday + i, 12] = (dgvNight.Rows[i].Cells[14].Value);
                        worksheet[rowday + i, 13] = (dgvNight.Rows[i].Cells[15].Value);

                        worksheet[rowday + i, 14] = (dgvNight.Rows[i].Cells[17].Value);
                        worksheet[rowday + i, 15] = (dgvNight.Rows[i].Cells[18].Value);
                        worksheet[rowday + i, 16] = (dgvNight.Rows[i].Cells[19].Value);

                        worksheet[rowday + i, 17] = (dgvNight.Rows[i].Cells[21].Value);
                        worksheet[rowday + i, 18] = (dgvNight.Rows[i].Cells[22].Value);
                        worksheet[rowday + i, 19] = (dgvNight.Rows[i].Cells[23].Value);

                        worksheet[rowday + i, 20] = (dgvNight.Rows[i].Cells[25].Value);
                        worksheet[rowday + i, 21] = (dgvNight.Rows[i].Cells[26].Value);
                        worksheet[rowday + i, 22] = (dgvNight.Rows[i].Cells[27].Value);

                        worksheet[rowday + i, 23] = (dgvNight.Rows[i].Cells[29].Value);
                        worksheet[rowday + i, 24] = (dgvNight.Rows[i].Cells[30].Value);
                        worksheet[rowday + i, 25] = (dgvNight.Rows[i].Cells[31].Value);
                    }

                }

                string csvdata = string.Empty;

                for (int  r = 1;  r < row;  r++)
                {
                    for (int c = 1; c < col; c++)
                    {
                        csvdata += $"{worksheet[r, c]},";
                    }
                    csvdata += "\r\n";
                }
                File.WriteAllText(fileName, csvdata);

                return true;
            }
            catch
            {

                return false;
            }

        }



        public bool ExportToMasterFile(string fileName, string[] date, DataGridView dgvDay, DataGridView dgvNight)
        {
            FileInfo excelFile = new FileInfo(fileName);
            ExcelPackage excel = new ExcelPackage(excelFile);

            string[] dateName = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            string[] header = new string[] {"Time","Period","Plan100-PlanTarget","Accu100-AccuTarget",
                "Volume","Accumulate","%OA","Volume","Accumulate","%OA","Volume","Accumulate","%OA",
            "Volume","Accumulate","%OA","Volume","Accumulate","%OA","Volume","Accumulate","%OA",
            "Volume","Accumulate","%OA"};

            try
            {

                var worksheet = excel.Workbook.Worksheets["bluePrint"];
                if (worksheet == null)
                {
                    return false;
                }

                if (dgvDay.Rows.Count == 0 && dgvNight.Rows.Count == 0)
                    return false;

                for (int row = 1; row < 31; row++)
                {
                    for (int col = 1; col < 26; col++)
                    {
                        worksheet.Cells[row, col].Value = null;
                    }
                }

                worksheet.Cells[1, 1].Value = String.Format($"Line name : {User.SectionName}");
                int rowday = 5;
                for (int j = 0; j < 7; j++)
                {
                    worksheet.Cells[rowday - 3, j * 3 + 5].Value = date[j];
                    worksheet.Cells[rowday - 2, j * 3 + 5].Value = dateName[j];
                    //worksheet.Cells[rowday - 2, j * 3 + 6].Value = workerday[j];
                }
                int len = header.Length;
                for (int j = 0; j < len; j++)
                {
                    worksheet.Cells[rowday - 1, j + 1].Value = header[j];
                }
                if (dgvDay.Rows.Count > 0)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        worksheet.Cells[rowday + i, 1].Value = (dgvDay.Rows[i].Cells[0].Value);
                        worksheet.Cells[rowday + i, 2].Value = (dgvDay.Rows[i].Cells[1].Value);
                        worksheet.Cells[rowday + i, 3].Value = (dgvDay.Rows[i].Cells[2].Value);
                        worksheet.Cells[rowday + i, 4].Value = (dgvDay.Rows[i].Cells[3].Value);

                        worksheet.Cells[rowday + i, 5].Value = (dgvDay.Rows[i].Cells[5].Value);
                        worksheet.Cells[rowday + i, 6].Value = (dgvDay.Rows[i].Cells[6].Value);
                        worksheet.Cells[rowday + i, 7].Value = (dgvDay.Rows[i].Cells[7].Value);

                        worksheet.Cells[rowday + i, 8].Value = (dgvDay.Rows[i].Cells[9].Value);
                        worksheet.Cells[rowday + i, 9].Value = (dgvDay.Rows[i].Cells[10].Value);
                        worksheet.Cells[rowday + i, 10].Value = (dgvDay.Rows[i].Cells[11].Value);

                        worksheet.Cells[rowday + i, 11].Value = (dgvDay.Rows[i].Cells[13].Value);
                        worksheet.Cells[rowday + i, 12].Value = (dgvDay.Rows[i].Cells[14].Value);
                        worksheet.Cells[rowday + i, 13].Value = (dgvDay.Rows[i].Cells[15].Value);

                        worksheet.Cells[rowday + i, 14].Value = (dgvDay.Rows[i].Cells[17].Value);
                        worksheet.Cells[rowday + i, 15].Value = (dgvDay.Rows[i].Cells[18].Value);
                        worksheet.Cells[rowday + i, 16].Value = (dgvDay.Rows[i].Cells[19].Value);

                        worksheet.Cells[rowday + i, 17].Value = (dgvDay.Rows[i].Cells[21].Value);
                        worksheet.Cells[rowday + i, 18].Value = (dgvDay.Rows[i].Cells[22].Value);
                        worksheet.Cells[rowday + i, 19].Value = (dgvDay.Rows[i].Cells[23].Value);

                        worksheet.Cells[rowday + i, 20].Value = (dgvDay.Rows[i].Cells[25].Value);
                        worksheet.Cells[rowday + i, 21].Value = (dgvDay.Rows[i].Cells[26].Value);
                        worksheet.Cells[rowday + i, 22].Value = (dgvDay.Rows[i].Cells[27].Value);

                        worksheet.Cells[rowday + i, 23].Value = (dgvDay.Rows[i].Cells[29].Value);
                        worksheet.Cells[rowday + i, 24].Value = (dgvDay.Rows[i].Cells[30].Value);
                        worksheet.Cells[rowday + i, 25].Value = (dgvDay.Rows[i].Cells[31].Value);
                    }
                }
                rowday = 20;
                for (int j = 0; j < 7; j++)
                {
                    worksheet.Cells[rowday - 3, j * 3 + 5].Value = date[j];
                    worksheet.Cells[rowday - 2, j * 3 + 5].Value = dateName[j];
                    //worksheet.Cells[rowday - 2, j * 3 + 6].Value = workernight[j];
                }
                for (int j = 0; j < len; j++)
                {
                    worksheet.Cells[rowday - 1, j + 1].Value = header[j];
                }

                if (dgvNight.Rows.Count > 0)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        worksheet.Cells[rowday + i, 1].Value = (dgvNight.Rows[i].Cells[0].Value);
                        worksheet.Cells[rowday + i, 2].Value = (dgvNight.Rows[i].Cells[1].Value);
                        worksheet.Cells[rowday + i, 3].Value = (dgvNight.Rows[i].Cells[2].Value);
                        worksheet.Cells[rowday + i, 4].Value = (dgvNight.Rows[i].Cells[3].Value);

                        worksheet.Cells[rowday + i, 5].Value = (dgvNight.Rows[i].Cells[5].Value);
                        worksheet.Cells[rowday + i, 6].Value = (dgvNight.Rows[i].Cells[6].Value);
                        worksheet.Cells[rowday + i, 7].Value = (dgvNight.Rows[i].Cells[7].Value);

                        worksheet.Cells[rowday + i, 8].Value = (dgvNight.Rows[i].Cells[9].Value);
                        worksheet.Cells[rowday + i, 9].Value = (dgvNight.Rows[i].Cells[10].Value);
                        worksheet.Cells[rowday + i, 10].Value = (dgvNight.Rows[i].Cells[11].Value);

                        worksheet.Cells[rowday + i, 11].Value = (dgvNight.Rows[i].Cells[13].Value);
                        worksheet.Cells[rowday + i, 12].Value = (dgvNight.Rows[i].Cells[14].Value);
                        worksheet.Cells[rowday + i, 13].Value = (dgvNight.Rows[i].Cells[15].Value);

                        worksheet.Cells[rowday + i, 14].Value = (dgvNight.Rows[i].Cells[17].Value);
                        worksheet.Cells[rowday + i, 15].Value = (dgvNight.Rows[i].Cells[18].Value);
                        worksheet.Cells[rowday + i, 16].Value = (dgvNight.Rows[i].Cells[19].Value);

                        worksheet.Cells[rowday + i, 17].Value = (dgvNight.Rows[i].Cells[21].Value);
                        worksheet.Cells[rowday + i, 18].Value = (dgvNight.Rows[i].Cells[22].Value);
                        worksheet.Cells[rowday + i, 19].Value = (dgvNight.Rows[i].Cells[23].Value);

                        worksheet.Cells[rowday + i, 20].Value = (dgvNight.Rows[i].Cells[25].Value);
                        worksheet.Cells[rowday + i, 21].Value = (dgvNight.Rows[i].Cells[26].Value);
                        worksheet.Cells[rowday + i, 22].Value = (dgvNight.Rows[i].Cells[27].Value);

                        worksheet.Cells[rowday + i, 23].Value = (dgvNight.Rows[i].Cells[29].Value);
                        worksheet.Cells[rowday + i, 24].Value = (dgvNight.Rows[i].Cells[30].Value);
                        worksheet.Cells[rowday + i, 25].Value = (dgvNight.Rows[i].Cells[31].Value);
                    }

                }

                excel.SaveAs(excelFile);
                return true;
            }
            catch
            {

                return false;
            }

        }


        //private bool PPASClearExcelFile(string filename)
        //{
        //    try
        //    {

        //        FileInfo excelFile = new FileInfo(filename);
        //        ExcelPackage excel = new ExcelPackage(excelFile);
        //        var worksheet = excel.Workbook.Worksheets["PPAS"];
        //        ExcelWorksheet anotherWorksheet = excel.Workbook.Worksheets.FirstOrDefault(x => x.Name == "PPAS");
        //        if (anotherWorksheet == null)
        //        {
        //            return false;
        //        }
        //        //Print actual and Accumulate and OA
        //        for (int rows = 0; rows < 10; rows++)
        //        {
        //            worksheet.Cells[24 + rows * 2, 1].Value = "";
        //            worksheet.Cells[24 + rows * 2, 5].Value = "";
        //            worksheet.Cells[25 + rows * 2, 6].Value = "";
        //            for (int col = 0; col < 7; col++)
        //            {
        //                worksheet.Cells[24 + rows * 2, 9 + 4 * col].Value = "";
        //                worksheet.Cells[25 + rows * 2, 10 + 4 * col].Value = "";
        //                worksheet.Cells[24 + rows * 2, 12 + 4 * col].Value = "";
        //            }
        //        }
        //        // Print OA
        //        for (int i = 0; i < 7; i++)
        //        {
        //            worksheet.Cells[44, 12 + 4 * i].Value = "";
        //        }
        //        //Print CT
        //        worksheet.Cells[14, 25].Value = "";
        //        // Print header
        //        worksheet.Cells[4, 1].Value = "";
        //        //Print Date /Month / year
        //        for (int i = 0; i < 7; i++)
        //        {
        //            worksheet.Cells[22, 9 + 4 * i].Value = "";
        //        }
        //        excel.SaveAs(excelFile);
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(new Form() { TopMost = true }, "Error code =      , Message : " + ex.ToString() + " (LOOP Clear data in Excel file)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //        throw;
        //    }
        //}



        public bool ProductionTodayExcel(List<Prod_ProductionToday> raw, string fileName)
        {
            FileInfo excelFile = new FileInfo(fileName);
            ExcelPackage excel = new ExcelPackage(excelFile);

            try
            {
                excel.Workbook.Worksheets.Add("sheet1");
                var worksheet = excel.Workbook.Worksheets["sheet1"];
                int row = 2;
                foreach (Prod_ProductionToday item in raw)
                {
                    worksheet.Cells[row, 1].Value = item.sectionCode;
                    worksheet.Cells[row, 2].Value = item.registDate;
                    worksheet.Cells[row, 3].Value = item.workShift;
                    worksheet.Cells[row, 4].Value = item.partNumber;
                    worksheet.Cells[row, 5].Value = item.Qty;
                    worksheet.Cells[row, 6].Value = item.dayNight;
                    row++;

                }

                // header
                string[] header = new string[] { "section", "record date", "wordk shift", "part number", "QTY", "day-night" };
                for (int i = 0; i < header.Length; i++)
                {
                    worksheet.Cells[1, 1 + i].Value = header[i];

                }


                excel.SaveAs(excelFile);
                return true;
                //  MessageBox.Show("done", "Export data to Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //******************************************************************************
            }
            catch (Exception)
            {
                //MessageBox.Show("Please close your file was open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

        }








    }

    public class DataGridViewToExcelResult
    {
        public bool Status { get; set; }
        public string FileName { get; set; }

    }
}
