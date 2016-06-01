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
    public partial class InquiryConditionForm : Form
    {
        private DealerWindow parent_window;
        private List<Dealer_list> dealer_list = new List<Dealer_list>(); // store Dealer_list from query result with condition
        private List<Dealer_list> dl = new List<Dealer_list>();
        public int selected_id; // store selected Dealer.id
        private string sort_field;
        private BindingSource bs = new BindingSource();
        private FORM_STATE form_state;
        private enum FORM_STATE
        {
            PROCESSING,
            READ
        }

        public InquiryConditionForm()
        {
            InitializeComponent();
        }

        public InquiryConditionForm(DealerWindow parent_window)
            : this()
        {
            this.parent_window = parent_window;
            this.sort_field = parent_window.GetSortFieldName();
        }

        private void InquiryConditionForm_Load(object sender, EventArgs e)
        {
            this.cbDataField.Items.Add(new ComboboxItem("Dealer Code", 0, "dealer"));
            this.cbDataField.Items.Add(new ComboboxItem("Prename", 0, "prenam"));
            this.cbDataField.Items.Add(new ComboboxItem("Name", 0, "compnam"));
            this.cbDataField.Items.Add(new ComboboxItem("Address 1", 0, "addr01"));
            this.cbDataField.Items.Add(new ComboboxItem("Address 2", 0, "addr02"));
            this.cbDataField.Items.Add(new ComboboxItem("Address 3", 0, "addr03"));
            this.cbDataField.Items.Add(new ComboboxItem("Zipcod", 0, "zipcod"));
            this.cbDataField.Items.Add(new ComboboxItem("Phone No.", 0, "telnum"));
            this.cbDataField.Items.Add(new ComboboxItem("Fax. No.", 0, "faxnum"));
            this.cbDataField.Items.Add(new ComboboxItem("Contact Name", 0, "contact"));
            this.cbDataField.Items.Add(new ComboboxItem("Contact Position", 0, "position"));
            this.cbDataField.Items.Add(new ComboboxItem("Business", 0, "busides"));
            this.cbDataField.Items.Add(new ComboboxItem("Area Code", 0, "area"));
            this.cbDataField.Items.Add(new ComboboxItem("Remark", 0, "remark"));
            this.cbDataField.SelectedIndex = 0;

            this.cbCompareType.Items.Add(new ComboboxItem("=", 0, "="));
            this.cbCompareType.Items.Add(new ComboboxItem("LIKE", 0, "Like"));
            //this.cbCompareType.Items.Add(new ComboboxItem("!=", 0, "!="));
            this.cbCompareType.SelectedIndex = 0;

            this.txtExpression.Text = this.parent_window.inquiry_expression;
            this.bs.DataSource = this.dl;
            this.dgvDealer.DataSource = this.bs;
            this.FillDataGrid();
        }

        private void InquiryConditionForm_Shown(object sender, EventArgs e)
        {
            this.cbDataField.Focus();
        }

        private void txtExpression_TextChanged(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).Text.Length == 0)
            {
                this.rbAnd.Enabled = false;
                this.rbOr.Enabled = false;
                this.rbAnd.Checked = false;
                this.rbOr.Checked = false;
                this.btnGo.Enabled = false;
                this.btnClear.Enabled = false;
            }
            else
            {
                this.rbAnd.Enabled = true;
                this.rbOr.Enabled = true;
                this.rbAnd.Checked = true;
                this.btnGo.Enabled = true;
                this.btnClear.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.cbDataField.SelectedItem != null && this.cbCompareType.SelectedItem != null)
            {
                string opr = (this.rbAnd.Checked ? " AND " : (this.rbOr.Checked ? " OR " : ""));
                string field = ((ComboboxItem)this.cbDataField.SelectedItem).string_value;
                string compare_type = ((ComboboxItem)this.cbCompareType.SelectedItem).string_value;
                string value = this.txtValue.Texts;

                this.txtExpression.Text += (compare_type == "Like" ? opr + field + " " + compare_type + " \'%" + value + "%\'" : opr + field + " " + compare_type + " \'" + value + "\'");
                SendKeys.Send("{TAB}");
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            this.parent_window.inquiry_expression = this.txtExpression.Text;
            string expression = this.txtExpression.Text;

            this.FormProcessing();
            bool post_success = false;
            string err_msg = "";
            int row_count = 0;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"expression\":\"" + expression + "\",";
                json_data += "\"sort_field\":\"" + this.sort_field + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "dealer/get_inquiry_condition", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                    this.dealer_list = sr.dealer_list;
                    row_count = sr.dealer_list.Count;
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
                    this.FormRead();
                    FillDataGrid();
                    if (row_count == 0)
                    {
                        this.btnOK.Enabled = false;
                        this.toolStripSelectedID.Text = "# ";
                        this.toolStripLoadedRow.Text = "0";
                        MessageAlert.Show(StringResource.DATA_NOT_FOUND, "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
                    }
                    else
                    {
                        this.btnOK.Enabled = true;
                        this.toolStripLoadedRow.Text = row_count.ToString();
                    }
                }
                else
                {
                    this.FormRead();
                    this.toolStripSelectedID.Text = "# ";
                    this.toolStripLoadedRow.Text = "0";
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            };
            worker.RunWorkerAsync();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show("Clear expression and all query result data?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                this.txtExpression.Text = "";
                this.toolStripSelectedID.Text = "# ";
                this.toolStripLoadedRow.Text = "0";
                //this.dgvDealer.DataSource = null;
                this.dealer_list.Clear();
                this.FillDataGrid();
                this.btnOK.Enabled = false;
                this.cbDataField.Focus();
            }
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

        private void dgvDealer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnOK.PerformClick();
        }

        private void FillDataGrid()
        {
            this.dl.Clear();
            foreach (Dealer_list d in this.dealer_list)
            {
                this.dl.Add(d);
            }
            this.bs.ResetBindings(false);

            this.dgvDealer.Columns[0].Visible = false;
            this.dgvDealer.Columns[1].Width = 120;
            this.dgvDealer.Columns[2].Width = 400;
            this.dgvDealer.Columns[3].Width = 120;
            this.dgvDealer.Columns[4].Width = 400;
            this.dgvDealer.Columns[5].Width = 400;
            this.dgvDealer.Columns[6].Width = 200;
            this.dgvDealer.Columns[7].Width = 300;
            this.dgvDealer.Columns[8].Width = 300;

            this.dgvDealer.DrawLineEffect();
            this.dgvDealer.Focus();

            if (this.dgvDealer.Rows.Count > 0)
            {
                this.dgvDealer.Rows[0].Cells[1].Selected = true;
            }

            this.dgvDealer.CurrentCellChanged += delegate
            {
                if (this.dgvDealer.CurrentCell != null)
                {
                    this.toolStripSelectedID.Text = "#" + ((int)this.dgvDealer.Rows[this.dgvDealer.CurrentCell.RowIndex].Cells[0].Value).ToString();
                }
                else
                {
                    this.toolStripSelectedID.Text = "#";
                }
            };
        }

        private void FormProcessing()
        {
            this.form_state = FORM_STATE.PROCESSING;
            this.toolStripProcessing.Visible = true;
            this.rbAnd.Enabled = false;
            this.rbOr.Enabled = false;
            this.cbCompareType.Enabled = false;
            this.cbDataField.Enabled = false;
            this.txtValue.Enabled = false;
            this.txtExpression.Enabled = false;
            this.dgvDealer.Enabled = false;
            this.btnOK.Enabled = false;
        }

        private void FormRead()
        {
            this.form_state = FORM_STATE.READ;
            this.toolStripProcessing.Visible = false;
            this.rbAnd.Enabled = true;
            this.rbOr.Enabled = true;
            this.cbCompareType.Enabled = true;
            this.cbDataField.Enabled = true;
            this.txtValue.Enabled = true;
            this.txtExpression.Enabled = true;
            this.dgvDealer.Enabled = true;
            this.btnOK.Enabled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvDealer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (((DataGridView)sender).Rows.Count > 0)
                {
                    this.btnOK.PerformClick();
                }
            }
        }
    }
}
