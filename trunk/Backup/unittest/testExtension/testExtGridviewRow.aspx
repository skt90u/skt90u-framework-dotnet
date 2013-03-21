<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testExtGridviewRow.aspx.vb" Inherits="testExtGridviewRow" %>

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
        <ContentTemplate>
          <asp:GridView ID="grv" 
            AutoGenerateColumns="false"
            runat="server">
            <Columns>
              <asp:CommandField
              ButtonType="Button" 
              ShowEditButton="true" />
              <asp:BoundField DataField="座號" HeaderText="座號" />
              <asp:BoundField DataField="姓名" HeaderText="姓名" />
              <asp:BoundField DataField="性別" HeaderText="性別" />
              <asp:BoundField DataField="住址" HeaderText="住址" />
            </Columns>
          </asp:GridView>
          <br />
          <asp:Label ID="lb" runat="server"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
