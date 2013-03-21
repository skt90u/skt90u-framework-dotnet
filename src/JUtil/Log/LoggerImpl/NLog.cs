using NLog;
using JUtil.ResourceManagement;
using JUtil.Extensions;
using System;
using System.Reflection;

namespace JUtil
{
    #region [How Do I] Use NLog
    // [NLog for WinForm]
    // (1) 加入-> 新增項目 -> NLog -> Typical NLog Configuration
    // (2) NLog.config -> 右鍵 -> 屬性 -> 複製到輸出目錄 -> 永遠複製
    // (3) NLog.config 設定方式請參考 : http://nlog-project.org/wiki/Configuration_file#Targets
    #endregion

    /// <remarks>
    /// 在WindowService上使用，必須具備LocalSystem的權限
    /// </remarks>>
    internal sealed class NLog : ILog
    {
        private Logger logger;

        public NLog()
        {
            MakeSureConfigExist();

            logger = LogManager.GetLogger("");
        }

        /// <summary>
        /// if nlog.config is not exist, create it
        /// </summary>
        private void MakeSureConfigExist()
        {
            string fileName = "NLog.config";
            string configPath = Path.File.GetAbsolutePath(Path.Directory.Application, fileName);

            if (!JUtil.Path.File.Exists(configPath))
            {
                bool isWebApp = JUtil.Environment.ApplicationType == ApplicationType.WebApplication;

                string resourceFilename = isWebApp ? "AspNet-NLog.config" : "NLog.config";

                string ResourceId = ResourcesLoader.LookupResourceId(GetType().Assembly, resourceFilename);

                ResourceManager.LocalResource[ResourceId].SaveAs(configPath);
            }

        }

        public void T(string format, params object[] args)
        {
            logger.Trace(format, args);
        }

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


    } // end of NLog
}
