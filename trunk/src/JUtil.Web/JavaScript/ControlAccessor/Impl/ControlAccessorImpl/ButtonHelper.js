(function() {

var ButtonHelper = function() {}

/// <summary>
/// Implement GetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
ButtonHelper.prototype.GetValue = function($el) {
  return $el.val();
};

/// <summary>
/// Implement SetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
ButtonHelper.prototype.SetValue=function($el, value) {
  $el.val(value);
};

ButtonHelper.prototype.IsMatchType=function($el) {
  /*
  <input type="submit" name="btn" value="" id="btn">
  */
return $el.is("input:submit");
};


AspNetControlHelperManager.Register(new ButtonHelper());	


})();
