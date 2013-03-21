Imports System.Data

Public Module Helper
    Private sCols() As String = New String() {"座號", "姓名", "性別", "住址"}
    Private nRow As Integer = 10

    Public Function getData() As DataTable
        Dim table As New DataTable

        Dim col As DataColumn = Nothing
        For Each sCol As String In sCols
            col = New DataColumn(sCol, GetType(System.String))
            table.Columns.Add(col)
        Next

        For i As Integer = 1 To nRow
            Dim row As DataRow = table.NewRow()
            For j As Integer = 0 To sCols.Length - 1

                Dim value As String = ""
                For k As Integer = 0 To j + 1
                    value += i.ToString()
                Next
                row.Item(sCols(j)) = value
            Next
            table.Rows.Add(row)
        Next

        Return table
    End Function
End Module
