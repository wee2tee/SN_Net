using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Threading;

namespace CC
{
    public enum XMessageBoxIcon : int
    {
        None = 0,
        Information = 8,
        Question = 16,
        Warning = 32,
        Stop = 64,
        Error = 128
    }

    public partial class XMessageBox : Form
    {
        private PictureBox pctIcon;
        private Panel panel1;
        private Button btnOK;
        private Button btnCancel;
        private Button btnYes;
        private Button btnNo;
        private Button btnRetry;
        private Button btnAbort;
        private Button btnIgnore;
        private ImageList imageList1;
        private IContainer components;
        private Panel panelBtnContainer;
        private RichTextBox txtMessage;

        private CultureInfo c_info = new CultureInfo("th-TH");

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMessageBox));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.pctIcon = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelBtnContainer = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctIcon)).BeginInit();
            this.panelBtnContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.txtMessage);
            this.panel1.Controls.Add(this.pctIcon);
            this.panel1.Name = "panel1";
            // 
            // txtMessage
            // 
            resources.ApplyResources(this.txtMessage, "txtMessage");
            this.txtMessage.BackColor = System.Drawing.Color.White;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Name = "txtMessage";
            // 
            // pctIcon
            // 
            resources.ApplyResources(this.pctIcon, "pctIcon");
            this.pctIcon.Name = "pctIcon";
            this.pctIcon.TabStop = false;
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnYes
            // 
            resources.ApplyResources(this.btnYes, "btnYes");
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Name = "btnYes";
            this.btnYes.UseVisualStyleBackColor = true;
            // 
            // btnNo
            // 
            resources.ApplyResources(this.btnNo, "btnNo");
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Name = "btnNo";
            this.btnNo.UseVisualStyleBackColor = true;
            // 
            // btnRetry
            // 
            resources.ApplyResources(this.btnRetry, "btnRetry");
            this.btnRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.UseVisualStyleBackColor = true;
            // 
            // btnAbort
            // 
            resources.ApplyResources(this.btnAbort, "btnAbort");
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.UseVisualStyleBackColor = true;
            // 
            // btnIgnore
            // 
            resources.ApplyResources(this.btnIgnore, "btnIgnore");
            this.btnIgnore.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon_error.png");
            this.imageList1.Images.SetKeyName(1, "icon_info.png");
            this.imageList1.Images.SetKeyName(2, "icon_question.png");
            this.imageList1.Images.SetKeyName(3, "icon_stop.png");
            this.imageList1.Images.SetKeyName(4, "icon_warning.png");
            // 
            // panelBtnContainer
            // 
            resources.ApplyResources(this.panelBtnContainer, "panelBtnContainer");
            this.panelBtnContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panelBtnContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBtnContainer.Controls.Add(this.btnAbort);
            this.panelBtnContainer.Controls.Add(this.btnIgnore);
            this.panelBtnContainer.Controls.Add(this.btnRetry);
            this.panelBtnContainer.Controls.Add(this.btnNo);
            this.panelBtnContainer.Controls.Add(this.btnYes);
            this.panelBtnContainer.Controls.Add(this.btnCancel);
            this.panelBtnContainer.Controls.Add(this.btnOK);
            this.panelBtnContainer.Name = "panelBtnContainer";
            // 
            // XMessageBox
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelBtnContainer);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XMessageBox";
            this.ShowIcon = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.XMessageBox_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.XMessageBox_Paint);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctIcon)).EndInit();
            this.panelBtnContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private string message;
        private string title_text;
        private MessageBoxButtons msg_button = MessageBoxButtons.OK;
        private XMessageBoxIcon msg_icon = XMessageBoxIcon.None;
        private MessageBoxDefaultButton msg_def_button = MessageBoxDefaultButton.Button1;
        private List<Button> available_button = new List<Button>();

        //private XMessageBox()
        //{
        //    this.InitializeComponent();
        //}

        private XMessageBox(CultureInfo c_info = null)
        {
            if(c_info != null)
            {
                if(c_info.Name == "en-US" || c_info.Name == "th-TH")
                {
                    this.c_info = c_info;
                    Thread.CurrentThread.CurrentUICulture = this.c_info;
                }
            }
            this.InitializeComponent();
        }

        private void XMessageBox_Load(object sender, EventArgs e)
        {
            this.txtMessage.Text = this.message;
            this.Text = this.title_text;

            switch (this.msg_button)
            {
                case MessageBoxButtons.OK:
                    this.available_button.Add(this.btnOK);
                    this.SetButtonPosition();
                    break;
                case MessageBoxButtons.OKCancel:
                    this.available_button.Add(this.btnOK);
                    this.available_button.Add(this.btnCancel);
                    this.SetButtonPosition();
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    this.available_button.Add(this.btnAbort);
                    this.available_button.Add(this.btnRetry);
                    this.available_button.Add(this.btnIgnore);
                    this.SetButtonPosition();
                    break;
                case MessageBoxButtons.YesNoCancel:
                    this.available_button.Add(this.btnYes);
                    this.available_button.Add(this.btnNo);
                    this.available_button.Add(this.btnCancel);
                    this.SetButtonPosition();
                    break;
                case MessageBoxButtons.YesNo:
                    this.available_button.Add(this.btnYes);
                    this.available_button.Add(this.btnNo);
                    this.SetButtonPosition();
                    break;
                case MessageBoxButtons.RetryCancel:
                    this.available_button.Add(this.btnRetry);
                    this.available_button.Add(this.btnCancel);
                    this.SetButtonPosition();
                    break;
                default:
                    this.available_button.Add(this.btnOK);
                    this.SetButtonPosition();
                    break;
            }

            foreach (var btn in this.available_button)
            {
                btn.Visible = true;
            }

            switch (this.msg_icon)
            {
                case XMessageBoxIcon.None:
                    this.pctIcon.Visible = false;
                    this.txtMessage.Dock = DockStyle.Fill;
                    break;
                case XMessageBoxIcon.Information:
                    this.pctIcon.Image = this.imageList1.Images["icon_info.png"];
                    break;
                case XMessageBoxIcon.Question:
                    this.pctIcon.Image = this.imageList1.Images["icon_question.png"];
                    break;
                case XMessageBoxIcon.Warning:
                    this.pctIcon.Image = this.imageList1.Images["icon_warning.png"];
                    break;
                case XMessageBoxIcon.Stop:
                    this.pctIcon.Image = this.imageList1.Images["icon_stop.png"];
                    break;
                case XMessageBoxIcon.Error:
                    this.pctIcon.Image = this.imageList1.Images["icon_error.png"];
                    break;
                default:
                    this.pctIcon.Visible = false;
                    this.txtMessage.Dock = DockStyle.Fill;
                    break;
            }

            switch (this.msg_def_button)
            {
                case MessageBoxDefaultButton.Button1:
                    if(this.available_button.Count >= 1)
                    {
                        this.ActiveControl = this.available_button[0];
                    }
                    break;
                case MessageBoxDefaultButton.Button2:
                    if(this.available_button.Count >= 2)
                    {
                        this.ActiveControl = this.available_button[1];
                    }
                    break;
                case MessageBoxDefaultButton.Button3:
                    if(this.available_button.Count >= 3)
                    {
                        this.ActiveControl = this.available_button[2];
                    }
                    break;
                default:
                    if (this.available_button.Count >= 1)
                    {
                        this.ActiveControl = this.available_button[0];
                    }
                    break;
            }
        }

        private void SetButtonPosition()
        {
            var margin_right = this.btnAbort.Location.X + this.btnAbort.Width;

            //for (int i = 0; i < this.available_button.Count; i++)
            for (int i = this.available_button.Count - 1; i > -1; i--)
            {
                this.available_button[i].SetBounds(margin_right - this.available_button[i].Width, this.available_button[i].Bounds.Y, this.available_button[i].Bounds.Width, this.available_button[i].Bounds.Height);

                margin_right -= this.available_button[i].Bounds.Width + 5;
            }
        }

        private void XMessageBox_Paint(object sender, PaintEventArgs e)
        {
            var size = TextRenderer.MeasureText(e.Graphics, this.message, this.Font, new Size(this.txtMessage.Width - SystemInformation.VerticalScrollBarWidth, this.txtMessage.Height), TextFormatFlags.WordBreak);

            var diff_height = size.Height - this.txtMessage.Height;

            this.Height += diff_height;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (this.btnCancel.Visible)
                {
                    this.btnCancel.PerformClick();
                    return true;
                }

                if(this.available_button.Count == 1 && this.available_button[0].Name == this.btnOK.Name)
                {
                    this.btnOK.PerformClick();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static DialogResult Show(string message, CultureInfo c_info = null)
        {
            XMessageBox msgbox = new XMessageBox(c_info);
            msgbox.message = message;
            msgbox.title_text = string.Empty;

            return msgbox.ShowDialog();
        }


        public static DialogResult Show(string message, string title, CultureInfo c_info = null)
        {
            XMessageBox msgbox = new XMessageBox(c_info);
            msgbox.message = message;
            msgbox.title_text = title;

            return msgbox.ShowDialog();
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons msg_button, CultureInfo c_info = null)
        {
            XMessageBox msgbox = new XMessageBox(c_info);
            msgbox.message = message;
            msgbox.title_text = title;
            msgbox.msg_button = msg_button;

            return msgbox.ShowDialog();
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons msg_button, XMessageBoxIcon msg_icon, CultureInfo c_info = null)
        {
            XMessageBox msgbox = new XMessageBox(c_info);
            msgbox.message = message;
            msgbox.title_text = title;
            msgbox.msg_button = msg_button;
            msgbox.msg_icon = msg_icon;

            return msgbox.ShowDialog();
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons msg_button, XMessageBoxIcon msg_icon, MessageBoxDefaultButton msg_default_button, CultureInfo c_info = null)
        {
            XMessageBox msgbox = new XMessageBox(c_info);
            msgbox.message = message;
            msgbox.title_text = title;
            msgbox.msg_button = msg_button;
            msgbox.msg_icon = msg_icon;
            msgbox.msg_def_button = msg_default_button;

            return msgbox.ShowDialog();
        }
    }
}
