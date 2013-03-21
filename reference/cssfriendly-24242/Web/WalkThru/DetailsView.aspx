<%@ Page Language="C#" %>

<script runat="server">
	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);

		XmlDataSource ContactsDS = new XmlDataSource();
		ContactsDS.DataFile = "~/App_Data/contacts.xml";
		ContactsDS.XPath = ".//Contacts/Contact";
		detailsViewSample.DataSource = ContactsDS;

		switch (pagerPositionOptions.SelectedValue)
		{
			case "Top":
				detailsViewSample.PagerSettings.Position = PagerPosition.Top;
				detailsViewSample.PagerSettings.Visible = true;
				break;
			case "Bottom":
				detailsViewSample.PagerSettings.Position = PagerPosition.Bottom;
				detailsViewSample.PagerSettings.Visible = true;
				break;
			case "TopAndBottom":
				detailsViewSample.PagerSettings.Position = PagerPosition.TopAndBottom;
				detailsViewSample.PagerSettings.Visible = true;
				break;
			case "Hidden":
				detailsViewSample.PagerSettings.Visible = false;
				break;
		}
		
		switch (pagerTypeOptions.SelectedValue)
		{
			case "NextPrevious":
				detailsViewSample.PagerSettings.Mode = PagerButtons.NextPrevious;
				break;
			case "NextPreviousFirstLast":
				detailsViewSample.PagerSettings.Mode = PagerButtons.NextPreviousFirstLast;
				break;
			case "Numeric":
				detailsViewSample.PagerSettings.Mode = PagerButtons.Numeric;
				break;
			case "NumericFirstLast":
				detailsViewSample.PagerSettings.Mode = PagerButtons.NumericFirstLast;
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
			detailsViewSample.PagerTemplate = null;
		}
	}

	protected override void OnLoadComplete(EventArgs e)
	{
		base.OnLoadComplete(e);

		detailsViewSample.DataBind();
	}

	protected void detailsView_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
	{
		detailsViewSample.PageIndex = e.NewPageIndex;
	}

	protected void TestLinkButton_Click(object sender, EventArgs e)
	{
	}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>DetailsView Sample</title>
    <link rel="stylesheet" href="DetailsView.css" type="text/css" />
</head>
<body>
	<form id="form1" runat="server">
		<h1>DetailsView Sample</h1>
		
		<div>
			<h2>Options</h2>
			<asp:DropDownList runat="server" ID="pagerPositionOptions" AutoPostBack="true">
				<asp:ListItem Value="Top" Selected="true">Top</asp:ListItem>
				<asp:ListItem Value="Bottom">Bottom</asp:ListItem>
				<asp:ListItem Value="TopAndBottom">Top and Bottom</asp:ListItem>
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
			<asp:DetailsView ID="detailsViewSample" runat="server" 
				OnPageIndexChanging="detailsView_PageIndexChanging"
				AllowPaging="True" AutoGenerateRows="false" EnableViewState="true">
				<HeaderTemplate>
					<h3>Author Details</h3>
				</HeaderTemplate>
				<Fields>
					<asp:BoundField HeaderText="ID" DataField="au_id" />
					<asp:BoundField HeaderText="Last name" DataField="au_lname" />
					<asp:BoundField HeaderText="First name" DataField="au_fname" />
					<asp:BoundField HeaderText="Phone" DataField="phone" />
					<asp:BoundField HeaderText="Street" DataField="address" />
					<asp:BoundField HeaderText="City" DataField="city" />
					<asp:BoundField HeaderText="State" DataField="state" />
					<asp:BoundField HeaderText="ZIP code" DataField="zip" />
					<asp:BoundField HeaderText="Contract" DataField="contract" />
				</Fields>
				<FooterTemplate>
					<hr />
				</FooterTemplate>
				<EmptyDataTemplate>
					<p>There is no data available.</p>
					 <asp:LinkButton ID="TestLinkButton" runat="server" CssClass="button" Text="Test Link" OnClick="TestLinkButton_Click" />
				</EmptyDataTemplate>
				<PagerSettings Mode="NextPreviousFirstLast" Position="bottom" 
					NextPageText="Next" PreviousPageText="Prev" FirstPageText="First" LastPageText="Last" />
				<PagerTemplate>
					<asp:Button runat="server" Text="First" CommandName="Page" CommandArgument="First"
						Enabled="<%# detailsViewSample.PageIndex > 0 %>" />
					<asp:Button runat="server" Text="Prev" CommandName="Page" CommandArgument="Prev"
						Enabled="<%# detailsViewSample.PageIndex > 0 %>" />
					<span runat="server">Page <%= detailsViewSample.PageIndex + 1 %> of <%= detailsViewSample.PageCount %></span>
					<asp:Button runat="server" Text="Next" CommandName="Page" CommandArgument="Next"
						Enabled="<%# detailsViewSample.PageIndex + 1 < detailsViewSample.PageCount %>" />
					<asp:Button runat="server" Text="Last" CommandName="Page" CommandArgument="Last"
						Enabled="<%# detailsViewSample.PageIndex + 1 < detailsViewSample.PageCount %>" />
				</PagerTemplate>
			</asp:DetailsView>
		</div>
	</form>
</body>
</html>
