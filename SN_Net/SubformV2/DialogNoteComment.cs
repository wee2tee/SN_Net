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
using System.IO;

namespace SN_Net.Subform
{
    public partial class DialogNoteComment : Form
    {
        private FORM_MODE form_mode;
        private note_comment tmp_comment;
        private BindingList<note_commentVM> comments;
        private BindingList<note_commentVM> complains;
        private note current_note;
        private MainForm main_form;
        private NOTE_COMMENT_TYPE comment_type;

        public enum NOTE_COMMENT_TYPE : int
        {
            COMMENT = 1,
            COMPLAIN = 2
        }

        public DialogNoteComment(MainForm main_form, note current_note)
        {
            this.main_form = main_form;
            this.current_note = FormNote.GetNote(current_note.id);
            InitializeComponent();
        }

        private void DialogNoteComment_Load(object sender, EventArgs e)
        {
            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.HideInlineForm();

            this.comments = new BindingList<note_commentVM>(GetComment(this.current_note.id).ToViewModel());
            this.complains = new BindingList<note_commentVM>(GetComplain(this.current_note.id).ToViewModel());

            this.dgvComment.DataSource = this.comments;
            this.dgvComplain.DataSource = this.complains;

            this.brPath._Text = this.current_note != null && this.current_note.file_path != null ? this.current_note.file_path : string.Empty;
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.btnCancel.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.dgvComment.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.dgvComplain.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.inlineComment.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineComplain.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);

            this.btnCommentAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnCommentEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnCommentDelete.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnCommentStop.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnCommentSave.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);

            this.btnComplainAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnComplainEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnComplainDelete.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnComplainStop.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnComplainSave.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);

            this.btnEditPath.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnPlay.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnStopPath.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.btnSavePath.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.brPath.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
        }

        private void FillForm()
        {

        }

        private void ShowInlineForm(DataGridViewRow row, NOTE_COMMENT_TYPE comment_type)
        {
            this.comment_type = comment_type;

            if (this.comment_type == NOTE_COMMENT_TYPE.COMMENT)
            {
                this.tmp_comment = (note_comment)row.Cells[this.col_comment_note_comment.Name].Value;
                this.inlineComment._Text = this.tmp_comment.description;
            }
            if(this.comment_type == NOTE_COMMENT_TYPE.COMPLAIN)
            {
                this.tmp_comment = (note_comment)row.Cells[this.col_complain_note_comment.Name].Value;
                this.inlineComplain._Text = this.tmp_comment.description;
            }

            this.SetInlineControlPosition(row);
        }

        private void SetInlineControlPosition(DataGridViewRow row)
        {
            if (!(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM))
                return;

            int col_index;

            if(this.comment_type == NOTE_COMMENT_TYPE.COMMENT)
            {
                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_comment.Name).First().Index;
                this.inlineComment.SetInlineControlPosition(row.DataGridView, row.Index, col_index);
                return;
            }
            if(this.comment_type == NOTE_COMMENT_TYPE.COMPLAIN)
            {
                col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_complain.Name).First().Index;
                this.inlineComplain.SetInlineControlPosition(row.DataGridView, row.Index, col_index);
                return;
            }
        }

        private void HideInlineForm()
        {
            this.inlineComment.SetBounds(-99999, 0, 0, 0);
            this.inlineComplain.SetBounds(-99999, 0, 0, 0);

            this.tmp_comment = null;
        }

        public static List<note_comment> GetComment(int note_id)
        {
            using (sn_noteEntities sn_note = DBXNote.DataSet())
            {
                return sn_note.note_comment.Where(c => c.note_id == note_id && c.type == (int)NOTE_COMMENT_TYPE.COMMENT).OrderBy(c => c.id).ToList();
            }
        }

        public static List<note_comment> GetComplain(int note_id)
        {
            using (sn_noteEntities sn_note = DBXNote.DataSet())
            {
                return sn_note.note_comment.Where(c => c.note_id == note_id && c.type == (int)NOTE_COMMENT_TYPE.COMPLAIN).OrderBy(c => c.id).ToList();
            }
        }

        private void btnCommentAdd_Click(object sender, EventArgs e)
        {
            note_commentVM nc = new note_comment
            {
                id = -1,
                date = DateTime.Now,
                note_id = this.current_note.id,
                type = (int)NOTE_COMMENT_TYPE.COMMENT,
                file_path = string.Empty,
                description = string.Empty,
                rec_by = this.main_form.loged_in_user.username
            }.ToViewModel();

            ((BindingList<note_commentVM>)this.dgvComment.DataSource).Add(nc);
            DataGridViewRow focused_row = this.dgvComment.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_comment_id.Name].Value == nc.id).FirstOrDefault();
            focused_row.Cells[this.col_comment.Name].Selected = true;

            this.ResetFormState(FORM_MODE.ADD_ITEM);
            this.ShowInlineForm(focused_row, NOTE_COMMENT_TYPE.COMMENT);
            this.inlineComment.Focus();
        }

        private void btnCommentEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvComment.CurrentCell == null)
                return;

            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm(this.dgvComment.Rows[this.dgvComment.CurrentCell.RowIndex], NOTE_COMMENT_TYPE.COMMENT);
            this.inlineComment.Focus();
        }

        private void btnCommentDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvComment.CurrentCell == null)
                return;

            this.dgvComment.Rows[this.dgvComment.CurrentCell.RowIndex].DrawDeletingRowOverlay();
            if (MessageAlert.Show("ลบรายการที่เลือก, ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
            {
                this.dgvComment.Rows[this.dgvComment.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                return;
            }

            bool delete_success = false;
            BackgroundWorker wrk = new BackgroundWorker();
            wrk.DoWork += delegate
            {
                using (sn_noteEntities sn_note = DBXNote.DataSet())
                {
                    try
                    {
                        var comment_to_remove = sn_note.note_comment.Find((int)this.dgvComment.Rows[this.dgvComment.CurrentCell.RowIndex].Cells[this.col_comment_id.Name].Value);
                        if (comment_to_remove != null)
                        {
                            sn_note.note_comment.Remove(comment_to_remove);
                            sn_note.SaveChanges();
                            delete_success = true;
                        }
                        else
                        {
                            MessageAlert.Show("รายการที่ต้องการลบ ไม่มีอยู่ในระบบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        return;
                    }
                }
            };
            wrk.RunWorkerCompleted += delegate
            {
                this.dgvComment.Rows[this.dgvComment.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                if (delete_success)
                {
                    this.comments = null;
                    this.comments = new BindingList<note_commentVM>(GetComment(this.current_note.id).ToViewModel());
                    this.dgvComment.DataSource = this.comments;
                }
            };
            wrk.RunWorkerAsync();
        }

        private void btnCommentStop_Click(object sender, EventArgs e)
        {
            this.HideInlineForm();
            if(this.form_mode == FORM_MODE.ADD_ITEM)
            {
                ((BindingList<note_commentVM>)this.dgvComment.DataSource).Remove(((BindingList<note_commentVM>)this.dgvComment.DataSource).Where(n => n.id == -1).First());
            }

            this.ResetFormState(FORM_MODE.READ_ITEM);
        }

        private void btnCommentSave_Click(object sender, EventArgs e)
        {
            if (this.tmp_comment == null)
                return;

            using (sn_noteEntities sn_note = DBXNote.DataSet())
            {
                try
                {
                    if (this.form_mode == FORM_MODE.ADD_ITEM)
                    {
                        sn_note.note_comment.Add(this.tmp_comment);
                        sn_note.SaveChanges();

                        this.HideInlineForm();
                        this.ResetFormState(FORM_MODE.READ_ITEM);
                        this.comments = null;
                        this.comments = new BindingList<note_commentVM>(GetComment(this.current_note.id).ToViewModel());
                        this.dgvComment.DataSource = this.comments;
                        this.btnCommentAdd.PerformClick();
                    }

                    if (this.form_mode == FORM_MODE.EDIT_ITEM)
                    {
                        var note_to_update = sn_note.note_comment.Find(this.tmp_comment.id);
                        if(note_to_update != null)
                        {
                            note_to_update.date = DateTime.Now;
                            note_to_update.description = this.tmp_comment.description;
                            note_to_update.rec_by = this.main_form.loged_in_user.username;
                        }
                        else
                        {
                            sn_note.note_comment.Add(this.tmp_comment);
                        }

                        sn_note.SaveChanges();
                        this.HideInlineForm();
                        this.ResetFormState(FORM_MODE.READ_ITEM);
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.InnerException.InnerException.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void btnComplainAdd_Click(object sender, EventArgs e)
        {
            note_commentVM nc = new note_comment
            {
                id = -1,
                date = DateTime.Now,
                note_id = this.current_note.id,
                type = (int)NOTE_COMMENT_TYPE.COMPLAIN,
                file_path = string.Empty,
                description = string.Empty,
                rec_by = this.main_form.loged_in_user.username
            }.ToViewModel();

            ((BindingList<note_commentVM>)this.dgvComplain.DataSource).Add(nc);
            DataGridViewRow focused_row = this.dgvComplain.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_complain_id.Name].Value == nc.id).FirstOrDefault();
            focused_row.Cells[this.col_complain.Name].Selected = true;

            this.ResetFormState(FORM_MODE.ADD_ITEM);
            this.ShowInlineForm(focused_row, NOTE_COMMENT_TYPE.COMPLAIN);
            this.inlineComplain.Focus();
        }

        private void btnComplainEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvComplain.CurrentCell == null)
                return;

            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm(this.dgvComplain.Rows[this.dgvComplain.CurrentCell.RowIndex], NOTE_COMMENT_TYPE.COMPLAIN);
            this.inlineComplain.Focus();
        }

        private void btnComplainDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvComplain.CurrentCell == null)
                return;

            this.dgvComplain.Rows[this.dgvComplain.CurrentCell.RowIndex].DrawDeletingRowOverlay();
            if (MessageAlert.Show("ลบรายการที่เลือก, ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
            {
                this.dgvComplain.Rows[this.dgvComplain.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                return;
            }

            bool delete_success = false;
            BackgroundWorker wrk = new BackgroundWorker();
            wrk.DoWork += delegate
            {
                using (sn_noteEntities sn_note = DBXNote.DataSet())
                {
                    try
                    {
                        var comment_to_remove = sn_note.note_comment.Find((int)this.dgvComplain.Rows[this.dgvComplain.CurrentCell.RowIndex].Cells[this.col_complain_id.Name].Value);
                        if (comment_to_remove != null)
                        {
                            sn_note.note_comment.Remove(comment_to_remove);
                            sn_note.SaveChanges();
                            delete_success = true;
                        }
                        else
                        {
                            MessageAlert.Show("รายการที่ต้องการลบ ไม่มีอยู่ในระบบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        return;
                    }
                }
            };
            wrk.RunWorkerCompleted += delegate
            {
                this.dgvComplain.Rows[this.dgvComplain.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                if (delete_success)
                {
                    this.complains = null;
                    this.complains = new BindingList<note_commentVM>(GetComplain(this.current_note.id).ToViewModel());
                    this.dgvComplain.DataSource = this.complains;
                }
            };
            wrk.RunWorkerAsync();
        }

        private void btnComplainStop_Click(object sender, EventArgs e)
        {
            this.HideInlineForm();
            if (this.form_mode == FORM_MODE.ADD_ITEM)
            {
                ((BindingList<note_commentVM>)this.dgvComplain.DataSource).Remove(((BindingList<note_commentVM>)this.dgvComplain.DataSource).Where(n => n.id == -1).First());
            }

            this.ResetFormState(FORM_MODE.READ_ITEM);
        }

        private void btnComplainSave_Click(object sender, EventArgs e)
        {
            if (this.tmp_comment == null)
                return;

            using (sn_noteEntities sn_note = DBXNote.DataSet())
            {
                try
                {
                    if (this.form_mode == FORM_MODE.ADD_ITEM)
                    {
                        sn_note.note_comment.Add(this.tmp_comment);
                        sn_note.SaveChanges();

                        this.HideInlineForm();
                        this.ResetFormState(FORM_MODE.READ_ITEM);
                        this.complains = null;
                        this.complains = new BindingList<note_commentVM>(GetComplain(this.current_note.id).ToViewModel());
                        this.dgvComplain.DataSource = this.complains;
                        this.btnComplainAdd.PerformClick();
                    }

                    if (this.form_mode == FORM_MODE.EDIT_ITEM)
                    {
                        var note_to_update = sn_note.note_comment.Find(this.tmp_comment.id);
                        if (note_to_update != null)
                        {
                            note_to_update.date = DateTime.Now;
                            note_to_update.description = this.tmp_comment.description;
                            note_to_update.rec_by = this.main_form.loged_in_user.username;
                        }
                        else
                        {
                            sn_note.note_comment.Add(this.tmp_comment);
                        }

                        sn_note.SaveChanges();
                        this.HideInlineForm();
                        this.ResetFormState(FORM_MODE.READ_ITEM);
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.InnerException.InnerException.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void dgvComment_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                e.CellStyle.BackColor = Color.MistyRose;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                e.Handled = true;
            }
        }

        private void dgvComplain_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                e.CellStyle.BackColor = Color.LightBlue;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                e.Handled = true;
            }
        }

        private void dgvComment_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                if(row_index > -1)
                {
                    ((XDatagrid)sender).Rows[row_index].Cells[this.col_comment.Name].Selected = true;
                }

                ContextMenu cm = new ContextMenu();
                MenuItem m_add = new MenuItem("Add");
                m_add.Click += delegate
                {
                    this.btnCommentAdd.PerformClick();
                };
                cm.MenuItems.Add(m_add);

                MenuItem m_edit = new MenuItem("Edit");
                m_edit.Click += delegate
                {
                    this.btnCommentEdit.PerformClick();
                };
                m_edit.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(m_edit);

                MenuItem m_delete = new MenuItem("Delete");
                m_delete.Click += delegate
                {
                    this.btnCommentDelete.PerformClick();
                };
                m_delete.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(m_delete);

                cm.Show((XDatagrid)sender, new Point(e.X, e.Y));
            }
        }

        private void dgvComplain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                if (row_index > -1)
                {
                    ((XDatagrid)sender).Rows[row_index].Cells[this.col_complain.Name].Selected = true;
                }

                ContextMenu cm = new ContextMenu();
                MenuItem m_add = new MenuItem("Add");
                m_add.Click += delegate
                {
                    this.btnComplainAdd.PerformClick();
                };
                cm.MenuItems.Add(m_add);

                MenuItem m_edit = new MenuItem("Edit");
                m_edit.Click += delegate
                {
                    this.btnComplainEdit.PerformClick();
                };
                m_edit.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(m_edit);

                MenuItem m_delete = new MenuItem("Delete");
                m_delete.Click += delegate
                {
                    this.btnComplainDelete.PerformClick();
                };
                m_delete.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(m_delete);

                cm.Show((XDatagrid)sender, new Point(e.X, e.Y));
            }
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            if (((XDatagrid)sender).CurrentCell == null)
                return;

            this.SetInlineControlPosition(((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex]);
        }

        private void dgvComment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
                this.btnCommentEdit.PerformClick();
        }

        private void dgvComplain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
                this.btnComplainEdit.PerformClick();
        }

        private void inlineComment__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_comment != null)
                this.tmp_comment.description = ((XTextEdit)sender)._Text;
        }

        private void inlineComplain__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_comment != null)
                this.tmp_comment.description = ((XTextEdit)sender)._Text;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Escape)
            {
                if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if (this.comment_type == NOTE_COMMENT_TYPE.COMMENT)
                        this.btnCommentStop.PerformClick();

                    if (this.comment_type == NOTE_COMMENT_TYPE.COMPLAIN)
                        this.btnComplainStop.PerformClick();

                    return true;
                }
                
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnCancel.PerformClick();
                    return true;
                }
            }

            if(keyData == Keys.Enter)
            {
                if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if(this.comment_type == NOTE_COMMENT_TYPE.COMMENT)
                    {
                        this.btnCommentSave.PerformClick();
                        return true;
                    }

                    if(this.comment_type == NOTE_COMMENT_TYPE.COMPLAIN)
                    {
                        this.btnComplainSave.PerformClick();
                        return true;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnEditPath_Click(object sender, EventArgs e)
        {
            this.ResetFormState(FORM_MODE.EDIT);
            this.brPath.Focus();
        }

        private void btnStopPath_Click(object sender, EventArgs e)
        {
            this.ResetFormState(FORM_MODE.READ_ITEM);
            note n = FormNote.GetNote(this.current_note.id);
            this.brPath._Text = n != null && n.file_path != null ? n.file_path : string.Empty;
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            if (this.brPath._Text.Trim().Length > 0 && !File.Exists(this.brPath._Text))
            {
                MessageAlert.Show("ค้นหาไฟล์ที่ระบุไม่พบ, กรุณาตรวจสอบใหม่อีกครั้ง", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.brPath.Focus();
                return;
            }

            using (sn_noteEntities sn_net = DBXNote.DataSet())
            {
                try
                {
                    var note = sn_net.note.Find(this.current_note.id);
                    if(note != null)
                    {
                        note.file_path = this.brPath._Text.Trim();
                        sn_net.SaveChanges();
                    }
                    else
                    {
                        MessageAlert.Show("รายการที่แก้ไข ไม่มีอยู่ในระบบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
            this.ResetFormState(FORM_MODE.READ_ITEM);
        }

        private void brPath__DoubleClicked(object sender, EventArgs e)
        {
            if(this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.btnEditPath.PerformClick();
            }
        }

        private void brPath__ButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            fd.InitialDirectory = ((XBrowseBox)sender)._Text;
            fd.CheckFileExists = true;
            fd.CheckPathExists = true;
            fd.Multiselect = false;
            if(fd.ShowDialog() == DialogResult.OK)
            {
                this.brPath._Text = fd.FileName;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.brPath._Text))
            {
                MessageAlert.Show("ค้นหาไฟล์ที่ระบุไม่พบ!", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return;
            }

            System.Diagnostics.Process.Start(this.brPath._Text);
        }
    }
}
