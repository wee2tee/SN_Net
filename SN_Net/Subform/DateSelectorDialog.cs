using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;

namespace SN_Net.Subform
{
    public partial class DateSelectorDialog : Form
    {
        public DateTime selected_date;
        private DateTime init_date;

        public DateSelectorDialog()
        {
            InitializeComponent();
            this.init_date = DateTime.Now;
        }

        public DateSelectorDialog(DateTime init_date)
            : this()
        {
            this.init_date = init_date;
        }

        private void DateSelectorDialog_Load(object sender, EventArgs e)
        {
            this.dtDatePicker.Value = this.init_date;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selected_date = this.dtDatePicker.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.btnOK.Focused || this.btnCancel.Focused)
                    return false;

                SendKeys.Send("{TAB}");
                return true;
            }

            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
