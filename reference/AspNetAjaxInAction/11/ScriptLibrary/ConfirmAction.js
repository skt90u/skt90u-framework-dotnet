Type.registerNamespace('Samples');

Samples.ConfirmAction = function() {
    this._message;
}
Samples.ConfirmAction.prototype = {
    get_message : function() {
        return this._message;
    },
    
    set_message : function(value) {
        this._message = value;
    },
    
    performAction : function() {
        return window.confirm(this._message);
    }
}
Samples.ConfirmAction.descriptor = {
    properties: [   {name: 'message', type: String} ]
}
Samples.ConfirmAction.registerClass('Samples.ConfirmAction', Sys.Preview.Action);

if(typeof(Sys) !== 'undefined')
    Sys.Application.notifyScriptLoaded();