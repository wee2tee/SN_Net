using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CC.Dialog;
using System.Reflection;

namespace CC
{
    public partial class XBrowseBox : UserControl
    {
        private string _text = string.Empty;
        public string _Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._textBox.Text = value;
            }
        }

        private bool _readonly = false;
        public bool _ReadOnly
        {
            get
            {
                return this._readonly;
            }
            set
            {
                this._textBox.ReadOnly = value;
                this._btnBrowse.Enabled = !value;
            }
        }

        public HorizontalAlignment _TextAlign
        {
            get
            {
                return this._textBox.TextAlign;
            }
            set
            {
                this._textBox.TextAlign = value;
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

                if (this._UseImage)
                {
                    if (value == BorderStyle.Fixed3D)
                    {
                        this._btnBrowse.Image = CC.Properties.Resources.zoom_12;
                    }
                    else if (value == BorderStyle.FixedSingle)
                    {
                        this._btnBrowse.Image = CC.Properties.Resources.zoom_14;
                    }
                    else if (value == BorderStyle.None)
                    {
                        this._btnBrowse.Image = CC.Properties.Resources.zoom_16;
                    }
                }

                this.Refresh();
            }
        }

        //public bool _AllowTextEdit
        //{
        //    get
        //    {
        //        return !this._textBox.ReadOnly;
        //    }
        //    set
        //    {
        //        this._textBox.ReadOnly = !value;
        //    }
        //}

        private bool use_image = true;
        public bool _UseImage
        {
            get
            {
                return this.use_image;
            }
            set
            {
                this.use_image = value;
                this.Refresh();
            }
        }

        public CharacterCasing _CharacterCasing
        {
            get
            {
                return this._textBox.CharacterCasing;
            }
            set
            {
                this._textBox.CharacterCasing = value;
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

        private string list_dialog_title;
        private Object list_object;
        public string FieldNameTextBoxShow;

        public event EventHandler _ButtonClick;
        public event EventHandler _GotFocus;
        public event EventHandler _Leave;
        public event EventHandler _DoubleClicked;
        public event EventHandler _TextChanged;

        public XBrowseBox()
        {
            InitializeComponent();
        }

        /** Create browse box with list selection dialog
         *param name="list_dialog_title" Windows title for list dialog
         * param name="list_object" Object list to show in list dialog
         * param name="column_collection" Gridview column collection to show in list dialog
         * param name="field_name_textbox_show" Field name of object to show in textbox after selected **/
        public XBrowseBox(string list_dialog_title, object list_object, string field_name_textbox_show)
            : this()
        {
            this.list_dialog_title = list_dialog_title;
            this.list_object = list_object;
            //this.list_object = new object();
            this.FieldNameTextBoxShow = field_name_textbox_show;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Height = 23;

            this.TabStop = true;
            this._textBox.GotFocus += delegate(object sender_obj, EventArgs e_obj)
            {
                if (this._ReadOnly)
                {
                    this.BackColor = Color.White;
                    this._textBox.BackColor = Color.White;
                    this.focused = false;
                }
                else
                {
                    this.BackColor = AppResource.EditableControlBackColor;
                    this._textBox.BackColor = AppResource.EditableControlBackColor;
                    this._textBox.SelectionStart = this._textBox.Text.Length;
                    this.focused = true;
                }

                if(this._GotFocus != null)
                {
                    this._GotFocus(this, e_obj);
                }
            };
            this._textBox.Leave += delegate(object sender_obj, EventArgs e_obj)
            {
                this.BackColor = Color.White;
                this._textBox.BackColor = Color.White;
                this.focused = false;

                if(this._Leave != null)
                {
                    this._Leave(this, e_obj);
                }
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!this._UseImage)
                this._btnBrowse.Image = null;

            //if(this._BorderStyle == BorderStyle.Fixed3D)
            //{
            //    this._textBox.Location = new Point(1, 2);
            //}
            //else if (this._BorderStyle == BorderStyle.FixedSingle)
            //{
            //    this._textBox.Location = new Point(2, 3);
            //}
            //else if(this._BorderStyle == BorderStyle.None)
            //{
            //    this._textBox.Location = new Point(3, 4);
            //}

            //if (this._ReadOnly)
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

            //    //TextRenderer.DrawText(e.Graphics, this._Text, this._textBox.Font, new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y + 2, e.ClipRectangle.Width, e.ClipRectangle.Height), this._textBox.ForeColor, flag);
            //    if (this._BorderStyle == BorderStyle.Fixed3D)
            //    {
            //        TextRenderer.DrawText(e.Graphics, this._Text, this._textBox.Font, new Rectangle(e.ClipRectangle.X - 2, e.ClipRectangle.Y + 2, e.ClipRectangle.Width, e.ClipRectangle.Height), this._textBox.ForeColor, flag);
            //        return;
            //    }
            //    else if(this._BorderStyle == BorderStyle.FixedSingle)
            //    {
            //        TextRenderer.DrawText(e.Graphics, this._Text, this._textBox.Font, new Rectangle(e.ClipRectangle.X - 1, e.ClipRectangle.Y + 3, e.ClipRectangle.Width, e.ClipRectangle.Height), this._textBox.ForeColor, flag);
            //        return;
            //    }
            //    else if(this._BorderStyle == BorderStyle.None)
            //    {
            //        TextRenderer.DrawText(e.Graphics, this._Text, this._textBox.Font, new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y + 4, e.ClipRectangle.Width, e.ClipRectangle.Height), this._textBox.ForeColor, flag);
            //        return;
            //    }
            //}
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (!this._ReadOnly)
            {
                this._textBox.Focus();
            }
        }

        private void _textBox_TextChanged(object sender, EventArgs e)
        {
            this._text = ((TextBox)sender).Text;
            this.label1.Text = ((TextBox)sender).Text;
            if(this._TextChanged != null)
            {
                this._TextChanged(this, e);
            }

            if (this._ReadOnly)
            {
                this.Refresh();
            }
        }

        private void _textBox_ReadOnlyChanged(object sender, EventArgs e)
        {
            this._readonly = ((TextBox)sender).ReadOnly;
            this.TabStop = this._ReadOnly ? false : true;
            ((TextBox)sender).Visible = ((TextBox)sender).ReadOnly ? false : true;
            this.label1.Visible = !((TextBox)sender).Visible;

            if (this._ReadOnly)
            {
                this.BackColor = Color.White;
                this._textBox.BackColor = Color.White;
            }

            //this.Refresh();
        }

        public void _btnBrowse_Click(object sender, EventArgs e)
        {
            if(this._ButtonClick != null)
            {
                this._ButtonClick(this, e);
            }
            else
            {
                if (this.list_object != null)
                {
                    ListDialog ld = new ListDialog(this.ParentForm, this, this.list_dialog_title, this.list_object);
                    if (ld.ShowDialog() == DialogResult.OK)
                    {
                        var shown_text = ld.selected_row.Cells[this.FieldNameTextBoxShow].Value;
                        this._textBox.Text = shown_text.ToString();
                        this._textBox.SelectionStart = this._textBox.Text.Length;
                    }
                }
            }

            this._textBox.Focus();
        }

        private void _btnBrowse_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this._textBox.BackColor = Color.White;
        }

        public void SetListObject(Object list_object)
        {
            this.list_object = list_object;
        }

        public void PerformButtonClick()
        {
            this._btnBrowse.PerformClick();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6)
            {
                this._btnBrowse.Focus();
                this._btnBrowse.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (this._DoubleClicked != null)
                this._DoubleClicked(this, e);
        }
    }
}
