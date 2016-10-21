using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainApp.Model;
using MainApp.Helper;

namespace MainApp.Component
{
    public partial class AppPanel : UserControl,IItemControl
    {
        private Item _item;
        private bool _isChanged = false;
        public AppPanel()
        {
            InitializeComponent();
        }

        public Model.Item GetItem()
        {
            if (_item == null)
            {
                _item = new Item();
                _item.ID = IDCreater.CreateID();
            }
            int isOldVersion = this.cbOldVersion.Checked ? 1 : 0;
            _item.Type = ItemType.App.ToString();
            _item.Name = txtName.Text.Trim();
            _item.Arg = txtStartPath.Text.Trim() + "*" + txtPad.Text.Trim() + "*" + txtConfig.Text.Trim() + "*" + isOldVersion.ToString();
            return _item;
        }

        public event ItemDeleteHandler ItemDelete;

        private void Delete_Click(object sender, EventArgs e)
        {
            if (this.ItemDelete!=null)
            {
                ItemDelete(this);
            }
        }

        public void LoadItem(Item item)
        {
            _item = item;
            if (item.Type == ItemType.App.ToString())
            {
                txtName.Text = item.Name;
                txtStartPath.Text = item.Arg.Split('*')[0];
                txtPad.Text = item.Arg.Split('*')[1];
                txtConfig.Text = item.Arg.Split('*')[2];
                int isOldversion=int.Parse(item.Arg.Split('*')[3]);
                cbOldVersion.Checked = isOldversion ==1? true : false;
            }
            _isChanged = false;
        }

        private void PadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "pad安装文件|*.swf";
            //ofd.InitialDirectory = Application.StartupPath;
            ofd.InitialDirectory = (string.IsNullOrEmpty(AppHelper.AppSelectPath)) ? Application.StartupPath : AppHelper.AppSelectPath;
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                AppHelper.AppSelectPath = ofd.FileName;
                var fileName = ofd.FileName;
                this.txtPad.Text = fileName;
                _isChanged = true;
            }
        }

        private void PadConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "pad配置文件|*.xml";
            //ofd.InitialDirectory = Application.StartupPath;
            ofd.InitialDirectory = (string.IsNullOrEmpty(AppHelper.AppSelectPath)) ? Application.StartupPath : AppHelper.AppSelectPath;
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                AppHelper.AppSelectPath = ofd.FileName;
                var fileName = ofd.FileName;
                this.txtConfig.Text = fileName;
                _isChanged = true;
            }
        }

        bool IItemControl.IsChanged
        {
            get
            {
                if (_isChanged == true)
                {
                    return true;
                }
                if (_item == null)
                {
                    return true;
                }
                int isOldversion = int.Parse(_item.Arg.Split('*')[3]);
                if (_item.Name != txtName.Text.Trim() || _item.Arg != txtStartPath.Text.Trim() + "*" + txtPad.Text.Trim() + "*" + txtConfig.Text.Trim()+"*"+isOldversion)
                {
                    return true;
                }
                return _isChanged;
            }
            set
            {
                _isChanged = value;
            }
        }
    }
}
