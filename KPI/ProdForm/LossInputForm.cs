using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using KPI.Parameter;
using System.Threading.Tasks;
using KPI.Class;
using System.Windows.Forms;
using KPI.Models;

namespace KPI.ProdForm
{
    public partial class LossInputForm : Form
    {
        readonly Dictionary<string, string> LossDic = new Dictionary<string, string>();
        readonly Dictionary<int, string> RecordDic = new Dictionary<int, string>();
        readonly List<string> LossList = new List<string>();
        readonly Dictionary<string, string> MCDic = new Dictionary<string, string>();
        readonly Dictionary<string, string> MClossDetailDic = new Dictionary<string, string>();

        public LossInputForm()
        {
            InitializeComponent();
            InitialDataGridViewList();
            InitialDataGridViewDetail();
            InitialDataGridViewHistory();
        }

        private void DataGridViewHistory_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 197, 197),
                ForeColor = Color.Black
            };
            DataGridViewCellStyle styleNormal = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 255, 255),
                ForeColor = Color.Black
            };


            int row = DataGridViewHistory.Rows.Count;
            int col = DataGridViewHistory.Columns.Count;

            if (row > 0)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        DataGridViewHistory.Rows[i].Cells[j].Style = styleNormal;
                    }
                }
                int rowc = DataGridViewHistory.CurrentRow.Index;
                for (int j = 0; j < col; j++)
                {
                    DataGridViewHistory.Rows[rowc].Cells[j].Style = style;
                }

                MClossDetailDic.Clear();
                cmbLossDetail.Items.Clear();
                string mcID = DataGridViewHistory.Rows[rowc].Cells[12].Value.ToString();
                if (mcID != null && mcID != "")
                {
                    string lossId = DataGridViewHistory.Rows[rowc].Cells[2].Value.ToString();
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.Loss_MachineLossDetailSQL(mcID, lossId);
                    if (sqlstatus)
                    {
                        DataTable dt = sql.Datatable;
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                MClossDetailDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                                cmbLossDetail.Items.Add(dr.ItemArray[1].ToString());
                            }
                            cmbLossDetail.SelectedIndex = 0;

                        }
                    }
                    else
                    {
                        cmbLossDetail.SelectedIndex = -1;
                    }
                }
            }
        
        }









        private void LossInputForm_Load(object sender, EventArgs e)
        {
            LoadLossItemTable();
            UpdateReport();
            comboBoxShift.SelectedIndex = 0;
            Roles();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteRecordTB();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (DataGridViewDetail.Rows.Count > 0)
            {
                SaveLossRecordTable();
            }
            else
            {
                MessageBox.Show(new Form() { TopMost = true }, "Please select detail loss before SAVE ! \n  if no items , registraion now.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }




        private void DeleteRecordTB()
        {
            if (DataGridViewHistory.RowCount > 0)
            {
                int key = DataGridViewHistory.CurrentRow.Index;
                string id = RecordDic.FirstOrDefault(x => x.Key == key).Value;
                if (id != null)
                {
                    string str = String.Format("เครื่องจะทำการลบ loss No. {0}", key + 1);
                    DialogResult r = MessageBox.Show(str, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (DialogResult.OK == r)
                    {
                        SqlClass sql = new SqlClass();
                        bool sqlstatus = sql.Loss_DeleteRecordSQL(id);
                        UpdateReport();
                    }
                }
            }
        }



        private void SaveLossRecordTable()
        {
            DateTime dt = dateTimePicker1.Value;
            string shift = comboBoxShift.Text;
            int yy = dt.Year;
            int MM = dt.Month;
            int dd = dt.Day;
            int HH = Convert.ToInt32(cmbHH.Text);
            int mm = Convert.ToInt32(cmbMM.Text);
            DateTime lossOccuredTime = new DateTime(yy, MM, dd, HH, mm, 00);
            string DateStartTime = lossOccuredTime.ToString("yyyy-MM-dd HH:mm:ss");

            string registDate = RegistDateTime.OutDate(lossOccuredTime);
            string sectioncode = User.SectionCode;
            string code = Convert.ToString(DataGridViewList.CurrentRow.Cells[0].Value);

            double mp = 0;
            double hour = 0;
            double minute = 0;
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

            double period = (hour * 3600 + minute * 60);
            if (period == 0)
            {
                MessageBox.Show("กรอก จำนวน เวลา ผิดพลาด", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double totalMH = mp * period; // unit second
            string lossDetailID = String.Empty;
            if (DataGridViewDetail.Rows.Count > 0)
            {
                string lossDetailName = Convert.ToString(DataGridViewDetail.CurrentRow.Cells[1].Value);
                lossDetailID = LossDic.FirstOrDefault(x => x.Value == lossDetailName).Key;
            }
            else
            {
                lossDetailID = "0";
            }
            DateTime dateStopTime = lossOccuredTime.AddSeconds(period);
            string dateStop = dateStopTime.ToString("yyyy-MM-dd HH:mm:ss");

            var query = new StringBuilder();
            query.Append("INSERT INTO [Production].[dbo].[Loss_RecordTable] ([SectionCode],[RegistDate],[ShiftAB],[LossId],[LossDetailId],[DateTimeStart],[DateTimeEnd],[second],[MP],[TotalMS],[divisionId],[plantId]) \n");
            query.AppendFormat($"VALUES('{sectioncode}','{registDate}','{shift}','{code}',{lossDetailID},'{DateStartTime}','{dateStop}',{period},{mp},{totalMH},'{User.Division}','{User.Plant}')");
            var a = query.ToString();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.Loss_SaveLossTimeSQL(query.ToString());
            UpdateReport();

        }



        private void UpdateReport()
        {
            DataGridViewHistory.Rows.Clear();
            RecordDic.Clear();
            DateTime dt = dateTimePicker1.Value;
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            string RegisDate = dt.ToString("yyyy-MM-dd");
            string sectioncode = User.SectionCode;
            bool status = int.TryParse(txtFilter.Text, out int txtfilter);
            txtfilter = status == true ? txtfilter * 60 : 0;
            string lossSelector = string.Empty;
            if (radioAll.Checked == true)
            {
                lossSelector = "A";
            }
            else if (radioGeneral.Checked == true)
            {
                lossSelector = "G";
            }
            else if (radioMcstop.Checked == true)
            {
                lossSelector = "M";
            }

            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SISS("ReadLossExc1", "@StringDate", RegisDate, "@timeLimit", txtfilter, "@section", sectioncode, "@selector", lossSelector);

            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];
                int row = dt1.Rows.Count;
                if (row > 0)
                {
                    double totalLoss = 0;
                    double totalperiod = 0;
                    for (int i = 0; i < row; i++)
                    {
                        string shift = Convert.ToString(dt1.Rows[i].ItemArray[0]);
                        string lossId = Convert.ToString(dt1.Rows[i].ItemArray[1]);
                        string lossdetailname = Convert.ToString(dt1.Rows[i].ItemArray[2]);
                        string mcname = Convert.ToString(dt1.Rows[i].ItemArray[3]);
                        string errorcode = Convert.ToString(dt1.Rows[i].ItemArray[4]);
                        string message = Convert.ToString(dt1.Rows[i].ItemArray[5]);
                        string period = Convert.ToString(dt1.Rows[i].ItemArray[6]);
                        string datestart = Convert.ToString(dt1.Rows[i].ItemArray[7]);
                        string datestop = Convert.ToString(dt1.Rows[i].ItemArray[8]);

                        string Mp = Convert.ToString(dt1.Rows[i].ItemArray[9]);
                        string total = Convert.ToString(dt1.Rows[i].ItemArray[10]);
                        string mcId = Convert.ToString(dt1.Rows[i].ItemArray[12]);
                        string run = Convert.ToString(dt1.Rows[i].ItemArray[11]);

                        DataGridViewHistory.Rows.Add((i + 1).ToString(), shift, lossId, lossdetailname, mcname, errorcode, message, period, datestart, datestop, Mp, total, mcId, run);
                        RecordDic.Add(i, run);

                        totalLoss += total != ""? double.Parse(total):0;
                        totalperiod += double.Parse(period);
                    }
                    txtTotal.Text = Math.Round((totalperiod), 2, MidpointRounding.AwayFromZero).ToString();
                }

            }
            cmbHH.SelectedIndex = hh;
            cmbMM.SelectedIndex = mm;

        }




        private void BtnlossEdit_Click(object sender, EventArgs e)
        {
            int row = DataGridViewDetail.CurrentRow.Index;
            string run = DataGridViewDetail.Rows[row].Cells[0].Value.ToString();
            lbId.Text = run;
            cmbMcList.SelectedIndex = 0;
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.Loss_EditDetailLossSQL(run);
            if (sqlstatus)
            {
                DataTable dt = sql.Datatable;
                if (dt.Rows.Count > 0)
                {
                    string mcId = dt.Rows[0].ItemArray[1].ToString();
                    txtNewLoss.Text = dt.Rows[0].ItemArray[0].ToString();
                    string mcname = MCDic.FirstOrDefault(x => x.Key == mcId).Value;
                    cmbMcList.Text = mcname;
                }

            }
        }

        private void BtnlossUpdate_Click(object sender, EventArgs e)
        {
            string mcname = cmbMcList.Text;
            string mcId = MCDic.FirstOrDefault(x => x.Value == mcname).Key;
            string lossdetailname = txtNewLoss.Text.Trim();
            string run = lbId.Text;
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.Loss_UpdateDetailLossSQL(lossdetailname, mcId, run);
            if (sqlstatus)
            {
                MessageBox.Show("Already updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnlossDel_Click(object sender, EventArgs e)
        {
            DeleteLossDetail();
        }

        private void BtnlossRegistration_Click(object sender, EventArgs e)
        {
            RegistrationLossDetail();
        }




        private void DataGridViewList_Click(object sender, EventArgs e)
        {
            UpdateLossDetail();
        }




        private void UpdateLossDetail()
        {
            DataGridViewDetail.Rows.Clear();
            LossDic.Clear();
            string lossId = Convert.ToString(DataGridViewList.CurrentRow.Cells[0].Value);
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.Loss_UpdateDetailViewSQL(lossId);
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                int row = dt1.Rows.Count;
                if (row > 0)
                {
                    for (int i = 0; i < row; i++)
                    {
                        string id = Convert.ToString(dt1.Rows[i].ItemArray[0]);
                        string name = Convert.ToString(dt1.Rows[i].ItemArray[1]);
                        DataGridViewDetail.Rows.Add(id, name);
                        LossDic.Add(id, name);
                    }

                }
            }
        }


        private void DeleteLossDetail()
        {
            int index2 = DataGridViewDetail.Rows.Count;
            if (index2 > 0)
            {
                string lossId = Convert.ToString(DataGridViewList.CurrentRow.Cells[0].Value);
                string lossDetailName = Convert.ToString(DataGridViewDetail.CurrentRow.Cells[1].Value);
                //string lossDetailId = LossDic.FirstOrDefault(x => x.Value == lossDetailName).Key;
                string lossDetailId = Convert.ToString(DataGridViewDetail.CurrentRow.Cells[0].Value);
                string show = String.Format("ยืนยัน loss detail ที่จะถูกลบทิ้งอย่างถาวร => \n {0}", lossDetailName);
                DialogResult r = MessageBox.Show(show, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (DialogResult.OK == r)
                {
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.Loss_DeleteLossDetailNameSQL(lossId, lossDetailId);
                    if (sqlstatus)
                    {
                        UpdateLossDetail();
                    }

                }
            }
            else
            {
                return;
            }
        }


        private void RegistrationLossDetail()
        {
            string lossId = Convert.ToString(DataGridViewList.CurrentRow.Cells[0].Value);
            string lossDetailName = txtNewLoss.Text;
            if (lossDetailName == String.Empty)
            {
                MessageBox.Show("ยังไม่ได้กรอกข้อความ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string show = String.Format("ชื่อ loss detail ตัวใหม่ที่จะถูกบันทึก => \n {0}", lossDetailName);
            string a = LossDic.FirstOrDefault(x => x.Value == lossDetailName).Key;
            if (a == null)
            {
                DialogResult r = MessageBox.Show(show, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (DialogResult.OK == r)
                {
                    string mcId = MCDic.FirstOrDefault(x => x.Value == cmbMcList.Text).Key;
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.Loss_InsertLossDetailNameSQL(lossId, lossDetailName, mcId);
                    UpdateLossDetail();
                    txtNewLoss.Text = String.Empty;
                }
            }
            else
            {
                MessageBox.Show("กรอกข้อความ ซ้ำ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }



        #region initial dataGridView and Role
        private void InitialDataGridViewList()
        {
            this.DataGridViewList.ColumnCount = 2;
            this.DataGridViewList.Columns[0].Name = "No";
            this.DataGridViewList.Columns[0].Width = 70;
            this.DataGridViewList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewList.Columns[1].Name = "Loss Name";
            this.DataGridViewList.Columns[1].Width = 500;
            this.DataGridViewList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewList.RowHeadersWidth = 30;
            this.DataGridViewList.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.DataGridViewList.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.DataGridViewList.RowHeadersWidth = 4;
            this.DataGridViewList.RowTemplate.Height = 30;
            this.DataGridViewList.RowsDefaultCellStyle.BackColor = Color.White;
            this.DataGridViewList.AlternatingRowsDefaultCellStyle.BackColor = Color.PowderBlue;
            DataGridViewList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DataGridViewList.AllowUserToResizeRows = false;
            DataGridViewList.AllowUserToResizeColumns = false;
        }

        private void InitialDataGridViewDetail()
        {
            this.DataGridViewDetail.ColumnCount = 2;
            this.DataGridViewDetail.Columns[0].Name = "ID";
            this.DataGridViewDetail.Columns[0].Width = 50;
            this.DataGridViewDetail.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewDetail.Columns[1].Name = "Loss detail";
            this.DataGridViewDetail.Columns[1].Width = 250;
            this.DataGridViewDetail.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.DataGridViewDetail.RowHeadersWidth = 30;
            this.DataGridViewDetail.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.DataGridViewDetail.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.DataGridViewDetail.RowHeadersWidth = 4;
            this.DataGridViewDetail.RowTemplate.Height = 30;
            this.DataGridViewDetail.RowsDefaultCellStyle.BackColor = Color.White;
            this.DataGridViewDetail.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;
            DataGridViewDetail.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DataGridViewDetail.AllowUserToResizeRows = false;
            DataGridViewDetail.AllowUserToResizeColumns = false;
        }

        private void InitialDataGridViewHistory()
        {
            this.DataGridViewHistory.ColumnCount = 14;
            this.DataGridViewHistory.Columns[0].Name = "No";
            this.DataGridViewHistory.Columns[0].Width = 50;
            this.DataGridViewHistory.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[1].Name = "Shift";
            this.DataGridViewHistory.Columns[1].Width = 30;
            this.DataGridViewHistory.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[2].Name = "Loss No";
            this.DataGridViewHistory.Columns[2].Width = 60;
            this.DataGridViewHistory.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[3].Name = "Loss detail name";
            this.DataGridViewHistory.Columns[3].Width = 150;
            this.DataGridViewHistory.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[4].Name = "M/C Name";
            this.DataGridViewHistory.Columns[4].Width = 150;
            this.DataGridViewHistory.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[5].Name = "ErrorCode";
            this.DataGridViewHistory.Columns[5].Width = 50;
            this.DataGridViewHistory.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[6].Name = "Alarm message";
            this.DataGridViewHistory.Columns[6].Width = 250;
            this.DataGridViewHistory.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[7].Name = "Period (min)";
            this.DataGridViewHistory.Columns[7].Width = 50;
            this.DataGridViewHistory.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[8].Name = "Occure start";
            this.DataGridViewHistory.Columns[8].Width = 150;
            this.DataGridViewHistory.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[9].Name = "Occure stop";
            this.DataGridViewHistory.Columns[9].Width = 150;
            this.DataGridViewHistory.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[10].Name = "MP";
            this.DataGridViewHistory.Columns[10].Width = 30;
            this.DataGridViewHistory.Columns[10].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[11].Name = "Total (min)";
            this.DataGridViewHistory.Columns[11].Width = 100;
            this.DataGridViewHistory.Columns[11].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.Columns[12].Name = "mcId";
            this.DataGridViewHistory.Columns[12].Width = 200;
            this.DataGridViewHistory.Columns[12].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.DataGridViewHistory.Columns[13].Name = "run";
            this.DataGridViewHistory.Columns[13].Width = 50;
            this.DataGridViewHistory.Columns[13].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewHistory.RowHeadersWidth = 20;
            this.DataGridViewHistory.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewHistory.RowHeadersWidth = 4;
            this.DataGridViewHistory.RowTemplate.Height = 25;
            DataGridViewHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DataGridViewHistory.AllowUserToResizeRows = false;
            DataGridViewHistory.AllowUserToResizeColumns = false;
        }

        private void Roles()
        {
            BtnDelete.Enabled = false;
            BtnRefresh.Enabled = false;
            BtnSave.Enabled = false;
            //btnRegisDelete.Enabled = false;
            //btnRegister.Enabled = false;
            if (User.Role == Models.Roles.Invalid) // invalid
            {

            }
            else if (User.Role == Models.Roles.General) // General
            {
                BtnRefresh.Enabled = true;
            }
            else if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager) // production
            {
                BtnDelete.Enabled = true;
                BtnRefresh.Enabled = true;
                BtnSave.Enabled = true;
                //btnRegisDelete.Enabled = true;
                //btnRegister.Enabled = true;
            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {
                BtnRefresh.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Min) // Admin-mini
            {
                BtnDelete.Enabled = true;
                BtnRefresh.Enabled = true;
                BtnSave.Enabled = true;
                //btnRegisDelete.Enabled = true;
                //btnRegister.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnDelete.Enabled = true;
                BtnRefresh.Enabled = true;
                BtnSave.Enabled = true;
                //btnRegisDelete.Enabled = true;
                //btnRegister.Enabled = true;
            }
        }

        #endregion



        private void LoadLossItemTable()
        {
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.Loss_LoadItemSQL();
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string[] row = new string[dt1.Rows[i].ItemArray.Length];
                        for (int j = 0; j < dt1.Rows[i].ItemArray.Length; j++)
                        {
                            row[j] = dt1.Rows[i].ItemArray[j].ToString();
                        }

                        DataGridViewList.Rows.Add(row);
                        LossList.Add(row[0]);
                    }
                }
                if (dt2.Rows.Count > 0)
                {
                    MCDic.Clear();
                    cmbMcList.Items.Add("N/A");
                    MCDic.Add("", "N/A");
                    foreach (DataRow dr in dt2.Rows)
                    {
                        cmbMcList.Items.Add(dr.ItemArray[1].ToString());
                        MCDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                    }
                    cmbMcList.SelectedIndex = 0;
                }
            }

        }

        private void BtnEditSave_Click(object sender, EventArgs e)
        {
            if (cmbLossDetail.SelectedIndex > -1)
            {
                DialogResult r = MessageBox.Show("Are you sure", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == r)
                {
                    int rowc = DataGridViewHistory.CurrentRow.Index;
                    string run = DataGridViewHistory.Rows[rowc].Cells[13].Value.ToString();
                    string lossDetailID = MClossDetailDic.FirstOrDefault(x => x.Value == cmbLossDetail.Text).Key;
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.Loss_UpdateMachineLossSQL(run, lossDetailID);
                    if (sqlstatus)
                    {
                        MessageBox.Show("Update Loss detail Completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateReport();
                    }
                    else
                    {
                        MessageBox.Show("Update Loss Failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose Loss detail before SAVE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
