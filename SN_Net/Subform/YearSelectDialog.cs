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
    public partial class YearSelectDialog : Form
    {
        public int selected_year;

        public YearSelectDialog(int default_year)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            InitializeComponent();

            this.selected_year = default_year;

            Console.WriteLine(" .. passing year is " + default_year);
        }

        private void YearSelectDialog_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year + 20; i > DateTime.Now.Year - 50; i--)
            {
                this.cbYear.Items.Add(i + 543);
            }
            this.cbYear.Text = (this.selected_year + 543).ToString();
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selected_year = Convert.ToInt32(this.cbYear.Text) - 543;
        }
    }
}
