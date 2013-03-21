(function($) {

  //============================================================
  //                      Extension                           
  //============================================================
  function objCompare(lhs, rhs, arrExclusion) {
    //console.log('objCompare(' + JSON.stringify(lhs) + ', ' + JSON.stringify(rhs) + ', ' + JSON.stringify(arrExclusion) + ')')

    for (p in lhs) {
      if (typeof (rhs[p]) == 'undefined') {
        //console.log('objCompare.1');
        return false;
      }
    }
    for (p in lhs) {
      // 加上 typeof lhs[p] == 'string', 是避免空字串, 被當成 undefined or null
      if (lhs[p] || typeof lhs[p] == 'string') {
        switch (typeof (lhs[p])) {
          case 'object':
            if (!objCompare(lhs[p], rhs[p], arrExclusion)) {
              //console.log('objCompare.2');
              return false;
            }
            break;
          case 'function':
            if (typeof (rhs[p]) == 'undefined' || (p != 'objCompare' && lhs[p].toString() != rhs[p].toString())) {
              //console.log('objCompare.3');
              return false;
            }
            break;
          default:
            if (lhs[p] != rhs[p]) {
              if (!isException(p)) {
                //console.log('objCompare.4');
                return false;
              }

              // conditional comparision
              // if the unequal attribute is inside arrExclusion
              // then ignore it !
              function isException(p) {
                for (var i = 0; arrExclusion && i < arrExclusion.length; i++) {
                  if (arrExclusion[i] == p) {
                    //console.log('objCompare.5');
                    return true;
                  }
                }
                //console.log('objCompare.6');
                return false;
              }
            } // if (lhs[p] != rhs[p])
            break;
        }
      } else {
        if (rhs[p]) {
          //console.log('objCompare.7');
          return false;
        }
      }
    }
    for (p in rhs) {
      if (typeof (lhs[p]) == 'undefined') {
        //console.log('objCompare.8');
        return false;
      }
    }
    //console.log('objCompare.9');
    return true;
  } // function objCompare(lhs, rhs, arrExclusion)

  /// <summary>
  /// Array remvoe
  /// </summary>
  /// <param name="obj">object you want to remove</param>
  /// <param name="arrExclusion"> attribute exclusion</param>
  /// <remarks>
  /// Usage remvoe            : arr1.equals(obj)
  /// Usage condtional remvoe : arr1.equals(obj, ['attr1'])
  /// </remarks>
  Array.prototype.remove = function(obj, arrExclusion) {
    return (function(arr, obj, arrExclusion) {
      var size = 0;
      for (var i = 0, n = 0; i < arr.length; i++) {
        var equal = null;
        var isObj = typeof obj === "object";
        if (isObj) {
          equal = objCompare(arr[i], obj, arrExclusion);
        }
        else {
          equal = arr[i] == obj;
        }
        if (!equal) {
          arr[n++] = arr[i];
          size++;
        }
      }
      arr.length = size;
    })(this, obj, arrExclusion);
  };

  /// <summary>
  /// remove all element in array
  /// </summary>
  /// <remarks>
  /// reference : http://www.joeyjavas.com/2007/08/10/how-to-remove-all-elements-from-a-javascript-array/
  /// </remarks>  
  Array.prototype.clear = function() {
    return (function(arr) {
      arr.length = 0;
    })(this);
  };

  $.fn.InfoGridview = function(options) {

    //============================================================
    //                         CONSTANTS                        
    //============================================================
    var selector = {
      rows: 'tr[rowtype=datarow]',
      cols: 'td[column]',
      col: 'td[column=field]',
      field: 'span:first, input[type=text]:first',
      numEdit: '.number',
      dateEdit: '.date',
      optionBtn: 'input[type=checkbox], input[type=radio]'
    };

    var defaults = {
      // DataKeyNames a string has javascript array format like ['F1', 'F2']
      // indicate which column is primary key
      DataKeyNames: null,

      // HiddenKey is a name of hidden field,
      // This hidden field is a bridge between client and server to
      // understand what data we selected
      HiddenKey: null
    };
    var options = $.extend(defaults, options);

    // --------------------------------------------------
    // Initialize global variables
    // --------------------------------------------------
    var $grv = $(this);
    var $rows = $grv.find(selector.rows);

    // all numEdit in gridview
    var $numEdit = $grv.find(selector.numEdit);
    // all numEdit in gridview
    var $dateEdit = $grv.find(selector.dateEdit);
    // all numEdit in gridview
    var $optionBtn = $grv.find(selector.optionBtn);
    // $numEdit, $dateEdit, $optionBtn is used for add event handlers on themself


    // $buffer is a hidden field for server side read what data we select
    var $buffer = $('#' + options.HiddenKey);
    // arrSelectedData is an array contain selected data (objects)
    var arrSelectedData = $.parseJSON($buffer.val());
    // once we change state or value in grid, we must to manage
    // arrSelectedData and $buffer

    // for more convenient, we transform DataKeyNames to an array(arrPrimaryKey)
    var arrPrimaryKey = $.parseJSON(options.DataKeyNames);
    // arrColumnName is a set of fields's name, but excluding primary keys
    var arrColumnName = (function() {
      // an array for keep all columns name but excluding primary key
      var arr = [];
      if ($rows.length == 0) return arr;
      var $cols = $rows.first().find(selector.cols);
      $cols.each(function() {
        var column = $(this).attr('column');
        for (var i = 0; arrPrimaryKey && i < arrPrimaryKey.length; i++) {
          if (arrPrimaryKey[i] != column)
            arr.push(column);
        }
      });
      return arr;
    })();

    // According to hidden value that keep selected data,
    // and restore its value and state.
    $rows.each(function() {
      var $row = $(this);
      var i = matchSelectedData($row);
      var $optionBtn = $row.find(selector.optionBtn);
      if (0 <= i && i <= arrSelectedData.length - 1) {
        var selecteddata = arrSelectedData[i];
        $.each(selecteddata, function(key, value) {
          setFieldValue($row, key, value);
          setChecked($optionBtn, true);
        });
      }
      else {
        setChecked($optionBtn, false);
      }
    });

    BindEvents();


    //======================================================================

    /// <summary>
    /// register event handlers
    /// </summary>
    /// <remarks>
    ///  - optionBtn : click event
    ///  - numEdit   : keyup event
    ///  - dateEdit  : change event
    /// </remarks>
    function BindEvents() {

      // --------------------------------------------------
      // attach click event handler to option button
      // --------------------------------------------------
      $optionBtn.bind('click', function() {
        var self = $(this);
        $currentrow = getCurrentRow(self);
        rowData = getRowData($currentrow);
        self.is(':radio') ? clickRadio() : clickCheckBox();
        updateBuffer();

        function clickRadio() {
          setChecked($optionBtn, false);
          setChecked(self, true);
          arrSelectedData.clear();
          arrSelectedData.push(rowData);
        }

        function clickCheckBox() {
          var checked = isCheckedRow(self);
          if (checked) {
            //console.log('before push:' + arrSelectedData.length);
            //console.log('push       :' + JSON.stringify(rowData));
            arrSelectedData.push(rowData);
            //console.log('after  push:' + arrSelectedData.length);
            //console.log('arrSelectedData:' + JSON.stringify(arrSelectedData););
          }
          else {
            //console.log('before remove:' + arrSelectedData.length);
            //console.log('remove       :' + JSON.stringify(rowData));
            arrSelectedData.remove(rowData, arrColumnName);
            //console.log('after  remove:' + arrSelectedData.length);
            //console.log('arrSelectedData:' + JSON.stringify(arrSelectedData));
          }
        }
      });

      // --------------------------------------------------
      // attach keyup event handler to numEdit
      // --------------------------------------------------
      $numEdit.bind('keyup', function(event) {
        if (validInput(event)) {
          updateSelectedData($(this));
        }
      });

      // --------------------------------------------------
      // attach change event handler to dateEdit
      // --------------------------------------------------
      $dateEdit.bind('change', function() {
        updateSelectedData($(this));
      });

    } // function BindEvents()

    /// <summary>
    /// get field's value according to specified field name in a row
    /// </summary>
    /// <param name="$row" type="jquery-object">specified row</param>
    /// <param name="field" type="string">field name</param>
    function getFieldValue($row, field) {
      $col = (field === undefined) ? $row : $row.find((selector.col).replace('field', field));
      return getvalue($col);

      function getvalue($col) {
        var val;
        $col.find(selector.field).each(function() {
          var $el = $(this);
          if (val = $el.val() || $el.text())
            return false;
        });
        return val;
      }
    }

    /// <summary>
    /// get field's name in current column
    /// </summary>
    /// <param name="$col" type="jquery-object">current column</param>
    function getKey($col) {
      return $col.attr('column');
    }

    /// <summary>
    /// set field's value according to specified field name in a row
    /// </summary>
    /// <param name="$row" type="jquery-object">specified row</param>
    /// <param name="field" type="string">field name</param>
    /// <param name="value" type="string">new field value</param>
    function setFieldValue($row, field, value) {
      $col = (value === undefined) ? $row : $row.find((selector.col).replace('field', field));
      value = (value === undefined) ? field : value;
      setvalue($col, value);

      function setvalue($col, value) {
        $col.find(selector.field).each(function() {
          var $el = $(this);
          if ($el.is('span')) $el.text(value);
          if ($el.is('input[type=text]')) $el.val(value);
        });
      }
    }

    /// <summary>
    /// set checkbox or radiobutton checkd / uncheckd 
    /// </summary>
    /// <param name="$optionBtn" type="jquery-object">optionButton</param>
    /// <param name="checked" type="boolean"></param>
    /// <remarks></remarks>
    function setChecked($optionBtn, checked) {
      $optionBtn.attr('checked', checked);
    }

    /// <summary>
    /// tell if event.Key whether change value or not
    /// </summary>
    /// <param name="event">eventObject</param>
    /// <remarks></remarks>
    function validInput(event) {
      // event.keyCode | ascii code
      // --------------------------
      // 48 ~ 57       | '0' ~ '9'
      // 8             | backspace
      // 46            | delete            
      var key = event.keyCode;
      return (48 <= key && key <= 57 || key == 8 || key == 46);
    }

    /// <summary>
    /// determine current row's data is selected data
    /// </summary>
    /// <param name="$row" type="jquery-object"></param>
    /// <remarks></remarks>
    function matchSelectedData($row) {
      if (!arrSelectedData || arrSelectedData.length == 0)
        return -1;
      for (var i = 0; i < arrSelectedData.length; i++) {
        var selecteddata = arrSelectedData[i];
        var match = true;
        for (var j = 0; j < arrPrimaryKey.length; j++) {
          var key = arrPrimaryKey[j];
          if (selecteddata[key] !== getFieldValue($row, key)) {
            match = false;
          }
        }
        if (match)
          return i;
      }
      return -1;
    }

    /// <summary>
    /// determine a sibling option button is checked or not
    /// </summary>
    /// <param name="$el">a jquery object indicate a element inside table cell</param>
    /// <remarks></remarks>
    function isCheckedRow($el) {
      var $row = getCurrentRow($el);
      var $option = $row.find(selector.optionBtn);
      return $option.is(':checked');
    }

    /// <summary>
    /// according to a element in table cell to get its parent's table row
    /// </summary>
    /// <param name="$el">a jquery object indicate a element inside table cell</param>
    /// <remarks></remarks>
    function getCurrentRow($el) {
      var $row = $el.parent('td').parent('tr');
      return $row;
    }

    /// <summary>
    /// get all table cell's data in a table row
    /// </summary>
    /// <param name="$row">a jquery object indicate a table row</param>
    function getRowData($row) {
      var $cols = $row.find(selector.cols);
      var rowdata = {};
      $cols.each(function() {
        $col = $(this);
        var key = getKey($col);
        var value = getFieldValue($col);
        rowdata[key] = value;
      });
      return rowdata;
    }

    /// <summary>
    /// refresh hidden value that present selectedData
    /// </summary>
    function updateBuffer() {
      var jsonStr = JSON.stringify(arrSelectedData);
      $buffer.val(jsonStr);
    }

    /// <summary>
    /// if element's value is change, update relative structure
    /// </summary>
    /// <param name="$el">a jquery object indicate a element inside table cell</param>
    /// <remarks></remarks>
    function updateSelectedData($el) {
      var checked = isCheckedRow($el);
      if (!checked) {
        return; // is checked row; if no, ignore it
      }
      else {
        var $currentrow = getCurrentRow($el);
        var rowData = getRowData($currentrow);
        arrSelectedData.remove(rowData, arrColumnName);
        arrSelectedData.push(rowData);
        updateBuffer();
      }
    } // function updateSelectedData($el)
  }; // end of $.fn.InfoGridview

})(jQuery);

/// <summary>
/// fix IE bug
/// in IE, if we set dateInput readonly, then after we change the date,
/// we can not recieve onchange event
/// </summary>
/// <param name="sender"></param>
/// <remarks></remarks>
function OnDateSelectionChanged(sender) {
  var $dateInput = $(sender._textbox._element);
  $dateInput.change();
}