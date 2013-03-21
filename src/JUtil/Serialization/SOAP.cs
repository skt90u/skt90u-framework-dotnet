using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace JUtil.Serialization
{
    /// <remarks>
    /// http://www.dotblogs.com.tw/atowngit/archive/2009/12/16/12491.aspx
    /// </remarks>
    public class SOAP
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
        
        string s = JUtil.Serialization.SOAP.Serialize(new ClsSerializable());
        ClsSerializable obj = JUtil.Serialization.SOAP.Deserialize<ClsSerializable>(s);
        */
        public static string Serialize(object o)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                SoapFormatter formatter = new SoapFormatter();
                //序利化物件
                formatter.Serialize(stream, o);

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T Deserialize<T>(string s)
        {
            using(Stream stream = new MemoryStream(Encoding.Default.GetBytes(s)))
            {
                SoapFormatter formatter = new SoapFormatter();

                object obj = formatter.Deserialize(stream);

                return (T)obj;
            }
        }
    }
}
