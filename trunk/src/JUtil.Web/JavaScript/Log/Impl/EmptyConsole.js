/*global Log: true, console: true, window: true, jQuery: true, alert: true */

(function($) {

  var logInstance;

  function GetLogInstance() {
    if (!logInstance) {
      logInstance = log4javascript.getDefaultLogger();
    }

    if (!logInstance) {
      throw new Error("You must load log4javascript.js before.");
    }

    return logInstance;
  }

  var EmptyConsole = function() { };

  EmptyConsole.prototype.D = function() {
    var message = ($.sprintf).apply(null, arguments);
    message = $.sprintf("[DEBUG] %s", message);
    GetLogInstance().debug(message);
  };

  EmptyConsole.prototype.I = function() {
    var message = ($.sprintf).apply(null, arguments);
    message = $.sprintf("[INFO] %s", message);
    GetLogInstance().info(message);
  };

  EmptyConsole.prototype.W = function() {
    var message = ($.sprintf).apply(null, arguments);
    message = $.sprintf("[WARN] %s", message);
    GetLogInstance().warn(message);
  };

  EmptyConsole.prototype.E = function() {
    var message = ($.sprintf).apply(null, arguments);
    message = $.sprintf("[ERROR] %s", message);
    GetLogInstance().error(message);
  };

  EmptyConsole.prototype.IsBrowserMatch = function() {
    return true;
  };

  Log.Register(new EmptyConsole());


} (jQuery));
