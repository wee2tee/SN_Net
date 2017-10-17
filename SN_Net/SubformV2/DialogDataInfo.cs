using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.Subform
{
    public partial class DialogDataInfo : Form
    {
        private DbInfo info;

        public DialogDataInfo(DbInfo info)
        {
            this.info = info;
            InitializeComponent();
        }

        private void DialogDataInfo_Load(object sender, EventArgs e)
        {
            this.txtDbName._Text = this.info.DbName;
            this.txtTbName._Text = this.info.TbName;
            this.txtExpression._Text = this.info.Expression;
            this.txtCreBy._Text = this.info.CreBy;
            this.txtCreDat._Text = this.info.CreDat.HasValue ? this.info.CreDat.Value.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.GetCultureInfo("th-TH")) : string.Empty;
            this.txtChgBy._Text = this.info.ChgBy;
            this.txtChgDat._Text = this.info.ChgDat.HasValue ? this.info.ChgDat.Value.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.GetCultureInfo("th-TH")) : string.Empty;
            this.txtRowId._Text = this.info.RecId.ToString();
            this.txtTotalRow._Text = this.info.TotalRec.ToString();

            this.ActiveControl = this.btnOk;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter || keyData == Keys.Escape)
            {
                this.btnOk.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public class DbInfo
    {
        public string DbName { get; set; }
        public string TbName { get; set; }
        public string Expression { get; set; }
        public string CreBy { get; set; }
        public DateTime? CreDat { get; set; }
        public string ChgBy { get; set; }
        public DateTime? ChgDat { get; set; }
        public int RecId { get; set; }
        public int TotalRec { get; set; }
    }
}
