using KPI.Class;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPI.OptionalForm
{
    public partial class StockForm : Form
    {
        private static readonly SerialPort serialPort1 = new SerialPort();
        private static readonly SerialPort serialPort2 = new SerialPort();
        private static int cnt1;
        private static int cnt2;
        private readonly Dictionary<string, string> PiecePerKanbanDic = new Dictionary<string, string>();

        private static string productionId1;
        private static string productionId2;
        private static string mode1;
        private static string mode2;
        private readonly string StockSerialPort1 = @"C:\KPi\Login\StockSerialPort1.txt";
        private readonly string StockSerialPort2 = @"C:\KPi\Login\StockSerialPort2.txt";

        readonly CancellationTokenSource[] cts = new CancellationTokenSource[3];

        public StockForm()
        {
            InitializeComponent();
        }



        private void StockForm_Load(object sender, EventArgs e)
        {
            StartThread1running();
            StartThread2running();
            StartThread3running();
            BtnPort1.BackColor = Color.FromArgb(225, 225, 225);
            BtnPort2.BackColor = Color.FromArgb(225, 225, 225);
            LoadSettingAndOpenSerialPort(1, StockSerialPort1, serialPort1);
            LoadSettingAndOpenSerialPort(2, StockSerialPort2, serialPort2);
            ReadPiecePerKanban();
            InitialDgv();

        }


        private void LoadSettingAndOpenSerialPort(int port, string destination, SerialPort serialPort)
        {
            try
            {
                string setting = File.ReadAllText(destination);

                string[] parts = setting.Split(',');
                if (parts.Length == 6)
                {
                    string comport = parts[0];

                    string stopbit = parts[4];
                    string parity = parts[2];
                    string mode = parts[5];
                    serialPort.PortName = comport;
                    serialPort.BaudRate = Convert.ToInt32(parts[1]);
                    serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                    serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopbit);
                    serialPort.DataBits = Convert.ToInt16(parts[3]);

                    //};
                    string str = string.Format("Serial Port : {0}, {1}, {2}, {3}, {4},direction:{5}", comport, serialPort.BaudRate, serialPort.DataBits, parity, stopbit, mode);
                    serialPort.Handshake = Handshake.None;
                    int maxRetries = 5;
                    const int sleepTimeInMs = 500;
                    while (maxRetries > 0)
                    {
                        try
                        {
                            serialPort.Open();
                            if (serialPort.IsOpen)
                            {
                                serialPort.DiscardInBuffer();
                                if (port == 1)
                                {
                                    serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler1);
                                    BtnPort1.BackColor = Color.LightGreen;
                                    mode1 = mode;
                                    lbSerial1.Text = str;
                                }
                                else if (port == 2)
                                {
                                    serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler2);
                                    mode2 = mode;
                                    BtnPort2.BackColor = Color.LightGreen;
                                    lbSerial2.Text = str;
                                }
                                break;
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            maxRetries--;
                            Thread.Sleep(sleepTimeInMs);
                        }
                        catch (Exception exception)
                        {
                            maxRetries--;
                            Console.WriteLine(exception.Message);
                        }
                    }

                    if (maxRetries != 5)
                    {
                        Console.WriteLine("maxRetries:{0}", maxRetries);
                    }
                }
            }
            catch (Exception)
            {

            }

        }






        private void StockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopThread1running();
            StopThread2running();
            StopThread3running();
            //QRSerialPort.Close(serialPort1);
            //QRSerialPort.Close(serialPort2);

        }




        private void StartThread1running()
        {
            if (cts[0] != null)
            {
                return;
            }
            cts[0] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Thread1Stock), cts[0].Token);
        }
        private void StopThread1running()
        {
            if (cts[0] == null)
            {
                return;
            }
            cts[0].Cancel();
            Thread.Sleep(250);
            cts[0].Dispose();
            cts[0] = null;
        }
        void Thread1Stock(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            Thread.Sleep(1000);
            while (!token.IsCancellationRequested)
            {

                StockExc();
                Thread.Sleep(30000);

            }
        }






        private void StartThread2running()
        {
            if (cts[1] != null)
            {
                return;
            }
            cts[1] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Thread2Chart), cts[1].Token);
        }
        private void StopThread2running()
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
        void Thread2Chart(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            Thread.Sleep(2000);
            while (!token.IsCancellationRequested)
            {
                UpdateChart();
                Thread.Sleep(30000);

            }
        }








        private void StartThread3running()
        {
            if (cts[2] != null)
            {
                return;
            }
            cts[2] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Thread3Dgv), cts[2].Token);
        }
        private void StopThread3running()
        {
            if (cts[2] == null)
            {
                return;
            }
            cts[2].Cancel();
            Thread.Sleep(250);
            cts[2].Dispose();
            cts[2] = null;
        }
        void Thread3Dgv(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            Thread.Sleep(1000);
            while (!token.IsCancellationRequested)
            {
                UpdateDataGridView();
                Thread.Sleep(10000);

            }
        }




        private void StockScannerSettingFormClosed_Close(object sender, EventArgs e)
        {
            //int maxRetries = 5;
            //const int sleepTimeInMs = 500;
            //string loggingMessage = string.Empty;

            //string comport = StockScannerSettingForm.com.comPort;

            //string stopbit = StockScannerSettingForm.com.stopBits;
            //string parity = StockScannerSettingForm.com.parity;
            //iNSerialPort = new SerialPort(comport)
            //{
            //    BaudRate = Convert.ToInt32(StockScannerSettingForm.com.baudRate),
            //    Parity = (Parity)Enum.Parse(typeof(Parity), parity),
            //    StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopbit),
            //    DataBits = Convert.ToInt16(StockScannerSettingForm.com.dataBits)
            //};

            //string str = string.Format("Serial Port : {0}, {1}, {2}, {3}, {4}", comport, iNSerialPort.BaudRate, iNSerialPort.DataBits, parity, stopbit);
            //lbSerial1.Text = str;

            //iNSerialPort.Handshake = Handshake.None;

            //while (maxRetries > 0)
            //{
            //    try
            //    {
            //        iNSerialPort.Open();
            //        if (iNSerialPort.IsOpen)
            //        {
            //            iNSerialPort.DiscardInBuffer();
            //            iNSerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            //            ReadPiecePerKanban();
            //            break;
            //        }
            //    }
            //    catch (UnauthorizedAccessException)
            //    {
            //        maxRetries--;
            //        Thread.Sleep(sleepTimeInMs);
            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine(exception.Message);
            //    }
            //}

            //if (maxRetries != 5)
            //{
            //    Console.WriteLine("maxRetries:{0}", maxRetries);
            //}

        }
        private static void DataReceivedHandler1(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            productionId1 = sp.ReadExisting();
            cnt1++;
            serialPort1.DiscardInBuffer();
            Console.WriteLine("Data Received Port 1:{0} : {1}",cnt1, productionId1);
        }

        private static void DataReceivedHandler2(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            productionId2 = sp.ReadExisting();
            cnt2++;
            serialPort2.DiscardInBuffer();
            Console.WriteLine("Data Received Port 2:{0} : {1}", cnt2, productionId2);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (productionId1 != null && productionId1 != "")
            {
                productionId1 = productionId1.Substring(0, 15);
                AsyncInsertTable1(productionId1);
                Message1.Text = String.Format("Count: {0} ,PartNumber: {1}", cnt1, productionId1);
                productionId1 = null;
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (productionId2 != null && productionId2 != "")
            {
                productionId2 = productionId2.Substring(0, 15);
                AsyncInsertTable2(productionId2);
                Message2.Text = String.Format("Count: {0} ,PartNumber: {1}",cnt2,productionId2);
                productionId2 = null;
            }
        }

        private void ReadPiecePerKanban()
        {
            SqlClassWGR sql = new SqlClassWGR();
            bool status = sql.StockPiecePerKanban(User.SectionCode);
            if (status)
            {
                DataTable dt = sql.Datatable;
                PiecePerKanbanDic.Clear();
                int row = dt.Rows.Count;
                if (row > 0)
                {
                    for (int i = 0; i < row; i++)
                    {
                        string pn = dt.Rows[i][0].ToString();
                        string qty = dt.Rows[i][1].ToString();
                        PiecePerKanbanDic.Add(pn, qty);
                    }

                }

            }

        }






        async void AsyncInsertTable1(string partnumber)
        {
            string piece = string.Empty;
            if (!String.IsNullOrEmpty(partnumber))
            {
                SqlClassWGR sql = new SqlClassWGR();
                if (PiecePerKanbanDic.ContainsKey(partnumber))
                {
                    piece = PiecePerKanbanDic.FirstOrDefault(z => z.Key == partnumber).Value;
                    if (mode1 == "IN")
                    {
                    }
                    else if (mode1 == "OUT")
                    {
                        piece = String.Format("-{0}", piece);
                    }

                    await Task.Run(() => sql.StockDataInputInsert(User.SectionCode, partnumber, piece, User.ID));
                }
                else
                {
                    return;
                }
            }
        }
        async void AsyncInsertTable2(string partnumber)
        {
            string piece = string.Empty;
            if (!String.IsNullOrEmpty(partnumber))
            {
                SqlClassWGR sql = new SqlClassWGR();
                if (PiecePerKanbanDic.ContainsKey(partnumber))
                {
                    piece = PiecePerKanbanDic.FirstOrDefault(z => z.Key == partnumber).Value;
                    if (mode2 == "IN")
                    {
                    }
                    else if (mode2 == "OUT")
                    {
                        piece = String.Format("-{0}", piece);
                    }

                    await Task.Run(() => sql.StockDataInputInsert(User.SectionCode, partnumber, piece, User.ID));
                }
                else
                {
                    return;
                }
            }
        }

     
        async static void StockExc()
        {
            SqlClassWGR sql = new SqlClassWGR();
            await Task.Run(() => sql.SSQL_S("StockMonitorProcessExc", "@SectionCode", User.SectionCode));

            //foreach (var section in SectionCode)
            //{
            //    // await Task.Run(() => OABoardMethod(section));
            //    await Task.Run(() => PPASMethod(section));
            //}

        }







        private void BtnPort1_Click(object sender, EventArgs e)
        {
            //QRSerialPort.Close(serialPort1);
            StockScannerSettingForm frm = new StockScannerSettingForm(StockSerialPort1);
            frm.StockScannerSettingFormClosed += new EventHandler(StockScannerSettingFormClosed_Close);
            frm.Show();
            // frm.Show(new Form() { TopMost = true });
        }

        private void BtnPort2_Click(object sender, EventArgs e)
        {
            //QRSerialPort.Close(serialPort2);
            StockScannerSettingForm frm = new StockScannerSettingForm(StockSerialPort2);
            frm.StockScannerSettingFormClosed += new EventHandler(StockScannerSettingFormClosed_Close);
            frm.Show(new Form() { TopMost = true });
        }
        private void InitialDgv()
        {
            this.DgvDataInput.ColumnCount = 4;
            this.DgvDataInput.Columns[0].Name = "No";
            this.DgvDataInput.Columns[0].Width = 80;
            this.DgvDataInput.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDataInput.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvDataInput.Columns[1].Name = "RegistDateTime";
            this.DgvDataInput.Columns[1].Width = 150;
            this.DgvDataInput.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            //this.DgvDataInput.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvDataInput.Columns[2].Name = "PartNumber";
            this.DgvDataInput.Columns[2].Width = 150;
            this.DgvDataInput.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            //this.DgvDataInput.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvDataInput.Columns[3].Name = "Piece";
            this.DgvDataInput.Columns[3].Width = 50;
            this.DgvDataInput.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDataInput.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvDataInput.RowHeadersWidth = 4;
            this.DgvDataInput.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvDataInput.RowTemplate.Height = 20;
            this.DgvDataInput.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvDataInput.RowsDefaultCellStyle.BackColor = Color.White;
            this.DgvDataInput.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(169, 235, 187);
        }

      



        private void InvokereUpdateDgv(Action a)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(a));
            }
            catch { }
        }

        private void UpdateDataGridView()
        {
            InvokereUpdateDgv(() =>
            {
                SqlClassWGR sql = new SqlClassWGR();
                bool status = sql.ReadStockDataInput(User.SectionCode);
                DgvDataInput.Rows.Clear();
                if (status)
                {
                    DataTable dt = sql.Datatable;
                    int row = dt.Rows.Count;
                    if (row > 0)
                    {
                        for (int i = 0; i < row; i++)
                        {
                            string[] str = new string[4];
                            for (int j = 0; j < 4; j++)
                            {
                                str[j] = dt.Rows[i][j].ToString();
                            }
                            DgvDataInput.Rows.Add(str);
                        }
                    }
                }

            });

        }




        private void InvokereUpdateChart(Action b)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(b));
            }
            catch { }
        }

        private void UpdateChart()
        {
            InvokereUpdateChart(() =>
            {
                Color[] color = {Color.FromArgb(255,127,127),Color.FromArgb(191, 255, 191),  Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

                SqlClassWGR sql = new SqlClassWGR();
                bool status = sql.ReadStockMonitor(User.SectionCode);
                if (status)
                {
                    DataTable dt = sql.Datatable;
                    Charts.StockMonitoring(dt, ChartStockMonitor, color);
                }

            });

        }

    }
}
