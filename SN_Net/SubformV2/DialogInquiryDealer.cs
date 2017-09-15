using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.Model;
using CC;

namespace SN_Net.Subform
{
    public partial class DialogInquiryDealer : Form
    {
        private BindingList<dealerVM> dealer_list;
        public dealer selected_dealer;

        public DialogInquiryDealer(dealer selected_dealer = null)
        {
            InitializeComponent();
            this.dealer_list = new BindingList<dealerVM>(GetDealer().ToViewModel());
            this.dgv.DataSource = this.dealer_list;

            if(selected_dealer != null)
            {
                var focused_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == selected_dealer.id).FirstOrDefault();
                if(focused_row != null)
                {
                    focused_row.Cells[this.col_dealercod.Name].Selected = true;
                }
            }

            this.ActiveControl = this.dgv;
        }

        public static List<dealer> GetDealer()
        {
            using (snEntities sn = DBX.DataSet())
            {
                return sn.dealer.Where(d => d.flag == 0).OrderBy(d => d.dealercod).ToList();
            }
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((XDatagrid)sender).CurrentCell == null)
                return;

            this.selected_dealer = (dealer)((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex].Cells[this.col_dealer.Name].Value;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            DialogInlineSearch s = new DialogInlineSearch();
            Point p = new Point(this.dgv.PointToScreen(Point.Empty).X, this.dgv.PointToScreen(Point.Empty).Y + this.dgv.Height);
            s.Location = p;
            if (s.ShowDialog() == DialogResult.OK)
            {
                var focused_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => ((string)r.Cells[this.col_dealercod.Name].Value).CompareTo(s.key_word) >= 0).FirstOrDefault();

                if (focused_row != null)
                {
                    focused_row.Cells[this.col_dealercod.Name].Selected = true;
                }
            }
            this.ActiveControl = this.dgv;
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                this.btnOK.PerformClick();
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
                return;

            DialogInlineSearch s = new DialogInlineSearch(e.KeyChar.ToString());
            Point p = new Point(this.dgv.PointToScreen(Point.Empty).X, this.dgv.PointToScreen(Point.Empty).Y + this.dgv.Height);
            s.Location = p;
            if (s.ShowDialog() == DialogResult.OK)
            {
                var focused_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => ((string)r.Cells[this.col_dealercod.Name].Value).CompareTo(s.key_word) >= 0).FirstOrDefault();

                if (focused_row != null)
                {
                    focused_row.Cells[this.col_dealercod.Name].Selected = true;
                }
            }
            this.ActiveControl = this.dgv;
            e.Handled = true;
            //base.OnKeyPress(e);
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
