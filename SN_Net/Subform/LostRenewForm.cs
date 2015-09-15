using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class LostRenewForm : Form
    {
        private GlobalVar G;
        private SnWindow parent_window;
        private Control current_focused_control;
        private Serial serial;
        private bool submit_result = false;

        public LostRenewForm()
        {
            InitializeComponent();
        }

        public LostRenewForm(SnWindow parent_window)
            : this()
        {
            this.parent_window = parent_window;
            this.G = this.parent_window.G;
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
        }

        private void LostRenewForm_Load(object sender, EventArgs e)
        {
            this.serial = this.parent_window.serial;
            this.mskLostSernum.Text = this.serial.sernum;
            //this.chkCDTraining.Checked = (this.serial.expdat.tryParseToDateTime() == false ? true : false);

            this.mskNewSernum.GotFocus += new EventHandler(this.keptCurrentFocusedControl);
            this.txtVersion.GotFocus += new EventHandler(this.keptCurrentFocusedControl);
            this.chkNewRwt.GotFocus += new EventHandler(this.keptCurrentFocusedControl);
            this.chkNewRwtJob.GotFocus += new EventHandler(this.keptCurrentFocusedControl);
            this.chkCDTraining.GotFocus += new EventHandler(this.keptCurrentFocusedControl);
            this.btnOK.GotFocus += new EventHandler(this.keptCurrentFocusedControl);
            this.btnCancel.GotFocus += new EventHandler(this.keptCurrentFocusedControl);

            this.mskNewSernum.GotFocus += new EventHandler(this.textBoxGotFocusHandler);
            this.txtVersion.GotFocus += new EventHandler(this.textBoxGotFocusHandler);
            this.mskNewSernum.Leave += new EventHandler(this.textBoxLeaveFocusHandler);
            this.txtVersion.Leave += new EventHandler(this.textBoxLeaveFocusHandler);

            this.mskNewSernum.Leave += new EventHandler(this.validateSernum);

            this.chkNewRwt.CheckStateChanged += new EventHandler(this.onCheckBoxStateChange);
            this.chkNewRwtJob.CheckStateChanged += new EventHandler(this.onCheckBoxStateChange);
        }

        private void LostRenewForm_Shown(object sender, EventArgs e)
        {
            this.mskNewSernum.Focus();
        }

        private void keptCurrentFocusedControl(object sender, EventArgs e)
        {
            this.current_focused_control = (Control)sender;
        }

        private void textBoxGotFocusHandler(object sender, EventArgs e)
        {
            if ((Control)sender is TextBox)
            {
                ((TextBox)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                ((TextBox)sender).ForeColor = Color.Black;
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
            if ((Control)sender is MaskedTextBox)
            {
                ((MaskedTextBox)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                ((MaskedTextBox)sender).ForeColor = Color.Black;
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void textBoxLeaveFocusHandler(object sender, EventArgs e)
        {
            if ((Control)sender is TextBox)
            {
                ((TextBox)sender).BackColor = Color.White;
                ((TextBox)sender).ForeColor = Color.Black;
            }
            if ((Control)sender is MaskedTextBox)
            {
                ((MaskedTextBox)sender).BackColor = Color.White;
                ((MaskedTextBox)sender).ForeColor = Color.Black;
            }
        }

        private void onCheckBoxStateChange(object sender, EventArgs e)
        {
            if ((CheckBox)sender == this.chkNewRwt)
            {
                if (((CheckBox)sender).Checked)
                {
                    this.chkNewRwtJob.CheckState = CheckState.Unchecked;
                }
            }
            if ((CheckBox)sender == this.chkNewRwtJob)
            {
                if (((CheckBox)sender).Checked)
                {
                    this.chkNewRwt.CheckState = CheckState.Unchecked;
                }
            }
        }

        private void validateSernum(object sender, EventArgs e)
        {
            if (ValidateSN.Check(((MaskedTextBox)sender).Text))
            {
                string version = this.mskNewSernum.Text.Substring(2, 1) + "." + this.mskNewSernum.Text.Substring(3, 1);
                this.txtVersion.Text = version;
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
                ((MaskedTextBox)sender).Focus();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (!(this.current_focused_control is Button))
                {
                    if (this.current_focused_control.Name == this.mskNewSernum.Name)
                    {
                        if (ValidateSN.Check(this.mskNewSernum.Text))
                        {
                            SendKeys.Send("{TAB}");
                            return true;
                        }
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                }
            }
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.formSaving();
            BackgroundWorker workerSubmit = new BackgroundWorker();
            workerSubmit.DoWork += new DoWorkEventHandler(this.workerSubmit_Dowork);
            workerSubmit.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerSubmit_Complete);
            workerSubmit.RunWorkerAsync();
        }

        private void workerSubmit_Dowork(object sender, DoWorkEventArgs e)
        {
            string json_data = "{\"id\":" + this.serial.id.ToString() + ",";
            json_data += "\"lost_sernum\":\"" + this.mskLostSernum.Text.cleanString() + "\",";
            json_data += "\"new_sernum\":\"" + this.mskNewSernum.Text.cleanString() + "\",";
            json_data += "\"version\":\"" + this.txtVersion.Text.cleanString() + "\",";
            json_data += "\"is_newrwt\":\"" + this.chkNewRwt.CheckState.ToYesOrNoString() + "\",";
            json_data += "\"is_newrwt_job\":\"" + this.chkNewRwtJob.CheckState.ToYesOrNoString() + "\",";
            json_data += "\"is_cdtraining\":\"" + this.chkCDTraining.CheckState.ToYesOrNoString() + "\",";
            json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";

            CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/lost_renew", json_data);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
            Console.WriteLine(sr.message);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.DialogResult = DialogResult.OK;
                this.submit_result = true;
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                this.submit_result = false;
            }
        }

        private void workerSubmit_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.submit_result)
            {
                this.Close();
            }
        }

        private void formSaving()
        {
            this.toolStripProcess.Visible = true;
            this.mskNewSernum.Enabled = false;
            this.txtVersion.Enabled = false;
            this.chkNewRwt.Enabled = false;
            this.chkNewRwtJob.Enabled = false;
            this.chkCDTraining.Enabled = false;
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
        }

        private void formReady()
        {
            this.toolStripProcess.Visible = false;
            this.mskNewSernum.Enabled = true;
            this.txtVersion.Enabled = true;
            this.chkNewRwt.Enabled = true;
            this.chkNewRwtJob.Enabled = true;
            this.chkCDTraining.Enabled = true;
            this.btnOK.Enabled = true;
            this.btnCancel.Enabled = true;
        }
    }
}
