Imports System.Web.UI
Imports System.Web

Public Class BrowserDetection

    Public Shared Function IsIE() As Boolean
        Return BrowserInfo.Browser.Equals("IE")
    End Function

    Private Shared ReadOnly Property BrowserInfo() As HttpBrowserCapabilities
        Get
            Return CurPage.Request.Browser
        End Get
    End Property

    Private Shared ReadOnly Property CurPage() As Page
        Get
            Return CType(HttpContext.Current.Handler, Page)
        End Get
    End Property
End Class
