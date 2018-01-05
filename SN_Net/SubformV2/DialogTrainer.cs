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
        private CustomDateEvent3 custom_date_event;
        //private DateTime curr_date;
        private BindingList<training_calendarVM> trainers;
        private BindingList<trainer_monthly_statVM> stats;
        private training_calendar tmp_train;
        private FORM_MODE form_mode;

        public DialogTrainer(MainForm main_form, CustomDateEvent3 custom_date_event)
        {
            this.main_form = main_form;
            this.custom_date_event = custom_date_event;
            //this.curr_date = custom_date_event.curr_date;
            InitializeComponent();
        }

        private void DialogTrainer_Load(object sender, EventArgs e)
        {
            this.RemoveInlineForm();
            this.ResetControlState(FORM_MODE.READ_ITEM);
            this.Text += this.custom_date_event.curr_date.ToString(" dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH"));
            this.dtDate.Value = this.custom_date_event.curr_date;
            this.GetTrainerData();
            this.dgvTrainer.DataSource = this.trainers;
            this.dgvStat.DataSource = this.stats;
            this.SetDropDownListItem();
            this.ActiveControl = this.dgvTrainer;
        }

        private void GetTrainerData()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                this.trainers = new BindingList<training_calendarVM>(note.training_calendar.Where(t => t.date == this.custom_date_event.curr_date).OrderBy(t => t.term).ThenBy(t => t.status).ToViewModel());
            }

            using (snEntities db = DBX.DataSet())
            {
                this.stats = new BindingList<trainer_monthly_statVM>(db.users.Where(u => u.training_expert == "Y").OrderBy(u => u.username).ToViewModelTrainerStat(this.custom_date_event.curr_date));
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

            this.dgvTrainer.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
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
            this.SetInlineControlPosition();
        }

        private void SetInlineControlPosition()
        {
            int col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_course_type.Name).First().Index;
            this.inlineCourseType.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);
            this.inlineCourseType._SelectedItem = this.inlineCourseType._Items.Cast<XDropdownListItem>().Where(i => (int)i.Value == this.tmp_train.course_type).FirstOrDefault();

            col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_code_name.Name).First().Index;
            this.inlineTrainer.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);
            var selected_trainer = this.inlineTrainer._Items.Cast<XDropdownListItem>().Where(i => (string)i.Value == this.tmp_train.trainer).FirstOrDefault();
            if (selected_trainer != null)
                this.inlineTrainer._SelectedItem = selected_trainer;

            col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_status.Name).First().Index;
            this.inlineStatus.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);
            this.inlineStatus._SelectedItem = this.inlineStatus._Items.Cast<XDropdownListItem>().Where(i => (int)i.Value == this.tmp_train.status).FirstOrDefault();

            col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_term.Name).First().Index;
            this.inlineTerm.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);
            this.inlineTerm._SelectedItem = this.inlineTerm._Items.Cast<XDropdownListItem>().Where(i => (int)i.Value == this.tmp_train.term).FirstOrDefault();

            col_index = this.dgvTrainer.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_remark.Name).First().Index;
            this.inlineRemark.SetInlineControlPosition(this.dgvTrainer, this.dgvTrainer.CurrentCell.RowIndex, col_index);
            this.inlineRemark._Text = this.tmp_train.remark;
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

                MenuItem mnu_delete = new MenuItem("ลบ");
                mnu_delete.Click += delegate
                {
                    this.btnDelete.PerformClick();
                };
                mnu_delete.Enabled = row_index == -1 ? false : true;
                cm.MenuItems.Add(mnu_delete);

                cm.Show(((XDatagrid)sender), new Point(e.X, e.Y));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ((BindingList<training_calendarVM>)this.dgvTrainer.DataSource).Add(new training_calendar
            {
                id = -1,
                course_type = 1,
                date = this.custom_date_event.curr_date,
                status = 1,
                term = 1,
                trainer = string.Empty,
                remark = string.Empty,
                rec_by = this.main_form.loged_in_user.username
            }.ToViewModel());

            this.dgvTrainer.Rows.Cast<DataGridViewRow>().Where(r => ((training_calendar)r.Cells[this.col_training_calendar.Name].Value).id == -1).First().Cells[this.col_course_type.Name].Selected = true;
            this.ResetControlState(FORM_MODE.ADD_ITEM);
            this.ShowInlineForm();
            this.inlineCourseType.Focus();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.ResetControlState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm();
            this.inlineCourseType.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (sn_noteEntities note = DBXNote.DataSet())
                {
                    if(this.form_mode == FORM_MODE.ADD_ITEM)
                    {
                        note.training_calendar.Add(this.tmp_train);
                        note.SaveChanges();
                        this.RemoveInlineForm();
                        this.ResetControlState(FORM_MODE.READ_ITEM);
                        this.GetTrainerData();
                        this.dgvTrainer.DataSource = this.trainers;
                        this.dgvStat.DataSource = this.stats;
                        this.btnAdd.PerformClick();
                    }
                    else
                    {
                        var t = note.training_calendar.Find(this.tmp_train.id);
                        if(t != null)
                        {
                            t.course_type = this.tmp_train.course_type;
                            t.date = this.tmp_train.date;
                            t.trainer = this.tmp_train.trainer;
                            t.status = this.tmp_train.status;
                            t.term = this.tmp_train.term;
                            t.remark = this.tmp_train.remark;
                            t.rec_by = this.tmp_train.rec_by;

                            note.SaveChanges();
                            this.RemoveInlineForm();
                            this.ResetControlState(FORM_MODE.READ_ITEM);
                        }
                        else
                        {
                            MessageAlert.Show("รายการที่ต้องการแก้ไขไม่มีอยู่ในระบบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        }
                    }

                    this.custom_date_event.RefreshView();
                }
            }
            catch (Exception ex)
            {
                if(MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                {
                    this.btnSave.PerformClick();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.RemoveInlineForm();
                this.ResetControlState(FORM_MODE.READ_ITEM);
                this.GetTrainerData();
                this.dgvTrainer.DataSource = this.trainers;
                this.dgvStat.DataSource = this.stats;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvTrainer.CurrentCell == null)
                return;

            this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].DrawDeletingRowOverlay();

            if(MessageAlert.Show("ลบรายการที่เลือก, ทำต่อหรือไม่", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (sn_noteEntities note = DBXNote.DataSet())
                {
                    var t = note.training_calendar.Find(((training_calendar)this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].Cells[this.col_training_calendar.Name].Value).id);
                    if (t != null)
                    {
                        note.training_calendar.Remove(t);
                        note.SaveChanges();
                        this.GetTrainerData();
                        this.dgvTrainer.DataSource = this.trainers;
                        this.dgvStat.DataSource = this.stats;
                        this.custom_date_event.RefreshView();
                    }
                    else
                    {
                        MessageAlert.Show("ค้นหารายการที่ต้องการลบไม่พบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                    }
                }
            }
            else
            {
                this.dgvTrainer.Rows[this.dgvTrainer.CurrentCell.RowIndex].ClearDeletingRowOverlay();
            }
        }

        private void inlineCourseType__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_train != null)
                this.tmp_train.course_type = (int)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void inlineTrainer__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_train != null)
                this.tmp_train.trainer = (string)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void inlineStatus__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_train != null)
                this.tmp_train.status = (int)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void inlineTerm__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_train != null)
                this.tmp_train.term = (int)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void inlineRemark__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_train != null)
                this.tmp_train.remark = ((XTextEdit)sender)._Text;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if (this.inlineRemark._Focused)
                    {
                        this.btnSave.PerformClick();
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                    }
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    this.btnCancel.PerformClick();
                }
                else
                {
                    this.Close();
                }
                return true;
            }

            if (keyData == Keys.F7)
            {
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.tabControl1.SelectedTab = this.tabPage2;
                    return true;
                }
            }

            if (keyData == Keys.F8)
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.tabControl1.SelectedTab = this.tabPage1;
                    return true;
                }
            }

            if (keyData == Keys.F9)
            {
                this.btnSave.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                this.btnAdd.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.E))
            {
                this.btnEdit.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                e.Cancel = true;
            }
        }

        private void dgvTrainer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                if (row_index == -1)
                {
                    this.btnAdd.PerformClick();
                }
                else
                {
                    this.btnEdit.PerformClick();
                }
            }
        }

        private void dgvTrainer_Resize(object sender, EventArgs e)
        {
            if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.SetInlineControlPosition();
            }
        }
    }
}
