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
        #region declare Data Model
        
        private Serial serial;
        private Istab busityp;
        private Istab area;
        private Istab howknown;
        private Dealer dealer;

        private Istab busityp_not_found = new Istab();
        private Istab area_not_found = new Istab();
        private Istab howknown_not_found = new Istab();
        private Dealer dealer_not_found = new Dealer();

        #endregion declare Data Model

        public int id;
        public string State;
        
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
            list_tb.Add(this.txtBusityp);
            list_tb.Add(this.txtDealer_dealer);
            list_tb.Add(this.txtHowknown);
            // Browse Button
            List<Button> list_btn = new List<Button>();
            list_btn.Add(this.btnBrowseBusityp);
            list_btn.Add(this.btnBrowseDealer);
            list_btn.Add(this.btnBrowseHowknown);
            // Pairing
            PairTextBoxWithBrowseButton.Attach(list_tb, list_btn);
            #endregion pairing TextBox with Browse Button

            #region pairing MaskedTextBox with DateTimePicker
            // MaskedTextBox
            List<MaskedTextBox> list_mt = new List<MaskedTextBox>();
            list_mt.Add(this.mskPurdat);
            list_mt.Add(this.mskExpdat);
            list_mt.Add(this.mskManual);
            list_mt.Add(this.mskVerextdat);
            // DateTimePicker
            List<DateTimePicker> list_dp = new List<DateTimePicker>();
            list_dp.Add(this.dpPurdat);
            list_dp.Add(this.dpExpdat);
            list_dp.Add(this.dpManual);
            list_dp.Add(this.dpVerextdat);
            // Pairing
            PairDatePickerWithMaskedTextBox.Attach(list_mt, list_dp);
            #endregion pairing MaskedTextBox with DateTimePicker

            #region configure formcontrolsequence
            // Form control
            List<Control> list_ct = new List<Control>();
            list_ct.Add(this.txtSerNum);
            list_ct.Add(this.txtVersion);
            list_ct.Add(this.txtArea);
            list_ct.Add(this.txtRefnum);
            list_ct.Add(this.txtPrenam);
            list_ct.Add(this.txtCompnam);
            list_ct.Add(this.txtAddr01);
            list_ct.Add(this.txtAddr02);
            list_ct.Add(this.txtAddr03);
            list_ct.Add(this.txtZipcod);
            list_ct.Add(this.txtTelnum);
            list_ct.Add(this.txtFaxnum);
            list_ct.Add(this.txtContact);
            list_ct.Add(this.txtPosition);
            list_ct.Add(this.txtOldnum);
            list_ct.Add(this.txtRemark);
            list_ct.Add(this.txtBusides);
            list_ct.Add(this.txtBusityp);
            list_ct.Add(this.txtDealer_dealer);
            list_ct.Add(this.txtHowknown);
            list_ct.Add(this.mskPurdat);
            list_ct.Add(this.txtReg);
            list_ct.Add(this.mskExpdat);
            list_ct.Add(this.mskManual);
            list_ct.Add(this.cbVerext);
            list_ct.Add(this.mskVerextdat);
            FormControlSequence.Attach(list_ct);
            #endregion configure formcontrolsequence

            this.sortMode = SORT_SN;
            this.getSerial(LAST_ROW, 0, this.sortMode);
            this.fillSerialInForm();
            //this.setFormState(FormState.FORM_STATE_READY);
        }

        private void SnWindow_Activated(object sender, EventArgs e)
        {
            this.txtDummyControl.Focus();
        }
        
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
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
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
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
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
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
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
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
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
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
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
                            this.dealer = (sr.dealer.Count > 0 ? sr.dealer[0] : this.dealer_not_found);
                        }
                        break;
                }
            }
            
            return serial;
        }
        #endregion Get Serial data from server

        private void fillSerialInForm()
        {
            this.id = this.serial.id;
            this.txtSerNum.Text = this.serial.sernum;
            this.txtVersion.Text = this.serial.version;
            this.txtPrenam.Text = this.serial.prenam;
            this.txtCompnam.Text = this.serial.compnam;
            this.txtAddr01.Text = this.serial.addr01;
            this.txtAddr02.Text = this.serial.addr02;
            this.txtAddr03.Text = this.serial.addr03;
            this.txtZipcod.Text = this.serial.zipcod;
            this.txtTelnum.Text = this.serial.telnum;
            this.txtTelnum2.Text = this.serial.telnum;
            this.txtFaxnum.Text = this.serial.faxnum;
            this.txtBusityp.Text = this.serial.busityp;
            this.lblBusityp.Text = this.busityp.typdes_th;
            this.txtBusides.Text = serial.busides;

            //this.dpPurdat.pickedDate(this.serial.purdat);
            this.mskPurdat.pickedDate(this.serial.purdat);
            
            //this.dpExpdat.pickedDate(this.serial.expdat);
            this.mskExpdat.pickedDate(this.serial.expdat);
            this.txtExpdat.pickedDate(this.serial.expdat);

            //this.dpManual.pickedDate(this.serial.manual);
            this.mskManual.pickedDate(this.serial.manual);

            //this.dpVerextdat.pickedDate(this.serial.verextdat);
            this.mskVerextdat.pickedDate(this.serial.verextdat);

            this.txtHowknown.Text = this.serial.howknown;
            this.lblHowknown.Text = this.howknown.typdes_th;
            this.txtArea.Text = this.serial.area;
            this.lblArea.Text = this.area.typdes_th;
            
            //Manual
            if (this.serial.manual != null)
            {
                DateTime _manual = Convert.ToDateTime(this.serial.manual, cinfo_us);
                this.dpManual.Value = _manual;
            }

            this.txtRefnum.Text = serial.refnum;
            this.txtRemark.Text = serial.remark;
            //this.cbVerext
            //this.dpVerextdat
            this.txtDealer_dealer.Text = this.serial.dealer_dealer;
            this.lblDealer_Dealer.Text = this.dealer.prenam + " " + this.dealer.compnam;
            
        }

        //private void setFormState(string form_state)
        //{
        //    List<Control> exclusion_control;
        //    List<int> disabled_btn;

        //    switch (form_state)
        //    {
        //        case FormState.FORM_STATE_READY:
        //            disabled_btn = new List<int>();
        //            disabled_btn.Add(3);
        //            disabled_btn.Add(4);

        //            FormState.Ready(this, this.tabControl1.SelectedTab, null, this.toolStrip1, disabled_btn);
        //            this.State = FormState.FORM_STATE_READY;
        //            break;

        //        case FormState.FORM_STATE_ADD:

        //            break;

        //        case FormState.FORM_STATE_EDIT:
        //            this.State = FormState.FORM_STATE_EDIT;
        //            exclusion_control = new List<Control>();
        //            exclusion_control.Add(this.txtTelnum2);
        //            exclusion_control.Add(this.txtContact2);
        //            exclusion_control.Add(this.txtExpdat);
        //            exclusion_control.Add(this.txtReg2);
                    
        //            disabled_btn = new List<int>();
        //            disabled_btn.Add(0);
        //            disabled_btn.Add(1);
        //            disabled_btn.Add(2);
        //            disabled_btn.Add(6);
        //            disabled_btn.Add(7);
        //            disabled_btn.Add(8);
        //            disabled_btn.Add(9);
        //            disabled_btn.Add(11);
        //            disabled_btn.Add(12);
        //            disabled_btn.Add(13);
        //            disabled_btn.Add(14);
        //            disabled_btn.Add(15);
                    
        //            FormState.Edit(this, this.tabControl1.SelectedTab, exclusion_control, this.toolStrip1, disabled_btn);
        //            break;

        //        default:
        //            break;
        //    }
        //}

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// Find dummyTextBox again when change tab
            FormState.tabChangeHandle(this, this.tabControl1.SelectedTab);
        }

        #region toolStrip

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
            //this.setFormState(FormState.FORM_STATE_EDIT);
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            //this.setFormState(FormState.FORM_STATE_ADD);
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            
        }
        #endregion toolStrip

        private void btnBrowseBusityp_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.busityp, Istab.TABTYP.BUSITYP);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.busityp = wind.istab;
                this.txtBusityp.Text = this.busityp.typcod;
                this.lblBusityp.Text = this.busityp.typdes_th;
            }
        }

        private void btnBrowseArea_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.area, Istab.TABTYP.AREA);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.area = wind.istab;
                this.txtArea.Text = this.area.typcod;
                this.lblArea.Text = this.area.typdes_th;
            }
        }

        private void btnBrowseHowknown_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.howknown, Istab.TABTYP.HOWKNOWN);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.howknown = wind.istab;
                this.txtHowknown.Text = this.howknown.typcod;
                this.lblHowknown.Text = this.howknown.typdes_th;
            }
        }

        private void btnBrowseDealer_Click(object sender, EventArgs e)
        {
            DealerList wind = new DealerList(this.dealer);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.dealer = wind.dealer;
                this.txtDealer_dealer.Text = this.dealer.dealer;
                this.lblDealer_Dealer.Text = this.dealer.prenam + " " + this.dealer.compnam;
            }
        }

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
    }
}
