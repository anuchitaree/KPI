using KPI.Class;
using KPI.Models;
using KPI.Parameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KPI.InitialForm
{
    public partial class ExclusionLossEditForm : Form
    {
        readonly Dictionary<string, string> Exc = new Dictionary<string, string>();
        readonly Dictionary<string, string> lossList = new Dictionary<string, string>();
        readonly Dictionary<string, string> sixGroupDic = new Dictionary<string, string>();
        private string srun = string.Empty;
        //bool _ExculsionMode = true;

        int index = 0;


        public ExclusionLossEditForm()
        {
            InitializeComponent();
            InitialDataGridView();
        }

        private void ExclusionLossEditForm_Load(object sender, EventArgs e)
        {
            SixGroupLossName();
            Roles();
        }


        #region TAB : Exlusion Time

        //private void btnExcRead_Click(object sender, EventArgs e)
        //{
        //    ExlcusionTimeRead();
        //}

        //private void btnExcAdd_Click(object sender, EventArgs e)
        //{
        //    ExclusionTimeAdd();
        //}

        //private void btnExcUpdate_Click(object sender, EventArgs e)
        //{
        //    ExclusionTimeUpdate();
        //}

        //private void btnExcDelete_Click(object sender, EventArgs e)
        //{
        //    ExclusionTimeDelete();
        //}

        //private void dataGridViewExc_Click(object sender, EventArgs e)
        //{

        //}

        //private void dataGridViewExc_DoubleClick(object sender, EventArgs e)
        //{
        //    index = DataGridViewExc.CurrentRow.Index;
        //    txtIdexc.Text = Convert.ToString(DataGridViewExc.Rows[index].Cells[0].Value);
        //    txtCodeexc.Text = Convert.ToString(DataGridViewExc.Rows[index].Cells[1].Value);
        //    textBoxNameexc.Text = Convert.ToString(DataGridViewExc.Rows[index].Cells[2].Value);
        //    txtSortexc.Text = Convert.ToString(DataGridViewExc.Rows[index].Cells[3].Value);
        //}






        private void BtnExcRead_Click(object sender, EventArgs e)
        {
            ExlcusionTimeRead();
        }

        private void BtnExcAdd_Click(object sender, EventArgs e)
        {
            ExclusionTimeAdd();
        }

        private void BtnExcUpdate_Click(object sender, EventArgs e)
        {
            ExclusionTimeUpdate();
        }

        private void BtnExcDelete_Click(object sender, EventArgs e)
        {
            ExclusionTimeDelete();
        }

        private void DataGridViewExc_Click(object sender, EventArgs e)
        {
            if (DataGridViewExc.Rows.Count > 0)
            {
                index = DataGridViewExc.CurrentRow.Index;
            }
        }

        private void DataGridViewExc_DoubleClick(object sender, EventArgs e)
        {
            index = DataGridViewExc.CurrentRow.Index;
            txtIdexc.Text = Convert.ToString(DataGridViewExc.Rows[index].Cells[0].Value);
            txtCodeexc.Text = Convert.ToString(DataGridViewExc.Rows[index].Cells[1].Value);
            textBoxNameexc.Text = Convert.ToString(DataGridViewExc.Rows[index].Cells[2].Value);
            txtSortexc.Text = Convert.ToString(DataGridViewExc.Rows[index].Cells[3].Value);
        }









        private void ExlcusionTimeRead()
        {
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ExlcusionTimeReadSQL();
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                DataGridViewExc.Rows.Clear();
                Exc.Clear();
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string run = dt1.Rows[i].ItemArray[0].ToString();
                        string excId = dt1.Rows[i].ItemArray[1].ToString();
                        string excName = dt1.Rows[i].ItemArray[2].ToString();
                        string sort = dt1.Rows[i].ItemArray[3].ToString();
                        DataGridViewExc.Rows.Add(run, excId, excName, sort);
                        Exc.Add(run, excId);
                    }
                }
            }

        }

        private void ExclusionTimeAdd()
        {
            if (!Exc.ContainsKey(txtCodeexc.Text))
            {
                DialogResult r = MessageBox.Show("Are you sure ? Confrim add new Exclusion ID " + txtCodeexc.Text, "Add Exclusion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r)
                {
                    string id = txtCodeexc.Text.Trim();
                    string name = textBoxNameexc.Text.Trim();
                    string sort = txtSortexc.Text.Trim() == "" ? "0" : txtSortexc.Text.Trim();

                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.ExclusionTimeAddSQL(id, name, sort);
                    if (sqlstatus)
                    {
                        txtCodeexc.Text = textBoxNameexc.Text = string.Empty;
                        ExlcusionTimeRead();
                        MessageBox.Show("Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failure", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("already has the Exclusion ID", "Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExclusionTimeUpdate()
        {
            if (txtCodeexc.Text == "")
            {
                return;
            }
            if (!Exc.ContainsKey(txtCodeexc.Text))
            {
                DialogResult r = MessageBox.Show("Are you sure ? Confrim update Exclusion/Loss ID " + txtCodeexc.Text, "Update Exclusion/Loss", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r)
                {
                    string code = txtCodeexc.Text;
                    string name = textBoxNameexc.Text;
                    string run = txtIdexc.Text;
                    string sort = txtSortexc.Text;

                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.ExclusionTimeUpdateSQL(code, name, sort, run);
                    if (sqlstatus)
                    {
                        ExlcusionTimeRead();
                        txtCodeexc.Text = textBoxNameexc.Text = txtIdexc.Text = txtSortexc.Text = "";
                        MessageBox.Show("Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failure", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("already has the Exclusion ID", "Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExclusionTimeDelete()
        {
            string id = Convert.ToString(DataGridViewExc.Rows[index].Cells[0].Value);
            DialogResult r = MessageBox.Show("Are you sure ? Confrim delete Exclusion/Loss ID " + txtCodeexc.Text, "Delete Exclusion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes == r)
            {

                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.ExclusionTimeDeleteSQL(id);
                if (sqlstatus)
                {
                    ExlcusionTimeRead();
                    txtCodeexc.Text = textBoxNameexc.Text = "";
                    MessageBox.Show("Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failure", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        #endregion



        #region TAB : Loss Time

        //private void btnReadloss_Click(object sender, EventArgs e)
        //{
        //    LossTimeRead();
        //}

        //private void btnAddloss_Click(object sender, EventArgs e)
        //{
        //    LossTimeAdd();
        //}

        //private void btnUpdateloss_Click(object sender, EventArgs e)
        //{
        //    LossTimeUpdate();
        //}

        //private void btnlossDel_Click(object sender, EventArgs e)
        //{
        //    LossTimeDelete();
        //}



        private void BtnReadloss_Click(object sender, EventArgs e)
        {
            LossTimeRead();
        }

        private void BtnAddloss_Click(object sender, EventArgs e)
        {
            LossTimeAdd();
        }

        private void BtnUpdateloss_Click(object sender, EventArgs e)
        {
            LossTimeUpdate();
        }

        private void BtnlossDel_Click(object sender, EventArgs e)
        {
            LossTimeDelete();
        }






        private void SixGroupLossName()
        {
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SixLossNameSQL();
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;

                cmbSixloss.Items.Clear();
                foreach (DataRow dr in dt1.Rows)
                {
                    sixGroupDic.Add(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());
                    cmbSixloss.Items.Add(dr.ItemArray[1].ToString());
                }
            }
        }

        private void LossTimeRead()
        {
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.LossTimeReadSQL();
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                DataGridViewLoss.Rows.Clear();
                lossList.Clear();
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string li1 = dt1.Rows[i].ItemArray[0].ToString();
                        string li2 = dt1.Rows[i].ItemArray[1].ToString();
                        li2 = sixGroupDic.FirstOrDefault(x => x.Key == li2).Value;
                        string li3 = dt1.Rows[i].ItemArray[2].ToString();
                        string li4 = dt1.Rows[i].ItemArray[3].ToString();
                        string li5 = dt1.Rows[i].ItemArray[4].ToString();
                        string li6 = dt1.Rows[i].ItemArray[5].ToString();
                        DataGridViewLoss.Rows.Add(li1, li2, li3, li4, li5, li6);
                        lossList.Add(li1, li2);

                    }
                }
            }

        }


        #endregion


        // /////////////////////////////// OPERATION LOOP //////////////////////////////////////////
        #region data Grid view initial and other
        private void InitialDataGridView()
        {
            this.DataGridViewExc.ColumnCount = 4;
            this.DataGridViewExc.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewExc.Columns[0].Name = "Id";
            this.DataGridViewExc.Columns[0].Width = 50;

            this.DataGridViewExc.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewExc.Columns[1].Name = "Code";
            this.DataGridViewExc.Columns[1].Width = 100;
            this.DataGridViewExc.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewExc.Columns[2].Name = "Exclusion/Loss Name";
            this.DataGridViewExc.Columns[2].Width = 600;
            this.DataGridViewExc.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewExc.Columns[3].Name = "Sort";
            this.DataGridViewExc.Columns[3].Width = 60;
            //this.dataGridViewExc.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            //this.dataGridViewExc.Columns[4].Name = "s.run";
            //this.dataGridViewExc.Columns[4].Width = 5;
            this.DataGridViewExc.RowHeadersWidth = 4;
            this.DataGridViewExc.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.DataGridViewExc.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.DataGridViewExc.RowTemplate.Height = 25;
            DataGridViewExc.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DataGridViewExc.AllowUserToResizeRows = false;
            DataGridViewExc.AllowUserToResizeColumns = false;




            this.DataGridViewLoss.ColumnCount = 6;
            this.DataGridViewLoss.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewLoss.Columns[0].Name = "Id";
            this.DataGridViewLoss.Columns[0].Width = 50;
            this.DataGridViewLoss.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewLoss.Columns[1].Name = "SixGroup";
            this.DataGridViewLoss.Columns[1].Width = 200;

            this.DataGridViewLoss.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewLoss.Columns[2].Name = "Code";
            this.DataGridViewLoss.Columns[2].Width = 100;
            this.DataGridViewLoss.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewLoss.Columns[3].Name = "Exclusion/Loss Name";
            this.DataGridViewLoss.Columns[3].Width = 600;
            this.DataGridViewLoss.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewLoss.Columns[4].Name = "Sort";
            this.DataGridViewLoss.Columns[4].Width = 60;
            this.DataGridViewLoss.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewLoss.Columns[5].Name = "s.run";
            this.DataGridViewLoss.Columns[5].Width = 50;
            this.DataGridViewLoss.RowHeadersWidth = 4;
            this.DataGridViewLoss.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.DataGridViewLoss.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.DataGridViewLoss.RowTemplate.Height = 25;
            DataGridViewLoss.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DataGridViewLoss.AllowUserToResizeRows = false;
            DataGridViewLoss.AllowUserToResizeColumns = false;

        }

        private void Roles()
        {
            BtnExcAdd.Enabled = false;
            BtnExcDelete.Enabled = false;
            BtnExcUpdate.Enabled = false;
            if (User.Role == Models.Roles.Invalid) // invalid
            {

            }
            else if (User.Role == Models.Roles.General) // General
            {

            }
            else if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager)  // production
            {


            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {
                BtnExcAdd.Enabled = true;
                BtnExcDelete.Enabled = true;
                BtnExcUpdate.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Min) // Admin-mini
            {
                BtnExcAdd.Enabled = true;
                BtnExcDelete.Enabled = true;
                BtnExcUpdate.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnExcAdd.Enabled = true;
                BtnExcDelete.Enabled = true;
                BtnExcUpdate.Enabled = true;
            }
        }

        #endregion



        //private void AutoLoadExc()
        //{
        //    if (txtCodeexc.Text != "")
        //    {

        //        if (Exc.ContainsValue(txtCodeexc.Text) == true)
        //        {
        //            int i = 0;
        //            foreach (var index in Exc)
        //            {
        //                if (index.Value == txtCodeexc.Text)
        //                {
        //                    break;
        //                }
        //                i += 1;
        //            }
        //            txtCodeexc.Text = Convert.ToString(dataGridViewExc.Rows[i].Cells[2].Value);
        //            textBoxNameexc.Text = Convert.ToString(dataGridViewExc.Rows[i].Cells[3].Value);

        //        }
        //    }
        //    else
        //    {
        //        textBoxNameexc.Text = txtSortexc.Text = string.Empty;
        //        //cmbSix.SelectedIndex = 0;
        //    }

        //}

        private void LossTimeAdd()
        {
            if (!Exc.ContainsKey(txtCodeexc.Text))
            {
                DialogResult r = MessageBox.Show("Are you sure ? Confrim add new Exclusion ID " + txtCodeexc.Text,
                    "Add Exclusion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r)
                {
                    string id = tbCodeloss.Text.Trim();
                    string name = tbNameloss.Text.Trim();
                    string sort = string.Empty;
                    string six = sixGroupDic.FirstOrDefault(x => x.Value == cmbSixloss.Text).Key;
                    six = six ?? "1";
                    sort = tbSortloss.Text.Trim() == "" ? "0" : tbSortloss.Text.Trim();

                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.LossTimeAddSQL(id, name, sort, six);
                    if (sqlstatus)
                    {
                        tbCodeloss.Text = tbNameloss.Text = tbSortloss.Text = string.Empty;
                        LossTimeRead();
                        MessageBox.Show("Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failure", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("already has the Exclusion ID", "Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LossTimeUpdate()
        {
            if (tbCodeloss.Text == "")
            {
                return;
            }
            if (!lossList.ContainsKey(tbCodeloss.Text))
            {
                DialogResult r = MessageBox.Show("Are you sure ? Confrim update Loss ID " + tbCodeloss.Text,
                    "Update Loss", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r)
                {
                    string code = tbCodeloss.Text;
                    string name = tbNameloss.Text;
                    string run = txtIdloss.Text;
                    string sort = tbSortloss.Text;
                    string six = sixGroupDic.FirstOrDefault(x => x.Value == cmbSixloss.Text).Key;

                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.LossTimeUpdateSQL(code, name, sort, six, run, srun);
                    if (sqlstatus)
                    {
                        LossTimeRead();
                        tbCodeloss.Text = tbNameloss.Text = "";
                        MessageBox.Show("Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failure", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("already has the loss ID", "Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LossTimeDelete()
        {
            string id = txtIdloss.Text;
            if (id != "" && srun != "")
            {
                DialogResult r = MessageBox.Show("Are you sure ? Confrim delete Loss ID " + tbCodeloss.Text, "Delete Loss",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r)
                {
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.LossTimeDeleteSQL(id, srun);
                    if (sqlstatus)
                    {
                        LossTimeRead();
                        tbCodeloss.Text = tbNameloss.Text =txtIdloss.Text=tbSortloss.Text=string.Empty;
                        MessageBox.Show("Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failure", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //private void dataGridViewLoss_DoubleClick(object sender, EventArgs e)
        //{
        //    index = DataGridViewLoss.CurrentRow.Index;
        //    txtIdloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[0].Value);
        //    cmbSixloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[1].Value);

        //    tbCodeloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[2].Value);
        //    tbNameloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[3].Value);
        //    tbSortloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[4].Value);
        //    srun = Convert.ToString(DataGridViewLoss.Rows[index].Cells[5].Value);
        //}

        private void DataGridViewLoss_DoubleClick(object sender, EventArgs e)
        {
            index = DataGridViewLoss.CurrentRow.Index;
            txtIdloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[0].Value);
            cmbSixloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[1].Value);

            tbCodeloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[2].Value);
            tbNameloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[3].Value);
            tbSortloss.Text = Convert.ToString(DataGridViewLoss.Rows[index].Cells[4].Value);
            srun = Convert.ToString(DataGridViewLoss.Rows[index].Cells[5].Value);
        }


    }
}
