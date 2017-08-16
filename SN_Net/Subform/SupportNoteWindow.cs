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
using System.Drawing.Printing;
using System.Threading;

namespace SN_Net.Subform
{
    public partial class SupportNoteWindow : Form
    {
        public MainForm main_form;
        public SnWindow parent_window;
        //private BindingSource bs;
        private List<SupportNote> note_list = new List<SupportNote>();
        private List<Istab> probcod;
        public List<SerialPassword> password_list;
        public List<Istab> list_verext;
        private SupportNote note;
        private Ma ma;
        public Serial serial = null;
        public List<Problem> list_problem = null;
        public DateTime current_work_date;
        private System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer t_notify;
        public FORM_MODE form_mode;
        private string search_sn = "";
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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            this.main_form = parent_window.main_form;
            this.parent_window = parent_window;
            this.FormRead();
        }

        public SupportNoteWindow(SnWindow parent_window, Serial serial, List<SerialPassword> password_list)
            : this()
        {
            this.main_form = parent_window.main_form;
            this.parent_window = parent_window;
            this.serial = serial;
            this.password_list = password_list;
        }

        private void SupportNote_Load(object sender, EventArgs e)
        {
            this.btnViewNote.Width = 0;
            this.list_verext = this.main_form.data_resource.LIST_VEREXT;
            this.probcod = this.main_form.data_resource.LIST_PROBLEM_CODE.Where(t => t.typcod != "RG").ToList<Istab>();
            this.txtDummy.Width = 0;
            this.current_work_date = DateTime.Now;
            this.PrepareControl();
            this.GetNote();
        }

        private void SupportNoteWindow_Shown(object sender, EventArgs e)
        {
            if (this.serial != null)
            {
                this.toolStripAdd.PerformClick();
                this.txtSernum.Texts = this.serial.sernum;
                this.ValidateSN(true);
            }
        }

        private void PrepareControl()
        {
            this.lblCompnam.Text = "";
            this.lblAddr.Text = "";
            this.lblCompnam2.Text = "";
            this.lblVerext.Text = "";

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

            #region Add Support Code to cbSupportCode (ComboBox)
            List<Users> support_users = new List<Users>();
            
            CRUDResult get_support_users = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_operation_users");
            ServerResult sr_support_users = JsonConvert.DeserializeObject<ServerResult>(get_support_users.data);
            
            if (sr_support_users.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                foreach (Users u in sr_support_users.users)
                {
                    this.cbUsersCode.Items.Add(new ComboboxItem(u.username, 0, u.username){ Tag = u});
                }
            }
            #endregion Add Support Code to cbSupportCode (ComboBox)

            #region Set Selected Support Code to current loged_in_user_name
            if (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES)
            {
                this.cbUsersCode.SelectedItem = this.cbUsersCode.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).id == this.main_form.G.loged_in_user_id).First<ComboboxItem>();
                this.cbUsersCode.Enabled = false;
            }
            else
            {
                this.cbUsersCode.SelectedIndex = 0;
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

            #region txtSernum Enter event
            this.txtSernum.textBox1.Enter += delegate
            {
                this.txtSernum.textBox1.SelectionStart = 0;
                this.txtSernum.textBox1.SelectionLength = 0;
            };
            #endregion txtSernum Enter event

            #region txtSernum Leave event
            // *************  Temporary disable this delegate  ************* //
            this.txtSernum.Leave += delegate
            {
                this.ValidateSN();
            };
            #endregion txtSernum Leave Event

            #region txtSernum Text change
            this.txtSernum.textBox1.TextChanged += delegate
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.BREAK)
                {
                    if (this.txtSernum.textBox1.Text.Replace("-", "").Trim().Length == 0)
                    {
                        this.toolStripSave.Enabled = true;
                    }
                    else
                    {
                        /* this setting is temporary */
                        if (this.txtSernum.textBox1.Text.Replace("-", "").Trim().Length == 10)
                        {
                            this.toolStripSave.Enabled = true;
                            return;
                        }
                        /*****************************/

                        this.toolStripSave.Enabled = false;
                    }
                }
            };
            #endregion txtSernum Text change


            //this.dtWorkDate.textBox1.GotFocus += delegate
            //{
            //    Console.WriteLine(" >>>> + current_work_dat : " + this.current_work_date.ToString());
            //    Console.WriteLine(" >>>> + dtWorkDate : " + this.dtWorkDate.ValDateTime.ToString());
            //};

            this.dtWorkDate.textBox1.Leave += delegate
            {
                if (!this.dtWorkDate.textBox1.Text.tryParseToDateTime())
                {
                    if (MessageAlert.Show("รูปแบบวันที่ไม่ถูกต้อง, โปรแกรมจะแสดงข้อมูลของวันที่ปัจจุบัน", "", MessageAlertButtons.OK, MessageAlertIcons.NONE) == DialogResult.OK)
                        this.dtWorkDate.ValDateTime = DateTime.Now;

                    return;
                }

                if (!(this.dtWorkDate.ValDateTime.ToMysqlDate() == this.current_work_date.ToMysqlDate()))
                {
                    this.GetNote();
                }
            };

            this.dtWorkDate.dateTimePicker1.ValueChanged += delegate
            {
                if (this.dtWorkDate.ValDateTime.ToMysqlDate() != this.current_work_date.ToMysqlDate())
                {
                    this.current_work_date = this.dtWorkDate.ValDateTime;
                    this.GetNote();
                }
            };

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

            #region Detect remark column clicked
            this.dgvNote.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                int row_index = ((DataGridView)sender).HitTest(e.X, e.Y).RowIndex;
                int col_index = ((DataGridView)sender).HitTest(e.X, e.Y).ColumnIndex;
                if (row_index > -1 && col_index == 25)
                {
                    Console.WriteLine("  >>>> column remark clicked");
                }
            };
            #endregion Detect remark column clicked

            #region Prevent change tab (TabControl1)
            this.tabControl1.Deselecting += delegate(object sender, TabControlCancelEventArgs e)
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT || this.form_mode == FORM_MODE.BREAK || this.form_mode == FORM_MODE.EDIT_BREAK || this.form_mode == FORM_MODE.PROCESSING)
                {
                    e.Cancel = true;
                }
            };
            #endregion Prevent change tab (TabControl1)

            #region Prevent change tab (TabControl2)
            this.tabControl2.Deselecting += delegate(object sender, TabControlCancelEventArgs e)
            {
                if (this.serial == null)
                {
                    e.Cancel = true;
                }
            };
            #endregion Prevent change tab (TabControl2)

            #region dgvNote context menu
            this.dgvNote.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                int row_index = ((DataGridView)sender).HitTest(e.X, e.Y).RowIndex;
                int col_index = ((DataGridView)sender).HitTest(e.X, e.Y).ColumnIndex;

                if (row_index < 0)
                    return;

                //if (this.form_mode != FORM_MODE.READ && col_index == 25)
                //{
                //    this.txtRemark.Focus();
                //    return;
                //}

                if (e.Button == MouseButtons.Right && this.form_mode == FORM_MODE.READ)
                {
                    ((DataGridView)sender).Rows[row_index].Cells[1].Selected = true;
                    ContextMenu c = new ContextMenu();

                    MenuItem m_add = new MenuItem("เพิ่มบันทึกการสนทนา <Alt+A>");
                    m_add.Click += delegate
                    {
                        this.toolStripAdd.PerformClick();
                    };
                    c.MenuItems.Add(m_add);

                    MenuItem m_break = new MenuItem("เพิ่มบันทึกการพักสาย <Alt+B>");
                    m_break.Click += delegate
                    {
                        this.toolStripBreak.PerformClick();
                    };
                    c.MenuItems.Add(m_break);

                    MenuItem m_edit = new MenuItem("แก้ไข <Alt+E>");
                    m_edit.Click += delegate
                    {
                        this.toolStripEdit.PerformClick();
                    };
                    m_edit.Enabled = (((DataGridView)sender).Rows[row_index].Tag is SupportNote ? true : false);
                    c.MenuItems.Add(m_edit);

                    c.Show((DataGridView)sender, new Point(e.X, e.Y));
                }
            };
            #endregion dgvNote context menu

            this.tm.Interval = 1000;
            this.tm.Tick += delegate
            {
                TimeSpan ts = new TimeSpan();

                if (this.tabControl1.SelectedTab == this.tabPage1) // Talk time
                {
                    this.dtEndTime.Value = DateTime.Now;
                    ts = TimeSpan.Parse((this.dtEndTime.Value - this.dtStartTime.Value).Hours.ToString() + ":" + (this.dtEndTime.Value - this.dtStartTime.Value).Minutes.ToString() + ":" + (this.dtEndTime.Value - this.dtStartTime.Value).Seconds.ToString());
                }
                else if (this.tabControl1.SelectedTab == this.tabPage2) // Break time
                {
                    if (PreferenceForm.BREAK_TIME_METHOD_CONFIGURATION() == (int)PreferenceForm.BREAK_TIME.AUTO) // automatic count time
                    {
                        this.dtBreakEnd.Value = DateTime.Now;
                        ts = TimeSpan.Parse((this.dtBreakEnd.Value - this.dtBreakStart.Value).Hours.ToString() + ":" + (this.dtBreakEnd.Value - this.dtBreakStart.Value).Minutes.ToString() + ":" + (this.dtBreakEnd.Value - this.dtBreakStart.Value).Seconds.ToString());
                        if (DateTime.Now.Hour == 12 || DateTime.Now.Hour == 17)
                        {
                            this.toolStripSave.PerformClick();
                        }
                    }
                    else // manual (specify time by user)
                    {
                        return;
                    }
                }

                this.main_form.lblTimeDuration.Text = ts.ToString();
            };

            this.txtSernum.textBox1.GotFocus += delegate
            {
                InputLanguage input_en = null;

                foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
                {
                    input_en = (lang.Culture.ToString().Equals("en-US") ? lang : input_en);
                }

                if (input_en != null)
                    InputLanguage.CurrentInputLanguage = input_en;
            };

            this.txtContact.textBox1.GotFocus += delegate
            {
                InputLanguage input_th = null;

                foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
                {
                    input_th = (lang.Culture.ToString().Equals("th-TH") ? lang : input_th);
                }

                if (input_th != null)
                    InputLanguage.CurrentInputLanguage = input_th;
            };
        }

        private void ValidateSN(bool skip_serial_check = false)
        {
            if (this.txtSernum.Texts.Replace("-", "").Trim().Length > 0)
            {
                if (this.serial != null && this.serial.sernum == this.txtSernum.Texts && skip_serial_check == false)
                    return;

                CRUDResult get_exist_sernum = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/check_sn_exist&sernum=" + this.txtSernum.Texts);
                ServerResult sr_exist_sernum = JsonConvert.DeserializeObject<ServerResult>(get_exist_sernum.data);

                if (sr_exist_sernum.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    if (sr_exist_sernum.serial.Count > 0) // s/n found
                    {
                        this.toolStripSave.Enabled = true;
                        this.btnViewDetail.Enabled = true;
                        this.picCheck.Visible = true;
                        this.lblCompnam.Text = sr_exist_sernum.serial[0].compnam;
                        if (this.list_verext.Find(v => v.typcod == sr_exist_sernum.serial[0].verext) != null)
                            this.lblVerext.Text = this.list_verext.Find(v => v.typcod == sr_exist_sernum.serial[0].verext).typcod + " : " + this.list_verext.Find(v => v.typcod == sr_exist_sernum.serial[0].verext).typdes_th;
                        this.lblAddr.Text = sr_exist_sernum.serial[0].addr01 + " " + sr_exist_sernum.serial[0].addr02 + " " + sr_exist_sernum.serial[0].addr03 + " " + sr_exist_sernum.serial[0].zipcod;
                        this.serial = sr_exist_sernum.serial[0];
                        this.password_list = sr_exist_sernum.serial_password;
                        this.list_problem = sr_exist_sernum.problem;
                        this.FillDgvProblem();

                        if (sr_exist_sernum.serial_password.Count > 0)
                        {
                            this.btnViewPassword.Enabled = true;
                            this.btnViewPassword.PerformClick();
                        }
                        else
                        {
                            this.btnViewPassword.Enabled = false;
                        }

                        if (sr_exist_sernum.ma.Count > 0)
                        {
                            this.ma = sr_exist_sernum.ma[0];
                            this.btnMA.Enabled = true;
                        }
                        else
                        {
                            this.ma = null;
                            this.btnMA.Enabled = false;
                        }

                        if (this.form_mode == FORM_MODE.ADD)
                        {
                            BackgroundWorker worker_spylog = new BackgroundWorker();
                            worker_spylog.DoWork += delegate
                            {
                                string json_data = "{\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\",";
                                json_data += "\"sernum\":\"" + this.txtSernum.textBox1.Text.cleanString() + "\",";
                                json_data += "\"compnam\":\"\"}";
                                ApiActions.POST(PreferenceForm.API_MAIN_URL() + "spylog/create", json_data);
                            };
                            worker_spylog.RunWorkerAsync();
                        }
                    }
                    else // s/n not found
                    {
                        MessageAlert.Show(StringResource.DATA_NOT_FOUND, "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                        this.tabControl2.SelectedTab = this.tabPage3;
                        this.btnViewDetail.Enabled = false;
                        this.btnViewPassword.Enabled = false;
                        this.picCheck.Visible = false;
                        this.serial = null;
                        this.list_problem = null;
                        this.lblCompnam.Text = "";
                        this.lblAddr.Text = "";
                        this.lblVerext.Text = "";
                        this.FillDgvProblem();
                        /* this setting is temporary */
                        //this.txtSernum.Focus();
                        this.toolStripSave.Enabled = true;
                        /*****************************/
                    }
                }
                else // error while get data from server
                {
                    MessageAlert.Show(sr_exist_sernum.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.tabControl2.SelectedTab = this.tabPage3;
                    this.btnViewDetail.Enabled = false;
                    this.btnViewPassword.Enabled = false;
                    this.picCheck.Visible = false;
                    this.serial = null;
                    this.list_problem = null;
                    this.lblCompnam.Text = "";
                    this.lblAddr.Text = "";
                    this.lblVerext.Text = "";
                    this.FillDgvProblem();
                    
                    /* this setting is temporary */
                    //this.txtSernum.Focus();
                    this.toolStripSave.Enabled = true;
                    /*****************************/
                }
            }
            else // s/n is blank
            {
                this.tabControl2.SelectedTab = this.tabPage3;
                this.btnViewDetail.Enabled = false;
                this.btnViewPassword.Enabled = false;
                this.picCheck.Visible = false;
                this.serial = null;
                this.list_problem = null;
                this.lblCompnam.Text = "";
                this.lblAddr.Text = "";
                this.lblVerext.Text = "";
                this.FillDgvProblem();

                /* this setting is temporary */
                this.toolStripSave.Enabled = true;
                /*****************************/
            }
        }

        private List<Note> SupportNote2Note(List<SupportNote> support_note_list)
        {
            List<Note> note_list = new List<Note>();
            int seq = 0;
            foreach (SupportNote snote in support_note_list)
            {
                note_list.Add(new Note()
                {
                    supportnote = snote,
                    id = snote.id,
                    is_break = snote.is_break,
                    seq = (++seq).ToString(),
                    users_name = snote.users_name,
                    date = snote.date,
                    start_time = snote.start_time,
                    end_time = snote.end_time,
                    duration = snote.duration,
                    sernum = snote.sernum,
                    contact = snote.contact,

                    map_drive = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.MAP_DRIVE.FormatNoteProblem()) ? "\u2713" : ""),
                    install = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.INSTALL_UPDATE.FormatNoteProblem()) ? "\u2713" : ""),
                    error = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.ERROR.FormatNoteProblem()) ? "\u2713" : ""),
                    fonts = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.FONTS.FormatNoteProblem()) ? "\u2713" : ""),
                    print = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.PRINT.FormatNoteProblem()) ? "\u2713" : ""),
                    training = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.TRAINING.FormatNoteProblem()) ? "\u2713" : ""),
                    stock = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.STOCK.FormatNoteProblem()) ? "\u2713" : ""),
                    form = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.FORM.FormatNoteProblem()) ? "\u2713" : ""),
                    rep_excel = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.REPORT_EXCEL.FormatNoteProblem()) ? "\u2713" : ""),
                    statement = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.STATEMENT.FormatNoteProblem()) ? "\u2713" : ""),
                    asset = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.ASSETS.FormatNoteProblem()) ? "\u2713" : ""),
                    secure = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.SECURE.FormatNoteProblem()) ? "\u2713" : ""),
                    year_end = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.YEAR_END.FormatNoteProblem()) ? "\u2713" : ""),
                    period = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.PERIOD.FormatNoteProblem()) ? "\u2713" : ""),
                    mail_wait = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.MAIL_WAIT.FormatNoteProblem()) ? "\u2713" : ""),
                    transfer_mkt = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.TRANSFER_MKT.FormatNoteProblem()) ? "\u2713" : ""),

                    remark = snote.remark,
                    reason = snote.reason
                });
            }

            return note_list;
        }

        private void GetNote()
        {
            string support_code = ((ComboboxItem)this.cbUsersCode.SelectedItem).string_value;
            string start_date = this.current_work_date.ToMysqlDate();

            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "supportnote/get_note&support_code=" + support_code + "&start_date=" + start_date + "&end_date=" + start_date);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.note_list = sr.support_note;
                this.FillDataGrid();
                if (this.note_list.Count == 0)
                    MessageAlert.Show("ไม่มีข้อมูลของวันที่ " + this.dtWorkDate.Texts, "", MessageAlertButtons.OK, MessageAlertIcons.INFORMATION);

                Console.WriteLine(" >> getNote() complete");
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }


        public void FillDataGrid(DataGridView dgv = null, List<SupportNote> notes = null)
        {
            DataGridView dgvNote = (dgv == null ? this.dgvNote : dgv);
            List<SupportNote> note_list = (notes == null ? this.note_list : notes);

            dgvNote.Tag = HelperClass.DGV_TAG.READ;
            dgvNote.Rows.Clear();
            dgvNote.Columns.Clear();
            dgvNote.EnableHeadersVisualStyles = false;

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.HeaderText = "ID";
            col0.Visible = false;
            col0.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvNote.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "ลำดับ";
            col1.Width = 40;
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvNote.Columns.Add(col1);

            dgvNote.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "วันที่",
                Width = 90,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Visible = false
            });

            dgvNote.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "รหัสพนักงาน",
                Width = 80,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Visible = false
            });

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "รับสาย";
            col2.Width = 65;
            col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvNote.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "วางสาย";
            col3.Width = 65;
            col3.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvNote.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "ระยะเวลา";
            col4.Width = 65;
            col4.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvNote.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.HeaderText = "S/N";
            col5.Width = 120;
            col5.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvNote.Columns.Add(col5);

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.HeaderText = "ชื่อลูกค้า";
            col6.Width = 160;
            col6.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvNote.Columns.Add(col6);

            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            col7.HeaderText = "Map Drive";
            col7.Width = 30;
            col7.SortMode = DataGridViewColumnSortMode.NotSortable;
            col7.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col7);

            DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
            col8.HeaderText = "Ins. /Up";
            col8.Width = 30;
            col8.SortMode = DataGridViewColumnSortMode.NotSortable;
            col8.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col8);

            DataGridViewTextBoxColumn col9 = new DataGridViewTextBoxColumn();
            col9.HeaderText = "Error";
            col9.Width = 30;
            col9.SortMode = DataGridViewColumnSortMode.NotSortable;
            col9.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col9);

            DataGridViewTextBoxColumn col10 = new DataGridViewTextBoxColumn();
            col10.HeaderText = "Ins. Fonts";
            col10.Width = 30;
            col10.SortMode = DataGridViewColumnSortMode.NotSortable;
            col10.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col10);

            DataGridViewTextBoxColumn col11 = new DataGridViewTextBoxColumn();
            col11.HeaderText = "Print";
            col11.Width = 30;
            col11.SortMode = DataGridViewColumnSortMode.NotSortable;
            col11.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col11);

            DataGridViewTextBoxColumn col12 = new DataGridViewTextBoxColumn();
            col12.HeaderText = "อบรม";
            col12.Width = 30;
            col12.SortMode = DataGridViewColumnSortMode.NotSortable;
            col12.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col12);

            DataGridViewTextBoxColumn col13 = new DataGridViewTextBoxColumn();
            col13.HeaderText = "สินค้า";
            col13.Width = 30;
            col13.SortMode = DataGridViewColumnSortMode.NotSortable;
            col13.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col13);

            DataGridViewTextBoxColumn col14 = new DataGridViewTextBoxColumn();
            col14.HeaderText = "Form Rep.";
            col14.Width = 30;
            col14.SortMode = DataGridViewColumnSortMode.NotSortable;
            col14.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col14);

            DataGridViewTextBoxColumn col15 = new DataGridViewTextBoxColumn();
            col15.HeaderText = "Rep> Excel";
            col15.Width = 30;
            col15.SortMode = DataGridViewColumnSortMode.NotSortable;
            col15.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col15);

            DataGridViewTextBoxColumn col16 = new DataGridViewTextBoxColumn();
            col16.HeaderText = "สร้างงบ";
            col16.Width = 30;
            col16.SortMode = DataGridViewColumnSortMode.NotSortable;
            col16.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col16);

            DataGridViewTextBoxColumn col17 = new DataGridViewTextBoxColumn();
            col17.HeaderText = "ท/ส. ค่าเสื่อม";
            col17.Width = 30;
            col17.SortMode = DataGridViewColumnSortMode.NotSortable;
            col17.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col17);

            DataGridViewTextBoxColumn col18 = new DataGridViewTextBoxColumn();
            col18.HeaderText = "Se cure";
            col18.Width = 30;
            col18.SortMode = DataGridViewColumnSortMode.NotSortable;
            col18.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col18);

            DataGridViewTextBoxColumn col19 = new DataGridViewTextBoxColumn();
            col19.HeaderText = "Year End";
            col19.Width = 30;
            col19.SortMode = DataGridViewColumnSortMode.NotSortable;
            col19.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col19);

            DataGridViewTextBoxColumn col20 = new DataGridViewTextBoxColumn();
            col20.HeaderText = "วันที่ ไม่อยู่ในงวด";
            col20.Width = 50;
            col20.SortMode = DataGridViewColumnSortMode.NotSortable;
            col20.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col20);

            DataGridViewTextBoxColumn col21 = new DataGridViewTextBoxColumn();
            col21.HeaderText = "Mail รอสาย/หลุด";
            col21.Width = 50;
            col21.SortMode = DataGridViewColumnSortMode.NotSortable;
            col21.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col21);

            DataGridViewTextBoxColumn col22 = new DataGridViewTextBoxColumn();
            col22.HeaderText = "โอนฝ่ายขาย";
            col22.Width = 30;
            col22.SortMode = DataGridViewColumnSortMode.NotSortable;
            col22.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col22);

            DataGridViewTextBoxColumn col24 = new DataGridViewTextBoxColumn();
            col24.HeaderText = "อื่น ๆ";
            col24.Width = 30;
            col24.SortMode = DataGridViewColumnSortMode.NotSortable;
            col24.HeaderCell.Style.Font = new Font("tahoma", 7f);
            dgvNote.Columns.Add(col24);

            DataGridViewTextBoxColumn col23 = new DataGridViewTextBoxColumn();
            col23.HeaderText = "หมายเหตุ";
            col23.SortMode = DataGridViewColumnSortMode.NotSortable;
            col23.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvNote.Columns.Add(col23);

            int cnt = 0;
            foreach (SupportNote note in note_list)
            {
                int r = dgvNote.Rows.Add();
                dgvNote.Rows[r].Tag = note;
                dgvNote.Rows[r].Cells[0].ValueType = typeof(int);
                dgvNote.Rows[r].Cells[0].Value = note.id;
                dgvNote.Rows[r].Cells[0].Tag = new DataRowIntention(DataRowIntention.TO_DO.READ);

                dgvNote.Rows[r].Cells[1].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                cnt += (note.is_break != "Y" ? 1 : 0);
                dgvNote.Rows[r].Cells[1].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[1].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[1].Value = (note.is_break != "Y" ? cnt.ToString() : "");

                dgvNote.Rows[r].Cells[2].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[2].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[2].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[2].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[2].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[2].pickedDate(note.date);

                dgvNote.Rows[r].Cells[3].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[3].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[3].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[3].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[3].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[3].Value = note.users_name;

                dgvNote.Rows[r].Cells[4].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[4].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[4].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[4].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[4].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[4].Value = note.start_time;

                dgvNote.Rows[r].Cells[5].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[5].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[5].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[5].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[5].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[5].Value = note.end_time;

                dgvNote.Rows[r].Cells[6].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[6].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[6].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[6].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[6].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[6].Value = note.duration;

                dgvNote.Rows[r].Cells[7].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[7].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[7].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[7].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[7].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[7].Value = note.sernum;

                dgvNote.Rows[r].Cells[8].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[8].Style.Alignment = (note.is_break != "Y" ? DataGridViewContentAlignment.MiddleLeft : DataGridViewContentAlignment.MiddleCenter);
                dgvNote.Rows[r].Cells[8].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[8].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[8].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[8].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[8].Value = (note.is_break != "Y" ? note.contact : this.ReadableBreakReason(note.reason));

                //using (Font f = new Font("Tahoma", 12f))
                //{
                dgvNote.Rows[r].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[9].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[9].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[9].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[9].Style.Font = f;
                dgvNote.Rows[r].Cells[9].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.MAP_DRIVE.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[10].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[10].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[10].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[10].Style.Font = f;
                dgvNote.Rows[r].Cells[10].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.INSTALL_UPDATE.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[11].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[11].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[11].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[11].Style.Font = f;
                dgvNote.Rows[r].Cells[11].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.ERROR.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[12].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[12].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[12].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[12].Style.Font = f;
                dgvNote.Rows[r].Cells[12].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.FONTS.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[13].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[13].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[13].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[13].Style.Font = f;
                dgvNote.Rows[r].Cells[13].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.PRINT.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[14].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[14].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[14].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[14].Style.Font = f;
                dgvNote.Rows[r].Cells[14].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.TRAINING.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[15].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[15].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[15].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[15].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[15].Style.Font = f;
                dgvNote.Rows[r].Cells[15].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.STOCK.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[16].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[16].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[16].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[16].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[16].Style.Font = f;
                dgvNote.Rows[r].Cells[16].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.FORM.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[17].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[17].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[17].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[17].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[17].Style.Font = f;
                dgvNote.Rows[r].Cells[17].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.REPORT_EXCEL.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[18].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[18].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[18].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[18].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[18].Style.Font = f;
                dgvNote.Rows[r].Cells[18].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.STATEMENT.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[19].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[19].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[19].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[19].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[19].Style.Font = f;
                dgvNote.Rows[r].Cells[19].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.ASSETS.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[20].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[20].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[20].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[20].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[20].Style.Font = f;
                dgvNote.Rows[r].Cells[20].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.SECURE.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[21].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[21].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[21].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[21].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[21].Style.Font = f;
                dgvNote.Rows[r].Cells[21].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.YEAR_END.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[22].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[22].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[22].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[22].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[22].Style.Font = f;
                dgvNote.Rows[r].Cells[22].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.PERIOD.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[23].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[23].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[23].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[23].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[23].Style.Font = f;
                dgvNote.Rows[r].Cells[23].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.MAIL_WAIT.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[24].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[24].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[24].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[24].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[24].Style.Font = f;
                dgvNote.Rows[r].Cells[24].Value = (note.problem.Contains(SupportNote.NOTE_PROBLEM.TRANSFER_MKT.FormatNoteProblem()) ? "\u2713" : "");

                dgvNote.Rows[r].Cells[25].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNote.Rows[r].Cells[25].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[25].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[25].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                //dgvNote.Rows[r].Cells[24].Style.Font = f;
                dgvNote.Rows[r].Cells[25].Value = (note.remark.Contains("{problem}") ? "\u2713" : "");

                dgvNote.Rows[r].Cells[26].ValueType = typeof(string);
                dgvNote.Rows[r].Cells[26].Style.ForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[26].Style.SelectionForeColor = (note.is_break != "Y" ? Color.Black : Color.Gray);
                dgvNote.Rows[r].Cells[26].Style.BackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[26].Style.SelectionBackColor = (note.is_break != "Y" ? Color.White : ColorResource.DISABLE_ROW_BACKGROUND);
                dgvNote.Rows[r].Cells[26].Value = note.remark.Replace("{problem}", "");
            }
            //dgvNote.DrawLineEffect();
            dgvNote.DrawDgvRowBorder();
        }

        private void FillDgvProblem()
        {
            this.dgvProblem.Rows.Clear();
            this.dgvProblem.Columns.Clear();
            this.dgvProblem.Tag = HelperClass.DGV_TAG.READ;

            this.dgvProblem.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Width = 80,
                HeaderText = "DATE",
                SortMode = DataGridViewColumnSortMode.NotSortable,
            });
            this.dgvProblem.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Width = 180,
                HeaderText = "NAME",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            this.dgvProblem.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Width = 35,
                HeaderText = "CO.",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            this.dgvProblem.Columns.Add(new DataGridViewTextBoxColumn()
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                HeaderText = "DESC.",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            if(this.list_problem != null){
                foreach (Problem p in this.list_problem)
                {
                    int r = this.dgvProblem.Rows.Add();
                    this.dgvProblem.Rows[r].Tag = p;
                    
                    this.dgvProblem.Rows[r].Cells[0].ValueType = typeof(string);
                    this.dgvProblem.Rows[r].Cells[0].pickedDate(p.date);

                    this.dgvProblem.Rows[r].Cells[1].ValueType = typeof(string);
                    this.dgvProblem.Rows[r].Cells[1].Value = p.name;

                    this.dgvProblem.Rows[r].Cells[2].ValueType = typeof(string);
                    this.dgvProblem.Rows[r].Cells[2].Value = p.probcod;

                    this.dgvProblem.Rows[r].Cells[3].ValueType = typeof(string);
                    this.dgvProblem.Rows[r].Cells[3].Value = p.probdesc;
                }
            }
            this.dgvProblem.DrawLineEffect();
        }

        #region FORM MODE
        private void FormRead()
        {
            this.form_mode = FORM_MODE.READ;
            //this.txtDummy.Focus();
            this.toolStripProcessing.Visible = false;

            #region TOOLSTRIP
            this.toolStripAdd.Enabled = true;
            this.toolStripEdit.Enabled = true;
            this.toolStripBreak.Enabled = true;
            this.toolStripTeacher.Enabled = (this.main_form.G.loged_in_user_training_expert ? true : false);
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripPrint.Enabled = true;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = false;
            this.txtSernum.Read_Only = true;
            this.txtContact.Read_Only = true;
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
            this.chTraining.Enabled = false;
            this.chYearEnd.Enabled = false;
            this.chOther.Enabled = false;
            this.txtRemark.Enabled = false;
            this.btnViewNote.Enabled = true;
            this.btnViewDetail.Enabled = false;
            this.btnViewPassword.Enabled = false;
            this.picCheck.Visible = false;
            this.btnMA.Enabled = false;
            #endregion FORM CONTROL

            this.splitContainer1.SplitterDistance = 78;
            this.tabControl1.Height = 0;
            this.main_form.lblTimeDuration.Visible = false;
            this.dgvNote.Enabled = true;
            this.dgvNote.Tag = HelperClass.DGV_TAG.READ;
            this.dgvNote.Refresh();
            this.dgvNote.Focus();
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
            this.toolStripTeacher.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripPrint.Enabled = false;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = false;
            this.txtContact.Read_Only = false;
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
            this.chTraining.Enabled = true;
            this.chYearEnd.Enabled = true;
            this.chOther.Enabled = true;
            this.chOther.Checked = false;
            this.txtRemark.Enabled = true;
            this.btnViewNote.Enabled = false;
            this.btnViewPassword.Enabled = false;
            this.btnMA.Enabled = (this.ma != null ? true : false);
            #endregion FORM CONTROL

            if(this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            this.tabControl1.Height = 220;
            this.splitContainer1.SplitterDistance = 302;
            this.dgvNote.Tag = HelperClass.DGV_TAG.LEAVE;
            this.dgvNote.Refresh();
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
            this.toolStripTeacher.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripPrint.Enabled = false;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = true;
            this.txtContact.Read_Only = false;
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
            this.chTraining.Enabled = true;
            this.chYearEnd.Enabled = true;
            this.chOther.Enabled = true;
            this.txtRemark.Enabled = true;
            this.btnViewNote.Enabled = false;
            this.btnMA.Enabled = (this.ma != null ? true : false);
            #endregion FORM CONTROL

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            this.tabControl1.Height = 220;
            this.splitContainer1.SplitterDistance = 302;
            //this.dgvNote.Tag = HelperClass.DGV_TAG.LEAVE;
            //this.dgvNote.Refresh();
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
            this.toolStripTeacher.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripPrint.Enabled = false;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = true;
            this.txtContact.Read_Only = true;
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
            this.chTraining.Enabled = false;
            this.chYearEnd.Enabled = false;
            this.txtRemark.Enabled = false;
            this.btnViewNote.Enabled = false;
            this.dtBreakStart.Enabled = (PreferenceForm.BREAK_TIME_METHOD_CONFIGURATION() == (int)PreferenceForm.BREAK_TIME.MANUAL ? true : false);
            this.dtBreakEnd.Enabled = (PreferenceForm.BREAK_TIME_METHOD_CONFIGURATION() == (int)PreferenceForm.BREAK_TIME.MANUAL ? true : false);
            this.btnMA.Enabled = false;
            #endregion FORM CONTROL

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            this.tabControl1.Height = 220;
            this.splitContainer1.SplitterDistance = 302;
            this.dgvNote.Tag = HelperClass.DGV_TAG.LEAVE;
            this.dgvNote.Refresh();
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
            this.toolStripTeacher.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripPrint.Enabled = false;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = true;
            this.txtContact.Read_Only = false;
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
            this.chTraining.Enabled = true;
            this.chYearEnd.Enabled = true;
            this.txtRemark.Enabled = true;
            this.btnViewNote.Enabled = false;
            this.dtBreakStart.Enabled = (PreferenceForm.BREAK_TIME_METHOD_CONFIGURATION() == (int)PreferenceForm.BREAK_TIME.MANUAL ? true : false);
            this.dtBreakEnd.Enabled = (PreferenceForm.BREAK_TIME_METHOD_CONFIGURATION() == (int)PreferenceForm.BREAK_TIME.MANUAL ? true : false);
            this.btnMA.Enabled = false;
            #endregion FORM CONTROL

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            this.tabControl1.Height = 220;
            this.splitContainer1.SplitterDistance = 302;
            //this.dgvNote.Tag = HelperClass.DGV_TAG.LEAVE;
            //this.dgvNote.Refresh();
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
            this.toolStripTeacher.Enabled = false;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripPrint.Enabled = false;
            #endregion TOOLSTRIP

            #region FORM CONTROL
            this.dtWorkDate.Read_Only = true;
            this.txtSernum.Read_Only = true;
            this.txtContact.Read_Only = true;
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
            this.chTraining.Enabled = false;
            this.chYearEnd.Enabled = false;
            this.txtRemark.Enabled = false;
            this.btnViewNote.Enabled = false;
            this.btnMA.Enabled = false;
            #endregion FORM CONTROL

            this.dgvNote.Tag = HelperClass.DGV_TAG.LEAVE;
            this.dgvNote.Refresh();
            this.dgvNote.Enabled = false;
        }
        #endregion FORM MODE

        private void ClearForm()
        {
            this.tabControl2.SelectedTab = this.tabPage3;
            this.parent_window.btnSupportNote.Enabled = true;
            this.serial = null;
            this.note = null;
            this.ma = null;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            this.splitContainer1.SplitterDistance = 42;

            #region First tab
            this.txtSernum.Texts = "";
            this.lblCompnam.Text = "";
            this.lblAddr.Text = "";
            this.lblVerext.Text = "";
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
            this.chTransferMkt.CheckState = CheckState.Unchecked;
            this.chTraining.CheckState = CheckState.Unchecked;
            this.chYearEnd.CheckState = CheckState.Unchecked;
            this.txtRemark.Text = "";
            #endregion First tab

            #region Second tab
            this.rbToilet.Checked = true;
            this.rbQt.Checked = false;
            this.rbMeetCust.Checked = false;
            this.rbTraining.Checked = false;
            this.rbCorrectData.Checked = false;
            this.rbOther.Checked = false;
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
            this.main_form.lblTimeDuration.Visible = (PreferenceForm.BREAK_TIME_METHOD_CONFIGURATION() == (int)PreferenceForm.BREAK_TIME.AUTO ? true : false);
            
            // start counting duration
            this.tm.Enabled = true;
            this.tm.Start();

            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(5000, "SN_Net", "กำลังบันทึกช่วงเวลาการปฏิบัติงาน", ToolTipIcon.Info);

            this.t_notify = new System.Windows.Forms.Timer();
            this.t_notify.Interval = 60000;
            this.t_notify.Enabled = true;
            this.t_notify.Tick += delegate
            {
                this.notifyIcon1.ShowBalloonTip(5000, "SN_Net", "กำลังบันทึกช่วงเวลาการปฏิบัติงาน", ToolTipIcon.Info);
            };
            this.t_notify.Start();
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            if (this.dtWorkDate.ValDateTime.ToString("dd-MM-yy", new CultureInfo("th-TH")) != DateTime.Now.ToString("dd-MM-yy", new CultureInfo("th-TH")))
            {
                if (MessageAlert.Show("คำเตือน : วันที่ทำการไม่เป็นปัจจุบัน\nต้องการกำหนดวันที่ให้เป็นปัจจุบันหรือไม่?", "คำเตือน", MessageAlertButtons.YES_NO, MessageAlertIcons.WARNING) == DialogResult.Yes)
                {
                    this.dtWorkDate.ValDateTime = DateTime.Now;
                }
            }
            this.cbProbcod.SelectedIndex = this.probcod.FindIndex(t => t.typcod == "--");
            this.FormAdd();
            this.main_form.lblTimeDuration.BackColor = Color.Red;
            this.main_form.lblTimeDuration.ForeColor = Color.White;
            this.BeginDuration();
            this.txtSernum.Focus();
        }

        private void toolStripBreak_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            if (this.dtWorkDate.dateTimePicker1.Value.ToString("dd-MM-yy") != DateTime.Now.ToString("dd-MM-yy"))
            {
                if (MessageAlert.Show("คำเตือน : วันที่ทำการไม่เป็นปัจจุบัน\nต้องการกำหนดวันที่ให้เป็นปัจจุบันหรือไม่?", "คำเตือน", MessageAlertButtons.YES_NO, MessageAlertIcons.WARNING) == DialogResult.Yes)
                {
                    this.dtWorkDate.dateTimePicker1.Value = DateTime.Now;
                    this.GetNote();
                }
            }

            this.dtBreakStart.Value = DateTime.Now;
            this.dtBreakEnd.Value = DateTime.Now;
            this.FormBreak();
            if (PreferenceForm.BREAK_TIME_METHOD_CONFIGURATION() == (int)PreferenceForm.BREAK_TIME.MANUAL)
            {
                this.dtBreakStart.Focus();
            }
            else
            {
                this.rbToilet.Focus();
            }
            this.main_form.lblTimeDuration.BackColor = Color.Black;
            this.main_form.lblTimeDuration.ForeColor = Color.Red;
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
                    this.cbProbcod.SelectedIndex = this.probcod.FindIndex(t => t.typcod == "--");

                    this.txtSernum.Texts = this.note.sernum;
                    this.dtStartTime.Text = this.note.start_time;
                    this.dtEndTime.Text = this.note.end_time;
                    this.txtRemark.Text = this.note.remark;
                    this.txtContact.Texts = this.note.contact;
                    this.CheckedProblem(this.note.problem);
                    this.chOther.Checked = this.note.remark.Contains("{problem}") ? true : false;
                    this.txtRemark.Text = this.note.remark.Replace("{problem}", "");
                    this.txtContact.Focus();

                    if (this.txtSernum.Texts.Replace("-", "").Trim().Length > 0)
                    {
                        CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/check_sn_exist&sernum=" + this.note.sernum);
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            if (sr.serial.Count<Serial>() > 0)
                            {
                                this.btnViewDetail.Enabled = true;
                                this.picCheck.Visible = true;
                                this.serial = sr.serial[0];
                                this.password_list = sr.serial_password;
                                this.list_problem = sr.problem;
                                this.lblCompnam.Text = this.serial.compnam;
                                this.lblAddr.Text = this.serial.addr01 + " " + this.serial.addr02 + " " + this.serial.addr03 + " " + this.serial.zipcod;
                                if (this.list_verext.Find(v => v.typcod == this.serial.verext) != null)
                                    this.lblVerext.Text = this.list_verext.Find(v => v.typcod == this.serial.verext).typcod + " : " + this.list_verext.Find(v => v.typcod == this.serial.verext).typdes_th;

                                this.FillDgvProblem();
                                if (sr.serial_password.Count > 0)
                                {
                                    this.btnViewPassword.Enabled = true;
                                }
                                else
                                {
                                    this.btnViewPassword.Enabled = false;
                                }
                                
                                if (sr.ma.Count > 0)
                                {
                                    this.ma = sr.ma[0];
                                    this.btnMA.Enabled = true;
                                }
                                else
                                {
                                    this.ma = null;
                                    this.btnMA.Enabled = false;
                                }
                            }
                            else
                            {
                                this.btnViewDetail.Enabled = false;
                                this.btnViewPassword.Enabled = false;
                                this.picCheck.Visible = false;
                                this.serial = null;
                                this.list_problem = null;
                                this.lblCompnam.Text = "";
                                this.lblAddr.Text = "";
                                this.lblVerext.Text = "";
                                this.FillDgvProblem();
                            }
                        }
                        else
                        {
                            this.btnViewDetail.Enabled = false;
                            this.btnViewPassword.Enabled = false;
                            this.picCheck.Visible = false;
                            this.serial = null;
                            this.list_problem = null;
                            this.lblCompnam.Text = "";
                            this.lblAddr.Text = "";
                            this.lblVerext.Text = "";
                            this.FillDgvProblem();
                        }
                    }
                    else
                    {
                        this.btnViewDetail.Enabled = false;
                        this.btnViewPassword.Enabled = false;
                        this.picCheck.Visible = false;
                        this.serial = null;
                        this.list_problem = null;
                        this.lblCompnam.Text = "";
                        this.lblAddr.Text = "";
                        this.lblVerext.Text = "";
                        this.FillDgvProblem();
                    }
                    
                    this.FormEdit();
                }
                else
                {
                    if (((SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Tag).reason.Contains(SupportNote.BREAK_REASON.TRAINING_TRAINER.FormatBreakReson())) // if Trainer
                    {
                        TrainerNoteDialog wind = new TrainerNoteDialog(this.main_form, (Users)((ComboboxItem)this.cbUsersCode.SelectedItem).Tag, this.dtWorkDate.dateTimePicker1.Value, (SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Tag);
                        if (wind.ShowDialog() == DialogResult.OK)
                        {
                            this.GetNote();
                        }
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
                        this.rbTraining.Checked = (this.note.reason.Contains(SupportNote.BREAK_REASON.TRAINING_ASSIST.FormatBreakReson()) ? true : false);
                        this.rbCorrectData.Checked = (this.note.reason.Contains(SupportNote.BREAK_REASON.CORRECT_DATA.FormatBreakReson()) ? true : false);
                        this.rbOther.Checked = (this.note.reason.Contains(SupportNote.BREAK_REASON.OTHER.FormatBreakReson()) ? true : false);
                        this.txtRemark2.Focus();

                        this.FormEditBreak();
                    }

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

            this.notifyIcon1.Visible = false;
            if(this.t_notify != null){
                this.t_notify.Enabled = false;
                this.t_notify.Dispose();
                this.t_notify = null;
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

            this.notifyIcon1.Visible = false;
            if (this.t_notify != null)
            {
                this.t_notify.Enabled = false;
                this.t_notify.Dispose();
                this.t_notify = null;
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
            string support_code = ((ComboboxItem)this.cbUsersCode.SelectedItem).string_value;
            string start_date = this.current_work_date.ToMysqlDate();

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
                    if (this.note_list.Count == 0)
                        MessageAlert.Show("ไม่มีข้อมูลของวันที่ " + this.dtWorkDate.Texts, "", MessageAlertButtons.OK, MessageAlertIcons.INFORMATION);
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
            if(this.chOther.Checked && this.txtRemark.Text.Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาป้อนปัญหาอื่น ๆ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.txtRemark.Focus();
                return;
            }

            this.FormProcessing();
            bool post_success = false;
            string err_msg = "";
            TimeSpan ts = new TimeSpan(this.dtEndTime.Value.Hour - this.dtStartTime.Value.Hour, this.dtEndTime.Value.Minute - this.dtStartTime.Value.Minute, this.dtEndTime.Value.Second - this.dtStartTime.Value.Second);

            string json_data = "{\"sernum\":\"" + (this.txtSernum.Texts.Replace("-", "").Replace(" ", "").Length > 0 ? this.txtSernum.Texts.cleanString() : "") + "\",";
            json_data += "\"date\":\"" + this.dtWorkDate.ValDateTime.ToMysqlDate() + "\",";
            json_data += "\"contact\":\"" + this.txtContact.Texts.cleanString() + "\",";
            json_data += "\"start_time\":\"" + this.dtStartTime.Text + "\",";
            json_data += "\"end_time\":\"" + this.dtEndTime.Text + "\",";
            json_data += "\"duration\":\"" + ts.ToString().Substring(0, 8) + "\",";
            json_data += "\"problem\":\"" + this.GetProblemString() +"\",";
            json_data += "\"remark\":\"" + (this.chOther.Checked ? "{problem}" : "") + this.txtRemark.Text.cleanString() + "\",";
            json_data += "\"also_f8\":\"" + this.chAlsoF8.CheckState.ToYesOrNoString() + "\",";
            json_data += "\"probcod\":\"" + ((ComboboxItem)this.cbProbcod.SelectedItem).string_value + "\",";
            json_data += "\"is_break\":\"N\",";
            json_data += "\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\",";
            json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

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
                        this.FormAdd();
                        //this.ClearForm();
                        //this.FormRead();
                    }
                }
            };

            worker.RunWorkerAsync();
        }

        private void SubmitEdit()
        {
            if (this.chOther.Checked && this.txtRemark.Text.Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาป้อนปัญหาอื่น ๆ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.txtRemark.Focus();
                return;
            }

            this.FormProcessing();
            bool post_success = false;
            string err_msg = "";

            string json_data = "{\"id\":" + this.note.id.ToString() + ",";
            json_data += "\"date\":\"" + this.dtWorkDate.ValDateTime.ToMysqlDate() + "\",";
            json_data += "\"sernum\":\"" + this.txtSernum.Texts.cleanString() + "\",";
            json_data += "\"contact\":\"" + this.txtContact.Texts.cleanString() + "\",";
            json_data += "\"problem\":\"" + this.GetProblemString() + "\",";
            json_data += "\"remark\":\"" + (this.chOther.Checked ? "{problem}" : "") + this.txtRemark.Text.cleanString() + "\",";
            json_data += "\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\",";
            json_data += "\"also_f8\":\"" + this.chAlsoF8.CheckState.ToYesOrNoString() + "\",";
            json_data += "\"probcod\":\"" + (this.cbProbcod.SelectedItem != null ? ((ComboboxItem)this.cbProbcod.SelectedItem).string_value : "") + "\",";
            json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

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
                        this.FormEdit();
                        //this.ClearForm();
                        //this.FormRead();
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
            //TimeSpan ts = new TimeSpan(this.dtBreakEnd.Value.Hour - this.dtBreakStart.Value.Hour, this.dtBreakEnd.Value.Minute - this.dtBreakStart.Value.Minute, this.dtBreakEnd.Value.Second - this.dtBreakStart.Value.Second);
            TimeSpan ts = TimeSpan.Parse(this.dtBreakEnd.Value.Hour.ToString() + ":" + this.dtBreakEnd.Value.Minute.ToString() + ":" + this.dtBreakEnd.Value.Second.ToString()) - TimeSpan.Parse(this.dtBreakStart.Value.Hour.ToString() + ":" + this.dtBreakStart.Value.Minute.ToString() + ":" + this.dtBreakStart.Value.Second.ToString());

            string json_data = "{\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\",";
            json_data += "\"date\":\"" + this.dtWorkDate.ValDateTime.ToMysqlDate() + "\",";
            json_data += "\"start_time\":\"" + this.dtBreakStart.Text + "\",";
            json_data += "\"end_time\":\"" + this.dtBreakEnd.Text + "\",";
            json_data += "\"duration\":\"" + ts.ToString().Substring(0, 8) + "\",";
            json_data += "\"sernum\":\"" + (this.txtSernum2.Texts.Replace("-", "").Replace(" ", "").Length > 0 ? this.txtSernum2.Texts.cleanString() : "") + "\",";
            json_data += "\"reason\":\"" + this.GetBreakReason() + "\",";
            json_data += "\"remark\":\"" + this.txtRemark2.Text.cleanString() +"\",";
            json_data += "\"is_break\":\"Y\",";
            json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

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
                        this.FormBreak();
                        //this.ClearForm();
                        //this.FormRead();
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
            TimeSpan ts = new TimeSpan(this.dtBreakEnd.Value.Hour - this.dtBreakStart.Value.Hour, this.dtBreakEnd.Value.Minute - this.dtBreakStart.Value.Minute, this.dtBreakEnd.Value.Second - this.dtBreakStart.Value.Second);

            string json_data = "{\"id\":" + this.note.id.ToString() + ",";
            json_data += "\"date\":\"" + this.dtWorkDate.ValDateTime.ToMysqlDate() + "\",";
            json_data += "\"start_time\":\"" + this.dtBreakStart.Text + "\",";
            json_data += "\"end_time\":\"" + this.dtBreakEnd.Text + "\",";
            json_data += "\"duration\":\"" + ts.ToString().Substring(0, 8) + "\",";
            json_data += "\"sernum\":\"" + (this.txtSernum2.Texts.Replace("-", "").Replace(" ", "").Length > 0 ? this.txtSernum2.Texts.cleanString() : "") + "\",";
            json_data += "\"reason\":\"" + this.GetBreakReason() + "\",";
            json_data += "\"remark\":\"" + this.txtRemark2.Text.cleanString() + "\",";
            json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

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
                        this.FormEditBreak();
                        //this.ClearForm();
                        //this.FormRead();
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
            problem += (this.chTransferMkt.Checked ? ((SupportNote.NOTE_PROBLEM)this.chTransferMkt.Tag).FormatNoteProblem() : "");
            
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
            reason += (this.rbOther.Checked ? ((SupportNote.BREAK_REASON)this.rbOther.Tag).FormatBreakReson() : "");

            return reason;
        }

        private string ReadableBreakReason(string formatted_reason) // Get the human readable string of break reason for display with Control
        {
            if (formatted_reason == SupportNote.BREAK_REASON.TOILET.FormatBreakReson())
            {
                return "** เข้าห้องน้ำ **";
            }
            else if (formatted_reason == SupportNote.BREAK_REASON.QT.FormatBreakReson())
            {
                return "** ทำใบเสนอราคา **";
            }
            else if (formatted_reason == SupportNote.BREAK_REASON.MEET_CUST.FormatBreakReson())
            {
                return "** ลูกค้ามาพบ **";
            }
            else if (formatted_reason == SupportNote.BREAK_REASON.TRAINING_TRAINER.FormatBreakReson())
            {
                return "** วิทยากรอบรม **";
            }
            else if (formatted_reason == SupportNote.BREAK_REASON.TRAINING_ASSIST.FormatBreakReson())
            {
                return "** ผู้ช่วยฯอบรม **";
            }
            else if (formatted_reason == SupportNote.BREAK_REASON.CORRECT_DATA.FormatBreakReson())
            {
                return "** แก้ไขข้อมูลลูกค้า **";
            }
            else if (formatted_reason == SupportNote.BREAK_REASON.OTHER.FormatBreakReson())
            {
                return "** อื่น ๆ **";
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
            this.chTransferMkt.CheckState = (problem.Contains(SupportNote.NOTE_PROBLEM.TRANSFER_MKT.FormatNoteProblem()) ? CheckState.Checked : CheckState.Unchecked);
        }

        public void CrossingCall(Serial serial, List<SerialPassword> password_list)
        {
            this.Activate();
            this.toolStripAdd.PerformClick();
            this.txtSernum.Texts = serial.sernum;
            this.ValidateSN();
        }

        private void toolStripTeacher_Click(object sender, EventArgs e)
        {
            TrainerNoteDialog dlg = new TrainerNoteDialog(this.main_form, (Users)((ComboboxItem)this.cbUsersCode.SelectedItem).Tag, this.dtWorkDate.ValDateTime);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.GetNote();
            }
        }

        private void btnViewPassword_Click(object sender, EventArgs e)
        {
            SerialPasswordList wind = new SerialPasswordList(this.password_list);
            wind.ShowDialog();
        }

        private void toolStripSearch_Click(object sender, EventArgs e)
        {
            SearchSerialBox sb = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.SERNUM);
            sb.mskSearchKey.Text = this.search_sn;
            if (sb.ShowDialog() == DialogResult.OK)
            {
                this.search_sn = sb.search_sn;

                bool get_success = false;
                string err_msg = "";
                List<SupportNote> search_result = null;

                string support_code = ((ComboboxItem)this.cbUsersCode.SelectedItem).string_value;
                string sernum = sb.search_sn;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult get;

                    DateTime date_from = DateTime.Now;
                    DateTime date_to = DateTime.Now;
                    if (PreferenceForm.SEARCH_NOTE_DATE_CONFIGURATION() == (int)PreferenceForm.SEARCH_DATE.BACKWARD_WEEK)
                    {
                        date_from = date_from.AddDays(-7);
                    }
                    if (PreferenceForm.SEARCH_NOTE_DATE_CONFIGURATION() == (int)PreferenceForm.SEARCH_DATE.BACKWARD_MONTH)
                    {
                        date_from = date_from.AddDays(-30);
                    }
                    if (PreferenceForm.SEARCH_NOTE_DATE_CONFIGURATION() == (int)PreferenceForm.SEARCH_DATE.BACKWARD_YEAR)
                    {
                        date_from = date_from.AddDays(-365);
                    }

                    if (PreferenceForm.SEARCH_NOTE_METHOD_CONFIGURATION() == (int)PreferenceForm.SEARCH_NOTE.PRIVATE)
                    {
                        get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "supportnote/search_note&support_code=" + support_code + "&sernum=" + sernum + "&date_from=" + date_from.ToMysqlDate() + "&date_to=" + date_to.ToMysqlDate());
                    }
                    else
                    {
                        get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "supportnote/search_note&support_code=*&sernum=" + sernum + "&date_from=" + date_from.ToMysqlDate() + "&date_to=" + date_to.ToMysqlDate());
                    }
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        get_success = true;
                        search_result = sr.support_note;
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
                        SimpleDatagridDialog sd = new SimpleDatagridDialog(SimpleDatagridDialog.BUTTON_MODE.CLOSE);
                        sd.Text = "ผลการค้นหา S/N : " + sb.search_sn + "";
                        sd.Width = 1200;
                        this.FillDataGrid(sd.dgv, search_result);
                        sd.dgv.Columns[2].Visible = true;
                        if (PreferenceForm.SEARCH_NOTE_METHOD_CONFIGURATION() == (int)PreferenceForm.SEARCH_NOTE.PUBLIC)
                            sd.dgv.Columns[3].Visible = true;
                        sd.ShowDialog();
                    }
                    else
                    {
                        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            this.GetNote();
            PrintDocument print_doc = new PrintDocument();

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("A4", 825, 1165);
            page_setup.PageSettings.Landscape = true;
            page_setup.PageSettings.Margins = new Margins(20, 20, 10, 25);

            PrintOutputSelection print_out = new PrintOutputSelection();
            if (print_out.ShowDialog() == DialogResult.OK)
            {
                int row_num = 0;
                int page_num = 0;
                int x = 0;

                print_doc.BeginPrint += delegate(object s, PrintEventArgs pe)
                {
                    row_num = 0;
                    page_num = 0;                    
                };

                print_doc.PrintPage += delegate(object s, PrintPageEventArgs pe)
                {
                    int x_pos = pe.MarginBounds.Left;
                    int y_pos = pe.MarginBounds.Top;

                    #region declare column width & cell padding
                    int cell_padding = 3;
                    
                    int col1 = 30; // seq.
                    int col2 = 55; // start_time
                    int col3 = 55; // end_time
                    int col4 = 55; // duration
                    int col5 = 80; // S/N
                    int col6 = 90; // name
                    int col7 = 30; // map drive
                    int col8 = 30; // ins./up
                    int col9 = 30; // error
                    int col10 = 30; // ins. fonts
                    int col11 = 30; // printer
                    int col12 = 30; // training
                    int col13 = 30; // stock
                    int col14 = 30; // form
                    int col15 = 30; // report -> excel
                    int col16 = 30; // balance sheet/statement
                    int col17 = 30; // assets
                    int col18 = 30; // secure
                    int col19 = 30; // year end
                    int col20 = 30; // period
                    int col21 = 30; // mail/wait
                    int col22 = 30; // transfer -> mkt.
                    int col23 = 280; // other/remark
                    #endregion declare column width


                    bool is_new_page = true;
                    page_num++;

                    if (is_new_page) // print report header
                    {
                        #region Print report header
                        StringFormat str_format_center = new StringFormat()
                        {
                            //FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap,
                            //Trimming = StringTrimming.None,
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        StringFormat str_format_right = new StringFormat()
                        {
                            //FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap,
                            //Trimming = StringTrimming.None,
                            Alignment = StringAlignment.Far,
                            LineAlignment = StringAlignment.Center
                        };

                        StringFormat str_format_left = new StringFormat()
                        {
                            //FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap,
                            //Trimming = StringTrimming.None,
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Center
                        };

                        // Report header
                        using (Font font = new Font("tahoma", 15f))
                        {
                            using (SolidBrush brush = new SolidBrush(Color.Black))
                            {
                                SizeF box_size = pe.Graphics.MeasureString("บันทึกการปฏิบัติงาน", font);
                                pe.Graphics.DrawString("บันทึกการปฏิบัติงาน", font, brush, new RectangleF(x_pos, y_pos, x_pos + pe.MarginBounds.Right - x_pos, box_size.Height), str_format_center);
                            }
                        }
                        using (Font font = new Font("tahoma", 12f))
                        {
                            using (SolidBrush brush = new SolidBrush(Color.Black))
                            {
                                SizeF box_date = pe.Graphics.MeasureString("วันที่ " + this.dtWorkDate.Texts, font);
                                pe.Graphics.DrawString("วันที่ " + this.dtWorkDate.Texts, font, brush, new RectangleF(x_pos, y_pos, box_date.Width, box_date.Height));
                                pe.Graphics.DrawLine(new Pen(Color.Black), x_pos + pe.Graphics.MeasureString("วันที่ ", font).Width, y_pos + box_date.Height, x_pos + box_date.Width, y_pos + box_date.Height);

                                SizeF box_user = pe.Graphics.MeasureString("รหัสพนักงาน : " + ((Users)((ComboboxItem)this.cbUsersCode.SelectedItem).Tag).username, font);
                                pe.Graphics.DrawString("รหัสพนักงาน : " + ((Users)((ComboboxItem)this.cbUsersCode.SelectedItem).Tag).username, font, brush, new RectangleF(pe.MarginBounds.Right - box_user.Width, y_pos, box_user.Width, box_user.Height));
                                pe.Graphics.DrawLine(new Pen(Color.Black), (pe.MarginBounds.Right - box_user.Width) + pe.Graphics.MeasureString("รหัสพนักงาน : ", font).Width, y_pos + box_user.Height, pe.MarginBounds.Right, y_pos + box_user.Height);
                            }
                        }

                        y_pos += 30;
                        using (Font font = new Font("tahoma", 8f))
                        {
                            using (SolidBrush brush = new SolidBrush(Color.Black))
                            {
                                SizeF box_size = pe.Graphics.MeasureString("หน้า " + page_num.ToString(), font);
                                pe.Graphics.DrawString("หน้า " + page_num.ToString(), font, brush, new RectangleF(pe.MarginBounds.Right - box_size.Width, y_pos, box_size.Width, box_size.Height));
                            }
                        }

                        y_pos += 20;
                        // Column header
                        using (SolidBrush brush_bg = new SolidBrush(Color.LightBlue))
                        {
                            int column_header_height = 35;
                            pe.Graphics.FillRectangle(brush_bg, new Rectangle(x_pos, y_pos, pe.MarginBounds.Right - x_pos, column_header_height));

                            using (Pen p = new Pen(Color.DarkGray))
                            {
                                pe.Graphics.DrawLine(p, x_pos, y_pos, pe.MarginBounds.Right, y_pos); // horizontal upper line

                                pe.Graphics.DrawLine(p, x_pos, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col1, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col2, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col3, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col4, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col5, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col6, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col7, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col8, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col9, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col10, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col11, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col12, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col13, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col14, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col15, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col16, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col17, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col18, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col19, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col20, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col21, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col22, y_pos, x_pos, y_pos + column_header_height);
                                pe.Graphics.DrawLine(p, x_pos += col23, y_pos, x_pos, y_pos + column_header_height);

                                x_pos = pe.MarginBounds.Left;
                                pe.Graphics.DrawLine(p, x_pos, y_pos + column_header_height, pe.MarginBounds.Right, y_pos + column_header_height); // horizontal lower line

                                using (Font font = new Font("tahoma", 6f))
                                {
                                    using (SolidBrush brush = new SolidBrush(Color.Black))
                                    {
                                        pe.Graphics.DrawString("ลำดับ", font, brush, new RectangleF(x_pos, y_pos, col1, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("รับสาย", font, brush, new RectangleF(x_pos += col1, y_pos, col2, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("วางสาย", font, brush, new RectangleF(x_pos += col2, y_pos, col3, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("ระยะเวลา", font, brush, new RectangleF(x_pos += col3, y_pos, col4, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("S/N", font, brush, new RectangleF(x_pos += col4, y_pos, col5, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("ชื่อลูกค้า", font, brush, new RectangleF(x_pos += col5, y_pos, col6, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Map Drive", font, brush, new RectangleF(x_pos += col6, y_pos, col7, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Ins./ Up", font, brush, new RectangleF(x_pos += col7, y_pos, col8, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Error", font, brush, new RectangleF(x_pos += col8, y_pos, col9, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Ins. Fonts", font, brush, new RectangleF(x_pos += col9, y_pos, col10, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Print", font, brush, new RectangleF(x_pos += col10, y_pos, col11, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("อบรม", font, brush, new RectangleF(x_pos += col11, y_pos, col12, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("สินค้า", font, brush, new RectangleF(x_pos += col12, y_pos, col13, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Form/Rep.", font, brush, new RectangleF(x_pos += col13, y_pos, col14, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Report ->XLS", font, brush, new RectangleF(x_pos += col14, y_pos, col15, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("สร้างงบฯ", font, brush, new RectangleF(x_pos += col15, y_pos, col16, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("ท/ส ค่าเสื่อม", font, brush, new RectangleF(x_pos += col16, y_pos, col17, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Secure", font, brush, new RectangleF(x_pos += col17, y_pos, col18, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Year End", font, brush, new RectangleF(x_pos += col18, y_pos, col19, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("วันที่ไม่อยู่ในงวด", font, brush, new RectangleF(x_pos += col19, y_pos, col20, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("Mail/รอสาย/หลุด", font, brush, new RectangleF(x_pos += col20, y_pos, col21, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("โอนฝ่ายขาย", font, brush, new RectangleF(x_pos += col21, y_pos, col22, column_header_height), str_format_center);
                                        pe.Graphics.DrawString("ปัญหาอื่น ๆ", font, brush, new RectangleF(x_pos += col22, y_pos, col23, column_header_height), str_format_center);
                                    }
                                }
                            }
                        }
                        y_pos += 15;
                        #endregion Print report header
                    }
                    #region Print data row
                    using (StringFormat str_center = new StringFormat()
                    {
                        FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap,
                        Trimming = StringTrimming.None,
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    })
                    {
                        using (StringFormat str_left = new StringFormat(){
                            FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap,
                            Trimming = StringTrimming.None,
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Center
                        })
                        {
                            using (StringFormat str_right = new StringFormat(){
                                FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap,
                                Trimming = StringTrimming.None,
                                Alignment = StringAlignment.Far,
                                LineAlignment = StringAlignment.Center
                            })
                            {
                                int line_height = 20;
                                for (int i = row_num; i < this.dgvNote.Rows.Count; i++)
                                {
                                    DataGridViewRow row = this.dgvNote.Rows[i];

                                    x_pos = pe.MarginBounds.Left;
                                    y_pos += line_height;

                                    if (y_pos > pe.MarginBounds.Bottom - line_height)
                                    {
                                        pe.Graphics.DrawLine(new Pen(Color.LightGray), x_pos, y_pos, pe.MarginBounds.Right, y_pos);

                                        pe.HasMorePages = true;
                                        return;
                                    }
                                    else
                                    {
                                        pe.HasMorePages = false;
                                    }

                                    if (row_num % 2 != 0)
                                    {
                                        using (SolidBrush brush = new SolidBrush(Color.Lavender))
                                        {
                                            pe.Graphics.FillRectangle(brush, x_pos, y_pos, pe.MarginBounds.Right - x_pos, line_height);
                                        }
                                    }

                                    using (Font font = new Font("tahoma", 8f))
                                    {
                                        using (SolidBrush brush = new SolidBrush(Color.Black))
                                        {
                                            using (Pen p = new Pen(Color.DarkGray))
                                            {
                                                pe.Graphics.DrawLine(p, x_pos, y_pos - 10, x_pos, y_pos + 20);
                                                pe.Graphics.DrawString((string)row.Cells[1].Value, font, brush, new RectangleF(x_pos, y_pos, col1 - cell_padding, line_height), str_right);
                                                x_pos += col1;

                                                pe.Graphics.DrawLine(p, x_pos, y_pos - 10, x_pos, y_pos + 20);
                                                pe.Graphics.DrawString((string)row.Cells[4].Value, font, brush, new RectangleF(x_pos, y_pos, col2, line_height), str_center);
                                                x_pos += col2;

                                                pe.Graphics.DrawLine(p, x_pos, y_pos - 10, x_pos, y_pos + 20);
                                                pe.Graphics.DrawString((string)row.Cells[5].Value, font, brush, new RectangleF(x_pos, y_pos, col3, line_height), str_center);
                                                x_pos += col3;

                                                pe.Graphics.DrawLine(p, x_pos, y_pos - 10, x_pos, y_pos + 20);
                                                pe.Graphics.DrawString((string)row.Cells[6].Value, font, brush, new RectangleF(x_pos, y_pos, col4, line_height), str_center);
                                                x_pos += col4;

                                                pe.Graphics.DrawLine(p, x_pos, y_pos - 10, x_pos, y_pos + 20);
                                                pe.Graphics.DrawString((string)row.Cells[7].Value, font, brush, new RectangleF(x_pos + cell_padding, y_pos, col5 - cell_padding, line_height), str_left);
                                                x_pos += col5;

                                                pe.Graphics.DrawLine(p, x_pos, y_pos - 10, x_pos, y_pos + 20);
                                                pe.Graphics.DrawString((string)row.Cells[8].Value, font, brush, new RectangleF(x_pos + cell_padding, y_pos, col6 - cell_padding, line_height), str_left);
                                                x_pos += col6;

                                                using (Font wingdings_font = new Font("wingdings", 8f))
                                                {
                                                    for (int c = 9; c <= 24; c++)
                                                    {
                                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 10, x_pos, y_pos + 20);
                                                        if (((string)row.Cells[c].Value).Length > 0)
                                                        {
                                                            pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), wingdings_font, brush, new RectangleF(x_pos, y_pos, col7, line_height), str_center);
                                                        }
                                                        x_pos += col7;
                                                    }
                                                }

                                                pe.Graphics.DrawLine(p, x_pos, y_pos - 10, x_pos, y_pos + 20);
                                                pe.Graphics.DrawString((string)row.Cells[25].Value, font, brush, new RectangleF(x_pos + cell_padding, y_pos, col23 - cell_padding, line_height), str_left);
                                                x_pos += col23;
                                                pe.Graphics.DrawLine(p, x_pos, y_pos - 10, x_pos, y_pos + 20);
                                            }
                                        }
                                    }

                                    row_num++;
                                }
                                x_pos = pe.MarginBounds.Left;
                                y_pos += line_height;
                                pe.Graphics.DrawLine(new Pen(Color.LightGray), x_pos, y_pos, pe.MarginBounds.Right, y_pos);
                            }
                        }
                    }
                    #endregion Print data row
                };

                if (print_out.output == PrintOutputSelection.OUTPUT.SCREEN)
                {
                    PrintPreviewDialog preview_dialog = new PrintPreviewDialog();
                    preview_dialog.SetBounds(this.ClientRectangle.X + 5, this.ClientRectangle.Y + 5, this.ClientRectangle.Width - 10, this.ClientRectangle.Height - 10);
                    preview_dialog.Document = print_doc;
                    preview_dialog.MdiParent = this.main_form;
                    preview_dialog.Show();
                }

                if (print_out.output == PrintOutputSelection.OUTPUT.PRINTER)
                {
                    PrintDialog print_dialog = new PrintDialog();
                    print_dialog.Document = print_doc;
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = false;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = true;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_doc.Print();
                    }
                }

                print_doc = null;
                page_setup = null;
            }
            else
            {
                print_doc = null;
                page_setup = null;
            }
        }

        private void SupportNoteWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.MdiFormClosing && this.form_mode != FORM_MODE.READ)
            {
                e.Cancel = true;
                return;
            }

            if ((e.CloseReason == CloseReason.UserClosing) && this.form_mode != FORM_MODE.READ)
            {
                if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "SN_Net", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            this.ClearForm();
            this.tm.Dispose();
            this.tm = null;
            this.parent_window.btnSupportNote.Enabled = true;
            this.main_form.supportnote_wind = null;
            this.main_form.lblTimeDuration.Visible = false;
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

                if (this.form_mode == FORM_MODE.READ)
                {
                    if (this.dtWorkDate.textBox1.Focused)
                    {
                        //if (this.dtWorkDate.textBox1.Text.tryParseToDateTime() == false)
                        //{
                        //    if (MessageAlert.Show("รูปแบบวันที่ไม่ถูกต้อง, ต้องการให้โปรแกรมแสดงข้อมูลของวันที่ปัจจุบันหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.OK)
                        //    {
                        //        this.dtWorkDate.ValDateTime = DateTime.Now;
                        //    }
                        //    else
                        //    {
                        //        this.dtWorkDate.textBox1.Focus();
                        //    }

                        //}

                        this.dtWorkDate.dateTimePicker1.Focus();
                        this.dtWorkDate.textBox1.Focus();
                        return true;
                    }
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

            if (keyData == (Keys.Alt | Keys.A) || keyData == (Keys.Alt | Keys.S))
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

            if (keyData == (Keys.Alt | Keys.P))
            {
                this.toolStripPrint.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.S))
            {
                this.toolStripSearch.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnMA_Click(object sender, EventArgs e)
        {
            MAFormDialog ma = new MAFormDialog(this.ma);
            ma.btnOK.Visible = false;
            ma.btnCancel.Text = "ปิด";
            ma.btnCancel.Focus();

            ma.maDateFrom.Read_Only = true;
            ma.maDateTo.Read_Only = true;
            ma.maEmail.Read_Only = true;

            ma.ShowDialog();
        }

        private void chOther_CheckedChanged(object sender, EventArgs e)
        {
            //if (((CheckBox)sender).Checked)
            //{
            //    this.txtRemark.ReadOnly = false;
            //    this.txtRemark.Focus();
            //}
            //else
            //{
            //    if(this.txtRemark.Text.Trim().Length > 0)
            //    {
            //        if (MessageAlert.Show("ลบปัญหาอื่น ๆ ที่ป้อนไว้, ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
            //        {
            //            ((CheckBox)sender).Checked = true;
            //            return;
            //        }
            //    }

            //    this.txtRemark.ReadOnly = true;
            //    this.txtRemark.Text = string.Empty;
            //}
        }

        private void txtRemark_DoubleClick(object sender, EventArgs e)
        {
            //if (((TextBox)sender).ReadOnly)
            //{
            //    this.chOther.Checked = true;
            //    ((TextBox)sender).ReadOnly = false;
            //    ((TextBox)sender).Focus();
            //}
        }
    }
}
