
namespace KPI.InitialForm
{
    partial class InitialRunForm
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
            this.label9 = new System.Windows.Forms.Label();
            this.BntList = new System.Windows.Forms.Button();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.CmbSection = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BtnInitalQuque = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMonth2 = new System.Windows.Forms.ComboBox();
            this.cmbMonth1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbYear2 = new System.Windows.Forms.ComboBox();
            this.comboBoxStartTable = new System.Windows.Forms.ComboBox();
            this.cmbYear1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonManyUpdate = new System.Windows.Forms.Button();
            this.DateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(477, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "dbo.Raw_workOT";
            // 
            // BntList
            // 
            this.BntList.Location = new System.Drawing.Point(776, 34);
            this.BntList.Name = "BntList";
            this.BntList.Size = new System.Drawing.Size(78, 42);
            this.BntList.TabIndex = 17;
            this.BntList.Text = "List";
            this.BntList.UseVisualStyleBackColor = true;
            this.BntList.Click += new System.EventHandler(this.BntList_Click);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(480, 43);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(203, 20);
            this.dateTimePicker3.TabIndex = 16;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(461, 290);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(393, 257);
            this.dataGridView3.TabIndex = 14;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(461, 88);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(393, 166);
            this.dataGridView2.TabIndex = 15;
            // 
            // CmbSection
            // 
            this.CmbSection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.CmbSection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbSection.FormattingEnabled = true;
            this.CmbSection.Location = new System.Drawing.Point(12, 9);
            this.CmbSection.Name = "CmbSection";
            this.CmbSection.Size = new System.Drawing.Size(387, 28);
            this.CmbSection.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(458, 274);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "dbo.PPAS Table ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 133);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(387, 119);
            this.dataGridView1.TabIndex = 11;
            // 
            // BtnInitalQuque
            // 
            this.BtnInitalQuque.Location = new System.Drawing.Point(332, 50);
            this.BtnInitalQuque.Name = "BtnInitalQuque";
            this.BtnInitalQuque.Size = new System.Drawing.Size(58, 70);
            this.BtnInitalQuque.TabIndex = 3;
            this.BtnInitalQuque.Text = "Generate";
            this.BtnInitalQuque.UseVisualStyleBackColor = true;
            this.BtnInitalQuque.Click += new System.EventHandler(this.BtnInitalQuque_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "สุดท้าย";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "เริ่มตาราง";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "เริ่มต้น";
            // 
            // cmbMonth2
            // 
            this.cmbMonth2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMonth2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonth2.FormattingEnabled = true;
            this.cmbMonth2.Items.AddRange(new object[] {
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
            this.cmbMonth2.Location = new System.Drawing.Point(173, 94);
            this.cmbMonth2.Name = "cmbMonth2";
            this.cmbMonth2.Size = new System.Drawing.Size(153, 24);
            this.cmbMonth2.TabIndex = 9;
            // 
            // cmbMonth1
            // 
            this.cmbMonth1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMonth1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonth1.FormattingEnabled = true;
            this.cmbMonth1.Items.AddRange(new object[] {
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
            this.cmbMonth1.Location = new System.Drawing.Point(174, 57);
            this.cmbMonth1.Name = "cmbMonth1";
            this.cmbMonth1.Size = new System.Drawing.Size(153, 24);
            this.cmbMonth1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(351, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "ใช้สำหรับการ Generate  ข้อมูลพื้นฐาน ในการคำนวน PPAS และ Red front";
            // 
            // cmbYear2
            // 
            this.cmbYear2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbYear2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYear2.FormattingEnabled = true;
            this.cmbYear2.Items.AddRange(new object[] {
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
            "2030",
            "2031",
            "2032",
            "2033",
            "2034",
            "2035"});
            this.cmbYear2.Location = new System.Drawing.Point(64, 94);
            this.cmbYear2.Name = "cmbYear2";
            this.cmbYear2.Size = new System.Drawing.Size(103, 24);
            this.cmbYear2.TabIndex = 10;
            // 
            // comboBoxStartTable
            // 
            this.comboBoxStartTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboBoxStartTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStartTable.FormattingEnabled = true;
            this.comboBoxStartTable.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBoxStartTable.Location = new System.Drawing.Point(61, 27);
            this.comboBoxStartTable.Name = "comboBoxStartTable";
            this.comboBoxStartTable.Size = new System.Drawing.Size(103, 24);
            this.comboBoxStartTable.TabIndex = 10;
            // 
            // cmbYear1
            // 
            this.cmbYear1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbYear1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYear1.FormattingEnabled = true;
            this.cmbYear1.Items.AddRange(new object[] {
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
            "2030",
            "2031",
            "2032",
            "2033",
            "2034",
            "2035"});
            this.cmbYear1.Location = new System.Drawing.Point(63, 60);
            this.cmbYear1.Name = "cmbYear1";
            this.cmbYear1.Size = new System.Drawing.Size(103, 24);
            this.cmbYear1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(300, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "ใช้กรณีเปลี่ยนแปลง ข้อมูลตารางพักเบรค ที่มากกว่า 1 วันทำงาน\r\nมีผลกระทบกับ PPAS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "วันสุดท้าย";
            // 
            // DateTimePickerEnd
            // 
            this.DateTimePickerEnd.Location = new System.Drawing.Point(62, 69);
            this.DateTimePickerEnd.Name = "DateTimePickerEnd";
            this.DateTimePickerEnd.Size = new System.Drawing.Size(200, 20);
            this.DateTimePickerEnd.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "วันเริ่ม";
            // 
            // ButtonManyUpdate
            // 
            this.ButtonManyUpdate.Location = new System.Drawing.Point(284, 33);
            this.ButtonManyUpdate.Name = "ButtonManyUpdate";
            this.ButtonManyUpdate.Size = new System.Drawing.Size(103, 56);
            this.ButtonManyUpdate.TabIndex = 3;
            this.ButtonManyUpdate.Text = "Many Update";
            this.ButtonManyUpdate.UseVisualStyleBackColor = true;
            this.ButtonManyUpdate.Click += new System.EventHandler(this.ButtonManyUpdate_Click);
            // 
            // DateTimePickerStart
            // 
            this.DateTimePickerStart.Location = new System.Drawing.Point(62, 32);
            this.DateTimePickerStart.Name = "DateTimePickerStart";
            this.DateTimePickerStart.Size = new System.Drawing.Size(200, 20);
            this.DateTimePickerStart.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.BtnInitalQuque);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmbMonth2);
            this.panel2.Controls.Add(this.cmbMonth1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbYear2);
            this.panel2.Controls.Add(this.comboBoxStartTable);
            this.panel2.Controls.Add(this.cmbYear1);
            this.panel2.Location = new System.Drawing.Point(12, 180);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(393, 254);
            this.panel2.TabIndex = 12;
            this.panel2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.DateTimePickerEnd);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ButtonManyUpdate);
            this.panel1.Controls.Add(this.DateTimePickerStart);
            this.panel1.Location = new System.Drawing.Point(12, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 108);
            this.panel1.TabIndex = 11;
            // 
            // InitialRunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 745);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.BntList);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.CmbSection);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "InitialRunForm";
            this.Text = "InitialRunForm";
            this.Load += new System.EventHandler(this.InitialRunForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BntList;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox CmbSection;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BtnInitalQuque;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbMonth2;
        private System.Windows.Forms.ComboBox cmbMonth1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbYear2;
        private System.Windows.Forms.ComboBox comboBoxStartTable;
        private System.Windows.Forms.ComboBox cmbYear1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DateTimePickerEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonManyUpdate;
        private System.Windows.Forms.DateTimePicker DateTimePickerStart;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}