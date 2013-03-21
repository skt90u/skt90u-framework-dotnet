(function() {

  /******************************************
  *	static public functions
  ******************************************/
  var AjaxControlToolKitPlugin = {
    RaiseZIndex: function (sender, args) {
        //
        // 目的在解決CalendarExtender在ModalPopupExtender被遮蔽的問題
        // CalendarExtender.OnClientShown = "AjaxControlToolKitPlugin.RaiseZIndex"
        //
        // reference : http://www.jb51.net/article/21047.htm
        
        sender._popupBehavior._element.style.zIndex = 1000005;
      }

  }; // var AjaxControlToolKitPlugin = {

  // Expose AjaxControlToolKitPlugin to the global object
  window.AjaxControlToolKitPlugin = AjaxControlToolKitPlugin;

})(window);
