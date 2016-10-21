using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MainApp.Helper
{
    public class ConfigHelper
    {
        private static ConfigHelper _configHelper;
        private ConfigHelper()
        {

        }

        public static ConfigHelper GetInstace()
        {
            if (_configHelper != null)
            {
                return _configHelper;
            }
            return null;
        }

        #region Data
        public string IP { get; set; }
        public int Port { get; set; }
        public string WebPort { get; set; }
        public int FtpPort { get; set; }
        #endregion

        public static void ResolveConfig(string path)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                _configHelper = new ConfigHelper();
                _configHelper.IP = xmlDocument.SelectSingleNode("Data/IP").InnerText;
                _configHelper.Port = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/Port").InnerText);
                _configHelper.WebPort = xmlDocument.SelectSingleNode("Data/WebPort").InnerText;
                _configHelper.FtpPort = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/FtpPort").InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("can't read config:" + e.Message);
            }
        }
    }
}
