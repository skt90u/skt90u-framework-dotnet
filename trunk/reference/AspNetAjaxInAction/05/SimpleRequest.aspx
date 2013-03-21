<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SimpleRequest.aspx.cs" Inherits="SimpleRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple web request and error handling</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
        
            <input type="button" value="Make request" onclick="makeRequest()" />
            <br /><br />
            <input type="button" value="Make bad request" onclick="makeBadRequest()" />
            <p>
                <span id="responseData"></span>
            </p>
        </div>                                    
    </form>
    
<script type="text/javascript">
<!--

    function makeRequest(){    
        var request = new Sys.Net.WebRequest();
        request.set_url("Hello.txt");
        request.add_completed(onRequestCompleted);
        request.invoke();    
    }
    
    function makeBadRequest(){
        var request = new Sys.Net.WebRequest();
        request.set_url("MissingFile.txt");
        request.add_completed(onRequestCompleted);
        request.invoke();
    }
    
    function onRequestCompleted(executor, args){  
          
        var e = executor.checkError();   
        if (e)
        {
            updateResults("error: " + e.message);
        }
        else
        {
            updateResults(executor.get_responseData());
        }             
    }
    
    function updateResults(update){    
        var results = $get('responseData');
        if (results.innerHTML.length > 0)
            results.innerHTML += "<br />";
            
        results.innerHTML += update;        
    }
    
    Sys.Net.WebRequestExecutor.prototype.checkError = function() {
        
        // Status Code Range	Meaning
        // 100-199	Informational status codes. These status codes will not normally arise during ASP.NET Ajax development and can be ignored.
        // 200-299	The request was received and successfully processed.
        // 300-399	The request needs to be redirected. The most common code in this range is HTTP status code 302 which is sent when Response.Redirect is called from ASP.NET code.
        // 400-499	The request contains an error. A Common error code is 404 which indicates that the resource (file) was not found on the server.
        // 500-599	The request was valid but the server failed to process the request. The most common error code in this range is 500, which is returned by ASP.NET when an exception occurs during request processing.                        
        
        var e = null;
        
        if (this.get_aborted()) {
            e = Error.create('Request Aborted.', 
                       { name : 'RequestAbortedError' });
        }
        else if (this.get_timedOut()) {
            e = Error.create('Request timed out.', 
                       { name : 'RequestTimeoutError' } );
        }
        else {
            var statusCode;
            try {
                statusCode = this.get_statusCode();
            }
            catch(e) {
                statusCode = 0;
            }
            
            if (statusCode < 100 || statusCode >= 600) {
                e = Error.create('Connection Error.', 
                                 {name : 'ConnectionError' });   
            }
            else if ((statusCode < 200) || (statusCode >= 300)) {
                e = Error.create('HTTP Error.', 
                                 { name : 'HTTPError', 
                                   "statusCode" : statusCode, 
                                   statusText : this.get_statusText() } );
            }	
        }
        
        return e;
    
    }

//-->
</script>   
</body>
</html>
