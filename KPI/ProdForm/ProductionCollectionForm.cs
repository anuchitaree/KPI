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
    public partial class ProductionCollectionForm : Form
    {
        private readonly DateTime registDate;
        private readonly string dayNight;
        private readonly string shift;
        private readonly List<ProductionOnShift> collect;
        private object log;
        public event EventHandler ProductionCollectionFormClosed;
        public ProductionCollectionForm(DateTime registDate, string dayNight, string shift, List<ProductionOnShift> collect)
        {
            InitializeComponent();
            this.registDate = registDate;
            this.dayNight = dayNight;
            this.shift = shift;
            this.collect = collect;
        }

        public ProductionCollectionForm()
        {

        }

        private void ProductionCollectionForm_Load(object sender, EventArgs e)
        {
            string[] header = new string[] { "No", "Partnumber", "Q'TY" };
            int[] width = new int[] { 50, 250, 100 };
            DataGridViewSetup.Norm1(DgvCollect, header, width);
            int count = 1;
            int total = 0;
            DgvCollect.Rows.Clear();
            foreach (ProductionOnShift item in collect)
            {
                var obj = new object[]
                {
                    count,
                    item.Partnumber,
                    item.Qty
                };
                DgvCollect.Rows.Add(obj);
                count++;
                total += item.Qty;
            }
            label1.Text = String.Format("Date : {0:yyyy-MM-dd} ,SHIFT {1}  ,Total =  {2:#,##0.##} pieces", registDate, shift, total);
        }

        private void ProductionCollectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ProductionCollectionFormClosed?.Invoke(this, EventArgs.Empty);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.ProductionCollectionFormClosed?.Invoke(this, EventArgs.Empty);
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (!CheckDataBeforeSave() || DgvCollect.Rows.Count == 0)
            {
                Lberr.Text = "Data in the table is NOT completed";
                return;
            }



            using (var db = new ProductionEntities11())
            {
                DateTime regist = new DateTime(registDate.Year, registDate.Month, registDate.Day, 0, 0, 0);
                int dayInMonth = DateTime.DaysInMonth(registDate.Year, registDate.Month);
                DateTime enddate = registDate.AddDays(dayInMonth - 1);


                bool ext = db.Prod_ProductionToday.Where(r => r.registDate == regist).Where(d => d.dayNight == dayNight)
                    .Where(s => s.sectionCode == User.SectionCode).Any();
                if (ext)
                {
                    Lberr.Text = $"Product { regist: yyyy - MM - dd} has ever registed ";
                    return;
                }
                DateTime registtime = DateTime.Now;
                var dataColl = new List<Prod_ProductionToday>();
                int rowcount = DgvCollect.Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    int qty = Convert.ToInt32(DgvCollect.Rows[i].Cells[2].Value);
                    if (qty == 0)
                        continue;
                    var coll = new Prod_ProductionToday()
                    {
                        sectionCode = User.SectionCode,
                        registDate = regist,
                        dayNight = dayNight,
                        workShift = shift,
                        partNumber = DgvCollect.Rows[i].Cells[1].Value.ToString(),
                        Qty = qty,
                    };
                    dataColl.Add(coll);
                }
                var app = new App_ProductionToday()
                {
                    sectionCode = User.SectionCode,
                    registDate = regist,
                    workShift = shift,
                    AppLL = Convert.ToInt32(User.ID),
                    AppLLdate = registtime,
                };

                /// Calculate MH (second) 
                /// 

                var quality = new List<PartnumberQTY>();
                foreach (var item in dataColl)
                {
                    var newcoll = new PartnumberQTY()
                    {
                        Partnumber = item.partNumber,
                        Qty = Convert.ToDouble(item.Qty)
                    };
                    quality.Add(newcoll);
                }


                var netTime = db.Prod_NetTimeTable.Where(s => s.sectionCode == User.SectionCode)
                    .Where(n => n.registYear == registDate.Year.ToString());



                var checkModelInNetTimeTable = from p in quality
                                               join n in netTime
                                               on p.Partnumber equals n.partNumber into allColumn
                                               from entry in allColumn
                                               select entry;

                int netcount = checkModelInNetTimeTable.Count();
                int qualitycount = quality.Count();
                if(netcount!= qualitycount)
                {
                    for (int i = 0; i < rowcount; i++)
                    {
                        int qty = Convert.ToInt32(DgvCollect.Rows[i].Cells[2].Value);
                        if (qty == 0)
                            continue;
                        string pn = DgvCollect.Rows[i].Cells[1].Value.ToString();

                        var chk = checkModelInNetTimeTable.Where(p => p.partNumber == pn).Any();
                        if (chk == false)
                        {
                            ProductionToday.PartnumberHighlight(DgvCollect, i, 1);
                        }
                    }
                    MessageBox.Show("Part number has not yet REGISTRATION \n, Please regist in Initial NET TIME before", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }




                var initialPg_STDMH = db.Pg_STDMH.Where(r => r.registDate >= regist && r.registDate <= enddate)
                       .Where(s => s.sectionCode == User.SectionCode).Any();
                if (initialPg_STDMH == false)
                {
                    List<Pg_STDMH> init = ProductionToday.InitialProductionToday(regist, User.SectionCode);
                    db.Pg_STDMH.AddRange(init);
                    db.SaveChanges();
                }




                var STDMH1 = from p in quality
                              join n in netTime
                              on p.Partnumber equals n.partNumber into ps from n in ps.DefaultIfEmpty()
                              select new PartnumberQTY
                              {
                                  Partnumber = p.Partnumber,
                                  Qty = p.Qty,
                                  Mh = p.Qty * n.netTime
                              };

                var STDMH = STDMH1
                             .GroupBy(x => 1).Select(c => new Pg_STDMH
                             {
                                 sectionCode = User.SectionCode,
                                 registDate = regist,
                                 workShift = shift,
                                 STD_MH = c.Sum(b => b.Mh),
                                 actucalQty = c.Sum(b => b.Qty),
                             }).FirstOrDefault();

                var existstdMH = db.Pg_STDMH
                    .Where(r => r.registDate == regist)
                    .Where(s => s.sectionCode == User.SectionCode)
                    .Where(s => s.workShift == shift).ToList();


                foreach (Pg_STDMH item in existstdMH)
                {
                    var update = new Pg_STDMH()
                    {
                        sectionCode = User.SectionCode,
                        registDate = regist,
                        workShift = shift,
                        STD_MH = STDMH.STD_MH/3600,
                        actucalQty = STDMH.actucalQty,
                    };
                    db.Entry(item).CurrentValues.SetValues(update);
                }
                db.SaveChanges();




                try
                {

                    db.Prod_ProductionToday.AddRange(dataColl);
                 
                    db.App_ProductionToday.Add(app);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    Lberr.Text = "Save data Error, Do it again";
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Dispose();
            this.ProductionCollectionFormClosed?.Invoke(this, EventArgs.Empty);
        }

        private bool CheckDataBeforeSave()
        {

            return true;
        }

        private void DgvCollect_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int r = DgvCollect.CurrentCell.RowIndex;
            int c = DgvCollect.CurrentCell.ColumnIndex;
            log = DgvCollect.Rows[r].Cells[c].Value;
        }

        private void DgvCollect_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle styleChange = new DataGridViewCellStyle
            {
                ForeColor = Color.Blue
            };
            int r = DgvCollect.CurrentRow.Index;
            int c = DgvCollect.CurrentCell.ColumnIndex;
            string pn = Convert.ToString(DgvCollect.Rows[r].Cells[c].Value);

            if (c == 1)
            {
                DgvCollect.Rows[r].Cells[c].Value = log;
            }
            else if (c == 2)
            {
                bool isnumeric = Converting.IsNumeric(pn);  //double.TryParse(pn, out double n);
                if (isnumeric == false)
                {
                    Lberr.Text = "Please fill only numberical";
                    DgvCollect.Rows[r].Cells[c].Value = log;
                    return;
                }
                else if (isnumeric == true)
                {
                    styleChange.ForeColor = Color.Red;
                    DgvCollect.Rows[r].Cells[c].Style = styleChange;
                    string pn1 = Convert.ToString(DgvCollect.Rows[r].Cells[1].Value);
                    Lberr.Text += $"{pn1}  Change {log}  to  {pn}  \n";
                }
            }
        }
    }
}
