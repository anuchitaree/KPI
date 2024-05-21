using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using KPI.Parameter;
using KPI.Class;
using KPI.Models;

namespace KPI.InitialForm
{
    public partial class STDNetTimeForm : Form
    {
        readonly Dictionary<object, object> NetDic2 = new Dictionary<object, object>();
        readonly List<int> memoCellChange = new List<int>();
        private object log1;
        private object log;
        //readonly string Section = string.Empty;
        public STDNetTimeForm()
        {
            InitializeComponent();
            DataGridViewInitial();
        }

        private void STDNetTimeForm_Load(object sender, EventArgs e)
        {
            cmbYear.Text = DateTime.Now.ToString("yyyy");
            Roles();
            cmbSectionCode.DataSource = new BindingSource(Dict.SectionCodeName, null);
            cmbSectionCode.DisplayMember = "Value";
            cmbSectionCode.ValueMember = "Key";
            int i = 0;
            foreach (KeyValuePair<string, string> item in Dict.SectionCodeName)
            {
                if (item.Key == User.SectionCode)
                {
                    cmbSectionCode.SelectedIndex = i;
                    break;
                }
                i += 1;
            }
        }

        #region LEFT SIDE ================================================================
        private void BtnStdSave_Click(object sender, EventArgs e)
        {
            string section = ((KeyValuePair<string, string>)cmbSectionCode.SelectedItem).Key;
            if (DataGridViewYearly.Rows.Count > 0)
            {
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.Yearly_SaveSTDRatioSQL(section, cmbYear.Text, DataGridViewYearly);
                if (sqlstatus)
                {
                    MessageBox.Show("Save Standand CTaverage , Standard Ratio, % OA Completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReadSTDTime();
                }
                else
                    MessageBox.Show("Try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStdNew_Click(object sender, EventArgs e)
        {
            string year = cmbYear.Text;
            DataGridViewYearly.Rows.Clear();
            for (int i = 0; i < 12; i++)
            {
                string month = (i + 1).ToString("00");
                DataGridViewYearly.Rows.Add(year, month, 10, 1, 100);
            }
        }

        private void BtnStdRead_Click(object sender, EventArgs e)
        {
            ReadSTDTime();
        }


        //private void dataGridViewYearly_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{

        //}


        private void DataGridViewYearly_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle styleChange = new DataGridViewCellStyle
            {
                ForeColor = Color.Blue
            };
            int r = DataGridViewYearly.CurrentRow.Index;
            int c = DataGridViewYearly.CurrentCell.ColumnIndex;
            string data = Convert.ToString(DataGridViewYearly.Rows[r].Cells[c].Value);

            if (c >= 2 && c <= 4)
            {
                if (Converting.IsNumeric(data) == true)
                {
                    styleChange.ForeColor = Color.Red;
                    DataGridViewYearly.Rows[r].Cells[c].Style = styleChange;
                    memoCellChange.Add(r);
                    Console.WriteLine(r);
                }
                else
                {
                    DataGridViewYearly.Rows[r].Cells[c].Value = log1;
                }
            }
        }

        //private void dataGridViewYearly_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    int r = dataGridViewYearly.CurrentCell.RowIndex;
        //    int c = dataGridViewYearly.CurrentCell.ColumnIndex;
        //    log1 = dataGridViewYearly.Rows[r].Cells[c].Value;
        //}

        private void DataGridViewYearly_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = DataGridViewYearly.CurrentCell.RowIndex;
            int c = DataGridViewYearly.CurrentCell.ColumnIndex;
            log1 = DataGridViewYearly.Rows[r].Cells[c].Value;
        }


        #region initial dataGridView
        private void DataGridViewInitial()
        {
            this.DataGridViewYearly.ColumnCount = 5;
            this.DataGridViewYearly.Columns[0].Name = "Year";
            this.DataGridViewYearly.Columns[0].Width = 50;
            this.DataGridViewYearly.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewYearly.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewYearly.Columns[1].Name = "Month";
            this.DataGridViewYearly.Columns[1].Width = 50;
            this.DataGridViewYearly.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewYearly.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewYearly.Columns[2].Name = "CycleTimeAvg";
            this.DataGridViewYearly.Columns[2].Width = 120;
            this.DataGridViewYearly.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewYearly.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewYearly.Columns[3].Name = "STD.Ratio";
            this.DataGridViewYearly.Columns[3].Width = 120;
            this.DataGridViewYearly.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewYearly.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewYearly.Columns[4].Name = "% OA";
            this.DataGridViewYearly.Columns[4].Width = 50;
            this.DataGridViewYearly.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewYearly.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.DataGridViewYearly.RowHeadersWidth = 4;
            this.DataGridViewYearly.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewYearly.RowTemplate.Height = 20;
            this.DataGridViewYearly.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewYearly.RowsDefaultCellStyle.BackColor = Color.White;
            this.DataGridViewYearly.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(169, 235, 187);

            this.DataGridViewNet.ColumnCount = 4;
            this.DataGridViewNet.Columns[0].Name = "No";
            this.DataGridViewNet.Columns[0].Width = 30;
            this.DataGridViewNet.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewNet.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewNet.Columns[1].Name = "Part Number";
            this.DataGridViewNet.Columns[1].Width = 150;
            this.DataGridViewNet.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewNet.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.DataGridViewNet.Columns[2].Name = "NetTime(sec)";
            this.DataGridViewNet.Columns[2].Width = 100;
            this.DataGridViewNet.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewNet.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewNet.Columns[3].Name = "CycleTimeAvg (sec)";
            this.DataGridViewNet.Columns[3].Width = 120;
            this.DataGridViewNet.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewNet.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewNet.RowHeadersWidth = 4;
            this.DataGridViewNet.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewNet.RowTemplate.Height = 20;
            this.DataGridViewNet.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewNet.RowsDefaultCellStyle.BackColor = Color.White;
            this.DataGridViewNet.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(229, 226, 254);
        }

        private void Roles()
        {
            BtnStdNew.Enabled = false;
            BtnStdSaveNew.Enabled = false;
            BtnStdRead.Enabled = false;
            BtnStdSave.Enabled = false;

           
            BtnNetlSave.Enabled = false;
            BtnNetDel.Enabled = false;
            BtnNetClear.Enabled = false;
            BtnNetRead.Enabled = false;
            BtnNetImportExcel.Enabled = true;

            if (User.Role ==Models.Roles.Invalid) // invalid
            {
                BtnStdRead.Enabled = true;
                BtnNetRead.Enabled = true;
            }
            else if (User.Role ==Models.Roles.General) // General
            {
                BtnStdRead.Enabled = true;
                BtnNetRead.Enabled = true;
            }
            else if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager) // production
            {
                BtnStdRead.Enabled = true;
                BtnNetRead.Enabled = true;
            }
            else if (User.Role ==Models.Roles.FacEng) // Fac eng
            {
                BtnStdNew.Enabled = true;
                BtnStdSaveNew.Enabled = true;
                BtnStdRead.Enabled = true;
                BtnStdSave.Enabled = true;

           
                BtnNetlSave.Enabled = true;
                BtnNetDel.Enabled = true;
                BtnNetClear.Enabled = true;
                BtnNetRead.Enabled = true;
                BtnNetImportExcel.Enabled = true;

            }
            else if (User.Role ==Models.Roles.Admin_Min) // Admin-mini
            {
                BtnStdNew.Enabled = true;
                BtnStdSaveNew.Enabled = true;
                BtnStdRead.Enabled = true;
                BtnStdSave.Enabled = true;

            
                BtnNetlSave.Enabled = true;
                BtnNetDel.Enabled = true;
                BtnNetClear.Enabled = true;
                BtnNetRead.Enabled = true;
                BtnNetImportExcel.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {

                BtnStdNew.Enabled = true;
                BtnStdSaveNew.Enabled = true;
                BtnStdRead.Enabled = true;
                BtnStdSave.Enabled = true;

            
                BtnNetlSave.Enabled = true;
                BtnNetDel.Enabled = true;
                BtnNetClear.Enabled = true;
                BtnNetRead.Enabled = true;
                BtnNetImportExcel.Enabled = true;

            }
        }

        #endregion




        private void ReadSTDTime()
        {
            string section = ((KeyValuePair<string, string>)cmbSectionCode.SelectedItem).Key;
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.Yearly_LoadStandardRecordSQL(section, cmbYear.Text);
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                DataGridViewYearly.Rows.Clear();
                if (dt1.Rows.Count > 0)
                {

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string[] row = new string[dt1.Rows[i].ItemArray.Length];
                        for (int j = 0; j < dt1.Rows[i].ItemArray.Length; j++)
                        {
                            row[j] = Convert.ToString(dt1.Rows[i].ItemArray[j]);
                        }
                        DataGridViewYearly.Rows.Add(row);
                    }
                }
                memoCellChange.Clear();
            }
        }



        #endregion

        #region RIGHT SIDE   ***************************************************

        private void BtnNetlSaveReplace_Click(object sender, EventArgs e)
        {
            if (DataGridViewNet.Rows.Count > 0)
            {
                if (CheckDataTable())
                {
                    SaveNetTime();
                }
            }
        }

        private void BtnNetDel_Click(object sender, EventArgs e)
        {
            DeleteNetTime();
        }

        private void BtnNetRead_Click(object sender, EventArgs e)
        {
            NetTimeRead();
        }

        private void BtnNetImportExcel_Click(object sender, EventArgs e)
        {
            ImmportExcel();
        }

        private void BtnNetClear_Click(object sender, EventArgs e)
        {
            DataGridViewNet.Rows.Clear();
        }

        //private void dataGridViewNet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{

        //}


        private void DataGridViewNet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle styleChange = new DataGridViewCellStyle
            {
                ForeColor = Color.Blue
            };
            int r = DataGridViewNet.CurrentRow.Index;
            int c = DataGridViewNet.CurrentCell.ColumnIndex;
            string pn = Convert.ToString(DataGridViewNet.Rows[r].Cells[c].Value);

            if (c == 1)
            {
                if (pn.Length >= 12)
                {
                    styleChange.ForeColor = Color.Red;
                    DataGridViewNet.Rows[r].Cells[c].Style = styleChange;
                }
                else
                {
                    DataGridViewNet.Rows[r].Cells[c].Value = log;
                }

            }
            else if (c > 1)
            {
                bool isnumeric = Converting.IsNumeric(pn);  //double.TryParse(pn, out double n);
                if (isnumeric == false)
                {

                    MessageBox.Show("Please fill only numberical", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DataGridViewNet.Rows[r].Cells[c].Value = log;
                    return;
                }
                else if (isnumeric == true)
                {
                    styleChange.ForeColor = Color.Red;
                    DataGridViewNet.Rows[r].Cells[c].Style = styleChange;
                }
            }

        }



        private bool CheckDataTable()
        {
            List<string> memo = new List<string>();
            memo.Clear();
            int i = 0;
            int row = DataGridViewNet.Rows.Count - 1;
            //bool result = row == 0 ? false:true;
            bool result = row != 0; // ? false : true;
            while (i<row)
            {
                if (DataGridViewNet.Rows[i].Cells[1].Value != null)
                {
                    string tg = DataGridViewNet.Rows[i].Cells[1].Value.ToString();
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
                }
                else
                {
                    break;
                }
                i++;
            }
            return result;
        }

        private void NetTimeRead()
        {
            string Section = ((KeyValuePair<string, string>)cmbSectionCode.SelectedItem).Key;
            DataGridViewNet.Rows.Clear();
            NetDic2.Clear();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.Yearly_LoadNetTimeStandardSQL(Section, cmbYear.Text);
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                int i = 1;
                foreach (DataRow dr in dt1.Rows)
                {
                    DataGridViewNet.Rows.Add(i.ToString(), dr.ItemArray[1], dr.ItemArray[2], dr.ItemArray[3]);
                    NetDic2.Add(dr.ItemArray[0], dr.ItemArray[1]);
                    i++;
                }
            }

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
            //textBox3.Text = openFileDialog1.FileName;
            openFileDialog1.Multiselect = false;
            //textBox3.ReadOnly = true;

            if (File.Exists(openFileDialog1.FileName) == true)
            {
                FileInfo excelFile = new FileInfo(openFileDialog1.FileName);
                ExcelPackage excel = new ExcelPackage(excelFile);
                List<string> partnumber = new List<string>();
                DataGridViewYearly.Rows.Clear();
                NetDic2.Clear();
                var worksheet = excel.Workbook.Worksheets["Sheet1"];
                int i = 2;
                int rows = 1;
                bool status;// = false;
                bool status0;// = false;
                do
                {

                    string pn = Convert.ToString(worksheet.Cells[i, 1].Value);
                    //bool status3 = (pn.Length > 5) ? true : false;
                    bool status3 = (pn.Length > 5);
                    status0 = status3;
                    string nt = "0";
                    bool status4 = false;
                    string ct = "0";
                    bool status5 = false;
                    try
                    {
                        double ntime = Convert.ToDouble(worksheet.Cells[i, 2].Value);
                        nt = ntime.ToString();
                        status4 = true;
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        double ctime = Convert.ToDouble(worksheet.Cells[i, 3].Value);
                        ct = ctime.ToString();
                        status5 = true;
                    }
                    catch (Exception)
                    {

                    }
                    status = status3 & status4 & status5;
                    if (status == true)
                    {
                        if (partnumber.Contains(pn) == false)
                            DataGridViewNet.Rows.Add(rows.ToString(), pn, nt, ct);
                        partnumber.Add(pn);
                        rows++;
                    }

                    i += 1;

                }
                while (status0);

            }
        }

        private void DeleteNetTime()
        {
            if (DataGridViewNet.Rows.Count > 1)
            {
                int r = DataGridViewNet.CurrentRow.Index;
                DataGridViewNet.Rows.RemoveAt(r);
            }
        }

        private void SaveNetTime()
        {
            string section = ((KeyValuePair<string, string>)cmbSectionCode.SelectedItem).Key;
            //string year = cmbYear.Text;
            DialogResult r = MessageBox.Show("Are you sure to replace all the part number", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (r == DialogResult.OK)
            {
                SqlClass sql = new SqlClass();
                bool sqlsttatus = sql.Yearly_SaveAllNetTimeSQL(section, cmbYear.Text, DataGridViewNet);
                if (sqlsttatus)
                {
                    MessageBox.Show("Replace all part number Completed ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NetTimeRead();
                }

            }
        }



        #endregion

        //private void dataGridViewNet_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    int r = DataGridViewNet.CurrentCell.RowIndex;
        //    int c = DataGridViewNet.CurrentCell.ColumnIndex;
        //    log = DataGridViewNet.Rows[r].Cells[c].Value;
          
        //}

        private void DataGridViewNet_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int r = DataGridViewNet.CurrentCell.RowIndex;
            int c = DataGridViewNet.CurrentCell.ColumnIndex;
            log = DataGridViewNet.Rows[r].Cells[c].Value;

        }
    }
}
