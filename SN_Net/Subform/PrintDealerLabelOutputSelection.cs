using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.Subform
{
    public partial class PrintDealerLabelOutputSelection : Form
    {
        private MainForm main_form;
        private Control current_focused_control;
        private List<Control> list_control = new List<Control>(); // store all input control
        public string dealer_from;
        public string dealer_to;
        public string condition;
        public OUTPUT output;
        public enum OUTPUT
        {
            SCREEN,
            PRINTER,
            FILE
        }

        public PrintDealerLabelOutputSelection()
        {
            InitializeComponent();
        }

        public PrintDealerLabelOutputSelection(MainForm main_form)
            : this()
        {
            this.main_form = main_form;
        }

        private void PrintDealerLabelOutputSelection_Shown(object sender, EventArgs e)
        {
            list_control.Add(this.txtFrom);
            list_control.Add(this.txtTo);
            list_control.Add(this.txtCond);
            list_control.Add(this.rbScreen);
            list_control.Add(this.rbPrinter);
            list_control.Add(this.rbFile);
            list_control.Add(this.btnOK);
            list_control.Add(this.btnCancel);
        
            this.KeepCurrentFocusedControl();
            this.rbScreen.Checked = true;
            this.txtFrom.Focus();
            this.current_focused_control = this.txtFrom;
        }

        private void KeepCurrentFocusedControl()
        {
            foreach (Control c in this.list_control)
            {
                c.GotFocus += delegate
                {
                    this.current_focused_control = c;
                    if (c is TextBox)
                    {
                        ((TextBox)c).SelectionStart = ((TextBox)c).Text.Length;
                    }
                };
            }
        }

        private void btnBrowseDealerFrom_Click(object sender, EventArgs e)
        {
            this.txtFrom.Focus();
            DealerList wind = new DealerList(new SnWindow(this.main_form), this.txtFrom.Text);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtFrom.Text = wind.dealer.dealer;
                this.txtFrom.SelectionStart = this.txtFrom.Text.Length;
            }
        }

        private void btnBrowseDealerTo_Click(object sender, EventArgs e)
        {
            this.txtTo.Focus();
            DealerList wind = new DealerList(new SnWindow(this.main_form), this.txtTo.Text);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtTo.Text = wind.dealer.dealer;
                this.txtTo.SelectionStart = this.txtTo.Text.Length;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (!(this.current_focused_control is Button))
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }
            if (keyData == Keys.F6)
            {
                if (this.current_focused_control == this.txtFrom)
                {
                    this.btnBrowseDealerFrom.PerformClick();
                    return true;
                }
                if (this.current_focused_control == this.txtTo)
                {
                    this.btnBrowseDealerTo.PerformClick();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.dealer_from = this.txtFrom.Text;
            this.dealer_to = this.txtTo.Text;
            this.condition = this.txtCond.Text;
            if (this.rbScreen.Checked)
            {
                this.output = OUTPUT.SCREEN;
            }
            else if (this.rbPrinter.Checked)
            {
                this.output = OUTPUT.PRINTER;
            }
            else if (this.rbFile.Checked)
            {
                this.output = OUTPUT.FILE;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
