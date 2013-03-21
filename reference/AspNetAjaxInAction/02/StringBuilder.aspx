<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>StringBuilder</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server"></asp:ScriptManager>
    
    <span id="urlLabel"></span>

    <script type="text/javascript">
    <!--
        function pageLoad(sender, e) {
            // Create a new instance of the StringBuilder class.
            var sb = new Sys.StringBuilder();
            
            sb.append('<h3>You are now browsing: '); 
            sb.append('<b><i>');
            sb.append(window.location);
            sb.append('</i></b></h3>');
            
            var urlLabel = $get('urlLabel');

            // Inject the HTML chunk inside the span element.
            urlLabel.innerHTML = sb.toString();
        }
    //-->
    </script>
    </form>
</body>
</html>
