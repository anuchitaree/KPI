
namespace KPI.ProdForm
{
    partial class ProductionProgressForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductionProgressForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.DgvProgress = new System.Windows.Forms.DataGridView();
            this.CmbYear = new System.Windows.Forms.ComboBox();
            this.CmbMonth = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnExportExcel = new System.Windows.Forms.Button();
            this.BtnExcelForm = new System.Windows.Forms.Button();
            this.BtnCAL = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnRef = new System.Windows.Forms.Button();
            this.BtnNewCal = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.chartProg = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.BtnHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvProgress)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartProg)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvProgress
            // 
            this.DgvProgress.AllowUserToAddRows = false;
            this.DgvProgress.AllowUserToDeleteRows = false;
            this.DgvProgress.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DgvProgress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvProgress.Location = new System.Drawing.Point(3, 299);
            this.DgvProgress.Name = "DgvProgress";
            this.DgvProgress.ReadOnly = true;
            this.DgvProgress.Size = new System.Drawing.Size(1242, 303);
            this.DgvProgress.TabIndex = 1;
            // 
            // CmbYear
            // 
            this.CmbYear.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbYear.FormattingEnabled = true;
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
            "2029"});
            this.CmbYear.Location = new System.Drawing.Point(246, 23);
            this.CmbYear.Name = "CmbYear";
            this.CmbYear.Size = new System.Drawing.Size(144, 28);
            this.CmbYear.TabIndex = 7;
            // 
            // CmbMonth
            // 
            this.CmbMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CmbMonth.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbMonth.FormattingEnabled = true;
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
            this.CmbMonth.Location = new System.Drawing.Point(396, 23);
            this.CmbMonth.Name = "CmbMonth";
            this.CmbMonth.Size = new System.Drawing.Size(124, 28);
            this.CmbMonth.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1520, 416);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Date";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.DgvProgress, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.47826F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.52174F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1248, 605);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.376004F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.22324F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.12713F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.47F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.867418F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.2401F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.376004F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.200047F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.12005F));
            this.tableLayoutPanel2.Controls.Add(this.BtnExportExcel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.CmbYear, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnExcelForm, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.CmbMonth, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnCAL, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnRefresh, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnRef, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnNewCal, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnHelp, 8, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1242, 54);
            this.tableLayoutPanel2.TabIndex = 29;
            // 
            // BtnExportExcel
            // 
            this.BtnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("BtnExportExcel.Image")));
            this.BtnExportExcel.Location = new System.Drawing.Point(3, 3);
            this.BtnExportExcel.Name = "BtnExportExcel";
            this.BtnExportExcel.Size = new System.Drawing.Size(85, 48);
            this.BtnExportExcel.TabIndex = 27;
            this.BtnExportExcel.Text = "Export Excel";
            this.BtnExportExcel.UseVisualStyleBackColor = true;
            this.BtnExportExcel.Click += new System.EventHandler(this.BtnExportExcel_Click);
            // 
            // BtnExcelForm
            // 
            this.BtnExcelForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnExcelForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExcelForm.Location = new System.Drawing.Point(107, 3);
            this.BtnExcelForm.Name = "BtnExcelForm";
            this.BtnExcelForm.Size = new System.Drawing.Size(133, 48);
            this.BtnExcelForm.TabIndex = 28;
            this.BtnExcelForm.Text = "Export Excel Form";
            this.BtnExcelForm.UseVisualStyleBackColor = true;
            this.BtnExcelForm.Visible = false;
            this.BtnExcelForm.Click += new System.EventHandler(this.BtnExcelForm_Click);
            // 
            // BtnCAL
            // 
            this.BtnCAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCAL.Location = new System.Drawing.Point(526, 3);
            this.BtnCAL.Name = "BtnCAL";
            this.BtnCAL.Size = new System.Drawing.Size(102, 48);
            this.BtnCAL.TabIndex = 26;
            this.BtnCAL.Text = "Calculate";
            this.BtnCAL.UseVisualStyleBackColor = true;
            this.BtnCAL.Click += new System.EventHandler(this.BtnCAL_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefresh.Location = new System.Drawing.Point(648, 3);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(120, 48);
            this.BtnRefresh.TabIndex = 29;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnRef
            // 
            this.BtnRef.Location = new System.Drawing.Point(899, 3);
            this.BtnRef.Name = "BtnRef";
            this.BtnRef.Size = new System.Drawing.Size(95, 48);
            this.BtnRef.TabIndex = 30;
            this.BtnRef.Text = "NEW Refresh";
            this.BtnRef.UseVisualStyleBackColor = true;
            this.BtnRef.Click += new System.EventHandler(this.BtnRef_Click);
            // 
            // BtnNewCal
            // 
            this.BtnNewCal.Location = new System.Drawing.Point(1003, 3);
            this.BtnNewCal.Name = "BtnNewCal";
            this.BtnNewCal.Size = new System.Drawing.Size(105, 48);
            this.BtnNewCal.TabIndex = 31;
            this.BtnNewCal.Text = "NEW Calculate";
            this.BtnNewCal.UseVisualStyleBackColor = true;
            this.BtnNewCal.Click += new System.EventHandler(this.BtnNewCal_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1242, 230);
            this.panel2.TabIndex = 29;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.chartProg, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1242, 230);
            this.tableLayoutPanel3.TabIndex = 30;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1142, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(95, 230);
            this.tableLayoutPanel4.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(92)))), ((int)(((byte)(174)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Items";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chartProg
            // 
            this.chartProg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(200)))), ((int)(((byte)(228)))));
            this.chartProg.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            this.chartProg.BorderlineColor = System.Drawing.Color.Black;
            this.chartProg.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartProg.BorderlineWidth = 2;
            chartArea1.AxisX.Title = "BY DATE";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.IsMarksNextToAxis = false;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.Title = "% RATIO";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.LineColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chartProg.ChartAreas.Add(chartArea1);
            this.chartProg.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            legend1.BorderColor = System.Drawing.Color.Black;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartProg.Legends.Add(legend1);
            this.chartProg.Location = new System.Drawing.Point(0, 0);
            this.chartProg.Margin = new System.Windows.Forms.Padding(0);
            this.chartProg.Name = "chartProg";
            series1.BorderColor = System.Drawing.Color.Black;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Transparent;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Label = "99";
            series1.Legend = "Legend1";
            series1.Name = "Sixloss";
            series1.ShadowColor = System.Drawing.Color.White;
            this.chartProg.Series.Add(series1);
            this.chartProg.Size = new System.Drawing.Size(1142, 230);
            this.chartProg.TabIndex = 29;
            this.chartProg.Text = "chart3";
            // 
            // BtnHelp
            // 
            this.BtnHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BtnHelp.Location = new System.Drawing.Point(1117, 3);
            this.BtnHelp.Name = "BtnHelp";
            this.BtnHelp.Size = new System.Drawing.Size(75, 48);
            this.BtnHelp.TabIndex = 32;
            this.BtnHelp.Text = "Help";
            this.BtnHelp.UseVisualStyleBackColor = false;
            this.BtnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
            // 
            // ProductionProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 605);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProductionProgressForm";
            this.Text = "ProductionProgressForm";
            this.Load += new System.EventHandler(this.ProductionProgressForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvProgress)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartProg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvProgress;
        private System.Windows.Forms.ComboBox CmbYear;
        private System.Windows.Forms.ComboBox CmbMonth;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button BtnExportExcel;
        private System.Windows.Forms.Button BtnExcelForm;
        private System.Windows.Forms.Button BtnCAL;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnRef;
        private System.Windows.Forms.Button BtnNewCal;
        private System.Windows.Forms.Button BtnHelp;
    }
}