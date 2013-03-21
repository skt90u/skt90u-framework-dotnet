/*global Log: true, console: true, window: true, jQuery: true */ 

(function($) {

var FirefoxConsole = function() {};

FirefoxConsole.prototype.D = function() {
  var message=($.sprintf).apply(null,arguments);
  console.debug(message);
};

FirefoxConsole.prototype.I = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.info(message);
};

FirefoxConsole.prototype.W = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.warn(message);
};

FirefoxConsole.prototype.E = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.error(message);
};

FirefoxConsole.prototype.IsBrowserMatch=function() {
  var isFirefox = $.browser.mozilla;
  var hasCONSOLE = !!window.console;
  return hasCONSOLE && isFirefox;
};
    
Log.Register(new FirefoxConsole());


}(jQuery));
