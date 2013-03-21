<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostBackTrigger.aspx.cs" Inherits="PostBackTrigger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PostBackTrigger Example</title>
</head>
<body>
    <form id="form1" runat="server">
    
<asp:ScriptManager ID="ScriptManager1" runat="server" />

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="LastUpdated" runat="server" />
        <br />
        <asp:Button ID="AsyncButton" runat="server" Text="Async Button" />
        <br />
        <asp:Button ID="PostBackButton" runat="server" Text="PostBack Button" />                                    
    </ContentTemplate>    
    <Triggers>          
        <asp:PostBackTrigger ControlID="PostBackButton" />
    </Triggers>
</asp:UpdatePanel>

    
    
    
    </form>
</body>
</html>
