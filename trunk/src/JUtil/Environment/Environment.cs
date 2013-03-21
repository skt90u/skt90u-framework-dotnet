using System;
using System.Diagnostics;
using System.Web;
using System.Text;
using System.Web.Hosting;
using System.Reflection;

namespace JUtil
{
    /// <summary>
    /// JUtil support platform
    /// </summary>
    public enum ApplicationType
    {
        /// <summary>
        /// Console
        /// </summary>
        ConsoleApplication = 0,

        /// <summary>
        /// WindowsForm
        /// </summary>
        WindowsFormsApplication = 1,

        /// <summary>
        /// Wpf
        /// </summary>
        WpfApplication = 2,

        /// <summary>
        /// AspNet
        /// </summary>
        WebApplication = 3,

        /// <summary>
        /// Service
        /// </summary>
        Service = 4
    }

    /*
    /// <summary>
    /// JUtil Environment 
    /// </summary>
    public static class Environment
    {
        /// <summary>
        /// current application type
        /// </summary>
        public static readonly ApplicationType ApplicationType;

        static Environment()
        {
            ApplicationType = getApplicationType();
        }

        #region getApplicationType

        /// <summary>
        /// get current application type
        /// </summary>
        /// <returns></returns>
        private static ApplicationType getApplicationType()
        {
            if (IsConsoleApplication)
                return ApplicationType.ConsoleApplication;

            if (IsWebApplication)
                return ApplicationType.WebApplication;

            if (IsWpfApplication)
                return ApplicationType.WpfApplication;

            return ApplicationType.WindowsFormsApplication;
        }

        #region IsConsoleApplication

        /// <summary>
        /// determine whether current application is ConsoleApplication
        /// </summary>
        private static bool IsConsoleApplication
        {
            get
            {
                //
                // http://bytes.com/topic/visual-basic-net/answers/546492-can-we-determine-runtime-if-app-console-windows
                //
                try
                {
                    string title = Console.Title;

                    // Window Server 2008 R2 在IIS 6.0 會丟出空字串
                    if (string.IsNullOrEmpty(title))
                        return false;

                    // A publish web application has Console.Title, like following format
                    // C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\aspnet_wp.exe
                    if (IsRunWithinIIS(title))
                        return false;

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion
        #region IsWebApplication

        /// <summary>
        /// determine whether current application is WebApplication
        /// </summary>
        private static bool IsWebApplication
        {
            get
            {
                try
                {
                    //
                    // check whether HttpContext.Current is null. 
                    // If it is, it's not a webapp
                    //
                    // http://stackoverflow.com/questions/2956629/determine-if-app-is-winforms-or-webforms

                    bool WebIsDeveloping = (null != HttpContext.Current);

                    //bool WebIsDeveloping = (null != HttpRuntime.Cache);

                    bool WebIsPublish = IsRunWithinIIS(System.Diagnostics.Process.GetCurrentProcess().ProcessName);

                    return WebIsDeveloping || WebIsPublish;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private static bool IsRunWithinIIS(string ProcessNameOrPath)
        {
            string[] arrIIS = {
                            // IIS 5.1
                            "aspnet_wp",

                            // IIS 6.1
                            "w3wp"
                           };

            foreach (string IIS in arrIIS)
            {
                if (ProcessNameOrPath.Contains(IIS))
                    return true;
            }

            return false;
        }

        #endregion
        #region IsWpfApplication

        /// <summary>
        /// determine whether current application is WpfApplication
        /// </summary>
        private static bool IsWpfApplication
        {
            get
            {
                try
                {
                    //
                    // http://social.msdn.microsoft.com/Forums/en-US/vbgeneral/thread/f9125a87-ff46-4a1b-abcb-9018921355f7/
                    //
                    StackTrace st = new StackTrace();
                    foreach (StackFrame sf in st.GetFrames())
                    {
                        string assemblyName = sf.GetMethod().DeclaringType.Assembly.GetName().Name;
                        string typeName = sf.GetMethod().DeclaringType.FullName;
                        string methodName = sf.GetMethod().Name;

                        string log = string.Format("assemblyName:{0}, typeName:{1}, methodName:{2}\n",
                            assemblyName,
                            typeName,
                            methodName);

                        bool WpfIsInXP = (assemblyName == "PresentationFramework" && typeName == "System.Windows.Application" && methodName == "Run");

                        //
                        // Wpf判斷在Window 2008 使用以上邏輯會出錯，使用CompareIt比對後，發現差異如下
                        //
                        bool WpfIsInWin2008 = (assemblyName == "WindowsBase" && typeName == "System.Windows.Threading.Dispatcher" && methodName == "TranslateAndDispatchMessage");

                        if (WpfIsInXP || WpfIsInWin2008)
                            return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion

        #endregion


    } // end of Environment
    */
    public static class Environment
    {
        #region IsRunWithinWebServer
        /// <summary>
        /// 判斷目前程式的執行環境是否以WebServer(Visual Studio Asp.Net開發環境)執行
        /// </summary>
        public static bool IsRunWithinWebServer
        {
            get
            {
                string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

                string[] arrWebServer = 
                {
                    // Visual Studio 2008
                    "WebDev.WebServer"
                };

                foreach (string WebServer in arrWebServer)
                    if (processName.Contains(WebServer))
                        return true;

                return false;
            }
        }
        #endregion
        #region IsRunWithinIIS
        /// <summary>
        /// 判斷目前程式的執行環境是否以IIS執行
        /// </summary>
        public static bool IsRunWithinIIS
        {
            get
            {
                string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

                string[] arrIIS = 
                {
                    // IIS 5.1
                    "aspnet_wp",

                    // IIS 6.1
                    "w3wp"
                };

                foreach (string IIS in arrIIS)
                    if (processName.Contains(IIS))
                        return true;

                return false;
            }
        }
        #endregion

        #region IsConsoleApplication
        /// <summary>
        /// 判斷目前程式的執行環境是否為Console
        /// </summary>
        /// <remarks>
        /// http://bytes.com/topic/visual-basic-net/answers/546492-can-we-determine-runtime-if-app-console-windows
        /// </remarks>
        public static bool IsConsoleApplication
        {
            get
            {
                try
                {
                    string title = Console.Title;

                    // WebApplication在IIS 6.0(Window Server 2008 R2)會丟出空字串，而非拋出例外
                    if (string.IsNullOrEmpty(title))
                        return false;

                    // WebApplication在IIS 5.0(WindowXP)會將Console.Title設定成以下字串
                    // C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\aspnet_wp.exe
                    // 
                    // 因此我們還需額外判斷目前應用程式是否在IIS中執行
                    if (IsRunWithinIIS)
                        return false;

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion
        #region IsWebApplication
        /// <summary>
        /// 判斷目前程式是否為WebApplication
        /// </summary>
        public static bool IsWebApplication
        {
            get
            {
                try
                {
                    //
                    // check whether HttpContext.Current is null. 
                    // If it is, it's not a webapp
                    //
                    // http://stackoverflow.com/questions/2956629/determine-if-app-is-winforms-or-webforms

                    bool DevelopingWeb = (null != HttpContext.Current);

                    bool PublishWeb = IsRunWithinIIS;

                    return DevelopingWeb || PublishWeb;

                    // 又或者可以用以下方是判斷(PS:尚未測試過)
                    //return IsRunWithinIIS || IsRunWithinWebServer;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion
        #region IsWpfApplication
        /// <summary>
        /// 判斷目前程式是否為WpfApplication
        /// </summary>
        public static bool IsWpfApplication
        {
            get
            {
                try
                {
                    //
                    // http://social.msdn.microsoft.com/Forums/en-US/vbgeneral/thread/f9125a87-ff46-4a1b-abcb-9018921355f7/
                    //
                    StackTrace st = new StackTrace();
                    foreach (StackFrame sf in st.GetFrames())
                    {
                        string assemblyName = sf.GetMethod().DeclaringType.Assembly.GetName().Name;
                        string typeName = sf.GetMethod().DeclaringType.FullName;
                        string methodName = sf.GetMethod().Name;

                        string log = string.Format("assemblyName:{0}, typeName:{1}, methodName:{2}\n",
                            assemblyName,
                            typeName,
                            methodName);

                        bool WpfIsInXP = (assemblyName == "PresentationFramework" && typeName == "System.Windows.Application" && methodName == "Run");

                        //
                        // Wpf判斷在Window 2008 使用以上邏輯會出錯，使用CompareIt比對後，發現差異如下
                        //
                        bool WpfIsInWin2008 = (assemblyName == "WindowsBase" && typeName == "System.Windows.Threading.Dispatcher" && methodName == "TranslateAndDispatchMessage");

                        if (WpfIsInXP || WpfIsInWin2008)
                            return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion
        #region IsWindowsFormsApplication
        public static bool IsWindowsFormsApplication
        {
            get
            {
                if (IsService || IsConsoleApplication || IsWebApplication || IsWpfApplication)
                    return false;

                return true;
            }
        }
        #endregion

        #region IsService
        /// <summary>
        /// 判斷目前程式是否為Service
        /// </summary>
        public static bool IsService
        {
            get
            {
                try
                {
                    System.Diagnostics.Process curProcess = System.Diagnostics.Process.GetCurrentProcess();

                    System.Diagnostics.Process parentProcess = curProcess.GetParent();

                    return parentProcess.MainModule.ModuleName == "services.exe";
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion


        #region ApplicationType
        /// <summary>
        /// get current application type
        /// </summary>
        /// <returns></returns>
        public static ApplicationType ApplicationType
        {
            get
            {
                if (IsService)
                    return ApplicationType.Service;

                if (IsConsoleApplication)
                    return ApplicationType.ConsoleApplication;

                if (IsWebApplication)
                    return ApplicationType.WebApplication;

                if (IsWpfApplication)
                    return ApplicationType.WpfApplication;

                return ApplicationType.WindowsFormsApplication;
            }
        }
        #endregion
        #region ApplicationDirectory
        /// <summary>
        /// 取得目前程式所在目錄
        /// </summary>
        public static string ApplicationDirectory
        {
            get
            {
                if (IsWebApplication)
                {
                    return HostingEnvironment.MapPath("~/");
                }
                else
                {
                    // WPF (How to get executable path)
                    // http://sne04.blogspot.com/2008/12/wpf-how-to-get-executable-path.html
                    // 雖然會得到的是目前 JUtil.dll 的路徑, 但是他會和執行檔放置在同一各目錄, 
                    // 因此與我們預期的結果相同
                    string path = Assembly.GetExecutingAssembly().Location;

                    int pos = path.LastIndexOf('\\');
                    if (pos != -1)
                        path = path.Substring(0, pos);

                    return path;
                }
            }
        }
        #endregion

        public static bool HasPrepared {get; set;}

    } // end of Environment
}
