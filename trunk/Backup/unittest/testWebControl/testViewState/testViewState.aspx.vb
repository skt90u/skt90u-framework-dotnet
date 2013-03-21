Imports JFramework.UnitTestHelper
Partial Class testViewState_testViewState
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ddl.DataSource = DataSource.DropDownList()
        ddl.DataBind()
    End Sub

    Protected Sub ddl_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl.Init
        
    End Sub
End Class
