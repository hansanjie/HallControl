using PointApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PointApp.AssetTool
{
    public class WebPageTool:IAssetTool
    {
        private ExeHelper _exeHelper = new ExeHelper();

        private string _assetName;

        public void OpenAsset(string assetName)
        {
            this._assetName = assetName;
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(PathHelper.FileinAssetFolder(assetName));
                string link = xdoc.SelectSingleNode("Data/Link").InnerText;
                this._exeHelper.OpenWebPage(link, "CHROME.EXE");
                //this._exeHelper.OpenWebPage(link, "IEXPLORE.EXE");
                //this._exeHelper.OpenWebPage(link);
            }
            catch (Exception exception)
            {
                ErrorLogHelper.Log(string.Concat("获取网址失败：", exception.Message));
            }
        }

        public void CloseAsset()
        {
            //_exeHelper.CloseExe();
            CloseChromeProcess();
        }

        private void CloseChromeProcess()
        {
            var ps=System.Diagnostics.Process.GetProcesses();
            foreach (var p in ps)
            {
                if (p.ProcessName.ToLower().Contains("chrome"))
                {
                    p.CloseMainWindow();
                }
            }
        }

        public AssetType AssetType
        {
            get { return AssetType.webpage; }
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
