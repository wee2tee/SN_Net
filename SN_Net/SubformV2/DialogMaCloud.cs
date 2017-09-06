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
    public partial class DialogMaCloud : Form
    {
        public DateTime? date_from;
        public DateTime? date_to;
        public string email;

        public DialogMaCloud(DateTime? date_from = null, DateTime? date_to = null, string email = "")
        {
            InitializeComponent();
            this.date_from = date_from;
            this.date_to = date_to;
            this.email = email;
        }

        private void DialogMA_Load(object sender, EventArgs e)
        {
            this.dtFrom._SelectedDate = this.date_from;
            this.dtTo._SelectedDate = this.date_to;
            this.txtEmail._Text = this.email;
        }

        private void xDatePicker1__SelectedDateChanged(object sender, EventArgs e)
        {
            this.date_from = ((XDatePicker)sender)._SelectedDate;
        }

        private void xDatePicker2__SelectedDateChanged(object sender, EventArgs e)
        {
            this.date_to = ((XDatePicker)sender)._SelectedDate;
        }

        private void xTextEdit1__TextChanged(object sender, EventArgs e)
        {
            this.email = ((XTextEdit)sender)._Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!this.date_from.HasValue)
            {
                MessageAlert.Show("กรุณาระบุวันที่เริ่ม", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.dtFrom.Focus();
                return;
            }

            if (!this.date_to.HasValue)
            {
                MessageAlert.Show("กรุณาระบุวันที่สิ้นสุด", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.dtTo.Focus();
                return;
            }

            if(this.txtEmail._Text.Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุอีเมล์", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.txtEmail.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if (this.btnOK.Focused || this.btnCancel.Focused)
                    return false;

                SendKeys.Send("{TAB}");
                return true;
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
