Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Specialized

Public Class ValidateCodeControl
    Inherits System.Web.UI.WebControls.WebControl
    Implements IPostBackDataHandler

    Private txbValidateCodeInput As TextBox
    Private imgValidateCodeImage As Image
    Private linkRefreshValidateCode As HyperLink

    Private strValidateCodeSessionName As String = "ValidateCodeSession"
    Private UserInput As String = String.Empty

#Region "TextBoxCssClass"
    Public Property TextBoxCssClass() As String
        Get
            Dim o As Object = ViewState("TextBoxCssClass")
            If o Is Nothing Then
                o = String.Empty
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("TextBoxCssClass") = value
        End Set
    End Property
#End Region
#Region "ImageCssClass"
    Public Property ImageCssClass() As String
        Get
            Dim o As Object = ViewState("ImageCssClass")
            If o Is Nothing Then
                o = String.Empty
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("ImageCssClass") = value
        End Set
    End Property
#End Region
#Region "RefreshLinkCssClass"
    Public Property RefreshLinkCssClass() As String
        Get
            Dim o As Object = ViewState("RefreshLinkCssClass")
            If o Is Nothing Then
                o = String.Empty
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("RefreshLinkCssClass") = value
        End Set
    End Property
#End Region
#Region "ImagePageUrl"
    Public Property ImagePageUrl() As String
        Get
            Dim o As Object = ViewState("ImagePageUrl")
            If o Is Nothing Then
                o = Me.Page.ResolveUrl("./ValidateCode.aspx")
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("ImagePageUrl") = value
        End Set
    End Property
#End Region
#Region "RefreshLinkText"
    Public Property RefreshLinkText() As String
        Get
            Dim o As Object = ViewState("RefreshLinkText")
            If o Is Nothing Then
                o = "換一換"
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("RefreshLinkText") = value
        End Set
    End Property
#End Region
#Region "ValidateCodeSessionName"
    Public Property ValidateCodeSessionName() As String
        Get
            Return strValidateCodeSessionName
        End Get
        Set(ByVal value As String)
            strValidateCodeSessionName = value
        End Set
    End Property
#End Region
#Region "IsMatch"
    Public Function IsMatch() As Boolean
        Dim ValidateCode As Object = HttpContext.Current.Session(ValidateCodeSessionName)
        If ValidateCode Is Nothing Then Return False
        Return UserInput = ValidateCode
    End Function
#End Region

    Public ReadOnly Property ValidateCodeUserInput() As String
        Get
            Return UserInput
        End Get
    End Property

    Protected Overrides ReadOnly Property TagKey() As HtmlTextWriterTag
        Get
            Return HtmlTextWriterTag.Table
        End Get
    End Property

    Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Page.RegisterRequiresPostBack(Me)
        Page.ClientScript.GetPostBackClientHyperlink(Me, "")
        ClientScriptManager.RegisterEmbeddedCSS(Me.GetType(), "JFramework.WebControl.ValidateCodeControl.css")
        ClientScriptManager.RegisterEmbeddedJs(Me.GetType(), "JFramework.WebControl.ValidateCodeControl.js")
    End Sub

    Protected Overrides Sub CreateChildControls()
        Controls.Clear()
        ClearChildViewState()
        CreateControlHierarchy()
        PrepareControlHierarchy()
        TrackViewState()
        ChildControlsCreated = True
    End Sub

    Private Function GetUniqueID(ByVal childID As String) As String
        Return ClientID & "_" & childID
    End Function

    Private Function GetOnClickScript() As String
        Dim js As String = String.Format("refreshValidateCodeImage('{0}', '{1}');", imgValidateCodeImage.ClientID, ImagePageUrl)
        Return js
    End Function

    Protected Overridable Sub CreateControlHierarchy()
        txbValidateCodeInput = New TextBox()
        imgValidateCodeImage = New Image()
        linkRefreshValidateCode = New HyperLink()

        txbValidateCodeInput.ID = GetUniqueID("txbValidateCodeInput")
        imgValidateCodeImage.ID = GetUniqueID("imgValidateCodeImage")
        linkRefreshValidateCode.ID = GetUniqueID("linkRefreshValidateCode")

        AppendClass("ValidateCodeControl")

        txbValidateCodeInput.CssClass = TextBoxCssClass
        imgValidateCodeImage.CssClass = ImageCssClass
        linkRefreshValidateCode.CssClass = RefreshLinkCssClass

        imgValidateCodeImage.ImageUrl = ImagePageUrl
        linkRefreshValidateCode.Text = RefreshLinkText

        linkRefreshValidateCode.Attributes.Add("onclick", GetOnClickScript())

        ChildControlsCreated = True
    End Sub

    Protected Overridable Sub PrepareControlHierarchy()
        Dim row As New TableRow()

        Dim inputCell As New TableCell()
        inputCell.Controls.Add(txbValidateCodeInput)
        row.Cells.Add(inputCell)
        inputCell.Dispose()

        Dim imageCell As New TableCell()
        imageCell.Controls.Add(imgValidateCodeImage)
        row.Cells.Add(imageCell)
        imageCell.Dispose()

        Dim linkCell As New TableCell()
        linkCell.Controls.Add(linkRefreshValidateCode)
        row.Cells.Add(linkCell)
        linkCell.Dispose()

        Controls.Add(row)
        row.Dispose()
    End Sub

    Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
        EnsureChildControls()
        MyBase.Render(writer)
    End Sub

    Public Overrides Sub Dispose()
        If txbValidateCodeInput IsNot Nothing Then txbValidateCodeInput.Dispose()
        If imgValidateCodeImage IsNot Nothing Then imgValidateCodeImage.Dispose()
        If linkRefreshValidateCode IsNot Nothing Then linkRefreshValidateCode.Dispose()
        GC.Collect()
        MyBase.Dispose()
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose()
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    Public Function LoadPostData(ByVal postDataKey As String, ByVal postCollection As System.Collections.Specialized.NameValueCollection) As Boolean Implements System.Web.UI.IPostBackDataHandler.LoadPostData
        Dim o As Object = postCollection(GetUniqueID("txbValidateCodeInput"))
        Dim userInputCode As String = DirectCast(o, String)
        If Not String.IsNullOrEmpty(userInputCode) Then
            UserInput = userInputCode
            Return True
        End If
        Return False
    End Function

    Public Sub RaisePostDataChangedEvent() Implements System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent

    End Sub
End Class
