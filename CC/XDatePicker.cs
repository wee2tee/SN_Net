using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CC.Dialog;
using System.Globalization;

namespace CC
{
    public partial class XDatePicker : UserControl
    {
        private DateTime? selected_date;
        public DateTime? _SelectedDate
        {
            get
            {
                return this.selected_date;
            }
            set
            {
                //this.selected_date = value;

                this.txtDate.Text = value.HasValue ? value.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH")) : "  /  /    ";
                this.label1.Text = value.HasValue ? value.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH")) : "  /  /    ";

                if (this.is_read_only)
                {
                    this.Refresh();
                }
            }
        }

        private bool focused;
        public bool _Focused
        {
            get
            {
                return this.focused;
            }
        }

        public bool _IsCalendarShown
        {
            get
            {
                return this.calendar != null ? true : false;
            }
        }

        private bool is_read_only;
        public bool _ReadOnly
        {
            get
            {
                return this.is_read_only;
            }
            set
            {
                string str_date = this.selected_date.HasValue ? this.selected_date.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH")) : "  /  /    ";
                this.txtDate.Text = str_date;
                this.label1.Text = str_date;
                this.is_read_only = value;
                this.btnShowCalendar.Enabled = !value;
                this.txtDate.Visible = !value;
                this.label1.Visible = value;

                this.Refresh();
            }
        }

        public CalendarDialog calendar;

        public event EventHandler _SelectedDateChanged;
        public event EventHandler _GotFocus;
        public event EventHandler _Leave;
        public event EventHandler _DoubleClicked;

        public XDatePicker()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.is_read_only)
            {
                this.BackColor = Color.White;
                this.txtDate.BackColor = Color.White;
            }

            base.OnPaint(e);
            if (this.is_read_only)
            {
                //string str_date = this.selected_date.HasValue ? this.selected_date.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture.DateTimeFormat) : "  /  /    ";

                //TextRenderer.DrawText(e.Graphics, str_date, this.txtDate.Font, new Point(0, 2), this.txtDate.ForeColor);
            }
        }

        private void btnShowCalendar_Click(object sender, EventArgs e)
        {
            var y = this.selected_date;

            if(this.calendar == null)
            {
                Point pnt = this.btnShowCalendar.PointToScreen(Point.Empty);
                this.calendar = new CalendarDialog(this);
                this.calendar.SetBounds(pnt.X, pnt.Y + this.btnShowCalendar.Height - 1, this.calendar.Width, this.calendar.Height);
                //this.calendar.FormClosed += delegate
                //{
                //    tmp_date = this.calendar.selected_date;
                //};
                this.calendar.Disposed += delegate
                {
                    //this.txtDate.Text = this.calendar.selected_date != null ? this.selected_date.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture) : "";
                    //this._SelectedDate = this.calendar.selected_date;
                    this.calendar = null;
                    this.txtDate.Focus();
                    var x = this.selected_date;
                };
                this.calendar.Show();
            }
        }

        public void SetDate(DateTime? date)
        {
            //this.selected_date = date;
            //this.txtDate.Text = this._SelectedDate.HasValue ? this._SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture.DateTimeFormat) : "  /  /    ";
            this._SelectedDate = date;
        }

        public void ShowCalendar()
        {
            this.btnShowCalendar.PerformClick();
        }

        private void XDatePicker_Load(object sender, EventArgs e)
        {
            this.txtDate.GotFocus += delegate(object sender_obj, EventArgs e_obj)
            {
                if(!this.is_read_only && this.txtDate.Focused)
                {
                    this.txtDate.BackColor = AppResource.EditableControlBackColor;
                    this.BackColor = AppResource.EditableControlBackColor;
                    this.txtDate.SelectionStart = 0;
                    this.focused = true;
                }
                else
                {
                    this.txtDate.BackColor = Color.White;
                    this.BackColor = Color.White;
                    this.focused = false;
                }

                if(this._GotFocus != null)
                {
                    this._GotFocus(this, e_obj);
                }
            };

            this.txtDate.Leave/*LostFocus*/ += delegate(object sender_obj, EventArgs e_obj)
            {
                this.SetDate(this._SelectedDate);
                this.txtDate.BackColor = Color.White;
                this.BackColor = Color.White;
                this.focused = false;

                if(this._Leave != null)
                {
                    this._Leave(this, e_obj);
                }
            };
        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {
            DateTime d;
            if (DateTime.TryParse(((MaskedTextBox)sender).Text, CultureInfo.GetCultureInfo("th-TH"), DateTimeStyles.None, out d))
            {
                this.selected_date = d;
                this.label1.Text = ((MaskedTextBox)sender).Text;
                if (this._SelectedDateChanged != null)
                    this._SelectedDateChanged(this, e);
            }
            else
            {
                this.selected_date = null;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F6)
            {
                this.btnShowCalendar.Focus();
                this.btnShowCalendar.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected void _SelectedDate_Changed(object sender, EventArgs e)
        {
            if (this._SelectedDateChanged != null)
                this._SelectedDateChanged(this, e);
        }

        private void txtDate_VisibleChanged(object sender, EventArgs e)
        {
            //this.label1.Visible = !(((MaskedTextBox)sender).Visible);
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (this._DoubleClicked != null)
                this._DoubleClicked(this, e);
        }

        private void XDatePicker_Enter(object sender, EventArgs e)
        {
            if (this._ReadOnly)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
