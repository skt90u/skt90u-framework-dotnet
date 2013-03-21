(function() {
  
  var IAspNetControlHelper = new Interface(
                                'IAspNetControlHelper', 
                                ['GetValue', 'SetValue', 'IsMatchType']);
  
  var helpers = [];
  
  /******************************************
   *	static public functions
   ******************************************/
  var AspNetControlHelperManager = {
    // IAspNetControlHelper
    Register : function(helperInstance){
      
      Interface.ensureImplements(helperInstance, IAspNetControlHelper);
      
      helpers.push(helperInstance);
    },
    
    GetHelper : function($el) {

      var isJQuery = $el instanceof jQuery;
      if (!isJQuery) {
        throw new Error("AspNetControlHelperManager.GetHelper: only accept jQuery Object.");
      }

      if ($el.length === 0) {
        throw new Error("AspNetControlHelperManager.GetHelper: there is no element in jQuery Object.");
      }
      
      for(var i=0,len=helpers.length; i<len; i++) 
      {
        var helper = helpers[i];
        
        if(helper.IsMatchType($el)) 
          return helper;
      }

      throw new Error("There is no IAspNetControlHelper can handle " + 
                       $el.attr('id'));
    }
  };
  
  // Expose AspNetControlHelperManager to the global object
  window.AspNetControlHelperManager = AspNetControlHelperManager;
  
})(window);
