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
        private Istab.TABTYP tabtyp;
        public Istab istab;
        private string selected_typcod;

        public IstabList(string typcod, Istab.TABTYP tabtyp)
        {
            InitializeComponent();
            this.tabtyp = tabtyp;
            this.selected_typcod = typcod;
            this.setTitleText();
            this.sort_by = SORT_TYPCOD;
        }

        private void setSelectedItem(){
            foreach (DataGridViewRow row in this.dgvIstab.Rows)
	        {
                if (((Istab)row.Tag).typcod == this.selected_typcod && ((Istab)row.Tag).tabtyp == this.tabtyp.ToTabtypString())
                {
                    row.Cells[1].Selected = true;
                    break;
                }
	        }
            this.dgvIstab.Focus();
        }

        private void IstabList_Shown(object sender, EventArgs e)
        {
            this.dgvIstab.Focus();
            EscapeKeyToCloseDialog.ActiveEscToClose(this);
        }

        private void setTitleText()
        {
            this.Text = Istab.getTabtypTitle(this.tabtyp);
        }

        private void IstabList_Load(object sender, EventArgs e)
        {
            this.fillInDataGrid(this.loadIstabData(this.sort_by));
        }

        private List<Istab> loadIstabData(string sort_by)
        {
            List<Istab> istabs = new List<Istab>();
            CRUDResult get = ApiActions.GET(ApiConfig.API_MAIN_URL + "istab/get_all&tabtyp=" + Istab.getTabtypString(this.tabtyp) + "&sort=" + sort_by);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                istabs = sr.istab;
            }

            return istabs;
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
            text_col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3),
                BackColor = Color.OliveDrab
            };
            this.dgvIstab.Columns.Add(text_col2);

            DataGridViewTextBoxColumn text_col3 = new DataGridViewTextBoxColumn();
            text_col3.HeaderText = "รายละเอียด";
            text_col3.Width = this.dgvIstab.ClientSize.Width - (text_col2.Width + 3);
            text_col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3)
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
            this.dgvIstab.Columns[2].Width = this.dgvIstab.ClientSize.Width - (this.dgvIstab.Columns[1].Width + 3);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IstabAddEditForm wind = new IstabAddEditForm(IstabAddEditForm.FORM_MODE.ADD, this.tabtyp);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.selected_typcod = wind.istab.typcod;
                this.fillInDataGrid(this.loadIstabData(this.sort_by));
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

        private void dgvIstab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                this.returnSelectedResult();
            }
            else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Alt)
            {
                this.btnAdd.PerformClick();
            }
            else if (e.KeyCode == Keys.E && e.Modifiers == Keys.Alt)
            {
                Istab istab = (Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag;
                this.showEditForm(istab);
            }
            else if (e.KeyCode == Keys.D && e.Modifiers == Keys.Alt)
            {
                Istab istab = (Istab)this.dgvIstab.Rows[this.dgvIstab.CurrentCell.RowIndex].Tag;
                this.showConfirmDelete(istab);
            }
            else if (e.KeyCode == Keys.S && e.Modifiers == Keys.Alt)
            {
                this.btnSearch.PerformClick();
            }
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
                int currentMouseOverRow = this.dgvIstab.HitTest(e.X, e.Y).RowIndex;
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
                this.selected_typcod = wind.istab.typcod;
                this.fillInDataGrid(this.loadIstabData(this.sort_by));
            }
        }

        private void showConfirmDelete(Istab istab)
        {
            if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.YES_NO, MessageAlertIcons.QUESTION) == DialogResult.Yes)
            {
                CRUDResult delete = ApiActions.DELETE(ApiConfig.API_MAIN_URL + "istab/delete&id=" + istab.id);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.fillInDataGrid(this.loadIstabData(sort_by));
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchBox s = new SearchBox();
            if (s.ShowDialog() == DialogResult.OK)
            {
                this.performSearch(s.txtKeyword.Text);
            }
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

        private void dgvIstab_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewColumn col in ((DataGridView)sender).Columns)
            {
                col.HeaderCell.Style.BackColor = Color.YellowGreen;
            }
            ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.OliveDrab;

            if (e.ColumnIndex == 1)
            {
                this.sort_by = SORT_TYPCOD;
            }
            else if(e.ColumnIndex == 2){
                this.sort_by = SORT_TYPDES;
            }
        }

        private void dgvIstab_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            ((DataGridView)sender).SetRowSelectedBorder(e);
        }

    }
}
