<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FormattingBehavior</title>
    <link type="text/css" href="Style/FormattingBehavior.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/ScriptLibrary/FormattingBehavior.js" />
        </Scripts>
    </asp:ScriptManager>
     
   <div class="form">
        <div>
            <span>Name:</span>
            <asp:TextBox ID="Name" runat="server"></asp:TextBox>
        </div>
        <div>
            <span>Last Name:</span>
            <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
        </div>
    </div>

    <script type="text/javascript">
    <!--
        Sys.Application.add_init(pageInit);
        
        function pageInit(sender, e) {
            // Attach the FormattingBehavior to the txtName textbox.
            $create(Samples.FormattingBehavior, 
              {'hoverCssClass':'field_hover', 'focusCssClass':'field_focus'},
              {}, {}, $get('Name'));

            // Attach the FormattingBehavior to the txtLastName textbox.
            $create(Samples.FormattingBehavior, 
              {'hoverCssClass':'field_hover', 'focusCssClass':'field_focus'},
              {}, {}, $get('LastName'));
        }
    //-->
    </script> 

    </form>
</body>
</html>
