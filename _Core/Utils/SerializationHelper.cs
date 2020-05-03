using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _Core.Utils
{
    public class SerializationHelper
    {
        #region ========== XmlSerializer ==========
        /// <summary>
        /// 序列化，使用标准的XmlSerializer，优先考虑使用。
        /// 不能序列化IDictionary接口.
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void XmlSerialize(object obj, string filename)
        {
            FileStream fs = null;
            // serialize it...
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// 反序列化，使用标准的XmlSerializer，优先考虑使用。
        /// 不能序列化IDictionary接口.
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns>type类型的对象实例</returns>
        public static object XmlDeserializeFromFile(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                // open the stream...
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        public static string XmlSerialize(object obj, Encoding ecoding)
        {
            if (obj == null)
            {
                return null;
            }
            XmlSerializer ser = new XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();//制定编码和磁盘文件 
            StreamWriter sWriter = new StreamWriter(stream, ecoding);
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            //empty namespaces
            xsn.Add(String.Empty, String.Empty);

            ser.Serialize(sWriter, obj, xsn);//序列化 

            string str = ecoding.GetString(stream.ToArray());

            stream.Close();

            return str;

        }

        public static string XmlSerialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            XmlSerializer ser = new XmlSerializer(obj.GetType());
            StringWriter sWriter = new StringWriter();
            ser.Serialize(sWriter, obj);
            return sWriter.ToString();
        }

        public static object XmlDeserialize(Type type, string xmlStr)
        {
            if (xmlStr == null || xmlStr.Trim() == "")
            {
                return null;
            }
            XmlSerializer ser = new XmlSerializer(type);
            StringReader sWriter = new StringReader(xmlStr);
            return ser.Deserialize(sWriter);
        }
        #endregion
    }
}
