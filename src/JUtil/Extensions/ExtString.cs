using System.Text;
using System.Text.RegularExpressions;
using System;

namespace JUtil.Extensions
{
    /// <summary>
    /// Extensions of String
    /// </summary>
    public static partial class ExtString
    {
        //////////////////////////////////////////////////////////////////////////
        // 轉型
        //////////////////////////////////////////////////////////////////////////
        public static bool ToBool(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(bool);

            if (string.Compare(source, "true", true /* ignoreCase */) == 0) return true;

            if (string.Compare(source, "false", true /* ignoreCase */) == 0) return false;

            bool value;

            if (!bool.TryParse(source, out value))
                value = default(bool);

            return value;
        }

        public static DateTime ToDateTime(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(DateTime);

            DateTime value;

            if (!DateTime.TryParse(source, out value))
                value = default(DateTime);

            return value;
        }

        public static decimal ToDecimal(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(decimal);

            decimal value;

            if (!decimal.TryParse(source, out value))
                value = default(decimal);

            return value;
        }

        public static short ToShort(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(short);

            short value;

            if (!short.TryParse(source, out value))
                value = default(short);

            return value;
        }

        public static long ToLong(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(long);

            long value;

            if (!long.TryParse(source, out value))
                value = default(long);

            return value;
        }

        public static int ToInt(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(int);

            int value;

            if (!int.TryParse(source, out value))
                value = default(int);

            return value;
        }

        public static float ToFloat(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(float);

            float value;

            if (!float.TryParse(source, out value))
                value = default(float);

            return value;
        }

        public static bool TryParseEnum<T>(this string value, bool ignoreCase, out T enumValue) where T : struct
        {
            try
            {
                enumValue = (T)System.Enum.Parse(typeof(T), value, ignoreCase);
                return true;
            }
            catch
            {
                enumValue = default(T);
                return false;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        // 寫檔
        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Save a string to a file
        /// </summary>
        /// <param name="self"></param>
        /// <param name="fileName"></param>
        public static void SaveAs(this string self, string fileName)
        {
            SaveAs(self, fileName, Encoding.UTF8);
        }

        public static void SaveAs(this string self, string fileName, Encoding encoding)
        {
            System.IO.File.WriteAllText(fileName, self, encoding);
        }

        /// <summary>
        /// Save a string to a exsiting file
        /// </summary>
        /// <param name="self"></param>
        /// <param name="filename"></param>
        public static void AppendTo(this string self, string filename)
        {
            AppendTo(self, filename, Encoding.UTF8);
        }

        public static void AppendTo(this string self, string filename, Encoding encoding)
        {
            bool append = true;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, append, encoding))
            {
                file.WriteLine(self);
            }
        }

        //////////////////////////////////////////////////////////////////////////

        public static string ToJavaScriptString(this string str)
        {
            return str.Replace("\n", "").Replace("\r", "").Replace(@"\", @"\\").Replace("'", @"\'").Replace("\"", "\\\"").Replace("/", @"\/");
        }

        /// <summary>
        /// 返回字符串真實長度，1個漢字長度為2
        /// </summary>
        public static int GetRealLength(this string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static string ToSimplified(this string self)
        {
            return CharSetConverter.ToSimplified(self);
        }

        public static string ToTraditional(this string self)
        {
            return CharSetConverter.ToTraditional(self);
        }

        public static int[] ToIntArray(string idList)
        {
            if (string.IsNullOrEmpty(idList))
                return null;

            string[] strArr = SplitString(idList, ",");

            int[] intArr = new int[strArr.Length];

            for (int i = 0; i < strArr.Length; i++)
                intArr[i] = strArr[i].ToInt();

            return intArr;
        }

        public static bool StrIsNullOrEmpty(this string source)
        {
            return (string.IsNullOrEmpty(source) || source.Trim().Equals(string.Empty));
        }

        public static string[] SplitString(this string strContent, string strSplit)
        {
            if (!StrIsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }
    } // end of ExtString
}
