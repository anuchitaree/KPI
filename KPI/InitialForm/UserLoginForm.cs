
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
    public partial class UserLoginForm : Form
    {

        public event EventHandler LoginSuccess;
        public event EventHandler ExitProductivity;

        public UserLoginForm()
        {
            InitializeComponent();
        }

        

        private void TxtUserID_Click(object sender, EventArgs e)
        {
            TxtUserID.Clear();
            panel1.BackColor = Color.FromArgb(78, 184, 206);
            TxtUserID.ForeColor = Color.FromArgb(78, 184, 206);

            panel2.BackColor = Color.WhiteSmoke;
            TxtPassCode.ForeColor = Color.WhiteSmoke;
            Dict.UserSection.Clear();
            CmbSection.DataSource = new BindingSource(Dict.UserSection, null);
           
        }

        private void TxtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                TxtPassCode.Focus();
                SubtxtPassCode();
                bool registed = LoadSectionCode(TxtUserID.Text);
                if (registed == true)
                {
                    ReadDefault(TxtUserID.Text);
                }

            }
        }

        private void TxtPassCode_Click(object sender, EventArgs e)
        {
            SubtxtPassCode();
        }

        private void TxtPassCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Login();
            }
        }





        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            UserRegistrationForm frmReg = new UserRegistrationForm();
            frmReg.LoginSuccess += new EventHandler(UserLogin_LoginSuccess);  // control from Userlogin
            frmReg.ExitProductivity += new EventHandler(UserLogin_ExitProductivity);

            panel3.Visible = false;
            frmReg.Show();
        }

       

        private void BtnCheckAuthoruty_Click(object sender, EventArgs e)
        {
            TxtPassCode.Focus();
            SubtxtPassCode();
            bool registed = LoadSectionCode(TxtUserID.Text);
            if (registed == true)
            {
                ReadDefault(TxtUserID.Text);
            }
        }





        private void UserLogin_LoginSuccess(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void UserLogin_ExitProductivity(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }



        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.ExitProductivity?.Invoke(this, EventArgs.Empty);

            this.Close();
        }



        private void UserLogin_Shown(object sender, EventArgs e)
        {

            Initial_loginLoader();
        }

       

        private void SubtxtPassCode()
        {
            TxtPassCode.Clear();
            TxtPassCode.PasswordChar = 'x';
            panel2.BackColor = Color.FromArgb(78, 184, 206);
            TxtPassCode.ForeColor = Color.FromArgb(78, 184, 206);


            panel1.BackColor = Color.WhiteSmoke;
            TxtUserID.ForeColor = Color.WhiteSmoke;
        }


        private void Initial_loginLoader()
        {
            this.TopMost = true;

            if (User.Shift== "A" || User.Shift == "") { comboBox2.SelectedIndex = 0; }
            else if (User.Shift == "B") { comboBox2.SelectedIndex = 1; }
            else if (User.Shift == "C") { comboBox2.SelectedIndex = 2; }

            if (!String.IsNullOrEmpty(TxtUserID.Text))
            {
                TxtUserID.SelectionStart = 0;
                TxtUserID.SelectionLength = TxtUserID.Text.Length;
            }

            TxtUserID.Select();
            this.ActiveControl = TxtUserID;
            TxtUserID.Focus();


        }


        private void Login()
        {
            int IdLen = TxtUserID.TextLength;
            int passcodeLen = TxtPassCode.TextLength;

            bool id = int.TryParse(TxtUserID.Text, out int userID);
            bool passcode = int.TryParse(TxtPassCode.Text, out int passcode1);

            SqlClass sql = new SqlClass();
            string msg = sql.UserLoginSSQL("UserRegistationLogin", "@pUserID", userID, "@pPassword", TxtPassCode.Text);
            if (msg == "User successfully")
            {
                int c = CmbSection.SelectedIndex;
                if (c >= 0)
                {
                    string Sectionkey = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Key;
                    string Sectionvalue = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Value;
                   

                    Loginrecord(TxtUserID.Text, "W");

                    User.ID = TxtUserID.Text;
                    User.SectionCode = Sectionkey;
                    User.SectionName = Sectionvalue;
                    User.LogInTime = DateTime.Now;

                    this.LoginSuccess?.Invoke(this, EventArgs.Empty);

                    this.Close();
                }
                else
                {
                    string msg1 = "Should select your section name. \n Please contact Administrator 5062-3464 to add section-code";
                    MessageBox.Show(msg1, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else
            {
                MessageBox.Show("Login failure", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private bool LoadSectionCode(string ID)
        {
            bool result = false;

            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SectioncodeAuthorizationSQL(ID);
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                if (dt1.Rows.Count > 0)
                {
                    Dict.UserSection.Clear();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string code = dt1.Rows[i].ItemArray[0].ToString();
                        string name = dt1.Rows[i].ItemArray[1].ToString();
                        Dict.UserSection.Add(code, code + " : " + name); //dictionary
                    }
                    CmbSection.DataSource = new BindingSource(Dict.UserSection, null);
                    CmbSection.DisplayMember = "Value";
                    CmbSection.ValueMember = "Key";
                    result = true;
                }
            }
            return result;


        }


        private void ReadDefault(string ID)
        {
            string filename = Paths.Login + ID + ".txt";
            bool isFileExists = File.Exists(filename);
            if (isFileExists == true)
            {
                string data;//= "";
                using (StreamReader sr = new StreamReader(filename))
                {
                    data = sr.ReadLine();
                    string[] d = new string[2];
                    d = data.Split(new char[] { ',' });

                    int i = 0;
                    foreach (KeyValuePair<string, string> item in Dict.UserSection)
                    {
                        if (item.Key == d[0])
                        {
                            CmbSection.SelectedIndex = i;
                            break;
                        }
                        i += 1;
                    }
                    comboBox2.SelectedIndex = Convert.ToInt32(d[1]);

                }

                string dayNight = OABorad.FindDayOrNight(DateTime.Now);
                comboBox3.SelectedIndex = (dayNight == "D") ? 0 : 1;


            }
        }


        private void Loginrecord(string ID, string type)
        {
            string filename = Paths.Login + ID + ".txt";
            bool isFileExists = File.Exists(filename);
            int cb2 = comboBox2.SelectedIndex;

            if (type == "W")
            {
                string Sectionkey = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Key;

                if (isFileExists == false)
                {
                    using (StreamWriter sw = File.CreateText(filename))
                    {
                        sw.WriteLine(Sectionkey + "," + cb2.ToString());
                        sw.Close();
                    }
                }
                else
                {
                    File.Delete(filename);
                    using (StreamWriter sw = File.CreateText(filename))
                    {
                        sw.WriteLine(Sectionkey + "," + cb2.ToString());
                        sw.Close();
                    }

                }
            }

        }

        private void UserLoginForm_Shown(object sender, EventArgs e)
        {
            Initial_loginLoader();
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TxtUserID.Text = "6000774";
            TxtPassCode.Focus();
            SubtxtPassCode();
            bool registed = LoadSectionCode(TxtUserID.Text);
            if (registed == true)
            {
                ReadDefault(TxtUserID.Text);
            }
            TxtPassCode.Text = "4";
            Login();
        }
    }
}
