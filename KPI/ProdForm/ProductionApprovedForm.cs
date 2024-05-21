using KPI.Class;
using KPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KPI.ProdForm
{
    public partial class ProductionApprovedForm : Form
    {

        internal bool WindowsPopup = false;
        internal int Month;
        List<ProdWorkToday> work = new List<ProdWorkToday>();

        public ProductionApprovedForm()
        {
            InitializeComponent();
        }

        #region Event #
        private void ProductionApprovedForm_Load(object sender, EventArgs e)
        {
            InitialDataGridView(Dtp_Select.Value);
            CmbDayNight.SelectedIndex = 0;
            CmbShift.SelectedIndex = 0;
            UpdateView();
            Roles();

        }

        private void BtnLL_Click(object sender, EventArgs e)
        {
            LLCollection();
        }

        private void BtnTL_Click(object sender, EventArgs e)
        {
            TLApproved();
            UpdateStatus();
            UpdateView();

        }

        private void BtnAM_Click(object sender, EventArgs e)
        {
            AMApproved();
            UpdateStatus();
            UpdateView();
        }

        private void Dtp_Select_ValueChanged(object sender, EventArgs e)
        {
            if (Month != Dtp_Select.Value.Month)
            {
                InitialDataGridView(Dtp_Select.Value);
                UpdateView();
                Month = Dtp_Select.Value.Month;
            }
        }

        private void ProductionCollectionFormClosed_Close(object sender, EventArgs e)
        {
            WindowsPopup = false;
            UpdateView();
            ButtonEnable(true);
            UpdateStatus();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }

        private void BtnUnRegist_Click(object sender, EventArgs e)
        {
            UnRegisterDataCollect();


        }

        #endregion

        //================  Main Function ===============//

        #region Main Function #
        private void Roles()
        {
            BtnLL.Visible = false;
            BtnTL.Visible = false;
            BtnAM.Visible = false;

            //int test = (int)User.Role;

            //int test2 = (int)Models.Roles.Prod_LineLeader;

            if (User.Role == Models.Roles.Invalid) // invalid
            {

            }
            else if (User.Role == Models.Roles.General) // General
            {

            }

            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {

            }
            else if (User.Role == Models.Roles.Admin_Min) // Admin-mini
            {

            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnLL.Visible = true;
                BtnTL.Visible = true;
                BtnAM.Visible = true;
            }
            else if (User.Role == Models.Roles.Prod_LineLeader) // Admin-Full
            {
                BtnLL.Visible = true;

            }
            else if (User.Role == Models.Roles.Prod_TeamLeader) // Admin-Full
            {
                BtnLL.Visible = true;
                BtnTL.Visible = true;
            }
            else if (User.Role == Models.Roles.Prod_Manager) // Admin-Full
            {
                BtnLL.Visible = true;
                BtnTL.Visible = true;
                BtnAM.Visible = true;
            }
        }

        private void InitialDataGridView(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int dayInmonth = DateTime.DaysInMonth(year, month);
            int column = dayInmonth + 2;
            string[] header = new string[column];
            int[] width = new int[column];
            header[0] = "No";
            header[1] = "Part Number";
            width[0] = 30;
            width[1] = 100;
            for (int i = 0; i < dayInmonth; i++)
            {
                header[i + 2] = $"{i + 1}";
                width[i + 2] = 45;
            }

            DataGridViewSetup.Norm2(DgvListDay, header, width);
            DataGridViewSetup.Norm2(DgvListNight, header, width);

        }

        private void UnRegisterDataCollect()
        {
            DateTime selectedtime = Dtp_Select.Value;
            int month = selectedtime.Month;
            int year = selectedtime.Year;
            int day = selectedtime.Day;

            int dayInMonth = DateTime.DaysInMonth(year, month);

            DateTime registStart = new DateTime(year, month, 1);
            DateTime registEnd = registStart.AddDays(dayInMonth - 1);

            DateTime registDate = new DateTime(year, month, day);


            int Id = Convert.ToInt32(User.ID);
            using (var context = new ProductionEntities11())
            {
                if (User.Role == Models.Roles.Prod_LineLeader)
                {
                    var checkLLRegist = context.App_ProductionToday.Where(x => x.registDate == registDate);
                    if (checkLLRegist == null)
                    {
                        return;
                    }

                    var appTL = checkLLRegist.FirstOrDefault(x => x.AppTL != null);
                    if (appTL == null)
                    {
                        // go delete
                        DeleteDataInApp_ProductionToday(registDate, CmbShift.SelectedIndex);
                        UpdateView();
                        UpdateStatus();
                    }
                    else
                    {
                        MessageBox.Show("Can not CANCEL Data Approved because your TL already approved \n Please inform your TL to delete", "Info", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }



                }
                else if (User.Role == Models.Roles.Prod_TeamLeader)
                {
                    var checkTLRegist = context.App_ProductionToday.Where(x => x.registDate == registDate);
                    if (checkTLRegist == null)
                    {
                        return;
                    }
                    var appAM = checkTLRegist.FirstOrDefault(x => x.AppAM != null);
                    if (appAM == null)
                    {
                        // go delete
                        DeleteDataInApp_ProductionToday(registDate, CmbShift.SelectedIndex);
                        UpdateView();
                        UpdateStatus();
                    }
                    else
                    {
                        MessageBox.Show("Can not CANCEL Data Approved because your MGR already approved \n Please inform your MGR to delete", "Info", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }



                }
                else if (User.Role == Models.Roles.Prod_Manager || User.Role == Models.Roles.Admin_Full)
                {

                    var checkAMRegist = context.App_ProductionToday.Where(x => x.registDate == registDate);
                    if (checkAMRegist == null)
                    {
                        return;
                    }
                    // go delete
                    DeleteDataInApp_ProductionToday(registDate, CmbShift.SelectedIndex);
                    UpdateView();
                    UpdateStatus();

                }

            }



        }

        private void DeleteDataInApp_ProductionToday(DateTime date, int shift)
        {
            int id = Convert.ToInt32(User.ID);
            using (var context = new ProductionEntities11())
            {
                try
                {
                    string shiftStr = "B";
                    if (shift == 0)
                    {
                        var removeBoth = context.App_ProductionToday
                                        .Where(r => r.registDate == date)
                                        .Where(s => s.sectionCode == User.SectionCode);
                        if (removeBoth != null)
                            context.App_ProductionToday.RemoveRange(removeBoth);

                        var reCollBoth = context.Prod_ProductionToday
                                        .Where(r => r.registDate == date)
                                        .Where(s => s.sectionCode == User.SectionCode);
                        if (reCollBoth != null)
                            context.Prod_ProductionToday.RemoveRange(reCollBoth);

                        if ((removeBoth != null) || (reCollBoth != null))
                            context.SaveChanges();

                        shiftStr = "B";


                        //var removePg_StdMH = context.Pg_STDMH
                        //                    .Where(r => r.registDate == date)
                        //                .Where(s => s.sectionCode == User.SectionCode);
                        //if (removePg_StdMH != null)
                        //    context.Pg_STDMH.RemoveRange(removePg_StdMH);

                    }
                    else if (shift == 1)
                    {
                        var removeA = context.App_ProductionToday
                                    .Where(r => r.registDate == date)
                                    .Where(s => s.sectionCode == User.SectionCode)
                                    .Where(s => s.workShift == "A");
                        if (removeA != null)
                            context.App_ProductionToday.RemoveRange(removeA);

                        var reCollA = context.Prod_ProductionToday
                                        .Where(r => r.registDate == date)
                                        .Where(s => s.sectionCode == User.SectionCode)
                                         .Where(s => s.workShift == "A");
                        if (reCollA != null)
                            context.Prod_ProductionToday.RemoveRange(reCollA);

                        if ((removeA != null) || (reCollA != null))
                            context.SaveChanges();

                   

                        //var removePg_StdMH = context.Pg_STDMH.Where(s => s.workShift == "A")
                        //                    .Where(r => r.registDate == date)
                        //                .Where(s => s.sectionCode == User.SectionCode);
                        //if (removePg_StdMH != null)
                        //    context.Pg_STDMH.RemoveRange(removePg_StdMH);



                        shiftStr = "A";
                    }
                    else if (shift == 2)
                    {
                        var removeB = context.App_ProductionToday
                                    .Where(r => r.registDate == date)
                                    .Where(s => s.sectionCode == User.SectionCode)
                                    .Where(s => s.workShift == "B");
                        if (removeB != null)
                            context.App_ProductionToday.RemoveRange(removeB);

                        var reCollB = context.Prod_ProductionToday
                                       .Where(r => r.registDate == date)
                                       .Where(s => s.sectionCode == User.SectionCode)
                                        .Where(s => s.workShift == "B");
                        if (reCollB != null)
                            context.Prod_ProductionToday.RemoveRange(reCollB);

                        if ((removeB != null) || (reCollB != null))
                            context.SaveChanges();


                        //var removePg_StdMH = context.Pg_STDMH.Where(s => s.workShift == "B")
                        //                   .Where(r => r.registDate == date)
                        //               .Where(s => s.sectionCode == User.SectionCode);
                        //if (removePg_StdMH != null)
                        //    context.Pg_STDMH.RemoveRange(removePg_StdMH);



                        shiftStr = "B";
                    }
                    PresetPg_STDMH(User.SectionCode, date, shift);

                    var newlog = new App_ProductionTodayLog()
                    {
                        sectionCode = User.SectionCode,
                        registDate = date,
                        workshift = shiftStr,
                        unRegistUserId = id,
                        remark = "",
                    };
                    context.App_ProductionTodayLog.Add(newlog);

                    context.SaveChanges();
                    MessageBox.Show("Already delete data", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please try it again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }

        }



        private bool PresetPg_STDMH(string section, DateTime date, int workShift)
        {
            try
            {

                using (var db = new ProductionEntities11())
                {
                    if (workShift == 1)
                    {
                        var exist = db.Pg_STDMH.Where(s => s.sectionCode == section)
                            .Where(r => r.registDate == date).SingleOrDefault(w => w.workShift == "A");
                        if (exist != null)
                        {
                            var newvalue = new Pg_STDMH()
                            {
                                sectionCode = section,
                                registDate = date,
                                workShift = "A",
                                STD_MH = 0,
                                actucalQty = 0,
                            };
                            db.Entry(exist).CurrentValues.SetValues(newvalue);
                        }
                    }
                    else if (workShift == 2)
                    {
                        var exist = db.Pg_STDMH.Where(s => s.sectionCode == section)
                            .Where(r => r.registDate == date).SingleOrDefault(w => w.workShift == "B");
                        if (exist != null)
                        {
                            var newvalue = new Pg_STDMH()
                            {
                                sectionCode = section,
                                registDate = date,
                                workShift = "B",
                                STD_MH = 0,
                                actucalQty = 0,
                            };
                            db.Entry(exist).CurrentValues.SetValues(newvalue);
                        }
                    }
                    else if (workShift == 0)
                    {
                        var existA = db.Pg_STDMH.Where(s => s.sectionCode == section)
                           .Where(r => r.registDate == date).SingleOrDefault(w => w.workShift == "A");
                        if (existA != null)
                        {
                            var newvalue = new Pg_STDMH()
                            {
                                sectionCode = section,
                                registDate = date,
                                workShift = "A",
                                STD_MH = 0,
                                actucalQty = 0,
                            };
                            db.Entry(existA).CurrentValues.SetValues(newvalue);
                        }
                        var existB = db.Pg_STDMH.Where(s => s.sectionCode == section)
                                               .Where(r => r.registDate == date).SingleOrDefault(w => w.workShift == "B");
                        if (existA != null)
                        {
                            var newvalue = new Pg_STDMH()
                            {
                                sectionCode = section,
                                registDate = date,
                                workShift = "B",
                                STD_MH = 0,
                                actucalQty = 0,
                            };
                            db.Entry(existB).CurrentValues.SetValues(newvalue);
                        }

                    }
                    db.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        private void LLCollection()
        {
            if (WindowsPopup == true)
                return;

            DateTime dateStart = Dtp_Select.Value;
            string dayNight = CmbDayNight.SelectedIndex == 0 ? "D" : "N";



            if (!RegistDateTime.DataCollectionPromis(dateStart, dayNight))
            {
                MessageBox.Show("Can not register production volume at the time during production", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ButtonEnable(false);
            DateTime starttime, endtime;
            int year = dateStart.Year;
            int month = dateStart.Month;
            int day = dateStart.Day;
            DateTime registDate = new DateTime(year, month, day);
            if (dayNight == "D")
            {
                starttime = new DateTime(year, month, day, 7, 30, 00);
                endtime = new DateTime(year, month, day, 19, 29, 59);
            }
            else
            {
                starttime = new DateTime(year, month, day, 19, 30, 00);
                endtime = new DateTime(year, month, day, 7, 29, 59);
                endtime = endtime.AddDays(1);

            }
            using (var db = new ProductionEntities11())
            {
                List<ProductionOnShift> prod = db.Prod_RecordTable.Where(r => r.registDateTime >= starttime && r.registDateTime <= endtime)
                    .Where(s => s.sectionCode == User.SectionCode).GroupBy(p => p.partNumber)
                    .Select(p => new ProductionOnShift
                    {
                        Partnumber = p.Key,
                        Qty = p.Count()
                    }).ToList();

                string shift;
                if (CmbShift.SelectedIndex == 1)
                    shift = "A";
                else if (CmbShift.SelectedIndex == 2)
                    shift = "B";
                else
                {
                    var ext = db.Prod_TodayWorkTable
                        .Where(d => d.dayNight == dayNight)
                        .Where(s => s.sectionCode == User.SectionCode)
                        .Where(r => r.registDate == registDate);
                    if (ext.Any())
                    {
                        shift = ext.FirstOrDefault().workShift;
                    }
                    else
                    {
                        MessageBox.Show("You have NOT YET registed Man-Power", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ButtonEnable(true);
                        return;
                    }

                }

                var frm = new ProductionCollectionForm(dateStart, dayNight, shift, prod);
                WindowsPopup = true;
                frm.ProductionCollectionFormClosed += new EventHandler(ProductionCollectionFormClosed_Close);
                frm.TopLevel = true;
                frm.TopMost = true;
                frm.Focus();
                frm.Show();
            }



        }

        private void UpdateView()
        {
            Color[] color = {Color.FromArgb(255,127,127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223,255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen,
                Color.FromArgb(255, 127, 127), Color.FromArgb(191, 255, 191), Color.FromArgb(149, 223, 255), Color.FromArgb(255, 252, 170), Color.FromArgb(238, 170, 255), Color.FromArgb(255, 202, 127), Color.Wheat, Color.Orange, Color.GreenYellow, Color.Magenta,
                    Color.LightCoral, Color.Tomato, Color.DeepPink, Color.Yellow, Color.Lime, Color.DarkOrange, Color.Maroon, Color.Salmon, Color.Aqua, Color.Firebrick, Color.Tan, Color.HotPink, Color.YellowGreen};

            DateTime selectedtime = Dtp_Select.Value;
            int month = selectedtime.Month;
            int year = selectedtime.Year;
            int dayInMonth = DateTime.DaysInMonth(year, month);

            DateTime registStart = new DateTime(year, month, 1);
            DateTime registEnd = registStart.AddDays(dayInMonth - 1);

            using (var db = new ProductionEntities11())
            {
                var raw = db.Prod_ProductionToday
                    .Where(r => r.registDate >= registStart && r.registDate <= registEnd)
                    .Where(s => s.sectionCode == User.SectionCode)
                    .ToList();

                var productionByDay = ProductionToday.ChartProductionByDay(raw, registStart);

                var prod_today = db.Prod_TodayWorkTable.Where(r => r.registDate >= registStart && r.registDate <= registEnd)
                    .Where(s => s.sectionCode == User.SectionCode && s.workHour > 0);

                var app_prod = db.App_ProductionToday.Where(r => r.registDate >= registStart && r.registDate <= registEnd)
                    .Where(s => s.sectionCode == User.SectionCode);


                var appProd = from t in prod_today
                              join a in app_prod
                                     on new { t.registDate, t.workShift } equals new { a.registDate, a.workShift } into ps
                              from a in ps.DefaultIfEmpty()
                              select new ProdWorkToday
                              {
                                  Registdate = t.registDate,
                                  WorkShift = t.workShift,
                                  LL = a.AppLL,
                                  TL = a.AppTL,
                                  AM = a.AppAM
                              };


                //var appProd = from t in db.Prod_TodayWorkTable
                //              where t.registDate >= registStart && t.registDate <= registEnd && t.sectionCode == User.SectionCode && t.workHour > 0
                //              join a in db.App_ProductionToday
                //                     on new { t.registDate, t.workShift } equals new { a.registDate, a.workShift } into ps
                //              from a in ps.DefaultIfEmpty()
                //              select new ProdWorkToday
                //              {
                //                  Registdate = t.registDate,
                //                  WorkShift = t.workShift,
                //                  LL = a.AppLL,
                //                  TL = a.AppTL,
                //                  AM = a.AppAM
                //              };



                ProductionToday.TableHiglight(appProd, LLstatus, BtnLL, TLstatus, BtnTL, AMstatus, BtnAM);


                work = appProd.ToList();

                //var str = new StringBuilder();
                //foreach (ProdWorkToday item in work.OrderBy(r=>r.Registdate))
                //{
                //    str.AppendFormat($"{item.Registdate}, {item.WorkShift}, {item.LL}, {item.TL}, {item.AM} \n");
                //}
                //Console.WriteLine(str.ToString());

                ProductionToday.ProductionByShift(DgvListDay, raw, work, registStart, "A");
                ProductionToday.ProductionByShift(DgvListNight, raw, work, registStart, "B");

                int count = raw.GroupBy(p => p.partNumber).Count();
                var partlist = raw.GroupBy(p => p.partNumber).Select(x => new PartNumber { Partnumber = x.Key }).ToList();
                Charts.ChartNewProductionDay(selectedtime, count, productionByDay, ChartTotal, color);
                ChartLegent.Legend_NewListMultiColor1(tpanelChartPartNumber, 1, "tpanelChartPartNumber", "lbcharSixtLoss", color, partlist);







            }






        }

        private void UpdateStatus()
        {

            DateTime selectedtime = Dtp_Select.Value;
            int month = selectedtime.Month;
            int year = selectedtime.Year;
            int dayInMonth = DateTime.DaysInMonth(year, month);

            DateTime registStart = new DateTime(year, month, 1);
            DateTime registEnd = registStart.AddDays(dayInMonth - 1);
            using (var db = new ProductionEntities11())
            {
                var appProd = from t in db.Prod_TodayWorkTable
                              where t.registDate >= registStart && t.registDate <= registEnd && t.sectionCode == User.SectionCode && t.workHour > 0

                              join a in db.App_ProductionToday
                                     on new { t.registDate, t.workShift } equals new { a.registDate, a.workShift } into ps
                              from a in ps.DefaultIfEmpty()
                              select new ProdWorkToday
                              {
                                  Registdate = t.registDate,
                                  WorkShift = t.workShift,
                                  LL = a.AppLL,
                                  TL = a.AppTL,
                                  AM = a.AppAM
                              };

                ProductionToday.TableHiglight(appProd, LLstatus, BtnLL, TLstatus, BtnTL, AMstatus, BtnAM);
            }

        }

        private void TLApproved()
        {
            if (WindowsPopup == false)
            {
                DateTime selectedtime = Dtp_Select.Value;
                int month = selectedtime.Month;
                int year = selectedtime.Year;
                int dayInMonth = DateTime.DaysInMonth(year, month);

                DateTime registStart = new DateTime(year, month, 1);
                DateTime registEnd = registStart.AddDays(dayInMonth - 1);

                using (var db = new ProductionEntities11())
                {
                    var ext = db.App_ProductionToday.Where(s => s.sectionCode == User.SectionCode)
                         .Where(g => g.registDate >= registStart && g.registDate <= registEnd);


                    if (ext.Any())
                    {
                        var tlApp = ext.Where(a => a.AppTL == null).ToList();
                        var countLLa = ext.Where(w => w.workShift == "A").Count();
                        var countLLb = ext.Where(w => w.workShift == "B").Count();
                        var countWorka = work.Where(w => w.WorkShift == "A").Count();
                        var countWorkb = work.Where(w => w.WorkShift == "B").Count();
                        if ((countLLa == countWorka) && (countLLb == countWorkb))  //(!tlApp.Any())
                        {
                            MessageBox.Show("มันถูก approved ไปหมดแล้ว ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }


                        DialogResult r = MessageBox.Show("Are you sure to TL approve ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DialogResult.Yes == r)
                        {

                            DateTime appDate = DateTime.Now;
                            foreach (App_ProductionToday a in tlApp)
                            {
                                var update = new App_ProductionToday()
                                {
                                    sectionCode = a.sectionCode,
                                    registDate = a.registDate,
                                    workShift = a.workShift,
                                    AppLL = a.AppLL,
                                    AppLLdate = a.AppLLdate,
                                    AppTL = Convert.ToInt32(User.ID),
                                    AppTLdate = appDate
                                };
                                db.Entry(a).CurrentValues.SetValues(update);
                            }
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        MessageBox.Show("LL ยังไม่ได้ register production today ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                }

            }
        }

        private void ButtonEnable(bool status)
        {
            BtnLL.Enabled = BtnTL.Enabled = BtnAM.Enabled = status;
        }

        private void AMApproved()
        {
            if (WindowsPopup == false)
            {
                DateTime selectedtime = Dtp_Select.Value;
                int month = selectedtime.Month;
                int year = selectedtime.Year;
                int dayInMonth = DateTime.DaysInMonth(year, month);

                DateTime registStart = new DateTime(year, month, 1);
                DateTime registEnd = registStart.AddDays(dayInMonth - 1);

                using (var db = new ProductionEntities11())
                {
                    var ext = db.App_ProductionToday.Where(s => s.sectionCode == User.SectionCode)
                         .Where(g => g.registDate >= registStart && g.registDate <= registEnd);


                    if (ext.Any())
                    {
                        var tlApp = ext.Where(a => a.AppAM == null);
                        if (!tlApp.Any())
                        {
                            MessageBox.Show("มันถูก approved ไปหมดแล้ว ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }


                        DialogResult r = MessageBox.Show("Are you sure to AM/MGR approve ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DialogResult.Yes == r)
                        {

                            DateTime appDate = DateTime.Now;
                            foreach (App_ProductionToday a in tlApp)
                            {
                                var update = new App_ProductionToday()
                                {
                                    sectionCode = a.sectionCode,
                                    registDate = a.registDate,
                                    workShift = a.workShift,
                                    AppLL = a.AppLL,
                                    AppLLdate = a.AppLLdate,
                                    AppTL = a.AppTL,
                                    AppTLdate = a.AppTLdate,
                                    AppAM = Convert.ToInt32(User.ID),
                                    AppAMdate = appDate,

                                };
                                db.Entry(a).CurrentValues.SetValues(update);
                            }
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        MessageBox.Show("LL ยังไม่ได้ register production today ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                }

            }
        }

        private void ExportExcel()
        {
            DateTime selectedtime = Dtp_Select.Value;
            int month = selectedtime.Month;
            int year = selectedtime.Year;
            int dayInMonth = DateTime.DaysInMonth(year, month);

            DateTime registStart = new DateTime(year, month, 1);
            DateTime registEnd = registStart.AddDays(dayInMonth - 1);

            var raw = new List<Prod_ProductionToday>();
            using (var db = new ProductionEntities11())
            {
                var ext = db.Prod_ProductionToday.Where(s => s.sectionCode == User.SectionCode)
                     .Where(g => g.registDate >= registStart && g.registDate <= registEnd);
                if (ext.Any())
                {
                    raw = ext.ToList();
                }
                else
                {
                    MessageBox.Show($"NO data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }


            string yearmonth = $"{year}{month:00}";
            ExportExcel exp = new ExportExcel();
            DataGridViewToExcelResult result = exp.FileName1("ProductToDay_", User.SectionCode, yearmonth);
            if (result.Status)
            {

                if (exp.ProductionTodayExcel(raw, result.FileName))
                {
                    MessageBox.Show($"Data was written on {result.FileName}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"CAN NOT write on {result.FileName} \n  Please try it again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion


    }
}
