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
    public partial class XTextEdit : UserControl
    {
        private bool read_only = false;
        public bool _ReadOnly
        {
            get
            {
                return this.read_only;
            }
            set
            {
                this.textBox1.ReadOnly = value;
                //this.Refresh();
            }
        }

        //private string text;
        public string _Text
        {
            get
            {
                //return this.text;
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }

        //private HorizontalAlignment text_align = HorizontalAlignment.Left;
        public HorizontalAlignment _TextAlign
        {
            get
            {
                return this.textBox1.TextAlign;
            }
            set
            {
                this.textBox1.TextAlign = value;
            }
        }

        public BorderStyle _BorderStyle
        {
            get
            {
                return this.BorderStyle;
            }
            set
            {
                this.BorderStyle = value;

                //if (value == BorderStyle.Fixed3D)
                //{

                //}
                //else if (value == BorderStyle.FixedSingle)
                //{

                //}
                //else if (value == BorderStyle.None)
                //{

                //}

                //this.Refresh();
            }
        }

        public int _MaxLength
        {
            get
            {
                return this.textBox1.MaxLength;
            }
            set
            {
                this.textBox1.MaxLength = value;
            }
        }

        public CharacterCasing _CharacterCasing
        {
            get
            {
                return this.textBox1.CharacterCasing;
            }
            set
            {
                this.textBox1.CharacterCasing = value;
            }
        }

        private bool focused;
        public bool _Focused
        {
            get
            {
                return this.focused;
            }
        }

        public event EventHandler _TextChanged;
        public event EventHandler _GotFocus;
        public event EventHandler _Leave;
        public event EventHandler _DoubleClicked;

        public XTextEdit()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.TabStop = true;
            this.BackColor = Color.White;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (!this.read_only)
            {
                this.textBox1.Focus();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //if(this._TextAlign == HorizontalAlignment.Right)
            //{
            //    if (this._BorderStyle == BorderStyle.Fixed3D)
            //    {
            //        this.textBox1.Location = new Point(5, 2);
            //    }
            //    else if (this._BorderStyle == BorderStyle.FixedSingle)
            //    {
            //        this.textBox1.Location = new Point(4, 3);
            //    }
            //    else if (this._BorderStyle == BorderStyle.None)
            //    {
            //        this.textBox1.Location = new Point(3, 4);
            //    }
            //}
            //else
            //{
            //    if (this._BorderStyle == BorderStyle.Fixed3D)
            //    {
            //        this.textBox1.Location = new Point(1, 2);
            //    }
            //    else if (this._BorderStyle == BorderStyle.FixedSingle)
            //    {
            //        this.textBox1.Location = new Point(2, 3);
            //    }
            //    else if (this._BorderStyle == BorderStyle.None)
            //    {
            //        this.textBox1.Location = new Point(3, 4);
            //    }
            //}

            //if (this.read_only)
            //{
            //    TextFormatFlags flag;
            //    if (this._TextAlign == HorizontalAlignment.Right)
            //    {
            //        flag = TextFormatFlags.Top | TextFormatFlags.Right | TextFormatFlags.NoClipping | TextFormatFlags.SingleLine;
            //    }
            //    else if (this._TextAlign == HorizontalAlignment.Center)
            //    {
            //        flag = TextFormatFlags.Top | TextFormatFlags.HorizontalCenter | TextFormatFlags.NoClipping | TextFormatFlags.SingleLine;
            //    }
            //    else
            //    {
            //        flag = TextFormatFlags.Top | TextFormatFlags.Left | TextFormatFlags.NoClipping | TextFormatFlags.SingleLine;
            //    }

            //    if(this._TextAlign == HorizontalAlignment.Right)
            //    {
            //        if (this._BorderStyle == BorderStyle.Fixed3D)
            //        {
            //            TextRenderer.DrawText(e.Graphics, this.textBox1.Text, this.textBox1.Font, new Rectangle(e.ClipRectangle.X + 2, e.ClipRectangle.Y + 2, e.ClipRectangle.Width, e.ClipRectangle.Height), this.textBox1.ForeColor, flag);
            //        }
            //        else if (this._BorderStyle == BorderStyle.FixedSingle)
            //        {
            //            TextRenderer.DrawText(e.Graphics, this.textBox1.Text, this.textBox1.Font, new Rectangle(e.ClipRectangle.X + 1, e.ClipRectangle.Y + 3, e.ClipRectangle.Width, e.ClipRectangle.Height), this.textBox1.ForeColor, flag);
            //        }
            //        else if (this._BorderStyle == BorderStyle.None)
            //        {
            //            TextRenderer.DrawText(e.Graphics, this.textBox1.Text, this.textBox1.Font, new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y + 4, e.ClipRectangle.Width, e.ClipRectangle.Height), this.textBox1.ForeColor, flag);
            //        }
            //    }
            //    else
            //    {
            //        if (this._BorderStyle == BorderStyle.Fixed3D)
            //        {
            //            TextRenderer.DrawText(e.Graphics, this.textBox1.Text, this.textBox1.Font, new Rectangle(e.ClipRectangle.X - 2, e.ClipRectangle.Y + 2, e.ClipRectangle.Width, e.ClipRectangle.Height), this.textBox1.ForeColor, flag);
            //        }
            //        else if (this._BorderStyle == BorderStyle.FixedSingle)
            //        {
            //            TextRenderer.DrawText(e.Graphics, this.textBox1.Text, this.textBox1.Font, new Rectangle(e.ClipRectangle.X - 1, e.ClipRectangle.Y + 3, e.ClipRectangle.Width, e.ClipRectangle.Height), this.textBox1.ForeColor, flag);
            //        }
            //        else if (this._BorderStyle == BorderStyle.None)
            //        {
            //            TextRenderer.DrawText(e.Graphics, this.textBox1.Text, this.textBox1.Font, new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y + 4, e.ClipRectangle.Width, e.ClipRectangle.Height), this.textBox1.ForeColor, flag);
            //        }
            //    }

            //}
        }

        private void XTextEdit_Load(object sender, EventArgs e)
        {
            this.textBox1.Enter += delegate
            {
                this.textBox1.SelectionStart = this.textBox1.Text.Length;
            };

            this.textBox1.GotFocus += delegate(object sender_obj, EventArgs e_obj)
            {
                if (this.read_only)
                {
                    this.BackColor = Color.White;
                    this.textBox1.BackColor = Color.White;
                    this.focused = false;
                }
                else
                {
                    this.BackColor = AppResource.EditableControlBackColor;
                    this.textBox1.BackColor = AppResource.EditableControlBackColor;
                    this.focused = true;
                }

                if (this._GotFocus != null)
                {
                    this._GotFocus(this, e_obj);
                }
            };

            this.textBox1.Leave += delegate(object sender_obj, EventArgs e_obj)
            {
                this.BackColor = Color.White;
                this.textBox1.BackColor = Color.White;
                this.focused = false;

                if(this._Leave != null)
                {
                    this._Leave(this, e_obj);
                }
            };
        }

        private void textBox1_ReadOnlyChanged(object sender, EventArgs e)
        {
            this.read_only = ((TextBox)sender).ReadOnly;
            this.TabStop = (this._ReadOnly ? false : true);
            this.textBox1.TabStop = (this._ReadOnly ? false : true);
            
            //((TextBox)sender).TabStop = this._ReadOnly ? false : true;
            this.textBox1.Visible = this.read_only ? false : true;

            if (this.read_only)
            {
                this.BackColor = Color.White;
                this.textBox1.BackColor = Color.White;
                this.textBox1.Visible = false;
                this.label1.Visible = true;
                //this.Refresh();
            }

            if (!this.read_only)
            {
                this.textBox1.Visible = true;
                this.textBox1.SelectionStart = 0;
                this.label1.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //this.text = ((TextBox)sender).Text;
            this.label1.Text = ((TextBox)sender).Text;

            //if (this.read_only)
            //{
            //    this.Refresh();
            //}
            if(this._TextChanged != null)
            {
                this._TextChanged(this, e);
            }
        }

        private void XTextEdit_Resize(object sender, EventArgs e)
        {
            //if (this._ReadOnly)
            //{
            //    this.Refresh();
            //}
        }

        private void textBox1_TextAlignChanged(object sender, EventArgs e)
        {
            this.label1.TextAlign = ((TextBox)sender).TextAlign == HorizontalAlignment.Center ? ContentAlignment.TopCenter : (((TextBox)sender).TextAlign == HorizontalAlignment.Right ? ContentAlignment.TopRight : ContentAlignment.TopLeft);
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (this._DoubleClicked != null)
                this._DoubleClicked(this, e);
        }

        private void XTextEdit_TabStopChanged(object sender, EventArgs e)
        {
            //this.textBox1.TabStop = this.TabStop;
        }

        private void XTextEdit_Enter(object sender, EventArgs e)
        {
            if (this._ReadOnly)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
