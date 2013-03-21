<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testLightBox.aspx.vb" Inherits="testLightBox_testLightBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register 
  Assembly="JFramework.WebControl" 
  Namespace="JFramework.WebControl" 
  TagPrefix="jw" %>
  
<%@ Register 
  Assembly="AjaxControlToolkit" 
  Namespace="AjaxControlToolkit" 
  TagPrefix="cc1" %>
  
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </cc1:ToolkitScriptManager>
      <jw:lightbox ID="lightbox01" runat="server" />
      <a href="images/image-1.jpg" rel="lightbox"><img src="images/thumb-1.jpg" width="100" height="40" alt="" /></a>
    </div>
    </form>
</body>
</html>
