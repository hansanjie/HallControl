using MainApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MainApp.Helper
{
    public class PointHelper
    {
        private Model.HallInfo _hallInfo;

        public Model.HallInfo HallInfo { set { _hallInfo = value; } }

        internal void UpdateChangePoints()
        {
            if (_hallInfo == null)
            {
                return;
            }
            var changePoints = from zone in _hallInfo.zoneList
                               from point in zone.PointList
                               where point.IsChanged == true
                               select point;
            foreach (var point in changePoints)
            {
                CreatePointDirectory(point);
            }
        }

        private void CreatePointDirectory(Model.PointInfo point)
        {
            try
            {
                string pointDir = LocalDataPath + "\\" + point.Name+"\\asset";
                if (Directory.Exists(pointDir))
                {
                    Directory.Delete(pointDir,true);
                }
                Directory.CreateDirectory(pointDir);
                var supportedLans = point.SupportedLan.Split(',');
                foreach (var item in point.Items)
                {
                    if (item.Type == ItemType.App.ToString())//app独立于语言文件夹
                    {
                        string appDir = pointDir + "\\" + item.Name;
                        if (Directory.Exists(appDir))//Created
                        {
                            return;
                        }
                        Directory.CreateDirectory(appDir);
                        string startFileName = item.Arg.Split('*')[0];
                        if (!string.IsNullOrEmpty(startFileName))
                        {
                            AppHelper.SaveStartFileName(startFileName, appDir);
                        }
                    }
                    else
                    {
                        string itemDir = pointDir + "\\" + item.Name;
                        Directory.CreateDirectory(itemDir);
                        foreach (var lan in supportedLans)
                        {
                            string itemLanDir = itemDir + "\\" + lan;
                            Directory.CreateDirectory(itemLanDir);
                            if (item.Type == ItemType.WebPage.ToString())//save web link
                            {
                                WebItemHelper.SaveLink(item.Arg, itemLanDir);
                            }
                        }
                    }
                }
                #region 旧目录结构
                //foreach (var lan in supportedLans)
                //{
                //    foreach (var item in point.Items)
                //    {
                //        if (item.Type == ItemType.App.ToString())//app独立于语言文件夹
                //        {
                //            string appDir = pointDir + "\\" + item.Name;
                //            if (Directory.Exists(appDir))//Created
                //            {
                //                return;
                //            }
                //            Directory.CreateDirectory(appDir);
                //            string startFileName = item.Arg.Split('*')[0];
                //            if (!string.IsNullOrEmpty(startFileName))
                //            {
                //                AppHelper.SaveStartFileName(startFileName, appDir);
                //            }
                //        }

                //        //non app
                //        string itemDir = pointDir + "\\" +lan+"\\"+ item.Name;
                //        Directory.CreateDirectory(itemDir);
                //        if (item.Type == ItemType.WebPage.ToString())//save web link
                //        {
                //            WebItemHelper.SaveLink(item.Arg, itemDir);
                //        }
                    //}
                //}
                #endregion



            }
            catch (Exception e)
            {
                ErrorLogHelper.Log("创建结构目录出错：" + e.Message);
                InfoLogHelper.AppendLog("创建结构目录出错：" + e.Message);
            }
        }

        public string LocalDataPath { get; set; }
    }
}
