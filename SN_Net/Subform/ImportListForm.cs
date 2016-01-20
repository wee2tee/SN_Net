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
    public partial class ImportListForm : Form
    {
        private GlobalVar G;
        private SnWindow parent_window;
        private List<RegisterData> register_data;
        private string dealer_code = string.Empty;
        RegisterData rg;
        Serial serial;
        string prob_email;
        Control current_focused_control;
        private enum FORM_STATE
        {
            BEGINING,
            EDITING,
            PROCESSING
        }
        private FORM_STATE form_state;

        public ImportListForm(SnWindow parent_window)
        {
            InitializeComponent();
            this.parent_window = parent_window;
            this.G = this.parent_window.G;
        }

        private void ImportListForm_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
            this.dummyTextBox.Width = 0;
            this.FormBegining();
            this.toolStripProcess.Visible = true;
            this.lblDealer_Compnam.Text = "";
            this.lblBusityp_typdes.Text = "";

            this.BindControlEvent();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"validate_code\":\"WeeTee\",";
                json_data += "\"p_type\":\"get_new_register_list\"}";

                CRUDResult post = ApiActions.POST("http://www.esg.co.th/esg/SN_Net_API/get_registered_sn.php", json_data);
                //CRUDResult post = ApiActions.POST("http://localhost/esg/SN_Net_API/get_registered_sn.php", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    if (sr.register_data.Count > 0)
                    {
                        this.register_data = sr.register_data;
                    }
                    else
                    {
                        this.register_data = new List<RegisterData>();
                    }
                }
            };

            worker.RunWorkerCompleted += delegate
            {
                this.fillDataGrid();
                this.toolStripProcess.Visible = false;
            };

            worker.RunWorkerAsync();
        }

        private void ImportListForm_Shown(object sender, EventArgs e)
        {
            this.dgvRegister.Focus();
        }

        private void BindControlEvent()
        {
            List<TextBox> list_textbox = new List<TextBox>();
            list_textbox.Add(this.sAddr01);
            list_textbox.Add(this.sAddr02);
            list_textbox.Add(this.sAddr03);
            list_textbox.Add(this.sBusides);
            list_textbox.Add(this.sBusityp);
            list_textbox.Add(this.sCompnam);
            list_textbox.Add(this.sContact);
            list_textbox.Add(this.sContEmail);
            list_textbox.Add(this.sContTelnum);
            list_textbox.Add(this.sDealer);
            list_textbox.Add(this.sEmail);
            list_textbox.Add(this.sFaxnum);
            list_textbox.Add(this.sPosition);
            list_textbox.Add(this.sPrenam);
            list_textbox.Add(this.sTelnum);
            list_textbox.Add(this.sZipcod);

            #region keep current control focus
            this.dgvRegister.GotFocus += new EventHandler(this.KeepFocusedControl);
            this.btnSave.GotFocus += new EventHandler(this.KeepFocusedControl);
            this.btnCancel.GotFocus += new EventHandler(this.KeepFocusedControl);

            foreach (TextBox tb in list_textbox)
            {
                tb.GotFocus += new EventHandler(this.KeepFocusedControl);
            }
            #endregion keep current control focus

            #region specify onEnter event
            foreach (TextBox tb in list_textbox)
            {
                tb.Enter += delegate
                {
                    tb.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                    tb.ForeColor = Color.Black;
                    tb.SelectionStart = tb.Text.Length;
                };
            }
            #endregion specify onEnter event

            #region specity onLeave event
            foreach (TextBox tb in list_textbox)
            {
                tb.Leave += delegate
                {
                    tb.BackColor = Color.White;
                    tb.ForeColor = Color.Black;
                };
            }
            #endregion specity onLeave event

            this.sBusityp.Leave += delegate
            {
                if (this.sBusityp.Text.Length > 0 && this.form_state == FORM_STATE.EDITING)
                {
                    if (this.parent_window.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.sBusityp.Text) != null)
                    {
                        this.lblBusityp_typdes.Text = this.parent_window.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.sBusityp.Text).typdes_th;
                    }
                    else
                    {
                        this.sBusityp.Focus();
                        SendKeys.Send("{F6}");
                    }
                }
                else
                {
                    this.lblBusityp_typdes.Text = "";
                }
            };

            this.sDealer.Leave += delegate
            {
                if (this.sDealer.Text.Length > 0 && this.form_state == FORM_STATE.EDITING)
                {
                    if (this.parent_window.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == this.sDealer.Text) != null)
                    {
                        this.lblDealer_Compnam.Text = this.parent_window.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == this.sDealer.Text).compnam;
                    }
                    else
                    {
                        this.sDealer.Focus();
                        SendKeys.Send("{F6}");
                    }
                }
                else
                {
                    this.lblDealer_Compnam.Text = "";
                }
            };
        }

        private void KeepFocusedControl(object sender, EventArgs e)
        {
            this.current_focused_control = (Control)sender;
            this.toolStripInfo.Text = (string)((Control)sender).Tag;
            Console.WriteLine(this.current_focused_control.Name + " is focused");
        }

        private void fillDataGrid()
        {
            this.dgvRegister.Rows.Clear();
            this.dgvRegister.Columns.Clear();
            this.dgvRegister.EnableHeadersVisualStyles = false;

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.HeaderText = "ID";
            col0.Width = 0;
            col0.Visible = false;
            this.dgvRegister.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "Sernum";
            //col1.Width = 110;
            col1.Width = this.dgvRegister.Width - (SystemInformation.VerticalScrollBarWidth + 3);
            col1.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Padding = new Padding(5, 3, 5, 3)
            };
            this.dgvRegister.Columns.Add(col1);

            //DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            //col2.HeaderText = "Compnam";
            ////col2.Width = this.dgvRegister.Width - (col0.Width + col1.Width + SystemInformation.VerticalScrollBarWidth + 3);
            //col2.Width = 330;
            //col2.HeaderCell.Style = new DataGridViewCellStyle()
            //{
            //    Alignment = DataGridViewContentAlignment.MiddleLeft,
            //    Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
            //    BackColor = ColorResource.COLUMN_HEADER_BROWN,
            //    Padding = new Padding(5, 3, 5, 3)
            //};
            //this.dgvRegister.Columns.Add(col2);

            foreach (RegisterData reg in this.register_data)
            {
                int r = this.dgvRegister.Rows.Add();
                this.dgvRegister.Rows[r].Height = 25;
                this.dgvRegister.Rows[r].Tag = reg;

                this.dgvRegister.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvRegister.Rows[r].Cells[0].Value = reg.id;

                this.dgvRegister.Rows[r].Cells[1].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };
                this.dgvRegister.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvRegister.Rows[r].Cells[1].Value = reg.sn;
                this.dgvRegister.Rows[r].Cells[1].ToolTipText = reg.comp_name;

                //this.dgvRegister.Rows[r].Cells[2].Style = new DataGridViewCellStyle()
                //{
                //    Font = new Font("Tahoma", 9.75f),
                //    Alignment = DataGridViewContentAlignment.MiddleLeft,
                //    BackColor = Color.White,
                //    ForeColor = Color.Black,
                //    SelectionBackColor = Color.White,
                //    SelectionForeColor = Color.Black
                //};
                //this.dgvRegister.Rows[r].Cells[2].ValueType = typeof(string);
                //this.dgvRegister.Rows[r].Cells[2].Value = reg.comp_name;
            }
            this.dgvRegister.FillLine(this.dgvRegister.Rows.Count + 3);
            this.dgvRegister.DrawLineEffect();
        }

        private void FormBegining()
        {
            this.form_state = FORM_STATE.BEGINING;
            this.dgvRegister.Enabled = true;
            this.dgvRegister.Focus();
            this.btnCancel.Enabled = false;
            this.btnSave.Enabled = false;
            this.toolStripProcess.Visible = false;

            this.wAddr01.Text = "";
            this.wAddr02.Text = "";
            this.wAddr03.Text = "";
            this.wBusides.Text = "";
            this.wBusityp.Text = "";
            this.wCompnam.Text = "";
            this.wContact.Text = "";
            this.wContEmail.Text = "";
            this.wContTelnum.Text = "";
            this.wDealer.Text = "";
            this.wEmail.Text = "";
            this.wFaxnum.Text = "";
            this.wPosition.Text = "";
            this.wPrenam.Text = "";
            this.wSernum.Text = " -   -";
            this.wTelnum.Text = "";
            this.wZipcod.Text = "";
            this.wRegDate.Text = "";

            this.sAddr01.Text = "";
            this.sAddr02.Text = "";
            this.sAddr03.Text = "";
            this.sBusides.Text = "";
            this.sBusityp.Text = "";
            this.sCompnam.Text = "";
            this.sContact.Text = "";
            this.sContEmail.Text = "";
            this.sContTelnum.Text = "";
            this.sDealer.Text = "";
            this.sEmail.Text = "";
            this.sFaxnum.Text = "";
            this.sPosition.Text = "";
            this.sPrenam.Text = "";
            this.sSernum.Text = " -   -";
            this.sTelnum.Text = "";
            this.sZipcod.Text = "";
            this.sRegDate.Text = "";
            this.lblBusityp_typdes.Text = "";
            this.lblDealer_Compnam.Text = "";

            this.sAddr01.Enabled = false;
            this.sAddr02.Enabled = false;
            this.sAddr03.Enabled = false;
            this.sBusides.Enabled = false;
            this.sBusityp.Enabled = false;
            this.sCompnam.Enabled = false;
            this.sContact.Enabled = false;
            this.sContEmail.Enabled = false;
            this.sContTelnum.Enabled = false;
            this.sDealer.Enabled = false;
            this.sEmail.Enabled = false;
            this.sFaxnum.Enabled = false;
            this.sPosition.Enabled = false;
            this.sPrenam.Enabled = false;
            //this.sSernum.Enabled = false;
            this.sTelnum.Enabled = false;
            this.sZipcod.Enabled = false;

            this.btnBrowseBusityp.Enabled = false;
            this.btnBrowseDealer.Enabled = false;

            this.btnAddr01.Enabled = false;
            this.btnAddr02.Enabled = false;
            this.btnAddr03.Enabled = false;
            this.btnBusides.Enabled = false;
            this.btnBusides.Enabled = false;
            this.btnCompnam.Enabled = false;
            this.btnContact.Enabled = false;
            this.btnContEmail.Enabled = false;
            this.btnContTelnum.Enabled = false;
            this.btnEmail.Enabled = false;
            this.btnFaxnum.Enabled = false;
            this.btnPosition.Enabled = false;
            this.btnPrenam.Enabled = false;
            this.btnTelnum.Enabled = false;
            this.btnZipcod.Enabled = false;
        }

        private void FormProcessing()
        {
            this.form_state = FORM_STATE.PROCESSING;
            this.dummyTextBox.Focus();
            this.dgvRegister.Enabled = false;
            this.btnCancel.Enabled = false;
            this.btnSave.Enabled = false;
            this.toolStripProcess.Visible = true;

            this.sAddr01.Enabled = false;
            this.sAddr02.Enabled = false;
            this.sAddr03.Enabled = false;
            this.sBusides.Enabled = false;
            this.sBusityp.Enabled = false;
            this.sCompnam.Enabled = false;
            this.sContact.Enabled = false;
            this.sContEmail.Enabled = false;
            this.sContTelnum.Enabled = false;
            this.sDealer.Enabled = false;
            this.sEmail.Enabled = false;
            this.sFaxnum.Enabled = false;
            this.sPosition.Enabled = false;
            this.sPrenam.Enabled = false;
            //this.sSernum.Enabled = false;
            this.sTelnum.Enabled = false;
            this.sZipcod.Enabled = false;

            this.btnBrowseBusityp.Enabled = false;
            this.btnBrowseDealer.Enabled = false;

            this.btnAddr01.Enabled = false;
            this.btnAddr02.Enabled = false;
            this.btnAddr03.Enabled = false;
            this.btnBusides.Enabled = false;
            this.btnBusides.Enabled = false;
            this.btnCompnam.Enabled = false;
            this.btnContact.Enabled = false;
            this.btnContEmail.Enabled = false;
            this.btnContTelnum.Enabled = false;
            this.btnEmail.Enabled = false;
            this.btnFaxnum.Enabled = false;
            this.btnPosition.Enabled = false;
            this.btnPrenam.Enabled = false;
            this.btnTelnum.Enabled = false;
            this.btnZipcod.Enabled = false;
        }

        private void FormEditing()
        {
            this.form_state = FORM_STATE.EDITING;
            this.dgvRegister.Enabled = false;
            this.btnCancel.Enabled = true;
            this.btnSave.Enabled = true;
            this.toolStripProcess.Visible = false;
            
            this.sAddr01.Enabled = true;
            this.sAddr02.Enabled = true;
            this.sAddr03.Enabled = true;
            this.sBusides.Enabled = true;
            this.sBusityp.Enabled = true;
            this.sCompnam.Enabled = true;
            this.sContact.Enabled = true;
            this.sContEmail.Enabled = true;
            this.sContTelnum.Enabled = true;
            this.sDealer.Enabled = true;
            this.sEmail.Enabled = true;
            this.sFaxnum.Enabled = true;
            this.sPosition.Enabled = true;
            this.sPrenam.Enabled = true;
            //this.sSernum.Enabled = true;
            this.sTelnum.Enabled = true;
            this.sZipcod.Enabled = true;

            this.btnBrowseBusityp.Enabled = true;
            this.btnBrowseDealer.Enabled = true;

            this.btnAddr01.Enabled = true;
            this.btnAddr02.Enabled = true;
            this.btnAddr03.Enabled = true;
            this.btnBusides.Enabled = true;
            this.btnBusides.Enabled = true;
            this.btnCompnam.Enabled = true;
            this.btnContact.Enabled = true;
            this.btnContEmail.Enabled = true;
            this.btnContTelnum.Enabled = true;
            this.btnEmail.Enabled = true;
            this.btnFaxnum.Enabled = true;
            this.btnPosition.Enabled = true;
            this.btnPrenam.Enabled = true;
            this.btnTelnum.Enabled = true;
            this.btnZipcod.Enabled = true;
        }

        private void dgvRegister_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.GetRegisterData();
        }

        private void GetRegisterData()
        {
            if (this.dgvRegister.Rows[this.dgvRegister.CurrentCell.RowIndex].Tag is RegisterData)
            {
                this.rg = (RegisterData)this.dgvRegister.Rows[this.dgvRegister.CurrentCell.RowIndex].Tag;
                bool get_serial_success = false;

                this.FormBegining();
                this.FormProcessing();
                RegisterData reg = (RegisterData)this.dgvRegister.Rows[this.dgvRegister.CurrentCell.RowIndex].Tag;
                this.wAddr01.Text = reg.comp_addr1;
                this.wAddr02.Text = reg.comp_addr2;
                this.wAddr03.Text = reg.comp_addr3;
                this.wBusides.Text = reg.comp_bus_desc;
                this.wBusityp.Text = reg.comp_bus_type;
                this.wCompnam.Text = reg.comp_name;
                this.wContact.Text = reg.cont_name;
                this.wContEmail.Text = reg.cont_email;
                this.wContTelnum.Text = reg.cont_tel;
                this.dealer_code = (reg.purchase_from == "Express" ? "X-ESG" : "");
                this.wDealer.Text = (reg.purchase_from == "Express" ? reg.purchase_from : reg.purchase_from_desc);
                this.wEmail.Text = reg.comp_email;
                this.wFaxnum.Text = reg.comp_fax;
                this.wPosition.Text = reg.cont_position;
                this.wPrenam.Text = reg.comp_prenam;
                this.wSernum.Text = reg.sn;
                this.wTelnum.Text = reg.comp_tel;
                this.wZipcod.Text = reg.comp_zipcod;
                this.wRegDate.pickedDate(reg.reg_date);
                this.sRegDate.pickedDate(reg.reg_date);

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/get_sn_register&sernum=" + this.wSernum.Text);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        if (sr.serial.Count > 0)
                        {
                            this.serial = sr.serial[0];
                            get_serial_success = true;
                        }
                        else
                        {
                            this.serial = null;
                            get_serial_success = false;
                            MessageAlert.Show("Can not find this S/N", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        }

                        if (sr.problem.Count > 0)
                        {
                            this.prob_email = sr.problem[0].probdesc;
                        }
                        else
                        {
                            this.prob_email = "";
                        }
                    }
                    else
                    {
                        this.serial = null;
                        get_serial_success = false;
                        MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };

                worker.RunWorkerCompleted += delegate
                {
                    if (get_serial_success)
                    {
                        this.sAddr01.Text = this.serial.addr01;
                        this.sAddr02.Text = this.serial.addr02;
                        this.sAddr03.Text = this.serial.addr03;
                        this.sBusides.Text = this.serial.busides;
                        this.sBusityp.Text = this.serial.busityp;
                        this.lblBusityp_typdes.Text = (this.parent_window.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.sBusityp.Text) != null ? this.parent_window.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.sBusityp.Text).typdes_th : "");
                        this.sCompnam.Text = this.serial.compnam;
                        this.sContact.Text = this.serial.contact;
                        this.sContEmail.Text = "";
                        this.sContTelnum.Text = "";
                        //this.sDealer.Text = this.dealer_code;
                        this.sDealer.Text = this.serial.dealer_dealer;
                        this.lblDealer_Compnam.Text = (this.parent_window.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == this.sDealer.Text) != null ? this.parent_window.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == this.sDealer.Text).compnam : "");
                        this.sEmail.Text = this.prob_email;
                        this.sFaxnum.Text = this.serial.faxnum;
                        this.sPosition.Text = this.serial.position;
                        this.sPrenam.Text = this.serial.prenam;
                        this.sSernum.Text = this.serial.sernum;
                        this.sTelnum.Text = this.serial.telnum;
                        this.sZipcod.Text = this.serial.zipcod;

                        this.FormEditing();
                        this.sPrenam.Focus();
                    }
                    else
                    {
                        this.FormBegining();
                    }
                };

                worker.RunWorkerAsync();
            }
        }

        private void btnBrowseBusityp_Click(object sender, EventArgs e)
        {
            IstabList wind = new IstabList(this.parent_window.main_form, this.sBusityp.Text.cleanString(), Istab.TABTYP.BUSITYP);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.sBusityp.Text = wind.istab.typcod;
                this.lblBusityp_typdes.Text = wind.istab.typdes_th;
                this.sBusityp.Focus();
                SendKeys.Send("{TAB}");
            }
            else
            {
                this.sBusityp.Focus();
            }
        }

        private void btnBrowseDealer_Click(object sender, EventArgs e)
        {
            DealerList wind = new DealerList(this.parent_window, this.sDealer.Text.cleanString());
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.sDealer.Text = wind.dealer.dealer;
                this.lblDealer_Compnam.Text = wind.dealer.compnam;
                this.sDealer.Focus();
                SendKeys.Send("{TAB}");
            }
            else
            {
                this.sDealer.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Control err_field = this.ValidateField();
            if (err_field == null)
            {
                this.FormProcessing();
                bool post_success = false;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    #region preparing json_data
                    string json_data = "{\"id\":" + this.serial.id.ToString() + ",";
                    json_data += "\"sernum\":\"" + this.serial.sernum + "\",";
                    json_data += "\"prenam\":\"" + this.sPrenam.Text.cleanString() + "\",";
                    json_data += "\"compnam\":\"" + this.sCompnam.Text.cleanString() + "\",";
                    json_data += "\"addr01\":\"" + this.sAddr01.Text.cleanString() + "\",";
                    json_data += "\"addr02\":\"" + this.sAddr02.Text.cleanString() + "\",";
                    json_data += "\"addr03\":\"" + this.sAddr03.Text.cleanString() + "\",";
                    json_data += "\"zipcod\":\"" + this.sZipcod.Text.cleanString() + "\",";

                    string telnum = "";
                    if (this.sTelnum.Text.Length > 0)
                    {
                        telnum += this.sTelnum.Text.cleanString();
                    }
                    if (this.sTelnum.Text.Length > 0 && this.sContTelnum.Text.Length > 0)
                    {
                        telnum += ", " + this.sContTelnum.Text.cleanString();
                    }
                    if (this.sTelnum.Text.Length == 0 && this.sContTelnum.Text.Length > 0)
                    {
                        telnum += this.sContTelnum.Text.cleanString();
                    }

                    json_data += "\"telnum\":\"" + telnum + "\",";
                    json_data += "\"faxnum\":\"" + this.sFaxnum.Text.cleanString() + "\",";
                    json_data += "\"busityp\":\"" + this.sBusityp.Text.cleanString() + "\",";
                    json_data += "\"busides\":\"" + this.sBusides.Text.cleanString() + "\",";
                    json_data += "\"dealer_dealer\":\"" + this.sDealer.Text.cleanString() + "\",";
                    json_data += "\"contact\":\"" + this.sContact.Text.cleanString() + "\",";

                    string email = "";
                    if (this.sEmail.Text.Length > 0)
                    {
                        email += this.sEmail.Text.cleanString();
                    }
                    if (this.sEmail.Text.Length > 0 && this.sContEmail.Text.Length > 0)
                    {
                        email += ", " + this.sContEmail.Text.cleanString();
                    }
                    if (this.sEmail.Text.Length == 0 && this.sContEmail.Text.Length > 0)
                    {
                        email += this.sContEmail.Text.cleanString();
                    }

                    json_data += (email.Length > 0 ? "\"email\":\"" + email + "\"," : "");
                    json_data += "\"position\":\"" + this.sPosition.Text.cleanString() + "\",";
                    json_data += "\"upfree\":\"Y\",";
                    json_data += "\"manual\":\"" + this.sRegDate.Text.toMySQLDate() + "\",";
                    json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";
                    #endregion preparing json_data

                    //Console.WriteLine("worker do_work");
                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/update_registered_data", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
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
                        ServerResult sr = this.MarkedServerDataAsRecorded();

                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            this.FormBegining();
                            MessageBox.Show("Update data completed");
                            this.register_data.Remove(this.rg);
                            this.fillDataGrid();
                            this.dgvRegister.Rows[0].Selected = true;
                            this.GetRegisterData();
                        }
                        else
                        {
                            MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                            this.FormBegining();
                        }
                    }
                };

                worker.RunWorkerAsync();
            }
            else
            {
                err_field.Focus();
                SendKeys.Send("{F6}");
            }
        }

        private ServerResult MarkedServerDataAsRecorded()
        {
            string json_data = "{\"validate_code\":\"WeeTee\",";
            json_data += "\"p_type\":\"marked_as_recorded\",";
            json_data += "\"id\":" + this.rg.id.ToString() + "}";
            
            CRUDResult post = ApiActions.POST("http://www.esg.co.th/esg/SN_Net_API/get_registered_sn.php", json_data);
            //CRUDResult post = ApiActions.POST("http://localhost/esg/SN_Net_API/get_registered_sn.php", json_data);
            return JsonConvert.DeserializeObject<ServerResult>(post.data);
        }

        private Control ValidateField()
        {
            Control err_field = null;

            if (this.sBusityp.Text.Length > 0 && this.parent_window.main_form.data_resource.LIST_BUSITYP.Find(t => t.typcod == this.sBusityp.Text.cleanString()) == null)
            {
                return this.sBusityp;
            }
            if (this.sDealer.Text.Length > 0 && this.parent_window.main_form.data_resource.LIST_DEALER.Find(t => t.dealer == this.sDealer.Text.cleanString()) == null)
            {
                return this.sDealer;
            }

            if (this.sBusityp.Text.Length == 0 || this.sDealer.Text.Length == 0)
            {
                string warning_msg = "";
                if (this.sBusityp.Text.Length == 0)
                {
                    err_field = this.sBusityp;
                    warning_msg += " \"Business Type\" ";
                }
                if (this.sDealer.Text.Length == 0)
                {
                    err_field = (this.sBusityp.Text.Length == 0 ? err_field = this.sBusityp : err_field = this.sDealer);
                    warning_msg += (this.sBusityp.Text.Length == 0 ? "and \"Purchase From\" " : "\"Purchase From\" ");
                }
                if (MessageAlert.Show(warning_msg + " is not specify, Do you want to continue?", "Warning", MessageAlertButtons.YES_NO, MessageAlertIcons.WARNING) != DialogResult.Yes)
                {
                    return err_field;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.serial = null;
            this.rg = null;
            this.dealer_code = "";
            this.prob_email = "";
            this.FormBegining();
        }

        #region Button : Copy Web data to S/N data 
        private void btnPrenam_Click(object sender, EventArgs e)
        {
            this.sPrenam.Text = this.wPrenam.Text;
            this.sPrenam.Focus();
        }

        private void btnCompnam_Click(object sender, EventArgs e)
        {
            this.sCompnam.Text = this.wCompnam.Text;
            this.sCompnam.Focus();
        }

        private void btnAddr01_Click(object sender, EventArgs e)
        {
            this.sAddr01.Text = this.wAddr01.Text;
            this.sAddr01.Focus();
        }

        private void btnAddr02_Click(object sender, EventArgs e)
        {
            this.sAddr02.Text = this.wAddr02.Text;
            this.sAddr02.Focus();
        }

        private void btnAddr03_Click(object sender, EventArgs e)
        {
            this.sAddr03.Text = this.wAddr03.Text;
            this.sAddr03.Focus();
        }

        private void btnZipcod_Click(object sender, EventArgs e)
        {
            this.sZipcod.Text = this.wZipcod.Text;
            this.sZipcod.Focus();
        }

        private void btnTelnum_Click(object sender, EventArgs e)
        {
            this.sTelnum.Text = this.wTelnum.Text;
            this.sTelnum.Focus();
        }

        private void btnFaxnum_Click(object sender, EventArgs e)
        {
            this.sFaxnum.Text = this.wFaxnum.Text;
            this.sFaxnum.Focus();
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            this.sEmail.Text = this.wEmail.Text;
            this.sEmail.Focus();
        }

        private void btnBusides_Click(object sender, EventArgs e)
        {
            this.sBusides.Text = this.wBusides.Text;
            this.sBusides.Focus();
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            this.sContact.Text = this.wContact.Text;
            this.sContact.Focus();
        }

        private void btnPosition_Click(object sender, EventArgs e)
        {
            this.sPosition.Text = this.wPosition.Text;
            this.sPosition.Focus();
        }

        private void btnContTelnum_Click(object sender, EventArgs e)
        {
            this.sContTelnum.Text = this.wContTelnum.Text;
            this.sContTelnum.Focus();
        }

        private void btnContEmail_Click(object sender, EventArgs e)
        {
            this.sContEmail.Text = this.wContEmail.Text;
            this.sContEmail.Focus();
        }
        #endregion Button : Copy Web data to S/N data

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.current_focused_control == this.dgvRegister)
                {
                    this.GetRegisterData();
                    return true;
                }
                else if (!(this.current_focused_control is Button))
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }
            if (keyData == Keys.F9)
            {
                this.btnSave.PerformClick();
                return true;
            }
            if (keyData == Keys.F6)
            {
                if (this.current_focused_control == this.sBusityp)
                {
                    if (this.form_state == FORM_STATE.EDITING)
                    {
                        this.btnBrowseBusityp.PerformClick();
                        return true;
                    }
                }
                if (this.current_focused_control == this.sDealer)
                {
                    if (this.form_state == FORM_STATE.EDITING)
                    {
                        this.btnBrowseDealer.PerformClick();
                        return true;
                    }
                }
            }
            //if (keyData == (Keys.Control | Keys.Enter))
            if (keyData == Keys.F12)
            {
                #region Control+Enter to Copy data in each field from Web to S/N
                if (this.current_focused_control == this.sPrenam)
                {
                    this.btnPrenam.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sCompnam)
                {
                    this.btnCompnam.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sAddr01)
                {
                    this.btnAddr01.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sAddr02)
                {
                    this.btnAddr02.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sAddr03)
                {
                    this.btnAddr03.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sZipcod)
                {
                    this.btnZipcod.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sTelnum)
                {
                    this.btnTelnum.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sFaxnum)
                {
                    this.btnFaxnum.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sEmail)
                {
                    this.btnEmail.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sBusides)
                {
                    this.btnBusides.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sContact)
                {
                    this.btnContact.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sPosition)
                {
                    this.btnPosition.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sContTelnum)
                {
                    this.btnContTelnum.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                if (this.current_focused_control == this.sContEmail)
                {
                    this.btnContEmail.PerformClick();
                    SendKeys.Send("{ENTER}");
                    return true;
                }
                #endregion Control+Enter to Copy data in each field from Web to S/N
            }
            if (keyData == (Keys.Control | Keys.F12))
            {
                this.btnPrenam.PerformClick();
                this.btnCompnam.PerformClick();
                this.btnAddr01.PerformClick();
                this.btnAddr02.PerformClick();
                this.btnAddr03.PerformClick();
                this.btnZipcod.PerformClick();
                this.btnTelnum.PerformClick();
                this.btnFaxnum.PerformClick();
                this.btnEmail.PerformClick();
                this.btnBusides.PerformClick();
                this.btnContact.PerformClick();
                this.btnPosition.PerformClick();
                this.btnContTelnum.PerformClick();
                this.btnContEmail.PerformClick();

                return true;
            }
            if (this.form_state == FORM_STATE.EDITING && (keyData == Keys.Left || keyData == Keys.Right) && (this.current_focused_control == this.btnSave || this.current_focused_control == this.btnCancel))
            {
                if (this.current_focused_control == this.btnSave)
                {
                    this.btnCancel.Focus();
                    return true;
                }
                else if(this.current_focused_control == this.btnCancel)
                {
                    this.btnSave.Focus();
                    return true;
                }
            }
            if (this.form_state == FORM_STATE.EDITING && (keyData == Keys.Up || keyData == Keys.Down) && (this.current_focused_control == this.btnSave || this.current_focused_control == this.btnCancel))
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
