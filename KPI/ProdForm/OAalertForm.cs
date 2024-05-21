using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPI.ProdForm
{
    public partial class OAalertForm : Form
    {
        private readonly string Linename = string.Empty;
        private readonly string Period = string.Empty;
        private readonly string PercentOA = string.Empty;
        public OAalertForm(string lineName,string period,string percentOA)
        {
            InitializeComponent();
            Linename = lineName;
            Period = period;
            PercentOA = percentOA;
        }

        public OAalertForm()
        {
        }

        private void OAalertForm_Load(object sender, EventArgs e)
        {
            lbTime.Text = string.Format("{0}", Period);
            lbOA.Text = string.Format("{0} %", PercentOA);
            lbLine.Text = Linename;
        }

       
        private void BtnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
