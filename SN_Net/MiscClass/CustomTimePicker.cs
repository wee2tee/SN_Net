using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    public partial class CustomTimePicker : UserControl
    {
        private bool read_only;
        public bool Read_Only
        {
            get
            {
                return this.read_only;
            }
            set
            {
                this.read_only = value;
                this.SetVisualControl();
            }
        }
        private DateTime time;
        public DateTime Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
                this.dateTimePicker1.Value = value;
            }
        }
        private bool show_second;
        public bool Show_Second
        {
            get
            {
                return this.show_second;
            }
            set
            {
                this.show_second = value;
                this.SetVisualControl();
            }
        }
        private string time_format;

        public CustomTimePicker()
        {
            InitializeComponent();

        }

        protected override void OnCreateControl()
        {
            this.BindingControlEvent();

            this.Time = DateTime.Now;
            base.OnCreateControl();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void BindingControlEvent()
        {
            this.dateTimePicker1.ValueChanged += delegate
            {
                this.label1.Text = this.dateTimePicker1.Value.ToString(this.time_format);
            };
            this.dateTimePicker1.TextChanged += delegate
            {
                string[] time = this.dateTimePicker1.Text.Split(':');
                this.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), 0);
            };
        }

        private void SetVisualControl()
        {
            if (this.read_only)
            {
                this.label1.Visible = true;
                this.dateTimePicker1.Visible = false;
            }
            else
            {
                this.label1.Visible = false;
                this.dateTimePicker1.Visible = true;
            }

            if (this.show_second)
            {
                this.time_format = "HH:mm:ss";
            }
            else
            {
                this.time_format = "HH:mm";
            }
            this.dateTimePicker1.CustomFormat = time_format;
        }
    }
}
