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
        #region 轉型
        public static bool ToBool(this string expression)
        {
            if (expression != null)
            {
                if (string.Compare(expression, "true", true /*ignoreCase*/) == 0)
                    return true;
                else if (string.Compare(expression, "false", true/*ignoreCase*/) == 0)
                    return false;
                else if (string.Compare(expression, "1") == 0)
                    return true;
                else if (string.Compare(expression, "0") == 0)
                    return false;
            }
            return default(bool);
        }

        public static int ToInt(this string str)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return default(int);

            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;

            return Convert.ToInt32(str.ToFloat());
        }

        public static float ToFloat(this string strValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
                return default(float);

            float intValue = default(float);
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(strValue, out intValue);
            }
            return intValue;
        }

        public static DateTime ToDateTime(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime))
                    return dateTime;
            }

            return default(DateTime);
        }

        public static T ToEnum<T>(this string value, T defValue)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentException)
            {
                return defValue;
            }
        }
        #endregion

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

        /// <summary>
        /// Save a string to a file
        /// </summary>
        /// <param name="self"></param>
        /// <param name="fileName"></param>
        public static void SaveAs(this string self, string fileName)
        {
            //System.IO.File.WriteAllText(fileName, self);
            System.IO.File.WriteAllText(fileName, self, Encoding.UTF8);
        }

        /// <summary>
        /// Save a string to a exsiting file
        /// </summary>
        /// <param name="self"></param>
        /// <param name="filename"></param>
        public static void AppendTo(this string self, string filename)
        {
            bool append = true;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, append))
            {
                file.WriteLine(self);
            } 
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

        public static bool StrIsNullOrEmpty(this string str)
        {
            return (str == null || str.Trim() == string.Empty);
        }

        public static string[] SplitString(string strContent, string strSplit)
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
