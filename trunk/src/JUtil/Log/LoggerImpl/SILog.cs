using System.IO;
using Gurock.SmartInspect;

namespace JUtil
{
    internal sealed class SILog : ILog
    {
        private const string SmartInspectPath = @"C:\Program Files\Gurock Software\SmartInspect Professional\SmartInspectConsole.exe";
        
        public static bool IsLoggerExist
        {
            get{ return System.IO.File.Exists(SmartInspectPath); }
        }

        private void MakeSureLoggerIsRunning()
        {
            // 如果執行環境是Service，
            // 無法判斷SmartInspectConsole.exe是否啟動
            // 也無法啟動SmartInspectConsole.exe(好像可以啟動，但是在背景執行)

            // 必須確保程式在Service之前執行
            if (Environment.IsService)
                return;

            string lpClassName = "TMainForm";
            string lpWindowName = null;

            if (!Process.Exists(lpClassName, lpWindowName))
            {
                if (File.Exists(SmartInspectPath))
                {
                    Process.Run(SmartInspectPath);

                    Process.WaitExists(lpClassName, lpWindowName);

                    // 必須延長等待時間，否則Log無法輸出到SmartInspectConsole
                    int delay = 2000;
                    System.Threading.Thread.Sleep(delay);
                }
            }
        }

        public SILog()
        {
            SiAuto.Si.Enabled = true;

            //SiAuto.Si.Connections = "text(filename=log.txt, append=true)";
            //SiAuto.Si.Connections = @"file(filename=log.sil)";
            //SiAuto.Si.Connections = @"tcp(host=192.168.166.16)";
            //SiAuto.Si.Connections = "pipe()";

            MakeSureLoggerIsRunning();
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
