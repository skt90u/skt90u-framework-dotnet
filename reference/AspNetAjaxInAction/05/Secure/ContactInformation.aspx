<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactInformation.aspx.cs" Inherits="Secure_ContactInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ASP.NET Authentication, Profile and Roles</title>
    <link href="../styles.css" type="text/css" rel="stylesheet" media="all" />
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"> 
    </asp:ScriptManager>    
    
    <div id="header">
        <div class="bg">
            <div class="r1">
                <div id="storeLogo">
                    <img src="../images/ContactInformationBanner.png" alt="Contact Information" />
                </div>
           </div>
        </div>
    </div>
    
		<div id="content">
			<div id="content-inner">
				
				<div id="contentOnly">
				    <div id="fullContent" >
				        <div >
                            <div style="padding-left:10px;">
                            
                                <div style="padding-top: 5px;">
                                    <a href="#" onclick="logoutUser();">Logout</a>
                                </div>
                            
                                <div class="title" style="padding-top: 20px;">
                                    Home Address
                                </div>
                                
                                <div>
                                    <table border="0">            
                                        <tr>
                                            <td>Address 1:</td>
                                            <td><input id="address1" type="text" /></td>
                                        </tr>
                                        <tr>
                                            <td>Address 2:</td>
                                            <td><input id="address2" type="text" /></td>
                                        </tr>
                                        <tr>
                                            <td>City:</td>
                                            <td><input id="city" type="text" /></td>
                                        </tr>
                                        <tr>
                                            <td>State:</td>
                                            <td><input id="state" type="text" /></td>
                                        </tr>
                                        <tr>
                                            <td>Zip:</td>
                                            <td><input id="zip" type="text" /></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td><input id="saveAddress" type="button" value="Save" onclick="saveProfile()" /></td>
                                        </tr>
                                    </table>
                                    
                                    <div style="padding-top: 10px;">
                                        <span id="updating" style="display: none;">
                                            <img src="../images/indicator.gif" alt="" />&nbsp;
                                            <span id="message">Saving</span>
                                        </span>
                                    </div>
                                </div>   
                                
                                <div id="adminView" style="display: none;">
                                    <b>Only admins can see this message!</b>
                                </div>
                                
                            </div>
				        </div>
				    </div>
				    <br class="clear" />
				    <br />
                </div>
            
            </div>
        </div>    
    
        <div id="footer">
            <div class="container">
                
            </div>
        </div>    
    
        <p>
            Comment out code in script and web.config to enable roles (requires ASP.NET AJAX 2.0)
        </p>
    
    </form>
    
<script type="text/javascript" language="javascript">
<!--

    function pageLoad(){        
        $get("address1").focus();        
        loadProfile();     
        
        // Uncomment this line to enable roles (requires ASP.NET AJAX 2.0)
        //loadRoles();                         
    }

// Uncomment this section to enable roles (requires ASP.NET AJAX 2.0)
//    function loadRoles(){               
//        Sys.Services.RoleService.load(onLoadRolesCompleted, onLoadRolesFailed, null);        
//    }

//    function onLoadRolesCompleted(result, userContext, methodName){                                        
//        if (Sys.Services.RoleService.isUserInRole("Admin")){
//            $get("adminView").style.display = "block";            
//        }
//    }

//    function onLoadRolesFailed(error, userContext, methodName){
//        alert(error.get_message());
//    }

    function loadProfile(){     
        Sys.Services.ProfileService.load(null, onLoadCompleted, onLoadFailed, null);        
    }

    function onLoadCompleted(numProperties, userContext, methodName){       
           
        var profile = Sys.Services.ProfileService;          
        $get("address1").value = profile.properties.Address1;
        $get("address2").value = profile.properties.Address2;
        $get("city").value = profile.properties.City;
        $get("state").value = profile.properties.State;
        $get("zip").value = profile.properties.Zip;
             
        window.setInterval(tryAutoSave, 10000);     
    }
    
    function onLoadFailed(error, userContext, methodName){
        alert(error.get_message());
    }

    function tryAutoSave(){        
        $get("updating").style.display = "block";
        $get("message").innerHTML = "Saving ...";
        saveProfile();
    }

    function saveProfile(){
        
        var addr1 = $get("address1").value;
        var addr2 = $get("address2").value;
        var city = $get("city").value;
        var state = $get("state").value;
        var zip = $get("zip").value;
        
        Sys.Services.ProfileService.properties.Address1 = addr1;
        Sys.Services.ProfileService.properties.Address2 = addr2;
        Sys.Services.ProfileService.properties.City = city;
        Sys.Services.ProfileService.properties.State = state;
        Sys.Services.ProfileService.properties.Zip = zip;        
        Sys.Services.ProfileService.save(null, onSaveCompleted, onSaveFailed, null);        
    }

    function onSaveCompleted(numProperties, userContext, methodName){
        $get("updating").style.display = "none";
    }

    function onSaveFailed(error, useContext, methodName){
        alert(error.get_message());
    }

    function logoutUser(){
        $get("updating").style.display = "block";
        $get("message").innerHTML = "Logging out...";
        Sys.Services.AuthenticationService.logout(null, onLogoutCompleted, onLogoutFailed, null);
    }

    function onLogoutCompleted(result, userContext, methodName){
        // result is null and reserved for future use        
    }
    
    function onLogoutFailed(error, userContext, methodName){
        alert(error.get_message());
    }

//-->
</script>
    
</body>
</html>
