Imports System.Data
Imports JFramework

Partial Class Gridview_sandbox
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RegisterCSS("css/GridView.css")
            grvContainer.CssClass = "ChromeBlackGridView"
            SetupStyle(grv)
            BindGridView(grv)
        End If
    End Sub

    Private Sub SetupStyle(ByVal grv As GridView)
        grv.CssClass = "GridViewStyle"
        grv.HeaderStyle.CssClass = "HeaderStyle"
        grv.PagerStyle.CssClass = "PageStyle"
        grv.RowStyle.CssClass = "RowStyle"
        grv.AlternatingRowStyle.CssClass = "AltRowStyle"
        grv.SelectedRowStyle.CssClass = "SelectedRowStyle"
    End Sub

    Private Sub BindGridView(ByVal grv As GridView)
        Dim connectionString As String = "Data Source=E:\GoogleProjectHosting\jelly-dotnet-framework\unittest\testDB\testingData\SQLite\Northwind.sl3"
        Dim dbMode As XDatabase.DatabaseMode = XDatabase.DatabaseMode.Sqlite
        Dim querySQL As String = "SELECT * from Categories"
        Dim xd As New XDatabase(dbMode, connectionString)
        grv.DataSource = xd.SelectSQL(querySQL)
        grv.DataBind()

    End Sub

    Protected Sub ddl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl.SelectedIndexChanged
        grvContainer.CssClass = ddl.SelectedValue
    End Sub
End Class
