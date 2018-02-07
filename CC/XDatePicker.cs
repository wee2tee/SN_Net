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
                
                this.selected_date = value;
                this.SetText(value);
                if(this._SelectedDateChanged != null)
                {
                    this._SelectedDateChanged(this, new EventArgs());
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
            if(this.calendar == null)
            {
                Point pnt = this.btnShowCalendar.PointToScreen(Point.Empty);
                this.calendar = new CalendarDialog(this);
                this.calendar.SetBounds(pnt.X, pnt.Y + this.btnShowCalendar.Height - 1, this.calendar.Width, this.calendar.Height);
                this.calendar.Disposed += delegate
                {
                    this.calendar = null;
                    this.txtDate.Focus();
                };
                this.calendar.Show();
            }
        }

        private void SetText(DateTime? date)
        {
            if (!date.HasValue)
            {
                this.txtDate.Text = "  /  /    ";
                return;
            }

            DateTime d;
            if (DateTime.TryParse(this.txtDate.Text, CultureInfo.GetCultureInfo("th-TH"), DateTimeStyles.None, out d))
            {
                if(d.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("th-TH")) == date.Value.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("th-TH")))
                {
                    if(this.txtDate.Text.Trim().Length < 10) // if txtDate is show only 2 digits year change it to 4 digits
                    {
                        this.txtDate.Text = date.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH"));
                    }
                    return;
                }
                else
                {
                    this.txtDate.Text = date.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH"));
                }
            }
            else
            {
                this.txtDate.Text = date.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH"));
            }

            
        }

        public void SetDate(DateTime? date)
        {
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
        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {
            this.label1.Text = ((MaskedTextBox)sender).Text;
            DateTime d;
            if (DateTime.TryParse(((MaskedTextBox)sender).Text, CultureInfo.GetCultureInfo("th-TH"), DateTimeStyles.None, out d))
            {
                this.selected_date = d;
            }
            else
            {
                this.selected_date = null;
            }

            if (this._SelectedDateChanged != null)
            {
                this._SelectedDateChanged(this, e);
            }
        }

        private void txtDate_Leave(object sender, EventArgs e)
        {
            DateTime d;
            if (DateTime.TryParse(((MaskedTextBox)sender).Text, CultureInfo.GetCultureInfo("th-TH"), DateTimeStyles.None, out d))
            {
                this._SelectedDate = d;
            }
            else
            {
                this._SelectedDate = null;
            }

            this.txtDate.BackColor = Color.White;
            this.BackColor = Color.White;
            this.focused = false;

            if (this._Leave != null)
            {
                this._Leave(this, e);
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
                this.Parent.SelectNextControl(this, true, true, true, false);
            }
        }

        public void SelectAllText()
        {
            this.txtDate.SelectionStart = 0;
            this.txtDate.SelectionLength = this.txtDate.Text.Length;
        }
    }
}
