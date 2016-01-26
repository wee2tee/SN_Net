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
using Newtonsoft.Json;
using WebAPI;
using WebAPI.ApiResult;
using System.Globalization;
using System.Threading;

namespace SN_Net.Subform
{
    public partial class MAFormDialog : Form
    {
        private SnWindow parent_window;
        public DateTime date_from;
        public DateTime date_to;
        public string email = "";
        private Ma Read_only_ma = null;

        public MAFormDialog()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            InitializeComponent();
        }

        public MAFormDialog(SnWindow parent_window)
            : this()
        {
            this.parent_window = parent_window;
        }

        public MAFormDialog(Ma ma_readonly)
            : this()
        {
            this.Read_only_ma = new Ma()
            {
                id = ma_readonly.id,
                email = ma_readonly.email,
                start_date = ma_readonly.start_date,
                end_date = ma_readonly.end_date,
                sernum = ma_readonly.sernum,
                rec_by = ma_readonly.rec_by,
                rec_date = ma_readonly.rec_date,
            };

        }

        private void MAFormDialog_Load(object sender, EventArgs e)
        {
            this.BindingControlEvent();
        }

        private void MAFormDialog_Shown(object sender, EventArgs e)
        {
            if (this.parent_window != null && this.parent_window.ma.Count > 0)
            {
                this.maDateFrom.TextsMysql = this.parent_window.ma[0].start_date;
                this.maDateTo.TextsMysql = this.parent_window.ma[0].end_date;
                this.maEmail.Texts = this.parent_window.ma[0].email;
            }

            if (this.Read_only_ma != null)
            {
                this.maDateFrom.TextsMysql = this.Read_only_ma.start_date;
                this.maDateTo.TextsMysql = this.Read_only_ma.end_date;
                this.maEmail.Texts = this.Read_only_ma.email;
                this.btnCancel.Focus();
            }
        }

        private void BindingControlEvent()
        {
            this.maDateFrom.dateTimePicker1.ValueChanged += delegate
            {
                this.date_from = this.maDateFrom.dateTimePicker1.Value;
            };

            this.maDateTo.dateTimePicker1.ValueChanged += delegate
            {
                this.date_to = this.maDateTo.dateTimePicker1.Value;
            };

            this.maEmail.textBox1.TextChanged += delegate
            {
                this.email = this.maEmail.textBox1.Text;
            };
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.maDateFrom.Texts.Replace("/", "").Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุวันที่เริ่ม", "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.maDateFrom.Focus();
                return;
            }

            if (this.maDateTo.Texts.Replace("/", "").Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุวันที่สิ้นสุด", "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.maDateTo.Focus();
                return;
            }

            if (this.maEmail.Texts.Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุอีเมล์", "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.maEmail.Focus();
                return;
            }

            this.SaveMA();
        }

        private void SaveMA()
        {
            bool post_success = false;
            string err_msg = "";
            this.maDateFrom.Read_Only = true;
            this.maDateTo.Read_Only = true;
            this.maEmail.Read_Only = true;

            string json_data = "{\"sernum\":\"" + this.parent_window.serial.sernum.cleanString() + "\",";
            json_data += "\"start_date\":\"" + this.date_from.ToMysqlDate() + "\",";
            json_data += "\"end_date\":\"" + this.date_to.ToMysqlDate() + "\",";
            json_data += "\"email\":\"" + this.email.cleanString() + "\",";
            json_data += "\"rec_by\":\"" + this.parent_window.main_form.G.loged_in_user_name + "\"}";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "ma/create_or_update", json_data);
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
                        this.SaveMA();
                    }
                    this.maDateFrom.Read_Only = false;
                    this.maDateTo.Read_Only = false;
                    this.maEmail.Read_Only = false;
                }
            };
            worker.RunWorkerAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (!(this.btnOK.Focused || this.btnCancel.Focused))
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

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
