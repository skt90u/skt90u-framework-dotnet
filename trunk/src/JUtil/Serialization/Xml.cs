using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace JUtil.Serialization
{
    public class Xml
    {
        /*
        [Serializable]
        public class ClsSerializable
        {
            private int _Number;
            internal string _Demo;
            public string Company = "C#";

            public ClsSerializable()
            {
                this._Number = 254;
                this._Demo = "this is a book";
            }

            public int Number
            {
                get { return this._Number; }
            }
            public string Demo
            {
                get { return this._Demo; }
            }
        } 
        
        string s = JUtil.Serialization.Xml.Serialize(new ClsSerializable());
        ClsSerializable obj = JUtil.Serialization.Xml.Deserialize<ClsSerializable>(s);
        */
        // http://blog.miniasp.com/post/2009/03/24/How-to-serialize-and-deserialize-using-CSharp-Part-II.aspx
        public static string Serialize(object o)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter writer = new StringWriter(sb);
                serializer.Serialize(writer, o);
                return sb.ToString();
            }
            catch (Exception e)
            {
                Log.E("JUtil.Serialization.Xml.Serialize({0})失敗，{1}", o.GetType().Name, e.Message);
                
                throw;
            }
        }

        public static T Deserialize<T>(string s) where T : new()
        {
            XmlDocument xdoc = new XmlDocument();

            try
            {
                xdoc.LoadXml(s);
                XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                object obj = ser.Deserialize(reader);

                return (T)obj;
            }
            catch (Exception e)
            {
                Log.E("JUtil.Serialization.Xml.Deserialize<{0}>({1})失敗，因為{2}，將使用預設建構子建構物件。", typeof(T).Name, s, e.Message);

                return new T();

                // default(T) is null, we don't want to return a null object
                //return default(T);
            }
        }
    }
}
