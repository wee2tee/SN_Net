namespace SN_Net.Subform
{
    partial class DialogYearlyHoliday
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
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xDatagrid1 = new CC.XDatagrid();
            this.col_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnChangeYear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xDatagrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = global::SN_Net.Properties.Resources.add;
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 37);
            this.btnAdd.Text = "Add <Alt+A>";
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.Image = global::SN_Net.Properties.Resources.edit;
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 37);
            this.btnEdit.Text = "Edit <Alt+E>";
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::SN_Net.Properties.Resources.trash;
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 37);
            this.btnDelete.Text = "Delete <Alt+D>";
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = global::SN_Net.Properties.Resources.stop;
            this.btnStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(36, 37);
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
            this.btnSave.Size = new System.Drawing.Size(36, 37);
            this.btnSave.Text = "Save <F9>";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnEdit,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnStop,
            this.btnSave,
            this.toolStripSeparator2,
            this.btnChangeYear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(518, 40);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xDatagrid1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 512);
            this.panel1.TabIndex = 5;
            // 
            // xDatagrid1
            // 
            this.xDatagrid1.AllowSortByColumnHeaderClicked = false;
            this.xDatagrid1.AllowUserToAddRows = false;
            this.xDatagrid1.AllowUserToDeleteRows = false;
            this.xDatagrid1.AllowUserToResizeColumns = false;
            this.xDatagrid1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.xDatagrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.xDatagrid1.ColumnHeadersHeight = 28;
            this.xDatagrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.xDatagrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_seq,
            this.col_day,
            this.col_date,
            this.col_description});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.xDatagrid1.DefaultCellStyle = dataGridViewCellStyle2;
            this.xDatagrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xDatagrid1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.xDatagrid1.EnableHeadersVisualStyles = false;
            this.xDatagrid1.FillEmptyRow = false;
            this.xDatagrid1.FocusedRowBorderRedLine = true;
            this.xDatagrid1.Location = new System.Drawing.Point(0, 0);
            this.xDatagrid1.MultiSelect = false;
            this.xDatagrid1.Name = "xDatagrid1";
            this.xDatagrid1.ReadOnly = true;
            this.xDatagrid1.RowHeadersVisible = false;
            this.xDatagrid1.RowTemplate.Height = 26;
            this.xDatagrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.xDatagrid1.Size = new System.Drawing.Size(518, 512);
            this.xDatagrid1.StandardTab = true;
            this.xDatagrid1.TabIndex = 0;
            // 
            // col_seq
            // 
            this.col_seq.DataPropertyName = "seq";
            this.col_seq.HeaderText = "ลำดับ";
            this.col_seq.MinimumWidth = 40;
            this.col_seq.Name = "col_seq";
            this.col_seq.ReadOnly = true;
            this.col_seq.Width = 40;
            // 
            // col_day
            // 
            this.col_day.DataPropertyName = "day";
            this.col_day.HeaderText = "วัน";
            this.col_day.MinimumWidth = 90;
            this.col_day.Name = "col_day";
            this.col_day.ReadOnly = true;
            this.col_day.Width = 90;
            // 
            // col_date
            // 
            this.col_date.DataPropertyName = "date";
            this.col_date.HeaderText = "วันที่";
            this.col_date.MinimumWidth = 110;
            this.col_date.Name = "col_date";
            this.col_date.ReadOnly = true;
            this.col_date.Width = 110;
            // 
            // col_description
            // 
            this.col_description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_description.DataPropertyName = "description";
            this.col_description.HeaderText = "รายละเอียด";
            this.col_description.Name = "col_description";
            this.col_description.ReadOnly = true;
            // 
            // btnChangeYear
            // 
            this.btnChangeYear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnChangeYear.Image = global::SN_Net.Properties.Resources.change_scope;
            this.btnChangeYear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnChangeYear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeYear.Name = "btnChangeYear";
            this.btnChangeYear.Size = new System.Drawing.Size(36, 37);
            this.btnChangeYear.Text = "เปลี่ยนปี";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // DialogYearlyHoliday
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 552);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DialogYearlyHoliday";
            this.ShowIcon = false;
            this.Text = "DialogYearlyHoliday";
            this.Load += new System.EventHandler(this.DialogYearlyHoliday_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xDatagrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnSave;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel1;
        private CC.XDatagrid xDatagrid1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_day;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_description;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnChangeYear;
    }
}