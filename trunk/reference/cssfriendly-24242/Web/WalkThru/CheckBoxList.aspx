<%@ Page Language="C#" %>

<script runat="server">
	protected void checkBoxList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (checkBoxList.SelectedItem != null)
		{
			string output = "You selected the following: ";
			foreach (ListItem item in checkBoxList.Items)
			{
				if (item.Selected)
				{
					output = String.Concat(output, '[', item.Text, '=', item.Value, ']');
				}
			}
			selectedItemLabel.Text = output;
		}
	}
	
	protected void pickSeasonButton_Click(object sender, EventArgs e)
	{
		if (checkBoxList.SelectedItem != null)
		{
			string output = "You selected the following: ";
			foreach (ListItem item in checkBoxList.Items)
			{
				if (item.Selected)
				{
					output = String.Concat(output, '[', item.Text, '=', item.Value, ']');
				}
			}
			selectedItemLabel.Text = output;
		}
	}

	protected void textDirectionButton_Click(object sender, EventArgs e)
	{
		checkBoxList.TextAlign = (checkBoxList.TextAlign == TextAlign.Left ? TextAlign.Right : TextAlign.Left);
		textAlignLabel.Text = String.Format("Current TextAlign is {0}.",
			checkBoxList.TextAlign.ToString());
	}

	protected void autoPostBackButton_Click(object sender, EventArgs e)
	{
		checkBoxList.AutoPostBack = !(checkBoxList.AutoPostBack);
		autoPostBackLabel.Text = String.Format("AutoPostBack is {0}.",
			(checkBoxList.AutoPostBack ? "on" : "off"));
	}
	
	protected void repeatDirectionButton_Click(object sender, EventArgs e)
	{
		checkBoxList.RepeatDirection = (checkBoxList.RepeatDirection == RepeatDirection.Horizontal ? RepeatDirection.Vertical : RepeatDirection.Horizontal);
		repeatDirectionLabel.Text = String.Format("RepeatDirection is {0}.",
			(checkBoxList.RepeatDirection.ToString()));
	}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CheckBoxList Sample</title>
    <style type="text/css">
    div.AspNet-CheckBoxList ul
	{
		list-style-type:none;
		padding:0px;
		margin:0px;
	}
	li.AspNet-CheckBoxList-Item
	{
		white-space:nowrap;    /* forces the checkbox and text to be on the same line */
	}
	li.AspNet-CheckBoxList-Disabled label
	{
		color:#999;
	}
	ul.AspNet-CheckBoxList-RepeatDirection-Horizontal li
	{
		display:inline;
	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<h3>CheckBoxList Demo</h3>
		
		<p>What is your favorite season?</p>

		<asp:CheckBoxList runat="server" ID="checkBoxList" CssClass="mylist" CausesValidation="true"
			RepeatDirection="Horizontal" AutoPostBack="false" OnSelectedIndexChanged="checkBoxList_SelectedIndexChanged" TabIndex="1">
			<asp:ListItem Value="1">Winter</asp:ListItem>
			<asp:ListItem Value="2">Spring</asp:ListItem>
			<asp:ListItem Value="3">Summer</asp:ListItem>
			<asp:ListItem Value="4">Fall</asp:ListItem>
			<asp:ListItem Value="5">I can't decide!</asp:ListItem>
			<asp:ListItem Enabled="false" Value="6">Disabled item</asp:ListItem>
		</asp:CheckBoxList>
		
		<asp:Button runat="server" ID="pickSeasonButton" Text="Pick a season" 
			CausesValidation="true" />
		<asp:Label runat="server" ID="selectedItemLabel" /><br />
		
		<asp:Button runat="server" ID="textDirectionButton" Text="Toggle TextAlign property"
			CausesValidation="false" OnClick="textDirectionButton_Click" />
		<asp:Label runat="server" ID="textAlignLabel" /><br />

		<asp:Button runat="server" ID="autoPostBackButton" Text="Toggle AutoPostBack property"
			CausesValidation="false" OnClick="autoPostBackButton_Click" />
		<asp:Label runat="server" ID="autoPostBackLabel" /><br />

		<asp:Button runat="server" ID="repeatDirectionButton" Text="Toggle RepeatDirection property"
			CausesValidation="false" OnClick="repeatDirectionButton_Click" />
		<asp:Label runat="server" ID="repeatDirectionLabel" /><br />
		
	    <hr />
    </div>
    
    <div>
		<h3>Markup Overview</h3>
		
		<ul>
			<li>The CheckBoxList is rendered as an unordered list <code>&lt;ul&gt;</code>, with each 
				checkbox button rendered in a list item <code>&lt;li&gt;</code> tag.</li>
			<li>A <code>&lt;div&gt;</code> wraps the markup, and is assigned the CSS class 
				<code>AspNet-CheckBoxList</code>.</li>
			<li>Each list item is assigned the CSS class <code>AspNet-CheckBoxList-Item</code>.</li>
			<li>Each disabled list item is assigned the additional CSS class <code>AspNet-CheckBoxList-Disabled</code>.</li>
			<li>The <code>CssClass</code> defined for the <code>&lt;asp:CheckBoxList&gt;</code> control 
				is applied to the unordered list (<code>&lt;ul&gt;</code> tag).</li>
			<li>The RepeatDirection property adds a CSS class, either <code>AspNet-CheckBoxList-RepeatDirection-Horizontal</code>
				or <code>AspNet-CheckBoxList-RepeatDirection-Vertical</code>. See the sample CSS for a way to handle horizontal directions.</li>
		</ul>
		
		<hr />
    </div>
    
    <div>
		<h3>Suggested minimal CSS styles and tips</h3>
		
		<pre>
div.AspNet-CheckBoxList ul
{
	list-style-type:none;
	padding:0px;
	margin:0px;
}
li.AspNet-CheckBoxList-Item
{
	white-space:nowrap;    /* forces the checkbox and text to be on the same line */
}
li.AspNet-CheckBoxList-Disabled label
{
	color:#999;
}
ul.AspNet-CheckBoxList-RepeatDirection-Horizontal li
{
	display:inline;
}
		</pre>
    </div>
    </form>
</body>
</html>
