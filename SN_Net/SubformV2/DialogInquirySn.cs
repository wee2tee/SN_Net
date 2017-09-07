using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.Model;
using SN_Net.MiscClass;
using CC;

namespace SN_Net.Subform
{
    public partial class DialogInquirySn : Form
    {
        public enum DIRECTION
        {
            ASC,
            DESC
        }
        public enum INQUIRY
        {
            REST,
            ALL
        }
        public enum SORT_BY
        {
            SERNUM,
            CONTACT,
            COMPNAM,
            DEALER,
            OLDNUM,
            BUSITYP,
            AREA
        }
        public enum INQUIRY_FILTER
        {
            ALL,
            MA,
            CLOUD
        }
        private INQUIRY inquiry_sign;
        private SORT_BY sort_by;
        private INQUIRY_FILTER inquiry_filter;
        public serial selected_serial = null;
        private List<SerialId> serial_id_list = null;
        private BindingList<serialVM> serial_list;

        public DialogInquirySn(SORT_BY sort_by, serial init_selected_serial = null, INQUIRY_FILTER inquiry_filter = INQUIRY_FILTER.ALL)
        {
            InitializeComponent();
            if(init_selected_serial != null)
            {
                this.inquiry_sign = INQUIRY.REST;
                this.Text = "Inquiry Rest";
                this.selected_serial = init_selected_serial;
            }
            else
            {
                this.inquiry_sign = INQUIRY.ALL;
                this.Text = "Inquiry All";
            }

            this.sort_by = sort_by;
            this.inquiry_filter = inquiry_filter;

            if (this.inquiry_filter == INQUIRY_FILTER.CLOUD)
                this.Text = "Inquiry for Cloud";

            if (this.inquiry_filter == INQUIRY_FILTER.MA)
                this.Text = "Inquiry for MA";
        }

        private void DialogInquirySn_Load(object sender, EventArgs e)
        {
            this.serial_id_list = this.GetSerialIdList(this.sort_by);

            if(this.inquiry_filter == INQUIRY_FILTER.ALL)
            {
                if (this.inquiry_sign == INQUIRY.ALL)
                {
                    try
                    {
                        int[] ids = this.serial_id_list.GetRange(0, 200).Select(s => s.id).ToArray<int>();
                        this.serial_list = new BindingList<serialVM>(this.GetRestSerial(ids).ToViewModel());
                        this.dgv.DataSource = this.serial_list;
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }

                if (this.inquiry_sign == INQUIRY.REST)
                {
                    try
                    {
                        int curr_ndx = this.serial_id_list.IndexOf(this.serial_id_list.Where(s => s.id == this.selected_serial.id).FirstOrDefault());
                        int start_ndx = curr_ndx < 50 ? 0 : curr_ndx - 50;
                        int end_ndx = start_ndx + 100 > this.serial_id_list.Count - 1 ? this.serial_id_list.Count - start_ndx : 100;
                        int[] ids = this.serial_id_list.GetRange(start_ndx, end_ndx).Select(s => s.id).ToArray<int>();
                        this.serial_list = new BindingList<serialVM>(this.GetRestSerial(ids).ToViewModel());
                        this.dgv.DataSource = this.serial_list;
                        this.dgv.FirstDisplayedScrollingRowIndex = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == this.selected_serial.id).First().Index;
                        this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == this.selected_serial.id).First().Cells[this.col_compnam.Name].Selected = true;
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }

                return;
            }

            if(this.inquiry_filter == INQUIRY_FILTER.MA || this.inquiry_filter == INQUIRY_FILTER.CLOUD)
            {
                try
                {
                    int[] ids;
                    if (this.serial_id_list.Count > 200)
                    {
                        ids = this.serial_id_list.GetRange(0, 200).Select(s => s.id).ToArray<int>();
                        this.serial_list = new BindingList<serialVM>(this.GetRestSerial(ids).ToViewModel());
                    }
                    else
                    {
                        ids = this.serial_id_list.GetRange(0, this.serial_id_list.Count).Select(s => s.id).ToArray<int>();
                        this.serial_list = new BindingList<serialVM>(this.GetRestSerial(ids).ToViewModel());
                    }
                    this.dgv.DataSource = this.serial_list;
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
                return;
            }
        }

        private List<SerialId> GetSerialIdList(SORT_BY sort_by)
        {
            if(this.inquiry_filter == INQUIRY_FILTER.ALL)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    switch (sort_by)
                    {
                        case SORT_BY.SERNUM:
                            return sn.serial.Where(s => s.flag == 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id }).ToList();
                        case SORT_BY.CONTACT:
                            return sn.serial.Where(s => s.flag == 0).OrderBy(s => s.contact).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id }).ToList();
                        case SORT_BY.COMPNAM:
                            return sn.serial.Where(s => s.flag == 0).OrderBy(s => s.compnam).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.compnam + ";" + s.sernum }).ToList();
                        case SORT_BY.DEALER:
                            return sn.serial.Include("dealer").Where(s => s.flag == 0).OrderBy(s => s.dealer_id.HasValue ? s.dealer.dealercod : "").ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.dealer.dealercod + ";" + s.sernum }).ToList();
                        case SORT_BY.OLDNUM:
                            return sn.serial.Where(s => s.flag == 0).OrderBy(s => s.oldnum).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.oldnum + ";" + s.sernum }).ToList();
                        case SORT_BY.BUSITYP:
                            return sn.serial.Include("istab1").Where(s => s.flag == 0).OrderBy(s => s.istab1.typcod).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.istab1.typcod + ";" + s.sernum }).ToList();
                        case SORT_BY.AREA:
                            return sn.serial.Include("istab").Where(s => s.flag == 0).OrderBy(s => s.istab.typcod).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.istab.typcod + ";" + s.sernum }).ToList();
                        default:
                            return sn.serial.Where(s => s.flag == 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.sernum }).ToList();
                    }
                }
            }
            
            if(this.inquiry_filter == INQUIRY_FILTER.MA)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    return sn.serial.Include("ma").Where(s => s.flag == 0).Where(s => s.ma.AsEnumerable().Count() > 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.sernum }).ToList();
                }
            }

            if(this.inquiry_filter == INQUIRY_FILTER.CLOUD)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    return sn.serial.Include("cloud_srv").Where(s => s.flag == 0).Where(s => s.cloud_srv.AsEnumerable().Count() > 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.sernum }).ToList();
                }
            }

            return null;
        }

        private List<serial> GetRestSerial(int[] ids)
        {
            using (snEntities sn = DBX.DataSet())
            {
                var ser = sn.serial.Include("dealer").Include("istab").Include("istab1").Where(s => ids.Contains(s.id)).ToList();

                switch (this.sort_by)
                {
                    case SORT_BY.SERNUM:
                        return ser.OrderBy(s => s.sernum).ToList();
                    case SORT_BY.CONTACT:
                        return ser.OrderBy(s => s.contact).ThenBy(s => s.sernum).ToList();
                    case SORT_BY.COMPNAM:
                        return ser.OrderBy(s => s.compnam).ThenBy(s => s.sernum).ToList();
                    case SORT_BY.DEALER:
                        return ser.OrderBy(s => s.dealer_id.HasValue ? s.dealer.dealercod : "").ThenBy(s => s.sernum).ToList();
                    case SORT_BY.OLDNUM:
                        return ser.OrderBy(s => s.oldnum).ThenBy(s => s.sernum).ToList();
                    case SORT_BY.BUSITYP:
                        return ser.OrderBy(s => s.istab1.typcod).ThenBy(s => s.sernum).ToList();
                    case SORT_BY.AREA:
                        return ser.OrderBy(s => s.istab.typcod).ThenBy(s => s.sernum).ToList();
                    default:
                        return ser.OrderBy(s => s.sernum).ToList();
                }
            }
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((XDatagrid)sender).CurrentCell == null)
                return;

            if (((XDatagrid)sender).Rows.Count != this.serial_list.Count)
                return;

            this.selected_serial = (serial)((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex].Cells[this.col_serial.Name].Value;

            // Load previous
            if (((XDatagrid)sender).CurrentCell.RowIndex == 0)
            {
                // is first row existing shown
                if ((int)((XDatagrid)sender).Rows[0].Cells[this.col_id.Name].Value == this.serial_id_list.First().id)
                    return;

                this.ShowLoadingBox();
                ((XDatagrid)sender).Enabled = false;
                this.btnOK.Enabled = false;
                this.btnCancel.Enabled = false;
                int focused_id = (int)((XDatagrid)sender).Rows[((XDatagrid)sender).FirstDisplayedScrollingRowIndex].Cells[this.col_id.Name].Value;

                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += delegate
                {
                    var first_row_ndx = this.serial_id_list.IndexOf(this.serial_id_list.Where(s => s.id == (int)((XDatagrid)sender).Rows[0].Cells[this.col_id.Name].Value).First());

                    int from_ndx = first_row_ndx >= 100 ? first_row_ndx - 100 : 0;
                    int range = first_row_ndx >= 100 ? 100 : first_row_ndx;
                    int[] ids = this.serial_id_list.GetRange(from_ndx, range).Select(s => s.id).ToArray();
                    List<serialVM> ser = this.GetRestSerial(ids).ToViewModel();
                    ser.Reverse();
                    ((XDatagrid)sender).Invoke(new Action(() => {
                        BindingList<serialVM> sl = (BindingList<serialVM>)((XDatagrid)sender).DataSource;
                        foreach (var item in ser)
                        {
                            sl.Insert(0, item);
                        }
                    }));
                };
                wrk.RunWorkerCompleted += delegate
                {
                    this.HideLoadingBox();
                    ((XDatagrid)sender).FirstDisplayedScrollingRowIndex = ((XDatagrid)sender).Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == focused_id).First().Index;
                    ((XDatagrid)sender).Enabled = true;
                    this.btnOK.Enabled = true;
                    this.btnCancel.Enabled = true;
                    ((XDatagrid)sender).Focus();
                };
                wrk.RunWorkerAsync();
            }

            // Load next
            if(((XDatagrid)sender).FirstDisplayedScrollingRowIndex > ((XDatagrid)sender).Rows.Count - 50)
            {
                // is last row existing shown
                if ((int)((XDatagrid)sender).Rows[((XDatagrid)sender).Rows.Count - 1].Cells[this.col_id.Name].Value == this.serial_id_list.Last().id)
                    return;

                this.ShowLoadingBox();
                ((XDatagrid)sender).Enabled = false;
                this.btnOK.Enabled = false;
                this.btnCancel.Enabled = false;
                int focused_id = (int)((XDatagrid)sender).Rows[((XDatagrid)sender).FirstDisplayedScrollingRowIndex].Cells[this.col_id.Name].Value;

                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += delegate
                {
                    var last_row_ndx = this.serial_id_list.IndexOf(this.serial_id_list.Where(s => s.id == (int)((XDatagrid)sender).Rows[((XDatagrid)sender).Rows.Count - 1].Cells[this.col_id.Name].Value).First());

                    int from_ndx = last_row_ndx + 1;
                    int range = this.serial_id_list.Count - from_ndx >= 100 ? 100 : this.serial_id_list.Count - from_ndx;
                    int[] ids = this.serial_id_list.GetRange(from_ndx, range).Select(s => s.id).ToArray();
                    List<serialVM> ser = this.GetRestSerial(ids).ToViewModel();
                    ((XDatagrid)sender).Invoke(new Action(() =>
                    {
                        BindingList<serialVM> sl = (BindingList<serialVM>)((XDatagrid)sender).DataSource;
                        foreach (var item in ser)
                        {
                            sl.Insert(sl.Count, item);
                        }
                    }));
                };
                wrk.RunWorkerCompleted += delegate
                {
                    this.HideLoadingBox();
                    ((XDatagrid)sender).FirstDisplayedScrollingRowIndex = ((XDatagrid)sender).Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == focused_id).First().Index;
                    ((XDatagrid)sender).Enabled = true;
                    this.btnOK.Enabled = true;
                    this.btnCancel.Enabled = true;
                    ((XDatagrid)sender).Focus();
                };
                wrk.RunWorkerAsync();
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
                return true;
            }

            if(keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                int col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_sernum.Name).First().Index;
                switch (this.sort_by)
                {
                    case SORT_BY.SERNUM:
                        break;
                    case SORT_BY.CONTACT:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_contact.Name).First().Index;
                        break;
                    case SORT_BY.COMPNAM:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_compnam.Name).First().Index;
                        break;
                    case SORT_BY.DEALER:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_dealer.Name).First().Index;
                        break;
                    case SORT_BY.OLDNUM:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_oldcod.Name).First().Index;
                        break;
                    case SORT_BY.BUSITYP:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_busityp.Name).First().Index;
                        break;
                    case SORT_BY.AREA:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_area.Name).First().Index;
                        break;
                    default:
                        col_index = ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_sernum.Name).First().Index;
                        break;
                }

                if(e.ColumnIndex == col_index)
                {
                    using (SolidBrush brush = new SolidBrush(Color.DarkOliveGreen))
                    {
                        e.CellStyle.BackColor = Color.YellowGreen;
                        e.CellStyle.SelectionBackColor = Color.YellowGreen;
                        e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                        e.Graphics.FillPolygon(brush, new Point[] { new Point(e.CellBounds.X + e.CellBounds.Width - 18, e.CellBounds.Y + 18), new Point(e.CellBounds.X + e.CellBounds.Width - 10, e.CellBounds.Y + 18), new Point(e.CellBounds.X + e.CellBounds.Width - 14, e.CellBounds.Y + 10) });
                        e.Handled = true;
                    }
                }
            }
        }
    }

    public class SerialId
    {
        public int id { get; set; }
        public string value { get; set; }
    }
}
