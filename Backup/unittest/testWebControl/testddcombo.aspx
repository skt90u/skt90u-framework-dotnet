<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testddcombo.aspx.vb" Inherits="testddcombo" %>

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
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        <CompositeScript>
          <Scripts>
          </Scripts>
        </CompositeScript>
      </cc1:ToolkitScriptManager>
      <jw:ddcombo 
        ID="box1" 
        runat="server">
      </jw:ddcombo>
      
      <jw:ddcombo 
        ID="box2" 
        runat="server">
      </jw:ddcombo>
    </div>
    </form>
</body>
</html>
