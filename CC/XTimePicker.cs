using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC
{
    public partial class XTimePicker : DateTimePicker
    {
        //const int WM_ERASEBKGND = 0x14;

        public XTimePicker()
        {
            InitializeComponent();
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "HH:mm:ss";
            this.ShowUpDown = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Console.WriteLine(" .. painted");
        }

        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    if (m.Msg == WM_ERASEBKGND)
        //    {
        //        Graphics g = Graphics.FromHdc(m.WParam);
        //        g.FillRectangle(new SolidBrush(AppResource.EditableControlBackColor), ClientRectangle);
        //        g.Dispose();
        //        return;
        //    }

        //    base.WndProc(ref m);
        //}
    }
}
