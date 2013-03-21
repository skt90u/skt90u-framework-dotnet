using System.Collections.Specialized;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI;

namespace JUtil.Web
{
    /// <summary>
    /// support StateManagementType in AspNet
    /// </summary>
    public enum StateManagementMode
    {
        /// <summary>
        /// StateManagementMode.Application
        /// </summary>
        Application = 0,

        /// <summary>
        /// StateManagementMode.Session
        /// </summary>
        Session = 1,

        /// <summary>
        /// StateManagementMode.Cache
        /// </summary>
        Cache = 2,

        /// <summary>
        /// StateManagementMode.Cookies
        /// </summary>
        Cookies = 3,

        /// <summary>
        /// 用於取得Get值
        /// </summary>
        QueryString = 4,

        /// <summary>
        /// 用於取得Post值
        /// </summary>
        Form = 5
    }

    /// <summary>
    /// support StateManagement operations in AspNet
    /// </summary>
    public static class StateManagement
    {
        /// <summary>
        /// Application variable
        /// </summary>
        public static HttpApplicationState Application { get { return HttpContext.Current.Application; } }

        /// <summary>
        /// Session variable
        /// </summary>
        public static HttpSessionState Session { get { return HttpContext.Current.Session; } }

        /// <summary>
        /// Cache variable
        /// </summary>
        public static Cache Cache { get { return HttpContext.Current.Cache; } }

        /// <summary>
        /// Cookies variable
        /// </summary>
        public static HttpCookieCollection Cookies { get { return HttpContext.Current.Response.Cookies; } }

        /// <summary>
        /// QueryString variable
        /// </summary>
        public static NameValueCollection QueryString { get { return curPage.Request.QueryString; } }

        /// <summary>
        /// Form variable
        /// </summary>
        public static NameValueCollection Form { get { return curPage.Request.Form; } }

        /// <summary>
        /// Remove variable by StateManagementMode and key
        /// </summary>
        public static void Remove(StateManagementMode mode, string key)
        {
            switch (mode)
            {
                case StateManagementMode.Application: { StateManagement.Application.Remove(key); break; }

                case StateManagementMode.Session: { StateManagement.Session.Remove(key); break; }

                case StateManagementMode.Cache: { StateManagement.Cache.Remove(key); break; }

                case StateManagementMode.Cookies: { StateManagement.Cookies.Remove(key); break; }

                case StateManagementMode.QueryString: { StateManagement.QueryString.Remove(key); break; }

                case StateManagementMode.Form: { StateManagement.Form.Remove(key); break; }
            }

            // 使用 dynamic
            //Type curType = typeof(StateManagement);
            //string propertyName = mode.ToString();
            //BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Static;
            //PropertyInfo propertyInfo = curType.GetProperty(propertyName, bindingAttr);
            //dynamic property = propertyInfo.GetValue(curType, null);
            //property.Remove(key);
        }

        /// <summary>
        /// get current page instance
        /// </summary>
        private static Page curPage
        {
            get { return (Page)HttpContext.Current.Handler; }
        }

    } // end of StateManagement
}
