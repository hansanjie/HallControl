using PointApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PointApp.AssetTool
{
    class AppTool : IAssetTool
    {

        private string _assetName;
        private ExeHelper _exeHelper = new ExeHelper();
        public void OpenAsset(string assetName)
        {
            try
            {
                this._assetName = assetName;
                XmlDocument xdoc = new XmlDocument();
                string startFilenamePath = string.Concat(PathHelper.ItemFolder(assetName), "\\startFileName.xml");
                //string startFilenamePath = string.Concat(PathHelper.AssetNonLanFolder(assetName), "\\startFileName.xml");
                xdoc.Load(startFilenamePath);
                string fileName = xdoc.SelectSingleNode("Data/StartFileName").InnerText;
                string startPath = string.Concat(PathHelper.ItemFolder(assetName), "\\", fileName);
                //string startPath = string.Concat(PathHelper.AssetNonLanFolder(assetName), "\\", fileName);
                this._exeHelper.Path = startPath;
                this._exeHelper.OpenExe();
            }
            catch (Exception exception)
            {
                ErrorLogHelper.Log(string.Concat("打开应用程序失败：", exception.Message));
            }
        }

        public void CloseAsset()
        {
            _exeHelper.CloseExe();
        }

        public AssetType AssetType
        {
            get { return PointApp.AssetType.app; }
        }


        public void DelAsset(string fun, string arg)
        {
            return;
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
