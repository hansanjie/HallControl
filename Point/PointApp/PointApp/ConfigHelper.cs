using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PointApp
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
        public int Port { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int SupportZone { get; set; }
        public int ZonePort { get; set; }
        public string ZoneIP { get; set; }
        #endregion

        public static void ResolveConfig(string path)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                _configHelper = new ConfigHelper();
                _configHelper.Port = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/Port").InnerText);
                _configHelper.Height = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/Height").InnerText);
                _configHelper.Width = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/Width").InnerText);
                _configHelper.SupportZone = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/SupportZone").InnerText);
                _configHelper.ZoneIP = xmlDocument.SelectSingleNode("Data/ZoneIP").InnerText;
                _configHelper.ZonePort = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/ZonePort").InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("can't read config:" + e.Message);
            }
        }
    }
}
