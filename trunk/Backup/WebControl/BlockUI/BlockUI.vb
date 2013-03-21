Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Text
Imports AjaxControlToolkit

' 有了 PageAdapter後, 此 WebControl僅作參考就好
Public Class BlockUI
    Inherits System.Web.UI.WebControls.Panel

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)

        Dim type As Type = Me.GetType()

        Me.Attributes("style") = "display:none;"
        ClientScriptManager.RegisterEmbeddedJs(type, "JFramework.WebControl.jquery-1.4.4.js")

        Dim lstWebResource As New List(Of String)
        lstWebResource.Add("JFramework.WebControl.jquery.blockUI.js")
        ClientScriptManager.RegisterCompositeScript(lstWebResource)

        RegisterBlockUI()
    End Sub

    Private Sub RegisterBlockUI()
        AddHandler Page.PreRenderComplete, New EventHandler(AddressOf RegisterScriptsOnPagePreRenderComplete)
    End Sub

    Private Sub RegisterScriptsOnPagePreRenderComplete(ByVal sender As Object, ByVal e As EventArgs)
        '頁面執行 Submit 時顯示 blockUI
        ScriptManager.RegisterOnSubmitStatement(Me, Me.GetType(), "blockUI", "jQuery.blockUI();")
        '頁面載入時隱藏 blockUI
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "unblockUI", "jQuery.unblockUI();", True)
    End Sub
End Class
