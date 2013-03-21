<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Callbacks</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server"></asp:ScriptManager>
    
    <input type="button" id="myButton" value="Time elapsed since page load" />

    <script type="text/javascript">
    <!--
        function pageLoad() {
            // The context object.
            var context = { date : new Date() };        
                                 
            // Create a callback that points to onButtonClick and pass
            // the context object.
            var clickCallback = 
                Function.createCallback(onButtonClick, context);          

            // Attach a handler to the click event of the button.
            $addHandler($get('myButton'), 'click', clickCallback);         
        } 

        function onButtonClick(evt, context) {
            // Here we can access both the event object and
            // the context.                             
            var loadTime = context.date;                                        
            var elapsed = new Date() - loadTime;
            
            alert((elapsed / 1000) + ' seconds');
        }
    //-->
    </script>
    
    </form>
</body>
</html>
