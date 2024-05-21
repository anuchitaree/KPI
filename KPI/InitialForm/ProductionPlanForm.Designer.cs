
namespace KPI.InitialForm
{
    partial class ProductionPlanForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbPlan = new System.Windows.Forms.Label();
            this.BntImportError = new System.Windows.Forms.Button();
            this.BtnSavePlan = new System.Windows.Forms.Button();
            this.BtnPNRead = new System.Windows.Forms.Button();
            this.BtnDeletePlan = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DataGridViewPN = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.BtnReadPN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnClearPlan = new System.Windows.Forms.Button();
            this.DgvPlan = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnCal = new System.Windows.Forms.Button();
            this.CmbYear = new System.Windows.Forms.ComboBox();
            this.CmbMonth = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel100 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewPN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPlan)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel100.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPlan
            // 
            this.lbPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPlan.ForeColor = System.Drawing.Color.Black;
            this.lbPlan.Location = new System.Drawing.Point(3, 26);
            this.lbPlan.Name = "lbPlan";
            this.lbPlan.Size = new System.Drawing.Size(100, 23);
            this.lbPlan.TabIndex = 1;
            this.lbPlan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BntImportError
            // 
            this.BntImportError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BntImportError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BntImportError.Location = new System.Drawing.Point(415, 56);
            this.BntImportError.Name = "BntImportError";
            this.BntImportError.Size = new System.Drawing.Size(97, 47);
            this.BntImportError.TabIndex = 7;
            this.BntImportError.Text = "Import Fr.Excel";
            this.BntImportError.UseVisualStyleBackColor = true;
            this.BntImportError.Click += new System.EventHandler(this.BtnImportError_Click);
            // 
            // BtnSavePlan
            // 
            this.BtnSavePlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnSavePlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSavePlan.ForeColor = System.Drawing.Color.Black;
            this.BtnSavePlan.Location = new System.Drawing.Point(3, 56);
            this.BtnSavePlan.Name = "BtnSavePlan";
            this.BtnSavePlan.Size = new System.Drawing.Size(98, 47);
            this.BtnSavePlan.TabIndex = 5;
            this.BtnSavePlan.Text = "Save";
            this.BtnSavePlan.UseVisualStyleBackColor = true;
            this.BtnSavePlan.Click += new System.EventHandler(this.BtnSavePlan_Click);
            // 
            // BtnPNRead
            // 
            this.BtnPNRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnPNRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPNRead.ForeColor = System.Drawing.Color.Black;
            this.BtnPNRead.Location = new System.Drawing.Point(415, 3);
            this.BtnPNRead.Name = "BtnPNRead";
            this.BtnPNRead.Size = new System.Drawing.Size(97, 47);
            this.BtnPNRead.TabIndex = 5;
            this.BtnPNRead.Text = "Read from \r\nProd.Plan";
            this.BtnPNRead.UseVisualStyleBackColor = true;
            this.BtnPNRead.Click += new System.EventHandler(this.BtnPNRead_Click);
            // 
            // BtnDeletePlan
            // 
            this.BtnDeletePlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDeletePlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeletePlan.Location = new System.Drawing.Point(138, 56);
            this.BtnDeletePlan.Name = "BtnDeletePlan";
            this.BtnDeletePlan.Size = new System.Drawing.Size(98, 47);
            this.BtnDeletePlan.TabIndex = 28;
            this.BtnDeletePlan.Text = "Delete \r\n(No Save)";
            this.BtnDeletePlan.UseVisualStyleBackColor = true;
            this.BtnDeletePlan.Click += new System.EventHandler(this.BtnDeletePlan_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.DataGridViewPN, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbSection, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(521, 623);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // DataGridViewPN
            // 
            this.DataGridViewPN.AllowUserToAddRows = false;
            this.DataGridViewPN.AllowUserToDeleteRows = false;
            this.DataGridViewPN.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DataGridViewPN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewPN.Location = new System.Drawing.Point(3, 196);
            this.DataGridViewPN.Name = "DataGridViewPN";
            this.DataGridViewPN.ReadOnly = true;
            this.DataGridViewPN.Size = new System.Drawing.Size(515, 424);
            this.DataGridViewPN.TabIndex = 0;
            this.DataGridViewPN.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewPN_CellDoubleClick);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tableLayoutPanel6.ColumnCount = 7;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4051F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.189046F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4051F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.851852F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.18518F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.37037F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4051F));
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 78);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(515, 112);
            this.tableLayoutPanel6.TabIndex = 32;
            // 
            // cmbSection
            // 
            this.cmbSection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbSection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(3, 48);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(515, 26);
            this.cmbSection.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(235)))), ((int)(((byte)(187)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(515, 25);
            this.label14.TabIndex = 1;
            this.label14.Text = "Yearly Standard NetTime";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(515, 20);
            this.label15.TabIndex = 33;
            this.label15.Text = "Select SectionCode";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnReadPN
            // 
            this.BtnReadPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnReadPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReadPN.ForeColor = System.Drawing.Color.Black;
            this.BtnReadPN.Location = new System.Drawing.Point(3, 3);
            this.BtnReadPN.Name = "BtnReadPN";
            this.BtnReadPN.Size = new System.Drawing.Size(98, 47);
            this.BtnReadPN.TabIndex = 5;
            this.BtnReadPN.Text = "Read From \r\nNetTime TB";
            this.BtnReadPN.UseVisualStyleBackColor = true;
            this.BtnReadPN.Click += new System.EventHandler(this.BtnReadPN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(2, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Plan (PCS)";
            // 
            // BtnClearPlan
            // 
            this.BtnClearPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClearPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearPlan.Location = new System.Drawing.Point(281, 56);
            this.BtnClearPlan.Name = "BtnClearPlan";
            this.BtnClearPlan.Size = new System.Drawing.Size(98, 47);
            this.BtnClearPlan.TabIndex = 27;
            this.BtnClearPlan.Text = "Clear";
            this.BtnClearPlan.UseVisualStyleBackColor = true;
            this.BtnClearPlan.Click += new System.EventHandler(this.BtnClearPlan_Click);
            // 
            // DgvPlan
            // 
            this.DgvPlan.AllowUserToAddRows = false;
            this.DgvPlan.AllowUserToDeleteRows = false;
            this.DgvPlan.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DgvPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvPlan.Location = new System.Drawing.Point(3, 198);
            this.DgvPlan.Name = "DgvPlan";
            this.DgvPlan.Size = new System.Drawing.Size(515, 422);
            this.DgvPlan.TabIndex = 0;
            this.DgvPlan.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridViewPlan_CellBeginEdit);
            this.DgvPlan.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewPlan_CellValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(515, 20);
            this.label4.TabIndex = 33;
            this.label4.Text = "Select Machine Number";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(235)))), ((int)(((byte)(187)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(515, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Production Monthly Plan Editor";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4051F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.189046F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4051F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.689422F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4051F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.925926F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.81482F));
            this.tableLayoutPanel4.Controls.Add(this.BtnReadPN, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.BntImportError, 6, 1);
            this.tableLayoutPanel4.Controls.Add(this.BtnSavePlan, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.BtnPNRead, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.BtnDeletePlan, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.BtnClearPlan, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel1, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.BtnCal, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 86);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(515, 106);
            this.tableLayoutPanel4.TabIndex = 32;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbPlan);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(281, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(98, 47);
            this.panel1.TabIndex = 30;
            // 
            // BtnCal
            // 
            this.BtnCal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnCal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCal.Location = new System.Drawing.Point(138, 3);
            this.BtnCal.Name = "BtnCal";
            this.BtnCal.Size = new System.Drawing.Size(98, 47);
            this.BtnCal.TabIndex = 31;
            this.BtnCal.Text = "Calculate";
            this.BtnCal.UseVisualStyleBackColor = true;
            this.BtnCal.Click += new System.EventHandler(this.BtnCal_Click);
            // 
            // CmbYear
            // 
            this.CmbYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CmbYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbYear.Items.AddRange(new object[] {
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030"});
            this.CmbYear.Location = new System.Drawing.Point(3, 3);
            this.CmbYear.Name = "CmbYear";
            this.CmbYear.Size = new System.Drawing.Size(251, 26);
            this.CmbYear.TabIndex = 11;
            this.CmbYear.SelectedIndexChanged += new System.EventHandler(this.CmbYear_SelectedIndexChanged);
            // 
            // CmbMonth
            // 
            this.CmbMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CmbMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.CmbMonth.Location = new System.Drawing.Point(260, 3);
            this.CmbMonth.Name = "CmbMonth";
            this.CmbMonth.Size = new System.Drawing.Size(252, 26);
            this.CmbMonth.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.CmbYear, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.CmbMonth, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 48);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(515, 32);
            this.tableLayoutPanel2.TabIndex = 29;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.DgvPlan, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(530, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(521, 623);
            this.tableLayoutPanel3.TabIndex = 26;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1054, 629);
            this.tableLayoutPanel5.TabIndex = 28;
            // 
            // tableLayoutPanel100
            // 
            this.tableLayoutPanel100.ColumnCount = 1;
            this.tableLayoutPanel100.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel100.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel100.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel100.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel100.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel100.Name = "tableLayoutPanel100";
            this.tableLayoutPanel100.RowCount = 2;
            this.tableLayoutPanel100.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel100.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel100.Size = new System.Drawing.Size(1060, 659);
            this.tableLayoutPanel100.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1054, 24);
            this.label1.TabIndex = 29;
            this.label1.Text = "Production Monthly Plan";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // ProductionPlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 659);
            this.Controls.Add(this.tableLayoutPanel100);
            this.Name = "ProductionPlanForm";
            this.Text = "ProductionPlanForm";
            this.Load += new System.EventHandler(this.ProductionPlanForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewPN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPlan)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel100.ResumeLayout(false);
            this.tableLayoutPanel100.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbPlan;
        private System.Windows.Forms.Button BntImportError;
        private System.Windows.Forms.Button BtnSavePlan;
        private System.Windows.Forms.Button BtnPNRead;
        private System.Windows.Forms.Button BtnDeletePlan;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView DataGridViewPN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button BtnReadPN;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnClearPlan;
        private System.Windows.Forms.DataGridView DgvPlan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox CmbYear;
        private System.Windows.Forms.ComboBox CmbMonth;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel100;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCal;
    }
}