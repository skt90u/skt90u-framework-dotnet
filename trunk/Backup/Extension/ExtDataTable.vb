Imports System.Runtime.CompilerServices

''' <summary>Enhance control functionality</summary>
Public Module ExtDataTable

    ''' <summary>just for debug, display the content of datatable</summary>
    <Extension()> _
    Public Sub Display(ByVal dt As DataTable)
        Dim bFirst As Boolean = True
        For Each col As DataColumn In dt.Columns
            If bFirst Then
                bFirst = False
            Else
                Console.Write(", ")
            End If
            Console.Write(col.ColumnName)
        Next
        Console.WriteLine("")

        For Each row As DataRow In dt.Rows
            bFirst = True
            For Each col As DataColumn In dt.Columns
                If bFirst Then
                    bFirst = False
                Else
                    Console.Write(", ")
                End If
                Console.Write(row(col.ColumnName))
            Next
            Console.WriteLine("")
        Next
    End Sub

    ''' <summary>get all fields's name of datatable</summary>
    <Extension()> _
    Public Function GetFieldNames(ByVal dt As DataTable) As List(Of String)
        Dim fieldNames As New List(Of String)
        For i As Integer = 0 To dt.Columns.Count - 1
            fieldNames.Add(dt.Columns.Item(i).ColumnName)
        Next
        Return fieldNames
    End Function

    ''' <summary>get record count of datatable</summary>
    <Extension()> _
    Public Function Count(ByVal dt As DataTable) As Integer
        Return dt.Rows.Count
    End Function

    ''' <summary>get field count of datatable</summary>
    <Extension()> _
    Public Function FieldCount(ByVal dt As DataTable) As Integer
        Return dt.Columns.Count
    End Function
    ''' <summary>get field's value of n-th data in datatable by field's name</summary>
    <Extension()> _
    Public Function GetValue(ByVal dt As DataTable, ByVal recNo As Integer, ByVal field As String) As Object
        Return dt.Rows(recNo).Item(field)
    End Function

    ''' <summary>get field's value of n-th data in datatable by field's index in the order</summary>
    <Extension()> _
    Public Function GetValue(ByVal dt As DataTable, ByVal recNo As Integer, ByVal fieldNo As Integer) As Object
        Return dt.Rows(recNo).Item(fieldNo)
    End Function

    ''' <summary>set field's value of n-th data in datatable by field's name</summary>
    <Extension()> _
    Public Sub SetValue(ByVal dt As DataTable, ByVal recNo As Integer, ByVal field As String, ByVal value As Object)
        Dim conversionType As Type = dt.FieldType(field)
        dt.Rows(recNo).Item(field) = value.ConvertTo(conversionType)
    End Sub

    ''' <summary>set field's value of n-th data in datatable by field's index in the order</summary>
    <Extension()> _
    Public Sub SetValue(ByVal dt As DataTable, ByVal recNo As Integer, ByVal fieldNo As Integer, ByVal value As Object)
        Dim conversionType As Type = dt.FieldType(fieldNo)
        dt.Rows(recNo).Item(fieldNo) = value.ConvertTo(conversionType)
    End Sub

    ''' <summary>get field's type from datatable by field's index in the order</summary>
    <Extension()> _
    Private Function FieldType(ByVal dt As DataTable, ByVal fieldNo As Integer) As Type
        Return dt.Columns.Item(fieldNo).DataType
    End Function

    ''' <summary>get field's type from datatable by field's name</summary>
    <Extension()> _
    Private Function FieldType(ByVal dt As DataTable, ByVal field As String) As Type
        Return dt.Columns.Item(field).DataType
    End Function
End Module
