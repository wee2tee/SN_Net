namespace CC
{
    partial class XPrintPreview
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnZoomFit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSplitNone = new System.Windows.Forms.ToolStripButton();
            this.btnSplit2 = new System.Windows.Forms.ToolStripButton();
            this.btnSplit4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.txtPageNum = new System.Windows.Forms.ToolStripTextBox();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrint,
            this.toolStripSeparator1,
            this.btnZoomIn,
            this.btnZoomOut,
            this.btnZoomFit,
            this.toolStripSeparator4,
            this.btnSplitNone,
            this.btnSplit2,
            this.btnSplit4,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.btnFirst,
            this.btnPrevious,
            this.txtPageNum,
            this.btnNext,
            this.btnLast,
            this.toolStripSeparator3,
            this.btnClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(888, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::CC.Properties.Resources.printer_16;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.Text = "พิมพ์ออกเครื่องพิมพ์ <Ctrl + P> , <Alt + P>";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = global::CC.Properties.Resources.zoom_in_16;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.btnZoomIn.Text = "ขยาย <Ctrl + +>";
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = global::CC.Properties.Resources.zoom_out_16;
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.btnZoomOut.Text = "ย่อ <Ctrl + ->";
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomFit
            // 
            this.btnZoomFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomFit.Image = global::CC.Properties.Resources.zoom_fit_16;
            this.btnZoomFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomFit.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.btnZoomFit.Name = "btnZoomFit";
            this.btnZoomFit.Size = new System.Drawing.Size(23, 22);
            this.btnZoomFit.Text = "ปรับให้พอดีกับขนาดหน้าต่าง <Ctrl + 0>";
            this.btnZoomFit.Click += new System.EventHandler(this.btnZoomFit_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSplitNone
            // 
            this.btnSplitNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSplitNone.Image = global::CC.Properties.Resources.page_split_none_16;
            this.btnSplitNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSplitNone.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.btnSplitNone.Name = "btnSplitNone";
            this.btnSplitNone.Size = new System.Drawing.Size(23, 22);
            this.btnSplitNone.Text = "แสดงผลแบบ 1 หน้า <Ctrl + 1>";
            this.btnSplitNone.Click += new System.EventHandler(this.btnSplitNone_Click);
            // 
            // btnSplit2
            // 
            this.btnSplit2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSplit2.Image = global::CC.Properties.Resources.page_split2_16;
            this.btnSplit2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSplit2.Name = "btnSplit2";
            this.btnSplit2.Size = new System.Drawing.Size(23, 22);
            this.btnSplit2.Text = "แสดงผล 2 หน้า <Ctrl + 2>";
            this.btnSplit2.Click += new System.EventHandler(this.btnSplit2_Click);
            // 
            // btnSplit4
            // 
            this.btnSplit4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSplit4.Image = global::CC.Properties.Resources.page_split4_16;
            this.btnSplit4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSplit4.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.btnSplit4.Name = "btnSplit4";
            this.btnSplit4.Size = new System.Drawing.Size(23, 22);
            this.btnSplit4.Text = "แสดงผล 4 หน้า <Ctrl + 4>";
            this.btnSplit4.Click += new System.EventHandler(this.btnSplit4_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(30, 22);
            this.toolStripLabel1.Text = "หน้า";
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = global::CC.Properties.Resources.first_16;
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(23, 22);
            this.btnFirst.Text = "หน้าแรก <Ctrl+Home>";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrevious.Image = global::CC.Properties.Resources.previous_16;
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(23, 22);
            this.btnPrevious.Text = "หน้าก่อนหน้า <Page Up>";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // txtPageNum
            // 
            this.txtPageNum.AutoSize = false;
            this.txtPageNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPageNum.Enabled = false;
            this.txtPageNum.Name = "txtPageNum";
            this.txtPageNum.Size = new System.Drawing.Size(50, 23);
            this.txtPageNum.Text = "0";
            this.txtPageNum.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPageNum.TextChanged += new System.EventHandler(this.txtPageNum_TextChanged);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = global::CC.Properties.Resources.next_16;
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 22);
            this.btnNext.Text = "หน้าถัดไป <Page Down>";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = global::CC.Properties.Resources.last_16;
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLast.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(23, 22);
            this.btnLast.Text = "หน้าสุดท้าย <Ctrl+End>";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = global::CC.Properties.Resources.stop_16;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "ปิด <Escape>";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // printPreviewControl1
            // 
            this.printPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl1.Location = new System.Drawing.Point(0, 25);
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.Size = new System.Drawing.Size(888, 500);
            this.printPreviewControl1.TabIndex = 1;
            // 
            // XPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 525);
            this.Controls.Add(this.printPreviewControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "XPrintPreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ตัวอย่างก่อนพิมพ์";
            this.Load += new System.EventHandler(this.FormPrintPreview_Load);
            this.Shown += new System.EventHandler(this.XPrintPreview_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.PrintPreviewControl printPreviewControl1;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrevious;
        private System.Windows.Forms.ToolStripTextBox txtPageNum;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnSplit2;
        private System.Windows.Forms.ToolStripButton btnSplit4;
        private System.Windows.Forms.ToolStripButton btnZoomFit;
        private System.Windows.Forms.ToolStripButton btnSplitNone;
        public System.Windows.Forms.ToolStripButton btnPrint;
    }
}