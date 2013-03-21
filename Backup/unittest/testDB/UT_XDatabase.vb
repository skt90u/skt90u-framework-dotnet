Imports System.Reflection
Imports JFramework

Module UT_XDatabase

    Enum eFeature As Integer
        BlockUI = &H1
        LightBox = &H2
    End Enum

    Sub Main()
        'func1()
        Console.WriteLine(eFeature.BlockUI)
        Console.WriteLine(eFeature.LightBox)
        Console.WriteLine(eFeature.LightBox Or eFeature.BlockUI)

        Return
        ' ------------------------------------------------------------------------------
        ' Setup testing Setting
        ' ------------------------------------------------------------------------------
        Dim testingSettings As New List(Of TestingSetting)

        ' ORACLE-Testing
        'testingSettings.Add(New TestingSetting(My.Resources.oracle_connectionString, XDatabase.DatabaseMode.Oracle, My.Resources.oracle_querySQL))
        ' SQLite-Testing
        testingSettings.Add(New TestingSetting(My.Resources.sqlite_connectionString, XDatabase.DatabaseMode.Sqlite, My.Resources.sqlite_querySQL))
        ' OleDb-Testing
        'testingSettings.Add(New TestingSetting(My.Resources.OleDb_connectionString, XDatabase.DatabaseMode.OleDb, My.Resources.OleDb_querySQL))
        ' mysql-Testing
        'testingSettings.Add(New TestingSetting(My.Resources.mysql_connectionString, XDatabase.DatabaseMode.MySql, My.Resources.mysql_querySQL))

        For Each ts As TestingSetting In testingSettings
            Dim xDB As XDatabase = New XDatabase(ts.dbMode, ts.connectionString)
            Try

                Console.WriteLine("==================================================")
                Console.WriteLine("DataBase Type     : {0}", ts.dbMode.ToString())
                Console.WriteLine("Connection String : {0}", ts.connectionString)
                Console.WriteLine("Select SQL        : {0}", ts.querySQL)
                Console.WriteLine("==================================================")
                Console.WriteLine("(1) Display data")
                Dim dt As DataTable = xDB.SelectSQL(ts.querySQL)
                dt.Display()
                Console.WriteLine("")

                'Console.WriteLine("(2) GetSchemaTable")
                'dt = xDB.GetSchemaTable(ts.querySQL)
                'dt.Display()
                'Console.WriteLine("")

                'Dim recordCount As Integer = xDB.SelectRecordCountSQL(ts.querySQL)
                Dim recordCount As Integer = xDB.SelectRecordCountSQL("Casd")
                Console.WriteLine("(2) Record Count:{0}", recordCount)

                Console.WriteLine("")
            Catch ex As Exception
                Console.WriteLine("--------------------------------------------------")
                Console.WriteLine("error : " & ex.Message)
                Console.WriteLine("--------------------------------------------------")
            Finally
                xDB = Nothing
            End Try
        Next
    End Sub

    Private Sub func1()
        Try
            func2()
        Catch ex As Exception
            Dim o As String = Nothing
            Dim a As Object = Nothing
        End Try
    End Sub

    Private Sub func2()
        Try
            func3()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub func3()
        Throw New ArgumentException("from func3")
    End Sub
End Module


