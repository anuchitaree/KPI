using KPI.Class;
using KPI.InitialForm;
using KPI.Models;
using KPI.OptionalForm;
using KPI.Parameter;
using KPI.ProdForm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KPI
{
    public partial class MainForm : Form
    {
        private Thread t1;
        private bool run = false;
        readonly CancellationTokenSource[] cts = new CancellationTokenSource[3];
        readonly bool Splash = false;
        private Form activeForm = null;

        public MainForm()
        {
            this.WindowState = FormWindowState.Normal;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PrepareFileIo();
            pSub2Child.Visible = false;
            pSub3Child.Visible = false;
            pSub8Child.Visible = false;
            panelLEFT.Visible = false;
            tableLayoutPanelTOP.Visible = false;
            PreparaResource();
            SplashStart();
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        private void UserLogin_LoginSuccess(object sender, EventArgs e)
        {
            BackGroundInitialData();
            Roles();
            LoopOAAlertStart();
            OptionPanel();
        }

        private void UserLogin_ExitProductivity(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            LogIn();
        }
        private void LogIn()
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "MainForm")
                    Application.OpenForms[i].Close();
            }
            ButtonBackColor();

            LoopOAAlertStop();
            User.SectionCode = "0000-00";

            panelLEFT.Visible = false;
            lbName.Text = lbShift.Text = "";
           

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(UserLoginForm))
                {
                    form.Activate();
                    return;

                }
            }

            User.LogOutTime = DateTime.Now;
            SqlClass sql = new SqlClass();
            sql.SaveTrackLogInSQL();

            UserLoginForm frm = new UserLoginForm();
            frm.LoginSuccess += new EventHandler(UserLogin_LoginSuccess);  // control from Userlogin

            if (Memory.UserRegistrationForm == false)
            {
                frm.Show();
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            LogOut();
        }

        private void LogOut()
        {
            User.LogOutTime = DateTime.Now;
            SqlClass sql = new SqlClass();
            sql.SaveTrackLogInSQL();

            panelLEFT.Visible = false;
            lbName.Visible = lbShift.Visible = false;
            User.SectionCode = "0000-00";
            LoopOAAlertStop();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainFormCloseing();
        }

        private void MainFormCloseing()
        {
            if (run == true)
            {
                run = false;
                t1.Abort();
                Thread.Sleep(1000);
            }

            if (cts[0] == null)
            {
                return;
            }
            cts[0].Cancel();
            Thread.Sleep(250);
            cts[0].Dispose();
            cts[0] = null;

            if (cts[1] == null)
            {
                return;
            }
            cts[1].Cancel();
            Thread.Sleep(250);
            cts[1].Dispose();
            cts[1] = null;

            if (cts[2] == null)
            {
                return;
            }
            cts[2].Cancel();
            Thread.Sleep(250);
            cts[2].Dispose();
            cts[2] = null;

            if (activeForm != null)
                activeForm.Close();

            User.LogOutTime = DateTime.Now;
            SqlClass sql = new SqlClass();
            sql.SaveTrackLogInSQL();

        }

        #region NAVIGRATION


        private void Btn100_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pSub1Child); // PRODUCTION
        }

        private void Btn200_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pSub2Child); // INITIAL SETUP
        }

        private void Btn300_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pSub3Child); //SERVICE
        }

        private void Btn800_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pSub8Child); // SYSTEM SETUP
        }



        #region PRODUCTION TAB

        private void Btn101_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ManPowerRegisterForm());
            ButtonBackColor();
            ButtonActive(Btn101);
        }

        private void Btn102_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ExculsionInputForm());
            ButtonBackColor();
            ButtonActive(Btn102);
        }

        private void Btn103_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductionProgressForm());
            ButtonBackColor();
            ButtonActive(Btn103);
        }

        private void Btn104_Click(object sender, EventArgs e)
        {
            OpenChildForm(new LossInputForm());
            ButtonBackColor();
            ButtonActive(Btn104);
        }

        private void Btn105_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ExculsionLossMonitorForm());
            ButtonBackColor();
            ButtonActive(Btn105);
        }

        private void Btn106_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PPASRealTimeForm());
            ButtonBackColor();
            ButtonActive(Btn106);
        }

        private void Btn107_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductionApprovedForm());
            ButtonBackColor();
            ButtonActive(Btn107);
        }

        private void Btn108_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MachineDownTimeForm());
            ButtonBackColor();
            ButtonActive(Btn108);
        }

        private void Btn109_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BankForm());
            ButtonBackColor();
            ButtonActive(Btn109);
        }

        private void Btn110_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TracebilityNewForm());
            ButtonBackColor();
            ButtonActive(Btn110);
        }

        private void Btn111_Click(object sender, EventArgs e)
        {
            OpenChildForm(new OEEnewForm());   //MBD  Tendency
            ButtonBackColor();
            ButtonActive(Btn111);
        }

        private void Btn112_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new OEEForm2());
            ButtonBackColor();
            ButtonActive(Btn112);
        }

        private void Btn113_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new OEEnewForm());
            ButtonBackColor();
            ButtonActive(Btn113);
        }

        private void Btn114_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BankForm());
            ButtonBackColor();
            ButtonActive(Btn114);
        }

        private void Btn115_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HVACForm());
            ButtonBackColor();
            ButtonActive(Btn115);
        }

        private void Btn116_Click(object sender, EventArgs e)
        {
            OpenChildForm(new WGRForm());
            ButtonBackColor();
            ButtonActive(Btn116);
        }


        #endregion

        #region INITIAL SETUP

        private void Btn201_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CalendarForm());
            ButtonBackColor();
            ButtonActive(Btn201);
        }

        private void Btn202_Click(object sender, EventArgs e)
        {
            OpenChildForm(new STDNetTimeForm());
            ButtonBackColor();
            ButtonActive(Btn202);
        }

        private void Btn203_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductionPlanForm());
            ButtonBackColor();
            ButtonActive(Btn203);
        }

        private void Btn204_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MachineFaultCodeForm());
            ButtonBackColor();
            ButtonActive(Btn204);
        }

        private void Btn205_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ExclusionLossEditForm());
            ButtonBackColor();
            ButtonActive(Btn205);
        }

        private void Btn206_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BreakTimeForm());
            ButtonBackColor();
            ButtonActive(Btn206);
        }

        private void Btn207_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MachineTimeForm());
            ButtonBackColor();
            ButtonActive(Btn207);
        }
        private void Btn208_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BankForm());
            ButtonBackColor();
            ButtonActive(Btn208);
        }

        private void Btn209_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AdditionalDataForm());
            ButtonBackColor();
            ButtonActive(Btn209);
        }

        private void Btn210_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddRemoveEmpForm());
            ButtonBackColor();
            ButtonActive(Btn210);
        }


        #endregion

        #region SERVICE

        private void Btn301_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductionCheckForm());
            ButtonBackColor();
            ButtonActive(Btn301);
        }

        private void Btn302_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DatabaseCheckForm());
            ButtonBackColor();
            ButtonActive(btn302);
        }

        private void Btn303_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductionActualForm());
            ButtonBackColor();
            ButtonActive(Btn303);
        }

        private void Btn304_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BankForm());
            ButtonBackColor();
            ButtonActive(Btn304);
        }

        private void Btn305_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BankForm());
            ButtonBackColor();
            ButtonActive(Btn305);
        }

        private void Btn306_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BankForm());
            ButtonBackColor();
            ButtonActive(Btn306);
        }

        private void Btn307_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BankForm());
            ButtonBackColor();
            ButtonActive(Btn307);
        }




        #endregion

        #region SYSTEM SETUP

        private void Btn801_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AdditionalDataForm());
            ButtonBackColor();
            ButtonActive(Btn801);
        }

        private void Btn802_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddRemoveEmpForm());
            ButtonBackColor();
            ButtonActive(Btn802);
        }

        private void Btn803_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SectionCodeAssignForm());
            ButtonBackColor();
            ButtonActive(Btn803);
        }

        private void Btn804_Click(object sender, EventArgs e)
        {
            //   OpenChildForm(new SectionCodeAssignForm());
            ButtonBackColor();
            ButtonActive(Btn804);
        }

        private void Btn805_Click(object sender, EventArgs e)
        {
            OpenChildForm(new InitialRunForm());
            ButtonBackColor();
            ButtonActive(Btn805);
        }

        private void Btn806_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BankForm());
            ButtonBackColor();
            ButtonActive(Btn806);
        }

        private void Btn807_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BankForm());
            ButtonBackColor();
            ButtonActive(Btn807);
        }



        #endregion


        #endregion

        //////    LOOP OPRATION   //////////////////////////////////////////////


        #region Tab start Loop

        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void OpenChildForm(Form childForm)
        {
            //Close child form
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "MainForm")
                    Application.OpenForms[i].Close();
            }



            try
            {
                if (activeForm != null)
                {
                    panelChild.Controls.Remove(childForm);
                    activeForm.Close();
                }

                activeForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                panelChild.Controls.Add(childForm);
                panelChild.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error message : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonActive(Button button)
        {
            button.BackColor = Color.FromArgb(18, 116, 239);
        }

        private void ButtonBackColor()
        {
            // PRODUCTION
            Color colorProduction = Color.FromArgb(70, 71, 117);
            Btn101.BackColor = colorProduction;
            Btn102.BackColor = colorProduction;
            Btn103.BackColor = colorProduction;
            Btn104.BackColor = colorProduction;
            Btn105.BackColor = colorProduction;
            Btn106.BackColor = colorProduction;
            Btn107.BackColor = colorProduction;
            Btn108.BackColor = colorProduction;
            Btn109.BackColor = colorProduction;
            Btn110.BackColor = colorProduction;
            Btn111.BackColor = colorProduction;
            Btn112.BackColor = colorProduction;
            Btn114.BackColor = colorProduction;
            Btn115.BackColor = colorProduction;
            Btn116.BackColor = colorProduction;
            // INITIAL SETUP
            Color colorInitial = Color.FromArgb(1, 24, 1);
            Btn201.BackColor = colorInitial;
            Btn202.BackColor = colorInitial;
            Btn203.BackColor = colorInitial;
            Btn204.BackColor = colorInitial;
            Btn205.BackColor = colorInitial;
            Btn206.BackColor = colorInitial;
            Btn207.BackColor = colorInitial;
            Btn208.BackColor = colorInitial;
            Btn209.BackColor = colorInitial;
            Btn210.BackColor = colorInitial;
            //SERVICE
            Color colorService = Color.FromArgb(48, 12, 56);
            Btn301.BackColor = colorService;
            btn302.BackColor = colorService;
            Btn303.BackColor = colorService;
            Btn304.BackColor = colorService;
            Btn305.BackColor = colorService;
            Btn306.BackColor = colorService;
            Btn307.BackColor = colorService;
            //SYSTEM SETUP
            Color colorSetup = Color.FromArgb(170, 55, 68);
            Btn801.BackColor = colorSetup;
            Btn802.BackColor = colorSetup;
            Btn803.BackColor = colorSetup;
            Btn804.BackColor = colorSetup;
            Btn805.BackColor = colorSetup;
            Btn806.BackColor = colorSetup;
            Btn807.BackColor = colorSetup;
        }

        private void HideSubMenu()
        {
            if (pSub1Child.Visible == true)
                pSub1Child.Visible = false;
            if (pSub2Child.Visible == true)
                pSub2Child.Visible = false;
            if (pSub3Child.Visible == true)
                pSub3Child.Visible = false;
            if (pSub8Child.Visible == true)
                pSub8Child.Visible = false;
        }
        #endregion

        private void PreparaResource()
        {
            toolStripStatusLabel1.Text = string.Format("Revision {0}", Software.Revision);
            string revision = string.Empty;
            SqlClass sql = new SqlClass();
            if (sql.ReadRevisionSQL() == false)
            {
                MessageBox.Show(new Form() { TopMost = true },
                    "กรุณาตรวจสอบช่องทางการ ตืดต่อ  C:\\KPi\\Login\\Connnection.dat \n เปิดไฟล์ด้วย notepad \n  เลือก \"4G\" ต่อด้วย internet หรือ \"Local\" ต่อด้วย internal network ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            DataTable dt1 = sql.Datatable;
            if (dt1.Rows.Count > 0)
            {
                revision = Convert.ToString(dt1.Rows[0].ItemArray[0]);
                string info = "หากพบปัญหาจากการใช้งาน หรือต้องการเพิ่มฟังก์ชั้น หรือปรับปรุงอื่นๆ  ติดต่อ ได้ที่ PED Coll&Lab IoT Development section Tel 5195 ";
                this.Text = string.Format("KPI:Productivity vesrion {0} by MPC PED {1}", revision, info);
            }
            string[] serverRev = revision.Split('.');
            string[] LocalRev = Software.Revision.Split('.');

            if (serverRev[0] == LocalRev[0] && serverRev[1] == LocalRev[1])
            {
                lbName.Text = lbShift.Text = "";
                LoopSendErrorToServerStart();

                if (true)
                {

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(UserLoginForm))
                        {
                            form.Activate();
                            return;
                        }
                    }
                    UserLoginForm frm = new UserLoginForm();
                    frm.LoginSuccess += new EventHandler(UserLogin_LoginSuccess);
                    frm.ExitProductivity += new EventHandler(UserLogin_ExitProductivity);
                    frm.Show();
                }
                RunThread();
                run = true;
            }
            else
            {
                string msg = string.Format("Revision current is {0} \n Now revision is {1} \n Please contact PED Collraborate R&D Lap.", Software.Revision, revision);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        private void BackGroundInitialData()
        {
            panelChild.Show();
            DateTime date = DateTime.Now;
            using (var context =new  ProductionEntities11())
            {
                var divPlant = context.Emp_SectionTable.FirstOrDefault(x => x.sectionCode == User.SectionCode);
                User.Division = divPlant.divisionID;
                User.Plant = divPlant.plantID;

                var sectionCodeList = context.Emp_SectionTable
                    .Where(d => d.divisionID == User.Division)
                    .Where(p => p.plantID == User.Plant).ToList();
                Dict.SectionCodeName.Clear();
                ListValue.SectionCodeName.Clear();
                foreach (var item in sectionCodeList)
                {
                    Dict.SectionCodeName.Add(item.sectionCode, item.sectionCode + " : " + item.sectionName); //dictionary
                    ListValue.SectionCodeName.Add(item.sectionCode + " : " + item.sectionName);
                }


                int id = Convert.ToInt32(User.ID);
                var user = context.Emp_ManPowersTable.FirstOrDefault(u => u.userID == id);
                User.Name = user.fullName;
                User.Role = (Roles)Convert.ToInt32(user.roles);
                User.Email = user.email;
                User.Phone = user.phone;

                var  mpFunction = context.Emp_MPFunctionTable.OrderBy(i=>i.functionID).ToList();
                Dict.EmpFunction.Clear();
                ListValue.EmpFunction.Clear();
                foreach (var item in mpFunction)
                {
                    Dict.EmpFunction.Add(item.functionID,item.functionName);
                    ListValue.EmpFunction.Add(item.functionName);
                }

            //    Dict.Workingday.Clear();
                Obj.DensoWorking  = context.Prod_DensoWorkingDayTable
                    .Where(y => y.registYear == date.Year)
                    .Select(x => new ProdDensoWorkingDay 
                    {
                        Registdate=x.registDate,
                        WorkHoliday=x.workHoliday,
                    }).ToList();
                //foreach (var item in densoWorking)
                //{
                //    Dict.Workingday.Add(item.Registdate, item.WorkHoliday);
                //}


                var info = context.Software_Info.OrderByDescending(r => r.registDate).FirstOrDefault();
                toolStripStatusLabel2.Text = info.info;
            }






            //SqlClass sql = new SqlClass();
            //bool sqlstatus = sql.SSQL_SS("InitialData2", "@RegistDateTime", DateTime.Now.ToString("yyyy-MM-dd"), "@EmpID", User.ID);
            //if (sqlstatus)
            //{
            //    DataSet ds = sql.Dataset;
            //    DataTable dt1 = ds.Tables[0];
            //    DataTable fun = ds.Tables[1];
            //    DataTable sect = ds.Tables[2];
            //    DataTable workday = ds.Tables[3];
            //    if (dt1.Rows.Count > 0)
            //    {
                   
            //        //User.Name = dt1.Rows[0].ItemArray[1].ToString();
            //        //User.Role = (Roles)Convert.ToInt32(dt1.Rows[0].ItemArray[5]);
            //        //User.Email = dt1.Rows[0].ItemArray[2].ToString();
            //        //User.Phone = dt1.Rows[0].ItemArray[3].ToString();
                   
            //        //if (User.ID == "" || User.Division == "" || User.Plant == "")
            //        //{
            //        //    MessageBox.Show("Detail of User is not enough", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        //}
            //    }

            //    //Dict.EmpFunction.Clear();
            //    //ListValue.EmpFunction.Clear();
            //    //if (fun.Rows.Count > 0)
            //    //{
            //    //    for (int i = 0; i < fun.Rows.Count; i++)
            //    //    {
            //    //        string code = fun.Rows[i].ItemArray[0].ToString();
            //    //        string name = fun.Rows[i].ItemArray[1].ToString();
            //    //        Dict.EmpFunction.Add(code, name);
            //    //        ListValue.EmpFunction.Add(name);
            //    //    }
            //    //}


            //    Dict.SectionCodeName.Clear();
            //    ListValue.SectionCodeName.Clear();
            //    //if (sect.Rows.Count > 0)
            //    //{
            //    //    for (int i = 0; i < sect.Rows.Count; i++)
            //    //    {
            //    //        string code = sect.Rows[i].ItemArray[0].ToString();
            //    //        string name = sect.Rows[i].ItemArray[1].ToString();
            //    //        Dict.SectionCodeName.Add(code, code + " : " + name); //dictionary
            //    //        ListValue.SectionCodeName.Add(code + " : " + name);

            //    //    }
            //    //}

            //    //Dict.Workingday.Clear();
            //    //if (workday.Rows.Count > 0)
            //    //{
            //    //    for (int i = 0; i < workday.Rows.Count; i++)
            //    //    {
            //    //        DateTime daytime = Convert.ToDateTime(workday.Rows[i].ItemArray[0]);
            //    //        string day = daytime.ToString("yyyy-MM-dd");
            //    //        int worktype = Convert.ToInt32(workday.Rows[i].ItemArray[1]);

            //    //        Dict.Workingday.Add(day, worktype);

            //    //    }
            //    //}


            //}

            panelLEFT.Visible = true;
            tableLayoutPanelTOP.Visible = true;
            lbName.Text = String.Format("{2} ,{3} \n\r Welcome K.{4} {0}, Section: {1} ", User.Name, User.SectionName, User.Division, User.Plant, User.ID);
            if (User.DayNight == "D")
            {
                lbShift.Text = String.Format($"Shift: {User.Shift} . Day");
                lbShift.ForeColor = Color.FromArgb(173, 208, 249);
            }
            else
            {
                lbShift.Text = String.Format($"Shift: {User.Shift} . Night");
                lbShift.ForeColor = Color.FromArgb(237, 139, 16);
            }
            Dict.EmpIDName.Clear();
            SqlClass sql2 = new SqlClass();
            sql2.LoadEmployeeAllSQL();

        }

        private void PrepareFileIo()
        {
            string root = @"C:\KPi";
            string subdir1 = @"C:\KPi\Log";
            string subdir2 = @"C:\KPi\Login";
            string subdir3 = @"C:\KPi\Source";
            string subdir4 = @"C:\KPi\Error";
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            if (!Directory.Exists(subdir1))
            {
                Directory.CreateDirectory(subdir1);
            }
            if (!Directory.Exists(subdir2))
            {
                Directory.CreateDirectory(subdir2);
            }
            if (!Directory.Exists(subdir3))
            {
                Directory.CreateDirectory(subdir3);
            }
            if (!Directory.Exists(subdir4))
            {
                Directory.CreateDirectory(subdir4);
            }

            string[] filearray = Directory.GetFiles(@"C:\KPI\Log", "*.log");
            try
            {
                foreach (var file in filearray)
                {
                    File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form() { TopMost = true }, "Error code = 1x0010 , Message : " + ex.ToString() + " (LOOP FileIoClass DeleteLogFile)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string filename = @"C:\KPi\Login\Connection.dat";
            bool isFileExists = File.Exists(filename);
            string destination; // = string.Empty;
            if (isFileExists == false)
            {
                using (StreamWriter sw = File.CreateText(filename))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("4G,");
                    sb.Append("Local");
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
            }
            string source = File.ReadAllText(filename); // File.CreateText(filename))
            string[] server = source.Split(',');
            destination = server[0];
            Connection.DB(destination);
        

        }

        private void Roles()
        {
            Btn802.Enabled = false;
            Btn803.Enabled = false;
            Btn804.Enabled = false;
            Btn805.Enabled = false;

            //int z = (int)Models.Roles.Admin_Full;

            switch (User.Role)
            {
                case Models.Roles.General:

                    break;
               
               
                case Models.Roles.FacEng:
                    Btn807.Enabled = true;
                    break;
                case Models.Roles.Admin_Min:
                    Btn807.Enabled = true;
                    Btn802.Enabled = true;
                    Btn803.Enabled = true;
                    Btn804.Enabled = true;
                    break;
                case Models.Roles.Admin_Full:
                    Btn807.Enabled = true;
                    Btn802.Enabled = true;
                    Btn803.Enabled = true;
                    Btn804.Enabled = true;
                    Btn805.Enabled = true;
                    break;
            }

        }

        private void OptionPanel()
        {
            Btn114.Visible = false;
            Btn115.Visible = false;
            Btn116.Visible = false;
            switch (User.Plant)
            {
                case "BPK":
                    Btn114.Visible = true;
                    break;
                case "SRG":
                    Btn115.Visible = true;
                    break;
                case "WGR":
                    Btn116.Visible = true;
                    break;
            }
        }

        #region Run Thread 1  and  ThreadPool: LoopSendErrorToServerStart , LoopOAAlertStart
        //=======================================// 
        #region THREAD 0 : DISPLAY DATE TIME

        private void RunThread()
        {
            if (t1 == null)
            {
                t1 = new Thread(new ThreadStart(DateTimeStatus));
            }
            t1.Start();

        }

        private void DateTimeStatus()
        {
            Thread.Sleep(1000);
            while (run)
            {

                Thread.Sleep(1000);
                labelTime.Invoke(new Action(() =>
                {
                    labelTime.Text = DateTime.Now.ToString("dd-MM-yyyy \n HH:mm:ss");
                }));
                string dn = OABorad.FindDayOrNight(DateTime.Now);
                if (User.DayNight != dn)
                {
                    User.DayNight = dn;
                    lbShift.Invoke(new Action(() =>
                    {
                        lbShift.Text = dn == "D" ? $"Shift: {User.Shift} . Day" : $"Shift: {User.Shift} . Night";
                        lbShift.ForeColor = dn == "D" ? Color.Aqua : Color.FromArgb(255, 128, 128);
                    }));
                }

            }
        }


        #endregion
        //=======================================// 
        #region THREAD POOL  1 : SAVE ERROR TO SQL SERVER

        private void LoopSendErrorToServerStart()
        {
            if (cts[0] != null)
            {
                return;
            }
            cts[0] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolSendErrorCode), cts[0].Token);
        }

        private void ThreadPoolSendErrorCode(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            Thread.Sleep(5000);
            while (!token.IsCancellationRequested)
            {
                SqlClass sql = new SqlClass();
                sql.SaveErrorLogFileXlsxSQL();
                Thread.Sleep(180000);
            }
        }

        #endregion
        //=======================================// 
        #region THREAD POOL 2 : OA ALERT

        private void LoopOAAlertStart()
        {
            if (cts[1] != null)
            {
                return;
            }
            cts[1] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolOAAlert), cts[1].Token);
        }

        private void LoopOAAlertStop()
        {
            if (cts[1] == null)
            {
                return;
            }
            cts[1].Cancel();
            Thread.Sleep(250);
            cts[1].Dispose();
            cts[1] = null;
        }

        private void ThreadPoolOAAlert(object obj)
        {
            Thread.Sleep(1000);
            while (run)
            {
                OAAlert();
                Thread.Sleep(60000);

            }
        }

        private void InvokeUI(Action a)
        {
            this.BeginInvoke(new MethodInvoker(a));
        }

        private void OAAlert()
        {
            InvokeUI(() =>
            {
                int[] _Timeline = new int[20];
                string percentOA = string.Empty;
                string monitor = string.Empty;
                string[] t = new string[20];
                string registdate = OABorad.FindRegistDateFromCurrentTime(DateTime.Now).ToString("yyyy-MM-dd");
                t[0] = "08:30";
                t[1] = "09:50";
                t[2] = "10:30";
                t[3] = "11:30";
                t[4] = "13:30";
                t[5] = "14:30";
                t[6] = "15:30";
                t[7] = "16:25";
                t[8] = "17:50";
                t[9] = "19:35";
                t[10] = "20:30";
                t[11] = "21:40";
                t[12] = "22:30";
                t[13] = "23:30";
                t[14] = "01:30";
                t[15] = "05:50";
                t[16] = "03:30";
                t[17] = "04:50";
                t[18] = "05:50";
                t[19] = "07:15";


                for (int i = 0; i < 20; i++)
                {
                    _Timeline[i] = TimeConvert(t[i]);
                }
                // 1) Check time is in gap
                int timenow = TimeConvert(DateTime.Now.ToString("HH:mm"));
                int hourNo = 0;
                for (int i = 0; i < 20; i++)
                {
                    if (_Timeline[i] < timenow && timenow < _Timeline[i] + 40)
                    {
                        hourNo = i + 1;
                    }
                }

                if (hourNo > 0)  // time is in gap
                {
                    // string filename = string.Format("C:\\KPi\\log\\{0}{1}.log", User.SectionCode, registdate);
                    string filename = string.Format("{0}{1}{2}.log", Paths.OAAlert, User.SectionCode, registdate);
                    bool isFileExists = File.Exists(filename);
                    if (isFileExists == false)
                    {
                        using (StreamWriter sw = File.CreateText(filename))
                        {
                            sw.Close();
                        }

                    }
                    string[] lines = File.ReadAllLines(filename);
                    List<string> lineList = lines.ToList();
                    int lineCount = lines.Length;
                    string hr = string.Empty;
                    if (lines.Length > 0)
                    {
                        string[] info = lines[lineCount - 1].Split(',');
                        hr = info[0];
                    }

                    if (hr != hourNo.ToString())
                    {
                        (string, string) result = Popup(hourNo.ToString());
                        lineList.Add(string.Format("{0},{1},{2}", hourNo, result.Item2, result.Item1));

                        using (StreamWriter sw = new StreamWriter(filename))
                        {
                            foreach (var str in lineList)
                            {
                                sw.WriteLine(str);
                            }
                            sw.Close();

                        }

                    }

                }
            });

        }

        private (string, string) Popup(string hourNo)
        {
            var data = (pop: "OK", oa: string.Empty);

            string registdate = OABorad.FindRegistDateFromCurrentTime(DateTime.Now).ToString("yyyy-MM-dd");
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.OAPopUpSQL(registdate, hourNo);
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                if (dt1.Rows.Count > 0)
                {
                    string red = dt1.Rows[0].ItemArray[2] != null ? Convert.ToString(dt1.Rows[0].ItemArray[3]) : "";
                    string workt = dt1.Rows[0].ItemArray[2] != null ? Convert.ToString(dt1.Rows[0].ItemArray[4]) : "";

                    string percentOA = dt1.Rows[0].ItemArray[2] != null ? Convert.ToString(dt1.Rows[0].ItemArray[2]) : "";
                    string monitor = dt1.Rows[0].ItemArray[1] != null ? Convert.ToString(dt1.Rows[0].ItemArray[1]) : "";
                    data.oa = percentOA;
                    if (red == "A" && workt == "W")
                    {

                        OAalertForm frm = new OAalertForm(User.SectionName, monitor, percentOA);
                        frm.Show();
                        data.pop = "NG";
                    }

                }
            }
            return data;

        }

        public int TimeConvert(string s)
        {
            string[] tim = s.Split(':');
            int timeline = Convert.ToInt32(tim[0]) * 60 + Convert.ToInt32(tim[1]);
            return timeline;
        }

        #endregion
        //=======================================// 
        #region THREAD POOL  2 : SHOW SPLASH SCREEN

        private void SplashStart()
        {
            if (cts[2] != null)
            {
                return;
            }
            cts[2] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolSplash), cts[2].Token);
        }

        //private void SplashStop()
        //{
        //    if (cts[2] == null)
        //    {
        //        return;
        //    }
        //    cts[2].Cancel();
        //    Thread.Sleep(250);
        //    cts[2].Dispose();
        //    cts[2] = null;
        //}

        private void ThreadPoolSplash(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            bool open = false;
            while (!token.IsCancellationRequested)
            {
                if (Splash == true && open == false)
                {
                    //LoadiingForm frm = new LoadiingForm();
                    //open = true;
                    //frm.Show();
                }
                else if (Splash == false)
                {
                    open = false;
                }
            }
        }








        #endregion
        //=======================================// 

        #endregion
    }
}
