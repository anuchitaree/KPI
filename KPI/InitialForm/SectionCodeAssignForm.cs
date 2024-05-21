using KPI.Class;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KPI.InitialForm
{
    public partial class SectionCodeAssignForm : Form
    {
        readonly List<string> selectedSection = new List<string>();
        readonly List<int> selectedID = new List<int>();


        internal List<EmpManPower> empList = new List<EmpManPower>();
        internal List<EmpSection> sectionList = new List<EmpSection>();
        public SectionCodeAssignForm()
        {
            InitializeComponent();
            InitialDataGridView();
        }

        private void SectionCodeAssignForm_Load(object sender, EventArgs e)
        {
            Roles();
        }


        #region Name and Section ListUp
        private void BtnListUp_Click(object sender, EventArgs e)
        {
            LoadData();
        }



        private void DataGridViewEmpList_Click(object sender, EventArgs e)
        {
            if (DgvEmpList.Rows.Count > 0)
            {
                int index = DgvEmpList.CurrentRow.Index;
                int id = Convert.ToInt32(DgvEmpList.Rows[index].Cells[1].Value);
                string name = Convert.ToString(DgvEmpList.Rows[index].Cells[2].Value);
                if (selectedID.Contains(id) == false)
                {
                    DgvSelectedName.Rows.Add(id, name);
                    selectedID.Add(id);
                }
            }
        }

        private void DataGridViewSectionList_Click(object sender, EventArgs e)
        {
            if (DgvSectionList.Rows.Count > 0)
            {
                int index = DgvSectionList.CurrentRow.Index;
                string section = Convert.ToString(DgvSectionList.Rows[index].Cells[1].Value);
                string name = Convert.ToString(DgvSectionList.Rows[index].Cells[2].Value);
                if (selectedSection.Contains(section) == false)
                {
                    DgvSelectedSection.Rows.Add(section, name);
                    selectedSection.Add(section);
                }
            }
        }

        #endregion


        #region Name and Section Assignment

        private void TbEmpID_TextChanged(object sender, EventArgs e)
        {
            if (TbEmpID.Text.Length < 7)
                return;

            int id = Convert.ToInt32(TbEmpID.Text);
            var exist = empList.FirstOrDefault(x => x.Id == id);
            if (exist != null)
            {
                if (!selectedID.Contains(id))
                {


                    DgvSelectedName.Rows.Add(exist.Id, exist.FullName);
                    selectedID.Add(id);
                    SectionCodeList(TbEmpID.Text, DgvSelectedSection);
                }
            }


        }

        private void TbSection_TextChanged(object sender, EventArgs e)
        {
            if (TbSection.Text.Length < 7)
                return;

            var section = sectionList.FirstOrDefault(x => x.SectionCode == TbSection.Text);
            if (section != null)
            {
                if (!selectedSection.Contains(TbSection.Text))
                {
                    DgvSelectedSection.Rows.Add(section.SectionCode, section.SectionName);
                    selectedSection.Add(TbSection.Text);
                }
            }

        }

        private void BtnClearName_Click(object sender, EventArgs e)
        {
            DgvSelectedName.Rows.Clear();
            selectedID.Clear();
            TbEmpID.Text = "";
        }

        private void BtnClearSection_Click(object sender, EventArgs e)
        {
            DgvSelectedSection.Rows.Clear();
            selectedSection.Clear();
            TbSection.Text = string.Empty;
        }

        private void BtnAllSectionCopy_Click(object sender, EventArgs e)
        {
            DgvSelectedSection.Rows.Clear();
            selectedSection.Clear();
            foreach (var item in sectionList)
            {
                DgvSelectedSection.Rows.Add(item.SectionCode, item.SectionName);
                selectedSection.Add(item.SectionCode);
            }

        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }


        private void DataGridViewSelectedName_DoubleClick(object sender, EventArgs e)
        {
            if (DgvSelectedName.Rows.Count > 0)
            {
                int index = DgvSelectedName.CurrentRow.Index;
                int id = Convert.ToInt32(DgvSelectedName.Rows[index].Cells[0].Value);
                DgvSelectedName.Rows.RemoveAt(index);
                selectedID.Remove(id);
            }
        }

        private void DataGridViewSelectedSection_DoubleClick(object sender, EventArgs e)
        {
            if (DgvSelectedSection.Rows.Count > 0)
            {
                int index = DgvSelectedSection.CurrentRow.Index;
                string section = Convert.ToString(DgvSelectedSection.Rows[index].Cells[0].Value);
                DgvSelectedSection.Rows.RemoveAt(index);
                selectedSection.Remove(section);
            }
        }

        #endregion


        private void TbEmpIDList_TextChanged(object sender, EventArgs e)
        {
            if (Converting.IsNumeric(TbEmpIDList.Text) == false)
            {
                MessageBox.Show("Please fill only numberical 7 digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (TbEmpIDList.Text.Length != 7)
            {
                return;
            }

            DgvAuthen.Rows.Clear();
            using (var context = new ProductionEntities11())
            {
                int id = Convert.ToInt32(TbEmpIDList.Text);
                var ext = context.Emp_AuthorizationTable.Where(i => i.userID == id);
                if (ext != null)
                {
                    var sectionlist = (from s in context.Emp_AuthorizationTable.Where(i => i.userID == id)
                                       join x in context.Emp_SectionTable
                                       on s.sectionCode equals x.sectionCode into ps
                                       from x in ps.DefaultIfEmpty()
                                       select new EmpSection
                                       {
                                           SectionCode = s.sectionCode,
                                           SectionName = x.sectionName
                                       }).ToList();

                    foreach (var item in sectionlist)
                    {
                        DgvAuthen.Rows.Add(item.SectionCode, item.SectionName);
                    }

                }

            }


        }



        // /////////////////////////////// OPERATION LOOP //////////////////////////////////////////

        private void SectionCodeList(string empId, DataGridView dgv)
        {
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.AssignSection_LoadAuthorizationSQL(empId);
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string code = dt1.Rows[i].ItemArray[0].ToString();
                        string name = dt1.Rows[i].ItemArray[1].ToString();
                        if (!selectedSection.Contains(code))
                        {
                            selectedSection.Add(code);
                            dgv.Rows.Add(code, name);
                        }
                    }
                }
            }
        }


        private void LoadData()
        {
            using (var context = new ProductionEntities11())
            {
                empList = context.Emp_ManPowersTable
                   .Select(x => new EmpManPower
                   {
                       Id = x.userID,
                       FullName = x.fullName,
                       Grade = x.grade
                   }).ToList();

                sectionList = context.Emp_SectionTable
                    .Select(x => new EmpSection
                    {
                        SectionCode = x.sectionCode,
                        SectionName = x.sectionName
                    }).ToList();

                int i = 1;
                foreach (var item in empList)
                {
                    DgvEmpList.Rows.Add(i, item.Id, item.FullName, item.Grade);
                    i++;
                }
                i = 1;
                foreach (var item in sectionList)
                {
                    DgvSectionList.Rows.Add(i, item.SectionCode, item.SectionName);
                    i++;
                }

            }

        }


        private void Save()
        {
            int row = DgvSelectedName.RowCount;
            if (row == 0)
            {
                MessageBox.Show("Select a name by double click left-side hand or Key EmpId", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            using (var context = new ProductionEntities11())
            {
                try
                {
                    for (int i = 0; i < row; i++)
                    {
                        int id = Convert.ToInt32(DgvSelectedName.Rows[i].Cells[0].Value);
                        var removeId = context.Emp_AuthorizationTable.Where(x => x.userID == id);
                        if (removeId != null)
                        {
                            context.Emp_AuthorizationTable.RemoveRange(removeId);
                        }
                    }
                    context.SaveChanges();

                    int rowsection = DgvSelectedSection.RowCount;
                    if (rowsection == 0)
                    {
                        MessageBox.Show("Delete section was assigned in above Id", "information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    var emp_AuthorizationTable = new List<Emp_AuthorizationTable>();
                    for (int i = 0; i < row; i++)
                    {
                        int id = Convert.ToInt32(DgvSelectedName.Rows[i].Cells[0].Value);
                        for (int j = 0; j < rowsection; j++)
                        {
                            string section = Convert.ToString(DgvSelectedSection.Rows[j].Cells[0].Value);
                            var addSection = new Emp_AuthorizationTable()
                            {
                                userID = id,
                                sectionCode = section,
                            };
                            emp_AuthorizationTable.Add(addSection);
                        }
                    }
                    context.Emp_AuthorizationTable.AddRange(emp_AuthorizationTable);
                    context.SaveChanges();
                    MessageBox.Show("Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {
                    MessageBox.Show("Try it again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }


        }



        #region InitialDataGridView
        private void InitialDataGridView()
        {
            string[] header = new string[] { "No", "Emp ID", "Full name", "Grade" };
            int[] width = new int[] { 40, 80, 200, 40 };
            DataGridViewSetup.Norm1(DgvEmpList, header, width);
 
            string[] header1 = new string[] { "No", "Section Code", "Section name" };
            int[] width1 = new int[] { 40, 70, 200, };
            DataGridViewSetup.Norm1(DgvSectionList, header1, width1);

            string[] header2 = new string[] { "Emp ID", "Full name" };
            int[] width2 = new int[] { 100, 200, };
            DataGridViewSetup.Norm1(DgvSelectedName, header2, width2);

            string[] header3 = new string[] {  "Section Code", "Section name" };
            int[] width3 = new int[] { 100, 200, };
            DataGridViewSetup.Norm1(DgvSelectedSection, header3, width3);
            DataGridViewSetup.Norm1(DgvAuthen, header3, width3);
        }

        private void Roles()
        {
            BtnSave.Enabled = false;
            BtnAllSectionCopy.Enabled = false;
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
                BtnSave.Enabled = true;
                BtnAllSectionCopy.Enabled = true;
            }

        }


        #endregion




    }
}
