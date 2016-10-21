using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CentralServer
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
        #endregion

        public static void ResolveConfig(string path)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                _configHelper = new ConfigHelper();
                _configHelper.Port = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/Port").InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("can't read config:" + e.Message);
            }
        }
    }
}
