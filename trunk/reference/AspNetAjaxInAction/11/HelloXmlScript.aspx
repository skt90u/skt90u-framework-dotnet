<%@ Page %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Hello XML-script</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
        </Scripts>
    </asp:ScriptManager>
    
    <script type="text/xml-script">
        <page xmlns="http://schemas.microsoft.com/xml-script/2005">
          <components>
            <application init="pageInit" />
          </components>
        </page>
    </script>

    <script type="text/javascript">
    <!--
        function pageInit(sender, e) {
            alert("Hello Xml-script!");
        }
    //-->
    </script>
    </form>
</body>
</html>
