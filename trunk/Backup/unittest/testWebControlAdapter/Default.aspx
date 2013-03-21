<%@ Page 
  Language="VB" 
  AutoEventWireup="false" 
  CodeFile="Default.aspx.vb" 
  Inherits="_Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
  Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register 
  Assembly="AjaxControlToolkit" 
  Namespace="AjaxControlToolkit" 
  TagPrefix="cc1" %>
  
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" FeatureFlags="BlockUI, LightBox">
    <div>
     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </cc1:ToolkitScriptManager>
      
      <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
          <asp:Button 
            ID="btn" 
            runat="server" 
            Text="新增"/>
          <a href="images/image-1.jpg" rel="lightbox"><img src="images/thumb-1.jpg" width="100" height="40" alt="" /></a>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
