using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MainApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool bCreatedNew;
            //Create a new mutex using specific mutex name
            Mutex m = new Mutex(false, "CentralControlConfig", out bCreatedNew);
            if (bCreatedNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("存在已启动的配置软件,请使用");
                Application.Exit();
            }
        }
    }
}
