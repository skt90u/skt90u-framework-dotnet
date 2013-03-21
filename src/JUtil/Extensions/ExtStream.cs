using System.IO;
using System;

namespace JUtil.Extensions
{
    /// <summary>
    /// Extensions of Stream
    /// </summary>
    public static class ExtStream
    {
        /// <summary>
        /// Save a stream to a specified file
        /// </summary>
        /// <param name="self"></param>
        /// <param name="fileName"></param>
        public static void SaveAs(this Stream self, string fileName)
        {
            if (self == null)
                throw new ArgumentNullException("cannot save empty Stream");

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(fs);
            BinaryReader reader = new BinaryReader(self);

            int Length = 256;
            Byte[] buffer = new Byte[Length];

            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            int bytesRead = reader.Read(buffer, 0, Length);
            while (bytesRead > 0)
            {
                writer.Write(buffer, 0, bytesRead);
                bytesRead = reader.Read(buffer, 0, Length);
            }

            writer.Close();
        }


    } // end of ExtStream
}


