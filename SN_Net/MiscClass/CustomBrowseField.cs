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
    public partial class CustomBrowseField : UserControl
    {
        private bool is_read_only;
        private string _text;
        private bool _required_text;
        private int _max_length;
        private int btn_width = 0;
        private StringFormat str_format_left;


        public string _Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                this._textBox.Text = value;
            }
        }

        public bool _ReadOnly
        {
            get
            {
                return this.is_read_only;
            }
            set
            {
                this.SetControlVisualMode(value);
            }
        }

        public bool _RequiredText
        {
            get
            {
                return this._required_text;
            }
            set
            {
                this._required_text = value;
            }
        }

        public int _MaxLength
        {
            get
            {
                return this._max_length;
            }
            set
            {
                this._max_length = value;
                this._textBox.MaxLength = value;
            }
        }

        public CustomBrowseField()
        {
            InitializeComponent();
        }

        private void CustomBrowseField_Load(object sender, EventArgs e)
        {
            this.BindControlEventHandler();
            str_format_left = new StringFormat()
            { 
                FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit, 
                Alignment = StringAlignment.Near, 
                LineAlignment = StringAlignment.Center 
            };
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.BackColor = Color.White;
            this.btn_width = this._btnBrowse.ClientSize.Width - 2;
            this.HideButton();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.is_read_only)
            {
                this.DrawText();
            }
        }

        private void SetControlVisualMode(bool is_read_only)
        {
            if (is_read_only)
            {
                this.is_read_only = is_read_only;
                this._textBox.ReadOnly = true;
                this._textBox.Visible = false;
                this.HideButton();
                this.DrawText();
            }
            else
            {
                this.is_read_only = is_read_only;
                this._textBox.ReadOnly = false;
                this._textBox.Visible = true;
                this.ShowButton();
            }
        }

        private void HideButton()
        {
            this._textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._btnBrowse.Visible = false;
            this.Width -= this.btn_width;
        }

        private void ShowButton()
        {
            this._textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this._btnBrowse.Visible = true;
            this.Width += this.btn_width;
        }

        private void DrawText()
        {
            using (Font font = new Font("tahoma", 9.75f))
            {
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    this.CreateGraphics().DrawString(this._text, font, brush, this.ClientRectangle, str_format_left);
                }
            }
        }

        private void BindControlEventHandler()
        {
            this.Enter += delegate(object sender, EventArgs e)
            {
                if (!this.is_read_only)
                {
                    this._textBox.Focus();
                    if (!this._btnBrowse.Visible)
                        this.ShowButton();
                }
            };
            this.Leave += delegate(object sender, EventArgs e)
            {
                if (this._required_text && this._textBox.Text.Trim().Length == 0)
                {
                    this.Focus();
                    this._btnBrowse.PerformClick();
                    return;
                }

                if (!this.is_read_only)
                    this.HideButton();
            };
            #region _textBox event handler
            this._textBox.TextChanged += delegate(object sender, EventArgs e)
            {
                this._Text = this._textBox.Text;
            };

            this._textBox.GotFocus += delegate(object sender, EventArgs e)
            {
                this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this._textBox.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this._textBox.SelectionStart = 0;
            };
            this._textBox.Leave += delegate(object sender, EventArgs e)
            {
                this.BackColor = Color.White;
                this._textBox.BackColor = Color.White;
            };
            #endregion _textBox event handler

            #region _btnBrowse event handler
            this._btnBrowse.Click += delegate
            {
                this._textBox.Focus();
            };
            #endregion _btnBrowse event handler
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6)
            {
                this._btnBrowse.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
