Imports System.Web.Script.Serialization

Public Class WebControlUtil
    Public Shared Function Deserialize(Of T)(ByVal jsonStr) As T
        ' [deserialize json string]
        ' http://weblogs.asp.net/hajan/archive/2010/07/23/javascriptserializer-dictionary-to-json-serialization-and-deserialization.aspx
        ' [deserialize json string to List(Of OrderDictionary)]
        ' http://stackoverflow.com/questions/402996/deserializing-json-objects-as-listtype-not-working-with-asmx-service
        Dim Result As T = Nothing
        Try
            Dim deserializer As New JavaScriptSerializer()
            Result = deserializer.Deserialize(Of T)(jsonStr)
        Catch ex As Exception
            Throw
        End Try
        Return Result
    End Function

    Public Shared Function Serialize(ByVal obj As Object) As String
        Dim Result As String = Nothing
        Try
            Dim serializer As New JavaScriptSerializer()
            Result = serializer.Serialize(obj)
        Catch ex As Exception
            Throw
        End Try
        Return Result
    End Function

End Class
