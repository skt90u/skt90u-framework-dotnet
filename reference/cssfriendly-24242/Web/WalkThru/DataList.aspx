<%@ Page Language="C#" %>

<script runat="server">
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>DataList Sample</title>
    <link rel="stylesheet" href="DataList.css" type="text/css" />
</head>
<body>
	<form id="form1" runat="server">
		<asp:XmlDataSource ID="ContactsDS" DataFile="~/App_Data/contacts.xml" runat="server" XPath=".//Contacts/Contact" />
		<div>
			<p>This sample page exposes a DataList rendered using the CSS adapters.</p>
			<div class="tablesDataList">
			<asp:DataList ID="DataListSample" runat="server" DataSourceID="ContactsDS" Caption="Author Details" EnableViewState="true"
				    RepeatColumns="3" RepeatDirection="Horizontal">
				<HeaderTemplate>
					Author Names
				</HeaderTemplate>
				<ItemTemplate>
					<%# Eval("au_lname") %> <%# Eval("au_fname") %>
				</ItemTemplate>
				<HeaderStyle CssClass="red" />
				<FooterTemplate>
					Thank you for testing!
				</FooterTemplate>
				<SeparatorTemplate>
				::
				</SeparatorTemplate>
				<FooterStyle CssClass="highlight" />
			</asp:DataList>
			</div>
		    <br />
		    <br />
		    <div class="flowDataListMultipleColumns">
			    <asp:DataList ID="dlstFlowMultipleColumns" runat="server" DataSourceID="ContactsDS" Caption="Author Details" EnableViewState="true"
				    RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Flow">
				    <HeaderTemplate>
					    RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Flow"
				    </HeaderTemplate>
				    <ItemTemplate>
					    <%# Eval("au_lname") %> <%# Eval("au_fname") %>
				    </ItemTemplate>
				    <FooterTemplate>
					    Thank you for testing!
				    </FooterTemplate>
			    </asp:DataList>
		    </div>
		    <br />
		    <br />
		    <div class="flowDataListMultipleColumnsVertical">
			    <asp:DataList ID="dlstFlowMultipleColumnsVertical" runat="server" DataSourceID="ContactsDS" Caption="Author Details" EnableViewState="true"
				    RepeatColumns="3" RepeatDirection="Vertical" RepeatLayout="Flow">
				    <HeaderTemplate>
					    RepeatColumns="3" RepeatDirection="Vertical" RepeatLayout="Flow"
				    </HeaderTemplate>
				    <ItemTemplate>
					    <%# Eval("au_lname") %> <%# Eval("au_fname") %>
				    </ItemTemplate>
				    <FooterTemplate>
					    Thank you for testing!
				    </FooterTemplate>
			    </asp:DataList>
		    </div>
		    <br />
		    <br />
		    <div class="flowDataListSingleColumn">
			    <asp:DataList ID="dlstFlowSingleColumn" runat="server" DataSourceID="ContactsDS" Caption="Author Details" EnableViewState="true"
				    RepeatColumns="1" RepeatLayout="Flow">
				    <HeaderTemplate>
					    RepeatColumns="1" RepeatLayout="Flow"
				    </HeaderTemplate>
				    <ItemTemplate>
					    <%# Eval("au_lname") %> <%# Eval("au_fname") %>
				    </ItemTemplate>
				    <FooterTemplate>
					    Thank you for testing!
				    </FooterTemplate>
			    </asp:DataList>
		    </div>
		</div>
	</form>
</body>
</html>
