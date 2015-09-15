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
    public partial class UpgradeProgramForm : Form
    {
        private GlobalVar G;
        private SnWindow parent_window;
        private Control current_focused_control;
        private FORM_MODE form_mode;

        private enum FORM_MODE
        {
            EDIT,
            SAVING
        }

        public UpgradeProgramForm(SnWindow parent_window)
        {
            InitializeComponent();

            this.parent_window = parent_window;
            this.G = this.parent_window.G;
            this.form_mode = FORM_MODE.EDIT;
        }

        private void UpgradeProgramForm_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
            this.mskSernumFrom.Text = this.parent_window.serial.sernum;
            //this.chkCDTraining.CheckState = (this.parent_window.serial.expdat.tryParseToDateTime() ? CheckState.Unchecked : CheckState.Checked);

            this.mskSernumFrom.Enter += new EventHandler(this.onControlEnterHandler);
            this.mskSernumTo.Enter += new EventHandler(this.onControlEnterHandler);
            this.txtVersion.Enter += new EventHandler(this.onControlEnterHandler);

            this.mskSernumFrom.Leave += new EventHandler(this.onControlLeaveHandler);
            this.mskSernumTo.Leave += new EventHandler(this.onControlLeaveHandler);
            this.txtVersion.Leave += new EventHandler(this.onControlLeaveHandler);

            this.mskSernumTo.Leave += new EventHandler(this.validateSernumFieldLeave);

            this.mskSernumFrom.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.mskSernumTo.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.txtVersion.GotFocus += new EventHandler(this.onControlFocusedHandler);

            this.mskSernumFrom.GotFocus += new EventHandler(this.keepCurrentControlFocused);
            this.mskSernumTo.GotFocus += new EventHandler(this.keepCurrentControlFocused);
            this.txtVersion.GotFocus += new EventHandler(this.keepCurrentControlFocused);
            this.chkNewRwt.GotFocus += new EventHandler(this.keepCurrentControlFocused);
            this.chkNewRwtJob.GotFocus += new EventHandler(this.keepCurrentControlFocused);
            this.chkCDTraining.GotFocus += new EventHandler(this.keepCurrentControlFocused);
            this.btnOK.GotFocus += new EventHandler(this.keepCurrentControlFocused);
            this.btnCancel.GotFocus += new EventHandler(this.keepCurrentControlFocused);

            this.chkNewRwt.CheckStateChanged += new EventHandler(this.onCheckBoxStateChange);
            this.chkNewRwtJob.CheckStateChanged += new EventHandler(this.onCheckBoxStateChange);

            this.txtVersion.Leave += delegate
            {
                if (this.txtVersion.Text.Length == 0)
                {
                    if (ValidateSN.Check(this.mskSernumTo.Text))
                    {
                        this.txtVersion.Text = this.mskSernumTo.Text.Substring(2, 1) + "." + this.mskSernumTo.Text.Substring(3, 1);
                    }
                    else
                    {
                        this.txtVersion.Text = "";
                    }
                }
            };
        }

        private void keepCurrentControlFocused(object sender, EventArgs e)
        {
            this.current_focused_control = (Control)sender;
        }

        private void onControlEnterHandler(object sender, EventArgs e)
        {
            if (sender is TextBox || sender is MaskedTextBox)
            {
                ((Control)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                ((Control)sender).ForeColor = Color.Black;
            }
        }

        private void onControlFocusedHandler(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
            if (sender is MaskedTextBox)
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            if (sender is NumericUpDown)
            {
                ((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Text.Length);
            }
        }

        private void onControlLeaveHandler(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.White;
            ((Control)sender).ForeColor = Color.Black;
        }

        private void validateSernumFieldLeave(object sender, EventArgs e)
        {
            if (!ValidateSN.Check(((MaskedTextBox)sender).Text))
            {
                ((MaskedTextBox)sender).Focus();
                this.txtVersion.Text = "";
                this.btnOK.Enabled = false;
            }
            else
            {
                this.txtVersion.Text = ((MaskedTextBox)sender).Text.Substring(2, 1) + "." + ((MaskedTextBox)sender).Text.Substring(3, 1);
                this.btnOK.Enabled = true;
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

        private void formSaving()
        {
            this.form_mode = FORM_MODE.SAVING;
            this.mskSernumTo.Enabled = false;
            this.txtVersion.Enabled = false;
            this.chkCDTraining.Enabled = false;
            this.chkNewRwt.Enabled = false;
            this.chkNewRwtJob.Enabled = false;
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.toolStripProcess.Visible = true;
        }

        private void formEdit()
        {
            this.form_mode = FORM_MODE.EDIT;
            this.mskSernumTo.Enabled = true;
            this.txtVersion.Enabled = true;
            this.chkCDTraining.Enabled = true;
            this.chkNewRwt.Enabled = true;
            this.chkNewRwtJob.Enabled = true;
            this.btnOK.Enabled = true;
            this.btnCancel.Enabled = true;
            this.toolStripProcess.Visible = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.EDIT)
                {
                    if (!(this.current_focused_control is Button))
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                }
            }
            if (keyData == Keys.Escape)
            {
                if (this.form_mode == FORM_MODE.EDIT)
                {
                    this.btnCancel.PerformClick();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool post_success = false;
            this.formSaving();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"id\":" + this.parent_window.serial.id.ToString() + ",";
                json_data += "\"sernum_from\":\"" + this.mskSernumFrom.Text + "\",";
                json_data += "\"sernum_to\":\"" + this.mskSernumTo.Text + "\",";
                json_data += "\"version\":\"" + this.txtVersion.Text + "\",";
                json_data += "\"is_cdtraining\":\"" + this.chkCDTraining.CheckState.ToYesOrNoString() + "\",";
                json_data += "\"is_newrwt\":\"" + this.chkNewRwt.CheckState.ToYesOrNoString() + "\",";
                json_data += "\"is_newrwt_job\":\"" + this.chkNewRwtJob.CheckState.ToYesOrNoString() + "\",";
                json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/upgrade_program", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                    this.parent_window.serial = sr.serial[0];
                    this.parent_window.problem = sr.problem;
                    this.parent_window.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : new List<Problem>());
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    post_success = false;
                }
            };

            worker.RunWorkerCompleted += delegate
            {
                if (post_success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.formEdit();
                }
            };

            worker.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
