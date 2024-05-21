using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace KPI.OptionalForm
{
    public partial class StockScannerSettingForm : Form
    {

        public static Serialport com;
        public event EventHandler StockScannerSettingFormClosed;
    
        private static readonly SerialPort serialPort = new SerialPort();
        private static string productionId;


        private readonly string destination;
        public StockScannerSettingForm(string destination)
        {
            this.destination = destination;
        }


        public StockScannerSettingForm()
        {
            InitializeComponent();
        }

        private void StockScannerSettingForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            FormLoad();
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
            string inout = RadStockIn.Checked == true ? "IN" : "OUT";
            string str = $"{com.comPort},{com.baudRate},{com.parity},{com.dataBits},{com.stopBits},{inout} ";

            File.WriteAllText(destination, str);
            CloseComport(serialPort);

           
           
        }

        private void FormLoad()
        {
            string setting = string.Empty;
            string comport = string.Empty;
            string baudRate = string.Empty;
            string parity = string.Empty;
            string datBit = string.Empty;
            string stopbit = string.Empty;
            //CmbPort.Items.Clear();
            //CmbRate.Items.Clear();
            //CmbParity.Items.Clear();
            //CmbData.Items.Clear();
            //CmbStop.Items.Clear();

            try
            {

                if (File.Exists(destination))
                {
                    setting = File.ReadAllText(destination);
                }

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
                string[] parts = setting.Split(',');



                if (parts.Length == 5)
                {
                    comport = parts[0];
                    baudRate = parts[1];
                    parity = parts[2];
                    datBit = parts[3];
                    stopbit = parts[4];

                    CmbPort.SelectedItem = comport;
                    com.baudRate = baudRate;//"38400";
                    CmbRate.SelectedItem = com.baudRate;
                    com.parity = parity;//"None";
                    CmbParity.SelectedItem = com.parity;
                    com.dataBits = datBit;// "8";
                    CmbData.SelectedItem = com.dataBits;
                    com.stopBits = stopbit;// "One";
                    CmbStop.SelectedIndex = 0;


                    serialPort.PortName = comport;
                    serialPort.BaudRate = Convert.ToInt32(baudRate);
                    serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                    serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopbit);
                    serialPort.DataBits = Convert.ToInt16(datBit);
                }
                else
                {
                    //string comport = ;
                    baudRate = "19200";
                    parity = "None";
                    datBit = "8";
                    stopbit = "One";

                    com.baudRate = baudRate;//"38400";
                    CmbRate.SelectedItem = com.baudRate;
                    com.parity = parity;//"None";
                    CmbParity.SelectedItem = com.parity;
                    com.dataBits = datBit;// "8";
                    CmbData.SelectedItem = com.dataBits;
                    com.stopBits = stopbit;// "One";
                    CmbStop.SelectedIndex = 0;

                    //iNSerialPort.PortName = comport;
                    serialPort.BaudRate = Convert.ToInt32(baudRate);
                    serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                    serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopbit);
                    serialPort.DataBits = Convert.ToInt16(datBit);
                }


                serialPort.Handshake = Handshake.None;

                int maxRetries = 5;
                const int sleepTimeInMs = 500;
                CloseComport(serialPort);
                while (maxRetries > 0)
                {
                    try
                    {
                       
                        serialPort.Open();
                        if (serialPort.IsOpen)
                        {
                            LbPortOpen.Text = string.Format("Port {0} ready", comport);
                            serialPort.DiscardInBuffer();
                            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                            break;
                        }
                        else
                        {
                            LbPortOpen.Text = string.Format("Port {0} Closed", comport);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        maxRetries--;
                        Thread.Sleep(sleepTimeInMs);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }

                if (maxRetries != 5)
                {
                    Console.WriteLine("maxRetries:{0}", maxRetries);
                }



            }
            catch (Exception)
            {


            }

        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            productionId = sp.ReadExisting();

            serialPort.DiscardInBuffer();
            Console.WriteLine("Data Received:{0}", productionId);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (productionId != null)
            {
                productionId = productionId.Substring(0, 15);
                TbTrialText.Text = productionId;
                productionId = null;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            CloseComport(serialPort);

            this.Close();

            this.StockScannerSettingFormClosed?.Invoke(this, EventArgs.Empty);
        }

        private void CloseComport(SerialPort serialPort)
        {
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                {
                    serialPort.DiscardOutBuffer();
                    serialPort.DiscardInBuffer();
                    serialPort.Close();
                   // serialPort = null;
                }
            }

        }
    }
}
