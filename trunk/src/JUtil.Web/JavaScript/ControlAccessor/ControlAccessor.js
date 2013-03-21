(function() {

  /******************************************
  *	static public functions
  ******************************************/
  var ControlAccessor = {
    GetControlValue: function($el) {
      var IAspNetControlHelper = AspNetControlHelperManager.GetHelper($el);
      return IAspNetControlHelper.GetValue($el);
    },

    SetControlValue: function($el, value) {
      var IAspNetControlHelper = AspNetControlHelperManager.GetHelper($el);
      IAspNetControlHelper.SetValue($el, value);
    }

  }; // var ControlAccessor = {

  // Expose ControlAccessor to the global object
  window.ControlAccessor = ControlAccessor;

})(window);
