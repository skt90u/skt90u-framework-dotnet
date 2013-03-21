/*global Log: true, console: true, window: true, jQuery: true */ 

(function($) {

var SafariConsole = function() {};

SafariConsole.prototype.D = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.debug(message);
};

SafariConsole.prototype.I = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.info(message);
};

SafariConsole.prototype.W = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.warn(message);
};

SafariConsole.prototype.E = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.error(message);
};
  
SafariConsole.prototype.IsBrowserMatch = function() {
  // reference
  //   - http://javascriptly.com/2008/09/javascript-to-detect-google-chrome/
  //   - http://api.jquery.com/jQuery.browser/
  var isSafari = $.browser.safari;
  var hasCONSOLE = !!window.console;
  return hasCONSOLE && isSafari;
};
    
    
Log.Register(new SafariConsole());

}(jQuery));
