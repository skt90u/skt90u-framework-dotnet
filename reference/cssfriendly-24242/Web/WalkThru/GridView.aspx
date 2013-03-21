<%@ Page Language="C#" %>

<script runat="server">
	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);

		XmlDataSource ContactsDS = new XmlDataSource();
		ContactsDS.DataFile = "~/App_Data/contacts.xml";
		ContactsDS.XPath = ".//Contacts/Contact";
		gridViewSample.DataSource = ContactsDS;

		switch (pagerPositionOptions.SelectedValue)
		{
			case "Top":
				gridViewSample.PagerSettings.Position = PagerPosition.Top;
				gridViewSample.PagerSettings.Visible = true;
				break;
			case "Bottom":
				gridViewSample.PagerSettings.Position = PagerPosition.Bottom;
				gridViewSample.PagerSettings.Visible = true;
				break;
			case "TopAndBottom":
				gridViewSample.PagerSettings.Position = PagerPosition.TopAndBottom;
				gridViewSample.PagerSettings.Visible = true;
				break;
			case "Hidden":
				gridViewSample.PagerSettings.Visible = false;
				break;
		}
		
		switch (pagerTypeOptions.SelectedValue)
		{
			case "NextPrevious":
				gridViewSample.PagerSettings.Mode = PagerButtons.NextPrevious;
				break;
			case "NextPreviousFirstLast":
				gridViewSample.PagerSettings.Mode = PagerButtons.NextPreviousFirstLast;
				break;
			case "Numeric":
				gridViewSample.PagerSettings.Mode = PagerButtons.Numeric;
				break;
			case "NumericFirstLast":
				gridViewSample.PagerSettings.Mode = PagerButtons.NumericFirstLast;
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
			gridViewSample.PagerTemplate = null;
		}
	}

	protected override void OnLoadComplete(EventArgs e)
	{
		base.OnLoadComplete(e);

		gridViewSample.DataBind();
	}

	protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gridViewSample.PageIndex = e.NewPageIndex;
	}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>GridView Sample</title>
    <link rel="stylesheet" href="GridView.css" type="text/css" />
</head>
<body>
	<form id="form1" runat="server">
		<h1>GridView Sample</h1>
		
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
			<asp:GridView ID="gridViewSample" runat="server" Caption="Author Details" 
				OnPageIndexChanging="gridView_PageIndexChanging" AlternatingRowStyle-CssClass=""
				AllowPaging="True" AutoGenerateColumns="false" EnableViewState="true" EditIndex="1" SelectedIndex="2">
				<Columns>
					<asp:BoundField HeaderText="ID" DataField="au_id" />
					<asp:BoundField HeaderText="Last name" DataField="au_lname" />
					<asp:BoundField HeaderText="First name" DataField="au_fname" />
					<asp:BoundField HeaderText="Phone" DataField="phone" />
					<asp:BoundField HeaderText="Street" DataField="address" />
					<asp:BoundField HeaderText="City" DataField="city" />
					<asp:BoundField HeaderText="State" DataField="state" />
					<asp:BoundField HeaderText="ZIP code" DataField="zip" />
					<asp:BoundField HeaderText="Contract" DataField="contract" />
				</Columns>
				<EmptyDataTemplate>
					<p>There is no data available.</p>
				</EmptyDataTemplate>
				<PagerSettings Mode="NextPrevious" Position="top"
					NextPageText="Next" PreviousPageText="Prev" FirstPageText="First" LastPageText="Last" />
				<PagerTemplate>
					<asp:Button runat="server" Text="First" CommandName="Page" CommandArgument="First"
						Enabled="<%# gridViewSample.PageIndex > 0 %>" />
					<asp:Button runat="server" Text="Prev" CommandName="Page" CommandArgument="Prev"
						Enabled="<%# gridViewSample.PageIndex > 0 %>" />
					<span runat="server">Page <%= gridViewSample.PageIndex + 1 %> of <%= gridViewSample.PageCount %></span>
					<asp:Button runat="server" Text="Next" CommandName="Page" CommandArgument="Next"
						Enabled="<%# gridViewSample.PageIndex + 1 < gridViewSample.PageCount %>" />
					<asp:Button runat="server" Text="Last" CommandName="Page" CommandArgument="Last"
						Enabled="<%# gridViewSample.PageIndex + 1 < gridViewSample.PageCount %>" />
				</PagerTemplate>
				<HeaderStyle CssClass="gvTitle" />
				<RowStyle CssClass="gvRow" />
				<AlternatingRowStyle CssClass="gvAltRow" />
			</asp:GridView>
		</div>
		
	</form>
</body>
</html>
