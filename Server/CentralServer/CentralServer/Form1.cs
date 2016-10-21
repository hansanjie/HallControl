using ServerDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CentralServer
{
    public partial class Form1 : Form
    {
        private AsyncServer asyncServer = new AsyncServer();
        private List<int> m_ClientIndexs = new List<int>();
        private delegate void DelegateMsg(int index, string msg);
        private delegate void DelString(string arg);
        private delegate void DelVoid();
        public Form1()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigHelper.ResolveConfig(Application.StartupPath + "\\config.xml");
            InitNet();
            this.WindowState = FormWindowState.Minimized;
        }

        private void InitNet()
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

            }
        }

        private void ForwardMsg(int sourceIndex, string msg)
        {
            foreach (var index in m_ClientIndexs)
            {
                if (index != sourceIndex)
                {
                    asyncServer.Send(index, msg + "|");
                }
            }
        }

        private void DealMsg(int index, string msg)
        {
            var msgPart = msg.Split(':');
            var fun = msgPart[0];
            //var arg = msgPart[msgPart.Length - 1];
            var arg = msg.Substring(msg.IndexOf(":") + 1);
            switch (fun)
            {
                case "updateHall":
                    var hallInfoString = arg;
                    this.Invoke(new DelString(SaveHallInfo), hallInfoString);
                    break;
                case "changePoints":
                    string pointsString = arg;
                    this.Invoke(new DelString(SaveChangeLog), pointsString);
                    break;
                case "removeLog":
                        RemoveChangeLog(arg);
                    break;

            }
        }

        private void RemoveChangeLog(string pointID)
        {
            string existedLog = "";
            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\data\\changeLog.xml", Encoding.GetEncoding("utf-8")))
                {
                    existedLog = sr.ReadToEnd().Trim();
                }
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\data\\changeLog.xml", false))//override hallinfo
                {
                    if (existedLog == null || existedLog == string.Empty)
                    {
                        return;
                    }
                    else//exist change ids
                    {
                        var existList = existedLog.Split(',').ToList();
                        if (existList.Contains(pointID))
                        {
                            existList.Remove(pointID);
                        }
                        string updatedLog = string.Join(",", existList.ToArray());
                        sw.Write(updatedLog);
                    }
                }
            }
            catch (Exception e)
            {
                SetText("chnage changeLog err:" + e.Message);
            } 
        }

        private void SaveChangeLog(string changeLog)
        {
            string existedLog = "";
            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\data\\changeLog.xml", Encoding.GetEncoding("utf-8")))
                {
                    existedLog = sr.ReadToEnd().Trim();
                }
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\data\\changeLog.xml", false))//override hallinfo
                {
                    if (existedLog == null || existedLog == string.Empty)
                    {
                        sw.Write(changeLog);
                    }
                    else//exist change ids
                    {
                        var existList = existedLog.Split(',').ToList();
                        var changeList = changeLog.Split(',').ToList();
                        foreach (var changeItem in changeList)
                        {
                            if (!existList.Contains(changeItem))
                            {
                                existList.Add(changeItem);
                            }
                        }
                        string updatedLog = string.Join(",", existList.ToArray());
                        sw.Write(updatedLog);
                    }
                }
            }
            catch (Exception e)
            {
                SetText("chnage changeLog err:" + e.Message);
            } 
        }

        private void SaveHallInfo(string text)
        {
            string texttoSave = text;
            using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\data\\hallinfo.xml",false))//override hallinfo
            {
                sw.Write(texttoSave);
            }
            //using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\data\\alldata.xml", false))//save to alldata
            //{
            //    sw.Write(texttoSave);
            //}
        }

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
