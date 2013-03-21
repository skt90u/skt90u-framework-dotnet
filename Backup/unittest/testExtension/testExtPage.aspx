<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testExtPage.aspx.vb" Inherits="testExtPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:TextBox ID="tb" runat="server"></asp:TextBox>
      <asp:Button ID="btn" runat="server" Text="Test FindControl" OnClick="TestFindControl" />
      <asp:Label ID="lb" runat="server" ></asp:Label>
    </div>
    </form>
</body>
</html>
