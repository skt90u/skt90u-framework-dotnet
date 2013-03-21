using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// 實作uploadify
    /// </summary>
    /// <remarks>
    /// http://www.uploadify.com/documentation/
    /// </remarks>
    public class uploadify : System.Web.UI.WebControls.CompositeControl
    {

	    #region "Private"
	    private Panel fileQueue = new Panel();

	    private FileUpload browseButton = new FileUpload();

        /// <summary>
        /// OnLoad Event
        /// </summary>
	    protected override void OnLoad(System.EventArgs e)
	    {
		    base.OnLoad(e);
		    if (Page.IsPostBack) {
			    EnsureChildControls();
		    }
	    }

        /// <summary>
        /// override CreateChildControls
        /// </summary>
	    protected override void CreateChildControls()
	    {
		    fileQueue.ID = "fileQueue";
		    fileQueue.CssClass = "fileQueue";
		    Controls.Add(fileQueue);

		    browseButton.ID = "browseButton";
		    Controls.Add(browseButton);

		    if (!auto) {
			    // if files don't upload automatically,
			    // create buttons for upload files and cancel queue
			    HyperLink uploadButton = CreateUploadButton();
			    Controls.Add(uploadButton);

			    HyperLink cancelButton = CreateCancelAllButton();
			    Controls.Add(cancelButton);
		    }
	    }

	    /// <summary>create a button that can trigger the file upload</summary>
	    private HyperLink CreateUploadButton()
	    {
		    HyperLink uploadButton = new HyperLink();
		    uploadButton.ID = "uploadButton";
		    uploadButton.NavigateUrl = string.Format("javascript:$('#{0}').uploadifyUpload();", browseButton.ClientID);
		    uploadButton.Text = "Upload Files";
		    return uploadButton;
	    }

	    /// <summary>
	    /// create a button that can remove all files from the file upload queue and 
	    /// cancel any file uploads that are in progress.
	    /// </summary>
	    private HyperLink CreateCancelAllButton()
	    {
		    HyperLink cancelButton = new HyperLink();
		    cancelButton.ID = "uploadButton";
		    cancelButton.NavigateUrl = string.Format("javascript:$('#{0}').uploadifyClearQueue();", browseButton.ClientID);
		    cancelButton.Text = "Clear Queue";
		    return cancelButton;
	    }

        /// <summary>
        /// OnPreRender Event
        /// </summary>
	    protected override void OnPreRender(System.EventArgs e)
	    {
		    Type type = this.GetType();

		    // uploadify stylesheet
            ClientScriptManager.RegisterEmbeddedCSS(type, "JUtil.Web.WebControls.uploadify.css.uploadify.default.css");
            ClientScriptManager.RegisterEmbeddedCSS(type, "JUtil.Web.WebControls.uploadify.css.uploadify.css");

		    // uploadify required javascript
            ClientScriptManager.RegisterEmbeddedJs(type, "JUtil.Web.WebControls.jQuery.jquery.1.4.4.js");

		    // uploadify dependence javascript
		    List<string> lstWebResource = new List<string>();
            lstWebResource.Add("JUtil.Web.WebControls.uploadify.scripts.swfobject.js");
            lstWebResource.Add("JUtil.Web.WebControls.uploadify.scripts.jquery.uploadify.v2.1.0.min.js");
		    ClientScriptManager.RegisterCompositeScript(lstWebResource);

		    // uploadify initilization javascript
		    string scripts = GenApplicationLoadScript();
		    ClientScriptManager.RegisterClientApplicationLoadScript(this, scripts);
	    }

	    private string GenApplicationLoadScript()
	    {
		    StringBuilder scripts = new StringBuilder();

            scripts.Append("$('#" + browseButton.ClientID + "').uploadify({");
		    // ------------------------------------------------------------
		    // Obsolete Options
		    // ------------------------------------------------------------
		    //scripts.Append(GetOption("height", ButtonHeight))
		    //scripts.Append(GetOption("width", ButtonWidth))
		    //scripts.Append(GetOption("displayData", displayData))
		    //scripts.Append(GetOption("scriptAccess", scriptAccess))
		    //scripts.Append(GetOption("wmode", wmode))
		    //scripts.Append(GetOption("rollover", rollover))
		    //scripts.Append(GetOption("removeCompleted", removeCompleted))
		    // ------------------------------------------------------------
		    // Private Options
		    // ------------------------------------------------------------
		    scripts.Append(GetOption("uploader", uploader, false));
		    scripts.Append(GetOption("queueID", queueID));
		    scripts.Append(GetOption("cancelImg", cancelImg));
		    scripts.Append(GetOption("expressInstall", expressInstall));
		    scripts.Append(GetOption("hideButton", hideButton));
		    scripts.Append(GetOption("buttonImg", buttonImg));
		    // ------------------------------------------------------------
		    // Public Options
		    // ------------------------------------------------------------
		    scripts.Append(GetOption("buttonText", buttonText));
		    scripts.Append(GetOption("auto", auto));
		    scripts.Append(GetOption("multi", multi));
		    scripts.Append(GetOption("fileDesc", fileDesc));
		    scripts.Append(GetOption("fileExt", fileExt));
		    scripts.Append(GetOption("queueSizeLimit", queueSizeLimit));
		    scripts.Append(GetOption("simUploadLimit", simUploadLimit));
		    scripts.Append(GetOption("sizeLimit", sizeLimit));

		    scripts.Append(GetOption("script", script));
		    scripts.Append(GetOption("checkScript", checkScript));
		    scripts.Append(GetOption("method", method));
		    scripts.Append(GetOption("folder", folder));
		    scripts.Append(GetOption("fileDataName", fileDataName));
		    scripts.Append(GetOption("scriptData", scriptData));

		    AttachEvent(scripts, "onAllComplete", onAllComplete);
		    AttachEvent(scripts, "onCancel", onCancel);
		    AttachEvent(scripts, "onCheck", onCheck);
		    AttachEvent(scripts, "onClearQueue", onClearQueue);
		    AttachEvent(scripts, "onComplete", onComplete);
		    AttachEvent(scripts, "onError", onError);
		    //AttachEvent(scripts, "onInit", onInit)
		    AttachEvent(scripts, "onOpen", onOpen);
		    AttachEvent(scripts, "onProgess", onProgess);
		    AttachEvent(scripts, "onQueueFull", onQueueFull);
		    AttachEvent(scripts, "onSelect", onSelect);
		    AttachEvent(scripts, "onSelectOnce", onSelectOnce);
		    AttachEvent(scripts, "onSWFReady", onSWFReady);

		    scripts.Append("});");

            string js = scripts.ToString();
		    return js;
	    }
	    #endregion

	    #region "Options"
	    #region "Default  Options"

        /// <summary>
        /// The type of data to display in the queue item during an upload
        /// </summary>
	    public enum eDisplayData
	    {
            /// <summary>
            /// percentage
            /// </summary>
		    percentage = 0,

            /// <summary>
            /// speed
            /// </summary>
		    speed
	    }

        /// <summary>
        /// The wmode of the flash file
        /// </summary>
	    public enum eWmode
	    {
            /// <summary>
            /// opaque
            /// </summary>
		    opaque = 0,

            /// <summary>
            /// transparent
            /// </summary>
		    transparent
	    }

        /// <summary>
        /// The form method for sending scriptData to the back-end script.
        /// </summary>
	    public enum eMethod
	    {
            /// <summary>
            /// POST
            /// </summary>
		    POST = 0,

            /// <summary>
            /// GET
            /// </summary>
		    GET
	    }

        /// <summary>
        /// Default Options of uploadify 
        /// </summary>
	    public class DefaultOptions
        {
            #region Obsolete Options
            /// <summary>
            /// The height of the browse button
            /// </summary>
		    public static readonly int height = 30;

            /// <summary>
            /// The width of the browse button.
            /// </summary>
		    public static readonly int width = 120;

            /// <summary>
            /// The type of data to display in the queue item during an upload.
            /// </summary>
		    public static readonly eDisplayData displayData = eDisplayData.percentage;

            /// <summary>
            /// The access mode for scripts in the main swf file.
            /// </summary>
		    public static readonly string scriptAccess = "always";

            /// <summary>
            /// The wmode of the flash file.
            /// </summary>
		    public static readonly eWmode wmode = eWmode.opaque;

            /// <summary>
            /// Enable to activate rollover states for your browse button.
            /// </summary>
		    public static readonly bool rollover = false;

            /// <summary>
            /// Enable automatic removal of the queue item for completed uploads.
            /// </summary>
		    public static readonly bool removeCompleted = false;
            #endregion

            #region Private Options
            /// <summary>
            /// The path to the uploadify.swf file.
            /// </summary>
            public static readonly string uploader = ClientScriptManager.GetWebResourceUrl(typeof(uploadify), "JUtil.Web.WebControls.uploadify.scripts.uploadify.swf");

            /// <summary>
            /// The path to an image you would like to use as the cancel button.
            /// </summary>
            public static readonly string cancelImg = ClientScriptManager.GetWebResourceUrl(typeof(uploadify), "JUtil.Web.WebControls.uploadify.images.cancel.png");

            /// <summary>
            /// The path to the expressInstall.swf file.
            /// </summary>
            public static readonly string expressInstall = ClientScriptManager.GetWebResourceUrl(typeof(uploadify), "JUtil.Web.WebControls.uploadify.scripts.expressInstall.swf");

            /// <summary>
            /// Enable to hide the flash button so you can style the underlying DIV element.
            /// </summary>
		    public static readonly bool hideButton = false;

            /// <summary>
            /// The path to an image you would like to use as the browse button.
            /// </summary>
            public static readonly string buttonImg = ClientScriptManager.GetWebResourceUrl(typeof(uploadify), "JUtil.Web.WebControls.uploadify.images.button.jpg");
            #endregion

            #region Public Options

            /// <summary>
            /// The text that appears on the default button.
            /// </summary>
            public static readonly string buttonText = "SELECT FILES";

            /// <summary>
            /// Automatically upload files as they are added to the queue.
            /// </summary>
		    public static readonly bool auto = true;

            /// <summary>
            /// Allow multiple file uploads.
            /// </summary>
		    public static readonly bool multi = true;

            /// <summary>
            /// The text that will appear in the file type drop down at the bottom of the browse dialog box.
            /// </summary>
		    public static readonly string fileDesc = null;

            /// <summary>
            /// A list of file extensions that are allowed for upload.
            /// </summary>
		    public static readonly string fileExt = null;

            /// <summary>
            /// The limit of files that can be in the queue at one time.
            /// </summary>
		    public static readonly int queueSizeLimit = 999;

            /// <summary>
            /// The limit of uploads that can run simultaneously per Uploadify instance
            /// </summary>
		    public static readonly int simUploadLimit = 1;

            /// <summary>
            /// The size limit in bytes for each file upload
            /// </summary>
            public static readonly int sizeLimit = 104857600; /* 100 MB = 100 x 1024 x 1024 */

            /// <summary>
            /// The path to the back-end script that checks for pre-existing files on the server.
            /// </summary>
		    public static readonly string checkScript = null;

            /// <summary>
            /// The form method for sending scriptData to the back-end script.
            /// </summary>
		    public static readonly eMethod method = eMethod.POST;

            /// <summary>
            /// The path to the folder where you want to save the files.
            /// </summary>
		    public static readonly string folder = "uploads";

            /// <summary>
            /// The name of your files array in the back-end script.
            /// </summary>
		    public static readonly string fileDataName = "Filedata";

            /// <summary>
            /// An object containing name/value pairs with additional information that should be sent to the back-end script when processing a file upload.
            /// </summary>
		    public static readonly string scriptData = null;
            #endregion
        }
	    #endregion
	    #region "Obsolete Options"
	    #region "height(optional)"
	    /// <summary>The height of the browse button.</summary>
	    [Obsolete("This attribute seem not work")]
	    public int ButtonHeight {
		    get {
			    object o = ViewState["ButtonHeight"];
			    if (o == null) {
				    o = DefaultOptions.height;
			    }
			    return Convert.ToInt32(o);
		    }
		    set { ViewState["ButtonHeight"] = value; }
	    }
	    #endregion
	    #region "width(optional)"
	    /// <summary>The width of the browse button.</summary>
	    [Obsolete("This attribute seem not work")]
	    public int ButtonWidth {
		    get {
			    object o = ViewState["ButtonWidth"];
			    if (o == null) {
				    o = DefaultOptions.width;
			    }
			    return Convert.ToInt32(o);
		    }
		    set { ViewState["ButtonWidth"] = value; }
	    }
	    #endregion
	    #region "displayData(optional)"
	    /// <summary>The type of data to display in the queue item during an upload.</summary>
	    [Obsolete("This attribute seem not work")]
	    public eDisplayData displayData {
		    get {
			    object o = ViewState["displayData"];
			    if (o == null) {
				    o = DefaultOptions.displayData;
			    }
			    return (eDisplayData)o;
		    }
		    set { ViewState["displayData"] = value; }
	    }
	    #endregion
	    #region "scriptAccess(optional)"
	    /// <summary>The access mode for scripts in the main swf file.</summary>
	    [Obsolete("This attribute seem not work")]
	    public string scriptAccess {
		    get {
			    object o = ViewState["scriptAccess"];
			    if (o == null) {
				    o = DefaultOptions.scriptAccess;
			    }
			    return Convert.ToString(o);
		    }
		    set { ViewState["scriptAccess"] = value; }
	    }
	    #endregion
	    #region "wmode(optional)"
	    /// <summary>The wmode of the flash file.</summary>
	    [Obsolete("This attribute seem not work")]
	    public eWmode wmode {
		    get {
			    object o = ViewState["wmode"];
			    if (o == null) {
				    o = DefaultOptions.wmode;
			    }
			    return (eWmode)o;
		    }
		    set { ViewState["wmode"] = value; }
	    }
	    #endregion
	    #region "rollover(internal-required)"
	    /// <summary>Enable to activate rollover states for your browse button.</summary>
	    [Obsolete("This attribute seem not work")]
	    private bool rollover {
		    get {
			    object o = ViewState["rollover"];
			    if (o == null) {
				    o = DefaultOptions.rollover;
			    }
			    return Convert.ToBoolean(o);
		    }
	    }
	    #endregion
	    #region "removeCompleted(optional)"
	    /// <summary>Enable automatic removal of the queue item for completed uploads.</summary>
	    [Obsolete("This attribute seem not work")]
	    public bool removeCompleted {
		    get {
			    object o = ViewState["removeCompleted"];
			    if (o == null) {
				    o = DefaultOptions.removeCompleted;
			    }
			    return Convert.ToBoolean(o);
		    }
		    set { ViewState["removeCompleted"] = value; }
	    }
	    #endregion
	    #endregion
	    #region "Private  Options"
	    #region "uploader(internal-required)"
	    /// <summary>The path to the uploadify.swf file.</summary>
	    private string uploader {
		    get {
			    object o = ViewState["uploader"];
			    if (o == null) {
				    o = DefaultOptions.uploader;
			    }
			    return Convert.ToString(o);
		    }
	    }
	    #endregion
	    #region "queueID(internal-required)"
	    /// <summary>The ID of the element on the page you want to use as your file queue.</summary>
	    private string queueID {
		    get { return fileQueue.ClientID; }
	    }
	    #endregion
	    #region "cancelImg(internal-required)"
	    /// <summary>The path to an image you would like to use as the cancel button.</summary>
	    /// <remarks>required</remarks>
	    private string cancelImg {
		    get {
			    object o = ViewState["cancelImg"];
			    if (o == null) {
				    o = DefaultOptions.cancelImg;
			    }
			    return Convert.ToString(o);
		    }
	    }
	    #endregion
	    #region "expressInstall(internal-required)"
	    /// <summary>The path to the expressInstall.swf file.</summary>
	    /// <remarks>Required:optional</remarks>
	    private string expressInstall {
		    get {
			    object o = ViewState["expressInstall"];
			    if (o == null) {
				    o = DefaultOptions.expressInstall;
			    }
			    return Convert.ToString(o);
		    }
	    }
	    #endregion
	    #region "hideButton(internal-required)"
	    /// <summary>Enable to hide the flash button so you can style the underlying DIV element.</summary>
	    private bool hideButton {
		    get {
			    object o = ViewState["hideButton"];
			    if (o == null) {
				    o = DefaultOptions.hideButton;
			    }
			    return Convert.ToBoolean(o);
		    }
	    }
	    #endregion
	    #region "buttonImg(internal-required)"
	    /// <summary>The path to an image you would like to use as the browse button.</summary>
	    private string buttonImg {
		    get {
			    object o = null;
			    if (string.IsNullOrEmpty(buttonText)) {
				    o = DefaultOptions.buttonImg;
			    }
			    return Convert.ToString(o);
		    }
	    }
	    #endregion
	    #endregion
	    #region "Public   Options"
	    #region "buttonText(optional)"
	    /// <summary>The text that appears on the default button.</summary>
	    public string buttonText {
		    get {
			    object o = ViewState["buttonText"];
			    if (o == null) {
				    o = DefaultOptions.buttonText;
			    }
			    return Convert.ToString(o);
		    }
		    set { ViewState["buttonText"] = value; }
	    }
	    #endregion
	    #region "auto(optional)"
	    /// <summary>Automatically upload files as they are added to the queue.</summary>
	    public bool auto {
		    get {
			    object o = ViewState["auto"];
			    if (o == null) {
				    o = DefaultOptions.auto;
			    }
			    return Convert.ToBoolean(o);
		    }
		    set { ViewState["auto"] = value; }
	    }
	    #endregion
	    #region "multi(optional)"
	    /// <summary>Allow multiple file uploads.</summary>
	    public bool multi {
		    get {
			    object o = ViewState["multi"];
			    if (o == null) {
				    o = DefaultOptions.multi;
			    }
			    return Convert.ToBoolean(o);
		    }
		    set { ViewState["multi"] = value; }
	    }
	    #endregion
	    #region "fileDesc(optional - required when using fileExt)"
	    /// <summary>The text that will appear in the file type drop down at the bottom of the browse dialog box.</summary>
	    public string fileDesc {
		    get {
			    object o = ViewState["fileDesc"];
			    if (o == null) {
				    o = DefaultOptions.fileDesc;
			    }
			    if (fileExt != null && o == null) {
				    //Throw New ArgumentException("You must specify fileDesc when using fileExt")
				    o = string.Format("Support {0}", fileExt);
			    }
			    return Convert.ToString(o);
		    }
		    set { ViewState["fileDesc"] = value; }
	    }
	    #endregion
	    #region "fileExt(optional - format like '*.png;*.jpg;*.gif')"
	    /// <summary>A list of file extensions that are allowed for upload.</summary>
	    public string fileExt {
		    get {
			    object o = ViewState["fileExt"];
			    if (o == null) {
				    o = DefaultOptions.fileExt;
			    }
			    return Convert.ToString(o);
		    }
		    set { ViewState["fileExt"] = value; }
	    }
	    #endregion
	    #region "queueSizeLimit(optional)"
	    /// <summary>The limit of files that can be in the queue at one time.</summary>
	    public int queueSizeLimit {
		    get {
			    object o = ViewState["queueSizeLimit"];
			    if (o == null) {
				    o = DefaultOptions.queueSizeLimit;
			    }
			    return Convert.ToInt32(o);
		    }
		    set { ViewState["queueSizeLimit"] = value; }
	    }
	    #endregion
	    #region "simUploadLimit(optional)"
	    /// <summary>The limit of uploads that can run simultaneously per Uploadify instance.</summary>
	    public int simUploadLimit {
		    get {
			    object o = ViewState["simUploadLimit"];
			    if (o == null) {
				    o = DefaultOptions.simUploadLimit;
			    }
			    return Convert.ToInt32(o);
		    }
		    set { ViewState["simUploadLimit"] = value; }
	    }
	    #endregion
	    #region "sizeLimit(optional)"
	    /// <summary>The size limit in bytes for each file upload.</summary>
	    public int sizeLimit {
		    get {
			    object o = ViewState["sizeLimit"];
			    if (o == null) {
				    o = DefaultOptions.sizeLimit;
			    }
			    return Convert.ToInt32(o);
		    }
		    set { ViewState["sizeLimit"] = value; }
	    }
	    #endregion

	    #region "script(required)"
	    /// <summary>The path to the back-end script that will process the file uploads.</summary>
	    public string script {
		    get {
			    object o = ViewState["script"];
			    if (o == null) {
				    throw new ArgumentException("You must specify the path to the back-end script that will process the file uploads");
			    }
			    return Convert.ToString(o);
		    }
		    set { ViewState["script"] = value; }
	    }
	    #endregion
	    #region "checkScript(optional)"
	    /// <summary>The path to the back-end script that checks for pre-existing files on the server.</summary>
	    public string checkScript {
		    get {
			    object o = ViewState["checkScript"];
			    if (o == null) {
				    o = DefaultOptions.checkScript;
			    }
			    return Convert.ToString(o);
		    }
		    set { ViewState["checkScript"] = value; }
	    }
	    #endregion
	    #region "method(optional)"
	    /// <summary>The form method for sending scriptData to the back-end script.</summary>
	    /// <remarks>Required:optional</remarks>
	    public string method {
		    get {
			    object o = ViewState["method"];
			    if (o == null) {
				    o = DefaultOptions.method;
			    }
			    return Convert.ToString(o);
		    }
		    set { ViewState["method"] = value; }
	    }
	    #endregion
	    #region "folder(optional)"
	    /// <summary>The path to the folder where you want to save the files.</summary>
	    public string folder {
		    get {
			    object o = ViewState["folder"];
			    if (o == null) {
				    o = DefaultOptions.folder;
			    }

                string theFolder = Convert.ToString(o);
                if (string.IsNullOrEmpty(theFolder))
                {
				    throw new ArgumentException("You must specify the path to the folder where you want to save the files");
			    }
                return theFolder;
		    }
		    set { ViewState["folder"] = value; }
	    }
	    #endregion
	    #region "fileDataName(optional)"
	    /// <summary>The name of your files array in the back-end script.</summary>
	    /// <remarks>
	    /// If you want to handle different uploadify in the same upload handler,
	    /// you can sepcify different fileDataName to tell if the PostedFile comes from
	    /// 
	    /// If aboving situation happens, you need create your own handler to handle
	    /// different PostedFile source
	    /// </remarks>
	    public string fileDataName {
		    get {
			    object o = ViewState["fileDataName"];
			    if (o == null) {
				    o = DefaultOptions.fileDataName;
			    }
			    return Convert.ToString(o);
		    }
		    set { ViewState["fileDataName"] = value; }
	    }
	    #endregion
	    #region "scriptData(optional)"
	    /// <summary>
	    /// An object containing name/value pairs with additional information 
	    /// that should be sent to the back-end script when processing a file upload.
	    /// </summary>
	    /// <remarks>
	    /// If your scriptData property pass a json object like 
	    /// {'firstName':'Ronnie','age':30}
	    /// 
	    /// you can access it via
	    /// context.Request("firstName")
	    /// context.Request("age")
	    /// </remarks>
	    public string scriptData {
		    get {
			    object o = ViewState["scriptData"];
			    if (o == null) {
				    o = DefaultOptions.scriptData;
			    }
			    return Convert.ToString(o);
		    }
		    set { ViewState["scriptData"] = value; }
	    }
	    #endregion
	    #endregion
	    #endregion

	    #region "Events"
	    #region "onAllComplete"
	    /// <summary>
	    /// Triggers once when all files in the queue have finished uploading.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event,data) { some code }
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onAllComplete {
		    get {
			    object o = ViewState["onAllComplete"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onAllComplete"] = value; }
	    }
	    #endregion
	    #region "onCancel"
	    /// <summary>
	    /// Triggers once for each file that is removed from the queue.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event,ID,fileObj,data) {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onCancel {
		    get {
			    object o = ViewState["onCancel"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onCancel"] = value; }
	    }
	    #endregion
	    #region "onCheck"
	    /// <summary>
	    /// Triggers at the beginning of an upload if a file with the same name is detected.
	    /// </summary>
	    /// <remarks>
	    /// format    : function() {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onCheck {
		    get {
			    object o = ViewState["onCheck"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onCheck"] = value; }
	    }
	    #endregion
	    #region "onClearQueue"
	    /// <summary>
	    /// Triggers once when the uploadifyClearQueue() method is called.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event) {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onClearQueue {
		    get {
			    object o = ViewState["onClearQueue"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onClearQueue"] = value; }
	    }
	    #endregion
	    #region "onComplete"
	    /// <summary>
	    /// Triggers once for each file upload that is completed.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event, ID, fileObj, response, data) {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onComplete {
		    get {
			    object o = ViewState["onComplete"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onComplete"] = value; }
	    }
	    #endregion
	    #region "onError"
	    /// <summary>
	    /// Triggers when an error is returned for a file upload.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event,ID,fileObj,errorObj) {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onError {
		    get {
			    object o = ViewState["onError"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onError"] = value; }
	    }
	    #endregion
	    #region "onInit"
	    #if false
	    /// <summary>
	    /// Triggers when the Uploadify instance is loaded.
	    /// </summary>
	    /// <remarks>
	    /// format    : function() {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onInit {
		    get {
			    object o = ViewState["onInit"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onInit"] = value; }
	    }
	    #endif
	    #endregion
	    #region "onOpen"
	    /// <summary>
	    /// Triggers once when a file in the queue begins uploading.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event,ID,fileObj) {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onOpen {
		    get {
			    object o = ViewState["onOpen"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onOpen"] = value; }
	    }
	    #endregion
	    #region "onProgess"
	    /// <summary>
	    /// Triggers each time the progress of a file upload changes.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event,ID,fileObj,data) {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onProgess {
		    get {
			    object o = ViewState["onProgess"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onProgess"] = value; }
	    }
	    #endregion
	    #region "onQueueFull"
	    /// <summary>
	    /// Triggers when the number of files in the queue matches the queueSizeLimit.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event,queueSizeLimit) {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onQueueFull {
		    get {
			    object o = ViewState["onQueueFull"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onQueueFull"] = value; }
	    }
	    #endregion
	    #region "onSelect"
	    /// <summary>
	    /// Triggers once for each file that is added to the queue.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event,ID,fileObj) {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onSelect {
		    get {
			    object o = ViewState["onSelect"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onSelect"] = value; }
	    }
	    #endregion
	    #region "onSelectOnce"
	    /// <summary>
	    /// Triggers once each time a file or group of files is added to the queue.
	    /// </summary>
	    /// <remarks>
	    /// format    : function(event,data) {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onSelectOnce {
		    get {
			    object o = ViewState["onSelectOnce"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onSelectOnce"] = value; }
	    }
	    #endregion
	    #region "onSWFReady"
	    /// <summary>
	    /// Triggers when the flash file is done loading.
	    /// </summary>
	    /// <remarks>
	    /// format    : function() {}
	    /// arguments : http://www.uploadify.com/documentation/
	    /// </remarks>
	    public string onSWFReady {
		    get {
			    object o = ViewState["onSWFReady"];
			    return Convert.ToString(o);
		    }
		    set { ViewState["onSWFReady"] = value; }
	    }
	    #endregion
	    #endregion

	    #region "Helpers"
	    private string ToJsonValue(object value)
	    {
		    string Result = "null";
		    if (value != null) {
			    if (value is string) {
				    if (-1 == Convert.ToString(value).IndexOf("'")) {
					    // [string]
					    Result = string.Format("'{0}'", value.ToString());
				    } else {
					    // [json object]
					    // if string contain quotes, we treat that as a object
					    Result = string.Format("{0}", value.ToString().ToLower());
				    }
			    } else {
				    if (value is eDisplayData || value is eWmode || value is eMethod) {
					    // these are special key string in uploadify.js
					    Result = string.Format("'{0}'", value.ToString());
				    } else {
					    // [boolean] or [integer]
					    Result = string.Format("{0}", value.ToString().ToLower());
				    }
			    }
		    }
		    return Result;
	    }

	    private string GetOption(string key, object value, bool includeComma)
	    {
		    string option = string.Empty;
		    if (includeComma)
			    option += string.Format(", ");
		    option += string.Format("'{0}'", key);
		    option += string.Format(": ");
		    option += string.Format("{0}", ToJsonValue(value));
		    return option;
	    }

        private string GetOption(string key, object value)
	    {
            return GetOption(key, value, true);
	    }

	    private void AttachEvent(StringBuilder scripts, string key, object value, bool includeComma)
	    {
            string fn = Convert.ToString(value);
            if (!string.IsNullOrEmpty(fn))
            {
                string el = string.Empty;
                if (includeComma)
                    el += string.Format(", ");
                el += string.Format("'{0}'", key);
                el += string.Format(": ");
                el += string.Format("{0}", value);
                scripts.Append(el);
            }
	    }

        private void AttachEvent(StringBuilder scripts, string key, object value)
	    {
		    AttachEvent(scripts, key, value, true);
	    }
	    #endregion

	    #region "Handler"
	    /// <summary>an example for saving uploaded file</summary>
	    /// <remarks>
	    /// this method is an example for saving uploaded file
	    /// 
	    /// if you have no idea how to deal with file upload process
	    /// you can just call this function on ProcessRequest at xxx.ashx
	    /// </remarks>
	    public static void Handle(HttpContext context)
	    {
		    context.Response.ContentType = "text/plain";
		    context.Response.Charset = "utf-8";

		    // if you pass 'scriptData': {'firstName':'Ronnie','age':30}
		    // you can access variables using context.Request(...) like following
		    //Dim firstName As String = context.Request("firstName")
		    //Dim age As String = context.Request("age")

		    HttpPostedFile file = context.Request.Files[DefaultOptions.fileDataName];
		    string uploadPath = HttpContext.Current.Server.MapPath(context.Request["folder"]) + "\\";

		    if (file != null) {
			    if (!Directory.Exists(uploadPath)) {
				    Directory.CreateDirectory(uploadPath);
			    }
			    file.SaveAs(uploadPath + file.FileName);
			    context.Response.Write("1");
		    } else {
			    context.Response.Write("0");
		    }
	    }

	    /// <summary>
	    /// an example for check uploaded file whether already exist on server
	    /// </summary>
	    /// <remarks>
	    /// this method is an example for check duplicated uploaded file
	    /// 
	    /// if you have no idea how to check duplicated uploaded file
	    /// you can just call this function on ProcessRequest at xxx.ashx
	    /// </remarks>
	    public static void CheckDuplicatedFile(HttpContext context)
	    {
		    // http://www.uploadify.com/forums/discussion/2372/checkscript-and-asp.net/p1
		    Dictionary<string, string> fileArray = new Dictionary<string, string>();

		    for (int x = 0; x <= context.Request.Form.Count - 1; x++) {
			    if (context.Request.Form.Keys[x] != "folder") {
				    string uploadPath = HttpContext.Current.Server.MapPath(context.Request["folder"]) + "\\";
				    string filename = context.Request.Form[x];
				    string filepath = uploadPath + filename;
				    if (File.Exists(filepath)) {
					    fileArray.Add(context.Request.Form.Keys[x], context.Request.Form[x]);
				    }
			    }
		    }

		    JavaScriptSerializer jSer = new JavaScriptSerializer();
		    context.Response.ContentType = "application/json";
		    string json = jSer.Serialize(fileArray);
		    context.Response.Write(json);
	    }
	    #endregion

    }
}
