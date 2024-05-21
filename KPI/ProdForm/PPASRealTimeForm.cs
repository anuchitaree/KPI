using KPI.Class;
using KPI.DataContain;
using KPI.KeepClass;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace KPI.ProdForm
{
    public partial class PPASRealTimeForm : Form
    {
        readonly Label[] lbList = new Label[7];


        readonly CancellationTokenSource[] cts = new CancellationTokenSource[4];
        private string dayNight;
        readonly string SectionDivPlant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);
        private int _WorkingInHour = 0;
        private string[] workerday = new string[7];
        private string[] workernight = new string[7];

        public PPASRealTimeForm()
        {
            InitializeComponent();
            InitialDataGridView();
            PPASHistoryInitial();
        }

        private void PPASRealTimeForm_Load(object sender, EventArgs e)
        {
            InitialComponent.DateTimePicker(dateTimePicker2);
            for (int i = 0; i < 7; i++)
            {
                textList2[i].Text = "";
            }
            RedFront();
            StartPPASFrameLoading();
            dayNight = string.Empty;

        }

        private void PPASRealTimeForm_Shown(object sender, EventArgs e)
        {
            StartORrunning();
            StartThreadStatus();
        }

        private void BtnUpdateRedFront_Click(object sender, EventArgs e)
        {
            RedFront();
        }


        private void PPASRealTimeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StoptThreadStatus();

            StopORrunning();

            StopPPASrunning();

            StopPPASFrameLoading();
        }


        ////////////////////// LOOP OPERATION /////////////////////////////////
        ///
        #region Using THREAD
        //=======================================================//

        #region THREAD POOL PPAS UPDATE BY DAY

        private void StartPPASrunning()
        {
            if (cts[0] != null)
            {
                return;
            }
            cts[0] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolPPAS), cts[0].Token);
        }

        private void StopPPASrunning()
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

        void ThreadPoolPPAS(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            Thread.Sleep(5000);
            while (!token.IsCancellationRequested)
            {
                Refresh31(DateTime.Now);
                Thread.Sleep(60000);
            }
        }

        private void Invokerefresh31(Action b)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(b));
            }
            catch { }
        }

        private void Refresh31(DateTime dt)
        {
            Invokerefresh31(() =>
            {
                Color[] color = {Color.Black,Color.Blue,Color.FromArgb(170,130,0), Color.FromArgb(255, 0, 255),
                    Color.FromArgb(0, 231,0), Color.FromArgb(255, 105, 54),
                    Color.FromArgb(78, 213, 251), Color.FromArgb(101, 85, 252),Color.FromArgb(255, 0, 0) };
                string section = User.SectionCode;
                string dayNight = User.DayNight;


                DataGridViewCellStyle styleNormal = new DataGridViewCellStyle
                {
                    ForeColor = Color.Black
                };
                DataGridViewCellStyle styleAbnormal = new DataGridViewCellStyle
                {
                    ForeColor = Color.Red
                };

                DateTime registDate = RegistDateTime.FindRegistDateFromCurrentTime(dt);
                int datePosition = (int)registDate.DayOfWeek == 0 ? 7 : (int)registDate.DayOfWeek;
                try
                {
                    using (var db = new ProductionEntities11())
                    {
                        var ppasOnScreen = db.Prod_PPASTable
                        .Where(s => s.sectionCode == section)
                        .Where(d => d.dayNight == dayNight)
                        .Where(r => r.registDate == registDate).OrderBy(x => x.hour).ToList();

                        int hh = 0;
                        foreach (Prod_PPASTable p in ppasOnScreen)
                        {
                            dataGridView1.Rows[hh].Cells[datePosition * 3 + 1].Value = p.volume;
                            dataGridView1.Rows[hh].Cells[datePosition * 3 + 2].Value = p.accVol;
                            dataGridView1.Rows[hh].Cells[datePosition * 3 + 3].Value = p.percentOA;
                            dataGridView2.Rows[hh].Cells[datePosition + 2].Value = p.volumePerHr;


                            if (p.hour <= _WorkingInHour)
                            {
                                //Console.WriteLine(p.hour);
                                dataGridView1.Rows[hh].Cells[datePosition * 3 + 1].Style = p.redAlarm == "A" ? styleAbnormal : styleNormal;
                                dataGridView2.Rows[hh].Cells[datePosition + 2].Style = p.redAlarm == "A" ? styleAbnormal : styleNormal;
                            }
                            else
                            {
                                dataGridView1.Rows[hh].Cells[datePosition * 3 + 1].Style = styleNormal;
                                dataGridView2.Rows[hh].Cells[datePosition + 2].Style = styleNormal;
                            }
                            lbWorking.Text = $"OA Target = {p.stdOA} %";
                            hh++;
                        }

                        Charts.RedRatio2(dataGridView2, chartRed, color, ppasOnScreen);


                    }
                }
                catch 
                { 
                }



            });

        }


        #endregion

        //=======================================================//

        #region THREAD POOL OR RUNNING
        private void StartORrunning()
        {
            if (cts[1] != null)
            {
                return;
            }
            cts[1] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolOR), cts[1].Token);
        }

        private void StopORrunning()
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

        void ThreadPoolOR(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            Thread.Sleep(1000);
            while (!token.IsCancellationRequested)
            {
                ORBoard();
                Thread.Sleep(15000);

            }
        }

        private void InvokeOR(Action a)
        {
            this.BeginInvoke(new MethodInvoker(a));
        }

        private void ORBoard()
        {
            InvokeOR(() =>
            {
                OA o = new OA();
                o = OABorad.CalculateOA(DateTime.Now, SectionDivPlant);
                if (o.status)
                {
                    tbPlan.Text = o.QtyPlan;
                    tbActual.Text = o.QtyTotal;
                    tbdiff.Text = o.Diff;
                    tbOR.Text = o.OR.ToString("0.0");
                    tbCT.Text = o.AvgCT;
                    tbActual.ForeColor = (o.OR < o.ORTarget) ? Color.Red : Color.Green;
                    tbOR.ForeColor = (o.OR < o.ORTarget) ? Color.Red : Color.Green;
                    _WorkingInHour = o.WorkingInHour;
                }


            });
        }

        #endregion

        //=======================================================//
        #region THREAD POOL MC STATUS RUNNING
        private void StartThreadStatus()
        {
            if (cts[2] != null)
            {
                return;
            }
            cts[2] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadStatus), cts[2].Token);
        }

        private void StoptThreadStatus()
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

        void ThreadStatus(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            Thread.Sleep(1000);
            while (!token.IsCancellationRequested)
            {
                try
                {

                    Thread.Sleep(500);
                    if (labelRunning.InvokeRequired)
                    {
                        labelRunning.Invoke(new Action(() =>
                        {
                            labelRunning.BackColor = Color.Green;
                        }));
                    }
                    else
                    {
                        labelRunning.BackColor = Color.Green;
                    }


                    Thread.Sleep(500);
                    if (labelRunning.InvokeRequired)
                    {
                        labelRunning.Invoke(new Action(() =>
                        {
                            labelRunning.BackColor = Color.Yellow;
                        }));
                    }
                    else
                    {
                        labelRunning.BackColor = Color.Yellow;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //throw;
                }


            }

        }


        #endregion

        //=======================================================//
        #region THREAD POOL FRAM LOADING

        private void StartPPASFrameLoading()
        {
            if (cts[3] != null)
            {
                return;
            }
            cts[3] = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolPPASFrameLoading), cts[3].Token);
        }

        private void StopPPASFrameLoading()
        {
            if (cts[3] == null)
            {
                return;
            }
            cts[3].Cancel();
            Thread.Sleep(250);
            cts[3].Dispose();
            cts[3] = null;
        }

        void ThreadPoolPPASFrameLoading(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            Refresh3(DateTime.Now);
            while (!token.IsCancellationRequested)
            {
                //refresh3(DateTime.Now);

                if (dayNight != User.DayNight)
                {
                    StopPPASrunning();
                    Thread.Sleep(1000);
                    Refresh3(DateTime.Now);
                    dayNight = User.DayNight;
                    StartPPASrunning();
                }
                Thread.Sleep(5000);
            }
        }

        private void Invokerefresh3(Action c)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(c));
            }
            catch (Exception)
            {

                //throw;
            }

        }

        private void Refresh3(DateTime dt)
        {
            Invokerefresh3(() =>
            {

                Color[] color = {Color.Black,Color.Blue,Color.FromArgb(170,130,0), Color.FromArgb(255, 0, 255),
                    Color.FromArgb(0, 231,0), Color.FromArgb(255, 105, 54),
                    Color.FromArgb(78, 213, 251), Color.FromArgb(101, 85, 252),Color.FromArgb(255, 0, 0), Color.Black,Color.FromArgb(24, 86, 0) };



                if (RadBetweenMonth.Checked == true && RadInMonth.Checked == false)
                {
                    DateTime registDate = OABorad.FindRegistDateFromCurrentTime(dt);
                    var d = new DayAndWeekClass();

                    PpasStartEndDate startday = d.FindStartEndDate(dt);

                    for (int i = 0; i < 7; i++)
                    {
                        DateTime rundate = startday.StartDate.AddDays(i);
                        lbList[i].Text = rundate.ToString("dddd \n d-MM-yyyy");

                    }

                    DataGridViewCellStyle styleNormal = new DataGridViewCellStyle
                    {
                        ForeColor = Color.Black
                    };
                    DataGridViewCellStyle styleAbnormal = new DataGridViewCellStyle
                    {
                        ForeColor = Color.Red
                    };
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    while (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows.RemoveAt(0);
                    }

                    for (int h = 0; h < 11; h++)
                    {
                        string[] row = new string[25];
                        dataGridView1.Rows.Add(row);
                    }

                    for (int h = 0; h < 10; h++)
                    {
                        string[] row = new string[10];
                        dataGridView2.Rows.Add(row);
                    }

                    using (var db = new ProductionEntities11())
                    {
                        var ppas7day = db.Prod_PPASTable
                         .Where(r => r.registDate >= startday.StartDate && r.registDate <= startday.EndDate)
                         .Where(w => w.dayNight == User.DayNight)
                         .Where(s => s.sectionCode == User.SectionCode).OrderBy(h => h.hour).ToList();


                        for (int day = 0; day < 7; day++)
                        {
                            DateTime startDate = startday.StartDate.AddDays(day);
                            var onePpasExist = ppas7day.Where(r => r.registDate == startDate);
                            if (onePpasExist.Any())
                            {
                                var onePpas1 = onePpasExist.OrderBy(h => h.hour).ToList();
                                int DayInWeek1 = (int)startDate.DayOfWeek;
                                int startcolumn = 1 + DayInWeek1 * 3;

                                int j = 0;
                                foreach (Prod_PPASTable item in onePpasExist)
                                {
                                    dataGridView1.Rows[j].Cells[startcolumn].Value = item.volume;
                                    dataGridView1.Rows[j].Cells[startcolumn + 1].Value = item.accVol;
                                    dataGridView1.Rows[j].Cells[startcolumn + 2].Value = item.percentOA;
                                    dataGridView1.Rows[10].Cells[2 + startcolumn].Value = item.percentOAavg;

                                    dataGridView2.Rows[j].Cells[2 + DayInWeek1].Value = item.volumePerHr;

                                    if (item.redAlarm == "A" && item.workStatus == "W")
                                    {
                                        dataGridView1.Rows[j].Cells[startcolumn].Style = styleAbnormal;
                                        dataGridView2.Rows[j].Cells[2 + DayInWeek1].Style = styleAbnormal;
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[j].Cells[startcolumn].Style = styleNormal;
                                        dataGridView2.Rows[j].Cells[2 + DayInWeek1].Style = styleNormal;
                                    }

                                    j++;
                                }



                            }
                        }

                        // Frame 

                        var frameDb = db.Prod_PPASTable.Where(r => r.registDate == startday.StartDate)
                            .Where(n => n.dayNight == User.DayNight)
                            .Where(s => s.sectionCode == User.SectionCode).ToList();

                        var onePpas = frameDb.OrderBy(h => h.hour).ToList();
                        int jj = 0;

                        foreach (Prod_PPASTable item in onePpas)
                        {
                            dataGridView1.Rows[jj].Cells[0].Value = item.monitor;

                            dataGridView1.Rows[jj].Cells[1].Value = item.period;

                            double period = item.period ?? 0;
                            double stdOA = item.stdOA ?? 0;
                            double plan100 = item.plan100 ?? 0;
                            double plan85 = plan100 * stdOA / 100;
                            double acc100 = item.accPlan ?? 0;
                            double acc85 = acc100 * stdOA / 100;
                            string buf = $"{plan100:0} - {plan85:0}";

                            dataGridView1.Rows[jj].Cells[2].Value = buf;
                            buf = ($"{acc100:0} - {acc85:0}");
                            dataGridView1.Rows[jj].Cells[3].Value = buf;

                            dataGridView2.Rows[jj].Cells[0].Value = item.monitor;
                            double Red100 = plan100 * 3600 / period;
                            dataGridView2.Rows[jj].Cells[1].Value = Red100.ToString("0");
                            double Red85 = plan85 * 3600 / period;
                            dataGridView2.Rows[jj].Cells[2].Value = Red85.ToString("0");
                            jj++;
                        }

                        dataGridView1.Rows[10].Cells[3].Value = "Total";
                        for (int i = 0; i < 7; i++)
                        {
                            dataGridView1.Rows[10].Cells[5 + i * 3].Value = dataGridView1.Rows[9].Cells[5 + i * 3].Value;
                        }

                    }

                    Charts.RedRatio(dataGridView2, chartRed, color);
                    ChartLegent.Legend_DTMultiColor(tableLayoutPanel16, 1, "tableLayoutPanel16", "lbchartLoss", color, LegendPlate.GetDayName());
                }

                // Pattern In MONTH
                else if (RadBetweenMonth.Checked == false && RadInMonth.Checked == true)
                {
                    DateTime registDate = OABorad.FindRegistDateFromCurrentTime(dt);
                    var d = new DayAndWeekClass();
                    PpasDisplayMember startday = d.FindStartingDateInWeek(registDate);

                    DataGridViewCellStyle styleNormal = new DataGridViewCellStyle
                    {
                        ForeColor = Color.Black
                    };
                    DataGridViewCellStyle styleAbnormal = new DataGridViewCellStyle
                    {
                        ForeColor = Color.Red
                    };
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    while (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows.RemoveAt(0);
                    }

                    for (int h = 0; h < 11; h++)
                    {
                        string[] row = new string[25];
                        dataGridView1.Rows.Add(row);
                    }

                    for (int h = 0; h < 10; h++)
                    {
                        string[] row = new string[10];
                        dataGridView2.Rows.Add(row);
                    }
                    DateTime firstdate = startday.StartingDate;
                    firstdate = new DateTime(firstdate.Year, firstdate.Month, firstdate.Day);
                    int numberOfDayInWeek = startday.DayInWeek;



                    DateTime lastdate = firstdate.AddDays(startday.NumberOfday - 1);

                    using (var db = new ProductionEntities11())
                    {
                        var Ppas7dayAll = db.Prod_PPASTable.Where(r => r.registDate >= firstdate && r.registDate <= lastdate)
                        .Where(s => s.sectionCode == User.SectionCode).Where(dd => dd.dayNight == User.DayNight).ToList();

                        // Frame
                        for (int i = 0; i < startday.NumberOfday; i++)
                        {
                            DateTime startdate = firstdate.AddDays(i);
                            var monitorExist = Ppas7dayAll.Where(r => r.registDate == startdate).OrderBy(o => o.hour).ToList();
                            if (monitorExist.Count > 0)
                            {
                                int k = 0;
                                foreach (var item in monitorExist)
                                {
                                    dataGridView1.Rows[k].Cells[0].Value = item.monitor;

                                    dataGridView1.Rows[k].Cells[1].Value = item.period;

                                    double period = item.period ?? 0;
                                    double stdOA = item.stdOA ?? 0;
                                    double plan100 = item.plan100 ?? 0;
                                    double plan85 = plan100 * stdOA / 100;
                                    double acc100 = item.accPlan ?? 0;
                                    double acc85 = acc100 * stdOA / 100;
                                    string buf = $"{plan100:0} - {plan85:0}";

                                    dataGridView1.Rows[k].Cells[2].Value = buf;
                                    buf = ($"{acc100:0} - {acc85:0}");
                                    dataGridView1.Rows[k].Cells[3].Value = buf;

                                    dataGridView2.Rows[k].Cells[0].Value = item.monitor;
                                    double Red100 = plan100 * 3600 / period;
                                    dataGridView2.Rows[k].Cells[1].Value = Red100.ToString("0");
                                    double Red85 = plan85 * 3600 / period;
                                    dataGridView2.Rows[k].Cells[2].Value = Red85.ToString("0");

                                    k++;
                                }
                            }

                        }
                        // Display
                        for (int i = 0; i < startday.NumberOfday; i++)
                        {
                            DateTime startdate = firstdate.AddDays(i);
                            var displayExist = Ppas7dayAll.Where(r => r.registDate == startdate).OrderBy(o => o.hour).ToList();
                            int daysOfweek1 = (int)startdate.DayOfWeek;
                            int daysOfweek = daysOfweek1 == 0 ? 6 : daysOfweek1 - 1;
                            int startColumn = daysOfweek * 3 + 4;
                            if (displayExist.Count > 0)
                            {
                                int k = 0;
                                foreach (var item in displayExist)
                                {
                                    dataGridView1.Rows[k].Cells[startColumn].Value = item.volume;
                                    dataGridView1.Rows[k].Cells[startColumn + 1].Value = item.accVol;
                                    dataGridView1.Rows[k].Cells[startColumn + 2].Value = item.percentOA;
                                    dataGridView1.Rows[10].Cells[2 + startColumn].Value = item.percentOAavg;

                                    dataGridView2.Rows[k].Cells[daysOfweek + 3].Value = item.volumePerHr;
                                    if (item.redAlarm == "A" && item.workStatus == "W")
                                    {
                                        dataGridView1.Rows[k].Cells[startColumn].Style = styleAbnormal;
                                        dataGridView2.Rows[k].Cells[daysOfweek + 3].Style = styleAbnormal;
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[k].Cells[startColumn].Style = styleNormal;
                                        dataGridView2.Rows[k].Cells[daysOfweek + 3].Style = styleNormal;
                                    }

                                    k++;
                                }
                            }

                        }


                        dataGridView1.Rows[10].Cells[3].Value = "Total";
                        for (int i = 0; i < 7; i++)
                        {
                            dataGridView1.Rows[10].Cells[5 + i * 3].Value = dataGridView1.Rows[9].Cells[5 + i * 3].Value;
                        }



                    }


                    for (int i = 0; i < 7; i++)
                    {
                        lbList[i].Text = string.Empty;

                    }
                    for (int i = 0; i < startday.NumberOfday; i++)
                    {
                        DateTime rundate = firstdate.AddDays(i);
                        lbList[i + numberOfDayInWeek - 1].Text = rundate.ToString("dddd \n dd-MM-yyyy");
                    }

                }

            });

        }



        #endregion

        //=======================================================//
        #endregion


        #region RED FRONT

        private void RedFront()
        {
            Color[] color = { Color.Blue,Color.Red };

            DateTime dtstart = dateTimePicker2.Value;
            DateTime dtstop = dateTimePicker3.Value;
            dtstart = new DateTime(dtstart.Year, dtstart.Month, dtstart.Day);
            dtstop = new DateTime(dtstop.Year, dtstop.Month, dtstop.Day);

            //query.Append("select registDate, count(*) FROM[Production].[dbo].[Prod_PPASTable] ");
            //query.AppendFormat(" where registDate between '{0}' and '{1}' ", startdate, stopdate);
            //query.AppendFormat("and sectionCode = '{0}'  and alarm = 'A'  Group by registDate order by registDate asc",
            //    User.SectionCode);

            using (var db = new ProductionEntities11())
            {
                var LoadRedFront = db.Prod_PPASTable.Where(r => r.registDate >= dtstart && r.registDate <= dtstop)
                    .Where(s => s.sectionCode == User.SectionCode).Where(a => a.alarm == "A")
                    .GroupBy(g => g.registDate).Select(n => new ProdRedFront
                    {
                        RegistDate = n.Key,
                        RedCount = n.Count(a => a.alarm=="A"),
                    }).ToList();
                if (LoadRedFront.Count > 0)
                {
                    int datasize = LoadRedFront.Count;
                    string[] dats = new string[datasize];
                    string[] redC = new string[datasize + 1];
                    redC[0] = "RedCount";
                    var LoadRedFrontSort = LoadRedFront.OrderBy(r => r.RegistDate).ToList();
                    int i = 0;
                    foreach (var item in LoadRedFrontSort)
                    {
                        dats[i] = item.RegistDate.ToString("dd-MM-yy");
                        redC[i + 1] = item.RedCount < 0 ? "0" : item.RedCount.ToString();
                        i++;
                    }
                    //SetGridView3(dats, redC, datasize);
                    Charts.RedFront1(LoadRedFrontSort, chartRedCount, color);
                    ChartLegent.Legend_DTMultiColor(tableLayoutPanel9, 1, "tableLayoutPanel9", "lbchartLoss", color, LegendPlate.GetRedCountName());
                }
            }


            


        }


        //private void SetGridView3(string[] header, string[] dat, int size)
        //{
        //    this.dataGridView3.ColumnCount = size + 1;
        //    this.dataGridView3.Columns[0].Name = "Date";
        //    this.dataGridView3.Columns[0].Width = 80;
        //    this.dataGridView3.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

        //    for (int i = 0; i < size; i++)
        //    {
        //        this.dataGridView3.Columns[1 + i].Name = header[i];
        //        this.dataGridView3.Columns[1 + i].Width = 55;
        //        this.dataGridView3.Columns[1 + i].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    }
        //    this.dataGridView3.RowHeadersWidth = 10;
        //    this.dataGridView3.DefaultCellStyle.Font = new Font("Tahoma", 8);
        //    this.dataGridView3.RowTemplate.Height = 20;
        //    this.dataGridView3.Rows.Clear();
        //    this.dataGridView3.Rows.Add(dat);
        //}

        #endregion



        #region inital datagridview

        private void InitialDataGridView()
        {
            this.dataGridView1.ColumnCount = 32 - 7;
            this.dataGridView1.Columns[0].Name = "Time";
            this.dataGridView1.Columns[0].Width = 80;
            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].Name = "Period";
            this.dataGridView1.Columns[1].Width = 35;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[2].Name = "P100-Tar";
            this.dataGridView1.Columns[2].Width = 60;
            this.dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[3].Name = "A100-Tar";
            this.dataGridView1.Columns[3].Width = 70;
            this.dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            for (int i = 0; i < 7; i++)
            {

                this.dataGridView1.Columns[4 + i * 3].Name = "Actual";
                this.dataGridView1.Columns[4 + i * 3].Width = 36;
                this.dataGridView1.Columns[4 + i * 3].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridView1.Columns[4 + i * 3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.dataGridView1.Columns[5 + i * 3].Name = "Accumulate";
                this.dataGridView1.Columns[5 + i * 3].Width = 41;
                this.dataGridView1.Columns[5 + i * 3].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridView1.Columns[5 + i * 3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.dataGridView1.Columns[6 + i * 3].Name = "%OA";
                this.dataGridView1.Columns[6 + i * 3].Width = 40;
                this.dataGridView1.Columns[6 + i * 3].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridView1.Columns[6 + i * 3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            }

            this.dataGridView1.RowHeadersWidth = 5;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridView1.RowTemplate.Height = 25;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;

            this.dataGridView2.ColumnCount = 10;
            this.dataGridView2.Columns[0].Name = "Time";
            this.dataGridView2.Columns[0].Width = 75;
            this.dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[1].Name = "100";
            this.dataGridView2.Columns[1].Width = 30;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[2].Name = "Tar";
            this.dataGridView2.Columns[2].Width = 30;
            this.dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;


            string[] day = new string[] { "Mon", "Tue", "Wen", "Thu", "Fri", "Sat", "Sun" };
            for (int i = 0; i < 7; i++)
            {
                this.dataGridView2.Columns[3 + i].Name = day[i];
                this.dataGridView2.Columns[3 + i].Width = 30;
                this.dataGridView2.Columns[3 + i].SortMode = DataGridViewColumnSortMode.NotSortable;

            }

            this.dataGridView2.RowHeadersWidth = 5;
            this.dataGridView2.DefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridView2.RowTemplate.Height = 25;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.AllowUserToResizeColumns = false;

            InitialLabel();

        }

        private void InitialLabel()
        {
            lbList[0] = lb20;
            lbList[1] = lb21;
            lbList[2] = lb22;
            lbList[3] = lb23;
            lbList[4] = lb24;
            lbList[5] = lb25;
            lbList[6] = lb26;

        }



        #endregion


        // TAB 2

        readonly TextBox[] textList2 = new TextBox[7];
        readonly Label[] lbList2 = new Label[7];

        private void BtnHistoryUpdate_Click(object sender, EventArgs e)
        {
            HistoryPpasUpdate();
        }

        private void HistoryPpasUpdate()
        {
            Color[] color = {Color.Black,Color.Blue,Color.FromArgb(170,130,0), Color.FromArgb(255, 0, 255),
                    Color.FromArgb(0, 231,0), Color.FromArgb(255, 105, 54),
                    Color.FromArgb(78, 213, 251), Color.FromArgb(101, 85, 252),Color.FromArgb(255, 0, 0) ,Color.FromArgb(101, 85, 252),Color.FromArgb(255, 0, 0) };

            DateTime dts = dateTimePicker1.Value;
            InitialHistory();
            if (RadHisBM.Checked == true && RadHisIM.Checked == false)
            {
                HistoryPpasBetweenMonth(dts);
            }
            else
            {
                HistoryPpasOneMonth(dts);
            }
            Charts.RedRatio(dataGridViewD, chartDay, color);
            Charts.RedRatio(dataGridViewN, chartNight, color);
            ChartLegent.Legend_DTMultiColor(tableLayoutPanelDay1, 1, "tableLayoutPanelDay1", "lbchartLoss", color, LegendPlate.GetDayHistoryName());
            ChartLegent.Legend_DTMultiColor(tableLayoutPanelNight1, 1, "tableLayoutPanelNight1", "lbchartLoss", color, LegendPlate.GetDayHistoryName());
        }

        private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            string[] date = new string[7];
            string startdate = "";
            string enddate = "";
            for (int i = 0; i < 7; i++)
            {
                date[i] = textList2[i].Text;
            }


            for (int i = 0; i < 7; i++)
            {
                if (date[i] != "")
                {
                    startdate = date[i];
                    break;
                }

            }
            for (int i = 6; i >= 0; i--)
            {
                if (date[i] != "")
                {
                    enddate = date[i];
                    break;
                }

            }

            ExportExcel exp = new ExportExcel();
            DataGridViewToExcelResult result = exp.FileNamePpasCSV("PPAS", User.SectionCode, $"{startdate}-{enddate}");
            if (result.Status)
            {

                if (exp.ExportToCSV(result.FileName, date, dataGridViewDay, dataGridViewNight))
                {
                    MessageBox.Show("Export data to CSV file completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void BtnRedRationGp_Click(object sender, EventArgs e)
        {
            int newWidth = 1036;
            panelGraph.MaximumSize = new Size(newWidth, panelGraph.Height);
            panelGraph.Size = new Size(newWidth, panelGraph.Height);
            panelGraph.Visible = true;
            panelTable.Visible = false;
        }

        private void BtnRedRatioTable_Click(object sender, EventArgs e)
        {
            panelTable.Visible = true;
            panelGraph.Visible = false;
        }

        private void PPASHistoryInitial()
        {
            InitialDataGridView2();
            InitialTextBox2();
            InitialLabel2();
            for (int i = 0; i < 7; i++)
            {
                textList2[i].Text = "01-01-1900";

                lbList2[i].Text = DataContain.Name.Day[i];
            }

        }



        #region InitialDataGridView

        private void InitialTextBox2()
        {
            textList2[0] = textBox2;
            textList2[1] = textBox3;
            textList2[2] = textBox4;
            textList2[3] = textBox5;
            textList2[4] = textBox6;
            textList2[5] = textBox7;
            textList2[6] = textBox8;
        }

        private void InitialLabel2()
        {

            lbList2[0] = label13;
            lbList2[1] = label20;
            lbList2[2] = label23;
            lbList2[3] = label12;
            lbList2[4] = label11;
            lbList2[5] = label14;
            lbList2[6] = label21;

        }



        private void InitialDataGridView2()
        {
            this.dataGridViewDay.ColumnCount = 32;
            this.dataGridViewDay.Columns[0].Name = "Time";
            this.dataGridViewDay.Columns[0].Width = 80;
            this.dataGridViewDay.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewDay.Columns[1].Name = "Period";
            this.dataGridViewDay.Columns[1].Width = 40;
            this.dataGridViewDay.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewDay.Columns[2].Name = "Plan100-Tar";
            this.dataGridViewDay.Columns[2].Width = 60;
            this.dataGridViewDay.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewDay.Columns[3].Name = "Acc100-Tar";
            this.dataGridViewDay.Columns[3].Width = 70;
            this.dataGridViewDay.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < 7; i++)
            {
                this.dataGridViewDay.Columns[4 + i * 4].Name = "";
                this.dataGridViewDay.Columns[4 + i * 4].Width = 1;
                this.dataGridViewDay.Columns[4 + i * 4].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewDay.Columns[5 + i * 4].Name = "Act";
                this.dataGridViewDay.Columns[5 + i * 4].Width = 30;
                this.dataGridViewDay.Columns[5 + i * 4].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewDay.Columns[5 + i * 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridViewDay.Columns[6 + i * 4].Name = "Acc";
                this.dataGridViewDay.Columns[6 + i * 4].Width = 40;
                this.dataGridViewDay.Columns[6 + i * 4].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewDay.Columns[6 + i * 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridViewDay.Columns[7 + i * 4].Name = "%OA";
                this.dataGridViewDay.Columns[7 + i * 4].Width = 40;
                this.dataGridViewDay.Columns[7 + i * 4].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewDay.Columns[7 + i * 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            this.dataGridViewDay.RowHeadersWidth = 10;
            this.dataGridViewDay.DefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridViewDay.RowTemplate.Height = 30;
            dataGridViewDay.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewDay.AllowUserToResizeRows = false;
            dataGridViewDay.AllowUserToResizeColumns = false;

            this.dataGridViewNight.ColumnCount = 32;
            this.dataGridViewNight.Columns[0].Name = "Time";
            this.dataGridViewNight.Columns[0].Width = 80;
            this.dataGridViewNight.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewNight.Columns[1].Name = "Period";
            this.dataGridViewNight.Columns[1].Width = 40;
            this.dataGridViewNight.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewNight.Columns[2].Name = "Plan100-Tar";
            this.dataGridViewNight.Columns[2].Width = 60;
            this.dataGridViewNight.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewNight.Columns[3].Name = "Acc100-Tar";
            this.dataGridViewNight.Columns[3].Width = 70;
            this.dataGridViewNight.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < 7; i++)
            {
                this.dataGridViewNight.Columns[4 + i * 4].Name = "";
                this.dataGridViewNight.Columns[4 + i * 4].Width = 5;
                this.dataGridViewNight.Columns[4 + i * 4].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewNight.Columns[5 + i * 4].Name = "Act";
                this.dataGridViewNight.Columns[5 + i * 4].Width = 30;
                this.dataGridViewNight.Columns[5 + i * 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridViewNight.Columns[6 + i * 4].Name = "Acc";
                this.dataGridViewNight.Columns[6 + i * 4].Width = 40;
                this.dataGridViewNight.Columns[6 + i * 4].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewNight.Columns[6 + i * 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridViewNight.Columns[7 + i * 4].Name = "%OA";
                this.dataGridViewNight.Columns[7 + i * 4].Width = 40;
                this.dataGridViewNight.Columns[7 + i * 4].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewNight.Columns[7 + i * 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            }
            this.dataGridViewNight.RowHeadersWidth = 10;
            this.dataGridViewNight.DefaultCellStyle.Font = new Font("Tahoma", 8);
            this.dataGridViewNight.RowTemplate.Height = 30;
            dataGridViewNight.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewNight.AllowUserToResizeRows = false;
            dataGridViewNight.AllowUserToResizeColumns = false;

            this.dataGridViewN.ColumnCount = 10;
            this.dataGridViewD.ColumnCount = 10;

        }


        #endregion


        #region History Ppas Pattern Between Month
        private void HistoryPpasBetweenMonth(DateTime dt)
        {
            string[] OADavg = new string[10];
            string[] OANavg = new string[10];
            var d = new DayAndWeekClass();
            PpasStartEndDate registdate = d.FindStartEndDate(dt);

            for (int i = 0; i < 7; i++)
            {
                textList2[i].Text = registdate.StartDate.AddDays(i).ToString("d-MM-yyyy");
            }

            using (var db = new ProductionEntities11())
            {
                // Data
                var ppas7day = db.Prod_PPASTable
                    .Where(r => r.registDate >= registdate.StartDate && r.registDate <= registdate.EndDate)
                    .Where(s => s.sectionCode == User.SectionCode).ToList();

                for (int day = 0; day < 7; day++)
                {
                    DateTime startDate = registdate.StartDate.AddDays(day);
                    var onePpasExist = ppas7day.Where(r => r.registDate == startDate);
                    if (onePpasExist.Any())
                    {
                        var onePpasExistd = onePpasExist.Where(a => a.dayNight == "D");
                        if (onePpasExistd.Any())
                        {
                            var onePpas = onePpasExistd.OrderBy(h => h.hour).ToList();
                            Display(dataGridViewDay, dataGridViewD, startDate, onePpas);
                            //if (day == 6)
                            //{
                            //    string jsonString11 = JsonSerializer.Serialize(onePpas);

                            //    Console.WriteLine(jsonString11);
                            //}
                        }
                        var onePpasExistn = onePpasExist.Where(a => a.dayNight == "N");
                        if (onePpasExistn.Any())
                        {
                            var onePpas = onePpasExistn.OrderBy(h => h.hour).ToList();
                            Display(dataGridViewNight, dataGridViewN, startDate, onePpas);
                        }

                    }

                }

                var worker = db.Emp_ManPowerRegistedTable
                    .Where(r => r.registDate >= registdate.StartDate && r.registDate <= registdate.EndDate)
                    .Where(s => s.sectionCode == User.SectionCode)
                    .GroupBy(s => new { s.registDate, s.DayNight, s.shiftAB })
                    .Select(n => new WorkerShift
                    {
                        RegistDate = n.Key.registDate,
                        DayNight = n.Key.DayNight,
                        ShiftAB = n.Key.shiftAB,
                    })
                    .ToList();

                int dayDiff = (int)(registdate.EndDate - registdate.StartDate).TotalDays + 1;

                //var standardDate = new List<WorkerShift>();
                for (int i = 0; i < dayDiff; i++)
                {
                    DateTime RegistDate = registdate.StartDate.AddDays(i);
                    var data1 = worker.Where(r => r.RegistDate == RegistDate && r.DayNight == "D")?.FirstOrDefault();
                    workerday[i] = data1 != null ? data1.ShiftAB : "";

                    var data2 = worker.Where(r => r.RegistDate == RegistDate && r.DayNight == "N")?.FirstOrDefault();
                    workernight[i] = data2 != null ? data2.ShiftAB : "";
                }

                //string jsonString1 = JsonSerializer.Serialize(workerday);

                //Console.WriteLine(jsonString1);
                // Frame 

                var frameDb = db.Prod_PPASTable.Where(r => r.registDate == registdate.StartDate)
                    .Where(s => s.sectionCode == User.SectionCode).ToList();
                var frameDbExistd = frameDb.Where(a => a.dayNight == "D");
                if (frameDbExistd.Any())
                {
                    var onePpas = frameDbExistd.OrderBy(h => h.hour).ToList();
                    Frame(dataGridViewDay, dataGridViewD, onePpas);
                }
                var frameDbExistn = frameDb.Where(a => a.dayNight == "N");
                if (frameDbExistn.Any())
                {
                    var onePpas = frameDbExistn.OrderBy(h => h.hour).ToList();
                    Frame(dataGridViewNight, dataGridViewN, onePpas);
                }

            }
            dataGridViewDay.Rows[10].Cells[3].Value = "Total";
            dataGridViewNight.Rows[10].Cells[3].Value = "Total";
            for (int i = 0; i < 7; i++)
            {
                dataGridViewDay.Rows[10].Cells[5 + i * 4].Value = workerday[i];
                dataGridViewDay.Rows[10].Cells[6 + i * 4].Value = dataGridViewDay.Rows[9].Cells[6 + i * 4].Value;
                dataGridViewNight.Rows[10].Cells[5 + i * 4].Value = workernight[i];
                dataGridViewNight.Rows[10].Cells[6 + i * 4].Value = dataGridViewNight.Rows[9].Cells[6 + i * 4].Value;
            }




        }

        private void Frame(DataGridView dgv, DataGridView dgvr, List<Prod_PPASTable> values)
        {
            int j = 0;
            double stdoa = 0;
            foreach (Prod_PPASTable item in values)
            {
                dgv.Rows[j].Cells[0].Value = item.monitor;

                dgv.Rows[j].Cells[1].Value = item.period;

                double period = item.period ?? 0;
                double stdOA = item.stdOA ?? 0;
                stdoa = stdOA;
                double plan100 = item.plan100 ?? 0;
                double plan85 = plan100 * stdOA / 100;
                double acc100 = item.accPlan ?? 0;
                double acc85 = acc100 * stdOA / 100;
                string buf = $"{plan100:0} - {plan85:0}";

                dgv.Rows[j].Cells[2].Value = buf;
                buf = ($"{acc100:0} - {acc85:0}");
                dgv.Rows[j].Cells[3].Value = buf;

                dgvr.Rows[j].Cells[0].Value = item.monitor;
                double Red100 = plan100 * 3600 / period;
                dgvr.Rows[j].Cells[1].Value = Red100.ToString("0");
                double Red85 = plan85 * 3600 / period;
                dgvr.Rows[j].Cells[2].Value = Red85.ToString("0");
                j++;
            }
            lbOA.Text = String.Format($"OA Target = {stdoa} %");
        }

        private void Display(DataGridView dgv, DataGridView dgvr, DateTime day, List<Prod_PPASTable> values)
        {
            int DayInWeek = (int)day.DayOfWeek;
            int startcolumn = 1 + DayInWeek * 4;
            DataGridViewCellStyle styleNormal = new DataGridViewCellStyle
            {
                ForeColor = Color.Black
            };
            DataGridViewCellStyle styleAbnormal = new DataGridViewCellStyle
            {
                ForeColor = Color.Red
            };
            int j = 0;
            foreach (Prod_PPASTable item in values)
            {

                dgv.Rows[j].Cells[startcolumn].Value = item.volume;
                dgv.Rows[j].Cells[startcolumn + 1].Value = item.accVol;
                dgv.Rows[j].Cells[startcolumn + 2].Value = item.percentOA;

                dgv.Rows[10].Cells[2 + startcolumn].Value = item.percentOAavg;

                dgvr.Rows[j].Cells[2 + DayInWeek].Value = item.volumePerHr;

                if (item.redAlarm == "A" && item.workStatus == "W")
                {
                    dgv.Rows[j].Cells[startcolumn].Style = styleAbnormal;
                }
                else
                {
                    dgv.Rows[j].Cells[startcolumn].Style = styleNormal;
                }

                j++;
            }
        }

        private void InitialHistory()
        {
            dataGridViewDay.Rows.Clear();
            dataGridViewNight.Rows.Clear();
            dataGridViewD.Rows.Clear();
            dataGridViewN.Rows.Clear();
            for (int h = 0; h < 11; h++)
            {
                string[] row = new string[31];
                dataGridViewDay.Rows.Add(row);
                dataGridViewNight.Rows.Add(row);
            }

            for (int h = 0; h < 11; h++)
            {
                string[] row = new string[10];
                dataGridViewD.Rows.Add(row);
                dataGridViewN.Rows.Add(row);

            }
            chartDay.Series.Clear();
            chartNight.Series.Clear();
            for (int i = 0; i < 7; i++)
            {
                textList2[i].Text = "";
                textList2[i].Visible = true;
                lbList2[i].Visible = true;
            }

        }

        #endregion

        #region History Ppas Pattern In Month
        private void HistoryPpasOneMonth(DateTime dt)
        {
            string[] OADavg = new string[10];
            string[] OANavg = new string[10];
            dt = new DateTime(dt.Year, dt.Month, dt.Day);
            var d = new DayAndWeekClass();
            PpasDisplayMember startday = d.FindStartingDateInWeek(dt);
            DataGridViewCellStyle styleNormal = new DataGridViewCellStyle
            {
                ForeColor = Color.Black
            };
            DataGridViewCellStyle styleAbnormal = new DataGridViewCellStyle
            {
                ForeColor = Color.Red
            };


            DateTime firstdate = startday.StartingDate;
            int DayInWeek = startday.DayInWeek;
            int NumberOfDate = startday.NumberOfday;
            DateTime lastdate = firstdate.AddDays(NumberOfDate - 1);

            using (var db = new ProductionEntities11())
            {

                var dataPpasAll = db.Prod_PPASTable.Where(s => s.sectionCode == User.SectionCode)
                    .Where(x => x.registDate >= firstdate & x.registDate <= lastdate).ToList();

                for (int i = 0; i < NumberOfDate; i++)
                {
                    DateTime rundate = firstdate.AddDays(i);
                    var dataOneDay = dataPpasAll.Where(x => x.registDate == rundate)
                        .Where(n => n.dayNight == "D").OrderBy(a => a.hour).ToList();

                    int startcolumn = 1 + (DayInWeek + i) * 4;

                    // for Day shift
                    if (dataOneDay.Count > 0)
                    {
                        int j = 0;
                        foreach (var item in dataOneDay)
                        {
                            dataGridViewDay.Rows[j].Cells[startcolumn].Value = item.volume;
                            dataGridViewDay.Rows[j].Cells[startcolumn + 1].Value = item.accVol;
                            dataGridViewDay.Rows[j].Cells[startcolumn + 2].Value = item.percentOA;

                            dataGridViewD.Rows[j].Cells[2 + DayInWeek + i].Value = item.volumePerHr;

                            if (item.workStatus == "W")
                            {
                                OADavg[DayInWeek + i] = item.percentOAavg.ToString();
                            }
                            // Mark red charactor at Actual volumn
                            if (item.redAlarm == "A" && item.workStatus == "W")
                            {
                                dataGridViewDay.Rows[j].Cells[startcolumn].Style = styleAbnormal;
                            }
                            else
                            {
                                dataGridViewDay.Rows[j].Cells[startcolumn].Style = styleNormal;
                            }
                            j++;
                        }
                    }
                    // for Night shift
                    var dataOneNight = dataPpasAll.Where(x => x.registDate == rundate)
                        .Where(n => n.dayNight == "N").OrderBy(a => a.hour).ToList();

                    if (dataOneNight.Count > 0)
                    {
                        int j = 0;
                        foreach (var item in dataOneNight)
                        {
                            dataGridViewNight.Rows[j].Cells[startcolumn].Value = item.volume;
                            dataGridViewNight.Rows[j].Cells[startcolumn + 1].Value = item.accVol;
                            dataGridViewNight.Rows[j].Cells[startcolumn + 2].Value = item.percentOA;
                            dataGridViewN.Rows[j].Cells[2 + DayInWeek + i].Value = item.volumePerHr;

                            if (item.workStatus == "W")
                            {
                                OANavg[DayInWeek + i] = item.percentOAavg.ToString();
                            }
                            if (item.redAlarm == "A" && item.workStatus == "W")
                            {
                                dataGridViewNight.Rows[j].Cells[startcolumn].Style = styleAbnormal;
                            }
                            else
                            {
                                dataGridViewNight.Rows[j].Cells[startcolumn].Style = styleNormal;
                            }
                            j++;
                        }
                    }


                }

                // Frame 
                var dataFrameDay = dataPpasAll.Where(x => x.registDate == firstdate)
                       .Where(n => n.dayNight == "D").OrderBy(a => a.hour).ToList();
                // Frame Day
                if (dataFrameDay.Count > 0)
                {
                    int j = 0;
                    foreach (var item in dataFrameDay)
                    {

                        dataGridViewDay.Rows[j].Cells[0].Value = item.monitor;
                        dataGridViewDay.Rows[j].Cells[1].Value = item.period;
                        double period = Convert.ToDouble(item.period);
                        double stdOA = Convert.ToDouble(item.stdOA);
                        double plan100 = Convert.ToDouble(item.plan100);
                        double plan85 = plan100 * stdOA / 100;
                        double acc100 = Convert.ToDouble(item.accPlan);
                        double acc85 = acc100 * stdOA / 100;
                        string buf = $"{plan100:0} - {plan85:0}";
                        dataGridViewDay.Rows[j].Cells[2].Value = buf;
                        buf = ($"{acc100:0} - {acc85:0}");
                        dataGridViewDay.Rows[j].Cells[3].Value = buf;
                        dataGridViewD.Rows[j].Cells[0].Value = item.monitor;
                        double Red100 = plan100 * 3600 / period;
                        dataGridViewD.Rows[j].Cells[1].Value = Red100.ToString("0");
                        double Red85 = plan85 * 3600 / period;
                        dataGridViewD.Rows[j].Cells[2].Value = Red85.ToString("0");
                        j++;
                    }
                }
                // Frame Night
                var dataFrameNight = dataPpasAll.Where(x => x.registDate == firstdate)
                      .Where(n => n.dayNight == "N").OrderBy(a => a.hour).ToList();
                if (dataFrameNight.Count > 0)
                {
                    int j = 0;
                    foreach (var item in dataFrameNight)
                    {
                        dataGridViewNight.Rows[j].Cells[0].Value = item.monitor;
                        dataGridViewNight.Rows[j].Cells[1].Value = item.period;
                        double period = Convert.ToDouble(item.period);
                        double stdOA = Convert.ToDouble(item.stdOA);
                        double plan100 = Convert.ToDouble(item.plan100);
                        double plan85 = plan100 * stdOA / 100;
                        double acc100 = Convert.ToDouble(item.accPlan);
                        double acc85 = acc100 * stdOA / 100;
                        string buf = $"{plan100:0} - {plan85:0}";
                        dataGridViewNight.Rows[j].Cells[2].Value = buf;
                        buf = $"{acc100:0} - {acc85:0}";
                        dataGridViewNight.Rows[j].Cells[3].Value = buf;
                        dataGridViewN.Rows[j].Cells[0].Value = item.monitor;
                        double Red100 = plan100 * 3600 / period;
                        dataGridViewN.Rows[j].Cells[1].Value = Red100.ToString("0");
                        double Red85 = plan85 * 3600 / period;
                        dataGridViewN.Rows[j].Cells[2].Value = Red85.ToString("0");
                        lbOA.Text = string.Format("OA Target = {0} %", stdOA);
                        j++;
                    }
                }

                for (int i = 0; i < 7; i++)
                {
                    dataGridViewDay.Rows[10].Cells[3 + i * 4].Value = OADavg[i];
                    dataGridViewNight.Rows[10].Cells[3 + i * 4].Value = OANavg[i];
                }


                for (int i = 0; i < 7; i++)
                {
                    textList2[i].Text = "";
                    textList2[i].Visible = false;
                    lbList2[i].Visible = false;
                }

                for (int i = 0; i < NumberOfDate; i++)
                {
                    textList2[DayInWeek - 1 + i].Text = firstdate.AddDays(i).ToString("d-MM-yyyy");
                    textList2[DayInWeek - 1 + i].Visible = true;
                    lbList2[DayInWeek - 1 + i].Visible = true;
                }



                var worker = db.Emp_ManPowerRegistedTable
                    .Where(r => r.registDate >= firstdate && r.registDate <= lastdate)
                    .Where(s => s.sectionCode == User.SectionCode)
                    .GroupBy(s => new { s.registDate, s.DayNight, s.shiftAB })
                    .Select(n => new WorkerShift
                    {
                        RegistDate = n.Key.registDate,
                        DayNight = n.Key.DayNight,
                        ShiftAB = n.Key.shiftAB,
                    })
                    .ToList();

                int dayDiff = (int)(lastdate - firstdate).TotalDays + 1;

                for (int i = 0; i < dayDiff; i++)
                {
                    DateTime RegistDate = firstdate.AddDays(i);
                    var data1 = worker.Where(r => r.RegistDate == RegistDate && r.DayNight == "D")?.FirstOrDefault();
                    workerday[i] = data1 != null ? data1.ShiftAB : "";

                    var data2 = worker.Where(r => r.RegistDate == RegistDate && r.DayNight == "N")?.FirstOrDefault();
                    workernight[i] = data2 != null ? data2.ShiftAB : "";
                }

                dataGridViewDay.Rows[10].Cells[3].Value = "Total";
                dataGridViewNight.Rows[10].Cells[3].Value = "Total";
                for (int i = 0; i < NumberOfDate; i++)
                {
                    int startcolumn = 1 + (DayInWeek + i) * 4;
                    dataGridViewDay.Rows[10].Cells[startcolumn].Value = workerday[i];
                    dataGridViewDay.Rows[10].Cells[startcolumn + 1].Value = dataGridViewDay.Rows[9].Cells[startcolumn + 1].Value;
                    dataGridViewNight.Rows[10].Cells[startcolumn].Value = workernight[i];
                    dataGridViewNight.Rows[10].Cells[startcolumn + 1].Value = dataGridViewNight.Rows[9].Cells[startcolumn + 1].Value;

                }


            }

        }



        #endregion

        private void ExportToMasterFile()
        {

            string[] date = new string[7];
            string startdate = null;
            string enddate = null;

            for (int i = 0; i < 7; i++)
            {
                date[i] = textList2[i].Text;

            }
            for (int i = 0; i < 7; i++)
            {
                if (date[i] != "")
                {
                    startdate = date[i];
                    break;
                }

            }
            for (int i = 6; i >= 0; i--)
            {
                if (date[i] != "")
                {
                    enddate = date[i];
                    break;
                }

            }

            ExportExcel exp = new ExportExcel();
            try
            {

                string file1 = @"C:\KPi\Source\PpasMaster.xlsx";
                if (File.Exists(file1))
                {
                    DataGridViewToExcelResult result = exp.FileNamePpas("PPAS", User.SectionCode, $"{startdate}-{enddate}");

                    if (result.Status)
                    {
                        if (exp.ExportToMasterFile(file1, date, dataGridViewDay, dataGridViewNight))
                        {
                            File.Copy(file1, result.FileName, true);
                            MessageBox.Show("Export data to Excel file completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            MessageBox.Show(new Form() { TopMost = true }, "The Master File is open ," +
                                "OR NOT Found WorkSheet \"bluePrint\" \n Please prepara C:\\KPi\\Source\\PpasMaster.xlsx before Export file again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }

                }
                else
                {
                    MessageBox.Show(new Form() { TopMost = true }, "NOT Found !!! \n C:\\KPi\\Source\\PpasMaster.xlsx", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(new Form() { TopMost = true }, "Error code = 1x100 , Message : " + ex.ToString() + " (LOOP can not found C:\\KPi\\Source\\PpasMaster.xlsx)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void BtnExport_Click_1(object sender, EventArgs e)
        {
            ExportToMasterFile();
        }

        private void RadBetweenMonth_CheckedChanged(object sender, EventArgs e)
        {
            Refresh3(DateTime.Now);
        }

        private void RadInMonth_CheckedChanged(object sender, EventArgs e)
        {
            Refresh3(DateTime.Now);
        }

        private void RadHisBM_CheckedChanged(object sender, EventArgs e)
        {
            HistoryPpasUpdate();
        }

        private void RadHisIM_CheckedChanged(object sender, EventArgs e)
        {
            HistoryPpasUpdate();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            HistoryPpasUpdate();
        }
    }
}
