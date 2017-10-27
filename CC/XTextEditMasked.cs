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
    public partial class XTextEditMasked : UserControl
    {
        public bool _ReadOnly {
            get {
                return this.maskedTextBox1.ReadOnly;
            }
            set {
                this.maskedTextBox1.ReadOnly = value;
            }
        }

        public string _Mask
        {
            get
            {
                return this.maskedTextBox1.Mask;
            }
            set
            {
                this.maskedTextBox1.Mask = value;
            }
        }

        public char _PromptChar
        {
            get
            {
                return this.maskedTextBox1.PromptChar;
            }
            set
            {
                this.maskedTextBox1.PromptChar = value;
            }
        }

        public HorizontalAlignment _TextAlign
        {
            get
            {
                return this.maskedTextBox1.TextAlign;
            }
            set
            {
                this.maskedTextBox1.TextAlign = value;
            }
        }

        public string _Text
        {
            get
            {
                return this.maskedTextBox1.Text;
            }
            set
            {
                this.maskedTextBox1.Text = value;
            }
        }

        public int _SelectionStart
        {
            get
            {
                return this.maskedTextBox1.SelectionStart;
            }
            set
            {
                this.maskedTextBox1.SelectionStart = value;
            }
        }

        public int _SelectionLength
        {
            get
            {
                return this.maskedTextBox1.SelectionLength;
            }
            set
            {
                this.maskedTextBox1.SelectionLength = value;
            }
        }

        public event EventHandler _TextChanged;
        public event EventHandler _GotFocus;
        public event EventHandler _Leave;
        public event EventHandler _DoubleClicked;

        public XTextEditMasked()
        {
            InitializeComponent();
            this.maskedTextBox1.GotFocus += MaskedTextBox1_GotFocus;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this._ReadOnly)
            {
                this.label1.Visible = true;
                this.maskedTextBox1.Visible = false;
            }
            else
            {
                this.label1.Visible = false;
                this.maskedTextBox1.Visible = true;
            }
        }

        private void maskedTextBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void MaskedTextBox1_GotFocus(object sender, EventArgs e)
        {
            if (this._ReadOnly)
            {
                ((MaskedTextBox)sender).BackColor = Color.White;
                this.BackColor = Color.White;
            }
            else
            {
                ((MaskedTextBox)sender).BackColor = AppResource.EditableControlBackColor;
                this.BackColor = AppResource.EditableControlBackColor;
                this.maskedTextBox1.SelectionStart = 0;
            }
            if (this._GotFocus != null)
            {
                this._GotFocus(sender, e);
            }
        }

        private void maskedTextBox1_ReadOnlyChanged(object sender, EventArgs e)
        {
            this.TabStop = (this._ReadOnly ? false : true);
            this.maskedTextBox1.TabStop = (this._ReadOnly ? false : true);

            if (this._ReadOnly)
            {
                ((MaskedTextBox)sender).Visible = false;
                this.label1.Visible = true;
                this.BackColor = Color.White;
            }
            else
            {
                ((MaskedTextBox)sender).Visible = true;
                this.label1.Visible = false;
                this.BackColor = this.maskedTextBox1.Focused ? AppResource.EditableControlBackColor : Color.White;
            }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.label1.Text = ((MaskedTextBox)sender).Text;
            if(this._TextChanged != null)
            {
                this._TextChanged(this, e);
            }
        }

        private void XTextEditMasked_Enter(object sender, EventArgs e)
        {
            if (this._ReadOnly)
            {
                this.BackColor = Color.White;
                this.Parent.SelectNextControl(this, true, true, true, true);
            }
            else
            {
                this.BackColor = AppResource.EditableControlBackColor;
                this.maskedTextBox1.Focus();
            }
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            ((MaskedTextBox)sender).BackColor = Color.White;

            if(this._Leave != null)
            {
                this._Leave(this, e);
            }
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if(this._DoubleClicked != null)
            {
                this._DoubleClicked(this, e);
            }
        }

        private void maskedTextBox1_TextAlignChanged(object sender, EventArgs e)
        {
            if(((MaskedTextBox)sender).TextAlign == HorizontalAlignment.Left)
            {
                this.label1.Location = new Point(1, 2);
                this.label1.TextAlign = ContentAlignment.TopLeft;
                return;
            }

            if(((MaskedTextBox)sender).TextAlign == HorizontalAlignment.Right)
            {
                //int x = (this.label1.Width - this.maskedTextBox1.Width - 3) * -1;
                var text_box_x = this.maskedTextBox1.Location.X + this.maskedTextBox1.Width;
                var label_x = this.label1.Location.X + this.label1.Width;
                var x = text_box_x - label_x + 6;

                this.label1.Location = new Point(x, 2);
                this.label1.TextAlign = ContentAlignment.TopRight;
            }

            if(((MaskedTextBox)sender).TextAlign == HorizontalAlignment.Center)
            {
                //int x = (Convert.ToInt32((this.label1.Width - this.maskedTextBox1.Width) / 2) - 8) * -1;
                var text_box_x = this.maskedTextBox1.Location.X + this.maskedTextBox1.Width;
                var label_x = this.label1.Location.X + this.label1.Width;
                var x = Convert.ToInt32(Math.Floor((decimal)(text_box_x - label_x) / 2)) + 3;

                this.label1.Location = new Point(x, 2);
                this.label1.TextAlign = ContentAlignment.TopCenter;
            }
        }
    }
}
