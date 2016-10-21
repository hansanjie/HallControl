using MainApp.Forms;
using MainApp.Helper;
using MainApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MainApp
{
    public partial class MainForm : Form,Ilog
    {
        private HallHelper _hallHelper;
        private string dataPath = Application.StartupPath + "\\data";
        private System.Timers.Timer logClearTimer = new System.Timers.Timer(5000);
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigHelper.ResolveConfig(Application.StartupPath + "\\config.xml");
            ErrorLogHelper.LocalPath = Application.StartupPath;
            InfoLogHelper.RegisterLog(this);
            logClearTimer.Elapsed += logClearTimer_Elapsed;
            ServerHelper.GetInstace();
            InitComponents();
            _hallHelper = new HallHelper();
            _hallHelper.LocalDataPath = Application.StartupPath;
            try
            {
                FillExistHallInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("get remote hallinfo err:" + ex.Message);
            }
        }

        private void InitComponents()
        {
            lbZone.SelectedIndexChanged += lbExZone_SelectedIndexChanged;
        }

        private void FillExistHallInfo()
        {
            System.Threading.Thread.Sleep(500);
            if (!ServerHelper.GetInstace().IsConnected)
            {
                InfoLogHelper.AppendLog("未连接到服务器，请检查配置和网络");
                return;
            }
            if (_hallHelper.LocalCurrentHallInfo == null)
            {
                _hallHelper.LocalCurrentHallInfo = HallHelper.PublishedHallInfo;
            }
            if (_hallHelper.LocalCurrentHallInfo == null)//no hallinfo
            {
                return;
            }
            this.txtHallName.Text = _hallHelper.LocalCurrentHallInfo.Name;
            if (_hallHelper.LocalCurrentHallInfo.zoneList != null && _hallHelper.LocalCurrentHallInfo.zoneList.Count() > 0)
            {
                foreach (var zoneInfo in _hallHelper.LocalCurrentHallInfo.zoneList)
                {
                    var index = lbZone.Items.Add(zoneInfo);
                }
                lbZone.SelectedIndex = 0;
            }
        }

        void lbExZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbPoint.Items.Clear();
            ZoneInfo selectedZone = ((sender as ListBox).SelectedItem as ZoneInfo);
            if (selectedZone != null && selectedZone.PointList!=null)
            {
                foreach (var point in selectedZone.PointList)
                {
                    lbPoint.Items.Add(point);
                }
                lbPoint.SelectedIndex = 0;
            }
        }

        private void btnZoneAdd_Click(object sender, EventArgs e)
        {
            AddZoneForm addZoneForm = new AddZoneForm();
            var result=addZoneForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                var zone = addZoneForm.Zone;
                AddZone(zone);
                lbZone.SelectedItem = zone;
            }
        }

        private void AddZone(ZoneInfo zone)
        {
            lbZone.Items.Add(zone);
        }

        private void btnZoneMinus_Click(object sender, EventArgs e)
        {
            this.lbZone.Items.Remove(this.lbZone.SelectedItem);
            if (this.lbZone.Items.Count != 0)
            {
                lbZone.SelectedIndex = 0;
            }
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            ZoneInfo zone = SelectedZone();
            PointInfo addedPoint;
            AddPointForm addForm=new AddPointForm();
            var result = addForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                addedPoint = addForm.PointInfo;
                if (zone.PointList != null)
                {
                    var pointList = zone.PointList.ToList();
                    pointList.Add(addedPoint);
                    zone.PointList = pointList.ToArray();
                }
                else//1st point
                {
                    zone.PointList = new PointInfo[] { addedPoint };
                }
                lbPoint.Items.Add(addedPoint);
                lbPoint.SelectedItem = addedPoint;
            }
        }

        private void btnPointMinus_Click(object sender, EventArgs e)
        {
            PointInfo selectedPoint = SelectedPoint();
            if (selectedPoint != null)
            {
                var zonepointList = SelectedZonePointList();
                zonepointList.Remove(selectedPoint);
                var zone = SelectedZone();
                zone.PointList = zonepointList.ToArray();
                lbPoint.Items.Remove(selectedPoint);
                if (lbPoint.Items.Count > 0)
                {
                    lbPoint.SelectedIndex = 0;
                }
            }
        }

        private List<PointInfo> SelectedZonePointList()
        {
            var zone = SelectedZone();
            if (zone.PointList == null)
            {
                return null;
            }
            else
            {
                return zone.PointList.ToList();
            }
        }

        private PointInfo SelectedPoint()
        {
            return lbPoint.SelectedItem as PointInfo;
        }

        private ZoneInfo SelectedZone()
        {
            return lbZone.SelectedItem as ZoneInfo;
        }

        private void Update_Click(object sender, EventArgs e)
        {
            _hallHelper.LocalCurrentHallInfo = HallInfoFromUI();
            _hallHelper.UpdateHallInfo();
        }

        private HallInfo HallInfoFromUI()
        {
            HallInfo hInfo = new HallInfo();
            hInfo.Name = txtHallName.Text.Trim();
            if (lbZone.Items.Count != 0)//zone exist
            {
                List<ZoneInfo> zoneList = new List<ZoneInfo>();
                foreach (var item in lbZone.Items)
                {
                    zoneList.Add(item as ZoneInfo);
                }
                hInfo.zoneList = zoneList.ToArray();
            }
            return hInfo;
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            _hallHelper.LocalCurrentHallInfo = HallInfoFromUI();
            if (lbPoint.SelectedItem == null)
            {
                InfoLogHelper.AppendLog("请选择一个展点");
                //SetMsg("请选择一个展点");
                return;
            }
            //_hallHelper
            PointInfo selectedInfo = lbPoint.SelectedItem as PointInfo;
            PointConfigForm pcf = new PointConfigForm();
            pcf.LocalPath = Application.StartupPath;
            pcf.PointInfo = selectedInfo;
            pcf.HallInfo = _hallHelper.LocalCurrentHallInfo;
            var result = pcf.ShowDialog();
            if (result == DialogResult.OK)
            {
                var selectedPoint = pcf.PointInfo;
                lbPoint.Items.Remove(selectedInfo);
                lbPoint.Items.Add(selectedInfo);
                lbPoint.SelectedItem = selectedInfo;
            }
        }

        void logClearTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke((Action)delegate { logClearTimer.Stop(); this.lbMsg.Text = string.Empty; });
        }

        //test
        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\data.xml";
            FTPHelpr fh = new FTPHelpr("127.0.0.1", 30009);
            fh.UpLoadFile(filePath, "1234567");
        }

        public void AppendLog(string log)
        {
            this.Invoke((Action)delegate { this.lbMsg.Text = log; logClearTimer.Start(); });
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var dialogResult= MessageBox.Show("退出展厅内容中控交互平台系统？", "确认退出", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        #region ListBox item order by drag
        private void LB_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox activeLB = sender as ListBox;
            if (activeLB.SelectedItem == null) return;
            if (e.Button == MouseButtons.Right)
            {
                int index = activeLB.IndexFromPoint(new Point(e.X, e.Y));
                if (index < 0) index = activeLB.Items.Count - 1;
                activeLB.SelectedIndex = index;
                activeLB.DoDragDrop(this.lbZone.SelectedItem, DragDropEffects.Move);
            }
        }

        private void LB_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void LB_DragDrop(object sender, DragEventArgs e)
        {
            ListBox activeLB = sender as ListBox;
            Point point = activeLB.PointToClient(new Point(e.X, e.Y));
            int index = activeLB.IndexFromPoint(point);
            if (index < 0) index = activeLB.Items.Count - 1;
            object data = activeLB.SelectedItem;
            activeLB.Items.Remove(data);
            activeLB.Items.Insert(index, data);
            activeLB.SelectedIndex = index;

            if (activeLB == this.lbPoint)
            {
                UpdateItemOrder();
            }
        }

        private void UpdateItemOrder()
        {
            ZoneInfo zi = lbZone.SelectedItem as ZoneInfo;
            List<PointInfo> pList = new List<PointInfo>();
            foreach (var item in lbPoint.Items)
            {
                PointInfo pi = item as PointInfo;
                pList.Add(pi);
            }
            zi.PointList = pList.ToArray();
        }
        #endregion

        private void LB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListBox activeLB = sender as ListBox;
            Point point = new Point(e.X, e.Y);
            int index = activeLB.IndexFromPoint(point);
            if (index < 0) index = activeLB.Items.Count - 1;
            ZoneInfo info = activeLB.SelectedItem as ZoneInfo;
            if (info != null)
            {
                EditZoneNameForm eznf = new EditZoneNameForm(info);
                var result = eznf.ShowDialog();
                if (result == DialogResult.OK)
                {
                    InfoLogHelper.AppendLog("展区名称已修改");
                    activeLB.Items.Remove(info);
                    activeLB.Items.Insert(index,info);
                    activeLB.SelectedIndex = index;
                }
            }
        }
    }
}
