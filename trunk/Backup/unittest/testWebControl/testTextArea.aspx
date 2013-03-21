<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testTextArea.aspx.vb" Inherits="testTextArea" %>

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
      <jw:XTextarea 
        ID="XTextarea1" 
        Text="12345"
        Width="200px"
        MaxLength="10"
        runat="server" />
      
      <asp:DropDownList ID="ddl" runat="server"></asp:DropDownList>
    </div>
    </form>
</body>
</html>
