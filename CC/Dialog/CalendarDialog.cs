using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC.Dialog
{
    public partial class CalendarDialog : Form
    {
        public DateTime? selected_date = null;
        private XDatePicker xdate_picker;
        private DateTime? lastclick_time = null;
        private DateTime? lastselect_datetime = null;
        
        public CalendarDialog()
        {
            InitializeComponent();
        }

        public CalendarDialog(XDatePicker xdate_picker)
            : this()
        {
            this.xdate_picker = xdate_picker;
            this.calendar.SelectionStart = xdate_picker._SelectedDate != null ? xdate_picker._SelectedDate.Value : DateTime.Now;
        }

        private void CalendarDialog_Load(object sender, EventArgs e)
        {
            
        }

        private void CalendarDialog_Shown(object sender, EventArgs e)
        {
            this.Width = this.calendar.Width + 5;
        }

        private void calendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.selected_date = e.Start;
        }

        private void CalendarDialog_Deactivate(object sender, EventArgs e)
        {
            //this.selected_date = null;
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
                return true;
            }

            if(keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.xdate_picker.SetDate(this.selected_date);
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.selected_date = null;
            this.Dispose();
        }

        private void calendar_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.lastclick_time.HasValue && this.lastselect_datetime.HasValue)
            {
                if (((DateTime.Now - this.lastclick_time.Value).TotalMilliseconds < SystemInformation.DoubleClickTime) && this.lastselect_datetime.Value == this.calendar.SelectionStart)
                {
                    this.lastselect_datetime = null;
                    this.lastclick_time = null;
                    this.btnOK.PerformClick();
                    return;
                }
                else
                {
                    this.lastselect_datetime = this.calendar.SelectionStart;
                    this.lastclick_time = DateTime.Now;
                    return;
                }
            }
            else
            {
                this.lastselect_datetime = this.calendar.SelectionStart;
                this.lastclick_time = DateTime.Now;
                return;
            }
        }
    }
}
