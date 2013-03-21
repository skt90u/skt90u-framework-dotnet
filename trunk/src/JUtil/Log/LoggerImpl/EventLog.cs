using System;
using NativeApi = System.Diagnostics;

namespace JUtil
{
    internal sealed class EventLog : ILog
    {
        private const int eventID = 9999;

        private static string processName;
        private static string ProcessName
        {
            get
            {
                if (processName == null)
                {
                    processName = NativeApi.Process.GetCurrentProcess().ProcessName;

                    processName = processName.Replace(".vshost", "");

                    if (!NativeApi.EventLog.SourceExists(processName))
                        NativeApi.EventLog.CreateEventSource(processName, "Application");
                }
                return processName;
            }
        }

        private static void SetLogPermission()
        {
            string machineName = string.Empty;
            NativeApi.EventLogPermissionAccess account = NativeApi.EventLogPermissionAccess.Administer;
            NativeApi.EventLogPermission logPermission = new NativeApi.EventLogPermission(account, "");
            logPermission.PermitOnly();
        }

        enum LogState
        {
            T = NativeApi.EventLogEntryType.Information,
            D = NativeApi.EventLogEntryType.Information,
            I = NativeApi.EventLogEntryType.Information,
            W = NativeApi.EventLogEntryType.Warning,
            E = NativeApi.EventLogEntryType.Error
        }


        #region ILog 成員

        public void T(string format, params object[] args)
        {
            Output(LogState.T, format, args);
        }

        public void D(string format, params object[] args)
        {
            Output(LogState.D, format, args);
        }

        public void I(string format, params object[] args)
        {
            Output(LogState.I, format, args);
        }

        public void W(string format, params object[] args)
        {
            Output(LogState.W, format, args);
        }

        public void E(string format, params object[] args)
        {
            Output(LogState.E, format, args);
        }
        
        #endregion

        private void Output(LogState state, string format, params object[] args)
        {
            string message = args.Length == 0 ? format : string.Format(format, args);
            
            NativeApi.EventLog.WriteEntry(ProcessName, message, (NativeApi.EventLogEntryType)state, eventID);
        }

    } // end of ConsoleLog
}

