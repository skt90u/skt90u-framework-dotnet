using System.Web;
using System.Web.UI;

namespace JUtil.Web
{
    /// <summary>
    /// Browser Information helper class
    /// </summary>
    public class BrowserDetecter
    {
        /// <summary>
        /// determine browser whether IE 
        /// </summary>
        /// <returns></returns>
        public static bool IsIE()
        {
            return BrowserInfo.Browser.Equals("IE");
        }

        private static HttpBrowserCapabilities BrowserInfo
        {
            get { return CurPage.Request.Browser; }
        }

        private static Page CurPage
        {
            get { return (Page)HttpContext.Current.Handler; }
        }


    } // end of BrowserDetecter
}
