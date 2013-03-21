Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Collections.ObjectModel
Imports System.Web.UI
Imports System.Collections

<Serializable()> _
Public Class FieldCollection
    Inherits Collection(Of InfoGridViewField)

    Public Function GetFieldType(ByVal colName As String) As InfoGridview.eFieldType
        Dim enumerator As IEnumerator = GetEnumerator()
        While enumerator.MoveNext()
            Dim aField As InfoGridViewField = CType(enumerator.Current, InfoGridViewField)
            If aField.colName = colName Then
                Return aField.Type
            End If
        End While
        Return InfoGridview.eFieldType.Label
    End Function

    Public Function GetFieldTitle(ByVal colName As String) As String
        Dim enumerator As IEnumerator = GetEnumerator()
        While enumerator.MoveNext()
            Dim aField As InfoGridViewField = CType(enumerator.Current, InfoGridViewField)
            If aField.colName = colName Then
                Return aField.Title
            End If
        End While
        Return colName
    End Function
End Class


