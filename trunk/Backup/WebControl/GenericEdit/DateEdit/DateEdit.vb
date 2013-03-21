Imports System.Web.UI.WebControls
Imports AjaxControlToolkit
Imports JFramework

Public Class DateEdit
    Inherits CompositeControl

    ''' <summary>只能依靠CalendarExtender選取日期</summary>
    Public Property [ReadOnly]() As Boolean
        Get
            Dim o As Object = ViewState("ReadOnly")
            If o Is Nothing Then
                o = False
            End If
            Return CType(o, Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("ReadOnly") = value
        End Set
    End Property

    ''' <summary>是否為必填欄位</summary>
    Public Property Required() As Boolean
        Get
            Dim o As Object = ViewState("Required")
            If o Is Nothing Then
                o = False
            End If
            Return CType(o, Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("Required") = value
        End Set
    End Property

    ''' <summary>指定驗證的群組</summary>
    Public Property ValidationGroup() As String
        Get
            Dim o As Object = ViewState("ValidationGroup")
            If o Is Nothing Then
                o = String.Empty
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("ValidationGroup") = value
        End Set
    End Property

    ''' <summary>與實際的textbox界接</summary>
    Public Property Text() As String
        Get
            Return tb.Text
        End Get
        Set(ByVal value As String)
            tb.Text = value
        End Set
    End Property

#Region "private member & property & enum"
    Private Const FormatErrorMessage As String = "日期不合法"
    Private Const RequiredErrorMessage As String = "請輸入日期"
    Private Const DateExpression As String = "^(____/__/__)|((19|20|21)\d\d[/](0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01]))$"

    Private tb As New TextBox
    Private editMask As New MaskedEditExtender
    Private ib As New ImageButton
    Private ce As New CalendarExtender
    Private re As New RegularExpressionValidator
#End Region

#Region "OnLoad"
    ''' <summary></summary>
    ''' <remarks>TODO: Page.IsPostBack 是否足夠判斷在UpdatePanel之內的控件</remarks>
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        If Page.IsPostBack Then
            EnsureChildControls()
        End If
    End Sub
#End Region

#Region "OnPreRender"
    ''' <remarks>在此註冊css, javascript</remarks>
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)
        Dim type As Type = Me.GetType()
        ClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControl.DateEdit.css")
    End Sub
#End Region

#Region "CreateChildControls"
    ''' <summary>定義複合控件架構</summary>
    Protected Overrides Sub CreateChildControls()
        tb.ID = "tb"
        tb.AppendClass("text")
        tb.AppendClass(CssClass)
        If [ReadOnly] Then tb.Attributes("readonly") = "readonly"

        editMask.ID = "editMask"
        editMask.TargetControlID = tb.ID
        editMask.Mask = "9999/99/99"
        editMask.AutoComplete = True
        editMask.ClearMaskOnLostFocus = False

        ib.ID = "ib"
        ib.AppendClass("image")
        ib.TabIndex = -1
        ib.ImageUrl = ClientScriptManager.GetWebResourceUrl(Me.GetType(), "JFramework.WebControl.Calendar.png")
        ib.CausesValidation = False

        ce.ID = "ce"
        ce.TargetControlID = tb.ID
        ce.PopupButtonID = ib.ID
        ce.Format = "yyyy/MM/dd"

        re.ID = "re"
        re.ControlToValidate = tb.ID
        re.SetFocusOnError = False
        re.ErrorMessage = FormatErrorMessage
        re.Display = ValidatorDisplay.Dynamic
        re.ValidationExpression = DateExpression
        re.ValidationGroup = ValidationGroup

        CssClass = "dateEdit"

        Controls.Add(tb)
        Controls.Add(editMask)
        Controls.Add(ib)
        Controls.Add(ce)
        Controls.Add(re)

        If Required Then
            Dim rv As New RequiredFieldValidator
            rv.ID = "rv"
            rv.ControlToValidate = tb.ID
            rv.InitialValue = "____/__/__"
            rv.ErrorMessage = RequiredErrorMessage
            rv.Display = ValidatorDisplay.Dynamic
            rv.SetFocusOnError = False
            rv.ValidationGroup = ValidationGroup
            Controls.Add(rv)
        End If
    End Sub
#End Region

End Class


