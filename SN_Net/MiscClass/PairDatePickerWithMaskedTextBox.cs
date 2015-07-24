using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace SN_Net.MiscClass
{
    public class PairDatePickerWithMaskedTextBox
    {

        public List<MaskedTextBox> list_tb = new List<MaskedTextBox>();
        public List<DateTimePicker> list_dt = new List<DateTimePicker>();

        private PairDatePickerWithMaskedTextBox(List<MaskedTextBox> list_tb, List<DateTimePicker> list_dt)
        {
            this.list_tb = list_tb;
            this.list_dt = list_dt;
            this.addAction(this.list_tb);
            this.addAction(this.list_dt);
        }

        public static void Attach(List<MaskedTextBox> list_maskedTextBox, List<DateTimePicker> list_dateTimePicker)
        {
            PairDatePickerWithMaskedTextBox p = new PairDatePickerWithMaskedTextBox(list_maskedTextBox, list_dateTimePicker);
        }

        private void addAction(List<MaskedTextBox> list_maskedTextBox)
        {
            foreach (MaskedTextBox tb in list_maskedTextBox)
            {
                tb.InsertKeyMode = InsertKeyMode.Overwrite;
                tb.PromptChar = ' ';

                tb.Leave += new EventHandler(this.maskedTextBoxLeaveHandler);
                tb.EnabledChanged += new EventHandler(this.EnableChangeHandler);
                tb.ReadOnlyChanged += new EventHandler(this.ReadOnlyChangeHandler);
                tb.GotFocus += new EventHandler(this.GotFocusHandler);
                tb.Click += new EventHandler(this.ClickToFocusHandler);
            }
        }

        private void addAction(List<DateTimePicker> list_dateTimePicker)
        {
            foreach (DateTimePicker dp in list_dateTimePicker)
            {
                dp.TabStop = false;
                dp.TabIndex = 999;
                dp.Value = DateTime.Now;

                dp.ValueChanged += new EventHandler(this.dateTimePickerValueChangeHandler);
                dp.Enter += new EventHandler(this.dateTimePickerBeforeShowCalendarHandler);
            }
        }

        private void maskedTextBoxLeaveHandler(object sender, EventArgs e)
        {
            int ndx = this.list_tb.FindIndex(t => t.Equals((MaskedTextBox)sender));

            string str_date = ((MaskedTextBox)sender).Text;
            DateTime out_datevalue;
            if (DateTime.TryParse(str_date, out out_datevalue))
            {
                this.list_dt[ndx].Value = out_datevalue;
            }
            else
            {
                this.list_dt[ndx].Value = DateTime.Now;
                ((MaskedTextBox)sender).Text = "";
            }
        }

        private void maskedTextBoxPressedF6(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                int ndx = this.list_tb.FindIndex(t => t.Equals((MaskedTextBox)sender));
                this.list_dt[ndx].Select();
                SendKeys.Send("{F4}");
            }
        }

        private void dateTimePickerValueChangeHandler(object sender, EventArgs e)
        {
            CultureInfo cinfo_th = new CultureInfo("th-TH");
            int ndx = this.list_dt.FindIndex(t => t.Equals((DateTimePicker)sender));

            this.list_tb[ndx].Text = ((DateTimePicker)sender).Value.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
        }

        private void dateTimePickerBeforeShowCalendarHandler(object sender, EventArgs e)
        {
            CultureInfo cinfo_th = new CultureInfo("th-TH");
            int ndx = this.list_dt.FindIndex(t => t.Equals((DateTimePicker)sender));

            DateTime out_datevalue;
            if (DateTime.TryParse(this.list_tb[ndx].Text, out out_datevalue))
            {
                ((DateTimePicker)sender).Value = out_datevalue;
            }
        }

        private void EnableChangeHandler(object sender, EventArgs e)
        {
            int ndx = this.list_tb.FindIndex(t => t.Equals((MaskedTextBox)sender));

            if (((MaskedTextBox)sender).Enabled)
            {
                ((MaskedTextBox)sender).BackColor = Color.White;
                this.list_dt[ndx].Enabled = true;
            }
            else
            {
                this.list_dt[ndx].Enabled = false;
            }
        }

        private void ReadOnlyChangeHandler(object sender, EventArgs e)
        {
            int ndx = this.list_tb.FindIndex(t => t.Equals((MaskedTextBox)sender));

            if (((MaskedTextBox)sender).ReadOnly)
            {
                ((MaskedTextBox)sender).BackColor = Color.White;
                this.list_dt[ndx].Enabled = false;
            }
            else
            {
                this.list_dt[ndx].Enabled = true;
            }
        }

        private void GotFocusHandler(object sender, EventArgs e)
        {
            //((MaskedTextBox)sender).SelectionStart = 0;
            ((MaskedTextBox)sender).SelectAll();
        }

        private void ClickToFocusHandler(object sender, EventArgs e)
        {
            //((MaskedTextBox)sender).SelectionStart = 0;
            ((MaskedTextBox)sender).SelectAll();
        }
    }
}
