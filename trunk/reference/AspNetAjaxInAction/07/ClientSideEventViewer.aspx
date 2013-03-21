<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClientSideEventViewer.aspx.cs" Inherits="ClientSideEventViewer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client-Side Event Viewer</title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Required ScriptManager control for ajax-enabled pages -->
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <div>        
            <p>
                <!-- UpdatePanel1 -->
                <asp:Panel ID="Panel1" runat="server" GroupingText="UpdatePanel1">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            Last Updated: <asp:Label ID="LastUpdated1" runat="server" />
                            <div>
                                <asp:Button ID="SlowUpdate" runat="server" Text="Slow Update" OnClick="SlowUpdate_Click" />
                                <asp:Button ID="Abort" runat="server" Text="Abort PostBack" />
                                <asp:Button ID="FastUpdate" runat="server" Text="Fast Update" OnClick="FastUpdate_Click" />
                                <asp:Button ID="ThrowError" runat="server" Text="Throw Error" OnClick="ThrowError_Click" />                            
                            </div>                
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>      
                <br />
                <!-- UpdatePanel2 -->
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Panel ID="Panel2" runat="server" GroupingText="UpdatePanel2">
							
                            <div>
                                <asp:Label ID="LastUpdated2" runat="server" />
                            </div>
                            <div>
                                <asp:Button ID="NormalUpdate" runat="server" Text="Normal PostBack" />
                            </div>                        
                        </asp:Panel>
						
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						
                    </ContentTemplate>
                    
					<triggers>
					
					<asp:postbacktrigger ControlID="NormalUpdate">
					</asp:postbacktrigger>
					
					</triggers>
					
                </asp:UpdatePanel>                         
            </p>
            
            <!-- Event Viewer -->
            <div>
                <table>
                    <caption>Client-Side Event Viewer</caption>
                    <thead>
                        <tr>
                            <th scope="col">Event</th>
                            <th scope="col">Details</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <td align="left" colspan="2">                                
                                <a href="#" onclick="clearEvents();">Clear&nbsp;
                                    <asp:Image ID="Clear" runat="server" ImageUrl="~/images/trashcan.gif"
                                     ImageAlign="AbsBottom" AlternateText=""/>
                                 </a>                        
                            </td>
                        </tr>
                    </tfoot>
                    <tbody id="clientEvents">
                    </tbody>
                </table>                
            </div>

            <!-- Asynchronous postback cue for users -->
            <div id="loadingPanel" class="asyncPostBackPanel" style="display: none;">
                <img src="images/indicator.gif" alt="" />&nbsp;&nbsp;Loading...
            </div>

        </div>                               
    </form>

<script type="text/javascript">

    // Register handlers for Application model events
    Sys.Application.add_init(onInit);
    Sys.Application.add_load(onLoad);

    // Registern handlers for PageRequestManager events
    with(Sys.WebForms.PageRequestManager.getInstance()) {
      add_initializeRequest(onInitializeRequest);
      add_beginRequest(onBeginRequest);
      add_pageLoading(onPageLoading);
      add_pageLoaded(onPageLoaded);
      add_endRequest(onEndRequest );
     }   

    // Init event - raised by Sys.Application
    function onInit(sender, args){                   
        var row = createEventRow("init", "");
        $get('clientEvents').appendChild(row);       
    }

    // Load event - raised by Sys.Application
    function onLoad(sender, args){                        
        var details;
        if (!args.get_isPartialLoad()){
            details = "Normal postback";
        }
        else{
            details = "Asynchronous postback";
        }
        
        var row = createEventRow("load", details);
        $get('clientEvents').appendChild(row);
    }
    
    // initializeRequest event - beginning stages of a request, raised to 
    // give us an opportunity to cancel or aborted postbacks. 
    function onInitializeRequest(sender, args){             
                          
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        var details = "postBackElementID = " + args.get_postBackElement().id;
    
        // Check to see if we're in an asychronous postback    
        if (prm.get_isInAsyncPostBack()){
        
            // Aborting a postback will stop the request from happening and 
            // no updates will be applied.
            if (args.get_postBackElement().id == "Abort"){     
                Sys.WebForms.PageRequestManager.getInstance().abortPostBack();        
                details += " (aborted)";
            }
            else{
            
                // Canceling a postback means that the previous postback will
                // continue to be processed, otherwise the most recent postback
                // will be handled instead.
                args.set_cancel(true);
                details += " (canceled)";
            }
        }
       
        var row = createEventRow("initializeRequest", details);
        $get('clientEvents').appendChild(row);                          
    }

    // beginRequest event - This is raised just before the request is sent to the server
    function onBeginRequest(sender, args){           
    
        // Show the user some visual cue that we are processing an
        // asynchronous request. Also, we only want to show this for
        // the longer operations, such as when the SlowUpdate button
        // is the element that invoked the postback.
        if (args.get_postBackElement().id == "SlowUpdate"){                
            $get('loadingPanel').style.display = 'block'; 
        }
                                                        
        var row = createEventRow("beginRequest", "");
        $get('clientEvents').appendChild(row);          
    }
        
    // pageLoading event - raised after the postback has been processed on
    // the server but before updates to the user interface are applied.
    function onPageLoading(sender, args){         
    
        // Retrieve the list of UpdatePanel controls that are being
        // updated or deleted.
        var details = new Sys.StringBuilder();
        details.append(displayPanels("Updating", args.get_panelsUpdating()));        
        details.append("  -  ");
        details.append(displayPanels("Deleting", args.get_panelsDeleting()));                                  
        var row = createEventRow("pageLoading", details.toString());
        $get('clientEvents').appendChild(row);        
    }
        
    // pageLoaded event - raised after the updates have been applied
    // to the user interface.
    function onPageLoaded(sender, args){   
    
        // Retrieve the list of UpdatePanels that were created and updated.
        var details = new Sys.StringBuilder();
        details.append(displayPanels("Created", args.get_panelsCreated()));
        details.append("  -  ");
        details.append(displayPanels("Updated", args.get_panelsUpdated()));    
        var row = createEventRow("pageLoaded", details.toString());
        $get('clientEvents').appendChild(row);        
    }
        
    // endRequest event - raised after the load event of the Sys.Application
    // object to signify the end of an asychronous postback.
    function onEndRequest(sender, args){                 
    
        // This event is always raised, even when an error occurs on the server.
        var details = "";
        var error = args.get_error();
        if (error != null){
           details = "Error: " + error.message;
            args.set_errorHandled(true);
        }
        else
            details = "No errors";            
        
        // Hide visual cue of asynchronous postback
        $get('loadingPanel').style.display = 'none';  
        var row = createEventRow("endRequest",  details);
        $get('clientEvents').appendChild(row);                    
    }

    // Helper function for creating a new row
    // in the event viewer table.
    function createEventRow(eventName, details){
                        
        var row = document.createElement("tr");
        
        var eventCell = document.createElement("td");
        var eventText = document.createTextNode(eventName);
        eventCell.appendChild(eventText);
        row.appendChild(eventCell);
                    
        var detailsCell = document.createElement("td");
        var detailsText = document.createTextNode(details);
        detailsCell.appendChild(detailsText);
        row.appendChild(detailsCell);        

        return row;       
    }

    // Helper method that will format some information
    // about the update panels in the pageLoaded and
    // pageLoading events.
    function displayPanels(action, panels){            
        var sb = new Sys.StringBuilder();
        sb.append(action + " " + panels.length + " panel");
        if (panels.length == 0 || panels.length > 1)
            sb.append("s");
        
        if (panels.length > 0){               
            sb.append(" (");
            for (var i = 0; i < panels.length; i++){
                if (i > 0)
                    sb.append(", ");
                    
                sb.append(panels[i].id);
            } 
            sb.append(")");              
        }        
        return sb.toString();
    }    

    // Helper method to clear the events in the
    // event viewer table.
    function clearEvents(){
        var events = $get('clientEvents');            
        while (events.firstChild) {
            events.removeChild(events.firstChild); 
        }      
    }

</script>



</body>
</html>
