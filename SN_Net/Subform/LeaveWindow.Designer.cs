namespace SN_Net.Subform
{
    partial class LeaveWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeaveWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripExport = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProcessing = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvLeaveSummary = new System.Windows.Forms.DataGridView();
            this.dgvLeaveList = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaveSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaveList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPrint,
            this.toolStripExport});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 43);
            this.toolStrip1.TabIndex = 32;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripPrint
            // 
            this.toolStripPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrint.Image = global::SN_Net.Properties.Resources.printer;
            this.toolStripPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPrint.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripPrint.Name = "toolStripPrint";
            this.toolStripPrint.Size = new System.Drawing.Size(36, 40);
            this.toolStripPrint.Text = "Print <Alt+P>";
            this.toolStripPrint.Click += new System.EventHandler(this.toolStripPrint_Click);
            // 
            // toolStripExport
            // 
            this.toolStripExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripExport.Enabled = false;
            this.toolStripExport.Image = global::SN_Net.Properties.Resources.export;
            this.toolStripExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExport.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripExport.Name = "toolStripExport";
            this.toolStripExport.Size = new System.Drawing.Size(36, 40);
            this.toolStripExport.Text = "Export to CSV <F12>";
            this.toolStripExport.Click += new System.EventHandler(this.toolStripExport_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripInfo,
            this.toolStripProcessing});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 33;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripInfo
            // 
            this.toolStripInfo.BackColor = System.Drawing.Color.Transparent;
            this.toolStripInfo.Name = "toolStripInfo";
            this.toolStripInfo.Size = new System.Drawing.Size(22, 17);
            this.toolStripInfo.Text = "     ";
            // 
            // toolStripProcessing
            // 
            this.toolStripProcessing.BackColor = System.Drawing.Color.Transparent;
            this.toolStripProcessing.ForeColor = System.Drawing.Color.Green;
            this.toolStripProcessing.Image = ((System.Drawing.Image)(resources.GetObject("toolStripProcessing.Image")));
            this.toolStripProcessing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripProcessing.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripProcessing.Name = "toolStripProcessing";
            this.toolStripProcessing.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripProcessing.Size = new System.Drawing.Size(745, 17);
            this.toolStripProcessing.Spring = true;
            this.toolStripProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripProcessing.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 43);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtTo);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dtFrom);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cbUsers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.splitContainer1.Size = new System.Drawing.Size(792, 496);
            this.splitContainer1.SplitterDistance = 72;
            this.splitContainer1.TabIndex = 34;
            // 
            // dtTo
            // 
            this.dtTo.Checked = false;
            this.dtTo.CustomFormat = "dddd , d MMMM yyy";
            this.dtTo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(393, 39);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(240, 23);
            this.dtTo.TabIndex = 115;
            this.dtTo.Value = new System.DateTime(2015, 10, 9, 16, 11, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(351, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 16);
            this.label1.TabIndex = 114;
            this.label1.Text = "ถึง";
            // 
            // dtFrom
            // 
            this.dtFrom.Checked = false;
            this.dtFrom.CustomFormat = "dddd , d MMMM yyy";
            this.dtFrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(91, 39);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(240, 23);
            this.dtFrom.TabIndex = 113;
            this.dtFrom.Value = new System.DateTime(2015, 10, 9, 16, 11, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 112;
            this.label3.Text = "วันที่ จาก";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 111;
            this.label2.Text = "รหัสพนักงาน";
            // 
            // cbUsers
            // 
            this.cbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(91, 10);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(96, 24);
            this.cbUsers.TabIndex = 109;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvLeaveSummary);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvLeaveList);
            this.splitContainer2.Size = new System.Drawing.Size(786, 417);
            this.splitContainer2.SplitterDistance = 317;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 3;
            // 
            // dgvLeaveSummary
            // 
            this.dgvLeaveSummary.AllowUserToAddRows = false;
            this.dgvLeaveSummary.AllowUserToDeleteRows = false;
            this.dgvLeaveSummary.AllowUserToResizeColumns = false;
            this.dgvLeaveSummary.AllowUserToResizeRows = false;
            this.dgvLeaveSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgvLeaveSummary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeaveSummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLeaveSummary.ColumnHeadersHeight = 28;
            this.dgvLeaveSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLeaveSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLeaveSummary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLeaveSummary.EnableHeadersVisualStyles = false;
            this.dgvLeaveSummary.Location = new System.Drawing.Point(0, 0);
            this.dgvLeaveSummary.MultiSelect = false;
            this.dgvLeaveSummary.Name = "dgvLeaveSummary";
            this.dgvLeaveSummary.ReadOnly = true;
            this.dgvLeaveSummary.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvLeaveSummary.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLeaveSummary.RowTemplate.Height = 25;
            this.dgvLeaveSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLeaveSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeaveSummary.Size = new System.Drawing.Size(317, 417);
            this.dgvLeaveSummary.TabIndex = 116;
            // 
            // dgvLeaveList
            // 
            this.dgvLeaveList.AllowUserToAddRows = false;
            this.dgvLeaveList.AllowUserToDeleteRows = false;
            this.dgvLeaveList.AllowUserToResizeColumns = false;
            this.dgvLeaveList.AllowUserToResizeRows = false;
            this.dgvLeaveList.BackgroundColor = System.Drawing.Color.White;
            this.dgvLeaveList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeaveList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLeaveList.ColumnHeadersHeight = 28;
            this.dgvLeaveList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLeaveList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLeaveList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLeaveList.EnableHeadersVisualStyles = false;
            this.dgvLeaveList.Location = new System.Drawing.Point(0, 0);
            this.dgvLeaveList.MultiSelect = false;
            this.dgvLeaveList.Name = "dgvLeaveList";
            this.dgvLeaveList.ReadOnly = true;
            this.dgvLeaveList.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvLeaveList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLeaveList.RowTemplate.Height = 25;
            this.dgvLeaveList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeaveList.Size = new System.Drawing.Size(466, 417);
            this.dgvLeaveList.TabIndex = 2;
            // 
            // LeaveWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(792, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "LeaveWindow";
            this.Text = "สรุปวันลา, ออกพบลูกค้า";
            this.Load += new System.EventHandler(this.LeaveWindow_Load);
            this.Shown += new System.EventHandler(this.LeaveWindow_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaveSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaveList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripExport;
        private System.Windows.Forms.ToolStripButton toolStripPrint;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripInfo;
        public System.Windows.Forms.ToolStripStatusLabel toolStripProcessing;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvLeaveList;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLeaveSummary;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}