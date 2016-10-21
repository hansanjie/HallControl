using ServerDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZoneServer
{
    public partial class Form1 : Form
    {
        private AsyncServer asyncServer = new AsyncServer();
        private List<int> m_ClientIndexs = new List<int>();

        private delegate void DelegateMsg(int index, string msg);
        private delegate void DelString(string arg);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigHelper.ResolveConfig(Application.StartupPath+"\\config.xml");
            InitServer();
        }

        #region server
        private void InitServer()
        {
            if (asyncServer.Listen(ConfigHelper.GetInstace().Port, 2000))
            {
                asyncServer.onConnected += new AsyncServer.SocketConnected(asyncServer_onConnected);
                asyncServer.onDisconnected += new AsyncServer.SocketDisconnected(asyncServer_onDisconnected);
                asyncServer.onDataByteIn += new AsyncServer.SocketDataByteIn(asyncServer_onDataByteIn);
            }
            else
            {
                MessageBox.Show("该端口被占用，请重新配置!");
                Process.GetCurrentProcess().Kill();
            }
        }

        //客户端连线
        void asyncServer_onConnected(int index, string ip)
        {
            string message = string.Format("{0}连线!", ip);
            this.Invoke(new DelString(SetText), new object[] { message });
            System.Threading.Thread.Sleep(100);
            if (!m_ClientIndexs.Contains(index))
            {
                m_ClientIndexs.Add(index);
            }
        }

        //客户端断线
        void asyncServer_onDisconnected(int index, string ip)
        {
            string message = string.Format("{0}断线!", ip);
            this.Invoke(new DelString(SetText), new object[] { message });
            if (m_ClientIndexs.Contains(index))
            {
                m_ClientIndexs.Remove(index);
            }
        }

        //消息进入
        void asyncServer_onDataByteIn(int index, string ip, byte[] SocketData)
        {
            try
            {
                string message = Encoding.UTF8.GetString(SocketData).Replace("\n", "");
                this.Invoke(new DelString(SetText), new object[] { message });
                string[] dataList = message.Split('|');//协议以|符号分割
                foreach (string data in dataList)
                {
                    if (!String.IsNullOrEmpty(data))
                    {
                        this.Invoke(new DelegateMsg(DealMsg), new object[] { index, data });
                    }
                }
            }
            catch (Exception e)
            {
                this.Invoke(new DelString(SetText), new object[] { e.Message });
            }
        }

        private void DealMsg(int index, string msg)
        {
            var msgPart = msg.Split(':');
            var fun = msgPart[0];
            var arg = msg.Substring(msg.IndexOf(":") + 1);
            switch (fun)
            {
                case "lan":
                    if (arg.Contains("setZone"))//change zone lan
                    {
                        arg = arg.Replace("setZone", "setPoint");
                        ForwardMsg(index,string.Format("lan:{0}|", arg));
                    }
                    break;
                default:
                    break;
            }
        }

        private void ForwardMsg(int sourceIndex, string msg)
        {
            foreach (var index in m_ClientIndexs)
            {
                if (index != sourceIndex)
                {
                    asyncServer.Send(index, msg );
                }
            }
        }
        #endregion

        private void SetText(string data)
        {
            rtb_data.AppendText(data + "\n");
            if (rtb_data.Lines.Count() >= 1000)
            {
                rtb_data.Clear();
            }
        }
    }
}
