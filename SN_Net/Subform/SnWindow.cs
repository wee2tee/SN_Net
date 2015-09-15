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
        public MainForm main_form;
        public GlobalVar G;
        public List<Serial> serial_id_list;


        #region declare Data Model
        public Serial serial;
        public Istab busityp;
        public Istab area;
        public Istab howknown;
        public Istab verext;
        public Dealer dealer;
        public List<Problem> problem;
        public List<Problem> problem_im_only;

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
            LOADING,
            SAVING,
            READ,
            ADD,
            EDIT,
            READ_ITEM,
            ADD_ITEM,
            EDIT_ITEM
        }

        private FORM_MODE form_mode;
        public int id;
        public const string SORT_ID = "id";
        public const string SORT_SN = "sernum";
        public const string SORT_CONTACT = "contact";
        public const string SORT_COMPANY = "compnam";
        public const string SORT_DEALER = "dealer_dealer";
        public const string SORT_OLDNUM = "oldnum";
        public const string SORT_BUSITYP = "busityp";
        public const string SORT_AREA = "area";
        public string sortMode;
        private CultureInfo cinfo_us = new CultureInfo("en-US");
        private CultureInfo cinfo_th = new CultureInfo("th-TH");
        private string json_data;
        private int find_id;
        private string find_sernum = "";
        private string find_contact = "";
        private string find_company = "";
        private string find_dealer = "";
        private string find_oldnum = "";
        private string find_busityp = "";
        private string find_area = "";
        private FIND_TYPE find_type;
        private List<Label> labels = new List<Label>();
        private List<Control> edit_controls = new List<Control>();
        private Control current_focused_control;
        private bool is_problem_im_only = false;

        private enum FIND_TYPE
        {
            SERNUM,
            CONTACT,
            COMPANY,
            DEALER,
            OLDNUM,
            BUSITYP,
            AREA
        }

        #endregion declare general variable

        public SnWindow(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
            this.pairingTextBoxWithBrowseButton();

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

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.KeyDownShortcutKeyToolstrip);
            
        }

        private void pairingTextBoxWithBrowseButton()
        {
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
            PairTextBoxWithBrowseButton.Attach(list_tb, list_btn, list_label, list_selection_data, this.main_form.data_resource);
        }

        private void SnWindow_Load(object sender, EventArgs e)
        {
            if (this.G.loged_in_user_level < GlobalVar.USER_GROUP_ADMIN)
            {
                this.btnCD.Visible = false;
                this.btnUP.Visible = false;
                this.btnUPNewRwt.Visible = false;
                this.btnUPNewRwtJob.Visible = false;
            }
            if (this.G.loged_in_user_level == GlobalVar.USER_GROUP_SUPPORT || this.G.loged_in_user_level == GlobalVar.USER_GROUP_ACCOUNT)
            {
                this.toolStripImport.Visible = false;
                this.toolStripGenSN.Visible = false;
            }
            if (this.G.loged_in_user_level == GlobalVar.USER_GROUP_SALES)
            {
                this.toolStripGenSN.Visible = false;
            }
            this.main_form = this.MdiParent as MainForm;
            this.FormLoading();
            this.tabControl1.Selecting += new TabControlCancelEventHandler(this.preventChangeTabInEditMode);
            this.transparentPanel1.Paint += new PaintEventHandler(this.editPanelPaintHandler);
            this.dgvProblem.MouseClick += new MouseEventHandler(this.showProblemContextMenu);
            this.transLayerHeader.Dock = DockStyle.Fill;
            this.transLayerBody1.Dock = DockStyle.Fill;
            this.transLayerBody2.Dock = DockStyle.Fill;

            this.sortMode = SORT_SN;
            this.catchLabelReadModeDoubleClick();
            this.catchInlineEditKeyEvent();

            BackgroundWorker snWorker = new BackgroundWorker();
            snWorker.DoWork += new DoWorkEventHandler(this.workerLoadLastSN_Dowork);
            snWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerLoadSN_Complete);
            snWorker.RunWorkerAsync();
        }

        private void workerLoadLastSN_Dowork(object sender, DoWorkEventArgs e)
        {
            this.getSerialIDList();
            this.getLastSerial();
        }

        private void workerLoadCurrentSN_Dowork(object sender, DoWorkEventArgs e)
        {
            this.getSerialIDList();
            this.getSerial(this.id);
            this.main_form.data_resource.Refresh();
            this.pairingTextBoxWithBrowseButton();
        }

        private void workerLoadSN_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            this.loadVerextComboBox();
            this.fillSerialInForm();
            this.dgvProblem.Dock = DockStyle.Fill;
            this.FormReady();
        }

        private void catchLabelReadModeDoubleClick()
        {
            this.labels.Add(this.lblVersion);
            this.labels.Add(this.lblArea);
            this.labels.Add(this.lblRefnum);
            this.labels.Add(this.lblPrenam);
            this.labels.Add(this.lblCompnam);
            this.labels.Add(this.lblAddr01);
            this.labels.Add(this.lblAddr02);
            this.labels.Add(this.lblAddr03);
            this.labels.Add(this.lblZipcod);
            this.labels.Add(this.lblTelnum);
            this.labels.Add(this.lblFaxnum);
            this.labels.Add(this.lblContact);
            this.labels.Add(this.lblPosition);
            this.labels.Add(this.lblOldnum);
            this.labels.Add(this.lblRemark);
            this.labels.Add(this.lblBusides);
            this.labels.Add(this.lblBusityp);
            this.labels.Add(this.lblDealer_dealer);
            this.labels.Add(this.lblHowknown);
            this.labels.Add(this.lblPurdat);
            this.labels.Add(this.lblExpdat);
            this.labels.Add(this.lblReg);
            this.labels.Add(this.lblManual);
            this.labels.Add(this.lblVerext);
            this.labels.Add(this.lblVerextdat);

            this.edit_controls.Add(this.txtVersion);
            this.edit_controls.Add(this.txtArea);
            this.edit_controls.Add(this.mskRefnum);
            this.edit_controls.Add(this.txtPrenam);
            this.edit_controls.Add(this.txtCompnam);
            this.edit_controls.Add(this.txtAddr01);
            this.edit_controls.Add(this.txtAddr02);
            this.edit_controls.Add(this.txtAddr03);
            this.edit_controls.Add(this.txtZipcod);
            this.edit_controls.Add(this.txtTelnum);
            this.edit_controls.Add(this.txtFaxnum);
            this.edit_controls.Add(this.txtContact);
            this.edit_controls.Add(this.txtPosition);
            this.edit_controls.Add(this.mskOldnum);
            this.edit_controls.Add(this.txtRemark);
            this.edit_controls.Add(this.txtBusides);
            this.edit_controls.Add(this.txtBusityp);
            this.edit_controls.Add(this.txtDealer_dealer);
            this.edit_controls.Add(this.txtHowknown);
            this.edit_controls.Add(this.mskPurdat);
            this.edit_controls.Add(this.mskExpdat);
            this.edit_controls.Add(this.txtReg);
            this.edit_controls.Add(this.mskManual);
            this.edit_controls.Add(this.cbVerext);
            this.edit_controls.Add(this.mskVerextdat);

            foreach (Label label in this.labels)
            {
                label.DoubleClick += new EventHandler(this.doubleClickToEdit);
            }
        }

        private void doubleClickToEdit(object sender, EventArgs e)
        {
            this.toolStripEdit.PerformClick();
            int ndx = this.labels.FindIndex(t => t.Equals((Label)sender));
            this.edit_controls[ndx].Focus();

            if (this.edit_controls[ndx] is TextBox)
            {
                ((TextBox)this.edit_controls[ndx]).SelectionStart = ((TextBox)this.edit_controls[ndx]).Text.Length;
            }
            if (this.edit_controls[ndx] is MaskedTextBox)
            {
                ((MaskedTextBox)this.edit_controls[ndx]).SelectionStart = 0;
                ((MaskedTextBox)this.edit_controls[ndx]).SelectionLength = ((MaskedTextBox)this.edit_controls[ndx]).Text.Length;
            }
        }

        #region Set form state
        private void FormLoading()
        {
            this.form_mode = FORM_MODE.LOADING;
            this.setToolStripFormMode();
            this.Cursor = Cursors.WaitCursor;
            this.main_form.Cursor = Cursors.WaitCursor;
            this.main_form.toolStripProcessing.Visible = true;
            this.main_form.menuStrip1.Enabled = false;

            List<Control> lct = new List<Control>();
            lct.Add(this.mskSernum);
            lct.Add(this.txtVersion);
            lct.Add(this.txtArea);
            lct.Add(this.mskRefnum);
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
            lct.Add(this.mskOldnum);
            lct.Add(this.txtRemark);
            lct.Add(this.txtBusides);
            lct.Add(this.txtBusityp);
            lct.Add(this.txtDealer_dealer);
            lct.Add(this.txtHowknown);
            lct.Add(this.mskPurdat);
            lct.Add(this.mskExpdat);
            lct.Add(this.txtReg);
            lct.Add(this.mskManual);
            lct.Add(this.cbVerext);
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
            llb.Add(this.lblVerext);
            llb.Add(this.lblVerextdat);
            this.Ready(lct, llb);
            this.txtDummy.Focus();
            this.transLayerHeader.Visible = true;
            this.transLayerBody1.Visible = true;
            this.transLayerBody2.Visible = true;
            foreach (Control ct in lct)
            {
                ct.Enter += new EventHandler(this.onControlEnterHandler);
                ct.GotFocus += new EventHandler(this.onControlFocusHandler);
                ct.Leave += new EventHandler(this.onControlLeaveHandler);
            }
        }

        private void FormSaving()
        {
            this.form_mode = FORM_MODE.SAVING;
            this.setToolStripFormMode();
            this.Cursor = Cursors.WaitCursor;
            this.main_form.Cursor = Cursors.WaitCursor;
            this.main_form.toolStripProcessing.Visible = true;
            this.main_form.menuStrip1.Enabled = false;

            this.txtDummy.Focus();
            this.transLayerHeader.Visible = true;
            this.transLayerBody1.Visible = true;
            this.transLayerBody2.Visible = true;
        }

        private void FormReady()
        {
            this.form_mode = FORM_MODE.READ;
            this.setToolStripFormMode();
            this.Cursor = Cursors.Default;
            this.main_form.Cursor = Cursors.Default;
            this.main_form.toolStripProcessing.Visible = false;
            this.main_form.menuStrip1.Enabled = true;

            List<Control> lct = new List<Control>();
            lct.Add(this.mskSernum);
            lct.Add(this.txtVersion);
            lct.Add(this.txtArea);
            lct.Add(this.mskRefnum);
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
            lct.Add(this.mskOldnum);
            lct.Add(this.txtRemark);
            lct.Add(this.txtBusides);
            lct.Add(this.txtBusityp);
            lct.Add(this.txtDealer_dealer);
            lct.Add(this.txtHowknown);
            lct.Add(this.mskPurdat);
            lct.Add(this.mskExpdat);
            lct.Add(this.txtReg);
            lct.Add(this.mskManual);
            lct.Add(this.cbVerext);
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
            llb.Add(this.lblVerext);
            llb.Add(this.lblVerextdat);
            this.Ready(lct, llb);
            this.transLayerHeader.Visible = false;
            this.transLayerBody1.Visible = false;
            this.transLayerBody2.Visible = false;
            this.chkIMOnly.Enabled = true;
            this.txtDummy.Focus();
            
            this.main_form.toolStripInfo.Text = "";
        }

        private void FormAdd()
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            this.lblAreaTypdes.Text = "";
            this.lblBusitypTypdes.Text = "";
            this.lblDealer_DealerCompnam.Text = "";
            this.lblHowknownTypdes.Text = "";
            this.form_mode = FORM_MODE.ADD;
            this.setToolStripFormMode();
            this.Cursor = Cursors.Default;
            this.main_form.Cursor = Cursors.Default;
            this.main_form.toolStripProcessing.Visible = false;
            this.main_form.menuStrip1.Enabled = true;

            List<Control> lct = new List<Control>();
            lct.Add(this.mskSernum);
            lct.Add(this.txtVersion);
            lct.Add(this.txtArea);
            lct.Add(this.mskRefnum);
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
            lct.Add(this.mskOldnum);
            lct.Add(this.txtRemark);
            lct.Add(this.txtBusides);
            lct.Add(this.txtBusityp);
            lct.Add(this.txtDealer_dealer);
            lct.Add(this.txtHowknown);
            lct.Add(this.mskPurdat);
            lct.Add(this.mskExpdat);
            lct.Add(this.txtReg);
            lct.Add(this.mskManual);
            lct.Add(this.cbVerext);
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
            llb.Add(this.lblVerext);
            llb.Add(this.lblVerextdat);
            this.Add(lct, llb);
            this.transLayerHeader.Visible = false;
            this.transLayerBody1.Visible = false;
            this.transLayerBody2.Visible = false;
        }

        private void FormEdit()
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            this.form_mode = FORM_MODE.EDIT;
            this.setToolStripFormMode();
            this.Cursor = Cursors.Default;
            this.main_form.Cursor = Cursors.Default;
            this.main_form.toolStripProcessing.Visible = false;
            this.main_form.menuStrip1.Enabled = true;

            List<Control> lct = new List<Control>();
            lct.Add(this.txtVersion);
            lct.Add(this.txtArea);
            lct.Add(this.mskRefnum);
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
            lct.Add(this.mskOldnum);
            lct.Add(this.txtRemark);
            lct.Add(this.txtBusides);
            lct.Add(this.txtBusityp);
            lct.Add(this.txtDealer_dealer);
            lct.Add(this.txtHowknown);
            lct.Add(this.mskPurdat);
            lct.Add(this.mskExpdat);
            lct.Add(this.txtReg);
            lct.Add(this.mskManual);
            lct.Add(this.cbVerext);
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
            llb.Add(this.lblVerext);
            llb.Add(this.lblVerextdat);
            this.Edit(lct, llb);
            this.transLayerHeader.Visible = false;
            this.transLayerBody1.Visible = false;
            this.transLayerBody2.Visible = false;
        }

        private void FormReadItem()
        {
            this.form_mode = FORM_MODE.READ_ITEM;
            this.setToolStripFormMode();
            this.Cursor = Cursors.Default;
            this.main_form.Cursor = Cursors.Default;
            this.btnSwithToRefnum.TabStop = true;
            this.main_form.toolStripProcessing.Visible = false;
            this.main_form.menuStrip1.Enabled = true;
            this.dgvProblem.Focus();
            this.transLayerHeader.Visible = false;
            this.transLayerBody1.Visible = false;
            this.transLayerBody2.Visible = false;
            this.main_form.toolStripInfo.Text = "";
        }

        private void FormAddItem()
        {
            this.form_mode = FORM_MODE.ADD_ITEM;
            this.setToolStripFormMode();
            this.Cursor = Cursors.Default;
            this.main_form.Cursor = Cursors.Default;
            this.btnSwithToRefnum.TabStop = false;
            this.main_form.toolStripProcessing.Visible = false;
            this.main_form.menuStrip1.Enabled = true;
            this.transLayerHeader.Visible = false;
            this.transLayerBody1.Visible = false;
            this.transLayerBody2.Visible = false;
        }

        private void FormEditItem()
        {
            this.form_mode = FORM_MODE.EDIT_ITEM;
            this.setToolStripFormMode();
            this.Cursor = Cursors.Default;
            this.main_form.Cursor = Cursors.Default;
            this.btnSwithToRefnum.TabStop = false;
            this.main_form.toolStripProcessing.Visible = false;
            this.main_form.menuStrip1.Enabled = true;
            this.transLayerHeader.Visible = false;
            this.transLayerBody1.Visible = false;
            this.transLayerBody2.Visible = false;
        }
        #endregion Set form state

        private void loadVerextComboBox()
        {
            this.cbVerext.Items.Clear();
            foreach (Istab verext in this.main_form.data_resource.LIST_VEREXT)
            {
                this.cbVerext.Items.Add(new ComboboxItem(verext.typcod + " - " + verext.typdes_th, 0, verext.typcod));
            }
        }

        #region DataGridView Event Handler
        private void dgvProblem_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            ((DataGridView)sender).SetRowSelectedBorder(e);
        }

        private void dgvProblem_Resize(object sender, EventArgs e)
        {
            int line_exist = (this.is_problem_im_only ? this.problem_im_only.Count : this.problem.Count);
            this.dgvProblem.FillLine(line_exist);
            this.setGridColumnExpand();
        }

        private void dgvProblem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row_ndx;

            if (this.dgvProblem.Rows[e.RowIndex].Tag is Problem)
            {
                this.dgvProblem.CurrentCell = this.dgvProblem.Rows[e.RowIndex].Cells[1];
                this.mskEditDate.pickedDate(((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).date);
                this.txtEditName.Text = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).name;
                this.txtEditCo.Text = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probcod;

                string prob_desc = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probdesc;

                if (this.G.loged_in_user_level == GlobalVar.USER_GROUP_ADMIN)
                {
                    this.txtEditDesc2.staticText = "";
                    this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probdesc;
                }
                else
                {
                    if (((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probcod == "RG")
                    {
                        string mac_code = this.GetMachineCode(((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probdesc);
                        this.txtEditDesc2.staticText = mac_code;
                        if (mac_code.Length > 0)
                        {
                            this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probdesc.Substring(mac_code.Length, prob_desc.Length - mac_code.Length).TrimStart();
                        }
                        else
                        {
                            this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probdesc.Substring(mac_code.Length, prob_desc.Length - mac_code.Length);
                        }
                    }
                    else
                    {
                        this.txtEditDesc2.staticText = "";
                        this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[e.RowIndex].Tag).probdesc;
                    }
                }
                
                row_ndx = e.RowIndex;
            }
            else
            {
                this.dgvProblem.CurrentCell = (this.is_problem_im_only ? this.dgvProblem.Rows[this.problem_im_only.Count].Cells[1] : this.dgvProblem.Rows[this.problem.Count].Cells[1]);
                row_ndx = (this.is_problem_im_only ? this.problem_im_only.Count : this.problem.Count);
            }
            this.showInlineProblemForm(row_ndx, e.ColumnIndex);
        }

        private void showProblemContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.toolStripItem.PerformClick();
                    int current_over_row = this.dgvProblem.HitTest(e.X, e.Y).RowIndex;
                    this.dgvProblem.Rows[current_over_row].Cells[1].Selected = true;

                    ContextMenu m = new ContextMenu();
                    if (this.dgvProblem.Rows[current_over_row].Tag is Problem)
                    {
                        MenuItem m_edit = new MenuItem("Edit data <Alt+E>");
                        m_edit.Click += new EventHandler(this.contextMenuEditHandler);
                        m_edit.Tag = current_over_row;
                        m.MenuItems.Add(m_edit);

                        MenuItem m_delete = new MenuItem("Delete data <Alt+D>");
                        m_delete.Click += new EventHandler(this.contextMenuDeleteHandler);
                        m_delete.Tag = current_over_row;
                        m.MenuItems.Add(m_delete);
                    }
                    else
                    {
                        MenuItem m_add = new MenuItem("Add data <Alt+A>");
                        m_add.Click += new EventHandler(this.contextMenuAddHandler);
                        m_add.Tag = current_over_row;
                        m.MenuItems.Add(m_add);
                    }
                    m.Show(this.dgvProblem, new Point(e.X, e.Y));
                }
            }
        }

        private void contextMenuAddHandler(object sender, EventArgs e)
        {
            int row_ndx = (this.is_problem_im_only ? this.problem_im_only.Count : this.problem.Count);
            this.showInlineProblemForm(row_ndx, 1);
        }

        private void contextMenuEditHandler(object sender, EventArgs e)
        {
            int row_ndx = (int)((MenuItem)sender).Tag;
            this.mskEditDate.pickedDate(((Problem)this.dgvProblem.Rows[row_ndx].Tag).date);
            this.txtEditName.Text = ((Problem)this.dgvProblem.Rows[row_ndx].Tag).name;
            this.txtEditCo.Text = ((Problem)this.dgvProblem.Rows[row_ndx].Tag).probcod;
            //this.txtEditDesc.Text = ((Problem)this.dgvProblem.Rows[row_ndx].Tag).probdesc;
            string prob_desc = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc;

            if (this.G.loged_in_user_level == GlobalVar.USER_GROUP_ADMIN)
            {
                this.txtEditDesc2.staticText = "";
                this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc;
            }
            else
            {
                if (((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probcod == "RG")
                {
                    string mac_code = this.GetMachineCode(((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc);
                    this.txtEditDesc2.staticText = mac_code;
                    if (mac_code.Length > 0)
                    {
                        this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc.Substring(mac_code.Length, prob_desc.Length - mac_code.Length).TrimStart();
                    }
                    else
                    {
                        this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc.Substring(mac_code.Length, prob_desc.Length - mac_code.Length);
                    }
                }
                else
                {
                    this.txtEditDesc2.staticText = "";
                    this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc;
                }
            }
            this.showInlineProblemForm(row_ndx, 1);
        }

        private void contextMenuDeleteHandler(object sender, EventArgs e)
        {
            this.deleteProblemData();
        }
        #endregion DataGridView Event Handler

        #region Manage Problem data
        private string GetMachineCode(string probDesc)
        {
            int dash_1st = 0;
            int dash_2nd = 0;
            int dash_3rd = 0;
            int start = 0;
            int end = probDesc.Length;
            int count = 0;
            int at = 0;
            int loop_cnt = 0;
            while ((start <= end) && (at > -1))
            {
                loop_cnt++;
                if (loop_cnt <= 3)
                {
                    count = end - start;
                    at = probDesc.IndexOf("-", start, count);
                    if (at == -1) break;
                    if (loop_cnt == 1)
                    {
                        dash_1st = at;
                    }
                    else if (loop_cnt == 2)
                    {
                        dash_2nd = at;
                    }
                    else if (loop_cnt == 3)
                    {
                        dash_3rd = at;
                    }

                    start = at + 1;
                }
                else
                {
                    break;
                }
            }
            if (dash_1st == 8)
            {
                if (dash_2nd > 0)
                {
                    // tryparse to int for stage 1 (2 digit after the second dash)
                    int parse1;
                    bool parse1_result = Int32.TryParse(probDesc.Substring(dash_2nd + 1, 2), out parse1);
                    if (parse1_result == true)
                    {
                        Console.WriteLine(parse1.ToString());
                        Console.WriteLine("REG NO. = \"" + probDesc.Substring(0, dash_2nd + 3) + "\"");
                        return probDesc.Substring(0, dash_2nd + 3);
                    }
                    else
                    {
                        if (dash_3rd > 0)
                        {
                            // tryparse to int for stage 2 (2 digit after the third dash)
                            int parse2;
                            bool parse2_result = Int32.TryParse(probDesc.Substring(dash_3rd + 1, 2), out parse2);
                            if (parse2_result == true)
                            {
                                Console.WriteLine(parse2.ToString());
                                Console.WriteLine("REG NO. = \"" + probDesc.Substring(0, dash_3rd + 3) + "\"");
                                return probDesc.Substring(0, dash_3rd + 3);
                            }
                        }
                    }
                }
            }

            // return empty string if not match for "Machine Code"
            return "";
        }

        private void showInlineProblemForm(int row_index, int column_index)
        {
            this.dgvProblem.Enabled = false;
            this.dgvProblem.CurrentCell = this.dgvProblem.Rows[row_index].Cells[1];

            Rectangle rect_date = this.dgvProblem.GetCellDisplayRectangle(1, row_index, true);
            Rectangle rect_name = this.dgvProblem.GetCellDisplayRectangle(2, row_index, true);
            Rectangle rect_co = this.dgvProblem.GetCellDisplayRectangle(3, row_index, true);
            Rectangle rect_desc = this.dgvProblem.GetCellDisplayRectangle(4, row_index, true);

            this.mskEditDate.SetBounds(rect_date.Left, rect_date.Top + 1, rect_date.Width - this.dpEditDate.ClientSize.Width, this.mskEditDate.ClientSize.Height);
            this.dpEditDate.SetBounds(rect_date.Left + (rect_date.Width - this.dpEditDate.ClientSize.Width), rect_date.Top + 1, this.dpEditDate.ClientSize.Width, this.dpEditDate.ClientSize.Height);
            this.txtEditName.SetBounds(rect_name.Left, rect_name.Top + 1, rect_name.Width, rect_name.Height);
            this.txtEditCo.SetBounds(rect_co.Left, rect_co.Top + 1, rect_co.Width, rect_co.Height);
            if (this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag is Problem)
            {
                if (((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probcod == "RG")
                {
                    this.txtEditDesc2.SetBounds(rect_desc.Left, rect_desc.Top + 1, rect_desc.Width - 1, rect_desc.Height - 2);
                }
                else
                {
                    this.txtEditDesc2.SetBounds(rect_desc.Left, rect_desc.Top + 1, rect_desc.Width - 1, rect_desc.Height - 2);
                }
            }
            else
            {
                this.txtEditDesc2.SetBounds(rect_desc.Left, rect_desc.Top + 1, rect_desc.Width - 1, rect_desc.Height - 2);
            }

            this.transparentPanel1.Dock = DockStyle.Fill;
            this.transparentPanel1.Tag = (this.dgvProblem.Rows[row_index].Tag is Problem ? this.dgvProblem.Rows[row_index].Tag : null);
            this.transparentPanel1.Visible = true;
            if (this.transparentPanel1.Tag is Problem)
            {
                this.FormEditItem();
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
                        this.txtEditDesc2.txtEdit.Focus();
                        //this.txtEditDesc2.txtEdit.SelectionStart = this.txtEditDesc2.txtEdit.Text.Length;
                        break;
                    default:
                        this.mskEditDate.Focus();
                        break;
                }
            }
            else
            {
                this.FormAddItem();
                this.dpEditDate.Value = DateTime.Now;
                this.mskEditDate.Focus();
            }
        }

        private void editPanelPaintHandler(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.dgvProblem.GetRowDisplayRectangle(this.dgvProblem.CurrentCell.RowIndex, false);
            e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Top, rect.Right, rect.Top);
            e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
        }

        private void submitProblemData()
        {
            bool submit_success = false;
            this.FormLoading();

            if (this.transparentPanel1.Tag is Problem) // Edit
            {
                BackgroundWorker worker_update = new BackgroundWorker();
                worker_update.DoWork += delegate
                {
                    string json_data = "";

                    int prob_id = ((Problem)this.transparentPanel1.Tag).id;
                    string prob_date = this.mskEditDate.Text.toMySQLDate();
                    string prob_name = this.txtEditName.Text.cleanString();
                    string prob_code = this.txtEditCo.Text.cleanString();
                    string prob_desc = (this.txtEditDesc2.txtStatic.Text.Length > 0 ? this.txtEditDesc2.txtStatic.Text + " " + this.txtEditDesc2.txtEdit.Text.cleanString() : this.txtEditDesc2.txtEdit.Text.cleanString());
                    string users_name = this.G.loged_in_user_name;
                    string serial_sernum = this.serial.sernum;

                    json_data += "{\"id\":" + prob_id + ",";
                    json_data += "\"date\":\"" + prob_date + "\",";
                    json_data += "\"name\":\"" + prob_name + "\",";
                    json_data += "\"probcod\":\"" + prob_code + "\",";
                    json_data += "\"probdesc\":\"" + prob_desc + "\",";
                    json_data += "\"users_name\":\"" + users_name + "\",";
                    json_data += "\"serial_sernum\":\"" + serial_sernum + "\"}";

                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "problem/update", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        submit_success = true;
                        this.loadProblemData();
                    }
                    else
                    {
                        submit_success = false;
                        MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                worker_update.RunWorkerCompleted += delegate
                {
                    if (submit_success)
                    {
                        this.clearInlineEditForm((Problem)this.transparentPanel1.Tag);
                        this.FormReadItem();
                    }
                    else
                    {
                        this.FormEditItem();
                    }
                };
                worker_update.RunWorkerAsync();
            }
            else // Add
            {
                if (this.G.loged_in_user_level < GlobalVar.USER_GROUP_ADMIN && this.txtEditCo.Text == "RG")
                {
                    MessageAlert.Show("Your permission is not allowed to record problem code as \"RG\"", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.FormAddItem();
                    this.txtEditCo.Focus();
                }
                else
                {
                    BackgroundWorker worker_add = new BackgroundWorker();
                    worker_add.DoWork += delegate
                    {
                        if (this.G.loged_in_user_level < GlobalVar.USER_GROUP_ADMIN && this.txtEditCo.Text == "RG")
                        {
                            submit_success = false;
                            MessageAlert.Show("Your permission is not allowed to record problem code as \"RG\"", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                            return;
                        }

                        string json_data = "";

                        string prob_date = this.mskEditDate.Text.toMySQLDate();
                        string prob_name = this.txtEditName.Text.cleanString();
                        string prob_code = this.txtEditCo.Text.cleanString();
                        //string prob_desc = this.txtEditDesc.Text.cleanString();
                        string prob_desc = this.txtEditDesc2.txtStatic.Text.cleanString() + " " + this.txtEditDesc2.txtEdit.Text.cleanString();
                        string users_name = this.G.loged_in_user_name;
                        string serial_sernum = this.serial.sernum;

                        json_data += "{\"date\":\"" + prob_date + "\",";
                        json_data += "\"name\":\"" + prob_name + "\",";
                        json_data += "\"probcod\":\"" + prob_code + "\",";
                        json_data += "\"probdesc\":\"" + prob_desc + "\",";
                        json_data += "\"users_name\":\"" + users_name + "\",";
                        json_data += "\"serial_sernum\":\"" + serial_sernum + "\"}";

                        CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "problem/create_new", json_data);
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            submit_success = true;
                            this.loadProblemData();
                        }
                        else
                        {
                            submit_success = false;
                            MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        }
                    };
                    worker_add.RunWorkerCompleted += delegate
                    {
                        if (submit_success)
                        {
                            Problem last_problem = (this.is_problem_im_only ? this.problem_im_only.Last<Problem>() : this.problem.Last<Problem>());
                            this.clearInlineEditForm(last_problem);
                            if (this.is_problem_im_only)
                            {
                                this.dgvProblem.Rows[this.problem_im_only.Count].Cells[1].Selected = true;
                            }
                            else
                            {
                                this.dgvProblem.Rows[this.problem.Count].Cells[1].Selected = true;
                            }
                            this.showInlineProblemForm(this.dgvProblem.CurrentCell.RowIndex, 1);
                            this.FormAddItem();
                        }
                        else
                        {
                            this.FormAddItem();
                        }
                    };
                    worker_add.RunWorkerAsync();
                }
            }
        }

        private void deleteProblemData()
        {
            if (this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag is Problem)
            {
                this.transparentPanel2.Dock = DockStyle.Fill;
                this.transparentPanel2.Visible = true;

                if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.YES_NO, MessageAlertIcons.QUESTION) == DialogResult.Yes)
                {
                    this.FormLoading();
                    BackgroundWorker worker_delete = new BackgroundWorker();
                    worker_delete.DoWork += delegate
                    {
                        Problem deleted_problem = (Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag;

                        CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "problem/delete&id=" + deleted_problem.id.ToString());
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            this.loadProblemData();
                        }
                        else
                        {
                            MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        }
                    };
                    worker_delete.RunWorkerCompleted += delegate
                    {
                        this.transparentPanel2.Visible = false;
                        this.fillInDatagrid();
                        this.FormReadItem();
                    };
                    worker_delete.RunWorkerAsync();
                }
                else
                {
                    this.transparentPanel2.Visible = false;
                }
            }
        }

        private void drawDeleteRowSlash(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.dgvProblem.GetRowDisplayRectangle(this.dgvProblem.CurrentCell.RowIndex, true);
            Pen p = new Pen(Color.Red, 1f);

            for (int i = rect.Left - 16; i < rect.Right; i += 8)
            {
                e.Graphics.DrawLine(p, i, rect.Bottom - 2, i + 23, rect.Top);
            }
        }

        private void clearInlineEditForm(Problem focused_problem_row = null)
        {
            this.transparentPanel1.Visible = false;
            this.transparentPanel1.Tag = null;
            this.mskEditDate.Text = "";
            this.txtEditName.Text = "";
            this.txtEditCo.Text = "";
            this.txtEditDesc2.txtStatic.Text = "";
            this.txtEditDesc2.txtEdit.Text = "";
            this.mskEditDate.BackColor = Color.White;
            this.txtEditName.BackColor = Color.White;
            this.txtEditCo.BackColor = Color.White;
            this.txtEditDesc2.txtEdit.BackColor = Color.White;
            this.txtEditDesc2.txtStatic.BackColor = Color.White;
            this.dgvProblem.Enabled = true;
            this.dgvProblem.Focus();
            this.fillInDatagrid();
            if(focused_problem_row == null)
            {
                if (!this.is_problem_im_only && this.problem.Count > 0)
                {
                    this.setSelectedDataGridItem((Problem)this.dgvProblem.Rows[this.problem.Count - 1].Tag);
                }
                else if (this.is_problem_im_only && this.problem_im_only.Count > 0)
                {
                    this.setSelectedDataGridItem((Problem)this.dgvProblem.Rows[this.problem_im_only.Count - 1].Tag);
                }
                else
                {
                    this.dgvProblem.CurrentCell = this.dgvProblem.Rows[0].Cells[1];
                }
            }
            else 
            {
                this.setSelectedDataGridItem(focused_problem_row);
            }
            this.form_mode = FORM_MODE.READ_ITEM;
            this.setToolStripFormMode();
        }

        private void catchInlineEditKeyEvent()
        {
            List<Control> lct = new List<Control>();
            lct.Add(this.mskEditDate);
            lct.Add(this.txtEditName);
            lct.Add(this.txtEditCo);
            lct.Add(this.txtEditDesc2.txtEdit);

            foreach (Control ct in lct)
            {
                ct.Enter += new EventHandler(this.inlineEditEnterHandler);
                ct.GotFocus += new EventHandler(this.inlineEditGotFocusedHandler);
                ct.Leave += new EventHandler(this.inlineEditLeaveHandler);
            }
        }

        private void inlineEditEnterHandler(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
            ((Control)sender).ForeColor = Color.Black;
        }

        private void inlineEditGotFocusedHandler(object sender, EventArgs e)
        {
            this.current_focused_control = (Control)sender;
            this.main_form.toolStripInfo.Text = (this.current_focused_control.Tag is string ? (string)this.current_focused_control.Tag : " ");

            if ((Control)sender == this.txtEditDesc2.txtEdit)
            {
                Istab probcod = this.main_form.data_resource.LIST_PROBLEM_CODE.Find(t => t.typcod == this.txtEditCo.Text);
                if (probcod == null)
                {
                    this.txtEditCo.Focus();
                }
            }
        }

        private void inlineEditLeaveHandler(object sender, EventArgs e)
        {
            if ((Control)sender == this.txtEditCo)
            {
                Istab probcod = this.main_form.data_resource.LIST_PROBLEM_CODE.Find(t => t.typcod == this.txtEditCo.Text);
                if (probcod == null)
                {
                    this.txtEditCo.Focus();
                    SendKeys.Send("{F6}");
                }
                else
                {
                    ((Control)sender).BackColor = Color.White;
                    ((Control)sender).ForeColor = Color.Black;
                }
            }
            else
            {
                ((Control)sender).BackColor = Color.White;
                ((Control)sender).ForeColor = Color.Black;
            }
        }
        #endregion Manage Problem data

        #region Get Serial data from server
        private void getSerialIDList()
        {
            CRUDResult get_id_list = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/get_id_list&sort=" + this.sortMode);
            ServerResult sr_id_list = JsonConvert.DeserializeObject<ServerResult>(get_id_list.data);
            if (sr_id_list.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.serial_id_list = sr_id_list.serial;
            }
        }

        private void getLastSerial()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/get_last");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                if (sr.serial.Count > 0)
                {
                    this.serial = sr.serial[0];
                    this.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : this.busityp_not_found);
                    this.area = (sr.area.Count > 0 ? sr.area[0] : this.area_not_found);
                    this.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : this.howknown_not_found);
                    this.verext = (sr.verext.Count > 0 ? sr.verext[0] : this.verext_not_found);
                    this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                    this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                    this.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : this.problem_not_found);
                }
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void getSerial(int row_id)
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/get_at&id=" + row_id);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                if (sr.serial.Count > 0) 
                {
                    this.serial = sr.serial[0];
                    this.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : this.busityp_not_found);
                    this.area = (sr.area.Count > 0 ? sr.area[0] : this.area_not_found);
                    this.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : this.howknown_not_found);
                    this.verext = (sr.verext.Count > 0 ? sr.verext[0] : this.verext_not_found);
                    this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                    this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                    this.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : this.problem_not_found);
                }
                else
                {
                    this.getSerialIDList();
                    this.getSerial(this.serial_id_list.Last<Serial>().id);
                }
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
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
            this.lblDealer_DealerCompnam.Text = this.dealer.compnam;
            this.lblHowknown.Text = this.serial.howknown;
            this.lblHowknownTypdes.Text = this.howknown.typdes_th;
            this.lblPurdat.pickedDate(this.serial.purdat);
            this.lblReg.Text = this.serial.upfree;
            this.lblExpdat.pickedDate(this.serial.expdat);
            this.lblManual.pickedDate(this.serial.manual);
            this.lblVerext.Text = this.verext.typcod + " - " + this.verext.typdes_th;
            this.lblVerextdat.pickedDate(this.serial.verextdat);
            #endregion Fill first tab data

            #region Fill second tab data
            this.lblTelnum2.Text = this.serial.telnum;
            this.lblExpdat2.pickedDate(this.serial.expdat);
            this.lblContact2.Text = this.serial.contact;
            this.lblReg2.Text = this.serial.upfree;
            this.fillInDatagrid();
            #endregion Fill second tab data

        }

        private void loadProblemData()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "problem/get_for_sn&sn=" + this.serial.sernum);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                this.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : this.problem_not_found);
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
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvProblem.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "NAME";
            col2.Width = 180;
            col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            this.dgvProblem.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "CO.";
            col3.Width = 40;
            col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvProblem.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "DESC.";
            col4.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            this.dgvProblem.Columns.Add(col4);

            List<Problem> problem = (this.is_problem_im_only ? this.problem_im_only : this.problem);

            foreach (Problem p in problem)
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
            int line_exist = (this.is_problem_im_only ? this.problem_im_only.Count : this.problem.Count);
            this.dgvProblem.FillLine(line_exist);
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
            this.txtEditDesc2.Width = this.dgvProblem.Columns[4].Width - 2;
        }

        #region Manage Serial data
        private void submitSerialData()
        {
            bool submit_serial_success = false;

            if (this.form_mode == FORM_MODE.ADD)
            {
                // Submit data in Add mode
                if (ValidateSN.Check(this.mskSernum.Text))
                {
                    this.json_data = string.Empty;
                    this.json_data += "{\"sernum\":\"" + this.mskSernum.Text.cleanString() + "\",";
                    this.json_data += "\"oldnum\":\"" + this.mskOldnum.Text.cleanString() + "\",";
                    this.json_data += "\"version\":\"" + this.txtVersion.Text.cleanString() + "\",";
                    this.json_data += "\"contact\":\"" + this.txtContact.Text.cleanString() + "\",";
                    this.json_data += "\"position\":\"" + this.txtPosition.Text.cleanString() + "\",";
                    this.json_data += "\"prenam\":\"" + this.txtPrenam.Text.cleanString() + "\",";
                    this.json_data += "\"compnam\":\"" + this.txtCompnam.Text.cleanString() + "\",";
                    this.json_data += "\"addr01\":\"" + this.txtAddr01.Text.cleanString() + "\",";
                    this.json_data += "\"addr02\":\"" + this.txtAddr02.Text.cleanString() + "\",";
                    this.json_data += "\"addr03\":\"" + this.txtAddr03.Text.cleanString() + "\",";
                    this.json_data += "\"zipcod\":\"" + this.txtZipcod.Text.cleanString() + "\",";
                    this.json_data += "\"telnum\":\"" + this.txtTelnum.Text.cleanString() + "\",";
                    this.json_data += "\"faxnum\":\"" + this.txtFaxnum.Text.cleanString() + "\",";
                    this.json_data += "\"busityp\":\"" + this.txtBusityp.Text.cleanString() + "\",";
                    this.json_data += "\"busides\":\"" + this.txtBusides.Text.cleanString() + "\",";
                    this.json_data += "\"purdat\":\"" + this.mskPurdat.Text.toMySQLDate() + "\",";
                    this.json_data += "\"expdat\":\"" + this.mskExpdat.Text.toMySQLDate() + "\",";
                    this.json_data += "\"howknown\":\"" + this.txtHowknown.Text.cleanString() + "\",";
                    this.json_data += "\"area\":\"" + this.txtArea.Text.cleanString() + "\",";
                    //this.json_data += "\"branch\":\"" +  + "\",";
                    this.json_data += "\"manual\":\"" + this.mskManual.Text.toMySQLDate() + "\",";
                    this.json_data += "\"upfree\":\"" + this.txtReg.Text.cleanString() + "\",";
                    this.json_data += "\"refnum\":\"" + this.mskRefnum.Text.cleanString() + "\",";
                    this.json_data += "\"remark\":\"" + this.txtRemark.Text.cleanString() + "\",";
                    this.json_data += "\"verext\":\"" + ((ComboboxItem)this.cbVerext.SelectedItem).string_value + "\",";
                    this.json_data += "\"verextdat\":\"" + this.mskVerextdat.Text.toMySQLDate() + "\",";
                    this.json_data += "\"users_id\":\"" + this.G.loged_in_user_id + "\",";
                    this.json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\",";
                    this.json_data += "\"dealer_dealer\":\"" + this.txtDealer_dealer.Text.cleanString() + "\"}";
                    this.FormSaving();
                    BackgroundWorker workerSerialCreate = new BackgroundWorker();
                    workerSerialCreate.DoWork += delegate
                    {
                        CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/create_new", this.json_data);
                        Console.WriteLine(post.data);
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            submit_serial_success = true;
                            this.getSerial(Convert.ToInt32(sr.message));
                            this.getSerialIDList();
                        }
                        else
                        {
                            submit_serial_success = false;
                            MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        }
                    };
                    workerSerialCreate.RunWorkerCompleted += delegate
                    {
                        if (submit_serial_success)
                        {
                            this.fillSerialInForm();
                            this.FormReady();
                            this.toolStripItem.PerformClick();
                            this.showInlineProblemForm(0, 1);
                        }
                        else
                        {
                            this.form_mode = FORM_MODE.ADD;
                            this.setToolStripFormMode();
                            this.Cursor = Cursors.Default;
                            this.main_form.Cursor = Cursors.Default;
                            this.main_form.toolStripProcessing.Visible = false;
                            this.main_form.menuStrip1.Enabled = true;

                            this.mskSernum.Focus();
                            this.transLayerHeader.Visible = false;
                            this.transLayerBody1.Visible = false;
                            this.transLayerBody2.Visible = false;
                        }
                    };
                    workerSerialCreate.RunWorkerAsync();
                }
                else
                {
                    MessageAlert.Show(StringResource.PLEASE_FILL_CORRECT_SN, "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                }
            }
            else if(this.form_mode == FORM_MODE.EDIT)
            {
                // Submit data in Edit mode
                this.json_data = string.Empty;
                this.json_data += "{\"id\":" + this.serial.id.ToString() + ",";
                this.json_data += "\"oldnum\":\"" + this.mskOldnum.Text.cleanString() + "\",";
                this.json_data += "\"version\":\"" + this.txtVersion.Text.cleanString() + "\",";
                this.json_data += "\"contact\":\"" + this.txtContact.Text.cleanString() + "\",";
                this.json_data += "\"position\":\"" + this.txtPosition.Text.cleanString() + "\",";
                this.json_data += "\"prenam\":\"" + this.txtPrenam.Text.cleanString() + "\",";
                this.json_data += "\"compnam\":\"" + this.txtCompnam.Text.cleanString() + "\",";
                this.json_data += "\"addr01\":\"" + this.txtAddr01.Text.cleanString() + "\",";
                this.json_data += "\"addr02\":\"" + this.txtAddr02.Text.cleanString() + "\",";
                this.json_data += "\"addr03\":\"" + this.txtAddr03.Text.cleanString() + "\",";
                this.json_data += "\"zipcod\":\"" + this.txtZipcod.Text.cleanString() + "\",";
                this.json_data += "\"telnum\":\"" + this.txtTelnum.Text.cleanString() + "\",";
                this.json_data += "\"faxnum\":\"" + this.txtFaxnum.Text.cleanString() + "\",";
                this.json_data += "\"busityp\":\"" + this.txtBusityp.Text.cleanString() + "\",";
                this.json_data += "\"busides\":\"" + this.txtBusides.Text.cleanString() + "\",";
                this.json_data += "\"purdat\":\"" + this.mskPurdat.Text.toMySQLDate() + "\",";
                this.json_data += "\"expdat\":\"" + this.mskExpdat.Text.toMySQLDate() + "\",";
                this.json_data += "\"howknown\":\"" + this.txtHowknown.Text.cleanString() + "\",";
                this.json_data += "\"area\":\"" + this.txtArea.Text.cleanString() + "\",";
                //this.json_data += "\"branch\":\"" +  + "\",";
                this.json_data += "\"manual\":\"" + this.mskManual.Text.toMySQLDate() + "\",";
                this.json_data += "\"upfree\":\"" + this.txtReg.Text.cleanString() + "\",";
                this.json_data += "\"refnum\":\"" + this.mskRefnum.Text.cleanString() + "\",";
                this.json_data += "\"remark\":\"" + this.txtRemark.Text.cleanString() + "\",";
                this.json_data += "\"verext\":\"" + ((ComboboxItem)this.cbVerext.SelectedItem).string_value + "\",";
                this.json_data += "\"verextdat\":\"" + this.mskVerextdat.Text.toMySQLDate() + "\",";
                this.json_data += "\"users_id\":\"" + this.G.loged_in_user_id + "\",";
                this.json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\",";
                this.json_data += "\"dealer_dealer\":\"" + this.txtDealer_dealer.Text.cleanString() + "\"}";
                this.FormSaving();
                BackgroundWorker workerSerialUpdate = new BackgroundWorker();
                workerSerialUpdate.DoWork += delegate
                {
                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/update", this.json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        submit_serial_success = true;
                        this.getSerial(Convert.ToInt32(sr.message));
                        this.getSerialIDList();
                    }
                    else
                    {
                        submit_serial_success = false;
                        MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                workerSerialUpdate.RunWorkerCompleted += delegate
                {
                    if (submit_serial_success)
                    {
                        this.fillSerialInForm();
                        this.FormReady();
                    }
                };
                workerSerialUpdate.RunWorkerAsync();
            }
        }

        private void findSerial()
        {
            if (this.find_id != this.serial.id)
            {
                this.FormLoading();
                BackgroundWorker workerFind = new BackgroundWorker();
                workerFind.DoWork += delegate
                {
                    this.getSerial(this.find_id);
                };
                workerFind.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                    this.FormReady();
                };
                workerFind.RunWorkerAsync();
            }
        }
        #endregion Manage Serial data

        #region ToolStrip click event handler
        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            this.FormAdd();
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.FormEdit();
        }

        private void toolStripDelete_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.YES_NO, MessageAlertIcons.QUESTION) == DialogResult.Yes)
            {
                this.FormLoading();

                BackgroundWorker workerDeleteSerial = new BackgroundWorker();
                workerDeleteSerial.DoWork += delegate
                {
                    int current_ndx = this.serial_id_list.FindIndex(t => t.id == this.serial.id);

                    CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "serial/delete&id=" + this.serial.id.ToString());
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        this.getSerialIDList();

                        if (current_ndx < this.serial_id_list.Count - 1)
                        {
                            this.getSerial(this.serial_id_list[current_ndx].id);
                        }
                        else
                        {
                            this.getSerial(this.serial_id_list.Last<Serial>().id);
                        }
                    }
                    else
                    {
                        MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                workerDeleteSerial.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                    this.FormReady();
                };
                workerDeleteSerial.RunWorkerAsync();
            }
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.fillInDatagrid();
                this.FormReady();
            }
            else if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                if (this.transparentPanel1.Tag is Problem)
                {
                    this.clearInlineEditForm((Problem)this.transparentPanel1.Tag);
                }
                else
                {
                    this.clearInlineEditForm();
                }
                this.FormReadItem();
            }
            else
            {
                this.form_mode = FORM_MODE.READ;
                this.setToolStripFormMode();
                this.fillSerialInForm();
                this.FormReady();
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.fillInDatagrid();
                this.FormReady();
            }
            else if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.submitProblemData();
            }
            else
            {
                this.submitSerialData();
            }
        }

        private void toolStripFirst_Click(object sender, EventArgs e)
        {
            this.find_id = this.serial_id_list.First<Serial>().id;
            this.findSerial();
        }

        private void toolStripLast_Click(object sender, EventArgs e)
        {
            this.find_id = this.serial_id_list.Last<Serial>().id;
            this.findSerial();
        }

        private void toolStripPrevious_Click(object sender, EventArgs e)
        {
            int current_ndx = this.serial_id_list.FindIndex(t => t.id == this.serial.id);
            if (current_ndx > 0)
            {
                this.find_id = this.serial_id_list[current_ndx - 1].id;
                this.findSerial();
            }
        }

        private void toolStripNext_Click(object sender, EventArgs e)
        {
            int current_ndx = this.serial_id_list.FindIndex(t => t.id == this.serial.id);
            if (current_ndx < this.serial_id_list.Count - 1)
            {
                this.find_id = this.serial_id_list[current_ndx + 1].id;
                this.findSerial();
            }
        }

        private void toolStripItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            this.form_mode = FORM_MODE.READ_ITEM;
            this.setToolStripFormMode();
            this.dgvProblem.Focus();
            this.fillInDatagrid();
            this.dgvProblem.CurrentCell = this.dgvProblem.Rows[0].Cells[1];
        }

        private void toolStripSearch_ButtonClick(object sender, EventArgs e)
        {
            SearchSerialBox box = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.SERNUM);
            box.mskSearchKey.Text = this.find_sernum;
            if (box.ShowDialog() == DialogResult.OK)
            {
                this.find_sernum = box.mskSearchKey.Text;
                this.find_type = FIND_TYPE.SERNUM;
                this.FormLoading();
                BackgroundWorker workerSearch = new BackgroundWorker();
                workerSearch.DoWork += new DoWorkEventHandler(this.workerSearch_DoWork);
                workerSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerSearch_Complete);
                workerSearch.RunWorkerAsync();
            }
        }

        private void toolStripInquiryAll_Click(object sender, EventArgs e)
        {
            SNInquiryWindow wind = new SNInquiryWindow(this, SNInquiryWindow.INQUIRY_TYPE.ALL);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.FormLoading();
                this.find_id = wind.selected_id;
                BackgroundWorker selectFromInquiryWorker = new BackgroundWorker();
                selectFromInquiryWorker.DoWork += new DoWorkEventHandler(this.selectFromInquiryWorker_DoWork);
                selectFromInquiryWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.selectFromInquiryWorker_Complete);
                selectFromInquiryWorker.RunWorkerAsync();
            }
        }

        private void toolStripInquiryRest_Click(object sender, EventArgs e)
        {
            SNInquiryWindow wind = new SNInquiryWindow(this, SNInquiryWindow.INQUIRY_TYPE.REST);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.FormLoading();
                this.find_id = wind.selected_id;
                BackgroundWorker selectFromInquiryWorker = new BackgroundWorker();
                selectFromInquiryWorker.DoWork += new DoWorkEventHandler(this.selectFromInquiryWorker_DoWork);
                selectFromInquiryWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.selectFromInquiryWorker_Complete);
                selectFromInquiryWorker.RunWorkerAsync();
            }
        }

        private void toolStripInquiryCondition_Click(object sender, EventArgs e)
        {

        }

        private void selectFromInquiryWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.getSerial(this.find_id);
        }

        private void selectFromInquiryWorker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            this.fillSerialInForm();
            this.FormReady();
        }

        private void searchContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSerialBox box = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.CONTACT);
            box.txtSearchKey.Text = this.find_contact;
            if (box.ShowDialog() == DialogResult.OK)
            {
                this.find_contact = box.txtSearchKey.Text;
                this.find_type = FIND_TYPE.CONTACT;
                this.FormLoading();
                BackgroundWorker workerSearch = new BackgroundWorker();
                workerSearch.DoWork += new DoWorkEventHandler(this.workerSearch_DoWork);
                workerSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerSearch_Complete);
                workerSearch.RunWorkerAsync();
            }
        }

        private void searchCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSerialBox box = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.COMPNAM);
            box.txtSearchKey.Text = this.find_company;
            if (box.ShowDialog() == DialogResult.OK)
            {
                this.find_company = box.txtSearchKey.Text;
                this.find_type = FIND_TYPE.COMPANY;
                this.FormLoading();
                BackgroundWorker workerSearch = new BackgroundWorker();
                workerSearch.DoWork += new DoWorkEventHandler(this.workerSearch_DoWork);
                workerSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerSearch_Complete);
                workerSearch.RunWorkerAsync();
            }
        }

        private void searchDealerCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSerialBox box = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.DEALER);
            box.txtSearchKey.Text = this.find_dealer;
            if (box.ShowDialog() == DialogResult.OK)
            {
                this.find_dealer = box.txtSearchKey.Text;
                this.find_type = FIND_TYPE.DEALER;
                this.FormLoading();
                BackgroundWorker workerSearch = new BackgroundWorker();
                workerSearch.DoWork += new DoWorkEventHandler(this.workerSearch_DoWork);
                workerSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerSearch_Complete);
                workerSearch.RunWorkerAsync();
            }
        }

        private void searchOldSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSerialBox box = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.OLDNUM);
            box.mskSearchKey.Text = this.find_oldnum;
            if (box.ShowDialog() == DialogResult.OK)
            {
                this.find_oldnum = box.mskSearchKey.Text;
                this.find_type = FIND_TYPE.OLDNUM;
                this.FormLoading();
                BackgroundWorker workerSearch = new BackgroundWorker();
                workerSearch.DoWork += new DoWorkEventHandler(this.workerSearch_DoWork);
                workerSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerSearch_Complete);
                workerSearch.RunWorkerAsync();
            }
        }

        private void searchBusinessTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSerialBox box = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.BUSITYP);
            box.txtSearchKey.Text = this.find_busityp;
            if (box.ShowDialog() == DialogResult.OK)
            {
                this.find_busityp = box.txtSearchKey.Text;
                this.find_type = FIND_TYPE.BUSITYP;
                this.FormLoading();
                BackgroundWorker workerSearch = new BackgroundWorker();
                workerSearch.DoWork += new DoWorkEventHandler(this.workerSearch_DoWork);
                workerSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerSearch_Complete);
                workerSearch.RunWorkerAsync();
            }
        }

        private void searchAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSerialBox box = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.AREA);
            box.txtSearchKey.Text = this.find_area;
            if (box.ShowDialog() == DialogResult.OK)
            {
                this.find_area = box.txtSearchKey.Text;
                this.find_type = FIND_TYPE.AREA;
                this.FormLoading();
                BackgroundWorker workerSearch = new BackgroundWorker();
                workerSearch.DoWork += new DoWorkEventHandler(this.workerSearch_DoWork);
                workerSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerSearch_Complete);
                workerSearch.RunWorkerAsync();
            }
        }

        #region find by criteria
        private void workerSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            string key_word = string.Empty;

            switch (this.find_type)
	        {
		        case FIND_TYPE.SERNUM:
                    if (this.sortMode != SORT_SN)
                    {
                        this.sortMode = SORT_SN;
                        this.getSerialIDList();
                    }

                    key_word = this.find_sernum;
                    if (key_word.Replace("-", "").Replace(" ", "").Length > 0)
                    {
                        foreach (Serial s in this.serial_id_list)
                        {
                            if (s.sernum.Length >= key_word.Length)
                            {
                                if (s.sernum.Substring(0, key_word.Length) == key_word)
                                {
                                    this.getSerial(s.id);
                                    break;
                                }
                            }

                            if (String.CompareOrdinal(s.sernum, key_word) == 0)
                            {
                                this.getSerial(s.id);
                                break;
                            }
                            else if (String.CompareOrdinal(s.sernum, key_word) > 0)
                            {
                                if (MessageAlert.Show(StringResource.DATA_NOT_FOUND_GET_NEXT_DATA, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                                {
                                    this.getSerial(s.id);
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    break;
                case FIND_TYPE.CONTACT:
                    if (this.sortMode != SORT_CONTACT)
                    {
                        this.sortMode = SORT_CONTACT;
                        this.getSerialIDList();
                    }

                    key_word = this.find_contact;
                    if (key_word.Length > 0)
                    {
                        foreach (Serial s in this.serial_id_list)
                        {
                            if (s.contact != null)
                            {
                                if (s.contact.Length >= key_word.Length)
                                {
                                    if (s.contact.Substring(0, key_word.Length) == key_word)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                }

                                if (String.CompareOrdinal(s.contact, key_word) == 0)
                                {
                                    this.getSerial(s.id);
                                    break;
                                }
                                else if (String.CompareOrdinal(s.contact, key_word) > 0)
                                {
                                    if (MessageAlert.Show(StringResource.DATA_NOT_FOUND_GET_NEXT_DATA, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case FIND_TYPE.COMPANY:
                    if (this.sortMode != SORT_COMPANY)
                    {
                        this.sortMode = SORT_COMPANY;
                        this.getSerialIDList();
                    }

                    key_word = this.find_company;
                    if (key_word.Length > 0)
                    {
                        foreach (Serial s in this.serial_id_list)
                        {
                            if (s.compnam != null)
                            {
                                if (s.compnam.Length >= key_word.Length)
                                {
                                    if (s.compnam.Substring(0, key_word.Length) == key_word)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                }

                                if (String.CompareOrdinal(s.compnam, key_word) == 0)
                                {
                                    this.getSerial(s.id);
                                    break;
                                }
                                else if (String.CompareOrdinal(s.compnam, key_word) > 0)
                                {
                                    if (MessageAlert.Show(StringResource.DATA_NOT_FOUND_GET_NEXT_DATA, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case FIND_TYPE.DEALER:
                    if (this.sortMode != SORT_DEALER)
                    {
                        this.sortMode = SORT_DEALER;
                        this.getSerialIDList();
                    }

                    key_word = this.find_dealer;
                    if (key_word.Length > 0)
                    {
                        foreach (Serial s in this.serial_id_list)
                        {
                            if (s.dealer_dealer != null)
                            {
                                if (s.dealer_dealer.Length >= key_word.Length)
                                {
                                    if (s.dealer_dealer.Substring(0, key_word.Length) == key_word)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                }

                                if (String.CompareOrdinal(s.dealer_dealer, key_word) == 0)
                                {
                                    this.getSerial(s.id);
                                    break;
                                }
                                else if (String.CompareOrdinal(s.dealer_dealer, key_word) > 0)
                                {
                                    if (MessageAlert.Show(StringResource.DATA_NOT_FOUND_GET_NEXT_DATA, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case FIND_TYPE.OLDNUM:
                    if (this.sortMode != SORT_OLDNUM)
                    {
                        this.sortMode = SORT_OLDNUM;
                        this.getSerialIDList();
                    }

                    key_word = this.find_oldnum;
                    if (key_word.Replace("-", "").Replace(" ", "").Length > 0)
                    {
                        foreach (Serial s in this.serial_id_list)
                        {
                            if (s.oldnum != null)
                            {
                                if (s.oldnum.Length >= key_word.Length)
                                {
                                    if (s.oldnum.Substring(0, key_word.Length) == key_word)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                }

                                if (String.CompareOrdinal(s.oldnum, key_word) == 0)
                                {
                                    this.getSerial(s.id);
                                    break;
                                }
                                else if (String.CompareOrdinal(s.oldnum, key_word) > 0)
                                {
                                    if (MessageAlert.Show(StringResource.DATA_NOT_FOUND_GET_NEXT_DATA, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case FIND_TYPE.BUSITYP:
                    if (this.sortMode != SORT_BUSITYP)
                    {
                        this.sortMode = SORT_BUSITYP;
                        this.getSerialIDList();
                    }

                    key_word = this.find_busityp;
                    if (key_word.Length > 0)
                    {
                        foreach (Serial s in this.serial_id_list)
                        {
                            if (s.busityp != null)
                            {
                                if (s.busityp.Length >= key_word.Length)
                                {
                                    if (s.busityp.Substring(0, key_word.Length) == key_word)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                }

                                if (String.CompareOrdinal(s.busityp, key_word) == 0)
                                {
                                    this.getSerial(s.id);
                                    break;
                                }
                                else if (String.CompareOrdinal(s.busityp, key_word) > 0)
                                {
                                    if (MessageAlert.Show(StringResource.DATA_NOT_FOUND_GET_NEXT_DATA, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case FIND_TYPE.AREA:
                    if (this.sortMode != SORT_AREA)
                    {
                        this.sortMode = SORT_AREA;
                        this.getSerialIDList();
                    }
                    key_word = this.find_area;
                    if (key_word.Length > 0)
                    {
                        foreach (Serial s in this.serial_id_list)
                        {
                            if (s.area != null)
                            {
                                if (s.area.Length >= key_word.Length)
                                {
                                    if (s.area.Substring(0, key_word.Length) == key_word)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                }

                                if (String.CompareOrdinal(s.area, key_word) == 0)
                                {
                                    this.getSerial(s.id);
                                    break;
                                }
                                else if (String.CompareOrdinal(s.area, key_word) > 0)
                                {
                                    if (MessageAlert.Show(StringResource.DATA_NOT_FOUND_GET_NEXT_DATA, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                                    {
                                        this.getSerial(s.id);
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void workerSearch_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            this.fillSerialInForm();
            this.FormReady();
        }
        #endregion find by criteria

        private void toolStripReload_Click(object sender, EventArgs e)
        {
            this.FormLoading();
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(this.workerLoadCurrentSN_Dowork);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerLoadSN_Complete);
            work.RunWorkerAsync();
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
            IstabList wind = new IstabList(this.main_form, this.txtBusityp.Text, Istab.TABTYP.BUSITYP);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtBusityp.Text = wind.istab.typcod;
                this.lblBusitypTypdes.Text = wind.istab.typdes_th;
                this.txtDealer_dealer.Focus();
            }
            else
            {
                this.txtBusityp.Focus();
            }
        }

        private void btnBrowseArea_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.main_form, this.txtArea.Text, Istab.TABTYP.AREA);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtArea.Text = wind.istab.typcod;
                this.lblAreaTypdes.Text = wind.istab.typdes_th;
                this.mskRefnum.Focus();
            }
            else
            {
                this.txtArea.Focus();
            }
        }

        private void btnBrowseHowknown_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.main_form, this.txtHowknown.Text, Istab.TABTYP.HOWKNOWN);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtHowknown.Text = wind.istab.typcod;
                this.lblHowknownTypdes.Text = wind.istab.typdes_th;
                this.mskPurdat.Focus();
            }
            else
            {
                this.txtHowknown.Focus();
            }
        }

        private void btnBrowseDealer_Click(object sender, EventArgs e)
        {
            DealerList wind = new DealerList(this, this.txtDealer_dealer.Text);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtDealer_dealer.Text = wind.dealer.dealer;
                this.lblDealer_DealerCompnam.Text = wind.dealer.compnam;
                this.txtHowknown.Focus();
            }
            else
            {
                this.txtDealer_dealer.Focus();
            }
        }
        #endregion Browse button

        private void SnWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm main_form = this.MdiParent as MainForm;
            main_form.sn_wind = null;
        }

        #region Add shortcut key to ToolStripButton
        private void KeyDownShortcutKeyToolstrip(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                this.toolStripPrevious.PerformClick();
            }
            if (e.KeyCode == Keys.PageDown)
            {
                this.toolStripNext.PerformClick();
            }
            if (e.KeyCode == Keys.Home && e.Modifiers == Keys.Control)
            {
                this.toolStripFirst.PerformClick();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.End && e.Modifiers == Keys.Control)
            {
                this.toolStripLast.PerformClick();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.toolStripStop.PerformClick();
                e.Handled = e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.F3)
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.tabControl1.SelectedTab = this.tabPage1;
                    e.Handled = true;
                }
            }
            if (e.KeyCode == Keys.F4)
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.tabControl1.SelectedTab = this.tabPage2;
                    e.Handled = true;
                }
            }
            if (e.KeyCode == Keys.F5)
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.toolStripReload.PerformClick();
                    e.Handled = true;
                }
            }
            if (e.KeyCode == Keys.F8)
            {
                this.toolStripItem.PerformClick();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F9)
            {
                this.toolStripSave.PerformClick();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.D && e.Alt)
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.deleteProblemData();
                }
                else
                {
                    this.toolStripDelete.PerformClick();
                }
            }
            if (e.KeyCode == Keys.L && e.Control)
            {
                this.toolStripInquiryAll.PerformClick();
            }
            if (e.KeyCode == Keys.L && e.Alt)
            {
                this.toolStripInquiryRest.PerformClick();
            }
            if (e.KeyCode == Keys.S && e.Alt)
            {
                this.toolStripSearchSN.PerformClick();
            }
            if (e.KeyCode == Keys.D2 && e.Alt)
            {
                this.toolStripSearchContact.PerformClick();
            }
            if (e.KeyCode == Keys.D3 && e.Alt)
            {
                this.toolStripSearchCompany.PerformClick();
            }
            if (e.KeyCode == Keys.D4 && e.Alt)
            {
                this.toolStripSearchDealer.PerformClick();
            }
            if (e.KeyCode == Keys.D5 && e.Alt)
            {
                this.toolStripSearchOldnum.PerformClick();
            }
            if (e.KeyCode == Keys.D6 && e.Alt)
            {
                this.toolStripSearchBusityp.PerformClick();
            }
            if (e.KeyCode == Keys.D7 && e.Alt)
            {
                this.toolStripSearchArea.PerformClick();
            }
            if (e.KeyCode == Keys.Enter && (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM))
            {
                e.Handled = e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Escape && this.form_mode == FORM_MODE.READ)
            {
                e.Handled = e.SuppressKeyPress = true;
            }
        }
        #endregion Add shortcut key to ToolStripButton

        private void setToolStripFormMode()
        {
            switch (this.form_mode)
            {
                case FORM_MODE.LOADING:
                    Console.WriteLine("FORM_MODE.LOADING");
                    this.toolStripAdd.Enabled = false;
                    this.toolStripEdit.Enabled = false;
                    this.toolStripDelete.Enabled = false;
                    this.toolStripStop.Enabled = false;
                    this.toolStripSave.Enabled = false;
                    this.toolStripFirst.Enabled = false;
                    this.toolStripPrevious.Enabled = false;
                    this.toolStripNext.Enabled = false;
                    this.toolStripLast.Enabled = false;
                    this.toolStripItem.Enabled = false;
                    this.toolStripImport.Enabled = false;
                    this.toolStripGenSN.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    this.toolStripInquiryAll.Enabled = false;
                    this.toolStripInquiryRest.Enabled = false;
                    this.toolStripSearchArea.Enabled = false;
                    this.toolStripSearchBusityp.Enabled = false;
                    this.toolStripSearchCompany.Enabled = false;
                    this.toolStripSearchContact.Enabled = false;
                    this.toolStripSearchDealer.Enabled = false;
                    this.toolStripSearchOldnum.Enabled = false;
                    this.toolStripSearchSN.Enabled = false;
                    this.toolStripReload.Enabled = false;
                    break;
                case FORM_MODE.SAVING:
                    Console.WriteLine("FORM_MODE.SAVING");
                    this.toolStripAdd.Enabled = false;
                    this.toolStripEdit.Enabled = false;
                    this.toolStripDelete.Enabled = false;
                    this.toolStripStop.Enabled = false;
                    this.toolStripSave.Enabled = false;
                    this.toolStripFirst.Enabled = false;
                    this.toolStripPrevious.Enabled = false;
                    this.toolStripNext.Enabled = false;
                    this.toolStripLast.Enabled = false;
                    this.toolStripItem.Enabled = false;
                    this.toolStripImport.Enabled = false;
                    this.toolStripGenSN.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    this.toolStripInquiryAll.Enabled = false;
                    this.toolStripInquiryRest.Enabled = false;
                    this.toolStripSearchArea.Enabled = false;
                    this.toolStripSearchBusityp.Enabled = false;
                    this.toolStripSearchCompany.Enabled = false;
                    this.toolStripSearchContact.Enabled = false;
                    this.toolStripSearchDealer.Enabled = false;
                    this.toolStripSearchOldnum.Enabled = false;
                    this.toolStripSearchSN.Enabled = false;
                    this.toolStripReload.Enabled = false;
                    break;
                case FORM_MODE.READ:
                    Console.WriteLine("FORM_MODE.READ");
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
                    this.toolStripImport.Enabled = true;
                    this.toolStripGenSN.Enabled = true;
                    this.toolStripUpgrade.Enabled = true;
                    this.toolStripBook.Enabled = true;
                    this.toolStripSet2.Enabled = true;
                    this.toolStripSearch.Enabled = true;
                    this.toolStripInquiryAll.Enabled = true;
                    this.toolStripInquiryRest.Enabled = true;
                    this.toolStripSearchArea.Enabled = true;
                    this.toolStripSearchBusityp.Enabled = true;
                    this.toolStripSearchCompany.Enabled = true;
                    this.toolStripSearchContact.Enabled = true;
                    this.toolStripSearchDealer.Enabled = true;
                    this.toolStripSearchOldnum.Enabled = true;
                    this.toolStripSearchSN.Enabled = true;
                    this.toolStripReload.Enabled = true;
                    break;
                case FORM_MODE.ADD:
                    Console.WriteLine("FORM_MODE.ADD");
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
                    this.toolStripImport.Enabled = false;
                    this.toolStripGenSN.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    this.toolStripInquiryAll.Enabled = false;
                    this.toolStripInquiryRest.Enabled = false;
                    this.toolStripSearchArea.Enabled = false;
                    this.toolStripSearchBusityp.Enabled = false;
                    this.toolStripSearchCompany.Enabled = false;
                    this.toolStripSearchContact.Enabled = false;
                    this.toolStripSearchDealer.Enabled = false;
                    this.toolStripSearchOldnum.Enabled = false;
                    this.toolStripSearchSN.Enabled = false;
                    this.toolStripReload.Enabled = false;
                    break;
                case FORM_MODE.EDIT:
                    Console.WriteLine("FORM_MODE.EDIT");
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
                    this.toolStripImport.Enabled = false;
                    this.toolStripGenSN.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    this.toolStripInquiryAll.Enabled = false;
                    this.toolStripInquiryRest.Enabled = false;
                    this.toolStripSearchArea.Enabled = false;
                    this.toolStripSearchBusityp.Enabled = false;
                    this.toolStripSearchCompany.Enabled = false;
                    this.toolStripSearchContact.Enabled = false;
                    this.toolStripSearchDealer.Enabled = false;
                    this.toolStripSearchOldnum.Enabled = false;
                    this.toolStripSearchSN.Enabled = false;
                    this.toolStripReload.Enabled = false;
                    break;
                case FORM_MODE.READ_ITEM:
                    Console.WriteLine("FORM_MODE.READ_ITEM");
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
                    this.toolStripImport.Enabled = false;
                    this.toolStripGenSN.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    this.toolStripInquiryAll.Enabled = false;
                    this.toolStripInquiryRest.Enabled = false;
                    this.toolStripSearchArea.Enabled = false;
                    this.toolStripSearchBusityp.Enabled = false;
                    this.toolStripSearchCompany.Enabled = false;
                    this.toolStripSearchContact.Enabled = false;
                    this.toolStripSearchDealer.Enabled = false;
                    this.toolStripSearchOldnum.Enabled = false;
                    this.toolStripSearchSN.Enabled = false;
                    this.toolStripReload.Enabled = false;
                    break;
                case FORM_MODE.ADD_ITEM:
                    Console.WriteLine("FORM_MODE.ADD_ITEM");
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
                    this.toolStripImport.Enabled = false;
                    this.toolStripGenSN.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    this.toolStripInquiryAll.Enabled = false;
                    this.toolStripInquiryRest.Enabled = false;
                    this.toolStripSearchArea.Enabled = false;
                    this.toolStripSearchBusityp.Enabled = false;
                    this.toolStripSearchCompany.Enabled = false;
                    this.toolStripSearchContact.Enabled = false;
                    this.toolStripSearchDealer.Enabled = false;
                    this.toolStripSearchOldnum.Enabled = false;
                    this.toolStripSearchSN.Enabled = false;
                    this.toolStripReload.Enabled = false;
                    break;
                case FORM_MODE.EDIT_ITEM:
                    Console.WriteLine("FORM_MODE.EDIT_ITEM");
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
                    this.toolStripImport.Enabled = false;
                    this.toolStripGenSN.Enabled = false;
                    this.toolStripUpgrade.Enabled = false;
                    this.toolStripBook.Enabled = false;
                    this.toolStripSet2.Enabled = false;
                    this.toolStripSearch.Enabled = false;
                    this.toolStripInquiryAll.Enabled = false;
                    this.toolStripInquiryRest.Enabled = false;
                    this.toolStripSearchArea.Enabled = false;
                    this.toolStripSearchBusityp.Enabled = false;
                    this.toolStripSearchCompany.Enabled = false;
                    this.toolStripSearchContact.Enabled = false;
                    this.toolStripSearchDealer.Enabled = false;
                    this.toolStripSearchOldnum.Enabled = false;
                    this.toolStripSearchSN.Enabled = false;
                    this.toolStripReload.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private int getMaxSerialId()
        {
            int max_id = 0;
            foreach (Serial s in this.serial_id_list)
            {
                if (s.id > max_id)
                {
                    max_id = s.id;
                }
            }

            return max_id;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab && this.form_mode == FORM_MODE.READ)
            {
                DataInfo data_info = new DataInfo();
                data_info.lblDataTable.Text = "Serial";
                data_info.lblExpression.Text = (this.sortMode == SORT_SN ? this.sortMode : this.sortMode + "+sernum");
                data_info.lblRecBy.Text = this.serial.users_name;
                data_info.lblRecDate.pickedDate(this.serial.chgdat);
                data_info.lblRecNo.Text = this.serial.id.ToString();
                data_info.lblTotalRec.Text = this.getMaxSerialId().ToString();
                data_info.lblTime.ForeColor = Color.DarkGray;
                data_info.lblRecTime.BackColor = Color.WhiteSmoke;
                data_info.ShowDialog();
                return true;
            }
            if (keyData == Keys.Tab && this.form_mode == FORM_MODE.READ_ITEM)
            {
                if (this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag is Problem)
                {
                    CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "problem/get_info&id=" + ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).id.ToString());
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        if (sr.problem.Count > 0)
                        {
                            DataInfo data_info = new DataInfo();
                            data_info.lblDataTable.Text = "Problem";
                            data_info.lblExpression.Text = "sernum+date";
                            data_info.lblRecBy.Text = sr.problem.First<Problem>().users_name;
                            data_info.lblRecDate.pickedDate(sr.problem.First<Problem>().date);
                            data_info.lblRecTime.Text = sr.problem.First<Problem>().time;
                            data_info.lblRecNo.Text = sr.problem.First<Problem>().id.ToString();
                            data_info.lblTotalRec.Text = sr.message;
                            data_info.ShowDialog();
                        }
                    }

                }
                return true;
            }
            if(keyData == (Keys.Alt | Keys.A))
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    int col_index = this.dgvProblem.CurrentCell.ColumnIndex;
                    this.showInlineProblemForm(this.problem.Count, col_index);
                }
                else if (this.form_mode == FORM_MODE.READ)
                {
                    this.toolStripAdd.PerformClick();
                }
                
                return true;
            }
            if (keyData == (Keys.Alt | Keys.E))
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    int row_index = this.dgvProblem.CurrentCell.RowIndex;
                    int col_index = this.dgvProblem.CurrentCell.ColumnIndex;
                    if (this.dgvProblem.Rows[row_index].Tag is Problem)
                    {
                        this.mskEditDate.pickedDate(((Problem)this.dgvProblem.Rows[row_index].Tag).date);
                        this.txtEditName.Text = ((Problem)this.dgvProblem.Rows[row_index].Tag).name;
                        this.txtEditCo.Text = ((Problem)this.dgvProblem.Rows[row_index].Tag).probcod;
                        //this.txtEditDesc.Text = ((Problem)this.dgvProblem.Rows[row_index].Tag).probdesc;
                        string prob_desc = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc;

                        if (this.G.loged_in_user_level == GlobalVar.USER_GROUP_ADMIN)
                        {
                            this.txtEditDesc2.staticText = "";
                            this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc;
                        }
                        else
                        {
                            if (((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probcod == "RG")
                            {
                                string mac_code = this.GetMachineCode(((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc);
                                this.txtEditDesc2.staticText = mac_code;
                                if (mac_code.Length > 0)
                                {
                                    this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc.Substring(mac_code.Length, prob_desc.Length - mac_code.Length).TrimStart();
                                }
                                else
                                {
                                    this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc.Substring(mac_code.Length, prob_desc.Length - mac_code.Length);
                                }
                            }
                            else
                            {
                                this.txtEditDesc2.staticText = "";
                                this.txtEditDesc2.txtEdit.Text = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probdesc;
                            }
                        }
                        this.showInlineProblemForm(row_index, col_index);
                    }
                }
                else
                {
                    this.toolStripEdit.PerformClick();
                }
                return true;
            }
            if(keyData == Keys.Enter && (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT || this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM))
            {
                if (this.current_focused_control == this.mskVerextdat || this.current_focused_control == this.txtEditDesc2.txtEdit)
                {
                    this.toolStripSave.PerformClick();
                }
                else
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }
            if (keyData == Keys.F6)
            {
                if (this.current_focused_control == this.txtArea)
                {
                    this.btnBrowseArea.PerformClick();
                    return true;
                }
                else if (this.current_focused_control == this.txtBusityp)
                {
                    this.btnBrowseBusityp.PerformClick();
                    return true;
                }
                else if (this.current_focused_control == this.txtDealer_dealer)
                {
                    this.btnBrowseDealer.PerformClick();
                    return true;
                }
                else if (this.current_focused_control == this.txtHowknown)
                {
                    this.btnBrowseHowknown.PerformClick();
                    return true;
                }
                else if (this.current_focused_control == this.mskPurdat)
                {
                    this.dpPurdat.Focus();
                    SendKeys.Send("{F4}");
                    this.dpPurdat.CloseUp += delegate
                    {
                        this.mskPurdat.Focus();
                    };
                    return true;
                }
                else if (this.current_focused_control == this.mskExpdat)
                {
                    this.dpExpdat.Focus();
                    SendKeys.Send("{F4}");
                    this.dpExpdat.CloseUp += delegate
                    {
                        this.mskExpdat.Focus();
                    };
                    return true;
                }
                else if (this.current_focused_control == this.mskManual)
                {
                    this.dpManual.Focus();
                    SendKeys.Send("{F4}");
                    this.dpManual.CloseUp += delegate
                    {
                        this.mskManual.Focus();
                    };
                    return true;
                }
                else if (this.current_focused_control == this.mskVerextdat)
                {
                    this.dpVerextdat.Focus();
                    SendKeys.Send("{F4}");
                    this.dpVerextdat.CloseUp += delegate
                    {
                        this.mskVerextdat.Focus();
                    };
                    return true;
                }
                else if (this.current_focused_control == this.cbVerext)
                {
                    SendKeys.Send("{F4}");
                    return true;
                }
                else if (this.current_focused_control == this.mskEditDate)
                {
                    this.dpEditDate.Focus();
                    SendKeys.Send("{F4}");
                    this.dpEditDate.CloseUp += delegate
                    {
                        this.mskEditDate.Focus();
                    };
                    return true;
                }
                else if (this.current_focused_control == this.txtEditCo)
                {
                    IstabList wind = new IstabList(this.main_form, this.txtEditCo.Text, Istab.TABTYP.PROBLEM_CODE);
                    if (wind.ShowDialog() == DialogResult.OK)
                    {
                        this.txtEditCo.Text = wind.istab.typcod;
                    }
                    else
                    {
                        this.txtEditCo.Focus();
                    }
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void onControlEnterHandler(object sender, EventArgs e)
        {
            if (sender is TextBox || sender is MaskedTextBox || sender is NumericUpDown)
            {
                ((Control)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                ((Control)sender).ForeColor = Color.Black;
            }
        }

        private void onControlFocusHandler(object sender, EventArgs e)
        {
            this.current_focused_control = (Control)sender;
            this.main_form.toolStripInfo.Text = (((Control)sender).Tag is string ? (string)((Control)sender).Tag : " ");
        }

        private void onControlLeaveHandler(object sender, EventArgs e)
        {
            if (sender is TextBox || sender is MaskedTextBox || sender is NumericUpDown)
            {
                ((Control)sender).BackColor = Color.White;
                ((Control)sender).ForeColor = Color.Black;
            }
            if ((Control)sender == this.txtArea || (Control)sender == this.txtBusityp || (Control)sender == this.txtHowknown)
            {
                if (((Control)sender).Text.Length > 0)
                {
                    Istab istab = null ;
                    if ((Control)sender == this.txtArea)
                    {
                        istab = this.main_form.data_resource.LIST_AREA.Find(t => t.typcod == this.txtArea.Text);
                    }
                    else if ((Control)sender == this.txtBusityp)
                    {
                        istab = this.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.txtBusityp.Text);
                    }
                    else if ((Control)sender == this.txtHowknown)
                    {
                        istab = this.main_form.data_resource.LIST_HOWKNOWN.Find(t => t.typcod == this.txtHowknown.Text);
                    }
                    
                    if (istab == null)
                    {
                        ((Control)sender).Focus();
                        SendKeys.Send("{F6}");
                    }
                    else
                    {
                        if ((Control)sender == this.txtArea)
                        {
                            this.lblAreaTypdes.Text = istab.typdes_th;
                        }
                        else if ((Control)sender == this.txtBusityp)
                        {
                            this.lblBusitypTypdes.Text = istab.typdes_th;
                        }
                        else if ((Control)sender == this.txtHowknown)
                        {
                            this.lblHowknownTypdes.Text = istab.typdes_th;
                        }
                    }
                }
                else
                {
                    if ((Control)sender == this.txtArea)
                    {
                        this.lblAreaTypdes.Text = "";
                    }
                    else if((Control)sender == this.txtBusityp)
                    {
                        this.lblBusitypTypdes.Text = "";
                    }
                    else if ((Control)sender == this.txtHowknown)
                    {
                        this.lblHowknownTypdes.Text = "";
                    }
                }
            }
            else if ((Control)sender == this.txtDealer_dealer)
            {
                if (((Control)sender).Text.Length > 0)
                {
                    Dealer dealer = this.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == ((Control)sender).Text);
                    if (dealer == null)
                    {
                        ((Control)sender).Focus();
                        SendKeys.Send("{F6}");
                    }
                    else
                    {
                        this.lblDealer_DealerCompnam.Text = dealer.compnam;
                    }
                }
                else
                {
                    this.lblDealer_DealerCompnam.Text = "";
                }
            }
        }

        private void toolStripAdd_EnabledChanged(object sender, EventArgs e)
        {
            if (((ToolStripButton)sender).Enabled)
            {
                this.btnLostRenew.Enabled = true;
                this.btnCD.Enabled = true;
                this.btnUP.Enabled = true;
                this.btnUPNewRwt.Enabled = true;
                this.btnUPNewRwtJob.Enabled = true;
            }
            else
            {
                this.btnLostRenew.Enabled = false;
                this.btnCD.Enabled = false;
                this.btnUP.Enabled = false;
                this.btnUPNewRwt.Enabled = false;
                this.btnUPNewRwtJob.Enabled = false;
            }
        }

        private void transparentPanel1_VisibleChanged(object sender, EventArgs e)
        {
            if (((TransparentPanel)sender).Visible)
            {
                this.chkIMOnly.Enabled = false;
            }
            else
            {
                this.chkIMOnly.Enabled = true;
            }
        }

        private void chkIMOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.is_problem_im_only = true;
                this.fillInDatagrid();
            }
            else
            {
                this.is_problem_im_only = false;
                this.fillInDatagrid();
            }
        }

        private void btnLostRenew_Click(object sender, EventArgs e)
        {
            LostRenewForm wind = new LostRenewForm(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.FormLoading();
                BackgroundWorker workerAfterLostRenew = new BackgroundWorker();
                workerAfterLostRenew.DoWork += delegate
                {
                    this.getSerialIDList();
                    this.getSerial(this.serial.id);
                };
                workerAfterLostRenew.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                    this.FormReady();
                };
                workerAfterLostRenew.RunWorkerAsync();
            }
            else
            {
                this.FormReady();
            }
        }

        private void btnSwithToRefnum_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ)
            {
                if (this.serial.refnum.Replace("-", "").Replace(" ", "").Length > 0)
                {
                    bool founded_sn = false;
                    this.FormLoading();
                    BackgroundWorker workerSwitchRefnum = new BackgroundWorker();
                    workerSwitchRefnum.DoWork += delegate
                    {
                        this.getSerialIDList();
                        CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/get_by_sernum&sernum=" + this.serial.refnum);
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            if (sr.serial.Count > 0)
                            {
                                founded_sn = true;
                                this.serial = sr.serial[0];
                                this.busityp = (sr.busityp.Count > 0 ? sr.busityp[0] : this.busityp_not_found);
                                this.area = (sr.area.Count > 0 ? sr.area[0] : this.area_not_found);
                                this.howknown = (sr.howknown.Count > 0 ? sr.howknown[0] : this.howknown_not_found);
                                this.verext = (sr.verext.Count > 0 ? sr.verext[0] : this.verext_not_found);
                                this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                                this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                                this.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : this.problem_not_found);
                            }
                            else
                            {
                                founded_sn = false;
                            }
                        }
                        else
                        {
                            MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        }
                    };
                    workerSwitchRefnum.RunWorkerCompleted += delegate
                    {
                        if (founded_sn)
                        {
                            this.fillSerialInForm();
                            this.FormReady();
                        }
                        else
                        {
                            this.FormReady();
                            MessageAlert.Show("Reference S/N not found", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        }
                        
                        
                    };
                    workerSwitchRefnum.RunWorkerAsync();
                }
            }
        }

        private void mskSernum_Leave(object sender, EventArgs e)
        {
            if(!ValidateSN.Check(((MaskedTextBox)sender).Text)){
                ((MaskedTextBox)sender).Focus();
            }
        }

        private void toolStripGenSN_Click(object sender, EventArgs e)
        {
            GenerateSNForm wind = new GenerateSNForm(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.FormLoading();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    this.getSerialIDList();
                };
                worker.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                    this.FormReady();
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnCD_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show("Generate \"CD training date\"", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                this.FormLoading();
                bool post_success = false;
                
                BackgroundWorker workerCD = new BackgroundWorker();
                workerCD.DoWork += delegate
                {
                    string json_data = "{\"id\":" + this.serial.id.ToString() + ",";
                    json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";
                    Console.WriteLine(json_data);
                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/gen_cd_training_date", json_data);
                    Console.WriteLine("post.data = " + post.data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        this.serial = sr.serial[0];
                        post_success = true;
                    }
                    else
                    {
                        MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        post_success = false;
                    }
                };
                
                workerCD.RunWorkerCompleted += delegate
                {
                    if (post_success)
                    {
                        this.lblExpdat.pickedDate(this.serial.expdat);
                        this.lblExpdat2.pickedDate(this.serial.expdat);
                        this.FormReady();
                    }
                    else
                    {
                        this.FormReady();
                    }
                };
                
                workerCD.RunWorkerAsync();
            }
            else
            {
                this.FormReady();
            }
        }

        private void btnUPNewRwt_Click(object sender, EventArgs e)
        {
            UpNewRwtLineForm wind = new UpNewRwtLineForm(this, UpNewRwtLineForm.DIALOG_TYPE.UP_NEWRWT);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.lblExpdat.pickedDate(this.serial.expdat);
                this.lblExpdat2.pickedDate(this.serial.expdat);
                this.lblVerext.Text = this.serial.verext.GetVerextSelectedString(this.cbVerext);
                this.lblVerextdat.pickedDate(this.serial.verextdat);
                this.fillInDatagrid();
                this.FormReady();
            }
            else
            {
                this.FormReady();
            }
        }

        private void btnUPNewRwtJob_Click(object sender, EventArgs e)
        {
            UpNewRwtLineForm wind = new UpNewRwtLineForm(this, UpNewRwtLineForm.DIALOG_TYPE.UP_NEWRWT_JOB);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.lblExpdat.pickedDate(this.serial.expdat);
                this.lblVerext.Text = this.serial.verext.GetVerextSelectedString(this.cbVerext);
                this.lblVerextdat.pickedDate(this.serial.verextdat);
                this.fillInDatagrid();
                this.FormReady();
            }
            else
            {
                this.FormReady();
            }
        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show("Generate \"Update\" line", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                bool post_success = false;
                this.FormLoading();

                BackgroundWorker workerUp = new BackgroundWorker();
                workerUp.DoWork += delegate
                {
                    string json_data = "{\"id\":" + this.serial.id.ToString() + ",";
                    json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";

                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "problem/gen_up_line", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        post_success = true;
                        this.problem = sr.problem;
                        this.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : new List<Problem>());
                    }
                    else
                    {
                        MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        post_success = false;
                    }
                };

                workerUp.RunWorkerCompleted += delegate
                {
                    if (post_success)
                    {
                        this.fillInDatagrid();
                        this.FormReady();
                    }
                    else
                    {
                        this.FormReady();
                    }
                };
                
                workerUp.RunWorkerAsync();
            }
            else
            {
                this.FormReady();
            }
        }

        private void toolStripUpgrade_Click(object sender, EventArgs e)
        {
            UpgradeProgramForm wind = new UpgradeProgramForm(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.lblSerNum.Text = this.serial.sernum;
                this.lblVersion.Text = this.serial.version;
                this.lblExpdat.pickedDate(this.serial.expdat);
                this.lblExpdat2.pickedDate(this.serial.expdat);
                this.lblVerext.Text = this.serial.verext.GetVerextSelectedString(this.cbVerext);
                this.lblVerextdat.pickedDate(this.serial.verextdat);
                this.fillInDatagrid();
                this.FormReady();
            }
            else
            {
                this.FormReady();
            }
        }

        private void toolStripBook_Click(object sender, EventArgs e)
        {
            SellBookForm wind = new SellBookForm(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.fillInDatagrid();
            }
        }

        private void toolStripSet2_Click(object sender, EventArgs e)
        {
            SellProgram2nd wind = new SellProgram2nd(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.FormLoading();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    this.getSerialIDList();
                    this.getSerial(this.serial.id);
                };
                worker.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                    this.FormReady();
                };
                worker.RunWorkerAsync();
            }
            else
            {
                this.FormReady();
            }
        }

        private void toolStripImport_Click(object sender, EventArgs e)
        {
            ImportListForm wind = new ImportListForm(this);
            wind.ShowDialog();
        }
    }

    //public class CompareStrings : IComparer<string>
    //{
    //    // Because the class implements IComparer, it must define a 
    //    // Compare method. The method returns a signed integer that indicates 
    //    // whether s1 > s2 (return is greater than 0), s1 < s2 (return is negative),
    //    // or s1 equals s2 (return value is 0). This Compare method compares strings. 
    //    public int Compare(string s1, string s2)
    //    {
    //        return string.CompareOrdinal(s1, s2);
    //    }
    //}

}
