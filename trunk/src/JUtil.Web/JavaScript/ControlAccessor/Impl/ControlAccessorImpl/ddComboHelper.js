(function() {

var ddComboHelper = function() {}

/// <summary>
/// Implement GetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
ddComboHelper.prototype.GetValue=function($el) {
  //return GetDdcomboInput($el).val();
  var $input = GetDdcomboInput($el);
  var value = $input.val();
  return valueToKey($input, value);
  
  function valueToKey($input, value) {
    
    var $caches=$input.parents("table[class='ddcombo_table']").next().find('option');
    
    var key='';
    
    $caches.each(function() {
      if($(this).text()==value) {
        key=$(this).val();
      }
    });
    
    return key;
  }
};

/// <summary>
/// Implement SetValue function of IAspNetControlHelper interface
/// </summary>
/// <param name="$el">a jQuery object</param>
ddComboHelper.prototype.SetValue=function($el,value) {
  GetDdcomboInput($el).val(value);
};

ddComboHelper.prototype.IsMatchType=function($el) {
  /*
  <div id="ctl00_ContentPlaceHolder1_ST_ID" class="ddcombo">
		<table id="ctl00_ContentPlaceHolder1_ST_ID_combotable" class="ddcombo_table" cellspacing="0" cellpadding="0" border="0" border="0">
			<tr>
				<td class="ddcombo_td1"><div class="ddcombo_div4" style="background: url(/WebResource.axd?d=c7XrEKiDr_xRnQ--pyXWPgNOOvXDJIbUXvmPB74HmjPtP-I-1L1oEREgP6-nMt3qdgtT9QcSHJa9KbljrzxkvA2&t=634409264039687500)">
					<input name="ctl00$ContentPlaceHolder1$ST_ID$combotable_input" type="text" id="ctl00_ContentPlaceHolder1_ST_ID_combotable_input" class="ddcombo_input1" title="請輸入" value="請輸入" style="color: gray; background: url(/WebResource.axd?d=c7XrEKiDr_xRnQ--pyXWPgNOOvXDJIbUXvmPB74HmjPtP-I-1L1oEREgP6-nMt3qdgtT9QcSHJa9KbljrzxkvA2&t=634409264039687500)" />
				</div></td><td id="ctl00_ContentPlaceHolder1_ST_ID_combotable_button" class="ddcombo_td2" valign="top" align="left"><a></a><img src="/WebResource.axd?d=c7XrEKiDr_xRnQ--pyXWPgNOOvXDJIbUXvmPB74HmjPcDESoOz5p_TX9xy4g18g60&amp;t=634409264039687500" src="" style="border-width:0px;display: none;" /></td>
			</tr>
		</table>
		<select name="ctl00$ContentPlaceHolder1$ST_ID$ctl03" id="ctl00_ContentPlaceHolder1_ST_ID_ctl03" style="display: none;">
			<option value="ST0001">ST0001-內湖旗艦館</option>
			<option value="ST0002">ST0002-花蓮門市</option>
			<option value="ST0003">ST0003-板橋門市</option>
		</select>
	</div>
  */
  return $el.hasClass('ddcombo');
};

function GetDdcomboInput($el)
{
  var id=$el.attr('id');
  var $input=$('#'+id+' input[type=text]');
  return $input;
}

AspNetControlHelperManager.Register(new ddComboHelper());

})();
