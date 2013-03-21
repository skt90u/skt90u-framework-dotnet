Type.prototype.createProperty = function(fieldName, propName) { // Consider adding a raiseNotification parameter.
    var e = Function._validateParams(arguments,
        [
            {name:"fieldName", type:String, mayBeNull:false, optional:false},
            {name:"propName", type:String, mayBeNull:false, optional:false}
        ]
    );
    if(e) throw e;
    
    var getter = function() {
        return this[fieldName];
    }
    
    var setter = function(value) {
        this[fieldName] = value;
    }
    
    this.prototype["get_" + propName] = getter;
    this.prototype["set_" + propName] = setter;
}

Type.prototype.createEvent = function(eventName) 
{
    var e = Function._validateParams(arguments,
        [
            {name:"eventName", type:String, mayBeNull:false, optional:false}
        ]
    );
    if(e) throw e;
           
    var addHandler = function(handler) {
        this.get_events().addHandler(eventName, handler);
    }
    
    var removeHandler = function(handler) {
        this.get_events().removeHandler(eventName, handler);
    }

    this.prototype['add_' + eventName] = addHandler;  
    this.prototype['remove_' + eventName] = removeHandler;        

    if(!this.__events) {    
        if(!this.inheritsFrom(Sys.Component)) {          
            this.prototype.get_events = function() {            
                if (!this._events) {                
                    this._events = new Sys.EventHandlerList();            
                }            

                return this._events;        
             }   
         }             

         this.prototype._raiseEvent = function(eventName, eventArgs) {            
            var handler = this.get_events().getHandler(eventName);                            

            if (handler) {                     
                if (!eventArgs) {                            
                    eventArgs = Sys.EventArgs.Empty;                     
                }                     

                handler(this, eventArgs);                
            }        
         }                

         this.__events = true;    
    }
}

if(typeof(Sys) !== "undefined")
    Sys.Application.notifyScriptLoaded();
