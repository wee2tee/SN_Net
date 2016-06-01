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
        const string MAIN_URL = "MAIN_URL";
        const string BREAK_TIME_METHOD = "BREAK_TIME_METHOD";
        const string SEARCH_NOTE_METHOD = "SEARCH_NOTE_METHOD";
        const string SEARCH_NOTE_DATE = "SEARCH_NOTE_DATE";
        public enum BREAK_TIME : int
        {
            AUTO = 1,
            MANUAL = 2
        }

        public enum SEARCH_NOTE : int
        {
            PRIVATE = 1,
            PUBLIC = 2
        }

        public enum SEARCH_DATE : int
        {
            CURRENT_DATE = 1,
            BACKWARD_WEEK = 2,
            BACKWARD_MONTH = 3,
            BACKWARD_YEAR = 4
        }

        public PreferenceForm()
        {
            InitializeComponent();
        }

        private void PreferenceForm_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
            
            this.LoadDependenciesData();
            this.loadPreferenceSettings();
        }

        private void LoadDependenciesData()
        {
            this.cbBreakTimeMethod.Items.Add(new ComboboxItem("อัตโนมัติโดยระบบ (Default)", (int)BREAK_TIME.AUTO, "AUTO"));
            this.cbBreakTimeMethod.Items.Add(new ComboboxItem("กำหนดเองโดยผู้ใช้งาน", (int)BREAK_TIME.MANUAL, "MANUAL"));

            this.cbSearchMethod.Items.Add(new ComboboxItem("ค้นหาเฉพาะบันทึกของผู้ใชงานปัจจุบัน (Default)", (int)SEARCH_NOTE.PRIVATE, "PRIVATE"));
            this.cbSearchMethod.Items.Add(new ComboboxItem("ค้นหาทั้งหมด(รวมถึงบันทึกของผู้ใช้รายอื่น ๆ ด้วย)", (int)SEARCH_NOTE.PUBLIC, "PUBLIC"));

            this.cbSearchDate.Items.Add(new ComboboxItem("เฉพาะวันที่ปัจจุบัน (Default)", (int)SEARCH_DATE.CURRENT_DATE, "CURRENT_DATE"));
            this.cbSearchDate.Items.Add(new ComboboxItem("ย้อนหลัง 7 วัน", (int)SEARCH_DATE.BACKWARD_WEEK, "BACKWARD_WEEK"));
            this.cbSearchDate.Items.Add(new ComboboxItem("ย้อนหลัง 30 วัน", (int)SEARCH_DATE.BACKWARD_MONTH, "BACKWARD_MONTH"));
            this.cbSearchDate.Items.Add(new ComboboxItem("ย้อนหลัง 365 วัน", (int)SEARCH_DATE.BACKWARD_YEAR, "BACKWARD_YEAR"));
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
            if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "SN_pref.txt")))
            {
                this.mskMainURL.Text = this.readPreferenceLine(MAIN_URL);
                if(this.readPreferenceLine(BREAK_TIME_METHOD).Length > 0 && Convert.ToInt32(this.readPreferenceLine(BREAK_TIME_METHOD)) != 0){
                    this.cbBreakTimeMethod.SelectedItem = this.cbBreakTimeMethod.Items.Cast<ComboboxItem>().Where(t => t.int_value == Convert.ToInt32(this.readPreferenceLine(BREAK_TIME_METHOD))).First<ComboboxItem>();
                }
                else{
                    this.cbBreakTimeMethod.SelectedIndex = 0;
                }

                if (this.readPreferenceLine(SEARCH_NOTE_METHOD).Length > 0 && Convert.ToInt32(this.readPreferenceLine(SEARCH_NOTE_METHOD)) != 0)
                {
                    this.cbSearchMethod.SelectedItem = this.cbSearchMethod.Items.Cast<ComboboxItem>().Where(t => t.int_value == Convert.ToInt32(this.readPreferenceLine(SEARCH_NOTE_METHOD))).First<ComboboxItem>();
                }
                else
                {
                    this.cbSearchMethod.SelectedIndex = 0;
                }

                if (this.readPreferenceLine(SEARCH_NOTE_DATE).Length > 0 && Convert.ToInt32(this.readPreferenceLine(SEARCH_NOTE_DATE)) != 0)
                {
                    this.cbSearchDate.SelectedItem = this.cbSearchDate.Items.Cast<ComboboxItem>().Where(t => t.int_value == Convert.ToInt32(this.readPreferenceLine(SEARCH_NOTE_DATE))).First<ComboboxItem>();
                }
                else
                {
                    this.cbSearchDate.SelectedIndex = 0;
                }
            }
            else
            {
                this.mskMainURL.Text = "";
                this.cbBreakTimeMethod.SelectedIndex = 0;
                this.cbSearchMethod.SelectedIndex = 0;
            }
        }

        private string readPreferenceLine(string key)
        {
            if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "SN_pref.txt")))
            {
                //int line_count = 0;
                foreach (string line in File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "SN_pref.txt")))
                {
                    //line_count++;
                    if (!(line.Contains("|")))
                        continue;

                    string[] conf = line.Split('|');

                    if (conf[0].Trim() == key)
                    {
                        //string[] setting = line.Split('|');
                        //return setting[1].Trim();
                        return conf[1].Trim();
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
            //using (StreamWriter file = new StreamWriter(this.appdata_path + "SN_pref.txt", false))
            using (StreamWriter file = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "SN_pref.txt"), false))
            {
                file.WriteLine(MAIN_URL + " | " + this.mskMainURL.Text);
                file.WriteLine(BREAK_TIME_METHOD + " | " + ((ComboboxItem)this.cbBreakTimeMethod.SelectedItem).int_value.ToString());
                file.WriteLine(SEARCH_NOTE_METHOD + " | " + ((ComboboxItem)this.cbSearchMethod.SelectedItem).int_value.ToString());
                file.WriteLine(SEARCH_NOTE_DATE + " | " + ((ComboboxItem)this.cbSearchDate.SelectedItem).int_value.ToString());
                this.toolStripCancel.Enabled = false;
                this.toolStripSave.Enabled = false;
                this.toolStripEdit.Enabled = true;
                this.mskMainURL.Enabled = false;
                this.cbBreakTimeMethod.Enabled = false;
                this.cbSearchMethod.Enabled = false;
                this.cbSearchDate.Enabled = false;
            }
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.toolStripCancel.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripEdit.Enabled = false;
            this.mskMainURL.Enabled = true;
            this.cbBreakTimeMethod.Enabled = true;
            this.cbSearchMethod.Enabled = true;
            this.cbSearchDate.Enabled = true;
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
            this.cbBreakTimeMethod.Enabled = false;
            this.cbSearchMethod.Enabled = false;
            this.cbSearchDate.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.E))
            {
                this.toolStripEdit.PerformClick();
                return true;
            }
            if (keyData == Keys.F9)
            {
                this.toolStripSave.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.toolStripCancel.PerformClick();
                return true;
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
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "SN_pref.txt")))
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

        private void mskMainURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                ((MaskedTextBox)sender).SelectionStart = ((MaskedTextBox)sender).Text.Length;
                e.Handled = true;
            }
        }

        public static string API_MAIN_URL()
        {
            PreferenceForm pref = new PreferenceForm();
            return pref.readPreferenceLine(MAIN_URL);
        }

        public static int BREAK_TIME_METHOD_CONFIGURATION()
        {
            PreferenceForm pref = new PreferenceForm();
            return (pref.readPreferenceLine(BREAK_TIME_METHOD).Length == 0 ? (int)BREAK_TIME.AUTO : Convert.ToInt32(pref.readPreferenceLine(BREAK_TIME_METHOD)));
        }

        public static int SEARCH_NOTE_METHOD_CONFIGURATION()
        {
            PreferenceForm pref = new PreferenceForm();
            return (pref.readPreferenceLine(SEARCH_NOTE_METHOD).Length == 0 ? (int)SEARCH_NOTE.PRIVATE : Convert.ToInt32(pref.readPreferenceLine(SEARCH_NOTE_METHOD)));
        }

        public static int SEARCH_NOTE_DATE_CONFIGURATION()
        {
            PreferenceForm pref = new PreferenceForm();
            return (pref.readPreferenceLine(SEARCH_NOTE_DATE).Length == 0 ? (int)SEARCH_DATE.CURRENT_DATE : Convert.ToInt32(pref.readPreferenceLine(SEARCH_NOTE_DATE)));
        }
    }
}
