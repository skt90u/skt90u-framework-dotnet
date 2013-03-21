<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nested.aspx.cs" Inherits="Nested" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div style="border: 1px dashed gray;">
     <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">   
      <ContentTemplate>
       <div>
         Last updated on: <%= DateTime.Now.ToLongTimeString() %> &nbsp;
         <asp:Button ID="bntOuter" Text="Outer" runat="server" />
       </div>
       <div>
        <asp:UpdatePanel ID="upd2" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
           &nbsp;&nbsp; 
           Last Updated on: <%= DateTime.Now.ToLongTimeString() %> 
           &nbsp;<asp:Button ID="btnInner" Text="Inner" runat="server" />
         </ContentTemplate>
        </asp:UpdatePanel>
       </div>
      </ContentTemplate>
     </asp:UpdatePanel>
    </div>


    </form>
</body>
</html>
