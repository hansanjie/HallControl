using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PointApp.Helper
{
    public class ErrorLogHelper
    {
        internal static void Log(string log)
        {
            using (StreamWriter sw = new StreamWriter(PathHelper.StartPath + "\\log.txt", true))//override hallinfo
            {
                string fullLog=string.Format("{0}--------{1}\r\n",DateTime.Now.ToLongDateString(),log);
                sw.Write(fullLog);
            }
        }
    }
}
