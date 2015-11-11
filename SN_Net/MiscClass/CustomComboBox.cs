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
    public partial class CustomComboBox : UserControl
    {
        private bool readOnly;
        //private int selectedItemIndex;
        private string texts;
        public bool item_shown;

        public CustomComboBox()
        {
            InitializeComponent();
            this.Height = 23;
            this.label1.Text = "";
            this.comboBox1.Text = "";
            this.comboBox1.Items.Clear();
            this.Read_Only = true;

            this.comboBox1.TextChanged += delegate
            {
                this.label1.Text = this.comboBox1.Text;
            };
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
                return this.texts;
            }
            set
            {
                this.texts = value;
                this.comboBox1.Text = value;
            }
        }

        //public int SelectedItemIndex
        //{
        //    get
        //    {
        //        return this.selectedItemIndex;
        //    }
        //    set
        //    {
        //        this.selectedItemIndex = value;
        //        this.comboBox1.SelectedIndex = value;
        //    }
        //}

        private void ShowHideControl()
        {
            if (this.readOnly)
            {
                this.label1.Visible = true;
                this.comboBox1.Visible = false;
            }
            else
            {
                this.comboBox1.Visible = true;
                this.label1.Visible = false;
            }
        }

        public void AddItem(object item)
        {
            this.comboBox1.Items.Add(item);
        }

        public void ClearItem()
        {
            this.comboBox1.Items.Clear();
        }

        protected override void OnCreateControl()
        {
            this.BackColor = Color.White;
            this.comboBox1.Size = new Size(this.ClientSize.Width + 2, this.comboBox1.Height);
            this.label1.Size = new Size(this.ClientSize.Width - 2, this.label1.Height);
            this.label1.BackColor = Color.White;
            base.OnCreateControl();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.comboBox1.Size = new Size(this.ClientSize.Width - 5, this.comboBox1.Height);
            this.label1.Size = new Size(this.ClientSize.Width - 2, this.label1.Height);
            base.OnSizeChanged(e);
        }

        private void CustomComboBox_Load(object sender, EventArgs e)
        {
            this.BindEventWithChildControl();
        }

        private void BindEventWithChildControl()
        {
            this.comboBox1.TextChanged += delegate
            {
                this.Texts = this.comboBox1.Text;
            };

            this.comboBox1.DropDown += delegate
            {
                this.item_shown = true;
            };

            this.comboBox1.DropDownClosed += delegate
            {
                this.item_shown = false;
            };
            this.comboBox1.SelectedIndexChanged += delegate
            {
                this.label1.Text = this.comboBox1.Text;
            };
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    if (!this.readOnly && this.comboBox1.Focused)
        //    {
        //        this.comboBox1.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
        //    }
        //    base.OnPaint(e);
        //}

        protected override void OnLeave(EventArgs e)
        {
            this.comboBox1.BackColor = Color.FromKnownColor(KnownColor.Control);
            base.OnLeave(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6)
            {
                SendKeys.Send("{F4}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
