Imports JFramework

Partial Class testDateEdit
    Inherits System.Web.UI.Page

    Protected Sub btn01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn01.Click
        pnl01.Visible = True
        de01.Text = "2011/11/11"
    End Sub


    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'Page.ClientScript.RegisterHiddenField("aaa", "bbb")
        'Page.ClientScript.RegisterHiddenField("ccc", "ddd")
        'Page.ClientScript.RegisterHiddenField("eee", "fff")
    End Sub

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click
        Dim o As Object = Request("aaa")

        Dim a As String = ""
    End Sub
End Class
