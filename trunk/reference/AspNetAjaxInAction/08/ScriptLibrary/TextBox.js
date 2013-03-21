Type.registerNamespace('Samples');

// Samples.TextBox class.
Samples.TextBox = function(element) {
    Samples.TextBox.initializeBase(this, [element]);
    
    this._ignoreEnterKey = false;                                           
}
Samples.TextBox.prototype = {
    // Component lifecycle.
    initialize : function() {                                       
        Samples.TextBox.callBaseMethod(this, 'initialize');         
                                                                    
        // Subscribe to the keypress event.                         
        $addHandlers(this.get_element(),                            
            {keypress:this._onKeyPress}, this);                   
    },                                                              
                                                                    
    dispose : function() {                                          
        // Detach event handlers.                                   
        $clearHandlers(this.get_element());                         
                                                                    
        Samples.TextBox.callBaseMethod(this, 'dispose');            
    },                                                              
    
    // Handle the keypress event.                                   
    _onKeyPress : function(evt) {                                   
        if(this._ignoreEnterKey && evt.charCode == 13) {            
            evt.preventDefault();                                   
        }                                                           
    },                                                              
    
    // Properties.
    get_ignoreEnterKey : function() {                               
        return this._ignoreEnterKey;                                
    },                                                              
                                                                    
    set_ignoreEnterKey : function(value) {                          
        this._ignoreEnterKey = value;                               
    }                                                               
}
Samples.TextBox.registerClass('Samples.TextBox', Sys.UI.Control);

