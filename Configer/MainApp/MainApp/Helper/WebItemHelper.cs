using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MainApp.Helper
{
    class WebItemHelper
    {
        internal static void SaveLink(string link, string itemDir)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(string.Format("<Data><Link>{0}</Link></Data>", link));
            string fullPath = itemDir + "\\link.xml";
            xDoc.Save(fullPath);
        }
    }
}
