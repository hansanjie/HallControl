using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApp.Helper
{
    public class IDCreater
    {
        //internal static string CreateID()
        //{
        //    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //    return Convert.ToInt64(ts.TotalSeconds).ToString(); 
        //}

        internal static string CreateID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
    }
}
