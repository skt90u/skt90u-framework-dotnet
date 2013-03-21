(function() {

    var userAgent = navigator.userAgent;
    var is_chrome = /chrome/.test(userAgent.toLowerCase());
    var is_opera = /opera/.test(userAgent);

    /******************************************
    *	static public functions
    ******************************************/
    var JUtil = {
        GetControlValue: function($el) {
            var IAspNetControlHelper = AspNetControlHelperManager.GetHelper($el);
            return IAspNetControlHelper.GetValue($el);
        },

        SetControlValue: function($el, value) {
            var IAspNetControlHelper = AspNetControlHelperManager.GetHelper($el);
            IAspNetControlHelper.SetValue($el, value);
        },

        GetAjaxControlToolKitObject: function(fieldName) {
            return $find(fieldName);
        },

        GetRetailJQueryObject: function(fieldName) {

            return $(getSelector(fieldName));

            function getSelector(fieldName) {
                var attribute = "ClientID";
                var selector = "[" + attribute + "='" + fieldName + "']:first";
                return selector;
            }
        },

        IsEmptyString: function(str) {
            // http://mohamedfaramawi.blogspot.com/2007/12/checking-if-string-is-empty-with.html
            var trimString = str.replace(/^\s+|\s+$/, '');
            return trimString.length == 0;
        }

    }; // var JUtil = {





    // Expose JUtil to the global object
    window.JUtil = JUtil;

})(window);