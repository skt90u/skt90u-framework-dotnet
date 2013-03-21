<%@ Page Language="C#" %>

<script runat="server">
	protected void radioButtonList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (radioButtonList.SelectedItem != null)
		{
			selectedItemLabel.Text = String.Format("You selected {0}, which has a value of {1}.",
				radioButtonList.SelectedItem.Text,
				radioButtonList.SelectedValue);
		}
	}
	
	protected void pickSeasonButton_Click(object sender, EventArgs e)
	{
		if (radioButtonList.SelectedItem != null)
		{ 
			selectedItemLabel.Text = String.Format("You selected {0}, which has a value of {1}.",
				radioButtonList.SelectedItem.Text,
				radioButtonList.SelectedValue);
		}
	}

	protected void textDirectionButton_Click(object sender, EventArgs e)
	{
		radioButtonList.TextAlign = (radioButtonList.TextAlign == TextAlign.Left ? TextAlign.Right : TextAlign.Left);
		textAlignLabel.Text = String.Format("Current TextAlign is {0}.",
			radioButtonList.TextAlign.ToString());
	}

	protected void autoPostBackButton_Click(object sender, EventArgs e)
	{
		radioButtonList.AutoPostBack = !(radioButtonList.AutoPostBack);
		autoPostBackLabel.Text = String.Format("AutoPostBack is {0}.",
			(radioButtonList.AutoPostBack ? "on" : "off"));
	}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RadioButtonList Sample</title>
    <style type="text/css">
    div.AspNet-RadioButtonList ul
	{
		list-style-type:none;
		padding:0px;
		margin:0px;
	}
	li.AspNet-RadioButtonList-Item
	{
		white-space:nowrap;    /* forces the radio button and text to be on the same line */
	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<h3>RadioButtonList Demo</h3>
		
		<p>What is your favorite season?</p>

		<asp:RequiredFieldValidator runat="server" ID="req_radioButtonList"
			ControlToValidate="radioButtonList" Display="dynamic" EnableClientScript="true"
			ErrorMessage="Please choose your favorite season." />
		<asp:RangeValidator runat="server" ID="val_radioButtonList"
			ControlToValidate="radioButtonList" Display="dynamic" EnableClientScript="true" 
			ErrorMessage="You must decide to continue!" 
			Type="integer" MinimumValue="1" MaximumValue="4" />
		<asp:RadioButtonList runat="server" ID="radioButtonList" CssClass="mylist" CausesValidation="true"
			AutoPostBack="false" OnSelectedIndexChanged="radioButtonList_SelectedIndexChanged">
			<asp:ListItem Value="1">Winter</asp:ListItem>
			<asp:ListItem Value="2">Spring</asp:ListItem>
			<asp:ListItem Value="3">Summer</asp:ListItem>
			<asp:ListItem Value="4">Fall</asp:ListItem>
			<asp:ListItem Value="5">I can't decide!</asp:ListItem>
			<asp:ListItem Enabled="false" Value="6">Disabled item</asp:ListItem>
		</asp:RadioButtonList>
		
		<asp:Button runat="server" ID="pickSeasonButton" Text="Pick a season" 
			CausesValidation="true" />
		<asp:Label runat="server" ID="selectedItemLabel" /><br />
		
		<asp:Button runat="server" ID="textDirectionButton" Text="Toggle TextAlign property"
			CausesValidation="false" OnClick="textDirectionButton_Click" />
		<asp:Label runat="server" ID="textAlignLabel" /><br />

		<asp:Button runat="server" ID="autoPostBackButton" Text="Toggle AutoPostBack property"
			CausesValidation="false" OnClick="autoPostBackButton_Click" />
		<asp:Label runat="server" ID="autoPostBackLabel" /><br />
		
	    <hr />
    </div>
    
    <div>
		<h3>Markup Overview</h3>
		
		<ul>
			<li>The RadioButtonList is rendered as an unordered list <code>&lt;ul&gt;</code>, with each 
				radio button rendered in a list item <code>&lt;li&gt;</code> tag.</li>
			<li>A <code>&lt;div&gt;</code> wraps the markup, and is assigned the CSS class 
				<code>AspNet-RadioButtonList</code>.</li>
			<li>Each list item is assigned the CSS class <code>AspNet-RadioButtonList-Item</code>.</li>
			<li>The <code>CssClass</code> defined for the <code>&lt;asp:RadioButtonList&gt;</code> control 
				is applied to the unordered list (<code>&lt;ul&gt;</code> tag).</li>
		</ul>
				
		<hr />
    </div>
    
    <div>
		<h3>Suggested minimal CSS styles and tips</h3>
		
		<pre>
div.AspNet-RadioButtonList ul
{
	list-style-type:none;  // removes bullet point
	padding:0px;           // removes list item white space
	margin:0px;            // removes list item white space
}
li.AspNet-RadioButtonList-Item
{
	white-space:nowrap;    // forces the radio button and text to be on the same line
}
		</pre>
    </div>
    </form>
</body>
</html>
