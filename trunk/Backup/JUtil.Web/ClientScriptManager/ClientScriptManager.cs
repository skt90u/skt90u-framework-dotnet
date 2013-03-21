using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using JUtil.Reflection;
using JUtil.Web.Extensions;

namespace JUtil.Web
{
    public class ClientScriptManager
    {
        /// <summary>取得內嵌資源對應的url</summary>
        /// <remarks>
        /// 要能夠使用GetWebResourceUrl取到ResourceUrl
        ///   - (1) 在AssemblyInfo.vb宣告使用的資源,例如:
        ///         (請將 中括號換成小於大於符號)
        ///         [Assembly: System.Web.UI.WebResource("JFramework.WebControl.DateEdit.css", "text/css")]
        /// 
        ///   - (2) 屬性中[建置動作] 欄位必須設定成內嵌資源
        /// 
        /// 自訂控件可能使用GetWebResourceUrl設定Image的, 
        /// 例如, ImageUrl = ClientScriptManager.GetWebResourceUrl
        /// 因此將GetWebResourceUrl設定成Pubic
        /// </remarks>
        public static string GetWebResourceUrl(Type type, string webResource)
        {
            return CurPage.ClientScript.GetWebResourceUrl(type, webResource);
        }

        /// <summary>在Header中加入css</summary>
        public static void RegisterEmbeddedCSS(Type type, string webResource)
        {
            string csslocation = GetWebResourceUrl(type, webResource);
            CurPage.RegisterCSS(csslocation);
        }

        /// <summary>在Header中加入Javascript</summary>
        /// <remarks>
        /// 適用於加入Framework類型的JavaScript,但必須確保Framework之間是沒有相依性的
        /// 這是因為在<head></head>中設定外部連結的JavaScript
        /// 無法確定的那一個先誰先載入(誰先下載完成,誰先載入)
        /// </remarks>
        public static void RegisterEmbeddedJs(Type type, string webResource)
        {
            string refUrl = GetWebResourceUrl(type, webResource);
            System.Collections.Specialized.StringCollection refs = JsRefCollection;
            if ((!refs.Contains(refUrl)))
            {
                refs.Add(refUrl);
            }
        }

        /// <summary>將指定的WebResource加入CompositeScript</summary>
        /// <remarks>
        /// (1) 請使用此函式註冊jquery.plugin, 如此才能保證JQuery.Core會在jquery.plugin載入完成
        /// (2) lstWebResource定義的順序不重要, 因為CompositeScript會將JavaScript合成一個檔案
        ///     而Browser會先讀取函式以及物件宣告後, 才會執行(我理解的說法)
        /// </remarks>
        public static void RegisterCompositeScript(List<string> lstWebResource, string assembly = "JFramework.WebControl")
        {
            foreach (string webResource in lstWebResource)
            {
                AddCompositeScript(webResource, assembly);
            }
        }

        /// <summary>在Client端, 初始化控件的行為, 如註冊onclick event</summary>
        /// <remarks>
        ///   時機   : Raised after all scripts have been loaded and all objects in the 
        ///            application that are created by using $create are 
        ///            initialized.
        ///            [PS: 會在 pageLoad function之前執行]
        /// 
        /// 觸發時機 : The load event is raised for all postbacks to the server
        ///            , which includes asynchronous postbacks.
        /// </remarks>
        public static void RegisterClientApplicationLoadScript(Control control, string script)
        {
            //     假如有一TextBox <asp:TextBox ID="tb" runat="server></asp:TextBox>
            //     <script type="text/javascript">    
            //       $(function(){
            //         var data = ['台北市中正區','台北市大同區','台北市中山區','台北市松山區','台北市大安區'];  
            //         $("#" + tb.ClientID).autocomplete(data, {matchContains: true}); 
            //       });
            //     </script>
            // 
            // 使用RegisterClientApplicationLoadScript方式註冊, 為
            // RegisterClientApplicationLoadScript(tb, "var data = ['台北市中正區','台北市大同區','台北市中山區','台北市松山區','台北市大安區'];  $("#" + tb.ClientID).autocomplete(data, {matchContains: true}); ")

            // ---------------------------------------------------------------------------- '
            //                        very   very   very   importmant
            // ---------------------------------------------------------------------------- '
            // 其實Ajax.Net的Sys.Application.add_load(function(){...}), 與JQuery的$(function(){...}); 不是等價的,
            // 但是如果控件放置在UpdatePanel之中, 使用Sys.Application.add_load才可以work
            // ---------------------------------------------------------------------------- '
            Type type = Reflect.GetCallingType();
            string key = string.Format("{0}_{1}_LoadScript", control.ClientID, type.Name);

            StringBuilder scripts = new StringBuilder();
            scripts.Append("Sys.Application.add_load(" + key + ");");
            scripts.Append("function " + key + "(){");
            scripts.Append(script);
            scripts.Append("Sys.Application.remove_load(" + key + ");");
            scripts.Append("}");

            RegisterStartupScript(control, key, scripts.ToString());
        }

        /// <summary>在Client端, 釋放資源</summary>
        /// <remarks>
        ///   時機   : Raised before all objects are disposed and before the 
        ///            browser window's window.unload event occurs.
        ///            [PS: 會在 pageLoad function之前執行]
        /// 
        /// 誰要用   : you should free any resources that your code is 
        ///            holding.
        /// </remarks>
        public static void RegisterClientApplicationUnLoadScript(Control control, string script)
        {
            Type type = Reflect.GetCallingType();
            string key = string.Format("{0}_{1}_UnLoadScript", control.ClientID, type.Name);

            StringBuilder scripts = new StringBuilder();

            scripts.Append("<script type='text/javascript'>");
            scripts.Append("Sys.Application.add_unload(function(){");
            scripts.Append(script);
            scripts.Append("});");
            scripts.Append("</script>");

            RegisterStartupScript(control, key, scripts.ToString());
        }

        /// <summary>(目前還不需要用到)</summary>
        /// <remarks>
        ///   時機   : Raised after all scripts have been loaded but before any
        ///            objects are created
        /// 
        ///   誰會用 : If you are writing a component, the init event gives 
        ///            you a point in the life cycle to add your component 
        ///            to the page. The component can then be used by other 
        ///            components or by script later in the page life cycle.
        /// 
        /// 執行次數 : The init event is raised only one time when the page 
        ///            is first rendered
        /// </remarks>
        public static void RegisterClientApplicationInitScript(Control contorl, string script)
        {
            Type type = Reflect.GetCallingType();
            string key = string.Format("{0}_{1}_InitScript", contorl.ClientID, type.Name);

            StringBuilder scripts = new StringBuilder();

            scripts.Append("<script type='text/javascript'>");
            scripts.Append("Sys.Application.add_init(function(){");
            scripts.Append(script);
            scripts.Append("});");
            scripts.Append("</script>");

            RegisterStartupScript(contorl, key, scripts.ToString());
        }

        public static void RegisterClientSubmitScript(Control contorl, string script)
        {
            ScriptManager.RegisterOnSubmitStatement(contorl, contorl.GetType(), Convert.ToString(contorl.ClientID) + "_SubmitScript", script);
        }

        #region "private method & member"

        private static string tagJsRef = "Javascripts-Collection";
        private static Page CurPage
        {
            get { return (Page)HttpContext.Current.Handler; }
        }

        private static ScriptManager CurSM
        {
            get { return ScriptManager.GetCurrent(CurPage); }
        }

        private static System.Collections.Specialized.StringCollection JsRefCollection
        {
            get
            {
                string key = tagJsRef;
                if (HttpContext.Current.Items[key] == null)
                {
                    HttpContext.Current.Items[key] = new System.Collections.Specialized.StringCollection();
                    CurPage.PreRenderComplete += new EventHandler(RegisterScriptsOnPagePreRenderComplete);
                }
                return (System.Collections.Specialized.StringCollection)HttpContext.Current.Items[key];
            }
        }

        private static void RegisterScriptsOnPagePreRenderComplete(object sender, EventArgs e)
        {
            Page page = sender as Page;
            System.Collections.Specialized.StringCollection refs = JsRefCollection;
            foreach (string url in refs)
            {
                page.Header.Controls.Add(new LiteralControl("<script type='text/javascript' src='" + url + "'></script>"));
            }
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

        private static Control CreateStyleLink(string href)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("rel", "Stylesheet");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("href", href);
            return link;
        }

        //Private Shared Sub RegisterClientApplicationLoadScript(ByVal control As Control, ByVal key As String, ByVal script As String)
        //    Dim scripts As New StringBuilder()

        //    scripts.Append("Sys.Application.add_load(" & key & ");")
        //    scripts.Append("function " & key & "(){")
        //    scripts.Append(script)
        //    scripts.Append("Sys.Application.remove_load(" & key & ");")
        //    scripts.Append("}")

        //    RegisterStartupScript(control, key, scripts.ToString())
        //End Sub

        //Private Shared Sub RegisterClientApplicationUnLoadScript(ByVal control As Control, ByVal key As String, ByVal script As String)
        //    Dim scripts As New StringBuilder()

        //    scripts.Append("<script type='text/javascript'>")
        //    scripts.Append("Sys.Application.add_unload(function(){")
        //    scripts.Append(script)
        //    scripts.Append("});")
        //    scripts.Append("</script>")

        //    RegisterStartupScript(control, key, scripts.ToString())
        //End Sub

        //Private Shared Sub RegisterClientApplicationInitScript(ByVal control As Control, ByVal key As String, ByVal script As String)
        //    Dim scripts As New StringBuilder()

        //    scripts.Append("<script type='text/javascript'>")
        //    scripts.Append("Sys.Application.add_init(function(){")
        //    scripts.Append(script)
        //    scripts.Append("});")
        //    scripts.Append("</script>")

        //    RegisterStartupScript(control, key, scripts.ToString())
        //End Sub

        /// <summary>將指定組件中的JavaScript Resource加入CompositeScript</summary>
        /// <param name="name">定義在AssemblyInfo.vb中的webResource名稱</param>
        /// <param name="assembly">組件名稱,以此專案而言是JFramework.WebControl</param>
        /// <remarks>
        /// 用法 
        /// ClientScriptManager.AddCompositeScript(Me, "JFramework.WebControl.jquery.textareaCounter.js", "JFramework.WebControl")
        /// </remarks>
        private static void AddCompositeScript(string name, string assembly)
        {
            AddCompositeScript(new ScriptReference(name, assembly));
        }

        /// <summary>將網路上指定的JavaScript加入CompositeScript</summary>
        /// <param name="control"></param>
        /// <param name="path">http://XXX/NNN.js</param>
        /// <remarks>
        /// 用法 
        /// ClientScriptManager.AddCompositeScript(Me, "http://code.jquery.com/jquery-1.5.js")
        /// </remarks>
        private static void AddCompositeScript(Control control, string path)
        {
            AddCompositeScript(new ScriptReference(path));
        }

        private static bool ReferenceIsExists(ScriptManager sm, ScriptReference scriptReference)
        {
            if (!string.IsNullOrEmpty(scriptReference.Path))
            {
                var scripts = from s in sm.CompositeScript.Scripts
                              where s.Path.ToLower() == scriptReference.Path.ToLower()
                              select s;
                return scripts.Count() > 0;
            }

            var typedScripts = from ts in sm.CompositeScript.Scripts
                               where ts.Assembly.ToLower() == scriptReference.Assembly.ToLower() &&
                               ts.Name.ToLower() == scriptReference.Name.ToLower()
                               select ts;

            return typedScripts.Count() > 0;
        }

        private static void AddCompositeScript(ScriptReference scriptReference)
        {
            if (!ReferenceIsExists(CurSM, scriptReference))
            {
                CurSM.CompositeScript.Scripts.Add(scriptReference);
            }
        }
        #endregion

#if false
	// DJ (DotNetAge jQuery Controls for ASP.NET)沒有
	// 使用網路上教的以下兩種方式註冊Javascript
	//   - ScriptManager.RegisterClientScriptInclude
	//   - Page.ClientScript.RegisterClientScriptInclude
	//
	// 目前測試可用, 但還在測試什麼時候是必須使用此方式註冊才能work

	/// <summary>
	/// Registers the client script include.
	/// </summary>
	/// <remarks>
	/// 判斷當頁面有ScriptManager時，應透過ScriptManager註冊JS，
	/// 使用Page.ClientScript註冊會在UpdatePanel Partial Render時遺漏參考。
	/// </remarks>

	public static void IncludeJs(System.Web.UI.Control control, string webResource)
	{
		ScriptManager sm = ScriptManager.GetCurrent(wc.Page);
		// 即使用ToolkitScriptManager代替ScriptManager, 
		// 也可使用ScriptManager.GetCurrent判斷()
		string id = wc.ID;
		string webResourceUrl = GetWebResourceUrl(wc, webResource);

		if (sm != null) {
			ScriptManager.RegisterClientScriptInclude(wc.Page, wc.GetType(), webResource, webResourceUrl);
		} else {
			wc.Page.ClientScript.RegisterClientScriptInclude(webResource, webResourceUrl);
		}
	}
#endif


    } // end of ClientScriptManager
}
