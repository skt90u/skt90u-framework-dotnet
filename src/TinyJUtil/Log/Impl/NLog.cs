using System.IO;
using NLog;
using TinyJUtil.Extensions;
using TinyJUtil.ResourceManagement;

namespace TinyJUtil
{
    #region [How Do I] Use NLog
    // [NLog for WinForm]
    // (1) 加入-> 新增項目 -> NLog -> Typical NLog Configuration
    // (2) NLog.config -> 右鍵 -> 屬性 -> 複製到輸出目錄 -> 永遠複製
    // (3) NLog.config 設定方式請參考 : http://nlog-project.org/wiki/Configuration_file#Targets
    #endregion

    public class NLog : ILog
    {
        private readonly Logger logger = null;
        
        public NLog()
        {
            // 確保設定檔存在，如果不存在就建立一個設定檔
            MakeSureConfigExist();

            logger = LogManager.GetLogger("");
        }

        private void MakeSureConfigExist()
        {
            string configPath = ExtPath.GetAbsolutePath("NLog.config");

            // 如果設定檔不存在，就從內嵌資源中生出一個設定檔
            if (!File.Exists(configPath))
            {
                // 選擇適當的內嵌資源的檔名
                string resourceFilename = Environment.IsWebApplication ? "AspNet-NLog.config" : "NLog.config";

                // 將檔名轉成對應的ResourceId
                string ResourceId = ResourcesLoader.GetResourceId(GetType().Assembly, resourceFilename);

                ResourcesLoader rl = new ResourcesLoader(GetType().Assembly);

                // 根據ResourceId取得內嵌資源，並存到configPath
                rl[ResourceId].SaveAs(configPath);
            }
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
