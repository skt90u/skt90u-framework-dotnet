Type.registerNamespace("Samples");

Samples.AjaxLogin = function(element) {
	Samples.AjaxLogin.initializeBase(this, [element]);
}

Samples.AjaxLogin.prototype = {
	initialize : function() {
		Samples.AjaxLogin.callBaseMethod(this, 'initialize');
		this._onLoginButtonClickedDelegate = Function.createDelegate(this, this._onLoginButtonClicked);
		$addHandler(this._loginButton, 'click', this._onLoginButtonClickedDelegate);
		this._originalonclick = this._loginButton.onclick;
		this._loginButton.onclick = ""; //Disable any default action
	},
	
	dispose : function() {
		$removeHandler(this._loginButton, 'click', this._onLoginButtonClickedDelegate);
		this._userName = null;
		this._password = null;
		this._rememberMe = null;
		this._loginButton = null;
		this._onLoginButtonClickedDelegate = null;
	},
	
	_onLoginButtonClicked : function(e) {
        var validationResult = typeof(Page_ClientValidate) == 'function' && 
								Page_ClientValidate(this._validationGroup) ? true : false;

		if (validationResult) {
			Sys.Services.AuthenticationService.login(
				this._userName.value,
				this._password.value,
				this._rememberMe && this._rememberMe.checked,
				null,
				null,
				Function.createDelegate(this, this._onLoginComplete),
				Function.createDelegate(this, this._onLoginFailed)	
			);
			
			this._element.disabled = true;
		}
		e.preventDefault();
	},
	
	_onLoginComplete : function(result) {
		this._raiseEvent(result ? "logonSuccess" : "logonFailed", Sys.EventArgs.Empty);
		this._element.disabled = false;
	},
	
	_onLoginFailed : function(err) {
		this._raiseEvent("logonFailed", Sys.EventArgs.Empty);
		this._element.disabled = false;
	},
	
	set_UserName : function(value) {
		this._userName = value;
	},
	
	set_Password : function(value) {
		this._password = value;
	},
		
	set_RememberMe : function(value) {
		this._rememberMe = value;
	},
	
	set_LoginButton : function(value) {
		this._loginButton = value;
	},

	set_ValidationGroup : function(value) {
		this._validationGroup = value;
	},

	get_UserName : function() {
		return this._userName ;
	},
	
	get_Password : function() {
		return this._password ;
	},
		
	get_RememberMe : function() {
		return this._rememberMe ;
	},
	
	get_LoginButton : function() {
		return this._loginButton ;
	},
	
	get_ValidationGroup : function() {
		return this._validationGroup;
	}
};

Samples.AjaxLogin.createEvent("logonSuccess");
Samples.AjaxLogin.createEvent("logonFailed");

Samples.AjaxLogin.registerClass('Samples.AjaxLogin', Sys.UI.Control);