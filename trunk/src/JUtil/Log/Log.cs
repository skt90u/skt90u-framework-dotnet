using System;
using System.Diagnostics;
using System.Reflection;


namespace JUtil
{
    /// <summary>
    /// Log Utility
    /// </summary>
    public sealed class Log
    {
        static Log()
        {
            logTrace = true;
            logDebug = true;
            logInfo = true;
            logWarn = true;
            logError = true;
        }

        /// <summary>
        /// current log mode
        /// </summary>
        public static LoggingMode LoggingMode
        {
            get
            {
                return loggingMode;
            }
            set
            {
                loggingMode = value;
                logger = null;
            }
        }
        private static LoggingMode loggingMode = LoggingMode.SILog;
        
        /// <summary>
        /// get logger instance according to LoggingMode
        /// </summary>
        /// <param name="LoggingMode"></param>
        /// <returns></returns>
        private static ILog LogFactory(LoggingMode LoggingMode)
        {
            ILog logger = null;
            switch (LoggingMode)
            {
                case LoggingMode.None:
                    {
                        logger = new NoneLog();
                    } break;

                case LoggingMode.Nlog:
                    {
                        logger = new NLog();
                    } break;

                case LoggingMode.SILog:
                    {
                        // 在Service之中使用SILog必須確保，必須確保SmartInspect程式在Service之前執行

                        bool SmartInspectIsExist = SILog.IsLoggerExist;

                        if (SmartInspectIsExist)
                            logger = new SILog();
                        else
                            logger = new NLog();

                    } break;

                case LoggingMode.Console:
                    {
                        logger = new ConsoleLog();
                    } break;

                case LoggingMode.EventLog:
                    {
                        logger = new EventLog();
                    } break;

                case LoggingMode.FileLog:
                    {
                        logger = new FileLog();
                    } break;
                
                default: throw new Exception("unknown LoggingMode");
            }
            return logger;
        }

        private static ILog logger = null;
        private static ILog Logger
        {
            get
            {
                if (logger == null)
                    logger = LogFactory(LoggingMode);
                
                return logger;
            }
        }

        /// <summary>
        /// 用於開發階段偵錯使用, 必須將使用 Log.T(...) 的專案中 DEBUG FLAG 打開才會有作用
        /// </summary>
        /// <remarks>
        /// 必須將使用 Log.T(...) 的專案中 DEBUG FLAG 打開才會有作用
        /// 
        /// [屬性] -> [建置] -> [一般] : 定義 DEBUG 常數
        /// </remarks>
        [Conditional("DEBUG")]
        public static void T(string format, params object[] args)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string funcName = caller.Name;
            string className = caller.DeclaringType.FullName;
            string message = FormatDetailMessage(className, funcName, format, args);
            
            if (LogTrace) Logger.T(message);

            ExtLog.T(format, args);
        }

        /// <summary>
        /// output debug message
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void D(string format, params object[] args)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string funcName = caller.Name;
            string className = caller.DeclaringType.FullName;
            string message = FormatDetailMessage(className, funcName, format, args);

            if (LogDebug) Logger.D(message);
            
            ExtLog.D(format, args);
        }
        
        /// <summary>
        /// output information message
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void I(string format, params object[] args)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string funcName = caller.Name;
            string className = caller.DeclaringType.FullName;
            string message = FormatDetailMessage(className, funcName, format, args);

            if (LogInfo) Logger.I(message);

            ExtLog.I(format, args);
        }
        
        /// <summary>
        /// output warning message
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void W(string format, params object[] args)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string funcName = caller.Name;
            string className = caller.DeclaringType.FullName;
            string message = FormatDetailMessage(className, funcName, format, args);
            
            if (LogWarn) Logger.W(message);

            ExtLog.W(format, args);
        }
        
        /// <summary>
        /// output error message
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void E(string format, params object[] args)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string funcName = caller.Name;
            string className = caller.DeclaringType.FullName;
            string message = FormatDetailMessage(className, funcName, format, args);
            
            if (LogError) Logger.E(message);

            ExtLog.E(format, args);
        }

        /// <summary>
        /// output error message
        /// </summary>
        /// <param name="ex"></param>
        public static void E(Exception ex)
        {
            string message = FormatExceptionMessage(ex);

            if (LogError) Logger.E(message);

            ExtLog.E(ex.Message);
        }

        /// <summary>
        /// helper function which can get exception's information
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string FormatExceptionMessage(Exception ex)
        {
            string type = ex.GetType().Name;
            
            string message = string.Empty;
            if (ex.InnerException != null)
            {
                message = string.Format("{0}(InnerException: {1})", ex.Message, ex.InnerException.Message);
            }
            else
            {
                message = ex.Message;
            }

            string stackTrace = ex.StackTrace;

            message = string.Format("ExceptionType : {0}, Message : {1}, StackTrace: {2}", type, message, stackTrace);
            return message;
        }

        /// <summary>
        /// format string before output to log
        /// </summary>
        private static string FormatDetailMessage(string className,
                                                  string funcName, 
                                                  string format, params object[] args)
        {
            string message = (args == null || args.Length == 0) ? format : string.Format(format, args);

            message = string.Format("[{0} @ {1}] : {2}", funcName, className, message);
            return message;
        }
        /// <summary>
        /// log error and pop up Error MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public static void ReportError(string text, string caption)
        {
            ExtMessageBox.Error(text, caption);

            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string funcName = caller.Name;
            string className = caller.DeclaringType.FullName;
            string message = string.Format("{0}, {1}", caption, text);
            message = FormatDetailMessage(className, funcName, message);

            if (LogError) Logger.E(message);
        }

        /// <summary>
        /// log error and pop up Error MessageBox
        /// </summary>
        /// <param name="ex"></param>
        public static void ReportError(Exception ex)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string funcName = caller.Name;
            string className = caller.DeclaringType.FullName;
            string text = string.Empty;
            if (ex.InnerException != null)
            {
                text = string.Format("{0}(InnerException: {1})", ex.Message, ex.InnerException.Message);
            }
            else
            {
                text = ex.Message;
            }
            string caption = string.Format("[{0} @ {1}]", funcName, className);

            ExtMessageBox.Error(text, caption);

            string message = FormatExceptionMessage(ex);
            if (LogError) Logger.E(message);
        }

        /// <summary>
        /// log info and pop up Info MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public static void ReportInfo(string text, string caption)
        {
            ExtMessageBox.Info(text, caption);

            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            
            string funcName = caller.Name;
            string className = caller.DeclaringType.FullName;
            string message = string.Format("{0}, {1}", caption, text);
            message = FormatDetailMessage(className, funcName, message);
            if (LogInfo) Logger.I(message);
        }

#if false
        /// <summary>
        /// 讓Client端能夠重新設定ReportError的方式
        /// </summary>
        //public static ExternalMessageBox EMB
        //{
        //    private get { return _EMB; }
        //    set { _EMB = value; }
        //}
        /* SETUP DEFAULT REPORT STRATEGY */
        //private static ExternalMessageBox _EMB = new ExternalMessageBox(ExtMessageBox.Error, ExtMessageBox.Info); 
#endif

        private static bool logTrace;
        private static bool logDebug;
        private static bool logInfo;
        private static bool logWarn;
        private static bool logError;

        public static bool LogTrace
        {
            get { return logTrace; }
            set { logTrace = value; }
        }
        public static bool LogDebug
        {
            get { return logDebug; }
            set { logDebug = value; }
        }
        public static bool LogInfo
        {
            get { return logInfo; }
            set { logInfo = value; }
        }
        public static bool LogWarn
        {
            get { return logWarn; }
            set { logWarn = value; }
        }
        public static bool LogError
        {
            get { return logError; }
            set { logError = value; }
        }
    } // end of Log
}
