Type.registerNamespace('Samples');

// Samples.Customer class.
Samples.Customer = function() {
    Samples.Customer.initializeBase(this);

    this._fullName;
}
Samples.Customer.prototype = {
    get_fullName : function() {
        return this._fullName;
    },

    set_fullName : function(value) {
        if(value != this._fullName) {
            this._fullName = value;

            // Raise the propertyChanged event.
            this.raisePropertyChanged('fullName');                 
        }
    }
}
Samples.Customer.registerClass('Samples.Customer', Sys.Component);
