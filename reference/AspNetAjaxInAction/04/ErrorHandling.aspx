<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorHandling.aspx.cs" Inherits="ErrorHandling" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <div>
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>            
              <asp:Button ID="ThrowError" runat="server" Text="Throw Error" OnClick="ThrowError_Click" />            
            </ContentTemplate>
        </asp:UpdatePanel>
    

    
    </div>
    </form>
</body>
</html>
