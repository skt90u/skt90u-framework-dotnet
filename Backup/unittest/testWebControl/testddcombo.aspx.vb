Imports JFramework

Partial Class testddcombo
    Inherits System.Web.UI.Page

    Private Sub BindData()
        Dim connectionString As String = "Data Source=E:\GoogleProjectHosting\jelly-dotnet-framework\src\unittest\testDB\testingData\SQLite\Northwind.sl3"
        Dim dbMode As XDatabase.DatabaseMode = XDatabase.DatabaseMode.Sqlite
        Dim querySQL As String = "SELECT CategoryID As FVALUE, Description AS FDESC from Categories"
        Dim xd As New XDatabase(dbMode, connectionString)
        box1.DataSource = xd.SelectSQL(querySQL)
        box1.DataTextField = "FDESC"
        box1.DataValueField = "FVALUE"
        box1.DataBind()

        box2.DataSource = xd.SelectSQL(querySQL)
        box2.DataTextField = "FVALUE"
        box2.DataValueField = "FDESC"
        box2.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub
End Class
