<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RenderAsChildren.aspx.cs" Inherits="RenderAsChildren" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <div style="border: 1px dashed gray; margin: 10px;">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%= DateTime.Now.ToLongTimeString() %>
            <br />
            I'm in a panel that ALWAYS updates.
            <br />
            <asp:Button ID="UpdateOtherPanels" runat="server" Text="Update Other Panels" OnClick="UpdateOtherPanels_Click" />
        </ContentTemplate>
     </asp:UpdatePanel>
    </div>

    <div style="border: 1px dashed gray; margin: 10px;">
     <asp:UpdatePanel ID="UpdatePanel2" runat="server"
        UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>            
            <%= DateTime.Now.ToLongTimeString() %>
            <asp:Button ID="Update" runat="server" Text="Update"  />
            <br />
            This panel can only be updated manually.
        </ContentTemplate>            
     </asp:UpdatePanel>
    </div>
    
    </form>
</body>
</html>
