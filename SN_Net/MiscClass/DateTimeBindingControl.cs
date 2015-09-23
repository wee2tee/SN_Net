using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    public static class DateTimeBindingControl
    {
        public static void pickedDate(this MaskedTextBox maskTextBox, string str_date)
        {
            CultureInfo cinfo_us = new CultureInfo("en-US");
            CultureInfo cinfo_th = new CultureInfo("th-TH");

            if (str_date != null)
            {
                DateTime dt = Convert.ToDateTime(str_date, cinfo_us);
                maskTextBox.Text = dt.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
            }
            else
            {
                maskTextBox.Text = "  /  /    ";
            }
        }

        public static void pickedDate(this TextBox textBox, string str_date)
        {
            CultureInfo cinfo_us = new CultureInfo("en-US");
            CultureInfo cinfo_th = new CultureInfo("th-TH");

            if (str_date != null)
            {
                DateTime dt = Convert.ToDateTime(str_date, cinfo_us);
                textBox.Text = dt.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
            }
            else
            {
                textBox.Text = "  /  /    ";
            }
        }

        public static void pickedDate(this Label label, string str_date)
        {
            CultureInfo cinfo_us = new CultureInfo("en-US");
            CultureInfo cinfo_th = new CultureInfo("th-TH");

            if (str_date != null)
            {
                DateTime dt = Convert.ToDateTime(str_date, cinfo_us);
                label.Text = dt.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
            }
            else
            {
                label.Text = "  /  /    ";
            }
        }

        public static void pickedDate(this CustomMaskedTextBox textbox, string str_date)
        {
            CultureInfo cinfo_us = new CultureInfo("en-US");
            CultureInfo cinfo_th = new CultureInfo("th-TH");

            if (str_date != null)
            {
                DateTime dt = Convert.ToDateTime(str_date, cinfo_us);
                textbox.Texts = dt.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
            }
            else
            {
                textbox.Texts = "  /  /    ";
            }
        }

        public static void pickedDate(this DataGridViewCell cell, string str_date)
        {
            CultureInfo cinfo_us = new CultureInfo("en-US");
            CultureInfo cinfo_th = new CultureInfo("th-TH");

            cell.ValueType = typeof(string);

            if (str_date != null)
            {
                DateTime dt = Convert.ToDateTime(str_date, cinfo_us);
                cell.Value = dt.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
            }
            else
            {
                cell.Value = "  /  /    ";
            }
        }

        public static void pickedDate(this DateTimePicker dateTimePicker, string str_date)
        {
            CultureInfo cinfo_us = new CultureInfo("en-US");
            CultureInfo cinfo_th = new CultureInfo("th-TH");

            if (str_date != null)
            {
                DateTime dt = Convert.ToDateTime(str_date, cinfo_us);
                dateTimePicker.Value = dt;
            }
            else
            {
                dateTimePicker.Value = DateTime.Now;
            }
        }

        public static string toMySQLDate(this string str_date)
        {
            CultureInfo cinfo_us = new CultureInfo("en-US");
            CultureInfo cinfo_th = new CultureInfo("th-TH");

            DateTime out_datetime;
            if (DateTime.TryParse(str_date, out out_datetime))
            {
                return out_datetime.ToString("yyyy-MM-dd", cinfo_us.DateTimeFormat);
            }
            else
            {
                return null;
            }
        }
    }
}
