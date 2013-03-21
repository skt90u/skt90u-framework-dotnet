Imports System.Runtime.CompilerServices
Imports System.Collections.Specialized
Imports System.Web.UI.WebControls

''' <summary>Enhance WebControl functionality</summary>
Public Module ExtWebControl

    <Extension()> _
    Public Sub AppendClass(ByVal wc As WebControl, ByVal className As String)
        If Not wc.CssClass.Contains(className) Then
            If Not String.IsNullOrEmpty(wc.CssClass) Then
                className = " " & className
            End If
            wc.CssClass = wc.CssClass & className
        End If
    End Sub

    <Extension()> _
    Public Sub RemoveClass(ByVal wc As WebControl, ByVal className As String)
        wc.CssClass = wc.CssClass.Replace(className, String.Empty)
        wc.CssClass = wc.CssClass.Trim()
    End Sub

    <Extension()> _
    Public Sub AppendClass(ByVal tis As Style, ByVal className As String)
        If Not tis.CssClass.Contains(className) Then
            If Not String.IsNullOrEmpty(tis.CssClass) Then
                className = " " & className
            End If
            tis.CssClass = tis.CssClass & className
        End If
    End Sub

    <Extension()> _
    Public Sub RemoveClass(ByVal tis As Style, ByVal className As String)
        tis.CssClass = tis.CssClass.Replace(className, String.Empty)
        tis.CssClass = tis.CssClass.Trim()
    End Sub
End Module
