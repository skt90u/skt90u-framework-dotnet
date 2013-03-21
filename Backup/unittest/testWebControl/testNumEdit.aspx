<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testNumEdit.aspx.vb" Inherits="testNumEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
        <jw:NumEdit 
        ID="NumEdit1" 
        Required="true"
        NumType="Integer"
        MaximumValue="10"
        MinimumValue="0"
        runat="server" >
      </jw:NumEdit> 
      </ContentTemplate>
    </asp:UpdatePanel>
    <div>
      <jw:NumEdit 
        ID="de" 
        Required="true"
        NumType="Integer"
        MaximumValue="123"
        MinimumValue="0"
        runat="server" >
      </jw:NumEdit> 
      
      <asp:Button ID="btn" runat="server" />
    </div>
    
    </form>
</body>
</html>
