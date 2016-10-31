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
    public partial class DateEventWindow : Form
    {
        private CustomDateEvent2 cde;
        private bool begin_add_at_first_show = false;
        private bool begin_edit_at_first_show = false;
        private EventCalendar current_event = null;
        CultureInfo cinfo_th = new CultureInfo("th-TH");
        private List<Users> users_list;
        private List<Istab> leave_cause;
        private List<Istab> users_group;
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            READ,
            EDIT,
            READ_ITEM,
            ADD_ITEM,
            EDIT_ITEM,
            PROCESSING
        }

        private enum LEAVE_STATUS : int
        {
            WAIT = 0,
            CONFIRMED = 1,
            CANCELED = 2
        }

        private enum DGV_TAG
        {
            NORMAL,
            DELETE
        }

        public DateEventWindow()
        {
            InitializeComponent();
        }

        //public DateEventWindow(CustomDateEvent cde)
        //    : this()
        //{
        //    this.cde = cde;
        //}

        //public DateEventWindow(CustomDateEvent cde, bool begin_add_at_first_show)
        //    : this()
        //{
        //    this.cde = cde;
        //    this.begin_add_at_first_show = begin_add_at_first_show;
        //}

        //public DateEventWindow(CustomDateEvent cde, bool begin_edit_at_first_show, EventCalendar ev)
        //    : this()
        //{
        //    this.cde = cde;
        //    this.begin_edit_at_first_show = begin_edit_at_first_show;
        //    this.current_event = ev;
        //}

        public DateEventWindow(CustomDateEvent2 cde)
            : this()
        {
            this.cde = cde;
        }

        public DateEventWindow(CustomDateEvent2 cde, bool begin_add_at_first_show)
            : this()
        {
            this.cde = cde;
            this.begin_add_at_first_show = begin_add_at_first_show;
        }

        public DateEventWindow(CustomDateEvent2 cde, bool begin_edit_at_first_show, EventCalendar ev)
            : this()
        {
            this.cde = cde;
            this.begin_edit_at_first_show = begin_edit_at_first_show;
            this.current_event = ev;
        }

        private void DateEventWindow_Load(object sender, EventArgs e)
        {
            this.txtDummy.Width = 0;
            this.BindingControlEvent();

            //this.groupBox1.Text = this.cde.Date.ThaiDayOfWeek() + " ที่ " + this.cde.Date.ToString("d MMMM yyyy", cinfo_th);
            this.groupBox1.Text = this.cde.date.Value.ThaiDayOfWeek() + " ที่ " + this.cde.date.Value.ToString("d MMMM yyyy", cinfo_th);

            #region Load users_list from server
            CRUDResult get_user = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_all");
            ServerResult sr_user = JsonConvert.DeserializeObject<ServerResult>(get_user.data);

            if (sr_user.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.users_list = sr_user.users;
            }
            #endregion Load users_list from server

            #region Load Absent_cause and Service_case from server
            CRUDResult get_leave_cause = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_leave_cause");
            ServerResult sr_leave_cause = JsonConvert.DeserializeObject<ServerResult>(get_leave_cause.data);

            if (sr_leave_cause.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.leave_cause = sr_leave_cause.istab;
            }
            #endregion Load Absent_cause and Service_case from server

            #region Load Users Group from Server
            CRUDResult get_group = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_all&tabtyp=" + Istab.TABTYP.USER_GROUP.ToTabtypString() + "&sort=typcod");

            ServerResult sr_group = JsonConvert.DeserializeObject<ServerResult>(get_group.data);
            if (sr_group.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.users_group = sr_group.istab;
            }

            this.cbGroupWeekend.AddItem(new ComboboxItem("", -1, ""));
            foreach (Istab g in this.users_group)
            {
                this.cbGroupWeekend.AddItem(new ComboboxItem(g.typcod + " : " + g.typdes_th, g.id, g.typcod) { Tag = g });
            }

            this.cbGroupMaid.AddItem(new ComboboxItem("", -1, ""));
            foreach (Istab g in this.users_group)
            {
                this.cbGroupMaid.AddItem(new ComboboxItem(g.typcod + " : " + g.typdes_th, g.id, g.typcod) { Tag = g });
            }
            #endregion Load Users Group from Server

            this.InitControl();

            this.FillDataGrid();
        }

        private void DateEventWindow_Shown(object sender, EventArgs e)
        {
            if (this.begin_add_at_first_show)
            {
                this.dgv.Rows[this.cde.absent_list.ExtractToEventCalendar().Count].Cells[1].Selected = true;
                this.FormAddItem();
                this.ShowInlineForm();

                if (this.dgv.Parent.Controls.Find("inline_users_name", true).Length > 0)
                {
                    ((CustomComboBox)this.dgv.Parent.Controls.Find("inline_users_name", true)[0]).Focus();
                }
            }
            else if (this.begin_edit_at_first_show && this.current_event != null)
            {
                if (this.dgv.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is EventCalendar).Where(r => ((EventCalendar)r.Tag).id == this.current_event.id).Count<DataGridViewRow>() > 0)
                {
                    this.dgv.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is EventCalendar).Where(r => ((EventCalendar)r.Tag).id == this.current_event.id).First<DataGridViewRow>().Cells[1].Selected = true;
                }
                else
                {
                    return;
                }
                this.FormEditItem();
                this.ShowInlineForm();

                if (this.dgv.Parent.Controls.Find("inline_users_name", true).Length > 0)
                {
                    ((CustomComboBox)this.dgv.Parent.Controls.Find("inline_users_name", true)[0]).Focus();
                }
            }
            else
            {
                this.FormRead();
            }
        }

        private void InitControl()
        {
            this.rbHoliday.Checked = (this.cde.note != null && ((NoteCalendar)this.cde.note).type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY ? true : false);
            this.rbWeekday.Checked = ((this.cde.note != null && ((NoteCalendar)this.cde.note).type == (int)NoteCalendar.NOTE_TYPE.WEEKDAY) || this.cde.note == null ? true : false);
            this.txtHoliday.Texts = (this.cde.note != null && ((NoteCalendar)this.cde.note).type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY ? this.cde.note.description : "");
            if (this.cde.note != null)
            {
                this.cbGroupMaid.comboBox1.SelectedItem = (((NoteCalendar)this.cde.note).type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY ? this.cbGroupMaid.comboBox1.Items[0] : (this.cbGroupMaid.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.Tag != null).Where(i => ((Istab)i.Tag).typcod == this.cde.note.group_maid).Count<ComboboxItem>() > 0 ? this.cbGroupMaid.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.Tag != null).Where(i => ((Istab)i.Tag).typcod == this.cde.note.group_maid).First<ComboboxItem>() : this.cbGroupMaid.comboBox1.Items[0]));
                this.cbGroupWeekend.comboBox1.SelectedItem = (((NoteCalendar)this.cde.note).type == (int)NoteCalendar.NOTE_TYPE.HOLIDAY ? this.cbGroupWeekend.comboBox1.Items[0] : (this.cbGroupWeekend.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.Tag != null).Where(i => ((Istab)i.Tag).typcod == this.cde.note.group_weekend).Count<ComboboxItem>() > 0 ? this.cbGroupWeekend.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.Tag != null).Where(i => ((Istab)i.Tag).typcod == this.cde.note.group_weekend).First<ComboboxItem>() : this.cbGroupWeekend.comboBox1.Items[0]));
            }
            else
            {
                this.cbGroupMaid.comboBox1.SelectedItem = this.cbGroupMaid.comboBox1.Items[0];
                this.cbGroupWeekend.comboBox1.SelectedItem = this.cbGroupWeekend.comboBox1.Items[0];
            }
            this.leaveMax.Value = (this.cde.note != null ? this.cde.note.max_leave : -1);
        }

        private void BindingControlEvent()
        {
            this.dgv.Paint += delegate
            {
                if (this.dgv.CurrentCell != null)
                {
                    Rectangle rect = this.dgv.GetRowDisplayRectangle(this.dgv.CurrentCell.RowIndex, false);
                    using (Pen p = new Pen(Color.Red))
                    {
                        this.dgv.CreateGraphics().DrawLine(p, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                        this.dgv.CreateGraphics().DrawLine(p, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);

                        if ((DGV_TAG)this.dgv.Tag == DGV_TAG.DELETE)
                        {
                            for (int i = rect.Left - 16; i < rect.Right; i += 8)
                            {
                                this.dgv.CreateGraphics().DrawLine(p, i, rect.Bottom - 2, i + 23, rect.Top);
                            }
                        }
                    }
                }
            };

            this.dgv.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag is EventCalendar)
                {
                    this.FormEditItem();
                }
                else
                {
                    this.FormAddItem();
                    this.dgv.Rows[this.cde.absent_list.ExtractToEventCalendar().Count].Cells[1].Selected = true;
                }
                this.ShowInlineForm();
            };

            this.dgv.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM)
                    {
                        if (this.form_mode == FORM_MODE.READ)
                        {
                            this.FormReadItem();
                        }

                        int row_index = this.dgv.HitTest(e.X, e.Y).RowIndex;
                        this.dgv.Rows[row_index].Cells[1].Selected = true;

                        ContextMenu m = new ContextMenu();
                        MenuItem m_add = new MenuItem("เพิ่ม <Alt+A>");
                        m_add.Click += delegate
                        {
                            this.dgv.Rows[this.cde.absent_list.ExtractToEventCalendar().Count].Cells[1].Selected = true;
                            this.FormAddItem();
                            this.ShowInlineForm();
                        };
                        m_add.Enabled = true;
                        m.MenuItems.Add(m_add);

                        MenuItem m_edit = new MenuItem("แก้ไข <Alt+E>");
                        m_edit.Click += delegate
                        {
                            this.FormEditItem();
                            this.ShowInlineForm();
                        };
                        m_edit.Enabled = (this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag is EventCalendar ? true : false);
                        m.MenuItems.Add(m_edit);

                        MenuItem m_copy = new MenuItem("คัดลอกไปยังวันที่ ... <Alt+C>");
                        m_copy.Click += delegate
                        {
                            DateSelectorDialog ds = new DateSelectorDialog(this.cde.date.Value);
                            if (ds.ShowDialog() == DialogResult.OK)
                            {
                                this.DoCopy(ds.selected_date, (EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag);
                            }
                        };
                        m_copy.Enabled = (this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag is EventCalendar ? true : false);
                        m.MenuItems.Add(m_copy);

                        MenuItem m_delete = new MenuItem("ลบ <Alt+D>");
                        m_delete.Click += delegate
                        {
                            this.DeleteItem();
                        };
                        m_delete.Enabled = (this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag is EventCalendar ? true : false);
                        m.MenuItems.Add(m_delete);

                        m.Show(this.dgv, new Point(e.X, e.Y));
                    }
                }
            };

            List<Control> ct = new List<Control>();
            ct.Add(this.txtHoliday.label1);
            ct.Add(this.cbGroupMaid.label1);
            ct.Add(this.cbGroupWeekend.label1);
            ct.Add(this.leaveMax);
            foreach (Control c in ct)
            {
                c.DoubleClick += delegate
                {
                    this.toolStripEdit.PerformClick();
                };
            }

            this.rbHoliday.EnabledChanged += delegate
            {
                if (this.rbHoliday.Enabled && this.rbHoliday.Checked)
                {
                    this.txtHoliday.Read_Only = false;
                }
                else
                {
                    this.txtHoliday.Read_Only = true;
                }
            };

            this.rbWeekday.EnabledChanged += delegate
            {
                if (this.rbWeekday.Enabled && this.rbWeekday.Checked)
                {
                    this.cbGroupWeekend.Read_Only = false;
                    this.cbGroupMaid.Read_Only = false;
                    this.leaveMax.Enabled = true;
                }
                else
                {
                    this.cbGroupWeekend.Read_Only = true;
                    this.cbGroupMaid.Read_Only = true;
                    this.leaveMax.Enabled = false;
                }
            };

            this.rbHoliday.CheckedChanged += delegate
            {
                if (this.rbHoliday.Checked && this.rbHoliday.Enabled)
                {
                    this.txtHoliday.Read_Only = false;
                    this.leaveMax.Enabled = false;
                }
                else
                {
                    this.txtHoliday.Read_Only = true;
                }
            };

            this.rbWeekday.CheckedChanged += delegate
            {
                if (this.rbWeekday.Checked && this.rbWeekday.Enabled)
                {
                    this.cbGroupWeekend.Read_Only = false;
                    this.cbGroupMaid.Read_Only = false;
                    this.leaveMax.Enabled = true;
                }
                else
                {
                    this.cbGroupWeekend.Read_Only = true;
                    this.cbGroupMaid.Read_Only = true;
                    this.leaveMax.Enabled = false;
                }
            };

            this.txtHoliday.textBox1.TextChanged += delegate
            {
                if (this.txtHoliday.Texts.Length > 0)
                {
                    this.cbGroupMaid.comboBox1.SelectedIndex = 0;
                    this.cbGroupWeekend.comboBox1.SelectedIndex = 0;
                }
            };

            //this.txtRemark.textBox1.TextChanged += delegate
            //{
            //    if (this.txtRemark.Texts.Length > 0)
            //    {
            //        this.txtHoliday.Texts = "";
            //    }
            //};

            this.leaveMax.GotFocus += delegate
            {
                this.leaveMax.Select(0, this.leaveMax.Text.Length);
            };
        }

        private void FillDataGrid(EventCalendar selected_item = null)
        {
            this.dgv.Rows.Clear();
            this.dgv.Columns.Clear();
            this.dgv.Tag = DGV_TAG.NORMAL;

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.Visible = false;
            this.dgv.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Width = 40;
            col1.HeaderText = "ลำดับ";
            col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Width = 120;
            col2.HeaderText = "ชื่อพนักงาน";
            col2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Width = 120;
            col3.HeaderText = "เหตุผล";
            col3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Width = 60;
            col4.HeaderText = "จาก";
            col4.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Width = 60;
            col5.HeaderText = "ถึง";
            col5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns.Add(col5);

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Width = 80;
            col6.HeaderText = "สถานะ";
            col6.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns.Add(col6);

            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            col7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col7.HeaderText = "หมายเหตุ/ชื่อลูกค้า";
            col7.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns.Add(col7);

            DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
            col8.Width = 120;
            col8.HeaderText = "เอกสารอ้างอิง";
            col8.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns.Add(col8);

            DataGridViewTextBoxColumn col9 = new DataGridViewTextBoxColumn();
            col9.Width = 90;
            col9.HeaderText = "หักค่าคอมฯ";
            col9.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns.Add(col9);

            int row_count = 0;

            List<EventCalendar> support_list = new List<EventCalendar>();
            List<EventCalendar> supervisor_list = new List<EventCalendar>();
            foreach (EventCalendar e in this.cde.absent_list.ExtractToEventCalendar())
            {
                if (this.users_list.Where(u => u.username == e.users_name && u.level >= GlobalVar.USER_LEVEL_SUPERVISOR).Count<Users>() > 0)
                {
                    supervisor_list.Add(e);
                }
                else
                {
                    support_list.Add(e);
                }
            }

            foreach (EventCalendar ev in support_list)
            {
                int r = this.dgv.Rows.Add();
                this.dgv.Rows[r].Tag = ev;

                this.dgv.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgv.Rows[r].Cells[0].Value = ev.id;

                row_count += (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? 0 : 1);
                this.dgv.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[1].Value = (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? "" : row_count.ToString());
                this.dgv.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgv.Rows[r].Cells[1].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[1].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgv.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[2].Value = ev.users_name + " : " + ev.realname;
                this.dgv.Rows[r].Cells[2].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[2].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgv.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[3].Value = this.GetEventTypdes(ev.event_type, ev.event_code);
                this.dgv.Rows[r].Cells[3].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[3].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgv.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[4].Value = ev.from_time.Substring(0, 5);
                this.dgv.Rows[r].Cells[4].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[4].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgv.Rows[r].Cells[5].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[5].Value = ev.to_time.Substring(0, 5);
                this.dgv.Rows[r].Cells[5].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[5].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgv.Rows[r].Cells[6].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[6].Value = this.GetLeaveStatusString((int)ev.status);
                this.dgv.Rows[r].Cells[6].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[6].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgv.Rows[r].Cells[7].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[7].Value = ev.customer; //(ev.customer.Length > 0 ? ev.customer : this.cde.GetTimeString(ev));
                this.dgv.Rows[r].Cells[7].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[7].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgv.Rows[r].Cells[8].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[8].Value = (ev.med_cert == "Y" ? "มีใบรับรองแพทย์" : (ev.med_cert == "N" ? "ไม่มีเอกสาร" : ""));
                this.dgv.Rows[r].Cells[8].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[8].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[8].Style.ForeColor = (ev.med_cert == "N" ? Color.Red : Color.Black);
                this.dgv.Rows[r].Cells[8].Style.SelectionForeColor = (ev.med_cert == "N" ? Color.Red : Color.Black);

                this.dgv.Rows[r].Cells[9].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[9].Value = (ev.fine > 0 ? ev.fine.ToString() : "");
                this.dgv.Rows[r].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgv.Rows[r].Cells[9].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[9].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgv.Rows[r].Cells[9].Style.ForeColor = Color.Red;
                this.dgv.Rows[r].Cells[9].Style.SelectionForeColor = Color.Red;
            }
            foreach (EventCalendar ev in supervisor_list)
            {
                int r = this.dgv.Rows.Add();
                this.dgv.Rows[r].Tag = ev;

                this.dgv.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgv.Rows[r].Cells[0].Value = ev.id;

                this.dgv.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[1].Value = "";
                this.dgv.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgv.Rows[r].Cells[1].Style.BackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[1].Style.SelectionBackColor = Color.Wheat;

                this.dgv.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[2].Value = ev.users_name + " : " + ev.realname;
                this.dgv.Rows[r].Cells[2].Style.BackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[2].Style.SelectionBackColor = Color.Wheat;

                this.dgv.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[3].Value = this.GetEventTypdes(ev.event_type, ev.event_code);
                this.dgv.Rows[r].Cells[3].Style.BackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[3].Style.SelectionBackColor = Color.Wheat;

                this.dgv.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[4].Value = ev.from_time.Substring(0, 5);
                this.dgv.Rows[r].Cells[4].Style.BackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[4].Style.SelectionBackColor = Color.Wheat;

                this.dgv.Rows[r].Cells[5].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[5].Value = ev.to_time.Substring(0, 5);
                this.dgv.Rows[r].Cells[5].Style.BackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[5].Style.SelectionBackColor = Color.Wheat;

                this.dgv.Rows[r].Cells[6].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[6].Value = this.GetLeaveStatusString((int)ev.status);
                this.dgv.Rows[r].Cells[6].Style.BackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[6].Style.SelectionBackColor = Color.Wheat;

                this.dgv.Rows[r].Cells[7].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[7].Value = ev.customer;
                this.dgv.Rows[r].Cells[7].Style.BackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[7].Style.SelectionBackColor = Color.Wheat;

                this.dgv.Rows[r].Cells[8].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[8].Value = (ev.med_cert == "Y" ? "มีใบรับรองแพทย์" : (ev.med_cert == "N" ? "ไม่มีเอกสาร" : ""));
                this.dgv.Rows[r].Cells[8].Style.BackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[8].Style.SelectionBackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[8].Style.ForeColor = (ev.med_cert == "N" ? Color.Red : Color.Black);
                this.dgv.Rows[r].Cells[8].Style.SelectionForeColor = (ev.med_cert == "N" ? Color.Red : Color.Black);

                this.dgv.Rows[r].Cells[9].ValueType = typeof(string);
                this.dgv.Rows[r].Cells[9].Value = (ev.fine > 0 ? ev.fine.ToString() : "");
                this.dgv.Rows[r].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgv.Rows[r].Cells[9].Style.BackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[9].Style.SelectionBackColor = Color.Wheat;
                this.dgv.Rows[r].Cells[9].Style.ForeColor = Color.Red;
                this.dgv.Rows[r].Cells[9].Style.SelectionForeColor = Color.Red;
            }
            this.dgv.FillLine(this.cde.absent_list.ExtractToEventCalendar().Count + 4);

            if (selected_item != null)
            {
                if (this.dgv.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is EventCalendar).Where(r => ((EventCalendar)r.Tag).id == selected_item.id).Count() > 0)
                {
                    this.dgv.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is EventCalendar).Where(r => ((EventCalendar)r.Tag).id == selected_item.id).First<DataGridViewRow>().Cells[1].Selected = true;
                }
            }
        }

        private void DoCopy(DateTime date, EventCalendar event_calendar)
        {
            bool post_success = false;
            string err_msg = "";
            int inserted_id = -1;

            this.FormProcessing();

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
            json_data += "\"rec_by\":\"" + this.cde.main_form.G.loged_in_user_name + "\"}";

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
                    foreach (CustomDateEvent ct in this.cde.Parent.Controls)
                    {
                        if (ct.Date.ToDMYDateValue() == date.ToDMYDateValue())
                        {
                            ct.RefreshData();
                            ct.RefreshView();
                        }
                    }
                    if (this.cde.date.Value.ToDMYDateValue() == date.ToDMYDateValue())
                    {
                        this.FillDataGrid();
                        this.dgv.Rows[this.cde.absent_list.ExtractToEventCalendar().FindIndex(t => t.id == inserted_id)].Cells[1].Selected = true;
                    }
                    this.FormReadItem();
                }
                else
                {
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.DoCopy(date, event_calendar);
                    }
                    this.FormReadItem();
                }
            };
            worker.RunWorkerAsync();
        }

        private string GetEventTypdes(string event_type, string event_code)
        {
            if (this.leave_cause.Find(t => t.tabtyp == event_type && t.typcod == event_code) != null)
            {
                return this.leave_cause.Find(t => t.typcod == event_code).typdes_th;
            }
            else
            {
                return "";
            }
        }

        private void ShowInlineForm()
        {

            int row_index = this.dgv.CurrentCell.RowIndex;

            CustomComboBox inline_users_name = new CustomComboBox();
            inline_users_name.Name = "inline_users_name";
            inline_users_name.comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            inline_users_name.Read_Only = false;
            inline_users_name.BorderStyle = BorderStyle.None;
            foreach (Users u in this.users_list)
            {
                ComboboxItem item = new ComboboxItem(u.username + " : " + u.name, u.id, u.username);
                inline_users_name.AddItem(item);
                if (this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if (u.username == ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).users_name)
                    {
                        inline_users_name.comboBox1.SelectedItem = item;
                    }
                }
            }
            inline_users_name.Leave += delegate
            {
                if (inline_users_name.comboBox1.Items.Cast<ComboboxItem>().Where(t => t.name.Length >= inline_users_name.comboBox1.Text.Length).Where(t => t.name.Substring(0, inline_users_name.comboBox1.Text.Length) == inline_users_name.comboBox1.Text).Count() > 0)
                {
                    inline_users_name.comboBox1.SelectedItem = inline_users_name.comboBox1.Items.Cast<ComboboxItem>().Where(t => t.name.Length >= inline_users_name.comboBox1.Text.Length).Where(t => t.name.Substring(0, inline_users_name.comboBox1.Text.Length) == inline_users_name.comboBox1.Text).First();
                }
                else
                {
                    inline_users_name.comboBox1.Focus();
                    SendKeys.Send("{F6}");
                }
            };
            inline_users_name.comboBox1.SelectedIndex = (this.form_mode == FORM_MODE.EDIT_ITEM ? this.users_list.FindIndex(t => t.username == ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).users_name) : 0);
            this.dgv.Parent.Controls.Add(inline_users_name);

            CustomComboBox inline_leave_cause = new CustomComboBox();
            inline_leave_cause.Name = "inline_leave_cause";
            inline_leave_cause.Read_Only = false;
            inline_leave_cause.BorderStyle = BorderStyle.None;
            foreach (Istab i in this.leave_cause)
            {
                ComboboxItem item = new ComboboxItem(i.typdes_th, i.id, i.typcod);
                item.Tag = i;
                inline_leave_cause.AddItem(item);
                if (this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if (i.tabtyp == ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).event_type && i.typcod == ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).event_code)
                    {
                        inline_leave_cause.comboBox1.SelectedItem = item;
                    }
                }
            }
            inline_leave_cause.comboBox1.SelectedIndex = (this.form_mode == FORM_MODE.EDIT_ITEM ? this.leave_cause.FindIndex(t => t.typcod == ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).event_code) : 0);
            this.dgv.Parent.Controls.Add(inline_leave_cause);
            

            CustomTimePicker inline_from_time = new CustomTimePicker();
            inline_from_time.Name = "inline_from_time";
            inline_from_time.Read_Only = false;
            inline_from_time.BorderStyle = BorderStyle.None;
            inline_from_time.Show_Second = false;
            this.dgv.Parent.Controls.Add(inline_from_time);
            inline_from_time.Time = (this.form_mode == FORM_MODE.EDIT_ITEM ? ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).from_time.TimeString2DateTime() : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 30, 0));

            CustomTimePicker inline_to_time = new CustomTimePicker();
            inline_to_time.Name = "inline_to_time";
            inline_to_time.Read_Only = false;
            inline_to_time.BorderStyle = BorderStyle.None;
            inline_to_time.Show_Second = false;
            this.dgv.Parent.Controls.Add(inline_to_time);
            inline_to_time.Time = (this.form_mode == FORM_MODE.EDIT_ITEM ? ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).to_time.TimeString2DateTime() : (this.cde.date.Value.GetDayIntOfWeek() == 7 ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0)));

            CustomComboBox inline_status = new CustomComboBox();
            inline_status.Name = "inline_status";
            inline_status.Read_Only = false;
            inline_status.BorderStyle = BorderStyle.None;
            inline_status.AddItem(new ComboboxItem("Wait", 0, "Wait"));
            inline_status.AddItem(new ComboboxItem("Confirmed", 1, "Confirmed"));
            inline_status.AddItem(new ComboboxItem("Canceled", 2, "Canceled"));
            this.dgv.Parent.Controls.Add(inline_status);
            inline_status.comboBox1.SelectedIndex = (this.form_mode == FORM_MODE.EDIT_ITEM ? ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).status : 1);
            
            CustomTextBox inline_customer = new CustomTextBox();
            inline_customer.Name = "inline_customer";
            inline_customer.Read_Only = false;
            inline_customer.MaxChar = 40;
            inline_customer.BorderStyle = BorderStyle.None;
            this.dgv.Parent.Controls.Add(inline_customer);
            inline_customer.Texts = (this.form_mode == FORM_MODE.EDIT_ITEM ? ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).customer : "");

            CustomComboBox inline_medcert = new CustomComboBox();
            inline_medcert.Name = "inline_medcert";
            inline_medcert.Read_Only = false;
            inline_medcert.BorderStyle = BorderStyle.None;
            inline_medcert.AddItem(new ComboboxItem("N/A (ไม่ระบุ)", 9, "X"));
            inline_medcert.AddItem(new ComboboxItem("ไม่มีเอกสาร", 0, "N"));
            inline_medcert.AddItem(new ComboboxItem("มีใบรับรองแพทย์", 1, "Y"));
            this.dgv.Parent.Controls.Add(inline_medcert);
            inline_medcert.comboBox1.SelectedItem = (this.form_mode == FORM_MODE.EDIT_ITEM ? inline_medcert.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.string_value == ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).med_cert).First<ComboboxItem>() : (ComboboxItem)inline_medcert.comboBox1.Items[0]);

            NumericUpDown inline_fine = new NumericUpDown();
            inline_fine.Name = "inline_fine";
            inline_fine.Maximum = 1000;
            inline_fine.Minimum = 0;
            inline_fine.AutoSize = false;
            inline_fine.Font = new Font("tahoma", 9.75f);
            inline_fine.ThousandsSeparator = true;
            inline_fine.BorderStyle = BorderStyle.None;
            inline_fine.TextAlign = HorizontalAlignment.Right;
            this.dgv.Parent.Controls.Add(inline_fine);
            inline_fine.Value = (this.form_mode == FORM_MODE.EDIT_ITEM ? ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).fine : 0);

            this.SetInlineFormPosition();
            this.dgv.SendToBack();
            this.dgv.Enabled = false;
            inline_users_name.BringToFront();
            inline_leave_cause.BringToFront();
            inline_from_time.BringToFront();
            inline_to_time.BringToFront();
            inline_status.BringToFront();
            inline_customer.BringToFront();
            inline_medcert.BringToFront();
            inline_fine.BringToFront();
        }

        private void ClearInlineForm()
        {
            if (this.dgv.Parent.Controls.Find("inline_users_name", true).Length > 0)
            {
                this.dgv.Parent.Controls.RemoveByKey("inline_users_name");
            }
            if (this.dgv.Parent.Controls.Find("inline_leave_cause", true).Length > 0)
            {
                this.dgv.Parent.Controls.RemoveByKey("inline_leave_cause");
            }
            if (this.dgv.Parent.Controls.Find("inline_from_time", true).Length > 0)
            {
                this.dgv.Parent.Controls.RemoveByKey("inline_from_time");
            }
            if (this.dgv.Parent.Controls.Find("inline_to_time", true).Length > 0)
            {
                this.dgv.Parent.Controls.RemoveByKey("inline_to_time");
            }
            if (this.dgv.Parent.Controls.Find("inline_status", true).Length > 0)
            {
                this.dgv.Parent.Controls.RemoveByKey("inline_status");
            }
            if (this.dgv.Parent.Controls.Find("inline_customer", true).Length > 0)
            {
                this.dgv.Parent.Controls.RemoveByKey("inline_customer");
            }
            if (this.dgv.Parent.Controls.Find("inline_medcert", true).Length > 0)
            {
                this.dgv.Parent.Controls.RemoveByKey("inline_medcert");
            }
            if (this.dgv.Parent.Controls.Find("inline_fine", true).Length > 0)
            {
                this.dgv.Parent.Controls.RemoveByKey("inline_fine");
            }
        }

        private void SetInlineFormPosition()
        {
            if (this.dgv.CurrentCell != null)
            {
                if (this.dgv.Parent.Controls.Find("inline_users_name", true).Length > 0)
                {
                    Rectangle rect_users_name = this.dgv.GetCellDisplayRectangle(2, this.dgv.CurrentCell.RowIndex, true);
                    CustomComboBox inline_users_name = (CustomComboBox)this.dgv.Parent.Controls.Find("inline_users_name", true)[0];
                    inline_users_name.SetBounds(rect_users_name.X + 2, rect_users_name.Y + 1, rect_users_name.Width - 1, rect_users_name.Height - 3);
                }

                if (this.dgv.Parent.Controls.Find("inline_leave_cause", true).Length > 0)
                {
                    Rectangle rect_leave_cause = this.dgv.GetCellDisplayRectangle(3, this.dgv.CurrentCell.RowIndex, true);
                    CustomComboBox inline_leave_cause = (CustomComboBox)this.dgv.Parent.Controls.Find("inline_leave_cause", true)[0];
                    inline_leave_cause.SetBounds(rect_leave_cause.X + 2, rect_leave_cause.Y + 1, rect_leave_cause.Width - 1, rect_leave_cause.Height - 3);
                }

                if (this.dgv.Parent.Controls.Find("inline_from_time", true).Length > 0)
                {
                    Rectangle rect_from_time = this.dgv.GetCellDisplayRectangle(4, this.dgv.CurrentCell.RowIndex, true);
                    CustomTimePicker inline_from_time = (CustomTimePicker)this.dgv.Parent.Controls.Find("inline_from_time", true)[0];
                    inline_from_time.SetBounds(rect_from_time.X + 2, rect_from_time.Y + 1, rect_from_time.Width - 1, rect_from_time.Height - 3);
                }

                if (this.dgv.Parent.Controls.Find("inline_to_time", true).Length > 0)
                {
                    Rectangle rect_to_time = this.dgv.GetCellDisplayRectangle(5, this.dgv.CurrentCell.RowIndex, true);
                    CustomTimePicker inline_to_time = (CustomTimePicker)this.dgv.Parent.Controls.Find("inline_to_time", true)[0];
                    inline_to_time.SetBounds(rect_to_time.X + 2, rect_to_time.Y + 1, rect_to_time.Width - 1, rect_to_time.Height - 3);
                }

                if (this.dgv.Parent.Controls.Find("inline_status", true).Length > 0)
                {
                    Rectangle rect_status = this.dgv.GetCellDisplayRectangle(6, this.dgv.CurrentCell.RowIndex, true);
                    CustomComboBox inline_status = (CustomComboBox)this.dgv.Parent.Controls.Find("inline_status", true)[0];
                    inline_status.SetBounds(rect_status.X + 2, rect_status.Y + 1, rect_status.Width - 1, rect_status.Height - 3);
                }

                if (this.dgv.Parent.Controls.Find("inline_customer", true).Length > 0)
                {
                    Rectangle rect_customer = this.dgv.GetCellDisplayRectangle(7, this.dgv.CurrentCell.RowIndex, true);
                    CustomTextBox inline_customer = (CustomTextBox)this.dgv.Parent.Controls.Find("inline_customer", true)[0];
                    inline_customer.SetBounds(rect_customer.X + 2, rect_customer.Y + 1, rect_customer.Width - 1, rect_customer.Height - 3);
                }

                if (this.dgv.Parent.Controls.Find("inline_medcert", true).Length > 0)
                {
                    Rectangle rect_medcert = this.dgv.GetCellDisplayRectangle(8, this.dgv.CurrentCell.RowIndex, true);
                    CustomComboBox inline_medcert = (CustomComboBox)this.dgv.Parent.Controls.Find("inline_medcert", true)[0];
                    inline_medcert.SetBounds(rect_medcert.X + 2, rect_medcert.Y + 1, rect_medcert.Width - 1, rect_medcert.Height - 3);
                }

                if (this.dgv.Parent.Controls.Find("inline_fine", true).Length > 0)
                {
                    Rectangle rect_fine = this.dgv.GetCellDisplayRectangle(9, this.dgv.CurrentCell.RowIndex, true);
                    //CustomMaskedTextBox inline_fine = (CustomMaskedTextBox)this.dgv.Parent.Controls.Find("inline_fine", true)[0];
                    NumericUpDown inline_fine = (NumericUpDown)this.dgv.Parent.Controls.Find("inline_fine", true)[0];
                    inline_fine.SetBounds(rect_fine.X + 2, rect_fine.Y + 1, rect_fine.Width - 1, rect_fine.Height - 3);
                }
            }
        }

        private void DeleteItem()
        {
            if (this.dgv.CurrentCell != null && this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag is EventCalendar)
            {
                this.dgv.Tag = DGV_TAG.DELETE;

                if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                {
                    EventCalendar ev = (EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag;
                    bool delete_success = false;

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += delegate
                    {
                        CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "eventcalendar/delete&id=" + ev.id.ToString());
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            delete_success = true;
                        }
                        else
                        {
                            delete_success = false;
                        }
                    };
                    worker.RunWorkerCompleted += delegate
                    {
                        if (delete_success)
                        {
                            this.dgv.Tag = DGV_TAG.NORMAL;
                            this.cde.RefreshData();
                            this.cde.RefreshView();
                            this.FillDataGrid();
                            this.FormReadItem();
                            this.dgv.Rows[0].Cells[1].Selected = true;
                        }
                        else
                        {
                            MessageAlert.Show("เกิดข้อผิดพลาด, กรุณาลองใหม่อีกครั้งในภายหลัง", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                            this.FillDataGrid((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag);
                        }
                    };
                    worker.RunWorkerAsync();
                }
                else
                {
                    this.FillDataGrid((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag);
                }
            }
        }

        private void FormRead()
        {
            this.form_mode = FORM_MODE.READ;
            this.toolStripProcessing.Visible = false;
            this.dgv.Enabled = true;
            this.txtDummy.Focus();

            #region Toolstrip button
            this.toolStripEdit.Enabled = true;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripItem.Enabled = true;
            #endregion Toolstrip button

            #region Form control
            this.rbHoliday.Enabled = false;
            this.rbWeekday.Enabled = false;
            this.leaveMax.Enabled = false;
            #endregion Form control
        }

        private void FormEdit()
        {
            this.form_mode = FORM_MODE.EDIT;
            this.toolStripProcessing.Visible = false;
            this.dgv.Enabled = false;

            #region Toolstrip button
            this.toolStripEdit.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripItem.Enabled = false;
            #endregion Toolstrip button

            #region Form control
            this.rbHoliday.Enabled = true;
            this.rbWeekday.Enabled = true;
            this.leaveMax.Enabled = (this.rbWeekday.Checked ? true : false);
            #endregion Form control
        }

        private void FormReadItem()
        {
            this.form_mode = FORM_MODE.READ_ITEM;
            this.toolStripProcessing.Visible = false;
            this.dgv.Enabled = true;
            this.dgv.Focus();

            #region Toolstrip button
            this.toolStripEdit.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripItem.Enabled = false;
            #endregion Toolstrip button

            #region Form control
            this.rbHoliday.Enabled = false;
            this.rbWeekday.Enabled = false;
            this.leaveMax.Enabled = false;
            #endregion Form control

            this.ClearInlineForm();
        }

        private void FormAddItem()
        {
            this.form_mode = FORM_MODE.ADD_ITEM;
            this.toolStripProcessing.Visible = false;

            #region Toolstrip button
            this.toolStripEdit.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripItem.Enabled = false;
            #endregion Toolstrip button

            #region Form control
            this.rbHoliday.Enabled = false;
            this.rbWeekday.Enabled = false;
            this.leaveMax.Enabled = false;
            #endregion Form control
        }

        private void FormEditItem()
        {
            this.form_mode = FORM_MODE.EDIT_ITEM;
            this.toolStripProcessing.Visible = false;

            #region Toolstrip button
            this.toolStripEdit.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripItem.Enabled = false;
            #endregion Toolstrip button

            #region Form control
            this.rbHoliday.Enabled = false;
            this.rbWeekday.Enabled = false;
            this.leaveMax.Enabled = false;
            #endregion Form control
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;
            this.toolStripProcessing.Visible = true;
            this.txtDummy.Focus();

            #region Toolstrip button
            this.toolStripEdit.Enabled = false;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripItem.Enabled = false;
            #endregion Toolstrip button

            #region Form control
            this.rbHoliday.Enabled = false;
            this.rbWeekday.Enabled = false;
            this.leaveMax.Enabled = false;
            #endregion Form control

            #region Set inline control to read-only state
            if (this.dgv.Parent.Controls.Find("inline_users_name", true).Length > 0)
            {
                ((CustomComboBox)this.dgv.Parent.Controls.Find("inline_users_name", true)[0]).Read_Only = true;
            }
            if (this.dgv.Parent.Controls.Find("inline_from_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgv.Parent.Controls.Find("inline_from_time", true)[0]).Read_Only = true;
            }
            if (this.dgv.Parent.Controls.Find("inline_to_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgv.Parent.Controls.Find("inline_to_time", true)[0]).Read_Only = true;
            }
            if (this.dgv.Parent.Controls.Find("inline_leave_cause", true).Length > 0)
            {
                ((CustomComboBox)this.dgv.Parent.Controls.Find("inline_leave_cause", true)[0]).Read_Only = true;
            }
            if (this.dgv.Parent.Controls.Find("inline_status", true).Length > 0)
            {
                ((CustomComboBox)this.dgv.Parent.Controls.Find("inline_status", true)[0]).Read_Only = true;
            }
            if (this.dgv.Parent.Controls.Find("inline_customer", true).Length > 0)
            {
                ((CustomTextBox)this.dgv.Parent.Controls.Find("inline_customer", true)[0]).Read_Only = true;
            }
            if (this.dgv.Parent.Controls.Find("inline_medcert", true).Length > 0)
            {
                ((CustomComboBox)this.dgv.Parent.Controls.Find("inline_medcert", true)[0]).Read_Only = true;
            }
            if (this.dgv.Parent.Controls.Find("inline_fine", true).Length > 0)
            {
                //((CustomMaskedTextBox)this.dgv.Parent.Controls.Find("inline_fine", true)[0]).Read_Only = true;
                ((NumericUpDown)this.dgv.Parent.Controls.Find("inline_fine", true)[0]).ReadOnly = true;
            }
            #endregion Set inline control to read-only state
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.FormEdit();
            if (this.rbHoliday.Checked)
            {
                this.txtHoliday.Focus();
            }
            if (this.rbWeekday.Checked)
            {
                this.cbGroupWeekend.comboBox1.Focus();
            }
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.EDIT)
            {
                this.FormRead();
                this.InitControl();
            }
            else if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.ClearInlineForm();
                this.FormReadItem();
            }
            else if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.FormRead();
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            #region Note calendar (main data)
            if (this.form_mode == FORM_MODE.EDIT)
            {
                bool post_success = false;

                int type = (this.rbHoliday.Checked ? (int)CustomDateEvent.NOTE_TYPE.HOLIDAY : (int)CustomDateEvent.NOTE_TYPE.NOTE);
                string description = (type == (int)CustomDateEvent.NOTE_TYPE.HOLIDAY ? this.txtHoliday.Texts : "");
                this.FormProcessing();

                string json_data = "{\"date\":\"" + this.cde.date.Value.ToMysqlDate() + "\",";
                json_data += "\"type\":\"" + type.ToString() + "\",";
                json_data += "\"description\":\"" + description + "\",";
                json_data += "\"group_maid\":\"" + ((ComboboxItem)this.cbGroupMaid.comboBox1.SelectedItem).string_value + "\",";
                json_data += "\"group_weekend\":\"" + ((ComboboxItem)this.cbGroupWeekend.comboBox1.SelectedItem).string_value + "\",";
                json_data += "\"max_leave\":" + this.leaveMax.Value.ToString() + ",";
                json_data += "\"rec_by\":\"" + this.cde.main_form.G.loged_in_user_name + "\"}";

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "notecalendar/create_or_update", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        post_success = true;
                    }
                    else
                    {
                        post_success = false;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (post_success)
                    {
                        this.cde.RefreshData();
                        this.cde.RefreshView();
                        this.InitControl();
                        this.FormRead();
                    }
                    else
                    {
                        MessageAlert.Show("เกิดข้อผิดพลาด, กรุณาลองใหม่", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                worker.RunWorkerAsync();
            }

            #endregion Note calendar (main data)

            #region Add item
            if (this.form_mode == FORM_MODE.ADD_ITEM)
            {
                bool post_success = false;
                string err_msg = "";
                int inserted_id = -1;

                EventCalendar ev = this.GetInlineEvent();

                this.FormProcessing();

                string json_data = "{\"users_name\":\"" + ev.users_name + "\",";
                json_data += "\"date\":\"" + ev.date + "\",";
                json_data += "\"from_time\":\"" + ev.from_time + "\",";
                json_data += "\"to_time\":\"" + ev.to_time + "\",";
                json_data += "\"event_type\":\"" + ev.event_type + "\",";
                json_data += "\"event_code\":\"" + ev.event_code + "\",";
                json_data += "\"customer\":\"" + ev.customer + "\",";
                json_data += "\"status\":\"" + ev.status.ToString() + "\",";
                json_data += "\"med_cert\":\"" + ev.med_cert + "\",";
                json_data += "\"fine\":" + ev.fine.ToString() + ",";
                json_data += "\"rec_by\":\"" + ev.rec_by + "\"}";

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
                        this.cde.RefreshData();
                        this.cde.RefreshView();
                        this.FillDataGrid();
                        this.dgv.Rows[this.cde.absent_list.ExtractToEventCalendar().FindIndex(t => t.id == inserted_id)].Cells[1].Selected = true;
                        this.FormReadItem();
                    }
                    else
                    {
                        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                worker.RunWorkerAsync();
            }
            #endregion Add item

            #region Edit item
            if (this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                bool post_success = false;
                string err_msg = "";
                int edited_id = -1;
                
                EventCalendar ev = this.GetInlineEvent();
                this.FormProcessing();

                string json_data = "{\"id\":" + ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).id.ToString() + ",";
                json_data += "\"users_name\":\"" + ev.users_name + "\",";
                json_data += "\"date\":\"" + ev.date + "\",";
                json_data += "\"from_time\":\"" + ev.from_time + "\",";
                json_data += "\"to_time\":\"" + ev.to_time + "\",";
                json_data += "\"event_type\":\"" + ev.event_type + "\",";
                json_data += "\"event_code\":\"" + ev.event_code + "\",";
                json_data += "\"customer\":\"" + ev.customer + "\",";
                json_data += "\"status\":\"" + ev.status.ToString() + "\",";
                json_data += "\"med_cert\":\"" + ev.med_cert + "\",";
                json_data += "\"fine\":" + ev.fine.ToString() + ",";
                json_data += "\"rec_by\":\"" + ev.rec_by + "\"}";

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "eventcalendar/update", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        post_success = true;
                        edited_id = Convert.ToInt32(sr.message);
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
                        this.cde.RefreshData();
                        this.cde.RefreshView();
                        this.FillDataGrid();
                        if (edited_id > -1)
                        {
                            this.dgv.Rows[this.cde.absent_list.ExtractToEventCalendar().FindIndex(t => t.id == edited_id)].Cells[1].Selected = true;
                        }
                        else
                        {
                            this.dgv.Rows[0].Cells[1].Selected = true;
                        }
                        this.FormReadItem();
                    }
                    else
                    {
                        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                worker.RunWorkerAsync();
            }
            #endregion Edit item
        }

        private void toolStripItem_Click(object sender, EventArgs e)
        {
            this.FormReadItem();
            this.dgv.Rows[0].Cells[1].Selected = true;
        }

        private EventCalendar GetInlineEvent()
        {
            EventCalendar ev = new EventCalendar();
            if(this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag is EventCalendar)
            {
                ev.id = ((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag).id;
            }

            ev.date = this.cde.date.Value.ToMysqlDate();
            if (this.dgv.Parent.Controls.Find("inline_users_name", true).Length > 0)
            {
                ev.users_name = ((ComboboxItem)((CustomComboBox)this.dgv.Parent.Controls.Find("inline_users_name", true)[0]).comboBox1.SelectedItem).string_value;
            }
            if (this.dgv.Parent.Controls.Find("inline_from_time", true).Length > 0)
            {
                CustomTimePicker inline_from_time = (CustomTimePicker)this.dgv.Parent.Controls.Find("inline_from_time", true)[0];
                ev.from_time = inline_from_time.Time.ToString("HH:mm", cinfo_th);
            }
            if (this.dgv.Parent.Controls.Find("inline_to_time", true).Length > 0)
            {
                CustomTimePicker inline_to_time = (CustomTimePicker)this.dgv.Parent.Controls.Find("inline_to_time", true)[0];
                ev.to_time = inline_to_time.Time.ToString("HH:mm", cinfo_th);
            }
            if (this.dgv.Parent.Controls.Find("inline_leave_cause", true).Length > 0)
            {
                ev.event_type = ((Istab)((ComboboxItem)((CustomComboBox)this.dgv.Parent.Controls.Find("inline_leave_cause", true)[0]).comboBox1.SelectedItem).Tag).tabtyp;
                ev.event_code = ((Istab)((ComboboxItem)((CustomComboBox)this.dgv.Parent.Controls.Find("inline_leave_cause", true)[0]).comboBox1.SelectedItem).Tag).typcod;
            }
            if (this.dgv.Parent.Controls.Find("inline_status", true).Length > 0)
            {
                ev.status = ((ComboboxItem)((CustomComboBox)this.dgv.Parent.Controls.Find("inline_status", true)[0]).comboBox1.SelectedItem).int_value;
            }
            if (this.dgv.Parent.Controls.Find("inline_customer", true).Length > 0)
            {
                CustomTextBox inline_customer = (CustomTextBox)this.dgv.Parent.Controls.Find("inline_customer", true)[0];
                ev.customer = inline_customer.Texts.cleanString();
            }
            if (this.dgv.Parent.Controls.Find("inline_medcert", true).Length > 0)
            {
                CustomComboBox inline_medcert = (CustomComboBox)this.dgv.Parent.Controls.Find("inline_medcert", true)[0];
                ev.med_cert = ((ComboboxItem)inline_medcert.comboBox1.SelectedItem).string_value;
            }
            if (this.dgv.Parent.Controls.Find("inline_medcert", true).Length > 0)
            {
                NumericUpDown inline_fine = (NumericUpDown)this.dgv.Parent.Controls.Find("inline_fine", true)[0];
                ev.fine = Convert.ToInt32(inline_fine.Value);
            }
            ev.rec_by = this.cde.main_form.G.loged_in_user_name;

            return ev;
        }

        private string GetLeaveStatusString(int status)
        {
            switch (status)
            {
                case (int)LEAVE_STATUS.WAIT:
                    return "Wait";
                case (int)LEAVE_STATUS.CONFIRMED:
                    return "Confirmed";
                case (int)LEAVE_STATUS.CANCELED:
                    return "Canceled";
                default:
                    return "";
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    //if (this.dgv.Parent.Controls.Find("inline_customer", true).Length > 0)
                    //{
                        //CustomTextBox inline_customer = (CustomTextBox)this.dgv.Parent.Controls.Find("inline_customer", true)[0];
                        //if (inline_customer.textBox1.Focused)
                        //{
                        //    DateEventSubWindow subwind = (this.form_mode == FORM_MODE.ADD_ITEM ? new DateEventSubWindow() : new DateEventSubWindow((EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag));
                        //    if (subwind.ShowDialog() == DialogResult.OK)
                        //    {
                        //        this.toolStripSave.PerformClick();
                        //    }
                        //    else
                        //    {
                        //        inline_customer.textBox1.Focus();
                        //    }
                        //    return true;
                        //}
                    //}
                    if (this.dgv.Parent.Controls.Find("inline_fine", true).Length > 0)
                    {
                        NumericUpDown inline_fine = (NumericUpDown)this.dgv.Parent.Controls.Find("inline_fine", true)[0];
                        if (inline_fine.Focused)
                        {
                            this.toolStripSave.PerformClick();
                            return true;
                        }
                    }
                }
                if (this.form_mode == FORM_MODE.EDIT)
                {
                    if (this.rbHoliday.Checked && this.txtHoliday.textBox1.Focused)
                    {
                        this.toolStripSave.PerformClick();
                        return true;
                    }
                    else if (this.rbWeekday.Checked && this.leaveMax.Focused)
                    {
                        this.toolStripSave.PerformClick();
                        return true;
                    }
                }
                if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM)
                {
                    return true;
                }
                SendKeys.Send("{TAB}");
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.toolStripStop.PerformClick();
                return true;
            }
            if (keyData == Keys.F8)
            {
                this.toolStripItem.PerformClick();
                return true;
            }
            if (keyData == Keys.F9)
            {
                this.toolStripSave.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.E))
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.toolStripEdit.PerformClick();
                    return true;
                }
                else if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    if (this.dgv.CurrentCell != null)
                    {
                        if (this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag is EventCalendar)
                        {
                            this.FormEditItem();
                            this.ShowInlineForm();
                            return true;
                        }
                    }
                }
            }
            if (keyData == (Keys.Alt | Keys.A))
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.dgv.Rows[this.cde.absent_list.ExtractToEventCalendar().Count].Cells[1].Selected = true;
                    this.FormAddItem();
                    this.ShowInlineForm();
                    return true;
                }
            }
            if (keyData == (Keys.Alt | Keys.D))
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.DeleteItem();
                }
            }

            if (keyData == (Keys.Alt | Keys.C))
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    if (this.dgv.CurrentCell == null)
                        return true;

                    if (this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag is EventCalendar)
                    {
                        DateSelectorDialog ds = new DateSelectorDialog(this.cde.date.Value);
                        if (ds.ShowDialog() == DialogResult.OK)
                        {
                            this.DoCopy(ds.selected_date, (EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag);
                        }
                    }

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
