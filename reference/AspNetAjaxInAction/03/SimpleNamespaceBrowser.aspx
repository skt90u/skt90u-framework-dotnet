<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Simple Namespace Browser</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server"></asp:ScriptManager>
    <div>
        <span>Namespace:</span>
        <select id="ddNamespace">
            <option>Select a namespace</option>
            <option>Sys</option>
            <option>Sys.Net</option>
            <option>Sys.UI</option>
            <option>Sys.Services</option>
            <option>Sys.Serialization</option>
        </select>
        <div id="info"></div>
    </div>

    <script type="text/javascript">
    <!--
        function pageLoad(sender, e) {
           var ddl = $get("ddNamespace");
           
           // Hook-up the change event of the dropdownlist.
           $addHandlers(ddl, {change:onNamespaceChange}, this);
        }
        
        function onNamespaceChange(evt) {
            var interfaces = [];
            var classes = [];
            var info = $get("info");
            var ddl = evt.target;
            
            if(ddl.selectedIndex == 0) {
                info.innerHTML = "";
                return;
            }
            
            // Parse the selected value.
            var ns = eval(ddl.options[ddl.selectedIndex].text);          
                    
            // Search the namespace for classes and interfaces.   
            for(var attr in ns) {
                var currMember = ns[attr];
                
                // Classes and interfaces are functions.
                if(typeof(currMember) == "function") {
                    if(Type.isClass(currMember)) {                        
                        Array.add(classes, currMember.getName());         
                    }
                    else if(Type.isInterface(currMember)) {               
                        Array.add(interfaces, currMember.getName());
                    }
                }
            }
            
            // Build the HTML with the information to display.
            var sb = new Sys.StringBuilder();                             
            
            displayArray(interfaces, "Interfaces:", sb);                  
            displayArray(classes, "Classes:", sb);                        
            info.innerHTML = sb.toString();
        }
        
        // Format an array of strings.
        function displayArray(arr, title, sb) {
            sb.append("<b>");
            sb.append(title);
            sb.append("</b><br />");
            
            sb.append(arr.join("<br/>") + "<br/>");
        }
    //-->
    </script>
    </form>
</body>
</html>
