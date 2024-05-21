using KPI.Class;
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

namespace KPI.InitialForm
{
    public partial class ProductionPlanForm : Form
    {

        //string Section = string.Empty;
        readonly List<string> pnList = new List<string>();
        readonly List<string> planList = new List<string>();
        private object log;


        public ProductionPlanForm()
        {
            InitializeComponent();
            DataGridViewInitial();
        }

        private void ProductionPlanForm_Load(object sender, EventArgs e)
        {
            cmbSection.DataSource = new BindingSource(Dict.SectionCodeName, null);
            cmbSection.DisplayMember = "Value";
            cmbSection.ValueMember = "Key";
            int i = 0;
            foreach (KeyValuePair<string, string> item in Dict.SectionCodeName)
            {
                if (item.Key == User.SectionCode)
                {
                    cmbSection.SelectedIndex = i;
                    break;
                }
                i += 1;
            }
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            CmbYear.Text = year.ToString();
            CmbMonth.SelectedIndex = month - 1;
            Roles();
        }




        #region INIIAL 

        private void DataGridViewInitial()
        {
            this.DataGridViewPN.ColumnCount = 2;
            this.DataGridViewPN.Columns[0].Name = "No";
            this.DataGridViewPN.Columns[0].Width = 50;
            this.DataGridViewPN.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewPN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewPN.Columns[1].Name = "Part Number";
            this.DataGridViewPN.Columns[1].Width = 150;
            this.DataGridViewPN.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewPN.RowHeadersWidth = 4;
            this.DataGridViewPN.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewPN.RowTemplate.Height = 20;
            this.DataGridViewPN.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewPN.RowsDefaultCellStyle.BackColor = Color.White;
            this.DataGridViewPN.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(169, 235, 187);
            this.DataGridViewPN.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.DataGridViewPN.AllowUserToResizeRows = false;
            this.DataGridViewPN.AllowUserToResizeColumns = false;

            this.DgvPlan.ColumnCount = 4;
            this.DgvPlan.Columns[0].Name = "Run";
            this.DgvPlan.Columns[0].Width = 5;
            this.DgvPlan.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvPlan.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvPlan.Columns[1].Name = "No";
            this.DgvPlan.Columns[1].Width = 100;
            this.DgvPlan.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvPlan.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvPlan.Columns[2].Name = "Part Number";
            this.DgvPlan.Columns[2].Width = 150;
            this.DgvPlan.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvPlan.Columns[3].Name = "QTY Plan";
            this.DgvPlan.Columns[3].Width = 100;
            this.DgvPlan.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvPlan.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvPlan.RowHeadersWidth = 4;
            this.DgvPlan.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvPlan.RowTemplate.Height = 20;
            this.DgvPlan.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvPlan.RowsDefaultCellStyle.BackColor = Color.White;
            this.DgvPlan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(229, 226, 254);
            this.DgvPlan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.DgvPlan.AllowUserToResizeRows = false;
            this.DgvPlan.AllowUserToResizeColumns = false;
        }

        private void Roles()
        {



            BtnSavePlan.Enabled = false;

            if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager) // production
            {
            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {

            }
            else if (User.Role == Models.Roles.Admin_Min)  // Admin -Mini
            {

                BtnSavePlan.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {

                BtnSavePlan.Enabled = true;
            }

        }

        //private void btnPNRead_Click(object sender, EventArgs e)
        //{
        //    ProductionPlan_Read();
        //}

        //private void bntImportError_Click(object sender, EventArgs e)
        //{
        //    ImmportExcel();
        //}


        private void BtnPNRead_Click(object sender, EventArgs e)
        {
            ProductionPlan_Read();
        }

        private void BtnImportError_Click(object sender, EventArgs e)
        {
            ImmportExcel();
        }

        private void ImmportExcel()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Title = "Browse Text Files",
                DefaultExt = "xlsx",
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FilterIndex = 2
            };
            openFileDialog1.ShowDialog();
            openFileDialog1.Multiselect = false;

            if (File.Exists(openFileDialog1.FileName) == true)
            {
                FileInfo excelFile = new FileInfo(openFileDialog1.FileName);
                ExcelPackage excel = new ExcelPackage(excelFile);
                DgvPlan.Rows.Clear();
                var worksheet = excel.Workbook.Worksheets["Sheet1"];
                int i = 2;
                int rows = 1;
                bool status0;// = false;
                do
                {
                    string pn = Convert.ToString(worksheet.Cells[i, 1].Value);
                    string plan = Convert.ToString(worksheet.Cells[i, 2].Value);
                    bool status3 = (pn.Length > 5);
                    status0 = status3;
                    bool status4 = Converting.IsNumeric(plan); //int.TryParse(plan, out int planC);

                    if (status3 == true && status4 == true)
                    {
                        if (pnList.Contains(pn) == true)
                        {
                            DgvPlan.Rows.Add("0", (rows).ToString(), pn, plan);
                        }
                        else
                        {
                            MessageBox.Show("Data loss, because part-number in nettime-table is not cover", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        rows++;
                    }

                    i += 1;

                }
                while (status0);

            }
        }


        private void BtnClearPlan_Click(object sender, EventArgs e)
        {
            DgvPlan.Rows.Clear();
        }

        //private void btnDeletePlan_Click(object sender, EventArgs e)
        //{

        //}


        private void BtnDeletePlan_Click(object sender, EventArgs e)
        {
            if (DgvPlan.Rows.Count > 0)
            {
                int r = DgvPlan.CurrentRow.Index;
                DgvPlan.Rows.RemoveAt(r);
            }
            int row = DgvPlan.Rows.Count;
            int total = 0;
            for (int i = 0; i < row; i++)
            {
                int qty = Int32.Parse(DgvPlan.Rows[i].Cells[3].Value.ToString());
                total = qty + total;
            }
            //lbPlan.Text = total.ToString();
            lbPlan.Text = DataFormat.Comma(total.ToString());
        }

        //private void bntSavePlan_Click(object sender, EventArgs e)
        //{

        //}



        private void BtnSavePlan_Click(object sender, EventArgs e)
        {
            if (CheckDataTable() == true)
            {
                SaveProductionPlan();
                CalculateProductionVolume();
            }
            else
            {
                MessageBox.Show("Wrong data in column QTY, please check it again  ", "Errror", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void SaveProductionPlan()
        {
            int row = DgvPlan.RowCount;
            if (row == 0)
                return;

            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;

            string month = (CmbMonth.SelectedIndex + 1).ToString("00");
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ProductionPlan_PartNumberPlanSaveSQL(section, CmbYear.Text, month, DgvPlan);
            if (sqlstatus)
            {
                MessageBox.Show("Save Production Plan Completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(" Save Production Plan Failure !!!! ", "Errror", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            using (var db = new ProductionEntities11())
            {
                DateTime dmy = new DateTime(Convert.ToInt32(CmbYear.Text), CmbMonth.SelectedIndex + 1, 1);
                int dayInMonth = DateTime.DaysInMonth(dmy.Year, dmy.Month);
                DateTime enddate = dmy.AddDays(dayInMonth - 1);

                var remove = db.Pg_PostPlan.Where(s => s.sectionCode == User.SectionCode)
                    .Where(r => r.registDate >= dmy && r.registDate <= enddate);
                if(remove != null)
                {
                    db.Pg_PostPlan.RemoveRange(remove);
                    db.SaveChanges();
                }

                double productionPlan = 0;
                for (int i = 0; i < row; i++)
                {
                    productionPlan += Convert.ToInt32(DgvPlan.Rows[i].Cells[3].Value);
                }

                var planWorkingDay = db.Prod_CustWorkingDayTable
                     .Where(s => s.sectionCode == section)
                    .Where(y => y.registYear == dmy.Year)
                    .Where(m => m.registMonth == dmy.Month)
                    .OrderBy(r => r.registDate)
                    .Select(a => new DateWorking
                    {
                        RegistDate = a.registDate,
                        WorkShift = a.workHoliday
                    }).ToList();

                var countWorkingDay = planWorkingDay.AsEnumerable()
                    .GroupBy(i => 1)
                    .Select(a => new QTY
                    {
                        qty = a.Sum(x => x.WorkShift)
                    }).FirstOrDefault();

                DateTime lastDateWork = planWorkingDay
                            .Where(s => s.WorkShift != 0)
                            .OrderByDescending(r => r.RegistDate)
                            .FirstOrDefault().RegistDate;


                double QtyperDay = (Math.Floor(productionPlan / countWorkingDay.qty));

                double Qtyacc = 0;


                var pre_progress = new List<Pg_PostPlan>();

                foreach (var item in planWorkingDay)
                {

                    double postPlan;
                    if (lastDateWork == item.RegistDate)
                    {
                        postPlan = productionPlan - Qtyacc;
                        Qtyacc = productionPlan;
                    }
                    else
                    {
                        postPlan = item.WorkShift * QtyperDay;
                        Qtyacc += item.WorkShift * QtyperDay;
                    }
                    var calendar = new Pg_PostPlan()
                    {
                        sectionCode = User.SectionCode,
                        registDate = item.RegistDate,
                        postPlan = postPlan,
                        postPlanAcc = Qtyacc,
                    };
                    pre_progress.Add(calendar);

                }
                try
                {
                    db.Pg_PostPlan.AddRange(pre_progress);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show(" Save Progress Post Production Plan Failure !!!! ", "Errror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               




            }


        }


        private bool CheckDataTable()
        {
            List<string> memo = new List<string>();
            memo.Clear();
            int i = 0;
            int row = DgvPlan.Rows.Count;
            bool result = row == 0;
            while (i < row)
            {
                if (DgvPlan.Rows[i].Cells[2].Value != null)
                {
                    string tg = DgvPlan.Rows[i].Cells[2].Value.ToString();
                    if (memo.Contains(tg) == false)
                    {
                        memo.Add(tg);
                    }
                    else
                    {
                        string error = $"Part number repeat at row = {i + 1}";
                        MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result = false;
                        break;
                    }
                    if (pnList.Contains(tg) == false)
                    {
                        string error = $"Part number is not in Yearly Standard net time table, row =  {i + 1} \n Please registration before plan it";
                        MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result = false;
                        break;
                    }
                    string qty = DgvPlan.Rows[i].Cells[3].Value.ToString();
                    bool qtystatus = Converting.IsNumeric(qty); //int.TryParse(qty, out int count);
                    if (qtystatus == false)
                    {
                        string error = $"Q'TY is not number, row =  {i + 1} \n Please correct it before save it";
                        MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result = false;
                        break;
                    }

                }
                else
                {
                    break;
                }
                i++;
                if (row == i)
                {
                    result = true;
                }
            }
            return result;
        }


        #endregion

        private void PartNumberdRead()
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            DgvPlan.Rows.Clear();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ProductionPlan_PartNumberReadSQL(CmbYear.Text, section);
            if (sqlstatus)
            {
                DataTable dt = sql.Datatable;

                //masterpnList.Clear();
                int row = dt.Rows.Count;
                for (int i = 0; i < row; i++)
                {
                    DgvPlan.Rows.Add("0", i, dt.Rows[i].ItemArray[0].ToString(), "0");
                    //masterpnList.Add(dt.Rows[0].ItemArray[0].ToString());
                }
            }
        }


        private void ProductionPlan_Read()
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            SqlClass sql = new SqlClass();
            string month = (CmbMonth.SelectedIndex + 1).ToString("00");
            bool sqlstatus = sql.ProductionPlan_PartNumberPlanReadSQL(CmbYear.Text, month, section);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt = ds.Tables[0];
                DataTable dt1 = ds.Tables[1];
                int i = 1;
                DgvPlan.Rows.Clear();
                planList.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DgvPlan.Rows.Add(dr.ItemArray[0], i, dr.ItemArray[1], dr.ItemArray[2]);
                        planList.Add(dr.ItemArray[1].ToString());
                        i++;
                    }
                }
                lbPlan.Text = string.Empty;
                if (dt1.Rows.Count > 0)
                {
                    string str = dt1.Rows[0].ItemArray[0].ToString();
                    lbPlan.Text = DataFormat.Comma(str);
                }
            }
        }

        private void BtnReadPN_Click(object sender, EventArgs e)
        {
            PartNumberdRead();
        }

        //private void btnCal_Click(object sender, EventArgs e)
        //{
        //    CalculateProductionVolume();
        //}

        private void BtnCal_Click(object sender, EventArgs e)
        {
            CalculateProductionVolume();
        }


        private void CalculateProductionVolume()
        {
            int row = DgvPlan.Rows.Count;
            int total = 0;
            if (row > 0)
            {
                for (int i = 0; i < row; i++)
                {
                    string str = DgvPlan.Rows[i].Cells[3].Value.ToString();
                    bool status = int.TryParse(str, out int plan);
                    if (status)
                    {
                        total += plan;
                    }
                }
            }
            lbPlan.Text = DataFormat.Comma(total.ToString());
        }



        private void CmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            NetTimeRead();
        }


        private void NetTimeRead()
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            DataGridViewPN.Rows.Clear();
            pnList.Clear();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.Yearly_LoadNetTimeStandardSQL(section, CmbYear.Text);
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                int i = 1;
                foreach (DataRow dr in dt1.Rows)
                {
                    DataGridViewPN.Rows.Add(i.ToString(), dr.ItemArray[1], dr.ItemArray[2], dr.ItemArray[3]);
                    pnList.Add(dr.ItemArray[1].ToString());
                    i++;
                }
            }

        }

        //private void dataGridViewPlan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        //private void dataGridViewPlan_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    int r = DataGridViewPlan.CurrentCell.RowIndex;
        //    int c = DataGridViewPlan.CurrentCell.ColumnIndex;
        //    log = DataGridViewPlan.Rows[r].Cells[c].Value;
        //}

        //private void dataGridViewPN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}


        private void DataGridViewPlan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle styleChange = new DataGridViewCellStyle
            {
                ForeColor = Color.Blue
            };
            int r = DgvPlan.CurrentRow.Index;
            int c = DgvPlan.CurrentCell.ColumnIndex;
            string pn = Convert.ToString(DgvPlan.Rows[r].Cells[c].Value);

            if (c == 2)
            {
                if (pn.Length >= 12)
                {
                    styleChange.ForeColor = Color.Red;
                    DgvPlan.Rows[r].Cells[c].Style = styleChange;
                }
                else
                {
                    DgvPlan.Rows[r].Cells[c].Value = log;
                }

            }
            else if (c == 3)
            {
                bool isnumeric = Converting.IsNumeric(pn); //double.TryParse(pn, out double n);
                if (isnumeric == false)
                {

                    MessageBox.Show("Please fill only numberical", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DgvPlan.Rows[r].Cells[c].Value = log;
                    return;
                }
                else if (isnumeric == true)
                {
                    styleChange.ForeColor = Color.Red;
                    DgvPlan.Rows[r].Cells[c].Style = styleChange;
                }
            }
        }

        private void DataGridViewPlan_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int r = DgvPlan.CurrentCell.RowIndex;
            int c = DgvPlan.CurrentCell.ColumnIndex;
            log = DgvPlan.Rows[r].Cells[c].Value;
        }

        private void DataGridViewPN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridViewPN.Rows.Count > 0)
            {
                int row = DataGridViewPN.CurrentRow.Cells[0].RowIndex;
                string pn = DataGridViewPN.Rows[row].Cells[1].Value.ToString();
                if (planList.Contains(pn) == false)
                {
                    DgvPlan.Rows.Add("", (row + 1).ToString(), pn, 0);
                    planList.Add(pn);
                }
                else
                {
                    MessageBox.Show("Repeat part number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
