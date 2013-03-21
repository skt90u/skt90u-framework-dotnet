// Example of an enumeration defined with the Microsoft Ajax Library.

Type.registerNamespace('Samples');

// Declare the Samples.Size enumeration.
Samples.Size = function() {
    throw Error.notImplemented();                          
}
// The prototype holds the enumeration's names and values.
Samples.Size.prototype = {                                 
    Small: 1,                                              
    Medium: 2,                                             
    Large: 3                                               
}
// Register Samples.Size as an enumeration.
Samples.Size.registerEnum('Samples.Size');                 

// FileAccess is an enumeration that uses flags.
Samples.FileAccess = function() {
    throw Error.notImplemented();
}
// Values in flags mode must be powers of 2.
Samples.FileAccess.prototype = {
    Read : 1,                                         
    Write : 2,                                        
    Execute : 4                                       
}
// Register Samples.FileAccess as an enumeration. The Boolean value 
// specifies whether the flags mode is enabled.
Samples.FileAccess.registerEnum('Samples.FileAccess', true);

