Imports JFramework

Partial Class testExtGridviewRow
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grv.DataSource = getData()
            grv.DataBind()
        End If
    End Sub

    Protected Sub grv_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grv.RowDataBound
        Dim row As GridViewRow = e.Row
        Dim CommandNames() As String = New String() {"Select", "Edit", "New"}
        For Each CommandName As String In CommandNames
            Dim btn As Button = row.GetButton(CommandName)
            If btn IsNot Nothing Then
                btn.Text = "This is " & CommandName
            End If
        Next
    End Sub

    Protected Sub grv_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grv.RowEditing
        Dim i As Integer = e.NewEditIndex
        Dim grv As GridView = CType(sender, GridView)
        Dim row As GridViewRow = grv.Rows.Item(i)

        Dim od As OrderedDictionary = row.ExtractValues()

        Dim value As String = ""
        value += "{"

        Dim bFirst As Boolean = True
        For Each entry As DictionaryEntry In od
            If bFirst Then
                bFirst = False
            Else
                value += ", "
            End If
            value += String.Format("{0}:{1}", entry.Key, entry.Value)
        Next
        value += "}"

        lb.Text = value
    End Sub
End Class
