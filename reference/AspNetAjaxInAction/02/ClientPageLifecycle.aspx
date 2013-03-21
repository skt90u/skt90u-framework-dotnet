<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Client Page Lifecycle</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server"></asp:ScriptManager>
    <div>
    
    </div>
    
    <script type="text/javascript">
    <!--
        Sys.Application.add_init(pageInit);

        function pageInit() {
            alert("Entered the Init stage!");
        }

        function pageLoad() {
            alert("Page loaded!");
            alert("Hello Microsoft Ajax!");
        }
        
        function pageUnload() {
            alert("Unloading page!");
        }
    //-->
    </script>

    </form>
</body>
</html>
