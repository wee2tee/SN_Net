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
        private string selected_dealer_code;
        //private Dealer focused_dealer;
        private List<Dealer> dealers = new List<Dealer>();
        private int sort_col = 1;

        public DealerList(string dealer_code)
        {
            InitializeComponent();

            this.selected_dealer_code = dealer_code;
            //this.focused_dealer = dealer;
            this.loadDealerList();
            this.fillInDatagrid();
        }

        private void DealerList_Shown(object sender, EventArgs e)
        {
            this.dgvDealer.Focus();
            EscapeKeyToCloseDialog.ActiveEscToClose(this);
        }

        private void loadDealerList()
        {
            CRUDResult get = ApiActions.GET(ApiConfig.API_MAIN_URL + "dealer/get_list");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.dealers = sr.dealer;
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void fillInDatagrid()
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
            text_col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3),
                BackColor = Color.OliveDrab
            };
            this.dgvDealer.Columns.Add(text_col2);

            DataGridViewTextBoxColumn text_col3 = new DataGridViewTextBoxColumn();
            text_col3.HeaderText = "รายละเอียด";
            text_col3.Width = this.dgvDealer.ClientSize.Width - (text_col2.Width + 3);
            text_col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3)
            };
            this.dgvDealer.Columns.Add(text_col3);

            foreach (Dealer dealer in this.dealers)
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

            this.setFocusedDealer();
        }

        private void setFocusedDealer()
        {
            foreach (DataGridViewRow row in this.dgvDealer.Rows)
            {
                if (((Dealer)row.Tag).dealer == this.selected_dealer_code)
                {
                    row.Cells[1].Selected = true;
                    this.dgvDealer.Focus();
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
                Dealer selected_dealer = (Dealer)((DataGridView)sender).Rows[e.RowIndex].Tag;
                this.returnSelectedResult(selected_dealer);
            }
        }

        private void returnSelectedResult(Dealer selected_dealer)
        {
            this.dealer = selected_dealer;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dgvDealer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOK.PerformClick();
            }
            else if (e.KeyCode == Keys.S && e.Modifiers == Keys.Alt)
            {
                this.btnSearch.PerformClick();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Dealer selected_dealer = (Dealer)this.dgvDealer.Rows[this.dgvDealer.CurrentCell.RowIndex].Tag;
            this.returnSelectedResult(selected_dealer);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchBox sb = new SearchBox();
            if (sb.ShowDialog() == DialogResult.OK)
            {
                this.dgvDealer.Search(sb.txtKeyword.Text, this.sort_col);
                Console.WriteLine("Keyword = " + sb.txtKeyword.Text + " , sort_col = " + this.sort_col.ToString());
            }
        }

        private void dgvDealer_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewColumn col in ((DataGridView)sender).Columns)
            {
                col.HeaderCell.Style.BackColor = Color.YellowGreen;
            }
            ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.OliveDrab;

            this.sort_col = e.ColumnIndex;
        }

        private void dgvDealer_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            ((DataGridView)sender).SetRowSelectedBorder(e);
        }
    }
}
