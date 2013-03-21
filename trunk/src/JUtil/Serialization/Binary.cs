using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace JUtil.Serialization
{
    /// <remarks>
    /// reference : http://www.dotblogs.com.tw/atowngit/archive/2009/12/16/12491.aspx
    /// </remarks>
    public class Binary
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
        
        MemoryStream stream = JUtil.Serialization.Binary.Serialize(new ClsSerializable());
        ClsSerializable obj = JUtil.Serialization.Binary.Deserialize<ClsSerializable>(stream);
        */
        public static MemoryStream Serialize(object o)
        {
            MemoryStream stream = new MemoryStream();

            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, o);

            return stream;

            // if you want to get byte array : Serialize(o).ToArray()
        }

        public static void Serialize(object o, string filepath)
        {
            using(MemoryStream stream = Serialize(o))
            {
                File.WriteAllBytes(filepath, stream.ToArray());
            }
        }

        public static T Deserialize<T>(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            object obj = formatter.Deserialize(stream);

            return (T)obj;
        }

        public static T Deserialize<T>(string filepath)
        {
            using(FileStream stream = new FileStream(filepath, FileMode.Open))
            {
                return Deserialize<T>(stream);
            }
        }

        public static T Deserialize<T>(byte[] arrByte)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(arrByte, 0, arrByte.Length);

                return Deserialize<T>(stream);
            }
        }
    }
}
