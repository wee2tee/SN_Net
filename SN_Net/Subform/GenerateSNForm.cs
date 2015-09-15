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
    public partial class GenerateSNForm : Form
    {
        private List<Dealer> dealers = new List<Dealer>();
        private SnWindow parent_window;
        private GlobalVar G;
        private Control current_focused_control;
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            EDIT,
            SAVING
        }

        public GenerateSNForm(SnWindow parent_window)
        {
            InitializeComponent();
            this.parent_window = parent_window;
            this.G = this.parent_window.G;
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
            //this.chkCDTraining.CheckState = CheckState.Checked;

            this.mskSernum.Enter += new EventHandler(this.onControlEnterHandler);
            this.numQty.Enter += new EventHandler(this.onControlEnterHandler);
            this.txtVersion.Enter += new EventHandler(this.onControlEnterHandler);
            this.txtDealer.Enter += new EventHandler(this.onControlEnterHandler);

            this.txtVersion.Leave += new EventHandler(this.onControlLeaveHandler);
            this.numQty.Leave += new EventHandler(this.onControlLeaveHandler);
            this.mskSernum.Leave += new EventHandler(this.onControlLeaveHandler);
            this.txtDealer.Leave += new EventHandler(this.onControlLeaveHandler);

            this.mskSernum.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.numQty.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.txtVersion.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.txtDealer.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.chkCDTraining.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.chkNewRwt.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.chkNewRwtJob.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.btnOK.GotFocus += new EventHandler(this.onControlFocusedHandler); ;
            this.btnCancel.GotFocus += new EventHandler(this.onControlFocusedHandler);

            this.mskSernum.Leave += new EventHandler(this.validateSernumFieldLeave);
            this.txtDealer.Leave += new EventHandler(this.validateDealerFieldLeave);

            this.chkNewRwt.CheckedChanged += new EventHandler(this.onCheckBoxStateChange);
            this.chkNewRwtJob.CheckedChanged += new EventHandler(this.onCheckBoxStateChange);

            this.getDealer();

            this.numQty.Leave += delegate
            {
                if (this.numQty.Text.Length == 0)
                {
                    this.numQty.Text = "1";
                }
            };
            this.txtVersion.Leave += delegate
            {
                if (this.txtVersion.Text.Length == 0)
                {
                    if (ValidateSN.Check(this.mskSernum.Text))
                    {
                        this.txtVersion.Text = this.mskSernum.Text.Substring(2, 1) + "." + this.mskSernum.Text.Substring(3, 1);
                    }
                }
            };

            this.form_mode = FORM_MODE.EDIT;
        }

        private void getDealer()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "dealer/get_list");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                if (sr.dealer != null)
                {
                    this.dealers = sr.dealer;
                }
            }
        }

        private void GenerateSNForm_Shown(object sender, EventArgs e)
        {
            this.mskSernum.Focus();
            this.lblDealer_Compnam.Text = "";
        }

        private void onControlEnterHandler(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
            ((Control)sender).ForeColor = Color.Black;
        }

        private void onControlFocusedHandler(object sender, EventArgs e)
        {
            this.current_focused_control = (Control)sender;

            if (sender is TextBox)
            {
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
            if (sender is MaskedTextBox)
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
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
            }
            else
            {
                this.txtVersion.Text = ((MaskedTextBox)sender).Text.Substring(2, 1) + "." + ((MaskedTextBox)sender).Text.Substring(3, 1);
            }
        }

        private void validateDealerFieldLeave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length > 0)
            {
                string dealer = ((TextBox)sender).Text;
                int ndx = this.dealers.FindIndex( t => t.dealer == dealer);
                if (ndx >= 0)  // found match dealer
                {
                    this.lblDealer_Compnam.Text = this.dealers[ndx].compnam;
                }
                else  // not found
                {
                    ((TextBox)sender).Focus();
                    SendKeys.Send("{F6}");
                }
            }
            else
            {
                this.lblDealer_Compnam.Text = "";
            }
        }

        private void btnBrowseDealer_Click(object sender, EventArgs e)
        {
            this.txtDealer.Focus();
            SendKeys.Send("{F6}");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool post_success = false;
            this.formSaving();

            BackgroundWorker workerSave = new BackgroundWorker();
            workerSave.DoWork += delegate
            {
                string json_data = "{\"sernum\":\"" + this.mskSernum.Text + "\",";
                json_data += "\"qty\":" + this.numQty.Value.ToString() + ",";
                json_data += "\"version\":\"" + this.txtVersion.Text + "\",";
                json_data += "\"dealer\":\"" + this.txtDealer.Text + "\",";
                json_data += "\"is_newrwt\":\"" + this.chkNewRwt.CheckState.ToYesOrNoString() + "\",";
                json_data += "\"is_newrwtjob\":\"" + this.chkNewRwtJob.CheckState.ToYesOrNoString() + "\",";
                json_data += "\"is_cdtraining\":\"" + this.chkCDTraining.CheckState.ToYesOrNoString() + "\",";
                json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";


                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/generate_sn", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                    this.parent_window.serial = sr.serial[0];
                    this.parent_window.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : new Istab());
                    this.parent_window.area = (sr.area.Count > 0 ? sr.area[0] : new Istab());
                    this.parent_window.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : new Istab());
                    this.parent_window.verext = (sr.verext.Count > 0 ? sr.verext[0] : new Istab());
                    this.parent_window.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : new Dealer());
                    this.parent_window.problem = new List<Problem>();
                    this.parent_window.problem_im_only = new List<Problem>();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    post_success = false;
                }
            };

            workerSave.RunWorkerCompleted += delegate
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

            workerSave.RunWorkerAsync();
        }

        private void formSaving()
        {
            this.form_mode = FORM_MODE.SAVING;
            this.mskSernum.Enabled = false;
            this.numQty.Enabled = false;
            this.txtVersion.Enabled = false;
            this.txtDealer.Enabled = false;
            this.chkCDTraining.Enabled = false;
            this.chkNewRwt.Enabled = false;
            this.chkNewRwtJob.Enabled = false;
            this.btnBrowseDealer.Enabled = false;
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.toolStripProcess.Visible = true;
        }

        private void formEdit()
        {
            this.form_mode = FORM_MODE.EDIT;
            this.mskSernum.Enabled = true;
            this.numQty.Enabled = true;
            this.txtVersion.Enabled = true;
            this.txtDealer.Enabled = true;
            this.chkCDTraining.Enabled = true;
            this.chkNewRwt.Enabled = true;
            this.chkNewRwtJob.Enabled = true;
            this.btnBrowseDealer.Enabled = true;
            this.btnOK.Enabled = true;
            this.btnCancel.Enabled = true;
            this.toolStripProcess.Visible = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (!(this.ActiveControl is Button))
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
            if (keyData == Keys.F6 && this.form_mode == FORM_MODE.EDIT && this.current_focused_control == this.txtDealer)
            {
                DealerList wind = new DealerList(this.parent_window, this.txtDealer.Text);
                if (wind.ShowDialog() == DialogResult.OK)
                {
                    this.txtDealer.Text = wind.dealer.dealer;
                    this.lblDealer_Compnam.Text = wind.dealer.compnam;
                    SendKeys.Send("{TAB}");
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
