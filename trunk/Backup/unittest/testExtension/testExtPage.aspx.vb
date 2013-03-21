Imports JFramework

Partial Class testExtPage
    Inherits System.Web.UI.Page

    Protected Sub TestFindControl(ByVal sender As Object, ByVal e As EventArgs)
        Dim found As TextBox = XFindControl(Of TextBox)("tb")

        'Dim found As TextBox = XFindControl("tb")
        If found IsNot Nothing Then
            lb.Text = "found the control, the value is " & found.Text
        Else
            lb.Text = "can not find the control"
        End If
    End Sub
End Class
