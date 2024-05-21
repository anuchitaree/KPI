using KPI.Class;
using KPI.Models;
using KPI.Parameter;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;


namespace KPI.ProdForm
{
    public partial class ProductionActualForm : Form
    {
        readonly string sectionDivPlatn = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
        private readonly List<string> HourList = new List<string>();
        private int _dayInmonth = 0;
        readonly Dictionary<string, string> MCNameDict = new Dictionary<string, string>();

        readonly List<string> _header = new List<string>();
        readonly string[,] prod = new string[23, 100];

        public ProductionActualForm()
        {
            InitializeComponent();
        }

        private void ProductionActualForm_Load(object sender, EventArgs e)
        {
            CmbDayNight.SelectedIndexChanged -= ReloadProductionVolumn;
            CmbMonth.SelectedIndexChanged -= ReloadProductionVolumn;
            CmbYear.SelectedIndexChanged -= ReloadProductionVolumn;
            DateTime da = DateTime.Now;
            int mon = da.Month;
            CmbDayNight.SelectedIndex = 2;
            CmbYear.Text = DateTime.Now.ToString("yyyy");
            CmbMonth.SelectedIndex = mon - 1;
            CmbDayNight.SelectedIndexChanged += ReloadProductionVolumn;
            CmbMonth.SelectedIndexChanged += ReloadProductionVolumn;
            CmbYear.SelectedIndexChanged += ReloadProductionVolumn;
            int i = 0;
            foreach (KeyValuePair<string, string> item in Dict.SectionCodeName)
            {
                if (item.Key == User.SectionCode)
                {
                    break;
                }
                i += 1;
            }
            using (var db = new ProductionEntities11())
            {
                var mclist = db.machineLists.Where(s => s.id_section == User.SectionCode)
                    .Select(n => new ProdMachineName
                    {
                        machineId = n.id_machine,
                        machineName = n.machine_name,
                        sort = n.sort,
                    }).ToList();
                foreach (var dr in mclist)
                {
                    MCNameDict.Add(dr.machineId, dr.machineName);
                    i++;
                }
                CmbMachineName.DataSource = new BindingSource(MCNameDict, null);
            }
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            string yearmonth = $"{CmbYear.Text}{CmbMonth.SelectedIndex + 1:00}";
            string selectedCmb = CmbMachineName.SelectedItem.ToString();
            string machineId = selectedCmb.Split(',')[0].Remove(0, 1);
            ExportExcel exp = new ExportExcel();
            DataGridViewToExcelResult result = exp.FileName1("ProductOf_" + machineId + "_", User.SectionCode, yearmonth);
            if (result.Status)
            {
                exp.ProductionByDayExcel(dataGridViewDay, result.FileName, CmbDayNight.Text);
            }

        }

        private void BtnExportForm_Click(object sender, EventArgs e)
        {
            if (dataGridViewDay.Rows.Count > 0)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = @"C:\",
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
                    InsertExcel(fileName, dataGridView1);
                }

            }
        }

        private void ReloadProductionVolumn(object sender, EventArgs e)
        {
            LoadProduction();
        }


        #region TAB 1: LEFT BUTTOM  / CHART & TABEL RIGHT SIDE HAND

        private void BtnUpdateByDay_Click(object sender, EventArgs e)
        {

            LoadProduction1();
            //LoadProduction();
            LoadChartProduction();
            PlotChartProduced();

            MessageBox.Show("Update Completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void LoadProduction1()
        {
            InitialDataGridView();

            int year = Convert.ToInt32(CmbYear.Text);
            int month = CmbMonth.SelectedIndex + 1;
            int dayinmonth = DateTime.DaysInMonth(year, month);
            string selectedCmb = CmbMachineName.SelectedItem.ToString();
            string machineId = selectedCmb.Split(',')[0].Remove(0, 1);

            using (var db = new ProductionEntities11())
            {
                DateTime startdate = new DateTime(year, month, 01);
                DateTime stopdate = new DateTime(year, month, dayinmonth);
               

                var volume = db.ML_RecordTable.Where(r => r.registDate >= startdate && r.registDate <= stopdate)
                    .Where(s => s.sectionCode == User.SectionCode).Where(m => m.mcNumber == machineId).Where(o=>o.OKNG=="OK")
                    .GroupBy(g => new { g.registDate, g.partNumber })
                    .Select(s => new ProductionSummay
                    {
                        RegistDate = s.Key.registDate,
                        Partnumber = s.Key.partNumber,
                        Qty = s.Count(x => x.OKNG != null)
                    }).ToList();

                var partnumberGroup = volume.GroupBy(g => g.Partnumber).Select(n => new PartNumber { Partnumber = n.Key }).ToList();

                var productionVolume = new List<ProductionByPartNumber>();
                foreach (var item in partnumberGroup)
                {
                    var datePiece = new List<DateQty>();
                    for (int i = 0; i < dayinmonth; i++)
                    {
                        DateTime date = startdate.AddDays(i);
                        int qty = 0;
                        var qtyExist = volume.Where(r => r.RegistDate == date).Where(p => p.Partnumber == item.Partnumber);
                        if (qtyExist.Any())
                            qty = qtyExist.FirstOrDefault().Qty;
                        datePiece.Add(new DateQty { RegistDate = date, Qty = qty });
                    }
                    productionVolume.Add(new ProductionByPartNumber { Partnumber = item.Partnumber, PiecePerDate = datePiece });
                }

                // inital dataGridView 
                int rownumber = partnumberGroup.Count()+2;
                dataGridViewDay.Rows.Clear();
                for (int i = 0; i < rownumber ; i++)
                {
                    string[] rows = new string[34];
                    for (int j = 0; j < 34; j++)
                    {
                        rows[j] = "";
                    }
                    dataGridViewDay.Rows.Add(rows);
                }

                int row = 0;
                foreach (var item in productionVolume)
                {
                    dataGridViewDay.Rows[row].Cells[0].Value = row + 1;
                    dataGridViewDay.Rows[row].Cells[1].Value = item.Partnumber;
                    int sumBypartnumber = 0;
                    foreach (var piece in item.PiecePerDate)
                    {
                        int day = piece.RegistDate.Day;
                        dataGridViewDay.Rows[row].Cells[day+1].Value = piece.Qty;
                        sumBypartnumber += piece.Qty;
                    }
                    dataGridViewDay.Rows[row].Cells[dayinmonth + 2].Value = sumBypartnumber;
                    row++;
                }


                var sumGroupbyPartnumber = volume
                    .GroupBy(g =>  g.RegistDate ).Select(x => new DateQty
                    { 
                        RegistDate = x.Key, Qty = x.Sum(a => a.Qty) 
                    }).ToList();

                dataGridViewDay.Rows[rownumber - 1].Cells[1].Value = "Total";
                foreach (var item in sumGroupbyPartnumber)
                {
                    int day = item.RegistDate.Day;
                    dataGridViewDay.Rows[rownumber-1].Cells[day + 1].Value = item.Qty;
                }

                //string jsonString11 = JsonSerializer.Serialize(sumGroupbyPartnumber);

                //Console.WriteLine(jsonString11);

            }

        }


        private void LoadProduction()
        {
            InitialDataGridView();

            int year = Convert.ToInt32(CmbYear.Text);
            int month = CmbMonth.SelectedIndex + 1;
            int dayinmonth = DateTime.DaysInMonth(year, month);
            string selectedCmb = CmbMachineName.SelectedItem.ToString();
            string machineId = selectedCmb.Split(',')[0].Remove(0, 1);
            var query = new StringBuilder();
            for (int i = 0; i < dayinmonth; i++)
            {
                DateTime starttime = new DateTime(year, month, 1, 07, 30, 00);
                starttime = starttime.AddDays(i);
                DateTime stoptime = new DateTime(year, month, 1, 19, 29, 59);
                stoptime = stoptime.AddDays(i);
                if (CmbDayNight.SelectedIndex == 1)
                {
                    starttime = new DateTime(year, month, 1, 19, 30, 00);
                    starttime = starttime.AddDays(i);
                    stoptime = new DateTime(year, month, 1, 07, 29, 59);
                    stoptime = stoptime.AddDays(1 + i);
                }
                else if (CmbDayNight.SelectedIndex == 2)
                {
                    starttime = new DateTime(year, month, 1, 07, 30, 00);
                    starttime = starttime.AddDays(i);
                    stoptime = new DateTime(year, month, 1, 07, 29, 59);
                    stoptime = stoptime.AddDays(1 + i);
                }
                string strstart = starttime.ToString("yyyy-MM-dd HH:mm:ss");
                string strstop = stoptime.ToString("yyyy-MM-dd HH:mm:ss");

                query.AppendFormat($"SELECT PartNumber, count(PartNumber) FROM [dbo].[Prod_RecordTable] where registDateTime between '{strstart}' AND '{strstop}' and SectionCode = '{User.SectionCode}' group by PartNumber \n\r");

            }


            string queryString = query.ToString();

            List<string> pn = new List<string>();
            string[,] amount = new string[32, 1000];
            int[] totalByday = new int[32];

            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.Prod_VolumLoadSQL(queryString);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                        {
                            if (ds.Tables[i].Rows[j].ItemArray[0] != null)
                            {
                                string a = ds.Tables[i].Rows[j].ItemArray[0].ToString();
                                int index = pn.IndexOf(a);
                                if (index == -1)
                                {
                                    pn.Add(a);
                                }
                                index = pn.IndexOf(a);
                                string qty = ds.Tables[i].Rows[j].ItemArray[1].ToString();
                                amount[i, index] = qty;
                                totalByday[i] = totalByday[i] + ChkString(qty);
                            }
                        }
                    }
                }
            }

            int b = pn.Count;
            dataGridViewDay.Rows.Clear();
            for (int i = 0; i < b + 2; i++)
            {
                string[] row = new string[34];
                for (int j = 0; j < 34; j++)
                {
                    row[j] = "";
                }
                dataGridViewDay.Rows.Add(row);
            }

            for (int i = 0; i < b; i++)
            {
                dataGridViewDay.Rows[i].Cells[0].Value = (1 + i).ToString();
                dataGridViewDay.Rows[i].Cells[1].Value = pn.ElementAt<string>(i);
                int total = 0;
                for (int j = 0; j < _dayInmonth + 1; j++)
                {
                    dataGridViewDay.Rows[i].Cells[2 + j].Value = amount[j, i];
                    total += ChkString(amount[j, i]);
                }
                dataGridViewDay.Rows[i].Cells[_dayInmonth + 2].Value = total.ToString();

            }
            dataGridViewDay.Rows[b + 1].Cells[1].Value = " Total by date ";
            ///////////////////////// Total all   //////////////////////////////////////////
            int totalall = 0;
            for (int i = 0; i < _dayInmonth + 1; i++)
            {
                if (totalByday[i] != 0)
                {
                    dataGridViewDay.Rows[b + 1].Cells[2 + i].Value = totalByday[i].ToString();
                }
                else
                {
                    dataGridViewDay.Rows[b + 1].Cells[2 + i].Value = "";
                }
                totalall += totalByday[i];
            }

            dataGridViewDay.Rows[b + 1].Cells[_dayInmonth + 2].Value = totalall.ToString();

            ////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////////



        }





        private void LoadChartProduction()
        {
            int month = CmbMonth.SelectedIndex + 1;
            int dayinmonth = DateTime.DaysInMonth(Convert.ToInt32(CmbYear.Text), month);
            DateTime daystart = new DateTime(Convert.ToInt32(CmbYear.Text), month, 1, 7, 30, 00);
            DateTime daystop = daystart.AddDays(dayinmonth);
            SqlClass sql = new SqlClass();
            string sectionDiVPlant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
            bool sqlstatus = sql.SSQL_SSS("ProdVolume", "@pStartTime", daystart.ToString("yyyy-MM-dd HH:mm:ss"), "@pStopTime", daystop.ToString("yyyy-MM-dd HH:mm:ss"), "@sectiondivplant", sectionDiVPlant);
            dataGridView2.Rows.Clear();
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt = ds.Tables[0];
                //DataTable dt1 = ds.Tables[1];
                if (dt.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string a = dr.ItemArray[0].ToString();
                        int b = Convert.ToInt32(dr.ItemArray[1]);
                        int c = Convert.ToInt32(dr.ItemArray[2]);
                        int d = Convert.ToInt32(dr.ItemArray[3]);
                        int e = Convert.ToInt32(dr.ItemArray[4]);
                        dataGridView2.Rows.Add(i, a, b, c, d, e);
                        i += 1;
                    }
                }

            }
        }

        private int ChkString(string val)
        {
            int result;// = 0;
            try
            {
                result = Convert.ToInt32(val);
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        public class DataGridViewProgressColumn : DataGridViewImageColumn
        {
            public float RedUntil { get; set; }
            public float YellowUntil { get; set; }
            public float OrangeUntil { get; set; }

            public DataGridViewProgressColumn()
            {
                CellTemplate = new DataGridViewProgressCell();
            }
        }

        class DataGridViewProgressCell : DataGridViewImageCell
        {
            // Used to make custom cell consistent with a DataGridViewImageCell
            static readonly Image emptyImage;
            static DataGridViewProgressCell()
            {
                emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            public DataGridViewProgressCell()
            {
                this.ValueType = typeof(int);
            }
            // Method required to make the Progress Cell consistent with the default Image Cell. 
            // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
            protected override object GetFormattedValue(object value,
                                int rowIndex, ref DataGridViewCellStyle cellStyle,
                                TypeConverter valueTypeConverter,
                                TypeConverter formattedValueTypeConverter,
                                DataGridViewDataErrorContexts context)
            {
                return emptyImage;
            }
            protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
            {
                try
                {
                    if (value == null)
                    {
                        return;
                    }
                    int progressVal = (int)value;
                    float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
                    Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
                    Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
                    // Draws the cell grid
                    base.Paint(g, clipBounds, cellBounds,
                     rowIndex, cellState, value, formattedValue, errorText,
                     cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

                    //  var progressColumn = this.DataGridView.Columns[this.ColumnIndex] as DataGridViewProgressColumn;
                    //DataGridViewProgressColumn progressColumn = this.DataGridView.Columns[this.ColumnIndex] as DataGridViewProgressColumn;
                    //if (percentage > 0.0 && progressColumn != null)
                    if (percentage > 0.0 && this.DataGridView.Columns[this.ColumnIndex] is DataGridViewProgressColumn progressColumn)
                    {
                        // Draw the progress bar and the text
                        Brush fillColor;
                        if (percentage > progressColumn.OrangeUntil)
                        {
                            fillColor = new SolidBrush(Color.FromArgb(141, 108, 255)); // violet
                        }
                        else if (percentage > progressColumn.YellowUntil && percentage <= progressColumn.OrangeUntil)
                        {
                            fillColor = new SolidBrush(Color.FromArgb(19, 255, 19)); // GREEN
                        }
                        else if (percentage > progressColumn.RedUntil && percentage <= progressColumn.YellowUntil)
                        {
                            fillColor = new SolidBrush(Color.FromArgb(255, 247, 0)); // YELLOW
                        }
                        else
                        {
                            fillColor = new SolidBrush(Color.FromArgb(255, 76, 95)); //RED
                        }

                        g.FillRectangle(fillColor, cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 4);
                        g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
                    }
                    else
                    {
                        // draw the text
                        if (this.DataGridView.CurrentRow.Index == rowIndex)
                            g.DrawString(progressVal.ToString() + "%", cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2);
                        else
                            g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
                    }
                }
                catch { }

            }
        }
        #endregion

        #region PRODUCTION  STACKCOLUMN  & EXCEL LEFT TOP

        private void PlotChartProduced()
        {
            Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen,
                Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            int rowcount = dataGridViewDay.Rows.Count;

            List<string> pn = new List<string>();
            pn.Clear();
            for (int i = 0; i < rowcount; i++)
            {
                pn.Add(dataGridViewDay.Rows[i].Cells[1].Value.ToString());
            }

            Charts.ChartProductionDay(pn, color, chartQTY, dataGridViewDay);
            ChartLegent.Legend_ListMultiColor1(tpanelChartPartNumber, 1, "tpanelChartPartNumber", "lbcharSixtLoss", color, pn);

        }

        private void InsertExcel(string fileName, DataGridView dataGridView1)
        {
            FileInfo excelFile = new FileInfo(fileName);
            ExcelPackage excel = new ExcelPackage(excelFile);

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

        #region InitialDataGridView()
        private void InitialDataGridView()
        {
            this.dataGridViewDay.Rows.Clear();
            int year = Convert.ToInt32(CmbYear.Text);
            int month = CmbMonth.SelectedIndex + 1;
            // calculate MP and productivity
            //DateTime dt = new DateTime(year, month, 1);
            _dayInmonth = (int)DateTime.DaysInMonth(year, month);


            this.dataGridViewDay.ColumnCount = _dayInmonth + 3;
            this.dataGridViewDay.Columns[0].Name = "No";
            this.dataGridViewDay.Columns[0].Width = 20; //20
            this.dataGridViewDay.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridViewDay.Columns[1].Name = "Part Number";
            this.dataGridViewDay.Columns[1].Width = 100; //80
            this.dataGridViewDay.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewDay.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            for (int i = 0; i < _dayInmonth; i++)
            {
                this.dataGridViewDay.Columns[2 + i].Name = (i + 1).ToString();
                this.dataGridViewDay.Columns[2 + i].Width = 35;
                this.dataGridViewDay.Columns[2 + i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.dataGridViewDay.Columns[_dayInmonth + 2].Name = "Total";
            this.dataGridViewDay.Columns[_dayInmonth + 2].Width = 50;
            this.dataGridViewDay.Columns[_dayInmonth + 2].SortMode = DataGridViewColumnSortMode.NotSortable;
            //this.dataGridViewDay.Columns[_dayInmonth + 3].Name = "Accumurate";
            //this.dataGridViewDay.Columns[_dayInmonth + 3].Width = 50;
            //this.dataGridViewDay.Columns[_dayInmonth + 3].SortMode = DataGridViewColumnSortMode.NotSortable;
            //this.dataGridViewDay.Columns[_dayInmonth + 4].Name = "Approved";
            //this.dataGridViewDay.Columns[_dayInmonth + 4].Width = 50;
            //this.dataGridViewDay.Columns[_dayInmonth + 4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewDay.RowHeadersWidth = 5;
            this.dataGridViewDay.DefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridViewDay.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridViewDay.RowTemplate.Height = 25;
            this.dataGridViewDay.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataGridViewDay.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(176, 208, 249);
            dataGridViewDay.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewDay.AllowUserToResizeRows = false;
            dataGridViewDay.AllowUserToResizeColumns = false;

            ///////////////////// DatagridView2 ////////////////////////////////
            DataGridViewProgressColumn column = new DataGridViewProgressColumn { RedUntil = 0.5f, YellowUntil = 0.75f, OrangeUntil = 1.10f };
            dataGridView2.ColumnCount = 5;
            dataGridView2.Columns[0].Name = "No";
            dataGridView2.Columns[0].Width = 20;
            dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[1].Name = "Part Number";
            dataGridView2.Columns[1].Width = 100;
            dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            //dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[2].Name = "Plan";
            dataGridView2.Columns[2].Width = 40;
            dataGridViewDay.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[3].Name = "Actual";
            dataGridView2.Columns[3].Width = 40;
            dataGridView2.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[4].Name = "Diff";
            dataGridView2.Columns[4].Width = 40;
            dataGridView2.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dataGridView2.Columns[5].Name = "%";
            //dataGridView2.Columns[5].Width = 40;
            //dataGridView2.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridView2.Columns[3].Name = "value";
            //dataGridView2.Columns[3].Width = 50;

            dataGridView2.Columns.Add(column);
            dataGridView2.Columns[5].Width = 90;
            //dataGridView2.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.HeaderText = "Progress";
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 4;
            this.dataGridView2.DefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridView2.RowTemplate.Height = 20;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.AllowUserToResizeColumns = false;
        }

        #endregion



        private void BtnExcelFormByDay_Click(object sender, EventArgs e)
        {
            ByHourExportExcelForm();
        }


        private void BtnExcelExportByHour_Click(object sender, EventArgs e)
        {
            ByHourExportExcel();
        }



        private void ByHourExportExcelForm()
        {
            if (dataGridViewDay.Rows.Count > 0)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = @"C:\",
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
                    InsertExcel(fileName, dataGridViewDay);
                }

            }
        }

        private void ByHourExportExcel()
        {
            ExportExcel exp = new ExportExcel();
            DataGridViewToExcelResult result = exp.FileName2("ProductByHour", User.SectionCode, DateTimePickerByHour.Value.ToString("yyyyMMdd"));
            if (result.Status)
            {
                exp.ProductionByHourExcel(dataGridViewHour, result.FileName, _header);
            }

        }



        #region TAB 2 : By Hour


        private void BtnUpdateHour_Click(object sender, EventArgs e)
        {
            UpdateByHour();
            PlotChartProducedByHour();
        }


        private void DateTimePickerByHour_ValueChanged(object sender, EventArgs e)
        {
            UpdateByHour();
            PlotChartProducedByHour();
        }


        private void UpdateByHour()
        {
            DateTime dt = DateTimePickerByHour.Value;

            int year = dt.Year;//Convert.ToInt32(comboBox4.Text);
            int month = dt.Month;//cmbMonth.SelectedIndex + 1;
            int day = dt.Day;

            //string div = CommonDefine.Emp_Division;
            //string plant = CommonDefine.Emp_Plant;
            string section = User.SectionCode;

            DateTime dr = new DateTime(1900, 1, 1);
            DateTime dc = new DateTime(year, month, day);
            double plus = (dc - dr).TotalDays;
            _header.Clear();
            _header.Add("No");
            _header.Add("Part Number");

            this.dataGridViewHour.ColumnCount = 23;
            this.dataGridViewHour.Columns[0].Name = "No";
            this.dataGridViewHour.Columns[0].Width = 30;
            this.dataGridViewHour.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewHour.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewHour.Columns[1].Name = "Part Number";
            this.dataGridViewHour.Columns[1].Width = 100;
            this.dataGridViewHour.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            string sectiondivplant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SSS("LoadProd_TimeBreakTable", "@DateToday", dt.ToString("yyyy-MM-dd"), "@DayNightDN", "B", "@sectiondivplant", sectiondivplant);
            var query = new StringBuilder();
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[1];
                if (dt1.Rows.Count > 0)
                {
                    HourList.Clear();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string hd = dt1.Rows[i].ItemArray[0].ToString();
                        HourList.Add(hd);
                        this.dataGridViewHour.Columns[2 + i].Name = hd;
                        this.dataGridViewHour.Columns[2 + i].Width = 50;
                        this.dataGridViewHour.Columns[2 + i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        this.dataGridViewHour.Columns[2 + i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DateTime start = Convert.ToDateTime(dt1.Rows[i].ItemArray[1]);
                        start = start.AddDays(plus);
                        DateTime stop = Convert.ToDateTime(dt1.Rows[i].ItemArray[2]);
                        stop = stop.AddDays(plus);
                        query.AppendFormat("SELECT PartNumber, count(PartNumber) FROM [dbo].[Prod_RecordTable] where registDateTime between '{0}' AND '{1}' and SectionCode = '{2}' group by PartNumber \n", start, stop, section);
                        _header.Add(hd);
                    }

                }
            }
            _header.Add("Total");

            this.dataGridViewHour.Columns[22].Name = "Total";
            this.dataGridViewHour.Columns[22].Width = 50;
            this.dataGridViewHour.Columns[22].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewHour.Columns[22].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewHour.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8);
            this.dataGridViewHour.RowHeadersWidth = 4;
            this.dataGridViewHour.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8);
            this.dataGridViewHour.RowTemplate.Height = 25;
            this.dataGridViewHour.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataGridViewHour.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(176, 208, 249);
            dataGridViewHour.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewHour.AllowUserToResizeRows = false;
            dataGridViewHour.AllowUserToResizeColumns = false;



            //var qq = query.ToString();

            SqlClass sql3 = new SqlClass();
            bool sqlstatus3 = sql3.Prod_VolumReadByPartNumberSQL(query.ToString());
            if (sqlstatus3)
            {
                DataSet ds2 = sql3.Dataset;
                List<string> pn = new List<string>();
                string[,] amount = new string[21, 500];
                int[] totalByday = new int[21];
                if (ds2.Tables.Count > 0)
                {
                    for (int i = 0; i < ds2.Tables.Count; i++)
                    {
                        if (ds2.Tables[i].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds2.Tables[i].Rows.Count; j++)
                            {
                                if (ds2.Tables[i].Rows[j].ItemArray[0] != null)
                                {
                                    string a = ds2.Tables[i].Rows[j].ItemArray[0].ToString();
                                    int index = pn.IndexOf(a);
                                    if (index == -1)
                                    {
                                        pn.Add(a);
                                    }
                                    index = pn.IndexOf(a);
                                    string qty = ds2.Tables[i].Rows[j].ItemArray[1].ToString();
                                    amount[i, index] = qty;
                                    totalByday[i] = totalByday[i] + ChkString(qty);
                                }
                            }
                        }
                    }

                }


                int b = pn.Count;
                dataGridViewHour.Rows.Clear();
                for (int i = 0; i < b + 2; i++)
                {
                    string[] row = new string[24];
                    for (int j = 0; j < 24; j++)
                    {
                        row[j] = "";
                    }
                    dataGridViewHour.Rows.Add(row);
                }

                for (int i = 0; i < b; i++)
                {
                    dataGridViewHour.Rows[i].Cells[0].Value = (1 + i).ToString();
                    dataGridViewHour.Rows[i].Cells[1].Value = pn.ElementAt<string>(i);
                    int total = 0;
                    for (int j = 0; j < 20; j++)
                    {
                        dataGridViewHour.Rows[i].Cells[2 + j].Value = amount[j, i];
                        total += ChkString(amount[j, i]);
                    }
                    dataGridViewHour.Rows[i].Cells[20 + 2].Value = total.ToString();

                }
                dataGridViewHour.Rows[b + 1].Cells[1].Value = " Total by Hour ";
                ///////////////////////// Total all   //////////////////////////////////////////
                int totalall = 0;
                for (int i = 0; i < 20 + 1; i++)
                {
                    if (totalByday[i] != 0)
                    {
                        dataGridViewHour.Rows[b + 1].Cells[2 + i].Value = totalByday[i].ToString();
                    }
                    else
                    {
                        dataGridViewHour.Rows[b + 1].Cells[2 + i].Value = "";
                    }
                    totalall += totalByday[i];
                }

                dataGridViewHour.Rows[b + 1].Cells[20 + 2].Value = totalall.ToString();
                ////////////////////////////////////////////////////////////////////////////////////
                for (int i = 0; i < dataGridViewHour.Columns.Count; i++)
                {
                    for (int j = 0; j < dataGridViewHour.Rows.Count; j++)
                    {
                        prod[i, j] = dataGridViewHour.Rows[j].Cells[i].Value == null ? "" : Convert.ToString(dataGridViewHour.Rows[j].Cells[i].Value);
                    }
                }

            }
        }

        private void PlotChartProducedByHour()
        {
            Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen,
                Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};


            int rowcount = dataGridViewHour.Rows.Count;
            //int colcount = dataGridViewHour.Columns.Count;
            List<string> pn = new List<string>();
            pn.Clear();
            for (int i = 0; i < rowcount; i++)
            {
                pn.Add(dataGridViewHour.Rows[i].Cells[1].Value.ToString());
            }
            Charts.ChartProductionHour(pn, HourList, color, chartHour, dataGridViewHour);
            ChartLegent.Legend_ListMultiColor1(tableLayoutPanel10, 1, "tableLayoutPanel10", "lbcharSixtLoss", color, pn);
        }

        #endregion By Hour


        #region TAB 3 : By Part number

        //private void btnUpdatePartNumber_Click(object sender, EventArgs e)
        //{

        //}


        private void BtnUpdatePartNumber_Click(object sender, EventArgs e)
        {
            //Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
            //        Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen,
            //    Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
            //        Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            string registdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SS("ProductionPartnumberCountExc", "@betweendate", registdate, "@sectiondivplant", sectionDivPlatn); // sectionDivPlatn
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];
                Charts.ColumnSingleColorOneAxis1(dt1, chartPartNumber, Color.FromArgb(191, 255, 191));
            }

        }


        private void BtnExportFormPN_Click(object sender, EventArgs e)
        {

        }

        private void BtnExcelExportPN_Click(object sender, EventArgs e)
        {

        }



        #endregion By Part number


    }
}
