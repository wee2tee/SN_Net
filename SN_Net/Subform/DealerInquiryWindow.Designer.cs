namespace SN_Net.Subform
{
    partial class DealerInquiryWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DealerInquiryWindow));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvDealer = new System.Windows.Forms.DataGridView();
            this.lblLoading = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSelectedID = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLoadedRec = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTotalRec = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProcessing = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDealer)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.splitContainer1.Panel1.Controls.Add(this.dgvDealer);
            this.splitContainer1.Panel1.Controls.Add(this.lblLoading);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Size = new System.Drawing.Size(942, 395);
            this.splitContainer1.SplitterDistance = 309;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // dgvDealer
            // 
            this.dgvDealer.AllowUserToAddRows = false;
            this.dgvDealer.AllowUserToDeleteRows = false;
            this.dgvDealer.AllowUserToOrderColumns = true;
            this.dgvDealer.AllowUserToResizeColumns = false;
            this.dgvDealer.AllowUserToResizeRows = false;
            this.dgvDealer.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.YellowGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDealer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDealer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDealer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDealer.EnableHeadersVisualStyles = false;
            this.dgvDealer.Location = new System.Drawing.Point(3, 3);
            this.dgvDealer.MultiSelect = false;
            this.dgvDealer.Name = "dgvDealer";
            this.dgvDealer.ReadOnly = true;
            this.dgvDealer.RowHeadersVisible = false;
            this.dgvDealer.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvDealer.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvDealer.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dgvDealer.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvDealer.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgvDealer.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDealer.RowTemplate.Height = 25;
            this.dgvDealer.RowTemplate.ReadOnly = true;
            this.dgvDealer.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDealer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDealer.Size = new System.Drawing.Size(299, 79);
            this.dgvDealer.TabIndex = 0;
            this.dgvDealer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDealer_CellDoubleClick);
            // 
            // lblLoading
            // 
            this.lblLoading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblLoading.Location = new System.Drawing.Point(322, 9);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(193, 29);
            this.lblLoading.TabIndex = 1;
            this.lblLoading.Text = "Loading please wait ...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(82, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(10, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 40);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSelectedID,
            this.toolStripStatusLabel1,
            this.toolStripLoadedRec,
            this.toolStripStatusLabel3,
            this.toolStripTotalRec,
            this.toolStripStatusLabel2,
            this.toolStripProcessing});
            this.statusStrip1.Location = new System.Drawing.Point(0, 373);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(942, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSelectedID
            // 
            this.toolStripSelectedID.Name = "toolStripSelectedID";
            this.toolStripSelectedID.Size = new System.Drawing.Size(13, 17);
            this.toolStripSelectedID.Text = "  ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = " , ";
            // 
            // toolStripLoadedRec
            // 
            this.toolStripLoadedRec.Name = "toolStripLoadedRec";
            this.toolStripLoadedRec.Size = new System.Drawing.Size(10, 17);
            this.toolStripLoadedRec.Text = " ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(18, 17);
            this.toolStripStatusLabel3.Text = "of";
            // 
            // toolStripTotalRec
            // 
            this.toolStripTotalRec.Name = "toolStripTotalRec";
            this.toolStripTotalRec.Size = new System.Drawing.Size(10, 17);
            this.toolStripTotalRec.Text = " ";
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
            this.toolStripProcessing.Size = new System.Drawing.Size(723, 17);
            this.toolStripProcessing.Spring = true;
            this.toolStripProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripProcessing.Visible = false;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(96, 17);
            this.toolStripStatusLabel2.Text = "record(s) loaded.";
            // 
            // DealerInquiryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 395);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "DealerInquiryWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inquiry";
            this.Load += new System.EventHandler(this.DealerInquiryWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDealer)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvDealer;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSelectedID;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLoadedRec;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripTotalRec;
        public System.Windows.Forms.ToolStripStatusLabel toolStripProcessing;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}