using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC
{
    public partial class XDatagrid : DataGridView
    {
        private bool row_border_redline;
        public bool FocusedRowBorderRedLine {
            get {
                return this.row_border_redline;
            }
            set
            {
                this.row_border_redline = value;
                this.ResetColor();
            }
        }

        private bool allow_sort_by_column_header_clicked;
        public bool AllowSortByColumnHeaderClicked
        {
            get
            {
                return this.allow_sort_by_column_header_clicked;
            }
            set
            {
                this.allow_sort_by_column_header_clicked = value;
            }
        }

        private bool fill_empty_row = false;
        public bool FillEmptyRow
        {
            get
            {
                return this.fill_empty_row;
            }
            set
            {
                this.fill_empty_row = value;
            }
        }

        private int sort_column = -1;
        public int SortColumn
        {
            get
            {
                return this.sort_column;
            }
        }

        private bool sort_asc = true;
        public bool SortASC
        {
            get
            {
                return this.sort_asc;
            }
        }

        public XDatagrid()
        {
            InitializeComponent();

            
            this.ColumnHeadersHeight = 28;
            this.RowTemplate.Height = 26;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;

            this.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(231, 207, 179); //Color.PeachPuff;

            if (this.row_border_redline)
            {
                this.DefaultCellStyle.BackColor = Color.White;
                this.DefaultCellStyle.ForeColor = Color.Black;
                this.DefaultCellStyle.SelectionBackColor = Color.White;
                this.DefaultCellStyle.SelectionForeColor = Color.Black;
            }
            using (Font tahoma = new Font("tahoma", 9.75f))
            {
                this.DefaultCellStyle.Font = tahoma;
            }

            this.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.EnableHeadersVisualStyles = false;

            this.MultiSelect = false;
            this.ReadOnly = true;
            this.RowHeadersVisible = false;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.StandardTab = true;

            this.VerticalScrollBar.Scroll += VerticalScrollBar_Scroll;
        }

        private void VerticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //if(e.NewValue > e.OldValue) // scroll down
            //{
            //    this.Rows[this.FirstDisplayedScrollingRowIndex + this.DisplayedRowCount(false)].Cells[this.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).First().Name].Selected = true;
            //    return;
            //}
            //if(e.NewValue < e.OldValue) // scroll up
            //{
            //    this.Rows[this.FirstDisplayedScrollingRowIndex].Cells[this.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).First().Name].Selected = true;
            //    return;
            //}

            this.Rows[this.FirstDisplayedScrollingRowIndex].Cells[this.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).First().Name].Selected = true;
        }

        protected override void OnCreateControl()
        {
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            base.OnCreateControl();
            
            this.AttachEventHandler();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            if (this.row_border_redline)
            {
                this.Refresh();
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (this.row_border_redline)
            {
                this.Refresh();
            }
        }

        private void ResetColor()
        {
            if (this.row_border_redline)
            {
                this.DefaultCellStyle.BackColor = Color.White;
                this.DefaultCellStyle.ForeColor = Color.Black;
                this.DefaultCellStyle.SelectionBackColor = Color.White;
                this.DefaultCellStyle.SelectionForeColor = Color.Black;
            }
            else
            {
                this.DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
                this.DefaultCellStyle.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                this.DefaultCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight);
                this.DefaultCellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText);
            }
        }

        private void AttachEventHandler()
        {
            this.Paint += new PaintEventHandler(this.DgvPainted);

            if (this.allow_sort_by_column_header_clicked)
            {
                this.CellPainting += new DataGridViewCellPaintingEventHandler(this.DrawColumnHeaderSortSign);
            }
        }

        private void DrawColumnHeaderSortSign(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
            
            if (e.RowIndex == -1 && e.ColumnIndex == this.SortColumn)
            {
                using (SolidBrush brush = new SolidBrush(Color.Sienna))
                {
                    if (!this.sort_asc)
                    {
                        /* แสดงแบบ Z->A */
                        using (Font font = new Font("tahoma", 5f))
                        {
                            e.Graphics.DrawString("Z", font, brush, new Point(e.CellBounds.X + e.CellBounds.Width - 16, e.CellBounds.Y + 6));
                            e.Graphics.DrawString("A", font, brush, new Point(e.CellBounds.X + e.CellBounds.Width - 16, e.CellBounds.Y + 13));
                        }
                        using (Pen pen = new Pen(Color.Sienna))
                        {
                            e.Graphics.DrawLine(pen, new Point(e.CellBounds.X + e.CellBounds.Width - 7, e.CellBounds.Y + 9), new Point(e.CellBounds.X + e.CellBounds.Width - 7, e.CellBounds.Y + 19));
                        }
                        e.Graphics.FillPolygon(brush, new Point[] { new Point(e.CellBounds.X + e.CellBounds.Width - 9, e.CellBounds.Y + 17), new Point(e.CellBounds.X + e.CellBounds.Width - 4, e.CellBounds.Y + 17), new Point(e.CellBounds.X + e.CellBounds.Width - 7, e.CellBounds.Y + 19) });
                        
                        /* แสดงเป็นรูปสามเหลี่ยม */
                        //e.Graphics.FillPolygon(brush, new Point[] { new Point(e.CellBounds.X + e.CellBounds.Width - 5, e.CellBounds.Y + 10), new Point(e.CellBounds.X + e.CellBounds.Width - 12, e.CellBounds.Y + 10), new Point(e.CellBounds.X + e.CellBounds.Width - 9, e.CellBounds.Y + 16) });
                    }
                    else
                    {
                        /* แสดงแบบ A->Z */
                        using (Font font = new Font("tahoma", 5f))
                        {
                            e.Graphics.DrawString("A", font, brush, new Point(e.CellBounds.X + e.CellBounds.Width - 16, e.CellBounds.Y + 6));
                            e.Graphics.DrawString("Z", font, brush, new Point(e.CellBounds.X + e.CellBounds.Width - 16, e.CellBounds.Y + 13));
                        }
                        using (Pen pen = new Pen(Color.Sienna))
                        {
                            e.Graphics.DrawLine(pen, new Point(e.CellBounds.X + e.CellBounds.Width - 7, e.CellBounds.Y + 9), new Point(e.CellBounds.X + e.CellBounds.Width - 7, e.CellBounds.Y + 19));
                        }
                        e.Graphics.FillPolygon(brush, new Point[] { new Point(e.CellBounds.X + e.CellBounds.Width - 9, e.CellBounds.Y + 17), new Point(e.CellBounds.X + e.CellBounds.Width - 4, e.CellBounds.Y + 17), new Point(e.CellBounds.X + e.CellBounds.Width - 7, e.CellBounds.Y + 19) });

                        /* แสดงเป็นรูปสามเหลี่ยม */
                        //e.Graphics.FillPolygon(brush, new Point[] { new Point(e.CellBounds.X + e.CellBounds.Width - 5, e.CellBounds.Y + 16), new Point(e.CellBounds.X + e.CellBounds.Width - 12, e.CellBounds.Y + 16), new Point(e.CellBounds.X + e.CellBounds.Width - 9, e.CellBounds.Y + 10) });
                    }
                }
                e.Handled = true;
            }
        }

        private void DgvPainted(object sender, PaintEventArgs e)
        {
            if (!this.row_border_redline)
                return;

            if (this.CurrentCell == null)
                return;

            //if (!this.Enabled)
            //    return;

            Rectangle rect = this.GetRowDisplayRectangle(this.CurrentCell.RowIndex, false);

            using (Pen p = new Pen(Color.Red))
            {
                e.Graphics.DrawLine(p, new Point(rect.X, rect.Y), new Point(rect.X + rect.Width, rect.Y));
                e.Graphics.DrawLine(p, new Point(rect.X, rect.Y + rect.Height - 2), new Point(rect.X + rect.Width, rect.Y + rect.Height - 2));
            }
        }

        /* 
         * Sort DataGridViewRow by column header clicked (Specify an object type that implement in bindingsource)
         */
        public void SortByColumn<T>(int column_index)
        {
            if (this.allow_sort_by_column_header_clicked && this.Columns[column_index].SortMode != DataGridViewColumnSortMode.NotSortable)
            {
                this.SetSortColumnInfo(column_index);

                string field_name = this.Columns[column_index].DataPropertyName;

                List<T> data = ((BindingSource)this.DataSource).DataSource as List<T>;
                if (this.SortASC)
                {
                    data = data.OrderBy(d => d.GetType().GetProperty(field_name).GetValue(d, null)).ToList();
                }
                else
                {
                    data = data.OrderByDescending(d => d.GetType().GetProperty(field_name).GetValue(d, null)).ToList();
                }
                ((BindingSource)this.DataSource).ResetBindings(true);
                ((BindingSource)this.DataSource).DataSource = data;
            }
        }

        public void SetSortColumnInfo(int sort_column)
        {
            if(this.SortColumn == sort_column)
            {
                this.sort_asc = !this.SortASC;
            }
            else
            {
                this.sort_column = sort_column;
                this.sort_asc = true;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.CurrentCell == null)
                return;

            if(e.Delta > 0) // scroll up
            {
                if(this.CurrentCell.RowIndex > 0)
                {
                    this.Rows[this.CurrentCell.RowIndex - 1].Cells[this.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).First().Name].Selected = true;
                }
            }
            if(e.Delta < 0) // scroll down
            {
                if (this.CurrentCell.RowIndex < this.Rows.Count - 1)
                {
                    this.Rows[this.CurrentCell.RowIndex + 1].Cells[this.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).First().Name].Selected = true;
                }
            }

            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
            //base.OnMouseWheel(e);
        }

        //protected override void OnScroll(ScrollEventArgs e)
        //{
        //    if (this.CurrentCell == null)
        //        return;
        //    //if(e.Type == ScrollEventType.)

        //    if(e.Type == ScrollEventType.)
        //    {
        //        if (this.CurrentCell.RowIndex != this.FirstDisplayedScrollingRowIndex)
        //        {
        //            this.Rows[this.FirstDisplayedScrollingRowIndex].Cells[this.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).First().Name].Selected = true;
        //        }
        //        return;
        //    }
        //}
    }
}
