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
    public partial class DealerInquiryWindow : Form
    {
        private DealerWindow parent_window;
        private string sort_field;
        private List<Dealer_list> dealer_list = new List<Dealer_list>(); // store loaded data of Dealer
        private List<Dealer> dealer_id_list; // store all id of Dealer
        public int selected_id; // store selected dealer_id
        private FORM_STATE form_state;
        private INQUIRY_TYPE inquiry_type;
        private enum FORM_STATE
        {
            PROCESSING,
            READ
        }

        public enum INQUIRY_TYPE
        {
            ALL,
            REST
        }

        public DealerInquiryWindow()
        {
            InitializeComponent();
        }

        public DealerInquiryWindow(DealerWindow parent_window, INQUIRY_TYPE inquiry_type)
            : this()
        {
            this.parent_window = parent_window;
            this.sort_field = this.parent_window.GetSortFieldName();
            this.inquiry_type = inquiry_type;
            this.dealer_id_list = parent_window.dealer_id_list;
            this.LoadDealerListData(0, this.dealer_id_list.Count - 1);
        }

        private void DealerInquiryWindow_Load(object sender, EventArgs e)
        {
            this.dgvDealer.Dock = DockStyle.Fill;
        }

        private void LoadDealerListData(int start_list_id, int stop_list_id)
        {
            this.FormProcessing();
            bool post_success = false;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string ids = string.Empty;
                for (int i = start_list_id; i <= stop_list_id; i++)
                {
                    if (i == 0)
                    {
                        ids += this.dealer_id_list[i].id.ToString();
                    }
                    else
                    {
                        ids += "," + this.dealer_id_list[i].id.ToString();
                    }
                }

                string json_data = "{\"ids\":\"" + ids + "\",";
                json_data += "\"sort_field\":\"" + this.sort_field + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "dealer/get_inquiry", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.dealer_list = sr.dealer_list;
                    post_success = true;
                }
                else
                {
                    post_success = false;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (post_success)
                {
                    this.toolStripLoadedRec.Text = this.dealer_list.Count.ToString();
                    this.toolStripTotalRec.Text = this.dealer_list.Count.ToString();
                    this.FillDataGrid();
                    this.FormRead();
                }
            };
            worker.RunWorkerAsync();
        }

        private void FillDataGrid()
        {
            this.dgvDealer.DataSource = this.dealer_list;

            this.dgvDealer.Columns[0].Visible = false;
            this.dgvDealer.Columns[1].Width = 120;
            this.dgvDealer.Columns[2].Width = 400;
            this.dgvDealer.Columns[3].Width = 120;
            this.dgvDealer.Columns[4].Width = 400;
            this.dgvDealer.Columns[5].Width = 400;
            this.dgvDealer.Columns[6].Width = 200;
            this.dgvDealer.Columns[7].Width = 300;
            this.dgvDealer.Columns[8].Width = 300;

            this.dgvDealer.CurrentCellChanged += delegate
            {
                this.toolStripSelectedID.Text = "#" + ((int)this.dgvDealer.Rows[this.dgvDealer.CurrentCell.RowIndex].Cells[0].Value).ToString();
            };
            this.SetSelectedItem();
            this.dgvDealer.DrawLineEffect();
            this.dgvDealer.Focus();
        }

        private void SetSelectedItem()
        {
            if (this.inquiry_type == INQUIRY_TYPE.REST)
            {
                foreach (DataGridViewRow row in this.dgvDealer.Rows)
                {
                    if ((int)row.Cells[0].Value == this.parent_window.dealer.id)
                    {
                        row.Cells[1].Selected = true;
                        break;
                    }
                }
            }
            else
            {
                this.dgvDealer.Rows[0].Cells[1].Selected = true;
            }
        }

        private void dgvDealer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnOK.PerformClick();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selected_id = (int)this.dgvDealer.Rows[this.dgvDealer.CurrentCell.RowIndex].Cells[0].Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormProcessing()
        {
            this.form_state = FORM_STATE.PROCESSING;
            this.toolStripProcessing.Visible = true;
            this.dgvDealer.Enabled = false;
            this.btnCancel.Enabled = false;
            this.btnOK.Enabled = false;
        }

        private void FormRead()
        {
            this.form_state = FORM_STATE.READ;
            this.toolStripProcessing.Visible = false;
            this.dgvDealer.Enabled = true;
            this.btnCancel.Enabled = true;
            this.btnOK.Enabled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
