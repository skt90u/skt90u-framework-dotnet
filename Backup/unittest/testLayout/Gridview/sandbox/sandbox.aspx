<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sandbox.aspx.vb" Inherits="Gridview_sandbox" %>

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
      </cc1:ToolkitScriptManager>
      
      <asp:DropDownList 
        ID="ddl" 
        AutoPostBack="true"
        runat="server">
        <asp:ListItem Text="ChromeBlackGridView" Value="ChromeBlackGridView"></asp:ListItem>
        <asp:ListItem Text="ChromeGridView" Value="ChromeGridView"></asp:ListItem>
        <asp:ListItem Text="GamerGridView" Value="GamerGridView"></asp:ListItem>
        <asp:ListItem Text="GlassBlackGridView" Value="GlassBlackGridView"></asp:ListItem>
        <asp:ListItem Text="SoftGreyGridView" Value="SoftGreyGridView"></asp:ListItem>
        <asp:ListItem Text="WhiteChromeGridView" Value="WhiteChromeGridView"></asp:ListItem>
        <asp:ListItem Text="YahooGridView" Value="YahooGridView"></asp:ListItem>
      </asp:DropDownList>
      
      <br />
      
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
          <asp:Panel 
            ID="grvContainer" 
            runat="server">
          <jw:XGridView 
            ID="grv" 
            runat="server">
          </jw:XGridView>
          </asp:Panel>
        </ContentTemplate>
      </asp:UpdatePanel>
      
    </div>
    </form>
</body>
</html>
