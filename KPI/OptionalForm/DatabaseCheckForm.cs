using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using KPI.Class;
using KPI.Models;
using KPI.Parameter;

namespace KPI.OptionalForm
{
    public partial class DatabaseCheckForm : Form
    {
        CancellationTokenSource[] cts = new CancellationTokenSource[1];


        public DatabaseCheckForm()
        {
            InitializeComponent();
        }

        private void DatabaseCheckForm_Load(object sender, EventArgs e)
        {
            Roles();
        }


        //*************************************************//
        #region Tab Page 1 :Record

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            //ServiceInitial();
            if (cmbCategory.SelectedIndex == 0)
            {
                IntialProd_RecordTableGridView();
            }
            //LOSS
            else if (cmbCategory.SelectedIndex == 1)
            {
                IntialLoss_RecordTableGridView();
            }
            // CT Machine
            else if (cmbCategory.SelectedIndex == 2)
            {
                IntialML_RecordTableGridView();
            }
            // CT Operator
            else if (cmbCategory.SelectedIndex == 3)
            {
                IntialML_NagaraStartTimeTableGridView();
            }
            dataGridView1.Rows.Clear();

        }

        private void cmbMC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateServiceDate();
            if (chkUpdate.Checked == true)
            {
                Startrunning();
                cmbCategory.Enabled = false;
                btnUpdate.Enabled = false;
            }


        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stoprunning();
            cmbCategory.Enabled = true;
            btnUpdate.Enabled = true;

        }


        private void ServiceMCStatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stoprunning();
        }
        ///  /// OPERATION LOOP //////
        ///  

        #region THREAD POOL UPDATE BY EVERY 10 SECOND
        private void Startrunning()
        {
            if (cts[0] != null)
            {
                return;
            }
            cts[0] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(threadPool), cts[0].Token);


        }

        private void Stoprunning()
        {
            if (cts[0] == null)
            {
                return;
            }
            cts[0].Cancel();
            Thread.Sleep(250);
            cts[0].Dispose();
            cts[0] = null;
        }

        void threadPool(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            Thread.Sleep(15000);
            while (!token.IsCancellationRequested)
            {
                refresh31(DateTime.Now);
                Thread.Sleep(10000);
            }
        }

        private void Invokerefresh31(Action b)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(b));
            }
            catch { }
        }

        private void refresh31(DateTime dt)
        {
            Invokerefresh31(() =>
            {

                UpdateServiceDate();
            });

        }

        #endregion

        #region DisplayData
        private void UpdateServiceDate()
        {
            // Production,PPAS
            if (cmbCategory.SelectedIndex == 0)
            {
                lbTabelName.Text = string.Format("Table: [Production].[dbo].[Prod_RecordTable]");
                int index = cmbOption.SelectedIndex;
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.ServiceMachineStatusInitalSQL(0, index);
                PPASShowTable(sql.Datatable);
            }
            //LOSS
            else if (cmbCategory.SelectedIndex == 1)
            {
                lbTabelName.Text = string.Format("Table: [Production].[dbo].[Loss_RecordTable]");
                int index = cmbOption.SelectedIndex;
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.ServiceMachineStatusInitalSQL(1, index);
                LOSSShowTable(sql.Datatable);
            }
            // CT_Machine
            else if (cmbCategory.SelectedIndex == 2)
            {
                lbTabelName.Text = string.Format("Table: [Production].[dbo].[ML_RecordTable]");
                int index = cmbOption.SelectedIndex;
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.ServiceMachineStatusInitalSQL(2, index);
                CT_MachineShowTable(sql.Datatable);

            }
            // CT_Operator
            else if (cmbCategory.SelectedIndex == 3)
            {
                lbTabelName.Text = string.Format("Table: [Production].[dbo].[ML_NagaraStartTimeTable]");
                lbOption.Text = "Select Station Number";
                int index = cmbOption.SelectedIndex;
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.ServiceMachineStatusInitalSQL(3, index);
                CT_OperatorShowTable(sql.Datatable);
            }

        }
        private void PPASShowTable(DataTable dt)
        {
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string section = dr.ItemArray[1] == null ? "" : dr.ItemArray[1].ToString();
                    DateTime date = Convert.ToDateTime(dr.ItemArray[2]);
                    string registdate = date.ToString("yyyy-MM-dd");
                    DateTime datetime = Convert.ToDateTime(dr.ItemArray[3]);
                    string datetimestr = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                    string pn = dr.ItemArray[4] == null ? "" : dr.ItemArray[4].ToString();
                    string year = dr.ItemArray[7] == null ? "" : dr.ItemArray[7].ToString();
                    dataGridView1.Rows.Add(dr.ItemArray[0], section, registdate, datetimestr, pn, year);
                }
            }
        }
        private void LOSSShowTable(DataTable dt)
        {
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string section = dr.ItemArray[1] == null ? "" : dr.ItemArray[1].ToString();
                    DateTime date = Convert.ToDateTime(dr.ItemArray[2]);
                    string datestr = date.ToString("yyyy-MM-dd");
                    string dn = dr.ItemArray[3] == null ? "" : dr.ItemArray[3].ToString();
                    string lossid = dr.ItemArray[4] == null ? "" : dr.ItemArray[4].ToString();
                    string lossdetailId = dr.ItemArray[5] == null ? "" : dr.ItemArray[5].ToString();
                    DateTime datetime1 = Convert.ToDateTime(dr.ItemArray[6]);
                    string datetimestr1 = datetime1.ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime datetime2 = Convert.ToDateTime(dr.ItemArray[7]);
                    string datetimestr2 = datetime1.ToString("yyyy-MM-dd HH:mm:ss");

                    string sec = dr.ItemArray[8] == null ? "" : dr.ItemArray[8].ToString();
                    string mp = dr.ItemArray[9] == null ? "" : dr.ItemArray[9].ToString();
                    string totalMS = dr.ItemArray[10] == null ? "" : dr.ItemArray[10].ToString();
                    string pn1 = dr.ItemArray[11] == null ? "" : dr.ItemArray[11].ToString();
                    string pn2 = dr.ItemArray[12] == null ? "" : dr.ItemArray[12].ToString();
                    string mc = dr.ItemArray[13] == null ? "" : dr.ItemArray[13].ToString();
                    string err = dr.ItemArray[14] == null ? "" : dr.ItemArray[14].ToString();
                    //string div = dr.ItemArray[15] == null ? "" : dr.ItemArray[15].ToString();
                    //string plant = dr.ItemArray[16] == null ? "" : dr.ItemArray[16].ToString();
                    string stop = dr.ItemArray[17] == null ? "" : dr.ItemArray[17].ToString();

                    dataGridView1.Rows.Add(dr.ItemArray[0], section, datestr, dn, lossid, lossdetailId,
                        datetimestr1, datetimestr2, sec, mp, totalMS, pn1, pn2, mc, err, stop);
                }
            }
        }
        private void CT_MachineShowTable(DataTable dt)
        {
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string section = dr.ItemArray[1] == null ? "" : dr.ItemArray[1].ToString();
                    DateTime date = Convert.ToDateTime(dr.ItemArray[2]);
                    string datestr = date.ToString("yyyy-MM-dd");
                    DateTime datetime = Convert.ToDateTime(dr.ItemArray[3]);
                    string datetimestr = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                    string pn = dr.ItemArray[4] == null ? "" : dr.ItemArray[4].ToString();
                    string mctime = dr.ItemArray[5] == null ? "" : dr.ItemArray[5].ToString();
                    string mcnumber = dr.ItemArray[6] == null ? "" : dr.ItemArray[6].ToString();
                    dataGridView1.Rows.Add(dr.ItemArray[0], section, datestr, datetimestr, pn, mctime, mcnumber);
                }
            }
        }
        private void CT_OperatorShowTable(DataTable dt)
        {
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string id = dr.ItemArray[0] == null ? "" : dr.ItemArray[0].ToString();
                    string section = dr.ItemArray[1] == null ? "" : dr.ItemArray[1].ToString();
                    DateTime date = Convert.ToDateTime(dr.ItemArray[2]);
                    string datestr = date.ToString("yyyy-MM-dd");
                    string dn = dr.ItemArray[3] == null ? "" : dr.ItemArray[3].ToString();
                    string station = dr.ItemArray[4] == null ? "" : dr.ItemArray[4].ToString();
                    DateTime datetime = Convert.ToDateTime(dr.ItemArray[5]);
                    string datetimestr = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                    string pn = dr.ItemArray[6] == null ? "" : dr.ItemArray[6].ToString();
                    string nagara = dr.ItemArray[7] == null ? "" : dr.ItemArray[7].ToString();
                    dataGridView1.Rows.Add(id, section, datestr, dn, station, datetimestr, pn, nagara);
                }
            }
        }
        #endregion

        #region INITIAL DataGridView
        private void IntialProd_RecordTableGridView()
        {
            this.dataGridView1.ColumnCount = 6;
            this.dataGridView1.Columns[0].Name = "Run";
            this.dataGridView1.Columns[0].Width = 150;
            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].Name = "SectionCode";
            this.dataGridView1.Columns[1].Width = 90;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[2].Name = "RegistDate";
            this.dataGridView1.Columns[2].Width = 100;
            this.dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[3].Name = "RegistDateTime";
            this.dataGridView1.Columns[3].Width = 200;
            this.dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[4].Name = "Part Number";
            this.dataGridView1.Columns[4].Width = 150;
            this.dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[5].Name = "registYear";
            this.dataGridView1.Columns[5].Width = 80;
            this.dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;


            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowTemplate.Height = 25;
        }
        private void IntialLoss_RecordTableGridView()
        {
            this.dataGridView1.ColumnCount = 16;
            this.dataGridView1.Columns[0].Name = "Run";
            this.dataGridView1.Columns[0].Width = 150;
            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].Name = "SectionCode";
            this.dataGridView1.Columns[1].Width = 90;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[2].Name = "RegistDate";
            this.dataGridView1.Columns[2].Width = 100;
            this.dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[3].Name = "DayNight";
            this.dataGridView1.Columns[3].Width = 50;
            this.dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[4].Name = "LossId";
            this.dataGridView1.Columns[4].Width = 80;
            this.dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[5].Name = "LossDetailId";
            this.dataGridView1.Columns[5].Width = 50;
            this.dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[6].Name = "dateTimeStart";
            this.dataGridView1.Columns[6].Width = 180;
            this.dataGridView1.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[7].Name = "dateTimeEnd";
            this.dataGridView1.Columns[7].Width = 180;
            this.dataGridView1.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[8].Name = "Second";
            this.dataGridView1.Columns[8].Width = 80;
            this.dataGridView1.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[9].Name = "MP";
            this.dataGridView1.Columns[9].Width = 50;
            this.dataGridView1.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.Columns[10].Name = "TotalMS";
            this.dataGridView1.Columns[10].Width = 100;
            this.dataGridView1.Columns[10].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[11].Name = "partNumberStart";
            this.dataGridView1.Columns[11].Width = 150;
            this.dataGridView1.Columns[11].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[12].Name = "partNumberEnd";
            this.dataGridView1.Columns[12].Width = 150;
            this.dataGridView1.Columns[12].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[13].Name = "MCNumber";
            this.dataGridView1.Columns[13].Width = 200;
            this.dataGridView1.Columns[13].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[14].Name = "errorCode";
            this.dataGridView1.Columns[14].Width = 100;
            this.dataGridView1.Columns[14].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[15].Name = "mcStop";
            this.dataGridView1.Columns[15].Width = 100;
            this.dataGridView1.Columns[15].SortMode = DataGridViewColumnSortMode.NotSortable;


            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowTemplate.Height = 25;
        }
        private void IntialML_RecordTableGridView()
        {
            this.dataGridView1.ColumnCount = 7;
            this.dataGridView1.Columns[0].Name = "Run";
            this.dataGridView1.Columns[0].Width = 150;
            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].Name = "SectionCode";
            this.dataGridView1.Columns[1].Width = 90;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[2].Name = "RegistDate";
            this.dataGridView1.Columns[2].Width = 100;
            this.dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[3].Name = "RegistDateTime";
            this.dataGridView1.Columns[3].Width = 200;
            this.dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[4].Name = "Part Number";
            this.dataGridView1.Columns[4].Width = 200;
            this.dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[5].Name = "mcTimeSec";
            this.dataGridView1.Columns[5].Width = 120;
            this.dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[6].Name = "[mcNumber]";
            this.dataGridView1.Columns[6].Width = 220;
            this.dataGridView1.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;


            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowTemplate.Height = 25;

        }
        private void IntialML_NagaraStartTimeTableGridView()
        {
            this.dataGridView1.ColumnCount = 8;
            this.dataGridView1.Columns[0].Name = "Run";
            this.dataGridView1.Columns[0].Width = 150;
            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].Name = "SectionCode";
            this.dataGridView1.Columns[1].Width = 90;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[2].Name = "RegistDate";
            this.dataGridView1.Columns[2].Width = 100;
            this.dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[3].Name = "DayNight";
            this.dataGridView1.Columns[3].Width = 100;
            this.dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[4].Name = "StationNo";
            this.dataGridView1.Columns[4].Width = 100;
            this.dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[5].Name = "RegistDateTime";
            this.dataGridView1.Columns[5].Width = 200;
            this.dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[6].Name = "Part Number";
            this.dataGridView1.Columns[6].Width = 200;
            this.dataGridView1.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[7].Name = "NagaraSwTime";
            this.dataGridView1.Columns[7].Width = 120;
            this.dataGridView1.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowTemplate.Height = 25;

        }


        #endregion

        #endregion

        //*************************************************//

        //*************************************************//
        #region Tab Page 2 : Unit
        private void btnUnitSearch_Click(object sender, EventArgs e)
        {

            if (cmbUnitSelect.SelectedIndex > -1)
            {
                if (cmbUnitSelect.SelectedItem.ToString() == "LOSS")
                {

                }
                else if (cmbUnitSelect.SelectedItem.ToString() == "CT Machine")
                {
                    lbUnit.Text = "Table :  [Production].[dbo].[ML_RecordTable]";
                    IntialMachineIdGridView();
                    dataGridView2.Rows.Clear();
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.ServiceUnitAmountInitalSQL(1);
                    if (sqlstatus)
                    {
                        DataTable dt = sql.Datatable;
                        if (dt.Rows.Count > 0)
                        {
                            int i = 1;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView2.Rows.Add(i, dr.ItemArray[0]);
                                i++;
                            }
                        }
                    }
                }
                else if (cmbUnitSelect.SelectedItem.ToString() == "CT Operator")
                {
                    lbUnit.Text = "Table : [Production].[dbo].[ML_NagaraStartTimeTable]";
                    IntialStationGridView();
                    dataGridView2.Rows.Clear();
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.ServiceUnitAmountInitalSQL(2);
                    if (sqlstatus)
                    {
                        DataTable dt = sql.Datatable;
                        if (dt.Rows.Count > 0)
                        {
                            int i = 1;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView2.Rows.Add(i, dr.ItemArray[0]);
                                i++;
                            }
                        }
                    }
                }
            }

        }

        private void btnUnitDel_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0 && cmbUnitSelect.SelectedIndex > -1 && tbpassword.Text == "AnuchiT")
            {
                int row = dataGridView2.CurrentRow.Index;
                string name = dataGridView2.Rows[row].Cells[1].Value.ToString();

                DialogResult r = MessageBox.Show("The data will be permanently deleted. Cannot be recovered. Are you sure ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r)
                {
                    if (cmbUnitSelect.SelectedItem.ToString() == "LOSS")
                    {

                    }
                    else if (cmbUnitSelect.SelectedItem.ToString() == "CT Machine")
                    {
                        SqlClass sql = new SqlClass();
                        bool sqlstatus = sql.ServiceUnitDeleteSQL(1, name);
                        if (sqlstatus)
                        {
                            string msg = string.Format("Already delete record that MachineId = {0} ", name);
                            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (cmbUnitSelect.SelectedItem.ToString() == "CT Operator")
                    {
                        SqlClass sql = new SqlClass();
                        bool sqlstatus = sql.ServiceUnitDeleteSQL(2, name);
                        if (sqlstatus)
                        {
                            string msg = string.Format("Already delete record that Station name = {0} ", name);
                            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
        }


        ///////////////////////////////  Operation Loop /////////////////////////////


        private void IntialStationGridView()
        {
            this.dataGridView2.ColumnCount = 2;
            this.dataGridView2.Columns[0].Name = "No";
            this.dataGridView2.Columns[0].Width = 50;
            this.dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[1].Name = "Station name";
            this.dataGridView2.Columns[1].Width = 150;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView2.RowHeadersWidth = 25;
            this.dataGridView2.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView2.RowHeadersWidth = 4;
            this.dataGridView2.RowTemplate.Height = 25;
        }

        private void IntialMachineIdGridView()
        {
            this.dataGridView2.ColumnCount = 2;
            this.dataGridView2.Columns[0].Name = "No";
            this.dataGridView2.Columns[0].Width = 50;
            this.dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[1].Name = "Machine Id";
            this.dataGridView2.Columns[1].Width = 150;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView2.RowHeadersWidth = 25;
            this.dataGridView2.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView2.RowHeadersWidth = 4;
            this.dataGridView2.RowTemplate.Height = 25;
        }



        //*************************************************//
        #endregion


        private void Roles()
        {
            //btn301.Enabled = false;
            //btnUnitDel.Enabled = false;
            btnUnitDel.Visible = false;
            tbpassword.Visible = false;
            label4.Visible = false;

            if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager)// production
            {

            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {


            }
            else if (User.Role == Models.Roles.Admin_Min)  // Admin -Mini
            {

            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                btnUnitDel.Enabled = true;
                btnUnitDel.Visible = true;
                tbpassword.Visible = true;
                label4.Visible = true;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanelFoor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lbOption_Click(object sender, EventArgs e)
        {

        }

        private void chkUpdate_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lbTabelName_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbUnitSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void lbUnit_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }





        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            UpdateServiceDate();
            if (chkUpdate.Checked == true)
            {
                Startrunning();
                cmbCategory.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }

        private void btnStop_Click_1(object sender, EventArgs e)
        {
            Stoprunning();
            cmbCategory.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void chkUpdate_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void btnUnitSearch_Click_1(object sender, EventArgs e)
        {

            if (cmbUnitSelect.SelectedIndex > -1)
            {
                if (cmbUnitSelect.SelectedItem.ToString() == "LOSS")
                {

                }
                else if (cmbUnitSelect.SelectedItem.ToString() == "CT Machine")
                {
                    lbUnit.Text = "Table :  [Production].[dbo].[ML_RecordTable]";
                    IntialMachineIdGridView();
                    dataGridView2.Rows.Clear();
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.ServiceUnitAmountInitalSQL(1);
                    if (sqlstatus)
                    {
                        DataTable dt = sql.Datatable;
                        if (dt.Rows.Count > 0)
                        {
                            int i = 1;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView2.Rows.Add(i, dr.ItemArray[0]);
                                i++;
                            }
                        }
                    }
                }
                else if (cmbUnitSelect.SelectedItem.ToString() == "CT Operator")
                {
                    lbUnit.Text = "Table : [Production].[dbo].[ML_NagaraStartTimeTable]";
                    IntialStationGridView();
                    dataGridView2.Rows.Clear();
                    SqlClass sql = new SqlClass();
                    bool sqlstatus = sql.ServiceUnitAmountInitalSQL(2);
                    if (sqlstatus)
                    {
                        DataTable dt = sql.Datatable;
                        if (dt.Rows.Count > 0)
                        {
                            int i = 1;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView2.Rows.Add(i, dr.ItemArray[0]);
                                i++;
                            }
                        }
                    }
                }
            }

        }

        private void btnUnitDel_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0 && cmbUnitSelect.SelectedIndex > -1 && tbpassword.Text == "AnuchiT")
            {
                int row = dataGridView2.CurrentRow.Index;
                string name = dataGridView2.Rows[row].Cells[1].Value.ToString();

                DialogResult r = MessageBox.Show("The data will be permanently deleted. Cannot be recovered. Are you sure ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r)
                {
                    if (cmbUnitSelect.SelectedItem.ToString() == "LOSS")
                    {

                    }
                    else if (cmbUnitSelect.SelectedItem.ToString() == "CT Machine")
                    {
                        SqlClass sql = new SqlClass();
                        bool sqlstatus = sql.ServiceUnitDeleteSQL(1, name);
                        if (sqlstatus)
                        {
                            string msg = string.Format("Already delete record that MachineId = {0} ", name);
                            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (cmbUnitSelect.SelectedItem.ToString() == "CT Operator")
                    {
                        SqlClass sql = new SqlClass();
                        bool sqlstatus = sql.ServiceUnitDeleteSQL(2, name);
                        if (sqlstatus)
                        {
                            string msg = string.Format("Already delete record that Station name = {0} ", name);
                            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }

        }

        private void cmbCategory_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //ServiceInitial();
            if (cmbCategory.SelectedIndex == 0)
            {
                IntialProd_RecordTableGridView();
            }
            //LOSS
            else if (cmbCategory.SelectedIndex == 1)
            {
                IntialLoss_RecordTableGridView();
            }
            // CT Machine
            else if (cmbCategory.SelectedIndex == 2)
            {
                IntialML_RecordTableGridView();
            }
            // CT Operator
            else if (cmbCategory.SelectedIndex == 3)
            {
                IntialML_NagaraStartTimeTableGridView();
            }
            dataGridView1.Rows.Clear();

        }
    }
}
