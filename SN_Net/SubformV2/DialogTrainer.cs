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
    public partial class DialogTrainer : Form
    {
        private MainForm main_form;
        private DateTime curr_date;
        private BindingList<training_calendarVM> trainers;
        private BindingList<trainer_monthly_statVM> stats;
        private training_calendar tmp_train;
        private FORM_MODE form_mode;

        public DialogTrainer(MainForm main_form, DateTime curr_date)
        {
            this.main_form = main_form;
            this.curr_date = curr_date;
            InitializeComponent();
        }

        private void DialogTrainer_Load(object sender, EventArgs e)
        {
            this.RemoveInlineForm();
            this.ResetControlState(FORM_MODE.READ_ITEM);
            this.Text += this.curr_date.ToString(" dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH"));
            this.dtDate.Value = this.curr_date;
            this.GetTrainerData();
            this.dgvTrainer.DataSource = this.trainers;
            this.dgvStat.DataSource = this.stats;
            this.SetDropDownListItem();
        }

        private void GetTrainerData()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                this.trainers = new BindingList<training_calendarVM>(note.training_calendar.Where(t => t.date == this.curr_date).OrderBy(t => t.term).ThenBy(t => t.status).ToViewModel());
            }

            using (snEntities db = DBX.DataSet())
            {
                this.stats = new BindingList<trainer_monthly_statVM>(db.users.Where(u => u.training_expert == "Y").OrderBy(u => u.username).ToViewModelTrainerStat(this.curr_date));
            }
        }

        private void SetDropDownListItem()
        {
            using (snEntities db = DBX.DataSet())
            {
                db.users.Where(u => u.training_expert == "Y").OrderBy(u => u.username).ToList().ForEach(u => this.inlineTrainer._Items.Add(new XDropdownListItem { Text = u.username + " : " + u.name, Value = u.username }));
            }

            this.inlineCourseType._Items.Add(new XDropdownListItem { Text = "Basic", Value = 1 });
            this.inlineCourseType._Items.Add(new XDropdownListItem { Text = "Advanced", Value = 2 });

            this.inlineStatus._Items.Add(new XDropdownListItem { Text = "วิทยากร", Value = 1 });
            this.inlineStatus._Items.Add(new XDropdownListItem { Text = "ผู้ช่วย", Value = 2 });

            this.inlineTerm._Items.Add(new XDropdownListItem { Text = "เช้า", Value = 1 });
            this.inlineTerm._Items.Add(new XDropdownListItem { Text = "บ่าย", Value = 2 });
        }

        private void ResetControlState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;
            this.btnAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnDelete.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnSave.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnCancel.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
        }

        private void RemoveInlineForm()
        {
            this.inlineCourseType.SetBounds(-99999, 0, this.inlineCourseType.Width, this.inlineCourseType.Height);
            this.inlineTrainer.SetBounds(-99999, 0, this.inlineTrainer.Width, this.inlineTrainer.Height);
            this.inlineStatus.SetBounds(-99999, 0, this.inlineStatus.Width, this.inlineStatus.Height);
            this.inlineTerm.SetBounds(-99999, 0, this.inlineTerm.Width, this.inlineTerm.Height);
            this.inlineRemark.SetBounds(-99999, 0, this.inlineRemark.Width, this.inlineRemark.Height);

            this.tmp_train = null;
        }

        private void ShowInlineForm()
        {
            if (this.dgvTrainer.CurrentCell == null)
                return;

            this.tmp_train = (training_calendar)this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].Cells[this.col_training_calendar.Name].Value;

            int col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_course_type.Name).First().Index;
            this.inlineCourseType.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);
            this.inlineCourseType._SelectedItem = this.inlineCourseType._Items.Cast<XDropdownListItem>().Where(i => (int)i.Value == this.tmp_train.course_type).FirstOrDefault();

            col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_code_name.Name).First().Index;
            this.inlineTrainer.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);

            col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_status.Name).First().Index;
            this.inlineStatus.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);

            col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_term.Name).First().Index;
            this.inlineTerm.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);

            col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_remark.Name).First().Index;
            this.inlineRemark.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);
        }

        private void dgvTrainer_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            
            if(e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_seq.Name).First().Index)
            {
                ((XDatagrid)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = e.RowIndex + 1;
            }
        }

        private void dgvStat_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_stat_seq.Name).First().Index)
            {
                ((XDatagrid)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = e.RowIndex + 1;
            }
        }

        private void dgvTrainer_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                if (row_index > -1)
                    ((XDatagrid)sender).Rows[row_index].Cells[this.col_code_name.Name].Selected = true;
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
                    this.btnEdit.PerformClick();
                };
                mnu_edit.Enabled = row_index == -1 ? false : true;
                cm.MenuItems.Add(mnu_edit);

                cm.Show(((XDatagrid)sender), new Point(e.X, e.Y));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ((BindingList<training_calendarVM>)this.dgvTrainer.DataSource).Add(new training_calendar
            {
                id = -1,
                course_type = 1,
                date = this.curr_date,
                status = 1,
                term = 1,
                trainer = string.Empty,
                remark = string.Empty,
                rec_by = this.main_form.loged_in_user.username
            }.ToViewModel());

            this.dgvTrainer.Rows.Cast<DataGridViewRow>().Where(r => ((training_calendar)r.Cells[this.col_training_calendar.Name].Value).id == -1).First().Cells[this.col_course_type.Name].Selected = true;
            this.ShowInlineForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.ShowInlineForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
