using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MainApp.Helper
{
    class AppHelper
    {
        internal static void SaveStartFileName(string startFileName, string itemDir)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(string.Format("<Data><StartFileName>{0}</StartFileName></Data>", startFileName));
            string fullPath = itemDir + "\\startFileName.xml";
            xDoc.Save(fullPath);
        }

        internal static void UploadPadFileandConfig(string dirName, string fullFileName, string fullFileConfigName)
        {
            FTPHelpr fh = new FTPHelpr(ConfigHelper.GetInstace().IP, ConfigHelper.GetInstace().FtpPort);
            fh.CreateDir(dirName);
            fh.UpLoadFile(fullFileName, "padcontrol",dirName);
            fh.UpLoadFile(fullFileConfigName,"config",dirName);
        }

        internal static void UploadPadFile(string dirName, string fullFileName)
        {
            FTPHelpr fh = new FTPHelpr(ConfigHelper.GetInstace().IP, ConfigHelper.GetInstace().FtpPort);
            fh.CreateDir(dirName);
            fh.UpLoadFile(fullFileName, "padcontrol", dirName);
        }

        internal static string AppSelectPath { get; set; }
    }
}
