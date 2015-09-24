﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class DealerWindow : Form
    {
        #region General variable declaration
        public MainForm main_form;
        private GlobalVar G;
        private FORM_STATE form_state;
        public SORT_MODE sort_mode;
        public string inquiry_expression = "";
        Control current_focused_control = null;
        private PrintDocument print_doc;

        string find_dealer = ""; // kept dealer search string
        string find_contact = ""; // kept contact search string
        string find_name = ""; // kept name search string
        string find_area = ""; // kept area search string
        #endregion General variable declaration

        #region Data Model variable declaration
        public List<Dealer> dealer_id_list;
        public Dealer dealer;
        public List<Serial> serial;
        #endregion Data Model variable declaration

        private enum FORM_STATE
        {
            PROCESSING,
            READ,
            READF8,
            READF7,
            ADD,
            EDIT,
            ADDF8,
            EDITF8,
        }

        public enum SORT_MODE
        {
            DEALER,
            CONTACT,
            COMPNAM,
            AREA
        }

        public enum PAGE_SETUP_NAME
        {
            LABEL,
            LITTLE_ENVELOPE,
            BIG_ENVELOPE
        }

        private List<CustomTextBox> list_customtextbox = new List<CustomTextBox>();

        public DealerWindow(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
            this.G = main_form.G;
            this.sort_mode = SORT_MODE.DEALER;
        }

        private void DealerWindow_Load(object sender, EventArgs e)
        {
            this.lblArea_typdes.Text = "";

            this.list_customtextbox.Add(this.txtDealer);
            this.list_customtextbox.Add(this.txtPrenam);
            this.list_customtextbox.Add(this.txtCompnam);
            this.list_customtextbox.Add(this.txtAddr01);
            this.list_customtextbox.Add(this.txtAddr02);
            this.list_customtextbox.Add(this.txtAddr03);
            this.list_customtextbox.Add(this.txtZipcod);
            this.list_customtextbox.Add(this.txtTelnum);
            this.list_customtextbox.Add(this.txtFaxnum);
            this.list_customtextbox.Add(this.txtContact);
            this.list_customtextbox.Add(this.txtPosition);
            this.list_customtextbox.Add(this.txtBusides);
            this.list_customtextbox.Add(this.txtArea);
            this.list_customtextbox.Add(this.txtRemark);

            this.BindAreaFieldEvent();
            this.BindCustomTextBoxEvent();
            this.txtDummy.Width = 0;
            this.FormProcessing();
            this.GetDealerIdList();
            this.GetFirst();
        }

        public string GetSortFieldName()
        {
            switch (this.sort_mode)
            {
                case SORT_MODE.DEALER:
                    return "dealer";
                case SORT_MODE.CONTACT:
                    return "contact";
                case SORT_MODE.COMPNAM:
                    return "compnam";
                case SORT_MODE.AREA:
                    return "area";
                default:
                    return "dealer";
            }
        }

        private void GetDealerIdList()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "dealer/get_dealer_id_list&order_by=" + this.GetSortFieldName());
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.dealer_id_list = (sr.dealer.Count > 0 ? sr.dealer : new List<Dealer>());
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                this.dealer_id_list = new List<Dealer>();
            }
            this.FormRead();
        }

        private void GetDealerByID(int id)
        {
            bool get_success = false;

            this.FormProcessing();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "dealer/get_with_id&id=" + id.ToString());
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    if (sr.dealer.Count > 0)
                    {
                        this.dealer = sr.dealer[0];
                        get_success = true;

                        if (sr.serial.Count > 0)
                        {
                            this.serial = sr.serial;
                        }
                        else
                        {
                            this.serial = new List<Serial>();
                        }
                    }
                    else
                    {
                        get_success = false;
                        MessageAlert.Show("There's no data", "", MessageAlertButtons.OK, MessageAlertIcons.INFORMATION);
                    }
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (get_success)
                {
                    this.FillForm();
                    this.FormRead();
                }
                else
                {
                    MessageAlert.Show(StringResource.DATA_NOT_FOUND, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            };
            worker.RunWorkerAsync();
        }

        private void GetFirst()
        {
            this.GetDealerByID(this.dealer_id_list.First<Dealer>().id);
        }

        private void GetLast()
        {
            this.GetDealerByID(this.dealer_id_list.Last<Dealer>().id);
        }

        private void GetPrev()
        {
            int curr_ndx = this.dealer_id_list.FindIndex(t => t.id == this.dealer.id);
            if (curr_ndx > 0)
            {
                this.GetDealerByID(this.dealer_id_list[curr_ndx - 1].id);
            }
        }

        private void GetNext()
        {
            int curr_ndx = this.dealer_id_list.FindIndex(t => t.id == this.dealer.id);
            if (curr_ndx < this.dealer_id_list.Count - 1)
            {
                this.GetDealerByID(this.dealer_id_list[curr_ndx + 1].id);
            }
        }

        private void BindAreaFieldEvent()
        {
            this.txtArea.Leave += delegate
            {
                if (this.txtArea.Texts.Length > 0)
                {
                    if (this.main_form.data_resource.LIST_AREA.Find(t => t.typcod == this.txtArea.textBox1.Text) == null)
                    {
                        this.txtArea.Focus();
                        SendKeys.Send("{F6}");
                    }
                    else
                    {
                        this.lblArea_typdes.Text = this.main_form.data_resource.LIST_AREA.Find(t => t.typcod == this.txtArea.textBox1.Text).typdes_th;
                    }
                }
                else
                {
                    this.lblArea_typdes.Text = "";
                }
            };
        }

        private void BindCustomTextBoxEvent()
        {
            foreach (CustomTextBox c in list_customtextbox)
            {
                c.textBox1.GotFocus += delegate
                {
                    this.current_focused_control = c;
                    c.textBox1.SelectionStart = c.textBox1.Text.Length;
                };
                c.label1.DoubleClick += delegate
                {
                    if (this.form_state == FORM_STATE.READ)
                    {
                        this.FormEdit();
                        c.Focus();
                    }
                };
            }
        }

        private void btnBrowseArea_Click(object sender, EventArgs e)
        {
            this.txtArea.Focus();
            IstabList wind = new IstabList(this.main_form, this.txtArea.Texts, Istab.TABTYP.AREA);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.txtArea.Texts = wind.istab.typcod;
                this.lblArea_typdes.Text = wind.istab.typdes_th;
            }
        }

        // Prevent changing tab while add/edit
        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.form_state != FORM_STATE.READ)
            {
                e.Cancel = true;
                if (this.current_focused_control != null)
                {
                    this.current_focused_control.Focus();
                }
            }
        }

        private void FillForm()
        {
            this.txtDealer.Texts = this.dealer.dealer;
            this.txtPrenam.Texts = this.dealer.prenam;
            this.txtCompnam.Texts = this.dealer.compnam;
            this.txtAddr01.Texts = this.dealer.addr01;
            this.txtAddr02.Texts = this.dealer.addr02;
            this.txtAddr03.Texts = this.dealer.addr03;
            this.txtZipcod.Texts = this.dealer.zipcod;
            this.txtTelnum.Texts = this.dealer.telnum;
            this.txtTelnum2.Texts = this.dealer.telnum;
            this.txtFaxnum.Texts = this.dealer.faxnum;
            this.txtContact.Texts = this.dealer.contact;
            this.txtContact2.Texts = this.dealer.contact;
            this.txtPosition.Texts = this.dealer.position;
            this.txtBusides.Texts = this.dealer.busides;
            this.txtArea.Texts = this.dealer.area;
            this.lblArea_typdes.Text = (this.main_form.data_resource.LIST_AREA.Find(t => t.typcod == this.txtArea.Texts) != null ? this.main_form.data_resource.LIST_AREA.Find(t => t.typcod == this.txtArea.Texts).typdes_th : "");
            this.txtRemark.Texts = this.dealer.remark;
            
            this.FillDataGridSerial();
        }

        private void FillDataGridSerial()
        {
            this.dgvSerial.Rows.Clear();
            this.dgvSerial.Columns.Clear();
            this.dgvSerial.EnableHeadersVisualStyles = false;

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn()
            {
                Visible = false,
                Width = 0,
                HeaderText = "ID"
            };
            this.dgvSerial.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            col1.Width = 110;
            col1.HeaderText = "S/N";
            col1.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col1.HeaderCell.Style = new DataGridViewCellStyle()
            {
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Font = new Font("Tahoma", 9.75f),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 1, 0, 1)
            };
            this.dgvSerial.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            col2.Width = 90;
            col2.HeaderText = "Purchase";
            col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Font = new Font("Tahoma", 9.75f),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 1, 0, 1)
            };
            this.dgvSerial.Columns.Add(col2);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.SortMode = DataGridViewColumnSortMode.NotSortable;
            col4.Width = 100;
            col4.HeaderText = "Area";
            col4.HeaderCell.Style = new DataGridViewCellStyle()
            {
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Font = new Font("Tahoma", 9.75f),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 1, 0, 1)
            };

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.SortMode = DataGridViewColumnSortMode.NotSortable;
            col3.Width = this.dgvSerial.ClientSize.Width - (col1.Width + col2.Width + col4.Width + SystemInformation.VerticalScrollBarWidth + 3);
            col3.HeaderText = "Customer Name";
            col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                BackColor = ColorResource.COLUMN_HEADER_BROWN,
                Font = new Font("Tahoma", 9.75f),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(0, 1, 0, 1)
            };
            this.dgvSerial.Columns.Add(col3);
            this.dgvSerial.Columns.Add(col4);

            foreach (Serial s in this.serial)
            {
                int r = this.dgvSerial.Rows.Add();
                this.dgvSerial.Rows[r].Tag = s;

                this.dgvSerial.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvSerial.Rows[r].Cells[0].Value = s.id;
                this.dgvSerial.Rows[r].Cells[0].Tag = new DataRowIntention(DataRowIntention.TO_DO.READ);

                this.dgvSerial.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvSerial.Rows[r].Cells[1].Value = s.sernum;

                this.dgvSerial.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvSerial.Rows[r].Cells[2].pickedDate(s.purdat);

                this.dgvSerial.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvSerial.Rows[r].Cells[3].Value = s.compnam;

                this.dgvSerial.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvSerial.Rows[r].Cells[4].Value = s.area;
            }
            this.dgvSerial.FillLine(this.serial.Count);
            this.dgvSerial.DrawLineEffect();
            this.dgvSerial.Resize += delegate
            {
                this.dgvSerial.FillLine(this.serial.Count);
            };

            //this.dgvSerial.MouseClick += new MouseEventHandler(this.DataGridRightClick);
        }

        //private void DataGridRightClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //    {
        //        Console.WriteLine("current_row : " + ((DataGridView)sender).CurrentCell.RowIndex.ToString());

        //        // reverse the row has right click before to read-row
        //        if (((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells[0].Tag is DataRowIntention)
        //        {
        //            ((DataRowIntention)(((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells[0]).Tag).to_do = DataRowIntention.TO_DO.READ;
        //        }

        //        // mark the current row as delete-row
        //        if (((DataGridView)sender).Rows[((DataGridView)sender).HitTest(e.X, e.Y).RowIndex].Cells[0].Tag is DataRowIntention)
        //        {
        //            ((DataGridView)sender).Rows[((DataGridView)sender).HitTest(e.X, e.Y).RowIndex].Cells[1].Selected = true;
        //            ((DataRowIntention)((DataGridView)sender).Rows[((DataGridView)sender).HitTest(e.X, e.Y).RowIndex].Cells[0].Tag).to_do = DataRowIntention.TO_DO.DELETE;
        //        }
        //    }
        //}

        // expand datagrid cell to fit container
        private void dgvSerial_Paint(object sender, PaintEventArgs e)
        {
            this.dgvSerial.Columns[3].Width = this.dgvSerial.ClientSize.Width - (this.dgvSerial.Columns[1].Width + this.dgvSerial.Columns[2].Width + this.dgvSerial.Columns[4].Width + SystemInformation.VerticalScrollBarWidth + 3);
        }

        #region CREATE,UPDATE,DELETE dealer
        private void CreateDealer()
        {
            this.txtDummy.Focus();
            bool post_success = false;
            string err_msg = "";

            this.FormProcessing();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"dealer\":\"" + this.txtDealer.Texts.cleanString() + "\",";
                json_data += "\"prenam\":\"" + this.txtPrenam.Texts.cleanString() + "\",";
                json_data += "\"compnam\":\"" + this.txtCompnam.Texts.cleanString() + "\",";
                json_data += "\"addr01\":\"" + this.txtAddr01.Texts.cleanString() + "\",";
                json_data += "\"addr02\":\"" + this.txtAddr02.Texts.cleanString() + "\",";
                json_data += "\"addr03\":\"" + this.txtAddr03.Texts.cleanString() + "\",";
                json_data += "\"zipcod\":\"" + this.txtZipcod.Texts.cleanString() + "\",";
                json_data += "\"telnum\":\"" + this.txtTelnum.Texts.cleanString() + "\",";
                json_data += "\"faxnum\":\"" + this.txtFaxnum.Texts.cleanString() + "\",";
                json_data += "\"contact\":\"" + this.txtContact.Texts.cleanString() + "\",";
                json_data += "\"position\":\"" + this.txtPosition.Texts.cleanString() + "\",";
                json_data += "\"busides\":\"" + this.txtBusides.Texts.cleanString() + "\",";
                json_data += "\"area\":\"" + this.txtArea.Texts.cleanString() + "\",";
                json_data += "\"remark\":\"" + this.txtRemark.Texts.cleanString() + "\",";
                json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "dealer/create_new", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                    this.dealer = sr.dealer[0];
                    this.serial = sr.serial;
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
                    this.GetDealerIdList();
                    this.FillForm();
                    this.FormRead();
                }
                else
                {
                    this.FormAdd();
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.txtDealer.Focus();
                }
            };
            worker.RunWorkerAsync();
        }

        private void UpdateDealer()
        {
            this.txtDummy.Focus();
            bool post_success = false;
            string err_msg = "";

            this.FormProcessing();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                string json_data = "{\"id\":" + this.dealer.id.ToString() + ",";
                json_data += "\"prenam\":\"" + this.txtPrenam.Texts.cleanString() + "\",";
                json_data += "\"compnam\":\"" + this.txtCompnam.Texts.cleanString() + "\",";
                json_data += "\"addr01\":\"" + this.txtAddr01.Texts.cleanString() + "\",";
                json_data += "\"addr02\":\"" + this.txtAddr02.Texts.cleanString() + "\",";
                json_data += "\"addr03\":\"" + this.txtAddr03.Texts.cleanString() + "\",";
                json_data += "\"zipcod\":\"" + this.txtZipcod.Texts.cleanString() + "\",";
                json_data += "\"telnum\":\"" + this.txtTelnum.Texts.cleanString() + "\",";
                json_data += "\"faxnum\":\"" + this.txtFaxnum.Texts.cleanString() + "\",";
                json_data += "\"contact\":\"" + this.txtContact.Texts.cleanString() + "\",";
                json_data += "\"position\":\"" + this.txtPosition.Texts.cleanString() + "\",";
                json_data += "\"busides\":\"" + this.txtBusides.Texts.cleanString() + "\",";
                json_data += "\"area\":\"" + this.txtArea.Texts.cleanString() + "\",";
                json_data += "\"remark\":\"" + this.txtRemark.Texts.cleanString() + "\",";
                json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "dealer/update_dealer", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                    this.dealer = sr.dealer[0];
                    this.serial = sr.serial;
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
                    this.GetDealerIdList();
                    this.FillForm();
                    this.FormRead();
                }
                else
                {
                    this.FormEdit();
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.txtPrenam.Focus();
                }
            };
            worker.RunWorkerAsync();
        }

        private void DeleteDealer()
        {
            //bool delete_success = false;
            //string err_msg = "";

            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += delegate
            //{
            //    CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "dealer/delete_dealer&id=" + this.dealer.id.ToString());
            //    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

            //    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            //    {
            //        this.dealer = sr.dealer[0];
            //        this.serial = sr.serial;
            //        delete_success = true;
            //    }
            //    else
            //    {
            //        delete_success = false;
            //        err_msg = sr.message;
            //    }
            //};
            //worker.RunWorkerCompleted += delegate
            //{
            //    if (delete_success)
            //    {
            //        this.GetDealerIdList();
            //        this.FillForm();
            //        this.FormRead();
            //    }
            //    else
            //    {
            //        this.FormRead();
            //        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            //    }
            //};
            //worker.RunWorkerAsync();
        }
        #endregion CREATE,UPDATE,DELETE dealer

        private void ShowInlineForm()
        {

        }

        private void ClearInlineForm()
        {

        }

        #region SET FORM MODE
        private void FormProcessing()
        {
            this.form_state = FORM_STATE.PROCESSING;
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripFirst.Enabled = false;
            this.toolStripPrev.Enabled = false;
            this.toolStripNext.Enabled = false;
            this.toolStripLast.Enabled = false;
            this.toolStripSearch.Enabled = false;
            this.toolStripInquiryAll.Enabled = false;
            this.toolStripInquiryRest.Enabled = false;
            this.toolStripInquiryCondition.Enabled = false;
            this.toolStripSearchArea.Enabled = false;
            this.toolStripSearchCode.Enabled = false;
            this.toolStripSearchContact.Enabled = false;
            this.toolStripSearchName.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintBigEnv.Enabled = false;
            this.toolStripPrintLabel3Col.Enabled = false;
            this.toolStripPrintLittleEnv.Enabled = false;

            this.toolStripProcessing.Visible = true;

            this.txtDealer.ReadOnly = true;
            this.txtPrenam.ReadOnly = true;
            this.txtCompnam.ReadOnly = true;
            this.txtAddr01.ReadOnly = true;
            this.txtAddr02.ReadOnly = true;
            this.txtAddr03.ReadOnly = true;
            this.txtZipcod.ReadOnly = true;
            this.txtTelnum.ReadOnly = true;
            this.txtFaxnum.ReadOnly = true;
            this.txtContact.ReadOnly = true;
            this.txtPosition.ReadOnly = true;
            this.txtBusides.ReadOnly = true;
            this.txtArea.ReadOnly = true;
            this.txtRemark.ReadOnly = true;

            this.btnBrowseArea.Enabled = false;

            this.txtDummy.Focus();
        }

        private void FormRead()
        {
            this.txtDummy.Focus();

            this.form_state = FORM_STATE.READ;
            this.toolStripAdd.Enabled = true;
            this.toolStripEdit.Enabled = true;
            this.toolStripDelete.Enabled = true;
            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripFirst.Enabled = true;
            this.toolStripPrev.Enabled = true;
            this.toolStripNext.Enabled = true;
            this.toolStripLast.Enabled = true;
            this.toolStripSearch.Enabled = true;
            this.toolStripInquiryAll.Enabled = true;
            this.toolStripInquiryRest.Enabled = true;
            this.toolStripInquiryCondition.Enabled = true;
            this.toolStripSearchArea.Enabled = true;
            this.toolStripSearchCode.Enabled = true;
            this.toolStripSearchContact.Enabled = true;
            this.toolStripSearchName.Enabled = true;
            this.toolStripPrint.Enabled = true;
            this.toolStripPrintBigEnv.Enabled = true;
            this.toolStripPrintLabel3Col.Enabled = true;
            this.toolStripPrintLittleEnv.Enabled = true;

            this.toolStripProcessing.Visible = false;

            this.txtDealer.ReadOnly = true;
            this.txtPrenam.ReadOnly = true;
            this.txtCompnam.ReadOnly = true;
            this.txtAddr01.ReadOnly = true;
            this.txtAddr02.ReadOnly = true;
            this.txtAddr03.ReadOnly = true;
            this.txtZipcod.ReadOnly = true;
            this.txtTelnum.ReadOnly = true;
            this.txtFaxnum.ReadOnly = true;
            this.txtContact.ReadOnly = true;
            this.txtPosition.ReadOnly = true;
            this.txtBusides.ReadOnly = true;
            this.txtArea.ReadOnly = true;
            this.txtRemark.ReadOnly = true;

            this.btnBrowseArea.Enabled = false;
        }

        private void FormAdd()
        {
            this.form_state = FORM_STATE.ADD;
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripFirst.Enabled = false;
            this.toolStripPrev.Enabled = false;
            this.toolStripNext.Enabled = false;
            this.toolStripLast.Enabled = false;
            this.toolStripSearch.Enabled = false;
            this.toolStripInquiryAll.Enabled = false;
            this.toolStripInquiryRest.Enabled = false;
            this.toolStripInquiryCondition.Enabled = false;
            this.toolStripSearchArea.Enabled = false;
            this.toolStripSearchCode.Enabled = false;
            this.toolStripSearchContact.Enabled = false;
            this.toolStripSearchName.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintBigEnv.Enabled = false;
            this.toolStripPrintLabel3Col.Enabled = false;
            this.toolStripPrintLittleEnv.Enabled = false;

            this.toolStripProcessing.Visible = false;

            this.txtDealer.ReadOnly = false;
            this.txtPrenam.ReadOnly = false;
            this.txtCompnam.ReadOnly = false;
            this.txtAddr01.ReadOnly = false;
            this.txtAddr02.ReadOnly = false;
            this.txtAddr03.ReadOnly = false;
            this.txtZipcod.ReadOnly = false;
            this.txtTelnum.ReadOnly = false;
            this.txtFaxnum.ReadOnly = false;
            this.txtContact.ReadOnly = false;
            this.txtPosition.ReadOnly = false;
            this.txtBusides.ReadOnly = false;
            this.txtArea.ReadOnly = false;
            this.txtRemark.ReadOnly = false;

            this.btnBrowseArea.Enabled = true;
        }

        private void FormEdit()
        {
            this.form_state = FORM_STATE.EDIT;
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripFirst.Enabled = false;
            this.toolStripPrev.Enabled = false;
            this.toolStripNext.Enabled = false;
            this.toolStripLast.Enabled = false;
            this.toolStripSearch.Enabled = false;
            this.toolStripInquiryAll.Enabled = false;
            this.toolStripInquiryRest.Enabled = false;
            this.toolStripInquiryCondition.Enabled = false;
            this.toolStripSearchArea.Enabled = false;
            this.toolStripSearchCode.Enabled = false;
            this.toolStripSearchContact.Enabled = false;
            this.toolStripSearchName.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintBigEnv.Enabled = false;
            this.toolStripPrintLabel3Col.Enabled = false;
            this.toolStripPrintLittleEnv.Enabled = false;

            this.toolStripProcessing.Visible = false;

            this.txtPrenam.ReadOnly = false;
            this.txtCompnam.ReadOnly = false;
            this.txtAddr01.ReadOnly = false;
            this.txtAddr02.ReadOnly = false;
            this.txtAddr03.ReadOnly = false;
            this.txtZipcod.ReadOnly = false;
            this.txtTelnum.ReadOnly = false;
            this.txtFaxnum.ReadOnly = false;
            this.txtContact.ReadOnly = false;
            this.txtPosition.ReadOnly = false;
            this.txtBusides.ReadOnly = false;
            this.txtArea.ReadOnly = false;
            this.txtRemark.ReadOnly = false;

            this.btnBrowseArea.Enabled = true;
        }

        private void FormReadItemF8()
        {
            this.form_state = FORM_STATE.READF8;
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripFirst.Enabled = false;
            this.toolStripPrev.Enabled = false;
            this.toolStripNext.Enabled = false;
            this.toolStripLast.Enabled = false;
            this.toolStripSearch.Enabled = false;
            this.toolStripInquiryAll.Enabled = false;
            this.toolStripInquiryRest.Enabled = false;
            this.toolStripInquiryCondition.Enabled = false;
            this.toolStripSearchArea.Enabled = false;
            this.toolStripSearchCode.Enabled = false;
            this.toolStripSearchContact.Enabled = false;
            this.toolStripSearchName.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintBigEnv.Enabled = false;
            this.toolStripPrintLabel3Col.Enabled = false;
            this.toolStripPrintLittleEnv.Enabled = false;

            this.toolStripProcessing.Visible = false;

            this.txtDealer.ReadOnly = true;
            this.txtPrenam.ReadOnly = true;
            this.txtCompnam.ReadOnly = true;
            this.txtAddr01.ReadOnly = true;
            this.txtAddr02.ReadOnly = true;
            this.txtAddr03.ReadOnly = true;
            this.txtZipcod.ReadOnly = true;
            this.txtTelnum.ReadOnly = true;
            this.txtFaxnum.ReadOnly = true;
            this.txtContact.ReadOnly = true;
            this.txtPosition.ReadOnly = true;
            this.txtBusides.ReadOnly = true;
            this.txtArea.ReadOnly = true;
            this.txtRemark.ReadOnly = true;

            this.btnBrowseArea.Enabled = false;
        }

        private void FormReadItemF7()
        {
            this.form_state = FORM_STATE.READF7;
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripFirst.Enabled = false;
            this.toolStripPrev.Enabled = false;
            this.toolStripNext.Enabled = false;
            this.toolStripLast.Enabled = false;
            this.toolStripSearch.Enabled = false;
            this.toolStripInquiryAll.Enabled = false;
            this.toolStripInquiryRest.Enabled = false;
            this.toolStripInquiryCondition.Enabled = false;
            this.toolStripSearchArea.Enabled = false;
            this.toolStripSearchCode.Enabled = false;
            this.toolStripSearchContact.Enabled = false;
            this.toolStripSearchName.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintBigEnv.Enabled = false;
            this.toolStripPrintLabel3Col.Enabled = false;
            this.toolStripPrintLittleEnv.Enabled = false;

            this.toolStripProcessing.Visible = false;

            this.txtDealer.ReadOnly = true;
            this.txtPrenam.ReadOnly = true;
            this.txtCompnam.ReadOnly = true;
            this.txtAddr01.ReadOnly = true;
            this.txtAddr02.ReadOnly = true;
            this.txtAddr03.ReadOnly = true;
            this.txtZipcod.ReadOnly = true;
            this.txtTelnum.ReadOnly = true;
            this.txtFaxnum.ReadOnly = true;
            this.txtContact.ReadOnly = true;
            this.txtPosition.ReadOnly = true;
            this.txtBusides.ReadOnly = true;
            this.txtArea.ReadOnly = true;
            this.txtRemark.ReadOnly = true;

            this.btnBrowseArea.Enabled = false;
        }

        private void FormAddItemF8()
        {
            this.form_state = FORM_STATE.ADDF8;
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripFirst.Enabled = false;
            this.toolStripPrev.Enabled = false;
            this.toolStripNext.Enabled = false;
            this.toolStripLast.Enabled = false;
            this.toolStripSearch.Enabled = false;
            this.toolStripInquiryAll.Enabled = false;
            this.toolStripInquiryRest.Enabled = false;
            this.toolStripInquiryCondition.Enabled = false;
            this.toolStripSearchArea.Enabled = false;
            this.toolStripSearchCode.Enabled = false;
            this.toolStripSearchContact.Enabled = false;
            this.toolStripSearchName.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintBigEnv.Enabled = false;
            this.toolStripPrintLabel3Col.Enabled = false;
            this.toolStripPrintLittleEnv.Enabled = false;

            this.toolStripProcessing.Visible = false;

            this.txtDealer.ReadOnly = true;
            this.txtPrenam.ReadOnly = true;
            this.txtCompnam.ReadOnly = true;
            this.txtAddr01.ReadOnly = true;
            this.txtAddr02.ReadOnly = true;
            this.txtAddr03.ReadOnly = true;
            this.txtZipcod.ReadOnly = true;
            this.txtTelnum.ReadOnly = true;
            this.txtFaxnum.ReadOnly = true;
            this.txtContact.ReadOnly = true;
            this.txtPosition.ReadOnly = true;
            this.txtBusides.ReadOnly = true;
            this.txtArea.ReadOnly = true;
            this.txtRemark.ReadOnly = true;

            this.btnBrowseArea.Enabled = false;
        }

        private void FormEditItemF8()
        {
            this.form_state = FORM_STATE.EDITF8;
            this.toolStripAdd.Enabled = false;
            this.toolStripEdit.Enabled = false;
            this.toolStripDelete.Enabled = false;
            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripFirst.Enabled = false;
            this.toolStripPrev.Enabled = false;
            this.toolStripNext.Enabled = false;
            this.toolStripLast.Enabled = false;
            this.toolStripSearch.Enabled = false;
            this.toolStripInquiryAll.Enabled = false;
            this.toolStripInquiryRest.Enabled = false;
            this.toolStripInquiryCondition.Enabled = false;
            this.toolStripSearchArea.Enabled = false;
            this.toolStripSearchCode.Enabled = false;
            this.toolStripSearchContact.Enabled = false;
            this.toolStripSearchName.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintBigEnv.Enabled = false;
            this.toolStripPrintLabel3Col.Enabled = false;
            this.toolStripPrintLittleEnv.Enabled = false;

            this.toolStripProcessing.Visible = false;

            this.txtDealer.ReadOnly = true;
            this.txtPrenam.ReadOnly = true;
            this.txtCompnam.ReadOnly = true;
            this.txtAddr01.ReadOnly = true;
            this.txtAddr02.ReadOnly = true;
            this.txtAddr03.ReadOnly = true;
            this.txtZipcod.ReadOnly = true;
            this.txtTelnum.ReadOnly = true;
            this.txtFaxnum.ReadOnly = true;
            this.txtContact.ReadOnly = true;
            this.txtPosition.ReadOnly = true;
            this.txtBusides.ReadOnly = true;
            this.txtArea.ReadOnly = true;
            this.txtRemark.ReadOnly = true;

            this.btnBrowseArea.Enabled = false;
        }
        #endregion SET FORM MODE

        public void collapseToolstrip()
        {
            //this.toolStrip1.SetBounds(this.toolStrip1.Location.X, this.toolStrip1.Location.Y, this.toolStrip1.ClientSize.Width, 0);
        }

        private void DealerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.State == FormState.FORM_STATE_ADD || this.State == FormState.FORM_STATE_EDIT)
            //{
            //    if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "Warning", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.OK)
            //    {
            //        MainForm main_form = this.MdiParent as MainForm;
            //        main_form.dealer_wind = null;
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
            //    main_form.dealer_wind = null;
            //}

            MainForm main_form = this.MdiParent as MainForm;
            main_form.dealer_wind = null;
        }

        #region SET TOOLSTRIP ACTION
        private void toolStripFirst_Click(object sender, EventArgs e)
        {
            this.GetFirst();
        }

        private void toolStripPrev_Click(object sender, EventArgs e)
        {
            this.GetPrev();
        }

        private void toolStripNext_Click(object sender, EventArgs e)
        {
            this.GetNext();
        }

        private void toolStripLast_Click(object sender, EventArgs e)
        {
            this.GetLast();
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            this.FormAdd();

            this.txtDealer.Texts = "";
            this.txtPrenam.Texts = "";
            this.txtCompnam.Texts = "";
            this.txtAddr01.Texts = "";
            this.txtAddr02.Texts = "";
            this.txtAddr03.Texts = "";
            this.txtZipcod.Texts = "";
            this.txtTelnum.Texts = "";
            this.txtFaxnum.Texts = "";
            this.txtContact.Texts = "";
            this.txtPosition.Texts = "";
            this.txtBusides.Texts = "";
            this.txtArea.Texts = "";
            this.lblArea_typdes.Text = "";
            this.txtRemark.Texts = "";

            this.txtDealer.Focus();
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.FormEdit();
            this.txtPrenam.Focus();
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            if (this.form_state == FORM_STATE.ADD || this.form_state == FORM_STATE.EDIT)
            {
                this.FillForm();
                this.FormRead();
            }
            else if (this.form_state == FORM_STATE.READF7 || this.form_state == FORM_STATE.READF8)
            {
                this.FormRead();
            }
            else if (this.form_state == FORM_STATE.ADDF8 || this.form_state == FORM_STATE.EDITF8)
            {
                this.ClearInlineForm();
                this.FormReadItemF8();
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (this.form_state == FORM_STATE.ADD)
            {
                this.CreateDealer();
            }
            else if (this.form_state == FORM_STATE.EDIT)
            {
                this.UpdateDealer();
            }
        }

        private void toolStripSearch_ButtonClick(object sender, EventArgs e)
        {
            this.toolStripSearchCode.PerformClick();
        }

        private void toolStripSearchCode_Click(object sender, EventArgs e)
        {
            SearchDealerBox wind = new SearchDealerBox(SearchDealerBox.SEARCH_TYPE.DEALER);
            wind.txtKeyWord.Text = this.find_dealer;
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.sort_mode = SORT_MODE.DEALER;
                this.GetDealerIdList();
                this.find_dealer = wind.txtKeyWord.Text;

                this.FormProcessing();
                int target_id = this.dealer.id; // default to current id
                bool is_founded = false; // default search result to false
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    if (this.find_dealer.Length > 0)
                    {
                        foreach (Dealer d in this.dealer_id_list)
                        {
                            if (d.dealer != null)
                            {
                                if (string.CompareOrdinal(d.dealer, this.find_dealer) >= 0)
                                {
                                    target_id = d.id;
                                    is_founded = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        is_founded = true;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (is_founded)
                    {
                        if (target_id != this.dealer.id)
                        {
                            this.GetDealerByID(target_id);
                            this.FillForm();
                        }
                        this.FormRead();
                    }
                    else
                    {
                        this.FormRead();
                        MessageAlert.Show(StringResource.DATA_NOT_FOUND, "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void toolStripSearchContact_Click(object sender, EventArgs e)
        {
            SearchDealerBox wind = new SearchDealerBox(SearchDealerBox.SEARCH_TYPE.CONTACT);
            wind.txtKeyWord.Text = this.find_contact;
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.sort_mode = SORT_MODE.CONTACT;
                this.GetDealerIdList();
                this.find_contact = wind.txtKeyWord.Text;

                this.FormProcessing();
                int target_id = this.dealer.id; // default to current id
                bool is_founded = false; // default search result to false
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    if (this.find_contact.Length > 0)
                    {
                        foreach (Dealer d in this.dealer_id_list)
                        {
                            if (d.contact != null)
                            {
                                if (string.CompareOrdinal(d.contact, this.find_contact) >= 0)
                                {
                                    target_id = d.id;
                                    is_founded = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        is_founded = true;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (is_founded)
                    {
                        if (target_id != this.dealer.id)
                        {
                            this.GetDealerByID(target_id);
                            this.FillForm();
                        }
                        this.FormRead();
                    }
                    else
                    {
                        this.FormRead();
                        MessageAlert.Show(StringResource.DATA_NOT_FOUND, "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void toolStripSearchName_Click(object sender, EventArgs e)
        {
            SearchDealerBox wind = new SearchDealerBox(SearchDealerBox.SEARCH_TYPE.NAME);
            wind.txtKeyWord.Text = this.find_name;
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.sort_mode = SORT_MODE.COMPNAM;
                this.GetDealerIdList();
                this.find_name = wind.txtKeyWord.Text;

                this.FormProcessing();
                int target_id = this.dealer.id; // default to current id
                bool is_founded = false; // default search result to false
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    if (this.find_name.Length > 0)
                    {
                        foreach (Dealer d in this.dealer_id_list)
                        {
                            if (d.compnam != null)
                            {
                                if (string.CompareOrdinal(d.compnam, this.find_name) >= 0)
                                {
                                    target_id = d.id;
                                    is_founded = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        is_founded = true;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (is_founded)
                    {
                        if (target_id != this.dealer.id)
                        {
                            this.GetDealerByID(target_id);
                            this.FillForm();
                        }
                        this.FormRead();
                    }
                    else
                    {
                        this.FormRead();
                        MessageAlert.Show(StringResource.DATA_NOT_FOUND, "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void toolStripSearchArea_Click(object sender, EventArgs e)
        {
            SearchDealerBox wind = new SearchDealerBox(SearchDealerBox.SEARCH_TYPE.AREA);
            wind.txtKeyWord.Text = this.find_area;
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.sort_mode = SORT_MODE.AREA;
                this.GetDealerIdList();
                this.find_area = wind.txtKeyWord.Text;

                this.FormProcessing();
                int target_id = this.dealer.id; // default to current id
                bool is_founded = false; // default search result to false
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    if (this.find_area.Length > 0)
                    {
                        foreach (Dealer d in this.dealer_id_list)
                        {
                            if (d.area != null)
                            {
                                if (string.CompareOrdinal(d.area, this.find_area) >= 0)
                                {
                                    target_id = d.id;
                                    is_founded = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        is_founded = true;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (is_founded)
                    {
                        if (target_id != this.dealer.id)
                        {
                            this.GetDealerByID(target_id);
                            this.FillForm();
                        }
                        this.FormRead();
                    }
                    else
                    {
                        this.FormRead();
                        MessageAlert.Show(StringResource.DATA_NOT_FOUND, "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void toolStripInquiryAll_Click(object sender, EventArgs e)
        {
            DealerInquiryWindow wind = new DealerInquiryWindow(this, DealerInquiryWindow.INQUIRY_TYPE.ALL);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.GetDealerByID(wind.selected_id);
            }
        }

        private void toolStripInquiryRest_Click(object sender, EventArgs e)
        {
            DealerInquiryWindow wind = new DealerInquiryWindow(this, DealerInquiryWindow.INQUIRY_TYPE.REST);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.GetDealerByID(wind.selected_id);
            }
        }

        private void toolStripInquiryCondition_Click(object sender, EventArgs e)
        {
            InquiryConditionForm wind = new InquiryConditionForm(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.GetDealerByID(wind.selected_id);
            }
        }

        private void toolStripPrintLittleEnv_Click(object sender, EventArgs e)
        {
            print_doc = new PrintDocument();
            print_doc.PrintPage += delegate(object o, PrintPageEventArgs pe)
            {
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    using (Font font = new Font("Tahoma", 12f))
                    {
                        int line_start = 140;
                        int line_height = (font.Height * 2) - 5;
                        int line_multi = 0;

                        pe.Graphics.DrawString("กรุณาส่ง", font, brush, new Point(190, line_start));
                        pe.Graphics.DrawString(this.dealer.contact, font, brush, new Point(280, line_start + (line_height * ++line_multi)));
                        pe.Graphics.DrawString((this.dealer.prenam.Length > 0 ? this.dealer.prenam + " " + this.dealer.compnam : this.dealer.compnam), font, brush, new Point(280, line_start + (line_height * ++line_multi)));
                        pe.Graphics.DrawString(this.dealer.addr01, font, brush, new Point(280, line_start + (line_height * ++line_multi)));
                        pe.Graphics.DrawString(this.dealer.addr02 + " " + this.dealer.addr03, font, brush, new Point(280, line_start + (line_height * ++line_multi)));
                        pe.Graphics.DrawString(this.dealer.zipcod, font, brush, new Point(280, line_start + (line_height * ++line_multi)));
                    }
                }
            };

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = this.print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("Little Envelope", 910, 425);


            PrintOutputSelection wind = new PrintOutputSelection();
            if (wind.ShowDialog() == DialogResult.OK)
            {
                if (wind.output == PrintOutputSelection.OUTPUT.PRINTER)
                {
                    PrintDialog print_dialog = new PrintDialog();
                    print_dialog.Document = this.print_doc;
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = false;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = true;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_doc.Print();
                    }
                }
                if (wind.output == PrintOutputSelection.OUTPUT.SCREEN)
                {
                    PrintPreviewDialog preview_dialog = new PrintPreviewDialog();
                    preview_dialog.Document = this.print_doc;
                    preview_dialog.MdiParent = this.main_form;
                    preview_dialog.Show();
                }
                if (wind.output == PrintOutputSelection.OUTPUT.FILE)
                {

                }
            }
            else
            {
                print_doc = null;
                page_setup = null;
            }
        }

        private void toolStripPrintBigEnv_Click(object sender, EventArgs e)
        {
            print_doc = new PrintDocument();
            print_doc.PrintPage += delegate(object o, PrintPageEventArgs pe)
            {
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    using (Font font = new Font("Tahoma", 12f))
                    {
                        int line_start = 190;
                        int line_height = (font.Height * 2) - 5;
                        int line_multi = 0;

                        pe.Graphics.DrawString("กรุณาส่ง", font, brush, new Point(180, line_start));
                        pe.Graphics.DrawString(this.dealer.contact, font, brush, new Point(270, line_start + (line_height * ++line_multi)));
                        pe.Graphics.DrawString((this.dealer.prenam.Length > 0 ? this.dealer.prenam + " " + this.dealer.compnam : this.dealer.compnam), font, brush, new Point(270, line_start + (line_height * ++line_multi)));
                        pe.Graphics.DrawString(this.dealer.addr01, font, brush, new Point(270, line_start + (line_height * ++line_multi)));
                        pe.Graphics.DrawString(this.dealer.addr02 + " " + this.dealer.addr03, font, brush, new Point(270, line_start + (line_height * ++line_multi)));
                        pe.Graphics.DrawString(this.dealer.zipcod, font, brush, new Point(270, line_start + (line_height * ++line_multi)));
                    }
                }
            };

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = this.print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("Big Envelope", 890, 600);

            PrintOutputSelection wind = new PrintOutputSelection();
            if (wind.ShowDialog() == DialogResult.OK)
            {
                if (wind.output == PrintOutputSelection.OUTPUT.PRINTER)
                {
                    PrintDialog print_dialog = new PrintDialog();
                    print_dialog.Document = this.print_doc;
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = false;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = true;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_doc.Print();
                    }
                }
                if (wind.output == PrintOutputSelection.OUTPUT.SCREEN)
                {
                    PrintPreviewDialog preview_dialog = new PrintPreviewDialog();
                    preview_dialog.Document = this.print_doc;
                    preview_dialog.MdiParent = this.main_form;
                    preview_dialog.Show();
                }
                if (wind.output == PrintOutputSelection.OUTPUT.FILE)
                {

                }
            }
            else
            {
                print_doc = null;
                page_setup = null;
            }
        }

        private void toolStripPrintLabel3Col_Click(object sender, EventArgs e)
        {
            print_doc = new PrintDocument();

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = this.print_doc;
            //page_setup.PageSettings.PaperSize = new PaperSize("Sticker 2 column", 825, 1165);
            page_setup.PageSettings.PaperSize = new PaperSize("Sticker 3 column", 1250, 1195);
            page_setup.PageSettings.Margins = new Margins(0, 0, 0, 0);

            PrintDealerLabelOutputSelection wind = new PrintDealerLabelOutputSelection(this.main_form);
            wind.txtFrom.Text = this.dealer.dealer;
            wind.txtTo.Text = this.dealer.dealer;
            if (wind.ShowDialog() == DialogResult.OK)
            {
                int row_num = 0;
                int page_count = 0;
                List<Dealer> list_dealer = new List<Dealer>();

                print_doc.BeginPrint += delegate(object o, PrintEventArgs pe)
                {
                    string json_data = "{\"dealer_from\":\"" + wind.dealer_from + "\",";
                    json_data += "\"dealer_to\":\"" + wind.dealer_to + "\",";
                    json_data += "\"condition\":\"" + wind.condition + "\"}";

                    CRUDResult get = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "dealer/get_for_print_label", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        if (sr.dealer.Count > 0)
                        {
                            list_dealer = sr.dealer;
                        }
                        else
                        {
                            MessageAlert.Show(StringResource.NO_DATA_IN_RANGE, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                            return;
                        }
                    }
                    else
                    {
                        MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        return;
                    }

                };

                print_doc.PrintPage += delegate(object o, PrintPageEventArgs pe)
                {
                    using (Font font = new Font("Tahoma", 9.75f))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.Black))
                        {
                            //int max_col = 2; // for sticker 2 column
                            int max_col = 3; // for sticker 3 column
                            int col_count = 0;
                            int col_width = 413;

                            int line_count = 0;
                            int line_height = 25;

                            int block_count = 1; // store row number of sticker
                            int block_height = line_height * 4;
                            int block_margin_top = 25;
                            int block_margin_bottom = 25;

                            page_count++;
                            int page_row_count = 0;

                            for (int i = row_num; i < list_dealer.Count; i++)
                            {
                                row_num++;
                                col_count++;
                                page_row_count++;

                                if (page_row_count > 1 && i % max_col == 0)
                                {
                                    col_count = 1;
                                    ++block_count;
                                }

                                int contact_x = 30 + (col_count * col_width) - col_width;
                                int contact_y = ((block_count == 1 ? block_margin_top : (block_margin_top + block_margin_bottom) * (block_count - 1))) + ((block_count * block_height) - block_height) + (++line_count * line_height) - line_height;

                                if (col_count == 1 && Math.Ceiling(Convert.ToDouble(page_row_count / max_col)) * block_height + (block_count * (block_margin_top + block_margin_bottom)) > pe.MarginBounds.Bottom)
                                {
                                    pe.HasMorePages = true;
                                    page_row_count = 0;
                                    block_count = 1;
                                    row_num--;
                                    return;
                                }
                                else
                                {
                                    pe.HasMorePages = false;
                                }

                                pe.Graphics.DrawString("ส่ง", font, brush, new Point(contact_x - 30, contact_y));
                                pe.Graphics.DrawString(list_dealer[i].contact, font, brush, new Point(contact_x, contact_y));

                                int name_x = 30 + (col_count * col_width) - col_width;
                                int name_y = ((block_count == 1 ? block_margin_top : (block_margin_top + block_margin_bottom) * (block_count - 1))) + ((block_count * block_height) - block_height) + (++line_count * line_height) - line_height;
                                pe.Graphics.DrawString(list_dealer[i].prenam + " " + list_dealer[i].compnam, font, brush, new Point(name_x, name_y));

                                int addr01_x = 30 + (col_count * col_width) - col_width;
                                int addr01_y = ((block_count == 1 ? block_margin_top : (block_margin_top + block_margin_bottom) * (block_count - 1))) + ((block_count * block_height) - block_height) + (++line_count * line_height) - line_height;
                                pe.Graphics.DrawString(list_dealer[i].addr01, font, brush, new Point(addr01_x, addr01_y));

                                int addr02_x = 30 + (col_count * col_width) - col_width;
                                int addr02_y = ((block_count == 1 ? block_margin_top : (block_margin_top + block_margin_bottom) * (block_count - 1))) + ((block_count * block_height) - block_height) + (++line_count * line_height) - line_height;
                                pe.Graphics.DrawString(list_dealer[i].addr02 + " " + list_dealer[i].addr03 + " " + list_dealer[i].zipcod, font, brush, new Point(addr02_x, addr02_y));

                                line_count = 0;
                            }
                        }
                    }
                };

                if (wind.output == PrintDealerLabelOutputSelection.OUTPUT.PRINTER)
                {
                    PrintDialog print_dialog = new PrintDialog();
                    print_dialog.Document = this.print_doc;
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = false;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = true;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_doc.Print();
                    }
                }
                if (wind.output == PrintDealerLabelOutputSelection.OUTPUT.SCREEN)
                {
                    PrintPreviewDialog preview_dialog = new PrintPreviewDialog();
                    preview_dialog.Document = this.print_doc;
                    preview_dialog.MdiParent = this.main_form;
                    preview_dialog.Show();
                }
                if (wind.output == PrintDealerLabelOutputSelection.OUTPUT.FILE)
                {

                }
            }
            else
            {
                print_doc = null;
                page_setup = null;
            }
        }

        private void toolStripPrintLabel2Col_Click(object sender, EventArgs e)
        {
            print_doc = new PrintDocument();

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = this.print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("Sticker 2 column", 825, 1165);
            //page_setup.PageSettings.PaperSize = new PaperSize("Sticker 3 column", 1250, 1195);
            page_setup.PageSettings.Margins = new Margins(0, 0, 0, 0);

            PrintDealerLabelOutputSelection wind = new PrintDealerLabelOutputSelection(this.main_form);
            wind.txtFrom.Text = this.dealer.dealer;
            wind.txtTo.Text = this.dealer.dealer;
            if (wind.ShowDialog() == DialogResult.OK)
            {
                int row_num = 0;
                int page_count = 0;
                List<Dealer> list_dealer = new List<Dealer>();

                print_doc.BeginPrint += delegate(object o, PrintEventArgs pe)
                {
                    string json_data = "{\"dealer_from\":\"" + wind.dealer_from + "\",";
                    json_data += "\"dealer_to\":\"" + wind.dealer_to + "\",";
                    json_data += "\"condition\":\"" + wind.condition + "\"}";

                    CRUDResult get = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "dealer/get_for_print_label", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        if (sr.dealer.Count > 0)
                        {
                            list_dealer = sr.dealer;
                        }
                        else
                        {
                            MessageAlert.Show(StringResource.NO_DATA_IN_RANGE, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                            return;
                        }
                    }
                    else
                    {
                        MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        return;
                    }

                };

                print_doc.PrintPage += delegate(object o, PrintPageEventArgs pe)
                {
                    using (Font font = new Font("Tahoma", 9.75f))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.Black))
                        {
                            int max_col = 2; // for sticker 2 column
                            //int max_col = 3; // for sticker 3 column
                            int col_count = 0;
                            int col_width = 413;

                            int line_count = 0;
                            int line_height = 25;

                            int block_count = 1; // store row number of sticker
                            int block_height = line_height * 4;
                            int block_margin_top = 25;
                            int block_margin_bottom = 25;

                            page_count++;
                            int page_row_count = 0;

                            for (int i = row_num; i < list_dealer.Count; i++)
                            {
                                row_num++;
                                col_count++;
                                page_row_count++;

                                if (page_row_count > 1 && i % max_col == 0)
                                {
                                    col_count = 1;
                                    ++block_count;
                                }

                                int contact_x = 30 + (col_count * col_width) - col_width;
                                int contact_y = ((block_count == 1 ? block_margin_top : (block_margin_top + block_margin_bottom) * (block_count - 1))) + ((block_count * block_height) - block_height) + (++line_count * line_height) - line_height;

                                if (col_count == 1 && Math.Ceiling(Convert.ToDouble(page_row_count / max_col)) * block_height + (block_count * (block_margin_top + block_margin_bottom)) > pe.MarginBounds.Bottom)
                                {
                                    pe.HasMorePages = true;
                                    page_row_count = 0;
                                    block_count = 1;
                                    row_num--;
                                    return;
                                }
                                else
                                {
                                    pe.HasMorePages = false;
                                }

                                pe.Graphics.DrawString("ส่ง", font, brush, new Point(contact_x - 30, contact_y));
                                pe.Graphics.DrawString(list_dealer[i].contact, font, brush, new Point(contact_x, contact_y));

                                int name_x = 30 + (col_count * col_width) - col_width;
                                int name_y = ((block_count == 1 ? block_margin_top : (block_margin_top + block_margin_bottom) * (block_count - 1))) + ((block_count * block_height) - block_height) + (++line_count * line_height) - line_height;
                                pe.Graphics.DrawString(list_dealer[i].prenam + " " + list_dealer[i].compnam, font, brush, new Point(name_x, name_y));

                                int addr01_x = 30 + (col_count * col_width) - col_width;
                                int addr01_y = ((block_count == 1 ? block_margin_top : (block_margin_top + block_margin_bottom) * (block_count - 1))) + ((block_count * block_height) - block_height) + (++line_count * line_height) - line_height;
                                pe.Graphics.DrawString(list_dealer[i].addr01, font, brush, new Point(addr01_x, addr01_y));

                                int addr02_x = 30 + (col_count * col_width) - col_width;
                                int addr02_y = ((block_count == 1 ? block_margin_top : (block_margin_top + block_margin_bottom) * (block_count - 1))) + ((block_count * block_height) - block_height) + (++line_count * line_height) - line_height;
                                pe.Graphics.DrawString(list_dealer[i].addr02 + " " + list_dealer[i].addr03 + " " + list_dealer[i].zipcod, font, brush, new Point(addr02_x, addr02_y));

                                line_count = 0;
                            }
                        }
                    }
                };

                if (wind.output == PrintDealerLabelOutputSelection.OUTPUT.PRINTER)
                {
                    PrintDialog print_dialog = new PrintDialog();
                    print_dialog.Document = this.print_doc;
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = false;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = true;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_doc.Print();
                    }
                }
                if (wind.output == PrintDealerLabelOutputSelection.OUTPUT.SCREEN)
                {
                    PrintPreviewDialog preview_dialog = new PrintPreviewDialog();
                    preview_dialog.Document = this.print_doc;
                    preview_dialog.MdiParent = this.main_form;
                    preview_dialog.Show();
                }
                if (wind.output == PrintDealerLabelOutputSelection.OUTPUT.FILE)
                {

                }
            }
            else
            {
                print_doc = null;
                page_setup = null;
            }
        }

        private void toolStripPrint_ButtonClick(object sender, EventArgs e)
        {
            this.toolStripPrintLabel3Col.PerformClick();
        }

        private void toolStripReload_Click(object sender, EventArgs e)
        {
            this.GetDealerByID(this.dealer.id);
        }
        #endregion SET TOOLSTRIP ACTION

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_state == FORM_STATE.ADD || this.form_state == FORM_STATE.EDIT)
                {
                    if (this.current_focused_control == this.txtRemark)
                    {
                        this.toolStripSave.PerformClick();
                        return true;
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                }
            }
            if (keyData == Keys.Escape)
            {
                this.toolStripStop.PerformClick();
                return true;
            }
            if (keyData == Keys.Tab && this.form_state == FORM_STATE.READ)
            {
                DataInfo data_info = new DataInfo();
                data_info.lblDataTable.Text = "DEALER";
                data_info.lblExpression.Text = (this.sort_mode == SORT_MODE.DEALER ? "dealer" : this.GetSortFieldName() + "+dealer");
                data_info.lblRecBy.Text = this.dealer.users_name;
                data_info.lblRecDate.pickedDate(this.dealer.chgdat);
                data_info.lblTime.ForeColor = Color.DarkGray;
                data_info.lblRecTime.BackColor = Color.WhiteSmoke;
                data_info.lblRecNo.Text = this.dealer.id.ToString();
                data_info.lblTotalRec.Text = this.dealer_id_list.Max(t => t.id).ToString();
                data_info.ShowDialog();
                return true;
            }
            if (keyData == Keys.F3)
            {
                if (this.form_state == FORM_STATE.READ)
                {
                    this.tabControl1.SelectedTab = this.tabPage1;
                    return true;
                }
            }
            if (keyData == Keys.F4)
            {
                if (this.form_state == FORM_STATE.READ)
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
            if (keyData == Keys.PageUp && this.form_state == FORM_STATE.READ)
            {
                this.toolStripPrev.PerformClick();
                return true;
            }
            if (keyData == Keys.PageDown && this.form_state == FORM_STATE.READ)
            {
                this.toolStripNext.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.Home) && this.form_state == FORM_STATE.READ)
            {
                this.toolStripFirst.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.End) && this.form_state == FORM_STATE.READ)
            {
                this.toolStripLast.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.A))
            {
                this.toolStripAdd.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.E))
            {
                this.toolStripEdit.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D))
            {
                this.toolStripDelete.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.S))
            {
                this.toolStripSearchCode.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.L))
            {
                this.toolStripInquiryAll.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.K))
            {
                this.toolStripInquiryCondition.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.L))
            {
                this.toolStripInquiryRest.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.P))
            {
                this.toolStripPrintLabel3Col.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.P))
            {
                this.toolStripPrintLittleEnv.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D2))
            {
                this.toolStripSearchContact.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D3))
            {
                this.toolStripSearchName.PerformClick();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D4))
            {
                this.toolStripSearchArea.PerformClick();
                return true;
            }
            if (keyData == Keys.F6 && this.current_focused_control == this.txtArea && (this.form_state == FORM_STATE.ADD || this.form_state == FORM_STATE.EDIT))
            {
                this.btnBrowseArea.PerformClick();
                return true;
            }
            if (keyData == Keys.F9 && (this.form_state == FORM_STATE.ADD || this.form_state == FORM_STATE.EDIT))
            {
                this.toolStripSave.PerformClick();
                return true;
            }
            if (keyData == Keys.F7 && this.form_state == FORM_STATE.READ)
            {
                this.tabControl1.SelectedTab = this.tabPage2;
                this.dgvSerial.Focus();
                this.FormReadItemF7();
                return true;
            }
            if (keyData == Keys.F8 && this.form_state == FORM_STATE.READ)
            {
                this.tabControl1.SelectedTab = this.tabPage1;
                this.dgvRemark.Focus();
                this.FormReadItemF8();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
