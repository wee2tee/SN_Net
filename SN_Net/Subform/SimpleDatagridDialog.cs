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
    public partial class SimpleDatagridDialog : Form
    {
        public BUTTON_MODE button_mode { get; set; }
        public enum BUTTON_MODE
        {
            OK_CANCEL,
            CLOSE
        }

        public SimpleDatagridDialog(BUTTON_MODE button_mode)
        {
            InitializeComponent();
            this.button_mode = button_mode;
        }

        private void SimpleDatagridDialog_Load(object sender, EventArgs e)
        {
            this.SetVisualControl();
        }

        private void SetVisualControl()
        {
            switch (this.button_mode)
            {
                case BUTTON_MODE.OK_CANCEL:
                    this.btnOK.Visible = true;
                    this.btnCancel.Visible = true;
                    this.btnClose.Visible = false;
                    break;
                case BUTTON_MODE.CLOSE:
                    this.btnOK.Visible = false;
                    this.btnCancel.Visible = false;
                    this.btnClose.Visible = true;
                    break;
                default:
                    this.btnOK.Visible = true;
                    this.btnCancel.Visible = true;
                    this.btnClose.Visible = false;
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                switch (this.button_mode)
                {
                    case BUTTON_MODE.OK_CANCEL:
                        this.btnOK.PerformClick();
                        return true;
                    case BUTTON_MODE.CLOSE:
                        return false;
                    default:
                        this.btnOK.PerformClick();
                        return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                switch (this.button_mode)
                {
                    case BUTTON_MODE.OK_CANCEL:
                        this.btnCancel.PerformClick();
                        return true;
                    case BUTTON_MODE.CLOSE:
                        this.btnClose.PerformClick();
                        return true;
                    default:
                        this.btnCancel.PerformClick();
                        return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
