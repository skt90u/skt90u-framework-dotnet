<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SimpleContentTemplate.aspx.cs" Inherits="SimpleContentTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Simple ContentTemplate example</title>
</head>
<body>
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />    
    <asp:UpdatePanel ID="UpdatePane11" runat="server">
        <ContentTemplate>
            <div>
                Last Updated: <%= DateTime.Now.ToLongTimeString() %>
            </div>
            <div>
                <asp:Button ID="Update" runat="server" Text="Update" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <hr />
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                Last Updated: <%= DateTime.Now.ToLongTimeString() %>
            </div>
            <div>
                <asp:Button ID="Update2" runat="server" Text="Update" />
            </div>            
        </ContentTemplate>

    </asp:UpdatePanel>
    
    <hr />
    <asp:Button ID="OutsideButton" runat="server" Text="Outside Button" />
    
    </form>
</body>
</html>
