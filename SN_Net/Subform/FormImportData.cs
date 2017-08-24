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
        //private BackgroundWorker wrk_istab = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
        //private BackgroundWorker wrk_serial = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
        //private BackgroundWorker wrk_problem = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
        //private BackgroundWorker wrk_dealer = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
        //private BackgroundWorker wrk_dmsg = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };

        public FormImportData()
        {
            InitializeComponent();
        }

        private void FormImportData_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fol = new FolderBrowserDialog();
            fol.RootFolder = Environment.SpecialFolder.MyComputer; //AppDomain.CurrentDomain.BaseDirectory
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
            this.btnGo.Enabled = this.data_path.Trim().Length > 0 ? true : false;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.data_path))
            {
                MessageAlert.Show("โฟลเดอร์ข้อมูลที่ท่านเลือกไม่สามารถเข้าถึงได้!", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return;
            }

            // Beginning import first Istab.dbf and follwed by the
            this.ImportIstab();

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
                istab_dbf = DbfTable.istab(this.data_path).ToIstabDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                // Collect area code from Dealer
                var area_dealers = DbfTable.dealer(this.data_path).ToDealerDbfList().GroupBy(d => d.area).Select(d => new { area = d.Key }).ToList();

                // Collect area code from Serial
                var area_serial = DbfTable.serial(this.data_path).ToSerialDbfList().GroupBy(d => d.area).Select(s => new { area = s.Key }).ToList();

                // Mixed area code from Dealer & Serial
                var areas = area_dealers.Concat(area_serial).GroupBy(a => a.area).Select(a => new { area = a.Key }).ToList();

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
                                        pattern = string.Empty,
                                        credat = DateTime.Now,
                                        flag = "1"
                                    };
                                    sn.istab.Add(istab);
                                    sn.SaveChanges();
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("istab", ex.Message);
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
                                        pattern = string.Empty,
                                        credat = DateTime.Now,
                                        flag = "1"
                                    };
                                    sn.istab.Add(istab);
                                    sn.SaveChanges();
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("istab", ex.Message);
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
                                        pattern = string.Empty,
                                        credat = DateTime.Now,
                                        flag = "1"
                                    };
                                    sn.istab.Add(istab);
                                    sn.SaveChanges();
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("istab", ex.Message);
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
                    this.KeepLog("", "", false);

                    // Continue to import dealer.dbf
                    if (!e.Cancelled)
                        this.ImportDealer();
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
                int curr_row = 0;
                int succeed_rows = 0;
                int failed_rows = 0;
                this.KeepLog("    ", "Reading dealer source data");
                dealer_dbf = DbfTable.dealer(this.data_path).ToDealerDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                total_row = dealer_dbf.Count;

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    foreach (var d in dealer_dbf)
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
                                    var area = sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_AREA && i.typcod == d.area).FirstOrDefault();

                                    dealer dealer = new dealer
                                    {
                                        dealercod = d.dealer,
                                        prenam = d.prenam,
                                        compnam = d.compnam,
                                        addr01 = d.addr01,
                                        addr02 = d.addr02,
                                        addr03 = d.addr03,
                                        zipcod = d.zipcod,
                                        telnum = d.telnum,
                                        contact = d.contact,
                                        position = d.position,
                                        busides = d.busides,
                                        area_id = area != null ? (int?)area.id : null,
                                        remark = d.remark,
                                        credat = DateTime.Now,
                                        flag = "1"
                                    };
                                    sn.dealer.Add(dealer);
                                    sn.SaveChanges();
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("dealer", ex.Message);
                            failed_rows++;
                            wrk.ReportProgress(++curr_row);
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
                    this.KeepLog("", "", false);

                    // Continue to import d_msg.dbf
                    if (!e.Cancelled)
                        this.ImportDmsg();
                };
                this.KeepLog("    ", "Start import dealer " + total_row.ToString() + " row(s)");

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
                int curr_row = 0;
                int succeed_rows = 0;
                int failed_rows = 0;
                this.KeepLog("    ", "Reading d_msg source data");
                dmsg_dbf = DbfTable.d_msg(this.data_path).ToDmsgDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                total_row = dmsg_dbf.Count;

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    foreach (var d in dmsg_dbf)
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
                                    var dealer = sn.dealer.Where(dl => dl.dealercod == d.dealer).FirstOrDefault();

                                    if(dealer == null)
                                    {
                                        this.KeepLog("d_msg", "Cannot find dealer code = '" + d.dealer + "'");
                                        wrk.ReportProgress(++curr_row);
                                        continue;
                                    }

                                    d_msg dmsg = new d_msg
                                    {
                                        dealer_id = dealer != null ? (int?)dealer.id : null,
                                        date = d.date,
                                        time = d.time,
                                        name = d.name,
                                        description = d.descrp,
                                        credat = DateTime.Now,
                                        flag = "1"
                                    };
                                    sn.d_msg.Add(dmsg);
                                    sn.SaveChanges();
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("d_msg", ex.Message);
                            failed_rows++;
                            wrk.ReportProgress(++curr_row);
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
                    this.KeepLog("", "", false);
                    
                    // Continue to import serial.dbf
                    if (!e.Cancelled)
                        this.ImportSerial();
                };
                this.KeepLog("    ", "Start import d_msg " + total_row.ToString() + " row(s)");

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
                int curr_row = 0;
                int succeed_rows = 0;
                int failed_rows = 0;
                this.KeepLog("    ", "Reading serial source data");
                serial_dbf = DbfTable.serial(this.data_path).ToSerialDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                total_row = serial_dbf.Count;

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    foreach (var s in serial_dbf)
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
                                    var dealer = sn.dealer.Where(dl => dl.dealercod == s.dealer).FirstOrDefault();
                                    var area = sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_AREA && i.typcod == s.area).FirstOrDefault();
                                    var busityp = sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_BUSITYP && i.typcod == s.busityp).FirstOrDefault();
                                    var howknown = sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_HOWKNOW && i.typcod == s.howknown).FirstOrDefault();
                                    var verext = sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod == s.verext).FirstOrDefault();

                                    serial serial = new serial
                                    {
                                        sernum = s.sernum,
                                        oldnum = s.oldnum,
                                        version = s.version,
                                        contact = s.contact,
                                        position = s.position,
                                        prenam = s.prenam,
                                        compnam = s.compnam,
                                        addr01 = s.addr01,
                                        addr02 = s.addr02,
                                        addr03 = s.addr03,
                                        zipcod = s.zipcod,
                                        telnum = s.telnum,
                                        busides = s.busides,
                                        purdat = s.purdat,
                                        expdat = s.expdat,
                                        branch = s.branch,
                                        manual = s.manual,
                                        upfree = s.upfree,
                                        refnum = s.refnum,
                                        remark = s.remark,
                                        dealer_id = dealer != null ? (int?)dealer.id : null,
                                        verextdat = s.verextdat,
                                        area_id = area != null ? (int?)area.id : null,
                                        busityp_id = busityp != null ? (int?)busityp.id : null,
                                        howknown_id = howknown != null ? (int?)howknown.id : null,
                                        verext_id = verext != null ? (int?)verext.id : null,
                                        credat = s.chgdat.HasValue ? s.chgdat.Value : DateTime.Now,
                                        chgdat = s.chgdat,
                                        flag = "1"
                                    };
                                    sn.serial.Add(serial);
                                    sn.SaveChanges();
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("serial", ex.Message);
                            failed_rows++;
                            wrk.ReportProgress(++curr_row);
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
                    this.KeepLog("", "", false);

                    // Continue to import problem.dbf
                    if (!e.Cancelled)
                        this.ImportProblem();
                };
                this.KeepLog("    ", "Start import serial " + total_row.ToString() + " row(s)");

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
                int curr_row = 0;
                int succeed_rows = 0;
                int failed_rows = 0;
                this.KeepLog("    ", "Reading problem source data");
                problem_dbf = DbfTable.problem(this.data_path).ToProblemDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                total_row = problem_dbf.Count;

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    foreach (var p in problem_dbf)
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
                                    var serial = sn.serial.Where(s => s.sernum == p.sernum).FirstOrDefault();
                                    var probcod = sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_PROBCOD && i.typcod == p.probcod).FirstOrDefault();

                                    if(serial == null)
                                    {
                                        this.KeepLog("problem", "Cannot find serial number '" + p.sernum + "'");
                                        wrk.ReportProgress(++curr_row);
                                        continue;
                                    }

                                    problem problem = new problem
                                    {
                                        probdesc = p.probdesc,
                                        date = p.date,
                                        time = p.time,
                                        name = p.name,
                                        serial_id = serial != null ? (int?)serial.id : null,
                                        probcod_id = probcod != null ? (int?)probcod.id : null,
                                        credat = DateTime.Now,
                                        flag = "1"
                                    };
                                    sn.problem.Add(problem);
                                    sn.SaveChanges();
                                    succeed_rows++;
                                    wrk.ReportProgress(++curr_row);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.KeepLog("problem", ex.Message);
                            failed_rows++;
                            wrk.ReportProgress(++curr_row);
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
                    this.KeepLog("", "", false);

                    // Import process completed
                    if (!e.Cancelled)
                        MessageAlert.Show("Import data successfully");
                };
                this.KeepLog("    ", "Start import problem " + total_row.ToString() + " row(s)");

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
                this.wrk.CancelAsync();
        }

        private void KeepLog(string table_name, string log_desc, bool perform_formatting = true)
        {
            string log = perform_formatting ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.GetCultureInfo("th-TH")) + "\t" + table_name + "\t" + log_desc + Environment.NewLine : table_name + log_desc + Environment.NewLine;
            //this.richTextBox1.Text += log;
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + @"\importLog.txt", log, Encoding.UTF8);
        }
    }

}
