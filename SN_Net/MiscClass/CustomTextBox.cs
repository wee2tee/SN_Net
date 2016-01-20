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
    public partial class CustomTextBox : UserControl
    {
        private bool read_Only;
        private string text;
        private int maxchar;
        private bool characterUpperCase;

        public CustomTextBox()
        {
            InitializeComponent();
            this.textBox1.Text = "";
            this.label1.Text = "";
            
            this.Read_Only = true;
            this.ShowHideControl();
            this.BackColor = Color.White;
            this.textBox1.BackColor = Color.White;
        }

        public int SelectionStart
        {
            get
            {
                return this.textBox1.SelectionStart;
            }
            set
            {
                this.textBox1.SelectionStart = value;
            }
        }

        public int SelectionLength
        {
            get
            {
                return this.textBox1.SelectionLength;
            }
            set
            {
                this.textBox1.SelectionLength = value;
            }
        }

        public bool Read_Only
        {
            get
            {
                return this.read_Only;
            }
            set
            {
                this.read_Only = value;
                this.ShowHideControl();
            }
        }

        public string Texts
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.text = value;
                this.textBox1.Text = value;
                this.label1.Text = value;
            }
        }

        public int MaxChar
        {
            get
            {
                return this.maxchar;
            }
            set
            {
                this.maxchar = value;
                this.textBox1.MaxLength = this.maxchar;
            }
        }

        public bool CharUpperCase
        {
            get
            {
                return this.characterUpperCase;
            }
            set
            {
                this.characterUpperCase = value;
                this.CharCasingControl();
            }
        }

        private void CustomTextBox_Load(object sender, EventArgs e)
        {
            this.BindingEventWithChildControl();

            this.textBox1.Size = new Size(this.ClientSize.Width - 5, this.textBox1.Height);
            this.label1.Size = new Size(this.ClientSize.Width - 2, this.label1.Height);
        }

        private void ShowHideControl()
        {
            if (this.read_Only)
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

        private void CharCasingControl()
        {
            if (this.characterUpperCase)
            {
                this.textBox1.CharacterCasing = CharacterCasing.Upper;
            }
            else
            {
                this.textBox1.CharacterCasing = CharacterCasing.Normal;
            }
        }

        private void BindingEventWithChildControl()
        {
            this.Enter += delegate
            {
                if (this.Read_Only)
                {
                    this.BackColor = Color.White;
                }
                else
                {
                    this.textBox1.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                    this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                }
            };

            this.GotFocus += delegate
            {
                if (!this.Read_Only)
                {
                    this.textBox1.Focus();
                }
            };

            this.Leave += delegate
            {
                if (!this.Read_Only)
                {
                    this.BackColor = Color.White;
                    this.textBox1.BackColor = Color.White;
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

            this.SizeChanged += delegate
            {
                this.textBox1.Size = new Size(this.ClientSize.Width - 3, this.textBox1.Height);
            };

            this.textBox1.TextChanged += delegate
            {
                this.Texts = this.textBox1.Text;
            };
            this.textBox1.Enter += delegate
            {
                if (!this.read_Only)
                {
                    this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                    this.textBox1.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                }
                else
                {
                    this.BackColor = Color.White;
                    this.textBox1.BackColor = Color.White;
                }
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.read_Only && this.textBox1.Focused)
            {
                this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.textBox1.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
            }
            else
            {
                this.BackColor = Color.White;
                this.textBox1.BackColor = Color.White;
            }
            base.OnPaint(e);
        }
    }
}
