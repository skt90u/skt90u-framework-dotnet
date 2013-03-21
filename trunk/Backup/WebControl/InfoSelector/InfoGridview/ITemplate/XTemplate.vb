Imports System.Web.UI.WebControls
Imports System.Web.UI

Public MustInherit Class XTemplate
    Implements ITemplate

    Protected templateType As ListItemType
    Protected columnName As String
    Protected txt As String

    Public Function GetColumnName() As String
        Return columnName
    End Function

    Public Sub New(ByVal type As ListItemType, ByVal colname As String, Optional ByVal text As String = "")
        templateType = type
        columnName = colname
        txt = text
    End Sub

    Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn
        DoInstantiateIn(container)
    End Sub

    Protected MustOverride Sub DoInstantiateIn(ByVal container As System.Web.UI.Control)
End Class