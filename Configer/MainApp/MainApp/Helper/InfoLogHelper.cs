using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApp.Helper
{
    public interface Ilog { void AppendLog(string log); };
    public class InfoLogHelper
    {
        private static Ilog ilog;
        public static void AppendLog(string log)
        {
            if(log!=null)
            {
                ilog.AppendLog(log);
            }
        }

        public static void RegisterLog(Ilog log)
        {
            if (log != null)
            {
                ilog = log;
            }
        }
    }
}
