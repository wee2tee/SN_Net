using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;
using System.IO;

namespace SN_Net.Subform
{
    public partial class CommentWindow : Form
    {
        private int note_id;
        private COMMENT_TYPE curr_comment_type;
        private SupportNoteComment prepared_comment; // preparing SupportNoteComment object for save
        private SupportStatWindow parent_wind;
        private Color color_light_blue = Color.FromArgb(198, 219, 255);
        private Color color_light_red = Color.FromArgb(255, 220, 224);
        private CustomTextBox inline_comment_desc;
            
        public enum COMMENT_TYPE : int
        {
            NONE = 0,
            COMMENT = 1,
            COMPLAIN = 2
        }

        public enum FORM_MODE
        {
            READ,
            ADD,
            EDIT,
            PROCESSING
        }

        public CommentWindow(SupportStatWindow parent_wind)
        {
            InitializeComponent();

            this.parent_wind = parent_wind;
            this.note_id = ((SupportNote)parent_wind.dgvNote.Rows[parent_wind.dgvNote.CurrentCell.RowIndex].Cells[0].Value).id;

            this.BindingControlEvent();
        }

        private void CommentWindow_Load(object sender, EventArgs e)
        {
            this.axWindowsMediaPlayer1.settings.autoStart = false;
            this.axWindowsMediaPlayer1.Ctlenabled = false;
            this.axWindowsMediaPlayer1.enableContextMenu = false;
        }

        private void CommentWindow_Shown(object sender, EventArgs e)
        {
            this.txtFilePath.Text = (this.parent_wind.supportnote_list.Find(t => t.id == this.note_id).file_path != null ? this.parent_wind.supportnote_list.Find(t => t.id == this.note_id).file_path : "");
            this.dgvComment.Tag = HelperClass.DGV_TAG.LEAVE;
            this.FillDgv(this.dgvComment, COMMENT_TYPE.COMMENT);
            this.dgvComplain.Tag = HelperClass.DGV_TAG.LEAVE;
            this.FillDgv(this.dgvComplain, COMMENT_TYPE.COMPLAIN);
            this.dgvComment.Focus();
            this.FormRead();
        }

        private void BindingControlEvent()
        {
            this.txtFilePath.TextChanged += delegate
            {
                if (this.txtFilePath.Text.Length > 0 && File.Exists(this.txtFilePath.Text))
                {
                    this.axWindowsMediaPlayer1.URL = this.txtFilePath.Text;
                    this.axWindowsMediaPlayer1.Ctlenabled = true;
                }
                else
                {
                    this.axWindowsMediaPlayer1.Ctlcontrols.stop();
                    this.axWindowsMediaPlayer1.URL = "";
                    this.axWindowsMediaPlayer1.Ctlenabled = false;
                }
            };

            this.dgvComment.GotFocus += new EventHandler(this.DgvGotFocusAction);

            this.dgvComplain.GotFocus += new EventHandler(this.DgvGotFocusAction);

            this.dgvComment.Leave += new EventHandler(this.DgvLeaveAction);

            this.dgvComplain.Leave += new EventHandler(this.DgvLeaveAction);

            this.dgvComment.Resize += delegate
            {
                this.SetInlineFormPosition(this.dgvComment);
            };

            this.dgvComplain.Resize += delegate
            {
                this.SetInlineFormPosition(this.dgvComplain);
            };

            this.dgvComment.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e) 
            {
                if (e.RowIndex > -1 && this.dgvComment.Rows[e.RowIndex].Tag is SupportNoteComment)
                {
                    this.btnEditComment.PerformClick();
                }
            };

            this.dgvComplain.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex > -1 && this.dgvComplain.Rows[e.RowIndex].Tag is SupportNoteComment)
                {
                    this.btnEditComplain.PerformClick();
                }
            };

            #region Context menu for datagridview
            this.dgvComment.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right && this.prepared_comment == null)
                {
                    int row_index = this.dgvComment.HitTest(e.X, e.Y).RowIndex;
                    if (row_index > -1)
                    {
                        this.dgvComment.Rows[row_index].Cells[0].Selected = true;
                        this.dgvComment.Focus();
                        ContextMenu cm = new ContextMenu();
                        MenuItem m_add = new MenuItem("เพิ่ม");
                        m_add.Click += delegate
                        {
                            this.btnAddComment.PerformClick();
                        };
                        cm.MenuItems.Add(m_add);

                        MenuItem m_edit = new MenuItem("แก้ไข");
                        m_edit.Click += delegate
                        {
                            this.btnEditComment.PerformClick();
                        };
                        cm.MenuItems.Add(m_edit);

                        MenuItem m_delete = new MenuItem("ลบ");
                        m_delete.Click += delegate
                        {
                            this.btnDeleteComment.PerformClick();
                        };
                        cm.MenuItems.Add(m_delete);

                        cm.Show(this.dgvComment, new Point(e.X, e.Y));
                    }
                }
            };

            this.dgvComplain.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right && this.prepared_comment == null)
                {
                    int row_index = this.dgvComplain.HitTest(e.X, e.Y).RowIndex;
                    if (row_index > -1)
                    {
                        this.dgvComplain.Rows[row_index].Cells[0].Selected = true;
                        this.dgvComplain.Focus();
                        ContextMenu cm = new ContextMenu();
                        MenuItem m_add = new MenuItem("เพิ่ม");
                        m_add.Click += delegate
                        {
                            this.btnAddComplain.PerformClick();
                        };
                        cm.MenuItems.Add(m_add);

                        MenuItem m_edit = new MenuItem("แก้ไข");
                        m_edit.Click += delegate
                        {
                            this.btnEditComplain.PerformClick();
                        };
                        cm.MenuItems.Add(m_edit);

                        MenuItem m_delete = new MenuItem("ลบ");
                        m_delete.Click += delegate
                        {
                            this.btnDeleteComplain.PerformClick();
                        };
                        cm.MenuItems.Add(m_delete);

                        cm.Show(this.dgvComplain, new Point(e.X, e.Y));
                    }
                }
            };
            #endregion Context menu for datagridview
        }

        private void DgvGotFocusAction(object sender, EventArgs e)
        {
            ((DataGridView)sender).Tag = HelperClass.DGV_TAG.READ;
            ((DataGridView)sender).Refresh();
        }

        private void DgvLeaveAction(object sender, EventArgs e)
        {
            ((DataGridView)sender).Tag = HelperClass.DGV_TAG.LEAVE;
            ((DataGridView)sender).Refresh();
        }

        private void FillDgv(DataGridView dgv, COMMENT_TYPE comment_type)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "โดย",
                Width = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable,
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "รายละเอียด",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable,
            });

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.Style.BackColor = (comment_type == COMMENT_TYPE.COMMENT ? this.color_light_blue : this.color_light_red);
                col.HeaderCell.Style.SelectionBackColor = (comment_type == COMMENT_TYPE.COMMENT ? this.color_light_blue : this.color_light_red);
            }

            foreach (SupportNoteComment n in this.parent_wind.supportnotecomment_list.FindAll(t => t.note_id == this.note_id && t.type == (int)comment_type))
            {
                int r = dgv.Rows.Add();
                dgv.Rows[r].Tag = n;

                dgv.Rows[r].Cells[0].ValueType = typeof(string);
                dgv.Rows[r].Cells[0].Value = n.rec_by;

                dgv.Rows[r].Cells[1].ValueType = typeof(string);
                dgv.Rows[r].Cells[1].Value = n.description;
            }

            dgv.DrawDgvRowBorder();
        }

        private void ShowInlineForm(COMMENT_TYPE comment_type, FORM_MODE mode)
        {
            this.curr_comment_type = comment_type;

            DataGridView dgv = null;
            switch (comment_type)
            {
                case COMMENT_TYPE.COMMENT:
                    dgv = this.dgvComment;
                    this.inline_comment_desc = new CustomTextBox()
                    {
                        Read_Only = false
                    };
                    dgv.Parent.Controls.Add(this.inline_comment_desc);
                    break;
                case COMMENT_TYPE.COMPLAIN:
                    dgv = this.dgvComplain;
                    this.inline_comment_desc = new CustomTextBox()
                    {
                        Read_Only = false
                    };
                    dgv.Parent.Controls.Add(this.inline_comment_desc);
                    break;
                case COMMENT_TYPE.NONE:
                    break;
                default:
                    break;
            }

            if (dgv != null && mode == FORM_MODE.ADD)
            {
                dgv.Rows[dgv.Rows.Add()].Cells[0].Selected = true;
                this.prepared_comment = new SupportNoteComment();
                this.prepared_comment.id = -1;
                this.prepared_comment.type = (int)comment_type;
                this.prepared_comment.note_id = this.note_id;
                this.prepared_comment.rec_by = this.parent_wind.main_form.G.loged_in_user_name;
                this.FormAdd();
            }

            if (dgv != null && mode == FORM_MODE.EDIT && dgv.Rows[dgv.CurrentCell.RowIndex].Tag is SupportNoteComment)
            {
                this.prepared_comment = (SupportNoteComment)dgv.Rows[dgv.CurrentCell.RowIndex].Tag;
                this.prepared_comment.rec_by = this.parent_wind.main_form.G.loged_in_user_name;

                this.inline_comment_desc.Texts = this.prepared_comment.description;
                this.FormEdit();
            }

            this.SetInlineFormPosition(dgv);
            dgv.Enabled = false;
            dgv.SendToBack();
            this.inline_comment_desc.BringToFront();
            this.inline_comment_desc.Focus();
            this.inline_comment_desc.SelectionStart = 0;
            this.inline_comment_desc.SelectionLength = 0;

            dgv.Tag = HelperClass.DGV_TAG.READ;
            dgv.Refresh();
        }

        private void SetInlineFormPosition(DataGridView dgv)
        {
            if (this.inline_comment_desc != null)
            {
                Rectangle rect;

                switch (this.curr_comment_type)
                {
                    case COMMENT_TYPE.NONE:
                        break;
                    case COMMENT_TYPE.COMMENT:
                        rect = this.dgvComment.GetCellDisplayRectangle(1, this.dgvComment.CurrentCell.RowIndex, true);
                        this.inline_comment_desc.SetBounds(rect.X + this.dgvComment.Location.X, rect.Y + this.dgvComment.Location.Y + 1, rect.Width, rect.Height - 3);
                        break;
                    case COMMENT_TYPE.COMPLAIN:
                        rect = this.dgvComplain.GetCellDisplayRectangle(1, this.dgvComplain.CurrentCell.RowIndex, true);
                        this.inline_comment_desc.SetBounds(rect.X + this.dgvComplain.Location.X, rect.Y + this.dgvComplain.Location.Y + 1, rect.Width, rect.Height - 3);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ClearInlineForm(DataGridView dgv)
        {
            this.prepared_comment = null;
            if (this.inline_comment_desc != null)
            {
                this.inline_comment_desc.Dispose();
                this.inline_comment_desc = null;
            }
            dgv.Enabled = true;

            if (dgv.Rows.Cast<DataGridViewRow>().Where(r => !(r.Tag is SupportNoteComment)).Count<DataGridViewRow>() > 0)
                dgv.Rows.Remove(dgv.Rows.Cast<DataGridViewRow>().Where(r => !(r.Tag is SupportNoteComment)).First<DataGridViewRow>());
        }

        private void SaveComment()
        {
            if (this.prepared_comment == null)
                return;

            string json_data = "{\"id\":" + this.prepared_comment.id.ToString() + ",";
            json_data += "\"note_id\":" + this.prepared_comment.note_id.ToString() + ",";
            json_data += "\"type\":" + this.prepared_comment.type.ToString() + ",";
            json_data += "\"description\":\"" + this.inline_comment_desc.Texts.cleanString() + "\",";
            json_data += "\"rec_by\":\"" + this.prepared_comment.rec_by.cleanString() + "\"}";

            this.FormProcessing();

            CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnotecomment/create_or_update", json_data);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                int comment_id = this.prepared_comment.id;

                if (this.prepared_comment.type == (int)COMMENT_TYPE.COMMENT)
                {
                    this.ClearInlineForm(this.dgvComment);
                    this.parent_wind.GetComments();
                    this.parent_wind.FillDataGrid(this.note_id);
                    this.FillDgv(this.dgvComment, COMMENT_TYPE.COMMENT);
                    if (comment_id == -1)
                    {
                        this.ShowInlineForm(COMMENT_TYPE.COMMENT, FORM_MODE.ADD);
                    }
                    else
                    {
                        this.FormRead();
                    }
                    return;
                }

                if (this.prepared_comment.type == (int)COMMENT_TYPE.COMPLAIN)
                {
                    this.ClearInlineForm(this.dgvComplain);
                    this.parent_wind.GetComments();
                    this.parent_wind.FillDataGrid(this.note_id);
                    this.FillDgv(this.dgvComplain, COMMENT_TYPE.COMPLAIN);
                    if (comment_id == -1)
                    {
                        this.ShowInlineForm(COMMENT_TYPE.COMPLAIN, FORM_MODE.ADD);
                    }
                    else
                    {
                        this.FormRead();
                    }
                    return;
                }
            }
            else
            {
                if (MessageAlert.Show(sr.message, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                {
                    this.SaveComment();
                    return;
                }

                if (this.prepared_comment.id == -1)
                {
                    this.FormAdd();
                }
                else
                {
                    this.FormEdit();
                }
            }
        }

        private bool DeleteComment(SupportNoteComment comment, DataGridView datagrid)
        {
            CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "supportnotecomment/delete&id=" + comment.id.ToString());
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return true;
            }
            else
            {
                if (MessageAlert.Show("เกิดข้อผิดพลาด", "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                {
                    this.DeleteComment(comment, datagrid);
                }
                return false;
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string app_dir = AppDomain.CurrentDomain.BaseDirectory;

            if (this.txtFilePath.Text.Trim().Length > 0)
            {
                try
                {
                    if (Directory.Exists(Path.GetDirectoryName(this.txtFilePath.Text)))
                    {
                        this.openFileDialog1.InitialDirectory = Path.GetDirectoryName(this.txtFilePath.Text);
                        this.openFileDialog1.FileName = this.txtFilePath.Text;
                    }
                    else
                    {
                        this.openFileDialog1.InitialDirectory = app_dir;
                    }
                }
                catch (DirectoryNotFoundException ex)
                {
                    this.openFileDialog1.InitialDirectory = app_dir;
                    throw;
                }
            }
            else
            {
                this.openFileDialog1.InitialDirectory = app_dir;
            }

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string json_data = "{\"id\":" + this.note_id.ToString() + ",";
                json_data += "\"file_path\":\"" + this.openFileDialog1.FileName.cleanString() + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/save_file_path", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.parent_wind.GetSingleNote(this.note_id);
                    this.txtFilePath.Text = (this.parent_wind.supportnote_list.Find(t => t.id == this.note_id) != null ? (this.parent_wind.supportnote_list.Find(t => t.id == this.note_id).file_path != null ? this.parent_wind.supportnote_list.Find(t => t.id == this.note_id).file_path : "") : "");
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void btnDeletePath_Click(object sender, EventArgs e)
        {
            if (this.txtFilePath.Text.Trim().Length == 0)
                return;

            if (MessageAlert.Show("ลบ Path ของไฟล์เสียง, ทำต่อ?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                string json_data = "{\"id\":" + this.note_id.ToString() + "}";
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnote/remove_file_path", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.parent_wind.GetSingleNote(this.note_id);
                    if (this.parent_wind.supportnote_list.Find(t => t.id == this.note_id) != null)
                    {
                        if (this.parent_wind.supportnote_list.Find(t => t.id == this.note_id).file_path != null)
                        {
                            this.txtFilePath.Text = this.parent_wind.supportnote_list.Find(t => t.id == this.note_id).file_path;
                            this.openFileDialog1.FileName = this.parent_wind.supportnote_list.Find(t => t.id == this.note_id).file_path;
                        }
                        else
                        {
                            this.txtFilePath.Text = "";
                            this.openFileDialog1.FileName = "";
                        }
                    }
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            this.ShowInlineForm(COMMENT_TYPE.COMMENT, FORM_MODE.ADD);
        }

        private void btnEditComment_Click(object sender, EventArgs e)
        {
            if (this.dgvComment.CurrentCell != null && this.dgvComment.Rows[this.dgvComment.CurrentCell.RowIndex].Tag is SupportNoteComment)
                this.ShowInlineForm(COMMENT_TYPE.COMMENT, FORM_MODE.EDIT);

            return;
        }

        private void btnStopComment_Click(object sender, EventArgs e)
        {
            if (this.prepared_comment.type == (int)COMMENT_TYPE.COMMENT)
            {
                if (MessageAlert.Show(StringResource.CONFIRM_CANCEL_ADD_EDIT, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                {
                    this.ClearInlineForm(this.dgvComment);
                    this.dgvComment.Focus();
                    this.FormRead();
                }
            }
        }

        private void btnSaveComment_Click(object sender, EventArgs e)
        {
            if (this.prepared_comment.type == (int)COMMENT_TYPE.COMMENT)
                this.SaveComment();
        }

        private void btnDeleteComment_Click(object sender, EventArgs e)
        {
            if (this.dgvComment.CurrentCell == null)
                return;

            if (this.dgvComment.Rows[this.dgvComment.CurrentCell.RowIndex].Tag is SupportNoteComment)
            {
                this.dgvComment.Tag = HelperClass.DGV_TAG.DELETE;
                this.dgvComment.Refresh();
                if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                {
                    this.FormProcessing();
                    if (this.DeleteComment((SupportNoteComment)this.dgvComment.Rows[this.dgvComment.CurrentCell.RowIndex].Tag, this.dgvComment) == true)
                    {
                        this.dgvComment.Tag = HelperClass.DGV_TAG.READ;
                        this.parent_wind.GetComments();
                        this.parent_wind.FillDataGrid(this.note_id);
                        this.FillDgv(this.dgvComment, COMMENT_TYPE.COMMENT);
                        this.dgvComment.Focus();
                    }
                    else
                    {
                        this.dgvComment.Tag = HelperClass.DGV_TAG.READ;
                        this.dgvComment.Refresh();
                        this.dgvComment.Focus();
                    }
                    this.FormRead();
                }

                this.dgvComment.Tag = HelperClass.DGV_TAG.READ;
                this.dgvComment.Refresh();
                this.dgvComment.Focus();
            }
        }

        private void btnAddComplain_Click(object sender, EventArgs e)
        {
            this.ShowInlineForm(COMMENT_TYPE.COMPLAIN, FORM_MODE.ADD);
        }

        private void btnEditComplain_Click(object sender, EventArgs e)
        {
            if (this.dgvComplain.CurrentCell != null && this.dgvComplain.Rows[this.dgvComplain.CurrentCell.RowIndex].Tag is SupportNoteComment)
                this.ShowInlineForm(COMMENT_TYPE.COMPLAIN, FORM_MODE.EDIT);

            return;
        }

        private void btnStopComplain_Click(object sender, EventArgs e)
        {
            if (this.prepared_comment.type == (int)COMMENT_TYPE.COMPLAIN)
            {
                if (MessageAlert.Show(StringResource.CONFIRM_CANCEL_ADD_EDIT, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                {
                    this.ClearInlineForm(this.dgvComplain);
                    this.dgvComplain.Focus();
                    this.FormRead();
                }
            }
        }

        private void btnSaveComplain_Click(object sender, EventArgs e)
        {
            if (this.prepared_comment.type == (int)COMMENT_TYPE.COMPLAIN)
                this.SaveComment();
        }

        private void btnDeleteComplain_Click(object sender, EventArgs e)
        {
            if (this.dgvComplain.CurrentCell == null)
                return;

            if (this.dgvComplain.Rows[this.dgvComplain.CurrentCell.RowIndex].Tag is SupportNoteComment)
            {
                this.dgvComplain.Tag = HelperClass.DGV_TAG.DELETE;
                this.dgvComplain.Refresh();
                if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                {
                    this.FormProcessing();
                    if (this.DeleteComment((SupportNoteComment)this.dgvComplain.Rows[this.dgvComplain.CurrentCell.RowIndex].Tag, this.dgvComplain) == true)
                    {
                        this.dgvComplain.Tag = HelperClass.DGV_TAG.READ;
                        this.parent_wind.GetComments();
                        this.parent_wind.FillDataGrid(this.note_id);
                        this.FillDgv(this.dgvComplain, COMMENT_TYPE.COMPLAIN);
                        this.dgvComplain.Focus();
                    }
                    else
                    {
                        this.dgvComplain.Tag = HelperClass.DGV_TAG.READ;
                        this.dgvComplain.Refresh();
                        this.dgvComplain.Focus();
                    }
                    this.FormRead();
                }

                this.dgvComplain.Tag = HelperClass.DGV_TAG.READ;
                this.dgvComplain.Refresh();
                this.dgvComplain.Focus();
            }
        }

        private void FormRead()
        {
            this.btnAddComment.Enabled = true;
            this.btnEditComment.Enabled = true;
            this.btnStopComment.Enabled = false;
            this.btnSaveComment.Enabled = false;
            this.btnDeleteComment.Enabled = true;
            this.btnAddComplain.Enabled = true;
            this.btnEditComplain.Enabled = true;
            this.btnStopComplain.Enabled = false;
            this.btnSaveComplain.Enabled = false;
            this.btnDeleteComplain.Enabled = true;
        }

        private void FormProcessing()
        {
            this.btnAddComment.Enabled = false;
            this.btnEditComment.Enabled = false;
            this.btnStopComment.Enabled = false;
            this.btnSaveComment.Enabled = false;
            this.btnDeleteComment.Enabled = false;
            this.btnAddComplain.Enabled = false;
            this.btnEditComplain.Enabled = false;
            this.btnStopComplain.Enabled = false;
            this.btnSaveComplain.Enabled = false;
            this.btnDeleteComplain.Enabled = false;
        }

        private void FormAdd()
        {
            this.btnAddComment.Enabled = false;
            this.btnEditComment.Enabled = false;
            this.btnStopComment.Enabled = true;
            this.btnSaveComment.Enabled = true;
            this.btnDeleteComment.Enabled = false;
            this.btnAddComplain.Enabled = false;
            this.btnEditComplain.Enabled = false;
            this.btnStopComplain.Enabled = true;
            this.btnSaveComplain.Enabled = true;
            this.btnDeleteComplain.Enabled = false;
        }

        private void FormEdit()
        {
            this.btnAddComment.Enabled = false;
            this.btnEditComment.Enabled = false;
            this.btnStopComment.Enabled = true;
            this.btnSaveComment.Enabled = true;
            this.btnDeleteComment.Enabled = false;
            this.btnAddComplain.Enabled = false;
            this.btnEditComplain.Enabled = false;
            this.btnStopComplain.Enabled = true;
            this.btnSaveComplain.Enabled = true;
            this.btnDeleteComplain.Enabled = false;

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.prepared_comment != null)
            {
                if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            this.axWindowsMediaPlayer1.Ctlcontrols.stop();
            this.axWindowsMediaPlayer1.Dispose();
            base.OnClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.inline_comment_desc.textBox1.Focused)
                {
                    if (this.prepared_comment == null)
                        return false;

                    if (this.prepared_comment.type == (int)COMMENT_TYPE.COMMENT)
                    {
                        this.btnSaveComment.PerformClick();
                        return true;
                    }

                    if (this.prepared_comment.type == (int)COMMENT_TYPE.COMPLAIN)
                    {
                        this.btnSaveComplain.PerformClick();
                        return true;
                    }
                }
            }

            if (keyData == Keys.F9)
            {
                if (this.prepared_comment == null)
                    return false;

                if (this.prepared_comment.type == (int)COMMENT_TYPE.COMMENT) // while in add/edit comment mode
                {
                    this.btnSaveComment.PerformClick();
                    return true;
                }

                if (this.prepared_comment.type == (int)COMMENT_TYPE.COMPLAIN) // while in add/edit complain mode
                {
                    this.btnSaveComplain.PerformClick();
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                if (this.prepared_comment == null) // while in read mode
                {
                    this.btnClose.PerformClick();
                    return true;
                }

                if (this.prepared_comment.type == (int)COMMENT_TYPE.COMMENT) // while in add/edit comment mode
                {
                    this.btnStopComment.PerformClick();
                    return true;
                }

                if (this.prepared_comment.type == (int)COMMENT_TYPE.COMPLAIN) // while in add/edit complain mode
                {
                    this.btnStopComplain.PerformClick();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
