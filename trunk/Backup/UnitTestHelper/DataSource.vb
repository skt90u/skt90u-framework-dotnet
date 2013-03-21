Public Class DataSource
    Public Shared Function DropDownList() As List(Of String)
        Dim lst As New List(Of String)
        Dim count As Integer = 100
        For i As Integer = 1 To count
            lst.Add(i.ToString())
        Next
        Return lst
    End Function
End Class
