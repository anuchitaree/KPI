using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using KPI.Parameter;
using KPI.Models;
using KPI.KeepClass;
using KPI.Class;


namespace KPI.InitialForm
{
    public partial class InitialRunForm : Form
    {
        public InitialRunForm()
        {
            InitializeComponent();
           
            CmbSection.DataSource = new BindingSource(Dict.SectionCodeName, null);
            CmbSection.DisplayMember = "Value";
            CmbSection.ValueMember = "Key";
        }

        //private void buttonManyUpdate_Click(object sender, EventArgs e)
        //{
        //    RunUpdate();
        //}

        private void ButtonManyUpdate_Click(object sender, EventArgs e)
        {
            RunUpdate();
        }



        private void InitialRunForm_Load(object sender, EventArgs e)
        {
            cmbYear1.SelectedIndex = 0;
            cmbYear2.SelectedIndex = 0;
            cmbMonth1.SelectedIndex = 0;
            cmbMonth2.SelectedIndex = 0;
            comboBoxStartTable.SelectedIndex = 0;

            int i = 0;
            foreach (KeyValuePair<string, string> item in Dict.SectionCodeName)
            {
                if (item.Key == User.SectionCode)
                {
                    CmbSection.SelectedIndex = i;
                    break;
                }
                i += 1;
            }
        }

        private void BtnInitalQuque_Click(object sender, EventArgs e)
        {
            GenerateLunchBrakTable();
        }

        //private void buttonGen_Click(object sender, EventArgs e)
        //{
        //    GenerateLunchBrakTable();
        //}

        //private void bntList_Click(object sender, EventArgs e)
        //{

        //}


        private void RunUpdate()
        {
            DateTime dtstart = DateTimePickerStart.Value;
            DateTime dtend = DateTimePickerEnd.Value;

            double dayAmount = (dtend - dtstart).TotalDays + 1;
            for (int i = 0; i < dayAmount; i++)
            {
                DateTime daystart = dtstart.AddDays(i);
                string date = daystart.ToString("yyyy-MM-dd HH:mm:ss.000");
                string sectioncode = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Key;
                string sectiondivplant = string.Format("{0}{1}{2}", sectioncode, User.Division, User.Plant);
                SqlClass sql = new SqlClass();
                sql.SSQL_SS("PPAS", "@RegistDateTime", date, "@sectiondivplant", sectiondivplant);

            }
            MessageBox.Show("Data was updated \n Finished");
        }


        private void GenerateLunchBrakTable()
        {
            int yr1 = 0; int yr2 = 0; int mh1 = 0; int mh2 = 0; int tablestart;
            try
            {
                tablestart = comboBoxStartTable.SelectedIndex;
                yr1 = int.Parse(cmbYear1.Text);
                mh1 = cmbMonth1.SelectedIndex + 1;
                yr2 = int.Parse(cmbYear2.Text);
                mh2 = cmbMonth2.SelectedIndex + 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            int month1 = yr1 * 12 + mh1;
            int month2 = yr2 * 12 + mh2;
            int monthDiff = month2 - month1 + 1;


            int buf = 0;
            if (comboBoxStartTable.SelectedIndex == 0)
            {
                buf = 2;
            }
            else if (comboBoxStartTable.SelectedIndex == 1)
            {
                buf = 1;
            }
            var query = new StringBuilder();
            query.Append("insert into [Production].[dbo].[Prod_TimeBreakQueueTable]([RegistYear],[RegistMonth],[SectionCode],[breakQueue],[DivisionId],[PlantId]) VALUES \n");

            int monthstart = mh1;
            int yearstart = yr1;
            for (int month = 0; month < monthDiff; month++)
            {
                if (monthstart == 13)
                {
                    monthstart = 1;
                    yearstart += 1;
                }

                int table = ((month + buf) % 2) + 1;
                query.AppendFormat("('{0}','{1}','{2}',{3},'{4}','{5}')", yearstart.ToString("0000"), monthstart.ToString("00"), User.SectionCode, table, User.Division, User.Plant);
                if (month < monthDiff - 1)
                {
                    query.Append(",\n");
                }
                monthstart += 1;

            }


            SqlClass sql = new SqlClass();
            sql.ReUpdate_TimeBreakQueueTableSQL(query.ToString());


            MessageBox.Show("Write data finished ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BntList_Click(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker3.Value;
            string sectioncode = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Key;

            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.ReUpdate_PPASListSQL(sectioncode, dt);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable workOT = ds.Tables[0];
                DataTable ppastable = ds.Tables[1];
                dataGridView2.AutoGenerateColumns = true;
                dataGridView2.DataSource = workOT;
                dataGridView3.AutoGenerateColumns = true;
                dataGridView3.DataSource = ppastable;
            }
        }
    }
}
