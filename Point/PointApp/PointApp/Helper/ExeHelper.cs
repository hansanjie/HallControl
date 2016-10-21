using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace PointApp.Helper
{
    public class ExeHelper
    {
        Process m_process;
        private string exePath;
        private static ExeHelper instance;

        public static ExeHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new ExeHelper();
            }

            return instance;
        }

        public ExeHelper()
        {
            
        }

        public string Path
        {
            set 
            {
                exePath = value;
                InitProcess();
            }
        }

        private void InitProcess()
        {
            CloseExe();
            m_process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = exePath;
            //processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            processStartInfo.WorkingDirectory = exePath.Substring(0, exePath.LastIndexOf('\\'));
            m_process.StartInfo = processStartInfo;
        }

        internal void OpenExe()
        {
            try
            {
                CloseExe();
                m_process.Start();
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                MessageBox.Show("无法打开程序，原因：" + e.Message);
            }
        }

        internal void OpenWebPage(string url,string navigate)
        {
            try
            {
                CloseExe();
                m_process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = navigate;
                processStartInfo.Arguments = url;
                processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                m_process.StartInfo = processStartInfo;
                m_process.Start();
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                MessageBox.Show("无法打开程序，原因：" + e.Message);
            }
        }

        internal void OpenWebPage(string url)
        {
            try
            {
                //CloseExe();
                //m_process = new Process();
                //ProcessStartInfo processStartInfo = new ProcessStartInfo();
                //processStartInfo.FileName = url;
                ////processStartInfo.Arguments = url;
                //processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                //m_process.StartInfo = processStartInfo;
                //m_process.Start();
                System.Diagnostics.Process.Start(url); 
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                MessageBox.Show("无法打开程序，原因：" + e.Message);
            }
        }

        internal void CloseExe()
        {
            try
            {
                if (m_process != null && !m_process.HasExited)
                {
                    m_process.Kill();
                }
            }
            catch
            {

            }
        }

        private void SendtoExe(string msg)
        {
            //WindowHandleHelper.SendMessage(msg, "VideoForm");
        }
    }
}
