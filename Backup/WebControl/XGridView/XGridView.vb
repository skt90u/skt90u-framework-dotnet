Imports System.Web.UI.WebControls
Imports System.Text

Public Class XGridView
    Inherits GridView

    Public Enum eGridViewStyle As Integer
        ChromeBlack = 0
        Chrome
        Gamer
        GlassBlack
        SoftGrey
        WhiteChrome
        Yahoo
    End Enum

    Public Property GridViewStyle() As eGridViewStyle
        Get
            Return GetPropertyValue(Of eGridViewStyle)("GridViewStyle", eGridViewStyle.ChromeBlack)
        End Get
        Set(ByVal value As eGridViewStyle)
            SetPropertyValue(Of eGridViewStyle)("GridViewStyle", value)
        End Set
    End Property

    Protected Function GetPropertyValue(Of V)(ByVal propertyName As String, ByVal nullValue As V) As V
        Dim propertyValue As Object = ViewState(propertyName)
        If propertyValue Is Nothing Then
            Return nullValue
        End If
        Return DirectCast(propertyValue, V)
    End Function

    Protected Sub SetPropertyValue(Of V)(ByVal propertyName As String, ByVal value As V)
        ViewState(propertyName) = value
    End Sub

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        SetupStyle(Me)
    End Sub

    Private Sub SetupStyle(ByVal grv As GridView)
        grv.AppendClass("GridViewStyle")
        grv.HeaderStyle.AppendClass("HeaderStyle")
        grv.PagerStyle.AppendClass("PagerStyle")
        grv.RowStyle.AppendClass("RowStyle")
        grv.AlternatingRowStyle.AppendClass("AltRowStyle")
        grv.SelectedRowStyle.AppendClass("SelectedRowStyle")
    End Sub

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)

        Dim thisType As Type = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType

        Dim type As Type = Me.GetType()
        ClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControl.XGridView.css")
        ClientScriptManager.RegisterEmbeddedJs(type, "JFramework.WebControl.jquery-1.4.4.js")
        Dim scripts As String = GenApplicationLoadScript()
        ClientScriptManager.RegisterClientApplicationLoadScript(Me, scripts)
    End Sub

    Private Function GenApplicationLoadScript() As String
        Dim scripts As New StringBuilder()
        Dim jsWrap As String = String.Format("$('#{0}').wrap('<div class=\'{1}\'></div>');", ClientID, GridViewStyle.ToString())
        scripts.Append(jsWrap)
        Dim js As String = scripts.ToString()
        Return js
    End Function
End Class
