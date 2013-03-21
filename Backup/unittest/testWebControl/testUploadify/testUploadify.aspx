<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testUploadify.aspx.vb" Inherits="testUploadify_testUploadify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register 
  Assembly="AjaxControlToolkit" 
  Namespace="AjaxControlToolkit" 
  TagPrefix="cc1" %>
  
<%@ Register 
  Assembly="JFramework.WebControl" 
  Namespace="JFramework.WebControl" 
  TagPrefix="jw" %>
  
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager> 
    <jw:uploadify 
        ID="uploadify1" 
        script="UploadHandler.ashx"
        checkScript="DuplicatedFileChecker.ashx"
        onAllComplete="function(){alert('onAllComplete');}"
        runat="server" />
    </form>
</body>
</html>
