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
            this.current_note = current_note;
            InitializeComponent();
        }

        private void DialogNoteComment_Load(object sender, EventArgs e)
        {
            this.HideInlineForm();

            this.comments = new BindingList<note_commentVM>(GetComment(this.current_note.id).ToViewModel());
            this.complains = new BindingList<note_commentVM>(GetComplain(this.current_note.id).ToViewModel());

            this.dgvComment.DataSource = this.comments;
            this.dgvComplain.DataSource = this.complains;
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.btnCancel.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.dgvComment.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.dgvComplain.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.inlineComment.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineComplain.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
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

        }

        private void btnCommentEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvComment.CurrentCell == null)
                return;

            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm(this.dgvComment.Rows[this.dgvComment.CurrentCell.RowIndex], NOTE_COMMENT_TYPE.COMMENT);
        }

        private void btnCommentDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnCommentStop_Click(object sender, EventArgs e)
        {

        }

        private void btnCommentSave_Click(object sender, EventArgs e)
        {

        }

        private void btnComplainAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnComplainEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvComplain.CurrentCell == null)
                return;

            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm(this.dgvComplain.Rows[this.dgvComplain.CurrentCell.RowIndex], NOTE_COMMENT_TYPE.COMPLAIN);
        }

        private void btnComplainDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnComplainStop_Click(object sender, EventArgs e)
        {

        }

        private void btnComplainSave_Click(object sender, EventArgs e)
        {

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

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
