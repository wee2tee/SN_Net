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

namespace SN_Net.Subform
{
    public partial class PreferenceForm : Form
    {
        public bool autoclick_edit = false;
        private string system_path;
        private string appdata_path;

        public PreferenceForm()
        {
            InitializeComponent();
            system_path = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            appdata_path = Path.Combine(system_path, "SN_Net\\");
        }

        private void PreferenceForm_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;

            this.loadPreferenceSettings();
        }

        private void PreferenceForm_Shown(object sender, EventArgs e)
        {
            if (this.autoclick_edit)
            {
                this.toolStripEdit.PerformClick();
            }
        }

        private void loadPreferenceSettings()
        {
            if (File.Exists(this.appdata_path + "SN_pref.txt"))
            {
                this.mskMainURL.Text = this.readPreferenceLine(1);
            }
            else
            {
                this.mskMainURL.Text = "";
            }
        }

        private string readPreferenceLine(int line_number)
        {
            if (File.Exists(this.appdata_path + "SN_pref.txt"))
            {
                int line_count = 0;
                foreach (string line in File.ReadAllLines(this.appdata_path + "SN_pref.txt"))
                {
                    line_count++;

                    if (line_count == line_number)
                    {
                        string[] setting = line.Split('|');
                        return setting[1].Trim();
                    }
                }
                return "";
            }
            else
            {
                return "";
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            using (StreamWriter file = new StreamWriter(this.appdata_path + "SN_pref.txt", false))
            {
                file.WriteLine("MAIN URL | " + this.mskMainURL.Text);
                this.toolStripCancel.Enabled = false;
                this.toolStripSave.Enabled = false;
                this.toolStripEdit.Enabled = true;
                this.mskMainURL.Enabled = false;
            }
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.toolStripCancel.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripEdit.Enabled = false;
            this.mskMainURL.Enabled = true;
            this.mskMainURL.Focus();
            this.mskMainURL.SelectionStart = this.mskMainURL.Text.Length;
        }

        private void toolStripCancel_Click(object sender, EventArgs e)
        {
            this.loadPreferenceSettings();
            this.toolStripCancel.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripEdit.Enabled = true;
            this.mskMainURL.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                this.toolStripSave.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.toolStripCancel.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PreferenceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E && e.Alt)
            {
                this.toolStripEdit.PerformClick();
            }
        }

        private void PreferenceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(this.appdata_path + "SN_pref.txt"))
            {
                if (!this.toolStripEdit.Enabled)
                {
                    if (MessageAlert.Show("ท่านต้องการปิดหน้าต่างนี้ โดยไม่บันทึกสิ่งที่แก้ไขใช่หรือไม่?", "", MessageAlertButtons.YES_NO, MessageAlertIcons.QUESTION) == DialogResult.Yes)
                    {
                        this.toolStripCancel.PerformClick();
                        this.Close();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                MessageAlert.Show("ท่านจำเป็นต้องตั้งค่า Web API main url ก่อนเริ่มใช้งาน");
                e.Cancel = true;
            }
        }

        public static string API_MAIN_URL()
        {
            PreferenceForm pref = new PreferenceForm();
            return pref.readPreferenceLine(1);
        }

        private void mskMainURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                ((MaskedTextBox)sender).SelectionStart = ((MaskedTextBox)sender).Text.Length;
                e.Handled = true;
            }
        }
    }
}
