namespace SN_Net.Subform
{
    partial class FormImportData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pgIstab = new System.Windows.Forms.ProgressBar();
            this.lblProgressIstab = new System.Windows.Forms.Label();
            this.pgSerial = new System.Windows.Forms.ProgressBar();
            this.lblProgressSerial = new System.Windows.Forms.Label();
            this.pgProblem = new System.Windows.Forms.ProgressBar();
            this.lblProgressProblem = new System.Windows.Forms.Label();
            this.pgDealer = new System.Windows.Forms.ProgressBar();
            this.lblProgressDealer = new System.Windows.Forms.Label();
            this.pgDmsg = new System.Windows.Forms.ProgressBar();
            this.lblProgressDmsg = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.col_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_table_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chIstab = new System.Windows.Forms.CheckBox();
            this.chDealer = new System.Windows.Forms.CheckBox();
            this.chDmsg = new System.Windows.Forms.CheckBox();
            this.chSerial = new System.Windows.Forms.CheckBox();
            this.chProblem = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numDealer = new System.Windows.Forms.NumericUpDown();
            this.numDmsg = new System.Windows.Forms.NumericUpDown();
            this.numSerial = new System.Windows.Forms.NumericUpDown();
            this.numProblem = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDealer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDmsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSerial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProblem)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(172, 24);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(372, 23);
            this.txtPath.TabIndex = 0;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "ที่เก็บไฟล์ข้อมูลเดิม (.dbf)";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(544, 23);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(29, 25);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGo.Enabled = false;
            this.btnGo.Location = new System.Drawing.Point(139, 238);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(142, 28);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "เริ่มนำเข้าข้อมูล <F5>";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(287, 238);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(172, 28);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "หยุดการนำเข้าข้อมูล <Esc>";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pgIstab
            // 
            this.pgIstab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgIstab.Location = new System.Drawing.Point(177, 81);
            this.pgIstab.Name = "pgIstab";
            this.pgIstab.Size = new System.Drawing.Size(393, 11);
            this.pgIstab.TabIndex = 3;
            // 
            // lblProgressIstab
            // 
            this.lblProgressIstab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgressIstab.BackColor = System.Drawing.Color.Transparent;
            this.lblProgressIstab.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblProgressIstab.Location = new System.Drawing.Point(430, 66);
            this.lblProgressIstab.Name = "lblProgressIstab";
            this.lblProgressIstab.Size = new System.Drawing.Size(142, 13);
            this.lblProgressIstab.TabIndex = 5;
            this.lblProgressIstab.Text = "0/0";
            this.lblProgressIstab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgSerial
            // 
            this.pgSerial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgSerial.Location = new System.Drawing.Point(177, 172);
            this.pgSerial.Name = "pgSerial";
            this.pgSerial.Size = new System.Drawing.Size(393, 11);
            this.pgSerial.TabIndex = 3;
            // 
            // lblProgressSerial
            // 
            this.lblProgressSerial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgressSerial.BackColor = System.Drawing.Color.Transparent;
            this.lblProgressSerial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblProgressSerial.Location = new System.Drawing.Point(430, 157);
            this.lblProgressSerial.Name = "lblProgressSerial";
            this.lblProgressSerial.Size = new System.Drawing.Size(142, 13);
            this.lblProgressSerial.TabIndex = 5;
            this.lblProgressSerial.Text = "0/0";
            this.lblProgressSerial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgProblem
            // 
            this.pgProblem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgProblem.Location = new System.Drawing.Point(177, 203);
            this.pgProblem.Name = "pgProblem";
            this.pgProblem.Size = new System.Drawing.Size(393, 11);
            this.pgProblem.TabIndex = 3;
            // 
            // lblProgressProblem
            // 
            this.lblProgressProblem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgressProblem.BackColor = System.Drawing.Color.Transparent;
            this.lblProgressProblem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblProgressProblem.Location = new System.Drawing.Point(430, 188);
            this.lblProgressProblem.Name = "lblProgressProblem";
            this.lblProgressProblem.Size = new System.Drawing.Size(142, 13);
            this.lblProgressProblem.TabIndex = 5;
            this.lblProgressProblem.Text = "0/0";
            this.lblProgressProblem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgDealer
            // 
            this.pgDealer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgDealer.Location = new System.Drawing.Point(177, 111);
            this.pgDealer.Name = "pgDealer";
            this.pgDealer.Size = new System.Drawing.Size(393, 11);
            this.pgDealer.TabIndex = 3;
            // 
            // lblProgressDealer
            // 
            this.lblProgressDealer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgressDealer.BackColor = System.Drawing.Color.Transparent;
            this.lblProgressDealer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblProgressDealer.Location = new System.Drawing.Point(430, 96);
            this.lblProgressDealer.Name = "lblProgressDealer";
            this.lblProgressDealer.Size = new System.Drawing.Size(142, 13);
            this.lblProgressDealer.TabIndex = 5;
            this.lblProgressDealer.Text = "0/0";
            this.lblProgressDealer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgDmsg
            // 
            this.pgDmsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgDmsg.Location = new System.Drawing.Point(177, 142);
            this.pgDmsg.Name = "pgDmsg";
            this.pgDmsg.Size = new System.Drawing.Size(393, 11);
            this.pgDmsg.TabIndex = 3;
            // 
            // lblProgressDmsg
            // 
            this.lblProgressDmsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgressDmsg.BackColor = System.Drawing.Color.Transparent;
            this.lblProgressDmsg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblProgressDmsg.Location = new System.Drawing.Point(430, 127);
            this.lblProgressDmsg.Name = "lblProgressDmsg";
            this.lblProgressDmsg.Size = new System.Drawing.Size(142, 13);
            this.lblProgressDmsg.TabIndex = 5;
            this.lblProgressDmsg.Text = "0/0";
            this.lblProgressDmsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvLog);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(12, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 127);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import Log";
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.AllowUserToResizeColumns = false;
            this.dgvLog.AllowUserToResizeRows = false;
            this.dgvLog.BackgroundColor = System.Drawing.Color.White;
            this.dgvLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.ColumnHeadersVisible = false;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_time,
            this.col_table_name,
            this.col_desc});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLog.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.Location = new System.Drawing.Point(3, 17);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowTemplate.Height = 20;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(564, 107);
            this.dgvLog.TabIndex = 0;
            // 
            // col_time
            // 
            this.col_time.DataPropertyName = "time";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy H:mm:ss";
            this.col_time.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_time.HeaderText = "Data/Time";
            this.col_time.MinimumWidth = 120;
            this.col_time.Name = "col_time";
            this.col_time.ReadOnly = true;
            this.col_time.Width = 120;
            // 
            // col_table_name
            // 
            this.col_table_name.DataPropertyName = "table_name";
            this.col_table_name.HeaderText = "Table Name";
            this.col_table_name.MinimumWidth = 60;
            this.col_table_name.Name = "col_table_name";
            this.col_table_name.ReadOnly = true;
            this.col_table_name.Width = 60;
            // 
            // col_desc
            // 
            this.col_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_desc.DataPropertyName = "desc";
            this.col_desc.HeaderText = "Description";
            this.col_desc.Name = "col_desc";
            this.col_desc.ReadOnly = true;
            // 
            // chIstab
            // 
            this.chIstab.AutoSize = true;
            this.chIstab.Checked = true;
            this.chIstab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chIstab.Location = new System.Drawing.Point(18, 76);
            this.chIstab.Name = "chIstab";
            this.chIstab.Size = new System.Drawing.Size(55, 20);
            this.chIstab.TabIndex = 8;
            this.chIstab.Text = "Istab";
            this.chIstab.UseVisualStyleBackColor = true;
            this.chIstab.CheckedChanged += new System.EventHandler(this.chIstab_CheckedChanged);
            // 
            // chDealer
            // 
            this.chDealer.AutoSize = true;
            this.chDealer.Checked = true;
            this.chDealer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chDealer.Location = new System.Drawing.Point(18, 106);
            this.chDealer.Name = "chDealer";
            this.chDealer.Size = new System.Drawing.Size(64, 20);
            this.chDealer.TabIndex = 8;
            this.chDealer.Text = "Dealer";
            this.chDealer.UseVisualStyleBackColor = true;
            this.chDealer.CheckedChanged += new System.EventHandler(this.chDealer_CheckedChanged);
            // 
            // chDmsg
            // 
            this.chDmsg.AutoSize = true;
            this.chDmsg.Checked = true;
            this.chDmsg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chDmsg.Location = new System.Drawing.Point(18, 137);
            this.chDmsg.Name = "chDmsg";
            this.chDmsg.Size = new System.Drawing.Size(66, 20);
            this.chDmsg.TabIndex = 8;
            this.chDmsg.Text = "D_msg";
            this.chDmsg.UseVisualStyleBackColor = true;
            this.chDmsg.CheckedChanged += new System.EventHandler(this.chDmsg_CheckedChanged);
            // 
            // chSerial
            // 
            this.chSerial.AutoSize = true;
            this.chSerial.Checked = true;
            this.chSerial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chSerial.Location = new System.Drawing.Point(18, 167);
            this.chSerial.Name = "chSerial";
            this.chSerial.Size = new System.Drawing.Size(60, 20);
            this.chSerial.TabIndex = 8;
            this.chSerial.Text = "Serial";
            this.chSerial.UseVisualStyleBackColor = true;
            this.chSerial.CheckedChanged += new System.EventHandler(this.chSerial_CheckedChanged);
            // 
            // chProblem
            // 
            this.chProblem.AutoSize = true;
            this.chProblem.Checked = true;
            this.chProblem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chProblem.Location = new System.Drawing.Point(18, 198);
            this.chProblem.Name = "chProblem";
            this.chProblem.Size = new System.Drawing.Size(74, 20);
            this.chProblem.TabIndex = 8;
            this.chProblem.Text = "Problem";
            this.chProblem.UseVisualStyleBackColor = true;
            this.chProblem.CheckedChanged += new System.EventHandler(this.chProblem_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(17, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Table Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(105, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Offset";
            // 
            // numDealer
            // 
            this.numDealer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.numDealer.Location = new System.Drawing.Point(90, 106);
            this.numDealer.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numDealer.Name = "numDealer";
            this.numDealer.Size = new System.Drawing.Size(80, 21);
            this.numDealer.TabIndex = 10;
            this.numDealer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDealer.ValueChanged += new System.EventHandler(this.numDealer_ValueChanged);
            // 
            // numDmsg
            // 
            this.numDmsg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.numDmsg.Location = new System.Drawing.Point(90, 137);
            this.numDmsg.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numDmsg.Name = "numDmsg";
            this.numDmsg.Size = new System.Drawing.Size(80, 21);
            this.numDmsg.TabIndex = 10;
            this.numDmsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDmsg.ValueChanged += new System.EventHandler(this.numDmsg_ValueChanged);
            // 
            // numSerial
            // 
            this.numSerial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.numSerial.Location = new System.Drawing.Point(90, 167);
            this.numSerial.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numSerial.Name = "numSerial";
            this.numSerial.Size = new System.Drawing.Size(80, 21);
            this.numSerial.TabIndex = 10;
            this.numSerial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSerial.ValueChanged += new System.EventHandler(this.numSerial_ValueChanged);
            // 
            // numProblem
            // 
            this.numProblem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.numProblem.Location = new System.Drawing.Point(90, 198);
            this.numProblem.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numProblem.Name = "numProblem";
            this.numProblem.Size = new System.Drawing.Size(80, 21);
            this.numProblem.TabIndex = 10;
            this.numProblem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numProblem.ValueChanged += new System.EventHandler(this.numProblem_ValueChanged);
            // 
            // FormImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 412);
            this.Controls.Add(this.numProblem);
            this.Controls.Add(this.numSerial);
            this.Controls.Add(this.numDmsg);
            this.Controls.Add(this.numDealer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chProblem);
            this.Controls.Add(this.chSerial);
            this.Controls.Add(this.chDmsg);
            this.Controls.Add(this.chDealer);
            this.Controls.Add(this.chIstab);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblProgressDmsg);
            this.Controls.Add(this.lblProgressDealer);
            this.Controls.Add(this.lblProgressProblem);
            this.Controls.Add(this.lblProgressSerial);
            this.Controls.Add(this.lblProgressIstab);
            this.Controls.Add(this.pgDmsg);
            this.Controls.Add(this.pgDealer);
            this.Controls.Add(this.pgProblem);
            this.Controls.Add(this.pgSerial);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.pgIstab);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPath);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(610, 390);
            this.Name = "FormImportData";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Old Data";
            this.Load += new System.EventHandler(this.FormImportData_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDealer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDmsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSerial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProblem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ProgressBar pgIstab;
        private System.Windows.Forms.Label lblProgressIstab;
        private System.Windows.Forms.ProgressBar pgSerial;
        private System.Windows.Forms.Label lblProgressSerial;
        private System.Windows.Forms.ProgressBar pgProblem;
        private System.Windows.Forms.Label lblProgressProblem;
        private System.Windows.Forms.ProgressBar pgDealer;
        private System.Windows.Forms.Label lblProgressDealer;
        private System.Windows.Forms.ProgressBar pgDmsg;
        private System.Windows.Forms.Label lblProgressDmsg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_table_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_desc;
        private System.Windows.Forms.CheckBox chIstab;
        private System.Windows.Forms.CheckBox chDealer;
        private System.Windows.Forms.CheckBox chDmsg;
        private System.Windows.Forms.CheckBox chSerial;
        private System.Windows.Forms.CheckBox chProblem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numDealer;
        private System.Windows.Forms.NumericUpDown numDmsg;
        private System.Windows.Forms.NumericUpDown numSerial;
        private System.Windows.Forms.NumericUpDown numProblem;
    }
}