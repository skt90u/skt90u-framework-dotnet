/// <summary>
/// Add a method to the Function object that can be used to declare methods.
/// </summary>
/// <param name="name">public function in a class</param>
/// <param name="fn">function object</param>
/// <remarks>
/// Usage : 
/// var Anim = function() { 
///   ...
/// };
/// - method usage sample
/// Anim.method('start', function() {
///   ...
/// });
///
/// - chained-calling sample
/// Anim.method('start', function() {
///   ...
/// }).method('stop', function() {
///   ...
/// });
/// </remarks>
Function.prototype.method = function(name, fn) {
  this.prototype[name] = fn;
  return this; // return self in order to allow it to be chained-calling
};

Function.prototype.registerClass = function(subClass, superClass) {
  subClass.prototype = new superClass();
  subClass.prototype.constructor = subClass;
  subClass.superclass = superClass.prototype;
  if (superClass.prototype.constructor == Object.prototype.constructor) {
    superClass.prototype.constructor = superClass;
  }
}