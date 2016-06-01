using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
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
        public Users current_user_from;
        public Users current_user_to;
        public DateTime current_date_from;
        public DateTime current_date_to;
        public MainForm main_form;
        public List<Users> list_support_users;
        public List<SupportNote> supportnote_list = new List<SupportNote>();
        public List<SupportNoteComment> supportnotecomment_list = new List<SupportNoteComment>();
        private List<EventCalendar> event_calendar = new List<EventCalendar>();
        private List<Istab> leave_cause;
        private List<Note> note_list = new List<Note>();
        private CultureInfo cinfo_th = new CultureInfo("th-TH");
        private CultureInfo cinfo_us = new CultureInfo("en-US");
        private BindingSource bs = new BindingSource();
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            READ,
            EDIT,
            PROCESSING
        }
        private int sorted_column = 5; // sort datagridview by 3rd column

        private BindingSource problem_all_source = new BindingSource();
        private List<ComboboxItem> list_problem_all = new List<ComboboxItem>();
        private List<ComboboxItem> list_problem_selected = new List<ComboboxItem>();

        private BindingSource reason_all_source = new BindingSource();
        private List<ComboboxItem> list_reason_all = new List<ComboboxItem>();
        private List<ComboboxItem> list_reason_selected = new List<ComboboxItem>();

        public SupportStatWindow(MainForm main_form, Users user_from, Users user_to, DateTime date_from, DateTime date_to)
        {
            InitializeComponent();
            this.main_form = main_form;
            this.current_user_from = user_from;
            this.current_user_to = user_to;
            this.current_date_from = date_from;
            this.current_date_to = date_to;
        }

        private void SupportStatWindow_Load(object sender, EventArgs e)
        {
            #region add problem to list_problem
            this.list_problem_all.Add(new ComboboxItem("Map Drive", 0, SupportNote.NOTE_PROBLEM.MAP_DRIVE.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("Install/Up", 1, SupportNote.NOTE_PROBLEM.INSTALL_UPDATE.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("Error", 2, SupportNote.NOTE_PROBLEM.ERROR.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("Fonts", 3, SupportNote.NOTE_PROBLEM.FONTS.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("Print", 4, SupportNote.NOTE_PROBLEM.PRINT.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("อบรม", 5, SupportNote.NOTE_PROBLEM.TRAINING.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("สินค้า", 6, SupportNote.NOTE_PROBLEM.STOCK.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("แก้ไขฟอร์ม/รายงาน", 7, SupportNote.NOTE_PROBLEM.FORM.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("รายงาน->Excel", 8, SupportNote.NOTE_PROBLEM.REPORT_EXCEL.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("สร้างงบฯ", 9, SupportNote.NOTE_PROBLEM.STATEMENT.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("ทรัพย์สิน/ค่าเสื่อม", 10, SupportNote.NOTE_PROBLEM.ASSETS.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("ระบบความปลอดภัย", 11, SupportNote.NOTE_PROBLEM.SECURE.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("ปิดประมวลผล", 12, SupportNote.NOTE_PROBLEM.YEAR_END.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("วันที่ไม่อยู่ในงวด", 13, SupportNote.NOTE_PROBLEM.PERIOD.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("Mail/รอสาย", 14, SupportNote.NOTE_PROBLEM.MAIL_WAIT.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("โอนสายฝ่ายขาย", 15, SupportNote.NOTE_PROBLEM.TRANSFER_MKT.FormatNoteProblem()));
            this.list_problem_all.Add(new ComboboxItem("ปัญหาอื่น ๆ", 16, "*"));
            #endregion add problem to list_problem

            #region add reason to list_reason
            this.list_reason_all.Add(new ComboboxItem("เข้าห้องน้ำ", 0, SupportNote.BREAK_REASON.TOILET.FormatBreakReson()));
            this.list_reason_all.Add(new ComboboxItem("ทำใบเสนอราคา", 1, SupportNote.BREAK_REASON.QT.FormatBreakReson()));
            this.list_reason_all.Add(new ComboboxItem("ลูกค้ามาพบ", 2, SupportNote.BREAK_REASON.MEET_CUST.FormatBreakReson()));
            this.list_reason_all.Add(new ComboboxItem("วิทยากร อบรม", 3, SupportNote.BREAK_REASON.TRAINING_TRAINER.FormatBreakReson()));
            this.list_reason_all.Add(new ComboboxItem("ผู้ช่วยฯ อบรม", 4, SupportNote.BREAK_REASON.TRAINING_ASSIST.FormatBreakReson()));
            this.list_reason_all.Add(new ComboboxItem("แก้ไขข้อมูลให้ลูกค้า", 5, SupportNote.BREAK_REASON.CORRECT_DATA.FormatBreakReson()));
            this.list_reason_all.Add(new ComboboxItem("อื่น ๆ", 6, SupportNote.BREAK_REASON.OTHER.FormatBreakReson()));
            #endregion add reason to list_reason

            this.AddEventHandler();
            this.LoadDependenciesData();
            this.FormInit();

            #region Load leave_cause from server
            CRUDResult get_leave_cause = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_all&tabtyp=" + Istab.getTabtypString(Istab.TABTYP.ABSENT_CAUSE) + "&sort=typcod");
            ServerResult sr_leave_cause = JsonConvert.DeserializeObject<ServerResult>(get_leave_cause.data);

            if (sr_leave_cause.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.leave_cause = sr_leave_cause.istab;
            }
            #endregion Load leave_cause from server


            #region config dgvNote column
            this.dgvNote.Columns[0].Visible = false;
            this.dgvNote.Columns[1].Visible = false;
            this.dgvNote.Columns[2].Visible = false;
            this.dgvNote.Columns[3].Width = 40;
            this.dgvNote.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvNote.Columns[4].Width = 70;
            this.dgvNote.Columns[5].Width = 80;
            this.dgvNote.Columns[5].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_ACTIVE_BROWN;
            this.dgvNote.Columns[6].Width = 65;
            this.dgvNote.Columns[7].Width = 65;
            this.dgvNote.Columns[8].Width = 65;
            this.dgvNote.Columns[9].Width = 120;
            this.dgvNote.Columns[10].Width = 100;
            for (int i = 11; i <= 26; i++)
            {
                this.dgvNote.Columns[i].Width = 35;
                this.dgvNote.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvNote.Columns[i].HeaderCell.Style.Font = new Font("Tahoma", 7f);
            }
            this.dgvNote.Columns[27].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvNote.Columns[28].Visible = false;

            this.dgvNote.Columns[3].HeaderText = "ลำดับ";
            this.dgvNote.Columns[4].HeaderText = "Support#";
            this.dgvNote.Columns[5].HeaderText = "วันที่";
            this.dgvNote.Columns[6].HeaderText = "รับสาย";
            this.dgvNote.Columns[7].HeaderText = "วางสาย";
            this.dgvNote.Columns[8].HeaderText = "ระยะเวลา";
            this.dgvNote.Columns[9].HeaderText = "S/N";
            this.dgvNote.Columns[10].HeaderText = "ชื่อลูกค้า";
            this.dgvNote.Columns[11].HeaderText = "Map Drive";
            this.dgvNote.Columns[12].HeaderText = "Ins. /Up";
            this.dgvNote.Columns[13].HeaderText = "Error";
            this.dgvNote.Columns[14].HeaderText = "Ins. Fonts";
            this.dgvNote.Columns[15].HeaderText = "Print";
            this.dgvNote.Columns[16].HeaderText = "อบรม";
            this.dgvNote.Columns[17].HeaderText = "สินค้า";
            this.dgvNote.Columns[18].HeaderText = "Form Rep.";
            this.dgvNote.Columns[19].HeaderText = "Ret > Excel";
            this.dgvNote.Columns[20].HeaderText = "สร้างงบ";
            this.dgvNote.Columns[21].HeaderText = "ท/ส ค่าเสื่อม";
            this.dgvNote.Columns[22].HeaderText = "Se cure";
            this.dgvNote.Columns[23].HeaderText = "Year End";
            this.dgvNote.Columns[24].HeaderText = "วันที่ ไม่อยู่ในงวด";
            this.dgvNote.Columns[25].HeaderText = "Mail รอสาย";
            this.dgvNote.Columns[26].HeaderText = "โอน ฝ่าย ขาย";
            this.dgvNote.Columns[27].HeaderText = "ปัญหาอื่น ๆ / หมายเหตุ";

            foreach (DataGridViewColumn col in this.dgvNote.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            #endregion config dgvNote column
        }

        private void SupportStatWindow_Shown(object sender, EventArgs e)
        {
            this.GetNote();
        }

        private void LoadDependenciesData()
        {
            #region Add Support Code to cbSupportCode (ComboBox)
            CRUDResult get_support_users = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_support_users");
            ServerResult sr_support_users = JsonConvert.DeserializeObject<ServerResult>(get_support_users.data);

            if (sr_support_users.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.list_support_users = sr_support_users.users;
            }
            #endregion Add Support Code to cbSupportCode (ComboBox)
        }

        private void FormInit()
        {
            this.txtDummy.Width = 0;
            this.lblWorkTime.Text = "";
            this.lblBreakTime.Text = "";
            this.lblTotalTime.Text = "";
            this.lblPeriodAbsent.Text = "";
            this.toolStripPrint.Enabled = false;
            this.chApplyCondition.CheckState = CheckState.Unchecked;
            this.FillDataGrid();

            #region Binding data_source to cbProblem (ComboBox)
            this.list_problem_selected = this.list_problem_all.ConvertAll<ComboboxItem>(t => t).ToList<ComboboxItem>();
            this.cbProblem.DataSource = this.problem_all_source;
            this.problem_all_source.DataSource = this.list_problem_selected;
            this.problem_all_source.ResetBindings(true);
            this.cbProblem.SelectedIndex = 0;
            #endregion Binding data_source to cbProblem (ComboBox)

            #region Binding data_source to cbReason (ComboBox)
            this.list_reason_selected = this.list_reason_all.ConvertAll<ComboboxItem>(t => t).ToList<ComboboxItem>();
            this.cbReason.DataSource = this.reason_all_source;
            this.reason_all_source.DataSource = this.list_reason_selected;
            this.reason_all_source.ResetBindings(true);
            this.cbReason.SelectedIndex = 0;
            #endregion Binding data_source to cbReason (ComboBox)

            #region add column to datagridview condition
            this.dgvProblem.Tag = HelperClass.DGV_TAG.READ;
            this.dgvProblem.DrawDgvRowBorder();
            this.dgvProblem.Columns.Add(new DataGridViewTextBoxColumn()
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvProblem.Columns.Add(new DataGridViewButtonColumn()
            {
                Width = 25,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Padding = new Padding(2)
                }
            });
            this.dgvReason.Tag = HelperClass.DGV_TAG.READ;
            this.dgvReason.DrawDgvRowBorder();
            this.dgvReason.Columns.Add(new DataGridViewTextBoxColumn()
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvReason.Columns.Add(new DataGridViewButtonColumn()
            {
                Width = 25,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Padding = new Padding(2)
                }
            });
            #endregion add column to datagridview condition


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

            this.dgvNote.RowPostPaint += delegate(object sender, DataGridViewRowPostPaintEventArgs e)
            {
                this.SetRowBackground(((DataGridView)sender).Rows[e.RowIndex]);
            };

            this.dgvNote.CellPainting += delegate(object sender, DataGridViewCellPaintingEventArgs e)
            {
                if (e.RowIndex == -1)
                {
                    e.PaintBackground(e.CellBounds, true);
                    e.PaintContent(e.CellBounds);
                    if (e.ColumnIndex >= 11 && e.ColumnIndex <= 26)
                    {
                        RectangleF summary_bound = new RectangleF(e.CellBounds.X + 1, (e.CellBounds.Y + e.CellBounds.Height) - 18, e.CellBounds.Width - 2, 17);

                        using (SolidBrush brush_bg = new SolidBrush(Color.LightGray))
                        {
                            e.Graphics.FillRectangle(brush_bg, summary_bound);
                        }
                        using (Pen p = new Pen(Color.Gray))
                        {
                            e.Graphics.DrawLine(p, summary_bound.X, summary_bound.Y, summary_bound.X + summary_bound.Width, summary_bound.Y);
                        }
                        

                        using (SolidBrush brush = new SolidBrush(Color.Blue))
                        {
                            using (Font font = new Font("tahoma", 7f))
                            {
                                using (StringFormat format = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap })
                                {
                                    if (e.ColumnIndex == 11) // map drive
                                    {
                                        int sum = this.note_list.Where(r => r.map_drive.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 12) // install update
                                    {
                                        int sum = this.note_list.Where(r => r.install.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 13) // Error
                                    {
                                        int sum = this.note_list.Where(r => r.error.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 14) // install fonts
                                    {
                                        int sum = this.note_list.Where(r => r.fonts.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 15) // print
                                    {
                                        int sum = this.note_list.Where(r => r.print.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 16) // อบรม
                                    {
                                        int sum = this.note_list.Where(r => r.training.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 17) // สินค้า
                                    {
                                        int sum = this.note_list.Where(r => r.stock.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 18) // form,report
                                    {
                                        int sum = this.note_list.Where(r => r.form.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 19) // report->excel
                                    {
                                        int sum = this.note_list.Where(r => r.rep_excel.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 20) // สร้างงบ
                                    {
                                        int sum = this.note_list.Where(r => r.statement.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 21) // ทรัพย์สิน/ค่าเสื่อม
                                    {
                                        int sum = this.note_list.Where(r => r.asset.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 22) // secure
                                    {
                                        int sum = this.note_list.Where(r => r.secure.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 23) // year end
                                    {
                                        int sum = this.note_list.Where(r => r.year_end.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 24) // วันที่ไม่อยู่ในงวด
                                    {
                                        int sum = this.note_list.Where(r => r.period.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 25) // Mail,รอสาย
                                    {
                                        int sum = this.note_list.Where(r => r.mail_wait.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                    if (e.ColumnIndex == 26) // โอนฝ่ายขาย
                                    {
                                        int sum = this.note_list.Where(r => r.transfer_mkt.Length > 0).Count<Note>();
                                        e.Graphics.DrawString(sum.ToString(), font, brush, summary_bound, format);
                                    }
                                }
                            }
                        }
                    }
                    e.Handled = true;
                }
            };

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
            //this.dgvNote.ColumnHeaderMouseClick += delegate(object sender, DataGridViewCellMouseEventArgs e)
            //{
            //    if (e.ColumnIndex == 3)
            //    {
            //        this.sorted_column = 3;
            //        this.FillDataGrid();
            //    }
            //    if (e.ColumnIndex == 6)
            //    {
            //        this.sorted_column = 6;
            //        this.FillDataGrid();
            //    }
            //};
            this.dgvNote.ColumnHeaderMouseClick += delegate(object sender, DataGridViewCellMouseEventArgs e)
            {
                if (e.RowIndex == -1 && e.Button == MouseButtons.Left)
                {
                    if (e.ColumnIndex == 5)
                    {
                        this.sorted_column = 5;
                        this.FillDataGrid();
                        this.dgvNote.Columns[5].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_ACTIVE_BROWN;
                        this.dgvNote.Columns[8].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_BROWN;
                    }

                    if (e.ColumnIndex == 8)
                    {
                        //this.dgvNote.Rows.Cast<DataGridViewRow>().OrderBy(r => ((SupportNote)r.Cells[0].Value).duration);
                        this.sorted_column = 8;
                        this.FillDataGrid();
                        this.dgvNote.Columns[8].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_ACTIVE_BROWN;
                        this.dgvNote.Columns[5].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_BROWN;
                    }
                }
            };
            #endregion Re-order datagridview by click column header

            #region DgvNote context menu
            this.dgvNote.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                int row_index = this.dgvNote.HitTest(e.X, e.Y).RowIndex;
                int col_index = this.dgvNote.HitTest(e.X, e.Y).ColumnIndex;

                if (row_index == -1)
                    return;

                if (!(this.dgvNote.Rows[row_index].Cells[0].Value is SupportNote))
                    return;

                this.dgvNote.Rows[row_index].Cells[col_index].Selected = true;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu mnu = new ContextMenu();

                    MenuItem add_talk = new MenuItem("บันทึกสายสนทนา");
                    add_talk.Click += delegate
                    {
                        SupportNoteDialog sd = new SupportNoteDialog(this, false);
                        if (sd.ShowDialog() == DialogResult.OK)
                        {
                            this.GetNote(0, sd.note.date, sd.note.start_time, sd.note.end_time);
                        }
                    };
                    MenuItem add_break = new MenuItem("พักสาย");
                    add_break.Click += delegate
                    {
                        SupportNoteDialog sd = new SupportNoteDialog(this, true);
                        if (sd.ShowDialog() == DialogResult.OK)
                        {
                            this.GetNote(0, sd.note.date, sd.note.start_time, sd.note.end_time);
                        }
                    };
                    MenuItem m_add = new MenuItem("เพิ่ม...", new MenuItem[]{ add_talk, add_break });
                    mnu.MenuItems.Add(m_add);

                    MenuItem m_remark = new MenuItem("แก้ไข");
                    m_remark.Click += delegate
                    {
                        this.toolStripEdit.PerformClick();
                    };
                    mnu.MenuItems.Add(m_remark);

                    MenuItem m_comment = new MenuItem("Comment/Complain");
                    m_comment.Click += delegate
                    {
                        this.ShowCommentForm();
                    };
                    mnu.MenuItems.Add(m_comment);

                    mnu.Show(this.dgvNote, new Point(e.X, e.Y));

                }
            };
            #endregion DgvNote context menu

            #region chApplyCondition CheckState Change
            this.chApplyCondition.CheckStateChanged += delegate
            {
                if (this.chApplyCondition.CheckState == CheckState.Unchecked)
                {
                    this.txtSernum.Read_Only = true;
                    this.chComment.Enabled = false;
                    this.cbProblem.Enabled = false;
                    this.cbReason.Enabled = false;
                    this.btnAddProblem.Enabled = false;
                    this.btnAddAllProblem.Enabled = false;
                    this.btnRemoveAllProblem.Enabled = false;
                    this.dgvProblem.Enabled = false;
                    this.btnAddReason.Enabled = false;
                    this.btnAddAllReason.Enabled = false;
                    this.btnRemoveAllReason.Enabled = false;
                    this.dgvReason.Enabled = false;
                }
                else
                {
                    this.txtSernum.Read_Only = false;
                    this.chComment.Enabled = true;
                    this.cbProblem.Enabled = true;
                    this.cbReason.Enabled = true;
                    this.btnAddProblem.Enabled = true;
                    this.btnAddAllProblem.Enabled = true;
                    this.btnRemoveAllProblem.Enabled = true;
                    this.dgvProblem.Enabled = true;
                    this.btnAddReason.Enabled = true;
                    this.btnAddAllReason.Enabled = true;
                    this.btnRemoveAllReason.Enabled = true;
                    this.dgvReason.Enabled = true;
                }
                this.FillDataGrid();
            };
            #endregion chApplyCondition CheckState Change

            #region Trigger by condition control
            this.txtSernum.textBox1.TextChanged += delegate
            {
                this.FillDataGrid();
                this.txtSernum.textBox1.Focus();
                this.txtSernum.SelectionStart = this.txtSernum.Texts.Length;
            };

            this.chComment.CheckStateChanged += delegate
            {
                this.FillDataGrid();
            };

            this.btnAddProblem.Click += delegate
            {
                if (this.cbProblem.SelectedItem == null)
                    return;

                this.AddProblemCondition((ComboboxItem)this.cbProblem.SelectedItem);

                this.FillDataGrid();
            };

            this.btnAddReason.Click += delegate
            {
                if (this.cbReason.SelectedItem == null)
                    return;

                this.AddReasonCondition((ComboboxItem)this.cbReason.SelectedItem);

                this.FillDataGrid();
            };

            this.btnAddAllProblem.Click += delegate
            {
                for (int i = this.list_problem_all.Count - 1; i >= 0; i--)
                {
                    this.AddProblemCondition(this.list_problem_all[i]);
                }

                this.FillDataGrid();
            };

            this.btnAddAllReason.Click += delegate
            {
                for (int i = this.list_reason_all.Count - 1; i >= 0; i--)
                {
                    this.AddReasonCondition(this.list_reason_all[i]);
                }

                this.FillDataGrid();
            };
            this.btnRemoveAllProblem.Click += delegate
            {
                for (int i = this.dgvProblem.Rows.Count - 1; i >= 0; i--)
                {
                    this.RemoveProblemCondition((ComboboxItem)this.dgvProblem.Rows[i].Tag);
                }

                this.FillDataGrid();
            };
            this.btnRemoveAllReason.Click += delegate
            {
                for (int i = this.dgvReason.Rows.Count - 1; i >= 0; i--)
                {
                    this.RemoveReasonCondition((ComboboxItem)this.dgvReason.Rows[i].Tag);
                }

                this.FillDataGrid();
            };
            this.dgvProblem.CellMouseClick += delegate(object sender, DataGridViewCellMouseEventArgs e)
            {
                if (e.RowIndex < 0)
                    return;

                if (e.ColumnIndex != 1)
                    return;

                if (this.dgvProblem.Rows[e.RowIndex].Tag is ComboboxItem)
                {
                    this.RemoveProblemCondition((ComboboxItem)this.dgvProblem.Rows[e.RowIndex].Tag);
                }

                this.FillDataGrid();
            };
            this.dgvReason.CellMouseClick += delegate(object sender, DataGridViewCellMouseEventArgs e)
            {
                if (e.RowIndex < 0)
                    return;

                if (e.ColumnIndex != 1)
                    return;

                if (this.dgvReason.Rows[e.RowIndex].Tag is ComboboxItem)
                {
                    this.RemoveReasonCondition((ComboboxItem)this.dgvReason.Rows[e.RowIndex].Tag);
                }

                this.FillDataGrid();
            };
            #endregion Trigger by condition control
        }

        private void AddProblemCondition(ComboboxItem prob)
        {
            if (this.list_problem_all.Find(t => t.string_value == prob.string_value) == null)
                return;

            this.list_problem_all.Remove(this.list_problem_all.Find(t => t.string_value == prob.string_value));
            this.problem_all_source.DataSource = this.list_problem_all;
            this.problem_all_source.ResetBindings(true);

            int r = this.dgvProblem.Rows.Add();
            this.dgvProblem.Rows[r].Tag = prob;

            this.dgvProblem.Rows[r].Cells[0].ValueType = typeof(string);
            this.dgvProblem.Rows[r].Cells[0].Value = prob.ToString();

            this.dgvProblem.Rows[r].Cells[1].Value = "x";
            this.dgvProblem.Rows[r].Cells[1].ToolTipText = "ลบ";
        }

        private void AddReasonCondition(ComboboxItem reason)
        {
            if (this.list_reason_all.Find(t => t.string_value == reason.string_value) == null)
                return;

            this.list_reason_all.Remove(this.list_reason_all.Find(t => t.string_value == reason.string_value));
            this.reason_all_source.DataSource = this.list_reason_all;
            this.reason_all_source.ResetBindings(true);

            int r = this.dgvReason.Rows.Add();
            this.dgvReason.Rows[r].Tag = reason;

            this.dgvReason.Rows[r].Cells[0].ValueType = typeof(string);
            this.dgvReason.Rows[r].Cells[0].Value = reason.ToString();

            this.dgvReason.Rows[r].Cells[1].Value = "x";
            this.dgvReason.Rows[r].Cells[1].ToolTipText = "ลบ";
        }

        private void RemoveProblemCondition(ComboboxItem prob)
        {
            if (this.dgvProblem.CurrentCell == null)
                return;

            if (this.dgvProblem.Rows.Cast<DataGridViewRow>().Where(r => ((ComboboxItem)r.Tag).string_value == prob.string_value).Count<DataGridViewRow>() <= 0)
                return;

            this.dgvProblem.Rows.Remove(this.dgvProblem.Rows.Cast<DataGridViewRow>().Where(r => ((ComboboxItem)r.Tag).string_value == prob.string_value).First<DataGridViewRow>());

            this.list_problem_all.Add(prob);
            this.list_problem_all = this.list_problem_all.OrderBy(t => t.int_value).ToList<ComboboxItem>();
            this.problem_all_source.DataSource = this.list_problem_all;
            this.problem_all_source.ResetBindings(true);
        }

        private void RemoveReasonCondition(ComboboxItem reason)
        {
            if (this.dgvReason.CurrentCell == null)
                return;

            if (this.dgvReason.Rows.Cast<DataGridViewRow>().Where(r => ((ComboboxItem)r.Tag).string_value == reason.string_value).Count<DataGridViewRow>() <= 0)
                return;

            this.dgvReason.Rows.Remove(this.dgvReason.Rows.Cast<DataGridViewRow>().Where(r => ((ComboboxItem)r.Tag).string_value == reason.string_value).First<DataGridViewRow>());

            this.list_reason_all.Add(reason);
            this.list_reason_all = this.list_reason_all.OrderBy(t => t.int_value).ToList<ComboboxItem>();
            this.reason_all_source.DataSource = this.list_reason_all;
            this.reason_all_source.ResetBindings(true);
        }

        private void ShowCommentForm()
        {
            int current_note_id = ((SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[0].Value).id;
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
            this.txtSernum.Focus();

            #region Toolstrip button
            this.btnAdd.Enabled = true;
            this.toolStripPrint.Enabled = true;
            this.toolStripPrintDetail.Enabled = true;
            this.toolStripPrintSummary.Enabled = true;
            this.toolStripExport.Enabled = true;
            this.toolStripExportDetail.Enabled = true;
            this.toolStripExportSummary.Enabled = true;
            this.toolStripRange.Enabled = true;
            this.toolStripEdit.Enabled = true;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            #endregion Toolstrip button

            #region Control state
            this.chApplyCondition.Enabled = true;
            #endregion Control state
        }

        private void FormEdit()
        {
            this.form_mode = FORM_MODE.EDIT;
            this.toolStripProcessing.Visible = false;

            #region Toolstrip button
            this.btnAdd.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintDetail.Enabled = false;
            this.toolStripPrintSummary.Enabled = false;
            this.toolStripExport.Enabled = false;
            this.toolStripExportDetail.Enabled = false;
            this.toolStripExportSummary.Enabled = false;
            this.toolStripRange.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            #endregion Toolstrip button

            #region Control state
            this.chApplyCondition.Enabled = false;
            #endregion Control state
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;
            this.toolStripProcessing.Visible = true;

            #region Toolstrip button
            this.btnAdd.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintDetail.Enabled = false;
            this.toolStripPrintSummary.Enabled = false;
            this.toolStripExport.Enabled = false;
            this.toolStripExportDetail.Enabled = false;
            this.toolStripExportSummary.Enabled = false;
            this.toolStripRange.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            #endregion Toolstrip button

            #region Control state
            this.chApplyCondition.Enabled = false;
            #endregion Control state
        }

        private void GetNote(int selected_row_id = 0, string date = "", string start_time = "", string end_time = "")
        {
            this.lblUserFrom.Text = this.current_user_from.username;
            this.lblUserTo.Text = this.current_user_to.username;
            this.lblDateFrom.Text = this.current_date_from.ToString("dd/MM/yy", cinfo_th);
            this.lblDateTo.Text = this.current_date_to.ToString("dd/MM/yy", cinfo_th);
            this.FormProcessing();
            bool get_success = false;
            string err_msg = "";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "supportnote/get_note_admin&user_from=" + this.current_user_from.username + "&user_to=" + this.current_user_to.username + "&start_date=" + this.current_date_from.ToMysqlDate() + "&end_date=" + this.current_date_to.ToMysqlDate());
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
                    List<EventCalendar> absent_day = this.event_calendar.Where(t => t.event_type == EventCalendar.EVENT_TYPE_ABSENT_CAUSE).ToList<EventCalendar>();
                    List<EventCalendar> service_day = this.event_calendar.Where(t => t.event_type == EventCalendar.EVENT_TYPE_SERVICE_CASE).ToList<EventCalendar>();
                    this.FillDataGrid(selected_row_id, date, start_time, end_time);
                    this.FormRead();
                }
                else
                {
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

        public void GetComments()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "supportnotecomment/get_comment&user_from=" + this.current_user_from.username + "&user_to=" + this.current_user_to.username + "&start_date=" + this.current_date_from.ToMysqlDate() + "&end_date=" + this.current_date_to.ToMysqlDate());
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.supportnotecomment_list = sr.support_note_comment;
            }
        }

        public void GetSingleNote(int note_id)
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "supportnote/get_single_note&id=" + note_id.ToString());
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                if (this.supportnote_list.Find(t => t.id == note_id) != null)
                {
                    this.supportnote_list.Find(t => t.id == note_id).contact = sr.support_note[0].contact;
                    this.supportnote_list.Find(t => t.id == note_id).date = sr.support_note[0].date;
                    this.supportnote_list.Find(t => t.id == note_id).duration = sr.support_note[0].duration;
                    this.supportnote_list.Find(t => t.id == note_id).end_time = sr.support_note[0].end_time;
                    this.supportnote_list.Find(t => t.id == note_id).file_path = sr.support_note[0].file_path;
                    this.supportnote_list.Find(t => t.id == note_id).is_break = sr.support_note[0].is_break;
                    this.supportnote_list.Find(t => t.id == note_id).problem = sr.support_note[0].problem;
                    this.supportnote_list.Find(t => t.id == note_id).reason = sr.support_note[0].reason;
                    this.supportnote_list.Find(t => t.id == note_id).rec_by = sr.support_note[0].rec_by;
                    this.supportnote_list.Find(t => t.id == note_id).remark = sr.support_note[0].remark;
                    this.supportnote_list.Find(t => t.id == note_id).sernum = sr.support_note[0].sernum;
                    this.supportnote_list.Find(t => t.id == note_id).start_time = sr.support_note[0].start_time;
                    this.supportnote_list.Find(t => t.id == note_id).users_name = sr.support_note[0].users_name;
                }
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private List<Note> JustConvertToNoteList()
        {
            List<SupportNote> support_note = this.supportnote_list.ConvertAll<SupportNote>(t => t).ToList<SupportNote>();
            List<Note> note_list = new List<Note>();

            int count = 0;
            foreach (SupportNote snote in support_note)
            {
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

                note.map_drive = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.MAP_DRIVE.FormatNoteProblem()) ? "\u2713" : "");
                note.install = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.INSTALL_UPDATE.FormatNoteProblem()) ? "\u2713" : "");
                note.error = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.ERROR.FormatNoteProblem()) ? "\u2713" : "");
                note.fonts = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.FONTS.FormatNoteProblem()) ? "\u2713" : "");
                note.print = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.PRINT.FormatNoteProblem()) ? "\u2713" : "");
                note.training = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.TRAINING.FormatNoteProblem()) ? "\u2713" : "");
                note.stock = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.STOCK.FormatNoteProblem()) ? "\u2713" : "");
                note.form = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.FORM.FormatNoteProblem()) ? "\u2713" : "");
                note.rep_excel = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.REPORT_EXCEL.FormatNoteProblem()) ? "\u2713" : "");
                note.statement = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.STATEMENT.FormatNoteProblem()) ? "\u2713" : "");
                note.asset = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.ASSETS.FormatNoteProblem()) ? "\u2713" : "");
                note.secure = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.SECURE.FormatNoteProblem()) ? "\u2713" : "");
                note.year_end = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.YEAR_END.FormatNoteProblem()) ? "\u2713" : "");
                note.period = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.PERIOD.FormatNoteProblem()) ? "\u2713" : "");
                note.mail_wait = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.MAIL_WAIT.FormatNoteProblem()) ? "\u2713" : "");
                note.transfer_mkt = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.TRANSFER_MKT.FormatNoteProblem()) ? "\u2713" : "");

                note_list.Add(note);
            }

            return note_list;
        }

        private void FilterNoteList()
        {
            List<SupportNote> support_note = this.supportnote_list.ConvertAll<SupportNote>(t => t).ToList<SupportNote>();

            if (this.chApplyCondition.CheckState == CheckState.Checked) // Applying condition
            {
                // S/N
                if (this.txtSernum.Texts.Trim().Length > 0)
                {
                    support_note = support_note.Where(n => n.sernum.Length >= this.txtSernum.Texts.Length).Where(n => n.sernum.Substring(0, this.txtSernum.Texts.Length) == this.txtSernum.Texts).ToList<SupportNote>();
                }
                // Problem
                List<SupportNote> tmp_note = new List<SupportNote>();
                foreach (DataGridViewRow r in this.dgvProblem.Rows)
	            {
                    if (((ComboboxItem)r.Tag).string_value == "*")
                    {
                        tmp_note = tmp_note.Concat<SupportNote>(support_note.Where(n => n.is_break == "N" && n.problem.Trim().Length == 0).ToList<SupportNote>()).ToList<SupportNote>();
                    }
                    else
                    {
                        tmp_note = tmp_note.Concat<SupportNote>(support_note.Where(n => n.problem.Contains(((ComboboxItem)r.Tag).string_value)).ToList<SupportNote>()).ToList<SupportNote>();
                    }
                    tmp_note = tmp_note.Distinct().ToList<SupportNote>();
	            }
                // Break
                List<SupportNote> tmp_break = new List<SupportNote>();
                foreach (DataGridViewRow r in this.dgvReason.Rows)
                {
                    if (support_note.Where(n => n.reason != null && n.reason.Contains(((ComboboxItem)r.Tag).string_value)).Count<SupportNote>() > 0)
                        tmp_break = tmp_break.Concat<SupportNote>(support_note.Where(n => n.reason != null && n.reason.Contains(((ComboboxItem)r.Tag).string_value)).ToList<SupportNote>()).ToList<SupportNote>();
                }
                support_note = tmp_note.Concat(tmp_break).ToList<SupportNote>().Distinct().ToList<SupportNote>();

                // Comment
                if (this.chComment.CheckState == CheckState.Checked)
                {
                    List<SupportNote> snote = support_note.ConvertAll<SupportNote>(t => t).ToList<SupportNote>();
                    foreach (SupportNote note in snote)
                    {
                        if (supportnotecomment_list.DistinctBy(p => p.note_id).ToList<SupportNoteComment>().Find(c => c.note_id == note.id) == null)
                        {
                            support_note.RemoveAll(s => s.id == note.id);
                        }
                    }
                }
            }
            if (this.sorted_column == 5) // sort by date + start_time
            {
                support_note = support_note.OrderBy(n => n.users_name + n.date + n.start_time).ToList<SupportNote>();
            }
            else if (this.sorted_column == 8) // sort by duration time
            {
                support_note = support_note.OrderBy(n => n.duration).ToList<SupportNote>();
            }

            this.note_list.Clear();
            int count = 0;
            foreach (SupportNote snote in support_note)
            {
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

                note.map_drive = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.MAP_DRIVE.FormatNoteProblem()) ? "\u2713" : "");
                note.install = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.INSTALL_UPDATE.FormatNoteProblem()) ? "\u2713" : "");
                note.error = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.ERROR.FormatNoteProblem()) ? "\u2713" : "");
                note.fonts = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.FONTS.FormatNoteProblem()) ? "\u2713" : "");
                note.print = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.PRINT.FormatNoteProblem()) ? "\u2713" : "");
                note.training = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.TRAINING.FormatNoteProblem()) ? "\u2713" : "");
                note.stock = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.STOCK.FormatNoteProblem()) ? "\u2713" : "");
                note.form = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.FORM.FormatNoteProblem()) ? "\u2713" : "");
                note.rep_excel = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.REPORT_EXCEL.FormatNoteProblem()) ? "\u2713" : "");
                note.statement = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.STATEMENT.FormatNoteProblem()) ? "\u2713" : "");
                note.asset = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.ASSETS.FormatNoteProblem()) ? "\u2713" : "");
                note.secure = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.SECURE.FormatNoteProblem()) ? "\u2713" : "");
                note.year_end = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.YEAR_END.FormatNoteProblem()) ? "\u2713" : "");
                note.period = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.PERIOD.FormatNoteProblem()) ? "\u2713" : "");
                note.mail_wait = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.MAIL_WAIT.FormatNoteProblem()) ? "\u2713" : "");
                note.transfer_mkt = (snote.problem.Contains(SupportNote.NOTE_PROBLEM.TRANSFER_MKT.FormatNoteProblem()) ? "\u2713" : "");

                this.note_list.Add(note);
            }
            
            this.lblTotalCall.Text = (this.current_user_from.id == this.current_user_to.id ? count.ToString() + " สาย" : "-");
            support_note = null;
        }

        public void FillDataGrid(int selected_row_id = 0, string date = "", string start_time = "", string end_time = "")
        {
            this.FilterNoteList();
            this.dgvNote.DataSource = this.bs;
            this.bs.DataSource = this.note_list;
            this.bs.ResetBindings(true);

            TimeSpan total_time = new TimeSpan(0, 0, 0);
            TimeSpan work_time = new TimeSpan(0, 0, 0);
            TimeSpan break_time = new TimeSpan(0, 0, 0);

            foreach (DataGridViewRow row in this.dgvNote.Rows)
            {
                if (row.Cells[0].Value is SupportNote)
                {
                    total_time += TimeSpan.Parse(((SupportNote)row.Cells[0].Value).duration);
                    work_time += (((SupportNote)row.Cells[0].Value).is_break == "N" ? TimeSpan.Parse(((SupportNote)row.Cells[0].Value).duration) : TimeSpan.Parse("0:0:0"));
                    break_time += (((SupportNote)row.Cells[0].Value).is_break == "Y" ? TimeSpan.Parse(((SupportNote)row.Cells[0].Value).duration) : TimeSpan.Parse("0:0:0"));
                }
            }
            this.lblWorkTime.Text = (this.current_user_from.id == this.current_user_to.id ? work_time.ToString() : "-");
            this.lblBreakTime.Text = (this.current_user_from.id == this.current_user_to.id ? break_time.ToString() : "-");
            this.lblTotalTime.Text = (this.current_user_from.id == this.current_user_to.id ? total_time.ToString() : "-");

            //int summary_row = this.dgvNote.Rows.Add();

            if (date.Length > 0 && start_time.Length > 0 && end_time.Length > 0)
            {
                if (this.dgvNote.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value is SupportNote).Where(r => ((SupportNote)r.Cells[0].Value).date == date && ((SupportNote)r.Cells[0].Value).start_time == start_time && ((SupportNote)r.Cells[0].Value).end_time == end_time).Count<DataGridViewRow>() > 0)
                {
                    this.dgvNote.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value is SupportNote).Where(r => ((SupportNote)r.Cells[0].Value).date == date && ((SupportNote)r.Cells[0].Value).start_time == start_time && ((SupportNote)r.Cells[0].Value).end_time == end_time).First<DataGridViewRow>().Cells[3].Selected = true;
                }
            }
            else if(this.dgvNote.Rows.Cast<DataGridViewRow>().Where(r => ((SupportNote)r.Cells[0].Value).id == selected_row_id).Count<DataGridViewRow>() > 0)
            {
                this.dgvNote.Rows.Cast<DataGridViewRow>().Where(r => ((SupportNote)r.Cells[0].Value).id == selected_row_id).First<DataGridViewRow>().Cells[3].Selected = true;
            }

        }

        private void SetRowBackground(DataGridViewRow row)
        {
            if (row.Cells[0].Value is SupportNote)
            {
                if (this.supportnotecomment_list.Find(t => t.note_id == ((SupportNote)row.Cells[0].Value).id) != null) // Has comment/complain
                {
                    if (this.supportnotecomment_list.Where(t => t.note_id == ((SupportNote)row.Cells[0].Value).id && t.type == (int)CommentWindow.COMMENT_TYPE.COMMENT).Count<SupportNoteComment>() > 0
                        && this.supportnotecomment_list.Where(t => t.note_id == ((SupportNote)row.Cells[0].Value).id && t.type == (int)CommentWindow.COMMENT_TYPE.COMPLAIN).Count<SupportNoteComment>() == 0) // Only comment
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            row.Cells[cell.ColumnIndex].Style.BackColor = Color.FromArgb(198,219,255);
                            row.Cells[cell.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(198, 219, 255);
                            row.Cells[cell.ColumnIndex].Style.ForeColor = Color.Black;
                            row.Cells[cell.ColumnIndex].Style.SelectionForeColor = Color.Black;
                        }
                        return;
                    }

                    if (this.supportnotecomment_list.Where(t => t.note_id == ((SupportNote)row.Cells[0].Value).id && t.type == (int)CommentWindow.COMMENT_TYPE.COMMENT).Count<SupportNoteComment>() == 0
                        && this.supportnotecomment_list.Where(t => t.note_id == ((SupportNote)row.Cells[0].Value).id && t.type == (int)CommentWindow.COMMENT_TYPE.COMPLAIN).Count<SupportNoteComment>() > 0) // Only complain
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            row.Cells[cell.ColumnIndex].Style.BackColor = Color.FromArgb(255, 220, 224);
                            row.Cells[cell.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(255, 220, 224);
                            row.Cells[cell.ColumnIndex].Style.ForeColor = Color.Black;
                            row.Cells[cell.ColumnIndex].Style.SelectionForeColor = Color.Black;
                        }
                        return;
                    }

                    if (this.supportnotecomment_list.Where(t => t.note_id == ((SupportNote)row.Cells[0].Value).id && t.type == (int)CommentWindow.COMMENT_TYPE.COMMENT).Count<SupportNoteComment>() > 0
                        && this.supportnotecomment_list.Where(t => t.note_id == ((SupportNote)row.Cells[0].Value).id && t.type == (int)CommentWindow.COMMENT_TYPE.COMPLAIN).Count<SupportNoteComment>() > 0) // Both comment/complain
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            row.Cells[cell.ColumnIndex].Style.BackColor = Color.FromArgb(255, 220, 224);
                            row.Cells[cell.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(255, 220, 224);
                            row.Cells[cell.ColumnIndex].Style.ForeColor = Color.Blue;
                            row.Cells[cell.ColumnIndex].Style.SelectionForeColor = Color.Blue;
                        }
                        return;
                    }

                }
                else // No comment, No complain
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        row.Cells[cell.ColumnIndex].Style.BackColor = (((SupportNote)row.Cells[0].Value).is_break == "Y" ? ColorResource.DISABLE_ROW_BACKGROUND : Color.White);
                        row.Cells[cell.ColumnIndex].Style.SelectionBackColor = (((SupportNote)row.Cells[0].Value).is_break == "Y" ? ColorResource.DISABLE_ROW_BACKGROUND : Color.White);
                        row.Cells[cell.ColumnIndex].Style.ForeColor = (((SupportNote)row.Cells[0].Value).is_break == "Y" ? Color.Gray : Color.Black);
                        row.Cells[cell.ColumnIndex].Style.SelectionForeColor = (((SupportNote)row.Cells[0].Value).is_break == "Y" ? Color.Gray : Color.Black);
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
            else if (formatted_reason == "{TRAINING_TRAINER}")
            {
                return "** วิทยากรอบรม **";
            }
            else if (formatted_reason == "{TRAINING_ASSIST}")
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
            reason += (problem.Contains(SupportNote.BREAK_REASON.TRAINING_TRAINER.FormatBreakReson()) ? "** วิทยากรอบรม **" : "");
            reason += (problem.Contains(SupportNote.BREAK_REASON.TRAINING_ASSIST.FormatBreakReson()) ? "** ผู้ช่วยฯอบรม **" : "");
            reason += (problem.Contains(SupportNote.BREAK_REASON.CORRECT_DATA.FormatBreakReson()) ? "** แก้ข้อมูลให้ลูกค้า **" : "");

            return reason;
        }

        private void btnViewNote_Click(object sender, EventArgs e)
        {
            this.GetNote();
        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            // Creating a List of print content
            List<object> content = new List<object>();
            foreach (Note n in this.note_list)
            {
                content.Add(n);
                foreach (SupportNoteComment cm in this.supportnotecomment_list.Where(c => c.note_id == n.id && c.type == (int)CommentWindow.COMMENT_TYPE.COMPLAIN).ToList<SupportNoteComment>())
                {
                    content.Add(cm);
                }
                foreach (SupportNoteComment cm in this.supportnotecomment_list.Where(c => c.note_id == n.id && c.type == (int)CommentWindow.COMMENT_TYPE.COMMENT).ToList<SupportNoteComment>())
                {
                    content.Add(cm);
                }
            }

            string print_time = "( " + DateTime.Now.ToString() + " )";
            PrintDocument print_doc = new PrintDocument();

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("A4", 825, 1165);
            page_setup.PageSettings.Landscape = true;
            page_setup.PageSettings.Margins = new Margins(0, 20, 0, 40);

            PrintOutputSelection wind = new PrintOutputSelection();
            if (wind.ShowDialog() == DialogResult.OK)
            {
                int row_num = 0;
                int page_no = 0;
                bool is_comment = false;
                bool is_complain = false;
                print_doc.BeginPrint += delegate(object obj_sender, PrintEventArgs pe)
                {
                    row_num = 0;
                    page_no = 0;
                    is_comment = false;
                    is_complain = false;
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
                                int col23_width = 25; // transfer -> mkt
                                int col24_width = 305; // remark
                                #endregion declare column width
                                StringFormat str_format_left = new StringFormat();
                                str_format_left.Alignment = StringAlignment.Near;
                                str_format_left.LineAlignment = StringAlignment.Center;

                                StringFormat str_format_center = new StringFormat();
                                str_format_center.Alignment = StringAlignment.Center;
                                str_format_center.LineAlignment = StringAlignment.Center;

                                StringFormat str_format_right = new StringFormat();
                                str_format_right.Alignment = StringAlignment.Far;
                                str_format_right.LineAlignment = StringAlignment.Center;

                                Font fontsmall = new Font("tahoma", 5f); // for some column header

                                //for (int i = row_num; i < this.note_list.Count; i++)
                                for (int i = row_num; i < content.Count; i++)
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
                                            pe.Graphics.DrawString("รายละเอียดการปฏิบัติงาน", new Font("tahoma", 11f, FontStyle.Bold), brush, new Rectangle(x_pos, y_pos, 300, 20));
                                            pe.Graphics.DrawString(print_time, font, brush, new Rectangle(x_pos + 1000, y_pos, 130, 13), str_format_right);
                                            y_pos += 20;

                                            pe.Graphics.DrawString("พนักงาน จาก", font, brush, new Rectangle(10, y_pos, 80, 25), str_format_left);
                                            using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                            {
                                                pe.Graphics.DrawString(this.lblUserFrom.Text, font_bold, brush, new Rectangle(90, y_pos, 100, 25), str_format_left);
                                            }
                                            pe.Graphics.DrawString("ถึง", font, brush, new Rectangle(190, y_pos, 30, 25), str_format_center);
                                            using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                            {
                                                pe.Graphics.DrawString(this.lblUserTo.Text, font_bold, brush, new Rectangle(220, y_pos, 100, 25), str_format_left);
                                            }
                                            y_pos += 20;
                                            pe.Graphics.DrawString("วันที่ จาก", font, brush, new Rectangle(10, y_pos, 80, 25), str_format_left);
                                            using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                            {
                                                pe.Graphics.DrawString(this.lblDateFrom.Text, font_bold, brush, new Rectangle(90, y_pos, 100, 25), str_format_left);
                                            }
                                            pe.Graphics.DrawString("ถึง", font, brush, new Rectangle(190, y_pos, 30, 25), str_format_center);
                                            using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                            {
                                                pe.Graphics.DrawString(this.lblDateTo.Text, font_bold, brush, new Rectangle(220, y_pos, 100, 25), str_format_left);
                                            }
                                            pe.Graphics.DrawString("หน้า : " + page_no.ToString(), font, brush, new Rectangle(x_pos + 1000, y_pos, 130, 13), str_format_right); // draw page no.
                                            y_pos += 25;

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
                                            pe.Graphics.DrawString("โอนฝ่ายขาย", fontsmall, brush, header_rect23, str_format_center);
                                            x_pos += col23_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect24 = new Rectangle(x_pos, y_pos, col24_width, 25);
                                            pe.Graphics.DrawString("ปัญหาอื่น ๆ", font, brush, header_rect24, str_format_center);
                                            x_pos += col24_width;
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
                                    if (content[i] is Note)
                                    {
                                        // Paint odd/even background
                                        if (row_num % 2 != 0 && content[i] is Note)
                                        {
                                            using (SolidBrush brush_bg = new SolidBrush(Color.Lavender))
                                            {
                                                pe.Graphics.FillRectangle(brush_bg, x_pos, y_pos - 6, pe.MarginBounds.Right - x_pos, 21);
                                            }
                                        }

                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);
                                        Rectangle rect0 = new Rectangle(x_pos, y_pos, col0_width, 13);
                                        pe.Graphics.DrawString(((Note)content[i]).seq, font, brush, rect0, str_format_right);
                                        x_pos += col0_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15); // column separator

                                        Rectangle rect1 = new Rectangle(x_pos, y_pos, col1_width, 13);
                                        pe.Graphics.DrawString(((Note)content[i]).users_name, font, brush, rect1, str_format_center);
                                        x_pos += col1_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                        Rectangle rect2 = new Rectangle(x_pos, y_pos, col2_width, 13);
                                        pe.Graphics.DrawString(((Note)content[i]).date, font, brush, rect2, str_format_center);
                                        x_pos += col2_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                        Rectangle rect3 = new Rectangle(x_pos, y_pos, col3_width, 13);
                                        pe.Graphics.DrawString(((Note)content[i]).start_time, font, brush, rect3, str_format_center);
                                        x_pos += col3_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                        Rectangle rect4 = new Rectangle(x_pos, y_pos, col4_width, 13);
                                        pe.Graphics.DrawString(((Note)content[i]).end_time, font, brush, rect4, str_format_center);
                                        x_pos += col4_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                        Rectangle rect5 = new Rectangle(x_pos, y_pos, col5_width, 13);
                                        pe.Graphics.DrawString(((Note)content[i]).duration, font, brush, rect5, str_format_center);
                                        x_pos += col5_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                        Rectangle rect6 = new Rectangle(x_pos, y_pos, col6_width, 13);
                                        pe.Graphics.DrawString(((Note)content[i]).sernum, font, brush, rect6);
                                        x_pos += col6_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                        Rectangle rect7 = new Rectangle(x_pos, y_pos, col7_width, 13);
                                        pe.Graphics.DrawString(((Note)content[i]).contact, font, brush, rect7);
                                        x_pos += col7_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                        using (Font font_wingdings = new Font("wingdings", 7f))
                                        {
                                            Rectangle rect8 = new Rectangle(x_pos, y_pos, col8_width, 13);
                                            if (((Note)content[i]).map_drive.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect8, str_format_center);
                                            x_pos += col8_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect9 = new Rectangle(x_pos, y_pos, col9_width, 13);
                                            if (((Note)content[i]).install.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect9, str_format_center);
                                            x_pos += col9_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect10 = new Rectangle(x_pos, y_pos, col10_width, 13);
                                            if (((Note)content[i]).error.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect10, str_format_center);
                                            x_pos += col10_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect11 = new Rectangle(x_pos, y_pos, col11_width, 13);
                                            if (((Note)content[i]).fonts.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect11, str_format_center);
                                            x_pos += col11_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect12 = new Rectangle(x_pos, y_pos, col12_width, 13);
                                            if (((Note)content[i]).print.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect12, str_format_center);
                                            x_pos += col12_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect13 = new Rectangle(x_pos, y_pos, col13_width, 13);
                                            if (((Note)content[i]).training.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect13, str_format_center);
                                            x_pos += col13_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect14 = new Rectangle(x_pos, y_pos, col14_width, 13);
                                            if (((Note)content[i]).stock.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect14, str_format_center);
                                            x_pos += col14_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect15 = new Rectangle(x_pos, y_pos, col15_width, 13);
                                            if (((Note)content[i]).form.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect15, str_format_center);
                                            x_pos += col15_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect16 = new Rectangle(x_pos, y_pos, col16_width, 13);
                                            if (((Note)content[i]).rep_excel.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect16, str_format_center);
                                            x_pos += col16_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect17 = new Rectangle(x_pos, y_pos, col17_width, 13);
                                            if (((Note)content[i]).statement.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect17, str_format_center);
                                            x_pos += col17_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect18 = new Rectangle(x_pos, y_pos, col18_width, 13);
                                            if (((Note)content[i]).asset.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect18, str_format_center);
                                            x_pos += col18_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect19 = new Rectangle(x_pos, y_pos, col19_width, 13);
                                            if (((Note)content[i]).secure.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect19, str_format_center);
                                            x_pos += col19_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect20 = new Rectangle(x_pos, y_pos, col20_width, 13);
                                            if (((Note)content[i]).year_end.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect20, str_format_center);
                                            x_pos += col20_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect21 = new Rectangle(x_pos, y_pos, col21_width, 13);
                                            if (((Note)content[i]).period.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect21, str_format_center);
                                            x_pos += col21_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect22 = new Rectangle(x_pos, y_pos, col22_width, 13);
                                            if (((Note)content[i]).mail_wait.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect22, str_format_center);
                                            x_pos += col22_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                            Rectangle rect23 = new Rectangle(x_pos, y_pos, col23_width, 13);
                                            if (((Note)content[i]).transfer_mkt.Length > 0)
                                                pe.Graphics.DrawString(((char)(byte)0xFC).ToString(), font_wingdings, brush, rect23, str_format_center);
                                            x_pos += col23_width;
                                            pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15); // column separator
                                        }

                                        Rectangle rect24 = new Rectangle(x_pos, y_pos, col24_width, 13);
                                        pe.Graphics.DrawString(((Note)content[i]).remark, font, brush, rect24);
                                        x_pos += col24_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                        is_comment = false;
                                        is_complain = false;
                                    }
                                    if (content[i] is SupportNoteComment)
                                    {
                                        using (SolidBrush brush_red = new SolidBrush(Color.Red))
                                        {
                                            using (SolidBrush brush_blue = new SolidBrush(Color.Blue))
                                            {
                                                pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15); // open row separator

                                                if (((SupportNoteComment)content[i]).type == (int)CommentWindow.COMMENT_TYPE.COMMENT)
                                                {
                                                    if (!is_comment)
                                                        pe.Graphics.DrawString("Comment : ", font, brush_blue, new Rectangle(x_pos + 30, y_pos, 100, 13));
                                                    
                                                    pe.Graphics.DrawString(((SupportNoteComment)content[i]).description, font, brush_blue, new Rectangle(x_pos + 90, y_pos, pe.MarginBounds.Right - (x_pos + 90) - pe.MarginBounds.Left, 13));
                                                    is_comment = true;
                                                }

                                                if (((SupportNoteComment)content[i]).type == (int)CommentWindow.COMMENT_TYPE.COMPLAIN)
                                                {
                                                    if (!is_complain)
                                                        pe.Graphics.DrawString("Complain : ", font, brush_red, new Rectangle(x_pos + 30, y_pos, 100, 13));

                                                    pe.Graphics.DrawString(((SupportNoteComment)content[i]).description, font, brush_red, new Rectangle(x_pos + 90, y_pos, pe.MarginBounds.Right - (x_pos + 90) - pe.MarginBounds.Left, 13));
                                                    is_complain = true;
                                                }

                                                pe.Graphics.DrawLine(p, pe.MarginBounds.Right, y_pos - 6, pe.MarginBounds.Right, y_pos + 15);  // close row separator
                                            }
                                        }

                                    }

                                    // Horizontal line
                                    x_pos = 10;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos + 15, x_pos + 1135, y_pos + 15);
                                    #endregion draw row data

                                    row_num++;
                                    y_pos += 20;
                                }
                                #region draw summary data
                                // draw separate column line
                                pe.Graphics.DrawLine(p, 440, y_pos - 12, 440, y_pos + 15);
                                pe.Graphics.DrawLine(p, 465, y_pos - 12, 465, y_pos + 15);
                                pe.Graphics.DrawLine(p, 490, y_pos - 12, 490, y_pos + 15);
                                pe.Graphics.DrawLine(p, 515, y_pos - 12, 515, y_pos + 15);
                                pe.Graphics.DrawLine(p, 540, y_pos - 12, 540, y_pos + 15);
                                pe.Graphics.DrawLine(p, 565, y_pos - 12, 565, y_pos + 15);
                                pe.Graphics.DrawLine(p, 590, y_pos - 12, 590, y_pos + 15);
                                pe.Graphics.DrawLine(p, 615, y_pos - 12, 615, y_pos + 15);
                                pe.Graphics.DrawLine(p, 640, y_pos - 12, 640, y_pos + 15);
                                pe.Graphics.DrawLine(p, 665, y_pos - 12, 665, y_pos + 15);
                                pe.Graphics.DrawLine(p, 690, y_pos - 12, 690, y_pos + 15);
                                pe.Graphics.DrawLine(p, 715, y_pos - 12, 715, y_pos + 15);
                                pe.Graphics.DrawLine(p, 740, y_pos - 12, 740, y_pos + 15);
                                pe.Graphics.DrawLine(p, 765, y_pos - 12, 765, y_pos + 15);
                                pe.Graphics.DrawLine(p, 790, y_pos - 12, 790, y_pos + 15);
                                pe.Graphics.DrawLine(p, 815, y_pos - 12, 815, y_pos + 15);
                                pe.Graphics.DrawLine(p, 840, y_pos - 12, 840, y_pos + 15);
                                // draw double horizontal line
                                pe.Graphics.DrawLine(p, 440, y_pos + 13, 840, y_pos + 13);
                                pe.Graphics.DrawLine(p, 440, y_pos + 15, 840, y_pos + 15);

                                pe.Graphics.DrawString("รวม", font, brush, new RectangleF(350, y_pos, 80, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.map_drive.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(440, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.install.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(465, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.error.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(490, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.fonts.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(515, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.print.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(540, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.training.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(565, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.stock.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(590, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.form.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(615, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.rep_excel.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(640, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.statement.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(665, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.asset.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(690, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.secure.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(715, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.year_end.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(740, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.period.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(765, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.mail_wait.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(790, y_pos, 25, 15), str_format_right);
                                pe.Graphics.DrawString(this.note_list.Where(n => n.transfer_mkt.Length > 0).Count<Note>().ToString(), font, brush, new RectangleF(815, y_pos, 25, 15), str_format_right);
                                #endregion draw summary data

                                y_pos += 20;
                                if (y_pos > pe.MarginBounds.Bottom)
                                {
                                    pe.HasMorePages = true;
                                    return;
                                }
                                else
                                {
                                    pe.HasMorePages = false;
                                }
                                pe.Graphics.DrawString("(เวลารับสาย : " + this.lblWorkTime.Text + ") + (เวลาพักสาย : " + this.lblBreakTime.Text + ") = (เวลารวม : " + this.lblTotalTime.Text + ")" , font, brush, new Rectangle(10, y_pos, 800, 15));

                                y_pos += 20;
                                if (y_pos > pe.MarginBounds.Bottom)
                                {
                                    pe.HasMorePages = true;
                                    return;
                                }
                                else
                                {
                                    pe.HasMorePages = false;
                                }
                                pe.Graphics.DrawString("จำนวนวันลา/ออกพบลูกค้า : " + this.lblPeriodAbsent.Text, font, brush, new Rectangle(10, y_pos, 400, 15));
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

        private void toolStripPrintSummary_Click(object sender, EventArgs e)
        {
            string print_time = "( " + DateTime.Now.ToString() + " )";

            PrintDocument print_doc = new PrintDocument();

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("A4", 825, 1165);
            page_setup.PageSettings.Landscape = false;
            page_setup.PageSettings.Margins = new Margins(0, 0, 0, 40);

            PrintOutputSelection wind = new PrintOutputSelection();
            if (wind.ShowDialog() == DialogResult.OK)
            {
                int row_num = 0;
                int page_no = 0;
                print_doc.BeginPrint += delegate(object obj_sender, PrintEventArgs pe)
                {
                    row_num = 0;
                    page_no = 0;
                };

                print_doc.PrintPage += delegate(object obj_sender, PrintPageEventArgs pe)
                {
                    bool is_new_page = true;
                    page_no++;

                    using (Font font = new Font("tahoma", 8f))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.Black))
                        {
                            using (Pen p = new Pen(Color.LightGray))
                            {
                                int y_pos = 5;
                                #region declare column width
                                int col0_width = 40; // seq
                                int col1_width = 60; // support#
                                int col2_width = 80; // name
                                int col3_width = 110; // talk_line_count
                                int col4_width = 110; // talk_time
                                int col5_width = 150; // talk_time/line_count
                                int col6_width = 110; // break_time
                                int col7_width = 130; // leave_time
                                //int col10_width = 25; // 
                                #endregion declare column width
                                StringFormat str_format_left = new StringFormat();
                                str_format_left.Alignment = StringAlignment.Near;
                                str_format_left.LineAlignment = StringAlignment.Center;

                                StringFormat str_format_center = new StringFormat();
                                str_format_center.Alignment = StringAlignment.Center;
                                str_format_center.LineAlignment = StringAlignment.Center;

                                StringFormat str_format_right = new StringFormat();
                                str_format_right.Alignment = StringAlignment.Far;
                                str_format_right.LineAlignment = StringAlignment.Center;

                                List<Note> note_list = this.JustConvertToNoteList();
                                List<Users> supports = this.list_support_users.Where(s => (s.username.CompareTo(this.current_user_from.username) == 0 || s.username.CompareTo(this.current_user_from.username) > 0) && (s.username.CompareTo(this.current_user_to.username) == 0 || s.username.CompareTo(this.current_user_to.username) < 0)).ToList<Users>();

                                for (int i = row_num; i < supports.Count; i++)
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
                                    if (is_new_page)
                                    {
                                        using (Pen pen_darkgray = new Pen(Color.DarkGray))
                                        {
                                            y_pos += 5;
                                            pe.Graphics.DrawString("สรุปการปฏิบัติงาน(Support)", new Font("tahoma", 11f, FontStyle.Bold), brush, new Rectangle(x_pos, y_pos, 300, 20));
                                            pe.Graphics.DrawString(print_time, font, brush, new Rectangle(x_pos + 650, y_pos, 130, 13), str_format_right);
                                            y_pos += 20;
                                            
                                            pe.Graphics.DrawString("พนักงาน จาก", font, brush, new Rectangle(10, y_pos, 80, 25), str_format_left);
                                            using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                            {
                                                pe.Graphics.DrawString(this.lblUserFrom.Text, font_bold, brush, new Rectangle(90, y_pos, 100, 25), str_format_left);
                                            }
                                            pe.Graphics.DrawString("ถึง", font, brush, new Rectangle(190, y_pos, 30, 25), str_format_center);
                                            using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                            {
                                                pe.Graphics.DrawString(this.lblUserTo.Text, font_bold, brush, new Rectangle(220, y_pos, 100, 25), str_format_left);
                                            }
                                            y_pos += 20;
                                            pe.Graphics.DrawString("วันที่ จาก", font, brush, new Rectangle(10, y_pos, 80, 25), str_format_left);
                                            using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                            {
                                                pe.Graphics.DrawString(this.lblDateFrom.Text, font_bold, brush, new Rectangle(90, y_pos, 100, 25), str_format_left);
                                            }
                                            pe.Graphics.DrawString("ถึง", font, brush, new Rectangle(190, y_pos, 30, 25), str_format_center);
                                            using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                            {
                                                pe.Graphics.DrawString(this.lblDateTo.Text, font_bold, brush, new Rectangle(220, y_pos, 100, 25), str_format_left);
                                            }
                                            pe.Graphics.DrawString("หน้า : " + page_no.ToString(), font, brush, new Rectangle(x_pos + 630, y_pos, 130, 13), str_format_right); // draw page no.
                                            y_pos += 25;

                                            pe.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), new RectangleF(x_pos, y_pos, 790, 25));

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos + 790, y_pos); // horizontal line upper

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("ลำดับ", font, brush, new Rectangle(x_pos, y_pos, col0_width, 25), str_format_center);
                                            x_pos += col0_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("Support#", font, brush, new Rectangle(x_pos, y_pos, col1_width, 25), str_format_center);
                                            x_pos += col1_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("ชื่อ", font, brush, new Rectangle(x_pos, y_pos, col2_width, 25), str_format_center);
                                            x_pos += col2_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("จำนวนสายที่รับ", font, brush, new Rectangle(x_pos, y_pos, col3_width, 25), str_format_center);
                                            x_pos += col3_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("รวมเวลารับสาย", font, brush, new Rectangle(x_pos, y_pos, col4_width, 25), str_format_center);
                                            x_pos += col4_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("เวลาเฉลี่ย/สาย", font, brush, new Rectangle(x_pos, y_pos, col5_width, 25), str_format_center);
                                            x_pos += col5_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("รวมเวลาพักสาย", font, brush, new Rectangle(x_pos, y_pos, col6_width, 25), str_format_center);
                                            x_pos += col6_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("รวมวันลา/ออกพบลูกค้า", font, brush, new Rectangle(x_pos, y_pos, col7_width, 25), str_format_center);
                                            x_pos += col7_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator

                                            x_pos = 10; // set x_pos again after use in header
                                            y_pos += 25;
                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos + 790, y_pos); // horizontal line below
                                        }

                                        y_pos += 7;
                                        is_new_page = false;
                                    }
                                    #endregion draw column header

                                    #region draw row data
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);
                                    using (SolidBrush brush_gray = new SolidBrush(Color.Gray))
                                    {
                                        pe.Graphics.DrawString((i + 1).ToString(), font, brush_gray, new Rectangle(x_pos, y_pos, col0_width - 5, 13), str_format_right);
                                    }
                                    x_pos += col0_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15); // column separator

                                    using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                    {
                                        pe.Graphics.DrawString(supports[i].username, font_bold, brush, new Rectangle(x_pos + 5, y_pos, col1_width - 5, 13), str_format_left);
                                    }
                                    x_pos += col1_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    pe.Graphics.DrawString(supports[i].name, font, brush, new Rectangle(x_pos + 5, y_pos, col2_width - 5, 13), str_format_left);
                                    x_pos += col2_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    pe.Graphics.DrawString(note_list.Where(n => n.users_name == supports[i].username && n.is_break != "Y").Count<Note>().ToString(), font, brush, new Rectangle(x_pos, y_pos, col3_width - 5, 13), str_format_right);
                                    x_pos += col3_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    string talk_time = (note_list.Where(n => n.users_name == supports[i].username && n.is_break != "Y").Count<Note>() > 0 ? Math.Ceiling(note_list.Where(n => n.users_name == supports[i].username).ToList<Note>().GetSummaryTalkTime().TotalMinutes).ToString() + " นาที" : "-");
                                    pe.Graphics.DrawString(talk_time, font, brush, new Rectangle(x_pos, y_pos, col4_width, 13), str_format_center);
                                    x_pos += col4_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    double min_per_line = Math.Floor(Math.Floor(note_list.Where(n => n.users_name == supports[i].username).ToList<Note>().GetSummaryTalkTime().TotalMinutes) / note_list.Where(n => n.users_name == supports[i].username && n.is_break != "Y").Count<Note>());
                                    double sec_per_line = Math.Floor((Math.Floor(note_list.Where(n => n.users_name == supports[i].username).ToList<Note>().GetSummaryTalkTime().TotalSeconds) - (min_per_line * note_list.Where(n => n.users_name == supports[i].username && n.is_break != "Y").Count<Note>() * 60)) / note_list.Where(n => n.users_name == supports[i].username && n.is_break != "Y").Count<Note>());
                                    string time_per_line = (note_list.Where(n => n.users_name == supports[i].username && n.is_break != "Y").Count<Note>() > 0 ? min_per_line.ToString() + " นาที " + sec_per_line.ToString() + " วินาที / 1 สาย" : "-");
                                    pe.Graphics.DrawString(time_per_line, font, brush, new Rectangle(x_pos, y_pos, col5_width, 13), str_format_center);
                                    x_pos += col5_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    string break_time = (note_list.Where(n => n.users_name == supports[i].username && n.is_break != "Y").Count<Note>() > 0 ? Math.Ceiling(note_list.Where(n => n.users_name == supports[i].username).ToList<Note>().GetSummaryBreakTime().TotalMinutes).ToString() + " นาที" : "-");
                                    pe.Graphics.DrawString(break_time, font, brush, new Rectangle(x_pos, y_pos, col6_width, 13), str_format_center);
                                    x_pos += col6_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    pe.Graphics.DrawString(this.event_calendar.Where(c => c.users_name == supports[i].username).ToList<EventCalendar>().GetSummaryHoursMinutesString(), font, brush, new Rectangle(x_pos, y_pos, col7_width, 13), str_format_center);
                                    x_pos += col7_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 15);  // column separator

                                    // Horizontal line
                                    x_pos = 10;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos + 15, x_pos + 790, y_pos + 15);
                                    #endregion draw row data

                                    row_num++;
                                    y_pos += 20;
                                }
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

        private void toolStripExportSummary_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Comma separated value | *.csv";
            dlg.DefaultExt = "csv";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string destination_filename = dlg.FileName;
                List<Note> note_list = this.JustConvertToNoteList();
                List<Users> supports = this.list_support_users.Where(s => (s.username.CompareTo(this.current_user_from.username) == 0 || s.username.CompareTo(this.current_user_from.username) > 0) && (s.username.CompareTo(this.current_user_to.username) == 0 || s.username.CompareTo(this.current_user_to.username) < 0)).ToList<Users>();
                DataTable dt = supports.ToDataTable<Users>();

                StringBuilder sb = new StringBuilder();

                // Create custom column header as we need
                sb.AppendLine("ลำดับ,Support#,ชื่อ,จำนวนสายที่รับ,รวมเวลารับสาย,เวลาเฉลี่ย/สาย,รวมเวลาพักสาย,รวมวันลา/ออกพบลูกค้า");

                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();

                    string line_count = note_list.Where(n => n.users_name == fields[1] && n.is_break != "Y").Count<Note>().ToString();

                    string talk_time = (note_list.Where(n => n.users_name == fields[1] && n.is_break != "Y").Count<Note>() > 0 ? Math.Ceiling(note_list.Where(n => n.users_name == fields[1]).ToList<Note>().GetSummaryTalkTime().TotalMinutes).ToString() + " นาที" : "-");

                    double min_per_line = Math.Floor(Math.Floor(note_list.Where(n => n.users_name == fields[1]).ToList<Note>().GetSummaryTalkTime().TotalMinutes) / note_list.Where(n => n.users_name == fields[1] && n.is_break != "Y").Count<Note>());
                    double sec_per_line = Math.Floor((Math.Floor(note_list.Where(n => n.users_name == fields[1]).ToList<Note>().GetSummaryTalkTime().TotalSeconds) - (min_per_line * note_list.Where(n => n.users_name == fields[1] && n.is_break != "Y").Count<Note>() * 60)) / note_list.Where(n => n.users_name == fields[1] && n.is_break != "Y").Count<Note>());
                    string time_per_line = (note_list.Where(n => n.users_name == fields[1] && n.is_break != "Y").Count<Note>() > 0 ? min_per_line.ToString() + " นาที " + sec_per_line.ToString() + " วินาที / 1 สาย" : "-");

                    string break_time = (note_list.Where(n => n.users_name == fields[1] && n.is_break != "Y").Count<Note>() > 0 ? Math.Ceiling(note_list.Where(n => n.users_name == fields[1]).ToList<Note>().GetSummaryBreakTime().TotalMinutes).ToString() + " นาที" : "-");

                    string leave_days = this.event_calendar.Where(c => c.users_name == fields[1]).ToList<EventCalendar>().GetSummaryLeaveDayString().Replace(",", " : ");

                    // Append some column data as we needf
                    sb.AppendLine(  (++count).ToString() + "," +
                                    fields[1] + "," +
                                    fields[3] + "," +
                                    line_count + "," +
                                    talk_time + "," +
                                    time_per_line + "," +
                                    break_time + "," +
                                    leave_days);

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
            //this.FormEdit();
            //string remark = (string)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[27].Value;

            //CustomTextBox ct = new CustomTextBox();
            //ct.Read_Only = false;
            //ct.BorderStyle = BorderStyle.None;
            //ct.Name = "txt_inline_remark";
            //ct.Texts = remark;
            
            //this.dgvNote.Parent.Controls.Add(ct);
            //this.AdjustInlineFormPositon();

            //this.dgvNote.Enabled = false;
            //this.dgvNote.SendToBack();
            //ct.BringToFront();
            //ct.Focus();
            //ct.SelectionStart = 0;
            //ct.SelectionLength = 0;
            if (this.dgvNote.CurrentCell == null)
                return;

            SupportNoteDialog sd;
            if (((SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[0].Value).is_break == "Y")
            {
                sd = new SupportNoteDialog(this, true, (SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[0].Value);
            }
            else
            {
                sd = new SupportNoteDialog(this, false, (SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[0].Value);
            }

            if (sd.ShowDialog() == DialogResult.OK)
            {
                this.GetNote(0, sd.note.date, sd.note.start_time, sd.note.end_time);
            }

        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            this.ClearInlineForm();
            this.FormRead();
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            int note_id = ((SupportNote)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[0].Value).id;
            string remark = "";
            if (this.dgvNote.Parent.Controls.Find("txt_inline_remark", true).Length > 0)
            {
                CustomTextBox ct = (CustomTextBox)this.dgvNote.Parent.Controls.Find("txt_inline_remark", true)[0];
                remark = ct.Texts;
            }
            this.FormProcessing();

            string json_data = "{\"id\":" + note_id.ToString() + ",";
            json_data += "\"remark\":\"" + remark.cleanString() + "\",";
            json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";
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
                Rectangle cell27_rect = this.dgvNote.GetCellDisplayRectangle(27, this.dgvNote.CurrentCell.RowIndex, true);
                //Console.WriteLine(" >>>> current cell.row_index : " + this.dgvNote.CurrentCell.RowIndex.ToString());
                ct.SetBounds(cell27_rect.X + 3, cell27_rect.Y + 1, cell27_rect.Width - 1, cell27_rect.Height - 2);
            }
        }

        private void toolStripRange_Click(object sender, EventArgs e)
        {
            LeaveRangeDialog dlg = new LeaveRangeDialog(this.main_form);
            dlg.Text = "กำหนดขอบเขตการแสดงข้อมูลการปฏิบัติงาน(Support)";
            dlg.user_from = this.current_user_from;
            dlg.user_to = this.current_user_to;
            dlg.date_from = this.current_date_from;
            dlg.date_to = this.current_date_to;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.current_user_from = dlg.user_from;
                this.current_user_to = dlg.user_to;
                this.current_date_from = dlg.date_from;
                this.current_date_to = dlg.date_to;
                this.GetNote();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
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
                this.toolStripExportDetail.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.F12))
            {
                this.toolStripExportSummary.PerformClick();
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
            if (keyData == (Keys.Alt | Keys.P))
            {
                this.toolStripPrintDetail.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.P))
            {
                this.toolStripPrintSummary.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void lblPeriodAbsent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.lblPeriodAbsent.Text != "-")
            {
                if (this.main_form.leave_wind == null)
                {
                    LeaveWindow wind = new LeaveWindow(this.main_form, this.current_user_from, this.current_user_to, this.current_date_from, this.current_date_to);
                    wind.MdiParent = this.main_form;
                    wind.Show();
                }
                else
                {
                    this.main_form.leave_wind.CrossingCall(this.current_user_from, this.current_user_to, this.current_date_from, this.current_date_to);
                    this.main_form.leave_wind.Activate();
                }
            }
        }

        private void SupportStatWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.UserClosing) && this.form_mode != FORM_MODE.READ)
            {
                if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "SN_Net", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void btnAddTalk_Click(object sender, EventArgs e)
        {
            SupportNoteDialog sd = new SupportNoteDialog(this, false);
            if (sd.ShowDialog() == DialogResult.OK)
            {
                this.GetNote(0, sd.note.date, sd.note.start_time, sd.note.end_time);
            }
        }

        private void btnAddBreak_Click(object sender, EventArgs e)
        {
            SupportNoteDialog sd = new SupportNoteDialog(this, true);
            if (sd.ShowDialog() == DialogResult.OK)
            {
                this.GetNote(0, sd.note.date, sd.note.start_time, sd.note.end_time);
            }
        }

    }
}
