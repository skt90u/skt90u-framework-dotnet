<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testValidationCode.aspx.vb" Inherits="testValidationCode_testValidationCode" %>

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
    <div>
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </cc1:ToolkitScriptManager>
      <jw:ValidateCodeControl
       ID="ValidateCodeControl1"
       runat="server" /> 
    </div>
    <div>
      <asp:Label ID="lblResult" runat="server"></asp:Label>
      <asp:Button ID="btn" runat="server" />
    </div>
    </form>
</body>
</html>
