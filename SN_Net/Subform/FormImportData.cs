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
                istab_dbf = DbfTable.istab(this.data_path).ToIstabDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                // Collect area code from Dealer
                var area_dealers = DbfTable.dealer(this.data_path).ToDealerDbfList().GroupBy(d => d.area).Select(d => new { area = d.Key }).ToList();

                // Collect area code from Serial
                var area_serial = DbfTable.serial(this.data_path).ToSerialDbfList().GroupBy(d => d.area).Select(s => new { area = s.Key }).ToList();

                // Mixed area code from Dealer & Serial
                var areas = area_dealers.Concat(area_serial).GroupBy(a => a.area).Select(a => new { area = a.Key }).ToList();
                
                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    total_row = istab_dbf.Count + areas.Count;
                    foreach (var i in istab_dbf)
                    {
                        if(((BackgroundWorker)sender).CancellationPending == true)
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
                                    credat = DateTime.Now
                                };
                                sn.istab.Add(istab);
                                sn.SaveChanges();
                                wrk.ReportProgress(++curr_row);
                            }
                        }
                    }

                    // Add area code to istab
                    foreach (var a in areas)
                    {
                        if(((BackgroundWorker)sender).CancellationPending == true)
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
                                    credat = DateTime.Now
                                };
                                sn.istab.Add(istab);
                                sn.SaveChanges();
                                wrk.ReportProgress(++curr_row);
                            }
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
                    // Continue to import dealer.dbf
                    this.ImportDealer();
                };
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
                dealer_dbf = DbfTable.dealer(this.data_path).ToDealerDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    total_row = dealer_dbf.Count;
                    int curr_row = 0;
                    foreach (var d in dealer_dbf)
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
                                };
                                sn.dealer.Add(dealer);
                                sn.SaveChanges();
                                wrk.ReportProgress(++curr_row);
                            }
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
                    MessageAlert.Show("Completed");
                };
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
                serial_dbf = DbfTable.serial(this.data_path).ToSerialDbfList();
                wrk = null;
                wrk = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

                wrk.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    total_row = serial_dbf.Count;
                    int curr_row = 0;
                    foreach (var i in serial_dbf)
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
                                //istab istab = new istab
                                //{
                                //    tabtyp = i.tabtyp,
                                //    typcod = i.typcod,
                                //    abbreviate_en = i.shortnam2,
                                //    abbreviate_th = i.shortnam,
                                //    typdes_en = i.typdes2,
                                //    typdes_th = i.typdes,
                                //};
                                //sn.istab.Add(istab);
                                sn.SaveChanges();
                                wrk.ReportProgress(++curr_row);
                            }
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
                    MessageAlert.Show("Completed");
                };
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
    }

}
