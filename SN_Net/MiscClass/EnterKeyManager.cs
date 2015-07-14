using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SN_Net.Subform;
using SN_Net.MiscClass;

namespace SN_Net.MiscClass
{
    public class EnterKeyManager
    {
        private Control parent_control;
        private List<Control> list_control = new List<Control>();

        private EnterKeyManager(Control control)
        {
            this.parent_control = control;
            this.Attach(control);
        }

        public static void Active(Control root_control)
        {
            EnterKeyManager em = new EnterKeyManager(root_control);
        }

        private void Attach(Control control)
        {
            if ((control is TextBox || control is ComboBox || control is Button) && control.TabStop == true)
            {
                control.KeyDown += new KeyEventHandler(this.goToNextControl);
                //control.Enter += new EventHandler(this.focusedBackgroundColor);
                control.Leave += new EventHandler(this.leaveBackgroundColor);
                
                list_control.Add(control);
            }
            foreach (Control ct in control.Controls)
            {
                this.Attach(ct);
            }
        }

        private void focusedBackgroundColor(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.YellowGreen;
        }

        private void leaveBackgroundColor(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.White;
        }

        private void goToNextControl(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control c = sender as Control;
                if ((string)c.Tag == "require" && c.Text.Length == 0)
                {
                    // stay here Do nothing.
                }
                else
                {
                    int curr_index = ((Control)sender).TabIndex;
                    foreach (Control ct in list_control)
                    {
                        if (ct.TabIndex == curr_index + 1 && ct.TabStop == true)
                        {
                            ct.Focus();
                            break;
                        }

                    }
                }
            }
        }
    }
}
