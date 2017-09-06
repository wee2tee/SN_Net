using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CC;

namespace SN_Net.Subform
{
    public partial class DialogPasswordSn : Form
    {
        public string password = string.Empty;

        public DialogPasswordSn()
        {
            InitializeComponent();
        }

        private void DialogPasswordSn_Load(object sender, EventArgs e)
        {

        }

        private void txtPassword__TextChanged(object sender, EventArgs e)
        {
            this.password = ((XTextEdit)sender)._Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(this.password.Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุรหัสผ่าน", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.txtPassword.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if (this.txtPassword.Focused)
                {
                    this.btnOK.PerformClick();
                    return true;
                }
            }

            if(keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
