
namespace KPI.ProdForm
{
    partial class TracebilityScannerSettingForm
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
            this.BtnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.CmbPort = new System.Windows.Forms.ComboBox();
            this.CmbStop = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CmbData = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbParity = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbRate = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(256, 178);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 34);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnRefresh);
            this.groupBox1.Controls.Add(this.CmbPort);
            this.groupBox1.Controls.Add(this.CmbStop);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.CmbData);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CmbParity);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CmbRate);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 235);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "COM Serial Setting";
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(256, 19);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(75, 30);
            this.BtnRefresh.TabIndex = 2;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // CmbPort
            // 
            this.CmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPort.FormattingEnabled = true;
            this.CmbPort.Location = new System.Drawing.Point(89, 19);
            this.CmbPort.Name = "CmbPort";
            this.CmbPort.Size = new System.Drawing.Size(151, 24);
            this.CmbPort.TabIndex = 1;
            this.CmbPort.SelectedIndexChanged += new System.EventHandler(this.CmbPort_SelectedIndexChanged);
            // 
            // CmbStop
            // 
            this.CmbStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmbStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbStop.FormattingEnabled = true;
            this.CmbStop.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.CmbStop.Location = new System.Drawing.Point(89, 189);
            this.CmbStop.Name = "CmbStop";
            this.CmbStop.Size = new System.Drawing.Size(151, 24);
            this.CmbStop.TabIndex = 1;
            this.CmbStop.SelectedIndexChanged += new System.EventHandler(this.CmbStop_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Data Bits.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Parity.";
            // 
            // CmbData
            // 
            this.CmbData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmbData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbData.FormattingEnabled = true;
            this.CmbData.Items.AddRange(new object[] {
            "9",
            "8",
            "7"});
            this.CmbData.Location = new System.Drawing.Point(89, 143);
            this.CmbData.Name = "CmbData";
            this.CmbData.Size = new System.Drawing.Size(151, 24);
            this.CmbData.TabIndex = 1;
            this.CmbData.SelectedIndexChanged += new System.EventHandler(this.CmbData_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Stop Bits.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Baud Rate.";
            // 
            // CmbParity
            // 
            this.CmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbParity.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmbParity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbParity.FormattingEnabled = true;
            this.CmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.CmbParity.Location = new System.Drawing.Point(89, 101);
            this.CmbParity.Name = "CmbParity";
            this.CmbParity.Size = new System.Drawing.Size(151, 24);
            this.CmbParity.TabIndex = 1;
            this.CmbParity.SelectedIndexChanged += new System.EventHandler(this.CmbParity_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM Port.";
            // 
            // CmbRate
            // 
            this.CmbRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbRate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmbRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbRate.FormattingEnabled = true;
            this.CmbRate.Items.AddRange(new object[] {
            "115200",
            "57600",
            "38400",
            "19200",
            "9600",
            "4800",
            "2400",
            "1200"});
            this.CmbRate.Location = new System.Drawing.Point(89, 61);
            this.CmbRate.Name = "CmbRate";
            this.CmbRate.Size = new System.Drawing.Size(151, 24);
            this.CmbRate.TabIndex = 1;
            this.CmbRate.SelectedIndexChanged += new System.EventHandler(this.CmbRate_SelectedIndexChanged);
            // 
            // TracebilityScannerSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 250);
            this.Controls.Add(this.groupBox1);
            this.Name = "TracebilityScannerSettingForm";
            this.Text = "TracebilityScannerSettingForm";
            this.Load += new System.EventHandler(this.TracebilityScannerSettingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.ComboBox CmbPort;
        private System.Windows.Forms.ComboBox CmbStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CmbData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CmbParity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbRate;
    }
}