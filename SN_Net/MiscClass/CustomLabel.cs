using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using SN_Net.Subform;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.MiscClass
{
    public partial class CustomLabel : UserControl
    {
        private NoteCalendar note_calendar;
        private List<TrainingCalendar> training_list;

        public CustomLabel()
        {
            InitializeComponent();

            this.lblMaid.Click += delegate
            {
                List<Users> users = this.GetUserInGroup(this.note_calendar.group_maid);
                string user = "สมาชิกในกลุ่ม : ";

                int cnt = 0;
                foreach (Users u in users)
                {
                    user += u.name + (++cnt < users.Count ? ", " : "");
                }

                MessageAlert.Show(user);
            };

            this.lblWeekend.Click += delegate
            {
                List<Users> users = this.GetUserInGroup(this.note_calendar.group_weekend);
                string user = "สมาชิกในกลุ่ม : ";

                int cnt = 0;
                foreach (Users u in users)
                {
                    user += u.name + (++cnt < users.Count ? ", " : "");
                }

                MessageAlert.Show(user);
            };
        }

        public void SetVisualControl(NoteCalendar note_calendar = null, List<TrainingCalendar> training_list = null)
        {
            this.note_calendar = note_calendar;
            this.training_list = training_list;

            if (this.note_calendar != null)
            {
                if (this.note_calendar.group_maid.Trim().Length > 0)
                {
                    this.picMaid.Visible = true;
                    this.lblMaid.Visible = true;
                    this.lblMaid.Text = note_calendar.group_maid;
                    //this.lblMaid.Click += delegate
                    //{
                        
                    //};
                }
                else
                {
                    this.picMaid.Visible = false;
                    this.lblMaid.Text = "";
                }

                if (this.note_calendar.group_weekend.Trim().Length > 0)
                {
                    this.picWeekend.Visible = true;
                    this.lblWeekend.Visible = true;
                    this.lblWeekend.Text = note_calendar.group_weekend;
                    //this.lblWeekend.Click += delegate
                    //{
                        
                    //};
                }
                else
                {
                    this.picWeekend.Visible = false;
                    this.lblWeekend.Text = "";
                }
            }
            else
            {
                this.picMaid.Visible = false;
                this.picWeekend.Visible = false;
                this.lblMaid.Visible = false;
                this.lblWeekend.Visible = false;
            }

            if (this.training_list != null)
            {
                this.lblTrainer.Visible = true;
                string trainer = (this.training_list.Count > 0 ? "อบรม(" : "");

                int trainer_count = 0;
                foreach (TrainingCalendar t in this.training_list)
                {
                    CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_realname&username=" + this.training_list[trainer_count].trainer);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        trainer += (++trainer_count == 1 ? sr.users[0].name : "," + sr.users[0].name);
                    }
                    else
                    {
                        trainer += "";
                    }

                }
                trainer += (this.training_list.Count > 0 ? ")" : "");

                this.lblTrainer.Text = trainer;
            }
            else
            {
                this.lblTrainer.Visible = false;
            }
        }

        private List<Users> GetUserInGroup(string usergroup)
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_user_in_group&usergroup=" + usergroup);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return sr.users;
            }
            else
            {
                return null;
            }
        }
    }
}
