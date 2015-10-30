using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;
using System.IO;

namespace SN_Net.Subform
{
    public partial class SupportStatWindow : Form
    {
        private MainForm main_form;
        private List<SupportNote> supportnote_list = new List<SupportNote>();
        private List<EventCalendar> event_calendar = new List<EventCalendar>();
        private List<Istab> leave_cause;
        private List<Note> note_list = new List<Note>();
        private BindingSource bs = new BindingSource();
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            READ,
            EDIT,
            PROCESSING
        }


        public SupportStatWindow()
        {
            InitializeComponent();
        }

        public SupportStatWindow(MainForm main_form)
            : this()
        {
            this.main_form = main_form;
        }

        private void SupportStatWindow_Load(object sender, EventArgs e)
        {
            this.AddEventHandler();
            this.FormInit();

            #region Load leave_cause from server
            CRUDResult get_leave_cause = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_all&tabtyp=" + Istab.getTabtypString(Istab.TABTYP.LEAVE_CAUSE) + "&sort=typcod");
            ServerResult sr_leave_cause = JsonConvert.DeserializeObject<ServerResult>(get_leave_cause.data);

            if (sr_leave_cause.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.leave_cause = sr_leave_cause.istab;
            }
            #endregion Load leave_cause from server
        }

        private void FormInit()
        {
            this.txtDummy.Width = 0;
            this.lblLeaveRemark.Text = "";
            this.toolStripPrint.Enabled = false;
            this.splitContainer1.SplitterDistance = 110;
            this.bs.DataSource = this.note_list;
            this.dgvNote.DataSource = this.bs;
            this.FillDataGrid();

            #region Add Support Code to cbSupportCode (ComboBox)
            this.cbSupportCode.Items.Add(new ComboboxItem("* All", 0, "*"));
            List<Users> support_users = new List<Users>();

            CRUDResult get_support_users = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_support_users");
            ServerResult sr_support_users = JsonConvert.DeserializeObject<ServerResult>(get_support_users.data);

            if (sr_support_users.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                foreach (Users u in sr_support_users.users)
                {
                    ComboboxItem item = new ComboboxItem(u.username, u.id, u.username);
                    item.Tag = u;
                    this.cbSupportCode.Items.Add(item);
                }
            }
            this.cbSupportCode.SelectedIndex = 0;
            #endregion Add Support Code to cbSupportCode (ComboBox)

            #region Add Problem type to cbProblem (ComboBox)
            this.cbProblem.Items.Add(new ComboboxItem("* All", 0, "*"));
            this.cbProblem.Items.Add(new ComboboxItem("Map Drive", 0, SupportNote.NOTE_PROBLEM.MAP_DRIVE.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("Install/Up", 0, SupportNote.NOTE_PROBLEM.INSTALL_UPDATE.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("Error", 0, SupportNote.NOTE_PROBLEM.ERROR.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("Fonts", 0, SupportNote.NOTE_PROBLEM.FONTS.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("Print", 0, SupportNote.NOTE_PROBLEM.PRINT.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("สินค้า", 0, SupportNote.NOTE_PROBLEM.STOCK.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("แก้ไขฟอร์ม/รายงาน", 0, SupportNote.NOTE_PROBLEM.FORM.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("รายงาน->Excel", 0, SupportNote.NOTE_PROBLEM.REPORT_EXCEL.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("สร้างงบฯ", 0, SupportNote.NOTE_PROBLEM.STATEMENT.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("ทรัพย์สิน/ค่าเสื่อม", 0, SupportNote.NOTE_PROBLEM.ASSETS.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("ระบบความปลอดภัย", 0, SupportNote.NOTE_PROBLEM.SECURE.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("ปิดประมวลผล", 0, SupportNote.NOTE_PROBLEM.YEAR_END.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("วันที่ไม่อยู่ในงวด", 0, SupportNote.NOTE_PROBLEM.PERIOD.FormatNoteProblem()));
            this.cbProblem.Items.Add(new ComboboxItem("Mail/รอสาย", 0, SupportNote.NOTE_PROBLEM.MAIL_WAIT.FormatNoteProblem()));
            this.cbProblem.SelectedIndex = 0;
            #endregion Add Problem type to cbProblem (ComboBox)

            #region Add Reason type to cbReason (ComboBox)
            this.cbReason.Items.Add(new ComboboxItem("* All", 0, "*"));
            this.cbReason.Items.Add(new ComboboxItem("เข้าห้องน้ำ", 0, SupportNote.BREAK_REASON.TOILET.FormatBreakReson()));
            this.cbReason.Items.Add(new ComboboxItem("ทำใบเสนอราคา", 0, SupportNote.BREAK_REASON.QT.FormatBreakReson()));
            this.cbReason.Items.Add(new ComboboxItem("ลูกค้ามาพบ", 0, SupportNote.BREAK_REASON.MEET_CUST.FormatBreakReson()));
            this.cbReason.Items.Add(new ComboboxItem("เข้าห้องอบรม", 0, SupportNote.BREAK_REASON.TRAINING.FormatBreakReson()));
            this.cbReason.Items.Add(new ComboboxItem("แก้ไขข้อมูลให้ลูกค้า", 0, SupportNote.BREAK_REASON.CORRECT_DATA.FormatBreakReson()));
            this.cbReason.SelectedIndex = 0;
            #endregion Add Reason type to cbReason (ComboBox)

            #region Support Code
            this.cbSupportCode.Text = "*";
            #endregion Support Code

            #region DatePicker
            this.dtDateStart.dateTimePicker1.Value = DateTime.Now;
            this.dtDateEnd.dateTimePicker1.Value = DateTime.Now;
            #endregion DatePicker

            #region S/N
            this.txtSernum.Texts = "*";
            #endregion S/N

            #region TimePicker
            //this.dtTimeStart.Text = "08:00:00";
            //this.dtTimeEnd.Text = "18:00:00";
            #endregion TimePicker
        }

        private void AddEventHandler()
        {
            #region Draw red line for current row & enable/disable toolstrip button
            this.dgvNote.Paint += delegate
            {
                if (this.dgvNote.Rows.Count > 0)
                {
                    this.toolStripPrint.Enabled = (this.form_mode == FORM_MODE.READ ? true : false);
                    this.toolStripExport.Enabled = (this.form_mode == FORM_MODE.READ ? true : false);
                    this.toolStripEdit.Enabled = (this.dgvNote.Parent.Controls.Find("txt_inline_remark", true).Length > 0 ? false : true);

                    Rectangle row_rect = this.dgvNote.GetRowDisplayRectangle(this.dgvNote.CurrentCell.RowIndex, false);
                    using (Pen p = new Pen(Color.Red))
                    {
                        this.dgvNote.CreateGraphics().DrawLine(p, row_rect.X, row_rect.Y, row_rect.X + row_rect.Width, row_rect.Y);
                        this.dgvNote.CreateGraphics().DrawLine(p, row_rect.X, row_rect.Y + row_rect.Height - 1, row_rect.X + row_rect.Width, row_rect.Y + row_rect.Height - 1);
                    }
                }
                else
                {
                    this.toolStripPrint.Enabled = false;
                    this.toolStripExport.Enabled = false;
                    this.toolStripEdit.Enabled = false;
                }
            };
            #endregion Draw red line for current row & enable/disable toolstrip button

            #region Set Background Color for break item
            this.dgvNote.RowPostPaint += delegate(object sender, DataGridViewRowPostPaintEventArgs e)
            {
                SupportNote snote = (SupportNote)((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value;
                if (snote.is_break == "Y")
                {
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorResource.DISABLE_ROW_BACKGROUND;
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorResource.DISABLE_ROW_BACKGROUND;
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Gray;
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Gray;
                }
            };
            #endregion Set Background Color for break item

            #region enable/disable cbProblem,cbReason depends on chProblem,chBreak
            this.chProblem.CheckedChanged += delegate
            {
                if (this.chProblem.Checked)
                {
                    this.cbProblem.Enabled = true;
                }
                else
                {
                    this.cbProblem.Enabled = false;
                }
            };
            this.chBreak.CheckedChanged += delegate
            {
                if (this.chBreak.Checked)
                {
                    this.cbReason.Enabled = true;
                }
                else
                {
                    this.cbReason.Enabled = false;
                }
            };
            #endregion enable/disable cbProblem,cbReason depends on chProblem,chBreak

            #region Adjust Inline Form Control position while datagridview is resized
            this.dgvNote.Resize += delegate
            {
                this.AdjustInlineFormPositon();
            };
            #endregion Adjust Inline Form Control position while datagridview is resized

            #region Show Inline Form When Double-click cell
            this.dgvNote.CellDoubleClick += delegate
            {
                if (this.dgvNote.CurrentCell != null)
                {
                    this.toolStripEdit.PerformClick();
                }
            };
            #endregion Show Inline Form When Double-click cell
        }

        private void FormRead()
        {
            this.form_mode = FORM_MODE.READ;
            this.toolStripProcessing.Visible = false;

            #region Toolstrip button
            this.toolStripPrint.Enabled = true;
            this.toolStripExport.Enabled = true;
            this.toolStripEdit.Enabled = true;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            #endregion Toolstrip button

            #region Control state
            this.cbSupportCode.Enabled = true;
            this.dtDateStart.Enabled = true;
            this.dtDateEnd.Enabled = true;
            this.cbProblem.Enabled = (this.chProblem.Checked ? true : false);
            this.cbReason.Enabled = (this.chBreak.Checked ? true : false);
            this.chProblem.Enabled = true;
            this.chBreak.Enabled = true;
            this.txtSernum.Enabled = true;
            this.btnViewNote.Enabled = true;
            #endregion Control state
        }

        private void FormEdit()
        {
            this.form_mode = FORM_MODE.EDIT;
            this.toolStripProcessing.Visible = false;

            #region Toolstrip button
            this.toolStripPrint.Enabled = false;
            this.toolStripExport.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            #endregion Toolstrip button

            #region Control state
            this.cbSupportCode.Enabled = false;
            this.dtDateStart.Enabled = false;
            this.dtDateEnd.Enabled = false;
            this.cbProblem.Enabled = false;
            this.cbReason.Enabled = false;
            this.chProblem.Enabled = false;
            this.chBreak.Enabled = false;
            this.txtSernum.Enabled = false;
            this.btnViewNote.Enabled = false;
            #endregion Control state
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;
            this.toolStripProcessing.Visible = true;

            #region Toolstrip button
            this.toolStripPrint.Enabled = false;
            this.toolStripExport.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            #endregion Toolstrip button

            #region Control state
            this.cbSupportCode.Enabled = false;
            this.dtDateStart.Enabled = false;
            this.dtDateEnd.Enabled = false;
            this.cbProblem.Enabled = false;
            this.cbReason.Enabled = false;
            this.chProblem.Enabled = false;
            this.chBreak.Enabled = false;
            this.txtSernum.Enabled = false;
            this.btnViewNote.Enabled = false;
            #endregion Control state
        }

        private void GetNote()
        {
            this.FormProcessing();
            bool get_success = false;
            string err_msg = "";

            string support_code = ((ComboboxItem)this.cbSupportCode.SelectedItem).string_value;
            string start_date = this.dtDateStart.TextsMysql;
            string end_date = this.dtDateEnd.TextsMysql;
            string is_problem = this.chProblem.CheckState.ToYesOrNoString();
            string is_break = this.chBreak.CheckState.ToYesOrNoString();
            string problem = ((ComboboxItem)this.cbProblem.SelectedItem).string_value;
            string reason = ((ComboboxItem)this.cbReason.SelectedItem).string_value;
            string sernum = this.txtSernum.Texts.cleanString();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "supportnote/get_note_admin&support_code=" + support_code + "&start_date=" + start_date + "&end_date=" + end_date + "&is_problem=" + is_problem + "&is_break=" + is_break + "&problem=" + problem + "&reason=" + reason + "&sernum=" + sernum);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.supportnote_list = sr.support_note;
                    this.event_calendar = sr.event_calendar;
                    get_success = true;
                }
                else
                {
                    err_msg = sr.message;
                    get_success = false;
                }
            };

            worker.RunWorkerCompleted += delegate
            {
                if (get_success)
                {
                    this.lblLeaveRemark.Text = (this.event_calendar.GetSummaryLeaveDayString().Length > 0 ? "ลางาน,ออกพบลูกค้า " + this.event_calendar.GetSummaryLeaveDayString() : "");
                    this.FillDataGrid();
                    this.FormRead();
                }
                else
                {
                    this.lblLeaveRemark.Text = "";
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.GetNote();
                    }
                    else
                    {
                        this.FormRead();
                    }
                }
            };

            worker.RunWorkerAsync();
        }

        private void FillDataGrid()
        {
            this.note_list.Clear();

            TimeSpan total_time = new TimeSpan(0, 0, 0);
            TimeSpan work_time = new TimeSpan(0, 0, 0);
            TimeSpan break_time = new TimeSpan(0, 0, 0);

            int count = 0;
            foreach (SupportNote snote in this.supportnote_list)
            {
                string[] duration = snote.duration.Split(':');
                total_time += new TimeSpan(Convert.ToInt32(duration[0]), Convert.ToInt32(duration[1]), Convert.ToInt32(duration[2]));
                work_time += (snote.is_break == "N" ? new TimeSpan(Convert.ToInt32(duration[0]), Convert.ToInt32(duration[1]), Convert.ToInt32(duration[2])) : new TimeSpan(0, 0, 0));
                break_time += (snote.is_break == "Y" ? new TimeSpan(Convert.ToInt32(duration[0]), Convert.ToInt32(duration[1]), Convert.ToInt32(duration[2])) : new TimeSpan(0, 0, 0));

                Note note = new Note();
                note.supportnote = snote;
                note.id = snote.id;
                note.is_break = snote.is_break;
                count += (snote.is_break != "Y" ? 1 : 0);
                note.seq = (snote.is_break != "Y" ? count.ToString() : "");
                note.users_name = snote.users_name;
                note.date = snote.date.M2WDate();
                note.start_time = snote.start_time;
                note.end_time = snote.end_time;
                note.duration = snote.duration;
                note.sernum = snote.sernum;
                note.contact = (snote.is_break != "Y" ? snote.contact : this.GetReason(snote.reason));
                note.remark = snote.remark;

                note.map_drive = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.MAP_DRIVE.FormatNoteProblem()) ? true : false);
                note.install = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.INSTALL_UPDATE.FormatNoteProblem()) ? true : false);
                note.error = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.ERROR.FormatNoteProblem()) ? true : false);
                note.fonts = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.FONTS.FormatNoteProblem()) ? true : false);
                note.print = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.PRINT.FormatNoteProblem()) ? true : false);
                note.training = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.TRAINING.FormatNoteProblem()) ? true : false);
                note.stock = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.STOCK.FormatNoteProblem()) ? true : false);
                note.form = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.FORM.FormatNoteProblem()) ? true : false);
                note.rep_excel = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.REPORT_EXCEL.FormatNoteProblem()) ? true : false);
                note.statement = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.STATEMENT.FormatNoteProblem()) ? true : false);
                note.asset = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.ASSETS.FormatNoteProblem()) ? true : false);
                note.secure = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.SECURE.FormatNoteProblem()) ? true : false);
                note.year_end = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.YEAR_END.FormatNoteProblem()) ? true : false);
                note.period = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.PERIOD.FormatNoteProblem()) ? true : false);
                note.mail_wait = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.MAIL_WAIT.FormatNoteProblem()) ? true : false);

                this.note_list.Add(note);
            }

            this.toolStripInfo.Text = "รวมเวลาปฏิบัติงาน : " + total_time.ToString() + " ( รับสาย = " + work_time.ToString() + ", พักสาย = " + break_time.ToString() + " )";
            this.bs.ResetBindings(false);

            this.dgvNote.Columns[0].Visible = false;
            this.dgvNote.Columns[1].Visible = false;
            this.dgvNote.Columns[2].Visible = false;
            this.dgvNote.Columns[3].HeaderText = "ลำดับ";
            this.dgvNote.Columns[3].Width = 40;
            this.dgvNote.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvNote.Columns[4].HeaderText = "Support#";
            this.dgvNote.Columns[4].Width = 70;
            this.dgvNote.Columns[5].HeaderText = "วันที่";
            this.dgvNote.Columns[5].Width = 80;
            this.dgvNote.Columns[6].HeaderText = "รับสาย";
            this.dgvNote.Columns[6].Width = 65;
            this.dgvNote.Columns[7].HeaderText = "วางสาย";
            this.dgvNote.Columns[7].Width = 65;
            this.dgvNote.Columns[8].HeaderText = "ระยะเวลา";
            this.dgvNote.Columns[8].Width = 65;
            this.dgvNote.Columns[9].HeaderText = "S/N";
            this.dgvNote.Columns[9].Width = 120;
            this.dgvNote.Columns[10].HeaderText = "ชื่อลูกค้า";
            this.dgvNote.Columns[10].Width = 160;
            this.dgvNote.Columns[11].HeaderText = "Map Drive";
            this.dgvNote.Columns[11].Width = 30;
            this.dgvNote.Columns[11].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvNote.Columns[12].HeaderText = "Ins. /Up";
            this.dgvNote.Columns[12].Width = 30;
            this.dgvNote.Columns[12].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[13].HeaderText = "Error";
            this.dgvNote.Columns[13].Width = 30;
            this.dgvNote.Columns[13].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[14].HeaderText = "Ins. Fonts";
            this.dgvNote.Columns[14].Width = 30;
            this.dgvNote.Columns[14].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[15].HeaderText = "Print";
            this.dgvNote.Columns[15].Width = 30;
            this.dgvNote.Columns[15].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[16].HeaderText = "อบรม";
            this.dgvNote.Columns[16].Width = 30;
            this.dgvNote.Columns[16].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[17].HeaderText = "สินค้า";
            this.dgvNote.Columns[17].Width = 30;
            this.dgvNote.Columns[17].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[18].HeaderText = "Form Rep.";
            this.dgvNote.Columns[18].Width = 30;
            this.dgvNote.Columns[18].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[19].HeaderText = "Rep> Excel";
            this.dgvNote.Columns[19].Width = 30;
            this.dgvNote.Columns[19].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[20].HeaderText = "สร้างงบ";
            this.dgvNote.Columns[20].Width = 30;
            this.dgvNote.Columns[20].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[21].HeaderText = "ท/ส. ค่าเสื่อม";
            this.dgvNote.Columns[21].Width = 30;
            this.dgvNote.Columns[21].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[22].HeaderText = "Se cure";
            this.dgvNote.Columns[22].Width = 30;
            this.dgvNote.Columns[22].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[23].HeaderText = "Year End";
            this.dgvNote.Columns[23].Width = 30;
            this.dgvNote.Columns[23].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[24].HeaderText = "วันที่ ไม่อยู่ในงวด";
            this.dgvNote.Columns[24].Width = 50;
            this.dgvNote.Columns[24].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[25].HeaderText = "Mail รอสาย";
            this.dgvNote.Columns[25].Width = 30;
            this.dgvNote.Columns[25].HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns[26].HeaderText = "ปัญหาอื่น ๆ";
            this.dgvNote.Columns[26].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvNote.Columns[27].Visible = false;

            if(this.dgvNote.Rows.Count > 0){
                //this.dgvNote.Rows[0].Cells[3].Selected = true;
                this.dgvNote.Focus();
            }
        }

        private string ReadableBreakReason(string formatted_reason) // Get the human readable string of break reason for display with Control
        {
            if (formatted_reason == "{TOILET}")
            {
                return "** เข้าห้องน้ำ **";
            }
            else if (formatted_reason == "{QT}")
            {
                return "** ทำใบเสนอราคา **";
            }
            else if (formatted_reason == "{MEET_CUST}")
            {
                return "** ลูกค้ามาพบ **";
            }
            else if (formatted_reason == "{TRAINING}")
            {
                return "** เข้าห้องอบรม **";
            }
            else if (formatted_reason == "{CORRECT_DATA}")
            {
                return "** แก้ไขข้อมูลลูกค้า **";
            }
            else
            {
                return "";
            }
        }

        private string GetReason(string problem) // Get human readable string of break reason for display in datagridview
        {
            string reason = "";
            reason += (problem.Contains(SupportNote.BREAK_REASON.TOILET.FormatBreakReson()) ? "** เข้าห้องน้ำ **" : "");
            reason += (problem.Contains(SupportNote.BREAK_REASON.QT.FormatBreakReson()) ? "** ทำใบเสนอราคา **" : "");
            reason += (problem.Contains(SupportNote.BREAK_REASON.MEET_CUST.FormatBreakReson()) ? "** ลูกค้ามาพบ **" : "");
            reason += (problem.Contains(SupportNote.BREAK_REASON.TRAINING.FormatBreakReson()) ? "** เข้าห้องอบรม **" : "");
            reason += (problem.Contains(SupportNote.BREAK_REASON.CORRECT_DATA.FormatBreakReson()) ? "** แก้ข้อมูลให้ลูกค้า **" : "");

            return reason;
        }

        private void btnViewNote_Click(object sender, EventArgs e)
        {
            this.GetNote();
        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            this.btnViewNote.PerformClick();
            PrintDocument print_doc = new PrintDocument();

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("A4", 825, 1165);
            page_setup.PageSettings.Landscape = true;
            page_setup.PageSettings.Margins = new Margins(0, 0, 0, 40);

            PrintOutputSelection wind = new PrintOutputSelection();
            if (wind.ShowDialog() == DialogResult.OK)
            {
                int row_num = 0;
                int page_no = 0;
                print_doc.BeginPrint += delegate(object obj_sender, PrintEventArgs pe)
                {
                    
                };

                print_doc.PrintPage += delegate(object obj_sender, PrintPageEventArgs pe)
                {
                    bool is_new_page = true;
                    page_no++;

                    using(Font font = new Font("tahoma", 7f)){
                        using(SolidBrush brush = new SolidBrush(Color.Black)){
                            using (Pen p = new Pen(Color.LightGray))
                            {
                                int y_pos = 5;
                                #region declare column width
                                int col0_width = 30; // seq
                                int col1_width = 50; // support#
                                int col2_width = 60; // date
                                int col3_width = 45; // start time
                                int col4_width = 45; // end time
                                int col5_width = 45; // duration
                                int col6_width = 75; // s/n
                                int col7_width = 80; // contact
                                int col8_width = 25; // map drive
                                int col9_width = 25; // install,update
                                int col10_width = 25; // error
                                int col11_width = 25; // install fonts
                                int col12_width = 25; // print
                                int col13_width = 25; // training
                                int col14_width = 25; // stock
                                int col15_width = 25; // edit form
                                int col16_width = 25; // report->excel
                                int col17_width = 25; // statement
                                int col18_width = 25; // asset
                                int col19_width = 25; // secure
                                int col20_width = 25; // year end
                                int col21_width = 25; // period
                                int col22_width = 25; // mail
                                int col23_width = 330; // remark
                                #endregion declare column width

                                StringFormat str_format_center = new StringFormat();
                                str_format_center.Alignment = StringAlignment.Center;
                                str_format_center.LineAlignment = StringAlignment.Center;

                                StringFormat str_format_right = new StringFormat();
                                str_format_right.Alignment = StringAlignment.Far;
                                str_format_right.LineAlignment = StringAlignment.Far;

                                Font fontsmall = new Font("tahoma", 5f); // for some column header

                                for (int i = row_num; i < this.note_list.Count; i++)
                                {
                                    int x_pos = 10;

                                    if (y_pos > pe.MarginBounds.Bottom)
                                    {
                                        pe.HasMorePages = true;
                                        return;
                                    }
                                    else
                                    {
                                        pe.HasMorePages = false;
                                    }

                                    #region draw column header
                                    if (is_new_page) // column header
                                    {
                                        using (Pen pen_darkgray = new Pen(Color.DarkGray))
                                        {
                                            y_pos += 5;
                                            pe.Graphics.DrawString("บันทึกการปฏิบัติงาน", new Font("tahoma", 11f, FontStyle.Bold), brush, new Rectangle(x_pos, y_pos, 300, 20));
                                            pe.Graphics.DrawString("(" + DateTime.Now.ToString() + ")", font, brush, new Rectangle(x_pos + 1000, y_pos, 130, 13), str_format_right);
                                            y_pos += 20;
                                            string report_condition = "ระบุขอบเขตรายงาน : (Support# : \'" + this.cbSupportCode.Text + "\', วันที่ : \'" + this.dtDateStart.Texts + "\' - \'" + this.dtDateEnd.Texts + "\'" + (this.chProblem.Checked ? ", ประเภทการสนทนา : \'" + this.cbProblem.Text + "\'" : "") + (this.chBreak.Checked ? ", ประเภทการพักสาย : \'" + this.cbReason.Text + "\'" : "") + ", S/N : \'" + this.txtSernum.Texts + "\')";
                                            pe.Graphics.DrawString(report_condition, font, brush, new Rectangle(x_pos, y_pos, 1100, 13));
                                            pe.Graphics.DrawString("หน้า : " + page_no.ToString(), font, brush, new Rectangle(x_pos + 1000, y_pos, 130, 13), str_format_right); // draw page no.
                                            y_pos += 15;

                                            pe.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), new RectangleF(x_pos, y_pos, 1135, 25));

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos + 1135, y_pos); // horizontal line upper

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect0 = new Rectangle(x_pos, y_pos, col0_width, 25);
                                            pe.Graphics.DrawString("ลำดับ", font, brush, header_rect0, str_format_center);
                                            x_pos += col0_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect1 = new Rectangle(x_pos, y_pos, col1_width, 25);
                                            pe.Graphics.DrawString("Support#", font, brush, header_rect1, str_format_center);
                                            x_pos += col1_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect2 = new Rectangle(x_pos, y_pos, col2_width, 25);
                                            pe.Graphics.DrawString("วันที่", font, brush, header_rect2, str_format_center);
                                            x_pos += col2_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect3 = new Rectangle(x_pos, y_pos, col3_width, 25);
                                            pe.Graphics.DrawString("รับสาย", font, brush, header_rect3, str_format_center);
                                            x_pos += col3_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect4 = new Rectangle(x_pos, y_pos, col4_width, 25);
                                            pe.Graphics.DrawString("วางสาย", font, brush, header_rect4, str_format_center);
                                            x_pos += col4_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect5 = new Rectangle(x_pos, y_pos, col5_width, 25);
                                            pe.Graphics.DrawString("ระยะเวลา", font, brush, header_rect5, str_format_center);
                                            x_pos += col5_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect6 = new Rectangle(x_pos, y_pos, col6_width, 25);
                                            pe.Graphics.DrawString("S/N", font, brush, header_rect6, str_format_center);
                                            x_pos += col6_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect7 = new Rectangle(x_pos, y_pos, col7_width, 25);
                                            pe.Graphics.DrawString("ชื่อลูกค้า", font, brush, header_rect7, str_format_center);
                                            x_pos += col7_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect8 = new Rectangle(x_pos, y_pos, col8_width, 25);
                                            pe.Graphics.DrawString("Map Drive", fontsmall, brush, header_rect8, str_format_center);
                                            x_pos += col8_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect9 = new Rectangle(x_pos, y_pos, col9_width, 25);
                                            pe.Graphics.DrawString("Ins. /Up", fontsmall, brush, header_rect9, str_format_center);
                                            x_pos += col9_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect10 = new Rectangle(x_pos, y_pos, col10_width, 25);
                                            pe.Graphics.DrawString("Error", fontsmall, brush, header_rect10, str_format_center);
                                            x_pos += col10_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect11 = new Rectangle(x_pos, y_pos, col11_width, 25);
                                            pe.Graphics.DrawString("Ins. Fonts", fontsmall, brush, header_rect11, str_format_center);
                                            x_pos += col11_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect12 = new Rectangle(x_pos, y_pos, col12_width, 25);
                                            pe.Graphics.DrawString("Print", fontsmall, brush, header_rect12, str_format_center);
                                            x_pos += col12_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect13 = new Rectangle(x_pos, y_pos, col13_width, 25);
                                            pe.Graphics.DrawString("อบรม", fontsmall, brush, header_rect13, str_format_center);
                                            x_pos += col13_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect14 = new Rectangle(x_pos, y_pos, col14_width, 25);
                                            pe.Graphics.DrawString("สินค้า", fontsmall, brush, header_rect14, str_format_center);
                                            x_pos += col14_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect15 = new Rectangle(x_pos, y_pos, col15_width, 25);
                                            pe.Graphics.DrawString("Form Rep.", fontsmall, brush, header_rect15, str_format_center);
                                            x_pos += col15_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect16 = new Rectangle(x_pos, y_pos, col16_width, 25);
                                            pe.Graphics.DrawString("Rep>Excel", fontsmall, brush, header_rect16, str_format_center);
                                            x_pos += col16_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect17 = new Rectangle(x_pos, y_pos, col17_width, 25);
                                            pe.Graphics.DrawString("สร้างงบ", fontsmall, brush, header_rect17, str_format_center);
                                            x_pos += col17_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect18 = new Rectangle(x_pos, y_pos, col18_width, 25);
                                            pe.Graphics.DrawString("สินทรัพย์ ค่าเสื่อม", fontsmall, brush, header_rect18, str_format_center);
                                            x_pos += col18_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect19 = new Rectangle(x_pos, y_pos, col19_width, 25);
                                            pe.Graphics.DrawString("Secure", fontsmall, brush, header_rect19, str_format_center);
                                            x_pos += col19_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect20 = new Rectangle(x_pos, y_pos, col20_width, 25);
                                            pe.Graphics.DrawString("Year End", fontsmall, brush, header_rect20, str_format_center);
                                            x_pos += col20_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect21 = new Rectangle(x_pos, y_pos, col21_width, 25);
                                            pe.Graphics.DrawString("วันที่ไม่อยู่ในงวด", fontsmall, brush, header_rect21, str_format_center);
                                            x_pos += col21_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect22 = new Rectangle(x_pos, y_pos, col22_width, 25);
                                            pe.Graphics.DrawString("Mail,รอสาย", fontsmall, brush, header_rect22, str_format_center);
                                            x_pos += col22_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect23 = new Rectangle(x_pos, y_pos, col23_width, 25);
                                            pe.Graphics.DrawString("ปัญหาอื่น ๆ", font, brush, header_rect23, str_format_center);
                                            x_pos += col23_width;
                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator

                                            x_pos = 10; // set x_pos again after use in header
                                            y_pos += 25;
                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos + 1135, y_pos); // horizontal line below
                                        }

                                        y_pos += 7;
                                        is_new_page = false;
                                    }
                                    #endregion draw column header

                                    #region draw row data
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);
                                    Rectangle rect0 = new Rectangle(x_pos, y_pos, col0_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].seq, font, brush, rect0, str_format_right);
                                    x_pos += col0_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15); // column separator

                                    Rectangle rect1 = new Rectangle(x_pos, y_pos, col1_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].users_name, font, brush, rect1, str_format_center);
                                    x_pos += col1_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect2 = new Rectangle(x_pos, y_pos, col2_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].date, font, brush, rect2, str_format_center);
                                    x_pos += col2_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect3 = new Rectangle(x_pos, y_pos, col3_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].start_time, font, brush, rect3, str_format_center);
                                    x_pos += col3_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect4 = new Rectangle(x_pos, y_pos, col4_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].end_time, font, brush, rect4, str_format_center);
                                    x_pos += col4_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect5 = new Rectangle(x_pos, y_pos, col5_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].duration, font, brush, rect5, str_format_center);
                                    x_pos += col5_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect6 = new Rectangle(x_pos, y_pos, col6_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].sernum, font, brush, rect6);
                                    x_pos += col6_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect7 = new Rectangle(x_pos, y_pos, col7_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].contact, font, brush, rect7);
                                    x_pos += col7_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect8 = new Rectangle(x_pos, y_pos, col8_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].map_drive.ToString().TF2YN(true), font, brush, rect8, str_format_center);
                                    x_pos += col8_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect9 = new Rectangle(x_pos, y_pos, col9_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].install.ToString().TF2YN(true), font, brush, rect9, str_format_center);
                                    x_pos += col9_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect10 = new Rectangle(x_pos, y_pos, col10_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].error.ToString().TF2YN(true), font, brush, rect10, str_format_center);
                                    x_pos += col10_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect11 = new Rectangle(x_pos, y_pos, col11_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].fonts.ToString().TF2YN(true), font, brush, rect11, str_format_center);
                                    x_pos += col11_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect12 = new Rectangle(x_pos, y_pos, col12_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].print.ToString().TF2YN(true), font, brush, rect12, str_format_center);
                                    x_pos += col12_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect13 = new Rectangle(x_pos, y_pos, col13_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].training.ToString().TF2YN(true), font, brush, rect13, str_format_center);
                                    x_pos += col13_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect14 = new Rectangle(x_pos, y_pos, col14_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].stock.ToString().TF2YN(true), font, brush, rect14, str_format_center);
                                    x_pos += col14_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect15 = new Rectangle(x_pos, y_pos, col15_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].form.ToString().TF2YN(true), font, brush, rect15, str_format_center);
                                    x_pos += col15_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect16 = new Rectangle(x_pos, y_pos, col16_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].rep_excel.ToString().TF2YN(true), font, brush, rect16, str_format_center);
                                    x_pos += col16_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect17 = new Rectangle(x_pos, y_pos, col17_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].statement.ToString().TF2YN(true), font, brush, rect17, str_format_center);
                                    x_pos += col17_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect18 = new Rectangle(x_pos, y_pos, col18_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].asset.ToString().TF2YN(true), font, brush, rect18, str_format_center);
                                    x_pos += col18_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect19 = new Rectangle(x_pos, y_pos, col19_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].secure.ToString().TF2YN(true), font, brush, rect19, str_format_center);
                                    x_pos += col19_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect20 = new Rectangle(x_pos, y_pos, col20_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].year_end.ToString().TF2YN(true), font, brush, rect20, str_format_center);
                                    x_pos += col20_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect21 = new Rectangle(x_pos, y_pos, col21_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].period.ToString().TF2YN(true), font, brush, rect21, str_format_center);
                                    x_pos += col21_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect22 = new Rectangle(x_pos, y_pos, col22_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].mail_wait.ToString().TF2YN(true), font, brush, rect22, str_format_center);
                                    x_pos += col22_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    Rectangle rect23 = new Rectangle(x_pos, y_pos, col23_width, 13);
                                    pe.Graphics.DrawString(this.note_list[i].remark, font, brush, rect23);
                                    x_pos += col23_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    // Horizontal line
                                    x_pos = 10;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos + 15, x_pos + 1135, y_pos + 15);
                                    #endregion draw row data

                                    row_num++;
                                    y_pos += 20;
                                }

                                if (y_pos > pe.MarginBounds.Bottom)
                                {
                                    pe.HasMorePages = true;
                                    return;
                                }
                                else
                                {
                                    pe.HasMorePages = false;
                                }
                                pe.Graphics.DrawString(this.toolStripInfo.Text, font, brush, new Rectangle(10, y_pos, 400, 15));
                            }
                        }
                    }
                };

                if (wind.output == PrintOutputSelection.OUTPUT.PRINTER)
                {
                    PrintDialog print_dialog = new PrintDialog();
                    print_dialog.Document = print_doc;
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = true;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = true;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_doc.Print();
                    }
                }

                if (wind.output == PrintOutputSelection.OUTPUT.SCREEN)
                {
                    PrintPreviewDialog preview_dialog = new PrintPreviewDialog();
                    preview_dialog.SetBounds(this.ClientRectangle.X + 5, this.ClientRectangle.Y + 5, this.ClientRectangle.Width - 10, this.ClientRectangle.Height - 10);
                    preview_dialog.Document = print_doc;
                    preview_dialog.MdiParent = this.main_form;
                    preview_dialog.Show();
                }

                if (wind.output == PrintOutputSelection.OUTPUT.FILE)
                {

                }
            }
            else
            {
                print_doc = null;
                page_setup = null;
            }
        }

        private void toolStripExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Comma separated value | *.csv";
            dlg.DefaultExt = "csv";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string destination_filename = dlg.FileName;
                
                DataTable dt = this.note_list.ToDataTable<Note>();

                StringBuilder sb = new StringBuilder();

                // Create column header as datatable header
                //string[] columnNames = dt.Columns.Cast<DataColumn>().
                //                                  Select(column => column.ColumnName).
                //                                  ToArray();
                //sb.AppendLine(string.Join(",", columnNames));

                // Create custom column header as we need
                sb.AppendLine("ลำดับ,Support#,วันที่,รับสาย,วางสาย,ระยะเวลา,S/N,ชื่อลูกค้า,Map Drive,Ins./Up,Error,Ins.Fonts,Print,อบรม,สินค้า,Form Rep.,Rep.>Excel,สร้างงบ,ท/ส ค่าเสื่อม,Secure,Year End,วันที่ไม่อยู่ในงวด,Mail รอสาย,ปัญหาอื่น ๆ");

                foreach (DataRow row in dt.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();

                    // Append all column data
                    //sb.AppendLine(string.Join(",", fields));

                    // Append some column data as we needf
                    sb.AppendLine(fields[3] + "," +
                                    fields[4] + "," +
                                    fields[5].toMySQLDate() + "," +
                                    fields[6] + "," +
                                    fields[7] + "," +
                                    fields[8] + "," +
                                    fields[9] + "," +
                                    fields[10] + "," +
                                    fields[11].TF2YN(true) + "," +
                                    fields[12].TF2YN(true) + "," +
                                    fields[13].TF2YN(true) + "," +
                                    fields[14].TF2YN(true) + "," +
                                    fields[15].TF2YN(true) + "," +
                                    fields[16].TF2YN(true) + "," +
                                    fields[17].TF2YN(true) + "," +
                                    fields[18].TF2YN(true) + "," +
                                    fields[19].TF2YN(true) + "," +
                                    fields[20].TF2YN(true) + "," +
                                    fields[21].TF2YN(true) + "," +
                                    fields[22].TF2YN(true) + "," +
                                    fields[23].TF2YN(true) + "," +
                                    fields[24].TF2YN(true) + "," +
                                    fields[25].TF2YN(true) + "," +
                                    fields[26]);
                }
                this.SaveExportedFile(destination_filename, sb.ToString());
            }
        }

        private void SaveExportedFile(string destination_filename, string content)
        {
            try
            {
                File.WriteAllText(destination_filename, content, Encoding.Default);
            }
            catch (IOException ex)
            {
                if (MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                {
                    this.SaveExportedFile(destination_filename, content);
                }
                else
                {
                    return;
                }
            }
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.FormEdit();
            string remark = (string)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[26].Value;

            CustomTextBox ct = new CustomTextBox();
            ct.ReadOnly = false;
            ct.BorderStyle = BorderStyle.None;
            ct.Name = "txt_inline_remark";
            ct.Texts = remark;
            
            this.dgvNote.Parent.Controls.Add(ct);
            this.AdjustInlineFormPositon();

            this.dgvNote.Enabled = false;
            this.dgvNote.SendToBack();
            ct.BringToFront();
            ct.Focus();
            ct.SelectionStart = 0;
            ct.SelectionLength = 0;
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            this.ClearInlineForm();
            this.FormRead();
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            DataGridViewCell curr_cell = this.dgvNote.CurrentCell; // store current cell to set focus after update remark
            int note_id = ((SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[0].Value).id;
            string remark = "";
            if (this.dgvNote.Parent.Controls.Find("txt_inline_remark", true).Length > 0)
            {
                CustomTextBox ct = (CustomTextBox)this.dgvNote.Parent.Controls.Find("txt_inline_remark", true)[0];
                remark = ct.Texts;
            }
            this.FormProcessing();

            string json_data = "{\"id\":" + note_id.ToString() + ",";
            json_data += "\"remark\":\"" + remark.cleanString() + "\"}";
            bool post_success = false;
            string err_msg = "";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/edit_remark_by_admin", json_data);
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
                    this.GetNote();
                    this.ClearInlineForm();
                    curr_cell.Selected = true;
                }
            };

            worker.RunWorkerAsync();
        }

        private void ClearInlineForm()
        {
            if (this.dgvNote.Parent.Controls.Find("txt_inline_remark", true).Length > 0)
            {
                this.dgvNote.Parent.Controls.RemoveByKey("txt_inline_remark");
            }
            this.dgvNote.Enabled = true;
            this.dgvNote.Focus();
        }

        private void AdjustInlineFormPositon()
        {
            if (this.dgvNote.Parent.Controls.Find("txt_inline_remark", true).Length > 0)
            {
                CustomTextBox ct = (CustomTextBox)this.dgvNote.Parent.Controls.Find("txt_inline_remark", true)[0];
                Rectangle cell26_rect = this.dgvNote.GetCellDisplayRectangle(26, this.dgvNote.CurrentCell.RowIndex, true);
                Console.WriteLine(" >>>> current cell.row_index : " + this.dgvNote.CurrentCell.RowIndex.ToString());
                ct.SetBounds(cell26_rect.X + 3, cell26_rect.Y + 1, cell26_rect.Width - 1, cell26_rect.Height - 2);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                //return true;
            }
            if (keyData == Keys.Escape)
            {
                this.toolStripStop.PerformClick();
                return true;
            }
            if (keyData == Keys.F12)
            {
                this.toolStripExport.PerformClick();
                return true;
            }
            if (keyData == Keys.F9)
            {
                this.toolStripSave.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.E))
            {
                this.toolStripEdit.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void lblLeaveRemark_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.lblLeaveRemark.Text.Length > 0)
            {
                if (this.main_form.leave_wind == null)
                {
                    LeaveWindow wind = new LeaveWindow(this.main_form, (Users)((ComboboxItem)this.cbSupportCode.SelectedItem).Tag, this.dtDateStart.dateTimePicker1.Value, this.dtDateEnd.dateTimePicker1.Value);
                    wind.MdiParent = this.main_form;
                    wind.Show();
                }
                else
                {
                    this.main_form.leave_wind.CrossingCall((Users)((ComboboxItem)this.cbSupportCode.SelectedItem).Tag, this.dtDateStart.dateTimePicker1.Value, this.dtDateEnd.dateTimePicker1.Value);
                    this.main_form.leave_wind.Activate();
                }
            }
        }
    }
}
