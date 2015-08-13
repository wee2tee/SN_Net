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
    public partial class SNInquiryWindow : Form
    {
        private SnWindow parentWindow;
        private List<Serial_list> serials = new List<Serial_list>();
        private INQUIRY_TYPE inquiry_type;
        public enum INQUIRY_TYPE
        {
            ALL,
            REST
        }
        private List<Serial_list> serial_list = new List<Serial_list>();
        private Serial current_serial;
        public int selected_id;

        public SNInquiryWindow()
        {
            InitializeComponent();
        }

        public SNInquiryWindow(SnWindow parentWindow, INQUIRY_TYPE inquiry_type)
            : this()
        {
            this.parentWindow = parentWindow;
            this.inquiry_type = inquiry_type;
        }

        private void SNInquiryWindow_Load(object sender, EventArgs e)
        {
            this.lblLoading.Dock = DockStyle.Fill;
            this.dgvSerial.Dock = DockStyle.Fill;
            this.setTitleText();
            this.serial_list = parentWindow.serial_list;
            this.current_serial = parentWindow.serial;
            this.dgvSerial.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.drawRowBorder);

            CRUDResult get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_change_today");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                foreach (Serial_list sl in sr.serial_list)
                {
                    int target_ndx = this.serial_list.FindIndex(t => t.ID == sl.ID);

                    if (target_ndx >= 0)
                    {
                        this.serial_list[target_ndx] = sl;
                    }
                    else
                    {
                        this.serial_list.Add(sl);
                    }
                }
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void drawRowBorder(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (this.dgvSerial.CurrentCell.RowIndex == e.RowIndex)
            {
                Rectangle rect = this.dgvSerial.GetRowDisplayRectangle(e.RowIndex, true);

                e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Top, rect.Right, rect.Top);
                e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);

                this.toolStripSelectedID.Text = "row id : " + this.dgvSerial.Rows[e.RowIndex].Cells[0].Value.ToString() + " of " + this.serial_list.Count.ToString();
            }
        }

        
        private void SNInquiryWindow_Shown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.dgvSerial.DataSource = this.serial_list;
            this.setSelectionItem();
            this.setupDatagridStyle();
            this.dgvSerial.Visible = true;
            this.btnCancel.Enabled = true;
            this.btnOK.Enabled = true;
            this.dgvSerial.Focus();
            this.Cursor = Cursors.Default;
        }

        private void setTitleText()
        {
            if (this.inquiry_type == INQUIRY_TYPE.ALL)
            {
                this.Text = "Inquiry All";
            }
            else
            {
                this.Text = "Inquiry Rest";
            }
        }

        
        private void setupDatagridStyle()
        {
            this.dgvSerial.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.DefaultCellStyle = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f),
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.White,
                SelectionForeColor = Color.Black,
                //Padding = new Padding(3),
                WrapMode = DataGridViewTriState.False
            };

            this.dgvSerial.Columns[0].Visible = false;
            this.dgvSerial.Columns[1].Width = 120;
            this.dgvSerial.Columns[2].Width = 120;
            this.dgvSerial.Columns[3].Width = 80;
            this.dgvSerial.Columns[4].Width = 400;
            this.dgvSerial.Columns[5].Width = 350;
            this.dgvSerial.Columns[6].Width = 100;
            this.dgvSerial.Columns[7].Width = 100;
            this.dgvSerial.Columns[8].Width = 300;
            this.dgvSerial.Columns[9].Width = 80;
            this.dgvSerial.Columns[10].Width = 400;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selected_id = (int)this.dgvSerial.Rows[this.dgvSerial.CurrentCell.RowIndex].Cells[0].Value;
        }

        private void setSelectionItem()
        {
            if (this.inquiry_type == INQUIRY_TYPE.REST)
            {
                if (this.current_serial != null)
                {
                    foreach (DataGridViewRow row in this.dgvSerial.Rows)
                    {
                        if ((int)row.Cells[0].Value == this.current_serial.id)
                        {
                            row.Cells[1].Selected = true;
                            break;
                        }
                    }
                }
                else
                {
                    this.dgvSerial.Rows[0].Cells[1].Selected = true;
                }
            }
            else
            {
                if (this.serial_list != null)
                {
                    this.dgvSerial.Rows[0].Cells[1].Selected = true;
                }
            }
        }

        private void SNInquiryWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOK.PerformClick();
            }
        }

        private void dgvSerial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnOK.PerformClick();
        }
    }
}
