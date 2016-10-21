using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointApp.AssetTool
{
    interface IAssetTool
    {
        void OpenAsset(string _startPath);

        AssetType AssetType { get; }

        void Reset();

        void DelAsset(string assetFuc, string assetName);

        void CloseAsset();

        string AssetName { get; }
    }
}
