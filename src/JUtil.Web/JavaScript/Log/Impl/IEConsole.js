/*global Log: true, console: true, window: true, jQuery: true */ 

(function($) {

var IEConsole = function() {};

IEConsole.prototype.D = function() {
  var message = ($.sprintf).apply( null, arguments );
  // IE.console 沒有 function 叫 "debug" 
  console.log(message);
};

IEConsole.prototype.I = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.info(message);
};

IEConsole.prototype.W = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.warn(message);
};

IEConsole.prototype.E = function() {
  var message = ($.sprintf).apply( null, arguments );
  console.error(message);
};

IEConsole.prototype.IsBrowserMatch=function() {
  var isIE = $.browser.msie;
  var hasCONSOLE = !!window.console;
  return hasCONSOLE && isIE;
};
    
Log.Register(new IEConsole());


}(jQuery));
