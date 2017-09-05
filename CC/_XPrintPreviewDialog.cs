using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC
{
    public class _XPrintPreviewDialog : PrintPreviewDialog
    {
        public event EventHandler _OutputToPrinter;

        private int total_page = 1000;
        private ToolStripButton btn_print;
        private ToolStripButton btn_close;
        private ToolStripButton btn_first;
        private ToolStripButton btn_previous;
        private ToolStripButton btn_next;
        private ToolStripButton btn_last;
        private ToolStripButton btn_zoom_in;
        private ToolStripButton btn_zoom_out;
        private ToolStripButton btn_zoom_fit;
        private ToolStripLabel lbl_page_num;
        private ToolStripButton btn_onepage;
        private ToolStripButton btn_twopages;
        private ToolStripButton btn_threepages;
        private ToolStripButton btn_fourpages;
        private ToolStripButton btn_sixpages;

        //public XPrintPreviewDialog()
        //{

        //}

        public _XPrintPreviewDialog(int total_page)
        {
            this.total_page = total_page;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ((ToolStripButton)((ToolStrip)this.Controls["toolStrip1"]).Items["printToolStripButton"]).Visible = false;
            ((ToolStripButton)((ToolStrip)this.Controls["toolStrip1"]).Items["closeToolStripButton"]).Visible = false;
            ((ToolStripLabel)((ToolStrip)this.Controls["toolStrip1"]).Items["pageToolStripLabel"]).Visible = false;
            ((ToolStripSeparator)((ToolStrip)this.Controls["toolStrip1"]).Items["separatorToolStripSeparator"]).Visible = false;
            ((ToolStripSeparator)((ToolStrip)this.Controls["toolStrip1"]).Items["separatorToolStripSeparator1"]).Visible = false;
            ((ToolStripSplitButton)((ToolStrip)this.Controls["toolStrip1"]).Items["zoomToolStripSplitButton"]).Visible = false;
            NumericUpDown num = (NumericUpDown)(((ToolStripControlHost)((ToolStrip)this.Controls["toolStrip1"]).Items[10])).Control;
            num.Visible = false;
            num.Maximum = this.total_page;
            num.ValueChanged += delegate (object sender, EventArgs ea)
            {
                this.lbl_page_num.Text = ((NumericUpDown)sender).Value.ToString() + " / " + this.total_page;
            };
            btn_onepage = (ToolStripButton)((ToolStrip)this.Controls["toolStrip1"]).Items["onepageToolStripButton"];
            btn_twopages = (ToolStripButton)((ToolStrip)this.Controls["toolStrip1"]).Items["twopagesToolStripButton"];
            btn_threepages = (ToolStripButton)((ToolStrip)this.Controls["toolStrip1"]).Items["threepagesToolStripButton"];
            btn_fourpages = (ToolStripButton)((ToolStrip)this.Controls["toolStrip1"]).Items["fourpagesToolStripButton"];
            btn_sixpages = (ToolStripButton)((ToolStrip)this.Controls["toolStrip1"]).Items["sixpagesToolStripButton"];

            btn_print = new ToolStripButton();
            btn_print.Image = CC.Properties.Resources.printer_16;
            btn_print.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_print.Text = "พิมพ์ออกเครื่องพิมพ์ <Alt + P>";
            btn_print.ToolTipText = "พิมพ์ออกเครื่องพิมพ์ <Alt + P>";
            btn_print.Click += delegate(object sender, EventArgs ea)
            {
                PrintDialog pd = new PrintDialog();
                pd.Document = this.Document;
                if(pd.ShowDialog() == DialogResult.OK)
                {
                    pd.Document.Print();
                }
            };

            btn_close = new ToolStripButton();
            btn_close.Image = CC.Properties.Resources.stop_16;
            btn_close.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_close.Text = "ปิด <Esc>";
            btn_close.ToolTipText = "ปิด <Esc>";
            btn_close.Click += delegate
            {
                this.Close();
            };

            lbl_page_num = new ToolStripLabel();
            lbl_page_num.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl_page_num.AutoSize = false;
            lbl_page_num.Width = 60;
            lbl_page_num.Font = new System.Drawing.Font("tahoma", 8f);
            lbl_page_num.Text = "1 / " + this.total_page;

            btn_first = new ToolStripButton();
            btn_first.Image = CC.Properties.Resources.first_16;
            btn_first.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_first.Text = "หน้าแรก <Ctrl + Home>";
            btn_first.ToolTipText = "หน้าแรก <Ctrl + Home>";
            btn_first.Click += delegate
            {
                num.Value = 1;
            };

            btn_previous = new ToolStripButton();
            btn_previous.Image = CC.Properties.Resources.previous_16;
            btn_previous.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_previous.Text = "หน้าที่แล้ว <PageUp>";
            btn_previous.ToolTipText = "หน้าที่แล้ว <PageUp>";
            btn_previous.Click += delegate
            {
                num.DownButton();
            };

            btn_next = new ToolStripButton();
            btn_next.Image = CC.Properties.Resources.next_16;
            btn_next.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_next.Text = "หน้าถัดไป <PageDown>";
            btn_next.ToolTipText = "หน้าถัดไป <PageDown>";
            btn_next.Click += delegate
            {
                num.UpButton();
            };

            btn_last = new ToolStripButton();
            btn_last.Image = CC.Properties.Resources.last_16;
            btn_last.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_last.Text = "หน้าสุดท้าย <Ctrl + End>";
            btn_last.ToolTipText = "หน้าสุดท้าย <Ctrl + End>";
            btn_last.Click += delegate
            {
                num.Value = num.Maximum;
            };

            btn_zoom_fit = new ToolStripButton();
            btn_zoom_fit.Image = CC.Properties.Resources.zoom_fit_16;
            btn_zoom_fit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_zoom_fit.Text = "ปรับขนาดให้พอดีกับหน้าต่าง <Ctrl + 0>";
            btn_zoom_fit.ToolTipText = "ปรับขนาดให้พอดีกับหน้าต่าง <Ctrl + 0>";
            btn_zoom_fit.Click += delegate
            {
                this.PrintPreviewControl.AutoZoom = true;
            };

            btn_zoom_in = new ToolStripButton();
            btn_zoom_in.Image = CC.Properties.Resources.zoom_in_16;
            btn_zoom_in.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_zoom_in.Text = "ขยาย <Ctrl + +>";
            btn_zoom_in.ToolTipText = "ขยาย <Ctrl + +>";
            btn_zoom_in.Click += delegate
            {
                this.PrintPreviewControl.Zoom += 0.2;
            };

            btn_zoom_out = new ToolStripButton();
            btn_zoom_out.Image = CC.Properties.Resources.zoom_out_16;
            btn_zoom_out.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_zoom_out.Text = "ย่อ <Ctrl + ->";
            btn_zoom_out.ToolTipText = "ย่อ <Ctrl + ->";
            btn_zoom_out.Click += delegate
            {
                if (this.PrintPreviewControl.Zoom <= 0.2)
                    return;

                this.PrintPreviewControl.Zoom -= 0.2;
            };

            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(0, btn_print);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(1, new ToolStripSeparator() { Margin = new Padding(3, 0, 3, 0) });

            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(2, btn_zoom_in);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(3, btn_zoom_out);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(4, btn_zoom_fit);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(5, new ToolStripSeparator() { Margin = new Padding(3, 0, 3, 0) });

            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(6, btn_onepage);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(7, btn_twopages);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(8, btn_threepages);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(9, btn_fourpages);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(10, btn_sixpages);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(11, new ToolStripSeparator() { Margin = new Padding(3, 0, 3, 0) });

            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(12, btn_first);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(13, btn_previous);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(14, lbl_page_num);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(15, btn_next);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(16, btn_last);
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(17, new ToolStripSeparator() { Margin = new Padding(3, 0, 3, 0) });
            ((ToolStrip)this.Controls["toolStrip1"]).Items.Insert(18, btn_close);

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.ShowIcon = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            //Console.WriteLine(".. >> mouse wheel");
        }

        public static int GetTotalPageCount(PrintDocument printDocument)
        {
            int count = 0;
            printDocument.PrintController = new PreviewPrintController();
            printDocument.PrintPage += (sender, e) => count++;
            printDocument.Print();
            return count;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btn_close.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.Home))
            {
                this.btn_first.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.End))
            {
                this.btn_last.PerformClick();
                return true;
            }

            if(keyData == Keys.PageUp)
            {
                this.btn_previous.PerformClick();
                return true;
            }

            if(keyData == Keys.PageDown)
            {
                this.btn_next.PerformClick();
                return true;
            }

            if(keyData == (Keys.Control | Keys.Oemplus))
            {
                this.btn_zoom_in.PerformClick();
                return true;
            }

            if(keyData == (Keys.Control | Keys.OemMinus))
            {
                this.btn_zoom_out.PerformClick();
                return true;
            }

            if(keyData == (Keys.Control | Keys.D0))
            {
                this.btn_zoom_fit.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XPrintPreviewDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(874, 547);
            this.Name = "XPrintPreviewDialog";
            this.ShowIcon = false;
            this.UseAntiAlias = true;
            this.ResumeLayout(false);

        }
    }
}
