using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using SN_Net.Subform;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;

namespace SN_Net.MiscClass
{
    public partial class CustomDateEvent2 : UserControl
    {
        public MainForm main_form;
        public DateTime? date;
        public List<AbsentVM> absent_list;
        public NoteCalendar note;
        private List<Istab> absent_cause;
        private List<TrainingCalendar> trainer_list;
        private List<Users> users_list;
        private int current_month;
        private int max_leave;
        private Calendar2 calendar;
        BindingSource bs;

        private Color bgDayInMonth = Color.MediumSlateBlue;
        private Color bgToday = Color.MediumSeaGreen;
        private Color bgHolidayInMonth = Color.Thistle;
        private Color bgDayOutOfMonth = Color.LightGray;

        public CustomDateEvent2(MainForm main_form, Calendar2 calendar, DateTime date, int current_month, List<AbsentVM> absent_list, List<Istab> absent_cause, List<TrainingCalendar> trainer_list, NoteCalendar note, List<Users> users_list, int max_leave)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            InitializeComponent();
            this.main_form = main_form;
            this.date = date;
            this.current_month = current_month;
            this.absent_list = absent_list;
            this.absent_cause = absent_cause;
            this.trainer_list = trainer_list;
            this.note = note;
            this.users_list = users_list;
            this.max_leave = max_leave;
            this.calendar = calendar;
        }

        private void CustomDateEvent2_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.bs = new BindingSource();
            this.bs.DataSource = this.absent_list;

            this.RefreshView();
        }

        public void RefreshData()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "eventcalendar/get_event&from_date=" + this.date.Value.ToMysqlDate() + "&to_date=" + this.date.Value.ToMysqlDate());
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.absent_list = sr.event_calendar.ToAbsentViewModel(this.absent_cause, this.users_list, this.max_leave);
                this.trainer_list = sr.training_calendar;
                this.note = sr.note_calendar.Where(t => t.date == this.date.Value.ToMysqlDate()).FirstOrDefault();
            }
        }

        public void RefreshView()
        {
            // lblDay
            this.lblDay.Text = this.date.Value.Day.ToString();
            if (this.date.Value.Month != this.current_month)
            {
                this.lblDay.BackColor = this.bgDayOutOfMonth;
            }
            else if (this.note != null && this.note.type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY || this.IsLastSaturday(this.date.Value) || this.date.Value.GetDayIntOfWeek() == 1 /* is sunday */)
            {
                this.lblDay.BackColor = this.bgHolidayInMonth;
            }
            else
            {
                this.lblDay.BackColor = this.date.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") ? this.bgToday : this.bgDayInMonth;
            }

            // btnDropDownMenu, btnAdd, btnTrainer, btnDetail availabilities
            if (this.date.Value.Month != this.current_month)
            {
                this.btnDropDownMenu.Enabled = false;
            }
            else if (this.IsLastSaturday(this.date.Value) /* is last saturday */ || this.date.Value.GetDayIntOfWeek() == 1 /* is sunday */)
            {
                this.btnDropDownMenu.Enabled = false;
            }
            else
            {
                this.btnDropDownMenu.Enabled = true;
                if (this.note != null && this.note.type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY)
                {
                    this.btnTrainer.Enabled = false;
                    this.btnAdd.Enabled = false;
                    this.btnDetail.Enabled = this.main_form.G.loged_in_user_level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
                }
                else
                {
                    this.btnTrainer.Enabled = this.main_form.G.loged_in_user_level >= (int)USER_LEVEL.SUPERVISOR || this.main_form.G.loged_in_user_training_expert ? true : false;
                    this.btnAdd.Enabled = this.main_form.G.loged_in_user_level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
                    this.btnDetail.Enabled = this.main_form.G.loged_in_user_level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
                }
            }

            // lblNoteDescription
            if (this.note != null && this.note.type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY)
            {
                this.lblNoteDescription.Text = this.note.description;
                this.dgv.SendToBack();
                this.lblNoteDescription.BringToFront();
            }
            else
            {
                this.lblNoteDescription.Text = "";
                this.lblNoteDescription.SendToBack();
                this.dgv.BringToFront();
            }

            // btnHoliday, btnMaid text and visibilities
            this.btnHoliday.Text = this.note != null ? this.note.group_weekend : "";
            this.btnMaid.Text = this.note != null ? this.note.group_maid : "";
            if (this.date.Value.GetDayIntOfWeek() == 7 && !this.IsLastSaturday(this.date.Value)) // if is saturday
            {
                if (this.note != null)
                {
                    if (this.note.type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY)
                    {
                        this.btnHoliday.Visible = false;
                        this.btnMaid.Visible = false;
                    }
                    else
                    {
                        this.btnHoliday.Text = this.note.group_weekend;
                        this.btnMaid.Text = this.note.group_maid;
                        this.btnHoliday.Visible = true;
                        this.btnMaid.Visible = true;
                    }
                }
                else
                {
                    this.btnHoliday.Visible = false;
                    this.btnMaid.Visible = false;
                }
            }
            else
            {
                this.btnHoliday.Visible = false;
                this.btnMaid.Visible = false;
            }

            // lblBottomText
            string bottom_text = this.trainer_list.Count > 0 ? "อบรม(" : "";

            foreach (var trainer in this.trainer_list.OrderBy(t => t.term.ToString() + t.status.ToString()))
            {
                bottom_text += trainer.id == this.trainer_list.OrderBy(t => t.term.ToString() + t.status.ToString()).First().id ? "" : ",";
                bottom_text += this.users_list.Where(u => u.username.Trim() == trainer.trainer.Trim()).First().name;
            }
            bottom_text += this.trainer_list.Count > 0 ? ")" : "";
            this.lblBottomText.Text = bottom_text;
            
            // Fill a datagrid
            this.FillDataGrid();
        }

        private void FillDataGrid()
        {
            this.dgv.DataSource = this.bs;
            this.bs.ResetBindings(true);
            this.bs.DataSource = this.absent_list;
        }

        private bool IsLastSaturday(DateTime date_to_check)
        {
            if (date_to_check.GetDayIntOfWeek() != 7)
                return false;

            if (date_to_check.Month != date_to_check.AddDays(7).Month)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.current_month != this.date.Value.Month)
            {
                using (SolidBrush brush = new SolidBrush(Color.LightGray))
                {
                    using (Font font = new Font("tahoma", 8f, FontStyle.Regular))
                    {
                        e.Graphics.DrawString(this.date.Value.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("th-TH")), font, brush, new PointF(30, 8));
                    }
                }
            }
            else if ((this.note != null && this.note.type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY) || this.IsLastSaturday(this.date.Value) || this.date.Value.GetDayIntOfWeek() == 1 /* is sunday */)
            {
                using (SolidBrush brush = new SolidBrush(Color.Thistle))
                {
                    using (Font font = new Font("tahoma", 8f, FontStyle.Regular))
                    {
                        e.Graphics.DrawString(this.date.Value.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("th-TH")), font, brush, new PointF(30, 8));
                    }
                }
            }
            else
            {
                using (SolidBrush brush_blue = new SolidBrush(Color.MediumSlateBlue))
                {
                    using (SolidBrush brush_green = new SolidBrush(Color.MediumSeaGreen))
                    {
                        using (Font font = new Font("tahoma", 8f, FontStyle.Regular))
                        {
                            if (this.date.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                            {
                                e.Graphics.DrawString(this.date.Value.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("th-TH")), font, brush_green, new PointF(30, 8));
                            }
                            else
                            {
                                e.Graphics.DrawString(this.date.Value.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("th-TH")), font, brush_blue, new PointF(30, 8));
                            }
                        }
                    }
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.PainFocusedDateBorder();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.calendar.current_date = this.date.Value;
            this.PainFocusedDateBorder();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.Refresh();
            if (this.current_month != this.date.Value.Month)
            {
                this.lblDay.BackColor = this.bgDayOutOfMonth;
            }
            else if ((this.note != null && this.note.type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY) || this.IsLastSaturday(this.date.Value) || this.date.Value.GetDayIntOfWeek() == 1 /* is sunday */)
            {
                this.lblDay.BackColor = this.bgHolidayInMonth;
            }
            else
            {
                this.lblDay.BackColor = this.date.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") ? this.bgToday : this.bgDayInMonth;
            }
        }

        private void PainFocusedDateBorder()
        {
            if (this.date != null &&  this.date == this.calendar.current_date)
            {
                using (Pen p = new Pen(Color.Crimson))
                {
                    this.CreateGraphics().DrawRectangle(p, new Rectangle(this.DisplayRectangle.X, this.DisplayRectangle.Y, this.DisplayRectangle.Width - 1, this.DisplayRectangle.Height - 1));
                }
                this.lblDay.BackColor = this.date.Value.Month == this.current_month ? Color.Crimson : this.lblDay.BackColor;
            }
        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value == null)
                return;

            EventCalendar ev = (EventCalendar)((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value;
            if (ev.status == (int)EventCalendar.EVENT_STATUS.WAIT)
            {
                for (int i = 0; i < ((DataGridView)sender).Columns.Count; i++)
                {
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[i].Style.BackColor = Color.FromArgb(217, 241, 249);
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[i].Style.SelectionBackColor = Color.FromArgb(217, 241, 249);
                }
                return;
            }
            if (ev.status == (int)EventCalendar.EVENT_STATUS.CANCELED)
            {
                for (int i = 0; i < ((DataGridView)sender).Columns.Count; i++)
                {
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[i].Style.BackColor = Color.FromArgb(249, 217, 217);
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[i].Style.SelectionBackColor = Color.FromArgb(249, 217, 217);
                }
                return;
            }

            int countable_leave_person = (int)((DataGridView)sender).Rows[e.RowIndex].Cells["colCountableLeavePerson"].Value;
            if (countable_leave_person == 0)
            {
                for (int i = 0; i < ((DataGridView)sender).Columns.Count; i++)
                {
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[i].Style.BackColor = Color.FromArgb(249, 237, 217);
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[i].Style.SelectionBackColor = Color.FromArgb(249, 237, 217);
                }
                return;
            }
        }

        private void btnHoliday_Click(object sender, EventArgs e)
        {
            this.Focus();
            string str_group = ((ToolStripButton)sender).Text.Trim();
            
            if (str_group.Length == 0)
                return;

            string user_in_group = "สมาชิกในกลุ่ม " + str_group + " : ";
            foreach (var user in this.users_list.Where(u => u.usergroup != null).Where(u => u.usergroup.Trim() == str_group).OrderBy(u => u.username))
            {
                user_in_group += user.id == this.users_list.Where(u => u.usergroup != null).Where(u => u.usergroup.Trim() == str_group).OrderBy(u => u.username).First().id ? "" : ", ";
                user_in_group += user.name;
            }

            MessageAlert.Show(user_in_group, "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
        }

        private void btnMaid_Click(object sender, EventArgs e)
        {
            this.Focus();
            string str_group = ((ToolStripButton)sender).Text.Trim();

            if (str_group.Length == 0)
                return;

            string user_in_group = "สมาชิกในกลุ่ม " + str_group + " : ";
            foreach (var user in this.users_list.Where(u => u.usergroup != null).Where(u => u.usergroup.Trim() == str_group).OrderBy(u => u.username))
            {
                user_in_group += user.id == this.users_list.Where(u => u.usergroup != null).Where(u => u.usergroup.Trim() == str_group).OrderBy(u => u.username).First().id ? "" : ", ";
                user_in_group += user.name;
            }

            MessageAlert.Show(user_in_group, "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (((DataGridView)sender).Focused)
            {
                foreach (DataGridViewCell cell in ((DataGridView)sender).CurrentRow.Cells)
                {
                    cell.Style.SelectionForeColor = Color.Red;
                }
            }
            else
            {
                foreach (DataGridViewCell cell in ((DataGridView)sender).CurrentRow.Cells)
                {
                    cell.Style.SelectionForeColor = Color.Black;
                }
            }
        }

        private void btnTrainer_Click(object sender, EventArgs e)
        {
            TrainingExpertWindow wind = new TrainingExpertWindow(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.RefreshData();
                this.RefreshView();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DateEventWindow wind = new DateEventWindow(this, true);
            wind.ShowDialog();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            DateEventWindow wind = new DateEventWindow(this);
            wind.ShowDialog();
        }

        private void DoCopy(DateTime date, EventCalendar event_calendar)
        {
            bool post_success = false;
            string err_msg = "";
            int inserted_id = -1;

            string json_data = "{\"users_name\":\"" + event_calendar.users_name + "\",";
            json_data += "\"date\":\"" + date.ToMysqlDate() + "\",";
            json_data += "\"from_time\":\"" + event_calendar.from_time + "\",";
            json_data += "\"to_time\":\"" + event_calendar.to_time + "\",";
            json_data += "\"event_type\":\"" + event_calendar.event_type + "\",";
            json_data += "\"event_code\":\"" + event_calendar.event_code + "\",";
            json_data += "\"customer\":\"" + event_calendar.customer + "\",";
            json_data += "\"status\":\"" + event_calendar.status.ToString() + "\",";
            json_data += "\"med_cert\":\"" + event_calendar.med_cert + "\",";
            json_data += "\"fine\":" + event_calendar.fine.ToString() + ",";
            json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "eventcalendar/create", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                    inserted_id = Convert.ToInt32(sr.message);
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
                    foreach (Control ct in this.ParentForm.Controls["tableLayoutPanel1"].Controls)
                    {
                        if (ct.GetType() == typeof(CustomDateEvent2))
                        {
                            CustomDateEvent2 date_event = ct as CustomDateEvent2;
                            if (date_event.date.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy"))
                            {
                                date_event.RefreshData();
                                date_event.RefreshView();
                                
                                DataGridViewRow target_row = ((CustomDateEvent2)ct).dgv.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value != null && ((EventCalendar)r.Cells[0].Value).id == inserted_id).FirstOrDefault();
                                if (target_row != null)
                                {
                                    date_event.dgv.Focus();
                                    date_event.dgv.CurrentCell = target_row.Cells[1];
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.DoCopy(date, event_calendar);
                    }
                }
            };
            worker.RunWorkerAsync();
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            int row_index = ((DataGridView)sender).HitTest(e.X, e.Y).RowIndex;

            if (row_index > -1 && e.Button == MouseButtons.Right)
            {
                ((DataGridView)sender).Focus();
                ((DataGridView)sender).CurrentCell = ((DataGridView)sender).Rows[row_index].Cells[1];
                this.ShowContextMenu(sender, e.X, e.Y);
            }
        }

        private void ShowContextMenu(object sender, int x_pos = -1, int y_pos = -1)
        {
            if (((DataGridView)sender).Rows.Count == 0)
                return;

            int row_index = ((DataGridView)sender).CurrentCell.RowIndex;

            EventCalendar event_calendar = null;
            var ev = ((DataGridView)sender).Rows[row_index].Cells[0].Value;
            if (ev != null)
            {
                event_calendar = ev as EventCalendar;
            }

            ContextMenu cm = new ContextMenu();

            MenuItem mnu_add = new MenuItem();
            mnu_add.Text = "เพิ่ม";
            mnu_add.Click += delegate
            {
                DateEventWindow date_event = new DateEventWindow(this, true);
                date_event.ShowDialog();
            };
            cm.MenuItems.Add(mnu_add);

            MenuItem mnu_edit = new MenuItem();
            mnu_edit.Text = "แก้ไข";
            mnu_edit.Enabled = event_calendar != null ? true : false;
            mnu_edit.Click += delegate
            {
                DateEventWindow date_event = new DateEventWindow(this, true, event_calendar);
                date_event.ShowDialog();
            };
            cm.MenuItems.Add(mnu_edit);

            MenuItem mnu_copy = new MenuItem();
            mnu_copy.Text = "คัดลอกรายการนี้ไปยังวันที่ ...";
            mnu_copy.Enabled = event_calendar != null ? true : false;
            mnu_copy.Click += delegate
            {
                DateSelectorDialog ds = new DateSelectorDialog(this.date.Value);
                if (ds.ShowDialog() == DialogResult.OK)
                {
                    this.DoCopy(ds.selected_date, (EventCalendar)this.dgv.Rows[row_index].Cells[0].Value);
                }
            };
            cm.MenuItems.Add(mnu_copy);

            MenuItem mnu_delete = new MenuItem();
            mnu_delete.Text = "ลบ";
            mnu_delete.Enabled = event_calendar != null ? true : false;
            mnu_delete.Click += delegate
            {
                if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                {
                    bool delete_success = false;
                    string err_msg = "";
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += delegate
                    {
                        CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "eventcalendar/delete&id=" + ((EventCalendar)this.dgv.Rows[row_index].Cells[0].Value).id.ToString());
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            delete_success = true;
                        }
                        else
                        {
                            delete_success = false;
                            err_msg = sr.message;
                        }
                    };
                    worker.RunWorkerCompleted += delegate
                    {
                        if (delete_success)
                        {
                            this.RefreshData();
                            this.RefreshView();
                        }
                        else
                        {
                            this.RefreshData();
                            this.RefreshView();
                            MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        }
                    };
                    worker.RunWorkerAsync();
                }
            };
            cm.MenuItems.Add(mnu_delete);

            if (x_pos < 0 || y_pos < 0)
            {
                x_pos = ((DataGridView)sender).GetCellDisplayRectangle(2, row_index, true).X;
                y_pos = ((DataGridView)sender).GetCellDisplayRectangle(2, row_index, true).Y;
            }

            cm.Show((DataGridView)sender, new Point(x_pos, y_pos));
        }

        private void lblBottomText_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void btnDropDownMenu_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void lblDay_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void lblNoteDescription_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void dgv_Enter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void dgv_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void dgv_Scroll(object sender, ScrollEventArgs e)
        {
            this.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Apps && this.dgv.Focused)
            {
                this.ShowContextMenu(this.dgv);
                return true;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
