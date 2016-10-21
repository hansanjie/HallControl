using PointApp.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PointApp.AssetTool
{
    public partial class FlashForm : Form,IAssetTool
    {
        private string _assetName;
        public FlashForm()
        {
            InitializeComponent();
        }

        private void FlashForm_Load(object sender, EventArgs e)
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
        }

        public void OpenAsset(string assetName)
        {
            this._assetName = assetName;
            string path = PathHelper.FileinAssetFolder(assetName);
            this.axShockwaveFlash1.Movie = path;
        }

        public void CloseAsset()
        {
            this.Close();
        }

        public AssetType AssetType
        {
            get { return PointApp.AssetType.flash; }
        }

        public void DelAsset(string fun, string arg)
        {
            if (fun == "control")
            {
                if (arg != null)
                {
                    if (arg == "play")
                    {
                        this.PlayFlash();
                    }
                    else if (arg == "puase")
                    {
                        this.PauseFlash();
                    }
                    else if (arg == "retreat")
                    {
                        this.RetreatFlash();
                    }
                    else if (arg == "forward")
                    {
                        this.ForwardFlash();
                    }
                }
            }
            else if (fun == "sound")
            {
                if (arg != null)
                {
                    if (arg == "up")
                    {
                        SoundHelper.TurnUp(base.Handle);
                    }
                    else if (arg == "down")
                    {
                        SoundHelper.TurnDown(base.Handle);
                    }
                    else if (arg == "mute")
                    {
                        SoundHelper.IsMute(base.Handle);
                    }
                    else if (arg.ToLower() == "unmute")
                    {
                        SoundHelper.IsMute(base.Handle);
                    }
                }
            }
        }

        private void RetreatFlash()
        {
            var offset = this.axShockwaveFlash1.TotalFrames / 5;
            var tFrame = this.axShockwaveFlash1.CurrentFrame() - offset;
            if (tFrame <= 1)
            {
                this.axShockwaveFlash1.GotoFrame(1);
            }
            else
            {
                this.axShockwaveFlash1.GotoFrame(tFrame);
            }
            this.PlayFlash();
        }

        private void ForwardFlash()
        {
            var offset = this.axShockwaveFlash1.TotalFrames / 5;
            var tFrame = this.axShockwaveFlash1.CurrentFrame() + offset;
            if (tFrame >= this.axShockwaveFlash1.TotalFrames)
            {
                this.axShockwaveFlash1.GotoFrame(this.axShockwaveFlash1.TotalFrames);
            }
            else
            {
                this.axShockwaveFlash1.GotoFrame(tFrame);
            }
            this.PlayFlash();
        }

        private void PauseFlash()
        {
            this.axShockwaveFlash1.Stop();
        }

        private void PlayFlash()
        {
            this.axShockwaveFlash1.Play();
        }



        public string AssetName
        {
            get { return _assetName; }
        }


        public void Reset()
        {
            //throw new NotImplementedException();
        }


    }
}
