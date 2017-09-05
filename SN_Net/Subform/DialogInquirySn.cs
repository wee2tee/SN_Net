﻿using System;
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
        private INQUIRY inquiry_sign;
        private SORT_BY sort_by;
        private serial init_serial = null;
        private List<SerialId> serial_id_list = null;

        private BindingList<serialVM> serial_list;

        public DialogInquirySn(SORT_BY sort_by, serial init_serial = null)
        {
            InitializeComponent();
            if(init_serial != null)
            {
                this.inquiry_sign = INQUIRY.REST;
                this.init_serial = init_serial;
            }
            else
            {
                this.inquiry_sign = INQUIRY.ALL;
            }

            this.sort_by = sort_by;
        }

        private void DialogInquirySn_Load(object sender, EventArgs e)
        {
            this.serial_id_list = this.GetSerialIdList(this.sort_by);

            if(this.inquiry_sign == INQUIRY.ALL)
            {
                try
                {
                    int[] ids = this.serial_id_list.GetRange(0, 200).Select(s => s.id).ToArray<int>();
                    this.serial_list = new BindingList<serialVM>(this.GetRestSerial(ids).ToViewModel());
                    this.dgv.DataSource = this.serial_list;
                    //this.serial_id_list.Where(s => ids.Contains(s.id)).ToList().ForEach(s => s.loaded = true);
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }

            if(this.inquiry_sign == INQUIRY.REST)
            {
                try
                {
                    int curr_ndx = this.serial_id_list.IndexOf(this.serial_id_list.Where(s => s.id == this.init_serial.id).FirstOrDefault());
                    int start_ndx = curr_ndx < 50 ? 0 : curr_ndx - 50;
                    int end_ndx = start_ndx + 100 > this.serial_id_list.Count - 1 ? this.serial_id_list.Count - start_ndx : 100;
                    int[] ids = this.serial_id_list.GetRange(start_ndx, end_ndx).Select(s => s.id).ToArray<int>();
                    this.serial_list = new BindingList<serialVM>(this.GetRestSerial(ids).ToViewModel());
                    this.dgv.DataSource = this.serial_list;
                    this.dgv.FirstDisplayedScrollingRowIndex = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == this.init_serial.id).First().Index;
                    this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == this.init_serial.id).First().Cells[this.col_compnam.Name].Selected = true;
                    //this.serial_id_list.Where(s => ids.Contains(s.id)).ToList().ForEach(s => s.loaded = true);
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private List<SerialId> GetSerialIdList(SORT_BY sort_by)
        {
            using (snEntities sn = DBX.DataSet())
            {
                switch (sort_by)
                {
                    case SORT_BY.SERNUM:
                        return sn.serial.Where(s => s.flag == 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.sernum }).ToList();
                    case SORT_BY.CONTACT:
                        return sn.serial.Where(s => s.flag == 0).OrderBy(s => s.contact).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.contact + ";" + s.sernum }).ToList();
                    case SORT_BY.COMPNAM:
                        return sn.serial.Where(s => s.flag == 0).OrderBy(s => s.compnam).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.compnam + ";" + s.sernum }).ToList();
                    case SORT_BY.DEALER:
                        return sn.serial.Include("dealer").Where(s => s.flag == 0).OrderBy(s => s.dealer.dealercod).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, value = s.dealer.dealercod + ";" + s.sernum }).ToList();
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
                        return ser.OrderBy(s => s.dealer.dealercod).ThenBy(s => s.sernum).ToList();
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

            // Load previous
            if(((XDatagrid)sender).CurrentCell.RowIndex == 0)
            {
                // is first row existing shown
                if ((int)((XDatagrid)sender).Rows[0].Cells[this.col_id.Name].Value == this.serial_id_list.First().id)
                    return;

                this.ShowLoadingBox();
                ((XDatagrid)sender).Enabled = false;
                List<serialVM> ser = null;
                int focused_id = (int)((XDatagrid)sender).Rows[((XDatagrid)sender).FirstDisplayedScrollingRowIndex].Cells[this.col_id.Name].Value;

                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += delegate
                {
                    var first_row_ndx = this.serial_id_list.IndexOf(this.serial_id_list.Where(s => s.id == (int)((XDatagrid)sender).Rows[0].Cells[this.col_id.Name].Value).First());

                    int from_ndx = first_row_ndx >= 200 ? first_row_ndx - 200 : 0;
                    int range = first_row_ndx >= 200 ? 200 : first_row_ndx;
                    int[] ids = this.serial_id_list.GetRange(from_ndx, range).Select(s => s.id).ToArray();
                    ser = this.GetRestSerial(ids).ToViewModel();
                    ser.Reverse();
                };
                wrk.RunWorkerCompleted += delegate
                {
                    BindingList<serialVM> sl = (BindingList<serialVM>)((XDatagrid)sender).DataSource;
                    foreach (var item in ser)
                    {
                        sl.Insert(0, item);
                    }

                    this.HideLoadingBox();
                    ((XDatagrid)sender).FirstDisplayedScrollingRowIndex = ((XDatagrid)sender).Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == focused_id).First().Index;
                    ((XDatagrid)sender).Enabled = true;
                };
                wrk.RunWorkerAsync();

                return;
            }

            // Load next
            if(((XDatagrid)sender).FirstDisplayedScrollingRowIndex > ((XDatagrid)sender).Rows.Count - 50)
            {
                // is last row existing shown
                if ((int)((XDatagrid)sender).Rows[((XDatagrid)sender).Rows.Count - 1].Cells[this.col_id.Name].Value == this.serial_id_list.Last().id)
                    return;

                int focused_id = (int)((XDatagrid)sender).Rows[((XDatagrid)sender).FirstDisplayedScrollingRowIndex].Cells[this.col_id.Name].Value;
                var last_row_ndx = this.serial_id_list.IndexOf(this.serial_id_list.Where(s => s.id == (int)((XDatagrid)sender).Rows[((XDatagrid)sender).Rows.Count - 1].Cells[this.col_id.Name].Value).First());
                //Console.WriteLine(" ==>> last row index : " + last_row_ndx);

                return;
            }

        }

        private void dgv_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }

    public class SerialId
    {
        public int id { get; set; }
        public string value { get; set; }
        public bool loaded { get; set; }
    }
}
