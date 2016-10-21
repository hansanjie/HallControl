using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MainApp.Model
{
    [Serializable]
    public class HallInfo
    {
        public ZoneInfo[] zoneList;
        public string Name;
    }
}
