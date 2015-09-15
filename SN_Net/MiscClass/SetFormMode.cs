using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SN_Net.MiscClass
{
    public static class SetFormMode
    {
        public static void Ready(this Form form, List<Control> edit_control, List<Label> label_control)
        {
            foreach (Control ct in edit_control)
            {
                int ndx = edit_control.FindIndex(t => t.Equals(ct));

                ct.SetBounds(label_control[ndx].Left, label_control[ndx].Top, ct.Width, ct.Height);
                ct.Text = label_control[ndx].Text;
                ct.Visible = false;
                if (ct is TextBox)
                {
                    ((TextBox)ct).ReadOnly = true;
                    ((TextBox)ct).BorderStyle = BorderStyle.FixedSingle;
                }
                if (ct is MaskedTextBox)
                {
                    ((MaskedTextBox)ct).ReadOnly = true;
                    ((MaskedTextBox)ct).BorderStyle = BorderStyle.FixedSingle;
                }
                label_control[ndx].Visible = true;
                //DoubleClickHandler.Attach(form, edit_control, label_control);
            }
        }

        public static void Add(this Form form, List<Control> edit_control, List<Label> label_control)
        {
            foreach (Control ct in edit_control)
            {
                int ndx = edit_control.FindIndex(t => t.Equals(ct));

                ct.SetBounds(label_control[ndx].Left, label_control[ndx].Top, ct.Width, ct.Height);
                ct.Visible = true;
                if (ct is TextBox)
                {
                    ((TextBox)ct).ReadOnly = false;
                    ((TextBox)ct).BorderStyle = BorderStyle.FixedSingle;
                    ((TextBox)ct).Text = "";
                    //((TextBox)ct).GotFocus += new EventHandler(SetFormMode.controlFocusedHandler);
                    //((TextBox)ct).Leave += new EventHandler(SetFormMode.controlLeaveHandler);
                }
                if (ct is MaskedTextBox)
                {
                    ((MaskedTextBox)ct).ReadOnly = false;
                    ((MaskedTextBox)ct).BorderStyle = BorderStyle.FixedSingle;
                    ((MaskedTextBox)ct).Text = "";
                    //((MaskedTextBox)ct).GotFocus += new EventHandler(SetFormMode.controlFocusedHandler);
                    //((MaskedTextBox)ct).Leave += new EventHandler(SetFormMode.controlLeaveHandler);
                    
                }
                if (ct is ComboBox)
                {
                    ((ComboBox)ct).SelectedIndex = 0;
                }
                label_control[ndx].Visible = false;
            }
            
            edit_control.First<Control>().Focus();
            if (edit_control.First() is TextBox)
            {
                ((TextBox)edit_control.First()).SelectionStart = ((TextBox)edit_control.First()).Text.Length;
            }
            if (edit_control.First() is MaskedTextBox)
            {
                ((MaskedTextBox)edit_control.First()).SelectionStart = 0;
            }
        }

        public static void Edit(this Form form, List<Control> edit_control, List<Label> label_control)
        {
            foreach (Control ct in edit_control)
            {
                int ndx = edit_control.FindIndex(t => t.Equals(ct));

                ct.SetBounds(label_control[ndx].Left, label_control[ndx].Top, ct.Width, ct.Height);
                ct.Text = label_control[ndx].Text;
                ct.Visible = true;
                if (ct is TextBox)
                {
                    ((TextBox)ct).ReadOnly = false;
                    ((TextBox)ct).BorderStyle = BorderStyle.FixedSingle;
                    //((TextBox)ct).GotFocus += new EventHandler(SetFormMode.controlFocusedHandler);
                    //((TextBox)ct).Leave += new EventHandler(SetFormMode.controlLeaveHandler);

                }
                if (ct is MaskedTextBox)
                {
                    ((MaskedTextBox)ct).ReadOnly = false;
                    ((MaskedTextBox)ct).BorderStyle = BorderStyle.FixedSingle;
                    //((MaskedTextBox)ct).GotFocus += new EventHandler(SetFormMode.controlFocusedHandler);
                    //((MaskedTextBox)ct).Leave += new EventHandler(SetFormMode.controlLeaveHandler);
                }
                label_control[ndx].Visible = false;
            }
            
            edit_control.First<Control>().Focus();
            if (edit_control.First() is TextBox)
            {
                ((TextBox)edit_control.First()).SelectionStart = ((TextBox)edit_control.First()).Text.Length;
            }
            if (edit_control.First() is MaskedTextBox)
            {
                ((MaskedTextBox)edit_control.First()).SelectionStart = ((MaskedTextBox)edit_control.First()).Text.Length;
            }
        }

        public static void controlFocusedHandler(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                ((TextBox)sender).ForeColor = Color.Black;
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
            if (sender is MaskedTextBox)
            {
                ((MaskedTextBox)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                ((MaskedTextBox)sender).ForeColor = Color.Black;
                ((MaskedTextBox)sender).SelectionStart = ((MaskedTextBox)sender).Text.Length;
            }
        }

        public static void controlLeaveHandler(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = Color.White;
                ((TextBox)sender).ForeColor = Color.Black;
            }
            if (sender is MaskedTextBox)
            {
                ((MaskedTextBox)sender).BackColor = Color.White;
                ((MaskedTextBox)sender).ForeColor = Color.Black;
            }
        }
    }
}
