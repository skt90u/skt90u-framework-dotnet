<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testExtGridview.aspx.vb" Inherits="testExtGridview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:ScriptManager ID="ScriptManager1" runat="server">
      </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
          <%--ExportExcel/ExportWord functions must run under PostBack mode--%>
          <asp:PostBackTrigger ControlID="btn" />
          <asp:PostBackTrigger ControlID="ReflectButton" />
        </Triggers>
        <ContentTemplate>
          <asp:GridView ID="grv" runat="server">
          </asp:GridView>
          <asp:Button ID="btn" runat="server" OnClick="ExportData" />  
          <asp:Button ID="ReflectButton" runat="server" OnClick="ReflectOnClick" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
