
namespace KPI.ProdForm
{
    partial class OAalertForm
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
            this.lbLine = new System.Windows.Forms.Label();
            this.BtnAccept = new System.Windows.Forms.Button();
            this.lbTime = new System.Windows.Forms.Label();
            this.lbOA = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLine
            // 
            this.lbLine.AutoSize = true;
            this.lbLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine.Location = new System.Drawing.Point(3, 6);
            this.lbLine.Name = "lbLine";
            this.lbLine.Size = new System.Drawing.Size(114, 24);
            this.lbLine.TabIndex = 0;
            this.lbLine.Text = "ALT ASSY 3";
            // 
            // BtnAccept
            // 
            this.BtnAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAccept.Location = new System.Drawing.Point(307, 49);
            this.BtnAccept.Name = "BtnAccept";
            this.BtnAccept.Size = new System.Drawing.Size(114, 58);
            this.BtnAccept.TabIndex = 1;
            this.BtnAccept.Text = "Accept";
            this.BtnAccept.UseVisualStyleBackColor = true;
            this.BtnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.Location = new System.Drawing.Point(1, 53);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(133, 25);
            this.lbTime.TabIndex = 0;
            this.lbTime.Text = " 07:35-08:30";
            // 
            // lbOA
            // 
            this.lbOA.AutoSize = true;
            this.lbOA.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOA.Location = new System.Drawing.Point(147, 58);
            this.lbOA.Name = "lbOA";
            this.lbOA.Size = new System.Drawing.Size(151, 42);
            this.lbOA.TabIndex = 0;
            this.lbOA.Text = " 84.5 %";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.lbLine);
            this.panel1.Controls.Add(this.BtnAccept);
            this.panel1.Controls.Add(this.lbTime);
            this.panel1.Controls.Add(this.lbOA);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 115);
            this.panel1.TabIndex = 3;
            // 
            // OAalertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(451, 134);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OAalertForm";
            this.Text = "OAalertForm";
            this.Load += new System.EventHandler(this.OAalertForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbLine;
        private System.Windows.Forms.Button BtnAccept;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label lbOA;
        private System.Windows.Forms.Panel panel1;
    }
}