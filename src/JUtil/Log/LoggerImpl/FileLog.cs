using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JUtil
{
    /// <remarks>
    /// 在WindowService上使用，必須具備LocalSystem的權限
    /// </remarks>>
    internal sealed class FileLog : ILog
    {
        #region ILog 成員

        private string LogPath;

        public FileLog()
        {
            LogPath = Path.File.GetAbsolutePath(Path.Directory.Application, "JUtil.log");
        }

        private static object _LockObject = null;
        private static object LockObject
        {
            get
            {
                if (_LockObject == null)
                {
                    _LockObject = new object();
                }
                return _LockObject;
            }
        }

        enum LogState
        {
            T = 0,
            D = 1,
            I = 2,
            W = 3,
            E = 4
        }

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
            lock (LockObject)
            {
                DateTime now = DateTime.Now;

                string message = args.Length == 0 ? format : string.Format(format, args);

                string output = string.Format("[{0}], [{1}] : {2}", now.ToString(), state.ToString(), message);

                WriteLog(output);
            }
        }

        private void WriteLog(string data)
        {
            try
            {
                StreamWriter _sw = new StreamWriter(LogPath, true);
                _sw.WriteLine(data);
                _sw.Flush();
                _sw.Close();
            }
            catch { }
        }

    } // end of FileLog
}
