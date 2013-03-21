<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testDateEdit.aspx.vb" Inherits="testDateEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </asp:ToolkitScriptManager>
      
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <asp:Panel ID="pnl01" Visible="false" runat="server">
            <asp:Label ID="lb01" runat="server" Text="de01"></asp:Label>
            <jw:DateEdit 
              ID="de01" 
              runat="server" >
            </jw:DateEdit>  
          </asp:Panel>
          <asp:Button 
          ID="btn01" 
          runat="server"  />
        </ContentTemplate>
      </asp:UpdatePanel>

      <asp:Panel ID="pnl02" runat="server">
        <asp:Label ID="lb02" runat="server" Text="de02"></asp:Label>      
        <jw:DateEdit 
          ID="de02" 
          Text="2011/01/10"
          runat="server" >
        </jw:DateEdit>  
      </asp:Panel>
      
      <asp:Button 
        ID="Button1" 
        Text="sayHi"
        OnClientClick="sayHi(); return false;"
        runat="server"  />
      
      <asp:Button ID="btn" runat="server" />
    </div>
    
    </form>
</body>
</html>
