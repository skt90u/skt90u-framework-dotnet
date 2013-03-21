
// Constructor.

var Interface = function(name, methods) {
    if(arguments.length != 2) {
        throw new Error("Interface constructor called with " + arguments.length
          + "arguments, but expected exactly 2.");
    }
    
    this.name = name;
    this.methods = [];
    for(var i = 0, len = methods.length; i < len; i++) {
        if(typeof methods[i] !== 'string') {
            throw new Error("Interface constructor expects method names to be " 
              + "passed in as a string.");
        }
        this.methods.push(methods[i]);        
    }    
};


// Static class method.

Interface.ensureImplements = function(object) {
  if(arguments.length < 2) {
    throw new Error(
      "Function Interface.ensureImplements called with " + 
      arguments.length  + 
      "arguments, but expected at least 2.");
  }

  for(var i = 1, len = arguments.length; i < len; i++) {
    var theInterface = arguments[i];
    
    if(theInterface.constructor !== Interface) {
      throw new Error(
        "Function Interface.ensureImplements expects arguments " + 
        "two and above to be instances of Interface.");
    }
    
          
    for(var j = 0, methodsLen = theInterface.methods.length; j < methodsLen; j++) {
      var method = theInterface.methods[j];
    
      if(!object[method] || typeof object[method] !== 'function') {
        throw new Error(
          "Function Interface.ensureImplements: object " + 
          "does not implement the " + 
          theInterface.name + 
          " theInterface. Method " + 
          method + 
          " was not found.");
      }
    }
  } // for(var i = 1, len = arguments.length; i < len; i++) {
}; // Interface.ensureImplements = function(object) {
