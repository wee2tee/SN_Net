using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using SN_Net.MiscClass;
using SN_Net.DataModels;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class SnWindow : Form
    {
        public DataResource data_resource = new DataResource();
        public GlobalVar G;

        #region declare Data Model
        private Serial serial;
        private Istab busityp;
        private Istab area;
        private Istab howknown;
        private Istab verext;
        private Dealer dealer;
        private List<Problem> problem;

        private Istab busityp_not_found = new Istab();
        private Istab area_not_found = new Istab();
        private Istab howknown_not_found = new Istab();
        private Istab verext_not_found = new Istab();
        private Dealer dealer_not_found = new Dealer();
        private List<Problem> problem_not_found = new List<Problem>();

        #endregion declare Data Model

        #region declare general variable
        private enum FORM_MODE
        {
            READ,
            ADD,
            EDIT,
            EDIT_ITEM
        }
        private FORM_MODE form_mode;
        public int id;
        private const int FIRST_ROW = -1;
        private const int LAST_ROW = 0;
        private const int FIND_NEXT = 1;
        private const int FIND_PREV = 2;
        private const string SORT_ID = "id";
        private const string SORT_SN = "sernum";
        private const string SORT_CONTACT = "contact";
        private const string SORT_COMPANY = "compnam";
        private const string SORT_DEALER = "dealer_dealer";
        private const string SORT_OLDNUM = "oldnum";
        private const string SORT_BUSITYP = "busityp";
        private const string SORT_AREA = "area";
        private string sortMode;
        private CultureInfo cinfo_us = new CultureInfo("en-US");
        private CultureInfo cinfo_th = new CultureInfo("th-TH");
        #endregion declare general variable

        public SnWindow()
        {
            InitializeComponent();
        }

        //public void collapseToolstrip()
        //{
        //    this.toolStrip1.SetBounds(this.toolStrip1.Location.X, this.toolStrip1.Location.Y, this.toolStrip1.ClientSize.Width, 0);
        //}

        private void SnWindow_Load(object sender, EventArgs e)
        {
            
            #region pairing TextBox with Browse Button
            // TextBox
            List<TextBox> list_tb = new List<TextBox>();
            list_tb.Add(this.txtArea);
            list_tb.Add(this.txtBusityp);
            list_tb.Add(this.txtDealer_dealer);
            list_tb.Add(this.txtHowknown);
            // Browse Button
            List<Button> list_btn = new List<Button>();
            list_btn.Add(this.btnBrowseArea);
            list_btn.Add(this.btnBrowseBusityp);
            list_btn.Add(this.btnBrowseDealer);
            list_btn.Add(this.btnBrowseHowknown);
            // Label
            List<Label> list_label = new List<Label>();
            list_label.Add(this.lblAreaTypdes);
            list_label.Add(this.lblBusitypTypdes);
            list_label.Add(this.lblDealer_DealerCompnam);
            list_label.Add(this.lblHowknownTypdes);
            // Selection Data
            List<PairTextBoxWithBrowseButton.SELECTION_DATA> list_selection_data = new List<PairTextBoxWithBrowseButton.SELECTION_DATA>();
            list_selection_data.Add(PairTextBoxWithBrowseButton.SELECTION_DATA.AREA);
            list_selection_data.Add(PairTextBoxWithBrowseButton.SELECTION_DATA.BUSITYP);
            list_selection_data.Add(PairTextBoxWithBrowseButton.SELECTION_DATA.DEALER);
            list_selection_data.Add(PairTextBoxWithBrowseButton.SELECTION_DATA.HOWKNOWN);
            // Pairing
            PairTextBoxWithBrowseButton.Attach(list_tb, list_btn, list_label, list_selection_data, data_resource);
            #endregion pairing TextBox with Browse Button

            #region pairing MaskedTextBox with DateTimePicker
            // MaskedTextBox
            List<MaskedTextBox> list_mt = new List<MaskedTextBox>();
            list_mt.Add(this.mskPurdat);
            list_mt.Add(this.mskExpdat);
            list_mt.Add(this.mskManual);
            list_mt.Add(this.mskVerextdat);
            list_mt.Add(this.mskEditDate);
            // DateTimePicker
            List<DateTimePicker> list_dp = new List<DateTimePicker>();
            list_dp.Add(this.dpPurdat);
            list_dp.Add(this.dpExpdat);
            list_dp.Add(this.dpManual);
            list_dp.Add(this.dpVerextdat);
            list_dp.Add(this.dpEditDate);
            // Pairing
            PairDatePickerWithMaskedTextBox.Attach(list_mt, list_dp);
            #endregion pairing MaskedTextBox with DateTimePicker

            #region Add key event handler to EditControl
            foreach (Control ct in this.transparentPanel1.Controls)
            {
                ct.KeyDown += new KeyEventHandler(this.editControlKeyHandler);
            }
            #endregion Add key event handler to EditControl

            this.loadVerextComboBox();
            this.sortMode = SORT_SN;
            this.getSerial(LAST_ROW, 0, this.sortMode);
            this.fillSerialInForm();
            this.AttachToolstripKeyPair(this);
            this.dgvProblem.Dock = DockStyle.Fill;
            this.tabControl1.Selecting += new TabControlCancelEventHandler(this.preventChangeTabInEditMode);
            this.transparentPanel1.Paint += new PaintEventHandler(this.editPanelPaintHandler);

            this.FormReady();
        }

        #region Set form state
        private void FormReady()
        {
            this.form_mode = FORM_MODE.READ;

            List<Control> lct = new List<Control>();
            lct.Add(this.txtSerNum);
            lct.Add(this.txtVersion);
            lct.Add(this.txtArea);
            lct.Add(this.txtRefnum);
            lct.Add(this.txtPrenam);
            lct.Add(this.txtCompnam);
            lct.Add(this.txtAddr01);
            lct.Add(this.txtAddr02);
            lct.Add(this.txtAddr03);
            lct.Add(this.txtZipcod);
            lct.Add(this.txtTelnum);
            lct.Add(this.txtFaxnum);
            lct.Add(this.txtContact);
            lct.Add(this.txtPosition);
            lct.Add(this.txtOldnum);
            lct.Add(this.txtRemark);
            lct.Add(this.txtBusides);
            lct.Add(this.txtBusityp);
            lct.Add(this.txtDealer_dealer);
            lct.Add(this.txtHowknown);
            lct.Add(this.mskPurdat);
            lct.Add(this.mskExpdat);
            lct.Add(this.txtReg);
            lct.Add(this.mskManual);
            lct.Add(this.mskVerextdat);
            List<Label> llb = new List<Label>();
            llb.Add(this.lblSerNum);
            llb.Add(this.lblVersion);
            llb.Add(this.lblArea);
            llb.Add(this.lblRefnum);
            llb.Add(this.lblPrenam);
            llb.Add(this.lblCompnam);
            llb.Add(this.lblAddr01);
            llb.Add(this.lblAddr02);
            llb.Add(this.lblAddr03);
            llb.Add(this.lblZipcod);
            llb.Add(this.lblTelnum);
            llb.Add(this.lblFaxnum);
            llb.Add(this.lblContact);
            llb.Add(this.lblPosition);
            llb.Add(this.lblOldnum);
            llb.Add(this.lblRemark);
            llb.Add(this.lblBusides);
            llb.Add(this.lblBusityp);
            llb.Add(this.lblDealer_dealer);
            llb.Add(this.lblHowknown);
            llb.Add(this.lblPurdat);
            llb.Add(this.lblExpdat);
            llb.Add(this.lblReg);
            llb.Add(this.lblManual);
            llb.Add(this.lblVerextdat);
            this.Ready(lct, llb);
        }

        private void FormEdit()
        {
            this.form_mode = FORM_MODE.EDIT;

            List<Control> lct = new List<Control>();
            lct.Add(this.txtVersion);
            lct.Add(this.txtArea);
            lct.Add(this.txtRefnum);
            lct.Add(this.txtPrenam);
            lct.Add(this.txtCompnam);
            lct.Add(this.txtAddr01);
            lct.Add(this.txtAddr02);
            lct.Add(this.txtAddr03);
            lct.Add(this.txtZipcod);
            lct.Add(this.txtTelnum);
            lct.Add(this.txtFaxnum);
            lct.Add(this.txtContact);
            lct.Add(this.txtPosition);
            lct.Add(this.txtOldnum);
            lct.Add(this.txtRemark);
            lct.Add(this.txtBusides);
            lct.Add(this.txtBusityp);
            lct.Add(this.txtDealer_dealer);
            lct.Add(this.txtHowknown);
            lct.Add(this.mskPurdat);
            lct.Add(this.mskExpdat);
            lct.Add(this.txtReg);
            lct.Add(this.mskManual);
            lct.Add(this.mskVerextdat);
            List<Label> llb = new List<Label>();
            llb.Add(this.lblVersion);
            llb.Add(this.lblArea);
            llb.Add(this.lblRefnum);
            llb.Add(this.lblPrenam);
            llb.Add(this.lblCompnam);
            llb.Add(this.lblAddr01);
            llb.Add(this.lblAddr02);
            llb.Add(this.lblAddr03);
            llb.Add(this.lblZipcod);
            llb.Add(this.lblTelnum);
            llb.Add(this.lblFaxnum);
            llb.Add(this.lblContact);
            llb.Add(this.lblPosition);
            llb.Add(this.lblOldnum);
            llb.Add(this.lblRemark);
            llb.Add(this.lblBusides);
            llb.Add(this.lblBusityp);
            llb.Add(this.lblDealer_dealer);
            llb.Add(this.lblHowknown);
            llb.Add(this.lblPurdat);
            llb.Add(this.lblExpdat);
            llb.Add(this.lblReg);
            llb.Add(this.lblManual);
            llb.Add(this.lblVerextdat);
            this.Edit(lct, llb);
        }

        private void FormAdd()
        {
            this.form_mode = FORM_MODE.ADD;

            List<Control> lct = new List<Control>();
            lct.Add(this.txtSerNum);
            lct.Add(this.txtVersion);
            lct.Add(this.txtArea);
            lct.Add(this.txtRefnum);
            lct.Add(this.txtPrenam);
            lct.Add(this.txtCompnam);
            lct.Add(this.txtAddr01);
            lct.Add(this.txtAddr02);
            lct.Add(this.txtAddr03);
            lct.Add(this.txtZipcod);
            lct.Add(this.txtTelnum);
            lct.Add(this.txtFaxnum);
            lct.Add(this.txtContact);
            lct.Add(this.txtPosition);
            lct.Add(this.txtOldnum);
            lct.Add(this.txtRemark);
            lct.Add(this.txtBusides);
            lct.Add(this.txtBusityp);
            lct.Add(this.txtDealer_dealer);
            lct.Add(this.txtHowknown);
            lct.Add(this.mskPurdat);
            lct.Add(this.mskExpdat);
            lct.Add(this.txtReg);
            lct.Add(this.mskManual);
            lct.Add(this.mskVerextdat);
            List<Label> llb = new List<Label>();
            llb.Add(this.lblSerNum);
            llb.Add(this.lblVersion);
            llb.Add(this.lblArea);
            llb.Add(this.lblRefnum);
            llb.Add(this.lblPrenam);
            llb.Add(this.lblCompnam);
            llb.Add(this.lblAddr01);
            llb.Add(this.lblAddr02);
            llb.Add(this.lblAddr03);
            llb.Add(this.lblZipcod);
            llb.Add(this.lblTelnum);
            llb.Add(this.lblFaxnum);
            llb.Add(this.lblContact);
            llb.Add(this.lblPosition);
            llb.Add(this.lblOldnum);
            llb.Add(this.lblRemark);
            llb.Add(this.lblBusides);
            llb.Add(this.lblBusityp);
            llb.Add(this.lblDealer_dealer);
            llb.Add(this.lblHowknown);
            llb.Add(this.lblPurdat);
            llb.Add(this.lblExpdat);
            llb.Add(this.lblReg);
            llb.Add(this.lblManual);
            llb.Add(this.lblVerextdat);
            this.Edit(lct, llb);
        }
        
        #endregion Set form state

        private void loadVerextComboBox()
        {
            foreach (Istab verext in this.data_resource.LIST_VEREXT)
            {
                this.cbVerext.Items.Add(new ComboboxItem(verext.typcod + " - " + verext.typdes_th, 0, verext.typcod));
            }
        }

        #region DataGridView Event Handler
        private void dgvProblem_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            ((DataGridView)sender).SetRowSelectedBorder(e);
        }

        private void setFocusedRowBorder(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dgvProblem_Resize(object sender, EventArgs e)
        {
            this.dgvProblem.FillLine(this.problem.Count);
            this.setGridColumnExpand();
        }

        private void dgvProblem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.form_mode = FORM_MODE.EDIT_ITEM;
            this.dgvProblem.Enabled = false;

            if (this.dgvProblem.Rows[e.RowIndex].Tag is Problem)
            {
                this.dgvProblem.CurrentCell = this.dgvProblem.Rows[e.RowIndex].Cells[1];
                this.mskEditDate.pickedDate(((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).date);
                this.txtEditName.Text = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).name;
                this.txtEditCo.Text = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probcod;
                this.txtEditDesc.Text = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probdesc;
            }
            else
            {
                this.dgvProblem.CurrentCell = this.dgvProblem.Rows[this.problem.Count].Cells[1];
            }
            int row_index = this.dgvProblem.CurrentRow.Index;

            this.setToolStripFormMode();
            this.showInlineProblemForm(row_index, e.ColumnIndex);
        }

        private void showInlineProblemForm(int row_index, int column_index)
        {
            Rectangle rect_date = this.dgvProblem.GetCellDisplayRectangle(1, row_index, true);
            Rectangle rect_name = this.dgvProblem.GetCellDisplayRectangle(2, row_index, true);
            Rectangle rect_co = this.dgvProblem.GetCellDisplayRectangle(3, row_index, true);
            Rectangle rect_desc = this.dgvProblem.GetCellDisplayRectangle(4, row_index, true);

            this.mskEditDate.SetBounds(rect_date.Left, rect_date.Top + 1, rect_date.Width - this.dpEditDate.ClientSize.Width, this.mskEditDate.ClientSize.Height);
            this.dpEditDate.SetBounds(rect_date.Left + (rect_date.Width - this.dpEditDate.ClientSize.Width), rect_date.Top + 1, this.dpEditDate.ClientSize.Width, this.dpEditDate.ClientSize.Height);
            this.txtEditName.SetBounds(rect_name.Left, rect_name.Top + 1, rect_name.Width, rect_name.Height);
            this.txtEditCo.SetBounds(rect_co.Left, rect_co.Top + 1, rect_co.Width, rect_co.Height);
            this.txtEditDesc.SetBounds(rect_desc.Left, rect_desc.Top + 1, rect_desc.Width, rect_desc.Height);

            this.transparentPanel1.Dock = DockStyle.Fill;
            this.transparentPanel1.Tag = (this.dgvProblem.Rows[row_index].Tag is Problem ? this.dgvProblem.Rows[row_index].Tag : null);
            this.transparentPanel1.Visible = true;
            if (this.transparentPanel1.Tag is Problem)
            {
                switch (column_index)
                {
                    case 1:
                        this.mskEditDate.Focus();
                        break;
                    case 2:
                        this.txtEditName.Focus();
                        this.txtEditName.SelectionStart = this.txtEditName.Text.Length;
                        break;
                    case 3:
                        this.txtEditCo.Focus();
                        this.txtEditCo.SelectionStart = this.txtEditCo.Text.Length;
                        break;
                    case 4:
                        this.txtEditDesc.Focus();
                        this.txtEditDesc.SelectionStart = this.txtEditDesc.Text.Length;
                        break;
                    default:
                        this.mskEditDate.Focus();
                        break;
                }
            }
            else
            {
                this.mskEditDate.Focus();
            }
        }

        #endregion DataGridView Event Handler

        #region edit control in datagridview Event Handler
        private void editPanelPaintHandler(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.dgvProblem.GetRowDisplayRectangle(this.dgvProblem.CurrentCell.RowIndex, false);
            e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Top, rect.Right, rect.Top);
            e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
        }

        private void editControlKeyHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.clearEditForm();
                this.dgvProblem.Focus();
            }
            if (e.KeyCode == Keys.F9)
            {
                this.submitProblemData();
            }
            if (((Control)sender).Name == this.txtEditDesc.Name && e.KeyCode == Keys.Enter)
            {
                this.submitProblemData();
            }
        }

        private void submitProblemData()
        {
            string json_data = "";
            CRUDResult post;

            if (this.transparentPanel1.Tag is Problem) // In case update
            {
                int prob_id = ((Problem)this.transparentPanel1.Tag).id;
                string prob_date = this.mskEditDate.Text.toMySQLDate();
                string prob_name = this.txtEditName.Text.cleanString();
                string prob_code = this.txtEditCo.Text.cleanString();
                string prob_desc = this.txtEditDesc.Text.cleanString();
                string users_name = this.G.loged_in_user_name;
                string serial_sernum = this.serial.sernum;

                json_data += "{\"id\":" + prob_id + ",";
                json_data += "\"date\":\"" + prob_date + "\",";
                json_data += "\"name\":\"" + prob_name + "\",";
                json_data += "\"probcod\":\"" + prob_code + "\",";
                json_data += "\"probdesc\":\"" + prob_desc + "\",";
                json_data += "\"users_name\":\"" + users_name + "\",";
                json_data += "\"serial_sernum\":\"" + serial_sernum + "\"}";

                post = ApiActions.POST(ApiConfig.API_MAIN_URL + "problem/update", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.loadProblemData();
                    this.fillInDatagrid();
                    this.setSelectedDataGridItem(sr.problem[0]);
                    this.clearEditForm();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
            else // In case create new problem
            {
                string prob_date = this.mskEditDate.Text.toMySQLDate();
                string prob_name = this.txtEditName.Text.cleanString();
                string prob_code = this.txtEditCo.Text.cleanString();
                string prob_desc = this.txtEditDesc.Text.cleanString();
                string users_name = this.G.loged_in_user_name;
                string serial_sernum = this.serial.sernum;

                json_data += "{\"date\":\"" + prob_date + "\",";
                json_data += "\"name\":\"" + prob_name + "\",";
                json_data += "\"probcod\":\"" + prob_code + "\",";
                json_data += "\"probdesc\":\"" + prob_desc + "\",";
                json_data += "\"users_name\":\"" + users_name + "\",";
                json_data += "\"serial_sernum\":\"" + serial_sernum + "\"}";

                post = ApiActions.POST(ApiConfig.API_MAIN_URL + "problem/create_new", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.clearEditForm();
                    this.loadProblemData();
                    this.fillInDatagrid();
                    this.dgvProblem.Rows[this.problem.Count].Cells[1].Selected = true;
                    this.showInlineProblemForm(this.dgvProblem.CurrentCell.RowIndex, 1);
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void clearEditForm()
        {
            this.transparentPanel1.Visible = false;
            this.transparentPanel1.Tag = null;
            this.mskEditDate.Text = "";
            this.txtEditName.Text = "";
            this.txtEditCo.Text = "";
            this.txtEditDesc.Text = "";
            this.dgvProblem.Enabled = true;
        }
        #endregion edit control in datagridview Event Handler

        #region Get Serial data from server
        private Serial getSerial(int row_id, int find_direction = 0, string sort_by = SORT_ID)
        {
            //Serial serial = new Serial();
            CRUDResult get;
            ServerResult sr;

            if (row_id < 1)
            {
                switch (row_id)
                {
                    case FIRST_ROW:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_first&sort=" + sort_by);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            this.serial = sr.serial[0];
                            this.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : this.busityp_not_found);
                            this.area = (sr.area.Count > 0 ? sr.area[0] : this.area_not_found);
                            this.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : this.howknown_not_found);
                            this.verext = (sr.verext.Count > 0 ? sr.verext[0] : this.verext_not_found);
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                            this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                        }
                        break;

                    case LAST_ROW:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_last&sort=" + sort_by);
                        Console.WriteLine(ApiConfig.API_MAIN_URL + "serial/get_last&sort=" + sort_by);
                        Console.WriteLine(get.data);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            this.serial = sr.serial[0];
                            this.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : this.busityp_not_found);
                            this.area = (sr.area.Count > 0 ? sr.area[0] : this.area_not_found);
                            this.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : this.howknown_not_found);
                            this.verext = (sr.verext.Count > 0 ? sr.verext[0] : this.verext_not_found);
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                            this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                        }
                        break;

                    default:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_last&sort=" + sort_by);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            this.serial = sr.serial[0];
                            this.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : this.busityp_not_found);
                            this.area = (sr.area.Count > 0 ? sr.area[0] : this.area_not_found);
                            this.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : this.howknown_not_found);
                            this.verext = (sr.verext.Count > 0 ? sr.verext[0] : this.verext_not_found);
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                            this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                        }
                        break;
                }
            }
            else
            {
                switch (find_direction)
                {
                    case FIND_NEXT:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_next&sort=" + sort_by + "&id=" + row_id);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            this.serial = sr.serial[0];
                            this.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : this.busityp_not_found);
                            this.area = (sr.area.Count > 0 ? sr.area[0] : this.area_not_found);
                            this.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : this.howknown_not_found);
                            this.verext = (sr.verext.Count > 0 ? sr.verext[0] : this.verext_not_found);
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                            this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                        }
                        break;

                    case FIND_PREV:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_prev&sort=" + sort_by + "&id=" + row_id);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            this.serial = sr.serial[0];
                            this.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : this.busityp_not_found);
                            this.area = (sr.area.Count > 0 ? sr.area[0] : this.area_not_found);
                            this.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : this.howknown_not_found);
                            this.verext = (sr.verext.Count > 0 ? sr.verext[0] : this.verext_not_found);
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                            this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                        }
                        break;

                    default:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_at&sort=" + sort_by + "&id=" + row_id);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            this.serial = sr.serial[0];
                            this.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : this.busityp_not_found);
                            this.area = (sr.area.Count > 0 ? sr.area[0] : this.area_not_found);
                            this.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : this.howknown_not_found);
                            this.verext = (sr.verext.Count > 0 ? sr.verext[0] : this.verext_not_found);
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                            this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                        }
                        break;
                }
            }
            
            return serial;
        }
        #endregion Get Serial data from server

        /// <summary>
        /// Fill the data in Form Control
        /// </summary>
        private void fillSerialInForm()
        {
            this.id = this.serial.id;
            
            #region Fill header data
            this.lblSerNum.Text = this.serial.sernum;
            this.lblVersion.Text = this.serial.version;
            this.lblArea.Text = this.serial.area;
            this.lblAreaTypdes.Text = this.area.typdes_th;
            this.lblRefnum.Text = serial.refnum;
            this.lblPrenam.Text = this.serial.prenam;
            this.lblCompnam.Text = this.serial.compnam;
            #endregion Fill header data

            #region Fill first tab data
            this.lblAddr01.Text = this.serial.addr01;
            this.lblAddr02.Text = this.serial.addr02;
            this.lblAddr03.Text = this.serial.addr03;
            this.lblZipcod.Text = this.serial.zipcod;
            this.lblTelnum.Text = this.serial.telnum;
            this.lblFaxnum.Text = this.serial.faxnum;
            this.lblContact.Text = this.serial.contact;
            this.lblPosition.Text = this.serial.position;
            this.lblOldnum.Text = this.serial.oldnum;
            
            this.lblRemark.Text = serial.remark;
            this.lblBusides.Text = serial.busides;
            this.lblBusityp.Text = this.serial.busityp;
            this.lblBusitypTypdes.Text = this.busityp.typdes_th;
            this.lblDealer_dealer.Text = this.serial.dealer_dealer;
            this.lblDealer_DealerCompnam.Text = this.dealer.prenam + " " + this.dealer.compnam;
            this.lblHowknown.Text = this.serial.howknown;
            this.lblHowknownTypdes.Text = this.howknown.typdes_th;
            this.lblPurdat.pickedDate(this.serial.purdat);
            //this.txtReg.Text
            this.lblExpdat.pickedDate(this.serial.expdat);
            this.lblManual.pickedDate(this.serial.manual);
            this.cbVerext.SelectedIndex = ComboboxItem.GetItemIndex(this.cbVerext, this.verext.typcod);
            this.lblVerextdat.pickedDate(this.serial.verextdat);
            #endregion Fill first tab data

            #region Fill second tab data
            this.lblTelnum2.Text = this.serial.telnum;
            this.lblExpdat2.pickedDate(this.serial.expdat);
            this.lblContact2.Text = this.serial.contact;
            this.fillInDatagrid();
            #endregion Fill second tab data
        }

        private void loadProblemData()
        {
            CRUDResult get = ApiActions.GET(ApiConfig.API_MAIN_URL + "problem/get_for_sn&sn=" + this.serial.sernum);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.problem = sr.problem;
            }
        }

        private void fillInDatagrid()
        {
            this.dgvProblem.Rows.Clear();
            this.dgvProblem.Columns.Clear();
            this.dgvProblem.EnableHeadersVisualStyles = false;

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.HeaderText = "ID";
            col0.Width = 0;
            col0.Visible = false;
            this.dgvProblem.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "DATE";
            col1.Width = 90;
            col1.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvProblem.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "NAME";
            col2.Width = 180;
            col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER,
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            this.dgvProblem.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "CO.";
            col3.Width = 40;
            col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvProblem.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "DESC.";
            col4.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER,
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            this.dgvProblem.Columns.Add(col4);

            foreach (Problem p in this.problem)
            {
                int r = this.dgvProblem.Rows.Add();
                this.dgvProblem.Rows[r].Height = 25;
                this.dgvProblem.Rows[r].Tag = p;

                this.dgvProblem.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvProblem.Rows[r].Cells[0].Value = p.id;

                this.dgvProblem.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvProblem.Rows[r].Cells[1].pickedDate(p.date);
                this.dgvProblem.Rows[r].Cells[1].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                this.dgvProblem.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvProblem.Rows[r].Cells[2].Value = p.name;
                this.dgvProblem.Rows[r].Cells[2].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                this.dgvProblem.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvProblem.Rows[r].Cells[3].Value = p.probcod;
                this.dgvProblem.Rows[r].Cells[3].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                this.dgvProblem.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvProblem.Rows[r].Cells[4].Value = p.probdesc;
                this.dgvProblem.Rows[r].Cells[4].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };
            }

            this.dgvProblem.FillLine(this.problem.Count);
            this.dgvProblem.CurrentCell = this.dgvProblem.Rows[0].Cells[1];
            this.setGridColumnExpand();
        }

        private void setSelectedDataGridItem(Problem selected_problem)
        {
            foreach (DataGridViewRow row in this.dgvProblem.Rows)
            {
                if (row.Tag is Problem)
                {
                    if (((Problem)row.Tag).id == selected_problem.id)
                    {
                        this.dgvProblem.CurrentCell = this.dgvProblem.Rows[row.Index].Cells[1];
                        break;
                    }
                }
            }
        }

        private void setGridColumnExpand()
        {
            this.dgvProblem.Columns[4].Width = this.dgvProblem.ClientSize.Width - (this.dgvProblem.Columns[1].Width + this.dgvProblem.Columns[2].Width + this.dgvProblem.Columns[3].Width + SystemInformation.VerticalScrollBarWidth + 3);
            this.txtEditDesc.Width = this.dgvProblem.Columns[4].Width;
        }

        #region ToolStrip click event handler
        private void toolStripFirst_Click(object sender, EventArgs e)
        {
            this.getSerial(FIRST_ROW, 0, sortMode);
            this.fillSerialInForm();
        }

        private void toolStripLast_Click(object sender, EventArgs e)
        {
            this.getSerial(LAST_ROW, 0, sortMode);
            this.fillSerialInForm();
        }

        private void toolStripPrevious_Click(object sender, EventArgs e)
        {
            this.getSerial(this.id, FIND_PREV, this.sortMode);
            this.fillSerialInForm();
        }

        private void toolStripNext_Click(object sender, EventArgs e)
        {
            this.getSerial(this.id, FIND_NEXT, this.sortMode);
            this.fillSerialInForm();
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.FormEdit();
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            this.FormAdd();
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            this.fillSerialInForm();
            this.FormReady();
            // cancel edit
        }

        private void toolStripItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            this.form_mode = FORM_MODE.EDIT_ITEM;
            this.setToolStripFormMode();
            this.dgvProblem.Focus();
            this.dgvProblem.CurrentCell = this.dgvProblem.Rows[0].Cells[1];
        }
        #endregion toolStrip

        #region TabControl Event Handler
        private void preventChangeTabInEditMode(object sender, TabControlCancelEventArgs e)
        {
            if (this.form_mode != FORM_MODE.READ)
            {
                e.Cancel = true;
            }
        }
        #endregion TabControl Event Handler

        #region Browse button
        private void btnBrowseBusityp_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.busityp, Istab.TABTYP.BUSITYP);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.busityp = wind.istab;
                this.txtBusityp.Text = this.busityp.typcod;
                this.lblBusitypTypdes.Text = this.busityp.typdes_th;
            }
            else
            {
                this.txtBusityp.Focus();
            }
        }

        private void btnBrowseArea_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.area, Istab.TABTYP.AREA);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.area = wind.istab;
                this.txtArea.Text = this.area.typcod;
                this.lblAreaTypdes.Text = this.area.typdes_th;
            }
            else
            {
                this.txtArea.Focus();
            }
        }

        private void btnBrowseHowknown_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.howknown, Istab.TABTYP.HOWKNOWN);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.howknown = wind.istab;
                this.txtHowknown.Text = this.howknown.typcod;
                this.lblHowknownTypdes.Text = this.howknown.typdes_th;
            }
            else
            {
                this.txtHowknown.Focus();
            }
        }

        private void btnBrowseDealer_Click(object sender, EventArgs e)
        {
            DealerList wind = new DealerList(this.dealer);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.dealer = wind.dealer;
                this.txtDealer_dealer.Text = this.dealer.dealer;
                this.lblDealer_DealerCompnam.Text = this.dealer.prenam + " " + this.dealer.compnam;
            }
            else
            {
                this.txtDealer_dealer.Focus();
            }
        }
        #endregion Browse button

        private void SnWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.State == FormState.FORM_STATE_ADD || this.State == FormState.FORM_STATE_EDIT)
            //{
            //    if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "Warning", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.OK)
            //    {
            //        MainForm main_form = this.MdiParent as MainForm;
            //        main_form.sn_wind = null;
            //        e.Cancel = false;
            //    }
            //    else
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //else
            //{
            //    MainForm main_form = this.MdiParent as MainForm;
            //    main_form.sn_wind = null;
            //}

            MainForm main_form = this.MdiParent as MainForm;
            main_form.sn_wind = null;

        }

        #region Add shortcut key to ToolStripButton
        private void AttachToolstripKeyPair(Control form)
        {
            foreach (Control ct in form.Controls)
            {
                ct.KeyDown += new KeyEventHandler(this.ControlKeyDownToolstrip);
                this.AttachToolstripKeyPair(ct);
            }
        }

        private void ControlKeyDownToolstrip(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                this.toolStripPrevious.PerformClick();
            }
            if (e.KeyCode == Keys.PageDown)
            {
                this.toolStripNext.PerformClick();
            }
            if (e.KeyCode == Keys.T && e.Modifiers == Keys.Control)
            {
                this.toolStripFirst.PerformClick();
            }
            if (e.KeyCode == Keys.B && e.Modifiers == Keys.Control)
            {
                this.toolStripLast.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    this.clearEditForm();
                    this.form_mode = FORM_MODE.READ;
                    this.setToolStripFormMode();
                }
                else
                {
                    this.toolStripStop.PerformClick();
                }
            }
            if (e.KeyCode == Keys.F8)
            {
                this.toolStripItem.PerformClick();
            }
            if (e.KeyCode == Keys.F9)
            {
                this.toolStripSave.PerformClick();
            }
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                this.toolStripAdd.PerformClick();
            }
            if (e.KeyCode == Keys.E && e.Modifiers == Keys.Control)
            {
                if (this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    this.showInlineProblemForm(this.dgvProblem.CurrentCell.RowIndex, this.dgvProblem.CurrentCell.ColumnIndex);
                }
                else
                {
                    this.toolStripEdit.PerformClick();
                }
            }
            if (e.KeyCode == Keys.D && e.Modifiers == Keys.Control)
            {
                this.toolStripDelete.PerformClick();
            }
        }

        #endregion Add shortcut key to ToolStripButton

        private void setToolStripFormMode()
        {
            switch (this.form_mode)
            {
                case FORM_MODE.READ:
                    this.toolStripAdd.Enabled = true;
                    this.toolStripEdit.Enabled = true;
                    this.toolStripDelete.Enabled = true;
                    this.toolStripStop.Enabled = false;
                    this.toolStripSave.Enabled = false;
                    this.toolStripFirst.Enabled = true;
                    this.toolStripPrevious.Enabled = true;
                    this.toolStripNext.Enabled = true;
                    this.toolStripLast.Enabled = true;
                    this.toolStripItem.Enabled = true;
                    this.toolStripUpgrade.Enabled = true;
                    this.toolStripBook.Enabled = true;
                    this.toolStripSet2.Enabled = true;
                    this.toolStripSearch.Enabled = true;
                    break;
                case FORM_MODE.ADD:
                    this.toolStripAdd.Enabled = false;
                    this.toolStripEdit.Enabled = false;
                    this.toolStripDelete.Enabled = false;
                    this.toolStripStop.Enabled = true;
                    this.toolStripSave.Enabled = true;
                    this.toolStripFirst.Enabled = false;
                    this.toolStripPrevious.Enabled = false;
                    this.toolStripNext.Enabled = false;
                    this.toolStripLast.Enabled = false;
                    this.toolStripItem.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    break;
                case FORM_MODE.EDIT:
                    this.toolStripAdd.Enabled = false;
                    this.toolStripEdit.Enabled = false;
                    this.toolStripDelete.Enabled = false;
                    this.toolStripStop.Enabled = true;
                    this.toolStripSave.Enabled = true;
                    this.toolStripFirst.Enabled = false;
                    this.toolStripPrevious.Enabled = false;
                    this.toolStripNext.Enabled = false;
                    this.toolStripLast.Enabled = false;
                    this.toolStripItem.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    break;
                case FORM_MODE.EDIT_ITEM:
                    this.toolStripAdd.Enabled = false;
                    this.toolStripEdit.Enabled = false;
                    this.toolStripDelete.Enabled = false;
                    this.toolStripStop.Enabled = true;
                    this.toolStripSave.Enabled = true;
                    this.toolStripFirst.Enabled = false;
                    this.toolStripPrevious.Enabled = false;
                    this.toolStripNext.Enabled = false;
                    this.toolStripLast.Enabled = false;
                    this.toolStripItem.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    break;
                default:
                    break;
            }
        }

    }
}
