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
    public partial class CustomTextBoxMaskedWithLabel : UserControl
    {
        public int MaxLength { get; set; }
        private string staticText;
        private string editableText;
        private bool is_focused;
        private bool readOnly;

        public CustomTextBoxMaskedWithLabel()
        {
            InitializeComponent();
        }

        /**
         * <summary>Gets or Sets static text</summary>
         * */
        public string StaticText
        {
            get
            {
                return this.staticText;
            }
            set
            {
                this.staticText = value;
                this.txtStatic.Text = value;
                this.SetControlWidth();
            }
        }
        /**
         * <summary>Gets or Sets editable text</summary>
         * */
        public string EditableText
        {
            get
            {
                return this.editableText;
            }
            set
            {
                this.editableText = value;
                this.txtEdit.Text = value;
            }
        }

        /**
         * <summary>Gets all text (static text concatenate with editable text)</summary>
         * */
        public string Texts
        {
            get
            {
                return (this.staticText.Length > 0 && this.staticText != null ? this.staticText + " " + this.editableText : this.editableText);
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
                this.SetReadOnlyState();
            }
        }

        private void SetReadOnlyState()
        {
            if (this.readOnly)
            {
                this.txtEdit.ReadOnly = true;
            }
            else
            {
                this.txtEdit.ReadOnly = false;
            }
        }

        private void TextBoxMaskedWithLabel_Load(object sender, EventArgs e)
        {
            this.MaxLength = 100;
            //this.txtEdit.MaxLength = this.MaxLength - this.lblMask.Text.Length;
            this.Height = 23;
            
            this.BackColor = Color.White;
            this.txtEdit.BackColor = Color.White;
            this.txtStatic.AutoSize = true;
            this.SetControlWidth();

            this.txtStatic.Enter += delegate
            {
                this.is_focused = true;
                if (this.readOnly)
                {
                    this.BackColor = Color.White;
                    this.txtEdit.BackColor = Color.White;
                    this.txtStatic.BackColor = Color.White;
                }
                else
                {
                    this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                    this.txtEdit.Focus();
                }
            };
            this.txtEdit.Enter += delegate
            {
                if (this.readOnly)
                {
                    this.BackColor = Color.White;
                    this.txtEdit.BackColor = Color.White;
                    this.txtStatic.BackColor = Color.White;
                }
                else
                {
                    this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                    this.txtStatic.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                    this.txtEdit.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                    this.txtEdit.ForeColor = Color.Black;
                    this.txtEdit.SelectionStart = 0; //this.txtEdit.Text.Length;
                    this.txtEdit.SelectionLength = 0;
                }
            };
            this.txtEdit.GotFocus += delegate
            {
                if (this.readOnly)
                {
                    this.BackColor = Color.White;
                }
                else
                {
                    this.is_focused = true;
                    this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                }
            };
            this.txtEdit.Leave += delegate
            {
                this.BackColor = Color.White;
                this.txtEdit.BackColor = Color.White;
                this.txtStatic.BackColor = Color.White;
                this.txtEdit.ForeColor = Color.Black;
                this.is_focused = false;
            };
            this.txtEdit.TextChanged += delegate
            {
                this.editableText = this.txtEdit.Text;
            };
        }

        private void TextBoxMaskedWithLabel_Paint(object sender, PaintEventArgs e)
        {
            //this.BackColor = (this.is_focused ? ColorResource.ACTIVE_CONTROL_BACKCOLOR : Color.White);
            ////this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;

            //this.txtStatic.Text = (this.staticText == null ? "" : this.staticText);
            //int width = (int)this.CreateGraphics().MeasureString(this.staticText, this.Font).Width - 8;
            //this.txtStatic.Location = new Point(3, 3);
            //this.txtStatic.Width = width;

            //int x_pos = (this.txtStatic.Width == 0 ? 3 : this.txtStatic.Width + 3);
            //this.txtEdit.Location = new Point(x_pos, 3);
            //this.txtEdit.Width = this.ClientSize.Width - (this.txtStatic.ClientSize.Width - (this.txtStatic.ClientSize.Width == 0 ? 3 : 0));
        }

        private void SetControlWidth()
        {
            this.txtStatic.Text = (this.staticText == null ? "" : this.staticText);
            int width = (int)this.CreateGraphics().MeasureString(this.staticText, this.Font).Width - 8;
            this.txtStatic.Location = new Point(3, 3);
            this.txtStatic.Width = width;

            int x_pos = (this.txtStatic.Width == 0 ? 3 : this.txtStatic.Width + 3);
            this.txtEdit.Location = new Point(x_pos, 3);
            this.txtEdit.Width = this.ClientSize.Width - (this.txtStatic.ClientSize.Width - (this.txtStatic.ClientSize.Width == 0 ? 3 : 0));
            this.txtEdit.MaxLength = 100 - (this.txtStatic.Text.Length + 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.txtEdit.Width = this.ClientSize.Width - (this.txtStatic.ClientSize.Width - (this.txtStatic.ClientSize.Width == 0 ? 3 : 0) + 6);
            if (this.is_focused)
            {
                this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.txtStatic.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.txtEdit.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
            }
            base.OnPaint(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this.BackColor = Color.White;
            this.txtStatic.BackColor = Color.White;
            this.txtEdit.BackColor = Color.White;
            base.OnLeave(e);
        }
    }

    public static class TextBoxMaskedWithLabelHelper
    {
        public static void FitToCell(this CustomTextBoxMaskedWithLabel textbox, DataGridViewCell cell)
        {
            Rectangle rect = cell.DataGridView.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
        }
    }
}
