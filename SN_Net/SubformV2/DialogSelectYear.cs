using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.Subform
{
    public partial class DialogSelectYear : Form
    {
        public int selected_year;

        public DialogSelectYear()
        {
            InitializeComponent();
        }

        private void DialogSelectYear_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year - 10; i < DateTime.Now.Year + 5; i++)
            {
                this.cbYear.Items.Add(i.ToString());
            }
            this.cbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selected_year = Convert.ToInt32(((ComboBox)sender).Text);
        }
    }
}
