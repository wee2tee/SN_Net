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
    public enum TABTYP
    {
        AREA,
        BUSITYP,
        HOWKNOWN,
        PROBCOD,
        VEREXT
    }

    public partial class DialogInquiryIstab : Form
    {
        private TABTYP tabtyp;
        private BindingList<istabVM> istab_list;
        public istab selected_istab;

        public DialogInquiryIstab(TABTYP tabtyp, istab selected_istab = null, List<istab> exclude_istab = null)
        {
            InitializeComponent();
            this.tabtyp = tabtyp;
            if(exclude_istab == null)
            {
                this.istab_list = new BindingList<istabVM>(GetIstab(this.tabtyp).OrderBy(s => s.typcod).ToViewModel());
            }
            else
            {
                this.istab_list = new BindingList<istabVM>(GetIstab(this.tabtyp, exclude_istab).OrderBy(s => s.typcod).ToViewModel());
                //exclude_istab.ForEach(x => this.istab_list.Remove(this.istab_list.Where(i => i.id == x.id).FirstOrDefault()));
            }
                
            this.dgv.DataSource = this.istab_list;

            if(selected_istab != null) // has initial selected istab
            {
                var focused_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == selected_istab.id).FirstOrDefault();

                if (focused_row != null)
                    focused_row.Cells[this.col_typcod.Name].Selected = true;
            }
            else
            {
                //if(this.dgv.Rows.Count > 0)
                //{
                //    this.selected_istab = (istab)this.dgv.Rows[0].Cells[this.col_istab.Name].Value;
                //}
                this.selected_istab = this.istab_list.First().istab;
            }

            this.ActiveControl = this.dgv;
        }

        public static List<istab> GetIstab(TABTYP tabtyp, List<istab> exclude_istabs = null)
        {
            using (snEntities sn = DBX.DataSet())
            {
                string tabtyp_str = tabtyp.GetTabtypString();
                int[] arr_exclude_ids = exclude_istabs != null ? exclude_istabs.Select(x => x.id).ToArray() : new int[0];
                var istabs = sn.istab.Where(s => s.flag == 0 && s.tabtyp == tabtyp_str && !(arr_exclude_ids.Contains(s.id))).ToList();

                return istabs;
            }
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((XDatagrid)sender).CurrentCell == null)
                return;

            this.selected_istab = (istab)((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex].Cells[this.col_istab.Name].Value;
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                this.btnOK.PerformClick();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            DialogInlineSearch s = new DialogInlineSearch();
            Point p = new Point(this.dgv.PointToScreen(Point.Empty).X, this.dgv.PointToScreen(Point.Empty).Y + this.dgv.Height);
            s.Location = p;
            if (s.ShowDialog() == DialogResult.OK)
            {
                var focused_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => ((string)r.Cells[this.col_typcod.Name].Value).CompareTo(s.key_word) >= 0).FirstOrDefault();

                if(focused_row != null)
                {
                    focused_row.Cells[this.col_typcod.Name].Selected = true;
                }
            }
            this.ActiveControl = this.dgv;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
                return;

            DialogInlineSearch s = new DialogInlineSearch(e.KeyChar.ToString());
            Point p = new Point(this.dgv.PointToScreen(Point.Empty).X, this.dgv.PointToScreen(Point.Empty).Y + this.dgv.Height);
            s.Location = p;
            if(s.ShowDialog() == DialogResult.OK)
            {
                var focused_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => ((string)r.Cells[this.col_typcod.Name].Value).CompareTo(s.key_word) >= 0).FirstOrDefault();

                if (focused_row != null)
                {
                    focused_row.Cells[this.col_typcod.Name].Selected = true;
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
