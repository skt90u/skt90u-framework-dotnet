<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ASP.NET Authentication</title>
    <link href="styles.css" type="text/css" rel="stylesheet" media="all" />
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">    
    </asp:ScriptManager>
        
    <div style="padding-top: 50px;">    
        <div id="smallHeader">
            <div class="bg">    
                <div class="r1">
                    <span id="smallHeaderTitle">Login</span>
                </div>
            </div>
        </div>
        <div id="smallContent">
            <div style="padding-top: 20px; padding-left: 20px;">
            
                <div class="title">
                Enter credentials to login
                </div>
            
                <table border="0" cellpadding="5">
                    <tr>
                        <td>Username:</td>
                        <td><input id="username" type="text" /></td>
                    </tr>
                    <tr>
                        <td>Password:</td>
                        <td><input id="password" type="password" /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <input type="button" value="Login" onclick="loginUser()" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <span id="loginStatus" style="color: Red;"></span>
                        </td>
                    </tr>
                </table> 
            </div>
            <br class="clear" />
            <br />   
            
        </div>
    </div>   
    <div id="smallFooter">
        <div class="container">
            <div>(argo - p@ssword1, user1 - p@ssword1)</div>
        </div>
    </div>   
    

    
    </form>
    
<script type="text/javascript" language="javascript">
<!--

    function pageLoad(){   
        
        with(Sys.Services.AuthenticationService){
            set_defaultLoginCompletedCallback(onLoginCompleted);
        }    
        $get("username").focus();            
    }
        
    function loginUser(){
               
        var username = $get('username');
        var password = $get('password');                                              
        Sys.Services.AuthenticationService.login(username.value,
                                                password.value,
                                                false,
                                                null,
                                                "Secure/ContactInformation.aspx",
                                                null,
                                                onLoginFailed,
                                                "User Context");       
    }
    
    function onLoginCompleted(validCredentials,
                                userContext,
                                methodName){
    
        if (validCredentials == false){
            $get("loginStatus").innerHTML = "Login failed.";
        }
    }
    
    function onLoginFailed(error, userContext, methodName){
        alert(error.get_message());
    }

//-->
</script>
    
    
</body>
</html>
