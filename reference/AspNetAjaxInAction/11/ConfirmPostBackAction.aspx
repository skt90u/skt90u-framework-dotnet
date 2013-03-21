<%@ Page %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ConfirmPostBack Action</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
            <asp:ScriptReference Path="ScriptLibrary/ConfirmPostBackAction.js" />
        </Scripts>
    </asp:ScriptManager>
    
    <div>
        <input id="myButton" type="button" value="Click Me" />
    </div>
    
    <script type="text/xml-script">
        <page xmlns="http://schemas.microsoft.com/xml-script/2005" xmlns:cc="Samples">
          <components>
            <button id="myButton">
                <click>
                    <cc:confirmPostBackAction target="myButton" message="Do you want to perform a postback?" />
                </click>
            </button>
          </components>
        </page>
    </script>
    </form>
</body>
</html>
