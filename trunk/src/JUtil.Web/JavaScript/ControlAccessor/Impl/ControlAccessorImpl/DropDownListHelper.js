/*
 * 以下元件都適用
 * asp:ListBox     
 * asp:DropDownList
 */
 
/*global AspNetControlHelperManager: true */ 

(function() {

var DropDownListHelper = function() {};

/// <summary>
/// Implement GetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>  
DropDownListHelper.prototype.GetValue = function($el) {
  return $el.val();
};

/// <summary>
/// Implement SetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
DropDownListHelper.prototype.SetValue=function($el, value) {
  $el.val(value);
};

DropDownListHelper.prototype.IsMatchType=function($el) {
  /*
  <select name="DropDownList1" id="DropDownList1">
	  <option selected="selected" value="男">男</option>
	  <option value="女">女</option>
  </select> 
  */
  
  // return $el.is("select") && $el.find("option").length > 0;
  // dropdownlist can have no item inside
  return $el.is("select");
};


AspNetControlHelperManager.Register(new DropDownListHelper());	


}());
