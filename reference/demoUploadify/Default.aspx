<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="WebApplication1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <title></title>
  <link href="css/default.css" rel="stylesheet" type="text/css" />
  <link href="css/uploadify.css" rel="stylesheet" type="text/css" />
  <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
  <script src="scripts/swfobject.js" type="text/javascript"></script>
  <script src="scripts/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
  <%--<script src="scripts/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>--%>
  <script type="text/javascript">
    $(document).ready(function() {
      $("#uploadify").uploadify({
        'uploader': 'scripts/uploadify.swf',
        'script': 'UploadHandler.ashx',
        'cancelImg': 'cancel.png',
        'folder': 'uploads',
        'queueID': 'fileQueue',
        'auto': true,
        'multi': true
      });
    });
  </script>  
</head>
<body>
  <form id="form1" runat="server">
    <div id="fileQueue"></div>
    <input type="file" name="uploadify" id="uploadify" />
  </form>
</body>
</html>
