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
using System.IO;

namespace SN_Net.Subform
{
    public partial class DialogPreference : Form
    {
        private MainForm main_form;
        private PreferenceValue pref;
        //private PreferenceValue tmp_pref;
        private FORM_MODE form_mode;

        public DialogPreference(MainForm main_form)
        {
            this.main_form = main_form;
            InitializeComponent();
        }

        private void DialogPreference_Load(object sender, EventArgs e)
        {
            this.pref = GetPreference();

            if(this.pref == null)
            {
                this.pref = new PreferenceValue
                {
                    serverName = string.Empty,
                    dbName = string.Empty,
                    userId = string.Empty,
                    passWord = string.Empty,
                    port = "3306"
                };
            }

            this.FillForm(this.pref);
        }

        private  void FillForm(PreferenceValue pref_to_fill)
        {
            if (this.pref != null)
            {
                this.txtServername.Text = this.pref.serverName;
                this.txtDbname.Text = this.pref.dbName;
                this.txtUserid.Text = this.pref.userId;
                this.txtPassword.Text = this.pref.passWord;
                int port_num;
                if(Int32.TryParse(this.pref.port, out port_num))
                {
                    this.numPort.Value = port_num;
                }
                else
                {
                    this.numPort.Value = 3306;
                }
            }
            else
            {
                this.txtServername.Text = string.Empty;
                this.txtDbname.Text = string.Empty;
                this.txtUserid.Text = string.Empty;
                this.txtPassword.Text = string.Empty;
                this.numPort.Value = 3306;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.pref == null)
                return;

            if (MessageAlert.Show("บันทึกการเปลี่ยนแปลง ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                return;

            if (DBX.TestConnection(this.pref) != true)
            {
                MessageAlert.Show("ไม่สามารถเชื่อมต่อฐานข้อมูลได้, กรุณาตรวจสอบการตั้งค่าการเชื่อมต่อฐานข้อมูล", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return;
            }

            try
            {
                File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "pref.txt", new List<string> { this.pref.serverName.Trim().ToBytesString(), this.pref.dbName.Trim().ToBytesString(), this.pref.userId.Trim().ToBytesString(), this.pref.passWord.Trim().ToBytesString(), this.pref.port.ToBytesString() }, Encoding.UTF8);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return;
            }
        }

        public static PreferenceValue GetPreference()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "pref.txt"))
                return null;

            PreferenceValue pv = new PreferenceValue
            {
                serverName = string.Empty,
                dbName = string.Empty,
                userId = string.Empty,
                passWord = string.Empty,
                port = "3306"
            };

            try
            {
                string[] str = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "pref.txt");

                pv.serverName = str[0].ExtractBytesString();
                pv.dbName = str[1].ExtractBytesString();
                pv.userId = str[2].ExtractBytesString();
                pv.passWord = str[3].ExtractBytesString();
                pv.port = str[4].ExtractBytesString();
                
                return pv;
            }
            catch (Exception ex)
            {
                return pv;
            }
        }

        private void txtServername_TextChanged(object sender, EventArgs e)
        {
            if (this.pref != null)
                this.pref.serverName = ((TextBox)sender).Text;
        }

        private void txtDbname_TextChanged(object sender, EventArgs e)
        {
            if (this.pref != null)
                this.pref.dbName = ((TextBox)sender).Text;
        }

        private void txtUserid_TextChanged(object sender, EventArgs e)
        {
            if (this.pref != null)
                this.pref.userId = ((TextBox)sender).Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (this.pref != null)
                this.pref.passWord = ((TextBox)sender).Text;
        }

        private void numPort_ValueChanged(object sender, EventArgs e)
        {
            if (this.pref != null)
                this.pref.port = ((NumericUpDown)sender).Value.ToString();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            PreferenceValue pref = this.pref;

            if (this.pref != null)
                pref = this.pref;

            if (pref == null)
                return;

            bool test_result = DBX.TestConnection(pref);
            if(test_result == true)
            {
                MessageAlert.Show("Test connection success.", "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
            }
            else
            {
                MessageAlert.Show("Test connection failed.", "", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if(this.btnOK.Focused || this.btnCancel.Focused)
                {
                    return false;
                }

                SendKeys.Send("{TAB}");
                return true;
            }

            if(keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            if(keyData == Keys.F9)
            {
                this.btnOK.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public class PreferenceValue
    {
        public string serverName { get; set; }
        public string dbName { get; set; }
        public string userId { get; set; }
        public string passWord { get; set; }
        public string port { get; set; }
    }
}
