Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Text
Imports AjaxControlToolkit

Public Class ddcombo
    Inherits System.Web.UI.WebControls.CompositeControl

#Region "Private"
    Private table As New Table
    Private tr As New TableRow
    Private td1 As New TableCell
    Private pnl As New Panel
    Private tb As New TextBox
    Private td2 As New TableCell
    Private hl As New HyperLink
    Private img As New Image
    Private ddl As New DropDownList

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        If Page.IsPostBack Then
            EnsureChildControls()
        End If
    End Sub

    Protected Overrides Sub CreateChildControls()
        Dim type As Type = Me.GetType()

        table.CssClass = "ddcombo_table"
        table.Attributes("cellspacing") = "0"
        table.Attributes("cellpadding") = "0"
        table.Attributes("border") = "0"
        table.ID = "combotable"

        td1.CssClass = "ddcombo_td1"

        pnl.CssClass = "ddcombo_div4"
        pnl.Attributes("style") = "background: url(" + ClientScriptManager.GetWebResourceUrl(type, "JFramework.WebControl.transparent_pixel.gif") + ")"

        tb.CssClass = "ddcombo_input1"
        tb.Attributes("title") = InitValue
        tb.Attributes("value") = InitValue
        tb.Attributes("style") = "color: gray; background: url(" + ClientScriptManager.GetWebResourceUrl(type, "JFramework.WebControl.transparent_pixel.gif") + ")"
        tb.ID = "combotable_input"

        td2.CssClass = "ddcombo_td2"
        td2.Attributes("valign") = "top"
        td2.Attributes("align") = "left"
        td2.ID = "combotable_button"

        img.Attributes("src") = ClientScriptManager.GetWebResourceUrl(type, "JFramework.WebControl.button2.png")
        img.Attributes("style") = "display: none;"

        pnl.Controls.Add(tb)
        td1.Controls.Add(pnl)
        td2.Controls.Add(hl)
        td2.Controls.Add(img)
        tr.Controls.Add(td1)
        tr.Controls.Add(td2)
        table.Controls.Add(tr)

        Controls.Add(table)

        ddl.Attributes("style") = "display: none;"
        Controls.Add(ddl)

        If Required Then
            AddRequiredValidator()
        End If

        If Not Page.IsPostBack Then
            DataBind()
        End If
    End Sub

    Private Function GenApplicationLoadScript() As String
        Dim scripts As New StringBuilder()
        scripts.Append("var data = [];")
        scripts.Append("$('#" & ddl.ClientID & " option').each(function() {")
        scripts.Append("data.push($(this).text());")
        scripts.Append("});")
        scripts.Append("$('#" & ClientID & "').ddcombo({")
        scripts.Append("minChars: 0,")

        scripts.Append("mustMatch: " & MustMatch.ToString().ToLower() & ",")
        scripts.Append("options: data,")
        scripts.Append("fnSelectChanged: " & OnClientSelectedIndexChanged)
        scripts.Append("});")
        Dim js As String = scripts.ToString()
        Return js
    End Function

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        Dim type As Type = Me.GetType()

        AppendClass("ddcombo")
        ClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControl.jquery.ddcombo.css")

        ClientScriptManager.RegisterEmbeddedJs(type, "JFramework.WebControl.jquery-1.4.4.js")

        Dim lstWebResource As New List(Of String)
        lstWebResource.Add("JFramework.WebControl.jquery.ready.js")
        lstWebResource.Add("JFramework.WebControl.jquery.flydom-3.1.1.js")
        lstWebResource.Add("JFramework.WebControl.jquery.bgiframe.min.js")
        lstWebResource.Add("JFramework.WebControl.jquery.dimensions.js")
        lstWebResource.Add("JFramework.WebControl.jquery.ajaxQueue.js")
        lstWebResource.Add("JFramework.WebControl.thickbox-compressed.js")
        lstWebResource.Add("JFramework.WebControl.jquery.ddcombo.js")
        ClientScriptManager.RegisterCompositeScript(lstWebResource)

        Dim scripts As String = GenApplicationLoadScript()
        ClientScriptManager.RegisterClientApplicationLoadScript(Me, scripts)
    End Sub

    Protected Overrides ReadOnly Property TagKey() As System.Web.UI.HtmlTextWriterTag
        Get
            Return HtmlTextWriterTag.Div
        End Get
    End Property

    Private Sub AddRequiredValidator()
        Dim rv As RequiredFieldValidator = New RequiredFieldValidator()
        rv.ID = "RequiredFieldValidator_" & tb.ID
        rv.InitialValue = InitValue
        rv.ErrorMessage = ErrorMessage
        rv.ControlToValidate = tb.ID

        If Not String.IsNullOrEmpty(ValidationGroup) Then
            rv.ValidationGroup = ValidationGroup
        End If

        'rv.SetFocusOnError = SetFocusOnError
        If UseCalloutExtender OrElse UseValidationSummary Then
            rv.Display = ValidatorDisplay.None
        End If
        Controls.Add(rv)

        If UseCalloutExtender Then
            Dim vc As ValidatorCalloutExtender = New ValidatorCalloutExtender()
            vc.ID = "ValidatorCalloutExtender_" & tb.ID
            vc.TargetControlID = rv.ID
            Controls.Add(vc)
        End If
    End Sub
#End Region

#Region "InitValue"
    Public Property InitValue() As String
        Get
            Return GetPropertyValue(Of String)("InitValue", "請輸入")
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InitValue", value)
        End Set
    End Property
#End Region
#Region "DataBind"
    Public Property DataValueField() As String
        Get
            Return ddl.DataValueField
        End Get
        Set(ByVal value As String)
            ddl.DataValueField = value
        End Set
    End Property

    Public Property DataTextField() As String
        Get
            Return ddl.DataTextField
        End Get
        Set(ByVal value As String)
            ddl.DataTextField = value
        End Set
    End Property

    Public Property DataSource() As Object
        Get
            Return ddl.DataSource
        End Get
        Set(ByVal value As Object)
            ddl.DataSource = value
        End Set
    End Property

    Public Overrides Sub DataBind()
        ddl.DataBind()
    End Sub
#End Region
#Region "Text"
    ''' <summary>取得ddcombo目前文字</summary>
    Public Property Text() As String
        Get
            Dim txt As String = tb.Text
            If InitValue.Equals(txt) Then
                txt = String.Empty
            End If
            Return txt
        End Get
        Set(ByVal value As String)
            tb.Text = value
        End Set
    End Property
#End Region
#Region "Value"
    ''' <summary>取得ddcombo目前文字對應的值</summary>
    Public Property Value() As Object
        Get
            For Each aItem As ListItem In ddl.Items
                If aItem.Text.Equals(Text) Then
                    Return aItem.Value
                End If
            Next
            Return Text
        End Get
        Set(ByVal value As Object)

            Dim sValue As String = String.Empty

            If Not IsDBNull(value) Then sValue = value.ToString()

            '必須在 DataBound()之後設值, 否則會無法Match到Text
            For Each aItem As ListItem In ddl.Items
                If aItem.Value.Equals(sValue) Then
                    sValue = aItem.Text
                    Exit For
                End If
            Next

            If String.IsNullOrEmpty(sValue) Then
                sValue = InitValue
            End If

            Text = sValue
        End Set
    End Property
#End Region
#Region "MustMatch"
    Public Property MustMatch() As Boolean
        Get
            Return GetPropertyValue(Of Boolean)("Required", False)
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Required", value)
        End Set
    End Property
#End Region
#Region "Required"
    Public Property Required() As Boolean
        Get
            Return GetPropertyValue(Of Boolean)("Required", False)
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Required", value)
        End Set
    End Property
#End Region
#Region "ErrorMessage"
    Public Property ErrorMessage() As String
        Get
            Return GetPropertyValue(Of String)("ErrorMessage", "請輸入")
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ErrorMessage", value)
        End Set
    End Property
#End Region
#Region "ValidationGroup"
    ''' <summary>驗證錯誤時顯示的訊息</summary>
    Public Property ValidationGroup() As String
        Get
            Return GetPropertyValue(Of String)("ErrorMessage", String.Empty)
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ErrorMessage", value)
        End Set
    End Property
#End Region
#Region "UseValidationSummary"
    ''' <summary>是否使用UseValidationSummary輸入驗證錯誤訊息</summary>
    Public Property UseValidationSummary() As Boolean
        Get
            Return GetPropertyValue(Of Boolean)("UseValidationSummary", Nothing)
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("UseValidationSummary", value)
        End Set
    End Property
#End Region
#Region "UseCalloutExtender"
    ''' <summary>是否使用ValidatorCalloutExtender呈現驗證錯誤的結果</summary>
    Public Property UseCalloutExtender() As Boolean
        Get
            Return GetPropertyValue(Of Boolean)("UseCalloutExtender", False)
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("UseCalloutExtender", value)
        End Set
    End Property
#End Region
#Region "OnClientSelectedIndexChanged"
    ''' <summary>設定當選取改變時要觸發的JavaScript函式</summary>
    Public Property OnClientSelectedIndexChanged() As String
        Get
            Return GetPropertyValue(Of String)("OnClientSelectedIndexChanged", "null")
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OnClientSelectedIndexChanged", value)
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

