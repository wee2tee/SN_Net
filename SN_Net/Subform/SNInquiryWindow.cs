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
        private List<Serial> serial_id_list;
        private string sortMode;
        private int limit = 50;
        private int offset = 0;
        private INQUIRY_TYPE inquiry_type;
        private int h_scroll_pos = 0;

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
            this.serial_id_list = parentWindow.serial_id_list;
            this.current_serial = parentWindow.serial;
            this.sortMode = parentWindow.sortMode;
            this.dgvSerial.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.drawRowBorder);
            this.dgvSerial.Paint += new PaintEventHandler(this.loadPreviousWhilePaint);
            this.dgvSerial.MouseWheel += new MouseEventHandler(this.mouseWheelHandler);

            if (this.inquiry_type == INQUIRY_TYPE.REST)
            {
                this.inquiryRest();
            }
            else
            {
                this.inquireAll();
            }
        }

        private void inquireAll()
        {
            if (this.serial_list.Count == 0)
            {
                string ids = string.Empty;
                for (int i = 0; i <= 100; i++)
                {
                    if (i == 0)
                    {
                        ids += this.serial_id_list[i].id.ToString();
                    }
                    else
                    {
                        ids += "," + this.serial_id_list[i].id.ToString();
                    }
                }

                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/get_inquiry&sort=" + this.sortMode + "&ids=" + ids);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    if (sr.serial_list.Count > 0)
                    {
                        this.serial_list = sr.serial_list;
                        this.toolStripLoadedRec.Text = this.serial_list.Count.ToString();
                        this.toolStripTotalRec.Text = this.serial_id_list.Count.ToString();
                    }
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void inquiryRest()
        {
            if (this.serial_list.Count == 0)
            {
                string ids = string.Empty;
                int current_ndx = this.serial_id_list.FindIndex(t => t.id == this.current_serial.id);
                int start_ndx = (current_ndx - 49 < 0 ? 0 : current_ndx - 49);
                int stop_ndx = (current_ndx + 50 > this.serial_id_list.Count - 1 ? this.serial_id_list.Count - 1 : current_ndx + 50);
                for (int i = start_ndx; i <= stop_ndx; i++)
                {
                    if (i == start_ndx)
                    {
                        ids += this.serial_id_list[i].id.ToString();
                    }
                    else
                    {
                        ids += "," + this.serial_id_list[i].id.ToString();
                    }
                }

                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/get_inquiry&sort=" + this.sortMode + "&ids=" + ids);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    if (sr.serial_list.Count > 0)
                    {
                        this.serial_list = sr.serial_list;
                        this.toolStripLoadedRec.Text = this.serial_list.Count.ToString();
                        this.toolStripTotalRec.Text = this.serial_id_list.Count.ToString();
                    }
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }

        }

        private void inquiryPrevious()
        {
            string ids = string.Empty;
            int first_item_id = this.serial_list.First<Serial_list>().ID;
            int first_item_ndx = this.serial_id_list.FindIndex(t => t.id == first_item_id);
            if (first_item_ndx > 0)
            {
                int rows_to_load = Convert.ToInt32(this.dgvSerial.ClientSize.Height / 25) + 10;

                int start_ndx = (first_item_ndx - rows_to_load < 0 ? 0 : first_item_ndx - rows_to_load);
                int stop_ndx = first_item_ndx - 1;
                for (int i = start_ndx; i <= stop_ndx; i++)
                {
                    if (i == start_ndx)
                    {
                        ids += this.serial_id_list[i].id.ToString();
                    }
                    else
                    {
                        ids += "," + this.serial_id_list[i].id.ToString();
                    }
                }

                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/get_inquiry&sort=" + this.sortMode + "&ids=" + ids);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    if (sr.serial_list.Count > 0)
                    {
                        int current_item_id = (int)this.dgvSerial.Rows[this.dgvSerial.CurrentCell.RowIndex].Cells[0].Value;
                        this.serial_list = sr.serial_list.Concat<Serial_list>(this.serial_list).ToList<Serial_list>();
                        this.dgvSerial.DataSource = this.serial_list;
                        int current_item_ndx = this.serial_list.FindIndex(t => t.ID == current_item_id);
                        this.dgvSerial.Rows[current_item_ndx].Cells[1].Selected = true;
                        this.dgvSerial.HorizontalScrollingOffset = this.h_scroll_pos;
                        this.toolStripLoadedRec.Text = this.serial_list.Count.ToString();
                    }
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void inquiryNext()
        {
            string ids = string.Empty;
            int last_item_id = this.serial_list.Last<Serial_list>().ID;
            int last_item_ndx = this.serial_id_list.FindIndex(t => t.id == last_item_id);
            if (last_item_ndx < this.serial_id_list.Count - 1)
            {
                int rows_to_load = Convert.ToInt32(this.dgvSerial.ClientSize.Height / 25) + 10; // +(this.serial_id_list.Count - last_item_ndx < 100 ? this.serial_id_list.Count - last_item_ndx : 0);

                int start_ndx = last_item_ndx + 1;
                //int stop_ndx = (start_ndx + rows_to_load > this.serial_id_list.Count - 1 ? (this.serial_id_list.Count - 1) - start_ndx : start_ndx + rows_to_load);
                int stop_ndx = (start_ndx + rows_to_load > this.serial_id_list.Count - 1 ? this.serial_id_list.Count - 1 : start_ndx + rows_to_load);
                for (int i = start_ndx; i <= stop_ndx; i++)
                {
                    if (i == start_ndx)
                    {
                        ids += this.serial_id_list[i].id.ToString();
                    }
                    else
                    {
                        ids += "," + this.serial_id_list[i].id.ToString();
                    }
                }

                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "serial/get_inquiry&sort=" + this.sortMode + "&ids=" + ids);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    if (sr.serial_list.Count > 0)
                    {
                        int current_item_id = (int)this.dgvSerial.Rows[this.dgvSerial.CurrentCell.RowIndex].Cells[0].Value;
                        this.serial_list = this.serial_list.Concat<Serial_list>(sr.serial_list).ToList<Serial_list>();
                        this.dgvSerial.DataSource = this.serial_list;
                        int current_item_ndx = this.serial_list.FindIndex(t => t.ID == current_item_id);
                        this.dgvSerial.Rows[current_item_ndx].Cells[1].Selected = true;
                        this.dgvSerial.HorizontalScrollingOffset = this.h_scroll_pos;
                        this.toolStripLoadedRec.Text = this.serial_list.Count.ToString();
                    }
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void drawRowBorder(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (this.dgvSerial.CurrentCell.RowIndex == e.RowIndex)
            {
                Rectangle rect = this.dgvSerial.GetRowDisplayRectangle(e.RowIndex, true);

                e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Top, rect.Right, rect.Top);
                e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);

                this.toolStripSelectedID.Text = "Current row ID : " + this.dgvSerial.Rows[e.RowIndex].Cells[0].Value.ToString();
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

        private void mouseWheelHandler(object sender, MouseEventArgs e)
        {
            
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
                if (this.current_serial != null && this.serial_list.Count > 0)
                {
                    int ndx = this.serial_list.FindIndex(t => t.ID == this.current_serial.id);
                    this.dgvSerial.Rows[ndx].Cells[1].Selected = true;
                }
                else
                {
                    if (this.dgvSerial.Rows.Count > 0)
                    {
                        this.dgvSerial.Rows[0].Cells[1].Selected = true;
                    }
                }
            }
            else
            {
                if (this.serial_list != null && this.serial_list.Count > 0)
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

        private void dgvSerial_Scroll(object sender, ScrollEventArgs e)
        {

            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                int first_displayed_ndx = this.dgvSerial.FirstDisplayedScrollingRowIndex;
                this.dgvSerial.Rows[first_displayed_ndx].Cells[1].Selected = true;
                this.dgvSerial.HorizontalScrollingOffset = this.h_scroll_pos;
            }
        }

        private void loadPreviousWhilePaint(object sender, PaintEventArgs e)
        {
            if (this.dgvSerial.FirstDisplayedScrollingRowIndex == 0)
            {
                this.inquiryPrevious();
            }
            else if (this.dgvSerial.FirstDisplayedScrollingRowIndex > this.serial_list.Count - 60)
            {
                this.inquiryNext();
            }
        }

        private void dgvSerial_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            this.h_scroll_pos = this.dgvSerial.HorizontalScrollingOffset;
        }
    }
}
