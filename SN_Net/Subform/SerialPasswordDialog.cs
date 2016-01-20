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
    public partial class SerialPasswordDialog : Form
    {
        private MainForm main_form;
        private SnWindow sn_wind;
        public int inserted_id;

        public SerialPasswordDialog(MainForm main_form, SnWindow sn_wind)
        {
            InitializeComponent();
            this.main_form = main_form;
            this.sn_wind = sn_wind;
        }

        private void SnPasswordDialog_Load(object sender, EventArgs e)
        {
            this.BindControlEventHandler();
        }

        private void SnPasswordDialog_Shown(object sender, EventArgs e)
        {
            this.txtPassword.Focus();
        }

        private void BindControlEventHandler()
        {
            this.btnOK.Click += delegate
            {
                this.SubmitPassword();
            };
        }

        private void SubmitPassword()
        {
            bool post_success = false;
            string err_msg = "";

            string json_data = "{\"sernum\":\"" + this.sn_wind.serial.sernum + "\",";
            json_data += "\"pass_word\":\"" + this.txtPassword.Texts.cleanString() + "\",";
            json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serialpassword/create", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                    this.inserted_id = (sr.serial_password.Count > 0 ? sr.serial_password[0].id : -1);
                }
                else
                {
                    post_success = false;
                    err_msg = sr.message;
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
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.SubmitPassword(); // if error occured and retry again
                        return;
                    }

                    this.btnCancel.PerformClick(); // if error occured and do not retry
                }
            };

            worker.RunWorkerAsync();

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.btnOK.Focused || this.btnCancel.Focused)
                    return false;

                SendKeys.Send("{TAB}");
                return true;
            }

            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
