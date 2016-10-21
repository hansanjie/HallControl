using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApp.Model
{
    [Serializable]
    public class ZoneInfo
    {
        public string ID;
        //public UInt64 version;
        public PointInfo[] PointList;
        public string Name;

        public override string ToString()
        {
            return Name;
        }
    }
}
