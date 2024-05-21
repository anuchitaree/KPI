using OfficeOpenXml.FormulaParsing.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KPI.Parameter;
using KPI.InitialForm;
using KPI.Class;


namespace KPI.InitialForm
{
    public partial class UserRegistrationForm : Form
    {

        public event EventHandler LoginSuccess;
        public event EventHandler ExitProductivity;


        public UserRegistrationForm()
        {
            InitializeComponent();
        }

        private void UserRegistrationForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;

            Memory.UserRegistrationForm = true;
        }

        //private void tbID_Click(object sender, EventArgs e)
        //{
        //    TbID.Clear();
        //    TbID.ForeColor = Color.BlueViolet;
        //}

        //private void tbPasscode_Click(object sender, EventArgs e)
        //{
        //    TbPasscode.Clear();
        //    TbPasscode.PasswordChar = 'x';
        //}

        //private void tbPasscodec_Click(object sender, EventArgs e)
        //{
        //    TbPasscodec.Clear();
        //    TbPasscodec.PasswordChar = 'x';
        //}


        private void TbID_Click(object sender, EventArgs e)
        {
            TbID.Clear();
            TbID.ForeColor = Color.BlueViolet;
        }

        private void TbPasscode_Click(object sender, EventArgs e)
        {
            TbPasscode.Clear();
            TbPasscode.PasswordChar = 'x';
        }

        private void TbPasscodec_Click(object sender, EventArgs e)
        {
            TbPasscodec.Clear();
            TbPasscodec.PasswordChar = 'x';
        }


        //private void TbID_Click(object sender, EventArgs e)
        //{
        //    TbID.Clear();
        //    TbID.ForeColor = Color.BlueViolet;
        //}

        //private void TbPasscode_Click(object sender, EventArgs e)
        //{
        //    TbPasscode.Clear();
        //    TbPasscode.PasswordChar = 'x';
        //}

        //private void TbPasscodec_Click(object sender, EventArgs e)
        //{
        //    TbPasscodec.Clear();
        //    TbPasscodec.PasswordChar = 'x';
        //}



        //private void btnSumit_Click(object sender, EventArgs e)
        //{
        //    Submit();
        //}

        //private void btnExit_Click(object sender, EventArgs e)
        //{

        //}



        private void BtnSumit_Click(object sender, EventArgs e)
        {
            Submit();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Memory.UserRegistrationForm = false;
            this.ExitProductivity?.Invoke(this, EventArgs.Empty);

            this.Close();
        }



        private void UserRegistrationForm_Shown(object sender, EventArgs e)
        {
            TbID.Select();
            TbID.Focus();
        }

        //private void tbID_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //}

        //private void tbPasscode_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //}

        //private void tbPasscodec_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //}

        // /////////////////////////////// OPERATION LOOP //////////////////////////////////////////

        private void Submit()
        {


            label9.Text = "Result";
            int IdLen = TbID.TextLength;
            int passcodeLen1 = TbPasscode.TextLength;
            int passcodeLen2 = TbPasscodec.TextLength;
            bool id = int.TryParse(TbID.Text, out int userID);
            bool passcode1 = int.TryParse(TbPasscode.Text, out int passcodec1);
            bool passcode2 = int.TryParse(TbPasscodec.Text, out int passcodec2);

            if (IdLen > 1 && (passcodeLen1 == passcodeLen2) && (passcode1 == true) && (passcode2 == true) && (id == true) && TbPasscode.Text == TbPasscodec.Text)
            {

                SqlClass sql = new SqlClass();
                string msg = sql.UserRegistrationSSQL("UserRegistation", "@pUserID", userID, "@pPassword", TbPasscode.Text);
                if (msg == "Sucessfull")
                {
                    label9.Text = "User regists successfully";
                    TbPasscode.Text = TbPasscodec.Text = TbID.Text = "";

                    this.LoginSuccess?.Invoke(this, EventArgs.Empty);

                    this.Close();

                }
                else
                {
                    label9.Text = "User registed failure. Please contact Help_Desk";
                }


            }
            else
            {
                MessageBox.Show("Please fill USER ID and PASSCODE before submit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TbPasscode.Text = TbPasscodec.Text = "";
            }


        }

        private void TbID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                TbPasscode.PasswordChar = 'x';
                TbPasscode.Select();
                TbPasscode.Focus();
            }
        }

        private void TbPasscode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                TbPasscodec.PasswordChar = 'x';
                TbPasscodec.Select();
                TbPasscodec.Focus();
            }
        }

        private void TbPasscodec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Submit();
            }
        }

        //private void BtnExit_Click(object sender, EventArgs e)
        //{

        //}

        //private void BtnSumit_Click(object sender, EventArgs e)
        //{

        //}
    }
}
