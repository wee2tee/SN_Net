using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.Model;
using SN_Net.Subform;
using CC;
using System.Globalization;

namespace SN_Net.MiscClass
{
    public partial class CustomDateEvent3 : UserControl
    {
        private MainForm main_form;
        private FormCalendar calendar;
        public DateTime curr_date;
        private DateTime first_date_of_month; // use to check for current month
        private BindingList<event_calendarVM> event_list;
        public note_calendar note_cal;
        private List<training_calendarVM> training_list;

        public CustomDateEvent3(MainForm main_form, FormCalendar calendar, DateTime curr_date, DateTime first_date_of_month, List<event_calendar> event_cal, note_calendar note_cal, List<training_calendar> training_cal)
        {
            this.main_form = main_form;
            this.calendar = calendar;
            this.curr_date = curr_date;
            this.first_date_of_month = first_date_of_month;
            this.event_list = new BindingList<event_calendarVM>(event_cal.ToViewModel());
            this.note_cal = note_cal;
            this.training_list = new List<training_calendarVM>(training_cal.ToViewModel());

            InitializeComponent();
        }

        private void CustomDateEvent3_Load(object sender, EventArgs e)
        {
            this.FillForm(this.curr_date, this.first_date_of_month, this.event_list, this.note_cal, this.training_list);
        }

        private void FillForm(DateTime curr_date, DateTime first_date_of_month, BindingList<event_calendarVM> event_cal, note_calendar note_cal, List<training_calendarVM> training_cal)
        {
            this.btnDropDownMenu.Visible = this.main_form.loged_in_user.level >= (int)USER_LEVEL.SUPERVISOR || (this.main_form.loged_in_user.training_expert == "Y") ? true : false;
            this.btnAdd.Visible = this.main_form.loged_in_user.level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
            this.btnDetail.Visible = this.main_form.loged_in_user.level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
            this.lblDay.Text = this.curr_date.Day.ToString();
            this.lblMonthYear.Text = this.curr_date.ToString("MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            if(this.curr_date.ToString("yyyyMM") != this.first_date_of_month.ToString("yyyyMM") || this.curr_date.IsLastSaturday() || this.curr_date.DayOfWeek == DayOfWeek.Sunday)
            {
                this.btnDropDownMenu.Visible = false;
            }

            int seq = 0;
            foreach (var ev in this.event_list)
            {
                if (ev.users.level < (int)USER_LEVEL.SUPERVISOR && (ev.event_calendar.status == (int)CALENDAR_EVENT_STATUS.WAIT || ev.event_calendar.status == (int)CALENDAR_EVENT_STATUS.CONFIRMED))
                {
                    ev.seq = ++seq;
                }
                else
                {
                    ev.seq = null;
                }
            }
            this.lblBottomText.Text = training_list.Count > 0 ? "อบรม : " + string.Join(",", training_list.Select(t => t.name).ToArray()) : string.Empty;
            this.toolTip1.SetToolTip(this.lblBottomText, this.lblBottomText.Text);
            if (this.lblBottomText.Text.Trim().Length == 0)
            {
                this.lblBottomText.SetBounds(this.lblBottomText.Bounds.X, this.lblBottomText.Bounds.Y + 14, this.lblBottomText.Bounds.Width, this.lblBottomText.Bounds.Height);
                this.lblBottomText.Height = 0;
                this.lblBottomText.BackColor = Color.Green;
            }

            this.lblNoteDescription.Text = this.note_cal != null ? this.note_cal.description : string.Empty;
            this.toolTip1.SetToolTip(this.lblNoteDescription, this.lblNoteDescription.Text);
            if (this.note_cal != null && this.note_cal.type == (int)CALENDAR_NOTE_TYPE.WEEKDAY && this.note_cal.description.Trim().Length > 0)
            {
                this.lblNoteDescription.TextAlign = ContentAlignment.TopLeft;
                this.lblNoteDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                this.lblNoteDescription.SetBounds(0, this.lblBottomText.Bounds.Y - 14, this.lblNoteDescription.Width, 14);
                this.lblNoteDescription.Font = new Font("tahoma", 8.25f, FontStyle.Regular);
                this.dgv.Height -= this.lblBottomText.Height;
            }

            if (this.note_cal != null && this.note_cal.type == (int)CALENDAR_NOTE_TYPE.HOLIDAY)
            {
                this.lblNoteDescription.BringToFront();
                this.dgv.SendToBack();
            }
            else
            {
                this.dgv.BringToFront();
                this.lblNoteDescription.SendToBack();
            }

            this.dgv.DataSource = this.event_list;

            this.btnHoliday.Text = this.note_cal != null ? this.note_cal.group_weekend : string.Empty;
            this.btnMaid.Text = this.note_cal != null ? this.note_cal.group_maid : string.Empty;

            this.btnHoliday.Visible = this.note_cal != null && this.note_cal.group_weekend != null && this.note_cal.group_weekend.Trim().Length > 0 ? true : false;
            this.btnMaid.Visible = this.note_cal != null && this.note_cal.group_maid != null && this.note_cal.group_maid.Trim().Length > 0 ? true : false;
            if(this.curr_date.ToString("dd-MM-yyyy", CultureInfo.GetCultureInfo("th-TH")) == DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.GetCultureInfo("th-TH")))
            {
                this.lblDay.BackColor = Color.Green;
            }
            else
            {
                this.lblDay.BackColor = (this.curr_date.ToString("yyyyMM") == this.first_date_of_month.ToString("yyyyMM") && !this.curr_date.IsLastSaturday() && this.curr_date.DayOfWeek != DayOfWeek.Sunday) ? Color.MediumSlateBlue : Color.LightGray;
            }
        }

        public void RefreshView()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                var event_cal = note.event_calendar.Include("note_istab").Where(c => c.date.CompareTo(this.curr_date) == 0).ToList();
                this.event_list = new BindingList<event_calendarVM>(event_cal.ToViewModel().OrderBy(ev => ev.users.level).ThenBy(ev => ev.event_calendar.id).ToList());

                this.note_cal = note.note_calendar.Where(c => c.date.CompareTo(this.curr_date) == 0).FirstOrDefault();

                this.training_list = note.training_calendar.Where(c => c.date.CompareTo(this.curr_date) == 0).ToViewModel();
            }

            int seq = 0;
            foreach (var ev in this.event_list)
            {
                if (ev.users.level < (int)USER_LEVEL.SUPERVISOR && (ev.event_calendar.status == (int)CALENDAR_EVENT_STATUS.WAIT || ev.event_calendar.status == (int)CALENDAR_EVENT_STATUS.CONFIRMED))
                {
                    ev.seq = ++seq;
                }
                else
                {
                    ev.seq = null;
                }
            }
            this.lblBottomText.Text = training_list.Count > 0 ? "อบรม : " + string.Join(",", training_list.Select(t => t.name).ToArray()) : string.Empty;
            this.toolTip1.SetToolTip(this.lblBottomText, this.lblBottomText.Text);
            if (this.lblBottomText.Text.Trim().Length == 0)
            {
                this.lblBottomText.SetBounds(this.lblBottomText.Bounds.X, this.lblBottomText.Bounds.Y + 14, this.lblBottomText.Bounds.Width, this.lblBottomText.Bounds.Height);
                this.lblBottomText.Height = 0;
                this.lblBottomText.BackColor = Color.Green;
            }

            this.lblDay.Text = this.curr_date.Day.ToString();
            this.lblMonthYear.Text = this.curr_date.ToString("MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            this.lblNoteDescription.Text = this.note_cal != null ? this.note_cal.description : string.Empty;
            if (this.note_cal != null && this.note_cal.type == (int)CALENDAR_NOTE_TYPE.WEEKDAY && this.note_cal.description.Trim().Length > 0)
            {
                this.lblNoteDescription.TextAlign = ContentAlignment.TopLeft;
                this.lblNoteDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                this.lblNoteDescription.SetBounds(0, this.lblBottomText.Bounds.Y - 14, this.lblNoteDescription.Width, 14);
                this.lblNoteDescription.Font = new Font("tahoma", 8.25f, FontStyle.Regular);
                this.dgv.Height -= this.lblBottomText.Height;
            }

            if (this.note_cal != null && this.note_cal.type == (int)CALENDAR_NOTE_TYPE.HOLIDAY)
            {
                this.lblNoteDescription.BringToFront();
                this.dgv.SendToBack();
            }
            else
            {
                this.dgv.BringToFront();
                this.lblNoteDescription.SendToBack();
            }

            this.dgv.DataSource = this.event_list;

            this.btnHoliday.Text = this.note_cal != null ? this.note_cal.group_weekend : string.Empty;
            this.btnMaid.Text = this.note_cal != null ? this.note_cal.group_maid : string.Empty;

            this.btnHoliday.Visible = this.note_cal != null && this.note_cal.group_weekend != null && this.note_cal.group_weekend.Trim().Length > 0 ? true : false;
            this.btnMaid.Visible = this.note_cal != null && this.note_cal.group_maid != null && this.note_cal.group_maid.Trim().Length > 0 ? true : false;
            if (this.curr_date.ToString("dd-MM-yyyy", CultureInfo.GetCultureInfo("th-TH")) == DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.GetCultureInfo("th-TH")))
            {
                this.lblDay.BackColor = Color.Green;
            }
            else
            {
                this.lblDay.BackColor = this.curr_date.Month.ToString() + this.curr_date.Year.ToString() == this.first_date_of_month.Month.ToString() + this.first_date_of_month.Year.ToString() ? Color.MediumSlateBlue : Color.LightGray;
            }
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                if (((users)((DataGridView)sender).Rows[e.RowIndex].Cells[this.col_users.Name].Value).level >= (int)USER_LEVEL.SUPERVISOR)
                {
                    e.CellStyle.BackColor = Color.Bisque;
                    e.CellStyle.SelectionBackColor = Color.Bisque;
                }
                else if (((event_calendar)((DataGridView)sender).Rows[e.RowIndex].Cells[this.col_event_calendar.Name].Value).status == (int)CALENDAR_EVENT_STATUS.WAIT)
                {
                    e.CellStyle.BackColor = Color.Lavender;
                    e.CellStyle.SelectionBackColor = Color.Lavender;
                }
                else if (((event_calendar)((DataGridView)sender).Rows[e.RowIndex].Cells[this.col_event_calendar.Name].Value).status == (int)CALENDAR_EVENT_STATUS.CANCELED)
                {
                    e.CellStyle.BackColor = Color.MistyRose;
                    e.CellStyle.SelectionBackColor = Color.MistyRose;
                }

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                e.Handled = true;
            }
        }

        private void dgv_Enter(object sender, EventArgs e)
        {
            ((DataGridView)sender).DefaultCellStyle.SelectionForeColor = Color.Red;
        }

        private void dgv_Leave(object sender, EventArgs e)
        {
            ((DataGridView)sender).DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void CustomDateEvent3_Enter(object sender, EventArgs e)
        {
            //this.BackColor = Color.Black;
        }

        private void CustomDateEvent3_Leave(object sender, EventArgs e)
        {
            //this.BackColor = Color.White;
        }

        private void btnHoliday_Click(object sender, EventArgs e)
        {
            using (snEntities db = DBX.DataSet())
            {
                var users = db.users.Include("istab").Where(u => u.istab.typcod == this.note_cal.group_weekend).Select(u => u.name).ToArray<string>();
                MessageAlert.Show(string.Join(", ", users));
            }
        }

        private void btnMaid_Click(object sender, EventArgs e)
        {
            using (snEntities db = DBX.DataSet())
            {
                var users = db.users.Include("istab").Where(u => u.istab.typcod == this.note_cal.group_maid).Select(u => u.name).ToArray<string>();
                MessageAlert.Show(string.Join(", ", users));
            }
        }

        private void btnTrainer_Click(object sender, EventArgs e)
        {
            DialogTrainer t = new DialogTrainer(this.main_form, this);
            t.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogAbsent abs = new DialogAbsent(this.main_form, this.calendar, this, DialogAbsent.PERFORM_ACTION.ADD);
            abs.ShowDialog();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            DialogAbsent abs = new DialogAbsent(this.main_form, this.calendar, this);
            abs.ShowDialog();
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.main_form.loged_in_user.level < (int)USER_LEVEL.SUPERVISOR)
                return;

            if(e.Button == MouseButtons.Right)
            {
                ((DataGridView)sender).Focus();

                int row_index = ((DataGridView)sender).HitTest(e.X, e.Y).RowIndex;
                if (row_index > -1)
                    ((DataGridView)sender).Rows[row_index].Cells[this.col_realname.Name].Selected = true;

                ContextMenu cm = new ContextMenu();
                MenuItem mnu_add = new MenuItem("เพิ่ม");
                mnu_add.Click += delegate
                {
                    this.btnAdd.PerformClick();
                };
                cm.MenuItems.Add(mnu_add);

                MenuItem mnu_edit = new MenuItem("แก้ไข");
                mnu_edit.Click += delegate
                {
                    var event_to_edit = (event_calendar)((DataGridView)sender).Rows[row_index].Cells[this.col_event_calendar.Name].Value;
                    DialogAbsent abs = new DialogAbsent(this.main_form, this.calendar, this, DialogAbsent.PERFORM_ACTION.EDIT, event_to_edit);
                    abs.ShowDialog();
                };
                mnu_edit.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(mnu_edit);

                MenuItem mnu_delete = new MenuItem("ลบ");
                mnu_delete.Click += delegate
                {
                    if(MessageAlert.Show("ลบรายการที่เลือก, ทำต่อหรือไม่", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                    {
                        using (sn_noteEntities note = DBXNote.DataSet())
                        {
                            var event_to_delete = note.event_calendar.Find(((event_calendar)((DataGridView)sender).Rows[row_index].Cells[this.col_event_calendar.Name].Value).id);

                            if (event_to_delete == null)
                            {
                                MessageAlert.Show("ค้นหารายการที่ต้องการลบไม่พบ, อาจมีผู้อื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                                return;
                            }

                            if (event_to_delete.series != null)
                            {
                                DialogConfirmDeleteAbsentRange conf = new DialogConfirmDeleteAbsentRange();
                                if(conf.ShowDialog() == DialogResult.OK)
                                {
                                    if(conf.delete_method == DialogConfirmDeleteAbsentRange.DELETE_METHOD.ALL)
                                    {
                                        var events = note.event_calendar.Where(n => n.series == event_to_delete.series).ToList();
                                        events.ForEach(ev => { note.event_calendar.Remove(ev); });
                                        note.SaveChanges();
                                        this.calendar.RefreshViewDates(events.Select(ev => ev.date).ToList());
                                    }
                                    if(conf.delete_method == DialogConfirmDeleteAbsentRange.DELETE_METHOD.ONE)
                                    {
                                        note.event_calendar.Remove(event_to_delete);
                                        note.SaveChanges();
                                        this.RefreshView();
                                    }
                                }
                            }
                            else
                            {
                                note.event_calendar.Remove(event_to_delete);
                                note.SaveChanges();
                                this.RefreshView();
                            }
                        }
                    }
                };
                mnu_delete.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(mnu_delete);

                cm.Show(((DataGridView)sender), new Point(e.X, e.Y));
            }
        }
    }
}
