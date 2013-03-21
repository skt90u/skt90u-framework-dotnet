using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace JUtil
{
    /// <summary>
    /// an implementation of IExtMessageBox interface in AspNet platform
    /// </summary>
    /// <remarks>
    /// Requirement : 
    ///     - 使用using JUtil.Web.Extensions;
    ///     
    ///     - 在Page_Load中加入this.LoadRequires()
    ///       相當於做了以下兩件事情[註解一][註解二]
    /// </remarks>
    /*
     *  [註解一]
     *     <asp:ScriptManager ID="SM" runat="server">
     *       <Scripts>
     *         <asp:ScriptReference Assembly="JUtil.Web" Name="JUtil.Web.JavaScript.jutil-all.js" />
     *       </Scripts>
     *     </asp:ScriptManager>
     */
    /*
     *   [註解二]
     *     <%@ Register Assembly="JUtil.Web" Namespace="JUtil.Web.WebControls" TagPrefix="cc1" %>
     *     <cc1:WebStyle ID="JWebStyle" runat="server" />
     */
    public class WebMessageBox : IExtMessageBox
    {
        #region IExtMessageBox 成員

        private enum eOutputMode
        {
            Error,
            Info
        }
        /// <summary>
        /// show error MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public void Error(string text, string caption)
        {
            if(!Environment.HasPrepared)
                throw new Exception("尚未載入相關javascript以及css, 請(1)using JUtil.Web.Extensions, (2)在Page_Load中加入this.LoadRequires()");

            Output(eOutputMode.Error, text, caption);
        }

        /// <summary>
        /// show info MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public void Info(string text, string caption)
        {
            if (!Environment.HasPrepared)
                throw new Exception("尚未載入相關javascript以及css, 請(1)using JUtil.Web.Extensions, (2)在Page_Load中加入this.LoadRequires()");

            Output(eOutputMode.Info, text, caption);
        }

        private void Output(eOutputMode outputMode, string text, string caption)
        {
            string key = GetScriptKey();

            string script = GetInlineScript(outputMode, key, text, caption);

            RegisterStartupScript(CurPage, key, script);
        }

        private static string GetScriptKey()
        {
            string ScriptKeyId = Guid.NewGuid().ToString();
            ScriptKeyId = ScriptKeyId.Replace("-", "_");

            string key = string.Format("InlineScript_{0}", ScriptKeyId);

            return key;
        }
        
        private static ScriptManager CurSM
        {
            get { return ScriptManager.GetCurrent(CurPage); }
        }

        private static void RegisterStartupScript(System.Web.UI.Control control, string key, string scripts)
        {
            ScriptManager sm = CurSM;
            // 即使用ToolkitScriptManager代替ScriptManager, 
            // 也可使用ScriptManager.GetCurrent判斷()
            if (sm != null)
            {
                ScriptManager.RegisterStartupScript(control, control.GetType(), key, scripts.ToString(), true);
            }
            else
            {
                CurPage.ClientScript.RegisterStartupScript(control.GetType(), key, scripts.ToString());
            }
        }

        private static System.Web.UI.Page CurPage
        {
            get { return (System.Web.UI.Page)HttpContext.Current.Handler; }
        }

        private string GetInlineScript(eOutputMode outputMode, string key, string message, string title)
        {
            // --------------------------------------------------
            // 這是錯誤的，應該要等待所有Script載入完成才執行。(Sys.Application.add_load)
            // --------------------------------------------------
            //StringBuilder scripts = new StringBuilder();
            //scripts.Append("<script type='text/javascript'>");
            //scripts.Append(GetAlertScript(message, title));
            //scripts.Append("</script>");
            //return scripts.ToString();

            StringBuilder scripts = new StringBuilder();
            string funcName = key;
            scripts.Append("Sys.Application.add_load(" + funcName + ");");
            scripts.Append("function " + funcName + "(){");
            scripts.Append(GetAlertScript(outputMode, message, title));
            scripts.Append("Sys.Application.remove_load(" + funcName + ");");
            scripts.Append("}");
            return scripts.ToString();
        }

        private string GetAlertScript(eOutputMode outputMode, string message, string title)
        {
            string funcName = string.Empty;
            switch (outputMode)
            {
                case eOutputMode.Error:
                    {
                        funcName = "jError";
                    } break;
                case eOutputMode.Info:
                    {
                        funcName = "jAlert";
                    } break;
            }
            message = message.Replace("'", "");
            title = title.Replace("'", "");

            string script = string.Format("{0}('{1}', '{2}');", funcName, message, title);
            return script;
        }

        
        #endregion
    }
}
