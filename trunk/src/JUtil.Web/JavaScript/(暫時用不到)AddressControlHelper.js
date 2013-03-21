var AddressControlHelper=(function() {

  // Return the constructor.
  return function(AddressRelativedFields) {

    var relativedFields=AddressRelativedFields;

    var relativedData;

    var AREACODE=JUtil.GetRetailJQueryObject(relativedFields.AREACODE);
    var ADDR_CITY=JUtil.GetAjaxControlToolKitObject(relativedFields.ADDR_CITY);
    var ADDR_AREA=JUtil.GetAjaxControlToolKitObject(relativedFields.ADDR_AREA);
    var ADDRESS=JUtil.GetRetailJQueryObject(relativedFields.ADDRESS);

    if(!AREACODE)
      throw new Error('AREACODE is not exist ['+relativedFields.AREACODE+']');

    if(!ADDR_CITY)
      throw new Error('ADDR_CITY is not exist ['+relativedFields.ADDR_CITY+']');

    if(!ADDR_AREA)
      throw new Error('ADDR_AREA is not exist ['+relativedFields.ADDR_AREA+']');

    if(!ADDRESS)
      throw new Error('ADDRESS is not exist ['+relativedFields.ADDRESS+']');


    this.SetValue=function(AddressRelativedData) {

      relativedData=AddressRelativedData;

      setAddrCity(relativedData.ADDR_CITY);
    };

    this.Cleanup=function() {
      var AddressRelativedData={
        AREACODE: '',
        ADDR_CITY: 0,
        ADDR_AREA: 0,
        ADDRESS: ''
      };

      this.SetValue(AddressRelativedData);
    };

    function setAddrCity(selectedIndex) {

      if(ADDR_CITY.get_element().selectedIndex!=selectedIndex) {
        ADDR_AREA._clearItems();

        ADDR_CITY.get_element().selectedIndex=selectedIndex;

        // raise the drop-down-change event
        ADDR_CITY._onChange();

        // raise the parent-drop-down-change event
        ADDR_AREA._onParentChange(null,true);
      }

      // waiting for tfCM_INV_ADDR_AREA_CascadingDropDown's data has bound.
      waitingDataChanged();
    }

    function setAddrArea(selectedIndex) {
      ADDR_AREA.get_element().selectedIndex=selectedIndex;
      ADDR_AREA._onChange();
    }

    function setAddress(value) {
      ADDRESS.val(value);
    }

    function setAreaCode(value) {
      AREACODE.val(value);
    }

    function waitingDataChanged() {
      if(ADDR_CITY.get_element().selectedIndex!=0&&
         !ADDR_AREA._isPopulated()) {
        // data has not been changed, reset the timer to wait

        // [note : waitingDataChanged is not global function, do not use below statement]
        // setTimeout("waitingDataChanged();", delay); <-- can not work !
        var delay=100;
        setTimeout(waitingDataChanged,delay);
      }
      else {
        setAddrArea(relativedData.ADDR_AREA);
        setAreaCode(relativedData.AREACODE);
        setAddress(relativedData.ADDRESS);
      }
    }

  } // end of constructor

})();