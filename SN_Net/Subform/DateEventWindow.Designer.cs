namespace SN_Net.Subform
{
    partial class DateEventWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateEventWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProcessing = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItem = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbGroupMaid = new SN_Net.MiscClass.CustomComboBox();
            this.cbGroupWeekend = new SN_Net.MiscClass.CustomComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.leaveMax = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDummy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbWeekday = new System.Windows.Forms.RadioButton();
            this.rbHoliday = new System.Windows.Forms.RadioButton();
            this.txtHoliday = new SN_Net.MiscClass.CustomTextBox();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leaveMax)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripInfo,
            this.toolStripProcessing});
            this.statusStrip1.Location = new System.Drawing.Point(0, 520);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(903, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
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
            this.toolStripProcessing.Size = new System.Drawing.Size(856, 17);
            this.toolStripProcessing.Spring = true;
            this.toolStripProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripProcessing.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEdit,
            this.toolStripStop,
            this.toolStripSave,
            this.toolStripSeparator1,
            this.toolStripItem});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(903, 43);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripEdit
            // 
            this.toolStripEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEdit.Image = global::SN_Net.Properties.Resources.edit;
            this.toolStripEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEdit.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripEdit.Name = "toolStripEdit";
            this.toolStripEdit.Size = new System.Drawing.Size(36, 40);
            this.toolStripEdit.Text = "Edit <Alt+E>";
            this.toolStripEdit.Click += new System.EventHandler(this.toolStripEdit_Click);
            // 
            // toolStripStop
            // 
            this.toolStripStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStop.Image = global::SN_Net.Properties.Resources.stop;
            this.toolStripStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripStop.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripStop.Name = "toolStripStop";
            this.toolStripStop.Size = new System.Drawing.Size(36, 40);
            this.toolStripStop.Text = "Cancel Add/Edit <ESC>";
            this.toolStripStop.Click += new System.EventHandler(this.toolStripStop_Click);
            // 
            // toolStripSave
            // 
            this.toolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSave.Image = global::SN_Net.Properties.Resources.save;
            this.toolStripSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripSave.Name = "toolStripSave";
            this.toolStripSave.Size = new System.Drawing.Size(36, 40);
            this.toolStripSave.Text = "Save <F9>";
            this.toolStripSave.Click += new System.EventHandler(this.toolStripSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripItem
            // 
            this.toolStripItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItem.Image = global::SN_Net.Properties.Resources.item;
            this.toolStripItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItem.Margin = new System.Windows.Forms.Padding(2, 1, 1, 2);
            this.toolStripItem.Name = "toolStripItem";
            this.toolStripItem.Size = new System.Drawing.Size(36, 40);
            this.toolStripItem.Text = "Entrance to item <F8>";
            this.toolStripItem.Click += new System.EventHandler(this.toolStripItem_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeight = 28;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.Location = new System.Drawing.Point(2, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.RowTemplate.Height = 25;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(899, 311);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 1;
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.splitContainer1.Size = new System.Drawing.Size(903, 477);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbGroupMaid);
            this.groupBox1.Controls.Add(this.cbGroupWeekend);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.leaveMax);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDummy);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbWeekday);
            this.groupBox1.Controls.Add(this.rbHoliday);
            this.groupBox1.Controls.Add(this.txtHoliday);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(561, 148);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cbGroupMaid
            // 
            this.cbGroupMaid.BackColor = System.Drawing.Color.White;
            this.cbGroupMaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cbGroupMaid.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbGroupMaid.Location = new System.Drawing.Point(156, 117);
            this.cbGroupMaid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbGroupMaid.Name = "cbGroupMaid";
            this.cbGroupMaid.Read_Only = true;
            this.cbGroupMaid.Size = new System.Drawing.Size(153, 25);
            this.cbGroupMaid.TabIndex = 4;
            this.cbGroupMaid.Texts = null;
            // 
            // cbGroupWeekend
            // 
            this.cbGroupWeekend.BackColor = System.Drawing.Color.White;
            this.cbGroupWeekend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cbGroupWeekend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbGroupWeekend.Location = new System.Drawing.Point(156, 88);
            this.cbGroupWeekend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbGroupWeekend.Name = "cbGroupWeekend";
            this.cbGroupWeekend.Read_Only = true;
            this.cbGroupWeekend.Size = new System.Drawing.Size(153, 25);
            this.cbGroupWeekend.TabIndex = 3;
            this.cbGroupWeekend.Texts = null;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "วันหยุดพิเศษสำหรับ :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(512, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "คน";
            // 
            // leaveMax
            // 
            this.leaveMax.BackColor = System.Drawing.Color.White;
            this.leaveMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leaveMax.Location = new System.Drawing.Point(466, 118);
            this.leaveMax.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.leaveMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.leaveMax.Name = "leaveMax";
            this.leaveMax.Size = new System.Drawing.Size(40, 23);
            this.leaveMax.TabIndex = 5;
            this.leaveMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.leaveMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(331, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "ลางาน/ออกพบลูกค้าได้";
            // 
            // txtDummy
            // 
            this.txtDummy.Location = new System.Drawing.Point(336, 22);
            this.txtDummy.Name = "txtDummy";
            this.txtDummy.Size = new System.Drawing.Size(102, 23);
            this.txtDummy.TabIndex = 7;
            this.txtDummy.Text = "put focus here";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "ทำความสะอาด :";
            // 
            // rbWeekday
            // 
            this.rbWeekday.AutoSize = true;
            this.rbWeekday.Enabled = false;
            this.rbWeekday.Location = new System.Drawing.Point(19, 59);
            this.rbWeekday.Name = "rbWeekday";
            this.rbWeekday.Size = new System.Drawing.Size(99, 20);
            this.rbWeekday.TabIndex = 2;
            this.rbWeekday.TabStop = true;
            this.rbWeekday.Text = "วันทำการปกติ";
            this.rbWeekday.UseVisualStyleBackColor = true;
            // 
            // rbHoliday
            // 
            this.rbHoliday.AutoSize = true;
            this.rbHoliday.Enabled = false;
            this.rbHoliday.Location = new System.Drawing.Point(19, 27);
            this.rbHoliday.Name = "rbHoliday";
            this.rbHoliday.Size = new System.Drawing.Size(64, 20);
            this.rbHoliday.TabIndex = 0;
            this.rbHoliday.TabStop = true;
            this.rbHoliday.Text = "วันหยุด";
            this.rbHoliday.UseVisualStyleBackColor = true;
            // 
            // txtHoliday
            // 
            this.txtHoliday.BackColor = System.Drawing.Color.White;
            this.txtHoliday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoliday.CharUpperCase = false;
            this.txtHoliday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHoliday.Location = new System.Drawing.Point(84, 26);
            this.txtHoliday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHoliday.MaxChar = 50;
            this.txtHoliday.Name = "txtHoliday";
            this.txtHoliday.Read_Only = true;
            this.txtHoliday.SelectionLength = 0;
            this.txtHoliday.SelectionStart = 0;
            this.txtHoliday.Size = new System.Drawing.Size(186, 23);
            this.txtHoliday.TabIndex = 1;
            this.txtHoliday.Texts = "";
            // 
            // DateEventWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 542);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateEventWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "เพิ้ม/แก้ไข เหตุการณ์ประจำวัน";
            this.Load += new System.EventHandler(this.DateEventWindow_Load);
            this.Shown += new System.EventHandler(this.DateEventWindow_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leaveMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripInfo;
        public System.Windows.Forms.ToolStripStatusLabel toolStripProcessing;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripEdit;
        private System.Windows.Forms.ToolStripButton toolStripStop;
        private System.Windows.Forms.ToolStripButton toolStripSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripItem;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbWeekday;
        private System.Windows.Forms.RadioButton rbHoliday;
        private MiscClass.CustomTextBox txtHoliday;
        private System.Windows.Forms.TextBox txtDummy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown leaveMax;
        private System.Windows.Forms.Label label4;
        private MiscClass.CustomComboBox cbGroupWeekend;
        private MiscClass.CustomComboBox cbGroupMaid;
    }
}