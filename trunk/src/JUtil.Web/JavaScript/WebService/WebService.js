(function() {

    var WebService = function(asmx) {
        // private fields
        this.asmx = asmx;

        // private function
        this.jsonToString = function(obj) {
            var THIS = this;
            switch (typeof (obj)) {
                case 'string':
                    return '"' + obj.replace(/(["\\])/g, '\\$1') + '"';
                case 'array':
                    return '[' + obj.map(THIS.jsonToString).join(',') + ']';
                case 'object':
                    if (obj instanceof Array) {
                        var strArr = [];
                        var len = obj.length;
                        for (var i = 0; i < len; i++) {
                            strArr.push(THIS.jsonToString(obj[i]));
                        }
                        return '[' + strArr.join(',') + ']';
                    } else if (obj == null) {
                        return 'null';

                    } else {
                        var string = [];
                        for (var property in obj) string.push(THIS.jsonToString(property) + ':' + THIS.jsonToString(obj[property]));
                        return '{' + string.join(',') + '}';
                    }
                case 'number':
                    return obj;
                case false:
                    return obj;
            }
        };
    };

    WebService.defaults = {
        type: "POST"
       , contentType: "application/json; charset=utf-8"
       , dataType: "json"
    };


    WebService.prototype =
    {
        // call(method, data)
        // call(method, data, success)
        // call(method, data, options, success)
        // call(method, data, options, success, error)
        call: function(method, data, options, success, error) {

            if (arguments.length < 2 || 5 < arguments.length) {
                throw "呼叫參數數目不正確";
            }

            switch (arguments.length) {
                //call(method, data)                              
                case 2:
                    {
                        options = $.extend({}, WebService.defaults, options);
                        success = success || default_success;
                        error = error || default_error;
                    } break;

                // call(method, data, success)                              
                case 3:
                    {
                        if (false == $.isFunction(options)) {
                            throw "呼叫方式不正確，正確格式為call(method, data, success)";
                        }
                        success = options;
                        options = $.extend({}, WebService.defaults, options);
                        error = error || default_error;
                    } break;

                default:
                    {
                        options = $.extend({}, WebService.defaults, options);
                        success = success || default_success;
                        error = error || default_error;
                    } break;
            }

            url = this.asmx + "/" + method;
            data = this.jsonToString(data);

            $.ajax({
                type: options.type,
                async: options.async,
                contentType: options.contentType,
                dataType: options.dataType,
                url: url,
                data: data,
                success: success,
                error: error
            });
        }
    }

    window.WebService = WebService;

    function default_success(data, textStatus, jqXHR) {
        // 有再修改的空間
        var respond = data.d;
        respond = respond.toString();
        Log.D(respond);
    }

    function default_error(jqXHR, textStatus, errorThrown) {

        var message = jqXHR.responseText;

        if (message.length == 0)
            message = "Unknown error";

        var popUp = window.open('', '');

        $(popUp.document).ready(writeToDocument);

        function writeToDocument() {
            popUp.document.open();
            popUp.document.write(message);
            popUp.document.close();
        }
    }


})(window);
