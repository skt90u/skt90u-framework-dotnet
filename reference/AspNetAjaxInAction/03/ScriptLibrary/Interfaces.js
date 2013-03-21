// Example of an interface declared with the Microsoft Ajax Library.

Type.registerNamespace('Samples');

// Declare the IComparable interface.
Samples.IComparable = function() {                              
    throw Error.notImplemented();                                
}                                                                
Samples.IComparable.prototype = {        
    // Define a single method called compareTo.                       
    compareTo : function(comparand) {                            
        throw Error.notImplemented();                            
    }                                                            
}
// Register Samples.IComparable as an interface.
Samples.IComparable.registerInterface('Samples.IComparable');   

//
// The Temperature class implements the IComparable interface.
Samples.Temperature = function(value) {
    this._value = value;
}
Samples.Temperature.prototype = {
    // Implement the compareTo method.
    compareTo : function(comparand) {
        if(Samples.Temperature.isInstanceOfType(comparand)) {     
            var thisValue = this.get_value();
            var comparandValue = comparand.get_value();
            
            if(thisValue == comparandValue)
                return 0;                                         
                
            return (thisValue > comparandValue) ? 1 : -1;         
        }
        else {
            throw Error.argumentType();                           
        }
    },
    
    get_value : function() {
        return this._value;
    },
    
    set_value : function(value) {
        this._value = value;
    }
}
// The interface type is passed in the call to registerClass.
Samples.Temperature.registerClass('Samples.Temperature', null, Samples.IComparable);                                         

