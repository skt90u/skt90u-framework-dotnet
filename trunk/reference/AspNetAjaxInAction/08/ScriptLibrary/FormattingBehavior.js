Type.registerNamespace('Samples');

// Samples.FormattingBehavior class.
Samples.FormattingBehavior = function(element) {
    Samples.FormattingBehavior.initializeBase(this, [element]);
    
    // Fields.
    this._hoverCssClass = null;                                               
    this._focusCssClass = null;                                               
    this._currentCssClass = null;                                             
    this._mouseOver = null;                                                   
    this._focus = null;                                                       
}
Samples.FormattingBehavior.prototype = {	
    // Component lifecycle.	
    initialize : function() {                                          
        Samples.FormattingBehavior.callBaseMethod(this, 'initialize'); 
		                                                            
		$addHandlers(this.get_element(),                            
		    {                                                       
		        mouseout:this._onMouseout,                        
		        mouseover:this._onMouseover,                      
		        focus:this._onFocus,                              
		        blur:this._onBlur                                 
		    }, this);                                               
    },                                                                  
                                                                        
    dispose : function() {                                              
        $clearHandlers(this.get_element());                             
                                                                        
        Samples.FormattingBehavior.callBaseMethod(this, 'dispose');     
    },                                                                  
    
    // Event handlers.
    _onMouseover : function() {                                         
		this._mouseOver = true;                                     
		this._setCssClass();                                        
    },                                                                  
                                                                        
    _onMouseout : function() {                                          
		this._mouseOver = false;                                    
		this._setCssClass();                                        
    },                                                                  
                                                                        
    _onFocus : function() {                                             
		this._focus = true;                                         
		this._setCssClass();                                        
    },                                                                  
                                                                        
    _onBlur : function() {                                              
		this._focus = false;                                        
		this._setCssClass();                                        
    },                                                                   
    
    // Helper method.
    _setCssClass : function() {
		if (this._currentCssClass) {
			Sys.UI.DomElement.removeCssClass(this._element,
                      this._currentCssClass);
			this._currentCssClass = null;
		}
		
		if (this._error) { 
            this._currentCssClass = this._errorCssClass; 
        } 
		else if (this._focus) { 
            this._currentCssClass = this._focusCssClass; 
        }
		else if (this._mouseOver) { 
            this._currentCssClass = this._hoverCssClass; 
        }
		
		if (this._currentCssClass) {
			Sys.UI.DomElement.addCssClass(this._element,
                      this._currentCssClass);
            }
    },
    
    // Properties.
    get_hoverCssClass : function() {                             
        return this._hoverCssClass;                              
    },                                                           
                                                                 
    set_hoverCssClass : function(value) {                        
        this._hoverCssClass = value;                             
    },                                                           
                                                                 
    get_focusCssClass : function() {                             
        return this._focusCssClass;                              
    },                                                           
                                                                 
    set_focusCssClass : function(value) {                        
        this._focusCssClass = value;                             
    }                                                            
}
// Inherit from Sys.UI.Behavior.
Samples.FormattingBehavior.registerClass('Samples.FormattingBehavior', Sys.UI.Behavior);
