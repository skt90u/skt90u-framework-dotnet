<%@ Page Language="C#" %>

<script runat="server">
    void OnClick(Object sender, MenuEventArgs e)
    {
        MessageLabel.Text = "You selected " + e.Item.Text + ".";
        e.Item.Selected = true;
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" href="SimpleMenu.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Menu ID="EntertainmentMenu" runat="server" Orientation="Horizontal" onmenuitemclick="OnClick" CssSelectorClass="SimpleEntertainmentMenu">
            <Items>
                <asp:MenuItem Text="Music">
                    <asp:MenuItem Text="Classical" />
                    <asp:MenuItem Text="Rock">
                        <asp:MenuItem Text="Electric" />
                        <asp:MenuItem Text="Acoustical" />
                    </asp:MenuItem>
                    <asp:MenuItem Text="Jazz" />
                </asp:MenuItem>
                <asp:MenuItem Text="Movies" Selectable="false">
                    <asp:MenuItem Text="Action" />
                    <asp:MenuItem Text="Drama" />
                    <asp:MenuItem Text="Musical" />
                </asp:MenuItem>
            </Items>
        </asp:Menu>
        
        <div id="EntertainmentMessage">
            <asp:Label id="MessageLabel" runat="server" />
        </div>
    </form>
</body>
</html>
