using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    public class FormControlSequence
    {
        private List<Control> list_control;

        private FormControlSequence(List<Control> list_control)
        {
            this.list_control = list_control;
            this.AddEventHandler();
        }

        public static void Attach(List<Control> list_control)
        {
            FormControlSequence fc = new FormControlSequence(list_control);
        }

        private void AddEventHandler()
        {
            foreach (Control ct in this.list_control)
            {
                ct.KeyDown += new KeyEventHandler(this.keyDownEventHandler);
                ct.GotFocus += new EventHandler(this.GotFocusHandler);
                ct.Leave += new EventHandler(this.LeaveHandler);
            }
        }

        private void keyDownEventHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int ndx = this.list_control.FindIndex(ct => ct.Equals((Control)sender));

                if(!(this.list_control[ndx] is Button)){ // If current control is Button e.g. Submit Button/Cancel Button perform it's default handler(simply click)
                    if (ndx < this.list_control.Count - 1) // if not the last control in the list go ahead to next control
                    {
                        this.list_control[ndx + 1].Focus();
                        
                        if (this.list_control[ndx + 1] is TextBox)
                        {
                            ((TextBox)this.list_control[ndx + 1]).SelectionStart = ((TextBox)this.list_control[ndx + 1]).Text.Length;
                            ((TextBox)this.list_control[ndx + 1]).SelectionLength = 0;
                        }
                    }
                }
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void GotFocusHandler(object sender, EventArgs e)
        {
            if ((Control)sender is TextBox || (Control)sender is MaskedTextBox)
            {
                ((Control)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                ((Control)sender).ForeColor = Color.Black;
            }
        }

        private void LeaveHandler(object sender, EventArgs e)
        {
            if ((Control)sender is TextBox || (Control)sender is MaskedTextBox)
            {
                ((Control)sender).BackColor = Color.White;
                ((Control)sender).ForeColor = Color.Black;
            }
        }
    }
}
