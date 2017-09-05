using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC.Dialog
{
    public partial class ListDialog : Form
    {
        private Object list_object;
        private BindingSource bs;
        //public object selected_object;
        //public int selected_id;
        public DataGridViewRow selected_row;
        private XBrowseBox xbrowse;

        public ListDialog()
        {
            InitializeComponent();
        }

        public ListDialog(Form parent_form, XBrowseBox xbrowse, string title, Object list_object)
            : this()
        {
            this.Text = title;
            this.Owner = parent_form;
            this.xbrowse = xbrowse;

            this.list_object = list_object;
        }

        private void ListDialog_Load(object sender, EventArgs e)
        {
            this.bs = new BindingSource();
            this.bs.DataSource = this.list_object;
            this.dgv.DataSource = this.bs;
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((XDatagrid)sender).Columns[((XDatagrid)sender).Columns.GetLastColumn(DataGridViewElementStates.None, DataGridViewElementStates.None).Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if(this.xbrowse != null)
            {
                DataGridViewRow focused_row = ((XDatagrid)sender).Rows.Cast<DataGridViewRow>().Where(r => r.Cells[this.xbrowse.FieldNameTextBoxShow].Value.ToString() == this.xbrowse._textBox.Text).FirstOrDefault();

                if(focused_row != null)
                {
                    ((XDatagrid)sender).CurrentCell = ((XDatagrid)sender).Rows[focused_row.Index].Cells[this.xbrowse.FieldNameTextBoxShow];
                }
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (((XDatagrid)sender).CurrentCell == null)
                return;

            //this.selected_object = ((List<object>)this.list_object).Where(o => (int)o.GetType().GetProperty("id").GetValue(o, null) == (int)((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex].Cells["col_id"].Value).First();

            //Console.WriteLine(".. >> selected_object.id = " + this.selected_object.GetType().GetProperty("id").GetValue(this.selected_object, null));

            //this.selected_id = (int)((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex].Cells["col_id"].Value;
            this.selected_row = ((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex];
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || ((XDatagrid)sender).CurrentCell == null)
                return;

            this.btnOK.PerformClick();
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
