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
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Globalization;

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
            this.serial_id_list = GetSerialIdList(this.sort_by, this.inquiry_filter);

            /****************************/
            //this.xDatagrid1.DataSource = this.serial_id_list;
            /****************************/

            if(this.inquiry_filter == INQUIRY_FILTER.ALL)
            {
                if (this.inquiry_sign == INQUIRY.ALL)
                {
                    try
                    {
                        int[] ids = this.serial_id_list.GetRange(0, 200).Select(s => s.id).ToArray<int>();
                        this.serial_list = new BindingList<serialVM>(this.GetRestSerial(ids).ToViewModel());
                        this.dgv.DataSource = this.serial_list;

                        if(this.dgv.Rows.Count > 0)
                            this.selected_serial = (serial)this.dgv.Rows[0].Cells[this.col_serial.Name].Value;
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

                    if (this.dgv.Rows.Count > 0)
                        this.selected_serial = (serial)this.dgv.Rows[0].Cells[this.col_serial.Name].Value;
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
                return;
            }
        }

        public static List<SerialId> GetSerialIdList(SORT_BY sort_by, INQUIRY_FILTER filter)
        {
            if(filter == INQUIRY_FILTER.ALL)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    List<SerialId> ids = null;
                    switch (sort_by)
                    {
                        case SORT_BY.SERNUM:
                            //ids = sn.Database.SqlQuery<SerialId>("Select id, sernum From serial Where flag=0 Order By sernum ASC").ToList<SerialId>();
                            ids = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id, sernum = s.sernum }).ToList();
                            return ids;
                        case SORT_BY.CONTACT:
                            //ids = sn.Database.SqlQuery<SerialId>("Select id From serial Where flag=0 Order By flag ASC, contact ASC, sernum ASC").ToList<SerialId>(); // Order By contact ASC, sernum ASC
                            ids = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.contact).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, contact = s.contact, sernum = s.sernum }).ToList();
                            return ids;
                        case SORT_BY.COMPNAM:
                            //ids = sn.Database.SqlQuery<SerialId>("Select id, compnam, sernum From serial Where flag=0 Order By compnam ASC, sernum ASC").ToList<SerialId>();
                            ids = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.compnam).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, compnam = s.compnam, sernum = s.sernum }).ToList();
                            return ids.Where(s => s.flag == 0).ToList();
                        case SORT_BY.DEALER:
                            //ids = sn.Database.SqlQuery<SerialId>("Select serial.id as id, dealer.dealercod as dealercod, serial.sernum as sernum From serial Left Join dealer On serial.dealer_id = dealer.id And serial.flag = 0 Order By dealercod ASC, sernum ASC").Select(s => new SerialId { id = s.id, dealercod = s.dealercod != null ? s.dealercod : "", sernum = s.sernum }).ToList();
                            ids = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.dealercod).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, dealercod = s.dealercod, sernum = s.sernum }).ToList();
                            return ids;
                        case SORT_BY.OLDNUM:
                            //ids = sn.Database.SqlQuery<SerialId>("Select id, oldnum, sernum From serial Where flag=0 Order By oldnum ASC, sernum ASC").ToList<SerialId>();
                            ids = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.oldnum).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, oldnum = s.oldnum, sernum = s.sernum }).ToList();
                            return ids.Where(s => s.flag == 0).ToList();
                        case SORT_BY.BUSITYP:
                            //ids = sn.Database.SqlQuery<SerialId>("Select serial.id as id, istab.typcod as busityp, serial.sernum as sernum From serial Left Join istab On serial.busityp_id = istab.id And serial.flag = 0 Order By busityp ASC, sernum ASC").Select(s => new SerialId { id = s.id, busityp = s.busityp != null ? s.busityp : "", sernum = s.sernum }).ToList();
                            ids = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.busityp).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, busityp = s.busityp, sernum = s.sernum }).ToList();
                            return ids.Where(s => s.flag == 0).ToList();
                        case SORT_BY.AREA:
                            //ids = sn.Database.SqlQuery<SerialId>("Select serial.id as id, istab.typcod as area, serial.sernum as sernum From serial Left Join istab On serial.area_id = istab.id And serial.flag = 0 Order By area ASC, sernum ASC").Select(s => new SerialId { id = s.id, area = s.area != null ? s.area : "", sernum = s.sernum }).ToList();
                            ids = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.area).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, area = s.area, sernum = s.sernum }).ToList();
                            return ids.Where(s => s.flag == 0).ToList();
                        default:
                            //ids = sn.Database.SqlQuery<SerialId>("Select id, sernum From serial Where flag=0 Order By sernum ASC").ToList<SerialId>();
                            ids = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id, sernum = s.sernum }).ToList();
                            return ids;
                    }
                }
            }
            
            if(filter == INQUIRY_FILTER.MA)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    return sn.serial.Include("ma").Where(s => s.flag == 0).Where(s => s.ma.AsEnumerable().Count() > 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id, sernum = s.sernum }).ToList();
                }
            }

            if(filter == INQUIRY_FILTER.CLOUD)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    return sn.serial.Include("cloud_srv").Where(s => s.flag == 0).Where(s => s.cloud_srv.AsEnumerable().Count() > 0).OrderBy(s => s.sernum).Select(s => new SerialId{ id = s.id, sernum = s.sernum }).ToList();
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
                        //return sn.Database.SqlQuery<serial>("Select * From serial Where id IN(" + String.Join(",", ids) + ") Order By sernum ASC").ToList();
                        return sn.serial.Where(s => ids.Contains(s.id)).OrderBy(s => s.sernum).ToList();
                    case SORT_BY.CONTACT:
                        //return sn.Database.SqlQuery<serial>("Select * From serial Where id IN(" + String.Join(",", ids) + ") Order By contact ASC, sernum ASC").ToList();
                        return sn.serial.Where(s => ids.Contains(s.id)).OrderBy(s => s.contact).ThenBy(s => s.sernum).ToList();
                    case SORT_BY.COMPNAM:
                        //return sn.Database.SqlQuery<serial>("Select * From serial Where id IN(" + String.Join(",", ids) + ") Order By compnam ASC, sernum ASC").ToList();
                        return sn.serial.Where(s => ids.Contains(s.id)).OrderBy(s => s.compnam).ThenBy(s => s.sernum).ToList();
                    case SORT_BY.DEALER:
                        //return sn.Database.SqlQuery<serial>("Select * From serial Left Join dealer On serial.dealer_id = dealer.id Where serial.id IN(" + String.Join(",", ids) + ") Order By dealer.dealercod ASC, serial.sernum ASC").ToList();
                        return sn.serial.Where(s => ids.Contains(s.id)).OrderBy(s => s.dealercod).ThenBy(s => s.sernum).ToList();
                    case SORT_BY.OLDNUM:
                        //return sn.Database.SqlQuery<serial>("Select * From serial Where id IN(" + String.Join(",", ids) + ") Order By oldnum ASC, sernum ASC").ToList();
                        return sn.serial.Where(s => ids.Contains(s.id)).OrderBy(s => s.oldnum).ThenBy(s => s.sernum).ToList();
                    case SORT_BY.BUSITYP:
                        //return sn.Database.SqlQuery<serial>("Select * From serial Left Join istab On serial.busityp_id = istab.id Where serial.id IN(" + String.Join(",", ids) + ") Order By istab.typcod ASC, serial.sernum ASC").ToList();
                        return sn.serial.Where(s => ids.Contains(s.id)).OrderBy(s => s.busityp).ThenBy(s => s.sernum).ToList();
                    case SORT_BY.AREA:
                        //return sn.Database.SqlQuery<serial>("Select * From serial Left Join istab On serial.area_id = istab.id Where serial.id IN(" + String.Join(",", ids) + ") Order By istab.typcod ASC, serial.sernum ASC").ToList();
                        return sn.serial.Where(s => ids.Contains(s.id)).OrderBy(s => s.area).ThenBy(s => s.sernum).ToList();
                    default:
                        //return sn.Database.SqlQuery<serial>("Select * From serial Where id IN(" + String.Join(",", ids) + ") Order By sernum ASC").ToList();
                        return sn.serial.Where(s => ids.Contains(s.id)).OrderBy(s => s.sernum).ToList();
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

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (((XDatagrid)sender).Rows.Count > 0)
                this.btnOK.Enabled = true;
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
        public string sernum { get; set; }
        public string contact { get; set; }
        public string compnam { get; set; }
        public string dealercod { get; set; }
        public string oldnum { get; set; }
        public string busityp { get; set; }
        public string area { get; set; }
        public int flag { get; set; }
    }
}
