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
    public partial class DialogSearchCondition : Form
    {
        public string condition_string = string.Empty;

        public DialogSearchCondition(string condition_string = "")
        {
            this.condition_string = condition_string;
            InitializeComponent();
        }

        private void DialogSearchCondition_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtCondition;
            this.txtCondition.Text = this.condition_string;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.condition_string = ((TextBox)sender).Text;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if (this.txtCondition.Focused)
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
