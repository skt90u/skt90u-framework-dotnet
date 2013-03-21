/// <reference path="jquery-1.4.4-vsdoc.js" />

/// <dependance name="jquery-1.4.4.js" />
/// <dependance name="json2.js" />
/// <dependance name="console.js" />

jQuery.WebMethod = function(url, data, success) {
  // url = asmx + '/' + WebMethod;
  // data = { name: 'jelly' };
  // success = function(respond){console.log(respond.d);};
  console.assert(jQuery.type(url) === "string", 'url must be a string');
  console.assert(jQuery.type(data) === "object", 'data must be a object');
  console.assert(jQuery.isFunction(success), 'success is not a function');

  data = JSON.stringify(data);

  jQuery.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    url: url,
    data: data,
    success: success,
    error: onerror
  });

  function onerror(e) {
    var func = '';
    func += 'jQuery.WebMethod(';
    func += url;
    func += ", ";
    func += data;
    func += ", ";
    func += success.toString();
    func += ')';

    console.error(func);
    console.error('responseText = ' + e.responseText);
  }
};

/* 
  var url = 'MyWebService.asmx/HelloWorld';
  var data = { name: 'jelly' };
  var onsuccess = function(respond) { console.log(respond.d); }
  $.WebMethod(url, data, onsuccess);
*/