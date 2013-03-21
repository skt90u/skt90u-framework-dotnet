<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testCompositeScript.aspx.vb" Inherits="testCompositeScript" %>

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
        <CompositeScript>
          <Scripts>
            <asp:ScriptReference Path="3.js" />
            <asp:ScriptReference Path="1.js" />
            <asp:ScriptReference Path="2.js" />
          </Scripts>
        </CompositeScript>
      </cc1:ToolkitScriptManager>  
    </div>
    </form>
</body>
</html>
