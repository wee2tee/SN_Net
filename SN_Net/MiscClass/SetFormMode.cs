using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace SN_Net.MiscClass
{
    public class SetFormMode
    {
        private Form form;
        private List<Control> list_controls;
        private List<Control> list_controls_disabled;
        private Control dummy_control;
        private Control focused_control;
        private Control current_control_focused;
        public MODE mode;
        public enum MODE
        {
            READY,
            ADD,
            EDIT
        }

        private SetFormMode(Form form, List<Control> controls, Control dummy_control, MODE mode)
        {
            if (mode == MODE.READY)
            {
                this.mode = mode;
                this.form = form;
                this.list_controls = controls;
                this.dummy_control = dummy_control;
                this.manageControl();
            }
        }

        private SetFormMode(Form form, List<Control> control_enabled, List<Control> control_disabled, Control focused_control, MODE mode)
        {
            if (mode == MODE.EDIT)
            {
                this.mode = mode;
                this.form = form;
                this.list_controls = control_enabled;
                this.list_controls_disabled = control_disabled;
                this.focused_control = focused_control;
                this.manageControl();
            }
            else if (mode == MODE.ADD)
            {
                this.mode = mode;
                this.form = form;
                this.list_controls = control_enabled;
                this.list_controls_disabled = control_disabled;
                this.focused_control = focused_control;
                this.manageControl();
            }
        }

        public static void Ready(Form form, List<Control> controls, Control dummy_control)
        {
            SetFormMode sfm = new SetFormMode(form, controls, dummy_control, MODE.READY);
        }

        public static void Add(Form form, List<Control> controls_enable, List<Control> controls_disabled, Control dummy_control)
        {
            SetFormMode sfm = new SetFormMode(form, controls_enable, controls_disabled, dummy_control, MODE.ADD);
        }

        public static void Edit(Form form, List<Control> controls_enabled, List<Control> controls_disabled, Control focused_control)
        {
            SetFormMode sfm = new SetFormMode(form, controls_enabled, controls_disabled, focused_control, MODE.EDIT);
        }

        private void manageControl()
        {
            switch (this.mode)
            {
                case MODE.READY:
                    Console.WriteLine("start in ready mode at " + DateTime.Now.ToString());
                    this.dummy_control.SetBounds(0, 0, 0, 0);
                    foreach (Control ct in this.list_controls)
                    {
                        ct.Cursor = Cursors.Default;

                        if (ct is TextBox)
                        {
                            ((TextBox)ct).Enabled = true;
                            ((TextBox)ct).ReadOnly = true;
                            ((TextBox)ct).BackColor = Color.White;
                        }
                        else if (ct is MaskedTextBox)
                        {
                            ((MaskedTextBox)ct).Enabled = true;
                            ((MaskedTextBox)ct).ReadOnly = true;
                            ((MaskedTextBox)ct).BackColor = Color.White;
                        }
                        else
                        {
                            ct.Enabled = false;
                        }
                        ct.GotFocus += new EventHandler(this.ControlFocusedHandler);
                    }
                    break;
                case MODE.ADD:
                    Console.WriteLine("become in add mode at " + DateTime.Now.ToString());
                    foreach (Control ct in this.list_controls)
                    {
                        if (ct is TextBox)
                        {
                            ct.Cursor = Cursors.IBeam;
                            ((TextBox)ct).Enabled = true;
                            ((TextBox)ct).ReadOnly = false;
                        }
                        else if (ct is MaskedTextBox)
                        {
                            ct.Cursor = Cursors.IBeam;
                            ((MaskedTextBox)ct).Enabled = true;
                            ((MaskedTextBox)ct).ReadOnly = false;
                        }
                        else
                        {
                            ct.Cursor = Cursors.Default;
                            ct.Enabled = true;
                        }
                        ct.GotFocus += new EventHandler(this.ControlFocusedHandler);
                        ct.Leave += new EventHandler(this.ControlLeaveHandler);
                    }
                    foreach (Control ct in this.list_controls_disabled)
                    {
                        ct.Enabled = false;
                    }
                    this.focused_control.Focus();
                    break;
                case MODE.EDIT:
                    Console.WriteLine("become in edit mode at " + DateTime.Now.ToString());
                    foreach (Control ct in this.list_controls)
                    {
                        if (ct is TextBox)
                        {
                            ct.Cursor = Cursors.IBeam;
                            ((TextBox)ct).Enabled = true;
                            ((TextBox)ct).ReadOnly = false;
                        }
                        else if (ct is MaskedTextBox)
                        {
                            ct.Cursor = Cursors.IBeam;
                            ((MaskedTextBox)ct).Enabled = true;
                            ((MaskedTextBox)ct).ReadOnly = false;
                        }
                        else
                        {
                            ct.Cursor = Cursors.Default;
                            ct.Enabled = true;
                        }
                        ct.GotFocus += new EventHandler(this.ControlFocusedHandler);
                        ct.Leave += new EventHandler(this.ControlLeaveHandler);
                    }
                    foreach (Control ct in this.list_controls_disabled)
                    {
                        ct.Enabled = false;
                    }
                    this.focused_control.Focus();
                    break;
                default:
                    break;
            }
        }

        private void ControlFocusedHandler(object sender, EventArgs e)
        {
            if (this.mode == MODE.READY)
            {
                this.dummy_control.Focus();
            }
            else
            {
                ((Control)sender).Focus();
                ((Control)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                if (sender is TextBox)
                {
                    ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
                }
                this.current_control_focused = ((Control)sender);
            }
        }

        private void ControlLeaveHandler(object sender, EventArgs e)
        {
            if (this.mode == MODE.ADD || this.mode == MODE.EDIT)
            {
                ((Control)sender).BackColor = Color.White;
            }
        }
    }
}
