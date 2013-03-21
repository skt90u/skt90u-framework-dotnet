Type.registerNamespace('Samples');

// AlertAction class.
Samples.AlertAction = function() {
    this._message;
}
Samples.AlertAction.prototype = {
    get_message : function() {
        return this._message;
    },
    
    set_message : function(value) {
        this._message = value;
    },
    
    // Override performAction.
    performAction : function() {
        return alert(this._message);
    }
}
// Class descriptor.
Samples.AlertAction.descriptor = {
    properties: [   {name: 'message', type: String} ]
}
Samples.AlertAction.registerClass('Samples.AlertAction', Sys.Preview.Action); // Inherit from Sys.Action
