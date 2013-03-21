/*global Log: true, console: true, window: true, jQuery: true */ 

(function($) {

var ChromeConsole = function() {};

ChromeConsole.prototype.D = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.debug(message);
};

ChromeConsole.prototype.I = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.info(message);
};

ChromeConsole.prototype.W = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.warn(message);
};

ChromeConsole.prototype.E = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.error(message);
};

ChromeConsole.prototype.IsBrowserMatch=function() {
  // reference
  //   - http://javascriptly.com/2008/09/javascript-to-detect-google-chrome/
  //   - http://api.jquery.com/jQuery.browser/
  var isChrome = $.browser.chrome;
  var hasCONSOLE = !!window.console;
  return hasCONSOLE && isChrome;
};

    
Log.Register(new ChromeConsole());


}(jQuery));
