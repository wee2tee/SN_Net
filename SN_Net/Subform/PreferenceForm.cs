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
        public PreferenceForm()
        {
            InitializeComponent();
        }

        private void PreferenceForm_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;

            this.loadPreferenceSettings();
        }

        private void loadPreferenceSettings()
        {
            if (File.Exists("SN_pref.txt"))
            {
                this.txtMainURL.Text = this.readPreferenceLine(1);
            }
            else
            {
                this.txtMainURL.Text = "";
            }
        }

        private string readPreferenceLine(int line_number)
        {
            int line_count = 0;
            foreach (string line in File.ReadAllLines("SN_pref.txt"))
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

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            using (StreamWriter file = new StreamWriter("SN_pref.txt", false))
            {
                file.WriteLine("MAIN URL | " + this.txtMainURL.Text);
                this.toolStripCancel.Enabled = false;
                this.toolStripSave.Enabled = false;
                this.toolStripEdit.Enabled = true;
                this.txtMainURL.Enabled = false;
            }
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.toolStripCancel.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripEdit.Enabled = false;
            this.txtMainURL.Enabled = true;
            this.txtMainURL.Focus();
            this.txtMainURL.SelectionStart = this.txtMainURL.Text.Length;
        }

        private void toolStripCancel_Click(object sender, EventArgs e)
        {
            this.loadPreferenceSettings();
            this.toolStripCancel.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripEdit.Enabled = true;
            this.txtMainURL.Enabled = false;
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

        public static string API_MAIN_URL()
        {
            PreferenceForm pref = new PreferenceForm();
            return pref.readPreferenceLine(1);
        }
    }
}
