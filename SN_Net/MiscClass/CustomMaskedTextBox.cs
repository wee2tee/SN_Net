using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    public partial class CustomMaskedTextBox : UserControl
    {
        private bool readOnly;
        private string text;
        private string maskString;
        private int selectedStringBegin;
        private int selectedStringLength;

        public CustomMaskedTextBox()
        {
            InitializeComponent();
            //this.textBox1.Mask = "";
            this.textBox1.PromptChar = ' ';
            this.Height = 23;
            this.textBox1.Text = "";
            this.label1.Text = "";

            this.Read_Only = true;
            this.ShowHideControl();
            this.BackColor = Color.White;
            this.textBox1.BackColor = Color.White;
        }

        public int SelectedStringBegin
        {
            get
            {
                return this.selectedStringBegin;
            }
            set
            {
                this.selectedStringBegin = value;
                this.textBox1.SelectionStart = value;
            }
        }

        public int SelectedStringEnd
        {
            get
            {
                return this.selectedStringLength;
            }
            set
            {
                this.selectedStringLength = value;
                this.textBox1.SelectionLength = value;
            }
        }

        public bool Read_Only
        {
            get
            {
                return this.readOnly;
            }
            set
            {
                this.readOnly = value;
                this.ShowHideControl();
            }
        }

        public string Texts
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.textBox1.Text = value;
                this.label1.Text = value;
            }
        }

        public string MaskString
        {
            get
            {
                return this.maskString;
            }
            set
            {
                this.maskString = value;
                this.textBox1.Mask = value;
            }
        }

        private void CustomMaskedTextBox_Load(object sender, EventArgs e)
        {
            this.BindingEventWithChildControl();
        }

        private void ShowHideControl()
        {
            if (this.readOnly)
            {
                this.label1.Visible = true;
                this.textBox1.Visible = false;
            }
            else
            {
                this.label1.Visible = false;
                this.textBox1.Visible = true;
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            if (!this.readOnly)
            {
                this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.textBox1.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.textBox1.ForeColor = Color.Black;
            }
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this.BackColor = Color.White;
            this.textBox1.BackColor = Color.White;
            this.textBox1.ForeColor = Color.Black;
            this.label1.ForeColor = Color.Black;
            base.OnLeave(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.textBox1.Size = new Size(this.ClientSize.Width - 5, this.textBox1.Height);
            this.label1.Size = new Size(this.ClientSize.Width - 2, this.label1.Height);
            base.OnSizeChanged(e);
        }

        private void BindingEventWithChildControl()
        {
            this.GotFocus += delegate
            {
                if (!this.Read_Only)
                {
                    this.textBox1.Focus();
                }
            };

            this.ForeColorChanged += delegate
            {
                this.textBox1.ForeColor = this.ForeColor;
                this.label1.ForeColor = this.ForeColor;
            };

            this.FontChanged += delegate
            {
                this.textBox1.Font = this.Font;
                this.label1.Font = this.Font;
            };

            this.textBox1.TextChanged += delegate
            {
                this.Texts = this.textBox1.Text;
            };
        }
    }
}
