namespace SN_Net.MiscClass
{
    partial class CustomDateEvent2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colEventCalendar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCountableLeavePerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDay = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnHoliday = new System.Windows.Forms.ToolStripButton();
            this.btnMaid = new System.Windows.Forms.ToolStripButton();
            this.btnDropDownMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnTrainer = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBottomText = new System.Windows.Forms.Label();
            this.lblNoteDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.dgv.Location = new System.Drawing.Point(1, 28);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 18;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(398, 113);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 2;
            this.dgv.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            this.dgv.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgv_RowPrePaint);
            this.dgv.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgv_Scroll);
            this.dgv.Enter += new System.EventHandler(this.dgv_Enter);
            this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
            // 
            // colEventCalendar
            // 
            this.colEventCalendar.DataPropertyName = "event_calendar";
            this.colEventCalendar.HeaderText = "Event Calendar Object";
            this.colEventCalendar.Name = "colEventCalendar";
            this.colEventCalendar.ReadOnly = true;
            this.colEventCalendar.Visible = false;
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
            // colName
            // 
            this.colName.DataPropertyName = "name";
            this.colName.HeaderText = "Name";
            this.colName.MinimumWidth = 38;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 38;
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
            // colEventDesc
            // 
            this.colEventDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEventDesc.DataPropertyName = "event_desc";
            this.colEventDesc.HeaderText = "Event Desc";
            this.colEventDesc.MinimumWidth = 2;
            this.colEventDesc.Name = "colEventDesc";
            this.colEventDesc.ReadOnly = true;
            // 
            // colCountableLeavePerson
            // 
            this.colCountableLeavePerson.DataPropertyName = "countable_leave_person";
            this.colCountableLeavePerson.HeaderText = "Countable";
            this.colCountableLeavePerson.Name = "colCountableLeavePerson";
            this.colCountableLeavePerson.ReadOnly = true;
            this.colCountableLeavePerson.Visible = false;
            // 
            // lblDay
            // 
            this.lblDay.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.lblDay.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDay.ForeColor = System.Drawing.Color.White;
            this.lblDay.Location = new System.Drawing.Point(1, 1);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(29, 24);
            this.lblDay.TabIndex = 3;
            this.lblDay.Text = "30";
            this.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDay.Click += new System.EventHandler(this.lblDay_Click);
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
            this.toolStrip1.Location = new System.Drawing.Point(300, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(99, 25);
            this.toolStrip1.TabIndex = 4;
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
            this.btnHoliday.Click += new System.EventHandler(this.btnHoliday_Click);
            // 
            // btnMaid
            // 
            this.btnMaid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnMaid.Image = global::SN_Net.Properties.Resources.maid;
            this.btnMaid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMaid.Name = "btnMaid";
            this.btnMaid.Size = new System.Drawing.Size(34, 20);
            this.btnMaid.Text = "_";
            this.btnMaid.Click += new System.EventHandler(this.btnMaid_Click);
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
            this.btnDropDownMenu.Click += new System.EventHandler(this.btnDropDownMenu_Click);
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
            this.btnTrainer.Click += new System.EventHandler(this.btnTrainer_Click);
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
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // lblBottomText
            // 
            this.lblBottomText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBottomText.BackColor = System.Drawing.Color.Transparent;
            this.lblBottomText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblBottomText.ForeColor = System.Drawing.Color.Blue;
            this.lblBottomText.Location = new System.Drawing.Point(3, 145);
            this.lblBottomText.Name = "lblBottomText";
            this.lblBottomText.Size = new System.Drawing.Size(394, 21);
            this.lblBottomText.TabIndex = 5;
            this.lblBottomText.Text = "label1";
            this.lblBottomText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBottomText.Click += new System.EventHandler(this.lblBottomText_Click);
            // 
            // lblNoteDescription
            // 
            this.lblNoteDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoteDescription.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblNoteDescription.ForeColor = System.Drawing.Color.Red;
            this.lblNoteDescription.Location = new System.Drawing.Point(1, 28);
            this.lblNoteDescription.Name = "lblNoteDescription";
            this.lblNoteDescription.Size = new System.Drawing.Size(398, 113);
            this.lblNoteDescription.TabIndex = 6;
            this.lblNoteDescription.Text = "หยุดชดเชยวันปิยะมหาราช";
            this.lblNoteDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoteDescription.Click += new System.EventHandler(this.lblNoteDescription_Click);
            // 
            // CustomDateEvent2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblBottomText);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.lblNoteDescription);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "CustomDateEvent2";
            this.Size = new System.Drawing.Size(400, 168);
            this.Load += new System.EventHandler(this.CustomDateEvent2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton btnDropDownMenu;
        private System.Windows.Forms.ToolStripMenuItem btnTrainer;
        private System.Windows.Forms.ToolStripMenuItem btnAdd;
        private System.Windows.Forms.ToolStripMenuItem btnDetail;
        private System.Windows.Forms.Label lblBottomText;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventCalendar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCountableLeavePerson;
        private System.Windows.Forms.ToolStripButton btnHoliday;
        private System.Windows.Forms.ToolStripButton btnMaid;
        private System.Windows.Forms.Label lblNoteDescription;
    }
}
