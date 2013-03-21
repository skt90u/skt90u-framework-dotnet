Imports System.Web.UI.WebControls
Imports System.Runtime.CompilerServices
Imports System.Reflection

''' <summary>Enhance ListViewItem functionality</summary>
Public Module ExtListViewItem

    ''' <summary>tell if aItem is InsertItem</summary>
    ''' <remarks>http://msdn.microsoft.com/zh-tw/library/system.web.ui.webcontrols.listview.insertitem.aspx</remarks>
    <Extension()> _
    Public Function isInsertItem(ByVal aItem As ListViewItem) As Boolean
        Return ListViewItemType.InsertItem = aItem.ItemType
    End Function

    ''' <summary>tell if aItem is EditItem</summary>
    ''' <remarks>http://www.blog.ingenuitynow.net/ASPNet+ListView+EditItem.aspx</remarks>
    <Extension()> _
    Public Function isEditItem(ByVal aItem As ListViewItem) As Boolean
        Dim lsv As ListView = CType(aItem.NamingContainer, ListView)
        Return (ListViewItemType.DataItem = aItem.ItemType) AndAlso (lsv.EditIndex = CType(aItem, ListViewDataItem).DisplayIndex)
    End Function

    ''' <summary>tell if aItem is ReadOnlyItem</summary>
    ''' <remarks>http://www.blog.ingenuitynow.net/ASPNet+ListView+EditItem.aspx</remarks>
    <Extension()> _
    Public Function isReadOnlyItem(ByVal aItem As ListViewItem) As Boolean
        Dim lsv As ListView = CType(aItem.NamingContainer, ListView)
        Return (ListViewItemType.DataItem = aItem.ItemType) AndAlso (lsv.EditIndex <> CType(aItem, ListViewDataItem).DisplayIndex)
    End Function
End Module
