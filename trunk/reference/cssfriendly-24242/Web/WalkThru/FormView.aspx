<%@ Page Language="C#" %>

<script runat="server">
	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);

		XmlDataSource ContactsDS = new XmlDataSource();
		ContactsDS.DataFile = "~/App_Data/contacts.xml";
		ContactsDS.XPath = ".//Contacts/Contact";
		formViewSample.DataSource = ContactsDS;

		switch (pagerPositionOptions.SelectedValue)
		{
			case "Top":
				formViewSample.PagerSettings.Position = PagerPosition.Top;
				formViewSample.PagerSettings.Visible = true;
				break;
			case "Bottom":
				formViewSample.PagerSettings.Position = PagerPosition.Bottom;
				formViewSample.PagerSettings.Visible = true;
				break;
			case "TopAndBottom":
				formViewSample.PagerSettings.Position = PagerPosition.TopAndBottom;
				formViewSample.PagerSettings.Visible = true;
				break;
			case "Hidden":
				formViewSample.PagerSettings.Visible = false;
				break;
		}
		
		switch (pagerTypeOptions.SelectedValue)
		{
			case "NextPrevious":
				formViewSample.PagerSettings.Mode = PagerButtons.NextPrevious;
				break;
			case "NextPreviousFirstLast":
				formViewSample.PagerSettings.Mode = PagerButtons.NextPreviousFirstLast;
				break;
			case "Numeric":
				formViewSample.PagerSettings.Mode = PagerButtons.Numeric;
				break;
			case "NumericFirstLast":
				formViewSample.PagerSettings.Mode = PagerButtons.NumericFirstLast;
				break;
		}
		
		if (emptyDataSetOption.Checked)
		{
			ContactsDS.XPath = ".//Contacts/NoContact";
		}
		else
		{
			ContactsDS.XPath = ".//Contacts/Contact";
		}

		if (customPagerTemplateOption.Checked == false)
		{
			formViewSample.PagerTemplate = null;
		}
	}

	protected override void OnLoadComplete(EventArgs e)
	{
		base.OnLoadComplete(e);

		formViewSample.DataBind();
	}

	protected void formView_PageIndexChanging(object sender, FormViewPageEventArgs e)
	{
		formViewSample.PageIndex = e.NewPageIndex;
	}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>FormView Sample</title>
    <link rel="stylesheet" href="FormView.css" type="text/css" />
</head>
<body>
	<form id="form1" runat="server">
		<h1>FormView Sample</h1>
		
		<div>
			<h2>Options</h2>
			<asp:DropDownList runat="server" ID="pagerPositionOptions" AutoPostBack="true">
				<asp:ListItem Value="Top" Selected="true">Top</asp:ListItem>
				<asp:ListItem Value="Bottom">Bottom</asp:ListItem>
				<asp:ListItem Value="TopAndBottom">TopAndBottom</asp:ListItem>
				<asp:ListItem Value="Hidden">Hidden</asp:ListItem>
			</asp:DropDownList>
			<asp:DropDownList runat="server" ID="pagerTypeOptions" AutoPostBack="true">
				<asp:ListItem Value="NextPrevious" Selected="true">NextPrevious</asp:ListItem>
				<asp:ListItem Value="NextPreviousFirstLast">NextPreviousFirstLast</asp:ListItem>
				<asp:ListItem Value="Numeric">Numeric</asp:ListItem>
				<asp:ListItem Value="NumericFirstLast">NumericFirstLast</asp:ListItem>
			</asp:DropDownList>
			<asp:CheckBox runat="server" ID="emptyDataSetOption" AutoPostBack="true" Text="Use empty data set" TextAlign="right" />
			<asp:CheckBox runat="server" ID="customPagerTemplateOption" AutoPostBack="true" Text="Use custom pager template" TextAlign="right" />
		</div>
		
		<hr />
		
		<div>
			<asp:FormView ID="formViewSample" runat="server" 
				OnPageIndexChanging="formView_PageIndexChanging"
				HeaderText="Author Details" AllowPaging="True" EnableViewState="True">
				<HeaderTemplate>
					<dl>
				</HeaderTemplate>
				<ItemTemplate>
						<dt><%# XPath("./@au_fname")%> <%# XPath("./@au_lname")%></dt>
						<dd><%# XPath("./@address")%><br />
							<%# XPath("./@city")%>, <%# XPath("./@state")%>  <%# XPath("./@zip")%><br />
							<%# XPath("./@phone")%></dd>
				</ItemTemplate>
				<FooterTemplate>
					</dl>
				</FooterTemplate>
				<PagerSettings Mode="NumericFirstLast" Position="bottom" PageButtonCount="10" />
				<PagerTemplate>
					<asp:Button runat="server" Text="First" CommandName="Page" CommandArgument="First"
						Enabled="<%# formViewSample.PageIndex > 0 %>" />
					<asp:Button runat="server" Text="Prev" CommandName="Page" CommandArgument="Prev"
						Enabled="<%# formViewSample.PageIndex > 0 %>" />
					<span runat="server">Page <%= formViewSample.PageIndex + 1 %> of <%= formViewSample.PageCount %></span>
					<asp:Button runat="server" Text="Next" CommandName="Page" CommandArgument="Next"
						Enabled="<%# formViewSample.PageIndex + 1 < formViewSample.PageCount %>" />
					<asp:Button runat="server" Text="Last" CommandName="Page" CommandArgument="Last"
						Enabled="<%# formViewSample.PageIndex + 1 < formViewSample.PageCount %>" />
				</PagerTemplate>
				<EmptyDataTemplate>
					<p>There is no data available.</p>
				</EmptyDataTemplate>
			</asp:FormView>
		</div>
	</form>
</body>
</html>
