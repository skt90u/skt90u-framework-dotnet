<%@ Page Language="C#" %>

<script runat="server">
    public void OnCheckChanged(Object sender, TreeNodeEventArgs e)
    {
        if (EntertainmentTreeView.CheckedNodes.Count > 0)
        {
            WhatsChecked.Text = "The checked values are:<ul>";
            foreach (TreeNode item in EntertainmentTreeView.CheckedNodes)
            {
                WhatsChecked.Text += "<li>";
                WhatsChecked.Text += item.Text;
                WhatsChecked.Text += "</li>";
            }
            WhatsChecked.Text += "</ul>";
        }
        else
        {
            WhatsChecked.Text = "Nothing is checked";
        }
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" href="SimpleTreeView.css" type="text/css" />
    <script type="text/javascript" src="CascadeCheckmarks.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TreeView ID="EntertainmentTreeView" runat="server"
          OnClientClickedCheckbox="CascadeCheckmarks(event)"
          OnTreeNodeCheckChanged="OnCheckChanged"
          OnAdaptedTreeNodeCheckChanged="OnCheckChanged"
          CssSelectorClass="SimpleEntertainmentTreeView"
          ExpandDepth="0"
          ShowCheckBoxes="All">
            <Nodes>
                <asp:TreeNode Text="Music">
                    <asp:TreeNode Text="Classical" />
                    <asp:TreeNode Text="Rock">
                        <asp:TreeNode Text="Electric" />
                        <asp:TreeNode Text="Something without a checkbox" ShowCheckBox="false" SelectAction="None" />
                        <asp:TreeNode Text="Acoustical" />
                    </asp:TreeNode>
                    <asp:TreeNode Text="Jazz" />
                </asp:TreeNode>
                <asp:TreeNode Text="Movies">
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
            <asp:Label id="WhatsChecked" runat="server" />
        </div>
    </form>
</body>
</html>
