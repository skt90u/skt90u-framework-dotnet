<%@ Page Language="C#" %>

<script runat="server">
    public void OnClick(Object sender, EventArgs e)
    {
        MessageLabel.Text = "You selected " + EntertainmentTreeView.SelectedNode.Text + ".";
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" href="SimpleTreeView.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:TreeView ID="EntertainmentTreeView" runat="server" OnSelectedNodeChanged="OnClick" OnAdaptedSelectedNodeChanged="OnClick" CssSelectorClass="SimpleEntertainmentTreeView" ExpandDepth="0">
            <Nodes>
                <asp:TreeNode Text="Music" SelectAction="Expand">
                    <asp:TreeNode Text="Classical" />
                    <asp:TreeNode Text="Rock">
                        <asp:TreeNode Text="Electric" />
                        <asp:TreeNode Text="Acoustical" />
                    </asp:TreeNode>
                    <asp:TreeNode Text="Jazz" />
                </asp:TreeNode>
                <asp:TreeNode Text="Movies" SelectAction="Expand">
                    <asp:TreeNode Text="Action" />
                    <asp:TreeNode Text="Drama" />
                    <asp:TreeNode Text="Musical" />
                </asp:TreeNode>
            </Nodes>
        </asp:TreeView>
        
        <div>
            <asp:Button runat="server" UseSubmitBehavior="true" Text="Submit" />
        </div>
        
        <div id="EntertainmentMessage">
            <asp:Label id="MessageLabel" runat="server" />
        </div>
    </form>
</body>
</html>
