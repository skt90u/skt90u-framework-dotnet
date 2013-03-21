using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUtil
{
    /// <summary>
    /// ReportError
    /// </summary>
    public delegate void ReportErrorFunc(string text, string caption);

    /// <summary>
    /// ReportError
    /// </summary>
    public delegate void ReportInfoFunc(string text, string caption);

    /// <summary>
    /// 提供外部註冊的訊息回報方式
    /// </summary>
    public class ExternalMessageBox : IExtMessageBox
    {
        private ReportErrorFunc ReportError;
        private ReportInfoFunc ReportInfo;

        /// <summary>
        /// 
        /// </summary>
        public ExternalMessageBox(ReportErrorFunc ReportError, ReportInfoFunc ReportInfo)
        {
            this.ReportError = ReportError;
            this.ReportInfo = ReportInfo;
        }

        #region IExtMessageBox 成員

        /// <summary>
        /// Implement Error  
        /// </summary>
        public void Error(string text, string caption)
        {
            try
            {
                ReportError(text, caption);
            }
            catch (Exception) { /*必須確保不丟出例外*/}
        }

        /// <summary>
        /// Implement Info 
        /// </summary>
        public void Info(string text, string caption)
        {
            try
            {
                ReportInfo(text, caption);
            }
            catch (Exception) { /*必須確保不丟出例外*/}
        }

        #endregion
    }
}
