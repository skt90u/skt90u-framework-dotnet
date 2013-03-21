using System.IO;
using Gurock.SmartInspect;
using System;

namespace TinyJUtil
{
    public class SILog : ILog
    {
        public SILog()
        {
            // 在IIS中執行的WebApplication
            //  - 無法判斷SmartInspect Console是否啟動(因為FindWindow失效)
            //  - 透過IIS開啟程式，只能在背景執行
            // 
            // 因此遇到在IIS中執行的WebApplication，必須手動開啟SmartInspect
            if (!Environment.IsRunWithinIIS)
                MakeSureActivate();

            // Enable logging via TCP/IP to the Console
            SiAuto.Si.Connections = "tcp()";
            SiAuto.Si.Enabled = true;
        }

        /// <summary>
        /// 確保SmartInspect在寫入訊息前，已經啟動
        /// </summary>
        private void MakeSureActivate()
        {
            // 檢查SmartInspect Console是否已經啟動
            string lpClassName = "TMainForm";
            string lpWindowName = null;
            if (Process.Exists(lpClassName, lpWindowName))
                return;

            // 使用SmartInspect Console預設路徑啟動
            string exePath = @"C:\Program Files\Gurock Software\SmartInspect Professional\SmartInspectConsole.exe";
            if (!File.Exists(exePath))
            {
                string error = string.Format("無法啟動SmartInspect，可能尚未安裝，或者安裝路徑不在{0}。", exePath);
                throw new FileNotFoundException(error);
            }

            Process.Run(exePath);

            Process.WaitExists(lpClassName, lpWindowName);

            // 必須延長等待時間，否則Log無法輸出到SmartInspectConsole
            int delay = 2000;
            System.Threading.Thread.Sleep(delay);
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
