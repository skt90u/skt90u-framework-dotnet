Imports System.Web
Imports System.Web.Services
Imports System.IO

Public Class UploadHandler
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        context.Response.Charset = "utf-8"

        Dim file As HttpPostedFile = context.Request.Files("Filedata")
        Dim uploadPath As String = HttpContext.Current.Server.MapPath(context.Request("folder")) & "\"

        If file IsNot Nothing Then
            If Not Directory.Exists(uploadPath) Then
                Directory.CreateDirectory(uploadPath)
            End If
            file.SaveAs(uploadPath + file.FileName)
            context.Response.Write("1")
        Else
            context.Response.Write("0")
        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class