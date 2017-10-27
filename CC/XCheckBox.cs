using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC
{
    public partial class XCheckBox : UserControl
    {

        public bool _Checked
        {
            get
            {
                return this.checkBox1.Checked;
            }
            set
            {
                this.checkBox1.Checked = value;
            }
        }

        private bool read_only = false;
        public bool _ReadOnly
        {
            get
            {
                //return this.read_only;
                return !this.checkBox1.Enabled;
            }
            set
            {
                //this.read_only = value;
                this.checkBox1.Enabled = !value;
                this.checkBox1.TabStop = !value;
                this.TabStop = !value;
            }
        }

        public event EventHandler _CheckedChanged;

        public XCheckBox()
        {
            InitializeComponent();
        }

        private void XCheckBox_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.checkBox1.Focus();
            if (!this._ReadOnly)
                this.checkBox1.Checked = !this.checkBox1.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.label1.Text = "\u2713";
            }
            else
            {
                this.label1.Text = string.Empty;
            }

            if (this._CheckedChanged != null)
            {
                this._CheckedChanged(this, e);
            }
        }

        private void XCheckBox_Enter(object sender, EventArgs e)
        {
            
        }

        private void XCheckBox_Leave(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_Enter(object sender, EventArgs e)
        {
            if (this._ReadOnly)
            {
                this.BackColor = Color.White;
                this.label1.BackColor = Color.White;
                this.Parent.SelectNextControl(this, true, true, true, true);
            }
            else
            {
                this.BackColor = AppResource.EditableControlBackColor;
                this.label1.BackColor = AppResource.EditableControlBackColor;
            }
        }

        private void checkBox1_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.label1.BackColor = Color.White;
        }
    }
}
