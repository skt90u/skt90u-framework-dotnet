set pack=%1

set dependences= jquery.alerts.js

for %%A in (%dependences%) do type %%A >> %pack%