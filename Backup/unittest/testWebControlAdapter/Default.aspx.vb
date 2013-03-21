
Partial Class _Default
    Inherits System.Web.UI.Page


    Protected Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click
        System.Threading.Thread.Sleep(5000)
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim o As Object = Me.Adapter
        Dim b As Object = Nothing
    End Sub
End Class
