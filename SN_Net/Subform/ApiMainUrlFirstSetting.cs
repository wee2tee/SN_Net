using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class ApiMainUrlFirstSetting : Form
    {
        private Timer t;
        private bool connection_success;
        private string system_path;
        private string appdata_path;

        public ApiMainUrlFirstSetting()
        {
            InitializeComponent();

            //system_path = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            //appdata_path = Path.Combine(system_path, "SN_Net\\");

            this.mskMainURL.GotFocus += new EventHandler(this.mskMainURL_GotFocus);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ApiMainUrlFirstSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (File.Exists(this.appdata_path + "SN_pref.txt"))
            if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "SN_pref.txt")))
            {
                this.DialogResult = DialogResult.OK;
                e.Cancel = false;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                e.Cancel = false;
            }
        }

        private void ApiMainUrlFirstSetting_Shown(object sender, EventArgs e)
        {
            this.mskMainURL.Focus();
        }

        private void mskMainURL_GotFocus(object sender, EventArgs e)
        {
            ((MaskedTextBox)sender).SelectionStart = ((MaskedTextBox)sender).Text.Length;
        }

        private void mskMainURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                ((MaskedTextBox)sender).SelectionStart = ((MaskedTextBox)sender).Text.Length;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOK.PerformClick();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.mskMainURL.Text != "http://")
            {
                this.btnOK.Enabled = false;
                this.t = new Timer();
                this.t.Interval = 200;
                this.t.Tick += new EventHandler(this.updateTimerText);
                this.t.Enabled = true;
                this.t.Start();

                BackgroundWorker workerTestConnection = new BackgroundWorker();
                workerTestConnection.DoWork += new DoWorkEventHandler(this.workerTestConnection_Dowork);
                workerTestConnection.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerTestConnection_Complete);
                workerTestConnection.RunWorkerAsync();
            }
            else
            {
                MessageAlert.Show("กรุณาป้อนข้อมูลให้ถูกต้อง", "Warning", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
            }
        }

        private void workerTestConnection_Dowork(object sender, DoWorkEventArgs e)
        {
            CRUDResult get = ApiActions.GET(this.mskMainURL.Text + "test/test_connection");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.connection_success = true;
            }
            else
            {
                this.connection_success = false;
            }
        }

        private void workerTestConnection_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.connection_success)
            {
                this.t.Stop();
                this.t = null;
                this.label2.Text = "";

                //if (!Directory.Exists(Path.Combine(system_path, "SN_Net")))
                //{
                //    DirectoryInfo dir_info = Directory.CreateDirectory(Path.Combine(system_path, "SN_Net"));
                //    if (dir_info.Exists)
                //    {
                //        using (StreamWriter file = new StreamWriter(this.appdata_path + "SN_pref.txt", false))
                //        {
                //            file.WriteLine("MAIN URL | " + this.mskMainURL.Text);
                //            this.Close();
                //        }
                //    }
                //}
                ///////////////////
                using (StreamWriter file = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "SN_pref.txt"), false))
                {
                    file.WriteLine("MAIN_URL | " + this.mskMainURL.Text);
                    this.Close();
                }
            }
            else
            {
                this.t.Stop();
                this.t = null;
                this.label2.Text = "";
                MessageAlert.Show(StringResource.CANNOT_CONNECT_TO_SERVER, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                this.btnOK.Enabled = true;
            }
        }
        
        private void updateTimerText(object sender, EventArgs e)
        {
            if (this.label2.Text == "")
            {
                this.label2.Text = "test connection";
            }
            else if (this.label2.Text == "test connection")
            {
                this.label2.Text = "test connection.";
            }
            else if (this.label2.Text == "test connection.")
            {
                this.label2.Text = "test connection..";
            }
            else if (this.label2.Text == "test connection..")
            {
                this.label2.Text = "test connection...";
            }
            else if (this.label2.Text == "test connection...")
            {
                this.label2.Text = "test connection....";
            }
            else if (this.label2.Text == "test connection....")
            {
                this.label2.Text = "test connection.....";
            }
            else if (this.label2.Text == "test connection.....")
            {
                this.label2.Text = "test connection";
            }
        }
    }
}
