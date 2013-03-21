/*global AspNetControlHelperManager: true */ 

(function() {

var HiddenFieldHelper = function() { };

/// <summary>
/// Implement GetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>  
HiddenFieldHelper.prototype.GetValue = function($el) {
  return $el.val();
};

/// <summary>
/// Implement SetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
HiddenFieldHelper.prototype.SetValue=function($el, value) {
  $el.val(value);
};

HiddenFieldHelper.prototype.IsMatchType=function($el) {
  /*
  <input type="hidden" name="hf" id="hf" value="123"> 
  */
  return $el.is("input:hidden");
};


AspNetControlHelperManager.Register(new HiddenFieldHelper());


}());
