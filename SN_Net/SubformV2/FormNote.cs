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
    public partial class FormNote : Form
    {
        public enum TRNTYP
        {
            TEL,
            BREAK,
            TRAIN
        }
        private FORM_MODE form_mode;
        private MainForm main_form;
        private BindingList<noteVM> note_list;
        private DateTime? curr_date;
        private users curr_user;
        private note tmp_note;
        private TRNTYP trn_typ;
        private Timer timer;

        public FormNote(MainForm main_form, DateTime? note_date = null, users note_user = null)
        {
            this.main_form = main_form;
            this.curr_date = note_date;
            this.curr_user = note_user;
            InitializeComponent();
        }

        private void FormNote_Load(object sender, EventArgs e)
        {
            this.HideInlineForm();
            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.RefreshForm();
            this.SetDropdownListItem();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(this.timer != null)
            {
                this.timer.Stop();
                this.timer.Enabled = false;
                this.timer.Dispose();
                this.timer = null;
            }
            this.main_form.form_note = null;
            base.OnClosing(e);
        }

        public static List<note> GetNote(users user, DateTime? note_date)
        {
            if (user == null)
                return null;

            using (sn_noteEntities sn_note = DBXNote.DataSet())
            {
                if (note_date.HasValue)
                {
                    // Only current date
                    var d = new DateTime(note_date.Value.Year, note_date.Value.Month, note_date.Value.Day, 0, 0, 0);
                    return sn_note.note.Where(n => n.users_name == user.username && n.date.CompareTo(d) == 0).OrderBy(n => n.date).ThenBy(n => n.start_time).ToList();
                }
                else
                {
                    // All date
                    return sn_note.note.Where(n => n.users_name == user.username).OrderBy(n => n.date).ThenBy(n => n.start_time).ToList();
                }
            }
        }

        private void SetDropdownListItem()
        {
            this.inlineTrainType._Items.Add(new XDropdownListItem { Text = "", Value = null });
            this.inlineTrainType._Items.Add(new XDropdownListItem { Text = "วิทยากร", Value = NoteReason.TRAINING_TRAINER });
            this.inlineTrainType._Items.Add(new XDropdownListItem { Text = "ผู้ช่วย", Value = NoteReason.TRAINING_ASSIST });

            this.inlineBreakType._Items.Add(new XDropdownListItem { Text = "", Value = null });
            this.inlineBreakType._Items.Add(new XDropdownListItem { Text = "เข้าห้องน้ำ", Value = NoteReason.TOILET });
            this.inlineBreakType._Items.Add(new XDropdownListItem { Text = "ทำใบเสนอราคา", Value = NoteReason.QT });
            this.inlineBreakType._Items.Add(new XDropdownListItem { Text = "ลูกค้ามาพบ", Value = NoteReason.MEET_CUST });
            this.inlineBreakType._Items.Add(new XDropdownListItem { Text = "แก้ไขข้อมูลให้ลูกค้า", Value = NoteReason.CORRECT_DATA });
            this.inlineBreakType._Items.Add(new XDropdownListItem { Text = "อื่น ๆ", Value = NoteReason.OTHER });
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.btnAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnStop.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnSave.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnSearch.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM, FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnWorkingDate.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.dgvNote.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
        }

        private void FillForm()
        {
            this.lblWorkingDate.Text = this.curr_date.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH"));
            this.dgvNote.DataSource = this.note_list;
        }

        private void RefreshForm()
        {
            using (BackgroundWorker wrk = new BackgroundWorker())
            {
                this.ShowLoadingBox();

                wrk.DoWork += delegate
                {
                    this.note_list = null;
                    if(this.curr_user != null && this.curr_date.HasValue)
                    {
                        this.note_list = new BindingList<noteVM>(GetNote(this.curr_user, this.curr_date).ToViewModel());
                    }
                    else
                    {
                        this.note_list = new BindingList<noteVM>();
                    }
                };

                wrk.RunWorkerCompleted += delegate
                {
                    if(this.note_list != null)
                        this.FillForm();

                    this.HideLoadingBox();
                };

                wrk.RunWorkerAsync();
            }
        }

        private void HideInlineForm()
        {
            this.inlineStart.SetBounds(-99999, 0, 0, 0);
            this.inlineEnd.SetBounds(-99999, 0, 0, 0);
            this.inlineDuration.SetBounds(-99999, 0, 0, 0);
            this.inlineSernum.SetBounds(-99999, 0, 0, 0);
            this.inlineContact.SetBounds(-99999, 0, 0, 0);
            this.inlineMapdrive.SetBounds(-99999, 0, this.inlineMapdrive.Width, this.inlineMapdrive.Height);
            this.inlineInstall.SetBounds(-99999, 0, this.inlineInstall.Width, this.inlineInstall.Height);
            this.inlineError.SetBounds(-99999, 0, this.inlineError.Width, this.inlineError.Height);
            this.inlineFont.SetBounds(-99999, 0, this.inlineFont.Width, this.inlineFont.Height);
            this.inlinePrint.SetBounds(-99999, 0, this.inlinePrint.Width, this.inlinePrint.Height);
            this.inlineTrain.SetBounds(-99999, 0, this.inlineTrain.Width, this.inlineTrain.Height);
            this.inlineStock.SetBounds(-99999, 0, this.inlineStock.Width, this.inlineStock.Height);
            this.inlineForm.SetBounds(-99999, 0, this.inlineForm.Width, this.inlineForm.Height);
            this.inlineReportExcel.SetBounds(-99999, 0, this.inlineReportExcel.Width, this.inlineReportExcel.Height);
            this.inlineStatement.SetBounds(-99999, 0, this.inlineStatement.Width, this.inlineStatement.Height);
            this.inlineAsset.SetBounds(-99999, 0, this.inlineAsset.Width, this.inlineAsset.Height);
            this.inlineSecure.SetBounds(-99999, 0, this.inlineSecure.Width, this.inlineSecure.Height);
            this.inlineYearend.SetBounds(-99999, 0, this.inlineYearend.Width, this.inlineYearend.Height);
            this.inlinePeriod.SetBounds(-99999, 0, this.inlinePeriod.Width, this.inlinePeriod.Height);
            this.inlineMail.SetBounds(-99999, 0, this.inlineMail.Width, this.inlineMail.Height);
            this.inlineTransfer.SetBounds(-99999, 0, this.inlineTransfer.Width, this.inlineTransfer.Height);
            this.inlineOther.SetBounds(-99999, 0, this.inlineOther.Width, this.inlineOther.Height);
            this.inlineRemark.SetBounds(-99999, 0, 0, 0);
            this.inlineTrainType.SetBounds(-99999, 0, 0, 0);
            this.inlineBreakType.SetBounds(-99999, 0, 0, 0);

            this.inlineStart.Enabled = false;
            this.inlineEnd.Enabled = false;
            this.inlineSernum._ReadOnly = true;
            this.inlineContact._ReadOnly = true;
            this.inlineMapdrive._ReadOnly = true;
            this.inlineInstall._ReadOnly = true;
            this.inlineError._ReadOnly = true;
            this.inlineFont._ReadOnly = true;
            this.inlinePrint._ReadOnly = true;
            this.inlineTrain._ReadOnly = true;
            this.inlineStock._ReadOnly = true;
            this.inlineForm._ReadOnly = true;
            this.inlineReportExcel._ReadOnly = true;
            this.inlineStatement._ReadOnly = true;
            this.inlineAsset._ReadOnly = true;
            this.inlineSecure._ReadOnly = true;
            this.inlineYearend._ReadOnly = true;
            this.inlinePeriod._ReadOnly = true;
            this.inlineMail._ReadOnly = true;
            this.inlineTransfer._ReadOnly = true;
            this.inlineOther._ReadOnly = true;
            this.inlineRemark._ReadOnly = true;
            this.inlineTrainType._ReadOnly = true;
            this.inlineBreakType._ReadOnly = true;

            this.tmp_note = null;
        }

        private void ShowInlineForm(DataGridViewRow row)
        {
            this.tmp_note = (note)row.Cells[this.col_note_note.Name].Value;

            this.SetInlineFormPosition(row);

            var note_vm = this.tmp_note.ToViewModel();
            this.inlineStart.Text = note_vm.start_time;
            this.inlineEnd.Text = note_vm.end_time;
            this.inlineDuration.Text = note_vm.duration;
            this.inlineSernum._Text = note_vm.sernum;
            this.inlineContact._Text = note_vm.contact;
            this.inlineMapdrive._Checked = note_vm.is_mapdrive ? true : false;
            this.inlineInstall._Checked = note_vm.is_installupdate ? true : false;
            this.inlineError._Checked = note_vm.is_error ? true : false;
            this.inlineFont._Checked = note_vm.is_font ? true : false;
            this.inlinePrint._Checked = note_vm.is_print ? true : false;
            this.inlineTrain._Checked = note_vm.is_training ? true : false;
            this.inlineStock._Checked = note_vm.is_stock ? true : false;
            this.inlineForm._Checked = note_vm.is_form ? true : false;
            this.inlineReportExcel._Checked = note_vm.is_reportexcel ? true : false;
            this.inlineStatement._Checked = note_vm.is_statement ? true : false;
            this.inlineAsset._Checked = note_vm.is_asset ? true : false;
            this.inlineSecure._Checked = note_vm.is_secure ? true : false;
            this.inlineYearend._Checked = note_vm.is_yearend ? true : false;
            this.inlinePeriod._Checked = note_vm.is_period ? true : false;
            this.inlineMail._Checked = note_vm.is_mail ? true : false;
            this.inlineTransfer._Checked = note_vm.is_transfer ? true : false;
            this.inlineOther._Checked = note_vm.is_other ? true : false;
            this.inlineRemark._Text = note_vm.remark;

            if(this.trn_typ == TRNTYP.BREAK)
            {
                var selected = this.inlineBreakType._Items.Cast<XDropdownListItem>().Where(i => (string)i.Value == this.tmp_note.reason).FirstOrDefault();

                if(selected != null)
                {
                    this.inlineBreakType._SelectedItem = selected;
                }
                else
                {
                    this.inlineBreakType._SelectedItem = this.inlineBreakType._Items.Cast<XDropdownListItem>().Where(i => i.Value == null).First();
                }
            }

            if(this.trn_typ == TRNTYP.TRAIN)
            {
                var selected = this.inlineTrainType._Items.Cast<XDropdownListItem>().Where(i => (string)i.Value == this.tmp_note.reason).FirstOrDefault();

                if (selected != null)
                {
                    this.inlineTrainType._SelectedItem = selected;
                }
                else
                {
                    this.inlineTrainType._SelectedItem = this.inlineTrainType._Items.Cast<XDropdownListItem>().Where(i => i.Value == null).First();
                }
            }
        }

        private void SetInlineFormPosition(DataGridViewRow row)
        {
            this.inlineStart.Enabled = this.form_mode == FORM_MODE.EDIT_ITEM || (this.form_mode == FORM_MODE.ADD_ITEM && this.trn_typ == TRNTYP.TRAIN) ? true : false;
            this.inlineEnd.Enabled = this.form_mode == FORM_MODE.EDIT_ITEM || (this.form_mode == FORM_MODE.ADD_ITEM && this.trn_typ == TRNTYP.TRAIN) ? true : false;

            int col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_start.Name).First().Index;
            this.inlineStart.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_end.Name).First().Index;
            this.inlineEnd.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_duration.Name).First().Index;
            this.inlineDuration.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            if(this.trn_typ == TRNTYP.TEL)
            {
                this.inlineSernum._ReadOnly = false;
                this.inlineContact._ReadOnly = false;
                this.inlineMapdrive._ReadOnly = false;
                this.inlineInstall._ReadOnly = false;
                this.inlineError._ReadOnly = false;
                this.inlineFont._ReadOnly = false;
                this.inlinePrint._ReadOnly = false;
                this.inlineTrain._ReadOnly = false;
                this.inlineStock._ReadOnly = false;
                this.inlineForm._ReadOnly = false;
                this.inlineReportExcel._ReadOnly = false;
                this.inlineStatement._ReadOnly = false;
                this.inlineAsset._ReadOnly = false;
                this.inlineSecure._ReadOnly = false;
                this.inlineYearend._ReadOnly = false;
                this.inlinePeriod._ReadOnly = false;
                this.inlineMail._ReadOnly = false;
                this.inlineTransfer._ReadOnly = false;
                this.inlineOther._ReadOnly = false;
                this.inlineRemark._ReadOnly = false;

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_sernum.Name).First().Index;
                this.inlineSernum.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_contact.Name).First().Index;
                this.inlineContact.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_mapdrive.Name).First().Index;
                this.inlineMapdrive.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_installupdate.Name).First().Index;
                this.inlineInstall.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_error.Name).First().Index;
                this.inlineError.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_installfonts.Name).First().Index;
                this.inlineFont.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_print.Name).First().Index;
                this.inlinePrint.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_training.Name).First().Index;
                this.inlineTrain.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_stock.Name).First().Index;
                this.inlineStock.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_form.Name).First().Index;
                this.inlineForm.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_reportexcel.Name).First().Index;
                this.inlineReportExcel.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_statement.Name).First().Index;
                this.inlineStatement.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_assets.Name).First().Index;
                this.inlineAsset.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_secure.Name).First().Index;
                this.inlineSecure.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_yearend.Name).First().Index;
                this.inlineYearend.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_period.Name).First().Index;
                this.inlinePeriod.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_mail.Name).First().Index;
                this.inlineMail.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_transfer.Name).First().Index;
                this.inlineTransfer.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_other.Name).First().Index; ;
                this.inlineOther.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_remark.Name).First().Index;
                this.inlineRemark.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                return;
            }

            if(this.trn_typ == TRNTYP.BREAK)
            {
                this.inlineBreakType._ReadOnly = false;
                this.inlineRemark._ReadOnly = false;

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_contact.Name).First().Index;
                this.inlineBreakType.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_remark.Name).First().Index;
                this.inlineRemark.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                return;
            }

            if(this.trn_typ == TRNTYP.TRAIN)
            {
                this.inlineTrainType._ReadOnly = false;

                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_contact.Name).First().Index;
                this.inlineTrainType.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                return;
            }
        }

        private void dgvNote_Resize(object sender, EventArgs e)
        {
            if (((XDatagrid)sender).CurrentCell == null)
                return;

            if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.SetInlineFormPosition(((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex]);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.curr_date.HasValue || (this.curr_date.Value.Date != DateTime.Now.Date))
            {
                //if (MessageAlert.Show("โปรแกรมจะกำหนดวันที่ของรายการเป็นวันที่ปัจจุบัน, ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                //    return;

                //this.curr_date = DateTime.Now;
                //this.RefreshForm();
                //MessageAlert.Show("")
            }

            DialogNoteType note_type = new DialogNoteType();
            if (note_type.ShowDialog() != DialogResult.OK)
                return;

            this.trn_typ = note_type.trn_typ;

            noteVM n = new note
            {
                id = -1,
                date = this.curr_date.Value, //DateTime.Now.Date,
                sernum = string.Empty,
                contact = string.Empty,
                start_time = DateTime.Now.ToString("HH:mm:ss", CultureInfo.GetCultureInfo("th-TH")),
                end_time = DateTime.Now.ToString("HH:mm:ss", CultureInfo.GetCultureInfo("th-TH")),
                duration = "00:00:00",
                is_break = this.trn_typ == TRNTYP.BREAK || this.trn_typ == TRNTYP.TRAIN ? "Y" : "N",
                file_path = string.Empty,
                problem = string.Empty,
                reason = string.Empty,
                remark = string.Empty,
                users_id = this.main_form.loged_in_user.id,
                users_name = this.main_form.loged_in_user.username,
                rec_by = this.main_form.loged_in_user.username
            }.ToViewModel();

            ((BindingList<noteVM>)this.dgvNote.DataSource).Add(n);
            var row = this.dgvNote.Rows.Cast<DataGridViewRow>().Where(r => ((note)r.Cells[this.col_note_note.Name].Value).id == -1).First();
            row.Cells[this.col_note_start.Name].Selected = true;

            this.ResetFormState(FORM_MODE.ADD_ITEM);
            this.ShowInlineForm(row);

            if(this.trn_typ == TRNTYP.TEL || this.trn_typ == TRNTYP.BREAK)
            {
                this.timer = new Timer();
                this.timer.Interval = 1000;
                this.timer.Tick += delegate
                {
                    this.inlineEnd.Text = DateTime.Now.ToString("HH:mm:ss", CultureInfo.GetCultureInfo("th-TH"));
                };
                this.timer.Enabled = true;
                this.timer.Start();
            }

            if (this.trn_typ == TRNTYP.TEL)
            {
                this.inlineSernum.Focus();
                return;
            }

            if(this.trn_typ == TRNTYP.BREAK)
            {
                this.inlineBreakType.Focus();
                return;
            }

            if(this.trn_typ == TRNTYP.TRAIN)
            {
                this.inlineStart.Focus();
                return;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvNote.CurrentCell == null)
                return;

            note note = (note)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[this.col_note_note.Name].Value;
            if (note.is_break == "N")
            {
                this.trn_typ = TRNTYP.TEL;
            }
            else
            {
                if (note.reason.Contains(NoteReason.TRAINING_ASSIST) || note.reason.Contains(NoteReason.TRAINING_TRAINER))
                {
                    this.trn_typ = TRNTYP.TRAIN;
                }
                else
                {
                    this.trn_typ = TRNTYP.BREAK;
                }
            }

            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm(this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex]);

            this.inlineStart.Focus();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.lblCompnam.Text = string.Empty;
            this.lblAddr.Text = string.Empty;
            this.lblVersion.Text = string.Empty;
            this.lblPassword.Text = string.Empty;
            this.lblMA.Text = string.Empty;
            this.lblCloud.Text = string.Empty;

            this.HideInlineForm();

            if (this.form_mode == FORM_MODE.ADD_ITEM)
            {
                ((BindingList<noteVM>)this.dgvNote.DataSource).Remove(((BindingList<noteVM>)this.dgvNote.DataSource).Where(i => i.note.id
                 == -1).First());
            }
            if(this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                note curr_note = (note)this.dgvNote.Rows[this.dgvNote.CurrentCell.RowIndex].Cells[this.col_note_note.Name].Value;
                note note = null;
                using (sn_noteEntities sn_note = DBXNote.DataSet())
                {
                    note = sn_note.note.Find(curr_note.id);
                }
                
                if(note != null)
                {
                    ((BindingList<noteVM>)this.dgvNote.DataSource).Where(n => n.note.id == curr_note.id).First().note = note;
                }
            }

            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.dgvNote.Focus();
            if(this.timer != null)
            {
                this.timer.Stop();
                this.timer.Enabled = false;
                this.timer.Dispose();
                this.timer = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tmp_note == null)
                return;

            if(this.form_mode == FORM_MODE.ADD_ITEM)
            {
                if((this.trn_typ == TRNTYP.BREAK || this.trn_typ == TRNTYP.TRAIN) && (this.tmp_note.reason == null || this.tmp_note.reason.Trim().Length == 0))
                {
                    if(this.trn_typ == TRNTYP.BREAK)
                    {
                        this.inlineBreakType.Focus();
                        SendKeys.Send("{F6}");
                        return;
                    }
                    if(this.trn_typ == TRNTYP.TRAIN)
                    {
                        this.inlineTrainType.Focus();
                        SendKeys.Send("{F6}");
                        return;
                    }
                }

                if(this.trn_typ == TRNTYP.BREAK && this.tmp_note.reason == NoteReason.OTHER && this.tmp_note.remark.Trim().Length == 0)
                {
                    MessageAlert.Show("กรุณาระบุเหตุผลในช่องหมายเหตุ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    this.inlineRemark.Focus();
                    return;
                }

                using (sn_noteEntities sn_note = DBXNote.DataSet())
                {
                    try
                    {
                        sn_note.note.Add(this.tmp_note);
                        sn_note.SaveChanges();

                        this.HideInlineForm();
                        this.ResetFormState(FORM_MODE.READ_ITEM);

                        this.dgvNote.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.InnerException.InnerException.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }

                if (this.timer != null)
                {
                    this.timer.Stop();
                    this.timer.Enabled = false;
                    this.timer.Dispose();
                    this.timer = null;
                }
                return;
            }
            
            if(this.form_mode == FORM_MODE.EDIT_ITEM)
            {

                if (this.timer != null)
                {
                    this.timer.Stop();
                    this.timer.Enabled = false;
                    this.timer.Dispose();
                    this.timer = null;
                }
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnWorkingDate_Click(object sender, EventArgs e)
        {
            DialogNoteChangeScope scope = new DialogNoteChangeScope(this.main_form, this.curr_user, this.curr_date);
            if(scope.ShowDialog() == DialogResult.OK)
            {
                this.curr_user = scope.selected_user;
                this.curr_date = scope.selected_date;
                this.RefreshForm();
            }
        }

        private void dgvNote_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                if (e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_seq.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_start.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_end.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_duration.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_sernum.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_contact.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_remark.Name).First().Index)
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 9.75f);
                }
                else
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 6.75f);
                }
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                e.Handled = true;
            }

            if(e.RowIndex > -1)
            {
                var countable_item = ((BindingList<noteVM>)((XDatagrid)sender).DataSource).ToList().Where(i => i.note.is_break == "N").ToList();
                var curr_item_is_countable = countable_item.Where(i => i.note.id == ((note)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_note_note.Name].Value).id).FirstOrDefault();

                if (curr_item_is_countable == null)
                {
                    e.CellStyle.BackColor = Color.WhiteSmoke;
                    e.CellStyle.SelectionBackColor = Color.WhiteSmoke;
                    e.CellStyle.ForeColor = Color.Gray;
                    e.CellStyle.SelectionForeColor = Color.Gray;
                }

                if (e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_seq.Name).First().Index)
                {
                    //((XDatagrid)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = e.RowIndex + 1;
                    
                    if(curr_item_is_countable != null)
                    {
                        var seq = countable_item.IndexOf(curr_item_is_countable) + 1;
                        ((XDatagrid)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = seq.ToString();
                    }
                    else
                    {
                        ((XDatagrid)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                    }
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    e.Handled = true;
                    return;
                }
                else if (e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_mapdrive.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_installupdate.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_error.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_installfonts.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_print.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_training.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_stock.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_form.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_reportexcel.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_statement.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_assets.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_secure.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_yearend.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_period.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_mail.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_transfer.Name).First().Index
                    || e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_note_is_other.Name).First().Index
                    )
                {
                    using (SolidBrush b = new SolidBrush(e.CellStyle.ForeColor))
                    {
                        e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground);
                        if ((bool)((XDatagrid)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value == true)
                        {
                            e.Graphics.DrawString("\u2713", e.CellStyle.Font, b, new Point(e.CellBounds.X + 6, e.CellBounds.Y + 4));
                        }
                        e.Handled = true;
                        return;
                    }
                }
                else
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    e.Handled = true;
                    return;
                }

            }
        }

        private void inlineStart_ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
                this.tmp_note.start_time = ((XTimePicker)sender).Text;
        }

        private void inlineEnd_ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                this.tmp_note.end_time = ((XTimePicker)sender).Text;
                this.inlineDuration.Text = (this.tmp_note.start_time.GetDifTimeInDate(this.tmp_note.end_time)).ToString(@"hh\:mm\:ss", CultureInfo.GetCultureInfo("th-TH"));
            }
        }

        private void inlineDuration_TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
                this.tmp_note.duration = ((Label)sender).Text;
        }

        private void inlineSernum__Leave(object sender, EventArgs e)
        {
            if (this.tmp_note == null)
                return;

            this.tmp_note.sernum = ((XTextEditMasked)sender)._Text.Trim();

            if (this.tmp_note.is_break == "Y")
                return;

            using (snEntities sn = DBX.DataSet())
            {
                var ser = sn.serial.Include("serial_password").Include("ma").Include("cloud_srv").Include("istab3").Where(s => s.flag == 0 && s.sernum.Trim() == ((XTextEditMasked)sender)._Text.Trim()).FirstOrDefault();
                if(ser == null)
                {
                    this.lblCompnam.Text = string.Empty;
                    this.lblAddr.Text = string.Empty;
                    this.lblVersion.Text = string.Empty;
                    this.lblPassword.Text = string.Empty;
                    this.lblMA.Text = string.Empty;
                    this.lblCloud.Text = string.Empty;
                    MessageAlert.Show("ค้นหา S/N " + ((XTextEditMasked)sender)._Text.Trim() + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                }
                else
                {
                    this.lblCompnam.Text = ser.prenam + " " + ser.compnam;
                    this.lblAddr.Text = ser.addr01 + " " + ser.addr02 + " " + ser.addr03 + " " + ser.zipcod;
                    this.lblVersion.Text = ser.version + " " + (ser.verext_id.HasValue ? ser.istab3.typdes_th : string.Empty);
                    this.lblPassword.Text = string.Join(" , ", ser.serial_password.Select(p => p.pass_word).ToArray());
                    var ma = ser.ma.ToList().Where(m => m.end_date.Value.CompareTo(DateTime.Now) > 0).OrderBy(m => m.end_date).FirstOrDefault();
                    if (ma != null)
                    {
                        this.lblMA.Text = "Yes (exp. " + ma.end_date.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH")) + ")";
                    }
                    var cloud = ser.cloud_srv.ToList().Where(c => c.end_date.Value.CompareTo(DateTime.Now) > 0).OrderBy(m => m.end_date).FirstOrDefault();
                    if(cloud != null)
                    {
                        this.lblCloud.Text = "Yes (exp. " + cloud.end_date.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH")) + ")";
                    }
                }
            }
        }

        private void inlineContact__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
                this.tmp_note.contact = ((XTextEdit)sender)._Text.Trim();
        }

        private void inlineMapdrive__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.MAP_DRIVE;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.MAP_DRIVE, "");
                }
            }
        }

        private void inlineInstall__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.INSTALL_UPDATE;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.INSTALL_UPDATE, "");
                }
            }
        }

        private void inlineError__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.ERROR;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.ERROR, "");
                }
            }
        }

        private void inlineFont__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.FONTS;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.FONTS, "");
                }
            }
        }

        private void inlinePrint__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.PRINT;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.PRINT, "");
                }
            }
        }

        private void inlineTrain__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.TRAINING;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.TRAINING, "");
                }
            }
        }

        private void inlineStock__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.STOCK;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.STOCK, "");
                }
            }
        }

        private void inlineForm__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.EDIT_FORM;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.EDIT_FORM, "");
                }
            }
        }

        private void inlineReportExcel__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.REPORT_EXCEL;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.REPORT_EXCEL, "");
                }
            }
        }

        private void inlineStatement__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.STATEMENT;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.STATEMENT, "");
                }
            }
        }

        private void inlineAsset__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.ASSETS;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.ASSETS, "");
                }
            }
        }

        private void inlineSecure__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.SECURE;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.SECURE, "");
                }
            }
        }

        private void inlineYearend__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.YEAR_END;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.YEAR_END, "");
                }
            }
        }

        private void inlinePeriod__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.PERIOD;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.PERIOD, "");
                }
            }
        }

        private void inlineMail__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.MAIL_WAIT;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.MAIL_WAIT, "");
                }
            }
        }

        private void inlineTransfer__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.TRANSFER_MKT;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.TRANSFER_MKT, "");
                }
            }
        }

        private void inlineOther__CheckedChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                if (((XCheckBox)sender)._Checked)
                {
                    this.tmp_note.problem += NoteProblem.OTHER;
                    this.tmp_note.remark = "{problem}" + this.tmp_note.remark;
                }
                else
                {
                    this.tmp_note.problem = this.tmp_note.problem.Replace(NoteProblem.OTHER, "");
                    this.tmp_note.remark = this.tmp_note.remark.Replace("{problem}", "");
                }
            }
        }

        private void inlineRemark__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
                this.tmp_note.remark = ((XTextEdit)sender)._Text;
        }

        private void inlineTrainType__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
                this.tmp_note.reason = (string)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void inlineBreakType__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_note != null)
            {
                string note_reason = (string)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
                this.tmp_note.reason = note_reason;
            }
        }

        private void dgvNote_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                if(row_index > -1)
                {
                    ((XDatagrid)sender).Rows[row_index].Cells[this.col_note_start.Name].Selected = true;
                }

                ContextMenu cm = new ContextMenu();
                MenuItem m_add = new MenuItem("Add <Alt+A>");
                m_add.Click += delegate
                {
                    this.btnAdd.PerformClick();
                };
                cm.MenuItems.Add(m_add);

                MenuItem m_edit = new MenuItem("Edit <Alt+E>");
                m_edit.Click += delegate
                {
                    this.btnEdit.PerformClick();
                };
                m_edit.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(m_edit);

                cm.Show(((XDatagrid)sender), new Point(e.X, e.Y));
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if (this.trn_typ == TRNTYP.BREAK)
                    {
                        if (this.inlineBreakType._Focused)
                        {
                            this.inlineRemark.Focus();
                            return true;
                        }
                        if (this.inlineRemark._Focused)
                        {
                            this.btnSave.PerformClick();
                            return true;
                        }
                    }

                    if (this.trn_typ == TRNTYP.TRAIN)
                    {
                        if (this.inlineTrainType._Focused)
                        {
                            this.btnSave.PerformClick();
                            return true;
                        }
                    }
                    if (this.trn_typ == TRNTYP.TEL)
                    {
                        if (this.inlineRemark._Focused)
                        {
                            this.btnSave.PerformClick();
                            return true;
                        }
                    }

                    SendKeys.Send("{TAB}");
                    return true;
                }
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                this.btnAdd.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.E))
            {
                this.btnEdit.PerformClick();
                return true;
            }

            if (keyData == Keys.Escape)
            {
                this.btnStop.PerformClick();
                return true;
            }

            if (keyData == Keys.F9)
            {
                this.btnSave.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.S))
            {
                this.btnSearch.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.G))
            {
                this.btnWorkingDate.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
