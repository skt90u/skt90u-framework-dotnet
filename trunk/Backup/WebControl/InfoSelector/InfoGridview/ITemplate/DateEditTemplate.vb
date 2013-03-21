Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports AjaxControlToolkit


Public Class DateEditTemplate
    Inherits XTemplate

    Public Sub New(ByVal type As ListItemType, ByVal colname As String, Optional ByVal text As String = "")
        MyBase.New(type, colname, text)
    End Sub

    Protected Overrides Sub DoInstantiateIn(ByVal container As System.Web.UI.Control)
        Select Case templateType
            Case ListItemType.Header
                Dim lb As New Label
                lb.Text = IIf(String.IsNullOrEmpty(txt), columnName, txt)
                container.Controls.Add(lb)
                Exit Select
            Case ListItemType.Item
                Dim tb As New TextBox
                tb.ID = "tf" & columnName
                tb.AppendClass("date")
                tb.Attributes("readonly") = "readonly"
                AddHandler tb.DataBinding, AddressOf DoDataBinding

                Dim editMask As New MaskedEditExtender
                editMask.ID = "tf" & columnName & "_editMask"
                editMask.TargetControlID = tb.ID
                editMask.Mask = "9999/99/99"
                editMask.AutoComplete = True
                editMask.ClearMaskOnLostFocus = False

                Dim ib As New ImageButton
                ib.ID = "tf" & columnName & "_ib"
                ib.AppendClass("image")
                ib.TabIndex = -1
                ib.ImageUrl = ClientScriptManager.GetWebResourceUrl(Me.GetType(), "JFramework.WebControl.Calendar.png")
                ib.CausesValidation = False

                Dim ce As New CalendarExtender
                ce.ID = "tf" & columnName & "_ce"
                ce.TargetControlID = tb.ID
                ce.PopupButtonID = ib.ID
                ce.Format = "yyyy/MM/dd"

                ' for fix ie bug
                If BrowserDetection.IsIE Then
                    ce.OnClientDateSelectionChanged = "OnDateSelectionChanged"
                End If

                container.Controls.Add(tb)
                container.Controls.Add(editMask)
                container.Controls.Add(ib)
                container.Controls.Add(ce)
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