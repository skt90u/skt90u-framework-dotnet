<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>11.2.3 Declarative WebService Call</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
      <scripts>
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
      </scripts>
    </asp:ScriptManager>
    
    <h2>
        <span>Time on Web Server: </span>
        <asp:Label ID="DateTime" runat="server"></asp:Label>
    </h2>

    <script type="text/xml-script">
      <page xmlns="http://schemas.microsoft.com/xml-script/2005">
        <components>
           <label id="DateTime" />
           
           <timer id="theTimer" interval="2000" enabled="true">
             <tick>
                 <invokeMethodAction target="timeServiceMethod" method="invoke">
                     <parameters userContext="" />
                 </invokeMethodAction>
             </tick>
           </timer>
          
           <serviceMethodRequest id="timeServiceMethod" 
                                 url="DateTimeService.asmx" 
                                 methodName="GetTimeAsString">
            <completed>
                <setPropertyAction target="DateTime"
                                   property="text"
                                   >
                    <bindings>
                        <binding dataContext="timeServiceMethod"
                                 dataPath="result"
                                 property="value"
                                 />        
                    </bindings>
                </setPropertyAction>
            </completed>
          </serviceMethodRequest>
        </components>
      </page>
    </script>
    </form>
</body>
</html>
