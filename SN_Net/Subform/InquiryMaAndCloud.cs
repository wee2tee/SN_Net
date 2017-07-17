using Newtonsoft.Json;
using SN_Net.MiscClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebAPI;
using WebAPI.ApiResult;
using SN_Net.DataModels;

namespace SN_Net.Subform
{
    public enum INQUIRY_SERVICE_TYPE
    {
        MA,
        CLOUD
    }

    public partial class InquiryMaAndCloud : Form
    {
        private SnWindow parent_window;
        private INQUIRY_SERVICE_TYPE inquiry_service_type;
        private bool inquiry_sort_asc = true;
        private BindingSource bs;
        private List<MACloud> macloud_list;
        private List<MACloud> macloud_list_filtered;
        public int selected_id;

        public InquiryMaAndCloud()
        {
            InitializeComponent();
        }

        public InquiryMaAndCloud(SnWindow parent_window, INQUIRY_SERVICE_TYPE inquiry_service_type)
            : this()
        {
            this.parent_window = parent_window;
            this.inquiry_service_type = inquiry_service_type;
        }

        private void InquiryMaAndCloud_Load(object sender, EventArgs e)
        {
            switch (this.inquiry_service_type)
            {
                case INQUIRY_SERVICE_TYPE.MA:
                    this.Text = "รายชื่อลูกค้าใช้บริการ MA.";
                    break;
                case INQUIRY_SERVICE_TYPE.CLOUD:
                    this.Text = "รายชื่อลูกค้าใช้บริการ Express on Cloud";
                    break;
                default:
                    this.Text = "";
                    break;
            }

            this.bs = new BindingSource();
            this.bs.DataSource = this.macloud_list_filtered;
            this.dgvSerial.DataSource = this.bs;

            switch (this.inquiry_service_type)
            {
                case INQUIRY_SERVICE_TYPE.MA:
                    this.GetServerData("ma/get_ma_inquiry");
                    break;
                case INQUIRY_SERVICE_TYPE.CLOUD:
                    this.GetServerData("cloudsrv/get_cloudsrv_inquiry");
                    break;
                default:
                    break;
            }
            
        }

        private void GetServerData(string route_path)
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + route_path);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            this.macloud_list = sr.macloud_list;
            this.macloud_list_filtered = this.macloud_list.FindAll(m => m.SERIAL_ID > -1);
            this.bs.DataSource = this.macloud_list_filtered;
            this.bs.ResetBindings(true);
        }

        private void dgvSerial_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.dgvSerial.CurrentCell != null)
            {
                this.lblRowPos.Text = "Row : " + (this.dgvSerial.CurrentCell.RowIndex + 1).ToString() + "/" + this.macloud_list_filtered.Count.ToString();
            }
        }

        private void InquiryMaAndCloud_Activated(object sender, EventArgs e)
        {
            if (this.macloud_list_filtered.Find(m => m.SERIAL_ID == this.parent_window.serial.id) != null)
            {
                this.dgvSerial.CurrentCell = this.dgvSerial.Rows.Cast<DataGridViewRow>().Where(r => (int)(r.Cells["colId"].Value) == this.parent_window.serial.id).First().Cells[1];
            }
        }

        private void numPeriod_ValueChanged(object sender, EventArgs e)
        {
            if (this.numPeriod.Value == -1)
            {
                if (this.inquiry_sort_asc)
                {
                    this.macloud_list_filtered = this.macloud_list.Where(m => m.SERIAL_ID > -1).OrderBy(m => m.END_DATE).ToList();
                }
                else
                {
                    this.macloud_list_filtered = this.macloud_list.Where(m => m.SERIAL_ID > -1).OrderByDescending(m => m.END_DATE).ToList();
                }
            }
            else
            {
                if (this.inquiry_sort_asc)
                {
                    this.macloud_list_filtered = this.macloud_list.Where(m => (m.END_DATE - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).TotalDays >= 0 && (m.END_DATE - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).TotalDays <= (int)this.numPeriod.Value).OrderBy(m => m.END_DATE).ToList();
                }
                else
                {
                    this.macloud_list_filtered = this.macloud_list.Where(m => (m.END_DATE - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).TotalDays >= 0 && (m.END_DATE - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).TotalDays <= (int)this.numPeriod.Value).OrderByDescending(m => m.END_DATE).ToList();
                }
            }

            this.bs.DataSource = this.macloud_list_filtered;
            this.bs.ResetBindings(true);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selected_id = (int)this.dgvSerial.Rows[this.dgvSerial.CurrentCell.RowIndex].Cells["colId"].Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
            }

            if (keyData == Keys.Enter)
            {
                if (this.numPeriod.Focused)
                    return false;

                if (this.dgvSerial.CurrentCell == null)
                    return false;

                this.btnOK.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvSerial_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex < 0 && this.dgvSerial.Columns[e.ColumnIndex].Name == "col_EndDate")
            {
                this.inquiry_sort_asc = !this.inquiry_sort_asc;
                if (this.inquiry_sort_asc)
                {
                    this.macloud_list_filtered = this.macloud_list_filtered.OrderBy(m => m.END_DATE).ToList();
                }
                else
                {
                    this.macloud_list_filtered = this.macloud_list_filtered.OrderByDescending(m => m.END_DATE).ToList();
                }

                this.bs.DataSource = this.macloud_list_filtered;
                this.bs.ResetBindings(true);
            }
        }

        private void dgvSerial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.btnOK.PerformClick();
            }
        }

        private void dgvSerial_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && this.dgvSerial.Columns[e.ColumnIndex].Name == "col_EndDate")
            {
                e.Paint(e.ClipBounds, (DataGridViewPaintParts.All));
                using (SolidBrush b = new SolidBrush(Color.FromArgb(255, 191, 191, 191)))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(e.CellBounds.X, e.CellBounds.Y + 2, e.CellBounds.Width - 1, e.CellBounds.Height - 4));
                }
                e.PaintContent(e.ClipBounds);

                Rectangle rect = e.CellBounds;

                using (SolidBrush b = new SolidBrush(Color.Black))
                {
                    Point p1, p2, p3;
                    if (this.inquiry_sort_asc)
                    {
                        p1 = new Point(rect.X + rect.Width - 20, rect.Y + 20);
                        p2 = new Point(rect.X + rect.Width - 12, rect.Y + 20);
                        p3 = new Point(rect.X + rect.Width - 16, rect.Y + 12);
                    }
                    else
                    {
                        p1 = new Point(rect.X + rect.Width - 20, rect.Y + 12);
                        p2 = new Point(rect.X + rect.Width - 12, rect.Y + 12);
                        p3 = new Point(rect.X + rect.Width - 16, rect.Y + 20);
                    }

                    e.Graphics.FillPolygon(b, new Point[] { p1, p2, p3 });
                }
                e.Handled = true;
            }
        }
    }
}
