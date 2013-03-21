// Example of event exposed with the Microsoft Ajax Library.

Type.registerNamespace('Samples');

Samples.Collection = function() {
    this._innerList = [];
    
    // Store an instance of Sys.EventHandlerList.
    this._events = null;                      
}
Samples.Collection.prototype = {
    // Add an item to the collection.
    add : function(member) {
        this._innerList.push(member);
        
        // Raise the itemAdded event.
        this._raiseEvent('itemAdded', Sys.EventArgs.Empty);
    },
    
    // Return the Sys.EventHandlerList instance.
    get_events : function() {
        if(!this._events) {
            this._events = new Sys.EventHandlerList();
        }
        
        return this._events;
    },

    // Add an handler for the itemAdded event.
    add_itemAdded : function(handler) {
        this.get_events().addHandler('itemAdded', handler);         
    },
    
    // Remove an handler for the itemAdded event.
    remove_itemAdded : function(handler) {
        this.get_events().removeHandler('itemAdded', handler);      
    },
                
    // Generic function to raise an event.
    _raiseEvent : function(eventName, eventArgs) {
        var handler = this.get_events().getHandler(eventName);      
                                                                         
        if (handler) {                                           
            if (!eventArgs) {                                    
                eventArgs = Sys.EventArgs.Empty;                 
            }                                                    
            handler(this, eventArgs);                               
        }      
    }
}
Samples.Collection.registerClass('Samples.Collection');

