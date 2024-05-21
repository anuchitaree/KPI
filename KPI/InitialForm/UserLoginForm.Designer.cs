
namespace KPI.InitialForm
{
    partial class UserLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserLoginForm));
            this.BtnChangePassword = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnCheckAuthority = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnBuildAccount = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtUserID = new System.Windows.Forms.TextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbSection = new System.Windows.Forms.ComboBox();
            this.TxtPassCode = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnSignIn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnChangePassword
            // 
            this.BtnChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnChangePassword.Location = new System.Drawing.Point(42, 581);
            this.BtnChangePassword.Name = "BtnChangePassword";
            this.BtnChangePassword.Size = new System.Drawing.Size(384, 72);
            this.BtnChangePassword.TabIndex = 22;
            this.BtnChangePassword.Text = "Change Password";
            this.BtnChangePassword.UseVisualStyleBackColor = true;
            this.BtnChangePassword.Click += new System.EventHandler(this.BtnChangePassword_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(128, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(311, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "หลังจากกรอกรหัสพนักงาน จากนั้น กด Enter หรือ กดปุ่ม ตรวจสอบ";
            // 
            // BtnCheckAuthority
            // 
            this.BtnCheckAuthority.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCheckAuthority.Location = new System.Drawing.Point(368, 98);
            this.BtnCheckAuthority.Name = "BtnCheckAuthority";
            this.BtnCheckAuthority.Size = new System.Drawing.Size(89, 52);
            this.BtnCheckAuthority.TabIndex = 20;
            this.BtnCheckAuthority.Text = "Check\r\nauthority";
            this.BtnCheckAuthority.UseVisualStyleBackColor = true;
            this.BtnCheckAuthority.Click += new System.EventHandler(this.BtnCheckAuthoruty_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Image = ((System.Drawing.Image)(resources.GetObject("BtnExit.Image")));
            this.BtnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnExit.Location = new System.Drawing.Point(356, 3);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(101, 90);
            this.BtnExit.TabIndex = 19;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(255, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "กรุณาเลือก Day/Nignt";
            this.label1.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.BtnChangePassword);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.BtnCheckAuthority);
            this.panel3.Controls.Add(this.BtnExit);
            this.panel3.Controls.Add(this.BtnBuildAccount);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.TxtUserID);
            this.panel3.Controls.Add(this.comboBox3);
            this.panel3.Controls.Add(this.comboBox2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.CmbSection);
            this.panel3.Controls.Add(this.TxtPassCode);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.BtnSignIn);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(460, 661);
            this.panel3.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 47);
            this.button1.TabIndex = 23;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnBuildAccount
            // 
            this.BtnBuildAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuildAccount.Location = new System.Drawing.Point(42, 490);
            this.BtnBuildAccount.Name = "BtnBuildAccount";
            this.BtnBuildAccount.Size = new System.Drawing.Size(384, 72);
            this.BtnBuildAccount.TabIndex = 17;
            this.BtnBuildAccount.Text = "Create you account";
            this.BtnBuildAccount.UseVisualStyleBackColor = true;
            this.BtnBuildAccount.Click += new System.EventHandler(this.BtnCreateAccount_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(34, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "กรุณาเลือก Shift";
            this.label4.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(206, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "(as progress sheet)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(36, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Select section code";
            // 
            // TxtUserID
            // 
            this.TxtUserID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(47)))));
            this.TxtUserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserID.ForeColor = System.Drawing.Color.White;
            this.TxtUserID.Location = new System.Drawing.Point(139, 115);
            this.TxtUserID.Name = "TxtUserID";
            this.TxtUserID.Size = new System.Drawing.Size(189, 33);
            this.TxtUserID.TabIndex = 0;
            this.TxtUserID.TabStop = false;
            this.TxtUserID.Text = "User";
            this.TxtUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtUserID.Click += new System.EventHandler(this.TxtUserID_Click);
            this.TxtUserID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtUserID_KeyPress);
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Day",
            "Night"});
            this.comboBox3.Location = new System.Drawing.Point(255, 338);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(171, 33);
            this.comboBox3.TabIndex = 12;
            this.comboBox3.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Shift A",
            "Shift B",
            "Shift C"});
            this.comboBox2.Location = new System.Drawing.Point(35, 338);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(171, 33);
            this.comboBox2.TabIndex = 12;
            this.comboBox2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(166)))), ((int)(((byte)(209)))));
            this.label2.Location = new System.Drawing.Point(162, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 55);
            this.label2.TabIndex = 6;
            this.label2.Text = "Log in";
            // 
            // CmbSection
            // 
            this.CmbSection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.CmbSection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbSection.FormattingEnabled = true;
            this.CmbSection.Location = new System.Drawing.Point(35, 270);
            this.CmbSection.Name = "CmbSection";
            this.CmbSection.Size = new System.Drawing.Size(391, 33);
            this.CmbSection.TabIndex = 12;
            // 
            // TxtPassCode
            // 
            this.TxtPassCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(47)))));
            this.TxtPassCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPassCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPassCode.ForeColor = System.Drawing.Color.White;
            this.TxtPassCode.Location = new System.Drawing.Point(137, 192);
            this.TxtPassCode.Name = "TxtPassCode";
            this.TxtPassCode.Size = new System.Drawing.Size(189, 33);
            this.TxtPassCode.TabIndex = 7;
            this.TxtPassCode.Text = "Password";
            this.TxtPassCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtPassCode.Click += new System.EventHandler(this.TxtPassCode_Click);
            this.TxtPassCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPassCode_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(96, 154);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 1);
            this.panel1.TabIndex = 8;
            // 
            // BtnSignIn
            // 
            this.BtnSignIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(166)))), ((int)(((byte)(209)))));
            this.BtnSignIn.FlatAppearance.BorderSize = 0;
            this.BtnSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSignIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSignIn.Location = new System.Drawing.Point(42, 391);
            this.BtnSignIn.Name = "BtnSignIn";
            this.BtnSignIn.Size = new System.Drawing.Size(384, 78);
            this.BtnSignIn.TabIndex = 10;
            this.BtnSignIn.Text = "Log in";
            this.BtnSignIn.UseVisualStyleBackColor = false;
            this.BtnSignIn.Click += new System.EventHandler(this.BtnSignIn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(96, 228);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 1);
            this.panel2.TabIndex = 9;
            // 
            // UserLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(485, 688);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserLoginForm";
            this.Text = "UserLogin";
            this.Shown += new System.EventHandler(this.UserLoginForm_Shown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnChangePassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnCheckAuthority;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnBuildAccount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtUserID;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CmbSection;
        private System.Windows.Forms.TextBox TxtPassCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnSignIn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
    }
}