<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageBoard.aspx.cs" Inherits="MessageBoardPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Message Board</title>
    <link href="styles.css" type="text/css" rel="stylesheet" media="all" />
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"> 
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/AutoSave.js" />
                <asp:ScriptReference Path="~/Scripts/Main.js" />
                <asp:ScriptReference Path="~/scripts/Resources.js" />
            </Scripts>  
            <Services>
                <asp:ServiceReference Path="~/MessageBoardService.asmx" InlineScript="True" />
            </Services>         
        </asp:ScriptManager>
        
        <div id="header">
            <div class="bg">
                <div class="r1">
                    <div id="storeLogo">
                        <img src="images/MessageBoard.png" alt="Message Board" />
                    </div>
               </div>
            </div>
        </div>
		<div id="content">
			<div id="content-inner">
				
				<div id="contentOnly">
				    <div id="fullContent" >
				        <div >
				            <div>
				                <a href="#" id="NewPost" onclick="return onNewPost();">New Post</a>&nbsp;
                                <a href="#" id="LoginLink" runat="server" onclick="return onLoginClick()" >Login</a>
                                <appcode:AjaxLogin ID="Login1" runat="server" style="display:none" OnLogonFailedHandler="onLogonFailed" OnLogonSuccessHandler="onLogonSuccess" CreateUserText="Register" CreateUserUrl="Register.aspx">
                                </appcode:AjaxLogin>
				                <div id="GetMessagesStatus" style="color: Red;"></div>
                                <div id="NewMessageForm" style="display: none; padding-top: 10px; padding-bottom: 10px;">
                                    <asp:Panel ID="PostPanel" runat="server" GroupingText="Post a Message" Width="95%">
                                        <div class="title" style="padding-top: 5px; display: none;">
                                            Post a message
                                        </div>
                                        <div class="subtitle">
                                            Use the form below to post your message
                                        </div>
                                        <table border="0" cellspacing="0">
                                            <tr>
                                                <td>Subject:</td>
                                                <td>
                                                    <asp:TextBox ID="Subject" runat="server" MaxLength="100" Width="200px" OnInit="Subject_Init"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Message:</td>
                                                <td>
                                                    <asp:TextBox ID="MessageText" runat="server" Rows="5" TextMode="MultiLine" Width="200px" OnInit="MessageText_Init"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="Post" runat="server" Text="Post" OnClientClick="return onPostMessage();" />
                                                    <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClientClick="return onCancelPostMessage();" />                                            
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="PostStatus" style="color: Red;">
                                        </div>
                                    </asp:Panel>
                                </div>				                
				            </div>
                            <div style="padding-top: 10px;">
                                <div class="title">
                                    Messages
                                </div>
                                <asp:GridView ID="Messages" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"
                                    DataKeyNames="MessageId" DataSourceID="MessageDataSource" Width="98%">
                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Subject">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="ShowMessageText" Text='<%# Eval("Subject") %>'
                                                        messageId='<%# Eval("MessageID") %>' class="messageLink"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                        
                                            <asp:BoundField DataField="UserName" HeaderText="Posted By" SortExpression="UserName"
                                                NullDisplayText="(Anonymous)" />
                                            <asp:BoundField DataField="PostedDate" HeaderText="Date" SortExpression="PostedDate" />
                                        </Columns>
                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="MessageDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MessageDatabase %>"
                                    SelectCommand="SELECT aspnet_Users.UserName, Messages.Subject, Messages.MessageId, Messages.Text, Messages.PostedDate FROM Messages LEFT OUTER JOIN aspnet_Users ON Messages.PostedBy = aspnet_Users.UserId ORDER BY Messages.PostedDate DESC">
                                </asp:SqlDataSource>                                
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
            © 2007 Manning Publications Co. - ASP.NET AJAX In Action    
            </div>
        </div>

    </form>
    
<script type="text/javascript" language="javascript">
<!--
    Sys.Application.add_init(onInit);
    Sys.Application.add_unload(onUnload);
    
    function onInit(sender, args){    
        var messages = $get("<%= Messages.ClientID %>");
        $addHandler(messages, 'click', onShowMessage);        
        window.setTimeout(checkForNewMessages, 1000);       
    }
    
    function onUnload(sender, args){        
        var messages = $get("<%= Messages.ClientID %>");
        $removeHandler(messages, 'click', onShowMessage);
    }

    function onShowMessage(e){
        if (e.target.className == 'messageLink') {
			var id = parseInt(e.target.getAttribute('messageId'));
			var parentElement = e.target.parentNode;
			
			while(parentElement.tagName.toLowerCase() != 'tr')
				parentElement = parentElement.parentNode;
			
			if (!parentElement.state) {
				AspNetAjaxInAction.MessageBoardService.GetMessageText(id, onGetMessageTextSuccess, onGetMessageTextFailure, parentElement);
				parentElement.state = 'loading';
			} else if (parentElement.state == 'loaded') {
				var textRow = parentElement.parentNode.rows[parentElement.rowIndex + 1];
				if (textRow.style.display == 'none') {
					textRow.style.display = '';
				} else {
					textRow.style.display = 'none';
				}
			}
        }        
        //Note normally returning false from the event handler code works
        //but for some reason in the latest version of FireFox it does not
        //so preventDefault is used
        e.preventDefault();
        return false;
    }
    
    function onGetMessageTextSuccess(text, row, methodName){
		var textRow = row.parentNode.insertRow(row.rowIndex + 1);
		textRow.className = "TextRow";
		var cell = textRow.insertCell(-1);
		cell.colSpan = 3;
		cell.innerHTML = text;
		row.state = 'loaded';    
    }
    
    function onGetMessageTextFailure(error, row, methodName){
        delete row.state;
    }

    function checkForNewMessages(){    
        var messages = $get("<%= Messages.ClientID %>");
        var lastMessageID = messages.rows[1].cells[0].getElementsByTagName('a')[0].getAttribute('messageID');
        AspNetAjaxInAction.MessageBoardService.GetMessages(10, lastMessageID, onGetMessagesSuccess, onGetMessagesFailure);
    }

    function onGetMessagesSuccess(messages, context, methodName){
        Sys.Debug.traceDump(messages);
		var grid = $get('<%= Messages.ClientID %>');
		
		Array.forEach(messages, function(message) {
			var row = grid.insertRow(1);
			row.className = "Row";
			var subjectCell = row.insertCell(-1);
			var link = document.createElement('a');
			link.className = "messageLink";
			link.innerHTML = message.Subject;
			link.href = '#' + message.MessageID;
			link.setAttribute('messageId', message.MessageID);
			
			var userCell = row.insertCell(-1);
			userCell.innerHTML = message.PostedBy;

			subjectCell.appendChild(link);
			var dateCell = row.insertCell(-1);
			dateCell.innerHTML = message.DatePosted.localeFormat("M/dd/yyyy h:mm:ss tt");
		});
		
		
		$get("GetMessagesStatus").innerHTML = messages.length ? messages.length + " new messages" : "";
		
		//Free up unused rows
		var rowsPerPage = <%=this.Messages.PageSize %> + 1 + <%= Messages.BottomPagerRow != null && Messages.BottomPagerRow.Visible ? 1 : 0 %>;
		
		while (grid.rows.length > rowsPerPage) {
		    grid.deleteRow(grid.rows.length - 2);
		}
		
		window.setTimeout(checkForNewMessages, 1000);
    }
    
    function onGetMessagesFailure(result, context, methodName){
		$get("GetMessagesStatus").innerHTML = error.get_message();		
		window.setTimeout(checkForNewMessages, 1000);    
    }

    function onNewPost(){
        $get("NewMessageForm").style.display = "";
        $get("NewPost").style.display = "none";
        $get("PostStatus").innerHTML = "";        
        return false;
    }

    function onPostMessage(){
        postMessage($get("Subject").value, $get("MessageText").value);
        return false;
    }

    function postMessage(subject, text){
        //var params = {"subject" : subject, "text" : text};
        //Sys.Net.WebServiceProxy.invoke("MessageBoardService.asmx", "PostMessage", false, params, onPostMessageSuccess, onPostMessageFailure, null, 1000);
        
        AspNetAjaxInAction.MessageBoardService.PostMessage(subject, text, onPostMessageSuccess, onPostMessageFailure, "user context", 1);        
        $get("PostStatus").innerHTML = "Posting...";
    }

    function onPostMessageSuccess(result, context, methodName){
        $get("NewMessageForm").style.display = "none";
        $get("NewPost").style.display = ""; 
        $get("Subject").value = "";
        $get("MessageText").value = "";        
    }

    function onPostMessageFailure(result, context, methodName){
        var errorMessage = result.get_message();
        $get("PostStatus").innerHTML = errorMessage;
    }
    
    function onCancelPostMessage(){
        $get("NewMessageForm").style.display = "none";
        $get("NewPost").style.display = "";
        return false;
    }
    
  

//-->
</script>
    
    
</body>
</html>
