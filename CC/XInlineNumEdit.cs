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
    public partial class XInlineNumEdit : UserControl
    {
        public int _DecimalDigit
        {
            get
            {
                return this.xNumEdit1._DecimalDigit;
            }
            set
            {
                this.xNumEdit1._DecimalDigit = value;
            }
        }

        public decimal _MaximumValue
        {
            get
            {
                return this.xNumEdit1._MaximumValue;
            }
            set
            {
                this.xNumEdit1._MaximumValue = value;
            }
        }

        public bool _UseThoundsandSeparate
        {
            get
            {
                return this.xNumEdit1._UseThoundsandSeparate;
            }
            set
            {
                this.xNumEdit1._UseThoundsandSeparate = value;
            }
        }

        public decimal _Value
        {
            get
            {
                return this.xNumEdit1._Value;
            }
            set
            {
                this.xNumEdit1._Value = value;
            }
        }

        public event EventHandler _ButtonOKClicked;
        public event EventHandler _ButtonCancelClicked;

        public XInlineNumEdit()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_ButtonOKClicked != null)
                this._ButtonOKClicked(this, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_ButtonCancelClicked != null)
                this._ButtonCancelClicked(this, e);
        }
    }
}
