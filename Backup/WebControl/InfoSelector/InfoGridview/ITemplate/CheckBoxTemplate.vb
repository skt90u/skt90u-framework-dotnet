Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls


Public Class CheckBoxTemplate
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
                Dim cb As New CheckBox
                cb.ID = "tf" & columnName
                container.Controls.Add(cb)
                Exit Select
        End Select
    End Sub
End Class