Imports System.Web.UI.WebControls

Public Class GenericEdit
    Inherits CompositeControl

    Private Const DefEditType As eEditType = eEditType.Date

    Public Enum eEditType As Int32
        [Date] = 0
        [Integer]
    End Enum

    Public Property EditType() As eEditType
        Get
            Dim o As Object = ViewState("EditType")
            If o Is Nothing Then
                o = DefEditType
            End If
            Return CType(o, eEditType)
        End Get
        Set(ByVal value As eEditType)
            ViewState("EditType") = value
        End Set
    End Property

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        If Page.IsPostBack Then
            EnsureChildControls()
        End If
    End Sub

    Protected Overrides Sub CreateChildControls()
        Dim input As Object = Nothing
        Select Case EditType
            Case eEditType.Date
                input = New DateEdit
                Exit Select
            Case eEditType.Integer
                input = New NumEdit
                CType(input, NumEdit).MaxLength = MaxLength
                Exit Select
        End Select

        If input IsNot Nothing Then
            Controls.Add(input)
        End If
    End Sub

    Public Property MaxLength() As Integer
        Get
            Dim o As Object = ViewState("MaxLength")
            If o Is Nothing Then
                ' if MaxLength is zero, which does't limit text length
                o = 0
            End If
            Return CType(o, Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("MaxLength") = value
        End Set
    End Property
End Class
