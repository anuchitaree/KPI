using System;
using System.IO.Ports;
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
    public partial class TracebilityScannerSettingForm : Form
    {
        public static Serialport com;

        public event EventHandler TracebilityScannerSettingFormClosed;

        public TracebilityScannerSettingForm()
        {
            InitializeComponent();
        }

        private void TracebilityScannerSettingForm_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            CmbPort.Items.Clear();
            foreach (string port in ports)
            {
                CmbPort.Items.Add(port);
            }
            if (ports.Length > 0)
            {
                CmbPort.SelectedIndex = 0;
                com.comPort = CmbPort.SelectedItem.ToString();
            }
            com.baudRate = "38400";
            CmbRate.SelectedItem = com.baudRate;
            com.parity = "None";
            CmbParity.SelectedItem = com.parity;
            com.dataBits = "8";
            CmbData.SelectedItem = com.dataBits;
            com.stopBits = "One";
            CmbStop.SelectedIndex = 0;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            CmbPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                CmbPort.Items.Add(port);
            }
            if (ports.Length > 0)
            {
                CmbPort.SelectedIndex = 0;
                com.comPort = CmbPort.SelectedItem.ToString();
            }
        }

        private void CmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbPort.SelectedIndex > -1)
                com.comPort = this.CmbPort.GetItemText(CmbPort.SelectedItem);
        }

        private void CmbRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.baudRate = CmbRate.SelectedItem.ToString();
        }

        private void CmbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.parity = CmbParity.SelectedItem.ToString();
        }

        private void CmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.dataBits = CmbData.SelectedItem.ToString();
        }

        private void CmbStop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbStop.SelectedIndex == 0)
                com.stopBits = "One";
            else if (CmbStop.SelectedIndex == 1)
                com.stopBits = "OnePointFive";
            else if (CmbStop.SelectedIndex == 2)
                com.stopBits = "Two";
        }
        public struct Serialport
        {
            public string comPort;
            public string baudRate;
            public string parity;
            public string dataBits;
            public string stopBits;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.Close();
            //EventHandler handler = this.TracebilityScannerSettingFormClosed;
            //if (handler != null)
            //    handler(this, EventArgs.Empty);
            this.TracebilityScannerSettingFormClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
