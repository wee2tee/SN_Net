namespace SN_Net.MiscClass
{
    partial class CustomDateEvent3
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblBottomText = new System.Windows.Forms.Label();
            this.lblNoteDescription = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnHoliday = new System.Windows.Forms.ToolStripButton();
            this.btnMaid = new System.Windows.Forms.ToolStripButton();
            this.btnDropDownMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnTrainer = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.colCountableLeavePerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventCalendar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.lblMonthYear = new System.Windows.Forms.Label();
            this.customDatagridview1 = new SN_Net.MiscClass.CustomDatagridview();
            this.col_event_calendar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_realname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customDatagridview1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBottomText
            // 
            this.lblBottomText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBottomText.BackColor = System.Drawing.Color.Transparent;
            this.lblBottomText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblBottomText.ForeColor = System.Drawing.Color.Blue;
            this.lblBottomText.Location = new System.Drawing.Point(1, 158);
            this.lblBottomText.Name = "lblBottomText";
            this.lblBottomText.Size = new System.Drawing.Size(404, 14);
            this.lblBottomText.TabIndex = 11;
            this.lblBottomText.Text = "label1";
            this.lblBottomText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNoteDescription
            // 
            this.lblNoteDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoteDescription.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblNoteDescription.ForeColor = System.Drawing.Color.Red;
            this.lblNoteDescription.Location = new System.Drawing.Point(-1, 34);
            this.lblNoteDescription.Name = "lblNoteDescription";
            this.lblNoteDescription.Size = new System.Drawing.Size(408, 123);
            this.lblNoteDescription.TabIndex = 12;
            this.lblNoteDescription.Text = "หยุดชดเชยวันปิยะมหาราช";
            this.lblNoteDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDay
            // 
            this.lblDay.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.lblDay.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDay.ForeColor = System.Drawing.Color.White;
            this.lblDay.Location = new System.Drawing.Point(-1, 7);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(29, 24);
            this.lblDay.TabIndex = 9;
            this.lblDay.Text = "30";
            this.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnHoliday,
            this.btnMaid,
            this.btnDropDownMenu});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(308, 9);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(99, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnHoliday
            // 
            this.btnHoliday.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnHoliday.Image = global::SN_Net.Properties.Resources.smile;
            this.btnHoliday.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHoliday.Name = "btnHoliday";
            this.btnHoliday.Size = new System.Drawing.Size(34, 20);
            this.btnHoliday.Text = "_";
            // 
            // btnMaid
            // 
            this.btnMaid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnMaid.Image = global::SN_Net.Properties.Resources.maid;
            this.btnMaid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMaid.Name = "btnMaid";
            this.btnMaid.Size = new System.Drawing.Size(34, 20);
            this.btnMaid.Text = "_";
            // 
            // btnDropDownMenu
            // 
            this.btnDropDownMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDropDownMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTrainer,
            this.btnAdd,
            this.btnDetail});
            this.btnDropDownMenu.Image = global::SN_Net.Properties.Resources.menu;
            this.btnDropDownMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDropDownMenu.Name = "btnDropDownMenu";
            this.btnDropDownMenu.Padding = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.btnDropDownMenu.Size = new System.Drawing.Size(31, 22);
            this.btnDropDownMenu.Text = "เมนูเพิ่มเติม";
            // 
            // btnTrainer
            // 
            this.btnTrainer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnTrainer.Image = global::SN_Net.Properties.Resources.teacher;
            this.btnTrainer.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.btnTrainer.Name = "btnTrainer";
            this.btnTrainer.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.btnTrainer.Size = new System.Drawing.Size(136, 24);
            this.btnTrainer.Text = "วิทยากรอบรม";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAdd.Image = global::SN_Net.Properties.Resources.plus1;
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.btnAdd.Size = new System.Drawing.Size(136, 24);
            this.btnAdd.Text = "เพิ่มรายการ";
            // 
            // btnDetail
            // 
            this.btnDetail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDetail.Image = global::SN_Net.Properties.Resources.detail;
            this.btnDetail.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.btnDetail.Size = new System.Drawing.Size(136, 24);
            this.btnDetail.Text = "ดูรายละเอียด";
            // 
            // colCountableLeavePerson
            // 
            this.colCountableLeavePerson.DataPropertyName = "countable_leave_person";
            this.colCountableLeavePerson.HeaderText = "Countable";
            this.colCountableLeavePerson.Name = "colCountableLeavePerson";
            this.colCountableLeavePerson.ReadOnly = true;
            this.colCountableLeavePerson.Visible = false;
            // 
            // colEventDesc
            // 
            this.colEventDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEventDesc.DataPropertyName = "event_desc";
            this.colEventDesc.HeaderText = "Event Desc";
            this.colEventDesc.MinimumWidth = 2;
            this.colEventDesc.Name = "colEventDesc";
            this.colEventDesc.ReadOnly = true;
            // 
            // colEventCode
            // 
            this.colEventCode.DataPropertyName = "event_code";
            this.colEventCode.HeaderText = "Event Code";
            this.colEventCode.MinimumWidth = 40;
            this.colEventCode.Name = "colEventCode";
            this.colEventCode.ReadOnly = true;
            this.colEventCode.Width = 40;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "name";
            this.colName.HeaderText = "Name";
            this.colName.MinimumWidth = 38;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 38;
            // 
            // colSeq
            // 
            this.colSeq.DataPropertyName = "seq";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.NullValue = null;
            this.colSeq.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSeq.HeaderText = "Seq";
            this.colSeq.MinimumWidth = 20;
            this.colSeq.Name = "colSeq";
            this.colSeq.ReadOnly = true;
            this.colSeq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSeq.Width = 20;
            // 
            // colEventCalendar
            // 
            this.colEventCalendar.DataPropertyName = "event_calendar";
            this.colEventCalendar.HeaderText = "Event Calendar Object";
            this.colEventCalendar.Name = "colEventCalendar";
            this.colEventCalendar.ReadOnly = true;
            this.colEventCalendar.Visible = false;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.ColumnHeadersVisible = false;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEventCalendar,
            this.colSeq,
            this.colName,
            this.colEventCode,
            this.colEventDesc,
            this.colCountableLeavePerson});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.GridColor = System.Drawing.Color.White;
            this.dgv.Location = new System.Drawing.Point(-1, 37);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 18;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(407, 117);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 8;
            // 
            // lblMonthYear
            // 
            this.lblMonthYear.AutoSize = true;
            this.lblMonthYear.Location = new System.Drawing.Point(31, 13);
            this.lblMonthYear.Name = "lblMonthYear";
            this.lblMonthYear.Size = new System.Drawing.Size(58, 13);
            this.lblMonthYear.TabIndex = 14;
            this.lblMonthYear.Text = "MM/YYYY";
            // 
            // customDatagridview1
            // 
            this.customDatagridview1._BackgroundText = "Sample";
            this.customDatagridview1._BackgroundTextcolor = System.Drawing.Color.Red;
            this.customDatagridview1._BackgroundTextFont = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customDatagridview1.AllowUserToAddRows = false;
            this.customDatagridview1.AllowUserToDeleteRows = false;
            this.customDatagridview1.AllowUserToResizeColumns = false;
            this.customDatagridview1.AllowUserToResizeRows = false;
            this.customDatagridview1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customDatagridview1.BackgroundColor = System.Drawing.Color.White;
            this.customDatagridview1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.customDatagridview1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDatagridview1.ColumnHeadersVisible = false;
            this.customDatagridview1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_event_calendar,
            this.col_seq,
            this.col_realname,
            this.col_type,
            this.col_description});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDatagridview1.DefaultCellStyle = dataGridViewCellStyle3;
            this.customDatagridview1.Location = new System.Drawing.Point(-1, 37);
            this.customDatagridview1.Name = "customDatagridview1";
            this.customDatagridview1.RowHeadersVisible = false;
            this.customDatagridview1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.customDatagridview1.Size = new System.Drawing.Size(370, 103);
            this.customDatagridview1.TabIndex = 13;
            this.customDatagridview1.Visible = false;
            // 
            // col_event_calendar
            // 
            this.col_event_calendar.DataPropertyName = "event_calendar";
            this.col_event_calendar.HeaderText = "Event calendar";
            this.col_event_calendar.Name = "col_event_calendar";
            this.col_event_calendar.ReadOnly = true;
            this.col_event_calendar.Visible = false;
            // 
            // col_seq
            // 
            this.col_seq.HeaderText = "Seq";
            this.col_seq.MinimumWidth = 20;
            this.col_seq.Name = "col_seq";
            this.col_seq.ReadOnly = true;
            this.col_seq.Width = 20;
            // 
            // col_realname
            // 
            this.col_realname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_realname.DataPropertyName = "realname";
            this.col_realname.FillWeight = 30F;
            this.col_realname.HeaderText = "Name";
            this.col_realname.Name = "col_realname";
            this.col_realname.ReadOnly = true;
            // 
            // col_type
            // 
            this.col_type.DataPropertyName = "type";
            this.col_type.HeaderText = "Type";
            this.col_type.Name = "col_type";
            this.col_type.ReadOnly = true;
            this.col_type.Visible = false;
            // 
            // col_description
            // 
            this.col_description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_description.DataPropertyName = "description";
            this.col_description.FillWeight = 70F;
            this.col_description.HeaderText = "Description";
            this.col_description.Name = "col_description";
            this.col_description.ReadOnly = true;
            // 
            // CustomDateEvent3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customDatagridview1);
            this.Controls.Add(this.lblBottomText);
            this.Controls.Add(this.lblNoteDescription);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.lblMonthYear);
            this.Name = "CustomDateEvent3";
            this.Size = new System.Drawing.Size(407, 179);
            this.Load += new System.EventHandler(this.CustomDateEvent3_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customDatagridview1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CustomDatagridview customDatagridview1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblBottomText;
        private System.Windows.Forms.Label lblNoteDescription;
        private System.Windows.Forms.ToolStripMenuItem btnDetail;
        private System.Windows.Forms.ToolStripMenuItem btnAdd;
        private System.Windows.Forms.ToolStripMenuItem btnTrainer;
        private System.Windows.Forms.ToolStripButton btnMaid;
        private System.Windows.Forms.ToolStripButton btnHoliday;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton btnDropDownMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCountableLeavePerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventCalendar;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lblMonthYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_event_calendar;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_realname;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_description;
    }
}
