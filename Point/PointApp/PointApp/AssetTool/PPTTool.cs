using PointApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointApp.AssetTool
{
    public class PPTTool:IAssetTool
    {
        private static PPTTool _pptTool;
        private PPTDeal _pptDeal;

        private string _assetName;

        private PPTTool()
        {

        }

        public static PPTTool GetInstance()
        {
            if (_pptTool == null)
            {
                _pptTool = new PPTTool();
            }
            return _pptTool;
        }

        public void OpenAsset(string assetName)
        {
            this._assetName = assetName;
            string filePath = PathHelper.FileinAssetFolder(assetName);
            if (_pptDeal == null)
            {
                this._pptDeal = new PPTDeal();
            }
            this._pptDeal.PPTOpen(filePath);
        }

        public void CloseAsset()
        {
            _pptDeal.PPTClose();
        }

        public AssetType AssetType
        {
            get { return PointApp.AssetType.ppt; }
        }

        public void DelAsset(string fun, string arg)
        {
            switch (arg)
            {
                case "nextPage":
                    _pptDeal.NextPage();
                    break;
                case "prePage":
                    _pptDeal.PrePage();
                    break;
                case "homePage":
                    _pptDeal.SetPPTPage(1);
                    break;
                case "endPage":
                    _pptDeal.SetPPTPage(_pptDeal.PageCount);
                    break;
                case "close":
                    CloseAsset();
                    break;
            }
        }

        public string AssetName
        {
            get { return _assetName; }
        }


        public void Reset()
        {
            _pptDeal.SetPPTPage(1);
        }
    }
}
