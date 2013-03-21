using System;
using System.Globalization;

namespace JUtil.Extensions
{
    /// <summary>
    /// Extensions of DateTime
    /// </summary>
    public static class ExtDateTime
    {
        /// <summary>
        /// ParseDateTime
        /// </summary>
        /// <remarks>
        /// reference : http://www.cnblogs.com/kirinboy/archive/2009/06/04/1496258.html
        /// </remarks>
        public static DateTime Parse(string strDateTime)
        {
            DateTime output = DateTime.MinValue;

            string[] DateTimeParseFormats = 
            {
                "yyyy/MM/dd HH:mm:ss",
                "yyyy/MM/dd"
            };

            foreach (string format in DateTimeParseFormats)
            {
                DateTime.TryParseExact(strDateTime, format, null, DateTimeStyles.None, out output);

                if (output != DateTime.MinValue)
                    break;
            }

            if (output == DateTime.MinValue)
            {
                string error = string.Format("字串 {0} 未被辨認為有效的 DateTime", strDateTime);
                throw new FormatException(error);
            }

            return output;
        }

        /// <summary>
        /// 判斷兩時間是否屬於同一天
        /// </summary>
        public static bool IsSameDate(this DateTime self, DateTime another)
        {
            return (self.Year == another.Year) &&
                   (self.Month == another.Month) &&
                   (self.Day == another.Day);
        }

        /// <summary>
        /// 取得指定日期當天第一秒時間
        /// </summary>
        public static DateTime ToDateBegin(this DateTime self)
        {
            int year = self.Year;
            int month = self.Month;
            int day = self.Day;
            int hour = 0;
            int minute = 0;
            int second = 0;
            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 取得指定日期當天最後一秒時間
        /// </summary>
        public static DateTime ToDateEnd(this DateTime self)
        {
            int year = self.Year;
            int month = self.Month;
            int day = self.Day;
            int hour = 23;
            int minute = 59;
            int second = 59;
            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 將 DateTime 轉成 yyyy/MM/dd 字串
        /// </summary>
        /// <param name="self">a DateTime object</param>
        /// <returns></returns>
        public static string ToYYYYMMdd(this DateTime self)
        {
            return string.Format("{0:yyyy/MM/dd}", self);
        }

        /// <summary>
        /// 將 DateTime 轉成 yyyy/MM/dd HH:mm:ss 字串
        /// </summary>
        /// <param name="self">a DateTime object</param>
        /// <returns></returns>
        public static string ToYYYYMMddHHmmss(this DateTime self)
        {
            return string.Format("{0:yyyy/MM/dd HH:mm:ss}", self);
        }

        /// <summary>
        /// Convert DateTime to "NULL" or TO_DATE(..., "")
        /// </summary>
        public static string ToSqlField(this DateTime self)
        {
            string sqlField = string.Empty;

            if (self.IsNull())
            {
                sqlField = "NULL";
            }
            else
            {
                sqlField = "TO_DATE('{0}', 'YYYY/MM/DD HH24:MI:SS')";
                sqlField = string.Format(sqlField, self.ToYYYYMMddHHmmss());
            }

            return sqlField;
        }

        private static DateTime NullDate = Convert.ToDateTime(null);

        /// <summary>
        /// 判斷是否為Null DateTime
        /// </summary>
        public static bool IsNull(this DateTime self)
        {
            return NullDate.Equals(self);
        }

    } // end of ExtDateTime
}
