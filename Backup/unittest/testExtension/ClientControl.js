/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("testExtension");

testExtension.ClientControl = function(element) {
    testExtension.ClientControl.initializeBase(this, [element]);
}

testExtension.ClientControl.prototype = {
    initialize: function() {
        testExtension.ClientControl.callBaseMethod(this, 'initialize');
        
        // 在此加入自訂初始設定
    },
    dispose: function() {        
        //在此加入自訂處置動作
        testExtension.ClientControl.callBaseMethod(this, 'dispose');
    }
}
testExtension.ClientControl.registerClass('testExtension.ClientControl', Sys.UI.Control);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
