using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MainApp.Model
{
    [Serializable]
    public class PointInfo
    {
        public string ID;
        public string IP;
        public string Port;
        public string Name;
        public string SupportedLan;

        //[XmlIgnore]
        public bool IsChanged = false;

        public Item[] Items;

        public override string ToString()
        {
            return Name;
        }
    }
}
