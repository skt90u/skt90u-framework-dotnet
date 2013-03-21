Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports AjaxControlToolkit

Public Class NumEditTemplate
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
                Dim tb As New TextBox
                tb.ID = "tf" & columnName
                AddHandler tb.DataBinding, AddressOf DoDataBinding
                tb.AppendClass("number")
                tb.MaxLength = 7

                Dim filter As New FilteredTextBoxExtender
                filter.ID = "tf" & columnName & "_filter"
                filter.TargetControlID = tb.ID
                filter.FilterType = FilterTypes.Numbers

                container.Controls.Add(tb)
                container.Controls.Add(filter)
                Exit Select
        End Select
    End Sub

    Private Sub DoDataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim txtdata As TextBox = DirectCast(sender, TextBox)
        Dim container As GridViewRow = DirectCast(txtdata.NamingContainer, GridViewRow)
        Dim dataValue As Object = DataBinder.Eval(container.DataItem, columnName)
        If Not dataValue.Equals(DBNull.Value) Then
            txtdata.Text = dataValue.ToString()
        End If
    End Sub
End Class