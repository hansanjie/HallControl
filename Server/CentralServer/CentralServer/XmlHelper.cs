using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CentralServer
{
    public class XmlHelper
    {
        internal static string SerializeObj<T>(T t)
        {
            using (StringWriter sw = new StringWriter())
            {
                var en=sw.Encoding;
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(sw, t);
                string result = sw.ToString();
                return result;
            }
        }

        internal static T DeSerialize<T>(string dataString)
        {
            using (StringReader rdr = new StringReader(dataString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                //反序列化，并将反序列化结果值赋给变量t
                T t = (T)serializer.Deserialize(rdr);
                //输出反序列化结果
                return t;
            }
            
        }
    }
}
