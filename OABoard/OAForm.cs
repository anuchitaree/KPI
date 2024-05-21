using KPI.Class;
using OABoard.Models;
using OABoard.OAClass;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OABoard
{
    public partial class OAForm : Form
    {
        private string SectionCode = "4314-00";
        private string Div = "TSD";
        private string Plant = "WGR";
        private string Connection = "I";
        private string IPAddress = "182.52.108.173";
        private string SectionCodeName = "";
        private string Destination;
        public OAForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Board_Load(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            //    LbPlan.Text = LbActual.Text = LbDiff.Text = LbOR.Text = LbPartNumber.Text = LbCT.Text = LbProductionName.Text = "";
            BtnExit.Text = "Initail !";
            BtnExit.BackColor = Color.FromArgb(255, 0, 0);
            BtnExit.ForeColor = Color.Black;

            InitialSetupLoadFromFile();

            BackgroundConnection();
            BackgroundDateTime();

            //RefreshInitialOaBoard();

        }

        private async void BackgroundDateTime()
        {
            void InnerMethod()
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                    LbDateTime.Invoke(new Action(() =>
                    {
                        LbDateTime.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm:ss");
                    }));
                }
            }
            var task = new Task(() => InnerMethod());
            task.Start();
            await task;
        }

        private async void BackgroundConnection()
        {
            System.Threading.Thread.Sleep(2000);
            bool ConnectionStatus = false; ;
            async void InnerMethod()
            {
                int delaytime = 2000;
                while (true)
                {
                    System.Threading.Thread.Sleep(delaytime);
                    bool t = await Task.Run(() => Sql.SqlOpenConnection());
                    //  Console.WriteLine($"Memory is {ConnectionStatus} , Check status ={t}");
                    if ((t == false && ConnectionStatus == true) || (t == true && ConnectionStatus == false))
                    {
                        delaytime = 2000;
                        ConnectionStatus = t;
                    }
                    else if (t == false && ConnectionStatus == false)
                    {
                        BtnExit.Invoke(new Action(() =>
                        {
                            BtnExit.Text = "Disconnected !";
                            BtnExit.BackColor = Color.FromArgb(255, 0, 0);
                            BtnExit.ForeColor = Color.Black;
                        }));
                        delaytime = 2000;
                    }
                    else
                    {
                        delaytime = 10000;
                    }

                }
            }
            var task = new Task(() => InnerMethod());
            task.Start();
            await task;
        }

        private async void OABoradRunningTask()
        {
            int loop = Convert.ToInt32(Param.CTAverage *0.7*1000);
            async void InnerMethod()
            {
                while (true)
                {
                    OaBoard t = await Task.Run(() => OABoard.OAClass.OA.CalculateOA(SectionCode, DateTime.Now));
                    Show1(t);
                    System.Threading.Thread.Sleep(loop);
                }

            }
            var task = new Task(() => InnerMethod());
            task.Start();
            await task;
        }


        private void Show1(OaBoard a)
        {

            LbPlan.Invoke(new Action(() =>
            {
                LbPlan.Text = String.Format("{0:#,##0}", a.Plan);
            }));

            LbActual.Invoke(new Action(() =>
            {
                LbActual.Text = String.Format("{0:#,##0}", a.Actual);
            }));
            LbDiff.Invoke(new Action(() =>
            {
                LbDiff.Text = String.Format("{0:#,##0}", a.Diff);
            }));
            LbOR.Invoke(new Action(() =>
            {
                LbOR.Text = String.Format("{0:##0.#}", a.OR);
            }));
            LbCT.Invoke(new Action(() =>
            {
                LbCT.Text = String.Format("{0:###.#}", a.CT);
            }));
            LbPartNumber.Invoke(new Action(() =>
            {
                LbPartNumber.Text = a.Partnumber;
            }));
            BtnExit.Invoke(new Action(() =>
            {
                BtnExit.ForeColor = Color.Black;
            }));
            switch (a.LineStatus)
            {
                case Lintestatus.Operate:
                    BtnExit.Invoke(new Action(() =>
                    {
                        BtnExit.BackColor = Color.LawnGreen;
                        BtnExit.Text = "Operate";
                    }));
                    break;
                case Lintestatus.Break:
                    BtnExit.Invoke(new Action(() =>
                    {
                        BtnExit.BackColor = Color.Blue;
                        BtnExit.Text = "Break";
                    }));
                    break;
                case Lintestatus.Stop:
                    BtnExit.Invoke(new Action(() =>
                    {
                        BtnExit.BackColor = Color.Red;
                        BtnExit.Text = "LineStop";
                    }));
                    break;
                default:
                    BtnExit.Invoke(new Action(() =>
                    {
                        BtnExit.Text = " ??? ";
                        BtnExit.BackColor = Color.Pink;
                        BtnExit.ForeColor = Color.Black;
                    }));
                    break;
            }
            switch (a.OaStatus)
            {
                case OaStatus.Morethan:
                    LbActual.Invoke(new Action(() =>
                    {
                        LbActual.ForeColor = Color.LightGreen;
                    }));
                    LbOR.Invoke(new Action(() =>
                    {
                        LbOR.ForeColor = Color.LightGreen;
                    }));
                    break;
                case OaStatus.Lessthan:
                    LbActual.Invoke(new Action(() =>
                    {
                        LbActual.ForeColor = Color.Yellow;
                    }));
                    LbOR.Invoke(new Action(() =>
                    {
                        LbOR.ForeColor = Color.Yellow;
                    }));
                    break;
                    //case Oastatus.Abnormal:
                    //    LbActual.Invoke(new Action(() =>
                    //    {
                    //        LbActual.ForeColor = Color.Red;
                    //    }));
                    //    LbOR.Invoke(new Action(() =>
                    //    {
                    //        LbOR.ForeColor = Color.Red;
                    //    }));
                    //    break;

            }
            LbProductionName.Invoke(new Action(() =>
            {
                LbProductionName.Text = SectionCodeName;
            }));
        }


        private async void RefreshInitialOaBoard()
        {
            OABoard.OAClass.OA.InitOABoard(SectionCode, DateTime.Now, Div, Plant);
            void InnerMethod()
            {
                bool memory = false;
                while (true)
                {
                    DateTime dt = DateTime.Now;
                    DateTime ds1 = new DateTime(dt.Year, dt.Month, dt.Day, 7, 30, 1);
                    DateTime ds2 = new DateTime(dt.Year, dt.Month, dt.Day, 19, 30, 1);
                    if ((dt == ds1 || dt == ds2) && memory == false)
                    {
                        OABoard.OAClass.OA.InitOABoard(SectionCode, DateTime.Now, Div, Plant);
                        memory = true;
                    }
                    else
                    {
                        memory = false;
                    }

                    System.Threading.Thread.Sleep(500);
                }
            }
            var task = new Task(() => InnerMethod());
            task.Start();
            await task;
        }



        private void InitialSetupLoadFromFile()
        {
            try
            {
                string root = @"C:\KPi";
                string subRoot = $"{root}\\Login";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                if (!Directory.Exists(subRoot))
                {
                    Directory.CreateDirectory(subRoot);
                }
                Destination = $"{subRoot}\\ConnectionBoard.dat";
                bool isFileExists = File.Exists(Destination);
                if (isFileExists == false)
                {

                    SelectConnection();
                }

                var sources = File.ReadAllLines(Destination);

                if (sources.Length == 0)
                    return;
                string[] sectioncode = sources[0].Split(',');
                if (sectioncode.Length != 2)
                    return;
                SectionCode = sectioncode[0].Length == 7 ? sectioncode[0].Trim() : "4314-00";


                Connection = sectioncode[1].Trim().ToUpper();

                if (Connection == "E" && sources.Length == 2)
                {
                    string IPAddress = sources[1];
                    KPI.Parameter.Connection.DBOaBoard(Connection, IPAddress);
                }
                else if (Connection == "I")
                {
                    KPI.Parameter.Connection.DBOaBoard(Connection, "");
                }
                else
                {
                    MessageBox.Show("Initial file error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                using (var db = new ProductionEntities())
                {
                    var sectionFind = db.Emp_SectionTable.FirstOrDefault(s => s.sectionCode == SectionCode);
                    if (sectionFind == null)
                    {
                        return;
                    }
                    SectionCodeName = sectionFind.sectionName;
                    Div = sectionFind.divisionID;
                    Plant = sectionFind.plantID;
                }
                OABoradRunningTask();
                RefreshInitialOaBoard();
            }
            catch (Exception)
            {
                BtnExit.Invoke(new Action(() =>
                {
                    BtnExit.Text = "No Data";
                    BtnExit.BackColor = Color.Yellow;
                    BtnExit.ForeColor = Color.Black;
                }));
            }



        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LbProductionName_Click(object sender, EventArgs e)
        {
            try
            {
                string section = SectionCode;
                if (KPI.Class.InputBox.In("New SectionCode", "Such as 4314-00 : ", ref section) == DialogResult.OK)
                {

                    using (var db = new ProductionEntities())
                    {
                        var sectionFind = db.Emp_SectionTable.FirstOrDefault(s => s.sectionCode == section);
                        if (sectionFind == null)
                        {
                            return;
                        }

                    }


                    FileStream fsOverwrite = new FileStream(Destination, FileMode.Create);
                    using (StreamWriter sw = new StreamWriter(fsOverwrite))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append($"{section},{Connection} \n\r");
                        sb.Append($"{IPAddress}");
                        sw.WriteLine(sb.ToString());
                        sw.Close();
                    }
                    InitialSetupLoadFromFile();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       
        }

        private void LbStatus_Click(object sender, EventArgs e)
        {
            if (SelectConnection())
            {
                InitialSetupLoadFromFile();
            }
        }


        private bool SelectConnection()
        {
            bool result = false;
            try
            {
                string connection = Connection;
                if (KPI.Class.InputBox.In("New Connection", " I:Internet , E:Ethernet ", ref connection) == DialogResult.OK)
                {
                    connection = connection.Trim().ToUpper();
                    if (connection == "I" || connection == "E")
                    {
                        FileStream fsOverwrite = new FileStream(Destination, FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(fsOverwrite))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append($"{SectionCode},{connection} \n\r");
                            sb.Append($"{IPAddress}");
                            sw.WriteLine(sb.ToString());
                            sw.Close();
                        }
                        result = true;
                    }
                }
                return result;

            }
            catch (Exception)
            {

                return result;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CT weight-average", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //this.WindowState.MinimumSize = new Size(800, 600);
            ////this.DefaultMinimumSize = true;
            //this.MinimizeBox = false;
            //this.MaximizeBox = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //this.MaximizeBox = true;
        }
    }


}
