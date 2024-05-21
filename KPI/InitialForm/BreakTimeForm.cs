using KPI.Class;
using KPI.Models;
using KPI.Parameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KPI.InitialForm
{
    public partial class BreakTimeForm : Form
    {
        readonly List<string> ququeList = new List<string>();
        readonly List<string> tableList = new List<string>();
        bool memoryNoBreakQuque;
        public BreakTimeForm()
        {
            InitializeComponent();
            cmbSection.DataSource = new BindingSource(Dict.SectionCodeName, null);
            cmbSection.DisplayMember = "Value";
            cmbSection.ValueMember = "Key";
            DataGridViewInitial();
            DataGridViewInitial2();
        }

        private void BreakTimeForm_Load(object sender, EventArgs e)
        {
            DataGridViewInitial();
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
            CmbYear.Text = DateTime.Now.ToString("yyyy");
            Roles();
            Roles2();
        }

        #region TAB 1  ==========================================================================


        private void BtnLoadd_Click(object sender, EventArgs e)
        {
            LoadBreakTimeTable();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
             SaveBreakTimeTable();
        }

        private void CmbYear_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string section = Dict.SectionCodeName.FirstOrDefault(x => x.Value == cmbSection.Text).Key;
            if (section == null || CmbYear.SelectedIndex < 0)
                return;
            if (memoryNoBreakQuque == true)
            {
                if (LoadBreakTime() == true)
                {
                    DialogResult r = MessageBox.Show("* is default data that has not yet save in DB. \n Shall you save it now ? ",
                              "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DialogResult.Yes == r)
                    {
                        SaveUpdate();
                        LoadBreakTime();
                    }
                }
            }
        }

        private void LoadBreakTimeTable()
        {
            if (memoryNoBreakQuque == true)
            {
                if (LoadBreakTime() == true)
                {
                    DialogResult r = MessageBox.Show("Please confirm the number of break table and SAVE before close this page",
                              "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DialogResult.Yes == r)
                    {
                        SaveUpdate();
                        LoadBreakTime();
                    }
                }
            }
            else
                MessageBox.Show("There is NOT Break Time Table. Please create it before \n Goto the Setup Tab",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        private bool LoadBreakTime()
        {
            string section = Dict.SectionCodeName.FirstOrDefault(x => x.Value == cmbSection.Text).Key;

            string sectionDivPlant1 = string.Format("{0}{1}{2}", section, User.Division, User.Plant);
            string year1 = CmbYear.Text;
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SS("BreakTimeLoadQuqueExc", "@yearstd", year1, "@sectiondivplant", sectionDivPlant1);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt = ds.Tables[0];
                dataGridViewQuque.Rows.Clear();
                bool error = false;
                for (int i = 0; i < 12; i++)
                {
                    string year = dt.Rows[i].ItemArray[0].ToString();
                    string month = dt.Rows[i].ItemArray[1].ToString();
                    string mark = dt.Rows[i].ItemArray[3].ToString() == "0" ? "*" : "";
                    if (mark == "*")
                    {
                        error = true;
                    }
                    int quque = Convert.ToInt32(dt.Rows[i].ItemArray[2]) - 1;
                    dataGridViewQuque.Rows.Add(year, month, mark);

                    if (quque >= 0)
                        dataGridViewQuque.Rows[i].Cells["BreakTimeQuque"].Value = (dataGridViewQuque.Rows[i].Cells["BreakTimeQuque"]
                            as DataGridViewComboBoxCell).Items[quque];

                }
                if (error == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }
        private void SaveBreakTimeTable()
        {
            if (CheckBeforeSave() == true)
            {
                if (SaveUpdate())
                    MessageBox.Show("Update Break Time quque Completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Try update it again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Wrong data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool CheckBeforeSave()
        {
            int row = dataGridViewQuque.Rows.Count;
            return row > 0 ? true : false;

        }
        private bool SaveUpdate()
        {
            string year = CmbYear.Text;
            string section = Dict.SectionCodeName.FirstOrDefault(x => x.Value == cmbSection.Text).Key;
            if (dataGridViewQuque.Rows.Count > 0)
            {
                var query = new StringBuilder();
                query.AppendFormat("Delete From [Production].[dbo].[Prod_TimeBreakQueueTable] where sectionCode='{0}' and registYear='{1}'", section, year);
                query.AppendFormat("Insert into  [Production].[dbo].[Prod_TimeBreakQueueTable] \n ");
                query.AppendFormat("([sectionCode],[registYear],[registMonth],[breakQueue] ) VALUES  \n");
                for (int i = 0; i < 12; i++)
                {
                    DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)dataGridViewQuque["BreakTimeQuque", i];
                    int quque = dcc.Items.IndexOf(dcc.Value) + 1;
                    query.AppendFormat("('{0}','{1}','{2}',{3}) \n", section, year, (i + 1).ToString("00"), quque);
                    if (i < 11)
                        query.Append(",");

                }
                var a = query.ToString();
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.BreakTimeSaveYearQuqueSQL(a);
                if (sqlstatus)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;


        }
        private void Roles()
        {
            BtnSave.Enabled = false;

            if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager) // production
            {
                BtnSave.Enabled = true;
            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {

            }
            else if (User.Role == Models.Roles.Admin_Min)  // Admin -Mini
            {
                BtnSave.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnSave.Enabled = true;
            }

        }
        private void DataGridViewInitial()
        {
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.BreakTimeLoadQuqueSQL();
            if (sqlstatus)
            {
                DataTable dt = sql.Datatable;
                ququeList.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ququeList.Add(dr.ItemArray[0].ToString());
                    }
                }
            }

            if (ququeList.Count == 0)
            {
                memoryNoBreakQuque = false;
                return;
            }
            else
                memoryNoBreakQuque = true;

           this.dataGridViewQuque.ColumnCount = 3;
            this.dataGridViewQuque.Columns[0].Name = "Year";
            this.dataGridViewQuque.Columns[0].Width = 100;
            this.dataGridViewQuque.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewQuque.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewQuque.Columns[1].Name = "Month";
            this.dataGridViewQuque.Columns[1].Width = 100;
            this.dataGridViewQuque.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewQuque.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewQuque.Columns[2].Name = "Error";
            this.dataGridViewQuque.Columns[2].Width = 50;
            this.dataGridViewQuque.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewQuque.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewQuque.RowHeadersWidth = 4;
            this.dataGridViewQuque.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridViewQuque.RowTemplate.Height = 30;
            this.dataGridViewQuque.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dataGridViewQuque.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.dataGridViewQuque.AllowUserToResizeRows = false;
            this.dataGridViewQuque.AllowUserToResizeColumns = false;

            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "BreakTimeQuque",
                Name = "BreakTimeQuque",
                DataSource = new BindingSource(ququeList, null),
                DropDownWidth = 10,
                Width = 150,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 2
            };
            comboBoxColumn.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn.DefaultCellStyle.Font = new Font("Tahoma", 9);

            dataGridViewQuque.Columns.Add(comboBoxColumn);

        }

        #endregion ==========================================================================



        #region TAB 2 =======================================================================







        private void BtnLoadTable_Click(object sender, EventArgs e)
        {
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.BrakeTable_LoadTableSQL();
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];
                if (dt.Rows.Count > 0)
                {
                    dataGridViewList.Rows.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        DateTime dta = Convert.ToDateTime(dr.ItemArray[2]);
                        DateTime dtb = Convert.ToDateTime(dr.ItemArray[3]);
                        dataGridViewList.Rows.Add(dr.ItemArray[0], dr.ItemArray[1], dta.ToString("d HH:mm:ss"),
                            dtb.ToString("d HH:mm:ss"), dr.ItemArray[4], dr.ItemArray[5], dr.ItemArray[6]);
                    }
                }
                else
                {
                    MessageBox.Show("There is NOT breaking time table. Shall create a table \n (your left-side hand) before start this process again",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (dt2.Rows.Count > 0)
                {
                    tableList.Clear();
                    cmbTable.Items.Clear();
                    foreach (DataRow dr in dt2.Rows)
                    {
                        cmbTable.Items.Add(dr.ItemArray[0]);
                        tableList.Add(dr.ItemArray[0].ToString());
                    }
                }
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (cmbTable.SelectedIndex > -1)
            {
                string table = cmbTable.Text;
                SqlClass sql = new SqlClass();
                bool sqlstatus = sql.BrakeTable_LoadTableSpecifySQL(table);
                if (sqlstatus)
                {
                    DataTable dte = sql.Datatable;
                    if (dte.Rows.Count > 0)
                    {
                        DataGridViewEditor.Rows.Clear();
                        foreach (DataRow dr in dte.Rows)
                        {
                            string dt11 = Convert.ToDateTime(dr.ItemArray[2]).ToString("d HH:mm:ss");
                            string dt12 = Convert.ToDateTime(dr.ItemArray[3]).ToString("d HH:mm:ss");
                            string dt21 = Convert.ToDateTime(dr.ItemArray[7]).ToString("d HH:mm");
                            string dt22 = Convert.ToDateTime(dr.ItemArray[8]).ToString("d HH:mm");
                            DataGridViewEditor.Rows.Add(dr.ItemArray[0], dr.ItemArray[1], dt11, dt12,
                                dr.ItemArray[4], dr.ItemArray[5], dr.ItemArray[6], dt21, dt22);
                        }
                    }
                }
            }
        }

        private void BntNew_Click(object sender, EventArgs e)
        {
            string start = "1 00:00:00";
            string mon = "00:00 - 00:00";
            string monstart = "1 00:00";
            DataGridViewEditor.Rows.Clear();
            for (int i = 0; i < 10; i++)
            {
                DataGridViewEditor.Rows.Add("", i + 1, start, start, mon, 00, "D", monstart, monstart);
            }
            for (int i = 11; i <= 20; i++)
            {
                DataGridViewEditor.Rows.Add("", i , start, start, mon, 00, "N", monstart, monstart);
            }
        }

        private void BtnSaveReplace_Click(object sender, EventArgs e)
        {
            if (CheckBeforeSQL() == true)
            {
                SaveReplace();
            }
            else
            {
                MessageBox.Show("Check at Red Mark is wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveNew_Click(object sender, EventArgs e)
        {
            if (CheckBeforeSQL() == true)
            {
                bool status = int.TryParse(txtTable.Text, out int newTable);
                if (status && tableList.Contains(txtTable.Text) == false)
                {
                    SaveNew(newTable.ToString());
                }
                else
                {
                    MessageBox.Show("Fill number of NEW Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Check at Red Mark is wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            EditTimeTable();
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            if (DataGridViewEditor.Rows.Count > 0 && lbHour.Text != string.Empty)
            {
                if (CheckBeforeEntry())
                {
                    MessageBox.Show("PASS", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("NOT PASS !!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEntry_Click(object sender, EventArgs e)
        {
            if (DataGridViewEditor.Rows.Count > 0 && lbHour.Text != string.Empty)
                if (CheckBeforeEntry())
                {
                    int day1 = cmbToday1.SelectedIndex;
                    int HH1 = Convert.ToInt32(cmbHH1.Text);
                    int MM1 = Convert.ToInt32(cmbMM1.Text);
                    int SS1 = Convert.ToInt32(cmbSS1.Text);
                    int day2 = cmbToday2.SelectedIndex;
                    int HH2 = Convert.ToInt32(cmbHH2.Text);
                    int MM2 = Convert.ToInt32(cmbMM2.Text);
                    int SS2 = Convert.ToInt32(cmbSS2.Text);
                    string start = string.Format("{0} {1}:{2}:{3}", day1 + 1, HH1.ToString("00"), MM1.ToString("00"), SS1.ToString("00"));
                    string stop = string.Format("{0} {1}:{2}:{3}", day2 + 1, HH2.ToString("00"), MM2.ToString("00"), SS2.ToString("00"));
                    string monitor = String.Format("{0}:{1} - {2}:{3}", cmbHH3.Text, cmbMM3.Text, cmbHH4.Text, cmbMM4.Text);
                    int r = Convert.ToInt32(lbHour.Text) - 1;
                    DataGridViewEditor.Rows[r].Cells[2].Value = start;
                    DataGridViewEditor.Rows[r].Cells[3].Value = stop;
                    DataGridViewEditor.Rows[r].Cells[4].Value = monitor;
                    DataGridViewEditor.Rows[r].Cells[5].Value = tbPeriod.Text; // (t4 - t3) * 60;


                    int day3 = cmbToday3.SelectedIndex;
                    int HH3 = Convert.ToInt32(cmbHH3.Text);
                    int MM3 = Convert.ToInt32(cmbMM3.Text);
                    int day4 = cmbToday4.SelectedIndex;
                    int HH4 = Convert.ToInt32(cmbHH4.Text);
                    int MM4 = Convert.ToInt32(cmbMM4.Text);
                    string startMonitor = string.Format("{0} {1}:{2}", day3 + 1, HH3.ToString("00"), MM3.ToString("00"));
                    string stopMonitor = string.Format("{0} {1}:{2}", day4 + 1, HH4.ToString("00"), MM4.ToString("00"));
                    DataGridViewEditor.Rows[r].Cells[7].Value = startMonitor;
                    DataGridViewEditor.Rows[r].Cells[8].Value = stopMonitor;
                    lbHour.Text = lbTable.Text = string.Empty;
                    MessageBox.Show("Alread Checked and updated at below table", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

        }




        #region Operation Loop

        private void EditTimeTable()
        {

            int rows = DataGridViewEditor.Rows.Count;
            if (rows > 0)
            {
                int r = DataGridViewEditor.CurrentRow.Index;
                lbTable.Text = DataGridViewEditor.Rows[r].Cells[0].Value.ToString();
                lbHour.Text = DataGridViewEditor.Rows[r].Cells[1].Value.ToString();
                GridViewToEditorP(DataGridViewEditor.Rows[r].Cells[2].Value.ToString(), cmbToday1, cmbHH1, cmbMM1, cmbSS1);
                GridViewToEditorP(DataGridViewEditor.Rows[r].Cells[3].Value.ToString(), cmbToday2, cmbHH2, cmbMM2, cmbSS2);
                GridViewToEditorM(DataGridViewEditor.Rows[r].Cells[7].Value.ToString(), cmbToday3, cmbHH3, cmbMM3);
                GridViewToEditorM(DataGridViewEditor.Rows[r].Cells[8].Value.ToString(), cmbToday4, cmbHH4, cmbMM4);
                tbPeriod.Text = DataGridViewEditor.Rows[r].Cells[5].Value.ToString();

            }

        }

        private void GridViewToEditorP(string datetime, ComboBox Cmbday, ComboBox Cmbhour, ComboBox Cmbminute, ComboBox Cmbsecond)
        {
            //  d HH:mm:ss
            string[] dateAndTimes = datetime.Split(' ');
            if (dateAndTimes[0] == "1")
                Cmbday.Text = "Today";
            else if (dateAndTimes[0] == "2")
                Cmbday.Text = "Tomorrow";
            string[] time = dateAndTimes[1].Split(':');
            Cmbhour.Text = time[0];
            Cmbminute.Text = time[1];
            Cmbsecond.Text = time[2];
        }

        private void GridViewToEditorM(string datetime, ComboBox Cmbday, ComboBox Cmbhour, ComboBox Cmbminute)
        {
            //  d HH:mm:ss
            string[] dateAndTimes = datetime.Split(' ');
            if (dateAndTimes[0] == "1")
                Cmbday.Text = "Today";
            else if (dateAndTimes[0] == "2")
                Cmbday.Text = "Tomorrow";
            string[] time = dateAndTimes[1].Split(':');
            Cmbhour.Text = time[0];
            Cmbminute.Text = time[1];
        }

        private int DateToSecond(string GridViewdatetime)
        {
            string[] dateAndTimes = GridViewdatetime.Split(' ');
            int day = Convert.ToInt32(dateAndTimes[0]);
            string[] time = dateAndTimes[1].Split(':');
            int hh = Convert.ToInt32(time[0]);
            int mm = Convert.ToInt32(time[1]);
            int group = time.Length;

            int ss = 0;
            if (group == 3)
                ss = Convert.ToInt32(time[2]);

            //try
            //{
            //    ss = Convert.ToInt32(time[2]);
            //}
            //catch (Exception)
            //{
            //    ss = 0;
            //}

            return day * 24 * 3600 + hh * 3600 + mm * 60 + ss;
        }

        private string DateGridViewToDateTime(string GridViewdatetime)
        {
            string[] dateAndTimes = GridViewdatetime.Split(' ');
            int day = Convert.ToInt32(dateAndTimes[0]);
            string[] time = dateAndTimes[1].Split(':');
            int hh = Convert.ToInt32(time[0]);
            int mm = Convert.ToInt32(time[1]);
            int groupTime = time.Length;
            int ss = 0;
            if (groupTime == 3)
                ss = Convert.ToInt32(time[2]);
            //try
            //{
            //    ss = Convert.ToInt32(time[2]);
            //}
            //catch (Exception)
            //{
            //    ss = 0;
            //}
            DateTime dt = new DateTime(1900, 1, day, hh, mm, ss);
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        #endregion Operation Loop

        private void Roles2()
        {

            BtnSaveReplace.Enabled = false;
            BtnSaveNew.Enabled = false;

            if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager)// production
            {

            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {
                BtnSaveReplace.Enabled = true;
                BtnSaveNew.Enabled = true;


            }
            else if (User.Role == Models.Roles.Admin_Min)  // Admin -Mini
            {
                BtnSaveReplace.Enabled = true;
                BtnSaveNew.Enabled = true;

            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnSaveReplace.Enabled = true;
                BtnSaveNew.Enabled = true;

            }

        }

        private bool CheckBeforeSQL()
        {
            bool result = true;
            List<int> monList = new List<int>();
            List<int> startstopList = new List<int>();
            DataGridViewCellStyle styleNormal = new DataGridViewCellStyle
            {
                ForeColor = Color.Black
            };
            DataGridViewCellStyle styleAbnormal = new DataGridViewCellStyle
            {
                ForeColor = Color.Red
            };

            if (DataGridViewEditor.Rows.Count > 0)
            {
                for (int r = 0; r < 20; r++)
                {
                    startstopList.Add(DateToSecond(DataGridViewEditor.Rows[r].Cells[2].Value.ToString()));
                    startstopList.Add(DateToSecond(DataGridViewEditor.Rows[r].Cells[2].Value.ToString()));
                    monList.Add(DateToSecond(DataGridViewEditor.Rows[r].Cells[7].Value.ToString()));
                    monList.Add(DateToSecond(DataGridViewEditor.Rows[r].Cells[8].Value.ToString()));

                }
                for (int i = 0; i < 20; i++)
                {
                    DataGridViewEditor.Rows[i].Cells[7].Style = styleNormal;
                    DataGridViewEditor.Rows[i].Cells[2].Style = styleNormal;
                }
                for (int i = 1; i < 40; i++)
                {
                    if (monList[i - 1] <= monList[i])
                    {
                    }
                    else
                    {
                        DataGridViewEditor.Rows[i / 2].Cells[7].Style = styleAbnormal;
                        result = false;
                    }

                    if (startstopList[i - 1] <= startstopList[i])
                    {
                    }
                    else
                    {
                        DataGridViewEditor.Rows[i / 2].Cells[2].Style = styleAbnormal;
                        result = false;
                    }
                }
            }
            return result;
        }

        private void SaveReplace()
        {
            var query = new StringBuilder();
            for (int i = 0; i < 20; i++)
            {
                string table = DataGridViewEditor.Rows[i].Cells[0].Value.ToString();
                string hour = DataGridViewEditor.Rows[i].Cells[1].Value.ToString();
                string start = DateGridViewToDateTime(DataGridViewEditor.Rows[i].Cells[2].Value.ToString());
                string stop = DateGridViewToDateTime(DataGridViewEditor.Rows[i].Cells[3].Value.ToString());
                string mon = DataGridViewEditor.Rows[i].Cells[4].Value.ToString();
                string period = DataGridViewEditor.Rows[i].Cells[5].Value.ToString();
                string startMonitor = DateGridViewToDateTime(DataGridViewEditor.Rows[i].Cells[7].Value.ToString());
                string stopMonitor = DateGridViewToDateTime(DataGridViewEditor.Rows[i].Cells[8].Value.ToString());
                query.AppendFormat("UPDATE [Production].[dbo].[Prod_TimeBreakTable]  ");
                query.AppendFormat("SET startTime='{0}',stopTime='{1}',monitor='{2}',period={3} ,startTimeMonitor='{4}',stopTimeMonitor='{5}' ", start, stop, mon, period, startMonitor, stopMonitor);
                query.AppendFormat(" WHERE divisionID='{0}' and plantID='{1}' and breakQueue={2} and hourNo={3} \n", User.Division, User.Plant, table, hour);
            }
            var a = query.ToString();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.BrakeTable_SaveReplaceSQL(a);
            if (sqlstatus)
            {
                MessageBox.Show("Save Update Brake Time Table Completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveNew(string table)
        {
            var query = new StringBuilder();
            query.AppendFormat("INSERT INTO [Production].[dbo].[Prod_TimeBreakTable] ([divisionID],[plantID],[breakQueue] ,[hourNo] ,[startTime],[stopTime],[monitor] ,[period],[dayNight],startTimeMonitor,stopTimeMonitor) VALUES \n");
            for (int i = 0; i < 20; i++)
            {
                string hour = DataGridViewEditor.Rows[i].Cells[1].Value.ToString();
                string start = DateGridViewToDateTime(DataGridViewEditor.Rows[i].Cells[2].Value.ToString());
                string stop = DateGridViewToDateTime(DataGridViewEditor.Rows[i].Cells[3].Value.ToString());
                string mon = DataGridViewEditor.Rows[i].Cells[4].Value.ToString();
                string period = DataGridViewEditor.Rows[i].Cells[5].Value.ToString();
                string daynight = DataGridViewEditor.Rows[i].Cells[6].Value.ToString();
                string startMonitor = DateGridViewToDateTime(DataGridViewEditor.Rows[i].Cells[7].Value.ToString());
                string stopMonitor = DateGridViewToDateTime(DataGridViewEditor.Rows[i].Cells[8].Value.ToString());
                query.AppendFormat("('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},'{8}','{9}','{10}')", User.Division, User.Plant, table, hour, start, stop, mon, period, daynight, startMonitor, stopMonitor);
                if (i < 19) query.Append(",\n");
            }
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.BrakeTable_SaveSQL(query.ToString());
            if (sqlstatus)
            {
                tableList.Add(table);
                MessageBox.Show("Save Brake Time Table Completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckBeforeEntry()
        {
            bool result;
            try
            {
                int day1 = cmbToday1.SelectedIndex;
                int HH1 = Convert.ToInt32(cmbHH1.Text);
                int MM1 = Convert.ToInt32(cmbMM1.Text);
                int SS1 = Convert.ToInt32(cmbSS1.Text);
                int day2 = cmbToday2.SelectedIndex;
                int HH2 = Convert.ToInt32(cmbHH2.Text);
                int MM2 = Convert.ToInt32(cmbMM2.Text);
                int SS2 = Convert.ToInt32(cmbSS2.Text);

                int day3 = cmbToday3.SelectedIndex;
                int HH3 = Convert.ToInt32(cmbHH3.Text);
                int MM3 = Convert.ToInt32(cmbMM3.Text);
                int day4 = cmbToday4.SelectedIndex;
                int HH4 = Convert.ToInt32(cmbHH4.Text);
                int MM4 = Convert.ToInt32(cmbMM4.Text);
                /// Check before insert /////
                int t1 = day1 * 24 * 3600 + HH1 * 3600 + MM1 * 60 + SS1;
                int t2 = day2 * 24 * 3600 + HH2 * 3600 + MM2 * 60 + SS2;
                int t3 = day3 * 24 * 60 + HH3 * 60 + MM3;
                int t4 = day4 * 24 * 60 + HH4 * 60 + MM4;
                tbPeriod.Text = ((t4 - t3) * 60).ToString();

                //DateTime d1 = new DateTime(1900, 1, 1 + day3, HH3, MM3, 00);
                //DateTime d2 = new DateTime(1900, 1, 1 + day4, HH4, MM4, 00);
                //int diff = Convert.ToInt32((d2 - d1).TotalSeconds);
                //tbPeriod.Text = diff.ToString();

                //bool perstatus = int.TryParse(tbPeriod.Text, out int period);

                if (t1 <= t2 && t3 <= t4)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                result = false;
                MessageBox.Show("", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void DataGridViewInitial2()
        {
            lbTable.Text = lbHour.Text = string.Empty;
            this.dataGridViewList.ColumnCount = 7;
            this.dataGridViewList.Columns[0].Name = "Table";
            this.dataGridViewList.Columns[0].Width = 40;
            this.dataGridViewList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewList.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewList.Columns[1].Name = "Hour";
            this.dataGridViewList.Columns[1].Width = 40;
            this.dataGridViewList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewList.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewList.Columns[2].Name = "Start Time";
            this.dataGridViewList.Columns[2].Width = 80;
            this.dataGridViewList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewList.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewList.Columns[3].Name = "End Time";
            this.dataGridViewList.Columns[3].Width = 80;
            this.dataGridViewList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewList.Columns[4].Name = "Monitor";
            this.dataGridViewList.Columns[4].Width = 110;
            this.dataGridViewList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewList.Columns[5].Name = "Period";
            this.dataGridViewList.Columns[5].Width = 50;
            this.dataGridViewList.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewList.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewList.Columns[6].Name = "DayNight";
            this.dataGridViewList.Columns[6].Width = 50;
            this.dataGridViewList.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewList.RowHeadersWidth = 4;
            this.dataGridViewList.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewList.RowTemplate.Height = 25;
            this.dataGridViewList.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.dataGridViewList.AllowUserToResizeRows = false;
            this.dataGridViewList.AllowUserToResizeColumns = false;

            this.DataGridViewEditor.ColumnCount = 9;
            this.DataGridViewEditor.Columns[0].Name = "Table";
            this.DataGridViewEditor.Columns[0].Width = 40;
            this.DataGridViewEditor.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewEditor.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewEditor.Columns[1].Name = "Hour";
            this.DataGridViewEditor.Columns[1].Width = 40;
            this.DataGridViewEditor.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewEditor.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewEditor.Columns[2].Name = "Start Time";
            this.DataGridViewEditor.Columns[2].Width = 80;
            this.DataGridViewEditor.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewEditor.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewEditor.Columns[3].Name = "End Time";
            this.DataGridViewEditor.Columns[3].Width = 80;
            this.DataGridViewEditor.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewEditor.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewEditor.Columns[4].Name = "Monitor";
            this.DataGridViewEditor.Columns[4].Width = 100;
            this.DataGridViewEditor.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewEditor.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewEditor.Columns[5].Name = "Period";
            this.DataGridViewEditor.Columns[5].Width = 50;
            this.DataGridViewEditor.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewEditor.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewEditor.Columns[6].Name = "DayNight";
            this.DataGridViewEditor.Columns[6].Width = 50;
            this.DataGridViewEditor.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewEditor.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewEditor.Columns[7].Name = "StartTimeMonitor";
            this.DataGridViewEditor.Columns[7].Width = 80;
            this.DataGridViewEditor.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewEditor.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewEditor.Columns[8].Name = "StopTimeMonitor";
            this.DataGridViewEditor.Columns[8].Width = 80;
            this.DataGridViewEditor.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewEditor.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DataGridViewEditor.RowHeadersWidth = 4;
            this.DataGridViewEditor.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewEditor.RowTemplate.Height = 25;
            this.DataGridViewEditor.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DataGridViewEditor.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.DataGridViewEditor.AllowUserToResizeRows = false;
            this.DataGridViewEditor.AllowUserToResizeColumns = false;


        }

        #endregion

        //private void dataGridViewEditor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    EditTimeTable();
        //}

        private void DataGridViewEditor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EditTimeTable();
        }

    }
}
