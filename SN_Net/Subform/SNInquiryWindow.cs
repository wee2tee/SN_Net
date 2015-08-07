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
        private int totalRec;
        private List<Serial_list> serials = new List<Serial_list>();
        private INQUIRY_TYPE inquiry_type;
        public enum INQUIRY_TYPE
        {
            ALL,
            REST
        }
        private string sortMode;
        private List<Serial> serial_id_list;
        private List<Serial_list> serial_list = new List<Serial_list>();
        private Serial current_serial;
        private int offset = 0;
        private int limit = 30000;
        public int selected_id;

        public SNInquiryWindow(INQUIRY_TYPE inquiry_type, string sort_mode, List<Serial> serial_id_list, Serial current_serial = null)
        {
            InitializeComponent();
            this.inquiry_type = inquiry_type;
            this.serial_id_list = serial_id_list;
            this.sortMode = sort_mode;
            this.current_serial = current_serial;
        }

        private void SNInquiryWindow_Load(object sender, EventArgs e)
        {
            this.lblLoading.Dock = DockStyle.Fill;
            this.dgvSerial.Dock = DockStyle.Fill;
            this.setTitleText();
            this.dgvSerial.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.drawRowBorder);
        }

        private void drawRowBorder(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (this.dgvSerial.CurrentCell.RowIndex == e.RowIndex)
            {
                Rectangle rect = this.dgvSerial.GetRowDisplayRectangle(e.RowIndex, true);

                e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Top, rect.Right, rect.Top);
                e.Graphics.DrawLine(new Pen(Color.Red, 1f), rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
            }
        }

        private void SNInquiryWindow_Shown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //this.offset = this.getOffset();
            BackgroundWorker loadSerial_Worker = new BackgroundWorker();
            loadSerial_Worker.DoWork += new DoWorkEventHandler(this.loadSerial_Worker_Dowork);
            loadSerial_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.loadSerial_Worker_Complete);
            loadSerial_Worker.RunWorkerAsync();
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

        private void loadSerial_Worker_Dowork(object sender, DoWorkEventArgs e)
        {
            this.loadSerialPart(this.sortMode, this.offset, this.limit);
        }

        private void loadSerialPart(string sortMode, int offset, int limit)
        {
            Console.WriteLine("offset = " + offset.ToString());
            CRUDResult get = ApiActions.GET(ApiConfig.API_MAIN_URL + "serial/get_inquiry&sort=" + sortMode + "&offset=" + offset.ToString() + "&limit=" + limit.ToString());

            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.serial_list = this.serial_list.Concat(sr.serial_list).ToList();
                this.totalRec = Convert.ToInt32(sr.message);
                Console.WriteLine("totalRec = " + this.totalRec.ToString());


                offset += this.serial_list.Count;
                if (this.serial_list.Count < this.totalRec)
                {
                    this.loadSerialPart(sortMode, offset, limit);
                }
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void loadSerial_Worker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Load complete at : " + DateTime.Now.ToString());
            Console.WriteLine("serial_list.Count = " + this.serial_list.Count.ToString());

            //this.fillInDataGrid();
            this.dgvSerial.DataSource = this.serial_list;
            this.dgvSerial.Columns[4].SortMode = DataGridViewColumnSortMode.Automatic;
            //this.dgvSerial.Sort(this.dgvSerial.Columns[4], ListSortDirection.Ascending);

            this.setSelectionItem();
            this.setupDatagridStyle();
            this.dgvSerial.Visible = true;
            this.btnCancel.Enabled = true;
            this.btnOK.Enabled = true;
            this.dgvSerial.Focus();
            //this.toolStripLoadedRec.Text = this.serials.Count.ToString();
            //this.toolStripTotalRec.Text = this.totalRec.ToString();
            this.Cursor = Cursors.Default;
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
            this.dgvSerial.Columns[7].Width = 300;
            this.dgvSerial.Columns[8].Width = 80;
            this.dgvSerial.Columns[9].Width = 400;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selected_id = (int)this.dgvSerial.Rows[this.dgvSerial.CurrentCell.RowIndex].Cells[0].Value;
        }

        private void fillInDataGrid()
        {
            this.dgvSerial.Rows.Clear();
            this.dgvSerial.Columns.Clear();
            this.dgvSerial.EnableHeadersVisualStyles = false;

            #region DataGridViewColumn
            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.HeaderText = "ID";
            col0.Width = 0;
            col0.Visible = false;
            this.dgvSerial.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "SERNUM";
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            col1.Width = 120;
            col1.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "OLDCOD";
            col2.Width = 120;
            col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "VERSION";
            col3.Width = 70;
            col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "COMPNAM";
            col4.Width = 400;
            col4.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.HeaderText = "CONTACT";
            col5.Width = 350;
            col5.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.Columns.Add(col5);

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.HeaderText = "DEALER";
            col6.Width = 100;
            col6.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.Columns.Add(col6);

            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            col7.HeaderText = "TELNUM";
            col7.Width = 300;
            col7.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.Columns.Add(col7);

            DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
            col8.HeaderText = "BUSITYP";
            col8.Width = 80;
            col8.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.Columns.Add(col8);

            DataGridViewTextBoxColumn col9 = new DataGridViewTextBoxColumn();
            col9.HeaderText = "BUSIDES";
            col9.Width = 300;
            col9.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                BackColor = ColorResource.COLUMN_HEADER_NOT_SORTABLE_GREEN,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3)
            };
            this.dgvSerial.Columns.Add(col9);
            #endregion DataGridViewColumn

            #region DataGridViewRow
            foreach (Serial_list s in this.serials)
            {
                int r = this.dgvSerial.Rows.Add();
                DataGridViewRow row = this.dgvSerial.Rows[r];
                row.Tag = s;
                row.Height = 25;

                row.Cells[0].ValueType = typeof(int);
                row.Cells[0].Value = s.ID;

                row.Cells[1].ValueType = typeof(string);
                row.Cells[1].Value = s.SERNUM;
                row.Cells[1].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                row.Cells[2].ValueType = typeof(string);
                row.Cells[2].Value = s.OLDCOD;
                row.Cells[2].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                row.Cells[3].ValueType = typeof(string);
                row.Cells[3].Value = s.VERSION;
                row.Cells[3].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                row.Cells[4].ValueType = typeof(string);
                row.Cells[4].Value = s.COMPNAM;
                row.Cells[4].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                row.Cells[5].ValueType = typeof(string);
                row.Cells[5].Value = s.CONTACT;
                row.Cells[5].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                row.Cells[6].ValueType = typeof(string);
                row.Cells[6].Value = s.DEALER;
                row.Cells[6].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                row.Cells[7].ValueType = typeof(string);
                row.Cells[7].Value = s.TELNUM;
                row.Cells[7].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                row.Cells[8].ValueType = typeof(string);
                row.Cells[8].Value = s.BUSITYP;
                row.Cells[8].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };

                row.Cells[9].ValueType = typeof(string);
                row.Cells[9].Value = s.BUSIDES;
                row.Cells[9].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black
                };
            }
            #endregion DataGridViewRow

            //this.setSelectionItem();
            this.dgvSerial.Focus();
        }

        private void setSelectionItem()
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
