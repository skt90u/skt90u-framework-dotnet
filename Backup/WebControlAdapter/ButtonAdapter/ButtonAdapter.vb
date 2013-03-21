Public Class ButtonAdapter
    Inherits System.Web.UI.WebControls.Adapters.WebControlAdapter

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)
        Control.CssClass = "btn01"
    End Sub
    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        RegisterScripts()
    End Sub

    Private Sub RegisterScripts()
        Dim type As Type = GetType(ButtonAdapter)
        Helpers.RegisterEmbeddedCSS("JFramework.WebControlAdapter.ButtonAdapter.css", type, Me.Page)
    End Sub

End Class
