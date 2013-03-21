<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Debugging</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server"></asp:ScriptManager>
    <div>
        <h2>Please open the browser's console.</h2>
    </div>
    
    <script type="text/javascript">
    <!--
        function pageLoad() {
            Sys.Debug.trace("I'm a debug message");
            
            Sys.Debug.traceDump(Sys.UI.DomElement.getBounds(document.body));
        }
    //-->
    </script>
    </form>
</body>
</html>
