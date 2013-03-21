Imports JFramework

Public Class TestingSetting

    Public Sub New(ByVal connectionString As String, _
                   ByVal dbMode As XDatabase.DatabaseMode, _
                   ByVal querySQL As String)
        Me.connectionString = connectionString
        Me.dbMode = dbMode
        Me.querySQL = querySQL
    End Sub

    ' 連線參數
    Public ReadOnly connectionString As String
    Public ReadOnly dbMode As XDatabase.DatabaseMode

    ' 查詢SQL
    Public ReadOnly querySQL As String

End Class
