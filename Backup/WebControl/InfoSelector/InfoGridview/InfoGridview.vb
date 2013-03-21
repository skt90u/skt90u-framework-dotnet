Imports System.Collections.Specialized
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Web.Script.Serialization

Public Class InfoGridview
    Inherits XGridView

#Region "Public Attributes"
#Region "Optional Attributes"
#Region "OptionButton(設定資料勾選方式{單選|多選}; DEFAULT = RadioButton)"
    ''' <remarks>
    ''' 只能使用CheckBox|RadioButton
    ''' 
    ''' selector.OptionButton = InfoGridview.eFieldType.CheckBox
    ''' </remarks>
    Public Property OptionButton() As eFieldType
        Get
            Return GetPropertyValue(Of eFieldType)("OptionButton", eFieldType.RadioButton)
        End Get
        Set(ByVal value As eFieldType)
            If Not value.Equals(eFieldType.CheckBox) AndAlso Not value.Equals(eFieldType.RadioButton) Then
                Debug.Assert(False)
                Throw New ArgumentException("The type of OptionButton must be checkbox or radioButton")
            End If
            SetPropertyValue(Of eFieldType)("OptionButton", value)
        End Set
    End Property
#End Region
#Region "FiltingText(使用者輸入的過濾條件; DEFAULT = String.Empty)"
    Public Property FiltingText() As String
        Get
            Return GetPropertyValue(Of String)("FiltingText", String.Empty)
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FiltingText", value)
        End Set
    End Property
#End Region
#Region "ExpandAll(展開全部或者僅顯示選取項;DEFAULT = Not Expand All"
    Public Property ExpandAll() As Boolean
        Get
            Return GetPropertyValue(Of Boolean)("ExpandAll", False)
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ExpandAll", value)
        End Set
    End Property
#End Region
#End Region
#Region "Necessary Attributes"
#Region "FiltingField(所要過濾的欄位)"
    Public Property FiltingField() As String
        Get
            Dim value As String = GetPropertyValue(Of String)("FiltingField", String.Empty)
            If String.IsNullOrEmpty(value) Then
                Throw New ArgumentException("You must specify which field you want to filter")
            End If
            Return value
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FiltingField", value)
        End Set
    End Property
#End Region
#Region "SelectSQL(所要查詢的SQL)"
    Public Property SelectSQL() As String
        Get
            Dim value As String = GetPropertyValue(Of String)("SelectSQL", String.Empty)
            If String.IsNullOrEmpty(value) Then
                Throw New ArgumentException("You must specify which query SQL you want to select")
            End If
            Return value
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SelectSQL", value)
        End Set
    End Property
#End Region
#End Region
#End Region

    Public Property Fields() As FieldCollection
        Get
            Return GetPropertyValue(Of FieldCollection)("Fields", Nothing)
        End Get
        Set(ByVal value As FieldCollection)
            SetPropertyValue(Of FieldCollection)("Fields", value)
        End Set
    End Property

    Public Property HiddenID() As String
        Get
            Return GetPropertyValue(Of String)("HiddenID", Nothing)
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HiddenID", value)
        End Set
    End Property

    Public Property HiddenClientID() As String
        Get
            Return GetPropertyValue(Of String)("HiddenClientID", Nothing)
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HiddenClientID", value)
        End Set
    End Property

    Private Property HiddenValue() As String
        Get
            Return SelectedDataBuffer.Value
        End Get
        Set(ByVal value As String)
            SelectedDataBuffer.Value = value
        End Set
    End Property

    Private ReadOnly Property SelectedDataBuffer() As HiddenField
        Get
            Try
                Dim selector As InfoSelector = NamingContainer
                Dim buffer As HiddenField = CType(selector.XFindControl(HiddenID), HiddenField)
                Return buffer
            Catch ex As Exception
                Throw
            End Try
        End Get
    End Property

    Public Property SelectedRecords() As List(Of OrderedDictionary)
        Get
            ' [deserialize json string]
            ' http://weblogs.asp.net/hajan/archive/2010/07/23/javascriptserializer-dictionary-to-json-serialization-and-deserialization.aspx
            ' [deserialize json string to List(Of OrderDictionary)]
            ' http://stackoverflow.com/questions/402996/deserializing-json-objects-as-listtype-not-working-with-asmx-service
            Dim lst As List(Of OrderedDictionary) = Nothing
            Try
                Dim deserializer As New JavaScriptSerializer()
                lst = deserializer.Deserialize(Of List(Of OrderedDictionary))(HiddenValue)
            Catch ex As Exception
                Throw
            End Try
            Return lst
        End Get
        Set(ByVal value As List(Of OrderedDictionary))
            HiddenValue = WebControlUtil.Serialize(value)
        End Set
    End Property

    Public Event AfterPageIndexChanging(ByVal sender As Object, ByVal e As EventArgs)

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)

        If PageSize > 0 Then
            AllowPaging = True
            AddHandler PageIndexChanging, AddressOf DoPageIndexChanging
        End If
        AutoGenerateColumns = False

        AddHandler RowDataBound, AddressOf DoRowDataBound
    End Sub

    Private Sub DoRowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim row As GridViewRow = e.Row
        If Not row.RowType.Equals(DataControlRowType.DataRow) Then Return

        Dim grv As GridView = CType(sender, GridView)

        ' first column always be optionButton
        row.Cells(0).HorizontalAlign = WebControls.HorizontalAlign.Center
        row.Attributes("rowtype") = "datarow"
        For i As Integer = 1 To row.Cells.Count - 1
            Dim tf As TemplateField = CType(grv.Columns(i), TemplateField)
            Dim xt As XTemplate = CType(tf.ItemTemplate, XTemplate)
            Dim columnName As String = xt.GetColumnName()
            Dim tc As TableCell = row.Cells(i)
            tc.Attributes("column") = columnName
        Next
    End Sub

    Protected Sub DoPageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        PageIndex = e.NewPageIndex
        DoDataBind()
        RaiseEvent AfterPageIndexChanging(Me, New EventArgs)
    End Sub

    Public Sub DoDataBind()
        Dim sql As String = GetQuerySQL()
        Using dt As DataTable = GetDataTable(sql)
            LoadDynamicColumn(dt)
            DataSource = dt
            DataBind()
        End Using
    End Sub

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)

        Dim type As Type = Me.GetType()

        AppendClass("InfoGridview")

        ' 放棄使用 Page.ClientScript.RegisterHiddenField 來 cache 所選取的資料
        ' 使用 RegisterHiddenField 註冊的HiddenField,
        ' 只能使用RegisterHiddenField 設值
        ' 並且使用 Page.Form(HiddenKey)取值
        'Page.ClientScript.RegisterHiddenField(HiddenKey, hiddenFieldValue)

        ClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControl.InfoGridview.css")
        ClientScriptManager.RegisterEmbeddedJs(type, "JFramework.WebControl.jquery-1.4.4.js")

        Dim lstWebResource As New List(Of String)
        lstWebResource.Add("JFramework.WebControl.json2.js")
        lstWebResource.Add("JFramework.WebControl.InfoGridview.js")
        ClientScriptManager.RegisterCompositeScript(lstWebResource)

        Dim scripts As String = GenApplicationLoadScript()
        ClientScriptManager.RegisterClientApplicationLoadScript(Me, scripts)
    End Sub

    Private Function GenApplicationLoadScript() As String
        Dim scripts As New StringBuilder()
        scripts.Append("var options = {")
        scripts.Append("'DataKeyNames': '" & WebControlUtil.Serialize(DataKeyNames) & "',")
        scripts.Append("HiddenKey: '" & HiddenClientID & "'")
        scripts.Append("};")
        scripts.Append("$('#" & ClientID & "').InfoGridview(options);")
        Return scripts.ToString()
    End Function

    Private Sub LoadDynamicColumn(ByVal dt As DataTable)
        Columns.Clear()
        Columns.Add(GetOptionTemplate(OptionButton))
        For Each col As DataColumn In dt.Columns
            Columns.Add(GetFieldTemplate(col.ColumnName))
        Next
    End Sub

    ''' <summary>
    ''' 考慮QueryCondition是否有輸入
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetQuerySQL() As String
        Dim sql As String = String.Empty
        sql = SelectSQL
        Dim fl As String = FiltingText
        If Not String.IsNullOrEmpty(fl) Then
            sql = String.Format("select * from ({0}) where {1} like '%{2}%'", sql, FiltingField, FiltingText)
        End If
        Return sql
    End Function

    ''' <summary>修改成BvStyle</summary>
    Private Function GetDataTable(ByVal sql As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim xd As XDatabase = Nothing
        Try
            Dim connectionString As String = "Data Source=E:\GoogleProjectHosting\jelly-dotnet-framework\src\unittest\testDB\testingData\SQLite\Northwind.sl3"
            Dim dbMode As XDatabase.DatabaseMode = XDatabase.DatabaseMode.Sqlite
            xd = New XDatabase(dbMode, connectionString)
            dt = xd.SelectSQL(sql)
            MakeSureFieldsConsistent(dt)
            If Not ExpandAll Then
                RemoveUnmatchRows(dt)
            End If
        Catch ex As Exception
            Throw
        Finally
            xd = Nothing
        End Try
        Return dt
    End Function

    Private Sub MakeSureFieldsConsistent(ByVal dt As DataTable)
        Dim records As List(Of OrderedDictionary) = SelectedRecords
        If records.Count = 0 Then Return
        Dim record As OrderedDictionary = records.First()
        For Each col As DataColumn In dt.Columns
            If Not record.Contains(col.ColumnName) Then
                Throw New ArgumentException("Fields Inconsistent, SelectedRecords don't contain [field: " + col.ColumnName + "] in queryTable")
            End If
        Next
        For Each key As String In record.Keys
            If Not dt.Columns.Contains(key) Then
                Throw New ArgumentException("Fields Inconsistent, queryTable don't contain [field: " + key + "] in SelectedRecords")
            End If
        Next
    End Sub

    ''' <summary>
    ''' 當設定[僅顯示選取項目]時, 會被呼叫
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub RemoveUnmatchRows(ByVal dt As DataTable)
        If dt Is Nothing Then
            Throw New ArgumentNullException("RemoveUnmatchRows: DataTable can't be null")
        End If

        ' -----------------------------------------------------------------
        ' DON'T PERFORM ROWS.REMOVE INSIDE FOR EACH STATEMENT OVER ONE TIME
        ' -----------------------------------------------------------------
        ' The problem that is when you delete the row, the datatable reindexes
        ' it automatically. so it can not access correctly 
        ' reference : http://www.daniweb.com/forums/thread186207.html
        'For Each row As DataRow In dt.Rows
        '    If Not PKisMatch(row) Then
        '        dt.Rows.Remove(row)
        '    End If
        'Next

        Dim i As Integer = dt.Rows.Count - 1
        Do While (i >= 0)
            For Each row As DataRow In dt.Rows
                If Not PKisMatch(row) Then
                    dt.Rows.Remove(row)
                    ' don't perform rows.remove inside for 
                    ' each statement "over one time"
                    Exit For
                End If
            Next
            i -= 1
        Loop
    End Sub

    Private Function PKisMatch(ByVal row As DataRow) As Boolean
        If row Is Nothing Then
            Throw New ArgumentNullException("PKisMatch: DataRow can't be null")
        End If

        Dim initValues As List(Of OrderedDictionary) = SelectedRecords
        Try
            If initValues.Count = 0 Then Return False

            For Each selectedValue As OrderedDictionary In initValues
                Dim match As Boolean = True
                For Each key As String In DataKeyNames
                    If Not selectedValue(key).Equals(row.Item(key)) Then
                        match = False
                    End If
                Next
                If match Then
                    Return True
                End If
            Next
        Catch ex As Exception
        Finally
            initValues.Clear()
            initValues = Nothing
        End Try
        Return False
    End Function

#Region "目前欄位支援的TemplateField"
    Public Enum eFieldType
        Label = 0
        NumEdit
        DateEdit
        CheckBox
        RadioButton
    End Enum

    Private Class FieldInfo
        Public Sub New(ByVal fieldType As eFieldType, _
                       ByVal typeTemplateField As Type)
            Me.fieldType = fieldType
            Me.typeTemplateField = typeTemplateField
        End Sub

        Public ReadOnly fieldType As eFieldType
        Public ReadOnly typeTemplateField As Type
    End Class

    Private Shared ReadOnly arrFieldInfo() As FieldInfo = New FieldInfo() { _
        New FieldInfo(eFieldType.Label, GetType(LabelTemplate)), _
        New FieldInfo(eFieldType.NumEdit, GetType(NumEditTemplate)), _
        New FieldInfo(eFieldType.DateEdit, GetType(DateEditTemplate)), _
        New FieldInfo(eFieldType.CheckBox, GetType(CheckBoxTemplate)), _
        New FieldInfo(eFieldType.RadioButton, GetType(RadioButtonTemplate)) _
    }
#End Region
#Region "產生FieldTemplate & OptionTemplate"
    Private Function GetTemplateFieldType(ByVal fieldType As eFieldType) As Type
        Dim typeTemplateField As Type = Nothing
        For Each fieldInfo As FieldInfo In arrFieldInfo
            If fieldInfo.fieldType.Equals(fieldType) Then
                Return fieldInfo.typeTemplateField
                Exit For
            End If
        Next
        Debug.Assert(False)
        Throw New ArgumentException("GetTemplateFieldType : Unknown fieldType")
    End Function

    Private Function GetTemplateFieldType(ByVal colName As String) As Type
        'Dim fieldType As eFieldType = GetFieldType(colName)
        Dim fieldType As eFieldType = Fields.GetFieldType(colName)
        Return GetTemplateFieldType(fieldType)
    End Function

    Private Function GetFieldTemplate(ByVal colName As String) As TemplateField
        Dim type As Type = GetTemplateFieldType(colName)
        Dim headerTemplate As XTemplate = Activator.CreateInstance(type, New Object() {ListItemType.Header, colName, Fields.GetFieldTitle(colName)})
        Dim itemTemplate As XTemplate = Activator.CreateInstance(type, New Object() {ListItemType.Item, colName, Fields.GetFieldTitle(colName)})
        Dim tf As New TemplateField
        tf.HeaderTemplate = headerTemplate
        tf.ItemTemplate = itemTemplate
        Return tf
    End Function

    Private Function GetOptionTemplate(ByVal optionType As eFieldType) As TemplateField
        If Not optionType.Equals(eFieldType.CheckBox) AndAlso Not optionType.Equals(eFieldType.RadioButton) Then
            Debug.Assert(False)
            Throw New ArgumentException("optionType must be checkbox or radiobutton")
        End If
        Dim type As Type = GetTemplateFieldType(optionType)
        Dim headerTemplate As XTemplate = Activator.CreateInstance(type, New Object() {ListItemType.Header, "option", "選項"})
        Dim itemTemplate As XTemplate = Activator.CreateInstance(type, New Object() {ListItemType.Item, "option", "選項"})
        Dim tf As New TemplateField
        tf.HeaderTemplate = headerTemplate
        tf.ItemTemplate = itemTemplate

        'Dim tis As New TableItemStyle
        'tis.HorizontalAlign = WebControls.HorizontalAlign.Center
        Return tf
    End Function
#End Region

End Class

