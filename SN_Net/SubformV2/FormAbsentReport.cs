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

namespace SN_Net.Subform
{
    public partial class FormAbsentReport : Form
    {
        private users users_from = null;
        private users users_to = null;
        private DateTime? date_from = null;
        private DateTime? date_to = null;

        private BindingList<AbsentCauseVM> cause1_list;
        private BindingList<AbsentCauseVM> cause2_list;
        private BindingList<AbsentPersonStatVM> absent_person_list;
        private BindingList<AbsentPersonStatVM> absent_summary_list;

        public FormAbsentReport()
        {
            InitializeComponent();
        }

        private void FormAbsentReport_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
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

                    this.GetData();
                    this.FillForm();
                }
            }
            
        }

        private void GetData()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                var person_absent = note.event_calendar.Where(ev => ev.users_name.Trim() == this.users_from.username.Trim() && ev.date.CompareTo(this.date_from.Value) >= 0 && ev.date.CompareTo(this.date_to.Value) <= 0).OrderBy(ev => ev.date).ThenBy(ev => ev.from_time).ToAbsentPersonStatVM();
                var p_seq = 0;
                person_absent.ForEach(ev => { p_seq++; ev.seq = p_seq; });
                this.absent_person_list = new BindingList<AbsentPersonStatVM>(person_absent);
                

                var all_absent = note.event_calendar.Where(ev => ev.date.CompareTo(this.date_from.Value) >= 0 && ev.date.CompareTo(this.date_to.Value) <= 0).ToList();
                this.absent_summary_list = new BindingList<AbsentPersonStatVM>(all_absent.ToAbsentPersonStatVM());
            }
        }

        private void FillForm()
        {
            this.lblUserFrom.Text = this.users_from.username + " : " + this.users_from.name;
            this.lblUserTo.Text = this.users_to.username + " : " + this.users_to.name;
            this.lblDateFrom.Text = this.date_from.Value.ToString("dddd  d MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            this.lblDateTo.Text = this.date_to.Value.ToString("dddd  d MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            //this.lblDateFrom2.Text = this.date_from.Value.ToString("dddd  d MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            //this.lblDateTo2.Text = this.date_to.Value.ToString("dddd  d MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            this.dtYearAbsentFrom.Value = DateTime.Now;
            this.dtYearAbsentTo.Value = DateTime.Now;

            this.dgvDetail.DataSource = this.absent_person_list;
            this.dgvSum.DataSource = this.absent_summary_list;
            this.SetCauseList();
        }

        private void SetCauseList()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                this.cause1_list = new BindingList<AbsentCauseVM>(note.note_istab.Where(i => i.tabtyp == CALENDAR_EVENT_TYPE.ABSENT).ToAbsentCauseVM());
                this.dgvAbsent.DataSource = this.cause1_list;

                this.cause2_list = new BindingList<AbsentCauseVM>(note.note_istab.Where(i => i.tabtyp == CALENDAR_EVENT_TYPE.MEET_CUST).ToAbsentCauseVM());
                this.dgvCust.DataSource = this.cause2_list;
            }
        }

        private void btnPrint1_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeScope_Click(object sender, EventArgs e)
        {
            DialogAbsentReportScope scope = new DialogAbsentReportScope();
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
    }
}