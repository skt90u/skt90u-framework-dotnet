Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.Script.Serialization

Public Class uploadify
    Inherits System.Web.UI.WebControls.CompositeControl

#Region "Private"
    Private fileQueue As New Panel
    Private browseButton As New FileUpload

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        If Page.IsPostBack Then
            EnsureChildControls()
        End If
    End Sub

    Protected Overrides Sub CreateChildControls()
        fileQueue.ID = "fileQueue"
        fileQueue.CssClass = "fileQueue"
        Controls.Add(fileQueue)

        browseButton.ID = "browseButton"
        Controls.Add(browseButton)

        If Not auto Then
            ' if files don't upload automatically,
            ' create buttons for upload files and cancel queue
            Dim uploadButton As HyperLink = CreateUploadButton()
            Controls.Add(uploadButton)

            Dim cancelButton As HyperLink = CreateCancelAllButton()
            Controls.Add(cancelButton)
        End If
    End Sub

    ''' <summary>create a button that can trigger the file upload</summary>
    Private Function CreateUploadButton() As HyperLink
        Dim uploadButton As New HyperLink
        uploadButton.ID = "uploadButton"
        uploadButton.NavigateUrl = String.Format("javascript:$('#{0}').uploadifyUpload();", browseButton.ClientID)
        uploadButton.Text = "Upload Files"
        Return uploadButton
    End Function

    ''' <summary>
    ''' create a button that can remove all files from the file upload queue and 
    ''' cancel any file uploads that are in progress.
    ''' </summary>
    Private Function CreateCancelAllButton() As HyperLink
        Dim cancelButton As New HyperLink
        cancelButton.ID = "uploadButton"
        cancelButton.NavigateUrl = String.Format("javascript:$('#{0}').uploadifyClearQueue();", browseButton.ClientID)
        cancelButton.Text = "Clear Queue"
        Return cancelButton
    End Function


    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        Dim type As Type = Me.GetType()

        ' uploadify stylesheet
        ClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControl.uploadify.default.css")
        ClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControl.uploadify.css")

        ' uploadify required javascript
        ClientScriptManager.RegisterEmbeddedJs(type, "JFramework.WebControl.jquery-1.4.4.js")

        ' uploadify dependence javascript
        Dim lstWebResource As New List(Of String)
        lstWebResource.Add("JFramework.WebControl.swfobject.js")
        lstWebResource.Add("JFramework.WebControl.jquery.uploadify.v2.1.0.min.js")
        ClientScriptManager.RegisterCompositeScript(lstWebResource)

        ' uploadify initilization javascript
        Dim scripts As String = GenApplicationLoadScript()
        ClientScriptManager.RegisterClientApplicationLoadScript(Me, scripts)
    End Sub

    Private Function GenApplicationLoadScript() As String
        Dim scripts As New StringBuilder()

        scripts.Append("$('#" + browseButton.ClientID + "').uploadify({")
        ' ------------------------------------------------------------
        ' Obsolete Options
        ' ------------------------------------------------------------
        'scripts.Append(GetOption("height", ButtonHeight))
        'scripts.Append(GetOption("width", ButtonWidth))
        'scripts.Append(GetOption("displayData", displayData))
        'scripts.Append(GetOption("scriptAccess", scriptAccess))
        'scripts.Append(GetOption("wmode", wmode))
        'scripts.Append(GetOption("rollover", rollover))
        'scripts.Append(GetOption("removeCompleted", removeCompleted))
        ' ------------------------------------------------------------
        ' Private Options
        ' ------------------------------------------------------------
        scripts.Append(GetOption("uploader", uploader, False))
        scripts.Append(GetOption("queueID", queueID))
        scripts.Append(GetOption("cancelImg", cancelImg))
        scripts.Append(GetOption("expressInstall", expressInstall))
        scripts.Append(GetOption("hideButton", hideButton))
        scripts.Append(GetOption("buttonImg", buttonImg))
        ' ------------------------------------------------------------
        ' Public Options
        ' ------------------------------------------------------------
        scripts.Append(GetOption("buttonText", buttonText))
        scripts.Append(GetOption("auto", auto))
        scripts.Append(GetOption("multi", multi))
        scripts.Append(GetOption("fileDesc", fileDesc))
        scripts.Append(GetOption("fileExt", fileExt))
        scripts.Append(GetOption("queueSizeLimit", queueSizeLimit))
        scripts.Append(GetOption("simUploadLimit", simUploadLimit))
        scripts.Append(GetOption("sizeLimit", sizeLimit))

        scripts.Append(GetOption("script", script))
        scripts.Append(GetOption("checkScript", checkScript))
        scripts.Append(GetOption("method", method))
        scripts.Append(GetOption("folder", folder))
        scripts.Append(GetOption("fileDataName", fileDataName))
        scripts.Append(GetOption("scriptData", scriptData))

        AttachEvent(scripts, "onAllComplete", onAllComplete)
        AttachEvent(scripts, "onCancel", onCancel)
        AttachEvent(scripts, "onCheck", onCheck)
        AttachEvent(scripts, "onClearQueue", onClearQueue)
        AttachEvent(scripts, "onComplete", onComplete)
        AttachEvent(scripts, "onError", onError)
        'AttachEvent(scripts, "onInit", onInit)
        AttachEvent(scripts, "onOpen", onOpen)
        AttachEvent(scripts, "onProgess", onProgess)
        AttachEvent(scripts, "onQueueFull", onQueueFull)
        AttachEvent(scripts, "onSelect", onSelect)
        AttachEvent(scripts, "onSelectOnce", onSelectOnce)
        AttachEvent(scripts, "onSWFReady", onSWFReady)

        scripts.Append("});")
        Dim js As String = scripts.ToString()
        Return js
    End Function
#End Region

#Region "Options"
#Region "Default  Options"

    Enum eDisplayData
        percentage = 0
        speed
    End Enum

    Enum eWmode
        opaque = 0
        transparent
    End Enum

    Enum eMethod
        POST = 0
        [GET]
    End Enum

    Class DefaultOptions
        ' Obsolete Options
        Public Shared ReadOnly height As Integer = 30
        Public Shared ReadOnly width As Integer = 120
        Public Shared ReadOnly displayData As eDisplayData = eDisplayData.percentage
        Public Shared ReadOnly scriptAccess As String = "always"
        Public Shared ReadOnly wmode As eWmode = eWmode.opaque
        Public Shared ReadOnly rollover As Boolean = False
        Public Shared ReadOnly removeCompleted As Boolean = False
        ' Private Options
        Public Shared ReadOnly uploader As String = ClientScriptManager.GetWebResourceUrl(GetType(uploadify), "JFramework.WebControl.uploadify.swf")
        Public Shared ReadOnly cancelImg As String = ClientScriptManager.GetWebResourceUrl(GetType(uploadify), "JFramework.WebControl.cancel.png")
        Public Shared ReadOnly expressInstall As String = ClientScriptManager.GetWebResourceUrl(GetType(uploadify), "JFramework.WebControl.expressInstall.swf")
        Public Shared ReadOnly hideButton As Boolean = False
        Public Shared ReadOnly buttonImg As String = ClientScriptManager.GetWebResourceUrl(GetType(uploadify), "JFramework.WebControl.button.jpg")
        ' Public Options
        Public Shared ReadOnly buttonText As String = "SELECT FILES"
        Public Shared ReadOnly auto As Boolean = True
        Public Shared ReadOnly multi As Boolean = True
        Public Shared ReadOnly fileDesc As String = Nothing
        Public Shared ReadOnly fileExt As String = Nothing
        Public Shared ReadOnly queueSizeLimit As Integer = 999
        Public Shared ReadOnly simUploadLimit As Integer = 1
        Public Shared ReadOnly sizeLimit As Integer = Nothing
        Public Shared ReadOnly checkScript As String = Nothing
        Public Shared ReadOnly method As eMethod = eMethod.POST
        Public Shared ReadOnly folder As String = "uploads"
        Public Shared ReadOnly fileDataName As String = "Filedata"
        Public Shared ReadOnly scriptData As String = Nothing
    End Class
#End Region
#Region "Obsolete Options"
#Region "height(optional)"
    ''' <summary>The height of the browse button.</summary>
    <Obsolete("This attribute seem not work")> _
    Public Property ButtonHeight() As Integer
        Get
            Dim o As Object = ViewState("ButtonHeight")
            If o Is Nothing Then
                o = DefaultOptions.height
            End If
            Return CType(o, Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("ButtonHeight") = value
        End Set
    End Property
#End Region
#Region "width(optional)"
    ''' <summary>The width of the browse button.</summary>
    <Obsolete("This attribute seem not work")> _
    Public Property ButtonWidth() As Integer
        Get
            Dim o As Object = ViewState("ButtonWidth")
            If o Is Nothing Then
                o = DefaultOptions.width
            End If
            Return CType(o, Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("ButtonWidth") = value
        End Set
    End Property
#End Region
#Region "displayData(optional)"
    ''' <summary>The type of data to display in the queue item during an upload.</summary>
    <Obsolete("This attribute seem not work")> _
    Public Property displayData() As eDisplayData
        Get
            Dim o As Object = ViewState("displayData")
            If o Is Nothing Then
                o = DefaultOptions.displayData
            End If
            Return CType(o, eDisplayData)
        End Get
        Set(ByVal value As eDisplayData)
            ViewState("displayData") = value
        End Set
    End Property
#End Region
#Region "scriptAccess(optional)"
    ''' <summary>The access mode for scripts in the main swf file.</summary>
    <Obsolete("This attribute seem not work")> _
    Public Property scriptAccess() As String
        Get
            Dim o As Object = ViewState("scriptAccess")
            If o Is Nothing Then
                o = DefaultOptions.scriptAccess
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("scriptAccess") = value
        End Set
    End Property
#End Region
#Region "wmode(optional)"
    ''' <summary>The wmode of the flash file.</summary>
    <Obsolete("This attribute seem not work")> _
    Public Property wmode() As eWmode
        Get
            Dim o As Object = ViewState("wmode")
            If o Is Nothing Then
                o = DefaultOptions.wmode
            End If
            Return CType(o, eWmode)
        End Get
        Set(ByVal value As eWmode)
            ViewState("wmode") = value
        End Set
    End Property
#End Region
#Region "rollover(internal-required)"
    ''' <summary>Enable to activate rollover states for your browse button.</summary>
    <Obsolete("This attribute seem not work")> _
    Private ReadOnly Property rollover() As Boolean
        Get
            Dim o As Object = ViewState("rollover")
            If o Is Nothing Then
                o = DefaultOptions.rollover
            End If
            Return CType(o, Boolean)
        End Get
    End Property
#End Region
#Region "removeCompleted(optional)"
    ''' <summary>Enable automatic removal of the queue item for completed uploads.</summary>
    <Obsolete("This attribute seem not work")> _
    Public Property removeCompleted() As Boolean
        Get
            Dim o As Object = ViewState("removeCompleted")
            If o Is Nothing Then
                o = DefaultOptions.removeCompleted
            End If
            Return CType(o, Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("removeCompleted") = value
        End Set
    End Property
#End Region
#End Region
#Region "Private  Options"
#Region "uploader(internal-required)"
    ''' <summary>The path to the uploadify.swf file.</summary>
    Private ReadOnly Property uploader() As String
        Get
            Dim o As Object = ViewState("uploader")
            If o Is Nothing Then
                o = DefaultOptions.uploader
            End If
            Return CType(o, String)
        End Get
    End Property
#End Region
#Region "queueID(internal-required)"
    ''' <summary>The ID of the element on the page you want to use as your file queue.</summary>
    Private ReadOnly Property queueID() As String
        Get
            Return fileQueue.ClientID
        End Get
    End Property
#End Region
#Region "cancelImg(internal-required)"
    ''' <summary>The path to an image you would like to use as the cancel button.</summary>
    ''' <remarks>required</remarks>
    Private ReadOnly Property cancelImg() As String
        Get
            Dim o As Object = ViewState("cancelImg")
            If o Is Nothing Then
                o = DefaultOptions.cancelImg
            End If
            Return CType(o, String)
        End Get
    End Property
#End Region
#Region "expressInstall(internal-required)"
    ''' <summary>The path to the expressInstall.swf file.</summary>
    ''' <remarks>Required:optional</remarks>
    Private ReadOnly Property expressInstall() As String
        Get
            Dim o As Object = ViewState("expressInstall")
            If o Is Nothing Then
                o = DefaultOptions.expressInstall
            End If
            Return CType(o, String)
        End Get
    End Property
#End Region
#Region "hideButton(internal-required)"
    ''' <summary>Enable to hide the flash button so you can style the underlying DIV element.</summary>
    Private ReadOnly Property hideButton() As Boolean
        Get
            Dim o As Object = ViewState("hideButton")
            If o Is Nothing Then
                o = DefaultOptions.hideButton
            End If
            Return CType(o, Boolean)
        End Get
    End Property
#End Region
#Region "buttonImg(internal-required)"
    ''' <summary>The path to an image you would like to use as the browse button.</summary>
    Private ReadOnly Property buttonImg() As String
        Get
            Dim o As Object = Nothing
            If String.IsNullOrEmpty(buttonText) Then
                o = DefaultOptions.buttonImg
            End If
            Return CType(o, String)
        End Get
    End Property
#End Region
#End Region
#Region "Public   Options"
#Region "buttonText(optional)"
    ''' <summary>The text that appears on the default button.</summary>
    Public Property buttonText() As String
        Get
            Dim o As Object = ViewState("buttonText")
            If o Is Nothing Then
                o = DefaultOptions.buttonText
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("buttonText") = value
        End Set
    End Property
#End Region
#Region "auto(optional)"
    ''' <summary>Automatically upload files as they are added to the queue.</summary>
    Public Property auto() As Boolean
        Get
            Dim o As Object = ViewState("auto")
            If o Is Nothing Then
                o = DefaultOptions.auto
            End If
            Return CType(o, Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("auto") = value
        End Set
    End Property
#End Region
#Region "multi(optional)"
    ''' <summary>Allow multiple file uploads.</summary>
    Public Property multi() As Boolean
        Get
            Dim o As Object = ViewState("multi")
            If o Is Nothing Then
                o = DefaultOptions.multi
            End If
            Return CType(o, Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("multi") = value
        End Set
    End Property
#End Region
#Region "fileDesc(optional - required when using fileExt)"
    ''' <summary>The text that will appear in the file type drop down at the bottom of the browse dialog box.</summary>
    Public Property fileDesc() As String
        Get
            Dim o As Object = ViewState("fileDesc")
            If o Is Nothing Then
                o = DefaultOptions.fileDesc
            End If
            If fileExt IsNot Nothing AndAlso o Is Nothing Then
                'Throw New ArgumentException("You must specify fileDesc when using fileExt")
                o = String.Format("Support {0}", fileExt)
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("fileDesc") = value
        End Set
    End Property
#End Region
#Region "fileExt(optional - format like '*.png;*.jpg;*.gif')"
    ''' <summary>A list of file extensions that are allowed for upload.</summary>
    Public Property fileExt() As String
        Get
            Dim o As Object = ViewState("fileExt")
            If o Is Nothing Then
                o = DefaultOptions.fileExt
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("fileExt") = value
        End Set
    End Property
#End Region
#Region "queueSizeLimit(optional)"
    ''' <summary>The limit of files that can be in the queue at one time.</summary>
    Public Property queueSizeLimit() As Integer
        Get
            Dim o As Object = ViewState("queueSizeLimit")
            If o Is Nothing Then
                o = DefaultOptions.queueSizeLimit
            End If
            Return CType(o, Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("queueSizeLimit") = value
        End Set
    End Property
#End Region
#Region "simUploadLimit(optional)"
    ''' <summary>The limit of uploads that can run simultaneously per Uploadify instance.</summary>
    Public Property simUploadLimit() As Integer
        Get
            Dim o As Object = ViewState("simUploadLimit")
            If o Is Nothing Then
                o = DefaultOptions.simUploadLimit
            End If
            Return CType(o, Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("simUploadLimit") = value
        End Set
    End Property
#End Region
#Region "sizeLimit(optional)"
    ''' <summary>The size limit in bytes for each file upload.</summary>
    Public Property sizeLimit() As Integer
        Get
            Dim o As Object = ViewState("sizeLimit")
            If o Is Nothing Then
                o = DefaultOptions.sizeLimit
            End If
            Return CType(o, Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("sizeLimit") = value
        End Set
    End Property
#End Region

#Region "script(required)"
    ''' <summary>The path to the back-end script that will process the file uploads.</summary>
    Public Property script() As String
        Get
            Dim o As Object = ViewState("script")
            If o Is Nothing Then
                Throw New ArgumentException("You must specify the path to the back-end script that will process the file uploads")
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("script") = value
        End Set
    End Property
#End Region
#Region "checkScript(optional)"
    ''' <summary>The path to the back-end script that checks for pre-existing files on the server.</summary>
    Public Property checkScript() As String
        Get
            Dim o As Object = ViewState("checkScript")
            If o Is Nothing Then
                o = DefaultOptions.checkScript
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("checkScript") = value
        End Set
    End Property
#End Region
#Region "method(optional)"
    ''' <summary>The form method for sending scriptData to the back-end script.</summary>
    ''' <remarks>Required:optional</remarks>
    Public Property method() As String
        Get
            Dim o As Object = ViewState("method")
            If o Is Nothing Then
                o = DefaultOptions.method
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("method") = value
        End Set
    End Property
#End Region
#Region "folder(optional)"
    ''' <summary>The path to the folder where you want to save the files.</summary>
    Public Property folder() As String
        Get
            Dim o As Object = ViewState("folder")
            If o Is Nothing Then
                o = DefaultOptions.folder
            End If
            If String.IsNullOrEmpty(o) Then
                Throw New ArgumentException("You must specify the path to the folder where you want to save the files")
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("folder") = value
        End Set
    End Property
#End Region
#Region "fileDataName(optional)"
    ''' <summary>The name of your files array in the back-end script.</summary>
    ''' <remarks>
    ''' If you want to handle different uploadify in the same upload handler,
    ''' you can sepcify different fileDataName to tell if the PostedFile comes from
    ''' 
    ''' If aboving situation happens, you need create your own handler to handle
    ''' different PostedFile source
    ''' </remarks>
    Public Property fileDataName() As String
        Get
            Dim o As Object = ViewState("fileDataName")
            If o Is Nothing Then
                o = DefaultOptions.fileDataName
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("fileDataName") = value
        End Set
    End Property
#End Region
#Region "scriptData(optional)"
    ''' <summary>
    ''' An object containing name/value pairs with additional information 
    ''' that should be sent to the back-end script when processing a file upload.
    ''' </summary>
    ''' <remarks>
    ''' If your scriptData property pass a json object like 
    ''' {'firstName':'Ronnie','age':30}
    ''' 
    ''' you can access it via
    ''' context.Request("firstName")
    ''' context.Request("age")
    ''' </remarks>
    Public Property scriptData() As String
        Get
            Dim o As Object = ViewState("scriptData")
            If o Is Nothing Then
                o = DefaultOptions.scriptData
            End If
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("scriptData") = value
        End Set
    End Property
#End Region
#End Region
#End Region

#Region "Events"
#Region "onAllComplete"
    ''' <summary>
    ''' Triggers once when all files in the queue have finished uploading.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event,data) { some code }
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onAllComplete() As String
        Get
            Dim o As Object = ViewState("onAllComplete")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onAllComplete") = value
        End Set
    End Property
#End Region
#Region "onCancel"
    ''' <summary>
    ''' Triggers once for each file that is removed from the queue.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event,ID,fileObj,data) {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onCancel() As String
        Get
            Dim o As Object = ViewState("onCancel")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onCancel") = value
        End Set
    End Property
#End Region
#Region "onCheck"
    ''' <summary>
    ''' Triggers at the beginning of an upload if a file with the same name is detected.
    ''' </summary>
    ''' <remarks>
    ''' format    : function() {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onCheck() As String
        Get
            Dim o As Object = ViewState("onCheck")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onCheck") = value
        End Set
    End Property
#End Region
#Region "onClearQueue"
    ''' <summary>
    ''' Triggers once when the uploadifyClearQueue() method is called.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event) {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onClearQueue() As String
        Get
            Dim o As Object = ViewState("onClearQueue")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onClearQueue") = value
        End Set
    End Property
#End Region
#Region "onComplete"
    ''' <summary>
    ''' Triggers once for each file upload that is completed.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event, ID, fileObj, response, data) {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onComplete() As String
        Get
            Dim o As Object = ViewState("onComplete")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onComplete") = value
        End Set
    End Property
#End Region
#Region "onError"
    ''' <summary>
    ''' Triggers when an error is returned for a file upload.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event,ID,fileObj,errorObj) {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onError() As String
        Get
            Dim o As Object = ViewState("onError")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onError") = value
        End Set
    End Property
#End Region
#Region "onInit"
#If 0 Then
    ''' <summary>
    ''' Triggers when the Uploadify instance is loaded.
    ''' </summary>
    ''' <remarks>
    ''' format    : function() {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onInit() As String
        Get
            Dim o As Object = ViewState("onInit")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onInit") = value
        End Set
    End Property
#End If
#End Region
#Region "onOpen"
    ''' <summary>
    ''' Triggers once when a file in the queue begins uploading.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event,ID,fileObj) {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onOpen() As String
        Get
            Dim o As Object = ViewState("onOpen")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onOpen") = value
        End Set
    End Property
#End Region
#Region "onProgess"
    ''' <summary>
    ''' Triggers each time the progress of a file upload changes.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event,ID,fileObj,data) {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onProgess() As String
        Get
            Dim o As Object = ViewState("onProgess")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onProgess") = value
        End Set
    End Property
#End Region
#Region "onQueueFull"
    ''' <summary>
    ''' Triggers when the number of files in the queue matches the queueSizeLimit.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event,queueSizeLimit) {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onQueueFull() As String
        Get
            Dim o As Object = ViewState("onQueueFull")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onQueueFull") = value
        End Set
    End Property
#End Region
#Region "onSelect"
    ''' <summary>
    ''' Triggers once for each file that is added to the queue.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event,ID,fileObj) {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onSelect() As String
        Get
            Dim o As Object = ViewState("onSelect")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onSelect") = value
        End Set
    End Property
#End Region
#Region "onSelectOnce"
    ''' <summary>
    ''' Triggers once each time a file or group of files is added to the queue.
    ''' </summary>
    ''' <remarks>
    ''' format    : function(event,data) {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onSelectOnce() As String
        Get
            Dim o As Object = ViewState("onSelectOnce")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onSelectOnce") = value
        End Set
    End Property
#End Region
#Region "onSWFReady"
    ''' <summary>
    ''' Triggers when the flash file is done loading.
    ''' </summary>
    ''' <remarks>
    ''' format    : function() {}
    ''' arguments : http://www.uploadify.com/documentation/
    ''' </remarks>
    Public Property onSWFReady() As String
        Get
            Dim o As Object = ViewState("onSWFReady")
            Return CType(o, String)
        End Get
        Set(ByVal value As String)
            ViewState("onSWFReady") = value
        End Set
    End Property
#End Region
#End Region

#Region "Helpers"
    Private Function ToJsonValue(ByVal value As Object) As String
        Dim Result As String = "null"
        If value IsNot Nothing Then
            If TypeOf value Is String Then
                If -1 = CType(value, String).IndexOf("'") Then
                    ' [string]
                    Result = String.Format("'{0}'", value.ToString())
                Else
                    ' [json object]
                    ' if string contain quotes, we treat that as a object
                    Result = String.Format("{0}", value.ToString().ToLower())
                End If
            Else
                If TypeOf value Is eDisplayData OrElse _
                   TypeOf value Is eWmode OrElse _
                   TypeOf value Is eMethod Then
                    ' these are special key string in uploadify.js
                    Result = String.Format("'{0}'", value.ToString())
                Else
                    ' [boolean] or [integer]
                    Result = String.Format("{0}", value.ToString().ToLower())
                End If
            End If
        End If
        Return Result
    End Function

    Private Function GetOption(ByVal key As String, ByVal value As Object, Optional ByVal includeComma As Boolean = True) As String
        Dim [option] As String = String.Empty
        If includeComma Then [option] += String.Format(", ")
        [option] += String.Format("'{0}'", key)
        [option] += String.Format(": ")
        [option] += String.Format("{0}", ToJsonValue(value))
        Return [option]
    End Function

    Private Sub AttachEvent(ByVal scripts As StringBuilder, _
                                 ByVal key As String, _
                                 ByVal value As Object, _
                                 Optional ByVal includeComma As Boolean = True)
        If value IsNot Nothing Then
            Dim [event] As String = String.Empty
            If includeComma Then [event] += String.Format(", ")
            [event] += String.Format("'{0}'", key)
            [event] += String.Format(": ")
            [event] += String.Format("{0}", value)
            scripts.Append([event])
        End If
    End Sub
#End Region

#Region "Handler"
    ''' <summary>an example for saving uploaded file</summary>
    ''' <remarks>
    ''' this method is an example for saving uploaded file
    ''' 
    ''' if you have no idea how to deal with file upload process
    ''' you can just call this function on ProcessRequest at xxx.ashx
    ''' </remarks>
    Public Shared Sub Handle(ByVal context As HttpContext)
        context.Response.ContentType = "text/plain"
        context.Response.Charset = "utf-8"

        ' if you pass 'scriptData': {'firstName':'Ronnie','age':30}
        ' you can access variables using context.Request(...) like following
        'Dim firstName As String = context.Request("firstName")
        'Dim age As String = context.Request("age")

        Dim file As HttpPostedFile = context.Request.Files(DefaultOptions.fileDataName)
        Dim uploadPath As String = HttpContext.Current.Server.MapPath(context.Request("folder")) & "\"

        If file IsNot Nothing Then
            If Not Directory.Exists(uploadPath) Then
                Directory.CreateDirectory(uploadPath)
            End If
            file.SaveAs(uploadPath + file.FileName)
            context.Response.Write("1")
        Else
            context.Response.Write("0")
        End If
    End Sub

    ''' <summary>
    ''' an example for check uploaded file whether already exist on server
    ''' </summary>
    ''' <remarks>
    ''' this method is an example for check duplicated uploaded file
    ''' 
    ''' if you have no idea how to check duplicated uploaded file
    ''' you can just call this function on ProcessRequest at xxx.ashx
    ''' </remarks>
    Public Shared Sub CheckDuplicatedFile(ByVal context As HttpContext)
        ' http://www.uploadify.com/forums/discussion/2372/checkscript-and-asp.net/p1
        Dim fileArray As New Dictionary(Of String, String)

        For x As Integer = 0 To context.Request.Form.Count - 1
            If context.Request.Form.Keys(x) <> "folder" Then
                Dim uploadPath As String = HttpContext.Current.Server.MapPath(context.Request("folder")) & "\"
                Dim filename As String = context.Request.Form.Item(x)
                Dim filepath As String = uploadPath + filename
                If File.Exists(filepath) Then
                    fileArray.Add(context.Request.Form.Keys(x), context.Request.Form.Item(x))
                End If
            End If
        Next

        Dim jSer As JavaScriptSerializer = New JavaScriptSerializer
        context.Response.ContentType = "application/json"
        Dim json As String = jSer.Serialize(fileArray)
        context.Response.Write(json)
    End Sub
#End Region

End Class
