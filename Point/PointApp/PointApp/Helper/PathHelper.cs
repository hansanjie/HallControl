using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PointApp.Helper
{
    public class PathHelper
    {
        public static string StartPath { get; set; }
        public static string AssetMainFolder { get { return StartPath + "\\asset"; } }

        public static string ItemFolder(string assetName)
        {
            //return AssetMainFolder + "\\" + LanHelper.CurrentLan + "\\" + folderName;
            return AssetMainFolder + "\\" + assetName + "\\" + LanHelper.CurrentLan;
        }

        internal static string AssetNonLanFolder(string assetName)
        {
            return AssetMainFolder +  "\\" + assetName;
        }

        internal static string FileinAssetFolder(string assetName)
        {
            //string fullFolder = AssetMainFolder + "\\" + LanHelper.CurrentLan + "\\" + assetName;
            //string fullFolder = AssetMainFolder + "\\" + assetName + "\\" + LanHelper.CurrentLan;
            var files = Directory.GetFiles(ItemFolder(assetName));
            if (files.Count() == 0)
            {
                return "";
            }
            else
            {
                return new FileInfo(files[0]).FullName;
            }
        }
    }
}
