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
    public partial class TrainerNoteDialog : Form
    {
        private MainForm main_form;
        private Users user;
        private DateTime date;
        private CultureInfo cinfo_th = new CultureInfo("th-TH");
        private CultureInfo cinfo_en = new CultureInfo("en-US");
        private SupportNote support_note;

        public TrainerNoteDialog(MainForm main_form, Users user, DateTime date, SupportNote support_note = null)
        {
            InitializeComponent();
            this.main_form = main_form;
            this.user = user;
            this.date = date;
            this.support_note = support_note;
        }

        private void TrainerNoteDialog_Load(object sender, EventArgs e)
        {
            
        }

        private void TrainerNoteDialog_Shown(object sender, EventArgs e)
        {
            if (this.support_note == null) // add mode
            {
                this.txtTrainer.Text = this.user.username + " : " + this.user.name;
                this.dtDate.Value = this.date;
                this.tmFrom.Value = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 8:30:0", cinfo_en);
                this.tmTo.Value = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 12:0:0", cinfo_en);
            }
            else // update mode
            {
                this.txtTrainer.Text = this.user.username + " : " + this.user.name;
                this.dtDate.pickedDate(this.support_note.date);
                this.tmFrom.Text = this.support_note.start_time;
                this.tmTo.Text = this.support_note.end_time;
            }
            this.SetTitle();
            this.tmFrom.Focus();
        }

        private void SetTitle()
        {
            if (this.support_note == null)
            {
                this.Text = "กำหนดช่วงเวลาอบรม";
            }
            else
            {
                this.Text = "แก้ไขช่วงเวลาอบรม";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TimeSpan ts = TimeSpan.Parse(this.tmTo.Value.Hour.ToString() + ":" + this.tmTo.Value.Minute.ToString() + ":00") - TimeSpan.Parse(this.tmFrom.Value.Hour.ToString() + ":" + this.tmFrom.Value.Minute.ToString() + ":00");

            if (this.support_note == null) // add mode
            {
                string json_data = "{\"date\":\"" + this.date.ToMysqlDate() + "\",";
                json_data += "\"sernum\":\"\",";
                json_data += "\"start_time\":\"" + this.tmFrom.Text + ":00\",";
                json_data += "\"end_time\":\"" + this.tmTo.Text + ":00\",";
                json_data += "\"duration\":\"" + ts.ToString().Substring(0, 8) + "\",";
                json_data += "\"reason\":\"" + SupportNote.BREAK_REASON.TRAINING_TRAINER.FormatBreakReson() + "\",";
                json_data += "\"remark\":\"\",";
                json_data += "\"users_name\":\"" + this.user.username + "\",";
                json_data += "\"is_break\":\"Y\",";
                json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/create_break", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
            else // update mode
            {
                string json_data = "{\"id\":" + this.support_note.id.ToString() + ",";
                json_data += "\"date\":\"" + this.date.ToMysqlDate() + "\",";
                json_data += "\"sernum\":\"\",";
                json_data += "\"start_time\":\"" + this.tmFrom.Text + ":00\",";
                json_data += "\"end_time\":\"" + this.tmTo.Text + ":00\",";
                json_data += "\"duration\":\"" + ts.ToString().Substring(0, 8) + "\",";
                json_data += "\"reason\":\"" + SupportNote.BREAK_REASON.TRAINING_TRAINER.FormatBreakReson() + "\",";
                json_data += "\"remark\":\"\",";
                json_data += "\"users_name\":\"" + this.user.username + "\",";
                json_data += "\"is_break\":\"Y\",";
                json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/update_break", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.btnOK.Focused || this.btnCancel.Focused)
                {
                    return false;
                }

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
