using System;
using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    //
    // Extension System.Windows.Forms.DateTimePicker
    //
    public static class ExtDateTimePicker
    {
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
