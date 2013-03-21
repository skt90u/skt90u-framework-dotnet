using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 組件的一般資訊是由下列的屬性集控制。
// 變更這些屬性的值即可修改組件的相關
// 資訊。
[assembly: AssemblyTitle("JUtil.Web")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("JUtil.Web")]
[assembly: AssemblyCopyright("Copyright ©  2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 將 ComVisible 設定為 false 會使得這個組件中的型別
// 對 COM 元件而言為不可見。如果您需要從 COM 存取這個組件中
// 的型別，請在該型別上將 ComVisible 屬性設定為 true。
[assembly: ComVisible(false)]

// 下列 GUID 為專案公開 (Expose) 至 COM 時所要使用的 typelib ID
[assembly: Guid("6f5246ad-060b-4f24-a75b-db6a1033f62f")]

// 組件的版本資訊是由下列四項值構成:
//
//      主要版本
//      次要版本 
//      組建編號
//      修訂編號
//
// 您可以指定所有的值，也可以依照以下的方式，使用 '*' 將組建和修訂編號
// 指定為預設值:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

/*
 * WebResource vs EmbeddedResource 
 * 共通點 : 
 *          - 必須設定成『內嵌資源』
 *          - 必須在 AssemblyInfo.cs中，定義WebResourceAttribute
 * 
 * 相異處 :  
 *          - WebResource      : 在WebResourceAttribute中的WebResourceName
 *                               可以自訂
 *
 *          - EmbeddedResource : 在WebResourceAttribute中的WebResourceName
 *                               必須滿足 
 *                               $(DefaultNamespace)$.$(相對路徑).$(檔案名稱)$
 *                               [其中，相對路徑每一層以逗號隔開]
 *                               
 * 使用時機 : 
 *          - WebResource :
 *          
 *          - EmbeddedResource : <asp:ScriptReference Assembly="$(Assembly)$" Name="$(WebResourceName)$"/>
 *                               
 *                               如果出現以下錯誤訊息，就檢查一下
 *                               EmbeddedResource定義的WebResourceName是否滿足上面規則
 *                               
 *                               ERROR : 
 *                               組件 'JUtil.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' 
 *                               包含名為 'UpdatePanelAnimation.js' 的 Web 資源，
 *                               但是未包含名為 'UpdatePanelAnimation.js' 的內嵌資源。
 *                               
 */

[assembly: System.Web.UI.WebResource(
    "JUtil.Web.JavaScript.jutil-all.js",
    "text/javascript")]

[assembly: System.Web.UI.WebResource(
    "JUtil.Web.JavaScript.jutil-all-debug.js",
    "text/javascript")]

[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.jQuery.jquery.1.4.4.js", "text/javascript")]

// --------------------------------------------------------------------------------
// DateEdit
// --------------------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.TextBox.DateEdit.DateEdit.css", "text/css")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.TextBox.DateEdit.Calendar.png", "image/png")] 

// --------------------------------------------------------------------------------
// XTextArea
// --------------------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.TextBox.XTextArea.jquery.textareaCounter.css", "text/css")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.TextBox.XTextArea.jquery.textareaCounter.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.TextBox.XTextArea.jquery.elastic.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.TextBox.XTextArea.jquery.elastic.source.js", "text/javascript")]

// --------------------------------------------------------------------------------
// uploadify
// --------------------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.uploadify.css.uploadify.default.css", "text/css")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.uploadify.css.uploadify.css", "text/css")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.uploadify.scripts.jquery.uploadify.v2.1.0.min.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.uploadify.scripts.swfobject.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.uploadify.scripts.uploadify.swf", "application/x-shockwave-flash")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.uploadify.scripts.sample.js", "text/javascript", PerformSubstitution = true)]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.uploadify.scripts.expressInstall.swf", "application/x-shockwave-flash")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.uploadify.images.cancel.png", "image/png")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.uploadify.images.button.jpg", "image/jpeg")]

// --------------------------------------------------------------------------------
// jAlert
// --------------------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.WebStyle.jAlert.images.help.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.WebStyle.jAlert.images.important.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.WebStyle.jAlert.images.info.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.WebStyle.jAlert.images.title.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.WebStyle.jAlert.jquery.alerts.css", "text/css", PerformSubstitution = true)]

