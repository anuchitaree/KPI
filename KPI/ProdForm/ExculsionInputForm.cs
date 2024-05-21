using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPI.Parameter;
using System.Windows.Forms;
using KPI.Class;
using KPI.Models;
using System.IO;


namespace KPI.ProdForm
{
    public partial class ExculsionInputForm : Form
    {
        readonly Dictionary<int, string> RecordDic = new Dictionary<int, string>();
        readonly Dictionary<string, string> ExcList = new Dictionary<string, string>();
        public ExculsionInputForm()
        {
            InitializeComponent();
            InitialDataGridView();
            InitialDataGridView3();
        }

        #region Event Tab 1
        private void BtnDel_Click(object sender, EventArgs e)
        {
            DeleteRecordTB();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string id = FindExcID();
            if (id == string.Empty)
            {
                return;
            }
            else
            {
                Save(id);
            }
        }


        private void ExculsionInputForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            cmbstyle.SelectedIndex = 0;
            LoadMonthlyData();
            UpdateReport();
            Roles();
        }

        private void DateTimePickerOccured_ValueChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }


        #endregion

        //////    LOOP OPRATION   //////////////////
        
        #region initial dataGridView
        private void InitialDataGridView()
        {
            this.dataGridView2.ColumnCount = 2;
            this.dataGridView2.Columns[0].Name = "No";
            this.dataGridView2.Columns[0].Width = 40;
            this.dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[1].Name = "Exclusion Name";
            this.dataGridView2.Columns[1].Width = 400;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.RowHeadersWidth = 4;
            this.dataGridView2.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 197, 197);
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.AllowUserToResizeColumns = false;

        }

        private void InitialDataGridView3()
        {
            this.dataGridView3.ColumnCount = 10;
            this.dataGridView3.Columns[0].Name = "No";
            this.dataGridView3.Columns[0].Width = 40;
            this.dataGridView3.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.Columns[1].Name = "Shift";
            this.dataGridView3.Columns[1].Width = 50;
            this.dataGridView3.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.Columns[2].Name = "Style";
            this.dataGridView3.Columns[2].Width = 50;
            this.dataGridView3.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.Columns[3].Name = "Code";
            this.dataGridView3.Columns[3].Width = 50;
            this.dataGridView3.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.Columns[4].Name = "Exclusion Name";
            this.dataGridView3.Columns[4].Width = 200;
            this.dataGridView3.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.Columns[5].Name = "MP";
            this.dataGridView3.Columns[5].Width = 40;
            this.dataGridView3.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.Columns[6].Name = "Period (min)";
            this.dataGridView3.Columns[6].Width = 50;
            this.dataGridView3.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.Columns[7].Name = "Total (min)";
            this.dataGridView3.Columns[7].Width = 60;
            this.dataGridView3.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.Columns[8].Name = "Decription";
            this.dataGridView3.Columns[8].Width = 300;
            this.dataGridView3.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.Columns[9].Name = "Recordtime";
            this.dataGridView3.Columns[9].Width = 200;
            this.dataGridView3.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView3.RowHeadersWidth = 20;
            this.dataGridView3.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView3.RowHeadersWidth = 4;
            this.dataGridView3.RowTemplate.Height = 25;
            this.dataGridView3.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSalmon;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView3.AllowUserToResizeRows = false;
            dataGridView3.AllowUserToResizeColumns = false;

        }
        #endregion

        #region Operation Loop
        private void Roles()
        {
            BtnDel.Enabled = false;
            BtnRefresh.Enabled = false;
            BtnSave.Enabled = false;
            if (User.Role == Models.Roles.Invalid) // invalid
            {

            }
            else if (User.Role == Models.Roles.General) // General
            {
                BtnRefresh.Enabled = true;
            }
            else if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager)  // production
            {
                BtnDel.Enabled = true;
                BtnRefresh.Enabled = true;
                BtnSave.Enabled = true;
            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {
                BtnRefresh.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Min) // Admin-mini
            {
                BtnDel.Enabled = true;
                BtnRefresh.Enabled = true;
                BtnSave.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnDel.Enabled = true;
                BtnRefresh.Enabled = true;
                BtnSave.Enabled = true;
            }
        }


        private void LoadMonthlyData()
        {
            SqlClass sql = new SqlClass();
            if (sql.Exclusion_LoadExclusionNameSQL())
            {
                DataTable dt1 = sql.Datatable;
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string row0 = dt1.Rows[i].ItemArray[0].ToString();
                        string row1 = dt1.Rows[i].ItemArray[1].ToString();

                        dataGridView2.Rows.Add(row0, row1);
                        ExcList.Add(row0, row1);

                    }
                }
            }

        }

        private void Save(string id)
        {
            DateTime dt = DateTimePickerOccured.Value;
            string RegisDate = dt.ToString("yyyy-MM-dd");
            //int yy = dt.Year;
            //int MM = dt.Month;
            //int dd = dt.Day;
            //int HH = Convert.ToInt32(cmbHH.Text);
            //int mm = Convert.ToInt32(cmbMM.Text);
            //DateTime dtNew = new DateTime(yy, MM, dd, HH, mm, 00);

            //string DateStartTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            string sectioncode = User.SectionCode;
            string decription = textBox2.Text;
            //string daynight = CommonDefine.DayNight;
            double mp;// = 0;
            double hour;// = 0;
            double minute;// = 0;
            string shift = comboBox1.SelectedIndex == 0 ? "A" : "B";

            try
            {
                mp = double.Parse(txtMP.Text);

            }
            catch (Exception)
            {
                MessageBox.Show("กรุุณา กรอกจำนวนคน เป็น ตัวเลข เท่านั้น", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                hour = double.Parse(txtHour.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("กรุุณา กรอกจำนวนเวลาชั่วโมง เป็น ตัวเลข เท่านั้น", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                minute = double.Parse(txtMinute.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("กรุุณา กรอกจำนวนเวลานาที เป็น ตัวเลข เท่านั้น", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double period = (hour * 60 + minute);
            if (period == 0)
            {
                MessageBox.Show("กรอก จำนวน เวลา ผิดพลาด", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double difftime = mp * period; // unit second

            string workstyle = cmbstyle.SelectedIndex == 0 ? "N" : "O";
            string dat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var query = new StringBuilder();
            query.Append("insert into [Production].[dbo].[Exclusion_RecordTable] ([SectionCode],[RegistDate],[ShiftAB],[Workstyle],[ExclusionId],");
            query.AppendFormat("[MP],[minute],[TotalMH],[RecordTime],[Decription],[divisionId],[plantId])");
            query.AppendFormat($" values('{sectioncode}','{RegisDate}','{shift}','{workstyle}','{id}',");
            query.AppendFormat($"{mp},{period},{difftime},'{dat}','{decription}','{User.Division}','{User.Plant}')");
            //var a = query.ToString();
            var sql = new SqlClass();
            sql.Exclusion_SaveExclusionTimeSQL(query.ToString());

            txtMP.Text = String.Empty;
            txtHour.Text = txtMinute.Text = "0";
            UpdateReport();

        }

        private void UpdateReport()
        {
            dataGridView3.Rows.Clear();
            RecordDic.Clear();
            DateTime dt = DateTimePickerOccured.Value;
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            cmbHH.SelectedIndex = hh;
            cmbMM.SelectedIndex = mm;
            string RegisDate = dt.ToString("yyyy-MM-dd");
            string sectiondivplant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SS("ExclusionTimeReadExc", "@RegistDate", RegisDate, "@sectiondivplant", sectiondivplant);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    double totalExc = 0;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string shift = Convert.ToString(dt1.Rows[i].ItemArray[0]);
                        string daynight = Convert.ToString(dt1.Rows[i].ItemArray[1]);
                        string code = Convert.ToString(dt1.Rows[i].ItemArray[2]);
                        string codename = Convert.ToString(dt1.Rows[i].ItemArray[3]);
                        string mp = Convert.ToString(dt1.Rows[i].ItemArray[4]);
                        string minute = Convert.ToString(dt1.Rows[i].ItemArray[5]);
                        string total = Convert.ToString(dt1.Rows[i].ItemArray[6]);
                        string dec = Convert.ToString(dt1.Rows[i].ItemArray[7]);
                        string rectime = Convert.ToString(dt1.Rows[i].ItemArray[8]);
                        dataGridView3.Rows.Add((i + 1).ToString(), shift, daynight, code, codename, mp, minute, total, dec, rectime);
                        totalExc += double.Parse(total);


                        string run = Convert.ToString(dt1.Rows[i].ItemArray[9]);
                        RecordDic.Add(i, run);
                    }
                    txtTotal.Text = Math.Round((totalExc / 60), 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    txtTotal.Text = string.Empty;
                }
            }
        }

        private string FindExcID()
        {
            string result = string.Empty;

            int row = dataGridView2.CurrentRow.Index;
            int col = dataGridView2.CurrentCell.ColumnIndex;

            if (col < 2)
            {
                col = 0;
            }
            else if (col > 2)
            {
                col = 3;
            }
            string id = Convert.ToString(dataGridView2.Rows[row].Cells[col].Value);
            bool rs = ExcList.ContainsKey(id);
            if (rs == true)
            {
                return id;
            }

            return result;
        }


        private void DeleteRecordTB()
        {
            if (dataGridView3.RowCount > 0)
            {
                int key = dataGridView3.CurrentRow.Index;
                string id = RecordDic.FirstOrDefault(x => x.Key == key).Value;
                if (id != null)
                {
                    string str = String.Format("เครื่องจะทำการลบ loss No. {0}", key + 1);
                    DialogResult r = MessageBox.Show(str, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (DialogResult.OK == r)
                    {
                        SqlClass sql = new SqlClass();
                        bool sqlstatus = sql.Exclusion_DeleteExclusionTimeRecordSQL(id);
                        UpdateReport();
                    }
                }
            }
        }

        #endregion

    }
}
