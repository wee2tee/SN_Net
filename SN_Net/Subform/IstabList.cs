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
    public partial class IstabList : Form
    {
        private const string SORT_TYPCOD = "typcod";
        private const string SORT_TYPDES = "typdes";
        private string sort_by;
        //private SnWindow parent_window;
        private MainForm main_form;
        private Istab.TABTYP tabtyp;
        public Istab istab;
        private string selected_typcod;
        private List<Istab> passing_list;

        public IstabList()
        {
            InitializeComponent();
        }

        //public IstabList(SnWindow parent_window, string typcod, Istab.TABTYP tabtyp)
        public IstabList(MainForm main_form, string typcod, Istab.TABTYP tabtyp)
            :   this()
        {
            //this.parent_window = parent_window;
            this.main_form = main_form;
            this.tabtyp = tabtyp;
            this.selected_typcod = typcod;
            this.setTitleText();
            this.sort_by = SORT_TYPCOD;
        }

        public IstabList(MainForm main_form, string typcod, Istab.TABTYP tabtyp, List<Istab> list_istab)
            : this(main_form, typcod, tabtyp)
        {
            this.passing_list = list_istab;
        }

        private void IstabList_Shown(object sender, EventArgs e)
        {
            this.dgvIstab.Focus();
        }

        private void setSelectedItem(Istab selected_item = null)
        {
            if (selected_item == null)
            {
                foreach (DataGridViewRow row in this.dgvIstab.Rows)
                {
                    if (string.CompareOrdinal(this.selected_typcod, ((Istab)row.Tag).typcod) <= 0)
                    {
                        row.Cells[1].Selected = true;
                        break;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in this.dgvIstab.Rows)
                {
                    if (((Istab)row.Tag).typcod == selected_item.typcod)
                    {
                        row.Cells[1].Selected = true;
                        break;
                    }
                }
            }
            this.dgvIstab.Focus();
        }

        private void setTitleText()
        {
            this.Text = Istab.getTabtypTitle(this.tabtyp);
        }

        private void IstabList_Load(object sender, EventArgs e)
        {
            //if (this.passing_list != null)
            //{
            //    this.fillInDataGrid(this.passing_list);
            //}
            //else
            //{
                this.fillInDataGrid(this.WhichDataToUse());
            //}
        }

        private void fillInDataGrid(List<Istab> istabs)
        {
            // Clear old data
            this.dgvIstab.Columns.Clear();
            this.dgvIstab.Rows.Clear();
            this.dgvIstab.EnableHeadersVisualStyles = false;
            this.dgvIstab.ColumnHeadersDefaultCellStyle.BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN;

            // Create column
            // ID
            DataGridViewTextBoxColumn text_col1 = new DataGridViewTextBoxColumn();
            text_col1.HeaderText = "ID.";
            text_col1.Width = 40;
            text_col1.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3)
            };
            text_col1.Visible = false;
            this.dgvIstab.Columns.Add(text_col1);

            DataGridViewTextBoxColumn text_col2 = new DataGridViewTextBoxColumn();
            text_col2.HeaderText = "รหัส";
            text_col2.Width = 100;
            text_col2.SortMode = DataGridViewColumnSortMode.Programmatic;
            text_col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3),
                BackColor = (this.sort_by == SORT_TYPCOD ? Color.OliveDrab : ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN)
            };
            this.dgvIstab.Columns.Add(text_col2);

            DataGridViewTextBoxColumn text_col3 = new DataGridViewTextBoxColumn();
            text_col3.HeaderText = "รายละเอียด";
            //text_col3.Width = this.dgvIstab.ClientSize.Width - (text_col2.Width + 3);
            text_col3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            text_col3.SortMode = DataGridViewColumnSortMode.Programmatic;
            text_col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3),
                BackColor = (this.sort_by == SORT_TYPDES ? Color.OliveDrab : ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN)
            };
            this.dgvIstab.Columns.Add(text_col3);

            foreach (Istab istab in istabs)
            {
                int r = this.dgvIstab.Rows.Add();
                this.dgvIstab.Rows[r].Tag = (Istab)istab;

                this.dgvIstab.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvIstab.Rows[r].Cells[0].Value = istab.id;
                this.dgvIstab.Rows[r].Cells[0].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleRight
                };

                this.dgvIstab.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvIstab.Rows[r].Cells[1].Value = istab.typcod;
                this.dgvIstab.Rows[r].Cells[1].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                };


                this.dgvIstab.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvIstab.Rows[r].Cells[2].Value = istab.typdes_th;
                this.dgvIstab.Rows[r].Cells[2].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                };
            }

            this.setSelectedItem();
        }

        private void IstabList_Resize(object sender, EventArgs e)
        {
            //this.dgvIstab.Columns[2].Width = this.dgvIstab.ClientSize.Width - (this.dgvIstab.Columns[1].Width + 3);
        }

        private List<Istab> WhichDataToUse()
        {
            if (this.passing_list != null)
            {
                return (this.sort_by == SORT_TYPCOD ? this.passing_list.OrderBy(t => t.typcod, new CompareStrings()).ToList<Istab>() : this.passing_list.OrderBy(t => t.typdes_th, new CompareStrings()).ToList<Istab>());
            }

            switch (this.tabtyp)
            {
                case Istab.TABTYP.AREA:
                    return (this.sort_by == SORT_TYPCOD ? this.main_form.data_resource.LIST_AREA.OrderBy(t => t.typcod, new CompareStrings()).ToList<Istab>() : this.main_form.data_resource.LIST_AREA.OrderBy(t => t.typdes_th, new CompareStrings()).ToList<Istab>());
                case Istab.TABTYP.VEREXT:
                    return (this.sort_by == SORT_TYPCOD ? this.main_form.data_resource.LIST_VEREXT.OrderBy(t => t.typcod, new CompareStrings()).ToList<Istab>() : this.main_form.data_resource.LIST_VEREXT.OrderBy(t => t.typdes_th, new CompareStrings()).ToList<Istab>());
                case Istab.TABTYP.HOWKNOWN:
                    return (this.sort_by == SORT_TYPCOD ? this.main_form.data_resource.LIST_HOWKNOWN.OrderBy(t => t.typcod, new CompareStrings()).ToList<Istab>() : this.main_form.data_resource.LIST_HOWKNOWN.OrderBy(t => t.typdes_th, new CompareStrings()).ToList<Istab>());
                case Istab.TABTYP.BUSITYP:
                    return (this.sort_by == SORT_TYPCOD ? this.main_form.data_resource.LIST_BUSITYP.OrderBy(t => t.typcod, new CompareStrings()).ToList<Istab>() : this.main_form.data_resource.LIST_BUSITYP.OrderBy(t => t.typdes_th, new CompareStrings()).ToList<Istab>());
                case Istab.TABTYP.PROBLEM_CODE:
                    return (this.sort_by == SORT_TYPCOD ? this.main_form.data_resource.LIST_PROBLEM_CODE.OrderBy(t => t.typcod, new CompareStrings()).ToList<Istab>() : this.main_form.data_resource.LIST_PROBLEM_CODE.OrderBy(t => t.typdes_th, new CompareStrings()).ToList<Istab>());
                default:
                    return new List<Istab>();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IstabAddEditForm wind = new IstabAddEditForm(IstabAddEditForm.FORM_MODE.ADD, this.tabtyp);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.main_form.data_resource.Refresh();
                this.fillInDataGrid(this.WhichDataToUse());
                this.setSelectedItem(wind.istab);
                this.dgvIstab.Focus();
            }
            else
            {
                this.dgvIstab.Focus();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Istab istab = (Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag;
            this.showEditForm(istab);
        }

        private void returnSelectedResult()
        {
            this.istab = (Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dgvIstab_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.returnSelectedResult();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.returnSelectedResult();
        }

        private void dgvIstab_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.passing_list != null)
                    return;

                int currentMouseOverRow = this.dgvIstab.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow > -1)
                {
                    this.dgvIstab.Rows[currentMouseOverRow].Selected = true;

                    ContextMenu m = new ContextMenu();
                    MenuItem mnu_edit = new MenuItem("แก้ไข");
                    mnu_edit.Tag = (Istab)this.dgvIstab.Rows[currentMouseOverRow].Tag;
                    mnu_edit.Click += this.performEdit;
                    m.MenuItems.Add(mnu_edit);

                    MenuItem mnu_delete = new MenuItem("ลบ");
                    mnu_delete.Tag = (Istab)this.dgvIstab.Rows[currentMouseOverRow].Tag;
                    mnu_delete.Click += this.performDelete;
                    m.MenuItems.Add(mnu_delete);

                    m.Show(this.dgvIstab, new Point(e.X, e.Y));
                }
            }
        }

        private void performDelete(object sender, EventArgs e)
        {
            Istab istab = (Istab)((MenuItem)sender).Tag;
            this.showConfirmDelete(istab);
        }

        private void performEdit(object sender, EventArgs e)
        {
            Istab istab = (Istab)((MenuItem)sender).Tag;
            this.showEditForm(istab);
        }

        private void showEditForm(Istab istab)
        {
            IstabAddEditForm wind = new IstabAddEditForm(IstabAddEditForm.FORM_MODE.EDIT, this.tabtyp, istab);
            
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.main_form.data_resource.Refresh();
                this.fillInDataGrid(this.WhichDataToUse());
                this.setSelectedItem(wind.istab);
                this.dgvIstab.Focus();
            }
            else
            {
                this.dgvIstab.Focus();
            }
        }

        private void showConfirmDelete(Istab istab)
        {
            if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.YES_NO, MessageAlertIcons.QUESTION) == DialogResult.Yes)
            {
                CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "istab/delete&id=" + istab.id);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.main_form.data_resource.Refresh();
                    this.fillInDataGrid(this.WhichDataToUse());
                    this.dgvIstab.Focus();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.dgvIstab.Focus();
                }
            }
            else
            {
                this.dgvIstab.Focus();
            }
        }

        private void dgvIstab_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewColumn col in ((DataGridView)sender).Columns)
            {
                col.HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN;
            }
            ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.OliveDrab;

            Istab current_item = (Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag;

            if (e.ColumnIndex == 1)
            {
                this.sort_by = SORT_TYPCOD;
                this.fillInDataGrid(this.WhichDataToUse());
            }
            else if(e.ColumnIndex == 2){
                this.sort_by = SORT_TYPDES;
                this.fillInDataGrid(this.WhichDataToUse());
            }

            this.setSelectedItem(current_item);
        }

        private void dgvIstab_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            ((DataGridView)sender).SetRowSelectedBorder(e);
        }

        private void performSearch(string keyword)
        {
            switch (this.sort_by)
            {
                case SORT_TYPCOD:
                    this.dgvIstab.Search(keyword, 1);
                    break;

                case SORT_TYPDES:
                    this.dgvIstab.Search(keyword, 2);
                    break;

                default:
                    break;
            }
        }

        private void IstabList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Length > 0)
            {
                SearchBox s = new SearchBox();
                s.txtKeyword.Text = e.KeyChar.ToString();
                s.txtKeyword.SelectionStart = s.txtKeyword.Text.Length;
                s.Location = new Point(this.Location.X + 8, this.Location.Y + this.ClientSize.Height - 25);
                s.SetBounds(s.Location.X, s.Location.Y, this.ClientSize.Width, s.ClientSize.Height);
                s.txtKeyword.SetBounds(s.txtKeyword.Location.X, s.txtKeyword.Location.Y, s.ClientSize.Width - 63, s.txtKeyword.ClientSize.Height);

                if (s.ShowDialog() == DialogResult.OK)
                {
                    this.performSearch(s.txtKeyword.Text);
                }
            }
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }
            if (keyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
                return true;
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
            if (keyData == (Keys.Alt | Keys.D))
            {
                Istab istab = (Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag;
                this.showConfirmDelete(istab);
                return true;
            }
            if (keyData == (Keys.Tab))
            {
                // do re-order column
                Istab current_item = (Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag;

                if (this.sort_by == SORT_TYPCOD)
                {
                    this.sort_by = SORT_TYPDES;
                    this.fillInDataGrid(this.WhichDataToUse());
                    this.dgvIstab.Columns[2].HeaderCell.Style.BackColor = Color.OliveDrab;
                    this.dgvIstab.Columns[1].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN;
                }
                else if (this.sort_by == SORT_TYPDES)
                {
                    this.sort_by = SORT_TYPCOD;
                    this.fillInDataGrid(this.WhichDataToUse());
                    this.dgvIstab.Columns[1].HeaderCell.Style.BackColor = Color.OliveDrab;
                    this.dgvIstab.Columns[2].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN;
                }
                this.setSelectedItem(current_item);

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
