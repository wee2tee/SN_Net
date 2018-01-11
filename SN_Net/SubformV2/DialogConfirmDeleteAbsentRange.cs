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
    public partial class DialogConfirmDeleteAbsentRange : Form
    {
        public enum DELETE_METHOD
        {
            ONE,
            ALL,
            NONE
        }
        public DELETE_METHOD delete_method = DELETE_METHOD.NONE;

        public DialogConfirmDeleteAbsentRange()
        {
            InitializeComponent();
        }

        private void btnDeleteOne_Click(object sender, EventArgs e)
        {
            this.delete_method = DELETE_METHOD.ONE;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            this.delete_method = DELETE_METHOD.ALL;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.delete_method = DELETE_METHOD.NONE;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
