/*global AspNetControlHelperManager: true */ 

(function() {

var RadioButtonListHelper = function() {};

/// <summary>
/// Implement GetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
/// <remarks>
/// reference : http://blog.csdn.net/pandahyang/archive/2009/06/28/4304924.aspx
/// </remarks>  
RadioButtonListHelper.prototype.GetValue = function($el) {
    return $el.find("input:checked").val();
};

/// <summary>
/// Implement SetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
/// <remarks>
/// reference : http://codeasp.net/blogs/raghav_khunger/microsoft-net/716/jquery-set-selected-item-of-radiobuttonlist
/// </remarks>  
RadioButtonListHelper.prototype.SetValue=function($el, value) {
  
  var $items = $el.find("input:radio");
  
  for(var i=0,len=$items.length; i<len; i++)
  {
    var checked = ($items[i].value == value);
    $items[i].checked = checked;
  } 
};

RadioButtonListHelper.prototype.IsMatchType=function($el) {
  /*
  <table id="tfSEX" border="0">
	  <tbody><tr>
		  <td><input id="tfSEX_0" type="radio" name="tfSEX" value="男" checked="checked"><label for="tfSEX_0">男</label></td>
	  </tr><tr>
		  <td><input id="tfSEX_1" type="radio" name="tfSEX" value="女"><label for="tfSEX_1">女</label></td>
	  </tr>
  </tbody></table>   
  */
  return $el.is("table") && $el.find("input:radio").length > 0;
};


AspNetControlHelperManager.Register(new RadioButtonListHelper());


}());
