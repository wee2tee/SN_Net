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
    public partial class SupportNoteWindow : Form
    {
        public MainForm main_form;
        public SnWindow parent_window;
        private List<SupportNote> note_list = new List<SupportNote>();
        private List<Istab> probcod;
        private SupportNote note;
        public Serial serial = null;
        private Timer tm = new Timer();
        private FORM_MODE form_mode;
        public enum FORM_MODE
        {
            READ,
            ADD,
            EDIT,
            BREAK,
            EDIT_BREAK,
            PROCESSING
        }
        
        public SupportNoteWindow()
        {
            InitializeComponent();
        }

        public SupportNoteWindow(SnWindow parent_window)
            : this()
        {
            this.main_form = parent_window.main_form;
            this.parent_window = parent_window;
            this.FormRead();
        }

        public SupportNoteWindow(SnWindow parent_window, Serial serial)
            : this()
        {
            this.main_form = parent_window.main_form;
            this.parent_window = parent_window;
            this.serial = serial;
        }

        private void SupportNote_Load(object sender, EventArgs e)
        {
            this.probcod = this.main_form.data_resource.LIST_PROBLEM_CODE.Where(t => t.typcod != "RG").ToList<Istab>();
            this.txtDummy.Width = 0;
            this.PrepareControl();
            this.GetNote();
        }

        private void PrepareControl()
        {
            this.lblCompnam.Text = "";
            this.lblCompnam2.Text = "";

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
            this.chYearEnd.Tag = SupportNote.NOTE_PROBLEM.YEAR_END;
            #endregion Attaching Checkbox Tag

            #region Attaching Radio Button Tag
            this.rbToilet.Tag = SupportNote.BREAK_REASON.TOILET;
            this.rbQt.Tag = SupportNote.BREAK_REASON.QT;
            this.rbMeetCust.Tag = SupportNote.BREAK_REASON.MEET_CUST;
            this.rbTraining.Tag = SupportNote.BREAK_REASON.TRAINING;
            this.rbCorrectData.Tag = SupportNote.BREAK_REASON.CORRECT_DATA;
            #endregion Attaching Radio Button Tag

            #region Add Support Code to cbSupportCode (ComboBox)
            List<Users> support_users = new List<Users>();
            
            CRUDResult get_support_users = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_support_users");
            ServerResult sr_support_users = JsonConvert.DeserializeObject<ServerResult>(get_support_users.data);
            
            if (sr_support_users.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                foreach (Users u in sr_support_users.users)
                {
                    this.cbSupportCode.Items.Add(new ComboboxItem(u.username, 0, u.username));
                }
            }
            #endregion Add Support Code to cbSupportCode (ComboBox)

            #region Set Selected Support Code to current loged_in_user_name
            if (this.main_form.G.loged_in_user_level == GlobalVar.USER_GROUP_SUPPORT)
            {
                this.cbSupportCode.Text = this.main_form.G.loged_in_user_name;
                this.cbSupportCode.Enabled = false;
            }
            else
            {
                this.cbSupportCode.SelectedIndex = 0;
            }
            #endregion Set Selected Support Code to current loged_in_user_name

            #region Set Selection Probcod in cbProbcod
            foreach (Istab prob in this.probcod)
            {
                this.cbProbcod.Items.Add(new ComboboxItem(prob.typcod + " : " + prob.typdes_th, 0, prob.typcod));
            }
            #endregion Set Selection Probcod in cbProbcod

            #region Set Working date to current date
            this.dtWorkDate.dateTimePicker1.Value = DateTime.Now;
            #endregion Set Working date to current date

            #region txtSernum Leave Event
            this.txtSernum.Leave += delegate
            {
                if (this.txtSernum.Texts.Length > 0)
                {
                    CRUDResult get_exist_sernum = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/check_sn_exist&sernum=" + this.txtSernum.Texts);
                    ServerResult sr_exist_sernum = JsonConvert.DeserializeObject<ServerResult>(get_exist_sernum.data);

                    if (sr_exist_sernum.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        if (sr_exist_sernum.serial.Count > 0)
                        {
                            this.btnViewDetail.Enabled = true;
                            this.lblCompnam.Text = sr_exist_sernum.serial[0].compnam;
                            this.serial = sr_exist_sernum.serial[0];
                        }
                        else
                        {
                            this.btnViewDetail.Enabled = false;
                            this.txtSernum.Focus();
                            this.lblCompnam.Text = "";
                        }
                    }
                }
            };
            #endregion txtSernum Leave Event

            #region txtSernum2 Leave Event
            //this.txtSernum2.Leave += delegate
            //{
            //    if (this.txtSernum2.Texts.Length > 0)
            //    {
            //        CRUDResult get_exist_sernum = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/check_sn_exist&sernum=" + this.txtSernum2.Texts);
            //        ServerResult sr_exist_sernum = JsonConvert.DeserializeObject<ServerResult>(get_exist_sernum.data);

            //        if (sr_exist_sernum.result == ServerResult.SERVER_RESULT_SUCCESS)
            //        {
            //            if (sr_exist_sernum.serial.Count > 0)
            //            {
            //                this.lblCompnam2.Text = sr_exist_sernum.serial[0].compnam;
            //            }
            //            else
            //            {
            //                this.txtSernum2.Focus();
            //                this.lblCompnam2.Text = "";
            //            }
            //        }
            //    }
            //};
            #endregion txtSernum2 Leave Event

            #region chAlsoF8 enable when txtRemark is not empty
            this.txtRemark.TextChanged += delegate
            {
                if (this.txtRemark.Text.Length > 0 && this.serial != null)
                {
                    this.chAlsoF8.Enabled = true;
                }
                else
                {
                    this.chAlsoF8.Enabled = false;
                    this.chAlsoF8.CheckState = CheckState.Unchecked;
                }
            };
            #endregion chAlsoF8 enable when txtRemark is not empty

            #region Enable/Disable Browse Probcod depend on chAlsoF8
            this.chAlsoF8.CheckedChanged += delegate
            {
                if (this.chAlsoF8.Checked)
                {
                    this.cbProbcod.Enabled = true;
                }
                else
                {
                    this.cbProbcod.Enabled = false;
                }
            };
            #endregion Enable/Disable Browse Probcod depend on chAlsoF8

            #region DobleClick cell to edit
            this.dgvNote.CellDoubleClick += delegate
            {
                this.toolStripEdit.PerformClick();
            };
            #endregion DobleClick cell to edit

            #region Prevent change tab
            this.tabControl1.Deselecting += delegate(object sender, TabControlCancelEventArgs e)
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT || this.form_mode == FORM_MODE.BREAK || this.form_mode == FORM_MODE.EDIT_BREAK || this.form_mode == FORM_MODE.PROCESSING)
                {
                    e.Cancel = true;
                }
            };
            #endregion Prevent change tab

            this.tm.Interval = 1000;
            this.tm.Tick += delegate
            {
                TimeSpan ts = new TimeSpan();

                if (this.tabControl1.SelectedTab == this.tabPage1)
                {
                    this.dtEndTime.Value = DateTime.Now;
                    ts = new TimeSpan(this.dtEndTime.Value.Hour - this.dtStartTime.Value.Hour, this.dtEndTime.Value.Minute - this.dtStartTime.Value.Minute, this.dtEndTime.Value.Second - this.dtStartTime.Value.Second);
                }
                else if (this.tabControl1.SelectedTab == this.tabPage2)
                {
                    this.dtBreakEnd.Value = DateTime.Now;
                    ts = new TimeSpan(this.dtBreakEnd.Value.Hour - this.dtBreakStart.Value.Hour, this.dtBreakEnd.Value.Minute - this.dtBreakStart.Value.Minute, this.dtBreakEnd.Value.Second - this.dtBreakStart.Value.Second);
                }

                this.main_form.lblTimeDuration.Text = ts.ToString();
            };
        }

        private void GetNote()
        {
            string support_code = ((ComboboxItem)this.cbSupportCode.SelectedItem).string_value;
            string start_date = this.dtWorkDate.TextsMysql;

            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "supportnote/get_note&support_code=" + support_code + "&start_date=" + start_date + "&end_date=" + start_date);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.note_list = sr.support_note;
                this.FillDataGrid();
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void FillDataGrid()
        {
            this.dgvNote.Rows.Clear();
            this.dgvNote.Columns.Clear();
            this.dgvNote.EnableHeadersVisualStyles = false;

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.HeaderText = "ID";
            col0.Visible = false;
            col0.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "ลำดับ";
            col1.Width = 40;
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "รับสาย";
            col2.Width = 65;
            col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "วางสาย";
            col3.Width = 65;
            col3.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "ระยะเวลา";
            col4.Width = 65;
            col4.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.HeaderText = "S/N";
            col5.Width = 120;
            col5.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col5);

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.HeaderText = "ชื่อลูกค้า";
            col6.Width = 160;
            col6.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvNote.Columns.Add(col6);

            DataGridViewCheckBoxColumn col7 = new DataGridViewCheckBoxColumn();
            col7.HeaderText = "Map Drive";
            col7.Width = 30;
            col7.SortMode = DataGridViewColumnSortMode.NotSortable;
            col7.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col7);

            DataGridViewCheckBoxColumn col8 = new DataGridViewCheckBoxColumn();
            col8.HeaderText = "Ins. /Up";
            col8.Width = 30;
            col8.SortMode = DataGridViewColumnSortMode.NotSortable;
            col8.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col8);

            DataGridViewCheckBoxColumn col9 = new DataGridViewCheckBoxColumn();
            col9.HeaderText = "Error";
            col9.Width = 30;
            col9.SortMode = DataGridViewColumnSortMode.NotSortable;
            col9.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col9);

            DataGridViewCheckBoxColumn col10 = new DataGridViewCheckBoxColumn();
            col10.HeaderText = "Ins. Fonts";
            col10.Width = 30;
            col10.SortMode = DataGridViewColumnSortMode.NotSortable;
            col10.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col10);

            DataGridViewCheckBoxColumn col11 = new DataGridViewCheckBoxColumn();
            col11.HeaderText = "Print";
            col11.Width = 30;
            col11.SortMode = DataGridViewColumnSortMode.NotSortable;
            col11.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col11);

            DataGridViewCheckBoxColumn col12 = new DataGridViewCheckBoxColumn();
            col12.HeaderText = "อบรม";
            col12.Width = 30;
            col12.SortMode = DataGridViewColumnSortMode.NotSortable;
            col12.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col12);

            DataGridViewCheckBoxColumn col13 = new DataGridViewCheckBoxColumn();
            col13.HeaderText = "สินค้า";
            col13.Width = 30;
            col13.SortMode = DataGridViewColumnSortMode.NotSortable;
            col13.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col13);

            DataGridViewCheckBoxColumn col14 = new DataGridViewCheckBoxColumn();
            col14.HeaderText = "Form Rep.";
            col14.Width = 30;
            col14.SortMode = DataGridViewColumnSortMode.NotSortable;
            col14.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col14);

            DataGridViewCheckBoxColumn col15 = new DataGridViewCheckBoxColumn();
            col15.HeaderText = "Rep> Excel";
            col15.Width = 30;
            col15.SortMode = DataGridViewColumnSortMode.NotSortable;
            col15.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col15);

            DataGridViewCheckBoxColumn col16 = new DataGridViewCheckBoxColumn();
            col16.HeaderText = "สร้างงบ";
            col16.Width = 30;
            col16.SortMode = DataGridViewColumnSortMode.NotSortable;
            col16.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col16);

            DataGridViewCheckBoxColumn col17 = new DataGridViewCheckBoxColumn();
            col17.HeaderText = "ท/ส. ค่าเสื่อม";
            col17.Width = 30;
            col17.SortMode = DataGridViewColumnSortMode.NotSortable;
            col17.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col17);

            DataGridViewCheckBoxColumn col18 = new DataGridViewCheckBoxColumn();
            col18.HeaderText = "Se cure";
            col18.Width = 30;
            col18.SortMode = DataGridViewColumnSortMode.NotSortable;
            col18.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col18);

            DataGridViewCheckBoxColumn col19 = new DataGridViewCheckBoxColumn();
            col19.HeaderText = "Year End";
            col19.Width = 30;
            col19.SortMode = DataGridViewColumnSortMode.NotSortable;
            col19.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col19);

            DataGridViewCheckBoxColumn col20 = new DataGridViewCheckBoxColumn();
            col20.HeaderText = "วันที่ ไม่อยู่ในงวด";
            col20.Width = 50;
            col20.SortMode = DataGridViewColumnSortMode.NotSortable;
            col20.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col20);

            DataGridViewCheckBoxColumn col21 = new DataGridViewCheckBoxColumn();
            col21.HeaderText = "Mail รอสาย";
            col21.Width = 30;
            col21.SortMode = DataGridViewColumnSortMode.NotSortable;
            col21.HeaderCell.Style.Font = new Font("tahoma", 7f);
            this.dgvNote.Columns.Add(col21);

            DataGridViewTextBoxColumn col22 = new DataGridViewTextBoxColumn();
            col22.HeaderText = "ปัญหาอื่น ๆ";
            col22.SortMode = DataGridViewColumnSortMode.NotSortable;
            col22.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvNote.Columns.Add(col22);

            int cnt = 0;
            foreach (SupportNote note in this.note_list)
            {
                int r = this.dgvNote.Rows.Add();
                this.dgvNote.Rows[r].Tag = note;
                this.dgvNote.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvNote.Rows[r].Cells[0].Value = note.id;
                this.dgvNote.Rows[r].Cells[0].Tag = new DataRowIntention(DataRowIntention.TO_DO.READ);

                this.dgvNote.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                cnt += (note.is_break != "Y" ? 1 : 0);
                this.dgvNote.Rows[r].Cells[1].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[1].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[1].Value = (note.is_break != "Y" ? cnt.ToString() : "");

                this.dgvNote.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[2].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[2].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[2].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[2].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[2].Value = note.start_time;

                this.dgvNote.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[3].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[3].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[3].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[3].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[3].Value = note.end_time;

                this.dgvNote.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[4].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[4].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[4].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[4].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[4].Value = note.duration;

                this.dgvNote.Rows[r].Cells[5].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[5].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[5].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[5].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[5].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[5].Value = note.sernum;

                this.dgvNote.Rows[r].Cells[6].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[6].Style.Alignment = (note.is_break != "Y" ? DataGridViewContentAlignment.MiddleLeft : DataGridViewContentAlignment.MiddleCenter);
                this.dgvNote.Rows[r].Cells[6].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[6].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[6].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[6].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[6].Value = (note.is_break != "Y" ? note.contact : this.ReadableBreakReason(note.reason));

                this.dgvNote.Rows[r].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[7].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[7].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[7].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[7].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.MAP_DRIVE.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[8].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[8].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[8].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[8].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.INSTALL_UPDATE.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[9].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[9].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[9].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[9].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.ERROR.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[10].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[10].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[10].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[10].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.FONTS.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[11].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[11].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[11].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[11].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.PRINT.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[12].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[12].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[12].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[12].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.TRAINING.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[13].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[13].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[13].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[13].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.STOCK.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[14].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[14].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[14].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[14].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.FORM.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[15].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[15].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[15].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[15].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[15].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.REPORT_EXCEL.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[16].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[16].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[16].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[16].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[16].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.STATEMENT.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[17].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[17].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[17].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[17].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[17].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.ASSETS.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[18].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[18].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[18].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[18].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[18].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.SECURE.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[19].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[19].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[19].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[19].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[19].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.YEAR_END.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[20].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[20].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[20].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[20].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[20].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.PERIOD.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[21].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Rows[r].Cells[21].ValueType = typeof(bool);
                this.dgvNote.Rows[r].Cells[21].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[21].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[21].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.MAIL_WAIT.FormatNoteProblem()) ? true : false);

                this.dgvNote.Rows[r].Cells[22].ValueType = typeof(string);
                this.dgvNote.Rows[r].Cells[22].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[22].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                this.dgvNote.Rows[r].Cells[22].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[22].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                this.dgvNote.Rows[r].Cells[22].Value = note.remark;
            }
            this.dgvNote.DrawLineEffect();
        }

        #region FORM MODE
        private void FormRead()
        {
            this.form_mode = FORM_MODE.READ;
            //this.txtDummy.Focus();
            this.dgvNote.Focus();
            this.toolStripProcessing.Visible = false;

            #region TOOLSTRIP
            this.toolStripAdd.Enabled = true;
            this.toolStripEdit.Enabled = true;
            this.toolStripBreak.Enabled = true;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = false;
            this.txtSernum.Read_Only = true;
            this.txtContact.ReadOnly = true;
            this.chAlsoF8.Enabled = false;
            this.chAssets.Enabled = false;
            this.chError.Enabled = false;
            this.chFonts.Enabled = false;
            this.chForm.Enabled = false;
            this.chInstall.Enabled = false;
            this.chMailWait.Enabled = false;
            this.chMapDrive.Enabled = false;
            this.chPeriod.Enabled = false;
            this.chPrint.Enabled = false;
            this.chRepExcel.Enabled = false;
            this.chSecure.Enabled = false;
            this.chStatement.Enabled = false;
            this.chStock.Enabled = false;
            this.chYearEnd.Enabled = false;
            this.txtRemark.Enabled = false;
            this.btnViewNote.Enabled = true;
            this.btnViewDetail.Enabled = false;
            #endregion FORM CONTROL

            this.splitContainer1.SplitterDistance = 78;
            this.tabControl1.Height = 0;
            this.main_form.lblTimeDuration.Visible = false;
            this.dgvNote.Enabled = true;
        }

        private void FormAdd()
        {
            this.form_mode = FORM_MODE.ADD;
            this.txtDummy.Focus();
            this.parent_window.btnSupportNote.Enabled = false;
            this.toolStripProcessing.Visible = false;

            #region TOOLSTRIP
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripBreak.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = false;
            this.txtContact.ReadOnly = false;
            this.chAssets.Enabled = true;
            this.chError.Enabled = true;
            this.chFonts.Enabled = true;
            this.chForm.Enabled = true;
            this.chInstall.Enabled = true;
            this.chMailWait.Enabled = true;
            this.chMapDrive.Enabled = true;
            this.chPeriod.Enabled = true;
            this.chPrint.Enabled = true;
            this.chRepExcel.Enabled = true;
            this.chSecure.Enabled = true;
            this.chStatement.Enabled = true;
            this.chStock.Enabled = true;
            this.chYearEnd.Enabled = true;
            this.txtRemark.Enabled = true;
            this.btnViewNote.Enabled = false;
            #endregion FORM CONTROL

            this.tabControl1.Height = 220;
            this.splitContainer1.SplitterDistance = 302;
            this.dgvNote.Enabled = false;
        }

        private void FormEdit()
        {
            this.form_mode = FORM_MODE.EDIT;
            this.txtDummy.Focus();
            this.parent_window.btnSupportNote.Enabled = false;
            this.toolStripProcessing.Visible = false;

            #region TOOLSTRIP
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripBreak.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = true;
            this.txtContact.ReadOnly = false;
            this.chAssets.Enabled = true;
            this.chError.Enabled = true;
            this.chFonts.Enabled = true;
            this.chForm.Enabled = true;
            this.chInstall.Enabled = true;
            this.chMailWait.Enabled = true;
            this.chMapDrive.Enabled = true;
            this.chPeriod.Enabled = true;
            this.chPrint.Enabled = true;
            this.chRepExcel.Enabled = true;
            this.chSecure.Enabled = true;
            this.chStatement.Enabled = true;
            this.chStock.Enabled = true;
            this.chYearEnd.Enabled = true;
            this.txtRemark.Enabled = true;
            this.btnViewNote.Enabled = false;
            #endregion FORM CONTROL

            this.tabControl1.Height = 220;
            this.splitContainer1.SplitterDistance = 302;
            this.dgvNote.Enabled = false;
        }

        private void FormBreak()
        {
            this.form_mode = FORM_MODE.BREAK;
            this.txtDummy.Focus();
            this.parent_window.btnSupportNote.Enabled = false;
            this.toolStripProcessing.Visible = false;

            #region TOOLSTRIP
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripBreak.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            #endregion TOOLSTRIP

            #region FORM CONTROL First Tab
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = true;
            this.txtContact.ReadOnly = true;
            this.chAssets.Enabled = false;
            this.chError.Enabled = false;
            this.chFonts.Enabled = false;
            this.chForm.Enabled = false;
            this.chInstall.Enabled = false;
            this.chMailWait.Enabled = false;
            this.chMapDrive.Enabled = false;
            this.chPeriod.Enabled = false;
            this.chPrint.Enabled = false;
            this.chRepExcel.Enabled = false;
            this.chSecure.Enabled = false;
            this.chStatement.Enabled = false;
            this.chStock.Enabled = false;
            this.chYearEnd.Enabled = false;
            this.txtRemark.Enabled = false;
            this.btnViewNote.Enabled = false;
            #endregion FORM CONTROL First Tab

            this.tabControl1.Height = 220;
            this.splitContainer1.SplitterDistance = 302;
            this.dgvNote.Enabled = false;
        }

        private void FormEditBreak()
        {
            this.form_mode = FORM_MODE.EDIT_BREAK;
            this.txtDummy.Focus();
            this.parent_window.btnSupportNote.Enabled = false;
            this.toolStripProcessing.Visible = false;

            #region TOOLSTRIP
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripBreak.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = true;
            this.txtContact.ReadOnly = false;
            this.chAssets.Enabled = true;
            this.chError.Enabled = true;
            this.chFonts.Enabled = true;
            this.chForm.Enabled = true;
            this.chInstall.Enabled = true;
            this.chMailWait.Enabled = true;
            this.chMapDrive.Enabled = true;
            this.chPeriod.Enabled = true;
            this.chPrint.Enabled = true;
            this.chRepExcel.Enabled = true;
            this.chSecure.Enabled = true;
            this.chStatement.Enabled = true;
            this.chStock.Enabled = true;
            this.chYearEnd.Enabled = true;
            this.txtRemark.Enabled = true;
            this.btnViewNote.Enabled = false;
            #endregion FORM CONTROL

            this.tabControl1.Height = 220;
            this.splitContainer1.SplitterDistance = 302;
            this.dgvNote.Enabled = false;
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;
            this.txtDummy.Focus();
            this.toolStripProcessing.Visible = true;

            #region TOOLSTRIP
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripBreak.Enabled = false;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = true;
            this.txtContact.ReadOnly = true;
            this.chAlsoF8.Enabled = false;
            this.chAssets.Enabled = false;
            this.chError.Enabled = false;
            this.chFonts.Enabled = false;
            this.chForm.Enabled = false;
            this.chInstall.Enabled = false;
            this.chMailWait.Enabled = false;
            this.chMapDrive.Enabled = false;
            this.chPeriod.Enabled = false;
            this.chPrint.Enabled = false;
            this.chRepExcel.Enabled = false;
            this.chSecure.Enabled = false;
            this.chStatement.Enabled = false;
            this.chStock.Enabled = false;
            this.chYearEnd.Enabled = false;
            this.txtRemark.Enabled = false;
            this.btnViewNote.Enabled = false;
            #endregion FORM CONTROL

            this.dgvNote.Enabled = false;
        }
        #endregion FORM MODE

        private void ClearForm()
        {
            this.parent_window.btnSupportNote.Enabled = true;
            this.serial = null;
            this.note = null;
            this.splitContainer1.SplitterDistance = 42;

            #region First tab
            this.txtSernum.Texts = "";
            this.lblCompnam.Text = "";
            this.txtContact.Texts = "";
            this.chAlsoF8.CheckState = CheckState.Unchecked;
            this.chAssets.CheckState = CheckState.Unchecked;
            this.chError.CheckState = CheckState.Unchecked;
            this.chFonts.CheckState = CheckState.Unchecked;
            this.chForm.CheckState = CheckState.Unchecked;
            this.chInstall.CheckState = CheckState.Unchecked;
            this.chMailWait.CheckState = CheckState.Unchecked;
            this.chMapDrive.CheckState = CheckState.Unchecked;
            this.chPeriod.CheckState = CheckState.Unchecked;
            this.chPrint.CheckState = CheckState.Unchecked;
            this.chRepExcel.CheckState = CheckState.Unchecked;
            this.chSecure.CheckState = CheckState.Unchecked;
            this.chStatement.CheckState = CheckState.Unchecked;
            this.chStock.CheckState = CheckState.Unchecked;
            this.chYearEnd.CheckState = CheckState.Unchecked;
            this.txtRemark.Text = "";
            #endregion First tab

            #region Second tab
            this.rbToilet.Checked = true;
            this.rbQt.Checked = false;
            this.rbMeetCust.Checked = false;
            this.rbTraining.Checked = false;
            this.rbCorrectData.Checked = false;
            this.txtSernum2.Texts = "";
            this.lblCompnam2.Text = "";
            this.txtRemark2.Text = "";
            #endregion Second tab

            if (this.tm != null)
            {
                this.tm.Stop();
                this.tm.Enabled = false;
            }
            this.main_form.lblTimeDuration.Text = TimeSpan.Zero.ToString();
        }

        public void BeginDuration() // start counting duration
        {
            this.dtStartTime.Value = DateTime.Now;
            this.dtEndTime.Value = DateTime.Now;
            this.main_form.lblTimeDuration.Visible = true;
            
            // start counting duration
            this.tm.Enabled = true;
            this.tm.Start();
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            if (this.dtWorkDate.dateTimePicker1.Value != DateTime.Now)
            {
                this.dtWorkDate.dateTimePicker1.Value = DateTime.Now;
                this.GetNote();
            }
            this.cbProbcod.SelectedIndex = this.probcod.FindIndex(t => t.typcod == "--");
            this.FormAdd();
            this.BeginDuration();
            this.txtSernum.Focus();
        }

        private void toolStripBreak_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            if (this.dtWorkDate.dateTimePicker1.Value != DateTime.Now)
            {
                this.dtWorkDate.dateTimePicker1.Value = DateTime.Now;
                this.GetNote();
            }
            this.dtBreakStart.Value = DateTime.Now;
            this.dtBreakEnd.Value = DateTime.Now;
            this.FormBreak();
            this.rbToilet.Focus();
            this.BeginDuration();
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvNote.CurrentCell != null && (this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex]).Tag is SupportNote)
            {
                this.note = (SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Tag;
                
                if (((SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Tag).is_break != "Y")
                {
                    this.tabControl1.SelectedTab = this.tabPage1;
                    this.FormEdit();
                    this.cbProbcod.SelectedIndex = this.probcod.FindIndex(t => t.typcod == "--");

                    CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/check_sn_exist&sernum=" + this.note.sernum);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        if (sr.serial.Count<Serial>() > 0)
                        {
                            this.serial = sr.serial[0];
                        }
                        else
                        {
                            this.serial = null;
                        }
                    }
                    else
                    {
                        this.serial = null;
                    }
                    this.txtSernum.Texts = this.note.sernum;
                    this.dtStartTime.Text = this.note.start_time;
                    this.dtEndTime.Text = this.note.end_time;
                    this.txtRemark.Text = this.note.remark;
                    this.txtContact.Texts = this.note.contact;
                    this.CheckedProblem(this.note.problem);
                    this.txtContact.Focus();
                }
                else
                {
                    this.tabControl1.SelectedTab = this.tabPage2;
                    this.FormEditBreak();
                    this.txtSernum2.Texts = this.note.sernum;
                    this.dtBreakStart.Text = this.note.start_time;
                    this.dtBreakEnd.Text = this.note.end_time;
                    this.txtRemark2.Text = this.note.remark;
                    this.rbToilet.Checked = (this.note.reason.Contains(SupportNote.BREAK_REASON.TOILET.FormatBreakReson()) ? true : false);
                    this.rbQt.Checked = (this.note.reason.Contains(SupportNote.BREAK_REASON.QT.FormatBreakReson()) ? true : false);
                    this.rbMeetCust.Checked = (this.note.reason.Contains(SupportNote.BREAK_REASON.MEET_CUST.FormatBreakReson()) ? true : false);
                    this.rbTraining.Checked = (this.note.reason.Contains(SupportNote.BREAK_REASON.TRAINING.FormatBreakReson()) ? true : false);
                    this.rbCorrectData.Checked = (this.note.reason.Contains(SupportNote.BREAK_REASON.CORRECT_DATA.FormatBreakReson()) ? true : false);
                    this.txtRemark2.Focus();
                }
            }
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show(StringResource.CONFIRM_CANCEL_ADD_EDIT, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                this.ClearForm();
                this.FormRead();
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.ADD)
            {
                this.SubmitAdd();
            }
            else if (this.form_mode == FORM_MODE.EDIT)
            {
                this.SubmitEdit();
            }
            else if (this.form_mode == FORM_MODE.BREAK)
            {
                this.SubmitBreak();   
            }
            else if (this.form_mode == FORM_MODE.EDIT_BREAK)
            {
                this.SubmitEditBreak();
            }
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            if (this.main_form.sn_wind != null)
            {
                if (this.main_form.sn_wind.serial.id != this.serial.id)
                {
                    this.main_form.sn_wind.serial = this.serial;
                    this.main_form.sn_wind.toolStripReload.PerformClick();
                }
                this.main_form.sn_wind.Activate();
            }
            else
            {
                SnWindow sn_wind = new SnWindow(this.main_form);
                this.main_form.sn_wind = sn_wind;
                sn_wind.MdiParent = this.main_form;
                sn_wind.Show();
            }
        }

        private void btnViewNote_Click(object sender, EventArgs e)
        {
            this.FormProcessing();
            bool get_success = false;
            string err_msg = "";
            string support_code = ((ComboboxItem)this.cbSupportCode.SelectedItem).string_value;
            string start_date = this.dtWorkDate.TextsMysql;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "supportnote/get_note&support_code=" + support_code + "&start_date=" + start_date + "&end_date=" + start_date);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.note_list = sr.support_note;
                    get_success = true;
                }
                else
                {
                    get_success = false;
                    err_msg = sr.message;
                }
            };

            worker.RunWorkerCompleted += delegate
            {
                if (get_success)
                {
                    this.FillDataGrid();
                    this.FormRead();
                }
                else
                {
                    this.FormRead();
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            };

            worker.RunWorkerAsync();
        }

        private void SubmitAdd()
        {
            this.FormProcessing();
            bool post_success = false;
            string err_msg = "";
            TimeSpan ts = new TimeSpan(this.dtEndTime.Value.Hour - this.dtStartTime.Value.Hour, this.dtEndTime.Value.Minute - this.dtStartTime.Value.Minute, this.dtEndTime.Value.Second - this.dtStartTime.Value.Second);

            string json_data = "{\"sernum\":\"" + (this.txtSernum.Texts.Replace("-", "").Replace(" ", "").Length > 0 ? this.txtSernum.Texts.cleanString() : "") + "\",";
            json_data += "\"contact\":\"" + this.txtContact.Texts.cleanString() + "\",";
            json_data += "\"start_time\":\"" + this.dtStartTime.Text + "\",";
            json_data += "\"end_time\":\"" + this.dtEndTime.Text + "\",";
            json_data += "\"duration\":\"" + ts.ToString().Substring(0, 8) + "\",";
            json_data += "\"problem\":\"" + this.GetProblemString() +"\",";
            json_data += "\"remark\":\"" + this.txtRemark.Text.cleanString() + "\",";
            json_data += "\"also_f8\":\"" + this.chAlsoF8.CheckState.ToYesOrNoString() + "\",";
            json_data += "\"probcod\":\"" + ((ComboboxItem)this.cbProbcod.SelectedItem).string_value + "\",";
            json_data += "\"is_break\":\"N\",";
            json_data += "\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\"}";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/create", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                }
                else
                {
                    err_msg = sr.message;
                    post_success = false;
                }
            };

            worker.RunWorkerCompleted += delegate
            {
                if (post_success)
                {
                    if (this.chAlsoF8.Checked)
                    {
                        this.main_form.sn_wind.loadProblemData();
                        this.main_form.sn_wind.fillInDatagrid();
                    }
                    this.ClearForm();
                    this.GetNote();
                    this.FormRead();
                }
                else
                {
                    this.FormAdd();
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.SubmitAdd();
                    }
                    else
                    {
                        this.ClearForm();
                        this.FormRead();
                    }
                }
            };

            worker.RunWorkerAsync();
        }

        private void SubmitEdit()
        {
            this.FormProcessing();
            bool post_success = false;
            string err_msg = "";

            string json_data = "{\"id\":" + this.note.id.ToString() + ",";
            json_data += "\"sernum\":\"" + this.txtSernum.Texts.cleanString() + "\",";
            json_data += "\"contact\":\"" + this.txtContact.Texts.cleanString() + "\",";
            json_data += "\"problem\":\"" + this.GetProblemString() + "\",";
            json_data += "\"remark\":\"" + this.txtRemark.Text.cleanString() + "\",";
            json_data += "\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\",";
            json_data += "\"also_f8\":\"" + this.chAlsoF8.CheckState.ToYesOrNoString() + "\",";
            json_data += "\"probcod\":\"" + ((ComboboxItem)this.cbProbcod.SelectedItem).string_value + "\"}";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/update", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                }
                else
                {
                    err_msg = sr.message;
                    post_success = false;
                }
            };

            worker.RunWorkerCompleted += delegate
            {
                if (post_success)
                {
                    if (this.chAlsoF8.Checked)
                    {
                        this.main_form.sn_wind.loadProblemData();
                        this.main_form.sn_wind.fillInDatagrid();
                    }
                    this.ClearForm();
                    this.GetNote();
                    this.FormRead();
                }
                else
                {
                    this.FormAdd();
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.SubmitEdit();
                    }
                    else
                    {
                        this.ClearForm();
                        this.FormRead();
                    }
                }
            };

            worker.RunWorkerAsync();
        }

        private void SubmitBreak()
        {
            this.FormProcessing();
            bool post_success = false;
            string err_msg = "";
            TimeSpan ts = new TimeSpan(this.dtBreakEnd.Value.Hour - this.dtBreakStart.Value.Hour, this.dtBreakEnd.Value.Minute - this.dtBreakStart.Value.Minute, this.dtBreakEnd.Value.Second - this.dtBreakStart.Value.Second);

            string json_data = "{\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\",";
            json_data += "\"start_time\":\"" + this.dtBreakStart.Text + "\",";
            json_data += "\"end_time\":\"" + this.dtBreakEnd.Text + "\",";
            json_data += "\"duration\":\"" + ts.ToString().Substring(0, 8) + "\",";
            json_data += "\"sernum\":\"" + (this.txtSernum2.Texts.Replace("-", "").Replace(" ", "").Length > 0 ? this.txtSernum2.Texts.cleanString() : "") + "\",";
            json_data += "\"reason\":\"" + this.GetBreakReason() + "\",";
            json_data += "\"remark\":\"" + this.txtRemark2.Text.cleanString() +"\",";
            json_data += "\"is_break\":\"Y\"}";

            Console.WriteLine(" >>> " + json_data);

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/create_break", json_data);
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
                    this.ClearForm();
                    this.GetNote();
                    this.FormRead();
                }
                else
                {
                    this.FormAdd();
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.SubmitBreak();
                    }
                    else
                    {
                        this.ClearForm();
                        this.FormRead();
                    }
                }
            };

            worker.RunWorkerAsync();
        }

        private void SubmitEditBreak()
        {
            this.FormProcessing();
            bool post_success = false;
            string err_msg = "";

            string json_data = "{\"id\":" + this.note.id.ToString() + ",";
            json_data += "\"sernum\":\"" + (this.txtSernum2.Texts.Replace("-", "").Replace(" ", "").Length > 0 ? this.txtSernum2.Texts.cleanString() : "") + "\",";
            json_data += "\"reason\":\"" + this.GetBreakReason() + "\",";
            json_data += "\"remark\":\"" + this.txtRemark2.Text.cleanString() + "\"}";

            Console.WriteLine(" >>> " + json_data);

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/update_break", json_data);
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
                    this.ClearForm();
                    this.GetNote();
                    this.FormRead();
                }
                else
                {
                    this.FormAdd();
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.SubmitEditBreak();
                    }
                    else
                    {
                        this.ClearForm();
                        this.FormRead();
                    }
                }
            };

            worker.RunWorkerAsync();
        }

        private string GetProblemString() // Get the string trailed of problem etc. "{MAP_DRIVE}{ERROR}{PRINT}" for store in DB
        {
            string problem = "";
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
            
            return problem;
        }

        private string GetBreakReason() // Get the string trailed of break reason etc. "{TOILET}" for store in DB
        {
            string reason = "";
            reason += (this.rbToilet.Checked ? ((SupportNote.BREAK_REASON)this.rbToilet.Tag).FormatBreakReson() : "");
            reason += (this.rbQt.Checked ? ((SupportNote.BREAK_REASON)this.rbQt.Tag).FormatBreakReson() : "");
            reason += (this.rbMeetCust.Checked ? ((SupportNote.BREAK_REASON)this.rbMeetCust.Tag).FormatBreakReson() : "");
            reason += (this.rbTraining.Checked ? ((SupportNote.BREAK_REASON)this.rbTraining.Tag).FormatBreakReson() : "");
            reason += (this.rbCorrectData.Checked ? ((SupportNote.BREAK_REASON)this.rbCorrectData.Tag).FormatBreakReson() : "");

            return reason;
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
        }

        protected override void OnActivated(EventArgs e)
        {
            if (this.serial != null)
            {
                this.toolStripAdd.PerformClick();
                this.txtSernum.Texts = this.serial.sernum;
                this.lblCompnam.Text = this.serial.compnam;
                this.txtContact.Focus();
            }
            base.OnActivated(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.ClearForm();
            this.tm.Dispose();
            this.tm = null;
            this.parent_window.btnSupportNote.Enabled = true;
            this.main_form.supportnote_wind = null;
            this.main_form.lblTimeDuration.Visible = false;

            base.OnClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                this.toolStripStop.PerformClick();
                return true;
            }

            if (keyData == Keys.F9)
            {
                this.toolStripSave.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                this.toolStripAdd.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.E))
            {
                this.toolStripEdit.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.B))
            {
                this.toolStripBreak.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
