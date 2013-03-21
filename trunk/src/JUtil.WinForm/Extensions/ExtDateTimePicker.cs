using System;
using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    /// <summary>
    /// Extension System.Windows.Forms.DateTimePicker
    /// </summary>
    public static class ExtDateTimePicker
    {
        /// <summary>
        /// Convert DateTimePicker to ToDate
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime ToDate(this DateTimePicker self)
        {
            DateTime dt = new DateTime(
                self.Value.Year, 
                self.Value.Month,
                self.Value.Day);
            return dt;
        }


    } // end of ExtDateTimePicker
}
