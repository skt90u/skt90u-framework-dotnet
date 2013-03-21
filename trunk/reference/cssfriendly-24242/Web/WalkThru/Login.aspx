<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Login Sample</title>
</head>
<body>
	<form id="form1" runat="server">
		<h1>GridView Sample</h1>
		<div>
			<asp:Login ID="_login" runat="server" TitleText="Login" UserNameLabelText="User Id:" PasswordLabelText="Password:"
				RememberMeText="Remember me next time" LoginButtonText="Login" LoginButtonType="Image"
				LoginButtonImageUrl="~/App_Themes/theme/arrowRight.gif" PasswordRecoveryText="Password Recovery"
				PasswordRecoveryUrl="Login.aspx" />
		</div>
	</form>
</body>
</html>
