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
    public partial class SellProgram2nd : Form
    {
        private SnWindow parent_window;
        private GlobalVar G;
        private Control current_focused_control;
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            EDIT,
            SAVING
        }
        private List<Dealer> dealers = new List<Dealer>();

        public SellProgram2nd(SnWindow parent_window)
        {
            InitializeComponent();

            this.parent_window = parent_window;
            this.G = this.parent_window.G;

            this.getDealer();
        }

        private void SellProgram2nd_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
            this.form_mode = FORM_MODE.EDIT;
            this.lblDealer_Compnam.Text = "";
            this.mskOldSernum.Text = this.parent_window.serial.sernum;
            this.msk2Sernum.Focus();

            this.msk2Sernum.Leave += new EventHandler(this.validateSernumFieldLeave);

            this.msk2Sernum.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.txtVersion.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.txtDealer.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.btnOK.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.btnCancel.GotFocus += new EventHandler(this.onControlFocusedHandler);

            this.msk2Sernum.Enter += new EventHandler(this.onControlEnterHandler);
            this.txtVersion.Enter += new EventHandler(this.onControlEnterHandler);
            this.txtDealer.Enter += new EventHandler(this.onControlEnterHandler);

            this.msk2Sernum.Leave += new EventHandler(this.onControlLeaveHandler);
            this.txtVersion.Leave += new EventHandler(this.onControlLeaveHandler);
            this.txtDealer.Leave += new EventHandler(this.onControlLeaveHandler);

            this.txtVersion.Leave += delegate
            {
                if (this.txtVersion.Text.Length == 0)
                {
                    if (ValidateSN.Check(this.msk2Sernum.Text))
                    {
                        this.txtVersion.Text = this.msk2Sernum.Text.Substring(2, 1) + "." + this.msk2Sernum.Text.Substring(3, 1);
                    }
                    else
                    {
                        this.txtVersion.Text = "";
                    }
                }
            };

            this.txtDealer.Leave += new EventHandler(this.validateDealerField);
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

        private void validateDealerField(object sender, EventArgs e)
        {
            string str_dealer = this.txtDealer.Text;

            if (str_dealer.Length > 0)
            {
                Dealer d = this.dealers.Find(t => t.dealer == str_dealer);

                if (d != null)
                {
                    this.lblDealer_Compnam.Text = d.compnam;
                }
                else
                {
                    this.txtDealer.Focus();
                    SendKeys.Send("{F6}");
                }
            }
            else
            {
                this.lblDealer_Compnam.Text = "";
            }
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
            this.current_focused_control = (Control)sender;

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

        private void formEdit()
        {
            this.form_mode = FORM_MODE.EDIT;
            this.msk2Sernum.Enabled = true;
            this.txtVersion.Enabled = true;
            this.txtDealer.Enabled = true;
            this.btnBrowseDealer.Enabled = true;
            this.btnOK.Enabled = true;
            this.btnCancel.Enabled = true;
            this.toolStripProcess.Visible = false;
        }

        private void formSaving()
        {
            this.form_mode = FORM_MODE.SAVING;
            this.msk2Sernum.Enabled = false;
            this.txtVersion.Enabled = false;
            this.txtDealer.Enabled = false;
            this.btnBrowseDealer.Enabled = false;
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.toolStripProcess.Visible = true;
        }

        private void btnBrowseDealer_Click(object sender, EventArgs e)
        {
            DealerList wind = new DealerList(this.parent_window, this.txtDealer.Text);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtDealer.Text = wind.dealer.dealer;
                this.lblDealer_Compnam.Text = wind.dealer.compnam;
                SendKeys.Send("{TAB}");
            }
            else
            {
                this.txtDealer.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.formSaving();
            bool post_success = false;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"id\":" + this.parent_window.serial.id.ToString() + ",";
                json_data += "\"sernum_old\":\"" + this.mskOldSernum.Text + "\",";
                json_data += "\"sernum_2\":\"" + this.msk2Sernum.Text + "\",";
                json_data += "\"version\":\"" + this.txtVersion.Text + "\",";
                json_data += "\"dealer_dealer\":\"" + this.txtDealer.Text + "\",";
                json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/sell_program_2nd", json_data);
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
                    this.parent_window.problem = (sr.problem.Count > 0 ? sr.problem : new List<Problem>());
                    this.parent_window.problem_im_only = (sr.problem.Count > 0 ? this.parent_window.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : new List<Problem>());
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.form_mode == FORM_MODE.EDIT)
            {
                if (keyData == Keys.F6 && this.current_focused_control == this.txtDealer)
                {
                    this.btnBrowseDealer.PerformClick();
                }
                if (keyData == Keys.Escape)
                {
                    this.btnCancel.PerformClick();
                }
                if (keyData == Keys.Enter && !(this.current_focused_control is Button))
                {
                    SendKeys.Send("{TAB}");
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
