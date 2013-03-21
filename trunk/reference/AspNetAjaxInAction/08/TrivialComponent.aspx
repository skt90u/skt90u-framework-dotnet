<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Trivial Component</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/ScriptLibrary/TrivialComponent.js" />
        </Scripts>
    </asp:ScriptManager>
    
    <script type="text/javascript">
    <!--
        Sys.Application.add_init(pageInit);                                

        function pageInit() {
            $create(Samples.TrivialComponent, {'id':'trivialComponent'});  
        }

        function pageLoad() {
            var trivialComponent = $find('trivialComponent');              
                
            trivialComponent.greet();                                      
        }
    //-->
    </script>
    </form>
</body>
</html>
