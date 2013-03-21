(function() {

var TextBoxHelper = function() {}

/// <summary>
/// Implement GetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
TextBoxHelper.prototype.GetValue = function($el) {
  return $el.val();
};

/// <summary>
/// Implement SetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
TextBoxHelper.prototype.SetValue=function($el, value) {
  $el.val(value);
};

TextBoxHelper.prototype.IsMatchType=function($el) {
  /*
  <input name="tb" type="text" value="I am a TextBox" id="tb">  
  */
  return $el.is("input:text");
};


AspNetControlHelperManager.Register(new TextBoxHelper());	


})();
