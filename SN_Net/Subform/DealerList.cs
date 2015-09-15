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
    public partial class DealerList : Form
    {
        public Dealer dealer; // the selected dealer
        SnWindow parent_window;
        private string selected_dealer_code;
        private SORT_MODE sort_mode;
        private enum SORT_MODE
        {
            DEALER,
            COMPNAM
        }

        public DealerList()
        {
            InitializeComponent();
        }

        public DealerList(SnWindow parent_window, string dealer_code)
            : this()
        {

            this.parent_window = parent_window;
            this.selected_dealer_code = dealer_code;
            this.sort_mode = SORT_MODE.DEALER;
            this.FillInDatagrid();
            this.SetSelectedItem();
        }

        private void DealerList_Shown(object sender, EventArgs e)
        {
            this.dgvDealer.Focus();
        }

        private List<Dealer> PrepareDealerList()
        {
            if (this.sort_mode == SORT_MODE.DEALER)
            {
                return this.parent_window.main_form.data_resource.LIST_DEALER.OrderBy(t => t.dealer, new CompareStrings()).ToList<Dealer>();
            }
            else
            {
                return this.parent_window.main_form.data_resource.LIST_DEALER.OrderBy(t => t.compnam, new CompareStrings()).ToList<Dealer>();
            }
        }

        private void FillInDatagrid()
        {
            // initialize
            this.dgvDealer.Columns.Clear();
            this.dgvDealer.Rows.Clear();
            this.dgvDealer.EnableHeadersVisualStyles = false;
            this.dgvDealer.ColumnHeadersDefaultCellStyle.BackColor = Color.YellowGreen;

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
            this.dgvDealer.Columns.Add(text_col1);

            DataGridViewTextBoxColumn text_col2 = new DataGridViewTextBoxColumn();
            text_col2.HeaderText = "รหัส";
            text_col2.Width = 100;
            text_col2.SortMode = DataGridViewColumnSortMode.Programmatic;
            text_col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3),
                BackColor = (this.sort_mode == SORT_MODE.DEALER ? Color.OliveDrab : ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN)
            };
            this.dgvDealer.Columns.Add(text_col2);

            DataGridViewTextBoxColumn text_col3 = new DataGridViewTextBoxColumn();
            text_col3.HeaderText = "รายละเอียด";
            text_col3.Width = this.dgvDealer.ClientSize.Width - (text_col2.Width + 3);
            text_col3.SortMode = DataGridViewColumnSortMode.Programmatic;
            text_col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3),
                BackColor = (this.sort_mode == SORT_MODE.COMPNAM ? Color.OliveDrab : ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN)
            };
            this.dgvDealer.Columns.Add(text_col3);

            foreach (Dealer dealer in this.PrepareDealerList())
            {
                int r = this.dgvDealer.Rows.Add();
                this.dgvDealer.Rows[r].Tag = (Dealer)dealer;

                this.dgvDealer.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvDealer.Rows[r].Cells[0].Value = dealer.id;
                this.dgvDealer.Rows[r].Cells[0].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleRight
                };

                this.dgvDealer.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvDealer.Rows[r].Cells[1].Value = dealer.dealer;
                this.dgvDealer.Rows[r].Cells[1].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,

                    Alignment = DataGridViewContentAlignment.MiddleLeft
                };


                this.dgvDealer.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvDealer.Rows[r].Cells[2].Value = dealer.compnam;
                this.dgvDealer.Rows[r].Cells[2].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,

                    Alignment = DataGridViewContentAlignment.MiddleLeft
                };
            }

            //this.setFocusedDealer();
        }

        private void SetSelectedItem(Dealer selected_item = null)
        {
            if (selected_item != null)
            {
                foreach (DataGridViewRow row in this.dgvDealer.Rows)
                {
                    if (((Dealer)row.Tag).id == selected_item.id)
                    {
                        row.Cells[1].Selected = true;
                        Console.WriteLine(row.Cells[1].Value.ToString());
                        break;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in this.dgvDealer.Rows)
                {
                    if (string.CompareOrdinal(this.selected_dealer_code, ((Dealer)row.Tag).dealer) <= 0)
                    {
                        row.Cells[1].Selected = true;
                        Console.WriteLine(row.Cells[1].Value.ToString());
                        break;
                    }
                }
            }
        }

        private void DealerList_Resize(object sender, EventArgs e)
        {
            this.dgvDealer.Columns[2].Width = this.dgvDealer.ClientSize.Width - (this.dgvDealer.Columns[1].Width + 3);
        }

        private void dgvDealer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.returnSelectedResult();
            }
        }

        private void returnSelectedResult()
        {
            this.dealer = (Dealer)this.dgvDealer.Rows[this.dgvDealer.CurrentCell.RowIndex].Tag;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.returnSelectedResult();
        }

        private void dgvDealer_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewColumn col in ((DataGridView)sender).Columns)
            {
                col.HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN;
            }
            ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.OliveDrab;

            Dealer current_item = (Dealer)this.dgvDealer.Rows[this.dgvDealer.CurrentCell.RowIndex].Tag;

            if (e.ColumnIndex == 1 && this.sort_mode == SORT_MODE.COMPNAM)
            {
                this.sort_mode = SORT_MODE.DEALER;
                this.FillInDatagrid();
                this.SetSelectedItem(current_item);
            }
            else if (e.ColumnIndex == 2 && this.sort_mode == SORT_MODE.DEALER)
            {
                this.sort_mode = SORT_MODE.COMPNAM;
                this.FillInDatagrid();
                this.SetSelectedItem(current_item);
            }
        }

        private void dgvDealer_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            ((DataGridView)sender).SetRowSelectedBorder(e);
        }

        private void DealerList_KeyPress(object sender, KeyPressEventArgs e)
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
                    this.PerformSearch(s.txtKeyword.Text);
                }
            }
        }

        private void PerformSearch(string keyword)
        {
            switch (this.sort_mode)
            {
                case SORT_MODE.DEALER:
                    this.dgvDealer.Search(keyword, 1);
                    break;
                case SORT_MODE.COMPNAM:
                    this.dgvDealer.Search(keyword, 2);
                    break;
                default:
                    break;
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
            if (keyData == Keys.Tab)
            {
                Dealer current_item = (Dealer)this.dgvDealer.Rows[this.dgvDealer.CurrentCell.RowIndex].Tag;
                if (this.sort_mode == SORT_MODE.DEALER)
                {
                    this.sort_mode = SORT_MODE.COMPNAM;
                    this.dgvDealer.Columns[1].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN;
                    this.dgvDealer.Columns[2].HeaderCell.Style.BackColor = Color.OliveDrab;
                }
                else
                {
                    this.sort_mode = SORT_MODE.DEALER;
                    this.dgvDealer.Columns[1].HeaderCell.Style.BackColor = Color.OliveDrab;
                    this.dgvDealer.Columns[2].HeaderCell.Style.BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN; 
                }
                this.FillInDatagrid();
                this.SetSelectedItem(current_item);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
