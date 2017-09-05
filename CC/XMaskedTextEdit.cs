using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace CC
{
    public partial class XMaskedTextEdit : MaskedTextBox
    {
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (!this.ReadOnly)
            {
                this.BackColor = AppResource.EditableControlBackColor;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.BackColor = Color.White;
        }

    }
}
