<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Array</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server"></asp:ScriptManager>
    
    <script type="text/javascript">
    <!--
        function pageLoad(sender, e) {
            var arr = new Array();                                
            
            // Add some items.
            Array.add(arr, 3);                                    
            Array.addRange(arr, [4, 5, "Hello World!"]);                  
            
            // Remove the last item.
            Array.removeAt(arr, arr.length - 1);                  
            
            // Compute the sum of the items.
            var sum = 0;                                           
            Array.forEach(arr,  
               function(item) { sum += item; });                 
                
            alert(sum);
            
            // Clear the array.
            Array.clear(arr);                                     
        }
    //-->
    </script>
    
    </form>
</body>
</html>
