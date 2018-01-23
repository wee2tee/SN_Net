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
    public partial class DialogPrintOutput : Form
    {
        public PRINT_OUTPUT output = PRINT_OUTPUT.SCREEN;

        public DialogPrintOutput()
        {
            InitializeComponent();
        }

        private void rdScreen_CheckedChanged(object sender, EventArgs e)
        {
            if(((RadioButton)sender).Checked)
                this.output = PRINT_OUTPUT.SCREEN;
        }

        private void rdPrinter_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                this.output = PRINT_OUTPUT.PRINTER;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
