using SN_Net.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.Subform
{
    public partial class YearlyHolidayAddEditDialog : Form
    {
        private MainForm main_form;
        public enum FORM_MODE
        {
            ADD,
            EDIT
        }
        public FORM_MODE form_mode;
        public NoteCalendar note_calendar;

        public YearlyHolidayAddEditDialog(MainForm main_form, NoteCalendar note_calendar = null)
        {
            InitializeComponent();
            this.main_form = main_form;


            if (note_calendar == null)
            {
                this.note_calendar = new NoteCalendar
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")),
                    description = string.Empty,
                    max_leave = -1,
                    rec_by = this.main_form.G.loged_in_user_name,
                    type = (int)NoteCalendar.NOTE_TYPE.HOLIDAY,
                    group_maid = string.Empty,
                    group_weekend = string.Empty
                };
            }
            else
            {
                this.note_calendar = note_calendar;
                this.note_calendar.rec_by = this.main_form.G.loged_in_user_name;
            }

            this.form_mode = note_calendar != null ? FORM_MODE.EDIT : FORM_MODE.ADD;
        }

        private void YearlyHolidayAddEditDialog_Load(object sender, EventArgs e)
        {
            this.dtDate.Value = this.note_calendar._Date;
            this.txtDescription.Text = this.note_calendar.description;
        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            this.note_calendar.date = ((DateTimePicker)sender).Value.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            this.note_calendar.description = ((TextBox)sender).Text;
        }
    }
}
