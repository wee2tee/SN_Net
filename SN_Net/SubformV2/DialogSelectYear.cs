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

        public DialogSelectYear(int? selected_year = null)
        {
            InitializeComponent();
            this.selected_year = selected_year.HasValue ? selected_year.Value : DateTime.Now.Year;
        }

        private void DialogSelectYear_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year - 10; i < DateTime.Now.Year + 5; i++)
            {
                this.cbYear.Items.Add(i.ToString());
            }
            this.cbYear.Text = this.selected_year.ToString();
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selected_year = Convert.ToInt32(((ComboBox)sender).Text);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.cbYear.DroppedDown)
                {
                    return false;
                }

                this.btnOK.PerformClick();
                return true;
            }

            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            if (keyData == Keys.F6)
            {
                this.cbYear.DroppedDown = true;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
