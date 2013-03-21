<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Element Wrappers</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
        </Scripts>
    </asp:ScriptManager>
    
    <div>
        <input type="button" id="greetButton" value="Click Me" />  
    </div>
    <div>
        <h1><span id="msgLabel"></span></h1>                       
    </div>

    <script type="text/xml-script">
        <page xmlns="http://schemas.microsoft.com/xml-script/2005">
          <components>
            <label id="msgLabel" />                                
            <button id="greetButton" click="onGreetButtonClick" /> 
          </components>
        </page>
    </script>

    <script type="text/javascript">
    <!--
        function onGreetButtonClick(sender, e) {
            $find('msgLabel').set_text('Hello XML-script!');
        }
    //-->
    </script>
    
    </form>
</body>
</html>
