
namespace KPI.ProdForm
{
    partial class ProductionApprovedForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.BtnAppHis = new System.Windows.Forms.Button();
            this.DgvListDay = new System.Windows.Forms.DataGridView();
            this.DgvListNight = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.ChartTotal = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tpanelChartPartNumber = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnAM = new System.Windows.Forms.Button();
            this.Dtp_Select = new System.Windows.Forms.DateTimePicker();
            this.BtnTL = new System.Windows.Forms.Button();
            this.CmbDayNight = new System.Windows.Forms.ComboBox();
            this.TLstatus = new System.Windows.Forms.Label();
            this.AMstatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnExcel = new System.Windows.Forms.Button();
            this.CmbShift = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LLstatus = new System.Windows.Forms.Label();
            this.BtnLL = new System.Windows.Forms.Button();
            this.BtnUnRegist = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListNight)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTotal)).BeginInit();
            this.tpanelChartPartNumber.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnAppHis
            // 
            this.BtnAppHis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnAppHis.Location = new System.Drawing.Point(800, 16);
            this.BtnAppHis.Margin = new System.Windows.Forms.Padding(0);
            this.BtnAppHis.Name = "BtnAppHis";
            this.BtnAppHis.Size = new System.Drawing.Size(100, 38);
            this.BtnAppHis.TabIndex = 2;
            this.BtnAppHis.Text = "History";
            this.BtnAppHis.UseVisualStyleBackColor = true;
            this.BtnAppHis.Visible = false;
            // 
            // DgvListDay
            // 
            this.DgvListDay.AllowUserToAddRows = false;
            this.DgvListDay.AllowUserToDeleteRows = false;
            this.DgvListDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvListDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvListDay.Location = new System.Drawing.Point(3, 28);
            this.DgvListDay.Name = "DgvListDay";
            this.DgvListDay.ReadOnly = true;
            this.DgvListDay.Size = new System.Drawing.Size(850, 390);
            this.DgvListDay.TabIndex = 3;
            // 
            // DgvListNight
            // 
            this.DgvListNight.AllowUserToAddRows = false;
            this.DgvListNight.AllowUserToDeleteRows = false;
            this.DgvListNight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvListNight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvListNight.Location = new System.Drawing.Point(859, 28);
            this.DgvListNight.Name = "DgvListNight";
            this.DgvListNight.ReadOnly = true;
            this.DgvListNight.Size = new System.Drawing.Size(850, 390);
            this.DgvListNight.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.DgvListNight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.DgvListDay, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 347);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1712, 421);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(850, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Production Today : SHIFT A";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(859, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(850, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Production Today : SHIFT B";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1718, 771);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel6.Controls.Add(this.ChartTotal, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tpanelChartPartNumber, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 63);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1712, 278);
            this.tableLayoutPanel6.TabIndex = 34;
            // 
            // ChartTotal
            // 
            this.ChartTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(200)))), ((int)(((byte)(228)))));
            this.ChartTotal.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            this.ChartTotal.BorderlineColor = System.Drawing.Color.Black;
            this.ChartTotal.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.ChartTotal.BorderlineWidth = 2;
            chartArea1.AxisX.Title = "DATE ";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.IsMarksNextToAxis = false;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.Title = "PCS";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.LineColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.ChartTotal.ChartAreas.Add(chartArea1);
            this.ChartTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            legend1.BorderColor = System.Drawing.Color.Black;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.ChartTotal.Legends.Add(legend1);
            this.ChartTotal.Location = new System.Drawing.Point(0, 0);
            this.ChartTotal.Margin = new System.Windows.Forms.Padding(0);
            this.ChartTotal.Name = "ChartTotal";
            series1.BorderColor = System.Drawing.Color.Black;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series1.Color = System.Drawing.Color.Transparent;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Label = "99";
            series1.Legend = "Legend1";
            series1.Name = "Sixloss";
            series1.ShadowColor = System.Drawing.Color.White;
            this.ChartTotal.Series.Add(series1);
            this.ChartTotal.Size = new System.Drawing.Size(1562, 278);
            this.ChartTotal.TabIndex = 33;
            this.ChartTotal.Text = "chart3";
            // 
            // tpanelChartPartNumber
            // 
            this.tpanelChartPartNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tpanelChartPartNumber.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tpanelChartPartNumber.ColumnCount = 1;
            this.tpanelChartPartNumber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpanelChartPartNumber.Controls.Add(this.label10, 0, 0);
            this.tpanelChartPartNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpanelChartPartNumber.Location = new System.Drawing.Point(1562, 0);
            this.tpanelChartPartNumber.Margin = new System.Windows.Forms.Padding(0);
            this.tpanelChartPartNumber.Name = "tpanelChartPartNumber";
            this.tpanelChartPartNumber.RowCount = 2;
            this.tpanelChartPartNumber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpanelChartPartNumber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpanelChartPartNumber.Size = new System.Drawing.Size(150, 278);
            this.tpanelChartPartNumber.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(92)))), ((int)(((byte)(174)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(3, 3);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "PART NUMBER";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 11;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 912F));
            this.tableLayoutPanel4.Controls.Add(this.BtnAM, 6, 1);
            this.tableLayoutPanel4.Controls.Add(this.BtnAppHis, 8, 1);
            this.tableLayoutPanel4.Controls.Add(this.Dtp_Select, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.BtnTL, 5, 1);
            this.tableLayoutPanel4.Controls.Add(this.CmbDayNight, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.TLstatus, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.AMstatus, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label7, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.BtnExcel, 9, 1);
            this.tableLayoutPanel4.Controls.Add(this.CmbShift, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.LLstatus, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.BtnLL, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.BtnUnRegist, 3, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1712, 54);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // BtnAM
            // 
            this.BtnAM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnAM.Location = new System.Drawing.Point(600, 16);
            this.BtnAM.Margin = new System.Windows.Forms.Padding(0);
            this.BtnAM.Name = "BtnAM";
            this.BtnAM.Size = new System.Drawing.Size(100, 38);
            this.BtnAM.TabIndex = 6;
            this.BtnAM.Text = "AM Approve";
            this.BtnAM.UseVisualStyleBackColor = true;
            this.BtnAM.Click += new System.EventHandler(this.BtnAM_Click);
            // 
            // Dtp_Select
            // 
            this.Dtp_Select.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dtp_Select.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_Select.Location = new System.Drawing.Point(3, 19);
            this.Dtp_Select.Name = "Dtp_Select";
            this.Dtp_Select.Size = new System.Drawing.Size(94, 20);
            this.Dtp_Select.TabIndex = 0;
            this.Dtp_Select.ValueChanged += new System.EventHandler(this.Dtp_Select_ValueChanged);
            // 
            // BtnTL
            // 
            this.BtnTL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnTL.Location = new System.Drawing.Point(500, 16);
            this.BtnTL.Margin = new System.Windows.Forms.Padding(0);
            this.BtnTL.Name = "BtnTL";
            this.BtnTL.Size = new System.Drawing.Size(100, 38);
            this.BtnTL.TabIndex = 6;
            this.BtnTL.Text = "TL Approve";
            this.BtnTL.UseVisualStyleBackColor = true;
            this.BtnTL.Click += new System.EventHandler(this.BtnTL_Click);
            // 
            // CmbDayNight
            // 
            this.CmbDayNight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbDayNight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDayNight.FormattingEnabled = true;
            this.CmbDayNight.Items.AddRange(new object[] {
            "DAY",
            "NIGHT"});
            this.CmbDayNight.Location = new System.Drawing.Point(103, 19);
            this.CmbDayNight.Name = "CmbDayNight";
            this.CmbDayNight.Size = new System.Drawing.Size(94, 21);
            this.CmbDayNight.TabIndex = 1;
            // 
            // TLstatus
            // 
            this.TLstatus.AutoSize = true;
            this.TLstatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TLstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLstatus.Location = new System.Drawing.Point(503, 0);
            this.TLstatus.Name = "TLstatus";
            this.TLstatus.Size = new System.Drawing.Size(94, 16);
            this.TLstatus.TabIndex = 7;
            this.TLstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AMstatus
            // 
            this.AMstatus.AutoSize = true;
            this.AMstatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AMstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AMstatus.Location = new System.Drawing.Point(603, 0);
            this.AMstatus.Name = "AMstatus";
            this.AMstatus.Size = new System.Drawing.Size(94, 16);
            this.AMstatus.TabIndex = 7;
            this.AMstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Select date";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(103, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Day-Night";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnExcel
            // 
            this.BtnExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExcel.Location = new System.Drawing.Point(900, 16);
            this.BtnExcel.Margin = new System.Windows.Forms.Padding(0);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(100, 38);
            this.BtnExcel.TabIndex = 8;
            this.BtnExcel.Text = "Export Excel";
            this.BtnExcel.UseVisualStyleBackColor = true;
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // CmbShift
            // 
            this.CmbShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbShift.FormattingEnabled = true;
            this.CmbShift.Items.AddRange(new object[] {
            "AUTO",
            "SHIFT A",
            "SHIFT B"});
            this.CmbShift.Location = new System.Drawing.Point(203, 19);
            this.CmbShift.Name = "CmbShift";
            this.CmbShift.Size = new System.Drawing.Size(94, 21);
            this.CmbShift.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(203, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Work SHIFT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LLstatus
            // 
            this.LLstatus.AutoSize = true;
            this.LLstatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LLstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LLstatus.Location = new System.Drawing.Point(403, 0);
            this.LLstatus.Name = "LLstatus";
            this.LLstatus.Size = new System.Drawing.Size(94, 16);
            this.LLstatus.TabIndex = 7;
            this.LLstatus.Text = "                  ";
            this.LLstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnLL
            // 
            this.BtnLL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnLL.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnLL.Location = new System.Drawing.Point(400, 16);
            this.BtnLL.Margin = new System.Windows.Forms.Padding(0);
            this.BtnLL.Name = "BtnLL";
            this.BtnLL.Size = new System.Drawing.Size(100, 38);
            this.BtnLL.TabIndex = 6;
            this.BtnLL.Text = "LL Collection data";
            this.BtnLL.UseVisualStyleBackColor = true;
            this.BtnLL.Click += new System.EventHandler(this.BtnLL_Click);
            // 
            // BtnUnRegist
            // 
            this.BtnUnRegist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnUnRegist.Location = new System.Drawing.Point(300, 16);
            this.BtnUnRegist.Margin = new System.Windows.Forms.Padding(0);
            this.BtnUnRegist.Name = "BtnUnRegist";
            this.BtnUnRegist.Size = new System.Drawing.Size(100, 38);
            this.BtnUnRegist.TabIndex = 11;
            this.BtnUnRegist.Text = "Delete";
            this.BtnUnRegist.UseVisualStyleBackColor = true;
            this.BtnUnRegist.Click += new System.EventHandler(this.BtnUnRegist_Click);
            // 
            // ProductionApprovedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1718, 771);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "ProductionApprovedForm";
            this.Text = "ProductionApprovedForm";
            this.Load += new System.EventHandler(this.ProductionApprovedForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvListDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListNight)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartTotal)).EndInit();
            this.tpanelChartPartNumber.ResumeLayout(false);
            this.tpanelChartPartNumber.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnAppHis;
        private System.Windows.Forms.DataGridView DgvListDay;
        private System.Windows.Forms.DataGridView DgvListNight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DateTimePicker Dtp_Select;
        private System.Windows.Forms.ComboBox CmbDayNight;
        private System.Windows.Forms.Button BtnAM;
        private System.Windows.Forms.Button BtnTL;
        private System.Windows.Forms.Button BtnLL;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartTotal;
        private System.Windows.Forms.TableLayoutPanel tpanelChartPartNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LLstatus;
        private System.Windows.Forms.Label TLstatus;
        private System.Windows.Forms.Label AMstatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnExcel;
        private System.Windows.Forms.ComboBox CmbShift;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnUnRegist;
    }
}