using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.MiscClass;
using SN_Net.DataModels;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class SnWindow : Form
    {
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
            this.sortMode = SORT_SN;
            this.fillSerialInForm(this.getSerial(LAST_ROW, 0, sortMode));
            this.setFormState(FormState.FORM_STATE_READY);
        }

        private void SnWindow_Activated(object sender, EventArgs e)
        {
            this.txtDummyControl.Focus();
        }
        
        #region Get Serial data from server
        private Serial getSerial(int row_id, int find_direction = 0, string sort_by = SORT_ID)
        {
            Serial serial = new Serial();
            CRUDResult get;
            ServerResult sr;

            if (row_id < 1)
            {
                switch (row_id)
                {
                    case FIRST_ROW:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_first&sort=" + sort_by);
                        Console.WriteLine(get.data);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            serial = sr.serial[0];
                        }
                        break;

                    case LAST_ROW:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_last&sort=" + sort_by);
                        Console.WriteLine(get.data);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            serial = sr.serial[0];
                        }
                        break;

                    default:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_last&sort=" + sort_by);
                        Console.WriteLine(get.data);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            serial = sr.serial[0];
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
                        Console.WriteLine(get.data);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            serial = sr.serial[0];
                        }
                        break;

                    case FIND_PREV:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_prev&sort=" + sort_by + "&id=" + row_id);
                        Console.WriteLine(get.data);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            serial = sr.serial[0];
                        }
                        break;

                    default:
                        get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_at&sort=" + sort_by + "&id=" + row_id);
                        Console.WriteLine(get.data);
                        sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            serial = sr.serial[0];
                        }
                        break;
                }
            }
            
            return serial;
        }
        #endregion Get Serial data from server

        private void fillSerialInForm(Serial serial)
        {
            this.id = serial.id;
            this.txtSerNum.Text = serial.sernum;
            this.txtVersion.Text = serial.version;
            this.txtPrenam.Text = serial.prenam;
            this.txtCompnam.Text = serial.compnam;
            this.txtAddr01.Text = serial.addr01;
            this.txtAddr02.Text = serial.addr02;
            this.txtAddr03.Text = serial.addr03;
            this.txtZipcod.Text = serial.zipcod;
            this.txtTelnum.Text = serial.telnum;
            this.txtTelnum2.Text = serial.telnum;
            this.txtFaxnum.Text = serial.faxnum;
            //this.cbBusityp
            this.txtBusides.Text = serial.busides;
            //this.dpPurdat
            //this.dpExpdat
            //this.txtExpdat.Text
            //this.cbHowKnow
            //this.cbArea
            //this.dpManual
            this.txtRefnum.Text = serial.refnum;
            this.txtRemark.Text = serial.remark;
            //this.cbVerext
            //this.dpVerextdat
            //this.cbDealer_Dealer

        }

        private void setFormState(string form_state)
        {
            List<Control> exclusion_control;
            List<int> disabled_btn;

            switch (form_state)
            {
                case FormState.FORM_STATE_READY:
                    disabled_btn = new List<int>();
                    disabled_btn.Add(3);
                    disabled_btn.Add(4);

                    FormState.Ready(this, this.tabControl1.SelectedTab, null, this.toolStrip1, disabled_btn);
                    this.State = FormState.FORM_STATE_READY;
                    break;

                case FormState.FORM_STATE_ADD:

                    break;

                case FormState.FORM_STATE_EDIT:
                    exclusion_control = new List<Control>();
                    exclusion_control.Add(this.txtTelnum2);
                    exclusion_control.Add(this.txtContact2);
                    exclusion_control.Add(this.txtExpdat);
                    exclusion_control.Add(this.txtReg2);
                    
                    disabled_btn = new List<int>();
                    disabled_btn.Add(0);
                    disabled_btn.Add(1);
                    disabled_btn.Add(2);
                    disabled_btn.Add(6);
                    disabled_btn.Add(7);
                    disabled_btn.Add(8);
                    disabled_btn.Add(9);
                    disabled_btn.Add(11);
                    disabled_btn.Add(12);
                    disabled_btn.Add(13);
                    disabled_btn.Add(14);
                    disabled_btn.Add(15);
                    
                    FormState.Edit(this, this.tabControl1.SelectedTab, exclusion_control, this.toolStrip1, disabled_btn);
                    break;

                default:
                    break;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// Find dummyTextBox again when change tab
            FormState.tabChangeHandle(this, this.tabControl1.SelectedTab);
        }

        private void toolStripFirst_Click(object sender, EventArgs e)
        {
            this.fillSerialInForm(this.getSerial(FIRST_ROW, 0, sortMode));
        }

        private void toolStripLast_Click(object sender, EventArgs e)
        {
            this.fillSerialInForm(this.getSerial(LAST_ROW, 0, sortMode));
        }

        private void toolStripPrevious_Click(object sender, EventArgs e)
        {
            this.fillSerialInForm(this.getSerial(this.id, FIND_PREV, this.sortMode));
        }

        private void toolStripNext_Click(object sender, EventArgs e)
        {
            this.fillSerialInForm(this.getSerial(this.id, FIND_NEXT, this.sortMode));
        }


        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.setFormState(FormState.FORM_STATE_EDIT);
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            this.setFormState(FormState.FORM_STATE_ADD);
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            
        }

        private void btnBrowseBusityp_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(IstabList.TITLE.BUSITYP);
            wind.ShowDialog();
        }
    }
}
