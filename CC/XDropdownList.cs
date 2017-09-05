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
    public partial class XDropdownList : UserControl
    {
        private bool read_only;
        public bool _ReadOnly
        {
            get
            {
                //return this.read_only;
                return !this.comboBox1.Enabled;
            }
            set
            {
                this.read_only = value;
                this.comboBox1.Enabled = !value;
            }
        }

        public string _Text
        {
            get
            {
                return this.comboBox1.Text;
            }
            set
            {
                this.comboBox1.Text = value;
            }
        }

        private bool _focused;
        public bool _Focused
        {
            get
            {
                return this._focused;
            }
        }

        public XDropdownListItem _SelectedItem
        {
            get
            {
                return (XDropdownListItem)this.comboBox1.SelectedItem;
            }
            set
            {
                if(this.comboBox1.Items.Count > 0 &&  this.comboBox1.Items.Cast<XDropdownListItem>().Where(i => i.Text == value.Text && i.Value == value.Value).FirstOrDefault() != null)
                {
                    this.comboBox1.SelectedItem = this.comboBox1.Items.Cast<XDropdownListItem>().Where(i => i.Text == value.Text && i.Value == value.Value).First();
                }
            }
        }

        public ComboBox.ObjectCollection _Items
        {
            get
            {
                return this.comboBox1.Items;
            }
        }

        public event EventHandler _SelectedItemChanged;
        public event EventHandler _GotFocus;
        public event EventHandler _Leave;
        public event EventHandler _DoubleClicked;

        public XDropdownList()
        {
            InitializeComponent();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (!this.read_only)
            {
                this.comboBox1.Focus();
            }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    if (this.read_only)
        //    {
        //        if (this.comboBox1.SelectedItem != null && this.comboBox1.SelectedItem is XDropdownListItem)
        //            TextRenderer.DrawText(e.Graphics, ((XDropdownListItem)this.comboBox1.SelectedItem).ToString(), this.comboBox1.Font, new Point(0, 3), this.comboBox1.ForeColor);
        //    }
        //}

        private void XDropdownList_Load(object sender, EventArgs e)
        {
            this.comboBox1.GotFocus += delegate(object sender_obj, EventArgs e_obj)
            {
                if (this.read_only)
                {
                    this.BackColor = Color.White;
                    this.comboBox1.BackColor = Color.White;
                    this._focused = false;
                }
                else
                {
                    this.BackColor = AppResource.EditableControlBackColor;
                    this.comboBox1.BackColor = AppResource.EditableControlBackColor;
                    this._focused = true;
                }

                if(this._GotFocus != null)
                {
                    this._GotFocus(this, e_obj);
                }
            };

            this.comboBox1.Leave += delegate(object sender_obj, EventArgs e_obj)
            {
                this.BackColor = Color.White;
                this.comboBox1.BackColor = Color.White;
                this._focused = false;

                if(this._Leave != null)
                {
                    this._Leave(this, e_obj);
                }
            };
        }

        private void comboBox1_EnabledChanged(object sender, EventArgs e)
        {
            //this.read_only = !this.Enabled;
            this.comboBox1.Visible = this.comboBox1.Enabled;
            this.TabStop = this.read_only ? false : true;

            if (this.read_only)
            {
                this.BackColor = Color.White;
                this.comboBox1.BackColor = Color.White;
                this.Refresh();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.read_only)
                this.Refresh();

            if (this._SelectedItemChanged != null)
                this._SelectedItemChanged(this, e);

            this.label1.Text = ((ComboBox)sender).Text;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F6)
            {
                SendKeys.Send("{F4}");
                return true;
            }

            if(keyData == Keys.Escape)
            {
                if (this.comboBox1.DroppedDown)
                {
                    this.comboBox1.DroppedDown = false;
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //this.label1.Text = ((ComboBox)sender).Text;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if(this._DoubleClicked != null)
            {
                this._DoubleClicked(this, e);
            }
        }
    }

    //public static class XDropdownListHelper
    //{
    //    public static void Add(this ComboBox.ObjectCollection items)
    //    {

    //    }
    //}

    public class XDropdownListItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
