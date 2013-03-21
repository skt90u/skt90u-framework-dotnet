set pack=%1

set dependences= WebService.js

for %%A in (%dependences%) do type %%A >> %pack%