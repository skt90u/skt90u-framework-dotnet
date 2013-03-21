Imports System.ComponentModel
Imports System.Web.UI
Imports System.Web.UI.WebControls

' 要繼承 ListView 必須加入參考System.Web.Extensions.dll (http://msdn.microsoft.com/zh-tw/library/system.web.ui.webcontrols.listview.aspx)
'<ToolboxData("<{0}:RetailListView runat=server></{0}:RetailListView>")> _
Public Class XListView
    Inherits ListView

    ' It's more convenient to initialize your listview in different mode.
    Public Event InitInsertItem As EventHandler(Of ListViewItemEventArgs)
    Public Event InitEditItem As EventHandler(Of ListViewItemEventArgs)
    Public Event InitReadOnlyItem As EventHandler(Of ListViewItemEventArgs)
    ' 如何使用
    ' 假設你的ListView叫做lsv, 請定義以下函式
    'Private Sub lsv_InitInsertItem(ByVal sender As Object, ByVal e As ListViewItemEventArgs) Handles lsv.InitInsertItem

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        AddHandler Me.ItemDataBound, AddressOf Me.CustomItemDataBound
        MyBase.OnInit(e)
    End Sub

    Private Sub CustomItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs)
        Dim aItem As ListViewItem = e.Item
        If aItem.isReadOnlyItem Then
            RaiseEvent InitReadOnlyItem(Me, New ListViewItemEventArgs(aItem))
        ElseIf aItem.isEditItem Then
            RaiseEvent InitEditItem(Me, New ListViewItemEventArgs(aItem))
        ElseIf aItem.isInsertItem Then
            RaiseEvent InitInsertItem(Me, New ListViewItemEventArgs(aItem))
        End If
    End Sub
End Class
