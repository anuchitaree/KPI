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

namespace KPI.InitialForm
{
    public partial class UserPasswordChangeForm : Form
    {
        public UserPasswordChangeForm()
        {
            InitializeComponent();
        }

        private void UserPasswordChangeForm_Load(object sender, EventArgs e)
        {
          //  CommonDefine.userChangeId = 1;
        }

        //private void btnExit_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSumit_Click(object sender, EventArgs e)
        {

        }
    }
}
