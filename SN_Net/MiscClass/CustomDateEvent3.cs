using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.Model;
using CC;
using System.Globalization;

namespace SN_Net.MiscClass
{
    public partial class CustomDateEvent3 : UserControl
    {
        public enum NOTE_CALENDAR_TYPE : int
        {
            WEEKDAY = 0,
            HOLIDAY = 1
        }

        private MainForm main_form;
        private DateTime curr_date;
        private BindingList<event_calendarVM> event_list;
        private note_calendar note_cal;
        private List<training_calendar> training_list;

        public CustomDateEvent3(MainForm main_form, DateTime curr_date, List<event_calendar> event_cal, note_calendar note_cal, List<training_calendar> training_cal)
        {
            this.main_form = main_form;
            this.curr_date = curr_date;
            this.event_list = new BindingList<event_calendarVM>(event_cal.ToViewModel());
            this.note_cal = note_cal;
            this.training_list = training_cal;

            InitializeComponent();
        }

        private void CustomDateEvent3_Load(object sender, EventArgs e)
        {
            this.lblDay.Text = this.curr_date.ToString("d", CultureInfo.GetCultureInfo("th-TH"));
            this.lblMonthYear.Text = this.curr_date.ToString("MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            this.lblNoteDescription.Text = this.note_cal != null ? this.note_cal.description : string.Empty;

            if (this.note_cal != null && this.note_cal.type == (int)NOTE_CALENDAR_TYPE.HOLIDAY)
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
            this.lblBottomText.Text = training_list.Count > 0 ? string.Join(",", training_list.Select(t => t.trainer).ToArray()) : string.Empty;
        }
    }
}
