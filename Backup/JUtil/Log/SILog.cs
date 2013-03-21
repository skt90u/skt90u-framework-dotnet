using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Gurock.SmartInspect;

namespace JUtil
{
    internal sealed class SILog : ILog
    {
        static SILog()
        {
            SiAuto.Si.Enabled = true;

            MakeSureLoggerIsRunning();
        }

        private static void MakeSureLoggerIsRunning()
        {
            string lpClassName = "TMainForm";
            string lpWindowName = null;
            string fileName = @"C:\Program Files\Gurock Software\SmartInspect Professional\SmartInspectConsole.exe";

            if (!Process.Exists(lpClassName, lpWindowName))
            {
                if (File.Exists(fileName))
                {
                    Process.Run(fileName);

                    Process.WaitExists(lpClassName, lpWindowName);
                }
            }
        }

        public void T(string format, params object[] args)
        {
            SiAuto.Main.LogDebug(format, args);
        }

        public void D(string format, params object[] args)
        {
            SiAuto.Main.LogDebug(format, args);
        }

        public void I(string format, params object[] args)
        {
            SiAuto.Main.LogMessage(format, args);
        }

        public void W(string format, params object[] args)
        {
            SiAuto.Main.LogWarning(format, args);
        }

        public void E(string format, params object[] args)
        {
            SiAuto.Main.LogError(format, args);
        }


    } // end of SILog
}
