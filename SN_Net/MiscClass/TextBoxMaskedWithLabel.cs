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
    public partial class TextBoxMaskedWithLabel : UserControl
    {
        public int MaxLength { get; set; }
        public string staticText { get; set; }
        //public string editableText { get; set; }
        private bool is_focused;

        public TextBoxMaskedWithLabel()
        {
            InitializeComponent();
        }

        private void TextBoxMaskedWithLabel_Load(object sender, EventArgs e)
        {
            this.MaxLength = 100;
            //this.txtEdit.MaxLength = this.MaxLength - this.lblMask.Text.Length;
            this.Height = 23;
            
            this.BackColor = Color.White;
            this.txtEdit.BackColor = Color.White;
            this.txtStatic.AutoSize = true;

            //this.txtStatic.Click += delegate
            //{
            //    this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
            //    this.txtEdit.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
            //    this.txtEdit.ForeColor = Color.Black;
            //    this.txtEdit.Focus();
            //    this.txtEdit.SelectionStart = 0;
            //    this.txtEdit.SelectionLength = 0;
            //};
            this.txtStatic.Enter += delegate
            {
                this.is_focused = true;
                this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.txtEdit.Focus();
            };
            this.txtEdit.Enter += delegate
            {
                this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.txtStatic.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.txtEdit.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.txtEdit.ForeColor = Color.Black;
                this.txtEdit.SelectionStart = 0; //this.txtEdit.Text.Length;
                this.txtEdit.SelectionLength = 0;
            };
            this.txtEdit.GotFocus += delegate
            {
                this.is_focused = true;
                this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
            };
            this.txtEdit.Leave += delegate
            {
                this.BackColor = Color.White;
                this.txtEdit.BackColor = Color.White;
                this.txtStatic.BackColor = Color.White;
                this.txtEdit.ForeColor = Color.Black;
                this.is_focused = false;
            };
        }

        private void TextBoxMaskedWithLabel_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = (this.is_focused ? ColorResource.ACTIVE_CONTROL_BACKCOLOR : Color.White);
            //this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;

            this.txtStatic.Text = (this.staticText == null ? "" : this.staticText);
            int width = (int)this.CreateGraphics().MeasureString(this.staticText, this.Font).Width - 8;
            this.txtStatic.Location = new Point(3, 3);
            this.txtStatic.Width = width;

            int x_pos = (this.txtStatic.Width == 0 ? 3 : this.txtStatic.Width + 3);
            this.txtEdit.Location = new Point(x_pos, 3);
            this.txtEdit.Width = this.ClientSize.Width - (this.txtStatic.ClientSize.Width - (this.txtStatic.ClientSize.Width == 0 ? 3 : 0));
        }

    }
}
