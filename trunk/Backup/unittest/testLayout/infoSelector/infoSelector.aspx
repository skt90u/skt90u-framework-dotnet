<%@ Page Language="VB" AutoEventWireup="false" CodeFile="infoSelector.aspx.vb" Inherits="infoSelector_infoSelector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100px">
    <asp:Panel ID="pnlContainer" runat="server" CssClass="pnlContainer">
      <asp:Panel ID="pnlCategory" runat="server" CssClass="pnlCategory">
        <asp:Label ID="lbCategory" runat="server" Text="Add New Custome" CssClass="lbCategory" />
      </asp:Panel>
      <asp:Panel ID="pnlQueryCondition" runat="server" CssClass="pnlQueryCondition" >
        <asp:Label ID="lbFilting" runat="server" CssClass="lbFilting" />
        <br />
        <asp:TextBox ID="txtFilting" runat="server" CssClass="text" />
        <asp:Button ID="btnFilting" runat="server" CssClass="button" />
      </asp:Panel>
      <asp:Panel ID="pnlCommands" runat="server" CssClass="pnlCommands">
        <asp:Button ID="btnExpand" runat="server" CssClass="button" Text="展開全部" />
        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="確定" />
        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="取消" />
      </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
