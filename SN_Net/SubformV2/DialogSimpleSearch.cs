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
    public partial class DialogSimpleSearch : Form
    {
        public string keyword = string.Empty;
        private bool is_search_sn = false;

        public DialogSimpleSearch(bool is_search_sn = false,string title_text = "", string label_text = "", string initial_keyword = "")
        {
            InitializeComponent();
            this.Text = title_text != null ? title_text : this.Text;
            this.lblKey.Text = label_text != null ? label_text : this.lblKey.Text;
            this.is_search_sn = is_search_sn;
            if (this.is_search_sn)
            {
                this.mskKeyword.Visible = true;
                this.txtKeyword.Visible = false;
                this.mskKeyword.Text = initial_keyword;
            }
            else
            {
                this.mskKeyword.Visible = false;
                this.txtKeyword.Visible = true;
                this.txtKeyword.Text = initial_keyword;
            }
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            if (!this.is_search_sn)
                this.keyword = ((TextBox)sender).Text;
        }

        private void mskKeyword_TextChanged(object sender, EventArgs e)
        {
            if (this.is_search_sn)
                this.keyword = ((MaskedTextBox)sender).Text;
        }

        private void DialogSimpleSearch_Load(object sender, EventArgs e)
        {
            if (this.is_search_sn)
            {
                this.ActiveControl = this.mskKeyword;
                this.mskKeyword.SelectionStart = 0;
                this.mskKeyword.SelectionLength = this.mskKeyword.Text.Length;
            }
            else
            {
                this.ActiveControl = this.txtKeyword;
                this.txtKeyword.SelectionStart = 0;
                this.txtKeyword.SelectionLength = this.txtKeyword.Text.Length;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if ((this.is_search_sn && this.mskKeyword.Focused) || (!this.is_search_sn && this.txtKeyword.Focused))
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
