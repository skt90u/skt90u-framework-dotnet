<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasicTriggers.aspx.cs" Inherits="BasicTriggers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
    <asp:Button ID="Button1" runat="server" Text="Update" />
    This button is not in an UpdatePanel
    <hr />
    
    <div style="padding: 10px; border: 1px solid gray;">  
        UpdatePanel1      
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%= DateTime.Now.ToLongTimeString() %>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <br />
    <div style="padding: 10px; border: 1px solid gray;">
        UpdatePanel2
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <%= DateTime.Now.ToLongTimeString() %>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    
    </form>
</body>
</html>
