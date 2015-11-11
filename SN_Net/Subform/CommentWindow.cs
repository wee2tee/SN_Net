using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;
using NAudio;
using NAudio.Wave;
using System.IO;

namespace SN_Net.Subform
{
    public partial class CommentWindow : Form
    {
        private int note_id;
        private SupportStatWindow parent_wind;
        private SupportNoteComment comment;
        //private IWavePlayer waveOutDevice;
        private DirectSoundOut waveOutDevice;
        private MediaFoundationReader mediaReader;
        private Timer timer_play;

        public enum COMMENT_TYPE : int
        {
            COMMENT = 1,
            COMPLAIN = 2
        }

        public CommentWindow(SupportStatWindow parent_wind)
        {
            InitializeComponent();

            this.parent_wind = parent_wind;
            this.note_id = ((Note)parent_wind.dgvNote.Rows[parent_wind.dgvNote.CurrentCell.RowIndex].Tag).id;
            this.comment = parent_wind.supportnotecomment_list.Find(t => t.note_id == this.note_id);

            this.BindingControlEvent();
        }

        private void CommentWindow_Load(object sender, EventArgs e)
        {
            this.axWindowsMediaPlayer1.settings.autoStart = false;
            this.axWindowsMediaPlayer1.Ctlenabled = false;
            this.axWindowsMediaPlayer1.enableContextMenu = false;
        }

        private void CommentWindow_Shown(object sender, EventArgs e)
        {
            if (this.comment != null)
            {
                this.chComment.CheckState = (this.comment.type == (int)COMMENT_TYPE.COMMENT ? CheckState.Checked : CheckState.Unchecked);
                this.chComplain.CheckState = (this.comment.type == (int)COMMENT_TYPE.COMPLAIN ? CheckState.Checked : CheckState.Unchecked);
                this.txtDescription.Text = this.comment.description;
                this.txtFilePath.Text = this.comment.file_path;
            }
        }

        private void BindingControlEvent()
        {
            this.chComment.CheckStateChanged += delegate
            {
                if (this.chComment.CheckState == CheckState.Checked)
                {
                    this.chComplain.CheckState = CheckState.Unchecked;
                    this.txtDescription.Enabled = true;
                    this.btnBrowse.Enabled = true;
                }
                else
                {
                    if (this.chComplain.CheckState == CheckState.Unchecked)
                    {
                        this.txtDescription.Text = "";
                        this.txtDescription.Enabled = false;
                        this.btnBrowse.Enabled = false;
                    }
                }
            };

            this.chComplain.CheckStateChanged += delegate
            {
                if (this.chComplain.CheckState == CheckState.Checked)
                {
                    this.chComment.CheckState = CheckState.Unchecked;
                    this.txtDescription.Enabled = true;
                    this.btnBrowse.Enabled = true;
                }
                else
                {
                    if (this.chComment.CheckState == CheckState.Unchecked)
                    {
                        this.txtDescription.Text = "";
                        this.txtDescription.Enabled = false;
                        this.btnBrowse.Enabled = false;
                    }
                }
            };

            #region Use Naudio Library
            //this.txtFilePath.TextChanged += delegate
            //{
            //    if (this.mediaReader != null)
            //    {
            //        this.ClearMediaPlayerObject();
            //    }

            //    if (this.txtFilePath.Text.Length > 0)
            //    {
            //        this.btnDeletePath.Enabled = true;
            //        this.btnPlay.Enabled = true;
            //        this.btnPlay.Enabled = true;
            //        this.btnPause.Enabled = false;
            //        this.btnStop.Enabled = false;
            //    }
            //    else
            //    {
            //        this.btnDeletePath.Enabled = false;
            //        this.btnPlay.Enabled = false;
            //        this.btnPause.Enabled = false;
            //        this.btnStop.Enabled = false;
            //    }
            //};
            #endregion Use Naudio Library

            this.txtFilePath.TextChanged += delegate
            {
                this.btnDeletePath.Enabled = (this.txtFilePath.Text.Length > 0 ? true : false);
                if (this.txtFilePath.Text.Length > 0 && File.Exists(this.txtFilePath.Text))
                {
                    this.axWindowsMediaPlayer1.URL = this.txtFilePath.Text;
                    this.axWindowsMediaPlayer1.Ctlenabled = true;
                }
                else
                {
                    this.axWindowsMediaPlayer1.Ctlcontrols.stop();
                    this.axWindowsMediaPlayer1.URL = "";
                    this.axWindowsMediaPlayer1.Ctlenabled = false;
                }
            };

            #region Use Naudio Library
            //this.trackBar1.ValueChanged += delegate
            //{
            //    this.lblCurrentDuration.Text = TimeSpan.FromSeconds((double)this.trackBar1.Value).ToString();
            //};

            //this.trackBar1.MouseDown += delegate(object sender, MouseEventArgs e)
            //{
            //    this.btnPause.PerformClick();
            //};
            
            //this.trackBar1.MouseUp += delegate(object sender, MouseEventArgs e)
            //{
            //    this.mediaReader.CurrentTime = TimeSpan.FromSeconds(this.trackBar1.Value);
            //    this.btnPlay.PerformClick();
            //};
            #endregion Use Naudio Library
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.chComment.CheckState == CheckState.Checked || this.chComplain.CheckState == CheckState.Checked) // Create or Update comment
            {
                SupportNoteComment comment = this.GetCommentDataInForm();

                string json_data = "{\"note_id\":\"" + comment.note_id.ToString() + "\",";
                json_data += "\"type\":\"" + comment.type + "\",";
                json_data += "\"description\":\"" + comment.description + "\",";
                json_data += "\"file_path\":\"" + comment.file_path + "\",";
                json_data += "\"rec_by\":\"" + comment.rec_by + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "supportnotecomment/create_or_update", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
            else // Delete comment
            {
                if (this.comment != null)
                {
                    CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "supportnotecomment/delete&id=" + this.comment.id.ToString());
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageAlert.Show("เกิดข้อผิดพลาด", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
        }

        private SupportNoteComment GetCommentDataInForm()
        {
            SupportNoteComment comment = new SupportNoteComment();
            comment.note_id = this.note_id;
            comment.type = (this.chComment.CheckState == CheckState.Checked ? (int)COMMENT_TYPE.COMMENT : (this.chComplain.CheckState == CheckState.Checked ? (int)COMMENT_TYPE.COMPLAIN : 0));
            comment.description = this.txtDescription.Text.cleanString();
            comment.file_path = this.txtFilePath.Text.cleanString();
            comment.rec_by = this.parent_wind.main_form.G.loged_in_user_name;

            return comment;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }
            if (keyData == Keys.F9)
            {
                this.btnOK.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = this.openFileDialog1.FileName;
            }
        }

        private void btnDeletePath_Click(object sender, EventArgs e)
        {
            this.txtFilePath.Text = "";
            this.openFileDialog1.FileName = "";
        }

        #region Use Naudio Library
        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.mediaReader == null || this.waveOutDevice == null)
                {
                    if (File.Exists(this.txtFilePath.Text))
                    {
                        //this.waveOutDevice = new WaveOut();
                        this.waveOutDevice = new DirectSoundOut();
                        this.mediaReader = new MediaFoundationReader(this.txtFilePath.Text);
                        this.waveOutDevice.Init(this.mediaReader);
                        this.trackBar1.Maximum = (int)this.mediaReader.TotalTime.TotalSeconds;
                        this.trackBar1.LargeChange = (this.trackBar1.Maximum <= 20 ? 1 : (this.trackBar1.Maximum > 20 && this.trackBar1.Maximum <= 40 ? 2 : 5));
                        this.lblTotalDuration.Text = this.mediaReader.TotalTime.ToString().Substring(0, 8);

                        #region About timer_play
                        if (this.timer_play != null)
                        {
                            this.timer_play = null;
                        }
                        this.timer_play = new Timer();
                        this.timer_play.Interval = 200;
                        this.timer_play.Enabled = true;
                        this.timer_play.Tick += delegate(object se, EventArgs ev)
                        {
                            if (this.trackBar1.Value < this.trackBar1.Maximum)
                            {
                                this.trackBar1.Value = (int)this.mediaReader.CurrentTime.TotalSeconds;
                                this.lblCurrentDuration.Text = this.mediaReader.CurrentTime.ToString().Substring(0, 8);
                            }
                            else
                            {
                                this.btnStop.PerformClick();
                            }
                        };
                        #endregion About timer_play
                    }
                    else
                    {
                        this.ClearMediaPlayerObject();
                        MessageAlert.Show("ไฟล์มีเดียที่ระบุ ค้นหาไม่พบ", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        return;
                    }
                }
                this.btnPlay.Enabled = false;
                this.btnPause.Enabled = true;
                this.btnStop.Enabled = true;
                this.trackBar1.Enabled = true;

                this.waveOutDevice.Play();
                this.timer_play.Start();
            }
            catch (Exception ex)
            {
                this.ClearMediaPlayerObject();
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                throw ex;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this.waveOutDevice.Pause();
            this.btnPause.Enabled = false;
            this.btnPlay.Enabled = true;
            this.btnStop.Enabled = true;
            this.timer_play.Stop();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.ClearMediaPlayerObject();

            this.btnStop.Enabled = false;
            this.btnPlay.Enabled = true;
            this.btnPause.Enabled = false;
            this.trackBar1.Enabled = false;
            this.trackBar1.Value = 0;
        }

        private void ClearMediaPlayerObject()
        {
            if (this.waveOutDevice != null)
            {
                this.waveOutDevice.Stop();
            }
            if (this.mediaReader != null)
            {
                this.mediaReader.Dispose();
                this.mediaReader = null;
            }
            if (this.waveOutDevice != null)
            {
                this.waveOutDevice.Dispose();
                this.waveOutDevice = null;
            }
            if (this.timer_play != null)
            {
                this.timer_play.Dispose();
                this.timer_play = null;
            }
            this.trackBar1.Value = 0;
            this.lblCurrentDuration.Text = "00:00:00";
            this.lblTotalDuration.Text = "00:00:00";
        }
        #endregion Use Naudio Library

        protected override void OnClosing(CancelEventArgs e)
        {
            this.ClearMediaPlayerObject();
            this.axWindowsMediaPlayer1.Ctlcontrols.stop();
            this.axWindowsMediaPlayer1.Dispose();
            base.OnClosing(e);
        }
    }
}
