using MainApp.Component;
using MainApp.Helper;
using MainApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MainApp.Forms
{
    public partial class PointConfigForm : Form
    {
        private PointInfo _pointInfo;
        private HallInfo _hallInfo;
        private HallHelper _hallHelper;
        public PointConfigForm()
        {
            InitializeComponent();
        }

        public PointInfo PointInfo { get { return _pointInfo; } set { _pointInfo = value; } }

        public HallInfo HallInfo { get { return _hallInfo; } set { _hallInfo = value; } }

        public string LocalPath { get; set; }

        private void PointConfigForm_Load(object sender, EventArgs e)
        {
            _hallHelper = new HallHelper();
            _hallHelper.LocalDataPath = this.LocalPath;
            cbType.SelectedIndex = 0;
            FillExistedPointInfo();
        }

        private void FillExistedPointInfo()
        {
            this.txtName.Text = _pointInfo.Name;
            this.txtIP.Text = _pointInfo.IP;
            this.txtPort.Text = _pointInfo.Port;
            //Fill items
            if (_pointInfo.Items == null || _pointInfo.Items.Count() == 0)//no item
            {
                return;
            }
            foreach (var item in _pointInfo.Items)
            {
                
                IItemControl itemCtr = null;
                if (item.Type == ItemType.App.ToString())
                {
                    itemCtr = new AppPanel();
                }
                else if(item.Type == ItemType.Flash.ToString())
                {
                    itemCtr = new FlashPanel();
                }
                else if (item.Type == ItemType.Image.ToString())
                {
                    itemCtr = new ImagePanel();
                }
                else if (item.Type == ItemType.PPT.ToString())
                {
                    itemCtr = new PPTPanel();
                }
                else if (item.Type == ItemType.Video.ToString())
                {
                    itemCtr =new VideoPanel();
                }
                else if (item.Type == ItemType.WebPage.ToString())
                {
                    itemCtr = new WebPagePanel();
                }

                //load config
                if (itemCtr != null)
                {
                    this.flpItem.Controls.Add(itemCtr as UserControl);
                    itemCtr.LoadItem(item);
                    AddDeleteEvent(itemCtr);
                }
            }

        }

        private void Confirm()
        {
            CheckChange();
            UpdateItemsVersion();
            _pointInfo.Name = txtName.Text.Trim();
            _pointInfo.IP = txtIP.Text.Trim();
            _pointInfo.Port = txtPort.Text.Trim();
            _pointInfo.SupportedLan = lanSelecter1.SelectedLan;
            List<Item> items = new List<Item>();
            foreach (var iControl in flpItem.Controls)
            {
                IItemControl itemControl = iControl as IItemControl;
                if (itemControl != null)
                {
                    items.Add(itemControl.GetItem());
                }
            }
            _pointInfo.Items = items.ToArray();
            _hallHelper.LocalCurrentHallInfo = _hallInfo;
            _hallHelper.UpdateHallInfo();
            _pointInfo.IsChanged = false;
        }

        private void UpdateItemsVersion()
        {
            foreach (var itemControl in flpItem.Controls)//item change
            {
                if ((itemControl as IItemControl).IsChanged)
                {
                    var item = (itemControl as IItemControl).GetItem();
                    item.Version += 1;
                }
            }
        }

        /// <summary>
        /// check if a pointinfo change
        /// </summary>
        private void CheckChange()
        {
            if (_pointInfo.IsChanged)
            {
                return;
            }

            if (this.lanSelecter1.IsChange())//lan change
            {
                _pointInfo.IsChanged = true;
                return;
            }
            if(_pointInfo.Name!=txtName.Text.Trim()||_pointInfo.IP!=txtIP.Text.Trim()||_pointInfo.Port!=txtPort.Text.Trim())//basic change
            {
                _pointInfo.IsChanged = true;
                return;
            }

            foreach (var itemControl in flpItem.Controls)//item change
            {
                if ((itemControl as IItemControl).IsChanged)
                {
                    _pointInfo.IsChanged = true;
                    return;
                }
            }
                
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        [Obsolete]
        private void OK_Click(object sender, EventArgs e)
        {
            Confirm();
            this.Close();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtIP.Text == "" || txtPort.Text == "")
            {
                lbInfo.Text = "*为必填内容，请完成后再提交";
                return;
            }
            Confirm();
            this.DialogResult = DialogResult.OK;
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            ItemType itemType = (ItemType)cbType.SelectedIndex;
            IItemControl itemtoAdd = null;
            switch (itemType)
            {
                case ItemType.Image:
                    itemtoAdd = new ImagePanel();
                    break;
                case ItemType.PPT:
                    itemtoAdd = new PPTPanel();
                    break;
                case ItemType.App:
                    itemtoAdd = new AppPanel();
                    break;
                case ItemType.Flash:
                    itemtoAdd = new FlashPanel();
                    break;
                case ItemType.Video:
                    itemtoAdd = new VideoPanel();
                    break;
                case ItemType.WebPage:
                    itemtoAdd = new WebPagePanel();
                    break;
                default:
                    break;
            }
            itemtoAdd.IsChanged = true;
            AddDeleteEvent(itemtoAdd);
        }

        private void AddDeleteEvent(IItemControl item)
        {
            if (item != null)
            {
                UserControl itemControl = item as UserControl;
                if (itemControl != null)
                {
                    item.ItemDelete += itemtoAdd_ItemDelete;
                    flpItem.Controls.Add(itemControl);
                    _pointInfo.IsChanged = true;
                }
            }
        }

        void itemtoAdd_ItemDelete(object sender)
        {
            UserControl itemControl = sender as UserControl;
            if (itemControl != null && flpItem.Controls.Contains(itemControl))
            {
                flpItem.Controls.Remove(itemControl);
            }
        }

        private void PadSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "pad安装文件|*.swf";
            ofd.InitialDirectory = Application.StartupPath;
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                var fileName = ofd.SafeFileName;
            }
        }
    }
}
