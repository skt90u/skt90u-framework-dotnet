Imports System.Data
Imports System.Data.Common
Imports System.Collections.Specialized

Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.Data.OracleClient
Imports Teradata.Client.Provider
Imports System.Data.SQLite
Imports System.Data.OleDb

Public Class XDatabase

    Public Enum DatabaseMode
        MySql = 0
        SqlServer = 1
        Oracle = 2
        Teradata = 3
        Sqlite = 4
        OleDb = 5
    End Enum

    Public Function SelectSQL(ByVal querySQL As String) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Using conn As DbConnection = CreateDbConnection()
                Using command As DbCommand = CreateDbCommand(conn)
                    command.CommandText = querySQL

                    Using adapter As DbDataAdapter = CreateDataAdapter()
                        ' http://msdn.microsoft.com/zh-tw/library/fks3666w.aspx
                        adapter.SelectCommand = command

                        dt = New DataTable()
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    Public Function SelectRecordCountSQL(ByVal querySQL As String) As Integer
        Dim count As Integer = 0
        Try
            Dim aliasField As String = "RECORDCOUNT"
            ' sql server :
            ' http://tw.myblog.yahoo.com/franklkjii/article?mid=437
            Dim aliasTable As String = "RECORDCOUNT_TABLE"
            Dim recordcountSQL As String = String.Format("SELECT COUNT(*) AS {0} FROM ({1}) {2}", aliasField, querySQL, aliasTable)
            Using dt As DataTable = SelectSQL(recordcountSQL)
                If dt.Rows.Count = 1 Then
                    count = Convert.ToInt32(dt.Rows(0)(aliasField))
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return count
    End Function

    Public Sub ExecuteSQL(ByVal SQLs As ICollection(Of String))
        Try
            Using conn As DbConnection = CreateDbConnection()
                Using command As DbCommand = CreateDbCommand(conn)
                    Using transaction As DbTransaction = CreateTransaction(conn, command)
                        Try
                            Dim enumerator As IEnumerator(Of String) = SQLs.GetEnumerator()
                            While enumerator.MoveNext()
                                Dim sql As String = enumerator.Current
                                command.CommandText = sql
                                command.ExecuteNonQuery()
                            End While
                            transaction.Commit()
                        Catch exExecuteSQL As Exception
                            Try
                                transaction.Rollback()
                            Catch exRollback As Exception
                                Throw exRollback
                            End Try
                            Throw exExecuteSQL
                        End Try
                        'using (DbTransaction transaction = CreateTransaction(conn, command))
                    End Using
                    'using (DbCommand command = CreateDbCommand(conn))
                End Using
                'using (DbConnection conn = CreateDbConnection())
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExecuteSQL(ByVal sql As String)
        Try
            Using conn As DbConnection = CreateDbConnection()
                Using command As DbCommand = CreateDbCommand(conn)
                    command.CommandText = sql

                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetSchemaTable(ByVal querySQL As String) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Using conn As DbConnection = CreateDbConnection()
                Using command As DbCommand = CreateDbCommand(conn)
                    command.CommandText = querySQL
                    Using reader As DbDataReader = command.ExecuteReader(CommandBehavior.KeyInfo)
                        dt = reader.GetSchemaTable()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    Private ReadOnly dbMode As DatabaseMode
    Private ReadOnly connectionString As String

    Public Sub New(ByVal dbMode As DatabaseMode, ByVal connectionString As String)
        Me.dbMode = dbMode
        Me.connectionString = connectionString
    End Sub

    Private Class DbInfo
        Public ReadOnly dbMode As DatabaseMode
        Public ReadOnly provider As String

        Public Sub New(ByVal dbMode As DatabaseMode, ByVal provider As String)
            Me.dbMode = dbMode
            Me.provider = provider
        End Sub
    End Class

    Private Shared ReadOnly arrDbInfo As DbInfo() = New DbInfo() {New DbInfo(DatabaseMode.MySql, "MySql.Data.MySqlClient"), New DbInfo(DatabaseMode.SqlServer, "System.Data.SqlClient"), New DbInfo(DatabaseMode.Oracle, "System.Data.OracleClient"), New DbInfo(DatabaseMode.Teradata, "Teradata.Client.Provider"), New DbInfo(DatabaseMode.Sqlite, "System.Data.SQLite"), New DbInfo(DatabaseMode.OleDb, "System.Data.OleDb")}

    Private Function GetDbProviderFactory() As DbProviderFactory
        Try
            Dim provider As String = String.Empty
            For Each dbInfo As DbInfo In arrDbInfo
                If dbInfo.dbMode = dbMode Then
                    provider = dbInfo.provider
                    Exit For
                End If
            Next
            Debug.Assert(Not String.IsNullOrEmpty(provider))

            Return DbProviderFactories.GetFactory(provider)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CreateDbConnection() As DbConnection
        Try
            Dim factory As DbProviderFactory = GetDbProviderFactory()
            Dim conn As DbConnection = factory.CreateConnection()
            conn.ConnectionString = connectionString
            Return conn
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CreateDataAdapter() As DbDataAdapter
        Try
            Dim factory As DbProviderFactory = GetDbProviderFactory()
            Dim adapter As DbDataAdapter = factory.CreateDataAdapter()
            Return adapter
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CreateDbCommand(ByVal conn As DbConnection) As DbCommand
        Try
            Dim factory As DbProviderFactory = GetDbProviderFactory()
            Dim command As DbCommand = factory.CreateCommand()

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            command.Connection = conn

            Return command
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CreateTransaction(ByVal conn As DbConnection, ByVal command As DbCommand) As DbTransaction
        Try
            Dim transaction As DbTransaction = conn.BeginTransaction()
            command.Transaction = transaction
            Return transaction
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

#Region "自動執行所有testedClassType內的Public Function"
#If 0 Then
    ''' <summary>自動執行所有testedClassType內的Public Function</summary>
    ''' <typeparam name="testedClassType">被測Class</typeparam>
    ''' <param name="ts">測試資料</param>
    ''' <remarks>
    ''' 測試Function的參數, 來自TestingSetting內的Field or Property
    ''' 
    ''' 
    ''' For Each ts As TestingSetting In testingSettings
    '''     Run(Of GenericDb)(ts)
    ''' Next
    ''' </remarks>
    Private Sub Run(Of testedClassType)(ByVal ts As TestingSetting)
        Dim testedClass As testedClassType = Activator.CreateInstance(GetType(testedClassType))
        Dim typeTestedClass As Type = testedClass.GetType()
        Dim mis As MethodInfo() = typeTestedClass.GetMethods()
        For Each mi As MethodInfo In mis
            ' 找出所有要被測試的Function (Public都會被測試)
            If mi.IsPublic AndAlso mi.IsStatic Then
                Dim args() As Object = Nothing
                Dim i As Integer = 0
                ' 在此Function找出所有需要的參數
                For Each p As ParameterInfo In mi.GetParameters()
                    Dim nameParameter As String = p.Name

                    ' 在TestingSetting找出名稱相同的Property or Field
                    Dim o As Object = Nothing
                    If o Is Nothing Then o = Reflect.GetPropertyValue(ts, nameParameter)
                    If o Is Nothing Then o = Reflect.GetFieldValue(ts, nameParameter)

                    ' 如果找到名稱相符的參數就將他塞到等會要呼叫Invoke的參數列中
                    If o IsNot Nothing Then
                        ReDim Preserve args(i)
                        args(i) = o
                        i += 1
                    End If
                Next
                ' 呼叫測試Function
                mi.Invoke(testedClass, args)
            End If
        Next
    End Sub
#End If
#End Region
