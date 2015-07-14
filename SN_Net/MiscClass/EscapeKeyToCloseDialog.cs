using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    /// <summary>
    /// This class is use to attach the escape key to close dialog
    /// (for dialog window only)
    /// </summary>
    public class EscapeKeyToCloseDialog
    {
        private Form form;

        public EscapeKeyToCloseDialog(Form form)
        {
            this.form = form;
            this.Attach(form);
        }

        public static void ActiveEscToClose(Form form){
            EscapeKeyToCloseDialog es = new EscapeKeyToCloseDialog(form);
        }

        /// <summary>
        /// Attach KeyEventHandler to all controls
        /// </summary>
        /// <param name="ct">Root control that start to find all controls in it</param>
        private void Attach(Control ct)
        {
            foreach (Control c in ct.Controls)
            {
                if (c is GroupBox)
                {
                    this.Attach(c);
                }
                else
                {
                    c.KeyDown += new KeyEventHandler(this.escapeToClose);
                }
            }

        }

        private void escapeToClose(object sender, KeyEventArgs e){
            if (e.KeyCode == Keys.Escape)
            {
                form.DialogResult = DialogResult.Cancel;
                form.Close();
            }
        }
    }
}
