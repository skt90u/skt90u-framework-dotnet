Type.registerNamespace('Samples');

// ConfirmPostBackAction class.
Samples.ConfirmPostBackAction = function() {
    this._message;
}
Samples.ConfirmPostBackAction.prototype = {
    get_message : function() {
        return this._message;
    },
    
    set_message : function(value) {
        this._message = value;
    },
    
    // Override performAction.
    performAction : function() {
        if(window.confirm(this._message)) {
            return Samples.ConfirmPostBackAction.callBaseMethod(this, 'performAction');
        }
    }
}
// Class descriptor.
Samples.ConfirmPostBackAction.descriptor = {
    properties: [   {name: 'message', type: String} ]
}
// Inherit from Sys.Preview.PostBackAction.
Samples.ConfirmPostBackAction.registerClass('Samples.ConfirmPostBackAction', Sys.Preview.PostBackAction);
