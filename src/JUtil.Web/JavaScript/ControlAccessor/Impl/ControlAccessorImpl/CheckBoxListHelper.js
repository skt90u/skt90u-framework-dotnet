(function() {

var CheckBoxListHelper = function() {}

/// <summary>
/// Implement GetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>  
CheckBoxListHelper.prototype.GetValue = function($el) {
    
    var values = [];

    $el.find("input[type=checkbox]:checked").each(function() {
      // <td><input id="cbl_0" type="checkbox" name="cbl$0" checked="checked"><label for="cbl_0">男</label></td>
      var $item = $(this);
      var value = $item.next().text();
      values.push(value);
    });
    
    return values;
};

/// <summary>
/// Implement SetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
CheckBoxListHelper.prototype.SetValue=function($el, values) {
  
  $el.find("input[type=checkbox]").each(function() {
    var $item=$(this);
    var value=$item.next().text();
    var checked = matchValue(value);
    $item.attr('checked',checked);
  });
  
  function matchValue(value)
  {
    for(var i=0,len=values.length;i<len;i++) {
      
      if(values[i]==value) 
        return true;
    }
    return false;
  }
};

CheckBoxListHelper.prototype.IsMatchType=function($el) {
  /*
  <table id="cbl" border="0">
	<tbody><tr>
		<td><input id="cbl_0" type="checkbox" name="cbl$0" checked="checked"><label for="cbl_0">男</label></td>
	</tr><tr>
		<td><input id="cbl_1" type="checkbox" name="cbl$1"><label for="cbl_1">女</label></td>
	</tr><tr>
		<td><input id="cbl_2" type="checkbox" name="cbl$2"><label for="cbl_2">人妖</label></td>
	</tr>
  </tbody></table>  
  */
  return $el.is("table") && $el.find("input:checkbox").length > 0;
};


AspNetControlHelperManager.Register(new CheckBoxListHelper());	


})();
