'Imports Helper
Imports JFramework

Partial Class testExtGridview
    Inherits System.Web.UI.Page

    ' ----------------------------------------------------------------------------'
    '                        very very very important !!!                         '
    ' ----------------------------------------------------------------------------'
    ' if you run ExportExcel/ExportWord functions in codebehide, 
    ' your derived page class( testExtGridview ) must override 
    ' VerifyRenderingInServerForm subroutine, and do nothing.
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As System.Web.UI.Control)
        'MyBase.VerifyRenderingInServerForm(control)
    End Sub

    Private filename As String = "testExtGridview"
    Private bExportExcel As Boolean = True

    Protected Sub ExportData(ByVal sender As Object, ByVal e As EventArgs)
        If bExportExcel Then
            grv.ExportExcel(filename & ".xls")
        Else
            grv.ExportWord(filename & ".doc")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grv.DataSource = getData()
            grv.DataBind()
        End If
    End Sub

    Protected Sub ReflectOnClick(ByVal sender As Object, ByVal e As EventArgs)
        btn.RaiseClick()
    End Sub
End Class
