using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.Model;
using SN_Net.MiscClass;
using CC;
using System.Globalization;
using System.Drawing.Printing;

namespace SN_Net.Subform
{
    public partial class FormAbsentReport : Form
    {
        /* for person absent */
        private users users_from = null;
        private users users_to = null;
        private DateTime? date_from = null;
        private DateTime? date_to = null;

        /* for summary absent */
        private users year_users_from = null;
        private users year_users_to = null;
        private DateTime? year_date_from = null;
        private DateTime? year_date_to = null;

        private List<event_calendar> person_events;
        private BindingList<AbsentCauseVM> cause1_list;
        private BindingList<AbsentCauseVM> cause2_list;
        private BindingList<AbsentPersonStatVM> absent_person_list;
        private BindingList<SummaryAbsent> absent_summary_list;
        private TimeSpan yearly_absent;
        private TimeSpan yearly_cust;
        private FORM_MODE form_mode;
        private event_calendar tmp_event_cal;
        private MainForm main_form;

        public FormAbsentReport(MainForm main_form)
        {
            this.main_form = main_form;
            InitializeComponent();
        }

        private void FormAbsentReport_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;

            Enum.GetValues(typeof(CALENDAR_EVENT_STATUS)).Cast<CALENDAR_EVENT_STATUS>().ToList().ForEach(s => this.inlineStatus._Items.Add(new XDropdownListItem { Text = s.ToString(), Value = (int)s }));
            this.inlineMedcert._Items.Add(new XDropdownListItem { Text = "N/A (ไม่ระบุ)", Value = CALENDAR_EVENT_MEDCERT.NOT_ASSIGN });
            this.inlineMedcert._Items.Add(new XDropdownListItem { Text = "ไม่มีเอกสาร", Value = CALENDAR_EVENT_MEDCERT.NOT_HAVE_MEDCERT });
            this.inlineMedcert._Items.Add(new XDropdownListItem { Text = "มีเอกสารอื่น ๆ", Value = CALENDAR_EVENT_MEDCERT.OTHER_DOCUMENT });
            this.inlineMedcert._Items.Add(new XDropdownListItem { Text = "มีใบรับรองแพทย์", Value = CALENDAR_EVENT_MEDCERT.HAVE_MEDCERT });
            this.RemoveInlineForm();
        }

        private void FormAbsentReport_Shown(object sender, EventArgs e)
        {
            if(this.users_from == null || this.users_to == null || this.date_from == null || this.date_to == null)
            {
                DialogAbsentReportScope scope = new DialogAbsentReportScope();
                if(scope.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
                else
                {
                    this.users_from = scope.user_from;
                    this.users_to = scope.user_to;
                    this.date_from = scope.date_from;
                    this.date_to = scope.date_to;

                    this.dtYearAbsentFrom.Value = DateTime.Now; //DateTime.Parse(this.date_to.Value.ToString("yyyy", CultureInfo.GetCultureInfo("en-US")) + "-01-01", CultureInfo.GetCultureInfo("en-US"));
                    this.dtYearAbsentTo.Value = DateTime.Now; //DateTime.Parse(this.date_to.Value.ToString("yyyy", CultureInfo.GetCultureInfo("en-US")) + "-12-31", CultureInfo.GetCultureInfo("en-US"));

                    this.GetData();
                    this.FillForm();
                    //this.ShowAbsentSummaryData();
                }
            }
            
        }

        private void GetData(/*List<note_istab> accepted_causes = null*/)
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                this.person_events = note.event_calendar.Where(ev => ev.users_name.Trim() == this.users_from.username.Trim() && ev.date.CompareTo(this.date_from.Value) >= 0 && ev.date.CompareTo(this.date_to.Value) <= 0).OrderBy(ev => ev.date).ThenBy(ev => ev.from_time).ToList();
                List<AbsentPersonStatVM> person_absent = this.person_events.ToAbsentPersonStatVM();
                //if (accepted_causes == null)
                //{
                //    person_absent = this.person_events.ToAbsentPersonStatVM();
                //}
                //else
                //{
                //    int?[] causes_ids = accepted_causes.Select(a => (int?)a.id).ToArray();
                //    person_absent = this.person_events.Where(ev => causes_ids.Contains(ev.event_code_id)).OrderBy(ev => ev.date).ThenBy(ev => ev.from_time).ToAbsentPersonStatVM();
                //}

                var p_seq = 0;
                person_absent.ForEach(ev => { p_seq++; ev.seq = p_seq; });
                this.absent_person_list = new BindingList<AbsentPersonStatVM>(person_absent);
                

                

                DateTime first_date_of_year = DateTime.Parse(this.date_to.Value.ToString("yyyy", CultureInfo.GetCultureInfo("en-US")) + "-01-01", CultureInfo.GetCultureInfo("en-US"));
                DateTime last_date_of_year = DateTime.Parse(this.date_to.Value.ToString("yyyy", CultureInfo.GetCultureInfo("en-US")) + "-12-31", CultureInfo.GetCultureInfo("en-US"));

                this.yearly_absent = TimeSpan.Parse("00:00");
                var year_abs = note.event_calendar.Where(ev => ev.users_name.Trim() == this.users_from.username.Trim() &&
                                ev.event_type == CALENDAR_EVENT_TYPE.ABSENT && 
                                ev.status != (int)CALENDAR_EVENT_STATUS.CANCELED &&
                                ev.date.CompareTo(first_date_of_year.Date) >= 0 && ev.date.CompareTo(last_date_of_year.Date) <= 0).Select(ev => new EventCalYearlyTimeSpan { event_calendar = ev }).ToList();
                year_abs.ForEach(ev => { this.yearly_absent = this.yearly_absent.Add(ev.time_span); });

                this.yearly_cust = TimeSpan.Parse("00:00");
                var year_cust = note.event_calendar.Where(ev => ev.users_name.Trim() == this.users_from.username.Trim() &&
                                ev.event_type == CALENDAR_EVENT_TYPE.MEET_CUST &&
                                ev.status != (int)CALENDAR_EVENT_STATUS.CANCELED &&
                                ev.date.CompareTo(first_date_of_year.Date) >= 0 && ev.date.CompareTo(last_date_of_year.Date) <= 0).Select(ev => new EventCalYearlyTimeSpan { event_calendar = ev }).ToList();
                year_cust.ForEach(ev => { this.yearly_cust = this.yearly_cust.Add(ev.time_span); });
            }
        }

        private void FillForm()
        {
            this.grpPtd.Text = "สรุปในช่วงวันที่ (" + this.date_from.Value.ToString("dd/MM/yy", CultureInfo.GetCultureInfo("th-TH")) + " - " + this.date_to.Value.ToString("dd/MM/yy", CultureInfo.GetCultureInfo("th-TH")) + ")";
            this.grpYear.Text = "สะสมจากต้นปี (ปี " + this.date_to.Value.ToString("yyyy", CultureInfo.GetCultureInfo("th-TH")) + ")";
            this.lblUserFrom.Text = this.users_from.username + " : " + this.users_from.name;
            this.lblUserTo.Text = this.users_to.username + " : " + this.users_to.name;
            this.lblDateFrom.Text = this.date_from.Value.ToString("dddd  d MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            this.lblDateTo.Text = this.date_to.Value.ToString("dddd  d MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            
            TimeSpan ts = TimeSpan.Parse("00:00");
            this.absent_person_list.Where(i => i.event_calendar.status != (int)CALENDAR_EVENT_STATUS.CANCELED && i.event_calendar.event_type == CALENDAR_EVENT_TYPE.ABSENT).ToList().ForEach(i =>
            {
                TimeSpan t = TimeSpan.Parse(i.time_to).Subtract(TimeSpan.Parse(i.time_from));
                if (TimeSpan.Parse(i.time_from).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(i.time_to).CompareTo(TimeSpan.Parse("13:00")) > 0)
                {
                    t = t.Subtract(TimeSpan.Parse("01:00"));
                }
                ts = ts.Add(t);
            });
            this.lblPtdAbsent.Text = ts.GetTimeSpanString();

            ts = TimeSpan.Parse("00:00");
            this.absent_person_list.Where(i => i.event_calendar.status != (int)CALENDAR_EVENT_STATUS.CANCELED && i.event_calendar.event_type == CALENDAR_EVENT_TYPE.MEET_CUST).ToList().ForEach(i =>
            {
                TimeSpan t = TimeSpan.Parse(i.time_to).Subtract(TimeSpan.Parse(i.time_from));
                if (TimeSpan.Parse(i.time_from).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(i.time_to).CompareTo(TimeSpan.Parse("13:00")) > 0)
                {
                    t = t.Subtract(TimeSpan.Parse("01:00"));
                }
                ts = ts.Add(t);
            });
            this.lblPtdCust.Text = ts.GetTimeSpanString();

            this.lblYearAbsent.Text = this.yearly_absent.GetTimeSpanString() + " (Max. " + this.users_from.max_absent.ToString() + " วัน)";
            this.lblYearAbsent.ForeColor = this.yearly_absent.GetTotalDays() > this.users_from.max_absent ? Color.Red : Color.Black;

            this.lblYearCust.Text = this.yearly_cust.GetTimeSpanString();

            this.dgvDetail.DataSource = this.absent_person_list;
            this.dgvSum.DataSource = this.absent_summary_list;
            this.SetCauseList();
            this.SetDropDownUserItem();
            this.dgvDetail.Focus();
        }

        private void SetCauseList()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                var list1 = note.note_istab.Where(i => i.tabtyp == CALENDAR_EVENT_TYPE.ABSENT).OrderBy(i => i.typdes_th).ToAbsentCauseVM();
                list1.ForEach(i =>
                {
                    TimeSpan ts = TimeSpan.Parse("00:00");
                    this.absent_person_list.Where(a => a.event_calendar.event_code_id == i.istab.id && a.event_calendar.status != (int)CALENDAR_EVENT_STATUS.CANCELED).ToList().ForEach(j =>
                    {
                        TimeSpan day_prd = TimeSpan.Parse(j.time_to).Subtract(TimeSpan.Parse(j.time_from));
                        if(TimeSpan.Parse(j.time_from).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(j.time_to).CompareTo(TimeSpan.Parse("13:00")) > 0)
                        {
                            day_prd = day_prd.Subtract(TimeSpan.Parse("01:00"));
                        }
                        ts = ts.Add(day_prd);
                    });
                    i.stat = ts.GetTimeSpanString();
                    i.enabled = this.person_events.Where(pe => pe.event_code_id == i.istab.id).Count() > 0 ? true : false;
                });
                this.cause1_list = new BindingList<AbsentCauseVM>(list1);
                
                this.dgvAbsent.DataSource = this.cause1_list;

                var list2 = note.note_istab.Where(i => i.tabtyp == CALENDAR_EVENT_TYPE.MEET_CUST).OrderBy(i => i.typdes_th).ToAbsentCauseVM();
                list2.ForEach(i =>
                {
                    TimeSpan ts = TimeSpan.Parse("00:00");
                    this.absent_person_list.Where(a => a.event_calendar.event_code_id == i.istab.id && a.event_calendar.status != (int)CALENDAR_EVENT_STATUS.CANCELED).ToList().ForEach(j =>
                    {
                        TimeSpan day_prd = TimeSpan.Parse(j.time_to).Subtract(TimeSpan.Parse(j.time_from));
                        if (TimeSpan.Parse(j.time_from).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(j.time_to).CompareTo(TimeSpan.Parse("13:00")) > 0)
                        {
                            day_prd = day_prd.Subtract(TimeSpan.Parse("01:00"));
                        }
                        ts = ts.Add(day_prd);
                    });
                    i.stat = ts.GetTimeSpanString();
                    i.enabled = this.person_events.Where(pe => pe.event_code_id == i.istab.id).Count() > 0 ? true : false;
                });
                this.cause2_list = new BindingList<AbsentCauseVM>(list2);
                this.dgvCust.DataSource = this.cause2_list;
            }
        }

        private void SetDropDownUserItem()
        {
            using (snEntities sn = DBX.DataSet())
            {
                this.drYearAbsentUserFrom._Items.Add(new XDropdownListItem { Text = "", Value = null });
                this.drYearAbsentUserTo._Items.Add(new XDropdownListItem { Text = "", Value = null });

                var users = sn.users.OrderBy(u => u.username).ToList();
                users.ForEach(u =>
                {
                    this.drYearAbsentUserFrom._Items.Add(new XDropdownListItem { Text = u.username + " : " + u.name, Value = u });
                    this.drYearAbsentUserTo._Items.Add(new XDropdownListItem { Text = u.username + " : " + u.name, Value = u });
                });

                this.drYearAbsentUserFrom._SelectedItem = this.drYearAbsentUserFrom._Items.Cast<XDropdownListItem>().Where(i => i.Value == null).FirstOrDefault();
                this.drYearAbsentUserTo._SelectedItem = this.drYearAbsentUserTo._Items.Cast<XDropdownListItem>().Where(i => i.Value == null).FirstOrDefault();
            }
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.dgvDetail.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.dgvAbsent.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.dgvCust.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.dgvSum.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);

            this.btnPrint.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnChangeScope.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnEditItem.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSaveItem.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnStopItem.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT_ITEM }, this.form_mode);

            this.btnAllAbsent.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnNoneAbsent.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnAllCust.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnNoneCust.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);

            //this.inlineFrom.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT_ITEM }, this.form_mode);
            //this.inlineTo.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT_ITEM }, this.form_mode);
            //this.inlineStatus.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT_ITEM }, this.form_mode);
            //this.inlineCustomer.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT_ITEM }, this.form_mode);
            //this.inlineMedcert.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT_ITEM }, this.form_mode);
            //this.inlineFine.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT_ITEM }, this.form_mode);
        }

        private void ShowInlineForm()
        {
            if (this.dgvDetail.CurrentCell == null)
                return;

            using (sn_noteEntities note = DBXNote.DataSet())
            {
                this.tmp_event_cal = note.event_calendar.Find(((event_calendar)this.dgvDetail.Rows[this.dgvDetail.CurrentCell.RowIndex].Cells[this.col_c3_event_calendar.Name].Value).id);
                if (this.tmp_event_cal != null)
                {
                    this.inlineFrom.Value = new DateTime(this.tmp_event_cal.date.Year, this.tmp_event_cal.date.Month, this.tmp_event_cal.date.Day, TimeSpan.Parse(this.tmp_event_cal.from_time).Hours, TimeSpan.Parse(this.tmp_event_cal.from_time).Minutes, 0);
                    this.inlineTo.Value = new DateTime(this.tmp_event_cal.date.Year, this.tmp_event_cal.date.Month, this.tmp_event_cal.date.Day, TimeSpan.Parse(this.tmp_event_cal.to_time).Hours, TimeSpan.Parse(this.tmp_event_cal.to_time).Minutes, 0);
                    this.inlineStatus._SelectedItem = this.inlineStatus._Items.Cast<XDropdownListItem>().Where(s => (int?)s.Value == this.tmp_event_cal.status).FirstOrDefault();
                    this.inlineCustomer._Text = this.tmp_event_cal.customer;
                    this.inlineMedcert._SelectedItem = this.inlineMedcert._Items.Cast<XDropdownListItem>().Where(s => (string)s.Value == this.tmp_event_cal.med_cert).FirstOrDefault();
                    this.inlineFine.Value = this.tmp_event_cal.fine.Value;
                    this.SetInlineControlPosition();
                }
            }
        }

        private void SetInlineControlPosition()
        {
            if (this.dgvDetail.CurrentCell == null)
                return;

            int col_index = this.dgvDetail.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_c3_time_from.Name).FirstOrDefault().Index;
            this.inlineFrom.SetInlineControlPosition(this.dgvDetail, this.dgvDetail.CurrentCell.RowIndex, col_index);

            col_index = this.dgvDetail.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_c3_time_to.Name).FirstOrDefault().Index; ;
            this.inlineTo.SetInlineControlPosition(this.dgvDetail, this.dgvDetail.CurrentCell.RowIndex, col_index);

            col_index = this.dgvDetail.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_c3_status.Name).FirstOrDefault().Index; ;
            this.inlineStatus.SetInlineControlPosition(this.dgvDetail, this.dgvDetail.CurrentCell.RowIndex, col_index);

            col_index = this.dgvDetail.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_c3_customer.Name).FirstOrDefault().Index;
            this.inlineCustomer.SetInlineControlPosition(this.dgvDetail, this.dgvDetail.CurrentCell.RowIndex, col_index);

            col_index = this.dgvDetail.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_c3_medcert.Name).FirstOrDefault().Index;
            this.inlineMedcert.SetInlineControlPosition(this.dgvDetail, this.dgvDetail.CurrentCell.RowIndex, col_index);

            col_index = this.dgvDetail.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_c3_fine.Name).FirstOrDefault().Index;
            this.inlineFine.SetInlineControlPosition(this.dgvDetail, this.dgvDetail.CurrentCell.RowIndex, col_index);
        }

        private void RemoveInlineForm()
        {
            this.inlineFrom.SetBounds(-9999, -9999, this.inlineFrom.Width, this.inlineFrom.Height);
            this.inlineTo.SetBounds(-9999, -9999, this.inlineTo.Width, this.inlineTo.Height);
            this.inlineStatus.SetBounds(-9999, -9999, this.inlineStatus.Width, this.inlineStatus.Height);
            this.inlineCustomer.SetBounds(-9999, -9999, this.inlineCustomer.Width, this.inlineCustomer.Height);
            this.inlineMedcert.SetBounds(-9999, -9999, this.inlineMedcert.Width, this.inlineMedcert.Height);
            this.inlineFine.SetBounds(-9999, -9999, this.inlineFine.Width, this.inlineFine.Height);

            this.tmp_event_cal = null;
        }

        private void dgvDetail_Resize(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.SetInlineControlPosition();
            }
        }

        private void btnPrint1_Click(object sender, EventArgs e)
        {
            if(this.absent_person_list.Count == 0)
            {
                MessageAlert.Show("ไม่มีข้อมูลตามขอบเขตที่ระบุ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return;
            }

            DialogPrintOutput p = new DialogPrintOutput();
            if(p.ShowDialog() == DialogResult.OK)
            {
                if(p.output == PRINT_OUTPUT.SCREEN)
                {
                    int total_page = XPrintPreview.GetTotalPageCount(this.GetPrintPersonAbsentDocument(this.absent_person_list.ToList()));
                    using (PrintDocument pdoc = this.GetPrintPersonAbsentDocument(this.absent_person_list.ToList(), total_page))
                    {
                        using (PrintDocument pdoc2 = this.GetPrintPersonAbsentDocument(this.absent_person_list.ToList(), total_page))
                        {
                            XPrintPreview xp = new XPrintPreview(pdoc, pdoc2, total_page);
                            xp.MdiParent = this.main_form;
                            xp.Show();
                        }
                    }
                }

                if(p.output == PRINT_OUTPUT.PRINTER)
                {
                    PrintDialog print_dialog = new PrintDialog();
                    int total_page = XPrintPreview.GetTotalPageCount(this.GetPrintPersonAbsentDocument(this.absent_person_list.ToList()));
                    print_dialog.Document = this.GetPrintPersonAbsentDocument(this.absent_person_list.ToList(), total_page);
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = false;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = false;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_dialog.Document.Print();
                    }
                }
            }
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            if(this.absent_summary_list == null)
            {
                MessageAlert.Show("กรุณากำหนดขอบเขตการแสดงข้อมูลสรุปวันลาก่อนสั่งพิมพ์รายงาน", "", MessageAlertButtons.OK, MessageAlertIcons.INFORMATION);
                if(this.form_mode == FORM_MODE.READ)
                {
                    this.tabControl1.SelectedTab = this.tabPage2;
                }
                return;
            }

            if (this.btnOKYearAbsent.Enabled)
            {
                MessageAlert.Show("ท่านยังไม่ได้คลิกปุ่ม \"ตกลง (แสดงข้อมูล)\", กรุณาคลิกปุ่มดังกล่าวก่อนสั่งพิมพ์รายงาน", "", MessageAlertButtons.OK, MessageAlertIcons.INFORMATION);
                return;
            }

            if(this.absent_summary_list.Count == 0)
            {
                MessageAlert.Show("ไม่มีข้อมูลตามขอบเขตที่ระบุ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return;
            }

            DialogPrintOutput p = new DialogPrintOutput();
            if (p.ShowDialog() == DialogResult.OK)
            {
                if (p.output == PRINT_OUTPUT.SCREEN)
                {
                    int total_page = XPrintPreview.GetTotalPageCount(this.GetPrintSumAbsentDocument(this.absent_summary_list.ToList()));
                    using (PrintDocument pdoc1 = this.GetPrintSumAbsentDocument(this.absent_summary_list.ToList(), total_page))
                    {
                        using (PrintDocument pdoc2 = this.GetPrintSumAbsentDocument(this.absent_summary_list.ToList(), total_page))
                        {
                            XPrintPreview xp = new XPrintPreview(pdoc1, pdoc2, total_page);
                            xp.MdiParent = this.main_form;
                            xp.Show();
                        }
                    }
                }

                if (p.output == PRINT_OUTPUT.PRINTER)
                {
                    PrintDialog print_dialog = new PrintDialog();
                    int total_page = XPrintPreview.GetTotalPageCount(this.GetPrintSumAbsentDocument(this.absent_summary_list.ToList()));
                    print_dialog.Document = this.GetPrintSumAbsentDocument(this.absent_summary_list.ToList(), total_page);
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = false;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = false;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_dialog.Document.Print();
                    }
                }
            }
        }

        private PrintDocument GetPrintPersonAbsentDocument(List<AbsentPersonStatVM> absents, int? total_page =  null)
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Margins = new Margins(20, 40, 40, 50);
            pd.DefaultPageSettings.Landscape = true;
            int item_count = 0;
            int page_count = 0;
            DateTime print_time = DateTime.Now;

            pd.BeginPrint += delegate(object sender, PrintEventArgs e)
            {
                page_count = 0;
                item_count = 0;

            };

            pd.PrintPage += delegate(object sender, PrintPageEventArgs e)
            {
                Font f = new Font("tahoma", 8f);
                Font f_bold = new Font("tahoma", 8f, FontStyle.Bold);
                Font f_title = new Font("tahoma", 10f, FontStyle.Bold);
                SolidBrush b = new SolidBrush(Color.Black);
                SolidBrush b_red = new SolidBrush(Color.Red);
                SolidBrush b_blue = new SolidBrush(Color.Blue);
                SolidBrush b_gray = new SolidBrush(Color.DimGray);
                SolidBrush b_background = new SolidBrush(Color.Lavender);
                SolidBrush b_whitesmoke = new SolidBrush(Color.WhiteSmoke);
                Pen p = new Pen(new SolidBrush(Color.DarkGray));
                StringFormat sf_left = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };
                StringFormat sf_center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };
                StringFormat sf_right = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };

                page_count++;
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                int line_gap = 10;

                Rectangle rect = new Rectangle(e.MarginBounds.Left, y, 350, f_title.Height);
                e.Graphics.DrawString("รายละเอียดวันลา/ออกพบลูกค้า", f_title, b, rect, sf_left);

                rect = new Rectangle(e.MarginBounds.Right - 100, y, 100, f.Height);
                e.Graphics.DrawString("หน้า : " + page_count.ToString() + (total_page.HasValue ? " / " + total_page.ToString() : ""), f, b, rect, sf_right);
                y += f_title.Height + line_gap;

                rect = new Rectangle(e.MarginBounds.Left, y, 80, f.Height);
                e.Graphics.DrawString("รหัสพนักงาน", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 20;
                e.Graphics.DrawString(":", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 100;
                e.Graphics.DrawString(absents.First().user_name, f, b, rect, sf_left);
                y += f.Height + line_gap;

                rect = new Rectangle(e.MarginBounds.Left, y, 80, f.Height);
                e.Graphics.DrawString("วันที่ จาก", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 20;
                e.Graphics.DrawString(":", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 150;
                e.Graphics.DrawString(this.lblDateFrom.Text, f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 30;
                e.Graphics.DrawString("ถึง", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 10;
                e.Graphics.DrawString(":", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 150;
                e.Graphics.DrawString(this.lblDateTo.Text, f, b, rect, sf_left);

                rect = new Rectangle(e.MarginBounds.Right - 250, y, 250, f.Height);
                e.Graphics.DrawString("(วัน/เวลาที่พิมพ์ : " + print_time.ToString("dd/MM/yy HH:mm:ss", CultureInfo.GetCultureInfo("th-TH")) + ")", f, b, rect, sf_right);
                y += f.Height + line_gap;

                e.Graphics.FillRectangle(b_background, new Rectangle(e.MarginBounds.X, y, e.MarginBounds.Width, f.Height + line_gap));
                e.Graphics.DrawLine(p, new Point(x, y), new Point(e.MarginBounds.Right, y));
                
                Rectangle rect_seq = new Rectangle(e.MarginBounds.X, y, 40, f.Height + line_gap);
                Rectangle rect_user = new Rectangle(rect_seq.X + rect_seq.Width, y, 80, f.Height + line_gap);
                Rectangle rect_date = new Rectangle(rect_user.X + rect_user.Width, y, 90, f.Height + line_gap);
                Rectangle rect_from = new Rectangle(rect_date.X + rect_date.Width, y, 45, f.Height + line_gap);
                Rectangle rect_to = new Rectangle(rect_from.X + rect_from.Width, y, 45, f.Height + line_gap);
                Rectangle rect_duration = new Rectangle(rect_to.X + rect_to.Width, y, 120, f.Height + line_gap);
                Rectangle rect_reason = new Rectangle(rect_duration.X + rect_duration.Width, y, 140, f.Height + line_gap);
                Rectangle rect_status = new Rectangle(rect_reason.X + rect_reason.Width, y, 90, f.Height + line_gap);
                Rectangle rect_customer = new Rectangle(rect_status.X + rect_status.Width, y, 250, f.Height + line_gap);
                Rectangle rect_medcert = new Rectangle(rect_customer.X + rect_customer.Width, y, 100, f.Height + line_gap);
                Rectangle rect_fine = new Rectangle(rect_medcert.X + rect_medcert.Width, y, e.MarginBounds.Width - (rect_seq.Width + rect_user.Width + rect_date.Width + rect_from.Width + rect_to.Width + rect_duration.Width + rect_reason.Width + rect_status.Width + rect_customer.Width + rect_medcert.Width), f.Height + line_gap);
                e.Graphics.DrawRectangle(p, rect_seq);
                e.Graphics.DrawRectangle(p, rect_user);
                e.Graphics.DrawRectangle(p, rect_date);
                e.Graphics.DrawRectangle(p, rect_from);
                e.Graphics.DrawRectangle(p, rect_to);
                e.Graphics.DrawRectangle(p, rect_duration);
                e.Graphics.DrawRectangle(p, rect_reason);
                e.Graphics.DrawRectangle(p, rect_status);
                e.Graphics.DrawRectangle(p, rect_customer);
                e.Graphics.DrawRectangle(p, rect_medcert);
                e.Graphics.DrawRectangle(p, rect_fine);
                e.Graphics.DrawString("ลำดับ", f, b, rect_seq, sf_center);
                e.Graphics.DrawString("รหัสพนักงาน", f, b, rect_user, sf_center);
                e.Graphics.DrawString("วันที่", f, b, rect_date, sf_center);
                e.Graphics.DrawString("จาก", f, b, rect_from, sf_center);
                e.Graphics.DrawString("ถึง", f, b, rect_to, sf_center);
                e.Graphics.DrawString("รวมเวลา", f, b, rect_duration, sf_center);
                e.Graphics.DrawString("เหตุผล", f, b, rect_reason, sf_center);
                e.Graphics.DrawString("จาก", f, b, rect_from, sf_center);
                e.Graphics.DrawString("ถึง", f, b, rect_to, sf_center);
                e.Graphics.DrawString("รวมเวลา", f, b, rect_duration, sf_center);
                e.Graphics.DrawString("เหตุผล", f, b, rect_reason, sf_center);
                e.Graphics.DrawString("สถานะ", f, b, rect_status, sf_center);
                e.Graphics.DrawString("ชื่อลูกค้า", f, b, rect_customer, sf_center);
                e.Graphics.DrawString("เอกสาร", f, b, rect_medcert, sf_center);
                e.Graphics.DrawString("หักค่าคอมฯ", f, b, rect_fine, sf_center);
                y += f.Height + line_gap;


                for (int i = item_count; i < absents.Count; i++)
                {
                    if (y > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    rect_seq.Y = y;
                    rect_user.Y = y;
                    rect_date.Y = y;
                    rect_from.Y = y;
                    rect_to.Y = y;
                    rect_duration.Y = y;
                    rect_reason.Y = y;
                    rect_status.Y = y;
                    rect_customer.Y = y;
                    rect_medcert.Y = y;
                    rect_fine.Y = y;

                    if(i%2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_seq); }
                    e.Graphics.DrawRectangle(p, rect_seq);
                    e.Graphics.DrawString((i + 1).ToString(), f, b_gray, rect_seq, sf_right);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_user); }
                    e.Graphics.DrawRectangle(p, rect_user);
                    e.Graphics.DrawString(absents[i].user_name, f, b, rect_user, sf_center);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_date); }
                    e.Graphics.DrawRectangle(p, rect_date);
                    e.Graphics.DrawString(absents[i].date.ToString("ddd dd/MM/yy", CultureInfo.GetCultureInfo("th-TH")), f, b, rect_date, sf_left);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_from); }
                    e.Graphics.DrawRectangle(p, rect_from);
                    e.Graphics.DrawString(absents[i].time_from, f, b, rect_from, sf_center);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_to); }
                    e.Graphics.DrawRectangle(p, rect_to);
                    e.Graphics.DrawString(absents[i].time_to, f, b, rect_to, sf_center);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_duration); }
                    e.Graphics.DrawRectangle(p, rect_duration);
                    e.Graphics.DrawString(absents[i].duration, f, b, rect_duration, sf_left);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_reason); }
                    e.Graphics.DrawRectangle(p, rect_reason);
                    e.Graphics.DrawString(absents[i].reason, f, b, rect_reason, sf_left);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_status); }
                    e.Graphics.DrawRectangle(p, rect_status);
                    SolidBrush brush = absents[i].event_calendar.status.Value == (int)CALENDAR_EVENT_STATUS.CANCELED ? b_red : (absents[i].event_calendar.status.Value == (int)CALENDAR_EVENT_STATUS.WAIT ? b_blue : b);
                    e.Graphics.DrawString(absents[i].status, f, brush, rect_status, sf_left);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_customer); }
                    e.Graphics.DrawRectangle(p, rect_customer);
                    e.Graphics.DrawString(absents[i].customer, f, b, rect_customer, sf_left);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_medcert); }
                    e.Graphics.DrawRectangle(p, rect_medcert);
                    e.Graphics.DrawString(absents[i].medcert, f, b, rect_medcert, sf_left);

                    if (i % 2 != 0) { e.Graphics.FillRectangle(b_whitesmoke, rect_fine); }
                    e.Graphics.DrawRectangle(p, rect_fine);
                    e.Graphics.DrawString(absents[i].fine, f, b, rect_fine, sf_right);

                    y += f.Height + line_gap;

                    item_count++;
                }

                y += f.Height + line_gap;

                Rectangle rect_bottom1 = new Rectangle(e.MarginBounds.Left, y, 300, f.Height + line_gap);
                Rectangle rect_bottom2 = new Rectangle(e.MarginBounds.Left, rect_bottom1.Y + f.Height + line_gap, 70, f.Height + line_gap);
                Rectangle rect_bottom3 = new Rectangle(rect_bottom2.X + rect_bottom2.Width, rect_bottom2.Y, 20, f.Height + line_gap);
                Rectangle rect_bottom4 = new Rectangle(rect_bottom3.X + rect_bottom3.Width, rect_bottom2.Y, 210, f.Height + line_gap);
                Rectangle rect_bottom5 = new Rectangle(rect_bottom2.X, rect_bottom4.Y + f.Height + line_gap, 70, f.Height + line_gap);
                Rectangle rect_bottom6 = new Rectangle(rect_bottom3.X, rect_bottom5.Y, 20, f.Height + line_gap);
                Rectangle rect_bottom7 = new Rectangle(rect_bottom4.X, rect_bottom5.Y, 210, f.Height + line_gap);

                //Rectangle rect_sum = new Rectangle(rect_ptd.X + rect_ptd.Width + 100, y, 250, f.Height + line_gap);

                if ((y + rect_bottom1.Height + rect_bottom2.Height + rect_bottom5.Height) > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    e.Graphics.DrawString(this.grpPtd.Text, f_bold, b, rect_bottom1, sf_left);
                    e.Graphics.DrawString("ลางาน", f, b, rect_bottom2, sf_left);
                    e.Graphics.DrawString(":", f, b, rect_bottom3, sf_center);
                    e.Graphics.DrawString(this.lblPtdAbsent.Text, f, b, rect_bottom4, sf_left);
                    e.Graphics.DrawString("ออกพบลูกค้า", f, b, rect_bottom5, sf_left);
                    e.Graphics.DrawString(":", f, b, rect_bottom6, sf_center);
                    e.Graphics.DrawString(this.lblPtdCust.Text, f, b, rect_bottom7, sf_left);

                    rect_bottom1.X += rect_bottom1.Width;
                    rect_bottom2.X += rect_bottom1.Width;
                    rect_bottom3.X += rect_bottom1.Width;
                    rect_bottom4.X += rect_bottom1.Width;
                    rect_bottom5.X += rect_bottom1.Width;
                    rect_bottom6.X += rect_bottom1.Width;
                    rect_bottom7.X += rect_bottom1.Width;
                    
                    e.Graphics.DrawString(this.grpYear.Text, f_bold, b, rect_bottom1, sf_left);
                    e.Graphics.DrawString("ลางาน", f, b, rect_bottom2, sf_left);
                    e.Graphics.DrawString(":", f, b, rect_bottom3, sf_center);
                    e.Graphics.DrawString(this.lblYearAbsent.Text, f, b, rect_bottom4, sf_left);
                    e.Graphics.DrawString("ออกพบลูกค้า", f, b, rect_bottom5, sf_left);
                    e.Graphics.DrawString(":", f, b, rect_bottom6, sf_center);
                    e.Graphics.DrawString(this.lblYearCust.Text, f, b, rect_bottom7, sf_left);

                }

            };

            return pd;
        }

        private PrintDocument GetPrintSumAbsentDocument(List<SummaryAbsent> absents, int? total_page = null)
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Margins = new Margins(20, 40, 40, 50);
            pd.DefaultPageSettings.Landscape = false;
            int item_count = 0;
            int page_count = 0;
            DateTime print_time = DateTime.Now;

            pd.BeginPrint += delegate (object sender, PrintEventArgs e)
            {
                item_count = 0;
                page_count = 0;
            };

            pd.PrintPage += delegate (object sender, PrintPageEventArgs e)
            {
                Font f = new Font("tahoma", 8f);
                Font f_bold = new Font("tahoma", 8f, FontStyle.Bold);
                Font f_title = new Font("tahoma", 10f, FontStyle.Bold);
                SolidBrush b = new SolidBrush(Color.Black);
                SolidBrush b_red = new SolidBrush(Color.Red);
                SolidBrush b_blue = new SolidBrush(Color.Blue);
                SolidBrush b_gray = new SolidBrush(Color.DimGray);
                SolidBrush b_background = new SolidBrush(Color.Lavender);
                SolidBrush b_whitesmoke = new SolidBrush(Color.WhiteSmoke);
                Pen p = new Pen(new SolidBrush(Color.DarkGray));
                StringFormat sf_left = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };
                StringFormat sf_center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };
                StringFormat sf_right = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };

                page_count++;
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                int line_gap = 10;

                Rectangle rect = new Rectangle(e.MarginBounds.Left, y, 350, f_title.Height);
                e.Graphics.DrawString("สรุปวันลา (สำหรับคิดค่าคอมฯ)", f_title, b, rect, sf_left);

                rect = new Rectangle(e.MarginBounds.Right - 100, y, 100, f.Height);
                e.Graphics.DrawString("หน้า : " + page_count.ToString() + (total_page.HasValue ? " / " + total_page.ToString() : ""), f, b, rect, sf_right);
                y += f_title.Height + line_gap;

                rect = new Rectangle(e.MarginBounds.Left, y, 100, f.Height);
                e.Graphics.DrawString("รหัสพนักงาน จาก", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 20;
                e.Graphics.DrawString(":", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 150;
                e.Graphics.DrawString(((users)((XDropdownListItem)this.drYearAbsentUserFrom._SelectedItem).Value).username, f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 30;
                e.Graphics.DrawString("ถึง", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 10;
                e.Graphics.DrawString(":", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 150;
                e.Graphics.DrawString(((users)((XDropdownListItem)this.drYearAbsentUserTo._SelectedItem).Value).username, f, b, rect, sf_left);
                y += f.Height + line_gap;

                rect = new Rectangle(e.MarginBounds.Left, y, 100, f.Height);
                e.Graphics.DrawString("วันที่ จาก", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 20;
                e.Graphics.DrawString(":", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 150;
                e.Graphics.DrawString(this.dtYearAbsentFrom.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH")), f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 30;
                e.Graphics.DrawString("ถึง", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 10;
                e.Graphics.DrawString(":", f, b, rect, sf_left);
                rect.X += rect.Width + 10;
                rect.Width = 150;
                e.Graphics.DrawString(this.dtYearAbsentTo.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH")), f, b, rect, sf_left);

                rect = new Rectangle(e.MarginBounds.Right - 250, y, 250, f.Height);
                e.Graphics.DrawString("(วัน/เวลาที่พิมพ์ : " + print_time.ToString("dd/MM/yy HH:mm:ss", CultureInfo.GetCultureInfo("th-TH")) + ")", f, b, rect, sf_right);
                y += f.Height + line_gap;

                e.Graphics.FillRectangle(b_background, new Rectangle(e.MarginBounds.X, y, e.MarginBounds.Width, f.Height + line_gap));
                e.Graphics.DrawLine(p, new Point(x, y), new Point(e.MarginBounds.Right, y));

                Rectangle rect_seq = new Rectangle(e.MarginBounds.X, y, 40, f.Height + line_gap);
                Rectangle rect_user = new Rectangle(rect_seq.X + rect_seq.Width, y, 80, f.Height + line_gap);
                Rectangle rect_name = new Rectangle(rect_user.X + rect_user.Width, y, 110, f.Height + line_gap);
                Rectangle rect_absent = new Rectangle(rect_name.X + rect_name.Width, y, 200, f.Height + line_gap);
                Rectangle rect_absent_comm = new Rectangle(rect_absent.X + rect_absent.Width, y, 200, f.Height + line_gap);
                Rectangle rect_fine = new Rectangle(rect_absent_comm.X + rect_absent_comm.Width, y, e.MarginBounds.Width - (rect_seq.Width + rect_user.Width + rect_name.Width + rect_absent.Width + rect_absent_comm.Width), f.Height + line_gap);
                e.Graphics.DrawRectangle(p, rect_seq);
                e.Graphics.DrawRectangle(p, rect_user);
                e.Graphics.DrawRectangle(p, rect_name);
                e.Graphics.DrawRectangle(p, rect_absent);
                e.Graphics.DrawRectangle(p, rect_absent_comm);
                e.Graphics.DrawRectangle(p, rect_fine);
                e.Graphics.DrawRectangle(p, rect_fine);
                e.Graphics.DrawString("ลำดับ", f, b, rect_seq, sf_center);
                e.Graphics.DrawString("รหัสพนักงาน", f, b, rect_user, sf_center);
                e.Graphics.DrawString("ชื่อ", f, b, rect_name, sf_center);
                e.Graphics.DrawString("จำนวนวันลา (จริง)", f, b, rect_absent, sf_center);
                e.Graphics.DrawString("จำนวนวันลา (คิดค่าคอมฯ)", f, b, rect_absent_comm, sf_center);
                e.Graphics.DrawString("หักค่าคอมฯ (บาท)", f, b, rect_fine, sf_center);
                y += f.Height + line_gap;

                for (int i = item_count; i < absents.Count; i++)
                {
                    if(y > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    rect_seq.Y = y;
                    rect_user.Y = y;
                    rect_name.Y = y;
                    rect_absent.Y = y;
                    rect_absent_comm.Y = y;
                    rect_fine.Y = y;

                    e.Graphics.DrawRectangle(p, rect_seq);
                    e.Graphics.DrawString(absents[i].seq.ToString(), f, b_gray, rect_seq, sf_right);

                    e.Graphics.DrawRectangle(p, rect_user);
                    e.Graphics.DrawString(absents[i].user_name, f, b, rect_user, sf_center);

                    e.Graphics.DrawRectangle(p, rect_name);
                    e.Graphics.DrawString(absents[i].name, f, b, rect_name, sf_left);

                    e.Graphics.DrawRectangle(p, rect_absent);
                    e.Graphics.DrawString(absents[i].tot_absent, f, b, rect_absent, sf_center);

                    e.Graphics.DrawRectangle(p, rect_absent_comm);
                    e.Graphics.DrawString(absents[i].tot_absent_comm, f_bold, b, rect_absent_comm, sf_center);

                    e.Graphics.DrawRectangle(p, rect_fine);
                    e.Graphics.DrawString((absents[i].fine > 0 ? absents[i].fine.ToString() : ""), f, b, rect_fine, sf_right);

                    y += f.Height + line_gap;
                    item_count++;
                }
            };

            return pd;
        }

        private void btnChangeScope_Click(object sender, EventArgs e)
        {
            DialogAbsentReportScope scope = new DialogAbsentReportScope(this.users_from, this.date_from, this.date_to);
            if (scope.ShowDialog() == DialogResult.OK)
            {
                this.users_from = scope.user_from;
                this.users_to = scope.user_to;
                this.date_from = scope.date_from;
                this.date_to = scope.date_to;

                this.GetData();
                this.FillForm();
            }
        }

        private void dgvAbsent_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            int cause_id = ((note_istab)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c1_istab.Name].Value).id;

            if((bool)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c1_enabled.Name].Value != true) // person absent list has no this cause
            {
                ((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c1_selected.Name].Value = false;
                e.CellStyle.BackColor = Color.LightGray;
                e.CellStyle.SelectionBackColor = Color.LightGray;
                e.CellStyle.ForeColor = Color.DimGray;
                e.CellStyle.SelectionForeColor = Color.DimGray;
            }

            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
            e.Handled = true;
        }

        private void dgvCust_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            int cause_id = ((note_istab)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c2_istab.Name].Value).id;

            if ((bool)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c2_enabled.Name].Value != true) // person absent list has no this cause
            {
                ((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c2_selected.Name].Value = false;
                e.CellStyle.BackColor = Color.LightGray;
                e.CellStyle.SelectionBackColor = Color.LightGray;
                e.CellStyle.ForeColor = Color.DimGray;
                e.CellStyle.SelectionForeColor = Color.DimGray;
            }

            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
            e.Handled = true;
        }

        private void dgvDetail_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            int status = ((event_calendar)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c3_event_calendar.Name].Value).status.Value;
            DayOfWeek dow = ((event_calendar)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c3_event_calendar.Name].Value).date.DayOfWeek;

            if (status == (int)CALENDAR_EVENT_STATUS.WAIT)
            {
                e.CellStyle.BackColor = Color.Lavender;
                e.CellStyle.SelectionBackColor = Color.Lavender;
            }
            else if(status == (int)CALENDAR_EVENT_STATUS.CANCELED)
            {
                e.CellStyle.BackColor = Color.MistyRose;
                e.CellStyle.SelectionBackColor = Color.MistyRose;
            }
            else if(dow == DayOfWeek.Saturday)
            {
                e.CellStyle.BackColor = Color.Beige;
                e.CellStyle.SelectionBackColor = Color.Beige;
            }

            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
            e.Handled = true;
        }

        private void dgvSum_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void dgvAbsent_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if(e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_c1_selected.Name).FirstOrDefault().Index)
            {
                note_istab cause = (note_istab)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c1_istab.Name].Value;
                if(this.person_events.Where(pe => pe.event_code_id == cause.id).Count() > 0)
                {
                    bool selected = ((BindingList<AbsentCauseVM>)((XDatagrid)sender).DataSource)[e.RowIndex].selected;
                    ((BindingList<AbsentCauseVM>)((XDatagrid)sender).DataSource)[e.RowIndex].selected = !selected;
                    this.ApplySelectionChange();
                }
            }
        }

        private void dgvCust_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_c2_selected.Name).FirstOrDefault().Index)
            {
                note_istab cause = (note_istab)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c2_istab.Name].Value;
                if (this.person_events.Where(pe => pe.event_code_id == cause.id).Count() > 0)
                {
                    bool selected = ((BindingList<AbsentCauseVM>)((XDatagrid)sender).DataSource)[e.RowIndex].selected;
                    ((BindingList<AbsentCauseVM>)((XDatagrid)sender).DataSource)[e.RowIndex].selected = !selected;
                    this.ApplySelectionChange();
                }
            }
        }

        private void ApplySelectionChange()
        {
            List<note_istab> abs = this.dgvAbsent.Rows.Cast<DataGridViewRow>().Where(r => (bool)r.Cells[this.col_c1_selected.Name].Value == true).ToList().Select(r => (note_istab)r.Cells[this.col_c1_istab.Name].Value).ToList();
            List<note_istab> cus = this.dgvCust.Rows.Cast<DataGridViewRow>().Where(r => (bool)r.Cells[this.col_c2_selected.Name].Value == true).ToList().Select(r => (note_istab)r.Cells[this.col_c2_istab.Name].Value).ToList();

            int?[] all_selected_cause_id = abs.Concat(cus).Select(a => (int?)a.id).ToArray();
            this.absent_person_list = new BindingList<AbsentPersonStatVM>(this.person_events.Where(pe => all_selected_cause_id.Contains(pe.event_code_id)).ToAbsentPersonStatVM());
            int seq = 0;
            this.absent_person_list.ToList().ForEach(a => a.seq = ++seq);
            this.dgvDetail.DataSource = this.absent_person_list;
        }

        private void btnOKYearAbsent_Click(object sender, EventArgs e)
        {
            this.ShowAbsentSummaryData();
            //this.dtYearAbsentFrom.Focus();
        }

        private void ShowAbsentSummaryData()
        {
            if(((XDropdownListItem)this.drYearAbsentUserFrom._SelectedItem).Value == null)
            {
                this.drYearAbsentUserFrom.Focus();
                SendKeys.Send("{F6}");
                return;
            }
            if (((XDropdownListItem)this.drYearAbsentUserTo._SelectedItem).Value == null)
            {
                this.drYearAbsentUserTo.Focus();
                SendKeys.Send("{F6}");
                return;
            }

            using (sn_noteEntities note = DBXNote.DataSet())
            {
                users user_from = (users)((XDropdownListItem)this.drYearAbsentUserFrom._SelectedItem).Value;
                users user_to = (users)((XDropdownListItem)this.drYearAbsentUserTo._SelectedItem).Value;

                var all_absent = note.event_calendar
                                .Where(ev => ev.date.CompareTo(this.dtYearAbsentFrom.Value.Date) >= 0 && ev.date.CompareTo(this.dtYearAbsentTo.Value.Date) <= 0 &&
                                ev.users_name.CompareTo(user_from.username) >= 0 && ev.users_name.CompareTo(user_to.username) <= 0 &&
                                ev.status != (int)CALENDAR_EVENT_STATUS.CANCELED && ev.event_type == CALENDAR_EVENT_TYPE.ABSENT).ToList();
                int seq = 0;
                var grouped_absent = all_absent.OrderBy(a => a.users_name).GroupBy(a => a.users_name).Select(a => new SummaryAbsent {
                                    seq = ++seq,
                                    user_name = a.First().users_name,
                                    name = a.First().realname,
                                    fine = (int)a.Sum(i => i.fine),
                                    remark = string.Empty,
                                    tot_absent = string.Empty,
                                    tot_absent_comm = string.Empty }).ToList();
                grouped_absent.ForEach(g =>
                {
                    TimeSpan ts_real = TimeSpan.Parse("00:00");
                    TimeSpan ts_comm = TimeSpan.Parse("00:00");
                    all_absent.Where(a => a.users_name == g.user_name).ToList().ForEach(a =>
                    {
                        ts_real = ts_real.Add(TimeSpan.Parse(a.from_time).GetDayTimeSpan(TimeSpan.Parse(a.to_time)));
                        if(a.med_cert == CALENDAR_EVENT_MEDCERT.HAVE_MEDCERT)
                        {
                            TimeSpan t = TimeSpan.Parse(a.from_time).GetDayTimeSpan(TimeSpan.Parse(a.to_time));
                            double m = Math.Floor(t.TotalMinutes / 2);
                            TimeSpan s = TimeSpan.FromMinutes(m);
                            ts_comm = ts_comm.Add(s);
                        }
                        else
                        {
                            ts_comm = ts_comm.Add(TimeSpan.Parse(a.from_time).GetDayTimeSpan(TimeSpan.Parse(a.to_time)));
                        }
                    });
                    g.tot_absent = ts_real.GetTimeSpanString();
                    g.tot_absent_comm = ts_comm.GetTimeSpanString();
                });
                this.absent_summary_list = new BindingList<SummaryAbsent>(grouped_absent);
                this.dgvSum.DataSource = this.absent_summary_list;

                this.dtYearAbsentFrom.Enabled = false;
                this.dtYearAbsentTo.Enabled = false;
                this.drYearAbsentUserFrom._ReadOnly = true;
                this.drYearAbsentUserTo._ReadOnly = true;
                this.btnEditScopeYearAbsent.Enabled = true;
                this.btnOKYearAbsent.Enabled = false;
            }
        }

        private void btnAllAbsent_Click(object sender, EventArgs e)
        {
            ((BindingList<AbsentCauseVM>)this.dgvAbsent.DataSource).ToList().ForEach(i => i.selected = true);
            this.dgvAbsent.Refresh();
            this.ApplySelectionChange();
        }

        private void btnNoneAbsent_Click(object sender, EventArgs e)
        {
            ((BindingList<AbsentCauseVM>)this.dgvAbsent.DataSource).ToList().ForEach(i => i.selected = false);
            this.dgvAbsent.Refresh();
            this.ApplySelectionChange();
        }

        private void btnAllCust_Click(object sender, EventArgs e)
        {
            ((BindingList<AbsentCauseVM>)this.dgvCust.DataSource).ToList().ForEach(i => i.selected = true);
            this.dgvCust.Refresh();
            this.ApplySelectionChange();
        }

        private void btnNoneCust_Click(object sender, EventArgs e)
        {
            ((BindingList<AbsentCauseVM>)this.dgvCust.DataSource).ToList().ForEach(i => i.selected = false);
            this.dgvCust.Refresh();
            this.ApplySelectionChange();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if(((TabControl)sender).SelectedTab == this.tabPage2)
            {
                this.dtYearAbsentFrom.Focus();
            }
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            ((XDatagrid)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            this.btnEditItem.PerformClick();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (this.dgvDetail.CurrentCell == null)
                return;

            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm();
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            if (this.tmp_event_cal == null)
                return;

            if(this.tmp_event_cal.from_time == "00:00")
            {
                this.inlineFrom.Focus();
                return;
            }
            if (this.tmp_event_cal.to_time == "00:00")
            {
                this.inlineTo.Focus();
                return;
            }

            using (sn_noteEntities note = DBXNote.DataSet())
            {
                var event_to_update = note.event_calendar.Find(this.tmp_event_cal.id);
                if(event_to_update == null)
                {
                    MessageAlert.Show("ค้นหารายการที่ต้องการแก้ไขไม่พบ, อาจมีผู้ใช้งานรายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    return;
                }

                event_to_update.from_time = this.tmp_event_cal.from_time;
                event_to_update.to_time = this.tmp_event_cal.to_time;
                event_to_update.customer = this.tmp_event_cal.customer;
                event_to_update.status = this.tmp_event_cal.status;
                event_to_update.med_cert = this.tmp_event_cal.med_cert;
                event_to_update.fine = this.tmp_event_cal.fine;

                note.SaveChanges();
                this.RemoveInlineForm();
                this.ResetFormState(FORM_MODE.READ);

                var ev = absent_person_list.Where(a => a.event_calendar.id == event_to_update.id).FirstOrDefault();
                ev.event_calendar.from_time = event_to_update.from_time;
                ev.event_calendar.to_time = event_to_update.to_time;
                ev.event_calendar.status = event_to_update.status;
                ev.event_calendar.customer = event_to_update.customer;
                ev.event_calendar.med_cert = event_to_update.med_cert;
                ev.event_calendar.fine = event_to_update.fine;

                this.dgvDetail.Refresh();
            }
        }

        private void btnStopItem_Click(object sender, EventArgs e)
        {
            this.RemoveInlineForm();
            this.ResetFormState(FORM_MODE.READ);
            this.dgvDetail.Focus();
        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if(this.form_mode != FORM_MODE.READ)
            {
                e.Cancel = true;
                this.inlineFrom.Focus();
                return;
            }
        }

        private void inlineFrom_ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_cal != null)
            {
                this.tmp_event_cal.from_time = ((XTimePicker)sender).Value.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH"));

                try
                {

                }
                catch (Exception)
                {

                }
            }
        }

        private void inlineTo_ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_cal != null)
            {
                this.tmp_event_cal.to_time = ((XTimePicker)sender).Value.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH"));
                try
                {

                }
                catch (Exception)
                {

                }
            }
        }

        private void inlineStatus__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_cal != null)
                this.tmp_event_cal.status = (int?)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void inlineCustomer__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_cal != null)
                this.tmp_event_cal.customer = ((XTextEdit)sender)._Text;
        }

        private void inlineMedcert__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_cal != null)
                this.tmp_event_cal.med_cert = (string)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void inlineFine_ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_cal != null)
                this.tmp_event_cal.fine = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        private void inlineFine_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.tmp_event_cal != null)
                this.tmp_event_cal.fine = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if (this.inlineFine.Focused)
                    {
                        this.btnSaveItem.PerformClick();
                        return true;
                    }

                    SendKeys.Send("{TAB}");
                    return true;
                }
                else if (this.tabControl1.SelectedTab == this.tabPage2 && (this.drYearAbsentUserFrom._Focused || this.drYearAbsentUserTo._Focused || this.dtYearAbsentFrom.Focused || this.dtYearAbsentTo.Focused))
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                if(this.form_mode == FORM_MODE.EDIT_ITEM && !this.inlineStatus._DroppedDown && !this.inlineMedcert._DroppedDown)
                {
                    this.btnStopItem.PerformClick();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnEditScopeYearAbsent_Click(object sender, EventArgs e)
        {
            this.dtYearAbsentFrom.Enabled = true;
            this.dtYearAbsentTo.Enabled = true;
            this.drYearAbsentUserFrom._ReadOnly = false;
            this.drYearAbsentUserTo._ReadOnly = false;
            ((Button)sender).Enabled = false;
            this.btnOKYearAbsent.Enabled = true;

            this.drYearAbsentUserFrom.Focus();
        }

        private void btnPrint_ButtonClick(object sender, EventArgs e)
        {
            this.btnPrint1.PerformClick();
        }
    }

    public class EventCalYearlyTimeSpan
    {
        public event_calendar event_calendar { get; set; }
        public TimeSpan time_span
        {
            get
            {
                TimeSpan ts = TimeSpan.Parse(this.event_calendar.to_time).Subtract(TimeSpan.Parse(this.event_calendar.from_time));

                if(TimeSpan.Parse(this.event_calendar.from_time).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(this.event_calendar.to_time).CompareTo(TimeSpan.Parse("13:00")) > 0)
                {
                    ts = ts.Subtract(TimeSpan.Parse("01:00"));
                }
                return ts;
            }
        }
    }

    public class SummaryAbsent
    {
        public int seq { get; set; }
        public string user_name { get; set; }
        public string name { get; set; }
        public string tot_absent { get; set; }
        public string tot_absent_comm { get; set; }
        public int fine { get; set; }
        public string remark { get; set; }
    }
}