Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Data.Common
Imports System.Data
Imports System.Collections.Specialized
Imports System.Text
Imports AjaxControlToolkit

Imports JFramework
Imports System.ComponentModel

'[PersistChildren(true), ParseChildren(false)] ： 把自定義控件中包含的內容分析為控件
'[PersistChildren(false), ParseChildren(true)] ： 把自定義控件中包含的內容分析為屬性

<ParseChildren(True), PersistChildren(False)> _
Public Class InfoSelector
    Inherits System.Web.UI.WebControls.CompositeControl

    Private UsePopupExtender As Boolean = True

#Region "FiltingTitle(所要過濾的欄位的中文名稱)"
    Public Property FiltingTitle() As String
        Get
            Dim o As Object = ViewState("FiltingTitle")
            If o Is Nothing Then o = FiltingField
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("FiltingTitle") = value
        End Set
    End Property
#End Region
#Region "ExpandAll(展開全部或者僅顯示選取項)"
    Private Property ExpandAll() As Boolean
        Get
            Return grv.ExpandAll
        End Get
        Set(ByVal value As Boolean)
            grv.ExpandAll = value
            btnExpand.Text = IIf(value, "顯示選取項", "展開全部")
        End Set
    End Property
#End Region
#Region "SelectSQL(所要查詢的SQL)"
    Public Property SelectSQL() As String
        Get
            Return grv.SelectSQL
        End Get
        Set(ByVal value As String)
            grv.SelectSQL = value
        End Set
    End Property
#End Region
#Region "FiltingField(所要過濾的欄位)"
    Public Property FiltingField() As String
        Get
            Return grv.FiltingField
        End Get
        Set(ByVal value As String)
            grv.FiltingField = value
        End Set
    End Property
#End Region
#Region "OptionButton(設定選取資料的方式, 單選 or 多選))"
    Public Property OptionButton() As InfoGridview.eFieldType
        Get
            Return grv.OptionButton
        End Get
        Set(ByVal value As InfoGridview.eFieldType)
            grv.OptionButton = value
        End Set
    End Property
#End Region
#Region "PageSize(GridView分頁大小)"
    Public Property PageSize() As Integer
        Get
            Return grv.PageSize
        End Get
        Set(ByVal value As Integer)
            grv.PageSize = value
        End Set
    End Property
#End Region
#Region "要作為主鍵值的欄位"
    Public Property DataKeyNames() As String
        Get
            Dim keyNames As String = String.Empty
            Dim Keys() As String = grv.DataKeyNames
            Dim bFirst As Boolean = True
            For Each Key As String In Keys
                If bFirst Then
                    bFirst = False
                Else
                    keyNames += ", "
                End If
                keyNames += Key
            Next
            Return keyNames
        End Get
        Set(ByVal value As String)
            Dim separator() As Char = {","}
            value.Replace(" ", String.Empty)
            grv.DataKeyNames = value.Split(separator, StringSplitOptions.RemoveEmptyEntries)
        End Set
    End Property
#End Region
#Region "Gridview欄位資訊"
    Private _fields As FieldCollection = Nothing
    ''' <remarks>
    ''' reference : http://blog.spontaneouspublicity.com/post/2007/05/23/Child-Collections-in-AspNet-Custom-Controls.aspx
    ''' </remarks>
    <PersistenceMode(PersistenceMode.InnerProperty)> _
    Public ReadOnly Property Fields() As FieldCollection
        Get
            If _fields Is Nothing Then
                _fields = New FieldCollection()
            End If
            Return _fields
        End Get
    End Property
#End Region
#Region "Subject"
    Public Property Subject() As String
        Get
            Dim o As Object = ViewState("Subject")
            If o Is Nothing Then o = String.Empty
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("Subject") = value
        End Set
    End Property

    Private ReadOnly Property HasSubject() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Subject)
        End Get
    End Property
#End Region

#Region "ChildControls"
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        If Page.IsPostBack Then
            EnsureChildControls()
        End If
    End Sub

    Private pnlContainer As New Panel

    Private pnlSubject As New Panel
    Private lbSubject As New Label

    Private pnlQueryCondition As New Panel
    Private lbFilting As New Label
    Private txtFilting As New TextBox
    Private btnFilting As New Button

    Private pnlData As New Panel
    Private grv As New InfoGridview
    Private pnlCommands As New Panel
    Private btnExpand As New Button
    Private btnSubmit As New Button
    Private btnCancel As New Button

    Private hiddenSelectedData As New HiddenField

    Private invisibleTarget As New Button
    Private popupExtender As New ModalPopupExtender
    Private popupWindow As New Panel

    Protected Overrides Sub CreateChildControls()
        ' ==================================================
        ' invisibleTarget
        ' ==================================================
        invisibleTarget.ID = ID & "invisibleTarget"
        invisibleTarget.Attributes("style") = "display: none"

        ' ==================================================
        ' PopupWindow
        ' ==================================================
        CssClass = "InfoSelector"
        pnlContainer.ID = "pnlContainer"

        ' ------------------------------------------------------------
        ' pnlSubject
        ' ------------------------------------------------------------
        pnlSubject.ID = "pnlSubject"
        pnlSubject.CssClass = "pnlSubject"
        lbSubject.ID = "lbSubject"
        lbSubject.Text = Subject
        pnlSubject.Controls.Add(lbSubject)

        ' ------------------------------------------------------------
        ' pnlQueryCondition
        ' ------------------------------------------------------------
        'lbFilting
        lbFilting.ID = "lbFilting"
        lbFilting.Text = String.Format("過濾條件(欄位：<strong>{0}</strong>)", FiltingTitle)
        ' txtFilting
        txtFilting.ID = "txtFilting"
        txtFilting.MaxLength = 20
        txtFilting.CssClass = "text"
        ' btnFilting
        btnFilting.ID = "btnFilting"
        btnFilting.CssClass = "button"
        btnFilting.CausesValidation = False
        AddHandler btnFilting.Click, AddressOf OnFilting
        ' pnlQueryCondition
        pnlQueryCondition.ID = "pnlQueryCondition"
        pnlQueryCondition.CssClass = "pnlQueryCondition"

        pnlQueryCondition.Controls.Add(lbFilting)
        pnlQueryCondition.Controls.Add(New LiteralControl("<br/>"))
        pnlQueryCondition.Controls.Add(txtFilting)
        pnlQueryCondition.Controls.Add(btnFilting)

        ' ------------------------------------------------------------
        ' pnlData
        ' ------------------------------------------------------------
        grv.ID = "grv"
        grv.GridViewStyle = XGridView.eGridViewStyle.Yahoo
        AddHandler grv.DataBound, AddressOf DoAfterDataBound

        pnlData.ID = "pnlData"
        pnlData.CssClass = "pnlData"
        pnlData.Controls.Add(grv)

        ' ------------------------------------------------------------
        ' pnlCommands
        ' ------------------------------------------------------------
        ' btnExpand
        btnExpand.ID = "btnExpand"
        btnExpand.CssClass = "button"
        btnExpand.CausesValidation = False
        AddHandler btnExpand.Click, AddressOf OnExpandAll
        ' btnSubmit
        btnSubmit.ID = "btnSubmit"
        btnSubmit.Text = "確定"
        btnSubmit.CssClass = "button"
        btnSubmit.CausesValidation = False
        AddHandler btnSubmit.Click, AddressOf OnSubmit
        ' btnCancel
        btnCancel.ID = "btnCancel"
        btnCancel.Text = "取消"
        btnCancel.CssClass = "button"
        btnCancel.CausesValidation = False
        ' pnlCommands
        pnlCommands.ID = "pnlCommands"
        pnlCommands.CssClass = "pnlCommands"
        pnlCommands.Controls.Add(btnExpand)
        pnlCommands.Controls.Add(btnSubmit)
        pnlCommands.Controls.Add(btnCancel)

        ' ------------------------------------------------------------
        ' hiddenSelectedData
        ' ------------------------------------------------------------
        hiddenSelectedData.ID = "SelectedData"
        grv.HiddenID = hiddenSelectedData.ID
        grv.HiddenClientID = ClientID & "_" & hiddenSelectedData.ID

        If HasSubject Then
            pnlContainer.Controls.Add(pnlSubject)
        End If
        pnlContainer.Controls.Add(pnlQueryCondition)
        pnlContainer.Controls.Add(pnlData)
        pnlContainer.Controls.Add(pnlCommands)
        pnlContainer.Controls.Add(hiddenSelectedData)

        ' --------------------------------------------------
        ' ModalPopupExtender
        ' --------------------------------------------------
        popupExtender.ID = "popupExtender"
        popupExtender.TargetControlID = invisibleTarget.ID
        popupExtender.PopupControlID = pnlContainer.ID
        popupExtender.CancelControlID = btnCancel.ID
        popupExtender.BackgroundCssClass = "modalBackground"

        Controls.Add(invisibleTarget)
        Controls.Add(pnlContainer)

        If UsePopupExtender Then
            Controls.Add(popupExtender)
        End If
    End Sub

    Protected Overrides ReadOnly Property TagKey() As System.Web.UI.HtmlTextWriterTag
        Get
            Return HtmlTextWriterTag.Div
        End Get
    End Property

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)
        Dim type As Type = Me.GetType()
        ClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControl.InfoSelector.css")
    End Sub
#End Region
#Region "Inner Event Handlers"
    Private Sub OnFilting(ByVal sender As Object, ByVal e As EventArgs)
        DataBind()
    End Sub

    Private Sub OnExpandAll(ByVal sender As Object, ByVal e As EventArgs)
        ExpandAll = Not ExpandAll
        DataBind()
    End Sub

    Private Sub OnSubmit(ByVal sender As Object, ByVal e As EventArgs)
        Dim lst As List(Of OrderedDictionary) = grv.SelectedRecords()
        RaiseEvent Submit(lst)
    End Sub

    Private Sub DoAfterDataBound(ByVal sender As Object, ByVal e As EventArgs)
        If UsePopupExtender Then
            popupExtender.Show()
        End If
    End Sub
#End Region
#Region "Supported Events"
    Public Event Submit(ByVal lst As List(Of OrderedDictionary))
#End Region
#Region "DataBind"
    Public Overrides Sub DataBind()
        grv.Fields = Fields
        grv.PageIndex = 0
        grv.FiltingText = txtFilting.Text
        grv.DoDataBind()
    End Sub

    Public Sub Show(Optional ByVal selectedRecords As List(Of OrderedDictionary) = Nothing)
        If selectedRecords Is Nothing Then selectedRecords = New List(Of OrderedDictionary)
        txtFilting.Text = String.Empty
        ExpandAll = selectedRecords.Count = 0
        grv.SelectedRecords = selectedRecords
        DataBind()
    End Sub

    Public Sub Show(ByVal querySelectedRecordsSQL As String)
        Dim connectionString As String = "Data Source=E:\GoogleProjectHosting\jelly-dotnet-framework\src\unittest\testDB\testingData\SQLite\Northwind.sl3"
        Dim dbMode As XDatabase.DatabaseMode = XDatabase.DatabaseMode.Sqlite
        Dim xd As XDatabase = New XDatabase(dbMode, connectionString)
        Dim selectedRecords As List(Of OrderedDictionary) = Nothing
        Try
            Using dt As DataTable = xd.SelectSQL(querySelectedRecordsSQL)
                selectedRecords = ToList(dt)
                Show(selectedRecords)
            End Using
        Catch ex As Exception
            Throw
        Finally
            If selectedRecords IsNot Nothing Then
                selectedRecords.Clear()
                selectedRecords = Nothing
            End If
        End Try
    End Sub

    Private Function ToList(ByVal dt As DataTable) As List(Of OrderedDictionary)
        Dim selectedRecords As New List(Of OrderedDictionary)
        For Each row As DataRow In dt.Rows
            Dim selectedRecord As New OrderedDictionary
            For Each col As DataColumn In dt.Columns
                selectedRecord.Add(col.ColumnName, row(col.ColumnName))
            Next
            selectedRecords.Add(selectedRecord)
        Next
        Return selectedRecords
    End Function
#End Region

End Class


