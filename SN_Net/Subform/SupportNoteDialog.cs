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
    public partial class SupportNoteDialog : Form
    {
        private SupportStatWindow stat_windows;
        private bool allow_change_tab = false;
        public string IS_BREAK;
        public SupportNote note;
        private MODE mode;
        public enum MODE
        {
            ADD,
            EDIT
        }

        public SupportNoteDialog()
        {
            InitializeComponent();
        }

        public SupportNoteDialog(SupportStatWindow stat_windows, bool is_break)
            : this()
        {
            this.mode = MODE.ADD;
            this.stat_windows = stat_windows;
            this.IS_BREAK = (is_break ? "Y" : "N");
        }

        public SupportNoteDialog(SupportStatWindow stat_windows, bool is_break, SupportNote editing_note)
            : this()
        {
            this.mode = MODE.EDIT;
            this.stat_windows = stat_windows;
            this.IS_BREAK = (is_break ? "Y" : "N");
            this.note = editing_note;
        }

        private void SupportNoteDialog_Load(object sender, EventArgs e)
        {
            #region Attaching Checkbox Tag
            this.chAssets.Tag = SupportNote.NOTE_PROBLEM.ASSETS;
            this.chError.Tag = SupportNote.NOTE_PROBLEM.ERROR;
            this.chFonts.Tag = SupportNote.NOTE_PROBLEM.FONTS;
            this.chForm.Tag = SupportNote.NOTE_PROBLEM.FORM;
            this.chInstall.Tag = SupportNote.NOTE_PROBLEM.INSTALL_UPDATE;
            this.chMailWait.Tag = SupportNote.NOTE_PROBLEM.MAIL_WAIT;
            this.chMapDrive.Tag = SupportNote.NOTE_PROBLEM.MAP_DRIVE;
            this.chPeriod.Tag = SupportNote.NOTE_PROBLEM.PERIOD;
            this.chPrint.Tag = SupportNote.NOTE_PROBLEM.PRINT;
            this.chRepExcel.Tag = SupportNote.NOTE_PROBLEM.REPORT_EXCEL;
            this.chSecure.Tag = SupportNote.NOTE_PROBLEM.SECURE;
            this.chStatement.Tag = SupportNote.NOTE_PROBLEM.STATEMENT;
            this.chStock.Tag = SupportNote.NOTE_PROBLEM.STOCK;
            this.chTraining.Tag = SupportNote.NOTE_PROBLEM.TRAINING;
            this.chTransferMkt.Tag = SupportNote.NOTE_PROBLEM.TRANSFER_MKT;
            this.chYearEnd.Tag = SupportNote.NOTE_PROBLEM.YEAR_END;
            #endregion Attaching Checkbox Tag
            
            #region Attaching Radio Button Tag
            this.rbToilet.Tag = SupportNote.BREAK_REASON.TOILET;
            this.rbQt.Tag = SupportNote.BREAK_REASON.QT;
            this.rbMeetCust.Tag = SupportNote.BREAK_REASON.MEET_CUST;
            this.rbTraining.Tag = SupportNote.BREAK_REASON.TRAINING_ASSIST;
            this.rbCorrectData.Tag = SupportNote.BREAK_REASON.CORRECT_DATA;
            this.rbOther.Tag = SupportNote.BREAK_REASON.OTHER;
            #endregion Attaching Radio Button Tag

            #region add users to cbUser
            this.cbUser.Items.Clear();
            foreach (Users u in this.stat_windows.list_support_users)
            {
                this.cbUser.Items.Add(new ComboboxItem(u.username + " : " + u.name, u.id, u.username) { Tag = u });
            }
            #endregion add users to cbUser

            this.BindingControlEventHandler();
        }

        private void SupportNoteDialog_Shown(object sender, EventArgs e)
        {
            if (this.mode == MODE.ADD) // Add mode
            {
                this.dtWorkDate.Value = (this.stat_windows.current_date_from);
                this.dtStartTime.Text = TimeSpan.Parse(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString()).ToString();
                this.dtEndTime.Text = TimeSpan.Parse(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString()).ToString();
                this.dtBreakStart.Text = TimeSpan.Parse(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString()).ToString();
                this.dtBreakEnd.Text = TimeSpan.Parse(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString()).ToString();

                if (this.stat_windows.current_user_from.username == this.stat_windows.current_user_to.username)
                {
                    this.cbUser.SelectedItem = this.cbUser.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).username == this.stat_windows.current_user_from.username).First<ComboboxItem>();
                }
            }
            else // Edit mode
            {
                if (this.cbUser.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).username == this.note.users_name).Count<ComboboxItem>() > 0)
                {
                    this.cbUser.SelectedItem = this.cbUser.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).username == this.note.users_name).First<ComboboxItem>();
                }
                this.dtWorkDate.Value = DateTime.Parse(this.note.date, CultureInfo.GetCultureInfo("en-US"));
                this.dtStartTime.Text = this.note.start_time;
                this.dtBreakStart.Text = this.note.start_time;
                this.dtEndTime.Text = this.note.end_time;
                this.dtBreakEnd.Text = this.note.end_time;
                this.txtSernum.Texts = this.note.sernum;
                this.txtSernum2.Texts = this.note.sernum;
                this.txtContact.Texts = this.note.contact;
                this.txtRemark.Text = this.note.remark;
                this.txtRemark2.Text = this.note.remark;

                this.CheckedProblem(this.note.problem);
                this.CheckedReason(this.note.reason);
            }

            if (this.IS_BREAK == "Y")
            {
                Console.WriteLine(" >> select tab 2 , allow-change-tab : " + this.allow_change_tab.ToString());
                this.SetSelectedTab(this.tabPage2);
            }
            else
            {
                Console.WriteLine(" >> select tab 1 , allow-change-tab : " + this.allow_change_tab.ToString());
                this.SetSelectedTab(this.tabPage1);
            }

        }

        private void BindingControlEventHandler()
        {
            this.tabControl1.Deselecting += delegate(object sender, TabControlCancelEventArgs e)
            {
                e.Cancel = (this.allow_change_tab ? false : true);
            };

            this.cbUser.Leave += delegate
            {
                if (this.cbUser.Text.Trim().Length > 0)
                {
                    if (this.cbUser.Items.Cast<ComboboxItem>().Where(i => i.ToString().Length >= this.cbUser.Text.Trim().Length).Where(i => i.ToString().Substring(0, this.cbUser.Text.Trim().Length) == this.cbUser.Text).Count<ComboboxItem>() > 0)
                    {
                        this.cbUser.SelectedItem = this.cbUser.Items.Cast<ComboboxItem>().Where(i => i.ToString().Length >= this.cbUser.Text.Trim().Length).Where(i => i.ToString().Substring(0, this.cbUser.Text.Trim().Length) == this.cbUser.Text).First<ComboboxItem>();
                    }
                    else
                    {
                        this.cbUser.Focus();
                        SendKeys.Send("{F4}");
                    }
                }
                else
                {
                    this.cbUser.Focus();
                }
            };
        }

        private void SetSelectedTab(TabPage tab_page)
        {
            this.allow_change_tab = true;
            this.tabControl1.SelectedTab = tab_page;
            this.allow_change_tab = false;
        }

        private void CheckedProblem(string problem) // Check the checkbox that relate to current note problem
        {
            this.chMapDrive.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.MAP_DRIVE.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chInstall.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.INSTALL_UPDATE.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chError.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.ERROR.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chFonts.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.FONTS.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chPrint.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.PRINT.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);

            this.chStock.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.STOCK.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chForm.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.FORM.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chRepExcel.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.REPORT_EXCEL.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chStatement.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.STATEMENT.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chAssets.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.ASSETS.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);

            this.chSecure.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.SECURE.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chYearEnd.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.YEAR_END.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chPeriod.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.PERIOD.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chMailWait.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.MAIL_WAIT.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chTraining.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.TRAINING.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
            this.chTransferMkt.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.TRANSFER_MKT.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
        }

        private void CheckedReason(string reason)
        {
            this.rbToilet.Checked = (reason.Contains(SupportNote.BREAK_REASON.TOILET.FormatBreakReson()) ? true : false);
            this.rbQt.Checked = (reason.Contains(SupportNote.BREAK_REASON.QT.FormatBreakReson()) ? true : false);
            this.rbMeetCust.Checked = (reason.Contains(SupportNote.BREAK_REASON.MEET_CUST.FormatBreakReson()) ? true : false);
            this.rbTraining.Checked = (reason.Contains(SupportNote.BREAK_REASON.TRAINING_ASSIST.FormatBreakReson()) ? true : false);
            this.rbCorrectData.Checked = (reason.Contains(SupportNote.BREAK_REASON.CORRECT_DATA.FormatBreakReson()) ? true : false);
            this.rbOther.Checked = (reason.Contains(SupportNote.BREAK_REASON.OTHER.FormatBreakReson()) ? true : false);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //this.note = this.GetSupportNoteInForm();
            this.GetSupportNoteInForm();

            if (note.date.Trim().Length == 0)
            {
                MessageBox.Show("กรุณาระบุวันที่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.dtWorkDate.Focus();
                return;
            }

            if (note.users_name.Trim().Length == 0)
            {
                MessageBox.Show("กรุณาระบุรหัสพนักงาน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cbUser.Focus();
                return;
            }

            this.SaveToDB(this.note);
        }

        private void SaveToDB(SupportNote note)
        {
            bool save_success = false;
            string err_msg = string.Empty;

            string json_data = "{\"id\":" + note.id + ",";
            json_data += "\"sernum\":\"" + (note.sernum.Replace("-", "").Replace(" ", "").Length > 0 ? note.sernum.cleanString() : "") + "\",";
            json_data += "\"date\":\"" + note.date + "\",";
            json_data += "\"contact\":\"" + note.contact.cleanString() + "\",";
            json_data += "\"start_time\":\"" + note.start_time + "\",";
            json_data += "\"end_time\":\"" + note.end_time + "\",";
            json_data += "\"duration\":\"" + note.duration.Substring(0, 8) + "\",";
            json_data += "\"problem\":\"" + note.problem.cleanString() + "\",";
            json_data += "\"remark\":\"" + note.remark.cleanString() + "\",";
            json_data += "\"also_f8\":\"" + "N" + "\",";
            json_data += "\"probcod\":\"" + "" + "\",";
            json_data += "\"is_break\":\"" + this.IS_BREAK + "\",";
            json_data += "\"reason\":\"" + note.reason.cleanString() + "\",";
            json_data += "\"users_name\":\"" + note.users_name + "\",";
            json_data += "\"rec_by\":\"" + note.rec_by + "\"}";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post;
                if (note.id < 0) // create
                {
                    post = (this.IS_BREAK == "N" ? ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/create", json_data) : ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/create_break", json_data));
                }
                else // update
                {
                    post = (this.IS_BREAK == "N" ? ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/update", json_data) : ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/update_break", json_data));
                }
                
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    save_success = true;
                }
                else
                {
                    err_msg = sr.message;
                    save_success = false;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (save_success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.SaveToDB(note);
                        return;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
            };
            worker.RunWorkerAsync();
        }

        private void GetSupportNoteInForm()
        {
            string start_time = (this.IS_BREAK == "Y" ? this.dtBreakStart.Text : this.dtStartTime.Text);
            string end_time = (this.IS_BREAK == "Y" ? this.dtBreakEnd.Text : this.dtEndTime.Text);

            this.note = (this.note == null ? new SupportNote() { id = -1 } : this.note);

            //SupportNote note = new SupportNote()
            //{
                this.note.date = this.dtWorkDate.Value.ToMysqlDate();
                this.note.users_name = ((Users)((ComboboxItem)this.cbUser.SelectedItem).Tag).username;
                this.note.start_time = start_time;
                this.note.end_time = end_time;
                this.note.duration = TimeSpan.FromSeconds(TimeSpan.Parse(end_time).TotalSeconds - TimeSpan.Parse(start_time).TotalSeconds).ToString();
                this.note.sernum = (this.IS_BREAK == "N" ? this.txtSernum.Texts : this.txtSernum2.Texts);
                this.note.contact = this.txtContact.Texts;
                this.note.problem = this.GetProblemFormattedString();
                this.note.remark = (this.IS_BREAK == "N" ? this.txtRemark.Text : this.txtRemark2.Text);
                this.note.is_break = this.IS_BREAK;
                this.note.reason = this.GetReasonFormattedString();
                this.note.file_path = "";
                this.note.rec_by = this.stat_windows.main_form.G.loged_in_user_name;
            //};

            //return this.note;
        }
       
        private string GetProblemFormattedString()
        {
            string problem = string.Empty;
            if (this.IS_BREAK == "Y")
            {
                return problem;
            }
            problem += (this.chMapDrive.Checked ? ((SupportNote.NOTE_PROBLEM)this.chMapDrive.Tag).FormatNoteProblem() : "");
            problem += (this.chInstall.Checked ? ((SupportNote.NOTE_PROBLEM)this.chInstall.Tag).FormatNoteProblem() : "");
            problem += (this.chError.Checked ? ((SupportNote.NOTE_PROBLEM)this.chError.Tag).FormatNoteProblem() : "");
            problem += (this.chFonts.Checked ? ((SupportNote.NOTE_PROBLEM)this.chFonts.Tag).FormatNoteProblem() : "");
            problem += (this.chPrint.Checked ? ((SupportNote.NOTE_PROBLEM)this.chPrint.Tag).FormatNoteProblem() : "");
            problem += (this.chStock.Checked ? ((SupportNote.NOTE_PROBLEM)this.chStock.Tag).FormatNoteProblem() : "");
            problem += (this.chForm.Checked ? ((SupportNote.NOTE_PROBLEM)this.chForm.Tag).FormatNoteProblem() : "");
            problem += (this.chRepExcel.Checked ? ((SupportNote.NOTE_PROBLEM)this.chRepExcel.Tag).FormatNoteProblem() : "");
            problem += (this.chStatement.Checked ? ((SupportNote.NOTE_PROBLEM)this.chStatement.Tag).FormatNoteProblem() : "");
            problem += (this.chAssets.Checked ? ((SupportNote.NOTE_PROBLEM)this.chAssets.Tag).FormatNoteProblem() : "");
            problem += (this.chSecure.Checked ? ((SupportNote.NOTE_PROBLEM)this.chSecure.Tag).FormatNoteProblem() : "");
            problem += (this.chYearEnd.Checked ? ((SupportNote.NOTE_PROBLEM)this.chYearEnd.Tag).FormatNoteProblem() : "");
            problem += (this.chPeriod.Checked ? ((SupportNote.NOTE_PROBLEM)this.chPeriod.Tag).FormatNoteProblem() : "");
            problem += (this.chMailWait.Checked ? ((SupportNote.NOTE_PROBLEM)this.chMailWait.Tag).FormatNoteProblem() : "");
            problem += (this.chTraining.Checked ? ((SupportNote.NOTE_PROBLEM)this.chTraining.Tag).FormatNoteProblem() : "");
            problem += (this.chTransferMkt.Checked ? ((SupportNote.NOTE_PROBLEM)this.chTransferMkt.Tag).FormatNoteProblem() : "");

            return problem;
        }

        private string GetReasonFormattedString()
        {
            string reason = string.Empty;
            if (this.IS_BREAK == "N")
            {
                return reason;
            }
            reason += (this.rbToilet.Checked ? ((SupportNote.BREAK_REASON)this.rbToilet.Tag).FormatBreakReson() : "");
            reason += (this.rbQt.Checked ? ((SupportNote.BREAK_REASON)this.rbQt.Tag).FormatBreakReson() : "");
            reason += (this.rbMeetCust.Checked ? ((SupportNote.BREAK_REASON)this.rbMeetCust.Tag).FormatBreakReson() : "");
            reason += (this.rbTraining.Checked ? ((SupportNote.BREAK_REASON)this.rbTraining.Tag).FormatBreakReson() : "");
            reason += (this.rbCorrectData.Checked ? ((SupportNote.BREAK_REASON)this.rbCorrectData.Tag).FormatBreakReson() : "");
            reason += (this.rbOther.Checked ? ((SupportNote.BREAK_REASON)this.rbOther.Tag).FormatBreakReson() : "");

            return reason;
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
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
