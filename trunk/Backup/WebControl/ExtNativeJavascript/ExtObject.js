/// <summary>
/// clone an object
/// </summary>
Object.prototype.clone = function() {
  function F() { }
  F.prototype = this;
  return new F;
};

/// <summary>
/// object comparsion
/// </summary>
/// <param name="x">another object</param>
/// <remarks>
/// Usage : obj1.equals(obj2)
/// </remarks>
Object.prototype.equals = function(x) {
  for (p in this) if (typeof (x[p]) == 'undefined') return false;

  for (p in this) {
    // 加上 typeof this[p] == 'string'是避免空字串, 被當成 undefined or null
    if (this[p] || typeof this[p] == 'string') {
      switch (typeof (this[p])) {
        case 'object':
          if (!this[p].equals(x[p]))return false;
          break;
        case 'function':
          if (typeof (x[p]) == 'undefined' || (p != 'equals' && this[p].toString() != x[p].toString())) return false;
          break;
        default:
          if (this[p] != x[p]) return false;
          break;
      }
    } else {
      if (x[p]) return false;
    }
  }

  for (p in x) if (typeof (this[p]) == 'undefined') return false;

  return true;
}
