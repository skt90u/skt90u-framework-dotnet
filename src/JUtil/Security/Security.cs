using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace JUtil
{
    /// <summary>
    /// Security
    /// </summary>
    /// <remarks>
    /// reference : http://blog.csdn.net/zhaoqiliang527/article/details/5691652
    /// </remarks>
    public static class Security
    {
        /// <summary>
        /// MD5函數
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5結果</returns>
        public static string MD5(string str)
        {
            string ret = string.Empty;

            byte[] b = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(str));
            
            for (int i=0; i<b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');

            return ret;
        }

        /// <summary>
        /// SHA256函數
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256結果</returns>
        public static string SHA256(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);

            SHA256Managed Sha256 = new SHA256Managed();

            byte[] Result = Sha256.ComputeHash(SHA256Data);

            return Convert.ToBase64String(Result);  //返回長度為44字節的字符串
        }

        /// <summary>
        /// Decode
        /// </summary>
        public static string Decode(string encodingData, string rgbKeyCode, string rgbIVCode)
        {
            string orgiData = string.Empty;

            byte[] bufEncodingData = Convert.FromBase64String(encodingData);

            byte[] rgbKey = Encoding.ASCII.GetBytes(rgbKeyCode);
            byte[] rgbIV = Encoding.ASCII.GetBytes(rgbIVCode);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(bufEncodingData);
            CryptoStream cs = new CryptoStream(ms, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Read);

            StreamReader reader = new StreamReader(cs);
            orgiData = reader.ReadToEnd();

            return orgiData;
        }

        /// <summary>
        /// Decode
        /// </summary>
        public static string Decode(string encodingData)
        {
            return Decode(encodingData, DEFAULT_RGB_KEY_CODE, DEFAULT_RGB_IV_CODE);
        }

        /// <summary>
        /// Encode
        /// </summary>
        public static string Encode(string orgiData, string rgbKeyCode, string rgbIVCode)
        {
            string encodingData = string.Empty;

            byte[] rgbKey = Encoding.ASCII.GetBytes(rgbKeyCode);
            byte[] rgbIV = Encoding.ASCII.GetBytes(rgbIVCode);

            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            int keySize = provider.KeySize;

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cs);
            writer.Write(orgiData);
            writer.Flush();
            cs.FlushFinalBlock();
            writer.Flush();
            encodingData = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

            return encodingData;
        }

        /// <summary>
        /// Encode
        /// </summary>
        public static string Encode(string orgiData)
        {
            return Encode(orgiData, DEFAULT_RGB_KEY_CODE, DEFAULT_RGB_IV_CODE);
        }

        private const string DEFAULT_RGB_KEY_CODE = "VavicApp"; // 對稱演算法所用的秘密金鑰
        private const string DEFAULT_RGB_IV_CODE = "VavicApp"; // 對稱演算法所用的初始化向量
    } // end of Security
}
