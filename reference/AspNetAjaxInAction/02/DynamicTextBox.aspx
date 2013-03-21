<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Dynamic TextBox</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server"></asp:ScriptManager>
    
    <div>
        <span>Please type some text:</span>
        <input type="text" id="txtNoDigits" />                     
    </div>

    <script type="text/javascript">
    <!--
        function pageLoad() {
            var txtNoDigits = $get('txtNoDigits');                 

            $addHandler(txtNoDigits,                               
                'keypress', txtNoDigits_keypress);                 
        }

        function pageUnload() {
            $removeHandler($get('txtNoDigits'),                     
                'keypress', txtNoDigits_keypress);                 
        }                                                          

        function txtNoDigits_keypress(evt) {                       
            var code = evt.charCode;                               
                                                                   
            if(code >= 48 && code <= 57) {                          
                evt.preventDefault();                              
            }                                                      
        }
    //-->
    </script> 
    </form>
</body>
</html>
