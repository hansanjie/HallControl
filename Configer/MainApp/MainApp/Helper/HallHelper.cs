using MainApp.Component;
using MainApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MainApp.Helper
{
    public class HallHelper
    {
        private HallInfo _hallInfo;
        public string LocalDataPath { get; set; }
        public HallHelper()
        {
            // TODO: Complete member initialization
        }

        public static HallInfo PublishedHallInfo
        {
            get
            {
                try
                {
                    string hallUrl = string.Format("http://{0}:{1}/hallinfo.xml", ConfigHelper.GetInstace().IP, ConfigHelper.GetInstace().WebPort);
                    WebClient wc = new WebClient();
                    wc.Encoding = Encoding.GetEncoding("utf-8");
                    string allDataXml = wc.DownloadString(hallUrl);
                    return XmlHelper.DeSerialize<HallInfo>(allDataXml);
                }
                catch (Exception e)
                {
                    ErrorLogHelper.Log("没有获得远程展厅信息：" + e.Message);
                    return null;
                }
            }
        }

        public HallInfo LocalCurrentHallInfo { get { return _hallInfo; } set { _hallInfo = value; } }

        internal void LoadHallInfo()
        {
            _hallInfo = PublishedHallInfo;
        }

        internal void UpdateHallInfo()
        {
            try
            {
                if (!ServerHelper.GetInstace().IsConnected)
                {
                    InfoLogHelper.AppendLog("未连接到服务器，请检查配置和网络");
                    return;
                }
                if (_hallInfo.zoneList == null)//no info
                {
                    ServerHelper.GetInstace().Send(string.Format("updateHall:{0}|", string.Empty));
                    InfoLogHelper.AppendLog("配置信息已更新");
                    return;
                }
                ItemHelper.UploadUpdatedItems(_hallInfo);
                string xmlString = XmlHelper.SerializeObj<HallInfo>(_hallInfo);
                this.UpdateChangePoints();
                SaveDatatoLocal(xmlString);
                ServerHelper.GetInstace().Send(string.Format("updateHall:{0}|", xmlString));
                InfoLogHelper.AppendLog("配置信息已更新");

            }
            catch (Exception e)
            {
                ErrorLogHelper.Log("update data err:" + e.Message);
            }
        }

        private void UploadPadFile(string pID)
        {
            string filePath = LocalDataPath + "\\" + pID + ".swf";
        }

        internal void UpdateChangePoints()
        {
            if (_hallInfo == null)
            {
                return;
            }
            var changePoints = from zone in _hallInfo.zoneList
                               where zone.PointList!=null
                               from point in zone.PointList
                               where point.IsChanged == true
                               select point;
            var cc = changePoints.ToList();
            foreach (var point in changePoints)
            {
                CreatePointDirectory(point);
                var i=changePoints.Count();
            }
        }

        private void CreatePointDirectory(Model.PointInfo point)
        {
            try
            {
                string pointDir = LocalDataPath + "\\" + point.Name + "\\asset";
                if (Directory.Exists(pointDir))
                {
                    Directory.Delete(pointDir, true);
                }
                Directory.CreateDirectory(pointDir);
                var supportedLans = point.SupportedLan.Split(',');
                foreach (var item in point.Items)
                {
                    //if (item.Type == ItemType.App.ToString())//app独立于语言文件夹
                    //{
                    //    string appDir = pointDir + "\\" + item.Name;
                    //    if (Directory.Exists(appDir))//Created
                    //    {
                    //        return;
                    //    }
                    //    Directory.CreateDirectory(appDir);
                    //    string startFileName = item.Arg.Split('*')[0];
                    //    if (!string.IsNullOrEmpty(startFileName))
                    //    {
                    //        AppHelper.SaveStartFileName(startFileName, appDir);
                    //    }
                    //}
                    //else
                    //{
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
                            else if (item.Type == ItemType.App.ToString())
                            {
                                string startFileName = item.Arg.Split('*')[0];
                                if (!string.IsNullOrEmpty(startFileName))
                                    AppHelper.SaveStartFileName(startFileName, itemLanDir);
                            }
                        }
                    //}
                }
            }
            catch (Exception e)
            {
                ErrorLogHelper.Log("创建结构目录出错：" + e.Message);
                InfoLogHelper.AppendLog("创建结构目录出错：" + e.Message);
            }
        }

        private void SaveDatatoLocal(string xmlString)
        {
            using (StreamWriter sw = new StreamWriter(LocalDataPath+"\\data.xml", false))//override hallinfo
            {
                sw.Write(xmlString);
            }
        }
    }
}
