using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KPI.Class;
using KPI.Models;
using KPI.Parameter;

namespace KPI.ProdForm
{
    public partial class DefectInputForm : Form
    {
        readonly Dictionary<string, string> reWorkModeDic = new Dictionary<string, string>();
        readonly Dictionary<string, string> EmpADic = new Dictionary<string, string>();
        readonly Dictionary<string, string> EmpBDic = new Dictionary<string, string>();
        readonly List<string> partNumberList = new List<string>();
        readonly Dictionary<string, string> Re_workMethodDic = new Dictionary<string, string>();
        readonly List<string> recoverMPId = new List<string>();
        private int year = 0;

        public string section = User.SectionCode;

        public DefectInputForm()
        {
            InitializeComponent();
            InitialRework();
        }

        private void DefectInputForm_Load(object sender, EventArgs e)
        {
            InitialComponent.DateTimePicker(dateTimePicker3);
            year = DateTime.Now.Year;
            LoadALLTable(year);
            ListDefectRecord();
        }

        #region InitialRework
        private void InitialRework()
        {
            InitialDataGridView1();
            InitialDataGridView2();
            InitialDataGridView4();
            InitialDataGridView5();
            ComboBox();
        }

        private void InitialDataGridView1()
        {
            this.dataGridView1.ColumnCount = 4;
            this.dataGridView1.Columns[0].Name = "No";
            this.dataGridView1.Columns[0].Width = 40;
            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].Name = "Rework Mode";
            this.dataGridView1.Columns[1].Width = 300;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[2].Name = "Description";
            this.dataGridView1.Columns[2].Width = 500;
            this.dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[3].Name = "run";
            this.dataGridView1.Columns[3].Width = 50;
            this.dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowTemplate.Height = 25;
            //this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            //this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Pink;

        }

        private void InitialDataGridView2()
        {
            this.dataGridView2.ColumnCount = 14;

            this.dataGridView2.Columns[0].Name = "No";
            this.dataGridView2.Columns[0].Width = 40;
            this.dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[1].Name = "RegistDate";
            this.dataGridView2.Columns[1].Width = 100;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[2].Name = "Occured date";
            this.dataGridView2.Columns[2].Width = 150;
            this.dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[3].Name = "Shift";
            this.dataGridView2.Columns[3].Width = 50;
            this.dataGridView2.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[4].Name = "Q'ty";
            this.dataGridView2.Columns[4].Width = 50;
            this.dataGridView2.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[5].Name = "Part Number";
            this.dataGridView2.Columns[5].Width = 150;
            this.dataGridView2.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[6].Name = "Rework-Mode";
            this.dataGridView2.Columns[6].Width = 200;
            this.dataGridView2.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[7].Name = "Who discover";
            this.dataGridView2.Columns[7].Width = 150;
            this.dataGridView2.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[8].Name = "When repair";
            this.dataGridView2.Columns[8].Width = 150;
            this.dataGridView2.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[9].Name = "Method repair";
            this.dataGridView2.Columns[9].Width = 200;
            this.dataGridView2.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[10].Name = "Who repair";
            this.dataGridView2.Columns[10].Width = 150;
            this.dataGridView2.Columns[10].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[11].Name = "Remark";
            this.dataGridView2.Columns[11].Width = 300;
            this.dataGridView2.Columns[11].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[12].Name = "New Part";
            this.dataGridView2.Columns[12].Width = 100;
            this.dataGridView2.Columns[12].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[13].Name = "run";
            this.dataGridView2.Columns[13].Width = 50;
            this.dataGridView2.Columns[13].SortMode = DataGridViewColumnSortMode.NotSortable;


            this.dataGridView2.RowHeadersWidth = 30;
            this.dataGridView2.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView2.RowHeadersWidth = 4;
            this.dataGridView2.RowTemplate.Height = 30;
            //this.dataGridView2.RowsDefaultCellStyle.BackColor = Color.White;
            //this.dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;
        }


        private void InitialDataGridView4()
        {

            this.dataGridView4.ColumnCount = 4;
            this.dataGridView4.Columns[0].Name = "No";
            this.dataGridView4.Columns[0].Width = 40;
            this.dataGridView4.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView4.Columns[1].Name = "Recovery method/ Sub part number";
            this.dataGridView4.Columns[1].Width = 300;
            this.dataGridView4.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView4.Columns[2].Name = "Description";
            this.dataGridView4.Columns[2].Width = 400;
            this.dataGridView4.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView4.Columns[3].Name = "run";
            this.dataGridView4.Columns[3].Width = 50;
            this.dataGridView4.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView4.RowHeadersWidth = 25;
            this.dataGridView4.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView4.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView4.RowHeadersWidth = 4;
            this.dataGridView4.RowTemplate.Height = 25;
            //this.dataGridView4.RowsDefaultCellStyle.BackColor = Color.White;
            //this.dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;

        }

        private void InitialDataGridView5()
        {
            this.dataGridView5.ColumnCount = 4;
            this.dataGridView5.Columns[0].Name = "No";
            this.dataGridView5.Columns[0].Width = 30;
            this.dataGridView5.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView5.Columns[1].Name = "Emp Id";
            this.dataGridView5.Columns[1].Width = 70;
            this.dataGridView5.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView5.Columns[2].Name = "Fullname Lastname";
            this.dataGridView5.Columns[2].Width = 200;
            this.dataGridView5.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView5.Columns[3].Name = "run";
            this.dataGridView5.Columns[3].Width = 5;
            this.dataGridView5.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView5.RowHeadersWidth = 25;
            this.dataGridView5.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView5.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView5.RowHeadersWidth = 4;
            this.dataGridView5.RowTemplate.Height = 25;
            this.dataGridView5.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataGridView5.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;

        }
        private void ComboBox()
        {
            comboBoxShift.SelectedIndex = 0;
            for (int i = 0; i < 60; i++)
            {
                cmbMin.Items.Add(i.ToString().PadLeft(2, '0'));
            }

            DateTime dt = DateTime.Now;
            int hour = dt.Hour;
            int min = dt.Minute;
            cmbHr.Text = hour.ToString().PadLeft(2, '0');
            cmbMin.Text = min.ToString().PadLeft(2, '0');


        }

        //private void ClearInputForm()
        //{
        //    txtQty.Text = string.Empty;
        //    comboBoxPNList.SelectedIndex = -1;
        //    comboBoxDefectMode.SelectedIndex = -1;
        //    cmbDiscover.SelectedIndex = -1;
        //    cmbRe_workMethod.SelectedIndex = -1;
        //    cmbRecoverPerson.SelectedIndex = -1;
        //    comboBoxShift.SelectedIndex = -1;
        //    textBox2.Text = string.Empty;
        //}


        #endregion


        #region Event : TAB 1   

        private void tbDefectMode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                TbDecr1.Focus();
            }
        }
        private void TbDecr1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnDefectModeSave.Focus();
            }
        }


        private void tbRework_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                TbDecr2.Focus();
            }
        }
        private void TbDecr2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnDefectPartSave.Focus();
            }
        }


        private void btnDefectModeDelete_Click(object sender, EventArgs e)
        {
            DeleteDefectMode();
        }
        private void btnDefectModeClear_Click(object sender, EventArgs e)
        {
            tbDefectMode.Text = TbDecr1.Text = string.Empty;
        }
        private void BtnDefectModeSave_Click(object sender, EventArgs e)
        {
            SaveDefectMode();
        }


        private void btnDefectPartDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count > 0)
            {
                DeletePartDefect();
            }
        }
        private void btnDefectPartClear_Click(object sender, EventArgs e)
        {
            tbPartDefect.Text = TbDecr2.Text = string.Empty;
        }
        private void btnDefectPartSave_Click(object sender, EventArgs e)
        {
            SaveDefectPart();
        }



        private void btnDefectRecordDelete_Click(object sender, EventArgs e)
        {
            DeleteDefectRecord();
        }
        private void btnDefectRecordList_Click(object sender, EventArgs e)
        {
            ListDefectRecord();
        }
        private void btnDefectRecordClear_Click(object sender, EventArgs e)
        {
            ClearDefectRecord();
        }
        private void btnDefectRecordSave_Click(object sender, EventArgs e)
        {
            SaveDefectRecord();
        }


        #endregion

        #region Operation : Tab 1

        private void DeleteDefectMode()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int row = dataGridView1.CurrentRow.Index;
                if (row > -1)
                {
                    string msg = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    string str = string.Format("Are you sure to delete \"{0}\" ", msg);
                    DialogResult r = MessageBox.Show(str, "Confrim", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (DialogResult.Yes == r)
                    {
                        string reworkModeName = dataGridView1.Rows[row].Cells[1].Value.ToString();
                        string key = reWorkModeDic.FirstOrDefault(x => x.Value == reworkModeName).Key;
                        SqlClass sql = new SqlClass();
                        bool sqlstatus = sql.ReworkDefectDeleteSQL(key);
                        if (sqlstatus)
                        {
                            LoadDefectMode();
                            MessageBox.Show("Delete completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }


        }
        private void SaveDefectMode()
        {
            string mode = tbDefectMode.Text.Trim();
            string des = TbDecr1.Text.Trim();
            if (mode != "")
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    if (reWorkModeDic.ContainsValue(mode) == true)
                    {
                        MessageBox.Show("Check Re-work MODE keyword again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                DialogResult r = MessageBox.Show("Are you sure", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == r)
                {
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.ReworkDefctSaveSQL(mode, des);
                    if (sqlstatus)
                    {
                        tbDefectMode.Text = TbDecr1.Text = string.Empty;
                        LoadDefectMode();
                        MessageBox.Show("Already save to Defect/Rework mode to dataBase", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
        }
        private void LoadDefectMode()
        {
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ReworkDefectLoadSQL();
            if (sqlstatus)
            {
                DataTable defectTB = sql.Datatable;
                dataGridView1.Rows.Clear();
                reWorkModeDic.Clear();
                //if (defectTB.Rows.Count > 0)
                //{
                int i = 1;
                foreach (DataRow dr in defectTB.Rows)
                {
                    reWorkModeDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());

                    dataGridView1.Rows.Add(i.ToString(), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString(), dr.ItemArray[0].ToString());
                    i++;
                }
                comboBoxDefectMode.DataSource = new BindingSource(reWorkModeDic.Values, null);
                if (reWorkModeDic.Count > 0)
                {
                    comboBoxDefectMode.SelectedIndex = 0;
                }
                else
                {
                    comboBoxDefectMode.SelectedIndex = -1;
                }
                //}
            }
        }


        private void DeletePartDefect()
        {
            if (dataGridView4.Rows.Count > 0)
            {
                string msg = dataGridView4.CurrentRow.Cells[1].Value.ToString();
                string str = string.Format("Are you sure to delete \"{0}\" ", msg);
                DialogResult r = MessageBox.Show(str, "Confrim", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (DialogResult.Yes == r)
                {

                    string recoverId = dataGridView4.CurrentRow.Cells[3].Value.ToString();
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.DeleteRe_WorkMethodSQL(recoverId);
                    if (sqlstatus)
                    {
                        LoadDefectPart();
                        MessageBox.Show("Delete completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void LoadDefectPart()
        {
            dataGridView4.Rows.Clear();
            Re_workMethodDic.Clear();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ReworkRecoverMethodLoadSQL();
            if (sqlstatus)
            {
                DataTable recoverTB = sql.Datatable;
                int i = 1;
                foreach (DataRow dr in recoverTB.Rows)
                {
                    dataGridView4.Rows.Add(i.ToString(), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString(), dr.ItemArray[0].ToString());
                    Re_workMethodDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                    i++;
                }
                cmbRe_workMethod.DataSource = new BindingSource(Re_workMethodDic.Values, null);

                if (Re_workMethodDic.Count > 0)
                {
                    cmbRe_workMethod.SelectedIndex = 0;
                }
                else
                {
                    cmbRe_workMethod.SelectedIndex = -1;
                }
            }
        }
        private void SaveDefectPart()
        {
            string method = tbPartDefect.Text.Trim();
            string des = TbDecr2.Text.Trim();

            if (method != "")
            {
                if (dataGridView4.Rows.Count != 0)
                {
                    if (Re_workMethodDic.ContainsValue(method) == true)
                    {
                        MessageBox.Show("Check Re-work method keyword again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.ReworkRecoverMethodSaveSQL(method, des);
                if (sqlstatus)
                {
                    tbPartDefect.Text = TbDecr2.Text = string.Empty;
                    LoadDefectPart();
                    MessageBox.Show("Already save to Recover method mode to dataBase", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }


        private void DeleteDefectRecord()
        {
            if (dataGridView2.Rows.Count > 0)
            {
                DialogResult r = MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == r)
                {
                    string id = dataGridView2.CurrentRow.Cells[13].Value.ToString();
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.ReworkDeleteLOGSQL(id);
                    if (sqlstatus)
                    {
                        ListDefectRecord();
                        //dataGridView2.Rows.RemoveAt(currRow2);
                    }
                }
            }
        }
        private void ListDefectRecord()
        {
            string startenddate = string.Format("{0}:{1}", dateTimePicker3.Value.ToString("yyyy-MM-dd"), dateTimePicker4.Value.ToString("yyyy-MM-dd"));
            string sectiondivplant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
            dataGridView2.Rows.Clear();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SS("rework_LoadRecordTable", "@betweendate", startenddate, "@sectiondivplant", sectiondivplant);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];
                int row = dt1.Rows.Count;
                if (row > 0)
                {
                    for (int i = 0; i < row; i++)
                    {
                        int col = dt1.Rows[i].ItemArray.Length;
                        string[] obj = new string[col + 1];

                        obj[0] = (i + 1).ToString();
                        DateTime dtt = Convert.ToDateTime(dt1.Rows[i].ItemArray[0]);
                        obj[1] = dtt.ToString("dd-MM-yyyy");
                        dtt = Convert.ToDateTime(dt1.Rows[i].ItemArray[1]);
                        obj[2] = dtt.ToString("dd-MM-yyyy hh:mm:ss");
                        obj[3] = dt1.Rows[i].ItemArray[2].ToString();
                        obj[4] = dt1.Rows[i].ItemArray[3].ToString();
                        obj[5] = dt1.Rows[i].ItemArray[4].ToString();
                        obj[6] = dt1.Rows[i].ItemArray[5].ToString();
                        obj[7] = dt1.Rows[i].ItemArray[6].ToString();
                        obj[8] = dt1.Rows[i].ItemArray[7].ToString();
                        obj[9] = dt1.Rows[i].ItemArray[8].ToString();
                        obj[10] = dt1.Rows[i].ItemArray[9].ToString();
                        obj[11] = dt1.Rows[i].ItemArray[10].ToString();
                        dataGridView2.Rows.Add(obj);
                    }
                }
            }

        }
        private void ClearDefectRecord()
        {
            txtQty.Text = string.Empty;
            comboBoxPNList.SelectedIndex = -1;
            comboBoxDefectMode.SelectedIndex = -1;
            cmbDiscover.SelectedIndex = -1;
            cmbRe_workMethod.SelectedIndex = -1;
            cmbRecoverPerson.SelectedIndex = -1;
            comboBoxShift.SelectedIndex = -1;
            textBox2.Text = string.Empty;
        }
        private void SaveDefectRecord()
        {
            bool input1 = comboBoxShift.SelectedIndex == -1 || comboBoxPNList.SelectedIndex == -1 || comboBoxDefectMode.SelectedIndex == -1 || cmbDiscover.SelectedIndex == -1;
            bool input2 = cmbRe_workMethod.SelectedIndex == -1 || cmbRecoverPerson.SelectedIndex == -1;
            bool status = int.TryParse(txtQty.Text, out int qty);
            if (input1 || input2 || !status)
            {
                return;
            }
            string shift = comboBoxShift.Text;
            string registDate = DateTime.Now.ToString("yyyy-MM-dd");
            string occuredDate = string.Format("{0} {1}:{2}:00", dateTimePicker1.Value.ToString("yyyy-MM-dd"), cmbHr.Text, cmbMin.Text);

            string partnumber = comboBoxPNList.Text;
            string reworkmode = reWorkModeDic.FirstOrDefault(x => x.Value == comboBoxDefectMode.Text).Key;
            string whodiscover = cmbDiscover.Text;


            string rework_when = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string rework_method = Re_workMethodDic.FirstOrDefault(x => x.Value == cmbRe_workMethod.Text).Key;
            KeyValuePair<string, string> select = (KeyValuePair<string, string>)cmbRecoverPerson.SelectedItem;
            string rework_who = select.Key;
            string rework_remark = textBox2.Text;

            var items = checkedListBox1.CheckedItems;
            int count = checkedListBox1.Items.Count;
            bool[] chk = new bool[count];
            for (int i = 0; i < count; i++)
            {
                chk[i] = checkedListBox1.GetItemChecked(i);
            }

            string id = string.Empty;
            if (comboBoxShift.Text == "A")
            {
                id = EmpADic.FirstOrDefault(x => x.Value == whodiscover).Key;
            }
            else if (comboBoxShift.Text == "B")
            {
                id = EmpBDic.FirstOrDefault(x => x.Value == whodiscover).Key;
            }

            var query = new StringBuilder();
            query.AppendFormat(" Insert into [Production].[dbo].[Rework_RecordTable] ([sectionCode] ,[registDate],[occuredDate],[shiftAB] \n");
            query.AppendFormat(" ,[qty] ,[partNumber] ,[reworkModeId],[discoverWhoId] \n");
            query.AppendFormat(" ,[repairWhen],[repairMethodId],[repairWhoId],[repairRemark] \n");
            query.AppendFormat(" ,[newPart] ) VALUES  ");
            query.AppendFormat(" ( '{0}','{1}','{2}','{3}', \n", User.SectionCode, registDate, occuredDate, shift);
            query.AppendFormat("  '{0}','{1}','{2}','{3}',", qty, partnumber, reworkmode, id);
            query.AppendFormat("  '{0}','{1}','{2}','{3}',", rework_when, rework_method, rework_who, rework_remark);
            query.AppendFormat("  '{0}' )", "");

            var q = query.ToString();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ReworkSAVELOGSQL(q);
            if (sqlstatus)
            {
                //Load_ReworkLog();
                ListDefectRecord();
                MessageBox.Show("Save log completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ClearInputForm();
            }

        }

        private void LoadALLTable(int year)
        {
            string sectiondivplant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SS("rework_Preparation1", "@registYear", year.ToString("0000"), "@sectiondivplant", sectiondivplant);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable TBReworkMode = ds.Tables[0];
                DataTable TBrecoverMethod = ds.Tables[1];
                DataTable TBPartNumber = ds.Tables[2];
                DataTable TBEmpA = ds.Tables[3];
                DataTable TBEmpB = ds.Tables[4];

                DataTable TBEmpRecover = ds.Tables[5];
                DataTable TBNewPart = ds.Tables[6];

                //DataTable TBLog = ds.Tables[7];

                dataGridView1.Rows.Clear();
                reWorkModeDic.Clear();
                int i = 1;
                if (TBReworkMode.Rows.Count > 0)
                {
                    foreach (DataRow dr in TBReworkMode.Rows)
                    {
                        reWorkModeDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                        dataGridView1.Rows.Add(i.ToString(), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString(), dr.ItemArray[0].ToString());
                        i++;
                    }
                    comboBoxDefectMode.DataSource = new BindingSource(reWorkModeDic.Values, null);
                    comboBoxDefectMode.SelectedIndex = 0;
                }

                dataGridView4.Rows.Clear();
                Re_workMethodDic.Clear();
                i = 1;
                if (TBrecoverMethod.Rows.Count > 0)
                {
                    foreach (DataRow dr in TBrecoverMethod.Rows)
                    {
                        Re_workMethodDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                        dataGridView4.Rows.Add(i.ToString(), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString(), dr.ItemArray[0].ToString());
                        i++;
                    }
                    cmbRe_workMethod.DataSource = new BindingSource(Re_workMethodDic.Values, null);
                    cmbRe_workMethod.SelectedIndex = 0;
                }

                partNumberList.Clear();
                //PartNumberTB.Rows.Clear();
                if (TBPartNumber.Rows.Count > 0)
                {
                    int n = 0;
                    foreach (DataRow dr in TBPartNumber.Rows)
                    {
                        partNumberList.Add(dr.ItemArray[0].ToString());
                        //PartNumberTB.Rows.Add(n, dr.ItemArray[0].ToString());
                        n++;
                    }
                    comboBoxPNList.DataSource = new BindingSource(partNumberList, null);
                    comboBoxPNList.SelectedIndex = 0;
                }

                EmpADic.Clear();
                if (TBEmpA.Rows.Count > 0)
                {
                    foreach (DataRow dr in TBEmpA.Rows)
                    {
                        EmpADic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                        cmbDiscover.Items.Add(dr.ItemArray[1].ToString());
                    }
                    cmbDiscover.SelectedIndex = 0;
                }
                EmpBDic.Clear();
                if (TBEmpB.Rows.Count > 0)
                {
                    foreach (DataRow dr in TBEmpB.Rows)
                    {
                        EmpBDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                    }
                }

                cmbRecoverPerson.Items.Clear();
                if (TBEmpRecover.Rows.Count > 0)
                {
                    recoverMPId.Clear();
                    List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
                    foreach (DataRow dr in TBEmpRecover.Rows)
                    {
                        data.Add(new KeyValuePair<string, string>(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString()));
                        recoverMPId.Add(dr.ItemArray[0].ToString());
                    }
                    cmbRecoverPerson.DataSource = null;
                    cmbRecoverPerson.Items.Clear();
                    cmbRecoverPerson.DataSource = new BindingSource(data, null);
                    cmbRecoverPerson.DisplayMember = "Value";
                    cmbRecoverPerson.ValueMember = "Key";
                }

                if (TBNewPart.Rows.Count > 0)
                {
                    var items = checkedListBox1.Items;
                    items.Clear();
                    foreach (DataRow dr in TBNewPart.Rows)
                    {
                        string ii = string.Format("{0}:{1}", dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                        items.Add(ii);
                    }


                }


            }


        }



        #endregion

        #region Event : TAB 2
        private void btnSaveRegistPerson_Click(object sender, EventArgs e)
        {
            SaveRegistPerson();
        }
        private void btnLoadRegistPerson_Click(object sender, EventArgs e)
        {
            LoadRegistPerson();
        }

        private void btnDeleteRegistPerson_Click(object sender, EventArgs e)
        {
            DeleteRegistPerson();
            LoadRegistPerson();
        }
        #endregion

        #region Operation : Tab 2
        private void SaveRegistPerson()
        {
            string id = tbId.Text.Trim();
            if (Dict.EmpIDName.ContainsKey(id) && recoverMPId.Contains(id) == false)
            {
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.ReworkRecoverPersonSaveSQL(id);
                if (sqlstatus)
                {
                    MessageBox.Show("Already Save Recover person to DB", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRegistPerson();
                    tbId.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        private void DeleteRegistPerson()
        {
            if (dataGridView5.Rows.Count > 0)
            {
                string run = dataGridView5.CurrentRow.Cells[3].Value.ToString();
                SqlClass sql = new SqlClass();
                bool sqlstatue = sql.ReworkRecoverPersonDeleteSQL(run);
                if (sqlstatue)
                {
                    MessageBox.Show("Complete");

                }
            }
        }
        private void LoadRegistPerson()
        {
            dataGridView5.Rows.Clear();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ReworkRecoverPersonLoadSQL();
            if (sqlstatus)
            {
                DataTable dt = sql.Datatable;
                List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
                recoverMPId.Clear();
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    dataGridView5.Rows.Add((1 + i).ToString(), dr.ItemArray[0], dr.ItemArray[1], dr.ItemArray[2]);
                    data.Add(new KeyValuePair<string, string>(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString()));
                    recoverMPId.Add(dr.ItemArray[0].ToString());
                }
                cmbRecoverPerson.DataSource = null;
                cmbRecoverPerson.Items.Clear();
                cmbRecoverPerson.DataSource = new BindingSource(data, null);
                cmbRecoverPerson.DisplayMember = "Value";
                cmbRecoverPerson.ValueMember = "Key";
            }


        }

        #endregion
    }
}
