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
using System.Globalization;
using CC;

namespace SN_Net.Subform
{
    public partial class DialogYearlyHoliday : Form
    {
        private MainForm main_form;
        private int year;
        private BindingList<YearlyHolidayVM> holidays;
        private FORM_MODE form_mode;
        private note_calendar tmp_note;

        public DialogYearlyHoliday(MainForm main_form, int? year = null)
        {
            this.main_form = main_form;
            this.year = year.HasValue ? year.Value : DateTime.Now.Year;
            InitializeComponent();
        }

        private void DialogYearlyHoliday_Load(object sender, EventArgs e)
        {
            this.Text = "วันหยุดประจำปี " + this.year.ToString();
            this.holidays = new BindingList<YearlyHolidayVM>(GetHolidays(this.year));
            this.dgv.DataSource = this.holidays;
            this.RemoveInlineForm();
            this.ResetFormState(FORM_MODE.READ_ITEM);
        }

        public static List<YearlyHolidayVM> GetHolidays(int year)
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                DateTime date_from = DateTime.Parse(year.ToString() + "-01-01", CultureInfo.GetCultureInfo("en-US"));
                DateTime date_to = DateTime.Parse(year.ToString() + "-12-31", CultureInfo.GetCultureInfo("en-US"));
                return note.note_calendar.Where(n => n.type == (int)CALENDAR_NOTE_TYPE.HOLIDAY && n.date.CompareTo(date_from) >= 0 && n.date.CompareTo(date_to) <= 0).OrderBy(h => h.date).ToViewModelYearlyHoliday();
            }
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.btnAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnDelete.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnSave.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnStop.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnChangeYear.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);

            this.dgv.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
        }

        private void ShowInlineForm()
        {
            if (this.dgv.CurrentCell == null)
                return;

            this.tmp_note = (note_calendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_note_calendar.Name].Value;

            this.SetInlineFormPosition();
        }

        private void RemoveInlineForm()
        {
            this.inlineDate.SetBounds(-9999, -9999, 0, 0);
            this.inlineDesc.SetBounds(-9999, -9999, 0, 0);

            this.tmp_note = null;
        }

        private void SetInlineFormPosition()
        {
            int col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_date.Name).FirstOrDefault().Index;
            this.inlineDate.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            this.inlineDate.MaxDate = DateTime.Parse(this.year.ToString() + "-12-31", CultureInfo.GetCultureInfo("en-US"));
            this.inlineDate.MinDate = DateTime.Parse(this.year.ToString() + "-01-01", CultureInfo.GetCultureInfo("en-US"));
            this.inlineDate.Value = this.tmp_note.date;


            col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_description.Name).FirstOrDefault().Index;
            this.inlineDesc.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            this.inlineDesc._Text = this.tmp_note.description;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeYear_Click(object sender, EventArgs e)
        {
            DialogSelectYear sel = new DialogSelectYear(this.year);
            if (sel.ShowDialog() == DialogResult.OK)
            {
                this.year = sel.selected_year;
                this.holidays = new BindingList<YearlyHolidayVM>(GetHolidays(this.year));
                this.dgv.DataSource = this.holidays;
                this.Text = "วันหยุดประจำปี " + this.year.ToString();
            }
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                if (e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_seq.Name).FirstOrDefault().Index)
                {
                    ((XDatagrid)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = e.RowIndex + 1;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F6)
            {
                this.btnChangeYear.PerformClick();
                return true;
            }

            if(keyData == Keys.Escape)
            {
                this.Close();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
