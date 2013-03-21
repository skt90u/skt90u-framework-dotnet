
Partial Class testValidationCode_testValidationCode
    Inherits System.Web.UI.Page

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click
        Dim bMatch As Boolean = ValidateCodeControl1.IsMatch()
        lblResult.Text = IIf(bMatch, "Match", "Not Match")
    End Sub
End Class
