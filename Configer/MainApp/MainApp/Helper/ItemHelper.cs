using MainApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApp.Helper
{
    class ItemHelper
    {
        internal static void UploadUpdatedItems(Model.HallInfo hallInfo)
        {
            Dictionary<string, Item> newDic = CreateAppDic(hallInfo);
            Dictionary<string, Item> publishedDic = CreateAppDic(HallHelper.PublishedHallInfo);
            foreach (var k in newDic.Keys)
            {
                if (!publishedDic.Keys.Contains(k) || newDic[k].Version != publishedDic[k].Version)//new or updated app
                {
                    string appID = k;
                    string pointName = FindPointName(appID, hallInfo);
                    string zoneName = FindZoneName(appID, hallInfo);
                    string dirName = string.Format("{0}_{1}_{2}", zoneName, pointName, appID);
                    var fileName = newDic[k].Arg.Split('*')[1];
                    var fileConfigName = newDic[k].Arg.Split('*')[2];
                    if (fileName != "" && fileConfigName != "")//upload file&config
                    {
                        AppHelper.UploadPadFileandConfig(dirName, fileName, fileConfigName);
                    }
                    else if (fileName != "")//upload file without config
                    {
                        AppHelper.UploadPadFile(dirName, fileName);
                    }
                }
            }
        }

        /// <summary>
        /// key:zoneName*pointName*appID,value:app
        /// </summary>
        /// <param name="hallInfo"></param>
        /// <returns></returns>
        private static Dictionary<string, Item> CreateAppDic(Model.HallInfo hallInfo)
        {
            Dictionary<string, Item> dic = new Dictionary<string, Item>();
            if (hallInfo == null)
            {
                return dic;
            }
            var appList = from zs in hallInfo.zoneList
                          from ps in zs.PointList
                          from items in ps.Items
                          where items.Type == MainApp.Model.ItemType.App.ToString()
                          select items;
            try
            {
                foreach (var app in appList)
                {
                    dic.Add(app.ID, app);
                }
            }
            catch//null
            {

            }
            return dic;
        }

        public static string FindPointName(string appID, HallInfo hallInfo)
        {
            var itemtoMatch= from zs in hallInfo.zoneList
                            from ps in zs.PointList
                            from items in ps.Items
                            where items.ID==appID
                            select items;

            var item=itemtoMatch.First();

            var pointtoMatch = from zs in hallInfo.zoneList
                           from ps in zs.PointList
                           where ps.Items.Contains(item)
                           select ps;

            var point = pointtoMatch.First();

            return point.Name;
        }

        public static string FindZoneName(string appID, HallInfo hallInfo)
        {
            string pointName = FindPointName(appID, hallInfo);
            var pointtoMatch = from zs in hallInfo.zoneList
                               from ps in zs.PointList
                               where ps.Name == pointName
                               select ps;
            var point = pointtoMatch.First();
            var zonetoMatch = from zs in hallInfo.zoneList
                              where zs.PointList.Contains(point)
                              select zs;
            var zoneName = zonetoMatch.First().Name;
            return zoneName;
        }
    }
}
