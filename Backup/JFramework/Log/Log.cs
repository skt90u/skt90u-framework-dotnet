using System;
using System.Diagnostics;
using System.Reflection;


namespace JFramework
{
    public class Log
    {
        enum LogMode
        {
            Nlog = 0,
            SILog = 1
        }

        private static LogMode mode = LogMode.SILog;

        private static ILog LogFactory(LogMode mode)
        {
            ILog logger = null;
            switch (mode)
            {
                case LogMode.Nlog:
                    {
                        logger = new JFramework.NLog();
                    } break;

                case LogMode.SILog:
                    {
                        logger = new JFramework.SILog();
                    } break;

                default: throw new Exception("unknown LogMode");
            }
            return logger;
        }

        private static ILog logger = null;
        private static ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = LogFactory(mode);
                }
                return logger;
            }
        }
        
        public static void D(string format, params object[] args)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string function = caller.DeclaringType.FullName;
            string module = caller.Module.Name;
            string message = FormatDetailMessage(module, function, format, args);
            Logger.D(message);
        }
        
        public static void I(string format, params object[] args)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string function = caller.DeclaringType.FullName;
            string module = caller.Module.Name;
            string message = FormatDetailMessage(module, function, format, args);
            Logger.I(message);
        }
        
        public static void W(string format, params object[] args)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string function = caller.DeclaringType.FullName;
            string module = caller.Module.Name;
            string message = FormatDetailMessage(module, function, format, args);
            Logger.W(message);
        }
        
        public static void E(string format, params object[] args)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string function = caller.DeclaringType.FullName;
            string module = caller.Module.Name;
            string message = FormatDetailMessage(module, function, format, args);
            Logger.E(message);
        }

        public static void E(Exception ex)
        {
            string message = FormatExceptionMessage(ex);
            Logger.E(message);
        }

        private static string FormatExceptionMessage(Exception ex)
        {
            string type = ex.GetType().Name;
            string message = ex.Message;
            string stackTrace = ex.StackTrace;
            message = string.Format("Exception : {0}, Message : {1}, StackTrace: {2}", type, message, stackTrace);
            return message;
        }

        private static string FormatDetailMessage(string module, string function, string format, params object[] args)
        {
            string message = string.Format(format, args);
            message = string.Format("[{0} @ {1}] : {2}", function, module, message);
            return message;
        }

        public static void ReportError(string text, string caption)
        {
            MessageBox.Error(text, caption);

            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string function = caller.DeclaringType.FullName;
            string module = caller.Module.Name;
            string message = string.Format("caption : {0}, text : {1}", caption, text);
            
            message = FormatDetailMessage(module, function, message);
            Logger.E(message);
        }

        public static void ReportError(Exception ex)
        {
            MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
            string function = caller.DeclaringType.FullName;
            string text = ex.Message;
            string caption = function;
            MessageBox.Error(text, caption);

            string message = FormatExceptionMessage(ex);
            Logger.E(message);
        }
    }
}
