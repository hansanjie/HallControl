using PointApp.Helper;
using ServerDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace PointApp.AssetTool
{
    public partial class VideoForm : Form,IAssetTool
    {
        private string _assetName;
        private System.Timers.Timer _updateLengthTimer;
        public VideoForm()
        {
            InitializeComponent();
        }

        public void OpenAsset(string assetName)
        {
            this._assetName = assetName;
            string videoPath = PathHelper.FileinAssetFolder(assetName);
            if (!string.IsNullOrEmpty(videoPath))
            {
                this.mediaPlayer.URL = videoPath;
                this._updateLengthTimer.Start();
                this.mediaPlayer.Ctlcontrols.currentPosition = 0;
                PlayVideo();
            }
        }

        public void CloseAsset()
        {
            this.Close();
        }

        public void DelAsset(string fun, string arg)
        {
            string str = fun;
            if (str != null)
            {
                if (str == "control")
                {
                    str = arg;
                    if (str != null)
                    {
                        if (str == "play")
                        {
                            this.PlayVideo();
                        }
                        else if (str == "puase")
                        {
                            this.PauseVideo();
                        }
                        else if (str == "retreat")
                        {
                            this.ReatreatVideo();
                        }
                        else if (str == "forward")
                        {
                            this.ForwardVideo();
                        }
                        else if (str == "close")
                        {
                            this.CloseVideo();
                        }
                    }
                }
                else if (str == "time")
                {
                    this.SetTime(arg);
                }
                else if (str == "sound")
                {
                    str = arg;
                    if (str != null)
                    {
                        if (str == "up")
                        {
                            SoundHelper.TurnUp(base.Handle);
                        }
                        else if (str == "down")
                        {
                            SoundHelper.TurnDown(base.Handle);
                        }
                        else if (str == "mute")
                        {
                            SoundHelper.IsMute(base.Handle);
                        }
                        else if (str == "unmute")
                        {
                            SoundHelper.IsMute(base.Handle);
                        }
                    }
                }
            }
        }

        private void SetTime(string timeString)
        {
            try
            {
                this.mediaPlayer.Ctlcontrols.play();
                this.mediaPlayer.Ctlcontrols.currentPosition = double.Parse(timeString);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("Fail to set Volume:", exception.Message));
            }
        }

        private void CloseVideo()
        {
            CloseAsset();
        }

        private void ForwardVideo()
        {
            this.mediaPlayer.Ctlcontrols.play();
            var offset = this.mediaPlayer.currentMedia.duration / 10;
            if (this.mediaPlayer.Ctlcontrols.currentPosition + offset >= this.mediaPlayer.currentMedia.duration)
            {
                this.mediaPlayer.Ctlcontrols.currentPosition = this.mediaPlayer.currentMedia.duration - 2;
            }
            else
            {
                this.mediaPlayer.Ctlcontrols.currentPosition += offset;
            }
        }

        private void ReatreatVideo()
        {
            this.mediaPlayer.Ctlcontrols.play();
            var offset = this.mediaPlayer.currentMedia.duration / 10;
            if (this.mediaPlayer.Ctlcontrols.currentPosition - offset <= 0)
            {
                this.mediaPlayer.Ctlcontrols.currentPosition = 0;
            }
            else
            {
                this.mediaPlayer.Ctlcontrols.currentPosition -= offset;
            }
        }

        private void PauseVideo()
        {
            this.mediaPlayer.Ctlcontrols.pause();
        }

        private void PlayVideo()
        {
            this.mediaPlayer.Ctlcontrols.play();
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Left = 0;
            this.Top = 0;
            if (ConfigHelper.GetInstace().Width == 0 || ConfigHelper.GetInstace().Height == 0)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.Width = ConfigHelper.GetInstace().Width;
                this.Height = ConfigHelper.GetInstace().Height;
            }
            this.mediaPlayer.settings.setMode("loop", true);
            this.mediaPlayer.uiMode = "none";
            this.mediaPlayer.stretchToFit = true;
            this._updateLengthTimer = new System.Timers.Timer(1000);
            this._updateLengthTimer.Elapsed += new ElapsedEventHandler(this._updateLengthTimer_Elapsed);
        }

        void _updateLengthTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            double videoLength = this.mediaPlayer.currentMedia.duration;
            AsyncServer.GetInstance().SendToAll(string.Format("video:totalTime&{0}|", videoLength.ToString()));
            this._updateLengthTimer.Stop();
        }


        public AssetType AssetType
        {
            get { return AssetType.video; }
        }


        public void Reset()
        {
            this.mediaPlayer.Ctlcontrols.currentPosition = 0;
            double videoLength = this.mediaPlayer.currentMedia.duration;
            AsyncServer.GetInstance().SendToAll(string.Format("video:totalTime&{0}|", videoLength.ToString()));
            PlayVideo();
        }

        public string AssetName
        {
            get { return _assetName; }
        }
    }
}
