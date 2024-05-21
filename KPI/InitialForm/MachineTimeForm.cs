using KPI.Class;
using KPI.Models;
using KPI.Parameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KPI.InitialForm
{
    public partial class MachineTimeForm : Form
    {

        readonly List<string> stationList = new List<string>();
        readonly Dictionary<string, string> stationDic = new Dictionary<string, string>();

        readonly List<string> MCList = new List<string>();
        readonly Dictionary<string, string> MCNameDict = new Dictionary<string, string>();
        readonly Dictionary<int, int> MCDict = new Dictionary<int, int>();

        List<MachineTime> MachinTimeList = new List<MachineTime>();

        public MachineTimeForm()
        {
            InitializeComponent();
        }

        private void MachineTimeForm_Load(object sender, EventArgs e)
        {
            cmbSection.DataSource = new BindingSource(Dict.SectionCodeName, null);
            cmbSection.DisplayMember = "Value";
            cmbSection.ValueMember = "Key";
            int i = 0;
            foreach (KeyValuePair<string, string> item in Dict.SectionCodeName)
            {
                if (item.Key == User.SectionCode)
                {
                    cmbSection.SelectedIndex = i;
                    break;
                }
                i += 1;
            }
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.StationGroupReadSQL(User.SectionCode);
            if (sqlstatus)
            {
                stationList.Add("-");
                stationDic.Add("0", "-");
                int k = 1;
                foreach (DataRow dr in sql.Datatable.Rows)
                {
                    stationList.Add(dr.ItemArray[0].ToString());
                    stationDic.Add(k.ToString(), dr.ItemArray[0].ToString());
                    k++;
                }
            }
            InitDgv1(dataGridViewMC);



            //Roles();
        }

        private void BtnMCList_Click(object sender, EventArgs e)
        {
            MachineNameRead1();
        }



        private void MachineNameRead1()
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            MCList.Clear();
            MCDict.Clear();
            MCNameDict.Clear();
            using (var db = new ProductionEntities11())
            {
                var existmc = db.machineLists.Where(s => s.id_section == section);
                if (existmc.Any())
                {
                    var mclist = existmc.ToList();
                    int i = 1;
                    foreach (var dr in mclist)
                    {
                        MCList.Add(dr.id_machine);
                        //  MCDict.Add((i - 1), Convert.ToInt32(dr.ItemArray[0]));
                        MCNameDict.Add(dr.id_machine, dr.machine_name);
                        i++;
                    }
                    cmbMachineName.DataSource = new BindingSource(MCNameDict, null);
                }

                var oeemcExist = db.oeeMachineTimes.Where(s => s.section_id == section);
                if (oeemcExist.Any())
                {
                    MachinTimeList = oeemcExist
                       .Select(x => new MachineTime()
                       {
                           MachineID = x.machine_id,
                           PartNumber = x.part_number,
                           MTminSec = x.mt_min_sec,
                           HTminSec = x.ht_min_sec
                       }).ToList();
                    LoadMTAndHTmin();

                }

            }



        }



        private void MachineNameRead()
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            MCList.Clear();
            MCDict.Clear();
            MCNameDict.Clear();
            var sql = new SqlClass();
            bool sqlstatus = sql.MC_MahineTimeNameReadSQL(section);
            if (sqlstatus)
            {
                DataSet ds = sql.Dataset;
                DataTable dt1 = ds.Tables[0];
                DataTable dtmctime = ds.Tables[1];
                int i = 1;
                foreach (DataRow dr in dt1.Rows)
                {
                    MCList.Add(dr.ItemArray[1].ToString());
                    MCDict.Add((i - 1), Convert.ToInt32(dr.ItemArray[0]));
                    MCNameDict.Add(dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString());
                    i++;
                }
                cmbMachineName.DataSource = new BindingSource(MCNameDict, null);

                MachinTimeList.Clear();
                foreach (DataRow dr in dtmctime.Rows)
                {
                    var m = new MachineTime()
                    {
                        MachineID = dr.ItemArray[0].ToString(),
                        PartNumber = dr.ItemArray[1].ToString(),
                        MTminSec = Convert.ToDouble(dr.ItemArray[2]),
                        HTminSec = Convert.ToDouble(dr.ItemArray[3]),
                    };
                    MachinTimeList.Add(m);
                }
                LoadMTAndHTmin();

            }

        }

        private void MachineTimeRead1()
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
           
            var sql = new SqlClass();
            bool sqlstatus = sql.MC_MahineTimeReadSQL(section);
            if (sqlstatus)
            {
                DataTable dtmctime = sql.Datatable;
                MachinTimeList.Clear();
                foreach (DataRow dr in dtmctime.Rows)
                {
                    var m = new MachineTime()
                    {
                        MachineID = dr.ItemArray[0].ToString(),
                        PartNumber = dr.ItemArray[1].ToString(),
                        MTminSec = Convert.ToDouble(dr.ItemArray[2]),
                        HTminSec = Convert.ToDouble(dr.ItemArray[3]),
                    };
                    MachinTimeList.Add(m);
                }
                LoadMTAndHTmin();
            }

        }






        private void BtnPartFromNetTime_Click(object sender, EventArgs e)
        {
            NetTimeRead();
        }

        private void NetTimeRead()
        {
            string Section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            string year = DateTime.Now.ToString("yyyy");
            dataGridViewMC.Rows.Clear();
            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.LoadPartNumberSQL(Section, year);
            if (sqlstatus)
            {
                DataTable dt1 = sql.Datatable;
                int i = 1;
                foreach (DataRow dr in dt1.Rows)
                {
                    dataGridViewMC.Rows.Add(i.ToString(), dr.ItemArray[0], 0, 0);

                    i++;
                }


            }

        }


        private void InitDgv1(DataGridView Dgv)
        {
            string[] header = new string[] { "No", "Part number", "Machine Time min (sec)", "Hand Time min (sec)" };

            int[] width = new int[] { 50, 150, 100, 100 };
            int col = header.Length;
            Dgv.ColumnCount = col;

            for (int i = 0; i < col; i++)
            {
                Dgv.Columns[i].Name = header[i];
                Dgv.Columns[i].Width = width[i];
                Dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            Dgv.RowHeadersWidth = 4;
            Dgv.DefaultCellStyle.Font = new Font("Tahoma", 9);
            Dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            Dgv.RowTemplate.Height = 30;
            Dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Dgv.AllowUserToResizeRows = false;
            Dgv.AllowUserToResizeColumns = false;
        }

        private void cmbMachineName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMTAndHTmin();
        }

        private void LoadMTAndHTmin()
        {
            string machineID = ((KeyValuePair<string, string>)cmbMachineName.SelectedItem).Key;
            dataGridViewMC.Rows.Clear();
            List<MachineTime> dbmachine = MachinTimeList.Where(m => m.MachineID == machineID).ToList();
            int i = 1;
            foreach (var db in dbmachine)
            {
                dataGridViewMC.Rows.Add(i++, db.PartNumber, db.MTminSec, db.HTminSec);
            }
        }



        public class MachineTime
        {
            public string MachineID { get; set; }
            public string PartNumber { get; set; }
            public double  MTminSec { get; set; }
            public double HTminSec { get; set; }
        }

        private void BtnMTSet_Click(object sender, EventArgs e)
        {
            int row = dataGridViewMC.Rows.Count-1;
            double mttime;
            bool sucess = double.TryParse(TbMTTime.Text, out mttime);
            if (sucess)
            {
                for (int i = 0; i < row; i++)
                {
                    dataGridViewMC.Rows[i].Cells[2].Value = TbMTTime.Text;
                }
            }
        }

        private void BtnHTSet_Click(object sender, EventArgs e)
        {
            int row = dataGridViewMC.Rows.Count - 1;
            double httime;
            bool sucess = double.TryParse(TbHTTime.Text, out httime);
            if (sucess)
            {
                for (int i = 0; i < row; i++)
                {
                    dataGridViewMC.Rows[i].Cells[3].Value = TbHTTime.Text;
                }
            }
        }

        private void BtnAllSave_Click(object sender, EventArgs e)
        {
            string section = ((KeyValuePair<string, string>)cmbSection.SelectedItem).Key;
            string machineID = ((KeyValuePair<string, string>)cmbMachineName.SelectedItem).Key;
            var sql = new SqlClass();
            bool status = sql.MachineTimeSaveSQL(section, machineID, dataGridViewMC);
            if (status)
            {
                MachineTimeRead1();
                MessageBox.Show("Save machine Time completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failure machine Time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TbMTTime_Click(object sender, EventArgs e)
        {
            TbMTTime.Clear();
        }

        private void TbHTTime_Click(object sender, EventArgs e)
        {
            TbHTTime.Clear();
        }
    }
}
