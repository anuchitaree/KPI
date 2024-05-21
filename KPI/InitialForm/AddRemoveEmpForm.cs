using KPI.Class;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KPI.InitialForm
{
    public partial class AddRemoveEmpForm : Form
    {
        readonly List<int> EmpIdList = new List<int>();
        readonly List<KeyValuePair<string, int>> Grade = new List<KeyValuePair<string, int>>();
        readonly List<KeyValuePair<string, string>> Role = new List<KeyValuePair<string, string>>();
        readonly List<KeyValuePair<string, string>> _Division = new List<KeyValuePair<string, string>>();
        readonly List<KeyValuePair<string, string>> _Plant = new List<KeyValuePair<string, string>>();

        int index = 0;


        internal List<EmpRole> Rolelist = new List<EmpRole>();
        public AddRemoveEmpForm()
        {
            InitializeComponent();
            InitialDataGridView();
        }

        private void AddRemoveEmpForm_Load(object sender, EventArgs e)
        {




            for (int i = 0; i < 12; i++)
            {
                string val = String.Format("G{0}", 12 - i);
                Grade.Add(new KeyValuePair<string, int>(val, i));
            }
            comboBoxG.DataSource = new BindingSource(Grade, null);
            comboBoxG.DisplayMember = "key";
            comboBoxG.ValueMember = "Value";


            using (var db = new ProductionEntities11())
            {
                Rolelist = db.Emp_Roles.OrderBy(s => s.sort)
                    .Select(s => new EmpRole { Id = s.Id, Role = s.role }).ToList();

                CmbRole.DataSource = Rolelist;
                CmbRole.ValueMember = "Id";
                CmbRole.DisplayMember = "Role";
            }



            //Role.Add(new KeyValuePair<string, string>("invalid", "0"));
            //Role.Add(new KeyValuePair<string, string>("General", "1"));
            //Role.Add(new KeyValuePair<string, string>("Production TL", "2"));
            //Role.Add(new KeyValuePair<string, string>("Fac Eng", "3"));
            //Role.Add(new KeyValuePair<string, string>("Admin-Mini", "4"));
            //Role.Add(new KeyValuePair<string, string>("Admin-Full", "5"));


            //CmbRole.DataSource = new BindingSource(Role, null);
            //CmbRole.DisplayMember = "key";
            //CmbRole.ValueMember = "Value";





            _Division.Add(new KeyValuePair<string, string>("ESD", "0"));
            _Division.Add(new KeyValuePair<string, string>("PEM", "1"));
            _Division.Add(new KeyValuePair<string, string>("TSD", "2"));
            comboBoxDivision.DataSource = new BindingSource(_Division, null);
            comboBoxDivision.DisplayMember = "key";
            comboBoxDivision.ValueMember = "Value";

            _Plant.Add(new KeyValuePair<string, string>("BPK", "0"));
            _Plant.Add(new KeyValuePair<string, string>("WGR", "1"));
            _Plant.Add(new KeyValuePair<string, string>("SRG", "2"));
            comboBoxPlant.DataSource = new BindingSource(_Plant, null);
            comboBoxPlant.DisplayMember = "key";
            comboBoxPlant.ValueMember = "Value";

            comboBoxPlant.SelectedIndex = -1;
            comboBoxDivision.SelectedIndex = -1;
            CmbRole.SelectedIndex = -1;
            comboBoxG.SelectedIndex = -1;
            CmbDiv.SelectedIndex = 0;
            CmbPlant.SelectedIndex = 0;

            Roles();
        }




        private void BtnRead_Click(object sender, EventArgs e)
        {
            ReadEmpTable();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (int.Parse(TbID.Text) > 0 && TbID.Text.Length == 7)
            {
                AddEmpTable();
            }
            else
            {
                MessageBox.Show("ID length = 7  ex 1234567 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateEmpTable();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteEmpTable();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TbID.Text = textBoxName.Text = textBoxEmail.Text = textBoxPhone.Text = comboBoxG.Text = "";
            comboBoxG.SelectedIndex = -1;
            CmbRole.SelectedIndex = -1;
            comboBoxPlant.SelectedIndex = -1;
            comboBoxDivision.SelectedIndex = -1;
            TbID.ReadOnly = false;
        }

        private void DataGridViewEmp_Click(object sender, EventArgs e)
        {
            if (DataGridViewEmp.RowCount > 0)
            {
                index = DataGridViewEmp.CurrentRow.Index;
            }
        }

        private void DataGridViewEmp_DoubleClick(object sender, EventArgs e)
        {
            index = DataGridViewEmp.CurrentRow.Index;
            TbID.ReadOnly = true;
            TbID.Text = Convert.ToString(DataGridViewEmp.Rows[index].Cells[1].Value);
            textBoxName.Text = Convert.ToString(DataGridViewEmp.Rows[index].Cells[2].Value);
            textBoxEmail.Text = Convert.ToString(DataGridViewEmp.Rows[index].Cells[3].Value);
            textBoxPhone.Text = Convert.ToString(DataGridViewEmp.Rows[index].Cells[4].Value);
            string G = Convert.ToString(DataGridViewEmp.Rows[index].Cells[4].Value);
            G = G.Trim();
            int i = 0;
            foreach (KeyValuePair<string, int> item in Grade)
            {
                if (item.Key == G)
                {
                    comboBoxG.SelectedIndex = i;
                    break;
                }
                i += 1;
            }

            string role = Convert.ToString(DataGridViewEmp.Rows[index].Cells[6].Value);
            CmbRole.SelectedIndex = Rolelist.FindIndex(s => s.Role == role);


            string division = Convert.ToString(DataGridViewEmp.Rows[index].Cells[7].Value);
            int index2 = Convert.ToInt32(_Division.FirstOrDefault(x => x.Key == division).Value);
            comboBoxDivision.SelectedIndex = index2;

            string plant = Convert.ToString(DataGridViewEmp.Rows[index].Cells[8].Value);
            int index3 = Convert.ToInt32(_Plant.FirstOrDefault(x => x.Key == plant).Value);
            comboBoxPlant.SelectedIndex = index3;
        }

        private void TbID_TextChanged(object sender, EventArgs e)
        {
            AutoLoadEmp();
        }

        private void TbID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                AutoLoadEmp();
            }
        }




        // /////////////////////////////// OPERATION LOOP //////////////////////////////////////////
        #region InitialDataGridView
        private void InitialDataGridView()
        {
            string[] header = new string[] { "No","รหัสพนักงาน","ชื่อ นามสกุล","อีเมล์","เบอร์โทรศัพท์","เกรด",
                "หน้าที่","Division","Plant"};
            int[] width = new int[] { 40,100,200,400,150,
                150,120,80,80};
            DataGridViewSetup.Norm1(DataGridViewEmp, header, width);

        }
        #endregion


        private void Roles()
        {
            //btn301.Enabled = false;
            BtnAdd.Enabled = false;
            BtnClear.Enabled = false;
            BtnDelete.Enabled = false;
            BtnRead.Enabled = false;
            BtnUpdate.Enabled = false;
            if (User.Role == Models.Roles.General) // genereal
            {

            }
            if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager) // production
            {

            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {

            }
            else if (User.Role == Models.Roles.Admin_Min)  // Admin -Mini
            {
                BtnRead.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnAdd.Enabled = true;
                BtnClear.Enabled = true;
                BtnDelete.Enabled = true;
                BtnRead.Enabled = true;
                BtnUpdate.Enabled = true;
            }

        }

        private void ReadEmpTable()
        {
            string div = CmbDiv.Text;
            string pl = CmbPlant.Text;
            using (var db = new ProductionEntities11())
            {
                var empList = new List<Emp_ManPowersTable>();
                if (CmbDiv.SelectedIndex == 0 && CmbPlant.SelectedIndex == 0)
                {
                    empList = db.Emp_ManPowersTable.OrderBy(u => u.userID).ToList();
                }
                else if (CmbDiv.SelectedIndex == 1 && CmbPlant.SelectedIndex == 0)
                {
                    empList = db.Emp_ManPowersTable.Where(d => d.divisionID == CmbDiv.Text)
                        .OrderBy(u => u.userID).ToList();
                }
                else if (CmbDiv.SelectedIndex == 0 && CmbPlant.SelectedIndex == 1)
                {
                    empList = db.Emp_ManPowersTable.Where(p => p.plantID == CmbPlant.Text)
                        .OrderBy(u => u.userID).ToList();
                }
                else if (CmbDiv.SelectedIndex != 0 && CmbPlant.SelectedIndex != 0)
                {
                    empList = db.Emp_ManPowersTable.Where(d => d.divisionID == CmbDiv.Text)
                        .Where(p => p.plantID == CmbPlant.Text)
                       .OrderBy(u => u.userID).ToList();
                }
                DataGridViewEmp.Rows.Clear();
                EmpIdList.Clear();
                int count = 1;
                foreach (Emp_ManPowersTable m in empList)
                {
                    object[] emp = new object[9];

                    EmpIdList.Add(m.userID);
                    emp[0] = count;
                    emp[1] = m.userID; // id
                    emp[2] = m.fullName; // name
                    emp[3] = m.email; // email
                    emp[4] = m.phone; // phone
                    emp[5] = m.grade; // grade

                    emp[6] = Rolelist.Where(r => r.Id == m.roles).FirstOrDefault().Role; // function
                    emp[7] = m.divisionID; // division
                    emp[8] = m.plantID; // plant
                    DataGridViewEmp.Rows.Add(emp);
                    count++;
                }
            }



        }

        //private void ReadEmpTable1()  // Wait delete 
        //{
        //    string where = string.Empty;
        //    string div = CmbDiv.Text;
        //    string pl = CmbPlant.Text;
        //    if (CmbDiv.Text == "" && CmbPlant.Text == "")
        //    {
        //        where = string.Format("");
        //    }
        //    else if (CmbDiv.Text != "" && CmbPlant.Text != "")
        //    {
        //        where = string.Format("WHERE [DivisionId]='{0}' and [PlantId]='{1}'", div, pl);
        //    }
        //    else if (CmbDiv.Text != "")
        //    {
        //        where = string.Format("WHERE [DivisionID]='{0}' ", div);
        //    }
        //    else if (CmbPlant.Text != "")
        //    {
        //        where = string.Format("WHERE [PlantId]='{0}'", pl);
        //    }

        //    empid.Clear();
        //    SqlClass sql = new SqlClass();
        //    bool sqlstatus = sql.EMP_LoadAllSQL(where);
        //    if (sqlstatus)
        //    {
        //        DataGridViewEmp.Rows.Clear();
        //        DataTable dt1 = sql.Datatable;
        //        if (dt1.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt1.Rows.Count; i++)
        //            {
        //                string[] emp = new string[9];
        //                empid.Add(Convert.ToString(dt1.Rows[i].ItemArray[0]));
        //                emp[0] = (i + 1).ToString(); // No
        //                emp[1] = dt1.Rows[i].ItemArray[0] == null ? "0" : dt1.Rows[i].ItemArray[0].ToString(); // id
        //                emp[2] = dt1.Rows[i].ItemArray[1] == null ? "0" : dt1.Rows[i].ItemArray[1].ToString(); // name
        //                emp[3] = dt1.Rows[i].ItemArray[2] == null ? "0" : dt1.Rows[i].ItemArray[2].ToString(); // email
        //                emp[4] = dt1.Rows[i].ItemArray[3] == null ? "0" : dt1.Rows[i].ItemArray[3].ToString(); // phone
        //                emp[5] = dt1.Rows[i].ItemArray[4] == null ? "0" : dt1.Rows[i].ItemArray[4].ToString(); // grade



        //                emp[6] = dt1.Rows[i].ItemArray[5] == null ? "0" : dt1.Rows[i].ItemArray[5].ToString(); // function

        //                emp[7] = dt1.Rows[i].ItemArray[6] == null ? "0" : dt1.Rows[i].ItemArray[6].ToString(); // division
        //                emp[8] = dt1.Rows[i].ItemArray[7] == null ? "0" : dt1.Rows[i].ItemArray[7].ToString(); // plant

        //                emp[6] = Role.FirstOrDefault(x => x.Value == emp[6]).Key;
        //                DataGridViewEmp.Rows.Add(emp);

        //            }
        //        }
        //    }

        //}


        private void AddEmpTable()
        {
            if (Converting.IsNumeric(TbID.Text) == false)
            {
                MessageBox.Show("Please fill only numberical", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
                

            int eid = Convert.ToInt32(TbID.Text);
            if (!EmpIdList.Contains(eid) && CmbRole.SelectedIndex != -1 )
            {

                DialogResult r = MessageBox.Show("Are you sure ? Confrim add new employee ID " + TbID.Text, "Add Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == r)
                {
                    using (var db = new ProductionEntities11())
                    {
                        try
                        {
                            var add = new Emp_ManPowersTable()
                            {
                                userID = Convert.ToInt32(eid),
                                fullName = textBoxName.Text.Trim(),
                                email = textBoxEmail.Text.Trim(),
                                phone = textBoxPhone.Text.Trim(),
                                grade = comboBoxG.Text.Trim(),
                                roles = (int)CmbRole.SelectedValue,
                                divisionID = comboBoxDivision.Text,
                                plantID = comboBoxPlant.Text,
                            };
                            db.Emp_ManPowersTable.Add(add);
                            db.SaveChanges();
                            TbID.Text = textBoxName.Text = textBoxEmail.Text = textBoxPhone.Text = comboBoxG.Text = "";
                            CmbRole.SelectedIndex = -1;
                            comboBoxG.SelectedIndex = -1;
                            ReadEmpTable();
                            MessageBox.Show("Add employee completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {

                            MessageBox.Show("Try it again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                    }
                }
            }
            else
            {
                MessageBox.Show("already has the emp ID or No input function", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateEmpTable()
        {
            if (TbID.Text == "")
            {
                MessageBox.Show("NO Emp ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Converting.IsNumeric(TbID.Text) == false)
            {
                MessageBox.Show("Please fill only numberical", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            int eid = Convert.ToInt32(TbID.Text);

            if (!EmpIdList.Contains(eid))
            {
                MessageBox.Show("already has the emp ID", "Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult r = MessageBox.Show("Are you sure ? Confrim update employee ID " + TbID.Text, "Update Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes == r)
            {
                using (var db = new ProductionEntities11())
                {
                    try
                    {
                        var exist = db.Emp_ManPowersTable.SingleOrDefault(u => u.userID == eid);
                        if (exist != null)
                        {
                            exist.fullName = textBoxName.Text.Trim();
                            exist.email = textBoxEmail.Text.Trim();
                            exist.phone = textBoxPhone.Text.Trim();
                            exist.grade = comboBoxG.Text.Trim();
                            exist.roles = (int)CmbRole.SelectedValue;
                            exist.divisionID = comboBoxDivision.Text;
                            exist.plantID = comboBoxPlant.Text;
                            db.SaveChanges();

                            TbID.Text = textBoxName.Text = textBoxEmail.Text = textBoxPhone.Text = comboBoxG.Text = "";
                            CmbRole.SelectedIndex = -1;
                            comboBoxG.SelectedIndex = -1;
                            ReadEmpTable();
                            MessageBox.Show("Add employee completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Try it again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }


            }

        }


        private void DeleteEmpTable()
        {
            int id = Convert.ToInt32(DataGridViewEmp.Rows[index].Cells[1].Value);
            DialogResult r = MessageBox.Show("Are you sure ? Confrim delete employee ID " + TbID.Text, "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes != r)
                return;

            using (var db = new ProductionEntities11())
            {
                try
                {
                    var userRemove = db.Emp_ManPowersTable.SingleOrDefault(x => x.userID == id);
                    if (userRemove != null)
                    {
                        db.Emp_ManPowersTable.Remove(userRemove);
                        db.SaveChanges();
                        ReadEmpTable();
                        TbID.Text = textBoxName.Text = textBoxEmail.Text = textBoxPhone.Text = comboBoxG.Text = "";
                        comboBoxG.SelectedIndex = -1;
                        CmbRole.SelectedIndex = -1;
                        MessageBox.Show("Delete employee completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
               

            }
        }




        private void AutoLoadEmp()
        {
            if (DataGridViewEmp.Rows.Count > 0)
            {
                if (TbID.Text.Length == 7)
                {
                    int keyid = Convert.ToInt32(TbID.Text);
                    if (EmpIdList.Contains(keyid) == true)
                    {
                        int no = EmpIdList.IndexOf(keyid);
                        TbID.Text = Convert.ToString(DataGridViewEmp.Rows[no].Cells[1].Value);
                        textBoxName.Text = Convert.ToString(DataGridViewEmp.Rows[no].Cells[2].Value);
                        textBoxEmail.Text = Convert.ToString(DataGridViewEmp.Rows[no].Cells[3].Value);
                        textBoxPhone.Text = Convert.ToString(DataGridViewEmp.Rows[no].Cells[4].Value);
                        comboBoxG.Text = Convert.ToString(DataGridViewEmp.Rows[no].Cells[5].Value);

                        string role = Convert.ToString(DataGridViewEmp.Rows[no].Cells[6].Value);
                        CmbRole.SelectedIndex = Rolelist.FindIndex(s => s.Role == role);

                        string division = Convert.ToString(DataGridViewEmp.Rows[no].Cells[7].Value);
                        comboBoxDivision.SelectedIndex = Convert.ToInt32(_Division.FirstOrDefault(x => x.Key == division).Value);

                        string plant = Convert.ToString(DataGridViewEmp.Rows[no].Cells[8].Value);
                        comboBoxPlant.SelectedIndex = Convert.ToInt32(_Plant.FirstOrDefault(x => x.Key == plant).Value);
                        DataGridViewEmp.FirstDisplayedScrollingRowIndex = no;


                    }
                }
            }
        }

       
    }
}
