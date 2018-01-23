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
        private users users_from = null;
        private users users_to = null;
        private DateTime? date_from = null;
        private DateTime? date_to = null;

        private List<event_calendar> person_events;
        private BindingList<AbsentCauseVM> cause1_list;
        private BindingList<AbsentCauseVM> cause2_list;
        private BindingList<AbsentPersonStatVM> absent_person_list;
        private BindingList<SummaryAbsent> absent_summary_list;
        private TimeSpan yearly_absent;
        private TimeSpan yearly_cust;
        private FORM_MODE form_mode;
        private event_calendar tmp_event_cal;

        public FormAbsentReport()
        {
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
                    this.ShowAbsentSummaryData();
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
            DialogPrintOutput p = new DialogPrintOutput();
            if(p.ShowDialog() == DialogResult.OK)
            {
                if(p.output == PRINT_OUTPUT.SCREEN)
                {
                    //Console.WriteLine(" ==> Screen");
                    PrintPreviewDialog pvd = new PrintPreviewDialog();
                    pvd.Document = this.GetPrintPersonAbsentDocument(this.absent_person_list.ToList());
                    pvd.ShowDialog();
                }

                if(p.output == PRINT_OUTPUT.PRINTER)
                {
                    //Console.WriteLine(" ==> Printer");
                    PrintDialog print_dialog = new PrintDialog();
                    print_dialog.Document = this.GetPrintPersonAbsentDocument(this.absent_person_list.ToList());
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = false;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = true;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_dialog.Document.Print();
                    }
                }
            }
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            DialogPrintOutput p = new DialogPrintOutput();
            if (p.ShowDialog() == DialogResult.OK)
            {
                if (p.output == PRINT_OUTPUT.SCREEN)
                {
                    //Console.WriteLine(" ==> Screen");
                }

                if (p.output == PRINT_OUTPUT.PRINTER)
                {
                    //Console.WriteLine(" ==> Printer");
                }
            }
        }

        private PrintDocument GetPrintPersonAbsentDocument(List<AbsentPersonStatVM> absents)
        {
            PrintDocument pd = new PrintDocument();
            //PageSetupDialog page_setup = new PageSetupDialog();
            //page_setup.Document = pd;
            //page_setup.PageSettings.PaperSize = new PaperSize("A4", 825, 1165);
            //page_setup.PageSettings.Landscape = true;
            //page_setup.PageSettings.Margins = new Margins(0, 0, 10, 40);

            pd.DefaultPageSettings.Margins = new Margins(20, 20, 40, 30);
            pd.DefaultPageSettings.Landscape = true;
            int item_count = 0;
            int page_count = 0;

            pd.BeginPrint += delegate(object sender, PrintEventArgs e)
            {
                page_count = 0;
                item_count = 0;
            };

            pd.PrintPage += delegate(object sender, PrintPageEventArgs e)
            {
                Font f = new Font("tahoma", 8f);
                Font f_title = new Font("tahoma", 10f, FontStyle.Bold);
                SolidBrush b = new SolidBrush(Color.Black);
                SolidBrush bg = new SolidBrush(Color.Lavender);
                Pen p = new Pen(new SolidBrush(Color.DarkGray));
                StringFormat sf_left = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };
                StringFormat sf_center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };
                StringFormat sf_right = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };

                page_count++;
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                int line_gap = 6;

                Rectangle rect = new Rectangle(e.MarginBounds.Left, y, 350, f_title.Height);
                e.Graphics.DrawString("สรุปวันลา/ออกพบลูกค้า", f_title, b, rect);

                rect = new Rectangle(e.MarginBounds.Right - 50, y, 50, f.Height);
                e.Graphics.DrawString("Page " + page_count.ToString(), f, b, rect);
                y += f_title.Height + line_gap;

                rect = new Rectangle(e.MarginBounds.Left, y, 80, f.Height);
                e.Graphics.DrawString("รหัสพนักงาน", f, b, rect);
                rect.X += rect.Width + 10;
                rect.Width = 20;
                e.Graphics.DrawString(":", f, b, rect);
                rect.X += rect.Width + 10;
                rect.Width = 100;
                e.Graphics.DrawString(absents.First().user_name, f, b, rect);
                y += f.Height + line_gap;

                rect = new Rectangle(e.MarginBounds.Left, y, 80, f.Height);
                e.Graphics.DrawString("วันที่ จาก", f, b, rect);
                rect.X += rect.Width + 10;
                rect.Width = 20;
                e.Graphics.DrawString(":", f, b, rect);
                rect.X += rect.Width + 10;
                rect.Width = 150;
                e.Graphics.DrawString(this.lblDateFrom.Text, f, b, rect);
                rect.X += rect.Width + 10;
                rect.Width = 30;
                e.Graphics.DrawString("ถึง", f, b, rect);
                rect.X += rect.Width + 10;
                rect.Width = 10;
                e.Graphics.DrawString(":", f, b, rect);
                rect.X += rect.Width + 10;
                rect.Width = 150;
                e.Graphics.DrawString(this.lblDateTo.Text, f, b, rect);
                y += f.Height + line_gap;

                e.Graphics.FillRectangle(bg, new Rectangle(e.MarginBounds.X, y, e.MarginBounds.Width, f.Height + line_gap));
                e.Graphics.DrawLine(p, new Point(x, y), new Point(e.MarginBounds.Right, y));
                
                Rectangle rect_seq = new Rectangle(e.MarginBounds.X, y, 40, f.Height + line_gap);
                Rectangle rect_user = new Rectangle(rect_seq.X + rect_seq.Width, y, 80, f.Height + line_gap);
                Rectangle rect_date = new Rectangle(rect_user.X + rect_user.Width, y, 90, f.Height + line_gap);
                e.Graphics.DrawRectangle(p, rect_seq);
                e.Graphics.DrawRectangle(p, rect_user);
                e.Graphics.DrawRectangle(p, rect_date);
                e.Graphics.DrawString("ลำดับ", f, b, rect_seq, sf_center);
                e.Graphics.DrawString("รหัสพนักงาน", f, b, rect_user, sf_center);
                e.Graphics.DrawString("วันที่", f, b, rect_date, sf_center);
                y += f.Height + line_gap;


                for (int i = item_count; i < absents.Count; i++)
                {
                    rect_seq.Y = y;
                    rect_user.Y = y;
                    rect_date.Y = y;

                    e.Graphics.DrawRectangle(p, rect_seq);
                    e.Graphics.DrawString((++i).ToString(), f, b, rect_seq, sf_right);

                    e.Graphics.DrawRectangle(p, rect_user);
                    e.Graphics.DrawString(absents[i].user_name, f, b, rect_user, sf_left);

                    e.Graphics.DrawRectangle(p, rect_date);
                    e.Graphics.DrawString(absents[i].date.ToString("ddd dd/MM/yy", CultureInfo.GetCultureInfo("th-TH")), f, b, rect_date, sf_left);
                    y += f.Height + line_gap;

                    item_count++;

                    if (y > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }
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
            this.dtYearAbsentFrom.Focus();
        }

        private void ShowAbsentSummaryData()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                var all_absent = note.event_calendar
                                .Where(ev => ev.date.CompareTo(this.dtYearAbsentFrom.Value.Date) >= 0 &&
                                ev.date.CompareTo(this.dtYearAbsentTo.Value.Date) <= 0 && ev.status != (int)CALENDAR_EVENT_STATUS.CANCELED &&
                                ev.event_type == CALENDAR_EVENT_TYPE.ABSENT).ToList();
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
                else if (this.tabControl1.SelectedTab == this.tabPage2 && (this.dtYearAbsentFrom.Focused || this.dtYearAbsentTo.Focused))
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