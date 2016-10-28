using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using System.Threading;
using System.Globalization;
using WebAPI.ApiResult;
using SN_Net.MiscClass;
using WebAPI;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class CloudsrvFormDialog : Form
    {
        private SnWindow parent_window;
        public DateTime date_from;
        public DateTime date_to;
        public string email = "";
        //private CloudSrv current_cs = null;

        public CloudsrvFormDialog()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            InitializeComponent();
        }

        //public CloudsrvFormDialog(SnWindow parent_window)
        //    : this()
        //{
        //    this.parent_window = parent_window;
        //}

        public CloudsrvFormDialog(SnWindow parent_window, CloudSrv cs = null)
            : this()
        {
            this.parent_window = parent_window;
        }

        private void CloudsrvFormDialog_Load(object sender, EventArgs e)
        {
            this.cloudDateFrom.dateTimePicker1.ValueChanged += delegate
            {
                this.date_from = this.cloudDateFrom.dateTimePicker1.Value;
            };

            this.cloudDateTo.dateTimePicker1.ValueChanged += delegate
            {
                this.date_to = this.cloudDateTo.dateTimePicker1.Value;
            };

            this.cloudEmail.textBox1.TextChanged += delegate
            {
                this.email = this.cloudEmail.textBox1.Text;
            };

            if (this.parent_window.cloudsrv.Count > 0)
            {
                this.cloudDateFrom.TextsMysql = this.parent_window.cloudsrv.First().start_date;
                this.cloudDateTo.TextsMysql = this.parent_window.cloudsrv.First().end_date;
                this.cloudEmail.Texts = this.parent_window.cloudsrv.First().email;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cloudDateFrom.Texts.Replace("/", "").Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุวันที่เริ่ม", "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.cloudDateFrom.Focus();
                return;
            }

            if (this.cloudDateTo.Texts.Replace("/", "").Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุวันที่สิ้นสุด", "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.cloudDateTo.Focus();
                return;
            }

            if (this.cloudEmail.Texts.Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุอีเมล์", "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.cloudEmail.Focus();
                return;
            }

            this.SaveCloudSrv();
        }

        private void SaveCloudSrv()
        {
            bool post_success = false;
            string err_msg = "";
            this.cloudDateFrom.Read_Only = true;
            this.cloudDateTo.Read_Only = true;
            this.cloudEmail.Read_Only = true;

            string json_data = "{\"sernum\":\"" + this.parent_window.serial.sernum.cleanString() + "\",";
            json_data += "\"start_date\":\"" + this.date_from.ToMysqlDate() + "\",";
            json_data += "\"end_date\":\"" + this.date_to.ToMysqlDate() + "\",";
            json_data += "\"email\":\"" + this.email.cleanString() + "\",";
            json_data += "\"rec_by\":\"" + this.parent_window.main_form.G.loged_in_user_name + "\"}";

            Console.WriteLine(" ... > " + json_data);

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "cloudsrv/create_or_update", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
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
                        this.SaveCloudSrv();
                    }
                    this.cloudDateFrom.Read_Only = false;
                    this.cloudDateTo.Read_Only = false;
                    this.cloudEmail.Read_Only = false;
                }
            };
            worker.RunWorkerAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                if (!(this.btnOK.Focused || this.btnCancel.Focused))
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }

                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
