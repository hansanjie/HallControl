using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MainApp.Helper
{
    public class ErrorLogHelper
    {
        public static string LocalPath { get; set; }
        internal static void Log(string log)
        {
            using (StreamWriter sw = new StreamWriter(LocalPath + "\\log.txt", true))//override hallinfo
            {
                string fullLog=string.Format("{0}--------{1}\r\n",DateTime.Now.ToString(),log);
                sw.Write(fullLog);
            }
        }
    }
}
