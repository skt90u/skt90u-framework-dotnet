(function(win) {
  var DEBUG = true;

  if (DEBUG && !!win.console) return;

  var func = DEBUG ? show : noop;
  var console = {};
  var members = ['log', 'info', 'warn', 'error', 'assert'];
  for (var i in members) {
    var member = members[i];
    console[member] = func;
  }
  console.clear = noop;
  win.console = console;  
  
  function show() {
    var expr = arguments[1] ? !arguments[0] : true;
    var info = arguments[1] ? arguments[1] : arguments[0];
    if (expr) alert(info);
  }
  function noop() { }
})(window);

/*
console.log('log');
console.info('info');
console.warn('warn');
console.error('error');
console.assert(true, 'true assert');
console.assert(false, 'show assert');
*/