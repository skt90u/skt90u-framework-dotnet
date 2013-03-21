using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BvWeb.Bv;
using JUtil.Path;

namespace JUtil
{
    internal sealed class BvLog : ILog
    {
        static BvLog()
        {
            MakeSureLoggerIsRunning();
        }

        private static void MakeSureLoggerIsRunning()
        {
            string lpClassName = "TFormDebugWin.UnicodeClass";
            string lpWindowName = null;
            string fileName = @"D:\DebugWin.exe";

            if (!Process.Exists(lpClassName, lpWindowName))
            {
                if (File.Exists(fileName))
                {
                    Process.Run(fileName);

                    Process.WaitExists(lpClassName, lpWindowName);

                    // 必須延長等待時間，否則Log無法輸出到SmartInspectConsole
                    int delay = 2000;
                    System.Threading.Thread.Sleep(delay);
                }
            }
        }

        #region ILog 成員

        public void T(string format, params object[] args)
        {
            string message = string.Format(format, args);
            BvUtil.logDebug(message);
        }

        public void D(string format, params object[] args)
        {
            string message = string.Format(format, args);
            BvUtil.logDebug(message);
        }

        public void I(string format, params object[] args)
        {
            string message = string.Format(format, args);
            BvUtil.logInfo(message);
        }

        public void W(string format, params object[] args)
        {
            string message = string.Format(format, args);
            BvUtil.logInfo(message);
        }

        public void E(string format, params object[] args)
        {
            string message = string.Format(format, args);
            BvUtil.logErr(message);
        }

        #endregion
    } // end of BvLog
}
