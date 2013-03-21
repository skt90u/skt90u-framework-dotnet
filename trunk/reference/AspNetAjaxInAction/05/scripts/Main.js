
function onCloseLogin() {
    $get(__LoginID).style.display = 'none';
    return false;
}

function onLoginClick() {
    if (Sys.Services.AuthenticationService.get_isLoggedIn()) {
        //Due to a bug in authentication service the page reloads after logout success
        Sys.Services.AuthenticationService.logout(null, onLogoutSuccess, onLogoutFailed);
    } else {
        $get(__LoginID).style.display = '';
    }
    
    return false;    
}

function onLogoutSuccess() {
    $get(__LoginLinkID).innerHTML = Resources.Login;
}

function onLogoutFailed() {
    alert(Resources.LogoutFailed);    
}

function onLogonSuccess(sender, args) {
    //Sender is the Ajax login control
    sender.get_element().style.display = 'none';
    //LoginLinkID is set via Scriptmanager.RegisterClientScriptBlock
    //This is done because ASP.NET changes client IDs
    $get(__LoginLinkID).innerHTML = Resources.Logout;
}

function onLogonFailed(sender, args) {
    //In real application replace the messagebox with something else
    alert(Resources.LogonFailed);
    //Sender is the Ajax login control
    sender.get_element().style.display = 'none';   
}
