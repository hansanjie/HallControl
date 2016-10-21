using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApp.Model
{
    public enum ItemType { Image, PPT, Video, WebPage, Flash, App };
    [Serializable]
    public class Item
    {
        public string ID;
        public string Name;
        public string Type;
        public string Arg;
        public int Version = 0;
    }
}
