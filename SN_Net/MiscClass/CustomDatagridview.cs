using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    public partial class CustomDatagridview : DataGridView
    {
        private Font _background_text_font = new Font("tahoma", 12f, FontStyle.Regular);
        public Font _BackgroundTextFont
        {
            get
            {
                return this._background_text_font;
            }
            set
            {
                this._background_text_font = value;
            }
        }

        private Color _background_text_color = Color.Black;
        public Color _BackgroundTextcolor
        {
            get
            {
                return this._background_text_color;
            }
            set
            {
                this._background_text_color = value;
            }
        }

        private string _background_text = string.Empty;
        public string _BackgroundText
        {
            get
            {
                return this._background_text;
            }
            set
            {
                this._background_text = value;
            }
        }

        private bool background_painted = false;

        public CustomDatagridview()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.CellPainting += CustomDatagridview_CellPainting;
        }

        private void CustomDatagridview_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.RowIndex > -1)
            {
                //e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily.Name, e.CellStyle.Font.Size, FontStyle.Bold);
                //e.CellStyle.ForeColor = Color.White;
                //e.Paint(e.ClipBounds, DataGridViewPaintParts.ContentForeground);
                //e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily.Name, e.CellStyle.Font.Size, FontStyle.Regular);
                //e.CellStyle.ForeColor = Color.Black;
                //e.Paint(e.ClipBounds, DataGridViewPaintParts.Border | DataGridViewPaintParts.Background | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground);
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                e.Handled = true;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    pevent.Graphics.DrawString("Sample text", new Font("tahoma", 12f, FontStyle.Regular), new SolidBrush(Color.Red), pevent.ClipRectangle, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        //}

        protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
        {
            base.PaintBackground(graphics, clipBounds, gridBounds);

            if(this.background_painted == false)
            {
                graphics.FillRectangle(new SolidBrush(this.BackgroundColor), clipBounds);
                graphics.DrawString(this._BackgroundText, this._BackgroundTextFont, new SolidBrush(Color.FromArgb(80, this._BackgroundTextcolor)), clipBounds, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                this.background_painted = true;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.background_painted = false;
            this.Refresh();
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);
            this.background_painted = false;
            this.Refresh();
        }
    }
}
