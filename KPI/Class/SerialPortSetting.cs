using System.IO.Ports;
using System.Windows.Forms;
namespace KPI.Class
{
    public static class SerialPortSetting
    {

        public static Serialport com;

        public static void Initial(ComboBox CmbPort, ComboBox CmbRate, ComboBox CmbParity, ComboBox CmbData, ComboBox CmbStop)
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




        public struct Serialport
        {
            public string comPort;
            public string baudRate;
            public string parity;
            public string dataBits;
            public string stopBits;
        }



    }

}
