<%@ Page Language="C#" %>

<script runat="server">
    public void OnClick(Object sender, EventArgs e)
    {
        MessageLabel.Text = "You selected " + EntertainmentTreeView.SelectedNode.Text + ".";
    }

    public void OnPopulate(Object sender, TreeNodeEventArgs e)
    {
        TreeNode child = null;
        switch (e.Node.Value)
        {
            case "Music":
                child = new TreeNode("Classical");
                e.Node.ChildNodes.Add(child);
                child = new TreeNode("Rock");
                child.PopulateOnDemand = true;                    
                e.Node.ChildNodes.Add(child);
                child = new TreeNode("Jazz");
                e.Node.ChildNodes.Add(child);
                break;
            case "Rock":
                child = new TreeNode("Electric");
                e.Node.ChildNodes.Add(child);
                child = new TreeNode("Acoustical");
                e.Node.ChildNodes.Add(child);
                break;
            case "Movies":
                child = new TreeNode("Action");
                e.Node.ChildNodes.Add(child);
                child = new TreeNode("Drama");
                e.Node.ChildNodes.Add(child);
                child = new TreeNode("Musical");
                e.Node.ChildNodes.Add(child);
                break;
        }
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" href="SimpleTreeView.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:TreeView ID="EntertainmentTreeView" runat="server"
            OnSelectedNodeChanged="OnClick"
            OnAdaptedSelectedNodeChanged="OnClick"
            OnTreeNodePopulate="OnPopulate"
            CssSelectorClass="SimpleEntertainmentTreeView"            
            ExpandDepth="0">
            <Nodes>
                <asp:TreeNode Text="Music" PopulateOnDemand="true" />
                <asp:TreeNode Text="Movies" PopulateOnDemand="true" />
            </Nodes>
        </asp:TreeView>
        
        <div id="EntertainmentMessage">
            <asp:Label id="MessageLabel" runat="server" />
        </div>
    </form>
</body>
</html>
