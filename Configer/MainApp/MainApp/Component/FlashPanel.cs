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
    public partial class FlashPanel : UserControl, IItemControl
    {
        private Item _item;
        private bool _isChanged = false;
        public FlashPanel()
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
            _item.Type = ItemType.Flash.ToString();
            _item.Name = txtName.Text.Trim();
            return _item;
        }

        public event ItemDeleteHandler ItemDelete;

        private void Delete_Click(object sender, EventArgs e)
        {
            if (this.ItemDelete != null)
            {
                ItemDelete(this);
            }
        }

        public void LoadItem(Item item)
        {
            _item = item;
            if (item.Type == ItemType.Flash.ToString())
            {
                txtName.Text = item.Name;
            }
        }


        public bool IsChanged()
        {
            if (_item.Name != txtName.Text.Trim())
            {
                return true;
            }
            return false;
        }


        bool IItemControl.IsChanged
        {
            get
            {
                if (_isChanged == true)
                {
                    return true;
                }
                else if (_item.Name != txtName.Text.Trim())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                _isChanged = value;
            }
        }
    }
}
