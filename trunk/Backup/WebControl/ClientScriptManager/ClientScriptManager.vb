Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports AjaxControlToolkit
Imports System.Web
Imports System.Text

Public Class ClientScriptManager

    ''' <summary>取得內嵌資源對應的url</summary>
    ''' <remarks>
    ''' 要能夠使用GetWebResourceUrl取到ResourceUrl
    '''   - (1) 在AssemblyInfo.vb宣告使用的資源,例如:
    '''         (請將 中括號換成小於大於符號)
    '''         [Assembly: System.Web.UI.WebResource("JFramework.WebControl.DateEdit.css", "text/css")]
    ''' 
    '''   - (2) 屬性中[建置動作] 欄位必須設定成內嵌資源
    ''' 
    ''' 自訂控件可能使用GetWebResourceUrl設定Image的, 
    ''' 例如, ImageUrl = ClientScriptManager.GetWebResourceUrl
    ''' 因此將GetWebResourceUrl設定成Pubic
    ''' </remarks>
    Public Shared Function GetWebResourceUrl(ByVal type As Type, ByVal webResource As String) As String
        Return CurPage.ClientScript.GetWebResourceUrl(type, webResource)
    End Function

    ''' <summary>在Header中加入css</summary>
    Public Shared Sub RegisterEmbeddedCSS(ByVal type As Type, ByVal webResource As String)
        Dim csslocation As String = GetWebResourceUrl(type, webResource)
        CurPage.RegisterCSS(csslocation)
    End Sub

    ''' <summary>在Header中加入Javascript</summary>
    ''' <remarks>
    ''' 適用於加入Framework類型的JavaScript,但必須確保Framework之間是沒有相依性的
    ''' 這是因為在<head></head>中設定外部連結的JavaScript
    ''' 無法確定的那一個先誰先載入(誰先下載完成,誰先載入)
    ''' </remarks>
    Public Shared Sub RegisterEmbeddedJs(ByVal type As Type, ByVal webResource As String)
        Dim refUrl As String = GetWebResourceUrl(type, webResource)
        Dim refs As System.Collections.Specialized.StringCollection = JsRefCollection
        If (Not refs.Contains(refUrl)) Then
            refs.Add(refUrl)
        End If
    End Sub

    ''' <summary>將指定的WebResource加入CompositeScript</summary>
    ''' <remarks>
    ''' (1) 請使用此函式註冊jquery.plugin, 如此才能保證JQuery.Core會在jquery.plugin載入完成
    ''' (2) lstWebResource定義的順序不重要, 因為CompositeScript會將JavaScript合成一個檔案
    '''     而Browser會先讀取函式以及物件宣告後, 才會執行(我理解的說法)
    ''' </remarks>
    Public Shared Sub RegisterCompositeScript(ByVal lstWebResource As List(Of String), _
                                              Optional ByVal assembly As String = "JFramework.WebControl")
        For Each webResource As String In lstWebResource
            AddCompositeScript(webResource, assembly)
        Next
    End Sub

    ''' <summary>在Client端, 初始化控件的行為, 如註冊onclick event</summary>
    ''' <remarks>
    '''   時機   : Raised after all scripts have been loaded and all objects in the 
    '''            application that are created by using $create are 
    '''            initialized.
    '''            [PS: 會在 pageLoad function之前執行]
    ''' 
    ''' 觸發時機 : The load event is raised for all postbacks to the server
    '''            , which includes asynchronous postbacks.
    ''' </remarks>
    Public Shared Sub RegisterClientApplicationLoadScript(ByVal control As Control, ByVal script As String)
        '     假如有一TextBox <asp:TextBox ID="tb" runat="server></asp:TextBox>
        '     <script type="text/javascript">    
        '       $(function(){
        '         var data = ['台北市中正區','台北市大同區','台北市中山區','台北市松山區','台北市大安區'];  
        '         $("#" + tb.ClientID).autocomplete(data, {matchContains: true}); 
        '       });
        '     </script>
        ' 
        ' 使用RegisterClientApplicationLoadScript方式註冊, 為
        ' RegisterClientApplicationLoadScript(tb, "var data = ['台北市中正區','台北市大同區','台北市中山區','台北市松山區','台北市大安區'];  $("#" + tb.ClientID).autocomplete(data, {matchContains: true}); ")

        ' ---------------------------------------------------------------------------- '
        '                        very   very   very   importmant
        ' ---------------------------------------------------------------------------- '
        ' 其實Ajax.Net的Sys.Application.add_load(function(){...}), 與JQuery的$(function(){...}); 不是等價的,
        ' 但是如果控件放置在UpdatePanel之中, 使用Sys.Application.add_load才可以work
        ' ---------------------------------------------------------------------------- '
        Dim type As Type = Reflect.GetCallingType()
        Dim key As String = String.Format("{0}_{1}_LoadScript", control.ClientID, type.Name)

        Dim scripts As New StringBuilder()
        scripts.Append("Sys.Application.add_load(" & key & ");")
        scripts.Append("function " & key & "(){")
        scripts.Append(script)
        scripts.Append("Sys.Application.remove_load(" & key & ");")
        scripts.Append("}")

        RegisterStartupScript(control, key, scripts.ToString())
    End Sub

    ''' <summary>在Client端, 釋放資源</summary>
    ''' <remarks>
    '''   時機   : Raised before all objects are disposed and before the 
    '''            browser window's window.unload event occurs.
    '''            [PS: 會在 pageLoad function之前執行]
    ''' 
    ''' 誰要用   : you should free any resources that your code is 
    '''            holding.
    ''' </remarks>
    Public Shared Sub RegisterClientApplicationUnLoadScript(ByVal control As Control, ByVal script As String)
        Dim type As Type = Reflect.GetCallingType()
        Dim key As String = String.Format("{0}_{1}_UnLoadScript", control.ClientID, type.Name)

        Dim scripts As New StringBuilder()

        scripts.Append("<script type='text/javascript'>")
        scripts.Append("Sys.Application.add_unload(function(){")
        scripts.Append(script)
        scripts.Append("});")
        scripts.Append("</script>")

        RegisterStartupScript(control, key, scripts.ToString())
    End Sub

    ''' <summary>(目前還不需要用到)</summary>
    ''' <remarks>
    '''   時機   : Raised after all scripts have been loaded but before any
    '''            objects are created
    ''' 
    '''   誰會用 : If you are writing a component, the init event gives 
    '''            you a point in the life cycle to add your component 
    '''            to the page. The component can then be used by other 
    '''            components or by script later in the page life cycle.
    ''' 
    ''' 執行次數 : The init event is raised only one time when the page 
    '''            is first rendered
    ''' </remarks>
    Public Shared Sub RegisterClientApplicationInitScript(ByVal contorl As Control, ByVal script As String)
        Dim type As Type = Reflect.GetCallingType()
        Dim key As String = String.Format("{0}_{1}_InitScript", contorl.ClientID, type.Name)

        Dim scripts As New StringBuilder()

        scripts.Append("<script type='text/javascript'>")
        scripts.Append("Sys.Application.add_init(function(){")
        scripts.Append(script)
        scripts.Append("});")
        scripts.Append("</script>")

        RegisterStartupScript(contorl, key, scripts.ToString())
    End Sub

    Public Shared Sub RegisterClientSubmitScript(ByVal contorl As Control, ByVal script As String)
        ScriptManager.RegisterOnSubmitStatement(contorl, _
                                                contorl.GetType(), _
                                                Convert.ToString(contorl.ClientID) & "_SubmitScript", _
                                                script)
    End Sub

#Region "private method & member"
    Private Shared tagJsRef As String = "Javascripts-Collection"

    Private Shared ReadOnly Property CurPage() As Page
        Get
            Return CType(HttpContext.Current.Handler, Page)
        End Get
    End Property

    Private Shared ReadOnly Property CurSM() As ScriptManager
        Get
            Return ScriptManager.GetCurrent(CurPage)
        End Get
    End Property

    Private Shared ReadOnly Property JsRefCollection() As System.Collections.Specialized.StringCollection
        Get
            Dim key As String = tagJsRef
            If HttpContext.Current.Items(key) Is Nothing Then
                HttpContext.Current.Items(key) = New System.Collections.Specialized.StringCollection()
                AddHandler CurPage().PreRenderComplete, New EventHandler(AddressOf RegisterScriptsOnPagePreRenderComplete)
            End If
            Return CType(HttpContext.Current.Items(key), System.Collections.Specialized.StringCollection)
        End Get
    End Property

    Private Shared Sub RegisterScriptsOnPagePreRenderComplete(ByVal sender As Object, ByVal e As EventArgs)
        Dim page As Page = TryCast(sender, Page)
        Dim refs As System.Collections.Specialized.StringCollection = JsRefCollection
        For Each url As String In refs
            page.Header.Controls.Add(New LiteralControl("<script type='text/javascript' src='" & url & "'></script>"))
        Next
    End Sub

    Private Shared Sub RegisterStartupScript(ByVal control As System.Web.UI.Control, ByVal key As String, ByVal scripts As String)
        Dim sm As ScriptManager = CurSM()
        ' 即使用ToolkitScriptManager代替ScriptManager, 
        ' 也可使用ScriptManager.GetCurrent判斷()
        If sm IsNot Nothing Then
            ScriptManager.RegisterStartupScript(control, control.GetType(), key, scripts.ToString(), True)
        Else
            CurPage.ClientScript.RegisterStartupScript(control.GetType(), key, scripts.ToString())
        End If
    End Sub

    Private Shared Function CreateStyleLink(ByVal href As String) As Control
        Dim link As New HtmlLink()
        link.Attributes.Add("rel", "Stylesheet")
        link.Attributes.Add("type", "text/css")
        link.Attributes.Add("href", href)
        Return link
    End Function

    'Private Shared Sub RegisterClientApplicationLoadScript(ByVal control As Control, ByVal key As String, ByVal script As String)
    '    Dim scripts As New StringBuilder()

    '    scripts.Append("Sys.Application.add_load(" & key & ");")
    '    scripts.Append("function " & key & "(){")
    '    scripts.Append(script)
    '    scripts.Append("Sys.Application.remove_load(" & key & ");")
    '    scripts.Append("}")

    '    RegisterStartupScript(control, key, scripts.ToString())
    'End Sub

    'Private Shared Sub RegisterClientApplicationUnLoadScript(ByVal control As Control, ByVal key As String, ByVal script As String)
    '    Dim scripts As New StringBuilder()

    '    scripts.Append("<script type='text/javascript'>")
    '    scripts.Append("Sys.Application.add_unload(function(){")
    '    scripts.Append(script)
    '    scripts.Append("});")
    '    scripts.Append("</script>")

    '    RegisterStartupScript(control, key, scripts.ToString())
    'End Sub

    'Private Shared Sub RegisterClientApplicationInitScript(ByVal control As Control, ByVal key As String, ByVal script As String)
    '    Dim scripts As New StringBuilder()

    '    scripts.Append("<script type='text/javascript'>")
    '    scripts.Append("Sys.Application.add_init(function(){")
    '    scripts.Append(script)
    '    scripts.Append("});")
    '    scripts.Append("</script>")

    '    RegisterStartupScript(control, key, scripts.ToString())
    'End Sub

    ''' <summary>將指定組件中的JavaScript Resource加入CompositeScript</summary>
    ''' <param name="name">定義在AssemblyInfo.vb中的webResource名稱</param>
    ''' <param name="assembly">組件名稱,以此專案而言是JFramework.WebControl</param>
    ''' <remarks>
    ''' 用法 
    ''' ClientScriptManager.AddCompositeScript(Me, "JFramework.WebControl.jquery.textareaCounter.js", "JFramework.WebControl")
    ''' </remarks>
    Private Shared Sub AddCompositeScript(ByVal name As String, ByVal assembly As String)
        AddCompositeScript(New ScriptReference(name, assembly))
    End Sub

    ''' <summary>將網路上指定的JavaScript加入CompositeScript</summary>
    ''' <param name="control"></param>
    ''' <param name="path">http://XXX/NNN.js</param>
    ''' <remarks>
    ''' 用法 
    ''' ClientScriptManager.AddCompositeScript(Me, "http://code.jquery.com/jquery-1.5.js")
    ''' </remarks>
    Private Shared Sub AddCompositeScript(ByVal control As Control, ByVal path As String)
        AddCompositeScript(New ScriptReference(path))
    End Sub

    Private Shared Function ReferenceIsExists(ByVal sm As ScriptManager, ByVal scriptReference As ScriptReference) As Boolean
        If Not String.IsNullOrEmpty(scriptReference.Path) Then
            Dim scripts = From s In sm.CompositeScript.Scripts _
                          Where s.Path.ToLower() = scriptReference.Path.ToLower() _
                          Select s
            Return scripts.Count() > 0
        End If

        Dim typedScripts = From ts In sm.CompositeScript.Scripts _
                           Where ts.Assembly.ToLower() = scriptReference.Assembly.ToLower() AndAlso ts.Name.ToLower() = scriptReference.Name.ToLower() _
                           Select ts
        Return typedScripts.Count() > 0
    End Function

    Private Shared Sub AddCompositeScript(ByVal scriptReference As ScriptReference)
        If Not ReferenceIsExists(CurSM, scriptReference) Then
            CurSM.CompositeScript.Scripts.Add(scriptReference)
        End If
    End Sub
#End Region

#If 0 Then
    ' DJ (DotNetAge jQuery Controls for ASP.NET)沒有
    ' 使用網路上教的以下兩種方式註冊Javascript
    '   - ScriptManager.RegisterClientScriptInclude
    '   - Page.ClientScript.RegisterClientScriptInclude
    '
    ' 目前測試可用, 但還在測試什麼時候是必須使用此方式註冊才能work

    ''' <summary>
    ''' Registers the client script include.
    ''' </summary>
    ''' <remarks>
    ''' 判斷當頁面有ScriptManager時，應透過ScriptManager註冊JS，
    ''' 使用Page.ClientScript註冊會在UpdatePanel Partial Render時遺漏參考。
    ''' </remarks>
    Public Shared Sub IncludeJs(ByVal control As System.Web.UI.Control, ByVal webResource As String)

        Dim sm As ScriptManager = ScriptManager.GetCurrent(wc.Page)
        ' 即使用ToolkitScriptManager代替ScriptManager, 
        ' 也可使用ScriptManager.GetCurrent判斷()
        Dim id As String = wc.ID
        Dim webResourceUrl As String = GetWebResourceUrl(wc, webResource)

        If sm IsNot Nothing Then
            ScriptManager.RegisterClientScriptInclude(wc.Page, wc.GetType(), webResource, webResourceUrl)
        Else
            wc.Page.ClientScript.RegisterClientScriptInclude(webResource, webResourceUrl)
        End If
    End Sub
#End If
End Class
