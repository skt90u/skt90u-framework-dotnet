<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PropertyChanged</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/ScriptLibrary/Customer.js" />
        </Scripts>
    </asp:ScriptManager>
    
    <script type="text/javascript">
    <!--
        function pageLoad() {
            var customer = new Samples.Customer();
            
            // Subscribe to the propertyChanged event.
            customer.add_propertyChanged(onPropertyChanged);
            
            
            customer.set_fullName('John Doe');

            function onPropertyChanged(sender, e) {
                if(e.get_propertyName() == 'fullName') {
                    alert('The customer has a new name: ' +
                        sender.get_fullName());
                }
            }
        }
    //-->
    </script>
    </form>
</body>
</html>
