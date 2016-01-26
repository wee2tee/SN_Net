using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace SN_Net.MiscClass
{
    public partial class CustomDateTimePicker : UserControl
    {
        private bool read_Only;
        private string texts;
        private string textMysql;
        private CultureInfo cinfo_us = new CultureInfo("en-US");
        private CultureInfo cinfo_th = new CultureInfo("th-TH");
        public bool calendar_shown;

        public CustomDateTimePicker()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            InitializeComponent();
            this.BindEventWithChildControl();
            this.Read_Only = true;
        }

        private void CustomDateTimePicker_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Now;
            this.textBox1.Mask = "00/00/0000";
            this.textBox1.PromptChar = ' ';
            this.textBox1.Text = "";
            this.label1.Text = "  /  /    ";
            this.BackColor = Color.White;
            this.textBox1.BackColor = Color.White;
            this.textBox1.ForeColor = Color.Black;
            this.label1.BackColor = Color.White;
            this.label1.ForeColor = Color.Black;
            this.Height = 23;
            this.Width = 96;
        }

        public bool Read_Only
        {
            get
            {
                return this.read_Only;
            }
            set
            {
                this.read_Only = value;
                this.SetControlState();
            }
        }

        /**
         * Get or Set Text value with format "dd/mm/yyyy" (Thai year format)
         * */
        public string Texts
        {
            get
            {
                return this.texts;
            }
            set
            {
                if (value != "  /  /  " && value != "")
                {
                    DateTime out_datetime;
                    if (DateTime.TryParse(value, cinfo_th, DateTimeStyles.None, out out_datetime))
                    {
                        this.texts = value;

                        string dt = out_datetime.ToString("yyyy-MM-dd", cinfo_th.DateTimeFormat);
                        this.textMysql = (Convert.ToInt32(dt.Substring(0, 4)) - 543).ToString() + "-" + dt.Substring(5, 2) + "-" + dt.Substring(8, 2);

                        this.textBox1.Text = value;
                        this.label1.Text = value;
                    }
                    else
                    {
                        this.texts = "";
                        this.textMysql = "";
                        this.textBox1.Text = "  /  /    ";
                        this.label1.Text = "  /  /    ";
                    }
                }
                else
                {
                    this.texts = "";
                    this.textMysql = "";
                    this.textBox1.Text = "  /  /    ";
                    this.label1.Text = "  /  /    ";
                }
            }
        }

        /**
         * Get or Set Text value with format "yyyy-mm-dd" (Mysql format)
         * */
        public string TextsMysql
        {
            get
            {
                return this.textMysql;
            }
            set
            {
                if (value != null && value.Length != 0)
                {
                    string add_year = (Convert.ToInt32(value.Substring(0, 4)) + 543).ToString() + value.Substring(4, 6);
                    DateTime out_datetime;
                    if (DateTime.TryParse(add_year, cinfo_th, DateTimeStyles.None, out out_datetime))
                    {
                        this.texts = value;

                        string dt = out_datetime.ToString("yyyy-MM-dd", cinfo_th.DateTimeFormat);
                        this.textMysql = (Convert.ToInt32(dt.Substring(0, 4)) - 543).ToString() + "-" + dt.Substring(5, 2) + "-" + dt.Substring(8, 2);

                        this.textBox1.Text = dt.Substring(8, 2) + "/" + dt.Substring(5, 2) + "/" + dt.Substring(0, 4);
                        this.label1.Text = dt.Substring(8, 2) + "/" + dt.Substring(5, 2) + "/" + dt.Substring(0, 4); ;
                        this.dateTimePicker1.Value = out_datetime;
                    }
                    else
                    {
                        this.texts = "";
                        this.textMysql = "";
                        this.textBox1.Text = "  /  /    ";
                        this.label1.Text = "  /  /    ";
                    }
                }
                else
                {
                    this.texts = "";
                    this.textMysql = "";
                    this.textBox1.Text = "  /  /    ";
                    this.label1.Text = "  /  /    ";
                }
            }
        }

        /**
         * Set DateTime value
         * */
        public DateTime ValDateTime
        {
            get
            {
                return this.dateTimePicker1.Value;
            }
            set
            {
                this.dateTimePicker1.Value = value;
            }
        }
        
        private void SetControlState()
        {
            if (this.read_Only)
            {
                this.textBox1.Visible = false;
                this.textBox1.Enabled = false;
                this.label1.Visible = true;
                this.dateTimePicker1.Enabled = false;
            }
            else
            {
                this.textBox1.Visible = true;
                this.textBox1.Enabled = true;
                this.label1.Visible = false;
                this.dateTimePicker1.Enabled = true;
            }
        }

        private void BindEventWithChildControl()
        {
            this.textBox1.Leave += delegate
            {
                this.BackColor = Color.White;
                this.textBox1.BackColor = Color.White;

                DateTime out_datetime;
                if(DateTime.TryParse(this.textBox1.Text, cinfo_th, DateTimeStyles.None, out out_datetime))
                {
                    DateTime dt = Convert.ToDateTime(this.textBox1.Text, cinfo_th);
                    this.Texts = dt.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
                }
                else
                {
                    this.dateTimePicker1.Value = DateTime.Now;
                    this.Texts = "  /  /    ";
                }
            };

            this.dateTimePicker1.Enter += delegate
            {
                if (!this.read_Only)
                {
                    //this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                    //this.textBox1.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                    //if (this.textBox1.Text.tryParseToDateTime())
                    //{
                    //    DateTime dt = Convert.ToDateTime(this.textBox1.Text, cinfo_th);
                    //    this.dateTimePicker1.Value = dt;
                    //}
                    //else
                    //{
                    //    this.dateTimePicker1.Value = DateTime.Now;
                    //    this.textBox1.Text = "  /  /    ";
                    //}
                }
            };

            this.dateTimePicker1.ValueChanged += delegate
            {
                this.Texts = this.dateTimePicker1.Value.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
                
                //this.textBox1.Text = this.dateTimePicker1.Value.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
                //this.label1.Text = this.textBox1.Text;
            };

            this.dateTimePicker1.DropDown += delegate
            {
                this.calendar_shown = true;
            };

            this.dateTimePicker1.CloseUp += delegate
            {
                this.textBox1.Focus();
                this.calendar_shown = false;
            };

            this.textBox1.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.F6)
                {
                    this.dateTimePicker1.Focus();
                    SendKeys.Send("{F4}");
                }
            };

            this.textBox1.Leave += delegate
            {
                DateTime out_date;
                if (DateTime.TryParse(this.textBox1.Text, out out_date))
                {
                    if (out_date >= this.dateTimePicker1.MinDate && out_date <= this.dateTimePicker1.MaxDate)
                    {
                        this.dateTimePicker1.Value = out_date;
                    }
                    else
                    {
                        this.Texts = "";
                        SendKeys.Send("{F6}");
                    }

                }
            };

            this.textBox1.GotFocus += delegate
            {
                this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.textBox1.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
            };
        }

        #region Protected method
        protected override void OnSizeChanged(EventArgs e)
        {
            this.Width = 96;
            this.Height = 23;
            base.OnSizeChanged(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            if (!this.read_Only)
            {
                this.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.textBox1.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                this.textBox1.ForeColor = Color.Black;
            }
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this.BackColor = Color.White;
            this.textBox1.BackColor = Color.White;
            this.textBox1.ForeColor = Color.Black;
            base.OnLeave(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (this.calendar_shown)
                {
                    this.dateTimePicker1.Focus();
                    SendKeys.Send("{F4}");
                    this.textBox1.Focus();
                    return true;
                }
            }

            if (keyData == Keys.F6)
            {
                if (!this.calendar_shown)
                {
                    this.dateTimePicker1.Focus();
                    SendKeys.Send("{F4}");
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion Protected method
    }
}
