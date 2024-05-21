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
    public partial class CalendarForm : Form
    {
        readonly string[] monthName = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        readonly string[] monthcode = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
        readonly Panel[] PanelList = new Panel[37];
        readonly Label[] LList = new Label[37];
        private string Section = User.SectionCode;
        readonly Panel[] cPanelList = new Panel[37];
        readonly Label[] cLList = new Label[37];
        readonly Dictionary<string, string> MonthToIndex = new Dictionary<string, string>();
        private int _dayofweek = 0;
        private int _dayinmonth = 0;
        private int _Month = 1;
        private int _Year = 2020;
        readonly Color sh0 = Color.FromArgb(255, 85, 85);  // red
        readonly Color sh1 = Color.FromArgb(240, 230, 140);  // yellow
        readonly Color sh2 = Color.White;

        public CalendarForm()
        {
            InitializeComponent();
            ComponentListInitial();
        }

        private void CalendarForm_Load(object sender, EventArgs e)
        {
            CmbYear.SelectedIndexChanged -= CmbYear_SelectedIndexChanged;
            CmbMonth.SelectedIndexChanged -= CmbMonth_SelectedIndexChanged;
            for (int i = 0; i < 12; i++)
            {
                MonthToIndex.Add(monthcode[i], monthName[i]);
            }
            CmbMonth.DataSource = new BindingSource(MonthToIndex, null);
            CmbMonth.DisplayMember = "Value";
            CmbMonth.ValueMember = "Key";

            for (int i = 0; i < 37; i++)
            {
                PanelList[i].Visible = false;
            }

            CmbYear.Text = DateTime.Now.ToString("yyyy");
            CmbMonth.SelectedIndex = DateTime.Now.Month - 1;

            _Year = 2020 + CmbYear.SelectedIndex;

            DateTime dt = new DateTime(_Year, _Month, 1);


            CmbSectionCode.DataSource = new BindingSource(Dict.SectionCodeName, null);
            CmbSectionCode.DisplayMember = "Value";
            CmbSectionCode.ValueMember = "Key";
            int n = 0;
            foreach (KeyValuePair<string, string> item in Dict.SectionCodeName)
            {
                if (item.Key == User.SectionCode)
                {
                    CmbSectionCode.SelectedIndex = n;
                    break;
                }
                n += 1;
            }
            Roles();
            LoadCalendar();
            CmbYear.SelectedIndexChanged += CmbYear_SelectedIndexChanged;
            CmbMonth.SelectedIndexChanged += CmbMonth_SelectedIndexChanged;
        }



        private void CmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        private void CmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Year = 2020 + CmbYear.SelectedIndex;
            LoadCalendar();
        }

        private void CmbSectionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Section = ((KeyValuePair<string, string>)CmbSectionCode.SelectedItem).Key;
        }


        private void LoadCalendar()
        {
            int year = int.Parse(CmbYear.Text);
            int month = int.Parse(((KeyValuePair<string, string>)CmbMonth.SelectedItem).Key);

            string section = ((KeyValuePair<string, string>)CmbSectionCode.SelectedItem).Key;
            string sectionDivPlant = string.Format("{0}{1}{2}", section, User.Division, User.Plant);

            DateTime dt = new DateTime(year, month, 1);
            _dayofweek = (int)dt.DayOfWeek;
            _dayinmonth = DateTime.DaysInMonth(year, month);

            for (int i = 0; i < 37; i++)
            {
                PanelList[i].Visible = false;
                cPanelList[i].Visible = false;
            }
            for (int i = 0 + _dayofweek; i < _dayinmonth + _dayofweek; i++)
            {
                PanelList[i].Visible = true;
                cPanelList[i].Visible = true;
                LList[i].Text = (i - _dayofweek + 1).ToString();
                cLList[i].Text = (i - _dayofweek + 1).ToString();
                PanelList[i].BackColor = Color.White;
                cPanelList[i].BackColor = Color.White;
            }
            string year1 = year.ToString("0000");
            string month1 = month.ToString("00");
            string yearmonth = string.Format("{0}-{1}", year1, month1);

            using (var db = new ProductionEntities11())
            {
                try
                {
                    // ******************  1 table  For Desno  ************************
                    var denso = db.Prod_DensoWorkingDayTable
                        .Where(x => x.registYear == year)
                        .Where(x => x.registMonth == month)
                        .Where(x => x.company =="DNTH")
                        .OrderBy(x => x.registDate).ToList();

                    if (!denso.Any())
                    {
                        labelWorkingday.Text = "Working day : 0";

                    }
                    else
                    {
                        double dayoff = 0;
                        int i = 0;
                        foreach (Prod_DensoWorkingDayTable d in denso)
                        {
                            int nor = d.workHoliday;
                            if (nor == 0)
                            {
                                PanelList[i + _dayofweek].BackColor = Color.FromArgb(255, 85, 85);
                                dayoff += 1;
                            }
                            i++;
                        }
                        labelWorkingday.Text = "Working day : " + (_dayinmonth - dayoff).ToString();
                    }
                    // ****************** 2 table   For Customer  ************************

                    var customer = db.Prod_CustWorkingDayTable
                        .Where(x => x.registYear == year)
                        .Where(x => x.registMonth == month)
                        .Where(x => x.sectionCode == section)
                        .OrderBy(x => x.registDate).ToList();

                    if (customer == null)
                    {
                        label4.Text = "Working day : 0";
                    }
                    else
                    {
                        double dayoff = 0.0;
                        int i = 0;
                        foreach (Prod_CustWorkingDayTable c in customer)
                        {
                            int nor = c.workHoliday;
                            if (nor == 0)
                            {
                                cPanelList[i + _dayofweek].BackColor = sh0; // red

                            }
                            else if (nor == 1)
                            {
                                cPanelList[i + _dayofweek].BackColor = sh1; // yellow
                                dayoff += 1;
                            }
                            else if (nor == 2)
                            {
                                cPanelList[i + _dayofweek].BackColor = sh2; // white
                                dayoff += 2;
                            }
                            i++;
                        }
                        label4.Text = string.Format($"Working day :   {dayoff / 2:0.0}");
                    }
                }
                catch
                {
                    MessageBox.Show("Load data failure, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void ChkL(int i)
        {
            if (PanelList[i].BackColor == Color.FromArgb(255, 85, 85))  //if (PanelClickMemory[i] == false)
            {
                PanelList[i].BackColor = Color.White;
            }
            else if (PanelList[i].BackColor == Color.White)
            {
                PanelList[i].BackColor = Color.FromArgb(255, 85, 85);
            }

        }

        private void ChkLc(int i)
        {
            if (cPanelList[i].BackColor == sh0)  //if (PanelClickMemory[i] == false)
            {

                cPanelList[i].BackColor = sh1;

            }
            else if (cPanelList[i].BackColor == sh1)
            {

                cPanelList[i].BackColor = sh2;

            }
            else if (cPanelList[i].BackColor == sh2)
            {

                cPanelList[i].BackColor = sh0;

            }

        }



        private void BtnSavemasterDenso_Click(object sender, EventArgs e)
        {
            int month = 1 + CmbMonth.SelectedIndex;
            int year = Int32.Parse(CmbYear.Text);
            //string section = ((KeyValuePair<string, string>)CmbSectionCode.SelectedItem).Key;
            DialogResult r = MessageBox.Show("Confrim date setting", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (r == DialogResult.OK)
            {
                _Month = 1 + CmbMonth.SelectedIndex;
                DateTime dt = new DateTime(year, month, 1);
                try
                {


                    using (var db = new ProductionEntities11())
                    {
                        List<Prod_DensoWorkingDayTable> existMonthDenso = new List<Prod_DensoWorkingDayTable>();

                        existMonthDenso = db.Prod_DensoWorkingDayTable
                            .Where(c => c.registYear == year && c.registMonth == month)
                            .Where(a => a.company == "DNTH").OrderBy(a => a.registDate).ToList();
                        if (!existMonthDenso.Any())
                        {

                            List<Prod_DensoWorkingDayTable> newDenso = new List<Prod_DensoWorkingDayTable>();
                            int days = DateTime.DaysInMonth(year, month);
                            for (int i = 0; i < days; i++)
                            {
                                var newTable = new Prod_DensoWorkingDayTable()
                                {
                                    registYear = year,
                                    registMonth = month,
                                    registDate = new DateTime(year, month, 1 + i),
                                    workHoliday = 0,
                                    company = "DNTH",
                                    
                                };
                                newDenso.Add(newTable);
                            }
                            db.Prod_DensoWorkingDayTable.AddRange(newDenso);
                            db.SaveChanges();
                            existMonthDenso = db.Prod_DensoWorkingDayTable
                            .Where(c => c.registYear == year && c.registMonth == month)
                            .Where(a => a.company == "DNTH").OrderBy(a => a.registDate).ToList();
                        }

                        int Dworkingday1 = 0;
                        foreach (Prod_DensoWorkingDayTable d in existMonthDenso)
                        {
                            int i = d.registDate.Day - 1;
                            int normal1 = 1;
                            if (PanelList[i + _dayofweek].BackColor == Color.FromArgb(255, 85, 85))
                            {
                                normal1 = 0;
                            }
                            else
                            {
                                Dworkingday1 += 1;
                            }
                            d.workHoliday = normal1;
                            d.company= "DNTH";
                         
                            db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                        }
                        db.SaveChanges();


                        labelWorkingday.Text = string.Format($"Working day : {Dworkingday1}");
                    }
                }
                catch
                {
                    MessageBox.Show("Save failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }



        private void BtnCustomer_Click(object sender, EventArgs e)
        {
            CustomerCalandarSave();
        }


        private void Roles()
        {
            CmbMonth.Enabled = false;
            CmbYear.Enabled = false;
            BtnCustomer.Enabled = false;
            BtnSavemasterDenso.Enabled = false;
            if (User.Role == Models.Roles.Invalid) // invalid
            {

            }
            else if (User.Role == Models.Roles.General) // General
            {
                CmbMonth.Enabled = true;
                CmbYear.Enabled = true;
            }
            else if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager) // production
            {
                CmbMonth.Enabled = true;
                CmbYear.Enabled = true;
            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {
                CmbMonth.Enabled = true;
                CmbYear.Enabled = true;
                BtnCustomer.Enabled = true;
                BtnSavemasterDenso.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Min) // Admin-mini
            {
                CmbMonth.Enabled = true;
                CmbYear.Enabled = true;
                BtnCustomer.Enabled = true;


            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                CmbMonth.Enabled = true;
                CmbYear.Enabled = true;
                BtnCustomer.Enabled = true;
                BtnSavemasterDenso.Enabled = true;
            }
        }



        #region Initial Component
        private void ComponentListInitial()
        {
            PanelList[0] = p1; PanelList[1] = p2; PanelList[2] = p3; PanelList[3] = p4; PanelList[4] = p5; PanelList[5] = p6; PanelList[6] = p7;
            PanelList[7] = p8; PanelList[8] = p9; PanelList[9] = p10; PanelList[10] = p11; PanelList[11] = p12; PanelList[12] = p13; PanelList[13] = p14;
            PanelList[14] = p15; PanelList[15] = p16; PanelList[16] = p17; PanelList[17] = p18; PanelList[18] = p19; PanelList[19] = p20; PanelList[20] = p21;
            PanelList[21] = p22; PanelList[22] = p23; PanelList[23] = p24; PanelList[24] = p25; PanelList[25] = p26; PanelList[26] = p27; PanelList[27] = p28;
            PanelList[28] = p29; PanelList[29] = p30; PanelList[30] = p31; PanelList[31] = p32; PanelList[32] = p33; PanelList[33] = p34; PanelList[34] = p35;
            PanelList[35] = p36; PanelList[36] = p37;

            LList[0] = l1; LList[1] = l2; LList[2] = l3; LList[3] = l4; LList[4] = l5; LList[5] = l6; LList[6] = l7;
            LList[7] = l8; LList[8] = l9; LList[9] = l10; LList[10] = l11; LList[11] = l12; LList[12] = l13; LList[13] = l14;
            LList[14] = l15; LList[15] = l16; LList[16] = l17; LList[17] = l18; LList[18] = l19; LList[19] = l20; LList[20] = l21;
            LList[21] = l22; LList[22] = l23; LList[23] = l24; LList[24] = l25; LList[25] = l26; LList[26] = l27; LList[27] = l28;
            LList[28] = l29; LList[29] = l30; LList[30] = l31; LList[31] = l32; LList[32] = l33; LList[33] = l34; LList[34] = l35;
            LList[35] = l36; LList[36] = l37;

            cPanelList[0] = cp1; cPanelList[1] = cp2; cPanelList[2] = cp3; cPanelList[3] = cp4; cPanelList[4] = cp5; cPanelList[5] = cp6; cPanelList[6] = cp7;
            cPanelList[7] = cp8; cPanelList[8] = cp9; cPanelList[9] = cp10; cPanelList[10] = cp11; cPanelList[11] = cp12; cPanelList[12] = cp13; cPanelList[13] = cp14;
            cPanelList[14] = cp15; cPanelList[15] = cp16; cPanelList[16] = cp17; cPanelList[17] = cp18; cPanelList[18] = cp19; cPanelList[19] = cp20; cPanelList[20] = cp21;
            cPanelList[21] = cp22; cPanelList[22] = cp23; cPanelList[23] = cp24; cPanelList[24] = cp25; cPanelList[25] = cp26; cPanelList[26] = cp27; cPanelList[27] = cp28;
            cPanelList[28] = cp29; cPanelList[29] = cp30; cPanelList[30] = cp31; cPanelList[31] = cp32; cPanelList[32] = cp33; cPanelList[33] = cp34; cPanelList[34] = cp35;
            cPanelList[35] = cp36; cPanelList[36] = cp37;

            cLList[0] = cl1; cLList[1] = cl2; cLList[2] = cl3; cLList[3] = cl4; cLList[4] = cl5; cLList[5] = cl6; cLList[6] = cl7;
            cLList[7] = cl8; cLList[8] = cl9; cLList[9] = cl10; cLList[10] = cl11; cLList[11] = cl12; cLList[12] = cl13; cLList[13] = cl14;
            cLList[14] = cl15; cLList[15] = cl16; cLList[16] = cl17; cLList[17] = cl18; cLList[18] = cl19; cLList[19] = cl20; cLList[20] = cl21;
            cLList[21] = cl22; cLList[22] = cl23; cLList[23] = cl24; cLList[24] = cl25; cLList[25] = cl26; cLList[26] = cl27; cLList[27] = cl28;
            cLList[28] = cl29; cLList[29] = cl30; cLList[30] = cl31; cLList[31] = cl32; cLList[32] = cl33; cLList[33] = cl34; cLList[34] = cl35;
            cLList[35] = cl36; cLList[36] = cl37;
        }
        #endregion



        private void CustomerCalandarSave()
        {
            int month = 1 + CmbMonth.SelectedIndex;
            int year = Int32.Parse(CmbYear.Text);
            string section = ((KeyValuePair<string, string>)CmbSectionCode.SelectedItem).Key;
            DialogResult r = MessageBox.Show("Confrim date setting", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (r == DialogResult.OK)
            {
                try
                {
                    using (var db = new ProductionEntities11())
                    {
                        List<Prod_CustWorkingDayTable> existMonthCustomer = new List<Prod_CustWorkingDayTable>();

                        existMonthCustomer = db.Prod_CustWorkingDayTable
                            .Where(c => c.registYear == year && c.registMonth == month)
                            .Where(a => a.sectionCode == section).OrderBy(a => a.registDate).ToList();
                        if (!existMonthCustomer.Any())
                        {

                            List<Prod_CustWorkingDayTable> newCustomer = new List<Prod_CustWorkingDayTable>();
                            int days = DateTime.DaysInMonth(year, month);
                            for (int i = 0; i < days; i++)
                            {
                                var newTable = new Prod_CustWorkingDayTable()
                                {
                                    sectionCode = Section,
                                    registYear = _Year,
                                    registMonth = _Month,
                                    registDate = new DateTime(year, month, 1 + i),
                                    workHoliday = 0,

                                };
                                newCustomer.Add(newTable);
                            }
                            db.Prod_CustWorkingDayTable.AddRange(newCustomer);
                            db.SaveChanges();

                            existMonthCustomer = db.Prod_CustWorkingDayTable
                            .Where(c => c.registYear == year && c.registMonth == month)
                             .Where(a => a.sectionCode == section).OrderBy(a => a.registDate).ToList();
                        }



                        double Cworkingday = 0;
                        int numberOfshift = 0;
                        foreach (Prod_CustWorkingDayTable d in existMonthCustomer)
                        {
                            int i = d.registDate.Day - 1;
                            int normal = 0;
                            if (cPanelList[i + _dayofweek].BackColor == sh0)  //holiday
                            {
                                normal = 0;
                            }
                            else if (cPanelList[i + _dayofweek].BackColor == sh1)  // 1shift
                            {
                                normal = 1;
                                Cworkingday += 1;
                                if (numberOfshift == 2) { numberOfshift = 1; }
                            }
                            else if (cPanelList[i + _dayofweek].BackColor == sh2)   // 2shift
                            {
                                normal = 2;
                                Cworkingday += 2;
                                numberOfshift = 2;
                            }

                            d.workHoliday = normal;
                            db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                        }
                        db.SaveChanges();


                        label4.Text = string.Format($"Working day :   {Cworkingday / 2:0.0}");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Save failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //DateTime dt = new DateTime(_Year, month, 1);
                //var sb = new StringBuilder();
                //sb.AppendFormat("delete from [Production].[dbo].[Prod_CustWorkingDayTable] Where [registYear]={0} and  [registMonth] = {1} and SectionCode = '{2}' \n", year, month, Section);
                //sb.Append("insert into [Production].[dbo].[Prod_CustWorkingDayTable] (Sectioncode,registYear,registMonth,registDate,WorkHoliday) Values \n ");

                //double Cworkingday = 0;
                //int numberOfshift = 0;

                //int dayinmonth = DateTime.DaysInMonth(year, month);
                //for (int i = 0; i < dayinmonth; i++)
                //{
                //    DateTime dts = new DateTime(_Year, month, 1 + i);
                //    // For Customer calendar
                //    int normal = 0;
                //    if (cPanelList[i + _dayofweek].BackColor == sh0)  //holiday
                //    {
                //        normal = 0;
                //    }
                //    else if (cPanelList[i + _dayofweek].BackColor == sh1)  // 1shift
                //    {
                //        normal = 1;
                //        Cworkingday += 1;
                //        if (numberOfshift == 2) { numberOfshift = 1; }
                //    }
                //    else if (cPanelList[i + _dayofweek].BackColor == sh2)   // 2shift
                //    {
                //        normal = 2;
                //        Cworkingday += 2;
                //        numberOfshift = 2;
                //    }
                //    sb.AppendFormat("('{0}',{1},{2},'{3}',{4}) \n", Section, _Year, month,
                //        dts.ToString("yyyy-MM-dd"), normal);
                //    if (i < dayinmonth - 1) sb.Append(",");

                //}

                //label4.Text = string.Format($"Working day :   {Cworkingday / 2:0.0}");

                //var a = sb.ToString();
                //SqlClass sql = new SqlClass();
                //bool status = sql.Monthly_SaveMasterCustomerSQL(sb.ToString());
                //if (status)
                //{
                //    MessageBox.Show("Complete", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    MessageBox.Show("Save failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

            }
        }

        #region DENSO  and CUSTOMER

        private void L1_Click(object sender, EventArgs e)
        {
            ChkL(0);
        }

        private void L2_Click(object sender, EventArgs e)
        {
            ChkL(1);
        }

        private void L3_Click(object sender, EventArgs e)
        {
            ChkL(2);
        }

        private void L4_Click(object sender, EventArgs e)
        {
            ChkL(3);
        }

        private void L5_Click(object sender, EventArgs e)
        {
            ChkL(4);
        }

        private void L6_Click(object sender, EventArgs e)
        {
            ChkL(5);
        }

        private void L7_Click(object sender, EventArgs e)
        {
            ChkL(6);
        }

        private void L8_Click(object sender, EventArgs e)
        {
            ChkL(7);
        }

        private void L9_Click(object sender, EventArgs e)
        {
            ChkL(8);
        }

        private void L10_Click(object sender, EventArgs e)
        {
            ChkL(9);
        }

        private void L11_Click(object sender, EventArgs e)
        {
            ChkL(10);
        }

        private void L12_Click(object sender, EventArgs e)
        {
            ChkL(11);
        }

        private void L13_Click(object sender, EventArgs e)
        {
            ChkL(12);
        }

        private void L14_Click(object sender, EventArgs e)
        {
            ChkL(13);
        }

        private void L15_Click(object sender, EventArgs e)
        {
            ChkL(14);
        }

        private void L16_Click(object sender, EventArgs e)
        {
            ChkL(15);
        }

        private void L17_Click(object sender, EventArgs e)
        {
            ChkL(16);
        }

        private void L18_Click(object sender, EventArgs e)
        {
            ChkL(17);
        }

        private void L19_Click(object sender, EventArgs e)
        {
            ChkL(18);
        }

        private void L20_Click(object sender, EventArgs e)
        {
            ChkL(19);
        }

        private void L21_Click(object sender, EventArgs e)
        {
            ChkL(20);
        }

        private void L22_Click(object sender, EventArgs e)
        {
            ChkL(21);
        }

        private void L23_Click(object sender, EventArgs e)
        {
            ChkL(22);
        }

        private void L24_Click(object sender, EventArgs e)
        {
            ChkL(23);
        }

        private void L25_Click(object sender, EventArgs e)
        {
            ChkL(24);
        }

        private void L26_Click(object sender, EventArgs e)
        {
            ChkL(25);
        }

        private void L27_Click(object sender, EventArgs e)
        {
            ChkL(26);
        }

        private void L28_Click(object sender, EventArgs e)
        {
            ChkL(27);
        }

        private void L29_Click(object sender, EventArgs e)
        {
            ChkL(28);
        }

        private void L30_Click(object sender, EventArgs e)
        {
            ChkL(29);
        }

        private void L31_Click(object sender, EventArgs e)
        {
            ChkL(30);
        }

        private void L32_Click(object sender, EventArgs e)
        {
            ChkL(31);
        }

        private void L33_Click(object sender, EventArgs e)
        {
            ChkL(32);
        }

        private void L34_Click(object sender, EventArgs e)
        {
            ChkL(33);
        }

        private void L35_Click(object sender, EventArgs e)
        {
            ChkL(34);
        }

        private void L36_Click(object sender, EventArgs e)
        {
            ChkL(35);
        }

        private void L37_Click(object sender, EventArgs e)
        {
            ChkL(36);
        }




        private void Cl1_Click(object sender, EventArgs e)
        {
            ChkLc(0);
        }

        private void Cl2_Click(object sender, EventArgs e)
        {
            ChkLc(1);
        }

        private void Cl3_Click(object sender, EventArgs e)
        {
            ChkLc(2);
        }

        private void Cl4_Click(object sender, EventArgs e)
        {
            ChkLc(3);
        }

        private void Cl5_Click(object sender, EventArgs e)
        {
            ChkLc(4);
        }

        private void Cl6_Click(object sender, EventArgs e)
        {
            ChkLc(5);
        }

        private void Cl7_Click(object sender, EventArgs e)
        {
            ChkLc(6);
        }

        private void Cl8_Click(object sender, EventArgs e)
        {
            ChkLc(7);
        }

        private void Cl9_Click(object sender, EventArgs e)
        {
            ChkLc(8);
        }

        private void Cl10_Click(object sender, EventArgs e)
        {
            ChkLc(9);
        }

        private void Cl11_Click(object sender, EventArgs e)
        {
            ChkLc(10);
        }

        private void Cl12_Click(object sender, EventArgs e)
        {
            ChkLc(11);
        }

        private void Cl13_Click(object sender, EventArgs e)
        {
            ChkLc(12);
        }

        private void Cl14_Click(object sender, EventArgs e)
        {
            ChkLc(13);
        }

        private void Cl15_Click(object sender, EventArgs e)
        {
            ChkLc(14);
        }

        private void Cl16_Click(object sender, EventArgs e)
        {
            ChkLc(15);
        }

        private void Cl17_Click(object sender, EventArgs e)
        {
            ChkLc(16);
        }

        private void Cl18_Click(object sender, EventArgs e)
        {
            ChkLc(17);
        }

        private void Cl19_Click(object sender, EventArgs e)
        {
            ChkLc(18);
        }

        private void Cl20_Click(object sender, EventArgs e)
        {
            ChkLc(19);
        }

        private void Cl21_Click(object sender, EventArgs e)
        {
            ChkLc(20);
        }

        private void Cl22_Click(object sender, EventArgs e)
        {
            ChkLc(21);
        }

        private void Cl23_Click(object sender, EventArgs e)
        {
            ChkLc(22);
        }

        private void Cl24_Click(object sender, EventArgs e)
        {
            ChkLc(23);
        }

        private void Cl25_Click(object sender, EventArgs e)
        {
            ChkLc(24);
        }

        private void Cl26_Click(object sender, EventArgs e)
        {
            ChkLc(25);
        }

        private void Cl27_Click(object sender, EventArgs e)
        {
            ChkLc(26);
        }

        private void Cl28_Click(object sender, EventArgs e)
        {
            ChkLc(27);
        }

        private void Cl29_Click(object sender, EventArgs e)
        {
            ChkLc(28);
        }

        private void Cl30_Click(object sender, EventArgs e)
        {
            ChkLc(29);
        }

        private void Cl31_Click(object sender, EventArgs e)
        {
            ChkLc(30);
        }

        private void Cl32_Click(object sender, EventArgs e)
        {
            ChkLc(31);
        }

        private void Cl33_Click(object sender, EventArgs e)
        {
            ChkLc(32);
        }

        private void Cl34_Click(object sender, EventArgs e)
        {
            ChkLc(33);
        }

        private void Cl35_Click(object sender, EventArgs e)
        {
            ChkLc(34);
        }

        private void Cl36_Click(object sender, EventArgs e)
        {
            ChkLc(35);
        }

        private void Cl37_Click(object sender, EventArgs e)
        {
            ChkLc(36);
        }



        #endregion
    }
}
