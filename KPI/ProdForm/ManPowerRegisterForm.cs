using KPI.Class;
using KPI.Models;
using KPI.Parameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KPI.ProdForm
{
    public partial class ManPowerRegisterForm : Form
    {
        readonly List<string> absentMPList = new List<string>();
        readonly List<string> decreseMPList = new List<string>();
        readonly List<string> EmpList = new List<string>();
        private object log;
        readonly Dictionary<string, string> empforLineADic = new Dictionary<string, string>();
        readonly Dictionary<string, string> empforLineBDic = new Dictionary<string, string>();

        //private double summaryOTa = 0;
        //private double summaryOTb = 0;
        private int rowMpRegistCurrentA;
        private int rowMpRegistCurrentB;
        private bool reportOpen = false;
        //readonly string SectionDivPlant = string.Format("{0}{1}{2}", User.SectionCode, User.Division, User.Plant);

        public ManPowerRegisterForm()
        {
            InitializeComponent();
            InitialMPRegister();
            InitialOperatorInSection();
        }

        private void ManPowerRegisterForm_Load(object sender, EventArgs e)
        {

            CmbSection.SelectedIndexChanged -= CmbSection_SelectedIndexChanged;

            LoadComboBox();
            LoadEventComponent1();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                LoadEventComponent1();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                LoadEventComponent2();
            }
        }


        #region Event TAB 1



        private void BtnMPOutA_Click(object sender, EventArgs e)
        {
            ShiftA_Decreae();
        }

        private void BtnMPOutB_Click(object sender, EventArgs e)
        {
            ShiftB_Decreae();
        }

        private void BtnMPIn_Click(object sender, EventArgs e)
        {
            ShiftA_B_DecreaseCancle();
        }

        private void BtnAbsentA_Click(object sender, EventArgs e)
        {
            MPAbsentA();
        }

        private void BtnAbsentB_Click(object sender, EventArgs e)
        {
            MPAbsentB();
        }

        private void BtnAbsentCancel_Click(object sender, EventArgs e)
        {
            MPAbsentCancel();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            NewMPRegister();

        }

        private void BtnUpdatetMP_Click(object sender, EventArgs e)
        {
            UpdateScreen();
        }

        private void BtnSaveMP_Click(object sender, EventArgs e)
        {
            SaveDailyRegister();
        }

        private void BtnCheckMP_Click(object sender, EventArgs e)
        {
            if (reportOpen == false)
            {
                UpdateReport();
                reportOpen = true;
            }

        }



        private void BtnOTA_Click(object sender, EventArgs e)
        {
            OTinputByClick_A();
        }

        private void BtnOTB_Click(object sender, EventArgs e)
        {
            OTinputByClick_B();
        }

        private void TbAdd1a_TextChanged(object sender, EventArgs e)
        {
            AddEmpIDByDirectInput_A();
        }

        private void TbAdd1b_TextChanged(object sender, EventArgs e)
        {
            AddEmpIDByDirectInput_B();
        }


        private void CmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbNight.SelectedIndex == 0)
            {
                CmbDay.SelectedIndex = 1;
            }
            else
            {
                CmbDay.SelectedIndex = 0;
            }
        }

        private void CmbNight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbDay.SelectedIndex == 0)
            {
                CmbNight.SelectedIndex = 1;
            }
            else
            {
                CmbNight.SelectedIndex = 0;
            }
        }


        #endregion

        #region  =====  OPERATION LOOP TAB 1  ========



        #region Initial DataGridView

        private void InitialMPRegister()
        {
            DgvShiftAInitial();
            DgvShiftBInitial();
            DgvAbsentInitial();
            DgvDecreaseInitial();
            DgvIncreaseInitial();
        }

        private void DgvShiftAInitial()
        {
            this.DgvShiftA.ColumnCount = 9;
            this.DgvShiftA.Columns[0].Name = "S/F";
            this.DgvShiftA.Columns[0].Width = 20;
            this.DgvShiftA.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftA.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftA.Columns[1].Name = "No";
            this.DgvShiftA.Columns[1].Width = 30;
            this.DgvShiftA.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftA.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftA.Columns[2].Name = "ID";
            this.DgvShiftA.Columns[2].Width = 70;
            this.DgvShiftA.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftA.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvShiftA.Columns[3].Name = "Full Name";
            this.DgvShiftA.Columns[3].Width = 150;
            this.DgvShiftA.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftA.Columns[4].Name = "Rate";
            this.DgvShiftA.Columns[4].Width = 40;
            this.DgvShiftA.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftA.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftA.Columns[5].Name = "Period";
            this.DgvShiftA.Columns[5].Width = 30;
            this.DgvShiftA.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftA.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftA.Columns[6].Name = "OtRate";
            this.DgvShiftA.Columns[6].Width = 40;
            this.DgvShiftA.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftA.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftA.Columns[7].Name = "OT";
            this.DgvShiftA.Columns[7].Width = 30;
            this.DgvShiftA.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftA.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftA.Columns[8].Name = "OTAccum";
            this.DgvShiftA.Columns[8].Width = 50;
            this.DgvShiftA.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftA.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftA.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvShiftA.RowHeadersWidth = 4;
            this.DgvShiftA.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvShiftA.RowTemplate.Height = 25;
            DgvShiftA.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DgvShiftA.AllowUserToResizeRows = false;
            DgvShiftA.AllowUserToResizeColumns = false;
            //DataSource = new BindingSource(ListValue.EmpFunction, null),
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Func",
                Name = "Responsibility",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 4
            };
            comboBoxColumn.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvShiftA.Columns.Add(comboBoxColumn);
            DataGridViewComboBoxColumn comboBoxColumn1 = new DataGridViewComboBoxColumn
            {
                HeaderText = "OTFunc",
                Name = "ResponsibilityForOT",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 7
            };
            comboBoxColumn1.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn1.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvShiftA.Columns.Add(comboBoxColumn1);
        }

        private void DgvShiftBInitial()
        {
            this.DgvShiftB.ColumnCount = 9;
            this.DgvShiftB.Columns[0].Name = "S/F";
            this.DgvShiftB.Columns[0].Width = 20;
            this.DgvShiftB.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftB.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvShiftB.Columns[1].Name = "No";
            this.DgvShiftB.Columns[1].Width = 30;
            this.DgvShiftB.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftB.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvShiftB.Columns[2].Name = "ID";
            this.DgvShiftB.Columns[2].Width = 70;
            this.DgvShiftB.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftB.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvShiftB.Columns[3].Name = "Full Name";
            this.DgvShiftB.Columns[3].Width = 150;
            this.DgvShiftB.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftB.Columns[4].Name = "Rate";
            this.DgvShiftB.Columns[4].Width = 40;
            this.DgvShiftB.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftB.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftB.Columns[5].Name = "Period";
            this.DgvShiftB.Columns[5].Width = 30;
            this.DgvShiftB.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftB.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftB.Columns[6].Name = "OtRate";
            this.DgvShiftB.Columns[6].Width = 40;
            this.DgvShiftB.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftB.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftB.Columns[7].Name = "OT";
            this.DgvShiftB.Columns[7].Width = 30;
            this.DgvShiftB.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftB.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftB.Columns[8].Name = "OTAccum";
            this.DgvShiftB.Columns[8].Width = 50;
            this.DgvShiftB.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvShiftB.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvShiftB.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvShiftB.RowHeadersWidth = 4;
            this.DgvShiftB.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvShiftB.RowTemplate.Height = 25;
            DgvShiftB.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DgvShiftB.AllowUserToResizeRows = false;
            DgvShiftB.AllowUserToResizeColumns = false;

            //DataSource = new BindingSource(ListValue.EmpFunction, null),
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Func",
                Name = "Responsibility",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 4
            };
            comboBoxColumn.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvShiftB.Columns.Add(comboBoxColumn);
            DataGridViewComboBoxColumn comboBoxColumn1 = new DataGridViewComboBoxColumn
            {
                HeaderText = "OTFunc",
                Name = "ResponsibilityForOT",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 7
            };
            comboBoxColumn1.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn1.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvShiftB.Columns.Add(comboBoxColumn1);
        }

        private void DgvDecreaseInitial()
        {
            // Decreaeing
            this.DgvDecrease.ColumnCount = 8 + 1;
            this.DgvDecrease.Columns[0].Name = "S/F";
            this.DgvDecrease.Columns[0].Width = 20;
            this.DgvDecrease.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDecrease.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvDecrease.Columns[1].Name = "No";
            this.DgvDecrease.Columns[1].Width = 25;
            this.DgvDecrease.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDecrease.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvDecrease.Columns[2].Name = "ID";
            this.DgvDecrease.Columns[2].Width = 60;
            this.DgvDecrease.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDecrease.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvDecrease.Columns[3].Name = "Full Name";
            this.DgvDecrease.Columns[3].Width = 130;
            this.DgvDecrease.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.DgvDecrease.Columns[4].Name = "Rate";
            this.DgvDecrease.Columns[4].Width = 40;
            this.DgvDecrease.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDecrease.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvDecrease.Columns[5].Name = "Period";
            this.DgvDecrease.Columns[5].Width = 40;
            this.DgvDecrease.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDecrease.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvDecrease.Columns[6].Name = "OTRate";
            this.DgvDecrease.Columns[6].Width = 40;
            this.DgvDecrease.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDecrease.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvDecrease.Columns[7].Name = "OT";
            this.DgvDecrease.Columns[7].Width = 30;
            this.DgvDecrease.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDecrease.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvDecrease.Columns[8].Name = "DayNight";
            this.DgvDecrease.Columns[8].Width = 40;
            this.DgvDecrease.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvDecrease.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.DgvDecrease.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvDecrease.RowHeadersWidth = 4;
            this.DgvDecrease.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvDecrease.RowTemplate.Height = 25;
            DgvDecrease.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DgvDecrease.AllowUserToResizeRows = false;
            DgvDecrease.AllowUserToResizeColumns = false;
            DataGridViewComboBoxColumn comboBoxColumn3 = new DataGridViewComboBoxColumn
            {
                HeaderText = "Func",
                Name = "Responsibility",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 4
            };
            comboBoxColumn3.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn3.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvDecrease.Columns.Add(comboBoxColumn3);
            DataGridViewComboBoxColumn comboBoxColumn31 = new DataGridViewComboBoxColumn
            {
                HeaderText = "OTFunc",
                Name = "ResponsibilityForOT",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 7
            };
            comboBoxColumn31.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn31.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvDecrease.Columns.Add(comboBoxColumn31);

            //DataSource = new BindingSource(ListValue.SectionCodeName, null),
            DataGridViewComboBoxColumn comboBoxColumn32 = new DataGridViewComboBoxColumn
            {
                HeaderText = "To Section",
                Name = "ToSection", //"IncreaseSection";
                DataSource = ListValue.SectionCodeName,
                DropDownWidth = 10,
                Width = 150,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 10
            };
            comboBoxColumn32.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn32.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvDecrease.Columns.Add(comboBoxColumn32);
        }

        private void DgvAbsentInitial()
        {
            this.DgvAbsent.ColumnCount = 9 + 1;
            this.DgvAbsent.Columns[0].Name = "S/F";
            this.DgvAbsent.Columns[0].Width = 20;
            this.DgvAbsent.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvAbsent.Columns[1].Name = "No";
            this.DgvAbsent.Columns[1].Width = 30;
            this.DgvAbsent.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvAbsent.Columns[2].Name = "ID";
            this.DgvAbsent.Columns[2].Width = 70;
            this.DgvAbsent.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvAbsent.Columns[3].Name = "Full Name";
            this.DgvAbsent.Columns[3].Width = 150;
            this.DgvAbsent.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[4].Name = "Rate";
            this.DgvAbsent.Columns[4].Width = 40;
            this.DgvAbsent.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvAbsent.Columns[5].Name = "Period";
            this.DgvAbsent.Columns[5].Width = 40;
            this.DgvAbsent.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvAbsent.Columns[6].Name = "OtRate";
            this.DgvAbsent.Columns[6].Width = 50;
            this.DgvAbsent.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvAbsent.Columns[7].Name = "OT";
            this.DgvAbsent.Columns[7].Width = 30;
            this.DgvAbsent.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvAbsent.Columns[8].Name = "OTAccum";
            this.DgvAbsent.Columns[8].Width = 50;
            this.DgvAbsent.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.DgvAbsent.Columns[9].Name = "DayNight";
            this.DgvAbsent.Columns[9].Width = 40;
            this.DgvAbsent.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvAbsent.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            this.DgvAbsent.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvAbsent.RowHeadersWidth = 4;
            this.DgvAbsent.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvAbsent.RowTemplate.Height = 25;

            DgvAbsent.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DgvAbsent.AllowUserToResizeRows = false;
            DgvAbsent.AllowUserToResizeColumns = false;
            DataGridViewComboBoxColumn comboBoxColumn21 = new DataGridViewComboBoxColumn
            {
                HeaderText = "Func",
                Name = "Responsibility",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 4
            };
            comboBoxColumn21.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn21.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvAbsent.Columns.Add(comboBoxColumn21);
            DataGridViewComboBoxColumn comboBoxColumn22 = new DataGridViewComboBoxColumn
            {
                HeaderText = "OTFunc",
                Name = "ResponsibilityForOT",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 7
            };
            comboBoxColumn22.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn22.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvAbsent.Columns.Add(comboBoxColumn22);
        }

        private void DgvIncreaseInitial()
        {
            //--- Increaseing 
            this.DgvIncrease.ColumnCount = 8 + 1;
            this.DgvIncrease.Columns[0].Name = "S/F";
            this.DgvIncrease.Columns[0].Width = 20;
            this.DgvIncrease.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvIncrease.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvIncrease.Columns[1].Name = "No";
            this.DgvIncrease.Columns[1].Width = 30;
            this.DgvIncrease.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvIncrease.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvIncrease.Columns[2].Name = "ID";
            this.DgvIncrease.Columns[2].Width = 70;
            this.DgvIncrease.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvIncrease.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvIncrease.Columns[3].Name = "Full Name";
            this.DgvIncrease.Columns[3].Width = 150;
            this.DgvIncrease.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvIncrease.Columns[4].Name = "Rate";
            this.DgvIncrease.Columns[4].Width = 40;
            this.DgvIncrease.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvIncrease.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvIncrease.Columns[5].Name = "Period";
            this.DgvIncrease.Columns[5].Width = 30;
            this.DgvIncrease.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvIncrease.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvIncrease.Columns[6].Name = "OTRate";
            this.DgvIncrease.Columns[6].Width = 40;
            this.DgvIncrease.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvIncrease.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvIncrease.Columns[7].Name = "OT";
            this.DgvIncrease.Columns[7].Width = 30;
            this.DgvIncrease.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvIncrease.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.DgvIncrease.Columns[8].Name = "DayNight";
            this.DgvIncrease.Columns[8].Width = 40;
            this.DgvIncrease.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DgvIncrease.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            this.DgvIncrease.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvIncrease.RowHeadersWidth = 4;
            this.DgvIncrease.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.DgvIncrease.RowTemplate.Height = 25;
            DgvIncrease.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DgvIncrease.AllowUserToResizeRows = false;
            DgvIncrease.AllowUserToResizeColumns = false;
            DataGridViewComboBoxColumn comboBoxColumn4 = new DataGridViewComboBoxColumn
            {
                HeaderText = "Func",
                Name = "Responsibility",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 4
            };
            comboBoxColumn4.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn4.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvIncrease.Columns.Add(comboBoxColumn4);
            DataGridViewComboBoxColumn comboBoxColumn41 = new DataGridViewComboBoxColumn
            {
                HeaderText = "OTFunc",
                Name = "ResponsibilityForOT",
                DataSource = ListValue.EmpFunction,
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 7
            };
            comboBoxColumn41.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn41.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvIncrease.Columns.Add(comboBoxColumn41);
            DataGridViewComboBoxColumn comboBoxColumn42 = new DataGridViewComboBoxColumn
            {
                HeaderText = "From Section",
                Name = "DecreasefromSection",
                DataSource = ListValue.SectionCodeName,
                DropDownWidth = 10,
                Width = 150,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 10
            };
            comboBoxColumn42.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn42.DefaultCellStyle.Font = new Font("Tahoma", 9);
            DgvIncrease.Columns.Add(comboBoxColumn42);

        }
        #endregion

        #region Prevention editor text in dataGridView
        private void EnableGridViewEvent()
        {
            DgvShiftA.CellClick += DataGridViewShiftA_CellClick;
            DgvShiftA.CellValueChanged += DataGridViewShiftA_CellValueChanged;
            DgvShiftB.CellClick += DataGridViewShiftB_CellClick;
            DgvShiftB.CellValueChanged += DataGridViewShiftB_CellValueChanged;

            DgvDecrease.CellClick += dataGridView3_CellClick;
            DgvDecrease.CellValueChanged += dataGridView3_CellValueChanged;
            DgvAbsent.CellClick += dataGridView2_CellClick;
            DgvAbsent.CellValueChanged += dataGridView2_CellValueChanged;
            DgvIncrease.CellClick += dataGridView4_CellClick;
            DgvIncrease.CellValueChanged += dataGridView4_CellValueChanged;
        }

        private void DisableGridViewEvent()
        {
            DgvShiftA.CellClick -= DataGridViewShiftA_CellClick;
            DgvShiftA.CellValueChanged -= DataGridViewShiftA_CellValueChanged;
            DgvShiftB.CellClick -= DataGridViewShiftB_CellClick;
            DgvShiftB.CellValueChanged -= DataGridViewShiftB_CellValueChanged;


            DgvDecrease.CellClick -= dataGridView3_CellClick;
            DgvDecrease.CellValueChanged -= dataGridView3_CellValueChanged;
            DgvAbsent.CellClick -= dataGridView2_CellClick;
            DgvAbsent.CellValueChanged -= dataGridView2_CellValueChanged;
            DgvIncrease.CellClick -= dataGridView4_CellClick;
            DgvIncrease.CellValueChanged -= dataGridView4_CellValueChanged;
        }

        //private void dataGridView1a_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    MemoryText(DataGridViewShiftA);
        //}

        //private void dataGridView1a_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    PreventEditText(DataGridViewShiftA);
        //}

        //private void dataGridView1b_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    MemoryText(DataGridViewShiftB);
        //}

        //private void dataGridView1b_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    PreventEditText(DataGridViewShiftB);
        //}

        private void DataGridViewShiftA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MemoryText(DgvShiftA);
        }

        private void DataGridViewShiftA_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            PreventEditText(DgvShiftA);
        }

        private void DataGridViewShiftB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MemoryText(DgvShiftB);
        }

        private void DataGridViewShiftB_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            PreventEditText(DgvShiftB);
        }



        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MemoryText(DgvDecrease);
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            PreventEditText(DgvDecrease);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MemoryText(DgvAbsent);
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            PreventEditText(DgvAbsent);
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MemoryText(DgvIncrease);
        }

        private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            PreventEditText(DgvIncrease);
        }




        //private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    MemoryText(DataGridViewDecrease);
        //}

        //private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    PreventEditText(DataGridViewDecrease);
        //}

        //private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    MemoryText(DataGridViewAbsent);
        //}

        //private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    PreventEditText(DataGridViewAbsent);
        //}

        //private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    MemoryText(DataGridViewIncrease);
        //}

        //private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    PreventEditText(DataGridViewIncrease);
        //}









        private void MemoryText(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                int r = dgv.CurrentCell.RowIndex;
                int c = dgv.CurrentCell.ColumnIndex;
                log = dgv.Rows[r].Cells[c].Value;
            }
        }

        private void PreventEditText(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                int r = dgv.CurrentRow.Index;
                int c = dgv.CurrentCell.ColumnIndex;
                string pn = Convert.ToString(dgv.Rows[r].Cells[c].Value);

                if (c <= 3)
                {
                    dgv.Rows[r].Cells[c].Value = log;
                }
                else if (c == 4 || c == 6)
                {
                    bool isnumeric = double.TryParse(pn, out double n);
                    if (isnumeric == false || n > 1)
                    {
                        MessageBox.Show("Please fill only numberical and Rate is not more than 1 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgv.Rows[r].Cells[c].Value = log;
                        return;
                    }
                }
                else if (c == 5)
                {
                    bool isnumeric = double.TryParse(pn, out double n);
                    if (isnumeric == false || n > 8)
                    {
                        MessageBox.Show("Please fill only numberical and WorkingTime is not more than 8 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgv.Rows[r].Cells[c].Value = log;
                        return;
                    }
                }
                else if (c == 7)
                {
                    bool isnumeric = double.TryParse(pn, out double n);
                    if (isnumeric == false)
                    {
                        MessageBox.Show("Please fill only numberical ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgv.Rows[r].Cells[c].Value = log;
                        return;
                    }
                }
            }
        }

        #endregion

        #region Operation Loop in Tab 1

        #region Management in Tab
        private void LoadEventComponent1()
        {
            CmbDay.SelectedIndex = 0;
            CmbNight.SelectedIndex = 1;
            LoadSectionMember();
            UpdateScreen();
            Roles();
            ToolTipBox();

            int i = 0;
            foreach (KeyValuePair<string, string> item in Dict.UserSection)
            {
                if (item.Key == User.SectionCode)
                {
                    CmbSection.SelectedIndex = i;
                    break;
                }
                i += 1;
            }

        }
        private void UpdateScreen()
        {
            absentMPList.Clear();
            decreseMPList.Clear();
            DisableGridViewEvent();
            CurrentMP();
            EnableGridViewEvent();
        }

        private void OTinputByClick_A()
        {
            int row = DgvShiftA.Rows.Count;
            try
            {
                double inputOT = double.Parse(OTaInput.Text);
                inputOT = Math.Round(inputOT, 1, MidpointRounding.AwayFromZero);
                for (int i = 0; i < row; i++)
                {
                    DgvShiftA.Rows[i].Cells[5 + 2].Value = inputOT.ToString();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลขเท่านั้น", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OTinputByClick_B()
        {
            int row = DgvShiftB.Rows.Count;
            try
            {
                double inputOT = double.Parse(OTbInput.Text);
                inputOT = Math.Round(inputOT, 1, MidpointRounding.AwayFromZero);
                for (int i = 0; i < row; i++)
                {
                    DgvShiftB.Rows[i].Cells[5 + 2].Value = inputOT.ToString();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลขเท่านั้น", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddEmpIDByDirectInput_A()
        {
            if (Dict.EmpIDName.ContainsKey(TbAdd1a.Text)) // have in the table
            {
                if (!empforLineADic.ContainsKey(TbAdd1a.Text))
                {
                    int r = DgvShiftA.Rows.Count;
                    string id = TbAdd1a.Text;
                    string fullname = Dict.EmpIDName.FirstOrDefault(x => x.Key == id).Value;

                    DgvShiftA.Rows.Add("A", (r + 1).ToString(), id, fullname, 1, 8, 1, 1);
                    empforLineADic.Add(id, fullname);

                    DgvShiftA.Rows[r].Cells["Responsibility"].Value = (DgvShiftA.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[0];
                    DgvShiftA.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvShiftA.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[0];
                    TbAdd1a.Text = string.Empty;
                }

            }
        }
        private void AddEmpIDByDirectInput_B()
        {
            if (Dict.EmpIDName.ContainsKey(TbAdd1b.Text)) // have in the table
            {
                if (!empforLineBDic.ContainsKey(TbAdd1b.Text))
                {
                    int r = DgvShiftB.Rows.Count;
                    string id = TbAdd1b.Text;
                    string fullname = Dict.EmpIDName.FirstOrDefault(x => x.Key == id).Value;

                    DgvShiftB.Rows.Add("A", (r + 1).ToString(), id, fullname, 1, 8, 1, 1);
                    empforLineBDic.Add(id, fullname);

                    DgvShiftB.Rows[r].Cells["Responsibility"].Value = (DgvShiftB.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[0];
                    DgvShiftB.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvShiftB.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[0];
                    TbAdd1b.Text = string.Empty;
                }

            }
        }


        private void ShiftA_Decreae()
        {
            DisableGridViewEvent();
            if (DgvShiftA.Rows.Count > 0)
            {
                rowMpRegistCurrentA = DgvShiftA.CurrentRow.Index;

                string st = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[2].Value.ToString();
                string name = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[3].Value.ToString();
                string dn = CmbDay.SelectedIndex == 0 ? "D" : "N";
                string rate = "0.0";
                string normal = "0.0";
                string rateOT = "0.0";
                string OT = "0.0";

                if (DgvShiftA.Rows[rowMpRegistCurrentA].Cells[4].Value != null)
                {
                    rate = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[4].Value.ToString();
                }
                if (DgvShiftA.Rows[rowMpRegistCurrentA].Cells[5].Value != null)
                {
                    normal = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[5].Value.ToString();
                }
                if (DgvShiftA.Rows[rowMpRegistCurrentA].Cells[6].Value != null)
                {
                    rateOT = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[6].Value.ToString();
                }
                if (DgvShiftA.Rows[rowMpRegistCurrentA].Cells[7].Value != null)
                {
                    OT = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[7].Value.ToString();
                }

                int dataGridViewCurrentRow = DgvShiftA.CurrentRow.Index;

                if (decreseMPList.Contains(st) == false) // have in the table
                {
                    //if (!decreseMPList.Contains(st))
                    //{
                    int decrow = DgvDecrease.Rows.Count;
                    DgvDecrease.Rows.Add("A", (decrow + 1).ToString(), st, name, rate, normal, rateOT, OT, dn);
                    decreseMPList.Add(st);
                    int r = DgvDecrease.Rows.Count - 1;

                    try
                    {
                        DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)DgvShiftA["Responsibility", dataGridViewCurrentRow];
                        int index = dcc.Items.IndexOf(dcc.Value);
                        DgvDecrease.Rows[r].Cells["Responsibility"].Value = (DgvDecrease.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[index];

                        DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)DgvShiftA["ResponsibilityForOT", dataGridViewCurrentRow];
                        int index1 = dcc1.Items.IndexOf(dcc1.Value);
                        DgvDecrease.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvDecrease.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[index1];
                    }
                    catch (Exception)
                    {
                        DgvDecrease.Rows[r].Cells["Responsibility"].Value = (DgvDecrease.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[0];
                        DgvDecrease.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvDecrease.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[0];
                    }
                    DgvDecrease.Rows[r].Cells["ToSection"].Value = (DgvDecrease.Rows[r].Cells["ToSection"] as DataGridViewComboBoxCell).Items[0];

                    //}

                }
            }
            EnableGridViewEvent();
        }
        private void ShiftB_Decreae()
        {
            DisableGridViewEvent();
            if (DgvShiftB.Rows.Count > 0)
            {
                rowMpRegistCurrentB = DgvShiftB.CurrentRow.Index;

                string st = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[2].Value.ToString();
                string name = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[3].Value.ToString();
                string dn = CmbNight.SelectedIndex == 0 ? "D" : "N";
                string rate = "0.0";
                string normal = "0.0";
                string rateOT = "0.0";
                string OT = "0.0";

                if (DgvShiftB.Rows[rowMpRegistCurrentB].Cells[2 + 2].Value != null)
                {
                    rate = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[2 + 2].Value.ToString();
                }
                if (DgvShiftB.Rows[rowMpRegistCurrentB].Cells[3 + 2].Value != null)
                {
                    normal = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[3 + 2].Value.ToString();
                }
                if (DgvShiftB.Rows[rowMpRegistCurrentB].Cells[4 + 2].Value != null)
                {
                    rateOT = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[4 + 2].Value.ToString();
                }
                if (DgvShiftB.Rows[rowMpRegistCurrentB].Cells[5 + 2].Value != null)
                {
                    OT = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[5 + 2].Value.ToString();
                }

                int dataGridViewCurrentRow = DgvShiftB.CurrentRow.Index;

                if (!decreseMPList.Contains(st)) // have in the table
                {
                    //if (!decreseMPList.Contains(st))
                    //{
                    int decrow = DgvDecrease.Rows.Count;
                    DgvDecrease.Rows.Add("B", (decrow + 1).ToString(), st, name, rate, normal, rateOT, OT, dn);
                    decreseMPList.Add(st);
                    int r = DgvDecrease.Rows.Count - 1;

                    try
                    {
                        DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)DgvShiftB["Responsibility", dataGridViewCurrentRow];
                        int index = dcc.Items.IndexOf(dcc.Value);
                        DgvDecrease.Rows[r].Cells["Responsibility"].Value = (DgvDecrease.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[index];

                        DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)DgvShiftB["ResponsibilityForOT", dataGridViewCurrentRow];
                        int index1 = dcc1.Items.IndexOf(dcc1.Value);
                        DgvDecrease.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvDecrease.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[index1];
                    }
                    catch (Exception)
                    {
                        DgvDecrease.Rows[r].Cells["Responsibility"].Value = (DgvDecrease.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[0];
                        DgvDecrease.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvDecrease.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[0];
                    }
                    DgvDecrease.Rows[r].Cells["ToSection"].Value = (DgvDecrease.Rows[r].Cells["ToSection"] as DataGridViewComboBoxCell).Items[0];

                    //}

                }
            }
            EnableGridViewEvent();
        }
        private void ShiftA_B_DecreaseCancle()
        {
            if (DgvDecrease.Rows.Count > 0)
            {
                int currentgridview3 = DgvDecrease.CurrentRow.Index;
                string st = DgvDecrease.Rows[currentgridview3].Cells[0 + 2].Value.ToString();
                DgvDecrease.Rows.RemoveAt(currentgridview3);
                decreseMPList.Remove(st);

                int rownb = DgvDecrease.Rows.Count;
                for (int i = 0; i < rownb; i++)
                {
                    DgvDecrease.Rows[i].Cells[1].Value = (i + 1).ToString();
                }

            }
        }

        private void MPAbsentA()
        {
            DisableGridViewEvent();
            if (DgvShiftA.Rows.Count > 0)
            {
                if (DgvShiftA.Rows[rowMpRegistCurrentA].Cells[2].Value == null) return;

                string st = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[2].Value.ToString();
                string name = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[3].Value.ToString();
                string dn = CmbDay.SelectedIndex == 0 ? "D" : "N";
                string rate = "0.0";
                string normal = "0.0";
                string rateOT = "0.0";
                string OT = "0.0";

                if (DgvShiftA.Rows[rowMpRegistCurrentA].Cells[2 + 2].Value != null)
                {
                    rate = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[2 + 2].Value.ToString();
                }
                if (DgvShiftA.Rows[rowMpRegistCurrentA].Cells[3 + 2].Value != null)
                {
                    normal = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[3 + 2].Value.ToString();
                }
                if (DgvShiftA.Rows[rowMpRegistCurrentA].Cells[4 + 2].Value != null)
                {
                    rateOT = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[4 + 2].Value.ToString();
                }
                if (DgvShiftA.Rows[rowMpRegistCurrentA].Cells[5 + 2].Value != null)
                {
                    OT = DgvShiftA.Rows[rowMpRegistCurrentA].Cells[5 + 2].Value.ToString();
                }

                int dataGridViewCurrentRow = DgvShiftA.CurrentRow.Index;

                if (!decreseMPList.Contains(st)) // have in the table
                {
                    if (!absentMPList.Contains(st) && EmpList.Contains(st))
                    {
                        int absentrow = DgvAbsent.Rows.Count;
                        DgvAbsent.Rows.Add("A", (absentrow + 1).ToString(), st, name, rate, normal, rateOT, OT, "", dn);
                        absentMPList.Add(st);
                        empforLineADic.Remove(st);
                        DgvShiftA.Rows.RemoveAt(rowMpRegistCurrentA);
                        int r = DgvAbsent.Rows.Count - 1;

                        try
                        {
                            DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)DgvShiftA["Responsibility", dataGridViewCurrentRow];
                            int index = dcc.Items.IndexOf(dcc.Value);
                            DgvAbsent.Rows[r].Cells["Responsibility"].Value = (DgvAbsent.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[index];

                            DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)DgvShiftA["ResponsibilityForOT", dataGridViewCurrentRow];
                            int index1 = dcc1.Items.IndexOf(dcc1.Value);
                            DgvAbsent.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvAbsent.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[index1];
                        }
                        catch (Exception)
                        {
                            DgvAbsent.Rows[r].Cells["Responsibility"].Value = (DgvAbsent.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[0];
                            DgvAbsent.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvAbsent.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[0];
                        }


                    }
                    else
                    {
                        empforLineADic.Remove(st);
                        DgvShiftA.Rows.RemoveAt(rowMpRegistCurrentA);
                    }

                }
                int rown = DgvShiftA.Rows.Count;

                rowMpRegistCurrentA = rown > 0 ? DgvShiftA.CurrentRow.Index : 0;

                for (int i = 0; i < rown; i++)
                {
                    DgvShiftA.Rows[i].Cells[1].Value = (i + 1).ToString();

                }
            }
            EnableGridViewEvent();
        }
        private void MPAbsentB()
        {
            DisableGridViewEvent();
            if (DgvShiftB.Rows.Count == 0)
            {
                return;
            }
            if (DgvShiftB.Rows[rowMpRegistCurrentB].Cells[2].Value == null) return;
            string st = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[2].Value.ToString();
            string name = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[3].Value.ToString();

            string dn = CmbNight.SelectedIndex == 0 ? "D" : "N";

            string rate = "0.0";
            string normal = "0.0";
            string rateOT = "0.0";
            string OT = "0.0";

            if (DgvShiftB.Rows[rowMpRegistCurrentB].Cells[2 + 2].Value != null)
            {
                rate = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[2 + 2].Value.ToString();
            }
            if (DgvShiftB.Rows[rowMpRegistCurrentB].Cells[3 + 2].Value != null)
            {
                normal = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[3 + 2].Value.ToString();
            }
            if (DgvShiftB.Rows[rowMpRegistCurrentB].Cells[4 + 2].Value != null)
            {
                rateOT = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[4 + 2].Value.ToString();
            }
            if (DgvShiftB.Rows[rowMpRegistCurrentB].Cells[5 + 2].Value != null)
            {
                OT = DgvShiftB.Rows[rowMpRegistCurrentB].Cells[5 + 2].Value.ToString();
            }

            int dataGridViewCurrentRow = DgvShiftB.CurrentRow.Index;

            if (!decreseMPList.Contains(st)) // have in the table
            {
                if (!absentMPList.Contains(st) && EmpList.Contains(st))
                {
                    int absentrow = DgvAbsent.Rows.Count;
                    DgvAbsent.Rows.Add("B", (absentrow + 1).ToString(), st, name, rate, normal, rateOT, OT, "", dn);
                    absentMPList.Add(st);
                    empforLineBDic.Remove(st);
                    DgvShiftB.Rows.RemoveAt(rowMpRegistCurrentB);
                    int r = DgvAbsent.Rows.Count - 1;

                    try
                    {
                        DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)DgvShiftB["Responsibility", dataGridViewCurrentRow];
                        int index = dcc.Items.IndexOf(dcc.Value);
                        DgvAbsent.Rows[r].Cells["Responsibility"].Value = (DgvAbsent.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[index];

                        DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)DgvShiftB["ResponsibilityForOT", dataGridViewCurrentRow];
                        int index1 = dcc1.Items.IndexOf(dcc1.Value);
                        DgvAbsent.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvAbsent.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[index1];
                    }
                    catch (Exception)
                    {
                        DgvAbsent.Rows[r].Cells["Responsibility"].Value = (DgvAbsent.Rows[r].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[0];
                        DgvAbsent.Rows[r].Cells["ResponsibilityForOT"].Value = (DgvAbsent.Rows[r].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[0];
                    }


                }
                else
                {
                    empforLineBDic.Remove(st);
                    DgvShiftB.Rows.RemoveAt(rowMpRegistCurrentB);
                }

            }
            int rown = DgvShiftB.Rows.Count;

            rowMpRegistCurrentB = rown > 0 ? DgvShiftB.CurrentRow.Index : 0;

            for (int i = 0; i < rown; i++)
            {
                DgvShiftB.Rows[i].Cells[1].Value = (i + 1).ToString();

            }

            EnableGridViewEvent();

        }
        private void MPAbsentCancel()
        {
            if (DgvAbsent.Rows.Count > 0)
            {
                int index = DgvAbsent.CurrentRow.Index;
                if (index > -1)
                {
                    string shift = Convert.ToString(DgvAbsent.Rows[index].Cells[0].Value);
                    string id = Convert.ToString(DgvAbsent.Rows[index].Cells[2].Value);
                    string fullname = Convert.ToString(DgvAbsent.Rows[index].Cells[3].Value);
                    string rate = Convert.ToString(DgvAbsent.Rows[index].Cells[4].Value);
                    string normal = Convert.ToString(DgvAbsent.Rows[index].Cells[5].Value);
                    string Otrate = Convert.ToString(DgvAbsent.Rows[index].Cells[6].Value);
                    string Ot = Convert.ToString(DgvAbsent.Rows[index].Cells[7].Value);

                    DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)DgvAbsent["Responsibility", index];
                    int PIC = dcc.Items.IndexOf(dcc.Value);

                    DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)DgvAbsent["ResponsibilityForOT", index];
                    int OTPIC = dcc1.Items.IndexOf(dcc1.Value);

                    if (shift == "A")
                    {
                        DgvShiftA.Rows.Add(shift, 0, id, fullname, rate, normal, Otrate, Ot);
                        empforLineADic.Add(id, fullname);   //////
                        int dgvIndex = DgvShiftA.Rows.Count - 1;
                        DgvShiftA.Rows[dgvIndex].Cells["Responsibility"].Value = (DgvShiftA.Rows[dgvIndex].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[PIC];
                        DgvShiftA.Rows[dgvIndex].Cells["ResponsibilityForOT"].Value = (DgvShiftA.Rows[dgvIndex].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[OTPIC];
                        int rows = DgvShiftA.Rows.Count;
                        for (int i = 0; i < rows; i++)
                        {
                            DgvShiftA.Rows[i].Cells[1].Value = (i + 1).ToString();
                        }

                    }
                    else if (shift == "B")
                    {
                        DgvShiftB.Rows.Add(shift, 0, id, fullname, rate, normal, Otrate, Ot);
                        empforLineBDic.Add(id, fullname); // 
                        int dgvIndex = DgvShiftB.Rows.Count - 1;
                        DgvShiftB.Rows[dgvIndex].Cells["Responsibility"].Value = (DgvShiftB.Rows[dgvIndex].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[PIC];
                        DgvShiftB.Rows[dgvIndex].Cells["ResponsibilityForOT"].Value = (DgvShiftB.Rows[dgvIndex].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[OTPIC];
                        int rows = DgvShiftB.Rows.Count;
                        for (int i = 0; i < rows; i++)
                        {
                            DgvShiftB.Rows[i].Cells[1].Value = (i + 1).ToString();
                        }

                    }
                    absentMPList.Remove(id);
                    DgvAbsent.Rows.RemoveAt(index);
                    int dvg2count = DgvAbsent.Rows.Count;
                    for (int i = 0; i < dvg2count; i++)
                    {
                        DgvAbsent.Rows[i].Cells[1].Value = (i + 1).ToString();
                    }
                }
            }

        }

        #endregion Management in Tab
        private void NewMPRegister()
        {
            DgvAbsent.Rows.Clear();
            DgvDecrease.Rows.Clear();
            DgvIncrease.Rows.Clear();
            absentMPList.Clear();
            decreseMPList.Clear();

            DisableGridViewEvent();

            LoadIncrease();
            LoadManPowerRegistation();

            EnableGridViewEvent();
        }

        private void LoadIncrease()
        {
            DateTime date = DTPRegister.Value;
            DateTime startdate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            using (var db = new ProductionEntities11())
            {
                List<EmpManPowerRegistedTable> IncreaseList = db.Emp_ManPowerRegistedTable.Where(s => s.sectionCode == User.SectionCode)
                    .Where(s => s.registDate == startdate).Where(d => d.DecInc == "I")
                    .Join(db.Emp_ManPowersTable, p => p.userID, e => e.userID, (p, e) => new EmpManPowerRegistedTable
                    {
                        UserId = p.userID,
                        Fullname = e.fullName,
                        ShiftAB = p.shiftAB,
                        FunctionId = p.functionID,
                        Rate = p.rate,
                        Period = p.period,
                        FunctionOTId = p.functionOtID,
                        RateOT = p.rateOT,
                        PeriodOT = p.periodOT,
                        SectionCodeFrom = p.sectionCodeFrom,
                    }).ToList();

                ManPowerRegister.LoadingIncrease(DgvIncrease, IncreaseList);

            }



        }




        private void SaveDailyRegister()
        {
            if (CheckIncreaseDestination() == false)
            {
                MessageBox.Show("Please check Decreasing Man power of distination section target should not same source section ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult r = MessageBox.Show("Confirm new or replace register Man power", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (r == DialogResult.OK)
            {
                if (!MPregister())
                    MessageBox.Show("Failured to save man-power registration. Try it again. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Saved man-power registration completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void Roles()
        {
            BtnAbsentA.Enabled = false;
            BtnAbsentB.Enabled = false;
            BtnMPOutA.Enabled = false;
            BtnMPOutB.Enabled = false;
            BtnMPIn.Enabled = false;
            BtnUpdate.Enabled = false;
            BtnSaveMP.Enabled = false;
            BtnNew.Enabled = false;
            if (User.Role == Models.Roles.Invalid) // invalid
            {

            }
            else if (User.Role == Models.Roles.General) // General
            {
                BtnAbsentA.Enabled = true;
                BtnAbsentB.Enabled = true;
                BtnMPOutA.Enabled = true;
                BtnMPOutB.Enabled = true;
                BtnMPIn.Enabled = true;
                BtnUpdate.Enabled = true;
                BtnNew.Enabled = true;
            }
            else if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager) // production
            {
                BtnAbsentA.Enabled = true;
                BtnAbsentB.Enabled = true;
                BtnMPOutA.Enabled = true;
                BtnMPOutB.Enabled = true;
                BtnMPIn.Enabled = true;
                BtnUpdate.Enabled = true;
                BtnNew.Enabled = true;
                BtnSaveMP.Enabled = true;
            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {

            }
            else if (User.Role == Models.Roles.Admin_Min) // Admin-mini
            {
                BtnAbsentA.Enabled = true;
                BtnAbsentB.Enabled = true;
                BtnMPOutA.Enabled = true;
                BtnMPOutB.Enabled = true;
                BtnMPIn.Enabled = true;
                BtnUpdate.Enabled = true;
                BtnSaveMP.Enabled = true;
                BtnNew.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                BtnAbsentA.Enabled = true;
                BtnAbsentB.Enabled = true;
                BtnMPOutA.Enabled = true;
                BtnMPOutB.Enabled = true;
                BtnMPIn.Enabled = true;
                BtnUpdate.Enabled = true;
                BtnSaveMP.Enabled = true;
                BtnNew.Enabled = true;
            }
        }

        private void CurrentMP()
        {
            DateTime dt = DTPRegister.Value;
            string dateToday = dt.ToString("yyyy-MM-dd");

            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SS("LoadCurrentMP", "@StringDate", dateToday, "@LineNoShift", User.SectionCode);
            if (sqlstatus)
            {
                DataSet ds3 = sql.Dataset;
                DataTable dt1 = ds3.Tables[0]; // Normal Shift A
                DataTable dt2 = ds3.Tables[1]; // Normal Shift B
                DataTable dt3 = ds3.Tables[2]; // Decrease Shift A,B
                DataTable dt4 = ds3.Tables[3]; // Increase Shift A,B
                DataTable dt5 = ds3.Tables[4]; // Absent  Shift A,B

                DgvAbsent.Rows.Clear();
                DgvDecrease.Rows.Clear();
                DgvIncrease.Rows.Clear();
                /////////////////// Noraml A ////////////////////////////////////////
                empforLineADic.Clear();
                DgvShiftA.Rows.Clear();
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string id = Convert.ToString(dt1.Rows[i][0]);
                        string fullname = Convert.ToString(dt1.Rows[i][1]);
                        string rate = Convert.ToString(dt1.Rows[i][3]);
                        string normal = Convert.ToString(dt1.Rows[i][4]);
                        string rateOT = Convert.ToString(dt1.Rows[i][6]);
                        string OT = Convert.ToString(dt1.Rows[i][7]);
                        string OTacc = Convert.ToString(dt1.Rows[i][8]);
                        string Shift = Convert.ToString(dt1.Rows[i][9]);

                        DgvShiftA.Rows.Add(Shift, (i + 1).ToString(), id, fullname, rate, normal, rateOT, OT, OTacc);
                        empforLineADic.Add(id, fullname);
                        int s33 = Convert.ToInt32(dt1.Rows[i][2]);
                        DgvShiftA.Rows[i].Cells["Responsibility"].Value = (DgvShiftA.Rows[i].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[s33];
                        int responForOT = Convert.ToInt32(dt1.Rows[i][5]);
                        DgvShiftA.Rows[i].Cells["ResponsibilityForOT"].Value = (DgvShiftA.Rows[i].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[responForOT];
                    }
                }
                /////////////////// Noraml B ////////////////////////////////////////
                empforLineBDic.Clear();
                DgvShiftB.Rows.Clear();
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    string id = Convert.ToString(dt2.Rows[i][0]);
                    string fullname = Convert.ToString(dt2.Rows[i][1]);
                    string rate = Convert.ToString(dt2.Rows[i][3]);
                    string normal = Convert.ToString(dt2.Rows[i][4]);
                    string rateOT = Convert.ToString(dt2.Rows[i][6]);
                    string OT = Convert.ToString(dt2.Rows[i][7]);
                    string OTacc = Convert.ToString(dt2.Rows[i][8]);
                    string Shift = Convert.ToString(dt2.Rows[i][9]);

                    DgvShiftB.Rows.Add(Shift, (i + 1).ToString(), id, fullname, rate, normal, rateOT, OT, OTacc);
                    empforLineBDic.Add(id, fullname);
                    int s33 = Convert.ToInt32(dt2.Rows[i][2]);
                    DgvShiftB.Rows[i].Cells["Responsibility"].Value = (DgvShiftB.Rows[i].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[s33];
                    int responForOT = Convert.ToInt32(dt2.Rows[i][5]);
                    DgvShiftB.Rows[i].Cells["ResponsibilityForOT"].Value = (DgvShiftB.Rows[i].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[responForOT];
                }

                /////////////////// Decresing ////////////////////////////////////////
                while (DgvDecrease.Rows.Count > 1)
                {
                    DgvDecrease.Rows.RemoveAt(0);
                }
                DgvDecrease.Rows.Clear();
                decreseMPList.Clear();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string id = Convert.ToString(dt3.Rows[i][0]);
                    string fullname = Convert.ToString(dt3.Rows[i][1]);
                    string rate = Convert.ToString(dt3.Rows[i][3]);
                    string normal = Convert.ToString(dt3.Rows[i][4]);
                    string rateOT = Convert.ToString(dt3.Rows[i][6]);
                    string OT = Convert.ToString(dt3.Rows[i][7]);
                    string shift = Convert.ToString(dt3.Rows[i][9]);

                    DgvDecrease.Rows.Add(shift, (i + 1).ToString(), id, fullname, rate, normal, rateOT, OT);
                    decreseMPList.Add(id);
                    int s33 = Convert.ToInt32(dt3.Rows[i][2]);
                    DgvDecrease.Rows[i].Cells["Responsibility"].Value = (DgvDecrease.Rows[i].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[s33];
                    int responForOT = Convert.ToInt32(dt3.Rows[i][5]);
                    DgvDecrease.Rows[i].Cells["ResponsibilityForOT"].Value = (DgvDecrease.Rows[i].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[responForOT];

                    string section = Convert.ToString(dt3.Rows[i][8]);
                    DataGridViewComboBoxCell cbc = (DgvDecrease.Rows[i].Cells["ToSection"] as DataGridViewComboBoxCell);
                    if (cbc.Items.Count > 0)
                    {
                        for (int j = 0; j < cbc.Items.Count; j++)
                        {
                            if (String.Compare(Dict.SectionCodeName.ElementAt(j).Key, section) == 0)
                            {
                                DgvDecrease.Rows[i].Cells["ToSection"].Value = (DgvDecrease.Rows[i].Cells["ToSection"] as DataGridViewComboBoxCell).Items[j];
                                break;
                            }
                        }
                    }

                }

                /////////////////////////// Incresing //////////////////////////////////
                while (DgvIncrease.Rows.Count > 1)
                {
                    DgvIncrease.Rows.RemoveAt(0);
                }
                DgvIncrease.Rows.Clear();
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    string id = Convert.ToString(dt4.Rows[i][0]);
                    string fullname = Convert.ToString(dt4.Rows[i][1]);
                    string rate = Convert.ToString(dt4.Rows[i][3]);
                    string normal = Convert.ToString(dt4.Rows[i][4]);
                    string rateOT = Convert.ToString(dt4.Rows[i][6]);
                    string OT = Convert.ToString(dt4.Rows[i][7]);
                    string fromsection = Convert.ToString(dt4.Rows[i][8]);
                    string shift = Convert.ToString(dt4.Rows[i][9]);

                    DgvIncrease.Rows.Add(shift, (i + 1).ToString(), id, fullname, rate, normal, rateOT, OT);
                    int s33 = Convert.ToInt32(dt4.Rows[i][2]);
                    DgvIncrease.Rows[i].Cells["Responsibility"].Value = (DgvIncrease.Rows[i].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[s33];
                    int responForOT = Convert.ToInt32(dt4.Rows[i][5]);
                    DgvIncrease.Rows[i].Cells["ResponsibilityForOT"].Value = (DgvIncrease.Rows[i].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[responForOT];

                    DataGridViewComboBoxCell cbc = (DgvIncrease.Rows[i].Cells["DecreasefromSection"] as DataGridViewComboBoxCell);
                    if (cbc.Items.Count > 0)
                    {
                        for (int j = 0; j < cbc.Items.Count; j++)
                        {
                            if (String.Compare(Dict.SectionCodeName.ElementAt(j).Key, fromsection) == 0)
                            {
                                DgvIncrease.Rows[i].Cells["DecreasefromSection"].Value = (DgvIncrease.Rows[i].Cells["DecreasefromSection"] as DataGridViewComboBoxCell).Items[j];
                                break;
                            }
                        }
                    }

                }

                /// Load Current Absent employee table ///

                DgvAbsent.Rows.Clear();
                if (dt5.Rows.Count > 0)
                {
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        string id = Convert.ToString(dt5.Rows[i][0]);
                        string fullname = Convert.ToString(dt5.Rows[i][1]);
                        string rate = Convert.ToString(dt5.Rows[i][3]);
                        string normal = Convert.ToString(dt5.Rows[i][4]);
                        string rateOT = Convert.ToString(dt5.Rows[i][6]);
                        string Shift = Convert.ToString(dt5.Rows[i][8]);

                        DgvAbsent.Rows.Add(Shift, (i + 1).ToString(), id, fullname, rate, normal, rateOT);
                        int s33 = Convert.ToInt32(dt5.Rows[i][2]);
                        DgvAbsent.Rows[i].Cells["Responsibility"].Value = (DgvAbsent.Rows[i].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[s33];
                        int responForOT = Convert.ToInt32(dt5.Rows[i][5]);
                        DgvAbsent.Rows[i].Cells["ResponsibilityForOT"].Value = (DgvAbsent.Rows[i].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[responForOT];


                    }
                }
            }
        }

        private void LoadSectionMember()
        {
            SqlClass sql = new SqlClass();
            if (sql.MPregist_LoadSectionMemberIDSQL())
            {
                DataTable dt1 = sql.Datatable;

                EmpList.Clear();
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string id = dt1.Rows[i].ItemArray[0].ToString();
                        EmpList.Add(id);
                    }
                }
            }
        }


        private void LoadManPowerRegistation()
        {
            string dt = DTPRegister.Value.ToString("yyyy-MM-dd");

            SqlClass sql = new SqlClass();
            bool sqlstatus = sql.SSQL_SS("ManPowerRegistation", "@StringDate", dt, "@LineNo", User.SectionCode);
            if (sqlstatus)
            {
                DataSet ds3 = sql.Dataset;
                DataTable dt1 = ds3.Tables[0];
                DataTable dt2 = ds3.Tables[1];
                while (DgvShiftA.Rows.Count > 1)
                {
                    DgvShiftA.Rows.RemoveAt(0);
                }
                empforLineADic.Clear();
                DgvShiftA.Rows.Clear();
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string id = Convert.ToString(dt1.Rows[i][0]);
                        string fullname = Convert.ToString(dt1.Rows[i][1]);
                        string rate = Convert.ToString(dt1.Rows[i][3]);
                        string rateOT = Convert.ToString(dt1.Rows[i][5]);
                        string OTacc = Convert.ToString(dt1.Rows[i][6]);
                        string shift = Convert.ToString(dt1.Rows[i][7]);
                        DgvShiftA.Rows.Add(shift, (i + 1).ToString(), id, fullname, rate, "8", rateOT, "", OTacc);
                        empforLineADic.Add(id, fullname);
                        int s33 = Convert.ToInt32(dt1.Rows[i][2]);
                        DgvShiftA.Rows[i].Cells["Responsibility"].Value = (DgvShiftA.Rows[i].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[s33];
                        int responForOT = Convert.ToInt32(dt1.Rows[i][4]);
                        DgvShiftA.Rows[i].Cells["ResponsibilityForOT"].Value = (DgvShiftA.Rows[i].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[responForOT];
                    }
                }

                while (DgvShiftB.Rows.Count > 1)
                {
                    DgvShiftB.Rows.RemoveAt(0);
                }
                empforLineBDic.Clear();
                DgvShiftB.Rows.Clear();
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        string id = Convert.ToString(dt2.Rows[i][0]);
                        string fullname = Convert.ToString(dt2.Rows[i][1]);
                        string rate = Convert.ToString(dt2.Rows[i][3]);
                        string rateOT = Convert.ToString(dt2.Rows[i][5]);
                        string OTacc = Convert.ToString(dt2.Rows[i][6]);
                        string shift = Convert.ToString(dt2.Rows[i][7]);
                        DgvShiftB.Rows.Add(shift, (i + 1).ToString(), id, fullname, rate, "8", rateOT, "", OTacc);
                        empforLineBDic.Add(id, fullname);
                        int s33 = Convert.ToInt32(dt2.Rows[i][2]);
                        DgvShiftB.Rows[i].Cells["Responsibility"].Value = (DgvShiftB.Rows[i].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[s33];
                        int responForOT = Convert.ToInt32(dt2.Rows[i][4]);
                        DgvShiftB.Rows[i].Cells["ResponsibilityForOT"].Value = (DgvShiftB.Rows[i].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[responForOT];


                    }
                }
            }

        }






        private bool MPregister()
        {
            DateTime dt = DTPRegister.Value;
            int day = dt.Day;
            int month = dt.Month;
            int year = dt.Year;
            int dayInMonth = DateTime.DaysInMonth(year, month);
            DateTime registDate = new DateTime(year, month, day, 0, 0, 0);
            DateTime enddate = registDate.AddDays(dayInMonth - 1);

            string section = User.SectionCode;
            string workType = ManPowerRegister.ChkTodayIsDay(registDate);

            string daynight_A = CmbDay.SelectedIndex == 0 ? "D" : "N";
            string daynight_B = CmbNight.SelectedIndex == 0 ? "D" : "N";

            List<Emp_ManPowerRegistedTable> manpowerList = PrepareMPregister(true);

            double exculsionTime;
            var exclusion_RecordTable = new List<Exclusion_RecordTable>();
            if (chkExculsion.Checked == true && manpowerList.Count != 0)
            {
                exclusion_RecordTable = ManPowerRegister.ExclusionTime(section, registDate, workType, ChkWorkA, ChkWorkB, manpowerList);
                exculsionTime = exclusion_RecordTable
                   .Where(r => r.registDate == registDate)
                   .Where(s => s.sectionCode == User.SectionCode)
                   .Where(s => s.exclusionID == "F1" || s.exclusionID == "F2" || s.exclusionID == "F3")
                   .GroupBy(s => s.registDate)
                   .Select(x => new Exclusion
                   {
                       Exclusiontime = x.Sum(a => a.totalMH / 60),
                   }).SingleOrDefault().Exclusiontime;
            }
            else
            {
                exculsionTime = 0;
            }



            List<Prod_TodayWorkTable> prod_TodayWorkTable = ManPowerRegister.CreateProdTodayWorkTable(section, registDate, daynight_A, daynight_B,
                ChkWorkA, ChkWorkB, manpowerList);


            using (var db = new ProductionEntities11())
            {
                try
                {
                    var removeEmpRegist = db.Emp_ManPowerRegistedTable.Where(r => r.registDate == registDate)
                        .Where(s => s.sectionCodeFrom == section);
                    if (removeEmpRegist != null)
                        db.Emp_ManPowerRegistedTable.RemoveRange(removeEmpRegist);

                    var removeExclusion = db.Exclusion_RecordTable.Where(r => r.registDate == registDate)
                        .Where(s => s.sectionCode == section);
                    if (removeExclusion != null && chkExculsion.Checked == true)
                        db.Exclusion_RecordTable.RemoveRange(removeExclusion);



                    var initProd_TodayWork = db.Prod_TodayWorkTable.Where(r => r.registDate == registDate)
                        .Where(s => s.sectionCode == section).Any();
                    if (initProd_TodayWork == false)
                    {
                        List<Prod_TodayWorkTable> init = ManPowerRegister.InitialProd_TodayWorkTable(registDate, section);
                        db.Prod_TodayWorkTable.AddRange(init);
                    }


                    var findD = prod_TodayWorkTable.FirstOrDefault(d => d.dayNight == "D");
                    var updateProd_TodayWorkD = db.Prod_TodayWorkTable.Where(r => r.registDate == registDate)
                       .Where(s => s.sectionCode == section).SingleOrDefault(s => s.dayNight == "D");
                    if (updateProd_TodayWorkD != null)
                    {
                        updateProd_TodayWorkD.workHour = findD.workHour;
                        updateProd_TodayWorkD.workShift = findD.workShift;
                        db.SaveChanges();
                    }

                    var findN = prod_TodayWorkTable.FirstOrDefault(d => d.dayNight == "N");
                    var updateProd_TodayWorkN = db.Prod_TodayWorkTable.Where(r => r.registDate == registDate)
                       .Where(s => s.sectionCode == section).SingleOrDefault(s => s.dayNight == "N");
                    if (updateProd_TodayWorkN != null)
                    {
                        updateProd_TodayWorkN.workHour = findN.workHour;
                        updateProd_TodayWorkN.workShift = findN.workShift;
                        db.SaveChanges();
                    }






                    var initPgMH = db.Pg_MH.Where(r => r.registDate >= registDate && r.registDate <= enddate)
                        .Where(s => s.sectionCode == section).Any();
                    if (initPgMH == false)
                    {
                        List<Pg_MH> init = ManPowerRegister.InitialProgressMH(registDate, section);
                        db.Pg_MH.AddRange(init);
                    }
                    db.SaveChanges();


                    //var removePgMH = db.Pg_MH.Where(r => r.registDate == registDate)
                    //  .Where(s => s.sectionCode == section);
                    //if (removePgMH != null)
                    //    db.Pg_MH.RemoveRange(removePgMH);



                    List<ShiftABMH> summaryMH = ManPowerRegister.SummaryManHour(manpowerList);
                    List<SummaryMH> summaryManHour = ManPowerRegister.SummaryOverAllManHour(summaryMH, exculsionTime);

                    var summary1 = summaryManHour.Where(s => s.Shift == "Total").FirstOrDefault();
                    var summary2 = summaryManHour.Where(s => s.Shift == "Exclusion").FirstOrDefault();
                    var summary3 = summaryManHour.Where(s => s.Shift == "GrossMH").FirstOrDefault();

                    var pgmh = db.Pg_MH.Where(s => s.sectionCode == section).Where(r => r.registDate == registDate).SingleOrDefault();
                    if (pgmh != null)
                    {
                        var pg = new Pg_MH()
                        {
                            sectionCode = section,
                            registDate = registDate,
                            MHNormal = summary1.WorkingHr,
                            MHOT = summary1.OvertimeHr,
                            TotalMH = summary1.TotalHr,
                            exclusionHr = summary2.TotalHr,
                            GMH = summary3.TotalHr,
                        };
                        db.Entry(pgmh).CurrentValues.SetValues(pg);
                    }





                    db.Emp_ManPowerRegistedTable.AddRange(manpowerList);

                    db.Exclusion_RecordTable.AddRange(exclusion_RecordTable);

                    //db.Prod_TodayWorkTable.AddRange(prod_TodayWorkTable);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot connect SQL dB", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

            }



        }


        private List<Emp_ManPowerRegistedTable> PrepareMPregister(bool save)
        {
            DateTime dt = DTPRegister.Value;
            int day = dt.Day;
            int month = dt.Month;
            int year = dt.Year;

            DateTime registDate = new DateTime(year, month, day, 0, 0, 0);

            string section = User.SectionCode;
            string workType = ManPowerRegister.ChkTodayIsDay(registDate);

            var manpowerList = new List<Emp_ManPowerRegistedTable>();

            string daynight_A = CmbDay.SelectedIndex == 0 ? "D" : "N";
            manpowerList = ManPowerRegister.MPRegistedNormal(DgvShiftA, section,
                registDate, "A", daynight_A, workType, ChkWorkA, manpowerList);

            string daynight_B = CmbNight.SelectedIndex == 0 ? "D" : "N";
            manpowerList = ManPowerRegister.MPRegistedNormal(DgvShiftB, section,
                registDate, "B", daynight_B, workType, ChkWorkB, manpowerList);

            manpowerList = ManPowerRegister.MPRegistedDecInc(DgvDecrease, section, registDate, workType, manpowerList, save);
            if (save == false)
                manpowerList = ManPowerRegister.MPRegistedInc(DgvIncrease, section, registDate, workType, manpowerList);

            manpowerList = ManPowerRegister.MPRegistedAbsent(DgvAbsent, section, registDate, workType, manpowerList);

            return manpowerList;
        }


        private bool CheckIncreaseDestination()
        {
            string Sectionkey = User.SectionCode;
            for (int i = 0; i < DgvDecrease.Rows.Count; i++)
            {
                DataGridViewComboBoxCell dcc2 = (DataGridViewComboBoxCell)DgvDecrease["ToSection", i];
                int ToSection = dcc2.Items.IndexOf(dcc2.Value);

                String SectionCodeNameKey = "";
                if (Dict.SectionCodeName.Count > 0)
                {
                    SectionCodeNameKey = Dict.SectionCodeName.ElementAt(ToSection).Key;
                }
                if (Sectionkey == SectionCodeNameKey)
                {
                    return false; // exit failure
                }

            }
            return true;
        }








        private void UpdateReport()
        {
            DateTime selectdate = DTPRegister.Value;
            DateTime registDate = new DateTime(DTPRegister.Value.Year, DTPRegister.Value.Month, DTPRegister.Value.Day, 0, 0, 0);
            PeriodDateOT date = ManPowerRegister.CheckOvertimeSummary(selectdate);
            string section = User.SectionCode;
            string workType = ManPowerRegister.ChkTodayIsDay(registDate);

            var empMPovertime = new List<EmpMPsummaryReport>();

            var nameList = new List<EmpNameList>();

            double exculsionTime;

            DateTime startdate, stopdate;
            if (date.Start > date.Stop)
            {
                startdate = date.Stop;
                stopdate = date.Start;

            }
            else
            {
                startdate = date.Start;
                stopdate = date.Stop;
            }
            using (var db = new ProductionEntities11())
            {
                var datalist = db.Emp_ManPowerRegistedTable
                    .Where(r => r.registDate >= startdate && r.registDate <= stopdate)
                    .Where(s => s.sectionCode == User.SectionCode).ToList();

                var summary = ManPowerRegister.ManPowerSummary(datalist, startdate, stopdate);

                empMPovertime = (from o in summary
                                 join e in db.Emp_ManPowersTable
                              on o.UserId equals e.userID
                                 select new EmpMPsummaryReport
                                 {
                                     UserId = o.UserId,
                                     Fullname = e.fullName,
                                     MWorkingTime = o.MWorkingTime,
                                     MOverTime = o.MOverTime,
                                     MTotalTime = o.MTotalTime
                                 }).ToList();

                nameList = (from o in empMPovertime
                            join e in db.Emp_ManPowersTable
                            on o.UserId equals e.userID
                            select new EmpNameList
                            {
                                UserId = o.UserId,
                                Fullname = e.fullName
                            }).ToList();



            }

            List<Emp_ManPowerRegistedTable> manpowerList = PrepareMPregister(false);

            var exclusion_RecordTable = new List<Exclusion_RecordTable>();
            if (chkExculsion.Checked == true && manpowerList.Count != 0)
            {
                exclusion_RecordTable = ManPowerRegister.ExclusionTime(section, registDate, workType, ChkWorkA, ChkWorkB, manpowerList);
                exculsionTime = exclusion_RecordTable
                    .Where(r => r.registDate == registDate)
                    .Where(s => s.sectionCode == User.SectionCode)
                    .Where(s => s.exclusionID == "F1" || s.exclusionID == "F2" || s.exclusionID == "F3")
                    .GroupBy(s => s.registDate)
                    .Select(x => new Exclusion
                    {
                        Exclusiontime = x.Sum(a => a.totalMH / 60),
                    }).SingleOrDefault().Exclusiontime;

            }
            else
            {
                exculsionTime = 0;
            }


            List<ShiftABMH> summaryMH = ManPowerRegister.SummaryManHour(manpowerList);
            List<SummaryMH> summaryManHour = ManPowerRegister.SummaryOverAllManHour(summaryMH, exculsionTime);

            var frm = new ManPowerReportForm(empMPovertime, nameList, summaryManHour, startdate, stopdate, selectdate);
            frm.ManPowerReportFormClosed += new EventHandler(ManPowerReportFormClosed_Close);
            frm.TopMost = true;
            frm.Show();


        }

        private void ManPowerReportFormClosed_Close(object sender, EventArgs e)
        {
            reportOpen = false;
        }



        private void ToolTipBox()
        {
            ToolTip toolTip1 = new ToolTip
            {
                UseFading = true,
                UseAnimation = true,
                IsBalloon = true,
                // Set up the delays for the ToolTip.
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                // Force the ToolTip text to be displayed whether or not the form is active.
                ShowAlways = true
            };

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.TbAdd1a, "กรอก รหัสพนักงาน 7 ตัว หากต้องการเพิ่มจำนวนพนักงาน");
            toolTip1.SetToolTip(this.TbAdd1b, "กรอก รหัสพนักงาน 7 ตัว หากต้องการเพิ่มจำนวนพนักงาน");
            toolTip1.SetToolTip(this.OTaInput, "กรอก จำนวนเวลา เช่น 2.5  คือ  2 ชม 30 นาที");
            toolTip1.SetToolTip(this.OTbInput, "กรอก จำนวนเวลา เช่น 2.5  คือ  2 ชม 30 นาที");
            toolTip1.SetToolTip(this.OTaForWork, "เช็ค การทำล่วงเวลาในครั้งนี้ เพื่อการผลิตงาน");
            toolTip1.SetToolTip(this.OTbForWork, "เช็ค การทำล่วงเวลาในครั้งนี้ เพื่อการผลิตงาน");
            toolTip1.SetToolTip(this.CmbDay, "เลือกการทำงาน จะผล เวลาสืบด้นข้อมูล");
            toolTip1.SetToolTip(this.CmbNight, "เลือกการทำงาน จะผล เวลาสืบด้นข้อมูล");
            toolTip1.SetToolTip(this.BtnOTA, "กดแทนการกด Enter");
            toolTip1.SetToolTip(this.BtnOTB, "กดแทนการกด Enter");

            toolTip1.SetToolTip(this.BtnCheckMP, "ตรวจสอบเวลาทำงาน");
            toolTip1.SetToolTip(this.chkExculsion, "ลง ExclusionTime อัตโนมัติ F1 5min,F2 20min,F3 5min,M1 233min");
            toolTip1.SetToolTip(this.BtnOTB, "กดแทนการกด Enter");
            toolTip1.SetToolTip(this.BtnOTB, "กดแทนการกด Enter");
            toolTip1.SetToolTip(this.BtnOTB, "กดแทนการกด Enter");
            toolTip1.SetToolTip(this.BtnOTB, "กดแทนการกด Enter");
            toolTip1.SetToolTip(this.BtnOTB, "กดแทนการกด Enter");
            toolTip1.SetToolTip(this.BtnOTB, "กดแทนการกด Enter");
            toolTip1.SetToolTip(this.BtnOTB, "กดแทนการกด Enter");
            toolTip1.SetToolTip(this.BtnOTB, "กดแทนการกด Enter");



            toolTip1.SetToolTip(this.OTbForWork, "ลบข้อมูล ออกจากแฟ้มในหน่วยงาน");
            toolTip1.SetToolTip(this.BtnOTA, "Double click เพื่อคัดลอกรายชื่อเข้าแฟ้มหน่วยงาน");

        }

        #endregion

        #endregion


        //============================================================================================//
        //============================================================================================//
        //============================================================================================//

        readonly Dictionary<string, string> empname = new Dictionary<string, string>();
        readonly Dictionary<string, string> empforLine = new Dictionary<string, string>();
        readonly Dictionary<string, string> shiftCodeToIndex = new Dictionary<string, string>();

        int registIndex;
        int listIndex;


        #region Evant TAB 2

        private void CmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //   string Shiftkey = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Key;
            LoadSectionMember1();
        }


        //private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string Shiftkey = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Key;
        //    LoadSectionMember(Shiftkey);
        //}

        //private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string Shiftkey = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Key;
        //    LoadSectionMember(Shiftkey);
        //}


        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string emp = dataGridView1.Rows[listIndex].Cells[1].Value.ToString();
            AddEmp(emp);
        }

        private void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            AddEmp(TbEmpID.Text);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveEmp();
        }

        private void btnSAVE_Click(object sender, EventArgs e)
        {
            MPregisterInSection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            listIndex = dataGridView1.CurrentRow.Index;
        }



        private void dataGridViewSection_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridViewSection.CurrentRow.Index;
            int c = dataGridViewSection.CurrentCell.ColumnIndex;
            string pn = Convert.ToString(dataGridViewSection.Rows[r].Cells[c].Value);

            if (c <= 2)
            {
                dataGridViewSection.Rows[r].Cells[c].Value = log;
            }
            else if (c == 3 || c == 4)
            {
                bool isnumeric = double.TryParse(pn, out double n);
                if (isnumeric == false || n > 1)
                {

                    MessageBox.Show("Please fill only numberical and Rate is not more than 1 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridViewSection.Rows[r].Cells[c].Value = log;
                    return;
                }
            }
        }



        private void dataGridViewSection_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridViewSection.CurrentCell.RowIndex;
            int c = dataGridViewSection.CurrentCell.ColumnIndex;
            log = dataGridViewSection.Rows[r].Cells[c].Value;
            Console.WriteLine(log);
        }


        #endregion


        #region  =====  OPERATION LOOP TAB 2  ========

        #region Inital Component in Tab 2

        private void InitialOperatorInSection()
        {
            InitialDataGridView();
            Roles2();
        }


        private void Roles2()
        {
            btnRemove.Enabled = false;
            btnSAVE.Enabled = false;
            btnEmpList.Enabled = false;
            if (User.Role == Models.Roles.Invalid) // invalid
            {

            }
            else if (User.Role == Models.Roles.General) // General
            {
                btnEmpList.Enabled = true;
            }
            else if (User.Role == Models.Roles.Prod_Outline || User.Role == Models.Roles.Prod_LineLeader || User.Role == Models.Roles.Prod_TeamLeader || User.Role == Models.Roles.Prod_Manager)  // production
            {
                btnRemove.Enabled = true;
                btnSAVE.Enabled = true;
                btnEmpList.Enabled = true;
            }
            else if (User.Role == Models.Roles.FacEng) // Fac eng
            {

            }
            else if (User.Role == Models.Roles.Admin_Min) // Admin-mini
            {
                btnRemove.Enabled = true;
                btnSAVE.Enabled = true;
                btnEmpList.Enabled = true;
            }
            else if (User.Role == Models.Roles.Admin_Full) // Admin-Full
            {
                btnRemove.Enabled = true;
                btnSAVE.Enabled = true;
                btnEmpList.Enabled = true;
            }
        }
        private void LoadComboBox()
        {
            empforLine.Clear();
            empname.Clear();
            CmbShift.Items.Clear();
            shiftCodeToIndex.Add("A", "Shift A");
            shiftCodeToIndex.Add("B", "Shift B");
            shiftCodeToIndex.Add("C", "Shift C");
            CmbShift.DataSource = new BindingSource(shiftCodeToIndex, null);
            CmbShift.DisplayMember = "Value";
            CmbShift.ValueMember = "Key";
            CmbSection.DataSource = new BindingSource(Dict.UserSection, null);
            CmbSection.DisplayMember = "Value";
            CmbSection.ValueMember = "Key";
        }
        private void InitialDataGridView()
        {
            this.dataGridView1.ColumnCount = 3;
            this.dataGridView1.Columns[0].Name = "No";
            this.dataGridView1.Columns[0].Width = 50;
            //this.dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //this.dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns[1].Name = "EmpID";
            this.dataGridView1.Columns[1].Width = 100;
            this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.Columns[2].Name = "Full Name";
            this.dataGridView1.Columns[2].Width = 300;
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView1.RowTemplate.Height = 25;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;

            this.dataGridViewSection.ColumnCount = 5;
            this.dataGridViewSection.Columns[0].Name = "No";
            this.dataGridViewSection.Columns[0].Width = 30;
            this.dataGridViewSection.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewSection.Columns[1].Name = "ID";
            this.dataGridViewSection.Columns[1].Width = 70;
            this.dataGridViewSection.Columns[2].Name = "Full Name";
            this.dataGridViewSection.Columns[2].Width = 200;
            this.dataGridViewSection.Columns[3].Name = "Rate";
            this.dataGridViewSection.Columns[3].Width = 70;
            this.dataGridViewSection.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewSection.Columns[4].Name = "OTRate";
            this.dataGridViewSection.Columns[4].Width = 70;
            this.dataGridViewSection.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewSection.RowHeadersWidth = 4;
            this.dataGridViewSection.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewSection.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridViewSection.RowTemplate.Height = 25;
            dataGridViewSection.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewSection.AllowUserToResizeRows = false;
            dataGridViewSection.AllowUserToResizeColumns = false;
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Func",
                Name = "Responsibility",
                DataSource = new BindingSource(ListValue.EmpFunction, null),
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 3
            };
            comboBoxColumn.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn.DefaultCellStyle.Font = new Font("Tahoma", 10);
            dataGridViewSection.Columns.Add(comboBoxColumn);
            DataGridViewComboBoxColumn comboBoxColumn1 = new DataGridViewComboBoxColumn
            {
                HeaderText = "OTFunc",
                Name = "ResponsibilityForOT",
                DataSource = new BindingSource(ListValue.EmpFunction, null),
                DropDownWidth = 10,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                DisplayIndex = 5
            };
            comboBoxColumn1.CellTemplate.Style.BackColor = Color.White;
            comboBoxColumn1.DefaultCellStyle.Font = new Font("Tahoma", 10);
            dataGridViewSection.Columns.Add(comboBoxColumn1);
        }

        private void LoadEventComponent2()
        {
            //CmbSection.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            //CmbShift.SelectedIndexChanged -= comboBox4_SelectedIndexChanged;
            CmbSection.SelectedIndexChanged -= CmbSection_SelectedIndexChanged;
            if (User.Shift == "A")
            {
                CmbShift.SelectedIndex = 0;
            }
            else if (User.Shift == "B")
            {
                CmbShift.SelectedIndex = 1;
            }
            else if (User.Shift == "C")
            {
                CmbShift.SelectedIndex = 2;
            }
            //LoadTable();


            //LoopSectionMemberStart();
            int c = CmbDay.Items.Count;
            //int i = 0;
            //foreach (KeyValuePair<string, string> item in Dict.UserSection)  ///*****************
            //{
            //    if (item.Key == User.SectionCode)
            //    {
            //        CmbDay.SelectedIndex = i;
            //        break;
            //    }
            //    i += 1;
            //}

            //CmbSection.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            //CmbShift.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            CmbSection.SelectedIndexChanged += CmbSection_SelectedIndexChanged;

            LoadSectionMember1(); // Pick
            Roles();
            ToolTipBoxInSection();

            int n = 1;
            foreach (var emplist in Dict.EmpIDName)
            {
                dataGridView1.Rows.Add(n.ToString(), emplist.Key, emplist.Value);
                n += 1;
            }




        }


        #endregion

        #region Operation Loop Tab 2
        private void AddEmp(string st)
        {

            if (Dict.EmpIDName.ContainsKey(st))
            {
                if (!empforLine.ContainsKey(st))
                {
                    int rownb = dataGridViewSection.Rows.Count;
                    string a = Dict.EmpIDName[st];
                    dataGridViewSection.Rows.Add((rownb + 1).ToString(), (st), a, 1, 1);
                    empforLine.Add((st), Dict.EmpIDName[st]);
                    //***************************************************
                    int r = dataGridViewSection.Rows.Count;
                    for (int i = 0; i < r; i++)
                    {
                        try
                        {
                            DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)dataGridViewSection["Responsibility", i];
                            int index = dcc.Items.IndexOf(dcc.Value);
                            DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)dataGridViewSection["ResponsibilityForOT", i];
                            int index1 = dcc1.Items.IndexOf(dcc.Value);
                        }
                        catch (Exception)
                        {
                            dataGridViewSection.Rows[i].Cells["Responsibility"].Value = (dataGridViewSection.Rows[i].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[0];
                            dataGridViewSection.Rows[i].Cells["ResponsibilityForOT"].Value = (dataGridViewSection.Rows[i].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[0];
                        }

                    }
                }

            }
        }

        private void RemoveEmp()
        {
            if (dataGridViewSection.Rows.Count > 0)
            {
                registIndex = dataGridViewSection.CurrentRow.Index;
                string st = dataGridViewSection.Rows[registIndex].Cells[1].Value.ToString();
                dataGridViewSection.Rows.RemoveAt(registIndex);
                empforLine.Remove(st);
                int rownb = dataGridViewSection.Rows.Count;
                for (int i = 0; i < rownb; i++)
                {
                    dataGridViewSection.Rows[i].Cells[0].Value = (i + 1).ToString();
                }
            }
        }



        private void LoadSectionMember1()
        {
            if (CmbSection.SelectedIndex > -1 && CmbShift.SelectedIndex > -1)
            {
                string Sectionkey = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Key;
                string Shiftkey = ((KeyValuePair<string, string>)CmbShift.SelectedItem).Key;
                dataGridViewSection.CellValueChanged -= dataGridViewSection_CellValueChanged;
                SqlClass sql = new SqlClass();
                if (sql.LoadSectionMemberSQL(Sectionkey, Shiftkey))
                {
                    DataTable dt1 = sql.Datatable;
                    while (dataGridViewSection.Rows.Count > 1)
                    {
                        dataGridViewSection.Rows.RemoveAt(0);
                    }
                    empforLine.Clear();
                    dataGridViewSection.Rows.Clear();
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string id = Convert.ToString(dt1.Rows[i][0]);
                            string fullname = Convert.ToString(dt1.Rows[i][1]);
                            string rate = Convert.ToString(dt1.Rows[i][3]);
                            string rateOT = Convert.ToString(dt1.Rows[i][5]);
                            empforLine.Add(id, fullname);

                            dataGridViewSection.Rows.Add((i + 1).ToString(), id, fullname, rate, rateOT);
                            int respon = Convert.ToInt32(dt1.Rows[i][2]);
                            dataGridViewSection.Rows[i].Cells["Responsibility"].Value = (dataGridViewSection.Rows[i].Cells["Responsibility"] as DataGridViewComboBoxCell).Items[respon];
                            int responForOT = Convert.ToInt32(dt1.Rows[i][4]);
                            dataGridViewSection.Rows[i].Cells["ResponsibilityForOT"].Value = (dataGridViewSection.Rows[i].Cells["ResponsibilityForOT"] as DataGridViewComboBoxCell).Items[responForOT];
                        }
                    }
                }
                dataGridViewSection.CellValueChanged += dataGridViewSection_CellValueChanged;
            }
        }


        private void MPregisterInSection()
        {

            string Sectionkey = ((KeyValuePair<string, string>)CmbSection.SelectedItem).Key;
            string Shiftkey = ((KeyValuePair<string, string>)CmbShift.SelectedItem).Key;

            if (CmbDay.SelectedItem == null)
            {
                MessageBox.Show("Please select your work center name before Regists", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            var sb = new StringBuilder();
            sb.AppendFormat("delete from Emp_SectionMemberTable where shiftAB = '{0}' and SectionCode = '{1}' \n\r", Shiftkey, Sectionkey);
            if (dataGridViewSection.Rows.Count > 0)
            {
                sb.Append("insert into Emp_SectionMemberTable ([SectionCode],[userID],[ShiftAB],[functionId],[Rate] ,[functionOTId],[RateOT],[DivisionID],[PlantID]) values \n");
                for (int i = 0; i < empforLine.Count; i++)
                {
                    string rate;
                    string rateOT;
                    string id = Convert.ToString(dataGridViewSection.Rows[i].Cells[0 + 1].Value);
                    try
                    {
                        rate = Convert.ToString(dataGridViewSection.Rows[i].Cells[2 + 1].Value);
                        double rate1 = double.Parse(rate);
                        rate = rate1.ToString();
                        rateOT = Convert.ToString(dataGridViewSection.Rows[i].Cells[2 + 1].Value);
                        double rate1OT = double.Parse(rateOT);
                        rateOT = rate1OT.ToString();
                    }
                    catch (Exception)
                    {
                        rate = "0.0";
                        rateOT = "0.0";
                    }

                    DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)dataGridViewSection["Responsibility", i];
                    int Responsibility = dcc.Items.IndexOf(dcc.Value);
                    DataGridViewComboBoxCell dcc1 = (DataGridViewComboBoxCell)dataGridViewSection["ResponsibilityForOT", i];
                    int ResponsibilityForOT = dcc1.Items.IndexOf(dcc1.Value);

                    string div = User.Division;
                    string plant = User.Plant;

                    sb.AppendFormat($"('{Sectionkey}',{id},'{Shiftkey}',{Responsibility},{rate} ,{ResponsibilityForOT} ,{rateOT},'{div}','{plant}' ) \n");
                    if (i < empforLine.Count - 1) sb.Append(",");


                }
            }
            SqlClass sql = new SqlClass();
            sql.SectionMenberRegistrationSQL(sb.ToString());

            MessageBox.Show("Registe name list in Production line Completed ", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }



        private void ToolTipBoxInSection()
        {
            ToolTip toolTip1 = new ToolTip
            {
                UseFading = true,
                UseAnimation = true,
                IsBalloon = true,
                // Set up the delays for the ToolTip.
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                // Force the ToolTip text to be displayed whether or not the form is active.
                ShowAlways = true
            };

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.TbEmpID, "กรอก รหัสพนักงาน 7 ตัว หากต้องการเพิ่มจำนวนพนักงาน");
            toolTip1.SetToolTip(this.btnSAVE, "บันทึกข้อมูล");
            toolTip1.SetToolTip(this.btnRemove, "ลบข้อมูล ออกจากแฟ้มในหน่วยงาน");
            toolTip1.SetToolTip(this.dataGridView1, "Double click เพื่อคัดลอกรายชื่อเข้าแฟ้มหน่วยงาน");

        }







        #endregion

        #endregion


    }
}
