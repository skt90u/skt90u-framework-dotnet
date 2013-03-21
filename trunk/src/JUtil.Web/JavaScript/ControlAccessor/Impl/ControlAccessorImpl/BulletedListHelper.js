/*global AspNetControlHelperManager: true */ 

(function() {

  var BulletedListHelper = function() { };

  /// <summary>
  /// Implement GetValue function of IAspNetControlHelper interface
  /// </summary>
  /// <param name="$el">a jQuery object</param>  
  BulletedListHelper.prototype.GetValue = function($el) {
    var value = [];
    $.each($el.find('li'), function(id, own) {
      value.push($(own).text());
    });
    return value;
  };

  /// <summary>
  /// Implement SetValue function of IAspNetControlHelper interface
  /// </summary>
  /// <param name="$el">a jQuery object</param>
  BulletedListHelper.prototype.SetValue = function($el, value) {
    $.each($el.find('li'), function(id, own) {
      $(own).remove();
    });
    
    $.each(value, function(idx, val) {
      $el.append('<li>' + val + '</li>');
    });
  };

  BulletedListHelper.prototype.IsMatchType = function($el) {
    /*
    <ul id="bl">
    <li>1</li>
    <li>2</li>
    <li>3</li>
    </ul>
    */
    return $el.is("ul");
  };


  AspNetControlHelperManager.Register(new BulletedListHelper());


} ());
