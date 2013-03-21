Imports System.Collections
Imports System.Reflection
Imports System.Web.UI
Imports System.Web.Configuration
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

''' <summary>
''' Provides static helper methods to CSSFriendly controls. Singleton instance.
''' </summary>
<Obsolete("主要是拿來參考而已, 仍然以JFramework.WebControl.ClientScriptManager進行操作")> _
Public Class Helpers
    ''' <summary>
    ''' Private constructor forces singleton.
    ''' </summary>
    Private Sub New()
    End Sub

    Public Shared Function GetListItemIndex(ByVal control As ListControl, ByVal item As ListItem) As Integer
        Dim index As Integer = control.Items.IndexOf(item)
        If index = -1 Then
            Throw New NullReferenceException("ListItem does not exist ListControl.")
        End If

        Return index
    End Function

    Public Shared Function GetListItemClientID(ByVal control As ListControl, ByVal item As ListItem) As String
        If control Is Nothing Then
            Throw New ArgumentNullException("Control can not be null.")
        End If

        Dim index As Integer = GetListItemIndex(control, item)

        Return [String].Format("{0}_{1}", control.ClientID, index.ToString())
    End Function

    Public Shared Function GetListItemUniqueID(ByVal control As ListControl, ByVal item As ListItem) As String
        If control Is Nothing Then
            Throw New ArgumentNullException("Control can not be null.")
        End If

        Dim index As Integer = GetListItemIndex(control, item)

        Return [String].Format("{0}${1}", control.UniqueID, index.ToString())
    End Function

    Public Shared Function HeadContainsLinkHref(ByVal page As Page, ByVal href As String) As Boolean
        If page Is Nothing Then
            Throw New ArgumentNullException("page")
        End If

        For Each control As Control In page.Header.Controls
            If TypeOf control Is HtmlLink AndAlso TryCast(control, HtmlLink).Href = href Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Shared Sub RegisterEmbeddedCSS(ByVal css As String, ByVal type As Type, ByVal page As Page)
        Dim filePath As String = page.ClientScript.GetWebResourceUrl(type, css)

        ' if filePath is not empty, embedded CSS exists -- register it
        If Not [String].IsNullOrEmpty(filePath) Then
            If Not Helpers.HeadContainsLinkHref(page, filePath) Then
                Dim link As New HtmlLink()
                link.Href = page.ResolveUrl(filePath)
                link.Attributes("type") = "text/css"
                link.Attributes("rel") = "stylesheet"
                page.Header.Controls.Add(link)
            End If
        End If
    End Sub

    Public Shared Sub RegisterClientScript(ByVal resource As String, ByVal type As Type, ByVal page As Page)
        Dim filePath As String = page.ClientScript.GetWebResourceUrl(type, resource)

        ' if filePath is empty, set to filename path
        If [String].IsNullOrEmpty(filePath) Then
            Dim folderPath As String = WebConfigurationManager.AppSettings.[Get]("CSSFriendly-JavaScript-Path")
            If [String].IsNullOrEmpty(folderPath) Then
                folderPath = "~/JavaScript"
            End If
            filePath = If(folderPath.EndsWith("/"), folderPath & resource, folderPath & "/" & resource)
        End If

        If Not page.ClientScript.IsClientScriptIncludeRegistered(type, resource) Then
            page.ClientScript.RegisterClientScriptInclude(type, resource, page.ResolveUrl(filePath))
        End If
    End Sub

    ''' <summary>
    ''' Gets the value of a non-public field of an object instance. Must have Reflection permission.
    ''' </summary>
    ''' <param name="container">The object whose field value will be returned.</param>
    ''' <param name="fieldName">The name of the data field to get.</param>
    ''' <remarks>Code initially provided by LonelyRollingStar.</remarks>
    Public Shared Function GetPrivateField(ByVal container As Object, ByVal fieldName As String) As Object
        Dim type As Type = container.[GetType]()
        Dim fieldInfo As FieldInfo = type.GetField(fieldName, BindingFlags.NonPublic Or BindingFlags.Instance)
        Return (If(fieldInfo Is Nothing, Nothing, fieldInfo.GetValue(container)))
    End Function
End Class
