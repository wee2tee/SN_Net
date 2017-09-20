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
    public partial class XTextEditWithMaskedLabel : UserControl
    {
        public string _TextPrefix
        {
            get
            {
                return this.lblTextPrefix.Text;
            }
        }

        public string _TextEditable
        {
            get
            {
                return this.txtTextEditable.Text;
            }
        }

        public string _TextAll
        {
            get
            {
                return this.lblTextAll.Text;
            }
        }

        public bool _ReadOnly
        {
            get
            {
                return !this.txtTextEditable.Visible;
            }
            set
            {
                this.txtTextEditable.Visible = !value;
            }
        }

        private int _max_length = 36500;
        public int _MaxLength
        {
            get
            {
                return this._max_length;
            }
            set
            {
                this._max_length = value;
                this.txtTextEditable.MaxLength = value - this.lblTextPrefix.Text.Length;
            }
        }

        public event EventHandler _TextChanged;
        public event EventHandler _DoubleClicked;

        public XTextEditWithMaskedLabel()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.txtTextEditable.Text = string.Empty;
            this.lblTextPrefix.Text = string.Empty;
            this.lblTextAll.Text = string.Empty;
            //this._ReadOnly = true;
        }

        private void XTextEditWithMaskedLabel_Load(object sender, EventArgs e)
        {

        }

        private void XTextEditWithMaskedLabel_Enter(object sender, EventArgs e)
        {
            if (this._ReadOnly)
            {
                this.txtTextEditable.BackColor = Color.White;
            }
            else
            {
                this.txtTextEditable.BackColor = AppResource.EditableControlBackColor;
                this.txtTextEditable.Focus();
            }
        }

        private void lblTextPrefix_TextChanged(object sender, EventArgs e)
        {
            this.txtTextEditable.MaxLength = this._MaxLength - ((Label)sender).Text.Length;
            this.lblTextAll.Text = ((Label)sender).Text + this.txtTextEditable.Text;
            
            if(this._TextChanged != null)
            {
                this._TextChanged(this, e);
            }
        }

        private void txtTextEditable_TextChanged(object sender, EventArgs e)
        {
            this.lblTextAll.Text = this.lblTextPrefix.Text + ((TextBox)sender).Text;
            
            if(this._TextChanged != null)
            {
                this._TextChanged(this, e);
            }
        }

        private void txtTextEditable_VisibleChanged(object sender, EventArgs e)
        {
            if (!((TextBox)sender).Visible) // _ReadOnly = true
            {
                this.lblTextAll.Visible = true;
                this.lblTextPrefix.Visible = false;

                this.BackColor = Color.White;
            }
            else 
            {
                this.lblTextAll.Visible = false;
                this.lblTextPrefix.Visible = true;

                if (this.txtTextEditable.Focused)
                {
                    this.txtTextEditable.BackColor = AppResource.EditableControlBackColor;
                }
                else
                {
                    this.txtTextEditable.BackColor = Color.White;
                }
            }
        }

        public void SetText(string all_text = "", string prefix_text = "")
        {
            this.lblTextPrefix.Text = prefix_text;

            if(all_text.Length > prefix_text.Length)
            {
                this.txtTextEditable.Text = all_text.Substring(prefix_text.Length, all_text.Length - prefix_text.Length);
            }
            else
            {
                this.txtTextEditable.Text = string.Empty;
            }

            this.SetControlSizing();
        }

        private void SetControlSizing()
        {
            Size prefix_size = TextRenderer.MeasureText(this._TextPrefix, this.lblTextPrefix.Font);
            this.lblTextPrefix.Width = prefix_size.Width;
            this.txtTextEditable.Location = new Point(this.lblTextPrefix.Location.X + prefix_size.Width, this.txtTextEditable.Location.Y);
            this.txtTextEditable.Size = new Size(this.Width - this.txtTextEditable.Location.X - 3, this.txtTextEditable.Height);
        }

        private void txtTextEditable_BackColorChanged(object sender, EventArgs e)
        {
            this.BackColor = ((TextBox)sender).BackColor;
        }

        private void XTextEditWithMaskedLabel_Leave(object sender, EventArgs e)
        {
            this.txtTextEditable.BackColor = Color.White;
        }

        private void lblTextPrefix_Click(object sender, EventArgs e)
        {
            this.txtTextEditable.Focus();
        }

        private void lblTextAll_DoubleClick(object sender, EventArgs e)
        {
            if(this._DoubleClicked != null)
            {
                this._DoubleClicked(this, e);
            }
        }
    }
}
