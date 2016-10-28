using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class IstabWindow : Form
    {
        private List<Istab> istabs;
        private Istab.TABTYP tabtyp;
        private MainForm main_form;
        private Color general_column_header = Color.FromArgb(231, 207, 181);
        private Color sorted_column_header = Color.FromArgb(194, 127, 101);
        private SORT_BY sort;
        private FORM_MODE form_mode;

        private CustomTextBox inline_typcod = null;
        private CustomTextBox inline_abbr_th = null;
        private CustomTextBox inline_abbr_en = null;
        private CustomTextBox inline_typdes_th = null;
        private CustomTextBox inline_typdes_en = null;
        private Control current_focused_control = null;

        public enum SORT_BY
        {
            TYPCOD,
            ABBR_TH,
            ABBR_EN,
            DESC_TH,
            DESC_EN
        }

        public enum FORM_MODE
        {
            READ,
            ADD,
            EDIT,
            PROCESSING
        }

        public enum DGV_TAG
        {
            NORMAL,
            DELETE
        }

        public IstabWindow(MainForm main_form, Istab.TABTYP tabtyp)
        {
            InitializeComponent();
            this.main_form = main_form;
            this.tabtyp = tabtyp;
            this.sort = SORT_BY.TYPCOD;
        }

        private void IstabWindow_Load(object sender, EventArgs e)
        {
            this.Text = Istab.getTabtypTitle(this.tabtyp);
            this.GetIstabData();
            this.BindControlEventHandler();
        }

        private void BindControlEventHandler()
        {
            #region Double click to add/edit
            this.dgvIstab.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex > -1)
                {
                    this.ShowInlineForm(e.ColumnIndex);
                    return;
                }
            };
            #endregion Double click to add/edit

            #region Draw selected border
            this.dgvIstab.Paint += delegate
            {
                if (this.dgvIstab.CurrentCell == null)
                    return;

                Rectangle rect = this.dgvIstab.GetRowDisplayRectangle(this.dgvIstab.CurrentCell.RowIndex, true);
                using(Pen p = new Pen(Color.Red))
                {
                    this.dgvIstab.CreateGraphics().DrawLine(p, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                    this.dgvIstab.CreateGraphics().DrawLine(p, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);

                    if ((DGV_TAG)this.dgvIstab.Tag == DGV_TAG.DELETE)
                    {
                        for (int i = rect.Left - 16; i < rect.Right; i += 8)
                        {
                            this.dgvIstab.CreateGraphics().DrawLine(p, i, rect.Bottom - 2, i + 23, rect.Top);
                        }
                    }
                }

            };
            #endregion Draw selected border

            #region Fill line and reposition inline_form
            this.dgvIstab.Resize += delegate
            {
                if (this.dgvIstab.CurrentCell == null)
                    return;

                this.dgvIstab.FillLine(this.istabs.Count);
                this.SetInlineFormPosition();
            };
            #endregion Fill line and reposition inline_form

            this.dgvIstab.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    int row_index = this.dgvIstab.HitTest(e.X, e.Y).RowIndex;

                    if (row_index < 0)
                        return;

                    this.dgvIstab.Rows[row_index].Cells[1].Selected = true;

                    ContextMenu m = new ContextMenu();
                    MenuItem m_add = new MenuItem("เพิ่ม <Alt+A>");
                    m_add.Click += delegate
                    {
                        this.toolStripAdd.PerformClick();
                    };
                    m.MenuItems.Add(m_add);

                    MenuItem m_edit = new MenuItem("แก้ไข <Alt+E>");
                    m_edit.Enabled = (this.dgvIstab.Rows[row_index].Tag is Istab ? true : false);
                    m_edit.Click += delegate
                    {
                        this.toolStripEdit.PerformClick();
                    };
                    m.MenuItems.Add(m_edit);

                    MenuItem m_delete = new MenuItem("ลบ <Alt+D>");
                    m_delete.Enabled = (this.dgvIstab.Rows[row_index].Tag is Istab ? true : false);
                    m_delete.Click += delegate
                    {
                        this.toolStripDelete.PerformClick();
                    };
                    m.MenuItems.Add(m_delete);

                    m.Show(this.dgvIstab, new Point(e.X, e.Y));
                }
            };
        }

        private void GetIstabData(Istab selected_item = null, bool performadd = false, int selected_row_index_after_delete = -1)
        {
            this.FormProcessing();

            string sort = "";

            switch (this.sort)
            {
                case SORT_BY.TYPCOD:
                    sort = "typcod";
                    break;
                case SORT_BY.ABBR_TH:
                    sort = "abbreviate_th";
                    break;
                case SORT_BY.ABBR_EN:
                    sort = "abbreviate_en";
                    break;
                case SORT_BY.DESC_TH:
                    sort = "typdes_th";
                    break;
                case SORT_BY.DESC_EN:
                    sort = "typdes_en";
                    break;
                default:
                    sort = "typcod";
                    break;
            }
            bool get_success = false;
            string err_msg = "";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_all&tabtyp=" + this.tabtyp.ToTabtypString() + "&sort=" + sort);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    get_success = true;
                    this.istabs = sr.istab;
                    this.main_form.data_resource.Refresh();
                }
                else
                {
                    get_success = false;
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (get_success)
                {
                    this.FillInDatagrid(selected_item);
                    this.FormRead();
                    if (performadd)
                    {
                        this.toolStripAdd.PerformClick();
                    }
                    if (selected_row_index_after_delete > -1)
                    {
                        this.dgvIstab.Rows[selected_row_index_after_delete].Cells[1].Selected = true;
                    }
                }
                else
                {
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.FormRead();
                }
            };
            worker.RunWorkerAsync();
        }

        private void FillInDatagrid(Istab selected_item = null)
        {
            this.dgvIstab.Rows.Clear();
            this.dgvIstab.Columns.Clear();
            this.dgvIstab.Tag = DGV_TAG.NORMAL;
            this.dgvIstab.EnableHeadersVisualStyles = false;

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.HeaderText = "ID";
            col0.Width = 0;
            col0.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = Color.YellowGreen,
                Padding = new Padding(0, 3, 0, 3),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            col0.Visible = false;
            this.dgvIstab.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "รหัส";
            col1.Width = 80;
            col1.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = this.sorted_column_header,
                Padding = new Padding(0, 3, 0, 3),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvIstab.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "ชื่อย่อไทย";
            col2.Width = 100;
            col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = this.general_column_header,
                Padding = new Padding(0, 3, 0, 3),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvIstab.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "ชื่อย่อEng";
            col3.Width = 100;
            col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = this.general_column_header,
                Padding = new Padding(0, 3, 0, 3),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvIstab.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "ชื่อเต็มไทย";
            col4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col4.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = this.general_column_header,
                Padding = new Padding(0, 3, 0, 3),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvIstab.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.HeaderText = "ชื่อเต็มEng";
            col5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col5.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = this.general_column_header,
                Padding = new Padding(0, 3, 0, 3),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvIstab.Columns.Add(col5);

            foreach (Istab istab in this.istabs)
            {
                int r = this.dgvIstab.Rows.Add();
                this.dgvIstab.Rows[r].Tag = istab;
                DataGridViewRow row = this.dgvIstab.Rows[r];
                row.Height = 25;
                row.ReadOnly = true;

                row.Cells[0].ValueType = typeof(int);
                row.Cells[0].Tag = "ID";
                row.Cells[0].Value = istab.id;
                row.Cells[0].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                };

                row.Cells[1].ValueType = typeof(string);
                row.Cells[1].Value = istab.typcod;
                row.Cells[1].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                };

                row.Cells[2].ValueType = typeof(string);
                row.Cells[2].Value = istab.abbreviate_th;
                row.Cells[2].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                };

                row.Cells[3].ValueType = typeof(string);
                row.Cells[3].Value = istab.abbreviate_en;
                row.Cells[3].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                };

                row.Cells[4].ValueType = typeof(string);
                row.Cells[4].Value = istab.typdes_th;
                row.Cells[4].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                };

                row.Cells[5].ValueType = typeof(string);
                row.Cells[5].Value = istab.typdes_en;
                row.Cells[5].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                };
            }

            this.dgvIstab.FillLine(this.istabs.Count);

            if (selected_item == null)
                return;

            if (this.dgvIstab.Rows.Cast<DataGridViewRow>().Where(t => t.Tag is Istab).Where(t => ((Istab)t.Tag).id == selected_item.id).Count() > 0)
            {
                this.dgvIstab.Rows.Cast<DataGridViewRow>().Where(t => t.Tag is Istab).Where(t => ((Istab)t.Tag).id == selected_item.id).First<DataGridViewRow>().Cells[1].Selected = true;
            }
        }

        private int GetTypcodMaxChar()
        {
            switch (this.tabtyp)
            {
                case Istab.TABTYP.AREA:
                    return 10;
                case Istab.TABTYP.VEREXT:
                    return 1;
                case Istab.TABTYP.HOWKNOWN:
                    return 4;
                case Istab.TABTYP.BUSITYP:
                    return 4;
                case Istab.TABTYP.PROBLEM_CODE:
                    return 2;
                case Istab.TABTYP.ABSENT_CAUSE:
                    return 4;
                case Istab.TABTYP.SERVICE_CASE:
                    return 4;
                default:
                    return 4;
            }
        }

        private void ShowInlineForm(int col_index = 1)
        {
            if (this.dgvIstab.CurrentCell == null)
                return;

            int focused_field = 1;

            if (this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag is Istab) // edit mode
            {
                this.FormEdit();
                focused_field = (col_index == 1 ? 2 : col_index);
            }
            else // add mode
            {
                this.dgvIstab.Rows[this.istabs.Count].Cells[1].Selected = true;
                this.FormAdd();
            }

            this.inline_typcod = new CustomTextBox();
            this.inline_typcod.Name = "inline_typcod";
            this.inline_typcod.Read_Only = (this.form_mode == FORM_MODE.EDIT ? true : false);
            this.inline_typcod.MaxChar = this.GetTypcodMaxChar();
            this.inline_typcod.BorderStyle = BorderStyle.None;
            this.inline_typcod.Texts = (this.form_mode == FORM_MODE.EDIT ? ((Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag).typcod : "");
            this.inline_typcod.textBox1.GotFocus += delegate
            {
                this.inline_typcod.SelectionStart = this.inline_typcod.Texts.Length;
                this.current_focused_control = this.inline_typcod.textBox1;
            };
            this.dgvIstab.Parent.Controls.Add(this.inline_typcod);

            this.inline_abbr_th = new CustomTextBox();
            this.inline_abbr_th.Name = "inline_abbr_th";
            this.inline_abbr_th.Read_Only = false;
            this.inline_abbr_th.MaxChar = 20;
            this.inline_abbr_th.BorderStyle = BorderStyle.None;
            this.inline_abbr_th.Texts = (this.form_mode == FORM_MODE.EDIT ? ((Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag).abbreviate_th : "");
            this.inline_abbr_th.textBox1.GotFocus += delegate
            {
                this.inline_abbr_th.SelectionStart = this.inline_abbr_th.Texts.Length;
                this.current_focused_control = this.inline_abbr_th.textBox1;

                if (this.inline_typcod.Texts.Length == 0)
                {
                    this.inline_typcod.Focus();
                }
            };
            this.dgvIstab.Parent.Controls.Add(this.inline_abbr_th);

            this.inline_abbr_en = new CustomTextBox();
            this.inline_abbr_en.Name = "inline_abbr_en";
            this.inline_abbr_en.Read_Only = false;
            this.inline_abbr_en.MaxChar = 20;
            this.inline_abbr_en.BorderStyle = BorderStyle.None;
            this.inline_abbr_en.Texts = (this.form_mode == FORM_MODE.EDIT ? ((Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag).abbreviate_en : "");
            this.inline_abbr_en.textBox1.GotFocus += delegate
            {
                this.inline_abbr_en.SelectionStart = this.inline_abbr_en.Texts.Length;
                this.current_focused_control = this.inline_abbr_en.textBox1;

                if (this.inline_typcod.Texts.Length == 0)
                {
                    this.inline_typcod.Focus();
                }
            };
            this.dgvIstab.Parent.Controls.Add(this.inline_abbr_en);

            this.inline_typdes_th = new CustomTextBox();
            this.inline_typdes_th.Name = "inline_typdes_th";
            this.inline_typdes_th.Read_Only = false;
            this.inline_typdes_th.MaxChar = 50;
            this.inline_typdes_th.BorderStyle = BorderStyle.None;
            this.inline_typdes_th.Texts = (this.form_mode == FORM_MODE.EDIT ? ((Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag).typdes_th : "");
            this.inline_typdes_th.textBox1.GotFocus += delegate
            {
                this.inline_typdes_th.SelectionStart = this.inline_typdes_th.Texts.Length;
                this.current_focused_control = this.inline_typdes_th.textBox1;

                if (this.inline_typcod.Texts.Length == 0)
                {
                    this.inline_typcod.Focus();
                }
            };
            this.dgvIstab.Parent.Controls.Add(this.inline_typdes_th);

            this.inline_typdes_en = new CustomTextBox();
            this.inline_typdes_en.Name = "inline_typdes_en";
            this.inline_typdes_en.Read_Only = false;
            this.inline_typdes_en.MaxChar = 50;
            this.inline_typdes_en.BorderStyle = BorderStyle.None;
            this.inline_typdes_en.Texts = (this.form_mode == FORM_MODE.EDIT ? ((Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag).typdes_en : "");
            this.inline_typdes_en.textBox1.GotFocus += delegate
            {
                this.inline_typdes_en.SelectionStart = this.inline_typdes_en.Texts.Length;
                this.current_focused_control = this.inline_typdes_en.textBox1;

                if (this.inline_typcod.Texts.Length == 0)
                {
                    this.inline_typcod.Focus();
                }
            };
            this.dgvIstab.Parent.Controls.Add(this.inline_typdes_en);

            this.SetInlineFormPosition();

            this.inline_typcod.BringToFront();
            this.inline_abbr_th.BringToFront();
            this.inline_abbr_en.BringToFront();
            this.inline_typdes_th.BringToFront();
            this.inline_typdes_en.BringToFront();
            this.dgvIstab.SendToBack();

            if (focused_field == 1)
            {
                this.inline_typcod.textBox1.Focus();
                return;
            }
            if (focused_field == 2)
            {
                this.inline_abbr_th.textBox1.Focus();
                return;
            }
            if (focused_field == 3)
            {
                this.inline_abbr_en.textBox1.Focus();
                return;
            }
            if (focused_field == 4)
            {
                this.inline_typdes_th.textBox1.Focus();
                return;
            }
            if (focused_field == 5)
            {
                this.inline_typdes_en.textBox1.Focus();
                return;
            }
        }

        private void SetInlineFormPosition()
        {
            if (this.dgvIstab.CurrentCell == null)
                return;

            Rectangle rect_typcod = this.dgvIstab.GetCellDisplayRectangle(1, this.dgvIstab.CurrentCell.RowIndex, true);
            Rectangle rect_abbr_th = this.dgvIstab.GetCellDisplayRectangle(2, this.dgvIstab.CurrentCell.RowIndex, true);
            Rectangle rect_abbr_en = this.dgvIstab.GetCellDisplayRectangle(3, this.dgvIstab.CurrentCell.RowIndex, true);
            Rectangle rect_typdes_th = this.dgvIstab.GetCellDisplayRectangle(4, this.dgvIstab.CurrentCell.RowIndex, true);
            Rectangle rect_typdes_en = this.dgvIstab.GetCellDisplayRectangle(5, this.dgvIstab.CurrentCell.RowIndex, true);
            
            if(this.inline_typcod != null)
            {
                this.inline_typcod.SetBounds(rect_typcod.X, rect_typcod.Y + 1, rect_typcod.Width - 1, rect_typcod.Height - 3);
            }
            if(this.inline_abbr_th != null)
            {
                this.inline_abbr_th.SetBounds(rect_abbr_th.X, rect_abbr_th.Y + 1, rect_abbr_th.Width - 1, rect_abbr_th.Height - 3);
            }
            if(this.inline_abbr_en != null)
            {
                this.inline_abbr_en.SetBounds(rect_abbr_en.X, rect_abbr_en.Y + 1, rect_abbr_en.Width - 1, rect_abbr_en.Height - 3);
            }
            if(this.inline_typdes_th != null)
            {
                this.inline_typdes_th.SetBounds(rect_typdes_th.X, rect_typdes_th.Y + 1, rect_typdes_th.Width - 1, rect_typdes_th.Height - 3);
            }
            if(this.inline_typdes_en != null)
            {
                this.inline_typdes_en.SetBounds(rect_typdes_en.X, rect_typdes_en.Y + 1, rect_typdes_en.Width - 1, rect_typdes_en.Height - 3);
            }
        }

        private void ClearInlineForm()
        {
            if (this.inline_typcod != null)
            {
                this.inline_typcod.Dispose();
                this.inline_typcod = null;
            }
            if (this.inline_abbr_th != null)
            {
                this.inline_abbr_th.Dispose();
                this.inline_abbr_th = null;
            }
            if (this.inline_abbr_en != null)
            {
                this.inline_abbr_en.Dispose();
                this.inline_abbr_en = null;
            }
            if (this.inline_typdes_th != null)
            {
                this.inline_typdes_th.Dispose();
                this.inline_typdes_th = null;
            }
            if (this.inline_typdes_en != null)
            {
                this.inline_typdes_en.Dispose();
                this.inline_typdes_en = null;
            }

            this.current_focused_control = null;
        }

        private void FormRead()
        {
            this.form_mode = FORM_MODE.READ;

            this.ClearInlineForm();

            this.toolStripAdd.Enabled = true;
            this.toolStripEdit.Enabled = true;
            this.toolStripDelete.Enabled = true;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripReload.Enabled = true;

            this.dgvIstab.Enabled = true;
            this.toolStripProcessing.Visible = false;
            this.dgvIstab.Focus();
        }

        private void FormAdd()
        {
            this.form_mode = FORM_MODE.ADD;

            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripReload.Enabled = false;

            this.dgvIstab.Enabled = false;
            this.toolStripProcessing.Visible = false;

            if (this.inline_typcod != null)
            {
                this.inline_typcod.Read_Only = false;
            }
            if (this.inline_abbr_th != null)
            {
                this.inline_abbr_th.Read_Only = false;
            }
            if (this.inline_abbr_en != null)
            {
                this.inline_abbr_en.Read_Only = false;
            }
            if (this.inline_typdes_th != null)
            {
                this.inline_typdes_th.Read_Only = false;
            }
            if (this.inline_typdes_en != null)
            {
                this.inline_typdes_en.Read_Only = false;
            }
        }

        private void FormEdit()
        {
            this.form_mode = FORM_MODE.EDIT;

            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripReload.Enabled = false;

            this.dgvIstab.Enabled = false;
            this.toolStripProcessing.Visible = false;

            if (this.inline_typcod != null)
            {
                this.inline_typcod.Read_Only = false;
            }
            if (this.inline_abbr_th != null)
            {
                this.inline_abbr_th.Read_Only = false;
            }
            if (this.inline_abbr_en != null)
            {
                this.inline_abbr_en.Read_Only = false;
            }
            if (this.inline_typdes_th != null)
            {
                this.inline_typdes_th.Read_Only = false;
            }
            if (this.inline_typdes_en != null)
            {
                this.inline_typdes_en.Read_Only = false;
            }
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;

            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripReload.Enabled = false;

            this.dgvIstab.Enabled = false;
            this.toolStripProcessing.Visible = true;

            if (this.inline_typcod != null)
            {
                this.inline_typcod.Read_Only = true;
            }
            if (this.inline_abbr_th != null)
            {
                this.inline_abbr_th.Read_Only = true;
            }
            if (this.inline_abbr_en != null)
            {
                this.inline_abbr_en.Read_Only = true;
            }
            if (this.inline_typdes_th != null)
            {
                this.inline_typdes_th.Read_Only = true;
            }
            if (this.inline_typdes_en != null)
            {
                this.inline_typdes_en.Read_Only = true;
            }
        }

        private void IstabWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.MdiFormClosing && this.form_mode != FORM_MODE.READ)
            {
                this.Activate();
                if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "SN_Net", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (e.CloseReason == CloseReason.UserClosing && this.form_mode != FORM_MODE.READ)
            {
                this.Activate();
                if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "SN_Net", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            switch (this.tabtyp)
            {
                case Istab.TABTYP.AREA:
                    this.main_form.area_wind = null;
                    break;
                case Istab.TABTYP.VEREXT:
                    this.main_form.verext_wind = null;
                    break;
                case Istab.TABTYP.HOWKNOWN:
                    this.main_form.howknown_wind = null;
                    break;
                case Istab.TABTYP.BUSITYP:
                    this.main_form.busityp_wind = null;
                    break;
                case Istab.TABTYP.PROBLEM_CODE:
                    this.main_form.probcode_wind = null;
                    break;
                case Istab.TABTYP.ABSENT_CAUSE:
                    this.main_form.leavecause_wind = null;
                    break;
                case Istab.TABTYP.SERVICE_CASE:
                    this.main_form.servicecase_wind = null;
                    break;
                default:
                    break;
            }
        }

        private Istab GetDataInForm()
        {
            Istab data = new Istab();
            if (this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag is Istab) // edit mode return row_id
            {
                data.id = ((Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag).id;
            }
            else // add mode return -1
            {
                data.id = -1;
            }

            if (this.dgvIstab.Parent.Controls.Find("inline_typcod", true).Length > 0)
            {
                data.typcod = ((CustomTextBox)this.dgvIstab.Parent.Controls.Find("inline_typcod", true)[0]).Texts;
            }
            if (this.dgvIstab.Parent.Controls.Find("inline_abbr_th", true).Length > 0)
            {
                data.abbreviate_th = ((CustomTextBox)this.dgvIstab.Parent.Controls.Find("inline_abbr_th", true)[0]).Texts;
            }
            if (this.dgvIstab.Parent.Controls.Find("inline_abbr_en", true).Length > 0)
            {
                data.abbreviate_en = ((CustomTextBox)this.dgvIstab.Parent.Controls.Find("inline_abbr_en", true)[0]).Texts;
            }
            if (this.dgvIstab.Parent.Controls.Find("inline_typdes_th", true).Length > 0)
            {
                data.typdes_th = ((CustomTextBox)this.dgvIstab.Parent.Controls.Find("inline_typdes_th", true)[0]).Texts;
            }
            if (this.dgvIstab.Parent.Controls.Find("inline_typdes_en", true).Length > 0)
            {
                data.typdes_en = ((CustomTextBox)this.dgvIstab.Parent.Controls.Find("inline_typdes_en", true)[0]).Texts;
            }

            return data;
        }

        private void CreateData()
        {
            this.FormProcessing();
            Istab data = this.GetDataInForm();

            bool post_success = false;
            string err_msg = "";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"tabtyp\":\"" + Istab.getTabtypString(this.tabtyp) + "\",";
                json_data += "\"typcod\":\"" + data.typcod.cleanString() + "\",";
                json_data += "\"abbreviate_th\":\"" + data.abbreviate_th.cleanString() + "\",";
                json_data += "\"abbreviate_en\":\"" + data.abbreviate_en.cleanString() + "\",";
                json_data += "\"typdes_th\":\"" + data.typdes_th.cleanString() + "\",";
                json_data += "\"typdes_en\":\"" + data.typdes_en.cleanString() + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "istab/create", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                }
                else
                {
                    post_success = false;
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (post_success)
                {
                    this.GetIstabData(null, true);
                }
                else
                {
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.FormAdd();
                    if (this.dgvIstab.Parent.Controls.Find("inline_typdes_en", true).Length > 0)
                    {
                        ((CustomTextBox)this.dgvIstab.Parent.Controls.Find("inline_typdes_en", true)[0]).textBox1.Focus();
                    }
                }
            };
            worker.RunWorkerAsync();
        }

        private void UpdateData()
        {
            this.FormProcessing();
            Istab data = this.GetDataInForm();

            bool post_success = false;
            string err_msg = "";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"id\":" + data.id.ToString() + ",";
                json_data += "\"tabtyp\":\"" + Istab.getTabtypString(this.tabtyp) + "\",";
                json_data += "\"typcod\":\"" + data.typcod.cleanString() + "\",";
                json_data += "\"abbreviate_th\":\"" + data.abbreviate_th.cleanString() + "\",";
                json_data += "\"abbreviate_en\":\"" + data.abbreviate_en.cleanString() + "\",";
                json_data += "\"typdes_th\":\"" + data.typdes_th.cleanString() + "\",";
                json_data += "\"typdes_en\":\"" + data.typdes_en.cleanString() + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "istab/submit_change", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                }
                else
                {
                    post_success = false;
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (post_success)
                {
                    this.GetIstabData(data);
                }
                else
                {
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.FormEdit();
                    if (this.dgvIstab.Parent.Controls.Find("inline_typdes_en", true).Length > 0)
                    {
                        ((CustomTextBox)this.dgvIstab.Parent.Controls.Find("inline_typdes_en", true)[0]).textBox1.Focus();
                    }
                }
            };
            worker.RunWorkerAsync();
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            this.dgvIstab.Rows[this.istabs.Count].Cells[1].Selected = true;
            this.ShowInlineForm(1);
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag is Istab)
            {
                this.ShowInlineForm(2);
            }
        }

        private void toolStripDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvIstab.CurrentCell == null)
                return;

            if (this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag is Istab)
            {
                this.dgvIstab.Tag = DGV_TAG.DELETE;

                if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                {
                    this.FormProcessing();

                    Istab delete_item = (Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag;
                    bool delete_success = false;
                    string err_msg = "";

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += delegate
                    {
                        CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "istab/delete&id=" + ((Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag).id.ToString());
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            delete_success = true;
                        }
                        else
                        {
                            delete_success = false;
                            err_msg = sr.message;
                        }
                    };
                    worker.RunWorkerCompleted += delegate
                    {
                        if (delete_success)
                        {
                            int delete_row_index = this.dgvIstab.Rows.Cast<DataGridViewRow>().Where(r => ((Istab)r.Tag).id == delete_item.id).First<DataGridViewRow>().Cells[1].RowIndex;
                            this.GetIstabData(null, false, delete_row_index);
                        }
                        else
                        {
                            MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                            this.dgvIstab.Tag = DGV_TAG.NORMAL;
                            this.FormRead();
                        }
                    };
                    worker.RunWorkerAsync();
                }
                else
                {
                    this.FillInDatagrid((Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag);
                }
            }
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            this.ClearInlineForm();
            this.FormRead();
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.ADD)
            {
                this.CreateData();
                return;
            }
            if (this.form_mode == FORM_MODE.EDIT)
            {
                this.UpdateData();
                return;
            }
        }

        private void toolStripReload_Click(object sender, EventArgs e)
        {
            this.GetIstabData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
                {
                    if (this.dgvIstab.Parent.Controls.Find("inline_typdes_en", true).Length > 0)
                    {
                        if (((CustomTextBox)this.dgvIstab.Parent.Controls.Find("inline_typdes_en", true)[0]).textBox1.Focused)
                        {
                            this.toolStripSave.PerformClick();
                            return true;
                        }
                    }

                    SendKeys.Send("{TAB}");
                    return true;
                }
                if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.PROCESSING)
                {
                    return true;
                }
            }
            if (keyData == Keys.Escape)
            {
                this.toolStripStop.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.A))
            {
                this.toolStripAdd.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.E))
            {
                this.toolStripEdit.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D))
            {
                this.toolStripDelete.PerformClick();
                return true;
            }
            if (keyData == Keys.F5)
            {
                this.toolStripReload.PerformClick();
                return true;
            }
            if (keyData == Keys.F9)
            {
                this.toolStripSave.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void IstabWindow_Activated(object sender, EventArgs e)
        {
            Console.WriteLine(" >> " + this.Text + " is activate");
            if (this.current_focused_control != null)
            {
                Console.WriteLine(" >>> " + this.current_focused_control.Name + " is focus");
                this.current_focused_control.Focus();
            }
        }

        public static List<Istab> GetIstab(string tabtyp)
        {
                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_all&tabtyp=" + tabtyp + "&sort=typcod");
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    return sr.istab;
                }
                else
                {
                    return new List<Istab>();
                }
        }
    }
}