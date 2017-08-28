using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.Model;
using System.IO;
using System.Globalization;

namespace SN_Net.Subform
{
    public partial class FormImportData : Form
    {
        private string data_path = string.Empty;
        private BackgroundWorker wrk;
        BindingList<ImportLog> import_logs;
        private List<TABLE_NAME> table_name = new List<TABLE_NAME> { TABLE_NAME.ISTAB, TABLE_NAME.DEALER, TABLE_NAME.D_MSG, TABLE_NAME.SERIAL, TABLE_NAME.PROBLEM };
        private enum TABLE_NAME : int
        {
            ISTAB = 1,
            DEALER = 2,
            D_MSG = 3,
            SERIAL = 4,
            PROBLEM = 5
        }
        private int offsetDealer = 0;
        private int offsetDmsg = 0;
        private int offsetSerial = 0;
        private int offsetProblem = 0;
        private bool importing = false;

        public FormImportData()
        {
            InitializeComponent();
        }

        private void FormImportData_Load(object sender, EventArgs e)
        {
            this.import_logs = new BindingList<ImportLog>();
            this.dgvLog.DataSource = this.import_logs;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.importing)
            {
                e.Cancel = true;
                MessageAlert.Show("To close this window, Please stop import data processing first.");
                return;
            }

            base.OnClosing(e);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fol = new FolderBrowserDialog();
            fol.RootFolder = Environment.SpecialFolder.MyComputer;
            fol.SelectedPath = this.data_path.Trim().Length > 0 ? this.data_path : string.Empty;
            fol.ShowNewFolderButton = false;
            if(fol.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = fol.SelectedPath;
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            this.data_path = ((TextBox)sender).Text;
            this.ValidatePrepareFormData();
        }

        private void chIstab_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.table_name.Add(TABLE_NAME.ISTAB);
            }
            else
            {
                this.table_name.Remove(TABLE_NAME.ISTAB);
            }
            this.ValidatePrepareFormData();
        }

        private void chDealer_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.table_name.Add(TABLE_NAME.DEALER);
            }
            else
            {
                this.table_name.Remove(TABLE_NAME.DEALER);
            }
            this.ValidatePrepareFormData();
        }

        private void chDmsg_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.table_name.Add(TABLE_NAME.D_MSG);
            }
            else
            {
                this.table_name.Remove(TABLE_NAME.D_MSG);
            }
            this.ValidatePrepareFormData();
        }

        private void chSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.table_name.Add(TABLE_NAME.SERIAL);
            }
            else
            {
                this.table_name.Remove(TABLE_NAME.SERIAL);
            }
            this.ValidatePrepareFormData();
        }

        private void chProblem_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.table_name.Add(TABLE_NAME.PROBLEM);
            }
            else
            {
                this.table_name.Remove(TABLE_NAME.PROBLEM);
            }
            this.ValidatePrepareFormData();
        }

        private void ValidatePrepareFormData()
        {
            if (this.data_path.Trim().Length > 0 && this.table_name.Count > 0)
            {
                this.btnGo.Enabled = true;
            }
            else
            {
                this.btnGo.Enabled = false;
            }
        }

        private void numDealer_ValueChanged(object sender, EventArgs e)
        {
            this.offsetDealer = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        private void numDmsg_ValueChanged(object sender, EventArgs e)
        {
            this.offsetDmsg = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        private void numSerial_ValueChanged(object sender, EventArgs e)
        {
            this.offsetSerial = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        private void numProblem_ValueChanged(object sender, EventArgs e)
        {
            this.offsetProblem = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.data_path))
            {
                MessageAlert.Show("โฟลเดอร์ข้อมูลที่ท่านเลือกไม่สามารถเข้าถึงได้!", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return;
            }

            // Beginning import
            this.importing = true;

            var first_table = this.table_name.OrderBy(t => (int)t).First();
            switch (first_table)
            {
                case TABLE_NAME.ISTAB:
                    this.ImportIstab();
                    break;
                case TABLE_NAME.DEALER:
                    this.ImportDealer();
                    break;
                case TABLE_NAME.D_MSG:
                    this.ImportDmsg();
                    break;
                case TABLE_NAME.SERIAL:
                    this.ImportSerial();
                    break;
                case TABLE_NAME.PROBLEM:
                    this.ImportProblem();
                    break;
                default:
                    break;
            }

            this.chIstab.Enabled = false;
            this.chDealer.Enabled = false;
            this.chDmsg.Enabled = false;
            this.chSerial.Enabled = false;
            this.chProblem.Enabled = false;

            this.numDealer.Enabled = false;
            this.numDmsg.Enabled = false;
            this.numSerial.Enabled = false;
            this.numProblem.Enabled = false;

            this.btnBrowse.Enabled = false;
            ((Button)sender).Enabled = false;
            this.btnStop.Enabled = true;
        }

        private void ImportIstab()
        {
            try
            {
                List<istabDbf> istab_dbf = null;
                int total_row = 0;
                int curr_row = 0;
                int succeed_rows = 0;
                int failed_rows = 0;
                this.KeepLog("    ", "Reading istab source data");
                istab_dbf = DbfTable.istab(this.data_path).ToIstabDbfList().GroupBy(i => i.tabtyp+i.typcod.ToUpper()).Select(i => new istabDbf { tabtyp = i.First().tabtyp, typcod = i.First().typcod, depcod = i.First().depcod, shortnam = i.First().shortnam, shortnam2 = i.First().shortnam2, typdes = i.First().typdes, typdes2 = i.First().typdes2, fld01 = i.First().fld01, fld02 = i.First().fld02, status = i.First().status }).ToList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                // Collect area code from Dealer
                var area_dealers = DbfTable.dealer(this.data_path).ToDealerDbfList().GroupBy(d => d.area).Select(d => new { area = d.Key }).ToList();

                // Collect area code from Serial
                var area_serial = DbfTable.serial(this.data_path).ToSerialDbfList().GroupBy(d => d.area).Select(s => new { area = s.Key }).ToList();

                // Mixed area code from Dealer & Serial
                var areas = area_dealers.Concat(area_serial).GroupBy(a => a.area.ToUpper()).Select(a => new { area = a.Key }).ToList();

                // Collect verext from Serial
                var verext = DbfTable.serial(this.data_path).ToSerialDbfList().GroupBy(s => s.verext).Select(s => new { verext = s.Key }).ToList();

                total_row = istab_dbf.Count + areas.Count + verext.Count;

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    // Add howknown,busityp,probcod to istab
                    foreach (var i in istab_dbf)
                    {
                        try
                        {
                            if (((BackgroundWorker)sender).CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {
                                using (snEntities sn = DBX.DataSet())
                                {
                                    istab istab = new istab
                                    {
                                        tabtyp = i.tabtyp,
                                        typcod = i.typcod,
                                        abbreviate_en = i.shortnam2,
                                        abbreviate_th = i.shortnam,
                                        typdes_en = i.typdes2,
                                        typdes_th = i.typdes,
                                        use_pattern = false,
                                        credat = DateTime.Now,
                                        //flag = 0
                                    };
                                    sn.istab.Add(istab);
                                    sn.SaveChanges();
                                    istab = null;
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("istab", ex.InnerException.Message);
                            failed_rows++;
                            wrk.ReportProgress(++curr_row);
                            continue;
                        }
                    }

                    // Add area code to istab
                    foreach (var a in areas)
                    {
                        try
                        {
                            if (((BackgroundWorker)sender).CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {
                                using (snEntities sn = DBX.DataSet())
                                {
                                    istab istab = new istab
                                    {
                                        tabtyp = istabDbf.TABTYP_AREA,
                                        typcod = a.area,
                                        abbreviate_th = a.area,
                                        typdes_th = a.area,
                                        use_pattern = false,
                                        credat = DateTime.Now,
                                        //flag = 0
                                    };
                                    sn.istab.Add(istab);
                                    sn.SaveChanges();
                                    istab = null;
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("istab", ex.InnerException.Message);
                            failed_rows++;
                            wrk.ReportProgress(++curr_row);
                            continue;
                        }
                    }

                    // Add verext code to istab
                    foreach (var a in verext)
                    {
                        try
                        {
                            if (((BackgroundWorker)sender).CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {
                                using (snEntities sn = DBX.DataSet())
                                {
                                    istab istab = new istab
                                    {
                                        tabtyp = istabDbf.TABTYP_VEREXT,
                                        typcod = a.verext,
                                        abbreviate_th = a.verext,
                                        typdes_th = a.verext,
                                        use_pattern = false,
                                        credat = DateTime.Now,
                                        //flag = 0
                                    };
                                    sn.istab.Add(istab);
                                    sn.SaveChanges();
                                    istab = null;
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("istab", ex.InnerException.Message);
                            failed_rows++;
                            wrk.ReportProgress(++curr_row);
                            continue;
                        }
                    }
                };
                wrk.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
                {
                    this.pgIstab.Maximum = total_row;
                    this.pgIstab.Value = e.ProgressPercentage;
                    this.lblProgressIstab.Text = this.pgIstab.Value.ToString() + "/" + this.pgIstab.Maximum.ToString();
                };
                wrk.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
                {
                    this.KeepLog("    ", "Import istab succeed : " + succeed_rows.ToString() + " row(s) , failed :" + failed_rows.ToString() + " row(s)");
                    this.KeepLog("", "------------------------------------------------------------------------------", false);

                    // Continue import next table
                    if (!e.Cancelled)
                    {
                        var rest_table = this.table_name.OrderBy(t => (int)t).Where(t => (int)t > (int)TABLE_NAME.ISTAB).Count();
                        if (rest_table > 0)
                        {
                            var next_table = this.table_name.OrderBy(t => (int)t).Where(t => (int)t > (int)TABLE_NAME.ISTAB).First();
                            switch (next_table)
                            {
                                case TABLE_NAME.DEALER:
                                    this.ImportDealer();
                                    break;
                                case TABLE_NAME.D_MSG:
                                    this.ImportDmsg();
                                    break;
                                case TABLE_NAME.SERIAL:
                                    this.ImportSerial();
                                    break;
                                case TABLE_NAME.PROBLEM:
                                    this.ImportProblem();
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            this.importing = false;
                            MessageAlert.Show("Import data completed");
                        }
                    }
                    else
                    {
                        this.importing = false;
                    }
                };

                this.KeepLog("    ", "Start import istab " + total_row.ToString() + " row(s)");
                wrk.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void ImportDealer()
        {
            try
            {
                List<dealerDbf> dealer_dbf = null;
                int total_row = 0;
                int succeed_rows = 0;
                int failed_rows = 0;
                this.KeepLog("    ", "Reading dealer source data");
                dealer_dbf = DbfTable.dealer(this.data_path).ToDealerDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                total_row = dealer_dbf.Count;

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    for (int i = this.offsetDealer; i < dealer_dbf.Count - 1; i++)
                    {
                        try
                        {
                            if (((BackgroundWorker)sender).CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {
                                using (snEntities sn = DBX.DataSet())
                                {
                                    string str_area = dealer_dbf[i].area;
                                    var area = sn.istab.Where(it => it.tabtyp == istabDbf.TABTYP_AREA && it.typcod == str_area).FirstOrDefault();

                                    dealer dealer = new dealer
                                    {
                                        dealercod = dealer_dbf[i].dealer,
                                        prenam = dealer_dbf[i].prenam,
                                        compnam = dealer_dbf[i].compnam,
                                        addr01 = dealer_dbf[i].addr01,
                                        addr02 = dealer_dbf[i].addr02,
                                        addr03 = dealer_dbf[i].addr03,
                                        zipcod = dealer_dbf[i].zipcod,
                                        telnum = dealer_dbf[i].telnum,
                                        contact = dealer_dbf[i].contact,
                                        position = dealer_dbf[i].position,
                                        busides = dealer_dbf[i].busides,
                                        area_id = area != null ? (int?)area.id : null,
                                        remark = dealer_dbf[i].remark,
                                        credat = DateTime.Now,
                                        //flag = 0
                                    };
                                    sn.dealer.Add(dealer);
                                    sn.SaveChanges();
                                    dealer = null;
                                    succeed_rows++;
                                    wrk.ReportProgress(i + 1);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("dealer", ex.InnerException.Message);
                            failed_rows++;
                            wrk.ReportProgress(i + 1);
                            continue;
                        }
                    }
                };
                wrk.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
                {
                    this.pgDealer.Maximum = total_row;
                    this.pgDealer.Value = e.ProgressPercentage;
                    this.lblProgressDealer.Text = this.pgDealer.Value.ToString() + "/" + this.pgDealer.Maximum.ToString();
                };
                wrk.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
                {
                    this.KeepLog("    ", "Import dealer succeed : " + succeed_rows.ToString() + " row(s) , failed :" + failed_rows.ToString() + " row(s)");
                    this.KeepLog("", "------------------------------------------------------------------------------", false);

                    // Continue import next table
                    if (!e.Cancelled)
                    {
                        var rest_table = this.table_name.OrderBy(t => (int)t).Where(t => (int)t > (int)TABLE_NAME.DEALER).Count();
                        if(rest_table > 0)
                        {
                            var next_table = this.table_name.OrderBy(t => (int)t).Where(t => (int)t > (int)TABLE_NAME.DEALER).First();
                            switch (next_table)
                            {
                                case TABLE_NAME.D_MSG:
                                    this.ImportDmsg();
                                    break;
                                case TABLE_NAME.SERIAL:
                                    this.ImportSerial();
                                    break;
                                case TABLE_NAME.PROBLEM:
                                    this.ImportProblem();
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            this.importing = false;
                            MessageAlert.Show("Import data completed");
                        }
                    }
                    else
                    {
                        this.importing = false;
                    }
                };
                this.KeepLog("    ", "Start import dealer " + total_row.ToString() + " row(s), Offset " + this.offsetDealer.ToString());

                wrk.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void ImportDmsg()
        {
            try
            {
                List<d_msgDbf> dmsg_dbf = null;
                int total_row = 0;
                int succeed_rows = 0;
                int failed_rows = 0;
                this.KeepLog("    ", "Reading d_msg source data");
                dmsg_dbf = DbfTable.d_msg(this.data_path).ToDmsgDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                total_row = dmsg_dbf.Count;

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    for (int i = this.offsetDmsg; i < dmsg_dbf.Count - 1; i++)
                    {
                        try
                        {
                            if (((BackgroundWorker)sender).CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {
                                using (snEntities sn = DBX.DataSet())
                                {
                                    string str_dealer = dmsg_dbf[i].dealer;

                                    var dealer = sn.dealer.Where(dl => dl.dealercod == str_dealer).FirstOrDefault();

                                    if (dealer == null)
                                    {
                                        failed_rows++;
                                        this.KeepLog("d_msg", "Cannot find dealer code = '" + dmsg_dbf[i].dealer + "'");
                                        wrk.ReportProgress(i + 1);
                                        continue;
                                    }

                                    d_msg dmsg = new d_msg
                                    {
                                        dealer_id = dealer != null ? (int?)dealer.id : null,
                                        date = dmsg_dbf[i].date,
                                        time = dmsg_dbf[i].time,
                                        name = dmsg_dbf[i].name,
                                        description = dmsg_dbf[i].descrp,
                                        credat = DateTime.Now,
                                        //flag = 0
                                    };
                                    sn.d_msg.Add(dmsg);
                                    sn.SaveChanges();
                                    dmsg = null;
                                    succeed_rows++;
                                    wrk.ReportProgress(i + 1);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            failed_rows++;
                            this.KeepLog("d_msg", ex.InnerException.Message);
                            wrk.ReportProgress(i + 1);
                            continue;
                        }
                    }
                };
                wrk.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
                {
                    this.pgDmsg.Maximum = total_row;
                    this.pgDmsg.Value = e.ProgressPercentage;
                    this.lblProgressDmsg.Text = this.pgDmsg.Value.ToString() + "/" + this.pgDmsg.Maximum.ToString();
                };
                wrk.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
                {
                    this.KeepLog("    ", "Import d_msg succeed : " + succeed_rows.ToString() + " row(s) , failed :" + failed_rows.ToString() + " row(s)");
                    this.KeepLog("", "------------------------------------------------------------------------------", false);

                    // Continue import next table
                    if (!e.Cancelled)
                    {
                        var rest_table = this.table_name.OrderBy(t => (int)t).Where(t => (int)t > (int)TABLE_NAME.D_MSG).Count();
                        if(rest_table > 0)
                        {
                            var next_table = this.table_name.OrderBy(t => (int)t).Where(t => (int)t > (int)TABLE_NAME.D_MSG).First();
                            switch (next_table)
                            {
                                case TABLE_NAME.SERIAL:
                                    this.ImportSerial();
                                    break;
                                case TABLE_NAME.PROBLEM:
                                    this.ImportProblem();
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            this.importing = false;
                            MessageAlert.Show("Import data completed");
                        }
                    }
                    else
                    {
                        this.importing = false;
                    }
                };

                this.KeepLog("    ", "Start import d_msg " + total_row.ToString() + " row(s), Offset " + this.offsetDmsg.ToString());
                wrk.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void ImportSerial()
        {
            try
            {
                List<serialDbf> serial_dbf = null;
                int total_row = 0;
                int succeed_rows = 0;
                int failed_rows = 0;
                this.KeepLog("    ", "Reading serial source data");
                serial_dbf = DbfTable.serial(this.data_path).ToSerialDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                total_row = serial_dbf.Count;

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    for (int i = this.offsetSerial; i < serial_dbf.Count - 1; i++)
                    {
                        try
                        {
                            if (((BackgroundWorker)sender).CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {
                                using (snEntities sn = DBX.DataSet())
                                {
                                    string str_dealer = serial_dbf[i].dealer;
                                    string str_area = serial_dbf[i].area;
                                    string str_busityp = serial_dbf[i].busityp;
                                    string str_howknown = serial_dbf[i].howknown;
                                    string str_verext = serial_dbf[i].verext;

                                    var dealer = sn.dealer.Where(dl => dl.dealercod == str_dealer).FirstOrDefault();
                                    var area = sn.istab.Where(it => it.tabtyp == istabDbf.TABTYP_AREA && it.typcod == str_area).FirstOrDefault();
                                    var busityp = sn.istab.Where(it => it.tabtyp == istabDbf.TABTYP_BUSITYP && it.typcod == str_busityp).FirstOrDefault();
                                    var howknown = sn.istab.Where(it => it.tabtyp == istabDbf.TABTYP_HOWKNOW && it.typcod == str_howknown).FirstOrDefault();
                                    var verext = sn.istab.Where(it => it.tabtyp == istabDbf.TABTYP_VEREXT && it.typcod == str_verext).FirstOrDefault();

                                    serial serial = new serial
                                    {
                                        sernum = serial_dbf[i].sernum,
                                        oldnum = serial_dbf[i].oldnum,
                                        version = serial_dbf[i].version,
                                        contact = serial_dbf[i].contact,
                                        position = serial_dbf[i].position,
                                        prenam = serial_dbf[i].prenam,
                                        compnam = serial_dbf[i].compnam,
                                        addr01 = serial_dbf[i].addr01,
                                        addr02 = serial_dbf[i].addr02,
                                        addr03 = serial_dbf[i].addr03,
                                        zipcod = serial_dbf[i].zipcod,
                                        telnum = serial_dbf[i].telnum,
                                        busides = serial_dbf[i].busides,
                                        purdat = serial_dbf[i].purdat,
                                        expdat = serial_dbf[i].expdat,
                                        branch = serial_dbf[i].branch,
                                        manual = serial_dbf[i].manual,
                                        upfree = serial_dbf[i].upfree,
                                        refnum = serial_dbf[i].refnum,
                                        remark = serial_dbf[i].remark,
                                        dealer_id = dealer != null ? (int?)dealer.id : null,
                                        verextdat = serial_dbf[i].verextdat,
                                        area_id = area != null ? (int?)area.id : null,
                                        busityp_id = busityp != null ? (int?)busityp.id : null,
                                        howknown_id = howknown != null ? (int?)howknown.id : null,
                                        verext_id = verext != null ? (int?)verext.id : null,
                                        credat = serial_dbf[i].chgdat.HasValue ? serial_dbf[i].chgdat.Value : DateTime.Now,
                                        chgdat = serial_dbf[i].chgdat,
                                        //flag = 0
                                    };
                                    sn.serial.Add(serial);
                                    sn.SaveChanges();
                                    serial = null;
                                    succeed_rows++;
                                    wrk.ReportProgress(i + 1);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("serial", ex.InnerException.Message);
                            failed_rows++;
                            wrk.ReportProgress(i + 1);
                            continue;
                        }
                    }
                };
                wrk.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
                {
                    this.pgSerial.Maximum = total_row;
                    this.pgSerial.Value = e.ProgressPercentage;
                    this.lblProgressSerial.Text = this.pgSerial.Value.ToString() + "/" + this.pgSerial.Maximum.ToString();
                };
                wrk.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
                {
                    this.KeepLog("    ", "Import serial succeed : " + succeed_rows.ToString() + " row(s) , failed :" + failed_rows.ToString() + " row(s)");
                    this.KeepLog("", "------------------------------------------------------------------------------", false);

                    // Continue import next table
                    if (!e.Cancelled)
                    {
                        var rest_table = this.table_name.OrderBy(t => (int)t).Where(t => (int)t > (int)TABLE_NAME.SERIAL).Count();
                        if(rest_table > 0)
                        {
                            var next_table = this.table_name.OrderBy(t => (int)t).Where(t => (int)t > (int)TABLE_NAME.SERIAL).First();
                            switch (next_table)
                            {
                                case TABLE_NAME.PROBLEM:
                                    this.ImportProblem();
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            this.importing = false;
                            MessageAlert.Show("Import data completed");
                        }
                    }
                    else
                    {
                        this.importing = false;
                    }
                };
                this.KeepLog("    ", "Start import serial " + total_row.ToString() + " row(s), Offset " + this.offsetSerial.ToString());

                wrk.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void ImportProblem()
        {
            try
            {
                List<problemDbf> problem_dbf = null;
                int total_row = 0;
                int succeed_rows = 0;
                int failed_rows = 0;
                this.KeepLog("    ", "Reading problem source data");
                problem_dbf = DbfTable.problem(this.data_path).ToProblemDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                total_row = problem_dbf.Count;

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    for (int i = this.offsetProblem; i < problem_dbf.Count - 1; i++)
                    {
                        try
                        {
                            if (((BackgroundWorker)sender).CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {
                                using (snEntities sn = DBX.DataSet())
                                {
                                    string sernum = problem_dbf[i].sernum;
                                    string prob = problem_dbf[i].probcod;
                                    var serial = sn.serial.Where(s => s.sernum == sernum).FirstOrDefault();
                                    var probcod = sn.istab.Where(it => it.tabtyp == istabDbf.TABTYP_PROBCOD && it.typcod == prob).FirstOrDefault();
                                    if (serial == null)
                                    {
                                        this.KeepLog("problem", "Cannot find serial number '" + problem_dbf[i].sernum + "'");
                                        failed_rows++;
                                        wrk.ReportProgress(i + 1);
                                        continue;
                                    }

                                    problem problem = new problem
                                    {
                                        probdesc = problem_dbf[i].probdesc,
                                        date = problem_dbf[i].date,
                                        time = problem_dbf[i].time,
                                        name = problem_dbf[i].name,
                                        serial_id = serial != null ? (int?)serial.id : null,
                                        probcod_id = probcod != null ? (int?)probcod.id : null,
                                        credat = DateTime.Now,
                                        //flag = 0
                                    };
                                    sn.problem.Add(problem);
                                    sn.SaveChanges();
                                    problem = null;
                                    succeed_rows++;
                                    wrk.ReportProgress(i + 1);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("problem", ex.InnerException.Message);
                            failed_rows++;
                            wrk.ReportProgress(i + 1);
                            continue;
                        }
                    }
                };
                wrk.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
                {
                    this.pgProblem.Maximum = total_row;
                    this.pgProblem.Value = e.ProgressPercentage;
                    this.lblProgressProblem.Text = this.pgProblem.Value.ToString() + "/" + this.pgProblem.Maximum.ToString();
                };
                wrk.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
                {
                    this.KeepLog("    ", "Import problem succeed : " + succeed_rows.ToString() + " row(s) , failed :" + failed_rows.ToString() + " row(s)");
                    this.KeepLog("", "------------------------------------------------------------------------------", false);

                    // Import process completed
                    if (!e.Cancelled)
                    {
                        this.importing = false;
                        MessageAlert.Show("Import data completed");
                    }
                    else
                    {
                        this.importing = false;
                    }
                };
                this.KeepLog("    ", "Start import problem " + total_row.ToString() + " row(s), Offset " + this.offsetProblem.ToString());

                wrk.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.wrk != null && this.wrk.WorkerSupportsCancellation)
            {
                this.wrk.CancelAsync();

                this.chIstab.Enabled = true;
                this.chDealer.Enabled = true;
                this.chDmsg.Enabled = true;
                this.chSerial.Enabled = true;
                this.chProblem.Enabled = true;

                this.numDealer.Enabled = true;
                this.numDmsg.Enabled = true;
                this.numSerial.Enabled = true;
                this.numProblem.Enabled = true;

                this.btnBrowse.Enabled = true;
                this.btnGo.Enabled = true;
                this.btnStop.Enabled = false;
            }
        }

        private void KeepLog(string table_name, string log_desc, bool perform_formatting = true)
        {
            this.dgvLog.Invoke(new Action(() => {
                BindingList<ImportLog> logs = (BindingList<ImportLog>)this.dgvLog.DataSource;
                if (perform_formatting)
                {
                    logs.Add(new ImportLog { time = DateTime.Now, table_name = table_name, desc = log_desc });
                }
                else
                {
                    logs.Add(new ImportLog { time = null, table_name = table_name, desc = log_desc });
                }
                
                this.dgvLog.FirstDisplayedScrollingRowIndex = this.dgvLog.Rows.Count - 1;
            }));
            
            //    File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + @"\importLog.txt", log, Encoding.UTF8);
        }
    }

    internal class ImportLog
    {
        public DateTime? time { get; set; }
        public string table_name { get; set; }
        public string desc { get; set; }

    }
}
