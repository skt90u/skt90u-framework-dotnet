<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RenderMode.aspx.cs" Inherits="RenderMode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RenderMode</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <p>
        (RenderMode="Inline")
        <br />
        Last Updated:
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <%= DateTime.Now.ToLongTimeString() %>                
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
    <hr />
    <p>
        (RenderMode="Block")
        <br />
        Last Updated:
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Block">
            <ContentTemplate>
                <%= DateTime.Now.ToLongTimeString() %>                
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>    
    
    
    </form>
</body>
</html>
