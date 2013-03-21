(function () {

  var ILog = new Interface(
    'ILog',
    [
	 'D' /* DEBUG */,
     'I' /* INFO  */,
     'W' /* WARN  */,
     'E' /* ERROR */,
     'IsBrowserMatch'
	 ]
  );
  
  var loggers = [];
  
  var logInstance;
  
  function GetLogInstance()
  {
    if(!logInstance)
    {
      for(var i=0,len=loggers.length;i<len;i++) {
        var logger = loggers[i];

        if(logger.IsBrowserMatch()) {
          logInstance = logger;
          break;
        }
      }

      if(!logInstance) {
        throw new Error("There is no suitable logger for current browser.");
      }
    }
    
    return logInstance;
  }
  
  /******************************************
   *	static public functions
   ******************************************/
  var Log = {
    
    Register : function(loggerInstance){
      
      Interface.ensureImplements(loggerInstance, ILog);
      
      loggers.push(loggerInstance);
    },

    D: function(format) {
      (GetLogInstance().D).apply(null,arguments);
    },
    
    I: function(format) {
      (GetLogInstance().I).apply(null,arguments);
    },
    
    W: function(format) {
      (GetLogInstance().W).apply(null,arguments);
    },
    
    E: function(format) {
      (GetLogInstance().E).apply(null,arguments);
    }
    
  };
  
  // Expose Log to the global object
  window.Log = Log;
  
})(window);
