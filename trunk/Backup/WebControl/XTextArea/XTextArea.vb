Imports System.Web.UI.WebControls
Imports System.Text

Public Class XTextArea
    Inherits System.Web.UI.WebControls.TextBox

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)

        TextMode = TextBoxMode.MultiLine

        Dim type As Type = Me.GetType()

        ClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControl.jquery.textareaCounter.css")

        ClientScriptManager.RegisterEmbeddedJs(Type, "JFramework.WebControl.jquery-1.4.4.js")

        Dim lstWebResource As New List(Of String)
        lstWebResource.Add("JFramework.WebControl.jquery.textareaCounter.js")
        lstWebResource.Add("JFramework.WebControl.jquery.elastic.source.js")
        ClientScriptManager.RegisterCompositeScript(lstWebResource)

        Dim scripts As String = GenApplicationLoadScript()
        ClientScriptManager.RegisterClientApplicationLoadScript(Me, scripts)
    End Sub

    Private Function GenApplicationLoadScript() As String
        Dim scripts As New StringBuilder()
        ' --------------------------------------------------
        ' textareaCounter
        ' --------------------------------------------------
        scripts.Append("var options = {")
        scripts.Append("'maxCharacterSize':  " & maxCharacterSize & ",")
        scripts.Append("'originalStyle'   : '" & originalStyle & "',")
        scripts.Append("'warningStyle'    : '" & warningStyle & "',")
        scripts.Append("'warningNumber'   :  " & warningNumber & ",")
        scripts.Append("'displayFormat'   : '" & displayFormat & "'")
        scripts.Append("};")
        scripts.Append("$('#" & ClientID & "').textareaCount(options);")
        ' --------------------------------------------------
        ' elastic
        ' --------------------------------------------------
        scripts.Append("$('#" & ClientID & "').elastic();")
        Return scripts.ToString()
    End Function

#Region "displayFormat"
    Public Property displayFormat() As String
        Get
            Return GetPropertyValue(Of String)("displayFormat", "目前字數: #input, 剩餘字數: #left, 容許字數: #max")
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("displayFormat", value)
        End Set
    End Property
#End Region
#Region "maxCharacterSize"
    Private ReadOnly Property maxCharacterSize() As String
        Get
            If MaxLength.Equals(0) Then
                Return -1
            End If
            Return MaxLength.ToString()
        End Get
    End Property
#End Region
#Region "warningNumber"
    Public Property warningNumber() As String
        Get
            Return GetPropertyValue(Of String)("warningNumber", "0")
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("warningNumber", value)
        End Set
    End Property
#End Region
#Region "warningStyle"
    Public Property warningStyle() As String
        Get
            Return GetPropertyValue(Of String)("warningStyle", "warningTextareaInfo")
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("warningStyle", value)
        End Set
    End Property
#End Region
#Region "originalStyle"
    Public Property originalStyle() As String
        Get
            Return GetPropertyValue(Of String)("originalStyle", "originalTextareaInfo")
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("originalStyle", value)
        End Set
    End Property
#End Region

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
End Class

