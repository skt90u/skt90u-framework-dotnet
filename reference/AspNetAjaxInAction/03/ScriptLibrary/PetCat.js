// Pet and Cat classes created with the Microsoft Ajax Library.

Type.registerNamespace('Samples');                      

// Pet constructor.
Samples.Pet = function(name, age) {                              
    this._name = name;                                    
    this._age = age;                                     
}
// Pet prototype.
Samples.Pet.prototype = {
    // Method.
    speak : function() {                           
        throw Error.notImplemented();              
    },

    // Getter.
    get_name : function() {                             
        return this._name;                              
    },                                                  

    // Setter.
    set_name : function(value) {                        
        this._name = value;                             
    },                                                  

    get_age : function() {                              
        return this._age;                               
    },                                                  

    set_age : function(value) {                        
        if(isNaN(value) || value < 0) {                   
            throw Error.argument('age');                              
        }                                               
        
        this._age = value;                                             
    }                                                   
}
// Register Pet as a class.
Samples.Pet.registerClass('Samples.Pet');

Samples.Cat = function(name, age) {
    Samples.Cat.initializeBase(this, [name, age]);               
}
Samples.Cat.prototype = {
    // Override the speak method.
    speak : function() {                                      
        alert('Meeeeooow');                                   
    }                                                         
}
// The Cat class inherits from Pet.
Samples.Cat.registerClass('Samples.Cat', Samples.Pet);        
