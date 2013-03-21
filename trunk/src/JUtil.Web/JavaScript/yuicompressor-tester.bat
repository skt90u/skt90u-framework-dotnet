@echo off

setlocal

set curDIR=E:\GoogleProjectHosting\jelly-dotnet-framework\src\JUtil.Web\JavaScript
set yuicompressor=%curDIR%\yuicompressor-2.4.6.jar

set debugFile=ControlAccessor\ControlAccessor.js
rem set debugFile=ControlAccessor\Impl\AspNetControlHelperManager.js
rem set debugFile=ControlAccessor\Impl\ControlAccessorImpl\CheckBoxListHelper.js
rem set debugFile=ControlAccessor\Impl\ControlAccessorImpl\ddComboHelper.js
rem set debugFile=ControlAccessor\Impl\ControlAccessorImpl\DropDownListHelper.js
rem set debugFile=ControlAccessor\Impl\ControlAccessorImpl\LabelHelper.js
rem set debugFile=ControlAccessor\Impl\ControlAccessorImpl\RadioButtonListHelper.js
rem set debugFile=ControlAccessor\Impl\ControlAccessorImpl\TextBoxHelper.js
rem set debugFile=Framework\Interface.js
rem set debugFile=Framework\jquery.js
rem set debugFile=Log\Log.js
rem set debugFile=Log\Dependence\jquery.browser.extension.js
rem set debugFile=Log\Dependence\jquery.sprintf.js
rem set debugFile=Log\Impl\ChromeConsole.js
rem set debugFile=Log\Impl\EmptyConsole.js
rem set debugFile=Log\Impl\FirefoxConsole.js
rem set debugFile=Log\Impl\IEConsole.js
rem set debugFile=Log\Impl\SafariConsole.js

echo %debugFile%

java -jar %yuicompressor% --type js --charset utf-8 -o outcome.js %debugFile%

endlocal