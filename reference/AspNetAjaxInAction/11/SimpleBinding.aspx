<%@ Page %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Simple Binding</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" 
                Name="PreviewScript.js" />
        </Scripts>
    </asp:ScriptManager>
    
    <div>
        <input type="text" id="srcText" />
        <span id="dstLabel" />
    </div>

    <script type="text/xml-script">
        <page xmlns="http://schemas.microsoft.com/xml-script/2005">
          <components>
            <textBox id="srcText" text="initial text" />

            <label id="dstLabel">
                <bindings>
                    <binding id="binding1" 
                             dataContext="srcText"
                             dataPath="text"
                             property="text"
                             />
                </bindings>
            </label>
          </components>
        </page>
    </script>
    
    <script type="text/javascript">
    <!--
        function pageLoad() {
//            var binding = new Sys.Preview.Binding();
//            binding.set_id('binding1');
//            binding.set_target($find('dstLabel'));
//            binding.set_dataContext($find('srcText'));
//            binding.set_dataPath('text');
//            binding.set_property('text');
//            binding.set_direction(Sys.Preview.BindingDirection.Out);
//            
//            $find('dstLabel').set_text("Label's text");
        }
    //-->
    </script>
    </form>
</body>
</html>