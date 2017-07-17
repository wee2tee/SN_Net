namespace SN_Net.Subform
{
    partial class InquiryMaAndCloud
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
            this.dgvSerial = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numPeriod = new System.Windows.Forms.NumericUpDown();
            this.lblRowPos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSernum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompnam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemainingDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSerial
            // 
            this.dgvSerial.AllowUserToAddRows = false;
            this.dgvSerial.AllowUserToDeleteRows = false;
            this.dgvSerial.AllowUserToOrderColumns = true;
            this.dgvSerial.AllowUserToResizeRows = false;
            this.dgvSerial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSerial.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvSerial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSerial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colSernum,
            this.colCompnam,
            this.colContact,
            this.colTelnum,
            this.col_StartDate,
            this.col_EndDate,
            this.colRemainingDays,
            this.colEmail,
            this.colStartDate,
            this.colEndDate});
            this.dgvSerial.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSerial.EnableHeadersVisualStyles = false;
            this.dgvSerial.Location = new System.Drawing.Point(12, 12);
            this.dgvSerial.MultiSelect = false;
            this.dgvSerial.Name = "dgvSerial";
            this.dgvSerial.ReadOnly = true;
            this.dgvSerial.RowHeadersVisible = false;
            this.dgvSerial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSerial.Size = new System.Drawing.Size(1210, 245);
            this.dgvSerial.TabIndex = 1;
            this.dgvSerial.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSerial_CellDoubleClick);
            this.dgvSerial.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSerial_CellMouseClick);
            this.dgvSerial.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvSerial_CellPainting);
            this.dgvSerial.CurrentCellChanged += new System.EventHandler(this.dgvSerial_CurrentCellChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(14, 277);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 32);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(100, 277);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(412, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "วัน";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "เฉพาะที่กำลังจะหมดอายุในอีก";
            // 
            // numPeriod
            // 
            this.numPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numPeriod.Location = new System.Drawing.Point(359, 283);
            this.numPeriod.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numPeriod.Name = "numPeriod";
            this.numPeriod.Size = new System.Drawing.Size(51, 23);
            this.numPeriod.TabIndex = 5;
            this.numPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numPeriod.ValueChanged += new System.EventHandler(this.numPeriod_ValueChanged);
            // 
            // lblRowPos
            // 
            this.lblRowPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRowPos.Location = new System.Drawing.Point(1058, 260);
            this.lblRowPos.Name = "lblRowPos";
            this.lblRowPos.Size = new System.Drawing.Size(167, 16);
            this.lblRowPos.TabIndex = 4;
            this.lblRowPos.Text = "0/0";
            this.lblRowPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(437, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(246, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "(0 = เฉพาะที่หมดอายุในวันนี้ , -1 = ทั้งหมด)";
            // 
            // colId
            // 
            this.colId.DataPropertyName = "serial_id";
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colSernum
            // 
            this.colSernum.DataPropertyName = "sernum";
            this.colSernum.HeaderText = "S/N";
            this.colSernum.Name = "colSernum";
            this.colSernum.ReadOnly = true;
            this.colSernum.Width = 110;
            // 
            // colCompnam
            // 
            this.colCompnam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCompnam.DataPropertyName = "compnam";
            this.colCompnam.HeaderText = "Company Name";
            this.colCompnam.Name = "colCompnam";
            this.colCompnam.ReadOnly = true;
            // 
            // colContact
            // 
            this.colContact.DataPropertyName = "contact";
            this.colContact.HeaderText = "Contact";
            this.colContact.Name = "colContact";
            this.colContact.ReadOnly = true;
            this.colContact.Width = 150;
            // 
            // colTelnum
            // 
            this.colTelnum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTelnum.DataPropertyName = "telnum";
            this.colTelnum.HeaderText = "Tel.";
            this.colTelnum.Name = "colTelnum";
            this.colTelnum.ReadOnly = true;
            // 
            // col_StartDate
            // 
            this.col_StartDate.DataPropertyName = "_start_date";
            this.col_StartDate.HeaderText = "Start Date";
            this.col_StartDate.Name = "col_StartDate";
            this.col_StartDate.ReadOnly = true;
            this.col_StartDate.Width = 80;
            // 
            // col_EndDate
            // 
            this.col_EndDate.DataPropertyName = "_end_date";
            this.col_EndDate.HeaderText = "End Date";
            this.col_EndDate.Name = "col_EndDate";
            this.col_EndDate.ReadOnly = true;
            this.col_EndDate.Width = 80;
            // 
            // colRemainingDays
            // 
            this.colRemainingDays.DataPropertyName = "remaining_days";
            this.colRemainingDays.HeaderText = "Remaining Day(s)";
            this.colRemainingDays.Name = "colRemainingDays";
            this.colRemainingDays.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEmail.DataPropertyName = "email";
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // colStartDate
            // 
            this.colStartDate.DataPropertyName = "start_date";
            this.colStartDate.HeaderText = "_HiddenStartDate";
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.ReadOnly = true;
            this.colStartDate.Visible = false;
            // 
            // colEndDate
            // 
            this.colEndDate.DataPropertyName = "end_date";
            this.colEndDate.HeaderText = "_HiddenEndDate";
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.ReadOnly = true;
            this.colEndDate.Visible = false;
            // 
            // InquiryMaAndCloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 321);
            this.Controls.Add(this.numPeriod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRowPos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvSerial);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(710, 180);
            this.Name = "InquiryMaAndCloud";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InquiryMaAndCloud";
            this.Activated += new System.EventHandler(this.InquiryMaAndCloud_Activated);
            this.Load += new System.EventHandler(this.InquiryMaAndCloud_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSerial;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numPeriod;
        private System.Windows.Forms.Label lblRowPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSernum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompnam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_EndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemainingDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndDate;

    }
}