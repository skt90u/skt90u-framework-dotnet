Imports System.Web.UI.WebControls
Imports AjaxControlToolkit


Public Class NumEdit
    Inherits CompositeControl

    ''' <summary>
    ''' 設定輸入最大長度, 當有使用RangeValidator時, 且NumType為整數
    ''' 會自動設定合適的最大長度
    ''' </summary>
    Public Property MaxLength() As Integer
        Get
            Dim o As Object = ViewState("MaxLength")
            If o Is Nothing Then
                o = 0
            End If

            If UsingRangeValidator AndAlso NumType.Equals(eNumType.Integer) Then
                Return MaximumValue.Length
            Else
                Return CType(o, Integer)
            End If
        End Get
        Set(ByVal value As Integer)
            ViewState("MaxLength") = value
        End Set
    End Property

    ''' <summary>定義RangeValidator的MaximumValue</summary>
    Public Property MaximumValue() As String
        Get
            Dim o As Object = ViewState("MaximumValue")
            If o Is Nothing Then
                o = String.Empty
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("MaximumValue") = value
        End Set
    End Property

    ''' <summary>定義RangeValidator的MinimumValue</summary>
    Public Property MinimumValue() As String
        Get
            Dim o As Object = ViewState("MinimumValue")
            If o Is Nothing Then
                o = String.Empty
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("MinimumValue") = value
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

    ''' <summary>指定Validator為哪一個要驗證的群組</summary>
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
            EnsureChildControls()
            Return tb.Text
        End Get
        Set(ByVal value As String)
            EnsureChildControls()
            tb.Text = value
        End Set
    End Property

    ''' <summary>取得或設定目前的NumEdit所要使用的類型</summary>
    Public Property NumType() As eNumType
        Get
            Dim o As Object = ViewState("NumType")
            If o Is Nothing Then
                o = DefNumType
            End If
            Return CType(o, eNumType)
        End Get
        Set(ByVal value As eNumType)
            ViewState("NumType") = value
        End Set
    End Property

#Region "private member & property & enum"

    ''' <summary>定義目前NumEdit支援的類型</summary>
    Enum eNumType
        [Integer] = 0
        [Double]
    End Enum

    ''' <summary>預設為整數</summary>
    Private Const DefNumType As eNumType = eNumType.Integer

    ''' <summary>RequiredValidator錯誤時,顯示的內容</summary>
    Private ReadOnly RequiredErrorMessage As String = "此為必填欄位"

    ''' <summary>當滑鼠mouseover到textbox時,顯示的ToolTip</summary>
    Private ReadOnly Property Tip() As String
        Get
            Dim message As String = ""
            If Required Then
                message += RequiredErrorMessage
            End If

            If UsingRangeValidator Then
                message += ",且"
                message += RangeErrorMessage
            End If
            Return message
        End Get
    End Property

    ''' <summary>RangeValidator錯誤時,顯示的內容</summary>
    Private ReadOnly Property RangeErrorMessage() As String
        Get
            Return String.Format("輸入數值必須在{0}到{1}之間", MinimumValue, MaximumValue)
        End Get
    End Property

    ''' <summary>判斷是否使用RangeValidator,必須定義MaximumValue,以及MinimumValue</summary>
    Private ReadOnly Property UsingRangeValidator() As Boolean
        Get
            Return Not String.IsNullOrEmpty(MaximumValue) AndAlso Not String.IsNullOrEmpty(MinimumValue)
        End Get
    End Property

    Private tb As New TextBox
    Private filter As New FilteredTextBoxExtender

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

#Region "CreateChildControls"
    ''' <summary>定義複合控件架構</summary>
    Protected Overrides Sub CreateChildControls()
        tb.ID = "tb"
        tb.CssClass = CssClass
        tb.MaxLength = MaxLength
        If UsingRangeValidator Then
            tb.ToolTip = Tip
        End If
        Controls.Add(tb)

        filter.ID = "filter"
        filter.TargetControlID = tb.ID
        Select Case NumType
            Case eNumType.Integer
                filter.FilterType = FilterTypes.Numbers
                Exit Select
            Case eNumType.Double
                ' 設定當數值型態為Double時, 必須支援的keycode
                filter.FilterType = FilterTypes.Numbers Or FilterTypes.Custom
                filter.FilterMode = FilterModes.ValidChars
                filter.ValidChars = "."
                Exit Select
        End Select
        Controls.Add(filter)

        ' 如果有定義最大值最小值範圍, 就設定RangeValidator
        If UsingRangeValidator Then
            Dim rangeV As New RangeValidator
            rangeV.ID = "rgV"
            rangeV.ControlToValidate = tb.ID
            rangeV.Display = ValidatorDisplay.Dynamic
            rangeV.ValidationGroup = ValidationGroup
            rangeV.MaximumValue = MaximumValue
            rangeV.MinimumValue = MinimumValue
            Select Case NumType
                Case eNumType.Integer
                    rangeV.Type = ValidationDataType.Integer
                    Exit Select
                Case eNumType.Double
                    rangeV.Type = ValidationDataType.Double
                    Exit Select
            End Select
            rangeV.ErrorMessage = RangeErrorMessage
            Controls.Add(rangeV)
        End If

        ' 如果有設定Required為True, 就加入RequiredFieldValidator
        If Required Then
            Dim requiredV As New RequiredFieldValidator
            requiredV.ID = "requiredV"
            requiredV.ControlToValidate = tb.ID
            requiredV.Display = ValidatorDisplay.Dynamic
            requiredV.ValidationGroup = ValidationGroup
            requiredV.ErrorMessage = RequiredErrorMessage
            Controls.Add(requiredV)
        End If
    End Sub
#End Region

End Class
