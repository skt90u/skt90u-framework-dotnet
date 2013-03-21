using System.IO;
using System;

namespace JUtil.Extensions
{
    public static class ExtStream
    {
        public static void SaveAs(this Stream self, string fileName)
        {
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


