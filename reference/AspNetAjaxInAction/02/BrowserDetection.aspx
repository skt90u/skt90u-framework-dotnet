<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Browser Detection</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server"></asp:ScriptManager>
    
    <script type="text/javascript">
    <!--
      function pageLoad() {
        var browser = String.format("Your browser is {0} {1}", Sys.Browser.name,
           Sys.Browser.version);

        alert(browser);
        
        if(Sys.Browser.agent == Sys.Browser.InternetExplorer) {
            alert('This message is displayed only on Internet Explorer!');
        }
      }
    //-->
    </script>
    
    </form>
</body>
</html>
