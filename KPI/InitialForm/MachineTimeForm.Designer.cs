
namespace KPI.InitialForm
{
    partial class MachineTimeForm
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
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.BtnBrowseFileMC = new System.Windows.Forms.Button();
            this.BtnAllSave = new System.Windows.Forms.Button();
            this.BtnPartFromNetTime = new System.Windows.Forms.Button();
            this.BtnDelMC = new System.Windows.Forms.Button();
            this.BtnClearMC = new System.Windows.Forms.Button();
            this.dataGridViewMC = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnMCList = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnMTSet = new System.Windows.Forms.Button();
            this.TbMTTime = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnHTSet = new System.Windows.Forms.Button();
            this.TbHTTime = new System.Windows.Forms.TextBox();
            this.BtnImportError = new System.Windows.Forms.Button();
            this.BntSaveError = new System.Windows.Forms.Button();
            this.BtnReadError = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbMachineName = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.BtnDeleteError = new System.Windows.Forms.Button();
            this.dataGridViewError = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnClearError = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel100 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMC)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewError)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel100.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSection
            // 
            this.cmbSection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbSection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(3, 3);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(293, 28);
            this.cmbSection.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(742, 20);
            this.label15.TabIndex = 33;
            this.label15.Tag = "  ";
            this.label15.Text = "                     Select SectionCode                |            Machine Numbe" +
    "r";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnBrowseFileMC
            // 
            this.BtnBrowseFileMC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnBrowseFileMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBrowseFileMC.Location = new System.Drawing.Point(590, 59);
            this.BtnBrowseFileMC.Name = "BtnBrowseFileMC";
            this.BtnBrowseFileMC.Size = new System.Drawing.Size(149, 50);
            this.BtnBrowseFileMC.TabIndex = 7;
            this.BtnBrowseFileMC.UseVisualStyleBackColor = true;
            this.BtnBrowseFileMC.Visible = false;
            // 
            // BtnAllSave
            // 
            this.BtnAllSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnAllSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAllSave.ForeColor = System.Drawing.Color.Black;
            this.BtnAllSave.Location = new System.Drawing.Point(3, 59);
            this.BtnAllSave.Name = "BtnAllSave";
            this.BtnAllSave.Size = new System.Drawing.Size(145, 50);
            this.BtnAllSave.TabIndex = 5;
            this.BtnAllSave.Text = "Save";
            this.BtnAllSave.UseVisualStyleBackColor = true;
            this.BtnAllSave.Click += new System.EventHandler(this.BtnAllSave_Click);
            // 
            // BtnPartFromNetTime
            // 
            this.BtnPartFromNetTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnPartFromNetTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPartFromNetTime.ForeColor = System.Drawing.Color.Black;
            this.BtnPartFromNetTime.Location = new System.Drawing.Point(590, 3);
            this.BtnPartFromNetTime.Name = "BtnPartFromNetTime";
            this.BtnPartFromNetTime.Size = new System.Drawing.Size(149, 50);
            this.BtnPartFromNetTime.TabIndex = 5;
            this.BtnPartFromNetTime.Text = "Part number List";
            this.BtnPartFromNetTime.UseVisualStyleBackColor = true;
            this.BtnPartFromNetTime.Click += new System.EventHandler(this.BtnPartFromNetTime_Click);
            // 
            // BtnDelMC
            // 
            this.BtnDelMC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDelMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelMC.Location = new System.Drawing.Point(200, 59);
            this.BtnDelMC.Name = "BtnDelMC";
            this.BtnDelMC.Size = new System.Drawing.Size(145, 50);
            this.BtnDelMC.TabIndex = 28;
            this.BtnDelMC.UseVisualStyleBackColor = true;
            this.BtnDelMC.Visible = false;
            // 
            // BtnClearMC
            // 
            this.BtnClearMC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClearMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearMC.Location = new System.Drawing.Point(401, 59);
            this.BtnClearMC.Name = "BtnClearMC";
            this.BtnClearMC.Size = new System.Drawing.Size(144, 50);
            this.BtnClearMC.TabIndex = 27;
            this.BtnClearMC.UseVisualStyleBackColor = true;
            this.BtnClearMC.Visible = false;
            // 
            // dataGridViewMC
            // 
            this.dataGridViewMC.AllowUserToDeleteRows = false;
            this.dataGridViewMC.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewMC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMC.Location = new System.Drawing.Point(3, 196);
            this.dataGridViewMC.Name = "dataGridViewMC";
            this.dataGridViewMC.Size = new System.Drawing.Size(742, 524);
            this.dataGridViewMC.TabIndex = 0;
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
            this.tableLayoutPanel6.Controls.Add(this.BtnBrowseFileMC, 6, 1);
            this.tableLayoutPanel6.Controls.Add(this.BtnAllSave, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.BtnPartFromNetTime, 6, 0);
            this.tableLayoutPanel6.Controls.Add(this.BtnDelMC, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.BtnClearMC, 4, 1);
            this.tableLayoutPanel6.Controls.Add(this.BtnMCList, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 4, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 78);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(742, 112);
            this.tableLayoutPanel6.TabIndex = 32;
            // 
            // BtnMCList
            // 
            this.BtnMCList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnMCList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMCList.Location = new System.Drawing.Point(3, 3);
            this.BtnMCList.Name = "BtnMCList";
            this.BtnMCList.Size = new System.Drawing.Size(145, 50);
            this.BtnMCList.TabIndex = 29;
            this.BtnMCList.Text = "Machine List";
            this.BtnMCList.UseVisualStyleBackColor = true;
            this.BtnMCList.Click += new System.EventHandler(this.BtnMCList_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel7.Controls.Add(this.BtnMTSet, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.TbMTTime, 0, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(200, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(144, 50);
            this.tableLayoutPanel7.TabIndex = 30;
            // 
            // BtnMTSet
            // 
            this.BtnMTSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnMTSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMTSet.Location = new System.Drawing.Point(46, 3);
            this.BtnMTSet.Name = "BtnMTSet";
            this.BtnMTSet.Size = new System.Drawing.Size(95, 44);
            this.BtnMTSet.TabIndex = 0;
            this.BtnMTSet.Text = "Setting MT ";
            this.BtnMTSet.UseVisualStyleBackColor = true;
            this.BtnMTSet.Click += new System.EventHandler(this.BtnMTSet_Click);
            // 
            // TbMTTime
            // 
            this.TbMTTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TbMTTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TbMTTime.Location = new System.Drawing.Point(3, 25);
            this.TbMTTime.Name = "TbMTTime";
            this.TbMTTime.Size = new System.Drawing.Size(37, 22);
            this.TbMTTime.TabIndex = 1;
            this.TbMTTime.Text = "0.0";
            this.TbMTTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TbMTTime.Click += new System.EventHandler(this.TbMTTime_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel8.Controls.Add(this.BtnHTSet, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.TbHTTime, 0, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(401, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(142, 50);
            this.tableLayoutPanel8.TabIndex = 30;
            // 
            // BtnHTSet
            // 
            this.BtnHTSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnHTSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHTSet.Location = new System.Drawing.Point(45, 3);
            this.BtnHTSet.Name = "BtnHTSet";
            this.BtnHTSet.Size = new System.Drawing.Size(94, 44);
            this.BtnHTSet.TabIndex = 0;
            this.BtnHTSet.Text = "Setting HT ";
            this.BtnHTSet.UseVisualStyleBackColor = true;
            this.BtnHTSet.Click += new System.EventHandler(this.BtnHTSet_Click);
            // 
            // TbHTTime
            // 
            this.TbHTTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TbHTTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TbHTTime.Location = new System.Drawing.Point(3, 25);
            this.TbHTTime.Name = "TbHTTime";
            this.TbHTTime.Size = new System.Drawing.Size(36, 22);
            this.TbHTTime.TabIndex = 1;
            this.TbHTTime.Text = "0.0";
            this.TbHTTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TbHTTime.Click += new System.EventHandler(this.TbHTTime_Click);
            // 
            // BtnImportError
            // 
            this.BtnImportError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnImportError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImportError.Location = new System.Drawing.Point(597, 60);
            this.BtnImportError.Name = "BtnImportError";
            this.BtnImportError.Size = new System.Drawing.Size(142, 51);
            this.BtnImportError.TabIndex = 7;
            this.BtnImportError.UseVisualStyleBackColor = true;
            this.BtnImportError.Visible = false;
            // 
            // BntSaveError
            // 
            this.BntSaveError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BntSaveError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BntSaveError.ForeColor = System.Drawing.Color.Black;
            this.BntSaveError.Location = new System.Drawing.Point(3, 60);
            this.BntSaveError.Name = "BntSaveError";
            this.BntSaveError.Size = new System.Drawing.Size(144, 51);
            this.BntSaveError.TabIndex = 5;
            this.BntSaveError.UseVisualStyleBackColor = true;
            this.BntSaveError.Visible = false;
            // 
            // BtnReadError
            // 
            this.BtnReadError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnReadError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReadError.ForeColor = System.Drawing.Color.Black;
            this.BtnReadError.Location = new System.Drawing.Point(597, 3);
            this.BtnReadError.Name = "BtnReadError";
            this.BtnReadError.Size = new System.Drawing.Size(142, 51);
            this.BtnReadError.TabIndex = 5;
            this.BtnReadError.UseVisualStyleBackColor = true;
            this.BtnReadError.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewMC, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 3);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(748, 723);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Controls.Add(this.cmbSection, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbMachineName, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 45);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(748, 30);
            this.tableLayoutPanel2.TabIndex = 31;
            // 
            // cmbMachineName
            // 
            this.cmbMachineName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMachineName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMachineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMachineName.Location = new System.Drawing.Point(302, 3);
            this.cmbMachineName.Name = "cmbMachineName";
            this.cmbMachineName.Size = new System.Drawing.Size(443, 28);
            this.cmbMachineName.TabIndex = 11;
            this.cmbMachineName.SelectedIndexChanged += new System.EventHandler(this.cmbMachineName_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(235)))), ((int)(((byte)(187)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(742, 25);
            this.label14.TabIndex = 1;
            this.label14.Text = "Machine  OEE";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnDeleteError
            // 
            this.BtnDeleteError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDeleteError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteError.Location = new System.Drawing.Point(198, 60);
            this.BtnDeleteError.Name = "BtnDeleteError";
            this.BtnDeleteError.Size = new System.Drawing.Size(144, 51);
            this.BtnDeleteError.TabIndex = 28;
            this.BtnDeleteError.UseVisualStyleBackColor = true;
            this.BtnDeleteError.Visible = false;
            // 
            // dataGridViewError
            // 
            this.dataGridViewError.AllowUserToDeleteRows = false;
            this.dataGridViewError.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewError.Location = new System.Drawing.Point(3, 198);
            this.dataGridViewError.Name = "dataGridViewError";
            this.dataGridViewError.Size = new System.Drawing.Size(742, 522);
            this.dataGridViewError.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(742, 20);
            this.label4.TabIndex = 33;
            this.label4.Text = " ";
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
            this.label3.Size = new System.Drawing.Size(742, 25);
            this.label3.TabIndex = 1;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4051F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.189046F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4051F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.689422F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4051F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.925926F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.81482F));
            this.tableLayoutPanel4.Controls.Add(this.BtnImportError, 6, 1);
            this.tableLayoutPanel4.Controls.Add(this.BntSaveError, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.BtnReadError, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.BtnDeleteError, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.BtnClearError, 4, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 78);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(742, 114);
            this.tableLayoutPanel4.TabIndex = 32;
            // 
            // BtnClearError
            // 
            this.BtnClearError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClearError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearError.Location = new System.Drawing.Point(404, 60);
            this.BtnClearError.Name = "BtnClearError";
            this.BtnClearError.Size = new System.Drawing.Size(144, 51);
            this.BtnClearError.TabIndex = 27;
            this.BtnClearError.UseVisualStyleBackColor = true;
            this.BtnClearError.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dataGridViewError, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(757, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(748, 723);
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
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1508, 729);
            this.tableLayoutPanel5.TabIndex = 28;
            // 
            // tableLayoutPanel100
            // 
            this.tableLayoutPanel100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(79)))), ((int)(((byte)(132)))));
            this.tableLayoutPanel100.ColumnCount = 1;
            this.tableLayoutPanel100.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel100.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel100.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel100.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel100.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel100.Name = "tableLayoutPanel100";
            this.tableLayoutPanel100.RowCount = 2;
            this.tableLayoutPanel100.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel100.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel100.Size = new System.Drawing.Size(1514, 765);
            this.tableLayoutPanel100.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1508, 30);
            this.label1.TabIndex = 29;
            this.label1.Text = "Machine Time and  Hand Time Registration";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MachineTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1514, 765);
            this.Controls.Add(this.tableLayoutPanel100);
            this.Name = "MachineTimeForm";
            this.Text = "MachineTimeForm";
            this.Load += new System.EventHandler(this.MachineTimeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMC)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewError)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel100.ResumeLayout(false);
            this.tableLayoutPanel100.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button BtnBrowseFileMC;
        private System.Windows.Forms.Button BtnAllSave;
        private System.Windows.Forms.Button BtnPartFromNetTime;
        private System.Windows.Forms.Button BtnDelMC;
        private System.Windows.Forms.Button BtnClearMC;
        private System.Windows.Forms.DataGridView dataGridViewMC;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button BtnImportError;
        private System.Windows.Forms.Button BntSaveError;
        private System.Windows.Forms.Button BtnReadError;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button BtnDeleteError;
        private System.Windows.Forms.DataGridView dataGridViewError;
        private System.Windows.Forms.ComboBox cmbMachineName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button BtnClearError;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel100;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button BtnMCList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button BtnMTSet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Button BtnHTSet;
        private System.Windows.Forms.TextBox TbMTTime;
        private System.Windows.Forms.TextBox TbHTTime;
    }
}