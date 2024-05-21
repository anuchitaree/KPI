using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KPI.KeepClass;
using KPI.Parameter;
using KPI.Class;
using KPI.Models;

namespace KPI.InitialForm
{
    public partial class AdditionalDataForm : Form
    {
        readonly List<string> SectionList = new List<string>();
        public AdditionalDataForm()
        {
            InitializeComponent();
            InitialDataGridView();
        }

        private void AdditionalDataForm_Load(object sender, EventArgs e)
        {
            Roles();
            SqlClass sql = new SqlClass();
            sql.AddSection_LoadDivisionPlantSQL();
            DataSet ds = sql.Dataset;
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            foreach (DataRow r in dt1.Rows)
            {
                cmbDiv.Items.Add(r.ItemArray[0].ToString());
            }
            foreach (DataRow r in dt2.Rows)
            {
                cmbPlant.Items.Add(r.ItemArray[0].ToString());
            }
        }

        //private void btnAddSection_Click(object sender, EventArgs e)
        //{

        //}


        private void BtnAddSection_Click(object sender, EventArgs e)
        {
            if (Txtsection.Text != "4111-03" && TxtsectionName.Text != "Stamp 2 : A/C Part")
            {

                if (cmbDiv.SelectedIndex > -1 && cmbPlant.SelectedIndex > -1)
                {
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.AddSection_SaveNewSectionCodeSQL(Txtsection.Text, TxtsectionName.Text, cmbDiv.Text, cmbPlant.Text);
                    if (sqlstatus)
                    {
                        MessageBox.Show("Add section code complete", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void TbSectionCode_TextChanged(object sender, EventArgs e)
        {
            if (TbSectionCode.Text.Length == 7)
            {
                if (SectionList.Contains(TbSectionCode.Text))
                {
                    int number = SectionList.IndexOf(TbSectionCode.Text);
                    dataGridViewSection.FirstDisplayedScrollingRowIndex = number;
                    TbNo.Text = (number+1).ToString();
                }
            }
        }


        ///////////////  LOOP OPERATION  ////////////////


        private void Roles()
        {

            BtnAddSection.Enabled = false;
            if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager) // production
            {

            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {


            }
            else if (User.Role == Models.Roles.Admin_Min)  // Admin -Mini
            {
                BtnAddSection.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnAddSection.Enabled = true;
            }

        }

        //private void txtsection_Click(object sender, EventArgs e)
        //{
        //    Txtsection.Clear();
        //}

        //private void txtsectionName_Click(object sender, EventArgs e)
        //{
        //    TxtsectionName.Clear();
        //}



        private void TxtSection_Click(object sender, EventArgs e)
        {
            Txtsection.Clear();
        }

        private void TxtSectionName_Click(object sender, EventArgs e)
        {
            TxtsectionName.Clear();
        }



        private void InitialDataGridView()
        {
            this.dataGridViewSection.ColumnCount = 5;
            this.dataGridViewSection.Columns[0].Name = "No";
            this.dataGridViewSection.Columns[0].Width = 50;
            this.dataGridViewSection.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewSection.Columns[1].Name = "Section Code";
            this.dataGridViewSection.Columns[1].Width = 100;
            this.dataGridViewSection.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewSection.Columns[2].Name = "Section Name";
            this.dataGridViewSection.Columns[2].Width = 300;
            this.dataGridViewSection.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewSection.Columns[3].Name = "Division";
            this.dataGridViewSection.Columns[3].Width = 100;
            this.dataGridViewSection.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewSection.Columns[4].Name = "Plant";
            this.dataGridViewSection.Columns[4].Width = 100;
            this.dataGridViewSection.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridViewSection.RowHeadersWidth = 25;
            this.dataGridViewSection.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewSection.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewSection.RowHeadersWidth = 4;
            this.dataGridViewSection.RowTemplate.Height = 25;
            this.dataGridViewSection.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataGridViewSection.AlternatingRowsDefaultCellStyle.BackColor = Color.PowderBlue;
            dataGridViewSection.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewSection.AllowUserToResizeRows = false;
            dataGridViewSection.AllowUserToResizeColumns = false;
        }

        private void BtnListUp_Click(object sender, EventArgs e)
        {
            string where;
            if (cmbDiv.SelectedIndex > -1 && cmbPlant.SelectedIndex > -1)
            {
                where = string.Format(" Where[divisionID] = '{0}' and[plantID] = '{1}' ", cmbDiv.Text, cmbPlant.Text);
            }
            else
            {
                where = "";
            }

            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SectionListSQL(where);
            if (sqlstatus)
            {
                DataTable dt = sql.Datatable;
                int i = 1;
                dataGridViewSection.Rows.Clear();
                SectionList.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    dataGridViewSection.Rows.Add(i.ToString(), dr.ItemArray[0], dr.ItemArray[1], dr.ItemArray[2], dr.ItemArray[3]);
                    SectionList.Add(dr.ItemArray[0].ToString());
                    i++;
                }

            }
        }
    }
}
