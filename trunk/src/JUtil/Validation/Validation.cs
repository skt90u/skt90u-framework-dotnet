using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JUtil
{
    public class Validation
    {
        public static bool IsHexString(string s)
        {
            try
            {
                Formatter.HexStringToByteArray(s);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsNonSense(string s)
        {
            return (s == null) || (s.Trim().Length == 0);
        }

        /// <summary>
        /// 檢測是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判斷的email字符串</param>
        /// <returns>判斷結果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        /// <summary>
        /// 檢測是否是正確的Url
        /// </summary>
        /// <param name="strUrl">要驗證的Url</param>
        /// <returns>判斷結果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 判斷是否為base64字符串
        /// </summary>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }

        /// <summary>
        /// 檢測是否有Sql危險字符
        /// </summary>
        /// <param name="str">要判斷字符串</param>
        /// <returns>判斷結果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 判斷是否為正確的時間格式
        /// </summary>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        /// <summary>
        /// 判斷是否為正確的IP格式
        /// </summary>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
