using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
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
        //public int macloud_exp_prd = 0;


        #region declare Data Model
        public Serial serial;
        public Istab busityp;
        public Istab area;
        public Istab howknown;
        public Istab verext;
        public Dealer dealer;
        public List<Problem> problem;
        public List<Problem> problem_im_only;
        public List<Ma> ma;
        public List<CloudSrv> cloudsrv;

        private List<Problem> problem_not_found = new List<Problem>();

        #endregion declare Data Model

        #region declare general variable
        private enum FORM_MODE
        {
            PROCESSING,
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
        private List<Control> list_edit_control;
        public Control current_focused_control;
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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            InitializeComponent();
            this.main_form = main_form;
        }

        private void SnWindow_Load(object sender, EventArgs e)
        {
            this.btnSupportViewNote.Visible = false;
            this.btnSupportNote.Visible = false;

            if (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_ADMIN)
            {
                this.btnCD.Visible = false;
                this.btnUP.Visible = false;
                this.btnUPNewRwt.Visible = false;
                this.btnUPNewRwtJob.Visible = false;
                this.btnSupportViewNote.Visible = true;
                this.btnSupportNote.Visible = true;
            }
            if (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ACCOUNT)
            {
                this.toolStripImport.Visible = false;
                this.toolStripGenSN.Visible = false;
            }
            //if (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES)
            //{
            //    this.toolStripGenSN.Visible = false;
            //}
            this.main_form = this.MdiParent as MainForm;
            this.BindEditControlHandler();
            this.FormProcessing();
            this.loadVerextComboBox();
            this.dgvProblem.MouseClick += new MouseEventHandler(this.showProblemContextMenu);

            this.sortMode = SORT_SN;

            BackgroundWorker snWorker = new BackgroundWorker();
            snWorker.DoWork += new DoWorkEventHandler(this.workerLoadLastSN_Dowork);
            snWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerLoadSN_Complete);
            snWorker.RunWorkerAsync();
        }

        private void BindEditControlHandler() // Create list of edit control and bind doubleclick event to edit handler, keep current_focused_control
        {
            this.list_edit_control = new List<Control>();
            this.list_edit_control.Add(this.txtSernum);
            this.list_edit_control.Add(this.txtArea);
            this.list_edit_control.Add(this.txtVersion);
            this.list_edit_control.Add(this.txtRefnum);
            this.list_edit_control.Add(this.txtPrenam);
            this.list_edit_control.Add(this.txtCompnam);
            this.list_edit_control.Add(this.txtAddr01);
            this.list_edit_control.Add(this.txtAddr02);
            this.list_edit_control.Add(this.txtAddr03);
            this.list_edit_control.Add(this.txtZipcod);
            this.list_edit_control.Add(this.txtTelnum);
            this.list_edit_control.Add(this.txtFaxnum);
            this.list_edit_control.Add(this.txtContact);
            this.list_edit_control.Add(this.txtPosition);
            this.list_edit_control.Add(this.txtOldnum);
            this.list_edit_control.Add(this.txtRemark);
            this.list_edit_control.Add(this.txtBusides);
            this.list_edit_control.Add(this.txtBusityp);
            this.list_edit_control.Add(this.txtDealer);
            this.list_edit_control.Add(this.txtHowknown);
            this.list_edit_control.Add(this.txtUpfree);
            this.list_edit_control.Add(this.dtPurdat);
            this.list_edit_control.Add(this.dtExpdat);
            this.list_edit_control.Add(this.dtManual);
            this.list_edit_control.Add(this.dtVerextdat);
            this.list_edit_control.Add(this.cbVerext);

            foreach (Control ct in this.list_edit_control)
            {
                if (ct is CustomTextBox)
                {
                    ((CustomTextBox)ct).label1.DoubleClick += delegate
                    {
                        if (this.form_mode == FORM_MODE.READ)
                        {
                            this.toolStripEdit.PerformClick();
                            if (ct == this.txtSernum)
                            {
                                this.txtVersion.Focus();
                            }
                            else
                            {
                                ct.Focus();
                            }
                        }
                    };

                    ((CustomTextBox)ct).textBox1.GotFocus += delegate
                    {
                        this.current_focused_control = ct;
                        this.toolStripInfo.Text = this.toolTip1.GetToolTip(this.current_focused_control);
                    };
                }
                if (ct is CustomMaskedTextBox)
                {
                    ((CustomMaskedTextBox)ct).label1.DoubleClick += delegate
                    {
                        if (this.form_mode == FORM_MODE.READ)
                        {
                            this.toolStripEdit.PerformClick();
                            if (ct == this.txtSernum)
                            {
                                this.txtVersion.Focus();
                            }
                            else
                            {
                                ct.Focus();
                            }
                        }
                    };

                    ((CustomMaskedTextBox)ct).textBox1.GotFocus += delegate
                    {
                        this.current_focused_control = ct;
                        this.toolStripInfo.Text = this.toolTip1.GetToolTip(this.current_focused_control);
                    };
                }
                if (ct is CustomComboBox)
                {
                    ((CustomComboBox)ct).label1.DoubleClick += delegate
                    {
                        if (this.form_mode == FORM_MODE.READ)
                        {
                            this.toolStripEdit.PerformClick();
                            if (ct == this.txtSernum)
                            {
                                this.txtVersion.Focus();
                            }
                            else
                            {
                                ct.Focus();
                            }
                        }
                    };

                    ((CustomComboBox)ct).comboBox1.GotFocus += delegate
                    {
                        this.current_focused_control = ct;
                        this.toolStripInfo.Text = this.toolTip1.GetToolTip(this.current_focused_control);
                    };
                }
                if (ct is CustomDateTimePicker)
                {
                    ((CustomDateTimePicker)ct).label1.DoubleClick += delegate
                    {
                        if (this.form_mode == FORM_MODE.READ)
                        {
                            this.toolStripEdit.PerformClick();
                            ct.Focus();
                        }
                    };
                    ((CustomDateTimePicker)ct).textBox1.GotFocus += delegate
                    {
                        this.current_focused_control = ct;
                        this.toolStripInfo.Text = this.toolTip1.GetToolTip(this.current_focused_control);
                    };
                }
            }

            this.txtSernum.Leave += delegate(object sender, EventArgs e) // Validate S/N while add new S/N
            {
                if (!ValidateSN.Check(((CustomMaskedTextBox)sender).Texts) && this.form_mode == FORM_MODE.ADD)
                {
                    ((CustomMaskedTextBox)sender).Focus();
                }
            };

            this.txtArea.Leave += delegate(object sender, EventArgs e) // Validate area code
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
                {
                    if (this.txtArea.Texts.Length == 0)
                    {
                        this.lblAreaTypdes.Text = "";
                        return;
                    }
                    if (this.main_form.data_resource.LIST_AREA.Find(t => t.typcod == this.txtArea.Texts) == null)
                    {
                        this.txtArea.Focus();
                        SendKeys.Send("{F6}");
                    }
                    else
                    {
                        this.lblAreaTypdes.Text = this.main_form.data_resource.LIST_AREA.Find(t => t.typcod == this.txtArea.Texts).typdes_th;
                    }
                }
            };

            this.txtBusityp.Leave += delegate
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
                {
                    if (this.txtBusityp.Texts.Length == 0)
                    {
                        this.lblBusitypTypdes.Text = "";
                        return;
                    }
                    if (this.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.txtBusityp.Texts) == null)
                    {
                        this.txtBusityp.Focus();
                        SendKeys.Send("{F6}");
                    }
                    else
                    {
                        this.lblBusitypTypdes.Text = this.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.txtBusityp.Texts).typdes_th;
                    }
                }
            };

            this.txtDealer.Leave += delegate
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
                {
                    if (this.txtDealer.Texts.Length == 0)
                    {
                        this.lblDealer_DealerCompnam.Text = "";
                        return;
                    }
                    if (this.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == this.txtDealer.Texts) == null)
                    {
                        this.txtDealer.Focus();
                        SendKeys.Send("{F6}");
                    }
                    else
                    {
                        this.lblDealer_DealerCompnam.Text = this.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == this.txtDealer.Texts).compnam;
                    }
                }
            };

            this.txtHowknown.Leave += delegate
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
                {
                    if (this.txtHowknown.Texts.Length == 0)
                    {
                        this.lblHowknownTypdes.Text = "";
                        return;
                    }
                    if (this.main_form.data_resource.LIST_HOWKNOWN.Find(t => t.typcod == this.txtHowknown.Texts) == null)
                    {
                        this.txtHowknown.Focus();
                        SendKeys.Send("{F6}");
                    }
                    else
                    {
                        this.lblHowknownTypdes.Text = this.main_form.data_resource.LIST_HOWKNOWN.Find(t => t.typcod == this.txtHowknown.Texts).typdes_th;
                    }
                }
            };
        }

        private void EditControlReadState() // Set control to read state
        {
            foreach (Control ct in this.list_edit_control)
            {
                if (ct is CustomTextBox)
                {
                    ((CustomTextBox)ct).Read_Only = true;
                }
                if (ct is CustomMaskedTextBox)
                {
                    ((CustomMaskedTextBox)ct).Read_Only = true;
                }
                if (ct is CustomComboBox)
                {
                    ((CustomComboBox)ct).Read_Only = true;
                }
            }
        }

        private void EditControlEditState() // Set control to edit state
        {
            foreach (Control ct in this.list_edit_control)
            {
                if (ct is CustomTextBox)
                {
                    ((CustomTextBox)ct).Read_Only = false;
                }
                if (ct is CustomMaskedTextBox)
                {
                    ((CustomMaskedTextBox)ct).Read_Only = false;
                }
                if (ct is CustomComboBox)
                {
                    ((CustomComboBox)ct).Read_Only = false;
                }
            }
        }

        private void EditControlBlank() // Clear all text in edit control
        {
            foreach (Control ct in this.list_edit_control)
            {
                if (ct is CustomTextBox)
                {
                    ((CustomTextBox)ct).Texts = "";
                }
                if (ct is CustomMaskedTextBox)
                {
                    ((CustomMaskedTextBox)ct).Texts = "";
                }
                if (ct is CustomComboBox)
                {
                    ((CustomComboBox)ct).Texts = "";
                }
                if (ct is CustomDateTimePicker)
                {
                    ((CustomDateTimePicker)ct).Texts = "  /  /    ";
                }
                if (ct is CustomComboBox)
                {
                    ((CustomComboBox)ct).comboBox1.SelectedIndex = 0;
                }
            }
            this.lblAreaTypdes.Text = "";
            this.lblBusitypTypdes.Text = "";
            this.lblDealer_DealerCompnam.Text = "";
            this.lblHowknownTypdes.Text = "";
            this.maDateFrom.Texts = "";
            this.maDateTo.Texts = "";
            this.maEmail.Texts = "";
        }

        private void workerLoadLastSN_Dowork(object sender, DoWorkEventArgs e)
        {
            this.getSerialIDList();
            this.getLastSerial();
        }

        private void workerLoadCurrentSN_Dowork(object sender, DoWorkEventArgs e)
        {
            this.getSerialIDList();
            this.getSerial(this.serial.id);
            this.main_form.data_resource.Refresh();
        }

        private void workerLoadSN_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            this.fillSerialInForm();
            this.dgvProblem.Dock = DockStyle.Fill;
            this.FormRead();
        }

        #region Set form state
        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;
            this.toolStripProcessing.Visible = true;

            #region Toolstrip Button
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
            #endregion Toolstrip Button

            #region Button
            this.btnBrowseArea.Enabled = false;
            this.btnBrowseBusityp.Enabled = false;
            this.btnBrowseDealer.Enabled = false;
            this.btnBrowseHowknown.Enabled = false;
            this.chkIMOnly.Enabled = false;
            this.btnLostRenew.Enabled = false;
            this.btnSwithToRefnum.Enabled = false;
            this.btnCD.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnCD.Enabled = false;
            this.btnUP.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUP.Enabled = false;
            this.btnUPNewRwt.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwt.Enabled = false;
            this.btnUPNewRwtJob.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwtJob.Enabled = false;
            this.btnSupportNote.Enabled = false;
            this.btnSupportViewNote.Enabled = false;
            this.btnPasswordAdd.Enabled = false;
            this.btnPasswordRemove.Enabled = false;
            this.btnEditMA.Enabled = false;
            this.btnDeleteMA.Enabled = false;
            #endregion Button

            #region DatePicker
            this.dtPurdat.Read_Only = true;
            this.dtExpdat.Read_Only = true;
            this.dtManual.Read_Only = true;
            this.dtVerextdat.Read_Only = true;
            #endregion DatePicker

            #region Inline Problem Form
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
            {
                CustomDateTimePicker date = (CustomDateTimePicker)this.dgvProblem.Parent.Controls.Find("inline_problem_date", true)[0];
                date.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
            {
                CustomTextBox name = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_name", true)[0];
                name.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
            {
                CustomBrowseField probcod = (CustomBrowseField)this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true)[0];
                probcod._ReadOnly = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
            {
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true)[0];
                probdesc.Read_Only = true;
            }
            #endregion Inline Problem Form

            this.txtDummy.Focus();
            this.EditControlReadState();
        }

        private void FormSaving()
        {
            this.form_mode = FORM_MODE.SAVING;
            this.toolStripProcessing.Visible = true;

            #region Toolstrip Button
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
            #endregion Toolstrip Button

            #region Button
            this.btnBrowseArea.Enabled = false;
            this.btnBrowseBusityp.Enabled = false;
            this.btnBrowseDealer.Enabled = false;
            this.btnBrowseHowknown.Enabled = false;
            this.chkIMOnly.Enabled = false;
            this.btnLostRenew.Enabled = false;
            this.btnSwithToRefnum.Enabled = false;
            this.btnCD.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnCD.Enabled = false;
            this.btnUP.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUP.Enabled = false;
            this.btnUPNewRwt.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwt.Enabled = false;
            this.btnUPNewRwtJob.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwtJob.Enabled = false;
            this.btnSupportNote.Enabled = false;
            this.btnSupportViewNote.Enabled = false;
            this.btnPasswordAdd.Enabled = false;
            this.btnPasswordRemove.Enabled = false;
            this.btnEditMA.Enabled = false;
            this.btnDeleteMA.Enabled = false;
            #endregion Button

            #region DatePicker
            this.dtPurdat.Read_Only = true;
            this.dtExpdat.Read_Only = true;
            this.dtManual.Read_Only = true;
            this.dtVerextdat.Read_Only = true;
            #endregion DatePicker

            this.EditControlReadState();
        }

        private void FormRead()
        {
            this.form_mode = FORM_MODE.READ;
            this.toolStripProcessing.Visible = false;
            this.toolStripInfo.Text = "";

            #region Toolstrip Button
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
            #endregion Toolstrip Button

            #region Button
            this.btnBrowseArea.Enabled = false;
            this.btnBrowseBusityp.Enabled = false;
            this.btnBrowseDealer.Enabled = false;
            this.btnBrowseHowknown.Enabled = false;
            this.chkIMOnly.Enabled = true;
            this.btnLostRenew.Enabled = true;
            this.btnSwithToRefnum.Enabled = true;
            this.btnCD.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnCD.Enabled = (this.btnCD.Visible ? true : false);
            this.btnUP.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUP.Enabled = (this.btnUP.Visible ? true : false);
            this.btnUPNewRwt.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwt.Enabled = (this.btnUPNewRwt.Visible ? true : false);
            this.btnUPNewRwtJob.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwtJob.Enabled = (this.btnUPNewRwtJob.Visible ? true : false);
            this.btnSupportNote.Enabled = ((this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES) ? (this.main_form.lblTimeDuration.Visible ? false : true) : false);
            this.btnSupportViewNote.Enabled = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES ? true : false);
            this.btnPasswordAdd.Enabled = true;
            this.btnPasswordRemove.Enabled = true;
            this.btnEditMA.Enabled = true;
            this.btnDeleteMA.Enabled = true;
            #endregion Button

            #region DatePicker
            this.dtPurdat.Read_Only = true;
            this.dtExpdat.Read_Only = true;
            this.dtManual.Read_Only = true;
            this.dtVerextdat.Read_Only = true;
            #endregion DatePicker

            #region Inline Problem Form
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
            {
                CustomDateTimePicker date = (CustomDateTimePicker)this.dgvProblem.Parent.Controls.Find("inline_problem_date", true)[0];
                date.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
            {
                CustomTextBox name = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_name", true)[0];
                name.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
            {
                CustomTextBox probcod = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true)[0];
                probcod.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
            {
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true)[0];
                probdesc.Read_Only = true;
            }
            #endregion Inline Problem Form

            this.txtDummy.Focus(); // throw(set) focus to dummy textbox
            this.EditControlReadState();
        }

        private void FormAdd()
        {
            #region no use
            //this.tabControl1.SelectedTab = this.tabPage1;
            //this.lblAreaTypdes.Text = "";
            //this.lblBusitypTypdes.Text = "";
            //this.lblDealer_DealerCompnam.Text = "";
            //this.lblHowknownTypdes.Text = "";
            //this.form_mode = FORM_MODE.ADD;
            //this.setToolStripFormMode();
            //this.Cursor = Cursors.Default;
            //this.main_form.Cursor = Cursors.Default;
            //this.main_form.toolStripProcessing.Visible = false;
            //this.main_form.menuStrip1.Enabled = true;

            //List<Control> lct = new List<Control>();
            //lct.Add(this.mskSernum);
            //lct.Add(this.txtVersion);
            //lct.Add(this.txtArea);
            //lct.Add(this.mskRefnum);
            //lct.Add(this.txtPrenam);
            //lct.Add(this.txtCompnam);
            //lct.Add(this.txtAddr01);
            //lct.Add(this.txtAddr02);
            //lct.Add(this.txtAddr03);
            //lct.Add(this.txtZipcod);
            //lct.Add(this.txtTelnum);
            //lct.Add(this.txtFaxnum);
            //lct.Add(this.txtContact);
            //lct.Add(this.txtPosition);
            //lct.Add(this.mskOldnum);
            //lct.Add(this.txtRemark);
            //lct.Add(this.txtBusides);
            //lct.Add(this.txtBusityp);
            //lct.Add(this.txtDealer_dealer);
            //lct.Add(this.txtHowknown);
            //lct.Add(this.mskPurdat);
            //lct.Add(this.mskExpdat);
            //lct.Add(this.txtReg);
            //lct.Add(this.mskManual);
            //lct.Add(this.cbVerext);
            //lct.Add(this.mskVerextdat);
            //List<Label> llb = new List<Label>();
            //llb.Add(this.lblSerNum);
            //llb.Add(this.lblVersion);
            //llb.Add(this.lblArea);
            //llb.Add(this.lblRefnum);
            //llb.Add(this.lblPrenam);
            //llb.Add(this.lblCompnam);
            //llb.Add(this.lblAddr01);
            //llb.Add(this.lblAddr02);
            //llb.Add(this.lblAddr03);
            //llb.Add(this.lblZipcod);
            //llb.Add(this.lblTelnum);
            //llb.Add(this.lblFaxnum);
            //llb.Add(this.lblContact);
            //llb.Add(this.lblPosition);
            //llb.Add(this.lblOldnum);
            //llb.Add(this.lblRemark);
            //llb.Add(this.lblBusides);
            //llb.Add(this.lblBusityp);
            //llb.Add(this.lblDealer_dealer);
            //llb.Add(this.lblHowknown);
            //llb.Add(this.lblPurdat);
            //llb.Add(this.lblExpdat);
            //llb.Add(this.lblReg);
            //llb.Add(this.lblManual);
            //llb.Add(this.lblVerext);
            //llb.Add(this.lblVerextdat);
            //this.Add(lct, llb);
            //this.transLayerHeader.Visible = false;
            //this.transLayerBody1.Visible = false;
            //this.transLayerBody2.Visible = false;
            #endregion no use
            this.txtDummy.Focus(); // set focus to dummy textbox first
            this.form_mode = FORM_MODE.ADD;
            this.toolStripProcessing.Visible = false;

            #region Toolstrip Button
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
            #endregion Toolstrip Button

            #region Button
            this.btnBrowseArea.Enabled = true;
            this.btnBrowseBusityp.Enabled = true;
            this.btnBrowseDealer.Enabled = true;
            this.btnBrowseHowknown.Enabled = true;
            this.chkIMOnly.Enabled = false;
            this.btnLostRenew.Enabled = false;
            this.btnSwithToRefnum.Enabled = false;
            this.btnCD.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnCD.Enabled = false;
            this.btnUP.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUP.Enabled = false;
            this.btnUPNewRwt.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwt.Enabled = false;
            this.btnUPNewRwtJob.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwtJob.Enabled = false;
            this.btnSupportNote.Enabled = false;
            this.btnSupportViewNote.Enabled = false;
            this.btnPasswordAdd.Enabled = false;
            this.btnPasswordRemove.Enabled = false;
            this.btnEditMA.Enabled = false;
            this.btnDeleteMA.Enabled = false;
            #endregion Button

            #region DatePicker
            this.dtPurdat.Read_Only = false;
            this.dtExpdat.Read_Only = false;
            this.dtManual.Read_Only = false;
            this.dtVerextdat.Read_Only = false;
            #endregion DatePicker

            #region Inline Problem Form
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
            {
                CustomDateTimePicker date = (CustomDateTimePicker)this.dgvProblem.Parent.Controls.Find("inline_problem_date", true)[0];
                date.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
            {
                CustomTextBox name = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_name", true)[0];
                name.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
            {
                CustomTextBox probcod = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true)[0];
                probcod.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
            {
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true)[0];
                probdesc.Read_Only = true;
            }
            #endregion Inline Problem Form

            this.EditControlEditState();
            this.txtSernum.Focus(); // throw(set) focus to txtSernum
        }

        private void FormEdit()
        {
            #region no use
            //this.tabControl1.SelectedTab = this.tabPage1;
            //this.form_mode = FORM_MODE.EDIT;
            //this.setToolStripFormMode();
            //this.Cursor = Cursors.Default;
            //this.main_form.Cursor = Cursors.Default;
            //this.main_form.toolStripProcessing.Visible = false;
            //this.main_form.menuStrip1.Enabled = true;

            //List<Control> lct = new List<Control>();
            //lct.Add(this.txtVersion);
            //lct.Add(this.txtArea);
            //lct.Add(this.mskRefnum);
            //lct.Add(this.txtPrenam);
            //lct.Add(this.txtCompnam);
            //lct.Add(this.txtAddr01);
            //lct.Add(this.txtAddr02);
            //lct.Add(this.txtAddr03);
            //lct.Add(this.txtZipcod);
            //lct.Add(this.txtTelnum);
            //lct.Add(this.txtFaxnum);
            //lct.Add(this.txtContact);
            //lct.Add(this.txtPosition);
            //lct.Add(this.mskOldnum);
            //lct.Add(this.txtRemark);
            //lct.Add(this.txtBusides);
            //lct.Add(this.txtBusityp);
            //lct.Add(this.txtDealer_dealer);
            //lct.Add(this.txtHowknown);
            //lct.Add(this.mskPurdat);
            //lct.Add(this.mskExpdat);
            //lct.Add(this.txtReg);
            //lct.Add(this.mskManual);
            //lct.Add(this.cbVerext);
            //lct.Add(this.mskVerextdat);
            //List<Label> llb = new List<Label>();
            //llb.Add(this.lblVersion);
            //llb.Add(this.lblArea);
            //llb.Add(this.lblRefnum);
            //llb.Add(this.lblPrenam);
            //llb.Add(this.lblCompnam);
            //llb.Add(this.lblAddr01);
            //llb.Add(this.lblAddr02);
            //llb.Add(this.lblAddr03);
            //llb.Add(this.lblZipcod);
            //llb.Add(this.lblTelnum);
            //llb.Add(this.lblFaxnum);
            //llb.Add(this.lblContact);
            //llb.Add(this.lblPosition);
            //llb.Add(this.lblOldnum);
            //llb.Add(this.lblRemark);
            //llb.Add(this.lblBusides);
            //llb.Add(this.lblBusityp);
            //llb.Add(this.lblDealer_dealer);
            //llb.Add(this.lblHowknown);
            //llb.Add(this.lblPurdat);
            //llb.Add(this.lblExpdat);
            //llb.Add(this.lblReg);
            //llb.Add(this.lblManual);
            //llb.Add(this.lblVerext);
            //llb.Add(this.lblVerextdat);
            //this.Edit(lct, llb);
            //this.transLayerHeader.Visible = false;
            //this.transLayerBody1.Visible = false;
            //this.transLayerBody2.Visible = false;
            #endregion no use
            this.txtDummy.Focus(); // set focus to dummy textbox first
            this.tabControl1.SelectedTab = tabPage1;
            this.form_mode = FORM_MODE.EDIT;
            this.toolStripProcessing.Visible = false;

            #region Toolstrip Button
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
            #endregion Toolstrip Button

            #region Button
            this.btnBrowseArea.Enabled = true;
            this.btnBrowseBusityp.Enabled = true;
            this.btnBrowseDealer.Enabled = true;
            this.btnBrowseHowknown.Enabled = true;
            this.chkIMOnly.Enabled = false;
            this.btnLostRenew.Enabled = false;
            this.btnSwithToRefnum.Enabled = false;
            this.btnCD.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnCD.Enabled = false;
            this.btnUP.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUP.Enabled = false;
            this.btnUPNewRwt.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwt.Enabled = false;
            this.btnUPNewRwtJob.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwtJob.Enabled = false;
            this.btnSupportNote.Enabled = false;
            this.btnSupportViewNote.Enabled = false;
            this.btnPasswordAdd.Enabled = false;
            this.btnPasswordRemove.Enabled = false;
            this.btnEditMA.Enabled = false;
            this.btnDeleteMA.Enabled = false;
            #endregion Button

            #region DatePicker
            this.dtPurdat.Read_Only = false;
            this.dtExpdat.Read_Only = false;
            this.dtManual.Read_Only = false;
            this.dtVerextdat.Read_Only = false;
            #endregion DatePicker

            #region Inline Problem Form
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
            {
                CustomDateTimePicker date = (CustomDateTimePicker)this.dgvProblem.Parent.Controls.Find("inline_problem_date", true)[0];
                date.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
            {
                CustomTextBox name = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_name", true)[0];
                name.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
            {
                CustomTextBox probcod = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true)[0];
                probcod.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
            {
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true)[0];
                probdesc.Read_Only = true;
            }
            #endregion Inline Problem Form

            this.EditControlEditState();
            this.txtSernum.Read_Only = true;
            this.txtVersion.Focus(); // throw(set) focus to txtVersion
        }

        private void FormReadItem()
        {
            #region no use
            //this.form_mode = FORM_MODE.READ_ITEM;
            //this.setToolStripFormMode();
            //this.Cursor = Cursors.Default;
            //this.main_form.Cursor = Cursors.Default;
            //this.btnSwithToRefnum.TabStop = true;
            //this.main_form.toolStripProcessing.Visible = false;
            //this.main_form.menuStrip1.Enabled = true;
            //this.dgvProblem.Focus();
            //this.transLayerHeader.Visible = false;
            //this.transLayerBody1.Visible = false;
            //this.transLayerBody2.Visible = false;
            //this.main_form.toolStripInfo.Text = "";
            #endregion no use
            this.form_mode = FORM_MODE.READ_ITEM;
            this.toolStripProcessing.Visible = false;
            this.toolStripInfo.Text = "";

            #region Toolstrip Button
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
            #endregion Toolstrip Button

            #region Button
            this.btnBrowseArea.Enabled = false;
            this.btnBrowseBusityp.Enabled = false;
            this.btnBrowseDealer.Enabled = false;
            this.btnBrowseHowknown.Enabled = false;
            this.chkIMOnly.Enabled = true;
            this.btnLostRenew.Enabled = false;
            this.btnSwithToRefnum.Enabled = false;
            this.btnCD.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnCD.Enabled = false;
            this.btnUP.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUP.Enabled = false;
            this.btnUPNewRwt.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwt.Enabled = false;
            this.btnUPNewRwtJob.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwtJob.Enabled = false;
            this.btnSupportNote.Enabled = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES ? (this.main_form.lblTimeDuration.Visible ? false : true) : false);
            this.btnSupportViewNote.Enabled = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES ? true : false);
            this.btnPasswordAdd.Enabled = false;
            this.btnPasswordRemove.Enabled = false;
            this.btnEditMA.Enabled = false;
            this.btnDeleteMA.Enabled = false;
            #endregion Button

            #region DatePicker
            this.dtPurdat.Read_Only = true;
            this.dtExpdat.Read_Only = true;
            this.dtManual.Read_Only = true;
            this.dtVerextdat.Read_Only = true;
            #endregion DatePicker

            #region Inline Problem Form
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
            {
                CustomDateTimePicker date = (CustomDateTimePicker)this.dgvProblem.Parent.Controls.Find("inline_problem_date", true)[0];
                date.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
            {
                CustomTextBox name = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_name", true)[0];
                name.Read_Only = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
            {
                CustomBrowseField probcod = (CustomBrowseField)this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true)[0];
                probcod._ReadOnly = true;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
            {
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true)[0];
                probdesc.Read_Only = true;
            }
            #endregion Inline Problem Form

            this.dgvProblem.Enabled = true;
            this.dgvProblem.Focus();
            this.dgvProblem.Rows[0].Cells[1].Selected = true;
        }

        private void FormAddItem()
        {
            #region no use
            //this.form_mode = FORM_MODE.ADD_ITEM;
            //this.setToolStripFormMode();
            //this.Cursor = Cursors.Default;
            //this.main_form.Cursor = Cursors.Default;
            //this.btnSwithToRefnum.TabStop = false;
            //this.main_form.toolStripProcessing.Visible = false;
            //this.main_form.menuStrip1.Enabled = true;
            //this.transLayerHeader.Visible = false;
            //this.transLayerBody1.Visible = false;
            //this.transLayerBody2.Visible = false;
            #endregion no use
            this.form_mode = FORM_MODE.ADD_ITEM;
            this.toolStripProcessing.Visible = false;

            #region Toolstrip Button
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
            #endregion Toolstrip Button

            #region Button
            this.btnBrowseArea.Enabled = false;
            this.btnBrowseBusityp.Enabled = false;
            this.btnBrowseDealer.Enabled = false;
            this.btnBrowseHowknown.Enabled = false;
            this.chkIMOnly.Enabled = false;
            this.btnLostRenew.Enabled = false;
            this.btnSwithToRefnum.Enabled = false;
            this.btnCD.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnCD.Enabled = false;
            this.btnUP.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUP.Enabled = false;
            this.btnUPNewRwt.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwt.Enabled = false;
            this.btnUPNewRwtJob.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwtJob.Enabled = false;
            this.btnSupportNote.Enabled = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES ? (this.main_form.lblTimeDuration.Visible ? false : true) : false);
            this.btnSupportViewNote.Enabled = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES ? true : false);
            this.btnPasswordAdd.Enabled = false;
            this.btnPasswordRemove.Enabled = false;
            this.btnEditMA.Enabled = false;
            this.btnDeleteMA.Enabled = false;
            #endregion Button

            #region DatePicker
            this.dtPurdat.Read_Only = true;
            this.dtExpdat.Read_Only = true;
            this.dtManual.Read_Only = true;
            this.dtVerextdat.Read_Only = true;
            #endregion DatePicker

            #region Inline Problem Form
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
            {
                CustomDateTimePicker date = (CustomDateTimePicker)this.dgvProblem.Parent.Controls.Find("inline_problem_date", true)[0];
                date.Read_Only = false;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
            {
                CustomTextBox name = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_name", true)[0];
                name.Read_Only = false;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
            {
                CustomTextBox probcod = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true)[0];
                probcod.Read_Only = false;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
            {
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true)[0];
                probdesc.Read_Only = false;
            }
            #endregion Inline Problem Form
        }

        private void FormEditItem()
        {
            #region no use
            //this.form_mode = FORM_MODE.EDIT_ITEM;
            //this.setToolStripFormMode();
            //this.Cursor = Cursors.Default;
            //this.main_form.Cursor = Cursors.Default;
            //this.btnSwithToRefnum.TabStop = false;
            //this.main_form.toolStripProcessing.Visible = false;
            //this.main_form.menuStrip1.Enabled = true;
            //this.transLayerHeader.Visible = false;
            //this.transLayerBody1.Visible = false;
            //this.transLayerBody2.Visible = false;
            #endregion no use
            this.form_mode = FORM_MODE.EDIT_ITEM;
            this.toolStripProcessing.Visible = false;

            #region Toolstrip Button
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
            #endregion Toolstrip Button

            #region Button
            this.btnBrowseArea.Enabled = false;
            this.btnBrowseBusityp.Enabled = false;
            this.btnBrowseDealer.Enabled = false;
            this.btnBrowseHowknown.Enabled = false;
            this.chkIMOnly.Enabled = false;
            this.btnLostRenew.Enabled = false;
            this.btnSwithToRefnum.Enabled = false;
            this.btnCD.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnCD.Enabled = false;
            this.btnUP.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUP.Enabled = false;
            this.btnUPNewRwt.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwt.Enabled = false;
            this.btnUPNewRwtJob.Visible = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_ADMIN ? true : false);
            this.btnUPNewRwtJob.Enabled = false;
            this.btnSupportNote.Enabled = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES ? (this.main_form.lblTimeDuration.Visible ? false : true) : false);
            this.btnSupportViewNote.Enabled = (this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SUPPORT || this.main_form.G.loged_in_user_level == GlobalVar.USER_LEVEL_SALES ? true : false);
            this.btnPasswordAdd.Enabled = false;
            this.btnPasswordRemove.Enabled = false;
            this.btnEditMA.Enabled = false;
            this.btnDeleteMA.Enabled = false;
            #endregion Button

            #region DatePicker
            this.dtPurdat.Read_Only = true;
            this.dtExpdat.Read_Only = true;
            this.dtManual.Read_Only = true;
            this.dtVerextdat.Read_Only = true;
            #endregion DatePicker

            #region Inline Problem Form
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
            {
                CustomDateTimePicker date = (CustomDateTimePicker)this.dgvProblem.Parent.Controls.Find("inline_problem_date", true)[0];
                date.Read_Only = false;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
            {
                CustomTextBox name = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_name", true)[0];
                name.Read_Only = false;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
            {
                CustomTextBox probcod = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true)[0];
                probcod.Read_Only = false;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
            {
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true)[0];
                probdesc.Read_Only = false;
            }
            #endregion Inline Problem Form
        }
        #endregion Set form state

        private void loadVerextComboBox() // Load ComboBox item of Verext
        {
            this.cbVerext.ClearItem();
            foreach (Istab verext in this.main_form.data_resource.LIST_VEREXT)
            {
                this.cbVerext.AddItem(new ComboboxItem(verext.typcod + " - " + verext.typdes_th, 0, verext.typcod));
            }
        }

        #region DataGridView Event Handler
        private void ManageDataGridRow()
        {
            if (this.problem != null)
            {
                int problem_count = (this.is_problem_im_only ? this.problem_im_only.Count : this.problem.Count);
                int dgv_row_count = this.dgvProblem.Rows.Count;

                for (int i = dgv_row_count - 1; i > problem_count; i--)
                {
                    this.dgvProblem.Rows.RemoveAt(i);
                }

                if (this.dgvProblem.ClientSize.Height > (problem_count * 25))
                {
                    int line_to_fill = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((this.dgvProblem.ClientSize.Height - (problem_count * 25)) / 25)));
                    for (int i = 0; i < line_to_fill; i++)
                    {
                        int r = this.dgvProblem.Rows.Add();
                        this.dgvProblem.Rows[r].Height = 25;
                    }
                }
                // add last row for prevent error in case total problem row more than datagridview space
                int rr = this.dgvProblem.Rows.Add();
                this.dgvProblem.Rows[rr].Height = 25;
            }
            this.dgvProblem.Columns[4].Width = this.dgvProblem.ClientSize.Width - (this.dgvProblem.Columns[1].Width + this.dgvProblem.Columns[2].Width + this.dgvProblem.Columns[3].Width + SystemInformation.VerticalScrollBarWidth + 3);
        }

        private void dgvProblem_Resize(object sender, EventArgs e)
        {
            if (this.dgvProblem.Rows.Count > 0)
            {
                this.ManageDataGridRow();
                this.setPositionInlineProblemForm(this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex]);
            }
        }

        private void dgvProblem_Paint(object sender, PaintEventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM || this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                DataGridViewRow row = ((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex];
                Rectangle row_rect = ((DataGridView)sender).GetRowDisplayRectangle(row.Index, false);

                using (Pen p = new Pen(Color.Red))
                {
                    if (((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Tag is Problem)
                    {
                        if (((Problem)((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Tag).intention.to_do == DataRowIntention.TO_DO.DELETE)
                        {
                            for (int i = row_rect.Left - 16; i < row_rect.Right; i += 8)
                            {
                                e.Graphics.DrawLine(p, i, row_rect.Bottom - 2, i + 23, row_rect.Top);
                            }
                        }
                    }
                    e.Graphics.DrawLine(p, row_rect.X, row_rect.Y, row_rect.X + row_rect.Width, row_rect.Y);
                    e.Graphics.DrawLine(p, row_rect.X, row_rect.Y + row_rect.Height - 1, row_rect.X + row_rect.Width, row_rect.Y + row_rect.Height - 1);
                }
            }
        }

        private void dgvProblem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag is Problem)
            {
                DataGridViewRow row = ((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex];
                //((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).intention.to_do = DataRowIntention.TO_DO.EDIT;
                this.showInlineProblemForm(row, e.ColumnIndex);
            }
            else
            {
                int problem_count = (this.is_problem_im_only ? this.problem_im_only.Count : this.problem.Count);
                this.dgvProblem.Rows[problem_count].Cells[1].Selected = true;
                this.showInlineProblemForm(this.dgvProblem.Rows[problem_count]);
            }
        }

        private void showProblemContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.toolStripItem.PerformClick();
                    int current_over_row = this.dgvProblem.HitTest(e.X, e.Y).RowIndex;
                    if (current_over_row < 0)
                        return;

                    this.dgvProblem.Rows[current_over_row].Cells[1].Selected = true;

                    ContextMenu m = new ContextMenu();
                    if (this.dgvProblem.Rows[current_over_row].Tag is Problem)
                    {
                        MenuItem m_add = new MenuItem("Add data <Alt+A>");
                        m_add.Click += delegate
                        {
                            Problem pattern = (this.dgvProblem.CurrentCell != null && this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag is Problem ? (Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag : null);
                            int problem_count = (this.is_problem_im_only ? this.problem_im_only.Count : this.problem.Count);
                            this.dgvProblem.Rows[problem_count].Cells[1].Selected = true;
                            this.showInlineProblemForm(this.dgvProblem.Rows[problem_count], 1, pattern);
                        };
                        m.MenuItems.Add(m_add);

                        MenuItem m_edit = new MenuItem("Edit data <Alt+E>");
                        m_edit.Click += delegate
                        {
                            this.showInlineProblemForm(this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex]);
                        };
                        m.MenuItems.Add(m_edit);

                        MenuItem m_delete = new MenuItem("Delete data <Alt+D>");
                        m_delete.Enabled = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_ADMIN && ((Problem)this.dgvProblem.Rows[current_over_row].Tag).probcod == "RG" ? false : true);
                        m_delete.Click += delegate(object o, EventArgs ea)
                        {
                            this.deleteProblemData();
                        };
                        m.MenuItems.Add(m_delete);
                    }
                    else
                    {
                        MenuItem m_add = new MenuItem("Add data <Alt+A>");
                        m_add.Click += delegate(object o, EventArgs ea)
                        {
                            int problem_count = (this.is_problem_im_only ? this.problem_im_only.Count : this.problem.Count);
                            this.dgvProblem.Rows[problem_count].Cells[1].Selected = true;
                            this.showInlineProblemForm(this.dgvProblem.Rows[problem_count]);
                        };
                        m.MenuItems.Add(m_add);
                    }
                    m.Show(this.dgvProblem, new Point(e.X, e.Y));
                }
            }
        }
        #endregion DataGridView Event Handler

        #region Manage Problem data
        private string GetMachineCode(string probDesc) // Get Machine Code (string) in probdesc
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
                    int parse1_len = (probDesc.Length >= dash_2nd + 3 ? 2 : (probDesc.Length == dash_2nd + 2 ? 1 : 0));
                    bool parse1_result = Int32.TryParse(probDesc.Substring(dash_2nd + 1, parse1_len), out parse1);

                    if (parse1_result == true)
                    {
                        return probDesc.Substring(0, dash_2nd + parse1_len + 1);
                    }
                    else
                    {
                        if (dash_3rd > 0)
                        {
                            // tryparse to int for stage 2 (2 digit after the third dash)
                            int parse2;
                            int parse2_len = (probDesc.Length >= dash_3rd + 3 ? 2 : (probDesc.Length == dash_3rd + 2 ? 1 : 0));
                            bool parse2_result = Int32.TryParse(probDesc.Substring(dash_3rd + 1, parse2_len), out parse2);
                            if (parse2_result == true)
                            {
                                return probDesc.Substring(0, dash_3rd + parse2_len + 1);
                            }
                        }
                    }
                }
            }

            // return empty string if not match for "Machine Code"
            return "";
        }

        private void showInlineProblemForm(DataGridViewRow row, int column_index = 1, Problem pattern = null)
        {
            this.dgvProblem.Enabled = false;

            Problem prob = new Problem();

            CustomDateTimePicker date = new CustomDateTimePicker();
            date.Name = "inline_problem_date";
            date.BringToFront();
            date.Read_Only = false;
            date.BorderStyle = BorderStyle.None;
            date.textBox1.GotFocus += delegate
            {
                this.current_focused_control = date;
                this.toolStripInfo.Text = this.toolTip1.GetToolTip(date);
            };
            toolTip1.SetToolTip(date, "<F6> = Show Calendar");
            CustomTextBox name = new CustomTextBox();
            name.Name = "inline_problem_name";
            name.BringToFront();
            name.Read_Only = false;
            name.BorderStyle = BorderStyle.None;
            name.textBox1.GotFocus += delegate
            {
                this.current_focused_control = name;
                this.toolStripInfo.Text = this.toolTip1.GetToolTip(name);
            };
            CustomBrowseField probcod = new CustomBrowseField();
            probcod.Name = "inline_problem_probcod";
            probcod.BringToFront();
            probcod._ReadOnly = false;
            probcod._MaxLength = 2;
            probcod.BorderStyle = BorderStyle.None;
            toolTip1.SetToolTip(probcod, "<F6> = Show Problem Code");
            probcod._btnBrowse.Click += delegate
            {
                IstabList co = new IstabList(this.main_form, probcod._Text, Istab.TABTYP.PROBLEM_CODE);
                if (co.ShowDialog() == DialogResult.OK)
                {
                    probcod._Text = co.istab.typcod;
                }
            };
            CustomTextBoxMaskedWithLabel probdesc = new CustomTextBoxMaskedWithLabel();
            probdesc.Name = "inline_problem_probdesc";
            probdesc.BringToFront();
            probdesc.BorderStyle = BorderStyle.None;
            probdesc.txtEdit.Enter += delegate
            {

            };
            probdesc.txtEdit.GotFocus += delegate
            {
                if (this.main_form.data_resource.LIST_PROBLEM_CODE.Find(t => t.typcod == probcod._Text) == null)
                {
                    SendKeys.Send("+{TAB}");
                    SendKeys.Send("{F6}");
                    return;
                }
                this.current_focused_control = probdesc;
                this.toolStripInfo.Text = this.toolTip1.GetToolTip(probdesc);
            };

            if (row.Tag is Problem) // edit existing problem
            {
                this.FormEditItem();
                prob = (Problem)row.Tag;
            }
            else // add new problem
            {
                this.FormAddItem();
                if (pattern != null)
                    prob = (pattern.probcod == "RG" && this.G.loged_in_user_level < GlobalVar.USER_LEVEL_ADMIN ? prob : pattern);
            }

            this.dgvProblem.Parent.Controls.Add(date);
            this.dgvProblem.Parent.Controls.Add(name);
            this.dgvProblem.Parent.Controls.Add(probcod);
            this.dgvProblem.Parent.Controls.Add(probdesc);
            this.dgvProblem.SendToBack();

            this.setPositionInlineProblemForm(row);

            // specify value in each field
            date.TextsMysql = (row.Tag is Problem ? prob.date : DateTime.Now.ToMysqlDate());
            name.Texts = prob.name;
            probcod._Text = prob.probcod;
            if (prob.probcod == "RG" && this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_ADMIN)
            {
                probcod.Visible = false;
                probdesc.StaticText = this.GetMachineCode(prob.probdesc);
                probdesc.EditableText = prob.probdesc.Substring(probdesc.StaticText.Length, prob.probdesc.Length - (probdesc.StaticText.Length)).Trim();
            }
            else
            {
                probdesc.StaticText = "";
                probdesc.EditableText = prob.probdesc;
            }

            if (column_index == 1)
            {
                date.Focus();
            }
            else if (column_index == 2)
            {
                name.Focus();
            }
            else if (column_index == 3)
            {
                if (probcod.Visible)
                    probcod.Focus();

                if (!probcod.Visible)
                    probdesc.Focus();
            }
            else if (column_index == 4)
            {
                probdesc.Focus();
            }
        }

        private void setPositionInlineProblemForm(DataGridViewRow row)
        {
            Control[] ct_date = this.dgvProblem.Parent.Controls.Find("inline_problem_date", true);
            if (ct_date.Length > 0)
            {
                Rectangle rect_date = this.dgvProblem.GetCellDisplayRectangle(1, row.Index, false);
                CustomDateTimePicker date = (CustomDateTimePicker)ct_date[0];
                date.SetBounds(rect_date.X + 1, rect_date.Y + 1, rect_date.Width, rect_date.Height - 2);
            }

            Control[] ct_name = this.dgvProblem.Parent.Controls.Find("inline_problem_name", true);
            if (ct_name.Length > 0)
            {
                Rectangle rect_name = this.dgvProblem.GetCellDisplayRectangle(2, row.Index, false);
                CustomTextBox name = (CustomTextBox)ct_name[0];
                name.SetBounds(rect_name.X + 1, rect_name.Y + 1, rect_name.Width - 2, rect_name.Height - 2);
            }

            Control[] ct_probcod = this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true);
            if (ct_probcod.Length > 0)
            {
                Rectangle rect_probcod = this.dgvProblem.GetCellDisplayRectangle(3, row.Index, false);
                CustomBrowseField probcod = (CustomBrowseField)ct_probcod[0];
                //probcod.SetBounds(rect_probcod.X + 1, rect_probcod.Y + 1, rect_probcod.Width - 2, rect_probcod.Height - 2);
                probcod.SetBounds(rect_probcod.X, rect_probcod.Y + 1, rect_probcod.Width - 3, rect_probcod.Height - 2);
            }

            Control[] ct_probdesc = this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true);
            if (ct_probdesc.Length > 0)
            {
                Rectangle rect_probdesc = this.dgvProblem.GetCellDisplayRectangle(4, row.Index, false);
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)ct_probdesc[0];
                probdesc.SetBounds(rect_probdesc.X + 1, rect_probdesc.Y + 1, rect_probdesc.Width - 2, rect_probdesc.Height - 2);
            }
        }

        private void clearInlineProblemForm(Problem focused_problem_row = null)
        {
            if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM || this.form_mode == FORM_MODE.PROCESSING)
            {
                int focus_row_index = this.dgvProblem.CurrentCell.RowIndex;
                this.FormReadItem();
                if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
                    this.dgvProblem.Parent.Controls.RemoveByKey("inline_problem_date");
                if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
                    this.dgvProblem.Parent.Controls.RemoveByKey("inline_problem_name");
                if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
                    this.dgvProblem.Parent.Controls.RemoveByKey("inline_problem_probcod");
                if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
                    this.dgvProblem.Parent.Controls.RemoveByKey("inline_problem_probdesc");

                this.dgvProblem.Rows[focus_row_index].Cells[1].Selected = true;
                this.current_focused_control = null;
            }
        }

        private void SubmitAddProblem()
        {
            bool submit_success = false;
            string err_msg = "";

            string prob_date = "";
            string prob_name = "";
            string prob_code = "";
            string prob_desc = "";
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
            {
                CustomDateTimePicker date = (CustomDateTimePicker)this.dgvProblem.Parent.Controls.Find("inline_problem_date", true)[0];
                prob_date = date.TextsMysql;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
            {
                CustomTextBox name = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_name", true)[0];
                prob_name = name.Texts;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
            {
                CustomBrowseField probcod = (CustomBrowseField)this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true)[0];
                prob_code = probcod._Text;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
            {
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true)[0];
                prob_desc = probdesc.Texts;
            }
            string users_name = this.main_form.G.loged_in_user_name;
            string serial_sernum = this.serial.sernum;

            // in case of record problem code as "RG"
            if (prob_code == "RG" && this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_ADMIN)
            {
                MessageAlert.Show("Your permission is not allowed to record problem code as \"RG\"", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return;
            }

            FORM_MODE before_submit_mode = this.form_mode;
            this.FormProcessing();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"date\":\"" + prob_date + "\",";
                json_data += "\"name\":\"" + prob_name.cleanString() + "\",";
                json_data += "\"probcod\":\"" + prob_code.cleanString() + "\",";
                json_data += "\"probdesc\":\"" + prob_desc.cleanString() + "\",";
                json_data += "\"users_name\":\"" + users_name.cleanString() + "\",";
                json_data += "\"serial_sernum\":\"" + serial_sernum.cleanString() + "\"}";

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
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (submit_success)
                {
                    this.clearInlineProblemForm();
                    this.fillInDatagrid();
                    if (before_submit_mode == FORM_MODE.ADD_ITEM && this.dgvProblem.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is Problem).Count<DataGridViewRow>() > 0)
                    {
                        this.dgvProblem.Rows[this.dgvProblem.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is Problem).Count<DataGridViewRow>()].Cells[1].Selected = true;
                        this.showInlineProblemForm(this.dgvProblem.Rows[this.dgvProblem.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is Problem).Count<DataGridViewRow>()]);
                    }
                }
                else
                {
                    this.FormAddItem();
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            };
            worker.RunWorkerAsync();
        }

        private void SubmitEditProblem()
        {
            bool submit_success = false;
            string err_msg = "";

            int prob_id = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).id;
            string prob_date = "";
            string prob_name = "";
            string prob_code = "";
            string prob_desc = "";
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_date", true).Length > 0)
            {
                CustomDateTimePicker date = (CustomDateTimePicker)this.dgvProblem.Parent.Controls.Find("inline_problem_date", true)[0];
                prob_date = date.TextsMysql;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_name", true).Length > 0)
            {
                CustomTextBox name = (CustomTextBox)this.dgvProblem.Parent.Controls.Find("inline_problem_name", true)[0];
                prob_name = name.Texts;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true).Length > 0)
            {
                CustomBrowseField probcod = (CustomBrowseField)this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true)[0];
                prob_code = probcod._Text;
            }
            if (this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true).Length > 0)
            {
                CustomTextBoxMaskedWithLabel probdesc = (CustomTextBoxMaskedWithLabel)this.dgvProblem.Parent.Controls.Find("inline_problem_probdesc", true)[0];
                prob_desc = probdesc.Texts;
            }
            string users_name = this.main_form.G.loged_in_user_name;
            string serial_sernum = this.serial.sernum;

            // in case of changing problem code from other to "RG"
            if (((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probcod != "RG" && prob_code == "RG" && this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_ADMIN)
            {
                MessageAlert.Show("Your permission is not allowed to change problem code to \"RG\"", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return;
            }

            this.FormProcessing();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"id\":" + prob_id + ",";
                json_data += "\"date\":\"" + prob_date.cleanString() + "\",";
                json_data += "\"name\":\"" + prob_name.cleanString() + "\",";
                json_data += "\"probcod\":\"" + prob_code.cleanString() + "\",";
                json_data += "\"probdesc\":\"" + prob_desc.cleanString() + "\",";
                json_data += "\"users_name\":\"" + users_name.cleanString() + "\",";
                json_data += "\"serial_sernum\":\"" + serial_sernum.cleanString() + "\"}";

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
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (submit_success)
                {
                    this.clearInlineProblemForm();
                    this.fillInDatagrid();
                }
                else
                {
                    this.FormEditItem();
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            };
            worker.RunWorkerAsync();
        }

        private void deleteProblemData()
        {
            if (this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag is Problem)
            {
                if (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_ADMIN && ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).probcod == "RG")
                    return;

                ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).intention.to_do = DataRowIntention.TO_DO.DELETE;
                this.dgvProblem.Refresh(); // call refresh to re-paint the datagridview

                if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.YES_NO, MessageAlertIcons.QUESTION) == DialogResult.Yes)
                {
                    bool delete_success = false;
                    string err_msg = "";
                    this.FormProcessing();

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += delegate
                    {
                        int deleted_id = ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).id;

                        CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "problem/delete&id=" + deleted_id.ToString());
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            delete_success = true;
                            this.loadProblemData();
                        }
                        else
                        {
                            delete_success = false;
                            err_msg = sr.message;
                        }
                    };
                    worker.RunWorkerCompleted += delegate
                    {
                        this.FormReadItem();

                        if (delete_success)
                        {
                            this.fillInDatagrid();
                        }
                        else
                        {
                            MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        }

                    };
                    worker.RunWorkerAsync();
                }
                else
                {
                    ((Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag).intention.to_do = DataRowIntention.TO_DO.READ;
                    this.dgvProblem.Refresh();
                }
            }
        }
        #endregion Manage Problem data

        #region Get Serial data from server
        private void getSerialIDList() // Get serial_id_list
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
                    this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                    this.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : this.problem_not_found);
                    this.ma = sr.ma;
                    this.cloudsrv = sr.cloudsrv;
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
                    this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                    this.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : this.problem_not_found);
                    this.ma = sr.ma;
                    this.cloudsrv = sr.cloudsrv;
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
            this.txtSernum.Texts = this.serial.sernum;
            this.txtVersion.Texts = this.serial.version;
            this.txtArea.Texts = this.serial.area;
            this.lblAreaTypdes.Text = (this.main_form.data_resource.LIST_AREA.Find(t => t.typcod == this.serial.area) != null ? this.main_form.data_resource.LIST_AREA.Find(t => t.typcod == this.serial.area).typdes_th : "");
            this.txtRefnum.Texts = serial.refnum;
            this.txtPrenam.Texts = this.serial.prenam;
            this.txtCompnam.Texts = this.serial.compnam;
            #endregion Fill header data

            #region Fill first tab data
            this.txtAddr01.Texts = this.serial.addr01;
            this.txtAddr02.Texts = this.serial.addr02;
            this.txtAddr03.Texts = this.serial.addr03;
            this.txtZipcod.Texts = this.serial.zipcod;
            this.txtTelnum.Texts = this.serial.telnum;
            this.txtFaxnum.Texts = this.serial.faxnum;
            this.txtContact.Texts = this.serial.contact;
            this.txtPosition.Texts = this.serial.position;
            this.txtOldnum.Texts = this.serial.oldnum;
            this.txtRemark.Texts = serial.remark;
            this.txtBusides.Texts = serial.busides;
            this.txtBusityp.Texts = this.serial.busityp;
            this.lblBusitypTypdes.Text = (this.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.serial.busityp) != null ? this.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.serial.busityp).typdes_th : "");
            this.txtDealer.Texts = this.serial.dealer_dealer;
            this.lblDealer_DealerCompnam.Text = (this.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == this.serial.dealer_dealer) != null ? this.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == this.serial.dealer_dealer).compnam : "");
            this.txtHowknown.Texts = this.serial.howknown;
            this.lblHowknownTypdes.Text = (this.main_form.data_resource.LIST_HOWKNOWN.Find(t => t.typcod == this.serial.howknown) != null ? this.main_form.data_resource.LIST_HOWKNOWN.Find(t => t.typcod == this.serial.howknown).typdes_th : "");
            this.dtPurdat.TextsMysql = this.serial.purdat;
            this.txtUpfree.Texts = this.serial.upfree;
            this.dtExpdat.TextsMysql = this.serial.expdat;
            this.dtManual.TextsMysql = this.serial.manual;
            this.cbVerext.Texts = (this.main_form.data_resource.LIST_VEREXT.Find(t => t.typcod == this.serial.verext) != null ? this.main_form.data_resource.LIST_VEREXT.Find(t => t.typcod == this.serial.verext).typcod + " - " + this.main_form.data_resource.LIST_VEREXT.Find(t => t.typcod == this.serial.verext).typdes_th : "");
            this.dtVerextdat.TextsMysql = this.serial.verextdat;
            this.maDateFrom.Texts = (this.ma.Count > 0 ? this.ma[0].start_date.M2WDate() : "  /  /  ");
            this.maDateTo.Texts = (this.ma.Count > 0 ? this.ma[0].end_date.M2WDate() : "  /  /  ");
            this.maEmail.Texts = (this.ma.Count > 0 ? this.ma[0].email : "");
            this.cloudDateFrom.Texts = (this.cloudsrv.Count > 0 ? this.cloudsrv[0].start_date.M2WDate() : "  /  /  ");
            this.cloudDateTo.Texts = (this.cloudsrv.Count > 0 ? this.cloudsrv[0].end_date.M2WDate() : "  /  /  ");
            this.cloudEmail.Texts = (this.cloudsrv.Count > 0 ? this.cloudsrv[0].email : "");
            if (this.ma.Count > 0)
            {
                this.lblMAExpireWarning.Visible = ((DateTime.Parse(this.ma.First().end_date, CultureInfo.GetCultureInfo("en-US")) - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).TotalDays > 30 ? false : true);
            }
            else
            {
                this.lblMAExpireWarning.Visible = false;
            }
            if (this.cloudsrv.Count > 0)
            {
                this.lblCloudExpireWarning.Visible = ((DateTime.Parse(this.cloudsrv.First().end_date, CultureInfo.GetCultureInfo("en-US")) - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).TotalDays > 30 ? false : true);
            }
            else
            {
                this.lblCloudExpireWarning.Visible = false;
            }
            #endregion Fill first tab data

            #region Fill second tab data
            this.lblTelnum2.Text = this.serial.telnum;
            this.lblExpdat2.pickedDate(this.serial.expdat);
            this.lblContact2.Text = this.serial.contact;
            this.lblUpfree.Text = this.serial.upfree;
            this.fillInDatagrid();
            #endregion Fill second tab data

            #region Fill password data
            this.FillDgvPassword(GetPasswordList(this.serial.sernum));
            #endregion Fill password data
        }

        public void loadProblemData()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "problem/get_for_sn&sn=" + this.serial.sernum);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.problem = (sr.problem.Count > 0 ? sr.problem : this.problem_not_found);
                this.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : this.problem_not_found);
            }
        }

        public void fillInDatagrid()
        {
            int current_row_index = (this.dgvProblem.Rows.Count > 0 ? this.dgvProblem.CurrentCell.RowIndex : 0); // keep current row index first
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
            col1.Width = 98;
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            col2.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            col3.SortMode = DataGridViewColumnSortMode.NotSortable;
            col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvProblem.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "DESC.";
            col4.SortMode = DataGridViewColumnSortMode.NotSortable;
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
                p.intention = new DataRowIntention(DataRowIntention.TO_DO.READ);

                this.dgvProblem.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvProblem.Rows[r].Cells[0].Value = p.id;
                this.dgvProblem.Rows[r].Cells[0].Tag = new DataRowIntention(DataRowIntention.TO_DO.READ);

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
            this.ManageDataGridRow();

            if (current_row_index <= this.dgvProblem.Rows.Count)
            {
                this.dgvProblem.Rows[current_row_index].Cells[1].Selected = true;
            }
            else
            {
                this.dgvProblem.Rows[0].Cells[1].Selected = true;
            }
        }

        #region Manage Serial data
        private void SubmitAddSerial()
        {
            bool submit_success = false;
            string err_msg = "";
            this.FormProcessing();

            if (ValidateSN.Check(this.txtSernum.Texts))
            {
                string json_data = "{\"sernum\":\"" + this.txtSernum.Texts.cleanString() + "\",";
                json_data += "\"oldnum\":\"" + this.txtOldnum.Texts.cleanString() + "\",";
                json_data += "\"version\":\"" + this.txtVersion.Texts.cleanString() + "\",";
                json_data += "\"contact\":\"" + this.txtContact.Texts.cleanString() + "\",";
                json_data += "\"position\":\"" + this.txtPosition.Texts.cleanString() + "\",";
                json_data += "\"prenam\":\"" + this.txtPrenam.Texts.cleanString() + "\",";
                json_data += "\"compnam\":\"" + this.txtCompnam.Texts.cleanString() + "\",";
                json_data += "\"addr01\":\"" + this.txtAddr01.Texts.cleanString() + "\",";
                json_data += "\"addr02\":\"" + this.txtAddr02.Texts.cleanString() + "\",";
                json_data += "\"addr03\":\"" + this.txtAddr03.Texts.cleanString() + "\",";
                json_data += "\"zipcod\":\"" + this.txtZipcod.Texts.cleanString() + "\",";
                json_data += "\"telnum\":\"" + this.txtTelnum.Texts.cleanString() + "\",";
                json_data += "\"faxnum\":\"" + this.txtFaxnum.Texts.cleanString() + "\",";
                json_data += "\"busityp\":\"" + this.txtBusityp.Texts.cleanString() + "\",";
                json_data += "\"busides\":\"" + this.txtBusides.Texts.cleanString() + "\",";
                json_data += "\"purdat\":\"" + this.dtPurdat.TextsMysql + "\",";
                json_data += "\"expdat\":\"" + this.dtExpdat.TextsMysql + "\",";
                json_data += "\"howknown\":\"" + this.txtHowknown.Texts.cleanString() + "\",";
                json_data += "\"area\":\"" + this.txtArea.Texts.cleanString() + "\",";
                json_data += "\"manual\":\"" + this.dtManual.TextsMysql + "\",";
                json_data += "\"upfree\":\"" + this.txtUpfree.Texts.cleanString() + "\",";
                json_data += "\"refnum\":\"" + this.txtRefnum.Texts.cleanString() + "\",";
                json_data += "\"remark\":\"" + this.txtRemark.Texts.cleanString() + "\",";
                json_data += "\"verext\":\"" + ((ComboboxItem)this.cbVerext.comboBox1.SelectedItem).string_value + "\",";
                json_data += "\"verextdat\":\"" + this.dtVerextdat.TextsMysql + "\",";
                json_data += "\"users_id\":\"" + this.main_form.G.loged_in_user_id + "\",";
                json_data += "\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\",";
                json_data += "\"dealer_dealer\":\"" + this.txtDealer.Texts.cleanString() + "\"}";

                this.FormSaving();

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/create_new", json_data);
                    Console.WriteLine(post.data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        submit_success = true;
                        this.getSerial(Convert.ToInt32(sr.message));
                        this.getSerialIDList();
                    }
                    else
                    {
                        submit_success = false;
                        err_msg = sr.message;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (submit_success)
                    {
                        this.FormRead();
                        this.fillSerialInForm();
                        this.toolStripItem.PerformClick();
                        this.showInlineProblemForm(this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex]);
                    }
                    else
                    {
                        this.FormAdd();
                        this.txtSernum.Focus();
                        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                worker.RunWorkerAsync();
            }
            else
            {
                MessageAlert.Show(StringResource.PLEASE_FILL_CORRECT_SN, "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.txtSernum.Focus();
            }
        }

        private void SubmitEditSerial()
        {
            bool submit_success = false;
            string err_msg = "";
            this.FormProcessing();

            string json_data = "{\"id\":" + this.serial.id.ToString() + ",";
            json_data += "\"oldnum\":\"" + this.txtOldnum.Texts.cleanString() + "\",";
            json_data += "\"version\":\"" + this.txtVersion.Texts.cleanString() + "\",";
            json_data += "\"contact\":\"" + this.txtContact.Texts.cleanString() + "\",";
            json_data += "\"position\":\"" + this.txtPosition.Texts.cleanString() + "\",";
            json_data += "\"prenam\":\"" + this.txtPrenam.Texts.cleanString() + "\",";
            json_data += "\"compnam\":\"" + this.txtCompnam.Texts.cleanString() + "\",";
            json_data += "\"addr01\":\"" + this.txtAddr01.Texts.cleanString() + "\",";
            json_data += "\"addr02\":\"" + this.txtAddr02.Texts.cleanString() + "\",";
            json_data += "\"addr03\":\"" + this.txtAddr03.Texts.cleanString() + "\",";
            json_data += "\"zipcod\":\"" + this.txtZipcod.Texts.cleanString() + "\",";
            json_data += "\"telnum\":\"" + this.txtTelnum.Texts.cleanString() + "\",";
            json_data += "\"faxnum\":\"" + this.txtFaxnum.Texts.cleanString() + "\",";
            json_data += "\"busityp\":\"" + this.txtBusityp.Texts.cleanString() + "\",";
            json_data += "\"busides\":\"" + this.txtBusides.Texts.cleanString() + "\",";
            json_data += "\"purdat\":\"" + this.dtPurdat.TextsMysql + "\",";
            json_data += "\"expdat\":\"" + this.dtExpdat.TextsMysql + "\",";
            json_data += "\"howknown\":\"" + this.txtHowknown.Texts.cleanString() + "\",";
            json_data += "\"area\":\"" + this.txtArea.Texts.cleanString() + "\",";
            json_data += "\"manual\":\"" + this.dtManual.TextsMysql + "\",";
            json_data += "\"upfree\":\"" + this.txtUpfree.Texts.cleanString() + "\",";
            json_data += "\"refnum\":\"" + this.txtRefnum.Texts.cleanString() + "\",";
            json_data += "\"remark\":\"" + this.txtRemark.Texts.cleanString() + "\",";
            json_data += "\"verext\":\"" + ((ComboboxItem)this.cbVerext.comboBox1.SelectedItem).string_value + "\",";
            json_data += "\"verextdat\":\"" + this.dtVerextdat.TextsMysql + "\",";
            json_data += "\"users_id\":\"" + this.main_form.G.loged_in_user_id + "\",";
            json_data += "\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\",";
            json_data += "\"dealer_dealer\":\"" + this.txtDealer.Texts.cleanString() + "\"}";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/update", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    submit_success = true;
                    this.getSerial(this.serial.id);
                    this.getSerialIDList();
                }
                else
                {
                    submit_success = false;
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (submit_success)
                {
                    this.fillSerialInForm();
                    this.FormRead();
                }
                else
                {
                    this.FormEdit();
                    this.txtVersion.Focus();
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            };
            worker.RunWorkerAsync();
        }

        private void findSerial()
        {
            if (this.find_id != this.serial.id)
            {
                this.FormProcessing();
                BackgroundWorker workerFind = new BackgroundWorker();
                workerFind.DoWork += delegate
                {
                    this.getSerial(this.find_id);
                };
                workerFind.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                    this.FormRead();
                };
                workerFind.RunWorkerAsync();
            }
        }
        #endregion Manage Serial data

        #region ToolStrip click event handler
        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            this.FormAdd();
            this.EditControlBlank();
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.FormEdit();
        }

        private void toolStripDelete_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.YES_NO, MessageAlertIcons.QUESTION) == DialogResult.Yes)
            {
                this.FormProcessing();

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
                    this.FormRead();
                };
                workerDeleteSerial.RunWorkerAsync();
            }
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.fillInDatagrid();
                this.FormRead();
            }
            else if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.clearInlineProblemForm();
            }
            else
            {
                this.FormRead();
                this.fillSerialInForm();
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.ADD)
            {
                this.SubmitAddSerial();
            }
            else if (this.form_mode == FORM_MODE.EDIT)
            {
                this.SubmitEditSerial();
            }
            else if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.fillInDatagrid();
                this.FormRead();
            }
            else if (this.form_mode == FORM_MODE.ADD_ITEM)
            {
                Control[] ct = this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true);
                if (ct.Length > 0)
                {
                    CustomBrowseField probcod = (CustomBrowseField)ct[0];
                    if (this.main_form.data_resource.LIST_PROBLEM_CODE.Find(t => t.typcod == probcod._Text) == null) // if probcod is invalid
                    {
                        probcod.Focus();
                        SendKeys.Send("{F6}");
                        return;
                    }
                }
                this.SubmitAddProblem();
            }
            else if (this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                Control[] ct = this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true);
                if (ct.Length > 0)
                {
                    CustomBrowseField probcod = (CustomBrowseField)ct[0];
                    if (this.main_form.data_resource.LIST_PROBLEM_CODE.Find(t => t.typcod == probcod._Text) == null) // if probcod is invalid
                    {
                        probcod.Focus();
                        SendKeys.Send("{F6}");
                        return;
                    }
                }
                this.SubmitEditProblem();
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
            this.FormReadItem();
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
                #region keep spy_log (no need response)
                string json_data = "{\"users_name\":\"" + this.G.loged_in_user_name + "\",";
                json_data += "\"sernum\":\"" + box.mskSearchKey.Text.cleanString() + "\",";
                json_data += "\"compnam\":\"\"}";
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "spylog/create", json_data);
                #endregion keep spy_log (no need response)

                this.find_sernum = box.mskSearchKey.Text;
                this.find_type = FIND_TYPE.SERNUM;
                this.FormProcessing();
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
                this.FormProcessing();
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
                this.FormProcessing();
                this.find_id = wind.selected_id;
                BackgroundWorker selectFromInquiryWorker = new BackgroundWorker();
                selectFromInquiryWorker.DoWork += new DoWorkEventHandler(this.selectFromInquiryWorker_DoWork);
                selectFromInquiryWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.selectFromInquiryWorker_Complete);
                selectFromInquiryWorker.RunWorkerAsync();
            }
        }

        private void selectFromInquiryWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.getSerial(this.find_id);
        }

        private void selectFromInquiryWorker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            this.fillSerialInForm();
            this.FormRead();
        }

        private void searchContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSerialBox box = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.CONTACT);
            box.txtSearchKey.Text = this.find_contact;
            if (box.ShowDialog() == DialogResult.OK)
            {
                this.find_contact = box.txtSearchKey.Text;
                this.find_type = FIND_TYPE.CONTACT;
                this.FormProcessing();
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
                #region keep spy_log (no need response)
                string json_data = "{\"users_name\":\"" + this.G.loged_in_user_name + "\",";
                json_data += "\"compnam\":\"" + box.txtSearchKey.Text.cleanString() + "\",";
                json_data += "\"sernum\":\"\"}";
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "spylog/create", json_data);
                #endregion keep spy_log (no need response)

                this.find_company = box.txtSearchKey.Text;
                this.find_type = FIND_TYPE.COMPANY;
                this.FormProcessing();
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
                this.FormProcessing();
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
                this.FormProcessing();
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
                this.FormProcessing();
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
                this.FormProcessing();
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
            this.FormRead();
        }
        #endregion find by criteria

        private void toolStripReload_Click(object sender, EventArgs e)
        {
            this.FormProcessing();
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(this.workerLoadCurrentSN_Dowork);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerLoadSN_Complete);
            work.RunWorkerAsync();
        }
        #endregion toolStrip

        #region Browse button
        private void btnBrowseBusityp_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.main_form, this.txtBusityp.Text, Istab.TABTYP.BUSITYP);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtBusityp.Texts = wind.istab.typcod;
                this.lblBusitypTypdes.Text = wind.istab.typdes_th;
            }
            this.txtBusityp.textBox1.Focus();
        }

        private void btnBrowseArea_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.main_form, this.txtArea.Text, Istab.TABTYP.AREA);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtArea.Texts = wind.istab.typcod;
                this.lblAreaTypdes.Text = wind.istab.typdes_th;
            }
            this.txtArea.textBox1.Focus();
        }

        private void btnBrowseHowknown_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.main_form, this.txtHowknown.Text, Istab.TABTYP.HOWKNOWN);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtHowknown.Texts = wind.istab.typcod;
                this.lblHowknownTypdes.Text = wind.istab.typdes_th;
            }
            this.txtHowknown.textBox1.Focus();
        }

        private void btnBrowseDealer_Click(object sender, EventArgs e)
        {
            DealerList wind = new DealerList(this, this.txtDealer.Texts);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtDealer.Texts = wind.dealer.dealer;
                this.lblDealer_DealerCompnam.Text = wind.dealer.compnam;
            }
            this.txtDealer.textBox1.Focus();
        }
        #endregion Browse button

        private void DisableChangeTabWhileAddEdit(object sender, TabControlCancelEventArgs e) // Prevent change tab while Add,Edit,ReadItem,AddItem,EditItem
        {
            if (this.form_mode != FORM_MODE.READ)
            {
                e.Cancel = true;
                if (this.current_focused_control != null)
                {
                    this.current_focused_control.Focus();
                }
                else
                {
                    this.txtDummy.Focus();
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
                this.FormProcessing();
                BackgroundWorker workerAfterLostRenew = new BackgroundWorker();
                workerAfterLostRenew.DoWork += delegate
                {
                    this.getSerialIDList();
                    this.getSerial(this.serial.id);
                };
                workerAfterLostRenew.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                    this.FormRead();
                };
                workerAfterLostRenew.RunWorkerAsync();
            }
            else
            {
                this.FormRead();
            }
        }

        private void btnSwithToRefnum_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ)
            {
                if (this.serial.refnum.Replace("-", "").Replace(" ", "").Length > 0)
                {
                    bool founded_sn = false;
                    this.FormProcessing();
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
                            this.FormRead();
                        }
                        else
                        {
                            this.FormRead();
                            MessageAlert.Show("Reference S/N not found", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        }


                    };
                    workerSwitchRefnum.RunWorkerAsync();
                }
            }
        }

        private void toolStripGenSN_Click(object sender, EventArgs e)
        {
            GenerateSNForm wind = new GenerateSNForm(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.FormProcessing();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    this.getSerialIDList();
                };
                worker.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                    this.FormRead();
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnCD_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show("Generate \"CD training date\"", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                this.FormProcessing();
                bool post_success = false;

                BackgroundWorker workerCD = new BackgroundWorker();
                workerCD.DoWork += delegate
                {
                    string json_data = "{\"id\":" + this.serial.id.ToString() + ",";
                    json_data += "\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\"}";
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
                        this.dtExpdat.TextsMysql = this.serial.expdat;
                        this.lblExpdat2.pickedDate(this.serial.expdat);
                        this.FormRead();
                    }
                    else
                    {
                        this.FormRead();
                    }
                };

                workerCD.RunWorkerAsync();
            }
            else
            {
                this.FormRead();
            }
        }

        private void btnUPNewRwt_Click(object sender, EventArgs e)
        {
            UpNewRwtLineForm wind = new UpNewRwtLineForm(this, UpNewRwtLineForm.DIALOG_TYPE.UP_NEWRWT);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.fillSerialInForm();
                this.FormRead();
            }
            else
            {
                this.FormRead();
            }
        }

        private void btnUPNewRwtJob_Click(object sender, EventArgs e)
        {
            UpNewRwtLineForm wind = new UpNewRwtLineForm(this, UpNewRwtLineForm.DIALOG_TYPE.UP_NEWRWT_JOB);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.fillSerialInForm();
                this.FormRead();
            }
            else
            {
                this.FormRead();
            }
        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show("Generate \"Update\" line", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                bool post_success = false;
                this.FormProcessing();

                BackgroundWorker workerUp = new BackgroundWorker();
                workerUp.DoWork += delegate
                {
                    string json_data = "{\"id\":" + this.serial.id.ToString() + ",";
                    json_data += "\"users_name\":\"" + this.main_form.G.loged_in_user_name + "\"}";

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
                        this.FormRead();
                    }
                    else
                    {
                        this.FormRead();
                    }
                };

                workerUp.RunWorkerAsync();
            }
            else
            {
                this.FormRead();
            }
        }

        private void toolStripUpgrade_Click(object sender, EventArgs e)
        {
            UpgradeProgramForm wind = new UpgradeProgramForm(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtSernum.Texts = this.serial.sernum;
                this.txtVersion.Texts = this.serial.version;
                this.dtExpdat.TextsMysql = this.serial.expdat;
                this.lblExpdat2.pickedDate(this.serial.expdat);
                //this.lblVerext.Text = this.serial.verext.GetVerextSelectedString(this.cbVerext);
                this.dtVerextdat.TextsMysql = this.serial.verextdat;
                this.fillInDatagrid();
                this.FormRead();
            }
            else
            {
                this.FormRead();
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
                this.FormProcessing();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    this.getSerialIDList();
                    this.getSerial(this.serial.id);
                };
                worker.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                    this.FormRead();
                };
                worker.RunWorkerAsync();
            }
            else
            {
                this.FormRead();
            }
        }

        private void toolStripImport_Click(object sender, EventArgs e)
        {
            ImportListForm wind = new ImportListForm(this);
            wind.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab && this.form_mode == FORM_MODE.READ)
            {
                DataInfo data_info = new DataInfo();

                int max_id = 0;
                foreach (Serial s in this.serial_id_list)
                {
                    if (s.id > max_id)
                    {
                        max_id = s.id;
                    }
                }

                data_info.lblDataTable.Text = "Serial";
                data_info.lblExpression.Text = (this.sortMode == SORT_SN ? this.sortMode : this.sortMode + "+sernum");
                data_info.lblRecBy.Text = this.serial.users_name;
                data_info.lblRecDate.pickedDate(this.serial.chgdat);
                data_info.lblRecNo.Text = this.serial.id.ToString();
                data_info.lblTotalRec.Text = max_id.ToString();
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
            if (keyData == (Keys.Alt | Keys.A))
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    Problem pattern = (this.dgvProblem.CurrentCell != null && this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag is Problem ? (Problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag : null);
                    int problem_count = (this.is_problem_im_only ? this.problem_im_only.Count : this.problem.Count);
                    this.dgvProblem.Rows[problem_count].Cells[1].Selected = true;
                    this.showInlineProblemForm(this.dgvProblem.Rows[problem_count], 1, pattern);
                    return true;
                }
                else if (this.form_mode == FORM_MODE.READ)
                {
                    this.toolStripAdd.PerformClick();
                    return true;
                }
            }
            if (keyData == (Keys.Alt | Keys.E))
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    if (this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Tag is Problem)
                    {
                        this.showInlineProblemForm(this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex]);
                        return true;
                    }
                }
                else
                {
                    this.toolStripEdit.PerformClick();
                    return true;
                }
            }
            if (keyData == Keys.Enter && (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT || this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM))
            {
                if (this.current_focused_control == this.dtVerextdat || this.current_focused_control.Name == "inline_problem_probdesc")
                {
                    this.toolStripSave.PerformClick();
                    return true;
                }

                SendKeys.Send("{TAB}");
                return true;
            }
            if (keyData == Keys.Escape)
            {
                //if (!(this.current_focused_control is CustomDateTimePicker)) // if current_focused_control is not CustomDateTimePicker
                //{
                //    if (!(this.current_focused_control is CustomComboBox)) // and if current_focused_control is not CustomComboBox
                //    {
                //        this.toolStripStop.PerformClick();
                //        this.current_focused_control = null;
                //        return true;
                //    }
                //    else
                //    {
                //        if (!((CustomComboBox)this.current_focused_control).item_shown) // if CustomComboBox is currently not showing items.
                //        {
                //            this.toolStripStop.PerformClick();
                //            this.current_focused_control = null;
                //            return true;
                //        }
                //        else // then if CustomComboBox is currently showing calendar just close the items portion.
                //        {
                //            SendKeys.Send("{F4}");
                //            return true;
                //        }
                //    }
                //}
                //else
                //{
                //    if (!((CustomDateTimePicker)this.current_focused_control).calendar_shown) // if CustomDateTimePicker is currently not showing calendar.
                //    {
                //        this.toolStripStop.PerformClick();
                //        this.current_focused_control = null;
                //        return true;
                //    }
                //    // then if CustomDateTimePicker is currently showing calendar just close the calendar.
                //}
                //if (this.dtPurdat.dateTimePicker1.Focused || this.dtManual.dateTimePicker1.Focused || this.dtExpdat.dateTimePicker1.Focused || this.dtVerextdat.dateTimePicker1.Focused || (this.cbVerext.comboBox1.Focused && this.cbVerext.comboBox1.DroppedDown))
                if (this.cbVerext.comboBox1.Focused && this.cbVerext.comboBox1.DroppedDown)
                {
                    SendKeys.Send("{F4}");
                    return true;
                }
                this.toolStripStop.PerformClick();
                return true;
            }
            if (keyData == Keys.F6)
            {
                if (this.current_focused_control != null)
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
                    else if (this.current_focused_control == this.txtDealer)
                    {
                        this.btnBrowseDealer.PerformClick();
                        return true;
                    }
                    else if (this.current_focused_control == this.txtHowknown)
                    {
                        this.btnBrowseHowknown.PerformClick();
                        return true;
                    }
                    else if (this.current_focused_control == this.dtPurdat)
                    {
                        this.dtPurdat.dateTimePicker1.Focus();
                        SendKeys.Send("{F4}");
                        return true;
                    }
                    else if (this.current_focused_control == this.dtExpdat)
                    {
                        this.dtExpdat.dateTimePicker1.Focus();
                        SendKeys.Send("{F4}");
                        return true;
                    }
                    else if (this.current_focused_control == this.dtManual)
                    {
                        this.dtManual.dateTimePicker1.Focus();
                        SendKeys.Send("{F4}");
                        return true;
                    }
                    else if (this.current_focused_control == this.dtVerextdat)
                    {
                        this.dtVerextdat.dateTimePicker1.Focus();
                        SendKeys.Send("{F4}");
                        return true;
                    }
                    else if (this.current_focused_control == this.cbVerext)
                    {
                        SendKeys.Send("{F4}");
                        return true;
                    }
                    else if (this.current_focused_control.Name == "inline_problem_date" && (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM))
                    {
                        Control[] ct = this.dgvProblem.Parent.Controls.Find("inline_problem_date", true);
                        if (ct.Length > 0)
                        {
                            CustomDateTimePicker dt = (CustomDateTimePicker)ct[0];
                            dt.dateTimePicker1.Focus();
                            SendKeys.Send("{F4}");
                            return true;
                        }
                    }
                    else if (this.current_focused_control.Name == "inline_problem_probcod" && (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM))
                    {
                        Control[] ct = this.dgvProblem.Parent.Controls.Find("inline_problem_probcod", true);
                        if (ct.Length > 0)
                        {
                            CustomTextBox probcod = (CustomTextBox)ct[0];
                            probcod.Focus();
                            IstabList wind = new IstabList(this.main_form, probcod.Texts, Istab.TABTYP.PROBLEM_CODE);
                            if (wind.ShowDialog() == DialogResult.OK)
                            {
                                probcod.Texts = wind.istab.typcod;
                                SendKeys.Send("{TAB}");
                            }
                            return true;
                        }
                    }
                }
            }
            if (keyData == Keys.PageUp)
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.toolStripPrevious.PerformClick();
                    return true;
                }
            }
            if (keyData == Keys.PageDown)
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.toolStripNext.PerformClick();
                    return true;
                }
            }
            if (keyData == (Keys.Control | Keys.Home))
            {
                this.toolStripFirst.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.End))
            {
                this.toolStripLast.PerformClick();
                return true;
            }
            if (keyData == Keys.F3)
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.tabControl1.SelectedTab = this.tabPage1;
                    return true;
                }
            }
            if (keyData == Keys.F4)
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.tabControl1.SelectedTab = this.tabPage2;
                    return true;
                }
            }
            if (keyData == Keys.F5)
            {
                this.toolStripReload.PerformClick();
                return true;
            }
            if (keyData == Keys.F8)
            {
                this.toolStripItem.PerformClick();
                return true;
            }
            if (keyData == Keys.F9)
            {
                this.toolStripSave.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D))
            {
                if (this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.deleteProblemData();
                    return true;
                }
                else
                {
                    this.toolStripDelete.PerformClick();
                    return true;
                }
            }
            if (keyData == (Keys.Control | Keys.L))
            {
                this.toolStripInquiryAll.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.L))
            {
                this.toolStripInquiryRest.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.S))
            {
                this.toolStripSearchSN.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D2))
            {
                this.toolStripSearchContact.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D3))
            {
                this.toolStripSearchCompany.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D4))
            {
                this.toolStripSearchDealer.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D5))
            {
                this.toolStripSearchOldnum.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D6))
            {
                this.toolStripSearchBusityp.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D7))
            {
                this.toolStripSearchArea.PerformClick();
                return true;
            }
            if (keyData == Keys.F2)
            {
                this.btnSupportNote.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.Alt | Keys.M))
            {
                //MessageBox.Show("Control + Alt. + M");
                this.toolStripInquiryMA.PerformClick();
            }
            if (keyData == (Keys.Control | Keys.Alt | Keys.C))
            {
                //MessageBox.Show("Control + Alt. + C");
                this.toolStripInquiryCloud.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSupportNote_Click(object sender, EventArgs e)
        {
            this.btnSupportNote.Enabled = false;
            if (this.main_form.supportnote_wind == null)
            {
                SupportNoteWindow wind = new SupportNoteWindow(this, this.serial, GetPasswordList(this.serial.sernum));
                wind.Text += " (" + this.main_form.G.loged_in_user_level.ToUserLevelString() + " : " + this.main_form.G.loged_in_user_name + " - " + this.main_form.G.loged_in_user_realname + ")";
                wind.MdiParent = this.main_form;
                this.main_form.supportnote_wind = wind;
                wind.Show();
            }
            else
            {
                //this.main_form.supportnote_wind.serial = this.serial;
                //this.main_form.supportnote_wind.password_list = GetPasswordList(this.serial.sernum);
                //this.main_form.supportnote_wind.BeginDuration();
                //this.main_form.supportnote_wind.Activate();
                this.main_form.supportnote_wind.CrossingCall(this.serial, GetPasswordList(this.serial.sernum));
            }
        }

        private void btnSupportPause_Click(object sender, EventArgs e)
        {
            this.splitContainer2.SplitterDistance = 143;
            //this.MinimumSize = new Size(this.MinimumSize.Width, 740);
        }

        private void btnSupportHistory_Click(object sender, EventArgs e)
        {
            if (this.main_form.supportnote_wind == null)
            {
                SupportNoteWindow wind = new SupportNoteWindow(this);
                wind.Text += " (" + this.main_form.G.loged_in_user_level.ToUserLevelString() + " : " + this.main_form.G.loged_in_user_name + " - " + this.main_form.G.loged_in_user_realname + ")";
                wind.MdiParent = this.main_form;
                this.main_form.supportnote_wind = wind;
                wind.Show();
            }
            else
            {
                this.main_form.supportnote_wind.Activate();
            }
        }

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    this.main_form.sn_wind = null;
        //    if (this.main_form.supportnote_wind != null)
        //    {
        //        this.main_form.supportnote_wind.Close();
        //    }
        //    base.OnClosing(e);
        //}

        private void btnPasswordAdd_Click(object sender, EventArgs e)
        {
            SerialPasswordDialog wind = new SerialPasswordDialog(this.main_form, this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.FillDgvPassword(GetPasswordList(this.serial.sernum));
                if (this.dgvPassword.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is SerialPassword).Where(r => ((SerialPassword)r.Tag).id == wind.inserted_id).Count<DataGridViewRow>() > 0)
                {
                    this.dgvPassword.CurrentCell = this.dgvPassword.Rows.Cast<DataGridViewRow>().Where(r => r.Tag is SerialPassword).Where(r => ((SerialPassword)r.Tag).id == wind.inserted_id).First<DataGridViewRow>().Cells[1];
                }
            }
        }

        private void btnPasswordRemove_Click(object sender, EventArgs e)
        {
            if (this.dgvPassword.CurrentCell == null)
                return;

            if (!(this.dgvPassword.Rows[this.dgvPassword.CurrentCell.RowIndex].Tag is SerialPassword))
                return;

            SerialPassword pwd_to_del = (SerialPassword)this.dgvPassword.Rows[this.dgvPassword.CurrentCell.RowIndex].Tag;

            if (MessageAlert.Show("ลบรหัสผ่าน \"" + pwd_to_del.pass_word + "\" นี้ใช่หรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {

                CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "serialpassword/delete&id=" + pwd_to_del.id.ToString());
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                //if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                //{
                this.FillDgvPassword(GetPasswordList(this.serial.sernum));
                //}
            }
        }

        public static List<SerialPassword> GetPasswordList(string sernum)
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serialpassword/get_password&sernum=" + sernum);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return sr.serial_password;
            }
            else
            {
                return new List<SerialPassword>();
            }
        }

        private void FillDgvPassword(List<SerialPassword> list_password)
        {
            this.dgvPassword.Rows.Clear();
            this.dgvPassword.Columns.Clear();
            this.dgvPassword.Tag = HelperClass.DGV_TAG.READ;

            this.dgvPassword.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Visible = false
            });
            this.dgvPassword.Columns.Add(new DataGridViewTextBoxColumn()
            {
                //AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            foreach (SerialPassword sp in list_password)
            {
                int r = this.dgvPassword.Rows.Add();
                this.dgvPassword.Rows[r].Tag = sp;

                this.dgvPassword.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvPassword.Rows[r].Cells[0].Value = sp.id;

                this.dgvPassword.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvPassword.Rows[r].Cells[1].Value = sp.pass_word;
            }
            //this.dgvPassword.DrawDgvRowBorder();
        }

        private void btnEditMA_Click(object sender, EventArgs e)
        {
            MAFormDialog ma = new MAFormDialog(this);
            if (ma.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    this.getSerial(this.serial.id);
                };
                worker.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnDeleteMA_Click(object sender, EventArgs e)
        {
            this.DeleteMA();
        }

        private void DeleteMA()
        {
            if (this.ma.Count == 0)
                return;

            if (MessageAlert.Show("ลบข้อมูลบริการ MA., ทำต่อ?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.Cancel)
                return;


            bool delete_success = false;
            string err_msg = "";

            this.FormProcessing();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "ma/delete&id=" + this.ma[0].id.ToString());
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    delete_success = true;
                }
                else
                {
                    delete_success = false;
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (delete_success)
                {
                    this.ma.RemoveAll(t => t.id > -1);
                    this.fillSerialInForm();
                    this.FormRead();
                }
                else
                {
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.DeleteMA();
                    }
                    this.FormRead();
                }
            };
            worker.RunWorkerAsync();
        }

        private void SnWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.MdiFormClosing && (this.form_mode != FORM_MODE.READ && this.form_mode != FORM_MODE.READ_ITEM))
            {
                this.Activate();
                if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "SN_Net", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (e.CloseReason == CloseReason.UserClosing && (this.form_mode != FORM_MODE.READ && this.form_mode != FORM_MODE.READ_ITEM))
            {
                this.Activate();
                if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "SN_Net", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            this.main_form.sn_wind = null;
            if (this.main_form.supportnote_wind != null)
            {
                this.main_form.supportnote_wind.Close();
            }
        }

        private void btnEditCloud_Click(object sender, EventArgs e)
        {
            CloudSrv cs = this.cloudsrv.Count > 0 ? this.cloudsrv.First() : null;

            CloudsrvFormDialog csf = new CloudsrvFormDialog(this, cs);
            if (csf.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    this.getSerial(this.serial.id);
                };
                worker.RunWorkerCompleted += delegate
                {
                    this.fillSerialInForm();
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnDeleteCloud_Click(object sender, EventArgs e)
        {
            this.DeleteCloudSrv();
        }

        private void DeleteCloudSrv()
        {
            if (this.cloudsrv.Count == 0)
                return;

            if (MessageAlert.Show("ลบข้อมูลบริการ Express on Cloud, ทำต่อ?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.Cancel)
                return;


            bool delete_success = false;
            string err_msg = "";

            this.FormProcessing();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "cloudsrv/delete&id=" + this.cloudsrv[0].id.ToString());
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    delete_success = true;
                }
                else
                {
                    delete_success = false;
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (delete_success)
                {
                    this.cloudsrv.RemoveAll(t => t.id > -1);
                    this.fillSerialInForm();
                    this.FormRead();
                }
                else
                {
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.DeleteCloudSrv();
                    }
                    this.FormRead();
                }
            };
            worker.RunWorkerAsync();
        }

        private void toolStripInquiryMA_Click(object sender, EventArgs e)
        {
            InquiryMaAndCloud im = new InquiryMaAndCloud(this, INQUIRY_SERVICE_TYPE.MA);
            if (im.ShowDialog() == DialogResult.OK)
            {
                this.getSerial(im.selected_id);
                this.fillSerialInForm();
            }
        }

        private void toolStripInquiryCloud_Click(object sender, EventArgs e)
        {
            InquiryMaAndCloud im = new InquiryMaAndCloud(this, INQUIRY_SERVICE_TYPE.CLOUD);
            if (im.ShowDialog() == DialogResult.OK)
            {
                this.getSerial(im.selected_id);
                this.fillSerialInForm();
            }
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
