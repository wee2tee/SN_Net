using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class TrainingExpertWindow : Form
    {
        private DateTime current_event_date;
        private CustomDateEvent date_event;
        private CustomDateEvent2 date_event2;
        private List<Users> list_trainer_all;
        private List<Users> list_trainer_selected;
        private List<Users> list_trainer_rest;
        private List<TrainingCalendar> list_training_calendar;
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            READ,
            EDIT,
            READ_F8,
            READ_F7,
            ADD_F8,
            EDIT_F8,
            PROCESSING
        }
        private CustomComboBox inline_course_type;
        private CustomComboBox inline_trainer;
        private CustomComboBox inline_status;
        private CustomComboBox inline_term;
        private CustomTextBox inline_remark;
        private Control current_focused_control;

        public enum COURSE_TYPE : int
        {
            BASIC = 1, // คอร์สพื้นฐาน
            ADVANCED = 2 // คอร์สแอดวานซ์
        }

        public enum TRAINER_STATUS : int
        {
            TRAINER = 1, // วิทยากร
            ASSIST = 2 // ผู้ช่วย
        }

        public enum TRAINING_TERM : int
        {
            AM = 1, // อบรมช่วงเช้า
            PM = 2 // อบรมช่วงบ่าย
        }

        public TrainingExpertWindow(CustomDateEvent date_event)
        {
            InitializeComponent();
            this.date_event = date_event;
            this.current_event_date = date_event.Date;
        }

        public TrainingExpertWindow(CustomDateEvent2 date_event)
        {
            InitializeComponent();
            this.date_event2 = date_event;
            this.current_event_date = date_event.date.Value;
        }

        private void TrainingExpertWindow_Load(object sender, EventArgs e)
        {
            this.LoadDependenciesData();
            this.BindingControlEventHandler();
            this.InitControl();
        }

        private void TrainingExpertWindow_Shown(object sender, EventArgs e)
        {
            this.FillForm();
            this.FormRead();
        }

        private void LoadDependenciesData()
        {
            this.list_trainer_all = this.GetTrainerAll();
            this.list_trainer_selected = this.GetTrainerSelected();
            this.list_trainer_rest = this.GetTrainerRest();
            this.list_training_calendar = this.GetTrainingCalendar();
        }

        private void BindingControlEventHandler()
        {
            #region DataGrid
            this.dgvTrainer.DrawDgvRowBorder();
            this.dgvStat.DrawDgvRowBorder();

            this.dgvTrainer.Resize += delegate
            {
                if (this.dgvTrainer.CurrentCell != null)
                {
                    this.dgvTrainer.FillLine(this.list_training_calendar.Where(t => t.date == this.current_event_date.ToMysqlDate()).ToList<TrainingCalendar>().Count);
                    this.SetInlineFormPosition();
                }
            };

            this.dgvStat.Resize += delegate
            {
                if (this.dgvStat.CurrentCell != null)
                {
                    this.dgvStat.FillLine(this.list_trainer_all.Count);
                }
            };

            this.dgvTrainer.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex > -1)
                {
                    if (this.dgvTrainer.Rows[e.RowIndex].Tag is TrainingCalendar)
                    {
                        this.ShowInlineForm(FORM_MODE.EDIT_F8);
                    }
                    else
                    {
                        this.ShowInlineForm(FORM_MODE.ADD_F8);
                    }
                }
            };

            this.dgvTrainer.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    int row_index = this.dgvTrainer.HitTest(e.X, e.Y).RowIndex;
                    if (row_index > -1)
                    {
                        this.FormReadF8();
                        this.dgvTrainer.Rows[row_index].Cells[0].Selected = true;

                        ContextMenu c = new ContextMenu();
                        MenuItem m_add = new MenuItem("เพิ่ม <Alt+A>");
                        m_add.Click += delegate
                        {
                            this.ShowInlineForm(FORM_MODE.ADD_F8);
                        };
                        c.MenuItems.Add(m_add);

                        MenuItem m_edit = new MenuItem("แก้ไข <Alt+E>");
                        m_edit.Enabled = (this.dgvTrainer.Rows[row_index].Tag is TrainingCalendar ? true : false);
                        m_edit.Click += delegate
                        {
                            this.ShowInlineForm(FORM_MODE.EDIT_F8);
                        };
                        c.MenuItems.Add(m_edit);

                        MenuItem m_delete = new MenuItem("ลบ <Alt+D>");
                        m_delete.Enabled = (this.dgvTrainer.Rows[row_index].Tag is TrainingCalendar ? true : false);
                        m_delete.Click += delegate
                        {
                            if (this.dgvTrainer.CurrentCell == null)
                                return;

                            if (!(this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].Tag is TrainingCalendar))
                                return;

                            this.dgvTrainer.Tag = HelperClass.DGV_TAG.DELETE;
                            if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                            {
                                this.DeleteItemF8();
                            }
                            else
                            {
                                this.dgvTrainer.Tag = HelperClass.DGV_TAG.READ;
                                this.dgvTrainer.Refresh();
                            }
                        };
                        c.MenuItems.Add(m_delete);

                        c.Show(this.dgvTrainer, new Point(e.X, e.Y));
                    }
                }
            };

            //this.dgvStat.MouseClick += delegate(object sender, MouseEventArgs e)
            //{
            //    if (e.Button == MouseButtons.Right)
            //    {
            //        int row_index = this.dgvStat.HitTest(e.X, e.Y).RowIndex;
            //        if (row_index > -1)
            //        {
            //            this.FormReadF7();
            //            this.dgvStat.Rows[row_index].Cells[0].Selected = true;

            //            if (!(this.dgvStat.Rows[row_index].Tag is Users))
            //                return;

            //            ContextMenu c = new ContextMenu();
            //            MenuItem m_trainer = new MenuItem("เพิ่มเป็นวิทยากรของวันนี้");
            //            m_trainer.Enabled = (this.dgvStat.Rows[row_index].Tag is Users ? true : false);
            //            MenuItem m_trainer_am = new MenuItem("ช่วงเช้า");
            //            m_trainer_am.Click += delegate
            //            {
            //                string json_data = "";

            //                BackgroundWorker worker = new BackgroundWorker();
            //                worker.DoWork += delegate
            //                {

            //                };
            //                worker.RunWorkerCompleted += delegate
            //                {

            //                };
            //                worker.RunWorkerAsync();
            //            };
            //            MenuItem m_trainer_pm = new MenuItem("ช่วงบ่าย");
            //            m_trainer_pm.Click += delegate
            //            {

            //            };
            //            m_trainer.MenuItems.Add(m_trainer_am);
            //            m_trainer.MenuItems.Add(m_trainer_pm);
            //            c.MenuItems.Add(m_trainer);

            //            MenuItem m_assist = new MenuItem("เพิ่มเป็นผู้ช่วยฯของวันนี้");
            //            m_assist.Enabled = (this.dgvStat.Rows[row_index].Tag is Users ? true : false);
            //            MenuItem m_assist_am = new MenuItem("ช่วงเช้า");
            //            m_assist_am.Click += delegate
            //            {

            //            };
            //            MenuItem m_assist_pm = new MenuItem("ช่วงบ่าย");
            //            m_assist_pm.Click += delegate
            //            {

            //            };
            //            m_assist.MenuItems.Add(m_assist_am);
            //            m_assist.MenuItems.Add(m_assist_pm);
            //            c.MenuItems.Add(m_assist);
                        
            //            c.Show(this.dgvStat, new Point(e.X, e.Y));
            //        }
            //    }
            //};
            #endregion DataGrid

            this.tabControl1.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if(this.current_focused_control != null)
                    this.current_focused_control.Focus();
            };

            this.tabControl1.Deselecting += delegate(object sender, TabControlCancelEventArgs e)
            {
                if (!(this.form_mode == FORM_MODE.READ))
                {
                    e.Cancel = true;
                }
            };
        }

        private void InlineControlGotFocus(object sender, EventArgs e)
        {
            if ((Control)sender != null)
            {
                if (sender is ComboBox)
                {
                    //((ComboBox)sender).DroppedDown = true;
                    if (((ComboBox)sender).SelectedItem == null)
                        SendKeys.Send("{F6}");
                }
                this.current_focused_control = (Control)sender;
            }
        }

        private void InitControl()
        {
            this.dtDate.Value = this.current_event_date;
        }

        private List<Users> GetTrainerAll()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_trainer");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return sr.users;
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return null;
            }
        }

        private List<Users> GetTrainerSelected()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "trainingcalendar/get_trainer&date_from=" + this.current_event_date.ToMysqlDate() + "&date_to=" + this.current_event_date.ToMysqlDate());
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return sr.users;
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return null;
            }
        }

        private List<Users> GetTrainerRest()
        {
            List<Users> rest = this.list_trainer_all.ConvertAll(t => t).ToList<Users>();
            foreach (Users u in this.list_trainer_selected)
            {
                if (rest.Where(r => r.id == u.id).Count<Users>() > 0)
                {
                    rest.Remove(rest.Find(r => r.id == u.id));
                }
            }

            return rest;
        }

        private List<TrainingCalendar> GetTrainingCalendar()
        {
            DateTime first_day_of_month = new DateTime(this.current_event_date.Year, this.current_event_date.Month, 1);
            DateTime last_day_of_month = first_day_of_month.AddMonths(1).AddDays(-1);
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "trainingcalendar/get_month_trainer&date_from=" + first_day_of_month.ToMysqlDate() + "&date_to=" + last_day_of_month.ToMysqlDate());
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return sr.training_calendar;
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return null;
            }
        }

        public static string GetTrainerStatusString(int status)
        {
            switch (status)
            {
                case (int)TRAINER_STATUS.TRAINER:
                    return "วิทยากร";
                case (int)TRAINER_STATUS.ASSIST:
                    return "ผู้ช่วย";
                default:
                    return "";
            }
        }

        public static string GetTrainingCourseTypeString(int course_type)
        {
            switch (course_type)
            {
                case (int)COURSE_TYPE.BASIC:
                    return "Basic";
                case (int)COURSE_TYPE.ADVANCED:
                    return "Advanced";
                default:
                    return "";
            }
        }

        public static string GetTrainingTermString(int term)
        {
            switch (term)
            {
                case (int)TRAINING_TERM.AM:
                    return "เช้า";
                case (int)TRAINING_TERM.PM:
                    return "บ่าย";
                default:
                    return "";
            }
        }

        public static string GetCourseTypeString(int course_type)
        {
            switch (course_type)
            {
                case (int)COURSE_TYPE.BASIC:
                    return "Basic";
                case (int)COURSE_TYPE.ADVANCED:
                    return "Advanced";
                default:
                    return "";
            }
        }

        private string GetTrainerDays(string trainer_name)
        {
            string days = "";

            int cnt = 0;
            foreach (TrainingCalendar t in this.list_training_calendar.Where(t => t.trainer == trainer_name && t.status == (int)TRAINER_STATUS.TRAINER).ToList<TrainingCalendar>())
            {
                days += (++cnt == 1 ? t.date.M2WDate() : ", " + t.date.M2WDate());
            }

            return days;
        }

        private string GetAssistDays(string trainer_name)
        {
            string days = "";

            int cnt = 0;
            foreach (TrainingCalendar t in this.list_training_calendar.Where(t => t.trainer == trainer_name && t.status == (int)TRAINER_STATUS.ASSIST).ToList<TrainingCalendar>())
            {
                days += (++cnt == 1 ? t.date.M2WDate() : ", " + t.date.M2WDate());
            }

            return days;
        }

        private void FillForm()
        {
            this.FillDgvTrainer();
            this.FillDgvStat();
        }

        private void FillDgvTrainer(TrainingCalendar selected_item = null)
        {
            this.dgvTrainer.Rows.Clear();
            this.dgvTrainer.Columns.Clear();
            this.dgvTrainer.Tag = HelperClass.DGV_TAG.READ;

            this.dgvTrainer.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ลำดับ",
                Width = 40
            });
            this.dgvTrainer.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "คอร์สอบรม",
                Width = 100
            });
            this.dgvTrainer.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "รหัส : ชื่อ",
                Width = 120
            });
            this.dgvTrainer.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "สถานะ",
                Width = 100
            });
            this.dgvTrainer.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ช่วงเวลา",
                Width = 80
            });
            this.dgvTrainer.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "หมายเหตุ",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            int cnt = 0;
            foreach (TrainingCalendar t in this.list_training_calendar.Where(t => t.date == this.current_event_date.ToMysqlDate()).ToList<TrainingCalendar>())
            {
                int r = this.dgvTrainer.Rows.Add();
                this.dgvTrainer.Rows[r].Tag = t;

                this.dgvTrainer.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvTrainer.Rows[r].Cells[0].Value = ++cnt;
                this.dgvTrainer.Rows[r].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvTrainer.Rows[r].Cells[0].Style.ForeColor = Color.Gray;
                this.dgvTrainer.Rows[r].Cells[0].Style.SelectionForeColor = Color.Gray;

                this.dgvTrainer.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvTrainer.Rows[r].Cells[1].Value = GetCourseTypeString(t.course_type);

                this.dgvTrainer.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvTrainer.Rows[r].Cells[2].Value = t.trainer + " : " + (this.list_trainer_all.Find(u => u.username == t.trainer) != null ? this.list_trainer_all.Find(u => u.username == t.trainer).name : "");

                this.dgvTrainer.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvTrainer.Rows[r].Cells[3].Value = GetTrainerStatusString(t.status);

                this.dgvTrainer.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvTrainer.Rows[r].Cells[4].Value = GetTrainingTermString(t.term);

                this.dgvTrainer.Rows[r].Cells[5].ValueType = typeof(string);
                this.dgvTrainer.Rows[r].Cells[5].Value = t.remark;
            }

            this.dgvTrainer.FillLine(this.list_training_calendar.Where(t => t.date == this.current_event_date.ToMysqlDate()).ToList<TrainingCalendar>().Count);
        }

        private void FillDgvStat()
        {
            this.dgvStat.Rows.Clear();
            this.dgvStat.Columns.Clear();
            this.dgvStat.Tag = HelperClass.DGV_TAG.READ;

            this.dgvStat.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ลำดับ",
                Width = 40
            });
            this.dgvStat.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "รหัส : ชื่อ",
                Width = 120
            });
            this.dgvStat.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "เป็นวิทยากร",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvStat.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "เป็นผู้ช่วย",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            int cnt = 0;
            foreach (Users u in this.list_trainer_all)
	        {
                int r = this.dgvStat.Rows.Add();
                this.dgvStat.Rows[r].Tag = u;

                this.dgvStat.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvStat.Rows[r].Cells[0].Value = ++cnt;
                this.dgvStat.Rows[r].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvStat.Rows[r].Cells[0].Style.ForeColor = Color.Gray;
                this.dgvStat.Rows[r].Cells[0].Style.SelectionForeColor = Color.Gray;

                this.dgvStat.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvStat.Rows[r].Cells[1].Value = u.username + " : " + u.name;

                this.dgvStat.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvStat.Rows[r].Cells[2].Value = this.GetTrainerDays(u.username);

                this.dgvStat.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvStat.Rows[r].Cells[3].Value = this.GetAssistDays(u.username);
	        }

            this.dgvStat.FillLine(this.list_trainer_all.Count);

        }

        private void ShowInlineForm(FORM_MODE mode)
        {
            if (mode == FORM_MODE.ADD_F8)
                this.dgvTrainer.Rows[this.list_training_calendar.Where(t => t.date == this.current_event_date.ToMysqlDate()).Count<TrainingCalendar>()].Cells[0].Selected = true;

            List<Users> rest_users = this.list_trainer_rest.ConvertAll(t => t).ToList<Users>();
            if (mode == FORM_MODE.EDIT_F8)
            {
                rest_users.Add(this.list_trainer_all.Find(u => u.username == ((TrainingCalendar)this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].Tag).trainer));
                rest_users = rest_users.OrderBy(t => t.username).ToList<Users>();
            }
            this.inline_course_type = (this.inline_course_type == null ? new CustomComboBox() { Read_Only = false, BorderStyle = BorderStyle.None } : this.inline_course_type);
            this.inline_course_type.comboBox1.Items.Clear();
            this.inline_course_type.AddItem(new ComboboxItem("Basic", 1, "Basic"));
            this.inline_course_type.AddItem(new ComboboxItem("Advanced", 2, "Advanced"));
            this.inline_course_type.comboBox1.SelectedIndex = 0;
            this.inline_trainer = (this.inline_trainer == null ? new CustomComboBox() { Read_Only = false, BorderStyle = BorderStyle.None } : this.inline_trainer);
            this.inline_trainer.comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            this.inline_trainer.comboBox1.Items.Clear();
            //foreach (Users u in rest_users)
            foreach (Users u in this.list_trainer_all)
            {
                this.inline_trainer.AddItem(new ComboboxItem(u.username + " : " + u.name, u.id, u.username) { Tag = u });
            }
            this.inline_trainer.comboBox1.Leave += delegate
            {
                ComboBox cb = this.inline_trainer.comboBox1;
                if (cb.Items.Cast<ComboboxItem>().Where(i => i.ToString().Length >= cb.Text.Length).Where(i => i.ToString().Substring(0, cb.Text.Length) == cb.Text).Count<ComboboxItem>() > 0)
                {
                    cb.SelectedItem = cb.Items.Cast<ComboboxItem>().Where(i => i.ToString().Length >= cb.Text.Length).Where(i => i.ToString().Substring(0, cb.Text.Length) == cb.Text).First<ComboboxItem>();
                }
                else
                {
                    cb.Focus();
                }
            };

            this.inline_status = (this.inline_status == null ? new CustomComboBox() { Read_Only = false, BorderStyle = BorderStyle.None } : this.inline_status);
            this.inline_status.comboBox1.Items.Clear();
            this.inline_status.AddItem(new ComboboxItem("วิทยากร", (int)TRAINER_STATUS.TRAINER, "TRAINER"));
            this.inline_status.AddItem(new ComboboxItem("ผู้ช่วย", (int)TRAINER_STATUS.ASSIST, "ASSISTANT"));
            this.inline_status.comboBox1.SelectedIndex = 0;
            this.inline_term = (this.inline_term == null ? new CustomComboBox() { Read_Only = false, BorderStyle = BorderStyle.None } : this.inline_term);
            this.inline_term.comboBox1.Items.Clear();
            this.inline_term.AddItem(new ComboboxItem("เช้า", (int)TRAINING_TERM.AM, "AM"));
            this.inline_term.AddItem(new ComboboxItem("บ่าย", (int)TRAINING_TERM.PM, "PM"));
            this.inline_term.comboBox1.SelectedIndex = 0;
            this.inline_remark = (this.inline_remark == null ? new CustomTextBox() { Read_Only = false, BorderStyle = BorderStyle.None } : this.inline_remark);

            #region binding inline control event handler
            this.inline_course_type.comboBox1.GotFocus += new EventHandler(this.InlineControlGotFocus);
            this.inline_trainer.comboBox1.GotFocus += new EventHandler(this.InlineControlGotFocus);
            this.inline_status.comboBox1.GotFocus += new EventHandler(this.InlineControlGotFocus);
            this.inline_term.comboBox1.GotFocus += new EventHandler(this.InlineControlGotFocus);
            this.inline_remark.textBox1.GotFocus += new EventHandler(this.InlineControlGotFocus);
            #endregion binding inline control event handler

            if (mode == FORM_MODE.ADD_F8)
            {
                this.FormAddF8();
            }
            else if (mode == FORM_MODE.EDIT_F8)
            {
                this.FormEditF8();
                TrainingCalendar trainer = (TrainingCalendar)this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].Tag;
                this.inline_course_type.comboBox1.SelectedItem = (this.inline_course_type.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.int_value == trainer.course_type).Count<ComboboxItem>() > 0 ? this.inline_course_type.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.int_value == trainer.course_type).First<ComboboxItem>() : null);
                this.inline_trainer.comboBox1.SelectedItem = (this.inline_trainer.comboBox1.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).username == trainer.trainer).Count<ComboboxItem>() > 0 ? this.inline_trainer.comboBox1.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).username == trainer.trainer).First<ComboboxItem>() : null);
                this.inline_status.comboBox1.SelectedItem = (this.inline_status.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.int_value == trainer.status).Count<ComboboxItem>() > 0 ? this.inline_status.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.int_value == trainer.status).First<ComboboxItem>() : null);
                this.inline_term.comboBox1.SelectedItem = (this.inline_term.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.int_value == trainer.term).Count<ComboboxItem>() > 0 ? this.inline_term.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.int_value == trainer.term).First<ComboboxItem>() : null);
                this.inline_remark.Texts = trainer.remark;
            }
            this.SetInlineFormPosition();
            this.inline_course_type.comboBox1.Focus();

            this.dgvTrainer.Parent.Controls.Add(this.inline_course_type);
            this.dgvTrainer.Parent.Controls.Add(this.inline_trainer);
            this.dgvTrainer.Parent.Controls.Add(this.inline_status);
            this.dgvTrainer.Parent.Controls.Add(this.inline_term);
            this.dgvTrainer.Parent.Controls.Add(this.inline_remark);

            this.dgvTrainer.SendToBack();
            this.inline_course_type.BringToFront();
            this.inline_trainer.BringToFront();
            this.inline_status.BringToFront();
            this.inline_term.BringToFront();
            this.inline_remark.BringToFront();

            this.inline_course_type.Focus();
        }

        private void SetInlineFormPosition()
        {
            if (this.form_mode == FORM_MODE.ADD_F8 || this.form_mode == FORM_MODE.EDIT_F8)
            {
                Rectangle rect_course_type = this.dgvTrainer.GetCellDisplayRectangle(1, this.dgvTrainer.CurrentCell.RowIndex, true);
                Rectangle rect_trainer = this.dgvTrainer.GetCellDisplayRectangle(2, this.dgvTrainer.CurrentCell.RowIndex, true);
                Rectangle rect_status = this.dgvTrainer.GetCellDisplayRectangle(3, this.dgvTrainer.CurrentCell.RowIndex, true);
                Rectangle rect_term = this.dgvTrainer.GetCellDisplayRectangle(4, this.dgvTrainer.CurrentCell.RowIndex, true);
                Rectangle rect_remark = this.dgvTrainer.GetCellDisplayRectangle(5, this.dgvTrainer.CurrentCell.RowIndex, true);

                this.inline_course_type.SetBounds(rect_course_type.X + 3, rect_course_type.Y + 4, rect_course_type.Width, rect_course_type.Height - 3);
                this.inline_trainer.SetBounds(rect_trainer.X + 3, rect_trainer.Y + 4, rect_trainer.Width, rect_trainer.Height - 3);
                this.inline_status.SetBounds(rect_status.X + 3, rect_status.Y + 4, rect_status.Width, rect_status.Height - 3);
                this.inline_term.SetBounds(rect_term.X + 3, rect_term.Y + 4, rect_term.Width, rect_term.Height - 3);
                this.inline_remark.SetBounds(rect_remark.X + 3, rect_remark.Y + 4, rect_remark.Width - 1, rect_remark.Height - 3);
            }
        }

        private void ClearInlineForm()
        {
            if (this.inline_course_type != null)
            {
                this.inline_course_type.Dispose();
                this.inline_course_type = null;
            }
            if (this.inline_trainer != null)
            {
                this.inline_trainer.Dispose();
                this.inline_trainer = null;
            }
            if (this.inline_status != null)
            {
                this.inline_status.Dispose();
                this.inline_status = null;
            }
            if (this.inline_term != null)
            {
                this.inline_term.Dispose();
                this.inline_term = null;
            }
            if (this.inline_remark != null)
            {
                this.inline_remark.Dispose();
                this.inline_remark = null;
            }
        }

        private bool ValidateInlineForm()
        {
            if (this.inline_course_type.comboBox1.SelectedItem == null)
            {
                MessageAlert.Show("กรุณาระบุประเภทคอร์สอบรม");
                this.inline_course_type.comboBox1.Focus();
                SendKeys.Send("{F6}");
                return false;
            }
            if (this.inline_trainer.comboBox1.SelectedItem == null)
            {
                MessageAlert.Show("กรุณาระบุชื่อวิทยากร");
                this.inline_trainer.comboBox1.Focus();
                SendKeys.Send("{F6}");
                return false;
            }
            if (this.inline_status.comboBox1.SelectedItem == null)
            {
                MessageAlert.Show("กรุณาระบุสถานะ");
                this.inline_status.comboBox1.Focus();
                SendKeys.Send("{F6}");
                return false;
            }
            if (this.inline_term.comboBox1.SelectedItem == null)
            {
                MessageAlert.Show("กรุณาระบุช่วงเวลา");
                this.inline_term.comboBox1.Focus();
                SendKeys.Send("{F6}");
                return false;
            }

            return true;
        }

        private TrainingCalendar GetInlineObject()
        {
            TrainingCalendar tc = new TrainingCalendar();

            tc.id = (this.form_mode == FORM_MODE.EDIT_F8 ? ((TrainingCalendar)this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].Tag).id : -1);
            tc.date = this.current_event_date.ToMysqlDate();
            tc.course_type = (this.inline_course_type != null ? ((ComboboxItem)this.inline_course_type.comboBox1.SelectedItem).int_value : 0);
            tc.trainer = (this.inline_trainer != null ? ((ComboboxItem)this.inline_trainer.comboBox1.SelectedItem).string_value : "");
            tc.status = (this.inline_status != null ? ((ComboboxItem)this.inline_status.comboBox1.SelectedItem).int_value : 0);
            tc.term = (this.inline_term != null ? ((ComboboxItem)this.inline_term.comboBox1.SelectedItem).int_value : 0);
            tc.remark = (this.inline_remark != null ? ((CustomTextBox)this.inline_remark).Texts.cleanString() : "");
            //tc.rec_by = this.date_event.G.loged_in_user_name;
            tc.rec_by = this.date_event2.main_form.G.loged_in_user_name;

            return tc;
        }

        private void DeleteItemF8()
        {
            int deleting_id = ((TrainingCalendar)this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].Tag).id;
            bool delete_success = false;
            string err_msg = "";

            this.FormProcessing();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "trainingcalendar/delete&id=" + deleting_id.ToString());
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
                    BackgroundWorker subworker = new BackgroundWorker();
                    subworker.DoWork += delegate
                    {
                        //this.date_event.RefreshData();
                        this.date_event2.RefreshData();
                        this.LoadDependenciesData();
                    };
                    subworker.RunWorkerCompleted += delegate
                    {
                        //this.date_event.RefreshView();
                        this.date_event2.RefreshView();
                        this.dgvTrainer.Tag = HelperClass.DGV_TAG.READ;
                        this.FormReadF8();
                        this.FillForm();
                    };
                    subworker.RunWorkerAsync();
                }
                else
                {
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.DeleteItemF8();
                        return;
                    }
                    this.dgvTrainer.Tag = HelperClass.DGV_TAG.READ;
                    this.dgvTrainer.Refresh();
                    this.FormReadF8();
                }
            };
            worker.RunWorkerAsync();
        }

        private void FormRead()
        {
            this.form_mode = FORM_MODE.READ;
            this.toolStripProcessing.Visible = false;

            this.btnStop.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnCopy.Enabled = true;
            this.btnItem.Enabled = true;
            this.btnItemF7.Enabled = true;
            this.btnItemF8.Enabled = true;
            this.btnReload.Enabled = true;

            this.dgvTrainer.Enabled = true;
            this.dgvStat.Enabled = true;
        }

        private void FormEdit()
        {
            this.form_mode = FORM_MODE.EDIT;
            this.toolStripProcessing.Visible = false;

            this.btnStop.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnCopy.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnReload.Enabled = false;

            this.dgvTrainer.Enabled = false;
            this.dgvStat.Enabled = false;
        }

        private void FormReadF8()
        {
            this.form_mode = FORM_MODE.READ_F8;
            this.toolStripProcessing.Visible = false;

            this.btnStop.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnCopy.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnReload.Enabled = false;

            this.dgvTrainer.Enabled = true;
            this.dgvStat.Enabled = false;

            this.dgvTrainer.Focus();
        }

        private void FormReadF7()
        {
            this.form_mode = FORM_MODE.READ_F7;
            this.toolStripProcessing.Visible = false;

            this.btnStop.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnCopy.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnReload.Enabled = false;

            this.dgvTrainer.Enabled = false;
            this.dgvStat.Enabled = true;

            this.dgvStat.Focus();
        }

        private void FormAddF8()
        {
            this.form_mode = FORM_MODE.ADD_F8;
            this.toolStripProcessing.Visible = false;

            this.btnStop.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnCopy.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnReload.Enabled = false;

            this.dgvTrainer.Enabled = false;
            this.dgvStat.Enabled = false;

            if (this.inline_course_type != null)
                this.inline_course_type.Read_Only = false;
            if (this.inline_trainer != null)
                this.inline_trainer.Read_Only = false;
            if (this.inline_status != null)
                this.inline_status.Read_Only = false;
            if (this.inline_term != null)
                this.inline_term.Read_Only = false;
            if (this.inline_remark != null)
                this.inline_remark.Read_Only = false;
        }

        private void FormEditF8()
        {
            this.form_mode = FORM_MODE.EDIT_F8;
            this.toolStripProcessing.Visible = false;

            this.btnStop.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnCopy.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnReload.Enabled = false;

            this.dgvTrainer.Enabled = false;
            this.dgvStat.Enabled = false;

            if (this.inline_course_type != null)
                this.inline_course_type.Read_Only = false;
            if (this.inline_trainer != null)
                this.inline_trainer.Read_Only = false;
            if (this.inline_status != null)
                this.inline_status.Read_Only = false;
            if (this.inline_term != null)
                this.inline_term.Read_Only = false;
            if (this.inline_remark != null)
                this.inline_remark.Read_Only = false;
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;
            this.toolStripProcessing.Visible = true;

            this.btnStop.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnCopy.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnReload.Enabled = false;

            this.dgvTrainer.Enabled = false;
            this.dgvStat.Enabled = false;

            if (this.inline_course_type != null)
                this.inline_course_type.Read_Only = true;
            if (this.inline_trainer != null)
                this.inline_trainer.Read_Only = true;
            if (this.inline_status != null)
                this.inline_status.Read_Only = true;
            if (this.inline_term != null)
                this.inline_term.Read_Only = true;
            if (this.inline_remark != null)
                this.inline_remark.Read_Only = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.ADD_F8 || this.form_mode == FORM_MODE.EDIT_F8)
            {
                this.ClearInlineForm();
                this.FormReadF8();
                return;
            }
            if (this.form_mode == FORM_MODE.READ_F7 || this.form_mode == FORM_MODE.READ_F8)
            {
                this.FormRead();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.ADD_F8 || this.form_mode == FORM_MODE.EDIT_F8)
            {
                if (!this.ValidateInlineForm())
                    return;

                TrainingCalendar tc = this.GetInlineObject();

                string json_data = "{\"id\":" + tc.id.ToString() + ",";
                json_data += "\"date\":\"" + tc.date + "\",";
                json_data += "\"course_type\":" + tc.course_type.ToString() + ",";
                json_data += "\"trainer\":\"" + tc.trainer + "\",";
                json_data += "\"status\":" + tc.status.ToString() + ",";
                json_data += "\"term\":" + tc.term.ToString() + ",";
                json_data += "\"remark\":\"" + tc.remark + "\",";
                json_data += "\"rec_by\":\"" + tc.rec_by + "\"}";

                bool post_success = false;
                string err_msg = "";
                TrainingCalendar processed_item = null;

                FORM_MODE before_post_mode = this.form_mode;
                this.FormProcessing();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "trainingcalendar/add_or_update", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        post_success = true;
                        processed_item = sr.training_calendar[0];
                        return;
                    }
                    else
                    {
                        post_success = false;
                        err_msg = sr.message;
                        return;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (post_success)
                    {
                        BackgroundWorker subwork = new BackgroundWorker();
                        subwork.DoWork += delegate
                        {
                            //this.date_event.RefreshData();
                            this.date_event2.RefreshData();
                            this.LoadDependenciesData();
                        };
                        subwork.RunWorkerCompleted += delegate
                        {
                            //this.date_event.RefreshView();
                            this.date_event2.RefreshView();
                            this.FillForm();
                            if (this.dgvTrainer.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is TrainingCalendar).Where(r => ((TrainingCalendar)r.Tag).id == processed_item.id).Count<DataGridViewRow>() > 0)
                                this.dgvTrainer.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is TrainingCalendar).Where(r => ((TrainingCalendar)r.Tag).id == processed_item.id).First<DataGridViewRow>().Cells[0].Selected = true;
                            this.ClearInlineForm();
                            if (before_post_mode == FORM_MODE.ADD_F8)
                                this.ShowInlineForm(FORM_MODE.ADD_F8);

                            if (before_post_mode == FORM_MODE.EDIT_F8)
                                this.FormReadF8();
                        };
                        subwork.RunWorkerAsync();
                        return;
                    }
                    else
                    {
                        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        if (before_post_mode == FORM_MODE.ADD_F8)
                        {
                            this.FormAddF8();
                            return;
                        }
                        if (before_post_mode == FORM_MODE.EDIT_F8)
                        {
                            this.FormEditF8();
                            return;
                        }
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnItemF8_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            this.FormReadF8();
        }

        private void btnItemF7_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            this.FormReadF7();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            this.FormProcessing();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                this.LoadDependenciesData();
            };
            worker.RunWorkerCompleted += delegate
            {
                this.FillForm();
                this.FormRead();
            };
            worker.RunWorkerAsync();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DateSelectorDialog ds = new DateSelectorDialog();
            if (ds.ShowDialog() == DialogResult.OK)
            {
                this.DoCopyTrainer(ds.selected_date);
            }
        }

        private void DoCopyTrainer(DateTime date)
        {
            string json_data = "{\"from_date\":\"" + this.current_event_date.ToMysqlDate() + "\",";
            json_data += "\"to_date\":\"" + date.ToMysqlDate() + "\",";
            //json_data += "\"rec_by\":\"" + this.date_event.G.loged_in_user_name + "\"}";
            json_data += "\"rec_by\":\"" + this.date_event2.main_form.G.loged_in_user_name + "\"}";

            this.FormProcessing();
            bool post_success = false;
            string err_msg = "";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "trainingcalendar/copy", json_data);
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
                    //foreach (Control ct in this.date_event.Parent.Controls)
                    //{
                    //    if (((CustomDateEvent)ct).Date.ToDMYDateValue() == date.ToDMYDateValue())
                    //    {
                    //        ((CustomDateEvent)ct).RefreshData();
                    //        ((CustomDateEvent)ct).RefreshView();
                    //    }
                    //}
                    foreach (Control ct in this.date_event2.ParentForm.Controls["tableLayoutPanel1"].Controls)
                    {
                        if (ct.GetType() == typeof(CustomDateEvent2))
                        {
                            if (((CustomDateEvent2)ct).date.Value.ToDMYDateValue() == date.ToDMYDateValue())
                            {
                                ((CustomDateEvent2)ct).RefreshData();
                                ((CustomDateEvent2)ct).RefreshView();
                            }
                        }
                    }
                    MessageAlert.Show("คัดลอกข้อมูลเรียบร้อย");
                }
                else
                {
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.DoCopyTrainer(date);
                    }
                }

                this.FormRead();
            };
            worker.RunWorkerAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD_F8 || this.form_mode == FORM_MODE.EDIT_F8)
                {
                    if(this.inline_remark.textBox1.Focused){
                        this.btnSave.PerformClick();
                        return true;
                    }

                    SendKeys.Send("{TAB}");
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                if (this.form_mode == FORM_MODE.ADD_F8 || this.form_mode == FORM_MODE.EDIT_F8)
                {
                    if (this.inline_course_type.item_shown || this.inline_trainer.item_shown || this.inline_status.item_shown || this.inline_term.item_shown)
                    {
                        return false;
                    }
                    this.btnStop.PerformClick();
                    return true;
                }

                this.btnStop.PerformClick();
                return true;
            }

            if (keyData == Keys.F5)
            {
                this.btnReload.PerformClick();
                return true;
            }

            if (keyData == Keys.F7)
            {
                this.btnItemF7.PerformClick();
                return true;
            }

            if (keyData == Keys.F8)
            {
                this.btnItemF8.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                if (this.form_mode == FORM_MODE.READ_F8)
                {
                    this.ShowInlineForm(FORM_MODE.ADD_F8);
                    return true;
                }
                return true;
            }

            if (keyData == (Keys.Alt | Keys.E))
            {
                if (this.form_mode == FORM_MODE.READ_F8)
                {
                    if (this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].Tag is TrainingCalendar)
                    {
                        this.ShowInlineForm(FORM_MODE.EDIT_F8);
                        return true;
                    }
                }
                return true;
            }

            if (keyData == (Keys.Alt | Keys.D))
            {
                if (this.form_mode == FORM_MODE.READ_F8)
                {
                    if (this.dgvTrainer.CurrentCell == null)
                        return true;

                    if (!(this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].Tag is TrainingCalendar))
                        return true;

                    this.dgvTrainer.Tag = HelperClass.DGV_TAG.DELETE;
                    if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                    {
                        this.DeleteItemF8();
                        return true;
                    }
                    else
                    {
                        this.dgvTrainer.Tag = HelperClass.DGV_TAG.READ;
                        this.dgvTrainer.Refresh();
                        return true;
                    }
                }
                return true;
            }

            if (keyData == (Keys.Alt | Keys.C))
            {
                this.btnCopy.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
