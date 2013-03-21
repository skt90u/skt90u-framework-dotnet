$(document).ready(function() {
    /* $("#uploadify").uploadify({ */
        $("#uploadify1_uploadify").uploadify({
        'uploader': '<%=WebResource("JFramework.WebControl.uploadify.swf")%>',
        'script': 'UploadHandler.ashx',
        'cancelImg': 'cancel.png',
        'folder': 'uploads',
        /* 'queueID': 'fileQueue', */
        'queueID': 'uploadify1_fileQueue',
        'auto': true,
        'multi': true
    });
});