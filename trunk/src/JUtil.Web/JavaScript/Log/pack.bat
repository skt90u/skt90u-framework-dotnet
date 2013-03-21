set pack=%1

set dependences= Dependence\jquery.sprintf.js, Dependence\jquery.browser.extension.js, Log.js, Impl\ChromeConsole.js, Impl\SafariConsole.js, Impl\FirefoxConsole.js, Impl\IEConsole.js, Impl\log4javascript.js, Impl\EmptyConsole.js

for %%A in (%dependences%) do type %%A >> %pack%