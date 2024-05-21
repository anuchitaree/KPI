using KPI.Class;
using KPI.Models;
using KPI.Parameter;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KPI.InitialForm
{
    public partial class MachineFaultCodeForm : Form
    {
        readonly List<string> MCList = new List<string>();
        readonly List<string> stationList = new List<string>();
        readonly Dictionary<string, string> stationDic = new Dictionary<string, string>();
        readonly Dictionary<string, string> MCNamrDict = new Dictionary<string, string>();
        readonly Dictionary<int, int> MCDict = new Dictionary<int, int>();
        readonly List<string> ErrorCodeList = new List<string>();


        public MachineFaultCodeForm()
        {
            InitializeComponent();
        }

        private void MachineFaultCodeForm_Load(object sender, EventArgs e)
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
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.StationGroupReadSQL(User.SectionCode);
            if (sqlstatus)
            {
                stationList.Add("-");
                stationDic.Add("0", "-");
                int k = 1;
                foreach (DataRow dr in sql.Datatable.Rows)
                {
                    stationList.Add(dr.ItemArray[0].ToString());
                    stationDic.Add(k.ToString(), dr.ItemArray[0].ToString());
                    k++;
                }
            }

            DataGridViewInitial();


            Roles();
        }

        #region LEFT =============================================

        private void BtnAllSaveMC_Click(object sender, EventArgs e)
        {
            //SaveUpDateMC();

            if (dataGridViewMC.Rows.Count > 0)
            {
                if (CheckDataTable())
                {
                    MC_MachineNameAdditionalSaveSQL();
                }
            }
        }

        private void BtnDelMC_Click(object sender, EventArgs e)
        {
            if (dataGridViewMC.Rows.Count > 0)
            {
                int r = dataGridViewMC.CurrentRow.Index;
                dataGridViewMC.Rows.RemoveAt(r);
                MCDict.Remove(r);
            }
        }

        private void BtnClearMC_Click(object sender, EventArgs e)
        {
            dataGridViewMC.Rows.Clear();
        }

        //private void buttonReadMC_Click(object sender, EventArgs e)
        //{
        //    MachineNameRead();
        //}

        //private void btnBrowseFileMC_Click(object sender, EventArgs e)
        //{
        //    ImmportExcelMC();
        //}



        private void BtnReadMC_Click(object sender, EventArgs e)
        {
            MachineNameRead();
        }

        private void BtnBrowseFileMC_Click(object sender, EventArgs e)
        {
            ImmportExcelMC();
        }




        #endregion


        ///////////////   OPERATION LOOP //////////////////////////////////////


        #region Initial Component
        private void Roles()
        {
            BtnAllSaveMC.Enabled = false;
            BtnDelMC.Enabled = false;
            BntSaveError.Enabled = false;
            BtnDeleteError.Enabled = false;
            if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager) // production
            {

            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {


            }
            else if (User.Role == Models.Roles.Admin_Min)  // Admin -Mini
            {

                BtnAllSaveMC.Enabled = true;
                BtnDelMC.Enabled = true;


                BntSaveError.Enabled = true;

                BtnDeleteError.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {

                BtnAllSaveMC.Enabled = true;
                BtnDelMC.Enabled = true;


                BntSaveError.Enabled = true;

                BtnDeleteError.Enabled = true;
            }

        }

        private void DataGridViewInitial()
        {

            this.dataGridViewMC.ColumnCount = 5;
            this.dataGridViewMC.Columns[0].Name = "No";
            this.dataGridViewMC.Columns[0].Width = 30;
            this.dataGridViewMC.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewMC.Columns[1].Name = "MachineNumber";
            this.dataGridViewMC.Columns[1].Width = 200;
            this.dataGridViewMC.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewMC.Columns[2].Name = "MachineName";
            this.dataGridViewMC.Columns[2].Width = 300;
            this.dataGridViewMC.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewMC.Columns[3].Name = "Sort";
            this.dataGridViewMC.Columns[3].Width = 50;
            this.dataGridViewMC.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewMC.Columns[4].Name = "StationGroup";
            this.dataGridViewMC.Columns[4].Width = 80;
            this.dataGridViewMC.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            //this.dataGridViewMC.Columns[5].Name = "MTaverg(sec)";
            //this.dataGridViewMC.Columns[5].Width = 100;
            //this.dataGridViewMC.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;

            //DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
            //comboBoxColumn.HeaderText = "StationGroup";
            //comboBoxColumn.Name = "StationGroup";
            //comboBoxColumn.DataSource = new BindingSource(stationList, null);
            //comboBoxColumn.DropDownWidth = 10;
            //comboBoxColumn.Width = 150;
            //comboBoxColumn.FlatStyle = FlatStyle.Flat;
            //comboBoxColumn.DisplayIndex = 6;
            //comboBoxColumn.CellTemplate.Style.BackColor = Color.White;
            //comboBoxColumn.DefaultCellStyle.Font = new Font("Tahoma", 9);
            //dataGridViewMC.Columns.Add(comboBoxColumn);
            //this.dataGridViewMC.AutoResizeColumns();
            //dataGridViewMC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            this.dataGridViewMC.RowHeadersWidth = 4;
            this.dataGridViewMC.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewMC.RowTemplate.Height = 20;
            this.dataGridViewMC.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewMC.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataGridViewMC.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(169, 235, 187);
            dataGridViewMC.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewMC.AllowUserToResizeRows = false;
            dataGridViewMC.AllowUserToResizeColumns = false;



            this.dataGridViewError.ColumnCount = 4;
            this.dataGridViewError.Columns[0].Name = "No";
            this.dataGridViewError.Columns[0].Width = 30;
            this.dataGridViewError.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewError.Columns[1].Name = "Fault Code";
            this.dataGridViewError.Columns[1].Width = 100;
            this.dataGridViewError.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewError.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewError.Columns[2].Name = "Message Alarm";
            this.dataGridViewError.Columns[2].Width = 300;
            this.dataGridViewError.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewError.Columns[3].Name = "Message Decription";
            this.dataGridViewError.Columns[3].Width = 300;
            this.dataGridViewError.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewError.RowHeadersWidth = 4;
            this.dataGridViewError.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewError.RowTemplate.Height = 20;
            this.dataGridViewError.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewError.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataGridViewError.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(229, 226, 254);
            dataGridViewError.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewError.AllowUserToResizeRows = false;
            dataGridViewError.AllowUserToResizeColumns = false;
        }

        #endregion

        #region  SCREEN LEFT SIDE


        private bool CheckDataTable()
        {
            List<string> memo = new List<string>();
            memo.Clear();
            int i = 0;
            int row = dataGridViewMC.Rows.Count - 1;
            bool result = row != 0;
            while (i < row)
            {
                if (dataGridViewMC.Rows[i].Cells[1].Value != null)
                {
                    string tg = dataGridViewMC.Rows[i].Cells[1].Value.ToString();
                    if (memo.Contains(tg) == false)
                    {
                        memo.Add(tg);
                    }
                    else
                    {
                        string error = $"Machine number repeat at row = {i + 1}";

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

        private void MachineNameRead()
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            dataGridViewMC.Rows.Clear();
            MCList.Clear();
            MCDict.Clear();
            MCNamrDict.Clear();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.MC_MahineNameReadSQL(section);
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                int i = 1;
                foreach (DataRow dr in dt1.Rows)
                {
                    dataGridViewMC.Rows.Add(i.ToString(), dr.ItemArray[1], dr.ItemArray[2], dr.ItemArray[3], dr.ItemArray[4]);

                    MCList.Add(dr.ItemArray[1].ToString());
                    MCDict.Add((i - 1), Convert.ToInt32(dr.ItemArray[0]));
                    MCNamrDict.Add(dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString());

                    i++;
                }
                cmbMachineName.DataSource = new BindingSource(MCNamrDict, null);
            }

        }

        private void ImmportExcelMC()
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
                dataGridViewMC.Rows.Clear();
                MCList.Clear();
                var worksheet = excel.Workbook.Worksheets["Sheet1"];
                int i = 2;
                int rows = 1;
                bool status;//= false;
                bool status0;// = false;
                do
                {
                    string MCId = Convert.ToString(worksheet.Cells[i, 1].Value);
                    bool statusMCId = (MCId.Length > 8);
                    status0 = statusMCId;
                    string mcname = Convert.ToString(worksheet.Cells[i, 2].Value);
                    bool statusMCname = mcname.Length > 5;
                    string sort = Convert.ToString(worksheet.Cells[i, 3].Value);
                    bool statusSort = Converting.IsNumeric(sort);  //int.TryParse(sort, out int o);


                    status = statusMCId & statusMCname & statusSort;
                    if (status == true)
                    {
                        if (MCList.Contains(MCId) == false)
                            dataGridViewMC.Rows.Add(rows.ToString(), MCId, mcname, sort);
                        MCList.Add(MCId);
                        rows++;
                    }
                    i += 1;
                }
                while (status0);

            }
        }

        private void MachineErrorCodeRead()
        {
            var index = cmbMachineName.FindStringExact(cmbMachineName.Text);
            if (index > -1)
            {
                string mcIdKey = ((KeyValuePair<string, string>)cmbMachineName.SelectedItem).Key;
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.MachineErrorCodeReadSQL(mcIdKey);
                if (sqlstatus)
                {
                    dataGridViewError.Rows.Clear();
                    DataTable dt = sql.Datatable;
                    if (dt.Rows.Count > 0)
                    {
                        int i = 1;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridViewError.Rows.Add(i.ToString(), dr.ItemArray[0], dr.ItemArray[1], dr.ItemArray[2]);
                            i++;
                        }
                    }
                }
            }


        }

        private void MC_MachineNameAdditionalSaveSQL()
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            DialogResult r = MessageBox.Show("Are you sure to replace all the part number", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (r == DialogResult.OK)
            {
                SqlClass sql = new SqlClass();
                bool sqlsttatus = sql.MC_MahineNameSaveSQL(section, dataGridViewMC);
                if (sqlsttatus)
                {
                    MessageBox.Show("Replace all part number Completed ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }
        #endregion

        #region SCREEN RIGHT SIDE
        //-//////   RIGHT SIDE  /////
       
        private void ErrorCodeSaveReplace()
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            string mcId = ((KeyValuePair<string, string>)cmbMachineName.SelectedItem).Key;
            DialogResult r = MessageBox.Show("Are you sure to replace all the part number", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (r == DialogResult.OK)
            {
                SqlClass sql = new SqlClass();
                bool sqlsttatus = sql.ErrorCodeSaveSQL(section, mcId, dataGridViewError);
                if (sqlsttatus)
                {
                    MessageBox.Show("Replace all part number Completed ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void ImportFaultCodeExcel()
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
                dataGridViewError.Rows.Clear();
                ErrorCodeList.Clear();
                var worksheet = excel.Workbook.Worksheets["FAULT CODE"];
                string mcIdKey = ((KeyValuePair<string, string>)cmbMachineName.SelectedItem).Key;
                string mcId;
                try
                {
                    mcId = Convert.ToString(worksheet.Cells[1, 2].Value).Trim();
                }
                catch (Exception)
                {
                    MessageBox.Show("Please make sure your sheet's name is FAULT CODE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (mcIdKey == mcId)
                {
                    int i = 9;
                    int rows = 1;
                    bool status0;// = false;
                    do
                    {
                        string faultId;// = string.Empty;
                        bool faultstatus;// = false;
                        faultId = Convert.ToString(worksheet.Cells[i, 1].Value);
                        faultstatus = int.TryParse(faultId, out int faultInt);
                        status0 = faultstatus;
                        string message = Convert.ToString(worksheet.Cells[i, 2].Value);
                        if (faultstatus == true)
                        {
                            if (ErrorCodeList.Contains(faultId) == false)
                            {
                                dataGridViewError.Rows.Add(rows.ToString(), faultInt.ToString(), message, "");
                                ErrorCodeList.Add(faultId);
                            
                                rows++;
                            }
                        }

                        i += 1;
                    }
                    while (status0);
                }
                else
                {
                    MessageBox.Show("Select Machine Number is not same as import from Excel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
        #endregion

        //private void bntSaveError_Click(object sender, EventArgs e)
        //{

        //}

        //private void btnDeleteError_Click(object sender, EventArgs e)
        //{

        //}

        private void BntSaveError_Click(object sender, EventArgs e)
        {

            if (dataGridViewError.Rows.Count > 0)
            {
                if (CheckDataTable())
                {
                    ErrorCodeSaveReplace();
                }
            }
        }

        private void BtnDeleteError_Click(object sender, EventArgs e)
        {
            if (dataGridViewError.Rows.Count > 0)
            {
                int r = dataGridViewError.CurrentRow.Index;
                dataGridViewError.Rows.RemoveAt(r);
            }
        }




        private void BtnClearError_Click(object sender, EventArgs e)
        {
            dataGridViewError.Rows.Clear();
        }

        //private void btnReadError_Click(object sender, EventArgs e)
        //{
        //    MachineErrorCodeRead();
        //}

        //private void bntImportError_Click(object sender, EventArgs e)
        //{
        //    ImportFaultCodeExcel();
        //}

        private void BtnReadError_Click(object sender, EventArgs e)
        {
            MachineErrorCodeRead();
        }

        private void BntImportError_Click(object sender, EventArgs e)
        {
            ImportFaultCodeExcel();
        }
    }
}
