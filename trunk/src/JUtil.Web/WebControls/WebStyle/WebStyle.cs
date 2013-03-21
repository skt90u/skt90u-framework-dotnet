using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// 提供JUtil.Web相關JavaScript會用到的CSS
    /// </summary>
    public class WebStyle : CompositeControl
    {
        #region OnPreRender
        /// <remarks>在此註冊css, javascript</remarks>
        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            Type type = this.GetType();

            ClientScriptManager.RegisterEmbeddedCSS(type, "JUtil.Web.WebControls.WebStyle.jAlert.jquery.alerts.css");
        }
        #endregion
    }
}
