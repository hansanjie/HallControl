using ClientDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MainApp.Helper
{
    public class ServerHelper
    {
        private static ServerHelper _serverHelper;
        private AsyncClient asyncClient;
        private System.Timers.Timer asyncTimer = new System.Timers.Timer();
        private string ip; int port;
        private ServerHelper()
        {

        }

        public bool IsConnected { get; set; }

        internal static ServerHelper GetInstace()
        {
            if (_serverHelper == null)
            {
                _serverHelper = new ServerHelper();
                _serverHelper.InitNet();
            }
            return _serverHelper;
        }

        #region socket
        private void InitNet()
        {
            try
            {
                InitAsyncTimer();
                ip = ConfigHelper.GetInstace().IP;
                port = ConfigHelper.GetInstace().Port;
                if (this.asyncClient != null)
                {
                    this.asyncClient.Dispose();
                    this.asyncClient.onConnected -= new AsyncClient.Connected(client_onConnected);
                    this.asyncClient.onDisConnect -= new AsyncClient.DisConnect(client_onDisConnect);
                    this.asyncClient.onDataByteIn -= new AsyncClient.DataByteIn(client_onDataByteIn);
                }
                asyncClient = new AsyncClient();
                asyncClient.onConnected += new AsyncClient.Connected(client_onConnected);
                asyncClient.Connect(ip, port);
                asyncClient.onDataByteIn += new AsyncClient.DataByteIn(client_onDataByteIn);
                asyncClient.onDisConnect += new AsyncClient.DisConnect(client_onDisConnect);
            }
            catch (Exception ex)
            {
                Console.WriteLine("cannot connect to server:" + ex.Message);
            }
        }

        private void InitAsyncTimer()
        {
            asyncTimer = new System.Timers.Timer();
            asyncTimer.Interval = 1500;
            asyncTimer.Elapsed += new System.Timers.ElapsedEventHandler(asyncTimer_Elapsed);
        }

        /// <summary>
        /// 服务器信息
        /// </summary>
        /// <param name="SocketData"></param>
        void client_onDataByteIn(byte[] SocketData)
        {
            //string cmd = System.Text.Encoding.UTF8.GetString(SocketData);
            //this.Invoke(new DelSetText(SetText), new object[] { cmd });
            //string[] dataList = cmd.Split(m_endChar, StringSplitOptions.RemoveEmptyEntries);
            //foreach (string data in dataList)
            //{
            //    this.Invoke(new DelDataDeal(DataDeal), new object[] { data });
            //}
        }

        void client_onConnected()
        {
            try
            {
                Thread.Sleep(100);
                asyncTimer.Stop();
                string message = string.Format("{0}连线!", ip);
                IsConnected = true;
                //this.Invoke(new DelSetText(SetText), new object[] { message });
            }
            catch (Exception ex)
            {
                Console.WriteLine("cannot connect to server:" + ex.Message);
            }
        }

        void client_onDisConnect()
        {
            string message = string.Format("{0}断线!", ip);
            IsConnected = false;
            //this.Invoke(new DelSetText(SetText), new object[] { message });
            asyncTimer.Start();
        }

        private void asyncTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ReConnect();
        }

        private void ReConnect()
        {
            asyncClient.Dispose();
            asyncClient.Connect(ip, port);
        }
        #endregion

        internal void Send(string msg)
        {
            asyncClient.Send(msg);
        }

        internal void Send(byte[] bytes)
        {
            asyncClient.Send(bytes);
        }
    }
}
