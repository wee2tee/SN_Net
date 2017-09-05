using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC
{
    public partial class XComboBox : ComboBox
    {
        public XComboBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (this.Enabled)
            {
                this.BackColor = AppResource.EditableControlBackColor;
            }
            else
            {
                this.BackColor = Color.White;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            this.BackColor = Color.White;
        }
    }
}
