using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class PrintOutputSelection : Form
    {
        public PageSetupDialog page_setup;
        public enum OUTPUT
        {
            PRINTER,
            SCREEN,
            FILE
        }
        public OUTPUT output;

        public PrintOutputSelection()
        {
            InitializeComponent();
            this.rbScreen.Checked = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rbPrinter.Checked)
            {
                this.output = OUTPUT.PRINTER;
            }
            else if (this.rbScreen.Checked)
            {
                this.output = OUTPUT.SCREEN;
            }
            else if (this.rbFile.Checked)
            {
                this.output = OUTPUT.FILE;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
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
