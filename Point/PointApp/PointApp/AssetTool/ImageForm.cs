using PointApp.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PointApp.AssetTool
{
    public partial class ImageForm : Form,IAssetTool
    {

        private string _assetName;


        private int _currentIndex;
        private string[] _imageFiles;
        public ImageForm()
        {
            InitializeComponent();
        }

        public void OpenAsset(string assetName)
        {
            this._assetName = assetName;
            this._imageFiles = Directory.GetFiles(PathHelper.ItemFolder(assetName));
            this.ShowImage(1);
        }

        private void ShowImage(int index)
        {
            string imageFile = SelectFile(index);
            if (string.IsNullOrEmpty(imageFile))
            {
                return;
            }
            this.pictureBox1.ImageLocation = imageFile;
            _currentIndex = index;
        }

        private string SelectFile(int index)
        {
            var fileName = from f in _imageFiles
                              where new FileInfo(f).Name.Split('.')[0] == index.ToString()
                              select f;
            if (fileName.Count() != 0)
            {
                return fileName.First();
            }
            else
            {
                return null;
            }
        }

        public void CloseAsset()
        {
            this.Close();
        }

        public AssetType AssetType
        {
            get { return AssetType.image; }
        }

        private void ImageForm_Load(object sender, EventArgs e)
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

        public void DelAsset(string fun, string arg)
        {
            switch (arg)
            {
                case "close":
                    CloseAsset();
                    break;
                case "nextPage":
                    NextImage();
                    break;
                case "prePage":
                    PreImage();
                    break;
                case "homePage":
                    FirstImage();
                    break;
            }
        }

        private void FirstImage()
        {
            ShowImage(1);
        }

        private void PreImage()
        {
            ShowImage(_currentIndex - 1);
        }

        private void NextImage()
        {
            ShowImage(_currentIndex + 1);
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
