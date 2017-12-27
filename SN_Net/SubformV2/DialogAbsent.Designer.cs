namespace SN_Net.Subform
{
    partial class DialogAbsent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnItem = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numLeaveCount = new System.Windows.Forms.NumericUpDown();
            this.dlGroupMaid = new CC.XDropdownList();
            this.dlGroupHoliday = new CC.XDropdownList();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemark = new CC.XTextEdit();
            this.txtHoliday = new CC.XTextEdit();
            this.rdWeekday = new System.Windows.Forms.RadioButton();
            this.rdHoliday = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv = new CC.XDatagrid();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnEditItem = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLeaveCount)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEdit,
            this.btnStop,
            this.btnSave,
            this.toolStripSeparator1,
            this.btnItem});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(785, 43);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.Image = global::SN_Net.Properties.Resources.edit;
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 40);
            this.btnEdit.Text = "Edit <Alt+E>";
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = global::SN_Net.Properties.Resources.stop;
            this.btnStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(36, 40);
            this.btnStop.Text = "Cancel Add/Edit <ESC>";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::SN_Net.Properties.Resources.save;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 40);
            this.btnSave.Text = "Save <F9>";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // btnItem
            // 
            this.btnItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnItem.Image = global::SN_Net.Properties.Resources.item;
            this.btnItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnItem.Margin = new System.Windows.Forms.Padding(2, 1, 1, 2);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(36, 40);
            this.btnItem.Text = "Entrance to item <F8>";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numLeaveCount);
            this.groupBox1.Controls.Add(this.dlGroupMaid);
            this.groupBox1.Controls.Add(this.dlGroupHoliday);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.txtHoliday);
            this.groupBox1.Controls.Add(this.rdWeekday);
            this.groupBox1.Controls.Add(this.rdHoliday);
            this.groupBox1.Location = new System.Drawing.Point(12, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(761, 176);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // numLeaveCount
            // 
            this.numLeaveCount.Location = new System.Drawing.Point(452, 111);
            this.numLeaveCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numLeaveCount.Name = "numLeaveCount";
            this.numLeaveCount.Size = new System.Drawing.Size(51, 23);
            this.numLeaveCount.TabIndex = 4;
            this.numLeaveCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // dlGroupMaid
            // 
            this.dlGroupMaid._ReadOnly = false;
            this.dlGroupMaid._SelectedItem = null;
            this.dlGroupMaid._Text = "";
            this.dlGroupMaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dlGroupMaid.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dlGroupMaid.Location = new System.Drawing.Point(164, 111);
            this.dlGroupMaid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dlGroupMaid.Name = "dlGroupMaid";
            this.dlGroupMaid.Size = new System.Drawing.Size(132, 23);
            this.dlGroupMaid.TabIndex = 3;
            // 
            // dlGroupHoliday
            // 
            this.dlGroupHoliday._ReadOnly = false;
            this.dlGroupHoliday._SelectedItem = null;
            this.dlGroupHoliday._Text = "";
            this.dlGroupHoliday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dlGroupHoliday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dlGroupHoliday.Location = new System.Drawing.Point(164, 84);
            this.dlGroupHoliday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dlGroupHoliday.Name = "dlGroupHoliday";
            this.dlGroupHoliday.Size = new System.Drawing.Size(132, 23);
            this.dlGroupHoliday.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "หมายเหตุ :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(504, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "คน";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "ลางาน/ออกพบลูกค้าได้";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "ทำความสะอาด :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "วันหยุดพิเศษสำหรับ :";
            // 
            // txtRemark
            // 
            this.txtRemark._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtRemark._ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRemark._MaxLength = 50;
            this.txtRemark._PasswordChar = '\0';
            this.txtRemark._ReadOnly = false;
            this.txtRemark._SelectionLength = 0;
            this.txtRemark._SelectionStart = 0;
            this.txtRemark._Text = "";
            this.txtRemark._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtRemark.BackColor = System.Drawing.Color.White;
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtRemark.Location = new System.Drawing.Point(164, 139);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(360, 23);
            this.txtRemark.TabIndex = 1;
            // 
            // txtHoliday
            // 
            this.txtHoliday._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoliday._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtHoliday._ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtHoliday._MaxLength = 50;
            this.txtHoliday._PasswordChar = '\0';
            this.txtHoliday._ReadOnly = false;
            this.txtHoliday._SelectionLength = 0;
            this.txtHoliday._SelectionStart = 0;
            this.txtHoliday._Text = "";
            this.txtHoliday._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtHoliday.BackColor = System.Drawing.Color.White;
            this.txtHoliday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoliday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHoliday.Location = new System.Drawing.Point(88, 20);
            this.txtHoliday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHoliday.Name = "txtHoliday";
            this.txtHoliday.Size = new System.Drawing.Size(220, 23);
            this.txtHoliday.TabIndex = 1;
            // 
            // rdWeekday
            // 
            this.rdWeekday.AutoSize = true;
            this.rdWeekday.Location = new System.Drawing.Point(15, 58);
            this.rdWeekday.Name = "rdWeekday";
            this.rdWeekday.Size = new System.Drawing.Size(99, 20);
            this.rdWeekday.TabIndex = 0;
            this.rdWeekday.TabStop = true;
            this.rdWeekday.Text = "วันทำการปกติ";
            this.rdWeekday.UseVisualStyleBackColor = true;
            // 
            // rdHoliday
            // 
            this.rdHoliday.AutoSize = true;
            this.rdHoliday.Location = new System.Drawing.Point(15, 22);
            this.rdHoliday.Name = "rdHoliday";
            this.rdHoliday.Size = new System.Drawing.Size(64, 20);
            this.rdHoliday.TabIndex = 0;
            this.rdHoliday.TabStop = true;
            this.rdHoliday.Text = "วันหยุด";
            this.rdHoliday.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Controls.Add(this.btnDeleteItem);
            this.panel1.Controls.Add(this.btnEditItem);
            this.panel1.Controls.Add(this.btnAddItem);
            this.panel1.Location = new System.Drawing.Point(4, 228);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(777, 273);
            this.panel1.TabIndex = 10;
            // 
            // dgv
            // 
            this.dgv.AllowSortByColumnHeaderClicked = false;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.FillEmptyRow = false;
            this.dgv.FocusedRowBorderRedLine = false;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 26;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(777, 273);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 0;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(9, 11);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(44, 23);
            this.btnAddItem.TabIndex = 1;
            this.btnAddItem.Text = "Add";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnEditItem
            // 
            this.btnEditItem.Location = new System.Drawing.Point(9, 40);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Size = new System.Drawing.Size(44, 23);
            this.btnEditItem.TabIndex = 1;
            this.btnEditItem.Text = "Edit";
            this.btnEditItem.UseVisualStyleBackColor = true;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(9, 69);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(44, 23);
            this.btnDeleteItem.TabIndex = 1;
            this.btnDeleteItem.Text = "Delete";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // DialogAbsent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 505);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DialogAbsent";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "เพิ่ม/แก้ไข เหตการณ์ประจำวัน";
            this.Load += new System.EventHandler(this.DialogAbsent_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLeaveCount)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numLeaveCount;
        private CC.XDropdownList dlGroupMaid;
        private CC.XDropdownList dlGroupHoliday;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CC.XTextEdit txtRemark;
        private CC.XTextEdit txtHoliday;
        private System.Windows.Forms.RadioButton rdWeekday;
        private System.Windows.Forms.RadioButton rdHoliday;
        private System.Windows.Forms.Panel panel1;
        private CC.XDatagrid dgv;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnEditItem;
        private System.Windows.Forms.Button btnAddItem;
    }
}