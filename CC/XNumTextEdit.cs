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
    public partial class XNumTextEdit : XTextEdit
    {
        private List<Keys> allowed_keys;
        private List<string> allowed_string;

        public XNumTextEdit()
        {
            InitializeComponent();

            #region declare allowed keys
            this.allowed_keys = new List<Keys>
            {
                Keys.D0,
                Keys.D1,
                Keys.D2,
                Keys.D3,
                Keys.D4,
                Keys.D5,
                Keys.D6,
                Keys.D7,
                Keys.D8,
                Keys.D9,
                Keys.NumPad0,
                Keys.NumPad1,
                Keys.NumPad2,
                Keys.NumPad3,
                Keys.NumPad4,
                Keys.NumPad5,
                Keys.NumPad6,
                Keys.NumPad7,
                Keys.NumPad8,
                Keys.NumPad9,
                //Keys.Decimal,
                Keys.NumLock,
                Keys.Left,
                Keys.Right,
                Keys.Up,
                Keys.Down,
                Keys.Shift,
                Keys.Control,
                Keys.Insert,
                Keys.Delete,
                Keys.Enter,
                Keys.Back,
                Keys.Home,
                Keys.End,
                (Keys.Shift | Keys.Left),
                (Keys.Shift | Keys.Right),
                (Keys.Shift | Keys.Home),
                (Keys.Shift | Keys.End),
                (Keys.Control | Keys.C),
                (Keys.Control | Keys.V),
                (Keys.Control | Keys.X)
            };
            #endregion declare allowed keys

            #region declare allowed char
            allowed_string = new List<string>
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9"
            };
            #endregion declare allowed char
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.allowed_keys.Where(k => k == e.KeyData).Count() == 0)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in ((TextBox)sender).Text.ToCharArray())
            {
                if(this.allowed_string.IndexOf(c.ToString()) < 0)
                {
                    ((TextBox)sender).Text = string.Empty;
                    break;
                }
            }
        }
    }
}
