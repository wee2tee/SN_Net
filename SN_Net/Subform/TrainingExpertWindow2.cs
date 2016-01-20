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
    public partial class TrainingExpertWindow2 : Form
    {
        private CultureInfo cinfo_th = new CultureInfo("th-TH");
        private CustomDateEvent date_event;
        private List<Users> trainer_all;
        private List<Users> trainer_rest;
        private List<Users> trainer_selected;
        private List<TrainingCalendar> training_calendar;

        public TrainingExpertWindow2(CustomDateEvent date_event)
        {
            InitializeComponent();

            this.date_event = date_event;
        }

        private void TrainingExpertWindow_Load(object sender, EventArgs e)
        {
            this.trainer_all = this.GetTrainerAll();
            this.trainer_rest = this.trainer_all.ConvertAll<Users>(t => t).ToList<Users>();
            this.trainer_selected = this.GetTrainerSelected();
            this.training_calendar = this.GetTrainingCalendar();

            this.BindingControlEvent();

            this.FillDgvAll();
            this.FillDgvSelected();
            this.FillDgvHistory();
        }

        private void BindingControlEvent()
        {
            #region Row hover background
            this.dgvAll.CellMouseEnter += new DataGridViewCellEventHandler(this.SetRowHoverBackground);
            this.dgvSelected.CellMouseEnter += new DataGridViewCellEventHandler(this.SetRowHoverBackground);
            this.dgvAll.CellMouseLeave += new DataGridViewCellEventHandler(this.SetRowLeaveBackground);
            this.dgvSelected.CellMouseLeave += new DataGridViewCellEventHandler(this.SetRowLeaveBackground);
            #endregion Row hover background

            #region Select/Deselect row
            this.dgvAll.CellMouseClick += new DataGridViewCellMouseEventHandler(this.SetRowSelect);
            this.dgvSelected.CellMouseClick += new DataGridViewCellMouseEventHandler(this.SetRowSelect);
            #endregion Select/Deselect row

            #region Delete trainer button
            this.dgvSelected.CellMouseClick += delegate(object sender, DataGridViewCellMouseEventArgs e)
            {
                if (((Users)((DataGridView)sender).Rows[e.RowIndex].Tag).training_expert == "N" && e.RowIndex > -1 && e.ColumnIndex == 3)
                {
                    if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                    {
                        int user_id = ((Users)((DataGridView)sender).Rows[e.RowIndex].Tag).id;
                        this.trainer_selected.Remove(this.trainer_selected.Find(t => t.id == user_id));
                        this.FillDgvSelected();
                    }
                }
            };
            #endregion Delete trainer button
        }

        private void SetRowSelect(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells[1].ValueType == typeof(bool) && ((Users)((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Tag).training_expert == "Y")
            {
                ((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells[1].Value = !(bool)((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells[1].Value;
            }
        }

        private void SetRowHoverBackground(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void SetRowLeaveBackground(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
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
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "trainingcalendar/get_trainer&date_from=" + this.date_event.Date.ToMysqlDate() + "&date_to=" + this.date_event.Date.ToMysqlDate());
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

        private List<TrainingCalendar> GetTrainingCalendar()
        {
            DateTime first_day_of_month = new DateTime(this.date_event.Date.Year, this.date_event.Date.Month, 1);
            DateTime last_day_of_month = first_day_of_month.AddMonths(1).AddDays(-1);
            Console.WriteLine(" >> first_day : " + first_day_of_month.ToString() + ", last_day" + last_day_of_month.ToString());
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "trainingcalendar/get_month_trainer&date_from=" + first_day_of_month.ToMysqlDate() + "&date_to=" + last_day_of_month.ToMysqlDate());
            Console.WriteLine(" >>> " + get.data);
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

        private void TrainingExpertWindow_Shown(object sender, EventArgs e)
        {
            this.Text = "วิทยากรอบรมประจำวัน " + this.date_event.Date.ToString("dddd , d MMM yy", cinfo_th.DateTimeFormat);
            this.lblMonthInfo.Text = "ข้อมูลรายบุคคล (เดือน " + this.date_event.Date.ToString("MMM yy", cinfo_th.DateTimeFormat) + ")";
        }

        private void FillDgvAll()
        {
            #region remove selected trainer from this list
            foreach (Users u in this.trainer_selected)
            {
                if (this.trainer_rest.Find(t => t.id == u.id) != null)
                {
                    this.trainer_rest.Remove(this.trainer_rest.Find(t => t.id == u.id));
                }
            }
            #endregion remove selected trainer from this list

            this.dgvAll.Rows.Clear();
            this.dgvAll.Columns.Clear();
            this.dgvAll.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Visible = false
            });
            this.dgvAll.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Width = 20
            });
            this.dgvAll.Columns.Add(new DataGridViewTextBoxColumn()
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            #region Fill in dgvAll
            foreach (Users u in this.trainer_rest)
            {
                int r = this.dgvAll.Rows.Add();
                this.dgvAll.Rows[r].Tag = u;

                this.dgvAll.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvAll.Rows[r].Cells[0].Value = u.id;

                this.dgvAll.Rows[r].Cells[1].ValueType = typeof(bool);
                this.dgvAll.Rows[r].Cells[1].Value = false;

                this.dgvAll.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvAll.Rows[r].Cells[2].Value = u.username + " : " + u.name;
            }
            #endregion Fill in dgvAll
        }

        private void FillDgvSelected()
        {
            this.dgvSelected.Rows.Clear();
            this.dgvSelected.Columns.Clear();
            this.dgvSelected.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Visible = false
            });
            this.dgvSelected.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Width = 20
            });
            this.dgvSelected.Columns.Add(new DataGridViewTextBoxColumn()
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvSelected.Columns.Add(new DataGridViewButtonColumn()
            {
                Width = 20
            });

            #region Fill in dgvSelected
            foreach (Users u in this.trainer_selected)
            {
                int r = this.dgvSelected.Rows.Add();
                this.dgvSelected.Rows[r].Tag = u;

                this.dgvSelected.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvSelected.Rows[r].Cells[0].Value = u.id;

                this.dgvSelected.Rows[r].Cells[1].ValueType = typeof(bool);
                this.dgvSelected.Rows[r].Cells[1].Value = false;
                if (u.training_expert == "N")
                {
                    this.dgvSelected.Rows[r].Cells[1].Style.Padding = new Padding(50, 0, 0, 0);
                }
                else
                {
                    this.dgvSelected.Rows[r].Cells[1].Style.Padding = new Padding(0, 0, 0, 0);
                }

                this.dgvSelected.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvSelected.Rows[r].Cells[2].Value = u.username + " : " + u.name;

                if (u.training_expert == "Y")
                {
                    this.dgvSelected.Rows[r].Cells[3].Value = "";
                    this.dgvSelected.Rows[r].Cells[3].Style.Padding = new Padding(25, 0, 0, 0);
                }
                else
                {
                    this.dgvSelected.Rows[r].Cells[3].Value = "x";
                    this.dgvSelected.Rows[r].Cells[3].Style.Font = new Font("tahoma", 7f);
                    this.dgvSelected.Rows[r].Cells[3].Style.Padding = new Padding(2, 2, 2, 2);
                    this.dgvSelected.Rows[r].Cells[3].ToolTipText = "ลบ";
                }
            }
            #endregion Fill in dgvSelected
        }

        private void FillDgvHistory()
        {
            this.dgvHistory.Rows.Clear();
            this.dgvHistory.Columns.Clear();

            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "col_id",
                Visible = false
            });
            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "col_trainer",
                Width = 120,
                HeaderText = "วิทยากร"
            });
            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "col_course_count",
                Width = 80,
                HeaderText = "จำนวนคอร์ส"
            });
            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "col_date",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                HeaderText = "วันที่"
            });

            foreach (Users u in this.trainer_all)
            {
                int r = this.dgvHistory.Rows.Add();
                this.dgvHistory.Rows[r].Tag = u;

                this.dgvHistory.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvHistory.Rows[r].Cells[0].Value = u.id;

                this.dgvHistory.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvHistory.Rows[r].Cells[1].Value = u.username + " : " + u.name;

                this.dgvHistory.Rows[r].Cells[2].ValueType = typeof(int);
                this.dgvHistory.Rows[r].Cells[2].Value = (this.training_calendar.Where(t => t.trainer == u.username).ToList<TrainingCalendar>() != null ? this.training_calendar.Where(t => t.trainer == u.username).ToList<TrainingCalendar>().Count : 0);
                this.dgvHistory.Rows[r].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.dgvHistory.Rows[r].Cells[3].ValueType = typeof(string);
                string[] arr_date = this.training_calendar.Where(t => t.trainer == u.username).ToList<TrainingCalendar>().Select(t => t.date.M2WDate()).ToArray();
                this.dgvHistory.Rows[r].Cells[3].Value = string.Join(", ", arr_date);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<Users> list_selected_trainer = (from r in this.dgvAll.Rows.Cast<DataGridViewRow>()
                                                   where (bool)r.Cells[1].Value == true
                                                   select (Users)r.Tag).ToList<Users>();

            foreach (Users u in list_selected_trainer)
            {
                this.dgvAll.Rows.Remove(this.dgvAll.Rows.Cast<DataGridViewRow>().Where(r => ((Users)r.Tag).id == u.id).First<DataGridViewRow>());
                this.trainer_rest.Remove(u);
            }

            this.trainer_selected = trainer_selected.Concat(list_selected_trainer).ToList<Users>().OrderBy(t => t.username).ToList<Users>();

            this.FillDgvSelected();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<Users> list_deselected_trainer = (from r in this.dgvSelected.Rows.Cast<DataGridViewRow>()
                                                   where (bool)r.Cells[1].Value == true
                                                   select (Users)r.Tag).ToList<Users>();

            foreach (Users u in list_deselected_trainer)
            {
                this.dgvSelected.Rows.Remove(this.dgvSelected.Rows.Cast<DataGridViewRow>().Where(r => ((Users)r.Tag).id == u.id).First<DataGridViewRow>());
                this.trainer_selected.Remove(u);
            }

            this.trainer_rest = trainer_rest.Concat(list_deselected_trainer).ToList<Users>().OrderBy(t => t.username).ToList<Users>();

            this.FillDgvAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<Users> list_selected_trainer = (from r in this.dgvSelected.Rows.Cast<DataGridViewRow>()
                                                   select (Users)r.Tag).ToList<Users>();

            StringBuilder trainer_id = new StringBuilder();
            int cnt = 0;
            foreach (Users u in list_selected_trainer)
            {
                cnt++;
                trainer_id.Append((cnt == 1 ? u.id.ToString() : "," + u.id.ToString()));
            }

            string json_data = "{\"date\":\"" + this.date_event.Date.ToMysqlDate() + "\",";
            json_data += "\"trainer_id\":\"" + trainer_id.ToString() + "\",";
            json_data += "\"rec_by\":\"" + this.date_event.G.loged_in_user_name + "\"}";
            Console.WriteLine(" >>> " + json_data);

            CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "trainingcalendar/change", json_data);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            if (keyData == Keys.F9)
            {
                this.btnOK.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
