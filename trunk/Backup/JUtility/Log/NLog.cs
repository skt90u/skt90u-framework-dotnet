using NLog;

namespace JFramework
{
    #region How to use NLog
    // [NLog for WinForm]
    // (1) 加入-> 新增項目 -> NLog -> Typical NLog Configuration
    // (2) NLog.config -> 右鍵 -> 屬性 -> 複製到輸出目錄 -> 永遠複製
    // (3) NLog.config 設定方式請參考 : http://nlog-project.org/wiki/Configuration_file#Targets
    #endregion

    class NLog : ILog
    {
        //private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Logger logger = LogManager.GetLogger("");

        public void D(string format, params object[] args)
        {
            logger.Debug(format, args);
        }

        public void I(string format, params object[] args)
        {
            logger.Info(format, args);
        }

        public void W(string format, params object[] args)
        {
            logger.Warn(format, args);
        }

        public void E(string format, params object[] args)
        {
            logger.Error(format, args);
        }
    }
}
