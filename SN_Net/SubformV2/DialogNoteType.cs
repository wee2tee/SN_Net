using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.Subform
{
    public partial class DialogNoteType : Form
    {
        public FormNote.TRNTYP trn_typ;

        public DialogNoteType()
        {
            InitializeComponent();
        }

        private void btnTel_Click(object sender, EventArgs e)
        {
            this.trn_typ = FormNote.TRNTYP.TEL;
        }

        private void btnBreak_Click(object sender, EventArgs e)
        {
            this.trn_typ = FormNote.TRNTYP.BREAK;
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            this.trn_typ = FormNote.TRNTYP.TRAIN;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Escape)
            {
                this.btnClose.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("x", new Font("MS Sans Serif", 8f, FontStyle.Regular), new SolidBrush(Color.DimGray), new Point(e.ClipRectangle.X + 3, e.ClipRectangle.Y));
        }
    }
}
