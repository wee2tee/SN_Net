using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC
{
    public partial class XTextBox : TextBox
    {
        

        public XTextBox()
        {
            
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.BackColor = Color.White;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (!this.ReadOnly && this.Enabled)
            {
                this.BackColor = AppResource.EditableControlBackColor;
            }
            else
            {
                this.BackColor = Color.White;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.BackColor = Color.White;
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            base.OnReadOnlyChanged(e);
            if (this.ReadOnly)
            {
                this.BackColor = Color.White;
            }
            else
            {
                if (this.Focused)
                {
                    this.BackColor = AppResource.EditableControlBackColor;
                }
                else
                {
                    this.BackColor = Color.White;
                }
            }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Console.WriteLine(" .. >> parent is " + this.Parent.Name);
        //    Rectangle rect = this.Parent.Bounds;
        //    e.Graphics.DrawRectangle(new Pen(Color.Blue), rect.X - 2, rect.Y - 2, rect.Width + 4, rect.Height + 4);
        //}

        //protected override void OnReadOnlyChanged(EventArgs e)
        //{
        //    base.OnReadOnlyChanged(e);

        //    if (this.ReadOnly)
        //    {
        //        this.BackColor = Color.Black;
        //        this.ForeColor = Color.White;
        //    }
        //    else
        //    {
        //        this.BackColor = Color.White;
        //        this.ForeColor = Color.Black;
        //    }
        //}
    }
}
