<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testXListView.aspx.vb" Inherits="testXListView" %>

<%@ Register 
  Assembly="JFramework.WebControl" 
  Namespace="JFramework.WebControl" 
  TagPrefix="jw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <jw:XListView ID="XListView1" runat="server" DataSourceID="XmlDataSource1" >
        <LayoutTemplate>
          <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
          <asp:Label ID="i" runat="server" Text='<%# Eval("id") %>'></asp:Label>
          <asp:Label ID="n" runat="server" Text='<%# Eval("url") %>'></asp:Label>
          <asp:Label ID="nn" runat="server" Text='<%# Eval("title") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:Label ID="i" runat="server" Text='<%# Eval("id") %>'></asp:Label>
          <asp:Label ID="n" runat="server" Text='<%# Eval("url") %>'></asp:Label>
        </EditItemTemplate>
        <InsertItemTemplate>
          <asp:Label ID="i" runat="server" Text='<%# Eval("id") %>'></asp:Label>
        </InsertItemTemplate>
      </jw:XListView>

      
   
      <asp:XmlDataSource ID="XmlDataSource1" runat="server" 
        DataFile="~/testingData.xml"></asp:XmlDataSource>

      
   
    </div>
    </form>
</body>
</html>
