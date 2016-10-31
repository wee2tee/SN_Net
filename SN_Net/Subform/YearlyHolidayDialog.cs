using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace SN_Net.Subform
{
    public partial class YearlyHolidayDialog : Form
    {
        private int current_year;

        public YearlyHolidayDialog(int year)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            InitializeComponent();

            this.current_year = year;
        }
    }
}
