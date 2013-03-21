(function() {

  var SOFT_BREAK = '\0';
  
  /******************************************
   *	static public functions
   ******************************************/
  var Input = {
    IsDigit: function(char) {
      var charCode = char.charCodeAt(0);
      return (47 < charCode && charCode < 58);
    },
    
    IsAlpha: function(char) {      var charCode = char.charCodeAt(0);      return (charCode > 64 && charCode <  91) ||              (charCode > 96 && charCode < 123);    },        /*    IsBreak: function(ch, isLast) {      return ch == SOFT_BREAK ||              ( !IsAlpha.IsAlnum(ch) &&               (!this.m_lastLetterFilter ||                 this.m_lastLetterFilter.IsValid(ch))             );    }    */
     
  }; // var Input = {

  // Expose Input to the global object
  window.Input = Input;
  
})(window);