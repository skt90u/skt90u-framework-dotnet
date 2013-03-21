using System;

namespace JUtil.Extensions
{
    public static class ExtDateTime
    {
        public static string ToYYYYMMdd(this DateTime source)
        {
            return string.Format("{0:yyyy/MM/dd}", source);
        }


    } // end of ExtDateTime
}
