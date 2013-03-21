Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Public Class LabelTemplate
    Inherits XTemplate

    Public Sub New(ByVal type As ListItemType, ByVal colname As String, Optional ByVal text As String = "")
        MyBase.New(type, colname, text)
    End Sub

    Protected Overrides Sub DoInstantiateIn(ByVal container As System.Web.UI.Control)
        Select Case templateType
            Case ListItemType.Header
                Dim lb As New Label
                lb.ID = "th" & columnName
                lb.Text = IIf(String.IsNullOrEmpty(txt), columnName, txt)
                container.Controls.Add(lb)
                Exit Select
            Case ListItemType.Item
                Dim lb As New Label
                lb.ID = "tf" & columnName
                AddHandler lb.DataBinding, AddressOf DoDataBinding
                container.Controls.Add(lb)
                Exit Select
        End Select
    End Sub

    Private Sub DoDataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim lb As Label = CType(sender, Label)
        Dim container As GridViewRow = DirectCast(lb.NamingContainer, GridViewRow)
        Dim dataValue As Object = DataBinder.Eval(container.DataItem, columnName)
        If Not dataValue.Equals(DBNull.Value) Then
            lb.Text = dataValue.ToString()
        End If
    End Sub
End Class