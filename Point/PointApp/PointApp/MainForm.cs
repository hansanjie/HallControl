using ClientDLL;
using PointApp.AssetTool;
using PointApp.Helper;
using ServerDLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace PointApp
{
    public enum AssetType { video, ppt, webpage, app, image, flash };
    public partial class MainForm : Form
    {
        private AsyncServer asyncServer;
        private List<int> m_ClientIndexs = new List<int>();

        private AsyncClient asyncClient = new AsyncClient();
        private System.Timers.Timer asyncTimer = new System.Timers.Timer();

        private delegate void DelegateMsg(int index, string msg);
        private delegate void DelString(string arg);
        private delegate void Del2StringArgs(string arg1,string arg2);
        private delegate void DelVoid();

        private IAssetTool _activeAssettool;
        private string _activeName = "";
        private string _startType = "";
        private string _startPath = "";
        private string _startLan = "";

        private TaskBarHelper _tbHelper = new TaskBarHelper();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.cbStartType.SelectedIndex = 0;
            PathHelper.StartPath = Application.StartupPath;
            ConfigHelper.ResolveConfig(Application.StartupPath + "\\config.xml");
            this.WindowState = FormWindowState.Minimized;
            LanHelper.CurrentLan = "en";
            Thread.Sleep(3000);
            InitStartInfo();
            InitServer();
            if (ConfigHelper.GetInstace().SupportZone == 1)
            {
                InitClient();
            }
            HideTaskBar();
            HideMouse();
        }

        private void InitStartInfo()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Application.StartupPath + "\\startinfo.xml");
            _startType = xdoc.SelectSingleNode("Data/StartType").InnerText;
            _startPath = xdoc.SelectSingleNode("Data/StartPath").InnerText;
            _startLan = xdoc.SelectSingleNode("Data/StartLan").InnerText;
            LanHelper.CurrentLan = _startLan;
            if (_startType != "" && _startPath != "" && _startLan != "")//start file comfirmed
            {
                //this.Left = this.Top = -1000;
                try
                {
                    StartDefault();
                }
                catch(Exception e)
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void StartDefault()
        {
            IAssetTool assetTool = CreateAssetTool((AssetType)Enum.Parse(typeof(AssetType), _startType));
            assetTool.OpenAsset(_startPath);
            _activeAssettool = assetTool;
            _activeName = _startPath;
            this.rtb_data.Visible = true;
        }

        private string StartFileName()
        {
            if (_startType == AssetType.image.ToString())//multy file
            {
                return "";
            }
            else
            {
                var files = Directory.GetFiles(_startPath);
                if (files.Count() == 0)
                {
                    return "";
                }
                else
                {
                    return new FileInfo(files[0]).FullName;
                }
            }
        }

        #region server
        private void InitServer()
        {
            asyncServer = AsyncServer.GetInstance();
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
            System.Threading.Thread.Sleep(10);
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
        #endregion

        #region client
        private void InitClient()
        {
            try
            {
                InitAsyncTimer();
                string zoneIP = ConfigHelper.GetInstace().ZoneIP;
                int zonePort = ConfigHelper.GetInstace().ZonePort;
                if (this.asyncClient != null)
                {
                    this.asyncClient.Dispose();
                    this.asyncClient.onConnected -= new AsyncClient.Connected(client_onConnected);
                    this.asyncClient.onDisConnect -= new AsyncClient.DisConnect(client_onDisConnect);
                    this.asyncClient.onDataByteIn -= new AsyncClient.DataByteIn(client_onDataByteIn);
                }
                asyncClient = new AsyncClient();
                asyncClient.onConnected += new AsyncClient.Connected(client_onConnected);
                asyncClient.Connect(zoneIP, zonePort);
                asyncClient.onDataByteIn += new AsyncClient.DataByteIn(client_onDataByteIn);
                asyncClient.onDisConnect += new AsyncClient.DisConnect(client_onDisConnect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            string cmd = System.Text.Encoding.UTF8.GetString(SocketData);
            this.Invoke(new DelString(SetText), new object[] { cmd });
            string[] dataList = cmd.Split('|');
            foreach (string data in dataList)
            {
                this.Invoke(new DelString(DataDataFromZoneServer), new object[] { data });
            }
        }

        void client_onConnected()
        {
            try
            {
                Thread.Sleep(100);
                asyncTimer.Stop();
                string message = "连接到展区!";
                this.Invoke(new DelString(SetText), new object[] { message });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void client_onDisConnect()
        {
            string message = "连接到展区断线!";
            this.Invoke(new DelString(SetText), new object[] { message });
            asyncTimer.Start();
        }

        private void asyncTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ReConnect();
        }

        private void ReConnect()
        {
            asyncClient.Dispose();
            asyncClient.Connect(ConfigHelper.GetInstace().ZoneIP, ConfigHelper.GetInstace().ZonePort);
        }
        #endregion

        void DataDataFromZoneServer(string msg)
        {
            try
            {
                var msgPart = msg.Split(':');
                var fun = msgPart[0];
                var arg = msg.Substring(msg.IndexOf(":") + 1);
                switch (fun)
                {
                    case "lan":
                        //this.Invoke(new DelString(DelLan), arg);
                        this.Invoke(new DelString(DelLanFromZone), arg);
                        break;
                    default://del asset
                        //this.Invoke(new Del2StringArgs(DelAsset), fun, arg);
                        break;
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
            var fun =msgPart[0];
            var arg=msgPart[1];
            switch(fun)
            {
                case "lan":
                    //this.Invoke(new DelString(DelLan), arg);
                    this.Invoke(new DelString(DelLanFromPad), arg);
                    break;
                default://del asset
                    this.Invoke(new Del2StringArgs(DelAsset), fun, arg);
                    break;
            }
        }

        private void DelAsset(string assetString, string msg)
        {
            AssetType aType = (AssetType)Enum.Parse(typeof(AssetType), assetString);
            var msgPart = msg.Split('&');
            var assetFuc = msgPart[0];
            var assetName = msg.Substring(msg.IndexOf("&") + 1);
            if (assetFuc == "open")
            {
                if (_activeName == assetName && _activeAssettool.AssetType == aType)//asset already opened
                {
                    _activeAssettool.Reset();
                }
                else //new asset
                {
                    HideMouse();
                    IAssetTool tool = CreateAssetTool(aType);
                    tool.OpenAsset(assetName);
                    if (tool.AssetType == AssetType.webpage||tool.AssetType == AssetType.app)//打开时间久，避免出现桌面
                    {
                        Thread.Sleep(3000);
                    }
                    if (!((tool.AssetType == _activeAssettool.AssetType) && (tool.AssetType == AssetType.ppt || tool.AssetType == AssetType.webpage)))//同文件类型切换时，ppt和网页不关闭
                    {
                        CloseActiveAsset();
                    }
                    _activeAssettool = tool;
                    _activeName = assetName;
                }
            }
            else
                _activeAssettool.DelAsset(assetFuc, assetName);
        }

        private IAssetTool CreateAssetTool(AssetType assetType)
        {
            IAssetTool newAssetTool;
            switch (assetType)
            {
                case AssetType.video:
                    newAssetTool = new VideoForm();
                    (newAssetTool as VideoForm).Show();
                    break;
                case AssetType.webpage:
                    newAssetTool = new WebPageTool();
                    break;
                case AssetType.flash:
                    newAssetTool = new FlashForm();
                    (newAssetTool as FlashForm).Show();
                    break;
                case AssetType.app:
                    newAssetTool = new AppTool();
                    break;
                case AssetType.image:
                    newAssetTool = new ImageForm();
                    (newAssetTool as ImageForm).Show();
                    break;
                case AssetType.ppt:
                    //newAssetTool = new PointApp.AssetTool.PPTTool();
                    newAssetTool = PPTTool.GetInstance();
                    break;
                default:
                    newAssetTool = null;
                    break;
            }
            return newAssetTool;
        }

        private void CloseActiveAsset()
        {
            _activeAssettool.CloseAsset();
            _activeAssettool = null;
            _activeName = "";
        }

        private void DelLan(string lanArg)
        {
            var msgPart = lanArg.Split('&');
            var targetRange = msgPart[0];
            var targetLan = lanArg.Substring(lanArg.IndexOf("&") + 1);
            if (targetLan == LanHelper.CurrentLan)
            {
                if (targetRange == "setZone")
                {
                    NotifyChangeZoneLan(targetLan);
                }
            }
            else
            {
                //LanHelper.CurrentLan = targetLan;
                if (targetRange == "setPoint")//close,then open from targetLan folder
                {
                    ChangePointLan(targetLan);
                }
                else if (targetRange == "setZone")
                {
                    //ChangePointLan(targetLan);
                    NotifyChangeZoneLan(targetLan);
                }
            }
            
        }

        private void DelLanFromPad(string lanArg)
        {
            var msgPart = lanArg.Split('&');
            var targetRange = msgPart[0];
            var targetLan = lanArg.Substring(lanArg.IndexOf("&") + 1);
            if (targetRange == "setZone")
            {
                NotifyChangeZoneLan(targetLan);
                if (_activeAssettool.AssetType != AssetType.app)
                {
                    ChangePointLan(targetLan);
                }
            }
            else if (targetRange == "setPoint")
            {
                if (_activeAssettool.AssetType != AssetType.app)
                {
                    ChangePointLan(targetLan);
                }
            }
            
        }

        private void DelLanFromZone(string lanArg)
        {
            var msgPart = lanArg.Split('&');
            var targetRange = msgPart[0];
            var targetLan = lanArg.Substring(lanArg.IndexOf("&") + 1);
            //if (targetLan == LanHelper.CurrentLan)
            //{
            //    return;
            //}
            //else
            //{
            //    if (targetRange == "setPoint")//close,then open from targetLan folder
            //    {
            //        ChangePointLan(targetLan);
            //    }
            //}
            if (targetRange == "setPoint")//close,then open from targetLan folder
            {
                ChangePointLan(targetLan);
            }
        }

        private void NotifyChangeZoneLan(string targetLan)
        {
            SendCurrentLantoZoneServer(targetLan);
        }

        private void SendCurrentLantoZoneServer(string targetLan)
        {
            string msgtoSend = string.Format("lan:setZone&{0}|", targetLan);
            asyncClient.Send(msgtoSend);
        }

        private void ChangePointLan(string targetLan)
        {
            //var lanDir = PathHelper.AssetMainFolder + "\\" + targetLan;
            //if(!Directory.Exists(lanDir))//Unsupported lan
            //{
            //    return;
            //}
            //LanHelper.CurrentLan = targetLan;

            if (_activeAssettool == null)
            {
                StartDefault();
            }
            else
            {
                //if (_activeAssettool.AssetType == AssetType.app)//app兼容问题
                //{
                //    return;
                //}
                if (targetLan == LanHelper.CurrentLan)
                {
                    return;
                }

                string itemDir = PathHelper.AssetNonLanFolder(_activeAssettool.AssetName) + "\\" + targetLan;
                if (!Directory.Exists(itemDir))//unsupported lan
                {
                    return;
                }

                try
                {
                    //old
                    //LanHelper.CurrentLan = targetLan;
                    //var assetTool = CreateAssetTool(_activeAssettool.AssetType);
                    //assetTool.OpenAsset(_activeAssettool.AssetName);
                    //if (assetTool.AssetType == AssetType.ppt || assetTool.AssetType == AssetType.webpage)//PPT会将所有语言一起关闭，切换时只打开不关闭
                    //{
                    //    _activeAssettool = assetTool;
                    //    return;
                    //}
                    //_activeAssettool.CloseAsset();
                    //_activeAssettool = assetTool;

                    //new
                    LanHelper.CurrentLan = targetLan;
                    var assetTool = CreateAssetTool(_activeAssettool.AssetType);

                    switch (assetTool.AssetType)
                    {
                        case AssetType.app:
                            CloseCurrentAsset();
                            assetTool.OpenAsset(_activeAssettool.AssetName);
                            break;
                        case AssetType.ppt:
                        case AssetType.webpage:
                            assetTool.OpenAsset(_activeAssettool.AssetName);
                            break;
                        default:
                            assetTool.OpenAsset(_activeAssettool.AssetName);
                            CloseActiveAsset();
                            break;
                    }
                    _activeAssettool = assetTool;

                    //assetTool.OpenAsset(_activeAssettool.AssetName);
                    //if (assetTool.AssetType == AssetType.ppt || assetTool.AssetType == AssetType.webpage)//PPT会将所有语言一起关闭，切换时只打开不关闭
                    //{
                    //    _activeAssettool = assetTool;
                    //    return;
                    //}
                    //_activeAssettool.CloseAsset();
                    //_activeAssettool = assetTool;
                }
                catch (Exception e)
                {
                    ErrorLogHelper.Log("切换语言错误：" + e.Message);
                }
            }
        }

        private void CloseCurrentAsset()
        {
            _activeAssettool.CloseAsset();
        }

        private void SetText(string data)
        {
            rtb_data.AppendText(data + "\n");
            if (rtb_data.Lines.Count() >= 1000)
            {
                rtb_data.Clear();
            }
        }

        private void SelectDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = PathHelper.StartPath + "\\asset";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _startPath = new DirectoryInfo(fbd.SelectedPath).Name;
                lbStartPath.Text = _startPath;
            }
        }

        private void UpdateStartInfo_Click(object sender, EventArgs e)
        {
            UpdateStartInfo();
        }

        private void UpdateStartInfo()
        {
            _startPath = lbStartPath.Text.Trim();
            _startType = cbStartType.SelectedItem.ToString().Trim();
            _startLan = cbLan.SelectedItem.ToString().Trim().ToLower();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(PathHelper.StartPath + "\\startinfo.xml");
            xdoc.SelectSingleNode("Data/StartType").InnerText = _startType;
            xdoc.SelectSingleNode("Data/StartPath").InnerText = _startPath;
            xdoc.SelectSingleNode("Data/StartLan").InnerText = _startLan;
            xdoc.Save(PathHelper.StartPath + "\\startinfo.xml");
            StartDefault();
        }

        private void HideTaskBar()
        {
            _tbHelper.SetTaskbarState(TaskBarHelper.AppBarStates.AutoHide);
        }

        private void ShowTaskBar()
        {
            _tbHelper.SetTaskbarState(TaskBarHelper.AppBarStates.AlwaysOnTop);
        }

        private void HideMouse()
        {
            MouseAndKeyControl.SetCursorPos(3850, 0);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShowTaskBar();
            if(_activeAssettool!=null)
            {
                _activeAssettool.CloseAsset();
            }
        }
    }   
}
