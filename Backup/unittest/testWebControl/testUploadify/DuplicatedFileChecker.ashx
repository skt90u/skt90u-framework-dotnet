<%@ WebHandler Language="VB" Class="DuplicatedFileChecker" %>

Imports System
Imports System.Web

Public Class DuplicatedFileChecker : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
    JFramework.WebControl.uploadify.CheckDuplicatedFile(context)
  End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class