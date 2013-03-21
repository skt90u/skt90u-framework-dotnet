<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryOnClient.aspx.cs" Inherits="HistoryOnClient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Managing History from the Client</title>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<asp:History ID="History1" runat="server" />

<div>           
    <p>
        <input type="button" id="button1" value="Button 1" onclick="handleClick(1);" />
        <input type="button" id="button2" value="Button 2" onclick="handleClick(2);" />
        <input type="button" id="button3" value="Button 3" onclick="handleClick(3);" />
    </p>        
    <p>
        State: <span id="currentState"></span>
        <br />
        <a href="#" id="permalink">Permalink</a>

    </p>    
</div>

</form>
    
<script type="text/javascript" language="javascript">
<!--

    Sys.Application.get_history().add_navigate(onNavigate);
    
    function pageLoad(sender, args){
        $get("currentState").innerHTML = 0;
    }
    
    function handleClick(state){    
        $get("currentState").innerHTML = state;
        Sys.Application.get_history().addHistoryPoint({pageState: state});
        updatePermalink();
    }

    function onNavigate(sender, args){
        var state = args.get_state().pageState || 0;
        $get("currentState").innerHTML = state;
        updatePermalink();
    }
    
    function updatePermalink(){
        var plink = $get("permalink");
        plink.href = "#" + Sys.Application.get_history().get_stateString();
        if (plink.href !== "#")
            plink.innerHTML = "Permalink: " + plink.href;
        else
            plink.innerHTML = "Permalink";
    }

//-->
</script>    
    
</body>
</html>
