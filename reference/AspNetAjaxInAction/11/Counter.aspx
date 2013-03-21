<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Counter</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
        </Scripts>
    </asp:ScriptManager>
    
    <div>
        <h1><span id="myLabel"></span></h1>
    </div>

    <script type="text/xml-script">
        <page xmlns:script="http://schemas.microsoft.com/xml-script/2005">
          <components>
            <label id="myLabel" text="0">
                <bindings>
                    <binding id="lblBinding"                         
                             dataContext="myLabel"                   
                             dataPath="text"                         
                             property="text"                         
                             transform="Add"                         
                             automatic="false"                       
                             />                                      
                </bindings>
            </label>

            <timer id="theTimer" enabled="true" interval="1000">     
                <tick>
                   <invokeMethodAction target="lblBinding"           
                                       method="evaluateIn"           
                                       />                            
                </tick>
            </timer>
          </components>
        </page>
    </script>
   </form>
</body>
</html>
