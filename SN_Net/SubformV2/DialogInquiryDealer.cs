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
    public partial class DialogInquiryDealer : Form
    {
        public enum SORT_BY
        {
            DEALERCOD,
            COMPNAM,
            AREA,
            CONTACT
        }
        private SORT_BY sort_by;
        private BindingList<dealerVM> dealer_list = null;
        public dealer selected_dealer = null;
        private List<dealer> dealers_to_show = null; // passing from FormDealer to show dealers (inquiry condition)

        public DialogInquiryDealer()
        {
            InitializeComponent();
        }

        public DialogInquiryDealer(SORT_BY sort_by = SORT_BY.DEALERCOD, dealer selected_dealer = null)
            : this()
        {
            this.sort_by = sort_by;
            this.selected_dealer = selected_dealer;
        }

        public DialogInquiryDealer(List<dealer> dealers_to_show)
            : this()
        {
            this.dealers_to_show = dealers_to_show;
        }

        private void DialogInquiryDealer_Load(object sender, EventArgs e)
        {
            if(this.dealers_to_show != null) // inquiry with condition
            {
                this.dealer_list = new BindingList<dealerVM>(this.dealers_to_show.ToViewModel());
            }
            else // ordinary case
            {
                this.dealer_list = new BindingList<dealerVM>(this.GetDealerList().ToViewModel());
            }
            
            this.dgv.DataSource = this.dealer_list;
        }

        private List<dealer> GetDealerList()
        {
            using (snEntities sn = DBX.DataSet())
            {
                switch (this.sort_by)
                {
                    case SORT_BY.DEALERCOD:
                        return sn.dealer.Where(d => d.flag == 0).OrderBy(d => d.dealercod).ToList();
                    case SORT_BY.COMPNAM:
                        return sn.dealer.Where(d => d.flag == 0).OrderBy(d => d.compnam).ThenBy(d => d.dealercod).ToList();
                    case SORT_BY.AREA:
                        return sn.dealer.Where(d => d.flag == 0).OrderBy(d => d.area).ThenBy(d => d.dealercod).ToList();
                    case SORT_BY.CONTACT:
                        return sn.dealer.Where(d => d.flag == 0).OrderBy(d => d.contact).ThenBy(d => d.dealercod).ToList();
                    default:
                        return sn.dealer.Where(d => d.flag == 0).OrderBy(d => d.dealercod).ToList();
                }
            }
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((XDatagrid)sender).CurrentCell == null)
            {
                this.btnOK.Enabled = false;
                return;
            }

            this.btnOK.Enabled = true;
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
                this.btnOK.PerformClick();
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                int col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_dealercod.Name).First().Index;
                switch (this.sort_by)
                {
                    case SORT_BY.DEALERCOD:
                        break;
                    case SORT_BY.CONTACT:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_contact.Name).First().Index;
                        break;
                    case SORT_BY.COMPNAM:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_compnam.Name).First().Index;
                        break;
                    case SORT_BY.AREA:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_area.Name).First().Index;
                        break;
                    default:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_dealercod.Name).First().Index;
                        break;
                }

                if (e.ColumnIndex == col_index)
                {
                    using (SolidBrush brush = new SolidBrush(Color.DarkOliveGreen))
                    {
                        e.CellStyle.BackColor = Color.YellowGreen;
                        e.CellStyle.SelectionBackColor = Color.YellowGreen;
                        e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                        e.Graphics.FillPolygon(brush, new Point[] { new Point(e.CellBounds.X + e.CellBounds.Width - 18, e.CellBounds.Y + 18), new Point(e.CellBounds.X + e.CellBounds.Width - 10, e.CellBounds.Y + 18), new Point(e.CellBounds.X + e.CellBounds.Width - 14, e.CellBounds.Y + 10) });
                        e.Handled = true;
                    }
                }
            }
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.selected_dealer != null)
            {
                var selected = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == this.selected_dealer.id).FirstOrDefault();
                if (selected != null)
                {
                    this.dgv.FirstDisplayedScrollingRowIndex = this.dgv.Rows.IndexOf(selected);
                    selected.Cells[this.col_compnam.Name].Selected = true;
                }
            }

            if (((XDatagrid)sender).CurrentCell != null && ((XDatagrid)sender).Rows.Count > 0)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.dgv.CurrentCell == null)
                return;

            this.selected_dealer = (dealer)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_dealer.Name].Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
                return true;
            }

            if(keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
