set pack=%1

set dependences= AjaxControlToolKitPlugin.js

for %%A in (%dependences%) do type %%A >> %pack%