<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo currentCulture= new System.Globalization.CultureInfo("it-IT");
        System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Globalization</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
    
    <div>
        <h2>Please open the browser's console!</h2>
    </div>
    
    <script type="text/javascript">
    <!--
        function pageLoad() {
            Sys.Debug.traceDump(Sys.CultureInfo.InvariantCulture);
            
            var date = new Date();
            alert(date.localeFormat("dddd, dd MMMM yyyy, hh:mm:ss"));
        }
    //-->
    </script>
    </form>
</body>
</html>
