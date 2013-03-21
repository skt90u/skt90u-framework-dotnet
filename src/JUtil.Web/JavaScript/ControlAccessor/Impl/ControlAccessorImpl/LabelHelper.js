/*global AspNetControlHelperManager: true */ 

(function() {

var LabelHelper = function() {};

/// <summary>
/// Implement GetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>  
LabelHelper.prototype.GetValue = function($el) {
  return $el.text();
};

/// <summary>
/// Implement SetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
LabelHelper.prototype.SetValue=function($el, value) {
  $el.text(value);
};

LabelHelper.prototype.IsMatchType=function($el) {
  /*
  <span id="lb">I am a label</span>  
  */
  return $el.is("span");
};


AspNetControlHelperManager.Register(new LabelHelper());


}());
