using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SN_Net.MiscClass
{
    public class FormState
    {
        public const string FORM_STATE_READY = "Ready";
        public const string FORM_STATE_ADD = "Add";
        public const string FORM_STATE_EDIT = "Edit";
        private Control focusedControl;
        private Control dummyControl;

        public FormState(Control tabpage)
        {
            this.findDummyControl(tabpage);
            this.dummyControl.SetBounds(0, 0, 0, 0);
        }

        #region manage dummyTextBox for throw Focused in Ready_state
        private void findDummyControl(Control control)
        {
            if (((string)control.Tag) == "dummyControl")
            {
                this.dummyControl = control;
            }
            foreach (Control ct in control.Controls)
            {
                this.findDummyControl(ct);
            }
        }

        public static void tabChangeHandle(Control root_control, TabPage tabpage)
        {
            FormState fs = new FormState(tabpage);
            fs.setControlRedyState(root_control);
        }
        #endregion manage dummyTextBox for throw Focused in Ready_state

        private void preventChangeTab(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void returnToFocused(object sender, EventArgs e)
        {
            //this.focusedControl.Focus();
        }

        private void allowChangeTab(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = false;
        }

        // prevent TextBox got focus on activated
        private void leaveWindow(object sender, EventArgs e)
        {
            this.dummyControl.Focus();
        }

        /// <summary>
        /// Set form state for FORM_STATE_READY, FORM_STATE_ADD, FORM_STATE_EDIT
        /// </summary>
        /// <param name="root_control">Root control, generally is the Form object</param>
        /// <param name="exclude_control">List of Control that exclude from state config</param>
        /// <param name="tool_strip">Toolstrip that contain ToolstripButton, ToolstripSplitButton, ....</param>
        /// <param name="disabled_button">List of Toolstrip items index for disabled</param>
        public static void Ready(Control root_control, TabPage tabpage, List<Control> exclude_control = null, ToolStrip tool_strip = null, List<int> disabled_button = null)
        {
            FormState fs = new FormState(tabpage);
            tabpage.Parent.Enabled = true;
            fs.setControlRedyState(root_control);
            fs.exclusionControl(FormState.FORM_STATE_READY, exclude_control);
            fs.setToolStripButtonState(tool_strip, disabled_button);
            ((TabControl)tabpage.Parent).Selecting += new TabControlCancelEventHandler(fs.allowChangeTab);
            root_control.Leave += new EventHandler(fs.leaveWindow);
        }

        public static void Edit(Control root_control, TabPage tabpage, List<Control> exclude_control = null, ToolStrip tool_strip = null, List<int> disabled_button = null)
        {
            FormState fs = new FormState(tabpage);
            fs.setControlAddEditState(root_control);
            fs.exclusionControl(FormState.FORM_STATE_EDIT, exclude_control);
            fs.setToolStripButtonState(tool_strip, disabled_button);
            ((TabControl)tabpage.Parent).Selecting += new TabControlCancelEventHandler(fs.preventChangeTab);
            ((TabControl)tabpage.Parent).Click += new EventHandler(fs.returnToFocused);
        }

        private void setControlRedyState(Control root_control)
        {
            if (root_control is TextBox)
            {
                TextBox tb = root_control as TextBox;
                tb.ReadOnly = true;
                tb.BackColor = Color.White;
                tb.ForeColor = Color.Black;
                tb.Cursor = Cursors.Default;
                tb.Enter += new EventHandler(this.textBoxHasFocus);
                tb.Leave += new EventHandler(this.textBoxLostFocus);

            }
            if (root_control is MaskedTextBox)
            {
                ((MaskedTextBox)root_control).ReadOnly = true;
                ((MaskedTextBox)root_control).BackColor = Color.White;
                ((MaskedTextBox)root_control).ForeColor = Color.Black;
                ((MaskedTextBox)root_control).Cursor = Cursors.Default;
                ((MaskedTextBox)root_control).Enter += new EventHandler(this.textBoxHasFocus);
                ((MaskedTextBox)root_control).Leave += new EventHandler(this.textBoxLostFocus);
            }
            if (root_control is ComboBox)
            {
                ComboBox cb = root_control as ComboBox;
                cb.Enabled = false;
            }
            if (root_control is DateTimePicker)
            {
                DateTimePicker dp = root_control as DateTimePicker;
                dp.Enabled = false;
                
            }
            foreach (Control ct in root_control.Controls)
            {
                this.setControlRedyState(ct);
            }
        }

        private void setControlAddEditState(Control root_control)
        {
            if (root_control is TextBox)
            {
                TextBox tb = root_control as TextBox;
                tb.ReadOnly = false;
                tb.BackColor = Color.White;
                tb.ForeColor = Color.Black;
                tb.Cursor = Cursors.IBeam;
                tb.Enter += new EventHandler(this.textBoxHasFocus);
                tb.Leave += new EventHandler(this.textBoxLostFocus);

            }
            if (root_control is MaskedTextBox)
            {
                ((MaskedTextBox)root_control).ReadOnly = false;
                ((MaskedTextBox)root_control).BackColor = Color.White;
                ((MaskedTextBox)root_control).ForeColor = Color.Black;
                ((MaskedTextBox)root_control).Cursor = Cursors.IBeam;
                ((MaskedTextBox)root_control).Enter += new EventHandler(this.textBoxHasFocus);
                ((MaskedTextBox)root_control).Leave += new EventHandler(this.textBoxLostFocus);
            }
            if (root_control is ComboBox)
            {
                ComboBox cb = root_control as ComboBox;
                cb.Enabled = true;
            }
            if (root_control is DateTimePicker)
            {
                DateTimePicker dp = root_control as DateTimePicker;
                dp.Enabled = true;

            }
            foreach (Control ct in root_control.Controls)
            {
                this.setControlAddEditState(ct);
            }
        }

        private void exclusionControl(string form_state, List<Control> exclude_control = null)
        {
            if (exclude_control != null)
            {
                switch (form_state)
                {
                    case FORM_STATE_READY:
                        foreach (Control ct in exclude_control)
                        {
                            ct.Enabled = true;
                            if (ct is TextBox)
                            {
                                TextBox tb = ct as TextBox;
                                tb.ReadOnly = false;
                            }
                        }
                        break;

                    case FORM_STATE_EDIT:
                        foreach (Control ct in exclude_control)
                        {
                            ct.Enabled = false;
                            if (ct is TextBox)
                            {
                                TextBox tb = ct as TextBox;
                                tb.ReadOnly = true;
                                tb.Enabled = true;
                            }
                        }
                        break;

                    case FORM_STATE_ADD:
                        foreach (Control ct in exclude_control)
                        {
                            ct.Enabled = false;
                            if (ct is TextBox)
                            {
                                TextBox tb = ct as TextBox;
                                tb.ReadOnly = true;
                                tb.Enabled = true;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private void setToolStripButtonState(ToolStrip tool_strip, List<int> disabled_button = null)
        {
            for (int i = 0; i < tool_strip.Items.Count; i++)
            {
                tool_strip.Items[i].Enabled = true;
            }
            if (disabled_button != null)
            {
                foreach (int item in disabled_button)
                {
                    tool_strip.Items[item].Enabled = false;
                }
            }
        }

        private void textBoxHasFocus(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox tb = sender as TextBox;
                if (!tb.ReadOnly)
                {
                    tb.BackColor = Color.LawnGreen;
                    this.focusedControl = tb;
                }
                else
                {
                    this.dummyControl.Focus();
                    tb.BackColor = Color.White;
                }
            }
            else if (sender is MaskedTextBox)
            {
                MaskedTextBox mt = sender as MaskedTextBox;
                if (!mt.ReadOnly)
                {
                    mt.BackColor = Color.LawnGreen;
                    this.focusedControl = mt;
                }
                else
                {
                    this.dummyControl.Focus();
                    mt.BackColor = Color.White;
                }
            }
            
        }

        private void textBoxLostFocus(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = Color.White;
            }
            else if(sender is MaskedTextBox)
            {
                ((MaskedTextBox)sender).BackColor = Color.White;
            }
        }
    }
}
