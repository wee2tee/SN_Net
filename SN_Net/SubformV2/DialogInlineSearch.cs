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
    public partial class DialogInlineSearch : Form
    {
        public string key_word = string.Empty;

        public DialogInlineSearch(string key_word = "")
        {
            InitializeComponent();
            this.txtKeyword.Text = key_word;
        }

        private void DialogInlineSearch_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtKeyword;
            this.txtKeyword.SelectionStart = this.txtKeyword.Text.Length;
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            this.key_word = ((TextBox)sender).Text;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return true;
            }

            if(keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
