Imports System.Runtime.CompilerServices
Imports System.Web.UI
Imports System.Web.UI.HtmlControls

''' <summary>Enhance Page functionality</summary>
Public Module ExtPage

    ''' <summary>insert css-link in head tag</summary>
    <Extension()> _
    Public Sub RegisterCSS(ByVal aPage As Page, ByVal csslocation As String)
        If String.IsNullOrEmpty(csslocation) Then Return
        If ContainsStyleLink(aPage, csslocation) Then Return
        Dim cssLink As Control = CreateStyleLink(csslocation)
        aPage.Header.Controls.Add(cssLink)
    End Sub

    ''' <summary>a helper function for create a css-link control</summary>
    Private Function CreateStyleLink(ByVal href As String) As Control
        Dim link As New HtmlLink()
        link.Attributes.Add("rel", "Stylesheet")
        link.Attributes.Add("type", "text/css")
        link.Attributes.Add("href", href)
        Return link
    End Function

    Private Function ContainsStyleLink(ByVal aPage As Page, ByVal href As String) As Boolean
        For Each control As Control In aPage.Header.Controls
            If TypeOf control Is HtmlLink AndAlso TryCast(control, HtmlLink).Href = href Then
                Return True
            End If
        Next
        Return False
    End Function
End Module
