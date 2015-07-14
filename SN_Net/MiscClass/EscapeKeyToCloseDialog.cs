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

        private EscapeKeyToCloseDialog(Control root_control)
        {
            this.form = root_control.FindForm();
            this.Attach(form);
        }

        public static void ActiveEscToClose(Control root_control){
            EscapeKeyToCloseDialog es = new EscapeKeyToCloseDialog(root_control);
        }

        /// <summary>
        /// Attach KeyEventHandler to all controls
        /// </summary>
        /// <param name="ct">Root control that start to find all controls in it</param>
        private void Attach(Control ct)
        {
            ct.KeyDown += new KeyEventHandler(this.escapeToClose);
            foreach (Control c in ct.Controls)
            {
                this.Attach(c);
            }

        }

        private void escapeToClose(object sender, KeyEventArgs e){
            if (e.KeyCode == Keys.Escape)
            {
                this.form.DialogResult = DialogResult.Cancel;
                this.form.Close();
            }
        }
    }
}
