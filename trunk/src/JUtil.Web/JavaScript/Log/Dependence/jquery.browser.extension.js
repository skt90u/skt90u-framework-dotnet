/*
 *	http://javascriptly.com/2008/09/javascript-to-detect-google-chrome/
 */
 
/*global jQuery: true, window: true */ 
 
(function($) {

  var userAgent = window.navigator.userAgent;
  var is_chrome = /chrome/.test(userAgent.toLowerCase());
  var is_opera  = /opera/.test(userAgent);

  $.browser.chrome = is_chrome;
  $.browser.safari = $.browser.webkit && !is_chrome;
  $.browser.opera  = is_opera;
  $.browser.msie   = $.browser.msie && !is_opera;
  
}(jQuery));
