<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XmlHttpRequest.aspx.cs" Inherits="XmlHttpRequest" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ASP.NET AJAX In Action - XmlHttpRequest</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span id="results">Loading...</span>
    </div>
    </form>
        
<script type="text/javascript">

var xmlHttp = null;

window.onload = function() {
    
    loadXmlHttp();    
    sendRequest("Welcome.htm");    
}    

function loadXmlHttp() {
    if (window.XMLHttpRequest) { // IE7, Mozilla, Safari, Opera, etc.
        xmlHttp = new XMLHttpRequest();
    } else if (window.ActiveXObject) {
        try{
            xmlHttp = new ActiveXObject("Microsoft.XMLHTTP"); // IE 5.x and 6
        }
        catch (e){}        
    }               
}        
    
function sendRequest(url) {        
    if (xmlHttp) {
               
        // Open HTTP connection to url.
        xmlHttp.open("GET", url, true); // true = async
       
        // Define the callback function for async call
        xmlHttp.onreadystatechange = onCallback;       
        
        // Specify form data in request body
        xmlHttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');        
        
        // Send request without any additional parameters
        xmlHttp.send(null);
    }
}

function onCallback() {    
 
  // Check for the completed status 
  if (xmlHttp.readyState == 4) {
    
    // Check for successful server response
    if (xmlHttp.status == 200){
        var r = document.getElementById('results'); 
        r.innerHTML = xmlHttp.responseText;         
    }
    else {
        // HTTP error
        alert('Error: ' + xmlHttp.status);
    }

  }
}
    
</script>    
</body>
</html>
