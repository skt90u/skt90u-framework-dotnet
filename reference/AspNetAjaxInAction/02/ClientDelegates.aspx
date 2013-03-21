<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Client Delegates</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server"></asp:ScriptManager>
    
    <input type="button" id="testButton" value="Click Me" />

    <script type="text/javascript">
    <!--
      function pageLoad() {
         // test is a property of the window object.
         this.test = "I'm a test string!";
         
         // Create a delegate that points to onButtonClick and pass the 
         // window object as the first argument.
         var clickDelegate = Function.createDelegate(this, onButtonClick);   

         // Handle the click event with the delegate.
         $addHandler($get('testButton'), 'click', clickDelegate);           
      }

      function onButtonClick() {
         // Access the window object.
         alert(this.test);
      }  
    //-->
    </script> 
    
    </form>
</body>
</html>
