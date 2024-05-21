
namespace KPI.OptionalForm
{
    partial class StockForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.BtnPort1 = new System.Windows.Forms.Button();
            this.lbSerial1 = new System.Windows.Forms.Label();
            this.Message1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.BtnPort2 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lbSerial2 = new System.Windows.Forms.Label();
            this.Message2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DgvDataInput = new System.Windows.Forms.DataGridView();
            this.ChartStockMonitor = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDataInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartStockMonitor)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnPort1
            // 
            this.BtnPort1.Location = new System.Drawing.Point(14, 16);
            this.BtnPort1.Name = "BtnPort1";
            this.BtnPort1.Size = new System.Drawing.Size(69, 41);
            this.BtnPort1.TabIndex = 2;
            this.BtnPort1.Text = "Ready";
            this.BtnPort1.UseVisualStyleBackColor = true;
            this.BtnPort1.Click += new System.EventHandler(this.BtnPort1_Click);
            // 
            // lbSerial1
            // 
            this.lbSerial1.AutoSize = true;
            this.lbSerial1.BackColor = System.Drawing.Color.White;
            this.lbSerial1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSerial1.Location = new System.Drawing.Point(89, 12);
            this.lbSerial1.Name = "lbSerial1";
            this.lbSerial1.Size = new System.Drawing.Size(56, 16);
            this.lbSerial1.TabIndex = 4;
            this.lbSerial1.Text = "Setting1";
            // 
            // Message1
            // 
            this.Message1.AutoSize = true;
            this.Message1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message1.Location = new System.Drawing.Point(82, 38);
            this.Message1.Name = "Message1";
            this.Message1.Size = new System.Drawing.Size(116, 16);
            this.Message1.TabIndex = 4;
            this.Message1.Text = "part number name";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // BtnPort2
            // 
            this.BtnPort2.Location = new System.Drawing.Point(14, 19);
            this.BtnPort2.Name = "BtnPort2";
            this.BtnPort2.Size = new System.Drawing.Size(69, 34);
            this.BtnPort2.TabIndex = 2;
            this.BtnPort2.Text = "Ready";
            this.BtnPort2.UseVisualStyleBackColor = true;
            this.BtnPort2.Click += new System.EventHandler(this.BtnPort2_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // lbSerial2
            // 
            this.lbSerial2.AutoSize = true;
            this.lbSerial2.BackColor = System.Drawing.Color.White;
            this.lbSerial2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSerial2.Location = new System.Drawing.Point(89, 14);
            this.lbSerial2.Name = "lbSerial2";
            this.lbSerial2.Size = new System.Drawing.Size(56, 16);
            this.lbSerial2.TabIndex = 8;
            this.lbSerial2.Text = "Setting2";
            // 
            // Message2
            // 
            this.Message2.AutoSize = true;
            this.Message2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message2.Location = new System.Drawing.Point(85, 37);
            this.Message2.Name = "Message2";
            this.Message2.Size = new System.Drawing.Size(116, 16);
            this.Message2.TabIndex = 4;
            this.Message2.Text = "part number name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnPort1);
            this.groupBox1.Controls.Add(this.lbSerial1);
            this.groupBox1.Controls.Add(this.Message1);
            this.groupBox1.Location = new System.Drawing.Point(9, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 60);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnPort2);
            this.groupBox2.Controls.Add(this.lbSerial2);
            this.groupBox2.Controls.Add(this.Message2);
            this.groupBox2.Location = new System.Drawing.Point(9, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 59);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Serial Port 2";
            // 
            // DgvDataInput
            // 
            this.DgvDataInput.AllowUserToAddRows = false;
            this.DgvDataInput.AllowUserToDeleteRows = false;
            this.DgvDataInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDataInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvDataInput.Location = new System.Drawing.Point(1253, 3);
            this.DgvDataInput.Name = "DgvDataInput";
            this.DgvDataInput.ReadOnly = true;
            this.DgvDataInput.Size = new System.Drawing.Size(444, 488);
            this.DgvDataInput.TabIndex = 10;
            // 
            // ChartStockMonitor
            // 
            this.ChartStockMonitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(200)))), ((int)(((byte)(228)))));
            this.ChartStockMonitor.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            this.ChartStockMonitor.BorderlineColor = System.Drawing.Color.Black;
            this.ChartStockMonitor.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.ChartStockMonitor.BorderlineWidth = 2;
            chartArea4.AxisX.Title = "Part number";
            chartArea4.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea4.AxisX2.IsMarksNextToAxis = false;
            chartArea4.AxisX2.LineColor = System.Drawing.Color.White;
            chartArea4.AxisY.Title = "PCS";
            chartArea4.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea4.AxisY2.LineColor = System.Drawing.Color.White;
            chartArea4.BackColor = System.Drawing.Color.White;
            chartArea4.Name = "ChartArea1";
            this.ChartStockMonitor.ChartAreas.Add(chartArea4);
            legend4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            legend4.BorderColor = System.Drawing.Color.Black;
            legend4.Enabled = false;
            legend4.Name = "Legend1";
            this.ChartStockMonitor.Legends.Add(legend4);
            this.ChartStockMonitor.Location = new System.Drawing.Point(6, 3);
            this.ChartStockMonitor.Margin = new System.Windows.Forms.Padding(0);
            this.ChartStockMonitor.Name = "ChartStockMonitor";
            series4.BorderColor = System.Drawing.Color.Black;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series4.Color = System.Drawing.Color.Transparent;
            series4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series4.Label = "99";
            series4.Legend = "Legend1";
            series4.Name = "Sixloss";
            series4.ShadowColor = System.Drawing.Color.White;
            this.ChartStockMonitor.Series.Add(series4);
            this.ChartStockMonitor.Size = new System.Drawing.Size(2633, 468);
            this.ChartStockMonitor.TabIndex = 34;
            this.ChartStockMonitor.Text = "chart3";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1706, 743);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DgvDataInput, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1700, 494);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.ChartStockMonitor);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1244, 488);
            this.panel2.TabIndex = 35;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 503);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1700, 237);
            this.panel1.TabIndex = 36;
            // 
            // StockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1706, 743);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "StockForm";
            this.Text = "StockForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StockForm_FormClosing);
            this.Load += new System.EventHandler(this.StockForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDataInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartStockMonitor)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnPort1;
        private System.Windows.Forms.Label lbSerial1;
        private System.Windows.Forms.Label Message1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button BtnPort2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lbSerial2;
        private System.Windows.Forms.Label Message2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView DgvDataInput;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartStockMonitor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}