using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SN_Net.Subform
{
    public partial class ApiMainUrlFirstSetting : Form
    {
        public ApiMainUrlFirstSetting()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.mskMainURL.Text != "http://")
            {
                using (StreamWriter file = new StreamWriter("SN_pref.txt", false))
                {
                    file.WriteLine("MAIN URL | " + this.mskMainURL.Text);
                    this.Close();
                }
            }
            else
            {
                MessageAlert.Show("กรุณาป้อนข้อมูลให้ถูกต้อง", "Warning", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ApiMainUrlFirstSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists("SN_pref.txt"))
            {
                this.DialogResult = DialogResult.OK;
                e.Cancel = false;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                e.Cancel = false;
            }
        }

        private void ApiMainUrlFirstSetting_Shown(object sender, EventArgs e)
        {
            this.mskMainURL.Focus();
            this.mskMainURL.SelectionStart = this.mskMainURL.Text.Length;
        }

        private void mskMainURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                ((MaskedTextBox)sender).SelectionStart = ((MaskedTextBox)sender).Text.Length;
                e.Handled = true;
            }
        }
    }
}
