set pack=%1

set dependences= Interface.js, jquery.js, json2.js

for %%A in (%dependences%) do type %%A >> %pack%