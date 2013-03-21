Imports System.Web.UI.WebControls
Imports System.Web.UI

<Assembly: System.Web.UI.WebResource("JFramework.WebControl.lightbox.css", "text/css", PerformSubstitution:=True)> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControl.prototype.js", "text/javascript")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControl.effects.js", "text/javascript")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControl.builder.js", "text/javascript")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControl.lightbox.js", "text/javascript", PerformSubstitution:=True)> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControl.closelabel.gif", "image/gif")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControl.loading.gif", "image/gif")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControl.nextlabel.gif", "image/gif")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControl.prevlabel.gif", "image/gif")> 

Public Class lightbox
    Inherits System.Web.UI.WebControls.CompositeControl

    Protected Overrides ReadOnly Property TagKey() As System.Web.UI.HtmlTextWriterTag
        Get
            Return HtmlTextWriterTag.Div
        End Get
    End Property

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)

        Dim type As Type = Me.GetType()
        ClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControl.lightbox.css")
        ClientScriptManager.RegisterEmbeddedJs(type, "JFramework.WebControl.prototype.js")

        Dim lstWebResource As New List(Of String)
        lstWebResource.Add("JFramework.WebControl.effects.js")
        lstWebResource.Add("JFramework.WebControl.builder.js")
        lstWebResource.Add("JFramework.WebControl.lightbox.js")
        ClientScriptManager.RegisterCompositeScript(lstWebResource)

        'Dim scripts As String = GenApplicationLoadScript()
        'ClientScriptManager.RegisterClientApplicationLoadScript(Me, scripts)
    End Sub
End Class
