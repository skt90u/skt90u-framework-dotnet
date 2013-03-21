Imports System.Text
Imports System.Threading
Imports System.Runtime.CompilerServices
Imports System.Web
Imports System.Web.UI.WebControls

''' <summary>Enhance gridview functionality</summary>
Public Module ExtGridview

    ' ----------------------------------------------------------'
    ' The requirements of using ExportExcel/ExportWord function
    ' ----------------------------------------------------------'
    '   - if you run ExportExcel/ExportWord functions 
    '     in codebehide, your derived page class must override 
    '     VerifyRenderingInServerForm subroutine, and do nothing.
    '     [ref : http://www.c-sharpcorner.com/uploadfile/dipalchoksi/exportxl_asp2_dc11032006003657am/exportxl_asp2_dc.aspx]
    '
    '   - ExportExcel/ExportWord functions must run under 
    '     PostBack mode, which mean if you put a button inside 
    '     update panel that can raise ExportExcel/ExportWord
    '     you need set the a PostBackTrigger like follow
    '     <asp:PostBackTrigger ControlID="$(your button id)" />
    '     [ref : http://nice-tutorials.blogspot.com/2009/06/export-gridview-to-excel-within-update.html]
    ' ----------------------------------------------------------'

    ''' <summary>export gridview content to a excel file</summary>
    <Extension()> _
    Public Sub ExportExcel(ByVal grv As GridView, ByVal fileName As String)
        grv.Export(Encoding.UTF8, fileName, "application/vnd.ms-excel")
    End Sub

    ''' <summary>export gridview content to a word file</summary>
    <Extension()> _
    Public Sub ExportWord(ByVal grv As GridView, ByVal fileName As String)
        grv.Export(Encoding.UTF8, fileName, "application/ms-word")
    End Sub

#Region "core of Export"
    <Extension()> _
    Private Sub Export(ByVal grv As GridView, ByVal enc As Encoding, ByVal fileName As String, ByVal contentType As String)
        If grv.Rows.Count > 65535 Then
            Dim err As String = String.Format("Out of range! the size of data is {0}, can not exceed 65535.", grv.Rows.Count)
            Throw New ArgumentException(err)
        End If

        Dim response As HttpResponse = HttpContext.Current.Response
        Try
            fileName = HttpUtility.UrlEncode(fileName, enc)
            response.Clear()
            response.Cache.SetCacheability(HttpCacheability.NoCache)
            response.Charset = enc.WebName
            response.ContentEncoding = enc
            response.ContentType = contentType

            Dim sText As String = String.Format("<meta http-equiv='Content-Type'; content='{0}';charset='{1}'>", contentType, enc.WebName)
            response.Write(sText)
            response.AddHeader("content-disposition", "attachment;filename=" & fileName)
            Dim stringWriter As New System.IO.StringWriter()
            Dim htmlWriter As New System.Web.UI.HtmlTextWriter(stringWriter)
            grv.RenderControl(htmlWriter)
            response.Write(stringWriter.ToString())
            response.Flush()
            response.End()
        Catch ex As ThreadAbortException
            ' http://msdn.microsoft.com/zh-tw/library/ms182363.aspx
            ' CA1002
            'Throw ex
            Throw
        End Try
    End Sub
#End Region

End Module
