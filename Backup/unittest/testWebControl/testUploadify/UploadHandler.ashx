<%@ WebHandler Language="VB" Class="UploadHandler" %>

Imports System
Imports System.Web

Public Class UploadHandler : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
    JFramework.WebControl.uploadify.Handle(context)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class