Type.registerNamespace('Samples');                                  

// Samples.TrivialComponent class.
Samples.TrivialComponent = function() {
    Samples.TrivialComponent.initializeBase(this);
}
Samples.TrivialComponent.prototype = {
    // Component lifecycle.
    //
    // Override the initialize method.
    initialize : function() {                                         
        Samples.TrivialComponent.callBaseMethod(this, 'initialize');  
                                                                      
        alert("I've been initialized!");                              
    },
    
    // Override the dispose method.
    dispose : function() {                                            
        alert("I’m being disposed!");                                 
                                                                      
        Samples.TrivialComponent.callBaseMethod(this, 'dispose');     
    },

    // A custom method.
    greet : function() {
        alert("Hello, I'm your first component!");                  
    }
}
// Inherit from Sys.Component.
Samples.TrivialComponent.registerClass('Samples.TrivialComponent', Sys.Component);                                                 
