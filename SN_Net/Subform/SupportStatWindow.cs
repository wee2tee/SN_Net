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
        public MainForm main_form;
        private List<SupportNote> supportnote_list = new List<SupportNote>();
        public List<SupportNoteComment> supportnotecomment_list = new List<SupportNoteComment>();
        private List<EventCalendar> event_calendar = new List<EventCalendar>();
        private List<Istab> leave_cause;
        private List<Note> note_list = new List<Note>();
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            READ,
            EDIT,
            PROCESSING
        }
        private int sorted_column = 3; // sort datagridview by 5th column

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
            CRUDResult get_leave_cause = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_all&tabtyp=" + Istab.getTabtypString(Istab.TABTYP.ABSENT_CAUSE) + "&sort=typcod");
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
                    ComboboxItem item = new ComboboxItem(u.username + " : " + u.name, u.id, u.username);
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
            this.dgvNote.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (this.dgvNote.CurrentCell != null)
                {
                    if (e.RowIndex > -1)
                    {
                        this.toolStripEdit.PerformClick();
                    }
                }
            };
            #endregion Show Inline Form When Double-click cell

            #region Re-order datagridview by click column header
            this.dgvNote.ColumnHeaderMouseClick += delegate(object sender, DataGridViewCellMouseEventArgs e)
            {
                if (e.ColumnIndex == 3)
                {
                    this.supportnote_list = this.supportnote_list.OrderBy(t => t.date).ThenBy(t => t.start_time).ToList<SupportNote>();
                    this.sorted_column = 3;
                    this.FillDataGrid();
                }
                if (e.ColumnIndex == 6)
                {
                    this.supportnote_list = this.supportnote_list.OrderBy(t => t.duration).ToList<SupportNote>();
                    this.sorted_column = 6;
                    this.FillDataGrid();
                }
            };
            #endregion Re-order datagridview by click column header

            #region Comment/Complain button
            this.dgvNote.CellMouseClick += delegate(object sender, DataGridViewCellMouseEventArgs e)
            {
                if (e.RowIndex > -1)
                {
                    if (e.ColumnIndex == 25)
                    {
                        this.ShowCommentForm();
                    }
                }
            };
            #endregion Comment/Complain button
        }

        private void ShowCommentForm()
        {
            int current_note_id = ((Note)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Tag).id;
            CommentWindow wind = new CommentWindow(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.GetNote(current_note_id);
            }
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

        private void GetNote(int selected_row_id = 0)
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
                    this.supportnotecomment_list = sr.support_note_comment;
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
                    this.FillDataGrid(selected_row_id);
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

        private void FillDataGrid(int selected_row_id = 0)
        {
            TimeSpan total_time = new TimeSpan(0, 0, 0);
            TimeSpan work_time = new TimeSpan(0, 0, 0);
            TimeSpan break_time = new TimeSpan(0, 0, 0);

            this.dgvNote.Rows.Clear();
            this.dgvNote.Columns.Clear();

            #region Create Columns
            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.Visible = false;
            this.dgvNote.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Width = 40;
            col1.HeaderText = "ลำดับ";
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            col1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvNote.Columns.Add(col1);
            
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Width = 70;
            col2.HeaderText = "Support#";
            col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Width = 80;
            col3.HeaderText = "วันที่";
            this.dgvNote.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Width = 65;
            col4.HeaderText = "รับสาย";
            col4.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Width = 65;
            col5.HeaderText = "วางสาย";
            col5.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col5);
            
            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Width = 65;
            col6.HeaderText = "ระยะเวลา";
            this.dgvNote.Columns.Add(col6);

            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            col7.Width = 120;
            col7.HeaderText = "S/N";
            col7.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col7);

            DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
            col8.Width = 160;
            col8.HeaderText = "ชื่อลูกค้า";
            col8.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col8);

            DataGridViewCheckBoxColumn col9 = new DataGridViewCheckBoxColumn();
            col9.Width = 30;
            col9.HeaderText = "Map Drive";
            col9.SortMode = DataGridViewColumnSortMode.NotSortable;
            col9.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col9);

            DataGridViewCheckBoxColumn col10 = new DataGridViewCheckBoxColumn();
            col10.Width = 30;
            col10.HeaderText = "Ins. /Up";
            col10.SortMode = DataGridViewColumnSortMode.NotSortable;
            col10.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col10);

            DataGridViewCheckBoxColumn col11 = new DataGridViewCheckBoxColumn();
            col11.Width = 30;
            col11.HeaderText = "Error";
            col11.SortMode = DataGridViewColumnSortMode.NotSortable;
            col11.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col11);

            DataGridViewCheckBoxColumn col12 = new DataGridViewCheckBoxColumn();
            col12.Width = 30;
            col12.HeaderText = "Ins. Fonts";
            col12.SortMode = DataGridViewColumnSortMode.NotSortable;
            col12.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col12);

            DataGridViewCheckBoxColumn col13 = new DataGridViewCheckBoxColumn();
            col13.Width = 30;
            col13.HeaderText = "Print";
            col13.SortMode = DataGridViewColumnSortMode.NotSortable;
            col13.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col13);

            DataGridViewCheckBoxColumn col14 = new DataGridViewCheckBoxColumn();
            col14.Width = 30;
            col14.HeaderText = "อบรม";
            col14.SortMode = DataGridViewColumnSortMode.NotSortable;
            col14.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col14);

            DataGridViewCheckBoxColumn col15 = new DataGridViewCheckBoxColumn();
            col15.Width = 30;
            col15.HeaderText = "สินค้า";
            col15.SortMode = DataGridViewColumnSortMode.NotSortable;
            col15.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col15);

            DataGridViewCheckBoxColumn col16 = new DataGridViewCheckBoxColumn();
            col16.Width = 30;
            col16.HeaderText = "Form Rep.";
            col16.SortMode = DataGridViewColumnSortMode.NotSortable;
            col16.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col16);

            DataGridViewCheckBoxColumn col17 = new DataGridViewCheckBoxColumn();
            col17.Width = 30;
            col17.HeaderText = "Rep> Excel";
            col17.SortMode = DataGridViewColumnSortMode.NotSortable;
            col17.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col17);

            DataGridViewCheckBoxColumn col18 = new DataGridViewCheckBoxColumn();
            col18.Width = 30;
            col18.HeaderText = "สร้างงบ";
            col18.SortMode = DataGridViewColumnSortMode.NotSortable;
            col18.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col18);

            DataGridViewCheckBoxColumn col19 = new DataGridViewCheckBoxColumn();
            col19.Width = 30;
            col19.HeaderText = "ท/ส. ค่าเสื่อม";
            col19.SortMode = DataGridViewColumnSortMode.NotSortable;
            col19.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col19);

            DataGridViewCheckBoxColumn col20 = new DataGridViewCheckBoxColumn();
            col20.Width = 30;
            col20.HeaderText = "Se cure";
            col20.SortMode = DataGridViewColumnSortMode.NotSortable;
            col20.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col20);

            DataGridViewCheckBoxColumn col21 = new DataGridViewCheckBoxColumn();
            col21.Width = 30;
            col21.HeaderText = "Year End";
            col21.SortMode = DataGridViewColumnSortMode.NotSortable;
            col21.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col21);

            DataGridViewCheckBoxColumn col22 = new DataGridViewCheckBoxColumn();
            col22.Width = 50;
            col22.HeaderText = "วันที่ ไม่อยู่ในงวด";
            col22.SortMode = DataGridViewColumnSortMode.NotSortable;
            col22.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col22);

            DataGridViewCheckBoxColumn col23 = new DataGridViewCheckBoxColumn();
            col23.Width = 30;
            col23.HeaderText = "Mail รอสาย";
            col23.SortMode = DataGridViewColumnSortMode.NotSortable;
            col23.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col23);

            DataGridViewTextBoxColumn col24 = new DataGridViewTextBoxColumn();
            col24.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col24.HeaderText = "ปัญหาอื่น ๆ";
            col24.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col24);

            DataGridViewButtonColumn col25 = new DataGridViewButtonColumn();
            col25.Width = 60;
            col25.HeaderText = "Comment/ Complain";
            col25.SortMode = DataGridViewColumnSortMode.NotSortable;
            col25.HeaderCell.Style.Font = new Font("tahoma", 8f);
            this.dgvNote.Columns.Add(col25);
            #endregion Create Columns

            int count = 0;

            this.note_list.Clear();
            foreach (SupportNote snote in this.supportnote_list)
            {
                total_time += TimeSpan.Parse(snote.duration);
                work_time += (snote.is_break == "N" ? TimeSpan.Parse(snote.duration) : TimeSpan.Parse("0:0:0"));
                break_time += (snote.is_break == "Y" ? TimeSpan.Parse(snote.duration) : TimeSpan.Parse("0:0:0"));

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

                int r = this.dgvNote.Rows.Add();
                this.dgvNote.Rows[r].Tag = note;
                this.SetRowBackground(this.dgvNote.Rows[r]);

                this.dgvNote.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvNote.Rows[r].Cells[0].Value = note.id;
                
                this.dgvNote.Rows[r].Cells[1].ValueType = typeof(int);
                this.dgvNote.Rows[r].Cells[1].Value = note.seq;

                this.dgvNote.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[2].Value = note.users_name;
                
                this.dgvNote.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[3].Value = note.date;

                this.dgvNote.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[4].Value = note.start_time;

                this.dgvNote.Rows[r].Cells[5].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[5].Value = note.end_time;

                this.dgvNote.Rows[r].Cells[6].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[6].Value = note.duration;

                this.dgvNote.Rows[r].Cells[7].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[7].Value = note.sernum;

                this.dgvNote.Rows[r].Cells[8].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[8].Value = note.contact;

                this.dgvNote.Rows[r].Cells[9].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[9].Value = note.map_drive;

                this.dgvNote.Rows[r].Cells[10].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[10].Value = note.install;

                this.dgvNote.Rows[r].Cells[11].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[11].Value = note.error;

                this.dgvNote.Rows[r].Cells[12].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[12].Value = note.fonts;

                this.dgvNote.Rows[r].Cells[13].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[13].Value = note.print;

                this.dgvNote.Rows[r].Cells[14].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[14].Value = note.training;

                this.dgvNote.Rows[r].Cells[15].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[15].Value = note.stock;

                this.dgvNote.Rows[r].Cells[16].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[16].Value = note.form;

                this.dgvNote.Rows[r].Cells[17].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[17].Value = note.rep_excel;

                this.dgvNote.Rows[r].Cells[18].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[18].Value = note.statement;

                this.dgvNote.Rows[r].Cells[19].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[19].Value = note.asset;

                this.dgvNote.Rows[r].Cells[20].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[20].Value = note.secure;

                this.dgvNote.Rows[r].Cells[21].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[21].Value = note.year_end;

                this.dgvNote.Rows[r].Cells[22].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[22].Value = note.period;

                this.dgvNote.Rows[r].Cells[23].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[23].Value = note.mail_wait;

                this.dgvNote.Rows[r].Cells[24].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[24].Value = note.remark;

                this.dgvNote.Rows[r].Cells[25].Value = "...";
            }

            this.toolStripInfo.Text = "รวมเวลาปฏิบัติงาน : " + total_time.ToString() + " ( รับสาย = " + work_time.ToString() + ", พักสาย = " + break_time.ToString() + " )";


            foreach (DataGridViewColumn col in this.dgvNote.Columns)
            {
                col.HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_BROWN;
            }
            this.dgvNote.Columns[this.sorted_column].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_ACTIVE_BROWN;

            if (this.dgvNote.Rows.Count > 0)
            {
                this.dgvNote.Focus();
                if (this.supportnote_list.Find(t => t.id == selected_row_id) != null)
                {
                    this.dgvNote.Rows[this.supportnote_list.FindIndex(t => t.id == selected_row_id)].Cells[1].Selected = true;
                }
            }
        }

        private void SetRowBackground(DataGridViewRow row)
        {
            if (row.Tag is Note)
            {
                if (this.supportnotecomment_list.Find(t => t.note_id == ((Note)row.Tag).id) != null) // Has comment/complain
                {
                    if (this.supportnotecomment_list.Find(t => t.note_id == ((Note)row.Tag).id).type == (int)CommentWindow.COMMENT_TYPE.COMMENT) // Is comment
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            row.Cells[cell.ColumnIndex].Style.BackColor = Color.FromArgb(198,219,255);
                            row.Cells[cell.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(198, 219, 255);
                            row.Cells[cell.ColumnIndex].Style.ForeColor = Color.Black;
                            row.Cells[cell.ColumnIndex].Style.SelectionForeColor = Color.Black;
                        }
                    }
                    else if (this.supportnotecomment_list.Find(t => t.note_id == ((Note)row.Tag).id).type == (int)CommentWindow.COMMENT_TYPE.COMPLAIN) // Is complain
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            row.Cells[cell.ColumnIndex].Style.BackColor = Color.FromArgb(255, 220, 224);
                            row.Cells[cell.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(255, 220, 224);
                            row.Cells[cell.ColumnIndex].Style.ForeColor = Color.Black;
                            row.Cells[cell.ColumnIndex].Style.SelectionForeColor = Color.Black;
                        }
                    }
                }
                else // No comment, No complain
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        row.Cells[cell.ColumnIndex].Style.BackColor = (((Note)row.Tag).is_break == "Y" ? ColorResource.DISABLE_ROW_BACKGROUND : Color.White);
                        row.Cells[cell.ColumnIndex].Style.SelectionBackColor = (((Note)row.Tag).is_break == "Y" ? ColorResource.DISABLE_ROW_BACKGROUND : Color.White);
                        row.Cells[cell.ColumnIndex].Style.ForeColor = (((Note)row.Tag).is_break == "Y" ? Color.Gray : Color.Black);
                        row.Cells[cell.ColumnIndex].Style.SelectionForeColor = (((Note)row.Tag).is_break == "Y" ? Color.Gray : Color.Black);
                    }
                }
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
            this.btnViewNote.PerformClick();
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Comma separated value | *.csv";
            dlg.DefaultExt = "csv";
            dlg.RestoreDirectory = true;
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
            string remark = (string)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[24].Value;

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
            int note_id = ((Note)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Tag).id;
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
                    this.GetNote(note_id);
                    this.ClearInlineForm();
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
                Rectangle cell24_rect = this.dgvNote.GetCellDisplayRectangle(24, this.dgvNote.CurrentCell.RowIndex, true);
                Console.WriteLine(" >>>> current cell.row_index : " + this.dgvNote.CurrentCell.RowIndex.ToString());
                ct.SetBounds(cell24_rect.X + 3, cell24_rect.Y + 1, cell24_rect.Width - 1, cell24_rect.Height - 2);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                //return true;
                if (this.form_mode == FORM_MODE.EDIT)
                {
                    if (this.dgvNote.Parent.Controls.Find("txt_inline_remark", true).Length > 0)
                    {
                        if (((CustomTextBox)this.dgvNote.Parent.Controls.Find("txt_inline_remark", true)[0]).textBox1.Focused)
                        {
                            this.toolStripSave.PerformClick();
                            return true;
                        }
                    }
                }
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
