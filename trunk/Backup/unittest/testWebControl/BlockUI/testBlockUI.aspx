<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testBlockUI.aspx.vb" Inherits="BlockUI_testBlockUI" %>

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
      
      <jw:BlockUI
          ID="bu" 
          runat="server">
      </jw:BlockUI>      
      
      <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
      
    </div>
    </form>
</body>
</html>
