Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Specialized

Public Class ValidateCodeImage
    Inherits System.Web.UI.WebControls.WebControl

    Public Enum ValidateCodeLengthType
        [Static]
        Random
    End Enum

    Public Enum ValidateCodeBorderColorType
        [Static]
        Random
    End Enum

    Public Enum ValidateCodeDisturb
        None = 0
        A = 10
        B = 20
        C = 30
        D = 40
        E = 50
        F = 60
        G = 80
        H = 100
        [Custom]
    End Enum

    Public Enum ValidateCodeFontColorType
        [Static]
        Random
        RandomAll
    End Enum

    Private Const strValidateCodeBound As String = "a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z|A|B|C|D|E|F|G|H|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z|0|1|2|3|4|5|6|7|8|9"
    Shared Fonts As String() = New String() {"Helvetica", "Geneva", "sans-serif", "Verdana", "Times New Roman", "Courier New"}

    Private strValidateCodeSessionName As String = "ValidateCodeSession"

    Public Property ValidateCodeBoundString() As String
        Get
            Dim objValidateCodeBound As Object = ViewState("ValidateCodeBound")
            Return If(objValidateCodeBound Is Nothing, strValidateCodeBound, objValidateCodeBound.ToString())
        End Get
        Set(ByVal value As String)
            ViewState("ValidateCodeBound") = value
        End Set
    End Property

    Public ReadOnly Property ValidateCodeBound() As String()
        Get
            If ViewState("ValidateCodeBoundCharArray") Is Nothing Then
                ViewState("ValidateCodeBoundCharArray") = ValidateCodeBoundString.Split(New Char() {"|"c})
            End If
            Return DirectCast(ViewState("ValidateCodeBoundCharArray"), String())
        End Get
    End Property

    Public Property ValidateCodeLengthMode() As ValidateCodeLengthType
        Get
            Dim objValidateCodeLengthMode As Object = ViewState("ValidateCodeLengthMode")
            Return If(objValidateCodeLengthMode Is Nothing, ValidateCodeLengthType.[Static], CType(objValidateCodeLengthMode, ValidateCodeLengthType))
        End Get
        Set(ByVal value As ValidateCodeLengthType)
            ViewState("ValidateCodeLengthMode") = value
        End Set
    End Property

    Public Property ValidateCodeMaxLength() As Byte
        Get
            Dim objLength As Object = ViewState("ValidateCodeMaxLength")
            Return If(objLength Is Nothing, CByte(4), CByte(objLength))
        End Get
        Set(ByVal value As Byte)
            If value <= ValidateCodeMinLength Then
                ViewState("ValidateCodeMaxLength") = ValidateCodeMinLength
            ElseIf value = 0 Then
                ViewState("ValidateCodeMaxLength") = 4
            Else
                ViewState("ValidateCodeMaxLength") = value
            End If
        End Set
    End Property

    Public Property ValidateCodeMinLength() As Byte
        Get
            Dim objLength As Object = ViewState("ValidateCodeMinLength")
            Return If(objLength Is Nothing, CByte(4), CByte(objLength))
        End Get
        Set(ByVal value As Byte)
            If value >= ValidateCodeMaxLength Then
                ViewState("ValidateCodeMinLength") = ValidateCodeMaxLength
            ElseIf value = 0 Then
                ViewState("ValidateCodeMinLength") = 4
            Else
                ViewState("ValidateCodeMinLength") = value
            End If
        End Set
    End Property

    Public Property ValidateCodeFontSize() As Byte
        Get
            Dim objValidateCodeFontSize As Object = ViewState("ValidateCodeFontSize")
            Return If(objValidateCodeFontSize Is Nothing, CByte(12), CByte(objValidateCodeFontSize))
        End Get
        Set(ByVal value As Byte)
            ViewState("ValidateCodeFontSize") = value
        End Set
    End Property

    Public Property ValiateCodeWidthModulus() As Single
        Get
            Dim objValidateCodeWidthModulus As Object = ViewState("ValidateCodeWidthModulus")
            Return If(objValidateCodeWidthModulus Is Nothing, 1.0F, CSng(objValidateCodeWidthModulus))
        End Get
        Set(ByVal value As Single)
            Dim tmpF As Single = value
            If tmpF < 0.0F Then
                tmpF = 1.0F
            ElseIf tmpF > 2.0F Then
                tmpF = 2.0F
            Else
                tmpF = 1
            End If
            ViewState("ValidateCodeWidthModulus") = tmpF
        End Set
    End Property

    Public Property ValidateCodeFontColorMode() As ValidateCodeFontColorType
        Get
            Dim objFontColorMode As Object = ViewState("ValidateCodeFontColorMode")
            Return If(objFontColorMode Is Nothing, ValidateCodeFontColorType.[Static], CType(objFontColorMode, ValidateCodeFontColorType))
        End Get
        Set(ByVal value As ValidateCodeFontColorType)
            ViewState("ValidateCodeFontColorMode") = value
        End Set
    End Property

    Public Property ValidateCodeFontColor() As Color
        Get
            Dim objFontColor As Object = ViewState("ValidateCodeFontColor")
            Return If(objFontColor Is Nothing, Color.Black, CType(objFontColor, Color))
        End Get
        Set(ByVal value As Color)
            ViewState("ValidateCodeFontColor") = value
        End Set
    End Property

    Public Property ValidateCodeBackColor() As Color
        Get
            Dim objBackColor As Object = ViewState("ValidateCodeBackColor")
            Return If(objBackColor Is Nothing, Color.FromArgb(&HFF, &HFF, &HCC), CType(objBackColor, Color))
        End Get
        Set(ByVal value As Color)
            ViewState("ValidateCodeBackColor") = value
        End Set
    End Property

    Public Property ValidateCodeBorderColor() As Color
        Get
            Dim objBorderColor As Object = ViewState("ValidateCodeBorderColor")
            Return If(objBorderColor Is Nothing, Color.Black, CType(objBorderColor, Color))
        End Get
        Set(ByVal value As Color)
            ViewState("ValidateCodeBorderColor") = value
        End Set
    End Property

    Public Property ValidateCodeBorderColorMode() As ValidateCodeBorderColorType
        Get
            Dim objBorderColorType As Object = ViewState("ValidateBorderColorMode")
            Return If(objBorderColorType Is Nothing, ValidateCodeBorderColorType.[Static], CType(objBorderColorType, ValidateCodeBorderColorType))
        End Get
        Set(ByVal value As ValidateCodeBorderColorType)
            ViewState("ValidateBorderColorMode") = value
        End Set
    End Property

    Public Property ValidateCodeBorderWidth() As Integer
        Get
            Dim objBorderWidth As Object = ViewState("ValidateCodeBorderWidth")
            Return If(objBorderWidth Is Nothing, 1, CInt(objBorderWidth))
        End Get
        Set(ByVal value As Integer)
            ViewState("ValidateCodeBorderWidth") = value
        End Set
    End Property

    Public Property ValidateCodeDisturbLevel() As ValidateCodeDisturb
        Get
            Dim objDistrubLevel As Object = ViewState("ValidateCodeDisturbLevel")
            Return If(objDistrubLevel Is Nothing, ValidateCodeDisturb.A, CType(objDistrubLevel, ValidateCodeDisturb))
        End Get
        Set(ByVal value As ValidateCodeDisturb)
            ViewState("ValidateCodeDisturbLevel") = value
        End Set
    End Property



    Public Property ValidateCodeDisturbNum() As Short
        Get
            Dim objDistrubNum As Object = ViewState("ValidateCodeDistrubNum")
            Return If(objDistrubNum Is Nothing, CShort(0), CShort(objDistrubNum))
        End Get
        Set(ByVal value As Short)
            ViewState("ValidateCodeDistrubNum") = value
        End Set
    End Property

    Public Property ValidateCodeDistrubLength() As Byte
        Get
            Dim o As Object = ViewState("ValidateCodeDistrubLength")
            Return If(o Is Nothing, CByte(10), CByte(ValidateCodeDistrubLength))
        End Get
        Set(ByVal value As Byte)
            ViewState("ValidateCodeDistrubLength") = value
        End Set
    End Property

    Private Property ValidateCodeSessionName() As String
        Get
            Return strValidateCodeSessionName
        End Get
        Set(ByVal value As String)
            strValidateCodeSessionName = value
        End Set
    End Property

    Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        PaintValidateCode()
    End Sub
    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        Me.SetNotCache()
        MyBase.OnInit(e)
    End Sub

    Private Function GetValidateCodeLength() As Byte
        If ValidateCodeLengthMode = ValidateCodeLengthType.[Static] Then
            Return ValidateCodeMaxLength
        Else
            Dim ran As New Random()
            Return CByte(ran.[Next](ValidateCodeMinLength, ValidateCodeMaxLength + 1))
        End If
    End Function

    Private Function GetValidateCode() As String
        Dim ran As New Random()
        Dim iBoundLength As Integer = ValidateCodeBound.Length
        Dim iMaxLength As Integer = GetValidateCodeLength()
        Dim strCode As String = ""
        For i As Integer = 0 To iMaxLength - 1
            strCode += ValidateCodeBound(ran.[Next](iBoundLength))
        Next

        If HttpContext.Current.Session(strValidateCodeSessionName) Is Nothing Then
            HttpContext.Current.Session.Add(strValidateCodeSessionName, strCode)
        Else
            HttpContext.Current.Session(strValidateCodeSessionName) = strCode
        End If

        Return strCode
    End Function

    Private Function GetFont() As Font
        Dim ran As New Random()
        Dim font As System.Drawing.Font = New Font(Fonts(ran.[Next](Fonts.Length)), ValidateCodeFontSize, GetFontStyle(), GraphicsUnit.Pixel)
        ran = Nothing
        Return font
    End Function

    Private Function GetFontStyle() As FontStyle
        Select Case New Random().[Next](0, 3)
            Case 0
                Return FontStyle.Bold
            Case 1
                Return FontStyle.Italic
            Case 2
                Return FontStyle.Bold Or FontStyle.Italic
            Case Else
                Return FontStyle.Regular
        End Select
    End Function

    Private Function GetRandomColor() As Color
        Dim ran As New Random()
        Return Color.FromArgb(ran.[Next](255), ran.[Next](255), ran.[Next](255))
    End Function

    Private Function GetRandomPoint(ByVal width As Integer, ByVal height As Integer) As Point
        Dim ran As New Random()
        Dim x As Integer = ran.[Next](0, width)
        Dim y As Integer = ran.[Next](0, height)
        Return New Point(x, y)
    End Function

    Private Function GetBrush() As Brush
        Dim ran As New Random()
        Dim brush As New SolidBrush(Color.FromArgb(ran.[Next](255), ran.[Next](255), ran.[Next](255)))
        Return brush
    End Function

    Private Function GetBorderPen() As Pen
        If ValidateCodeBorderColorMode = ValidateCodeBorderColorType.Random Then
            Return New Pen(GetRandomColor(), ValidateCodeBorderWidth)
        Else
            Return New Pen(ValidateCodeBorderColor, ValidateCodeBorderWidth)
        End If
    End Function

    Private Function GetValidateCodeWidth() As Integer
        Return CInt(Math.Truncate((GetValidateCode().Length * ValidateCodeFontSize) * ValiateCodeWidthModulus))
    End Function

    Private Function GetValidateCodeHeight() As Integer
        Return ValidateCodeFontSize * 2
    End Function

    Private Sub PaintValidateCode()
        Dim iWidth As Integer = GetValidateCodeWidth()
        Dim iHeight As Integer = GetValidateCodeHeight()

        Dim objBitmap As New Bitmap(iWidth, iHeight)
        Dim objG As Graphics = Graphics.FromImage(objBitmap)
        Try
            objG.Clear(ValidateCodeBackColor)
            PaintDisturb(objG, iWidth, iHeight)
            Me.PaintCode(objG)
            objG.DrawRectangle(GetBorderPen(), New Rectangle(0, 0, iWidth - ValidateCodeBorderWidth, iHeight - ValidateCodeBorderWidth))
            Using imageStream As New MemoryStream()
                objBitmap.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                HttpContext.Current.Response.BinaryWrite(imageStream.ToArray())
            End Using
        Catch
        Finally
            objBitmap.Dispose()
            objG.Dispose()
        End Try

    End Sub

    Public Sub PaintCode(ByVal g As Graphics)
        Dim sf As New StringFormat()
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        Dim strValidateCode As String = GetValidateCode()

        Select Case Me.ValidateCodeFontColorMode
            Case ValidateCodeFontColorType.RandomAll
                Dim ran As New Random()
                For i As Integer = 0 To strValidateCode.Length - 1
                    Using brush As Brush = New SolidBrush(Color.FromArgb(ran.[Next](0, 255), ran.[Next](0, 255), ran.[Next](0, 255)))
                        g.DrawString(strValidateCode.Substring(i, 1), GetFont(), brush, GetCodeRect(i), sf)
                    End Using
                Next
                ran = Nothing
                Exit Select
            Case ValidateCodeFontColorType.Random
                g.DrawString(strValidateCode, GetFont(), GetBrush(), New Rectangle(0, 0, Me.GetValidateCodeWidth(), Me.GetValidateCodeHeight()), sf)
                Exit Select
            Case Else
                Using brush As Brush = New SolidBrush(Color.Black)
                    g.DrawString(strValidateCode, GetFont(), brush, New Rectangle(0, 0, Me.GetValidateCodeWidth(), Me.GetValidateCodeHeight()), sf)
                End Using
                Exit Select
        End Select

        sf.Dispose()
    End Sub

    Public Function GetCodeRect(ByVal index As Integer) As Rectangle
        Dim subWidth As Integer = Me.GetValidateCodeWidth() \ GetValidateCodeLength()
        Dim subLeftPosition As Integer = subWidth * index

        Return New Rectangle(subLeftPosition, 0, subWidth, Me.GetValidateCodeHeight())
    End Function

    Private Sub PaintDisturb(ByVal g As Graphics, ByVal width As Integer, ByVal height As Integer)
        Dim ran As New Random()

        Dim disturbDensity As Integer = 0
        If ValidateCodeDisturbLevel <> ValidateCodeDisturb.[Custom] Then
            disturbDensity = CInt(ValidateCodeDisturbLevel)
        Else
            disturbDensity = ValidateCodeDisturbNum
        End If

        For i As Integer = 0 To disturbDensity - 1
            Dim p1 As New Point(ran.[Next](0, width), ran.[Next](0, height))
            Dim p2 As New Point(p1.X + ran.[Next](-ValidateCodeDistrubLength, ValidateCodeDistrubLength), p1.Y + ran.[Next](-ValidateCodeDistrubLength, ValidateCodeDistrubLength))
            Using pen As New Pen(Color.FromArgb(ran.[Next](0, 255), ran.[Next](0, 255), ran.[Next](0, 255)))
                g.DrawLine(pen, p1, p2)
            End Using
        Next
    End Sub

    Private Sub SetNotCache()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1)
        HttpContext.Current.Response.Expires = 0
        HttpContext.Current.Response.CacheControl = "no-cache"
        HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
        MyBase.Dispose()
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Me.Dispose()
        Finally
            MyBase.Finalize()
        End Try
    End Sub
End Class
