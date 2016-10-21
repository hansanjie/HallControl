using MainApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApp.Component
{
    public delegate void ItemDeleteHandler(object sender);
    interface IItemControl
    {
        Item GetItem();
        event ItemDeleteHandler ItemDelete;
        void LoadItem(Item item);
        bool IsChanged { set; get; }
    }
}
