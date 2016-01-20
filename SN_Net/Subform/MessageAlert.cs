using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.MiscClass;

namespace SN_Net.Subform
{
    public partial class MessageAlert : Form
    {
        

        public MessageAlert()
        {
            InitializeComponent();
        }

        private void MessageAlert_Load(object sender, EventArgs e)
        {
            
        }

        public static DialogResult Show(string message, string caption, MessageAlertButtons button, MessageAlertIcons icon)
        {
            MessageAlert m = new MessageAlert();
            m.lblMessage.Text = message;
            m.Text = caption;
            m.setIconImage(icon);

            switch (button)
            {
                case MessageAlertButtons.OK:
                    m.btnOK.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnOK.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnOK.Height) / 2), m.btnOK.Width, m.btnOK.Height);
                    m.btnOK.Visible = true;
                    break;
                case MessageAlertButtons.OK_CANCEL:
                    m.btnCancel.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnCancel.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnCancel.Height) / 2), m.btnCancel.Width, m.btnCancel.Height);
                    m.btnOK.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnCancel.Width + 10 + m.btnOK.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnOK.Height) / 2), m.btnOK.Width, m.btnOK.Height);
                    m.btnOK.Visible = true;
                    m.btnCancel.Visible = true;
                    m.btnCancel.Focus();
                    break;
                case MessageAlertButtons.YES:
                    m.btnYes.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnYes.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnYes.Height) / 2), m.btnYes.Width, m.btnYes.Height);
                    m.btnYes.Visible = true;
                    break;
                case MessageAlertButtons.YES_NO:
                    m.btnNo.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnNo.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnNo.Height) / 2), m.btnNo.Width, m.btnNo.Height);
                    m.btnYes.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnNo.Width + 10 + m.btnYes.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnYes.Height) / 2), m.btnYes.Width, m.btnYes.Height);
                    m.btnYes.Visible = true;
                    m.btnNo.Visible = true;
                    break;
                case MessageAlertButtons.RETRY_CANCEL:
                    m.btnCancel.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnCancel.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnCancel.Height) / 2), m.btnCancel.Width, m.btnCancel.Height);
                    m.btnRetry.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnCancel.Width + 10 + m.btnRetry.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnRetry.Height) / 2), m.btnRetry.Width, m.btnRetry.Height);
                    m.btnRetry.Visible = true;
                    m.btnCancel.Visible = true;
                    m.btnCancel.Focus();
                    break;
                default:
                    break;
            }
            return m.ShowDialog();
        }
        
        public static DialogResult Show(string message, string caption, MessageAlertButtons button)
        {
            MessageAlert m = new MessageAlert();
            m.lblMessage.Text = message;
            m.Text = caption;
            // Set lblMessage position,size , hide pictureBoxIcon
            m.pictureBoxIcon.SetBounds(0, 0, 0, 0);
            m.lblMessage.SetBounds(m.lblMessage.Location.X - 55, m.lblMessage.Location.Y, m.lblMessage.ClientSize.Width + 55, m.lblMessage.ClientSize.Height);

            switch (button)
            {
                case MessageAlertButtons.OK:
                    m.btnOK.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnOK.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnOK.Height) / 2), m.btnOK.Width, m.btnOK.Height);
                    m.btnOK.Visible = true;
                    break;
                case MessageAlertButtons.OK_CANCEL:
                    m.btnCancel.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnCancel.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnCancel.Height) / 2), m.btnCancel.Width, m.btnCancel.Height);
                    m.btnOK.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnCancel.Width + 10 + m.btnOK.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnOK.Height) / 2), m.btnOK.Width, m.btnOK.Height);
                    m.btnOK.Visible = true;
                    m.btnCancel.Visible = true;
                    break;
                case MessageAlertButtons.YES:
                    m.btnYes.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnYes.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnYes.Height) / 2), m.btnYes.Width, m.btnYes.Height);
                    m.btnYes.Visible = true;
                    break;
                case MessageAlertButtons.YES_NO:
                    m.btnNo.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnNo.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnNo.Height) / 2), m.btnNo.Width, m.btnNo.Height);
                    m.btnYes.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnNo.Width + 10 + m.btnYes.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnYes.Height) / 2), m.btnYes.Width, m.btnYes.Height);
                    m.btnYes.Visible = true;
                    m.btnNo.Visible = true;
                    break;
                case MessageAlertButtons.RETRY_CANCEL:
                    m.btnCancel.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnCancel.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnCancel.Height) / 2), m.btnCancel.Width, m.btnCancel.Height);
                    m.btnRetry.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnCancel.Width + 10 + m.btnRetry.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnRetry.Height) / 2), m.btnRetry.Width, m.btnRetry.Height);
                    m.btnRetry.Visible = true;
                    m.btnCancel.Visible = true;
                    break;
                default:
                    break;
            }
            return m.ShowDialog();
        }
        
        public static DialogResult Show(string message, string caption)
        {
            MessageAlert m = new MessageAlert();
            m.lblMessage.Text = message;
            m.Text = caption;
            // Set lblMessage position,size , hide pictureBoxIcon
            m.pictureBoxIcon.SetBounds(0, 0, 0, 0);
            m.lblMessage.SetBounds(m.lblMessage.Location.X - 55, m.lblMessage.Location.Y, m.lblMessage.ClientSize.Width + 55, m.lblMessage.ClientSize.Height);
            // Set OK button to show
            m.btnOK.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnOK.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnOK.Height) / 2), m.btnOK.Width, m.btnOK.Height);
            m.btnOK.Visible = true;

            return m.ShowDialog();
        }

        public static DialogResult Show(string message)
        {
            MessageAlert m = new MessageAlert();
            m.lblMessage.Text = message;
            // Set form caption to blank
            m.Text = "";
            // Set lblMessage position,size , hide pictureBoxIcon
            m.pictureBoxIcon.SetBounds(0, 0, 0, 0);
            m.lblMessage.SetBounds(m.lblMessage.Location.X - 55, m.lblMessage.Location.Y, m.lblMessage.ClientSize.Width + 55, m.lblMessage.ClientSize.Height);
            // Set OK button to show
            m.btnOK.SetBounds(m.panelBtn.ClientSize.Width - (10 + m.btnOK.Width), Convert.ToInt32((m.panelBtn.ClientSize.Height - m.btnOK.Height) / 2), m.btnOK.Width, m.btnOK.Height);
            m.btnOK.Visible = true;

            return m.ShowDialog();
        }


        private void setIconImage(MessageAlertIcons icon)
        {
            switch (icon)
            {
                case MessageAlertIcons.ERROR:
                    this.pictureBoxIcon.Image = imageListIcon.Images["icon-error.png"];
                    break;
                    
                case MessageAlertIcons.INFORMATION:
                    this.pictureBoxIcon.Image = imageListIcon.Images["icon-info.png"];
                    break;

                case MessageAlertIcons.QUESTION:
                    this.pictureBoxIcon.Image = imageListIcon.Images["icon-question.png"];
                    break;

                case MessageAlertIcons.STOP:
                    this.pictureBoxIcon.Image = imageListIcon.Images["icon-stop.png"];
                    break;

                case MessageAlertIcons.WARNING:
                    this.pictureBoxIcon.Image = imageListIcon.Images["icon-warning.png"];
                    break;

                default:
                    this.pictureBoxIcon.SetBounds(0, 0, 0, 0);
                    this.lblMessage.SetBounds(this.lblMessage.Location.X - 55, this.lblMessage.Location.Y, this.lblMessage.ClientSize.Width + 55, this.lblMessage.ClientSize.Height);
                    break;
            }
        }

        private void escapeToClose(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void MessageAlert_Shown(object sender, EventArgs e)
        {
            this.btnCancel.GotFocus += new EventHandler(this.buttonGotFocusHandler);
            this.btnNo.GotFocus += new EventHandler(this.buttonGotFocusHandler);
            this.btnOK.GotFocus += new EventHandler(this.buttonGotFocusHandler);
            this.btnRetry.GotFocus += new EventHandler(this.buttonGotFocusHandler);
            this.btnYes.GotFocus += new EventHandler(this.buttonGotFocusHandler);
            this.btnCancel.Leave += new EventHandler(this.buttonLeaveFocusHandler);
            this.btnNo.Leave += new EventHandler(this.buttonLeaveFocusHandler);
            this.btnOK.Leave += new EventHandler(this.buttonLeaveFocusHandler);
            this.btnRetry.Leave += new EventHandler(this.buttonLeaveFocusHandler);
            this.btnYes.Leave += new EventHandler(this.buttonLeaveFocusHandler);

            if (this.btnNo.Visible)
            {
                this.btnNo.Focus();
            }
            if (this.btnCancel.Visible)
            {
                this.btnCancel.Focus();
            }
        }

        private void buttonGotFocusHandler(object sender, EventArgs e)
        {
            //((Button)sender).BackColor = Color.LightCyan;
        }

        private void buttonLeaveFocusHandler(object sender, EventArgs e)
        {
            //((Button)sender).BackColor = Color.FromKnownColor(KnownColor.ControlLightLight);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right)
            {
                SendKeys.Send("{TAB}");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public enum MessageAlertButtons
    {
        OK,
        OK_CANCEL,
        YES,
        YES_NO,
        RETRY_CANCEL
    }
    public enum MessageAlertIcons
    {
        ERROR,
        INFORMATION,
        NONE,
        QUESTION,
        STOP,
        WARNING
    }
}
