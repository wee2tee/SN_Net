using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class LeaveRange : Form
    {
        private MainForm main_form;
        private List<Users> list_users = new List<Users>();
        private List<Istab> list_reason = new List<Istab>();
        private List<ComboboxItem> list_medcert = new List<ComboboxItem>();
        private List<ComboboxItem> list_status = new List<ComboboxItem>();
        private CultureInfo cinfo_th = new CultureInfo("th-TH");
        private CultureInfo cinfo_en = new CultureInfo("en-US");
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            EDITING,
            PROCESSING
        }

        public LeaveRange(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void LeaveRange_Load(object sender, EventArgs e)
        {
            this.BindControlEventHandler();
            this.LoadDependenciesData();
            this.InitControlData();
        }

        private void LeaveRange_Shown(object sender, EventArgs e)
        {
            this.FormEditing();
            this.cbUsers.Focus();
            this.chIsFine.CheckState = CheckState.Unchecked;
        }

        private void BindControlEventHandler()
        {
            this.chIsFine.CheckedChanged += delegate
            {
                if (this.chIsFine.CheckState == CheckState.Checked)
                {
                    this.chMonday.Enabled = true;
                    this.chTuesday.Enabled = true;
                    this.chWednesday.Enabled = true;
                    this.chThursday.Enabled = true;
                    this.chFriday.Enabled = true;
                    this.chSaturday.Enabled = true;
                    this.numFine.Enabled = true;
                }
                else
                {
                    this.chMonday.Enabled = false;
                    this.chTuesday.Enabled = false;
                    this.chWednesday.Enabled = false;
                    this.chThursday.Enabled = false;
                    this.chFriday.Enabled = false;
                    this.chSaturday.Enabled = false;
                    this.numFine.Enabled = false;
                }
            };

            this.cbUsers.Leave += delegate
            {
                if (this.cbUsers.Items.Cast<ComboboxItem>().Where(i => i.name.Length >= this.cbUsers.Text.Length).Where(i => i.name.Substring(0, this.cbUsers.Text.Length) == this.cbUsers.Text).Count<ComboboxItem>() > 0)
                {
                    this.cbUsers.SelectedItem = this.cbUsers.Items.Cast<ComboboxItem>().Where(i => i.name.Length >= this.cbUsers.Text.Length).Where(i => i.name.Substring(0, this.cbUsers.Text.Length) == this.cbUsers.Text).First<ComboboxItem>();
                }
                else
                {
                    this.cbUsers.Focus();
                    SendKeys.Send("{F6}");
                }
            };
        }

        private void LoadDependenciesData()
        {
            #region Users list
            CRUDResult get_user = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_all");
            ServerResult sr_user = JsonConvert.DeserializeObject<ServerResult>(get_user.data);

            if (sr_user.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.list_users = sr_user.users;
            }
            #endregion Users list

            #region Leave reason
            CRUDResult get_leave_cause = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_leave_cause");
            ServerResult sr_leave_cause = JsonConvert.DeserializeObject<ServerResult>(get_leave_cause.data);

            if (sr_leave_cause.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.list_reason = sr_leave_cause.istab;
            }
            #endregion Leave reason

            #region Medical Certificate
            this.list_medcert.Add(new ComboboxItem("N/A (ไม่ระบุ)", 9, "X"));
            this.list_medcert.Add(new ComboboxItem("ไม่มีใบรับรองแพทย์", 0, "N"));
            this.list_medcert.Add(new ComboboxItem("มีใบรับรองแพทย์", 1, "Y"));
            #endregion Medical Certificate

            #region Status
            this.list_status.Add(new ComboboxItem("WAIT", (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM, "WAIT"));
            this.list_status.Add(new ComboboxItem("CONFIRMED", (int)CustomDateEvent.EVENT_STATUS.CONFIRMED, "CONFIRMED"));
            this.list_status.Add(new ComboboxItem("CANCELED", (int)CustomDateEvent.EVENT_STATUS.CANCELED, "CANCELED"));
            #endregion Status
        }

        private void InitControlData()
        {
            #region Fill cbReason
            foreach (Istab i in this.list_reason)
            {
                this.cbReason.Items.Add(new ComboboxItem(i.typdes_th, i.id, i.typcod) { Tag = i });
            }
            #endregion Fill cbReason

            #region Fill cbUsers
            foreach (Users u in this.list_users)
            {
                this.cbUsers.Items.Add(new ComboboxItem(u.username + " : " + u.name, u.id, u.username) { Tag = u });
            }
            #endregion Fill cbUsers
            
            this.cbMedCert.DataSource = this.list_medcert;
            this.cbStatus.DataSource = this.list_status;
            this.cbStatus.SelectedIndex = 1;

            this.dtDateStart.Value = DateTime.Now;
            this.dtDateEnd.Value = DateTime.Now;
            this.dtFromTime.Time = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 8:30:00", cinfo_en.DateTimeFormat, DateTimeStyles.None);
            this.dtToTime.Time = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 17:30:00", cinfo_en.DateTimeFormat, DateTimeStyles.None);

            this.chMonday.Checked = false;
            this.chTuesday.Checked = false;
            this.chWednesday.Checked = false;
            this.chThursday.Checked = false;
            this.chFriday.Checked = false;
            this.chSaturday.Checked = false;

            this.numFine.Value = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cbUsers.SelectedItem == null)
            {
                this.cbUsers.Focus();
                SendKeys.Send("{F6}");
                return;
            }
            if (this.cbReason.SelectedItem == null)
            {
                this.cbReason.Focus();
                SendKeys.Send("{F6}");
                return;
            }
            if (this.cbMedCert.SelectedItem == null)
            {
                this.cbMedCert.Focus();
                SendKeys.Send("{F6}");
                return;
            }

            if (MessageAlert.Show("ยืนยันการบันทึกข้อมูล", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                this.SubmitEventData();
            }
        }

        private void SubmitEventData()
        {
            this.FormProcessing();

            string users_name = ((Users)((ComboboxItem)this.cbUsers.SelectedItem).Tag).username;
            string event_type = ((Istab)((ComboboxItem)this.cbReason.SelectedItem).Tag).tabtyp;
            string event_code = ((Istab)((ComboboxItem)this.cbReason.SelectedItem).Tag).typcod;
            string from_date = this.dtDateStart.Value.ToMysqlDate();
            string to_date = this.dtDateEnd.Value.ToMysqlDate();
            string from_time = this.dtFromTime.Time.ToString("HH:mm", cinfo_th);
            string to_time = this.dtToTime.Time.ToString("HH:mm", cinfo_th);
            string med_cert = ((ComboboxItem)this.cbMedCert.SelectedItem).string_value;
            int status = ((ComboboxItem)this.cbStatus.SelectedItem).int_value;
            string customer = this.txtCustomer.Text.cleanString();
            string is_fine = this.chIsFine.CheckState.ToYesOrNoString();
            string fine_monday = this.chMonday.CheckState.ToYesOrNoString();
            string fine_tuesday = this.chTuesday.CheckState.ToYesOrNoString();
            string fine_wednesday = this.chWednesday.CheckState.ToYesOrNoString();
            string fine_thursday = this.chThursday.CheckState.ToYesOrNoString();
            string fine_friday = this.chFriday.CheckState.ToYesOrNoString();
            string fine_saturday = this.chSaturday.CheckState.ToYesOrNoString();
            int fine = (int)this.numFine.Value;

            string json_data = "{\"users_name\":\"" + users_name + "\",";
            json_data += "\"event_type\":\"" + event_type + "\",";
            json_data += "\"event_code\":\"" + event_code + "\",";
            json_data += "\"from_date\":\"" + from_date + "\",";
            json_data += "\"to_date\":\"" + to_date + "\",";
            json_data += "\"from_time\":\"" + from_time + "\",";
            json_data += "\"to_time\":\"" + to_time + "\",";
            json_data += "\"med_cert\":\"" + med_cert + "\",";
            json_data += "\"status\":" + status.ToString() + ",";
            json_data += "\"customer\":\"" + customer + "\",";
            json_data += "\"is_fine\":\"" + is_fine + "\",";
            json_data += "\"fine_days\":\"" + this.GetFineDays() + "\",";
            json_data += "\"fine\":" + fine.ToString() + ",";
            json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

            bool post_success = false;
            string err_msg = "";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "eventcalendar/create_range", json_data);
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
                        this.SubmitEventData();
                    }
                    else
                    {
                        this.FormEditing();
                    }
                }
            };
            worker.RunWorkerAsync();
        }

        private string GetFineDays()
        {
            string fine_days = "";

            if (this.chMonday.Checked)
            {
                fine_days += (fine_days.Length == 0 ? "1" : ",1");
            }
            if (this.chTuesday.Checked)
            {
                fine_days += (fine_days.Length == 0 ? "2" : ",2");
            }
            if (this.chWednesday.Checked)
            {
                fine_days += (fine_days.Length == 0 ? "3" : ",3");
            }
            if (this.chThursday.Checked)
            {
                fine_days += (fine_days.Length == 0 ? "4" : ",4");
            }
            if (this.chFriday.Checked)
            {
                fine_days += (fine_days.Length == 0 ? "5" : ",5");
            }
            if (this.chSaturday.Checked)
            {
                fine_days += (fine_days.Length == 0 ? "6" : ",6");
            }

            return fine_days;
        }

        private void FormEditing()
        {
            this.form_mode = FORM_MODE.EDITING;
            this.toolStripProcessing.Visible = false;

            this.cbUsers.Enabled = true;
            this.cbReason.Enabled = true;
            this.cbMedCert.Enabled = true;
            this.dtDateStart.Enabled = true;
            this.dtDateEnd.Enabled = true;
            this.dtFromTime.Enabled = true;
            this.dtToTime.Enabled = true;
            this.cbStatus.Enabled = true;
            this.txtCustomer.Enabled = true;
            this.btnOK.Enabled = true;
            this.btnCancel.Enabled = true;
            this.chIsFine.Enabled = true;

            this.chMonday.Enabled = this.chIsFine.Checked;
            this.chTuesday.Enabled = this.chIsFine.Checked;
            this.chWednesday.Enabled = this.chIsFine.Checked;
            this.chThursday.Enabled = this.chIsFine.Checked;
            this.chFriday.Enabled = this.chIsFine.Checked;
            this.chSaturday.Enabled = this.chIsFine.Checked;
            this.numFine.Enabled = this.chIsFine.Checked;
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;
            this.toolStripProcessing.Visible = true;

            this.cbUsers.Enabled = false;
            this.cbReason.Enabled = false;
            this.cbMedCert.Enabled = false;
            this.dtDateStart.Enabled = false;
            this.dtDateEnd.Enabled = false;
            this.dtFromTime.Enabled = false;
            this.dtToTime.Enabled = false;
            this.cbStatus.Enabled = false;
            this.txtCustomer.Enabled = false;
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.chIsFine.Enabled = false;

            this.chMonday.Enabled = false;
            this.chTuesday.Enabled = false;
            this.chWednesday.Enabled = false;
            this.chThursday.Enabled = false;
            this.chFriday.Enabled = false;
            this.chSaturday.Enabled = false;
            this.numFine.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.EDITING)
                {
                    if (this.cbUsers.Focused && this.cbUsers.Text.Length == 0)
                    {
                        SendKeys.Send("{F6}");
                        return true;
                    }

                    if (this.btnCancel.Focused || this.btnOK.Focused)
                    {
                        return false;
                    }

                    SendKeys.Send("{TAB}");
                    return true;
                }
            }

            if (keyData == Keys.F6)
            {
                if (this.form_mode == FORM_MODE.EDITING)
                {
                    if (this.cbMedCert.Focused || this.cbReason.Focused || this.cbUsers.Focused || this.dtDateStart.Focused || this.dtDateEnd.Focused)
                    {
                        SendKeys.Send("{F4}");
                        return true;
                    }
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
