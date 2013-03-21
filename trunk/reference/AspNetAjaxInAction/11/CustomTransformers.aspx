<%@ Page %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Custom Transformers</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
        </Scripts>
    </asp:ScriptManager>
       
    <div style="font-size:64pt">
        <span id="myLabel"></span>
    </div>

    <script type="text/xml-script">
        <page xmlns:script="http://schemas.microsoft.com/xml-script/2005">
          <components>
            <label id="myLabel" text="">
                <bindings>
                    <binding id="lblBinding"
                             dataContext="myLabel"
                             dataPath="text"
                             property="text"
                             transform="GreetMessage"
                             transformerArgument="Hello XML-script!"
                             automatic="false"
                             />
                </bindings>
            </label>
            <timer id="theTimer" enabled="true" interval="100">
                <tick>
                   <invokeMethodAction target="lblBinding"
                                       method="evaluateIn"
                                       />
                </tick>
            </timer>
          </components>
        </page>
    </script>
    
    <script type="text/javascript">
    <!--
        function GreetMessage(sender, e) {
            var message = e.get_transformerArgument();
            
            if(typeof(message) !== 'string') return;
                        
            var currText = e.get_value();
            var nextIndex = currText.length == 0 ? message.length : 
                 (currText.length - 1) % (message.length + 1);
            var nextText = message.substr(message.length - nextIndex, message.length);
            
            e.set_value(nextText);
        }
    //-->
    </script>
    </form>
</body>
</html>
